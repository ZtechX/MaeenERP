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
            Dim owner_phone As String = "0"
            Dim P1_id As String = "0"
            Dim P1_name As String = ""
            Dim p1_phone As String = "0"
            Dim P2_id As String = "0"
            Dim P2_name As String = ""
            Dim p2_phone As String = "0"
            Dim type As String = ""
            Dim order_type As String = ""
            Dim m_dt As String = ""
            Dim h_dt As String = ""
            Dim message As String = ""
            Dim addAction As Boolean = False
            Dim event_id = "-1"
            Dim advisor_phone = "0"
            Dim AssignedTo_id = ""
            Dim AssignedTo_phone = "0"
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select advis.id as advisor_id,advis.User_PhoneNumber as advisor_phone,isNull(owner_id,0) as owner,isNull(event_id,-1) as event_id," +
            " p1.User_PhoneNumber as p1_phone,isNull(p1.full_name,'') as p1_name, isNull(p1.id,0) as p1_id," +
            " p2.User_PhoneNumber as p2_phone,isNull(p2.full_name,'') as p2_name,isNull(p2.id,0) as p2_id," +
            " case ash_orders.type when 1 then 'تاجيل' when 2 then 'إلغاء' end as 'order_type'" +
            " ,new_date_m,new_date_h,case details.type when 1 then 'استلام وتسليم أولاد'when 1 then 'استلام وتسليم نفقة' end as type,details.date_m,details.date_h" +
            " From ash_orders left Join ash_case_receiving_delivery_details details on details.id=event_id" +
            " left join tblUsers p1 on p1.related_id=details.deliverer_id and p1.User_Type=9" +
            " left join tblUsers p2 on p2.related_id=details.reciever_id and p2.User_Type=9" +
            " left join tblUsers advis on p2.related_id=details.advisor and p2.User_Type=6" +
            " where  ash_orders.id=" + id)
            If dt.Rows.Count <> 0 Then
                advisor_phone = dt.Rows(0).Item("advisor_phone").ToString()
                owner_id = dt.Rows(0).Item("owner").ToString()
                P1_id = dt.Rows(0).Item("p1_id").ToString()
                P1_name = dt.Rows(0).Item("p1_name").ToString()
                p1_phone = dt.Rows(0).Item("p1_phone").ToString()
                P2_id = dt.Rows(0).Item("p2_id").ToString()
                P2_name = dt.Rows(0).Item("p2_name").ToString()
                p2_phone = dt.Rows(0).Item("p2_phone").ToString()
                type = dt.Rows(0).Item("type").ToString()
                m_dt = PublicFunctions.ConvertNumbertoDate(dt.Rows(0).Item("date_m").ToString())
                h_dt = dt.Rows(0).Item("date_h").ToString()
                order_type = dt.Rows(0).Item("order_type").ToString()
                event_id = dt.Rows(0).Item("event_id").ToString()

                If P1_id <> owner_id Then
                    AssignedTo_id = P1_id
                    AssignedTo_phone = p1_phone
                    owner_phone = p2_phone
                Else
                    AssignedTo_id = P2_id
                    AssignedTo_phone = p2_phone
                    owner_phone = p1_phone
                End If
                If date_m <> "" Then
                    Dim date_old = Date.ParseExact(m_dt, "dd/MM/yyyy", Nothing)
                    Dim date_new = Date.ParseExact(date_m, "dd/MM/yyyy", Nothing)
                    Dim days = Convert.ToInt32(date_new.Subtract(date_old).Days())
                    If days <= 0 Then
                        Return "False|التاريخ الجديد يجب أن يكون أكبر من تاريخ المعاد الموافق  " + date_old
                    End If
                End If
                If LoginInfo.GetUser__Id() <> owner_id Then
                    addAction = True
                End If

                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction

                Dim dic As New Dictionary(Of String, Object)
                Dim value As String = ""
                Dim userType = LoginInfo.getUserType()
                Dim Note_title = ""
                Dim Day = DateTime.Now.ToString("dd/MM/yyyy")

                If userType = "6" Then
                    If Accept Then
                        value = "2"
                        message = "طلب " + order_type + " " + type + " المقرر فى يوم" + m_dt + "   " + h_dt
                        If order_type = "تاجيل" Then
                            message = message + " إلى اليوم الموافق " + date_m + "   " + date_h
                            Dim dict As New Dictionary(Of String, Object)
                            dict.Add("new_date_m", date_m)
                            dict.Add("new_date_h", date_h)
                            If Not PublicFunctions.TransUpdateInsert(dict, "ash_case_receiving_delivery_details", event_id, _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If
                        End If
                        Note_title = "طلب لتاجيل/إلغاء معاد"
                    Else
                        value = "3"
                        message = "تم رفض طلب " + order_type + " " + type + " المقرر فى يوم" + m_dt + "   " + h_dt + " يرجى الالتزام بالحضور فى المعاد"
                        Note_title = "رد على الطلب"
                        AssignedTo_id = owner_id
                        AssignedTo_phone = owner_phone
                    End If
                    Dim dictNotification As New Dictionary(Of String, Object)
                    dictNotification.Add("RefCode", id)
                    dictNotification.Add("RefType", 5)
                    dictNotification.Add("Date", Day)
                    dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                    dictNotification.Add("Remarks", message)
                    dictNotification.Add("FormUrl", "Aslah_Module/orders.aspx?id=" + id)
                    dictNotification.Add("NotTitle", Note_title)
                    dictNotification.Add("AssignedTo", AssignedTo_id)
                    If Not PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return "False|لم يتم الحفظ"
                    End If

                    If LoginInfo.SendSMS() Then
                        Dim dic_sms_archive As New Dictionary(Of String, Object)

                        dic_sms_archive.Add("user_id", LoginInfo.GetUser__Id())
                        dic_sms_archive.Add("Message", Note_title)
                        dic_sms_archive.Add("Send_To", AssignedTo_phone)
                        dic_sms_archive.Add("date_m", Day)
                        dic_sms_archive.Add("event_id", id)
                        dic_sms_archive.Add("Type", "order")
                        dic_sms_archive.Add("comp_id", LoginInfo.GetComp_id())

                        If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Return "False|لم يتم الحفظ"
                        End If
                    End If
                    dic.Add("status", value)

                ElseIf userType = "9" And addAction Then
                    dic.Add("otherP_Accept", Accept)

                    Dim dt_new = dt.Rows(0).Item("new_date_m").ToString
                    Dim dt_new_h = dt.Rows(0).Item("new_date_h").ToString
                    Dim dictNotification As New Dictionary(Of String, Object)
                    dictNotification.Add("RefCode", id)
                    dictNotification.Add("Date", Day)
                    dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                    dictNotification.Add("Remarks", "")
                    dictNotification.Add("FormUrl", "Aslah_Module/orders.aspx?id=" + id)
                    dictNotification.Add("NotTitle", "الاستجابة للطلب")
                    dictNotification.Add("AssignedTo", "")
                    If Accept Then
                        If order_type = "تاجيل" Then
                            dictNotification("Remarks") = "تم الموافقة على تاجيل" + " " + type + " المقرر فى يوم" + m_dt + "   " + h_dt + " إلى اليوم الموافق " + dt_new + "   " + dt_new_h
                            Dim dict As New Dictionary(Of String, Object)
                            dict.Add("date_m", PublicFunctions.ConvertNumbertoDate(dt_new))
                            dict.Add("date_h", dt.Rows(0).Item("new_date_h"))
                            If Not PublicFunctions.TransUpdateInsert(dict, "ash_case_receiving_delivery_details", event_id, _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If
                            If Not DBManager.ExcuteQueryTransaction("update tblNotifications set Date=" + dt_new + " where RefCode=" + event_id + " and RefType in (1,2,3,4)", _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If
                            If Not DBManager.ExcuteQueryTransaction("update tblsms_archive set date_m=" + dt_new + " where event_id=" + event_id + " and Type='recieve_delivery' ", _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If
                        ElseIf order_type = "إلغاء" Then
                            dictNotification("Remarks") = "تم الموافقة على إلغاء" + " " + type + " المقرر فى يوم" + m_dt + "   " + h_dt

                            Dim dict As New Dictionary(Of String, Object)
                            dict.Add("deleted", 1)
                            If Not PublicFunctions.TransUpdateInsert(dict, "ash_case_receiving_delivery_details", event_id, _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If
                            If Not DBManager.ExcuteQueryTransaction("delete from  tblNotifications  where RefCode=" + event_id + " and RefType in (1,2,3,4)", _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If
                            If Not DBManager.ExcuteQueryTransaction("delete from tblsms_archive where event_id=" + event_id + " and Type='recieve_delivery' ", _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If

                        End If
                    Else
                        If order_type = "تاجيل" Then
                            dictNotification("Remarks") = "تم رفض تاجيل" + " " + type + " المقرر فى يوم" + m_dt + "   " + h_dt + " إلى اليوم الموافق " + dt_new + "   " + dt_new_h
                        ElseIf order_type = "إلغاء" Then
                            dictNotification("Remarks") = "تم رفض إلغاء" + " " + type + " المقرر فى يوم" + m_dt + "   " + h_dt
                        End If
                    End If
                    dictNotification("AssignedTo") = owner_id
                    If Not PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return "False|لم يتم الحفظ"
                    End If
                    Dim dic_sms_archive As New Dictionary(Of String, Object)
                    If LoginInfo.SendSMS() Then
                        dic_sms_archive.Add("user_id", LoginInfo.GetUser__Id())
                        dic_sms_archive.Add("Message", dictNotification("Remarks"))
                        dic_sms_archive.Add("Send_To", owner_phone)
                        dic_sms_archive.Add("date_m", Day)
                        dic_sms_archive.Add("event_id", id)
                        dic_sms_archive.Add("Type", "order")
                        dic_sms_archive.Add("comp_id", LoginInfo.GetComp_id())

                        If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Return "False|لم يتم الحفظ"
                        End If
                        If Not Accept Then
                            dic_sms_archive("Send_To") = advisor_phone
                            If Not PublicFunctions.TransUpdateInsert(dic_sms_archive, "tblsms_archive", "", _sqlconn, _sqltrans) Then
                                _sqltrans.Rollback()
                                _sqlconn.Close()
                                Return "False|لم يتم الحفظ"
                            End If
                        End If

                    End If
                    If Not Accept Then
                        dictNotification("AssignedTo") = dt.Rows(0).Item("advisor_id").ToString()
                        If Not PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Return "False|لم يتم الحفظ"
                        End If
                    End If

                Else
                    Return "False|ليس لديك الصلاحية لأخد الاجراء"
                End If

                If PublicFunctions.TransUpdateInsert(dic, "ash_orders", id, _sqlconn, _sqltrans) Then
                    _sqltrans.Commit()
                    _sqlconn.Close()
                    Return "True"
                End If
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