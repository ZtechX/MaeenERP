#Region "Import"
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Web.Script.Services
Imports BusinessLayer.BusinessLayer
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Generic
Imports System
Imports ERpMaen

#End Region

'Imports System.Xml
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<System.Web.Script.Services.ScriptService()>
Public Class orders
    Inherits System.Web.Services.WebService

#Region "Global_Variables"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
#End Region

#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Save(ByVal id As String, ByVal Accept As String, ByVal date_m As String, ByVal date_h As String) As String

        Try
            Dim owner_id As String = "0"
            Dim P1_id As String = "0"
            Dim P2_id As String = "0"
            Dim type As String = ""
            Dim order_type As String = ""
            Dim m_dt As String = ""
            Dim h_dt As String = ""
            Dim message As String = ""
            Dim addAction As Boolean = False
            Dim event_id = "-1"
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select isNull(owner_id,0) as owner ,isNull(event_id,-1) as event_id,isNull(p1_id.id,0) as p1_id,isNull(p2_id.id,0) as p2_id," +
            " case ash_orders.type when 1 then 'تاجيل' when 2 then 'إلغاء' end as 'order_type'" +
            " case details.type when 1 then 'استلام وتسليم أولاد'when 1 then 'استلام وتسليم نفقة' end as type,details.date_m,details.date_h" +
            " From ash_orders left Join ash_case_receiving_delivery_details details on details.id=event_id" +
            " left join tblUsers p1_id on p1_id.related_id=details.deliverer_id and p1_id.User_Type=9" +
            " left join tblUsers p2_id on p2_id.related_id=details.reciever_id and p2_id.User_Type=9" +
            " where ash_orders.id=" + id)
            If dt.Rows.Count <> 0 Then
                owner_id = dt.Rows(0).Item("owner").ToString()
                P1_id = dt.Rows(0).Item("p1_id").ToString()
                P2_id = dt.Rows(0).Item("p2_id").ToString()
                type = dt.Rows(0).Item("type").ToString()
                m_dt = dt.Rows(0).Item("date_m").ToString()
                h_dt = dt.Rows(0).Item("date_h").ToString()
                order_type = dt.Rows(0).Item("order_type").ToString()
                event_id = dt.Rows(0).Item("event_id").ToString()
                If LoginInfo.GetUser__Id() <> owner_id Then
                    addAction = True
                End If
            End If
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction

            Dim dic As New Dictionary(Of String, Object)
            Dim value As String = ""
            Dim userType = LoginInfo.getUserType()
            Dim dictNotification As New Dictionary(Of String, Object)
            dictNotification.Add("RefCode", id)
            dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
            dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
            dictNotification.Add("Remarks", "الاستجابة للطلب")
            dictNotification.Add("FormUrl", "Aslah_Module/orders.aspx?id=" + id)
            dictNotification.Add("AssignedTo", "")
            If userType = "6" Then
                If Accept Then
                    Dim dict As New Dictionary(Of String, Object)
                    dict.Add("date_m", date_m)
                    dict.Add("date_h", date_h)
                    If Not PublicFunctions.TransUpdateInsert(dict, "ash_case_receiving_delivery_details", event_id, _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return "False|لم يتم الحفظ"
                    End If
                    value = "2"
                    message = "تم الموافقة على طلب " + order_type + " " + type + " المقرر فى يوم" + m_dt + "   " + h_dt
                    If order_type = "تاجيل" Then
                        message = " إلى اليوم الموافق " + date_m + "   " + date_h
                    End If
                Else
                    value = "3"
                    message = "تم رفض طلب " + order_type + " " + type + " المقرر فى يوم" + m_dt + "   " + h_dt + " يرجى الالتزام بالحضور فى المعاد"

                End If
                dictNotification.Add("NotTitle", message)
                dictNotification("AssignedTo") = P1_id
                If Not PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return "False|لم يتم الحفظ"
                End If
                dictNotification("AssignedTo") = P2_id
                If Not PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return "False|لم يتم الحفظ"
                End If
                dic.Add("status", value)
            ElseIf userType = "9" And addAction Then
                dic.Add("otherP_Accept", Accept)
            Else
                Return "False|ليس لديك الصلاحية لأخد الاجراء"
            End If
            If PublicFunctions.TransUpdateInsert(dic, "ash_orders", id, _sqlconn, _sqltrans) Then
                _sqltrans.Commit()
                _sqlconn.Close()
                Return "True"
            End If
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return "False|لم يتم الحفظ"

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return "False|لم يتم الحفظ"
        End Try
    End Function
#End Region


End Class