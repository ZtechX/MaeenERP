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
Public Class ConsultationDetails
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
    Public Function Save(ByVal id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            If id = "" Then
                dictBasicDataJson.Add("comp_id", LoginInfo.GetComp_id())
                dictBasicDataJson.Add("created_By", LoginInfo.GetUser__Id())
            End If

            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_consultings", id, _sqlconn, _sqltrans) Then
                Dim admin_id = ""
                Dim dt As DataTable
                dt = DBManager.Getdatatable("select id from tblUsers where superAdmin=1 and comp_id=" + LoginInfo.GetComp_id())
                If dt.Rows.Count <> 0 Then
                    admin_id = dt.Rows(0)(0).ToString
                End If
                Dim dictBasicDataJson1 As New Dictionary(Of String, Object)
                Dim Con_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                dictBasicDataJson1.Add("RefCode", Con_id)
                dictBasicDataJson1.Add("NotTitle", "إنشاء استشارة")
                dictBasicDataJson1.Add("Date", dictBasicDataJson("start_date").ToString)
                dictBasicDataJson1.Add("AssignedTo", admin_id)
                dictBasicDataJson1.Add("CreatedBy", LoginInfo.GetUser__Id())
                dictBasicDataJson1.Add("Remarks", "استشارة")
                dictBasicDataJson1.Add("FormUrl", "Aslah_Module/ConsultationDetails.aspx?id=" + Con_id)
                If PublicFunctions.TransUpdateInsert(dictBasicDataJson1, "tblNotifications", "", _sqlconn, _sqltrans) Then
                    _sqltrans.Commit()
                    _sqlconn.Close()
                    Return True
                End If
            End If
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try
    End Function
#End Region

#Region "add Advisor"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function addAdvisor(ByVal consulat_id As String, ByVal advisor_id As String, ByVal oldadvisor_id As String) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction

            If DBManager.ExcuteQuery("update ash_consultings set  advisor_id=" + advisor_id + " where id=" + consulat_id) <> -1 Then

                Dim dictBasicDataJson1 As New Dictionary(Of String, Object)
                dictBasicDataJson1.Add("RefCode", consulat_id)
                dictBasicDataJson1.Add("NotTitle", "إسناد استشارة")
                dictBasicDataJson1.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                dictBasicDataJson1.Add("AssignedTo", getadvisorUser_id(advisor_id))
                dictBasicDataJson1.Add("CreatedBy", LoginInfo.GetUser__Id())
                dictBasicDataJson1.Add("Remarks", "استشارة")
                dictBasicDataJson1.Add("FormUrl", "Aslah_Module/ConsultationDetails.aspx")
                If PublicFunctions.TransUpdateInsert(dictBasicDataJson1, "tblNotifications", "", _sqlconn, _sqltrans) Then
                    _sqltrans.Commit()
                    _sqlconn.Close()
                    _sqlconn.Open()
                    _sqltrans = _sqlconn.BeginTransaction
                    If oldadvisor_id <> "0" Then
                        DBManager.ExcuteQuery("update tblNotifications set  Deleted=1 where RefCode=" + consulat_id + " and AssignedTo=" + getadvisorUser_id(oldadvisor_id) + " and NotTitle='إسناد استشارة'")
                        Dim dictBasicDataJson2 As New Dictionary(Of String, Object)
                        dictBasicDataJson2.Add("RefCode", consulat_id)
                        dictBasicDataJson2.Add("NotTitle", "إلغاءإسناد استشارة")
                        dictBasicDataJson2.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                        dictBasicDataJson2.Add("AssignedTo", getadvisorUser_id(oldadvisor_id))
                        dictBasicDataJson2.Add("CreatedBy", LoginInfo.GetUser__Id())
                        dictBasicDataJson2.Add("Remarks", "استشارة")
                        dictBasicDataJson2.Add("FormUrl", "Aslah_Module/ConsultationDetails.aspx")
                        If Not PublicFunctions.TransUpdateInsert(dictBasicDataJson2, "tblNotifications", "", _sqlconn, _sqltrans) Then
                            _sqltrans.Rollback()
                            _sqlconn.Close()
                            Return False
                        End If
                    End If

                    _sqltrans.Commit()
                    _sqlconn.Close()
                    Return True
                End If
            End If
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try
    End Function
#End Region
#Region "get advisor User_id"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getadvisorUser_id(ByVal id As String) As String
        Dim dt As New DataTable
        dt = DBManager.Getdatatable("select id from tblUsers where User_Type=6 and related_id=" + id)
        Dim str = ""
        If dt.Rows.Count <> 0 Then
            str = dt.Rows(0)(0).ToString
        End If
        Return str
    End Function

#End Region
#Region "Check user is SuperAdmin"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function isSuperAdmin() As Boolean

        Return LoginInfo.ConsultationSuperAdmin()

    End Function

#End Region


#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Edit(ByVal editItemId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("ash_consultings", editItemId)
            Names.Add("1")
            Names.Add(str)
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "Delete"
    ''' <summary>
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Delete(ByVal deleteItem As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "ash_consultings") Then
                DBManager.ExcuteQuery("delete from ash_consulting_messages where consulting_id=" + deleteItem)
                DBManager.ExcuteQuery("upgate tblNotifications set Deleted=1 where RefCode=" + deleteItem + " and Remarks='استشارة'")
                Names.Add("1")
                Names.Add("تم الحذف بنجاح!")

            Else
                    Names.Add("2")
                Names.Add("لا يمكن الحذف!")
            End If
            Return Names.ToArray
        Catch
            Names.Add("2")
            Names.Add("لا يمكن الحذف!")
            Return Names.ToArray
        End Try
    End Function
#End Region

#Region "get Consult Num"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getConsultNum() As String

        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select isNull(Max(code),0) from ash_consultings")
            If dt.Rows.Count <> 0 Then
                Return dt.Rows(0)(0).ToString
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region

#Region "get advisor"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getAdvisor(ByVal consulat_id As String) As String

        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select isNull(advisor_id, 0) from ash_consultings where id=" + consulat_id)
            If dt.Rows.Count <> 0 Then
                Return dt.Rows(0)(0).ToString
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region

End Class