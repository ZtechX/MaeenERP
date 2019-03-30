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
Public Class advisor
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
    Public Function Save(ByVal id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As String

        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim group_id = ""
            Dim user_id = ""
            Dim dt As New DataTable
            If id = "" Then
                dt = DBManager.Getdatatable("select id from tbllock_up where RelatedId=2 and Type='PG' and Comp_id=" + LoginInfo.GetComp_id())
                If dt.Rows.Count <> 0 Then
                    group_id = dt.Rows(0)(0).ToString
                End If
            Else
                dt = DBManager.Getdatatable("select id from tblUsers where related_id=" + id)
                If dt.Rows.Count <> 0 Then
                    user_id = dt.Rows(0)(0).ToString
                End If
            End If
                Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("comp_id", LoginInfo.GetComp_id())
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_advisors", id, _sqlconn, _sqltrans) Then

                Dim dictBasicDataJson1 As New Dictionary(Of String, Object)
                dictBasicDataJson1.Add("full_name", dictBasicDataJson("name"))
                'dictBasicDataJson1.Add("User_Name", dictBasicDataJson("user_nm"))
                dictBasicDataJson1.Add("User_Email", dictBasicDataJson("email"))
                dictBasicDataJson1.Add("Active", dictBasicDataJson("active"))
                dictBasicDataJson1.Add("User_Password", dictBasicDataJson("password"))
                dictBasicDataJson1.Add("User_PhoneNumber", dictBasicDataJson("tel"))
                dictBasicDataJson1.Add("user_indenty", dictBasicDataJson("advisor_identiy"))
                dictBasicDataJson1.Add("comp_id", LoginInfo.GetComp_id())
                dictBasicDataJson1.Add("User_Type", 6)
                If id = "" Then
                    dictBasicDataJson1.Add("related_id", PublicFunctions.GetIdentity(_sqlconn, _sqltrans))
                    dictBasicDataJson1.Add("group_id", group_id)
                Else
                    dictBasicDataJson1.Add("related_id", id)
                End If
                Dim dtcheckemailphone As New DataTable
                If id = "" Then
                    dtcheckemailphone = DBManager.Getdatatable("Select * from TblUsers where user_indenty='" + dictBasicDataJson1("user_indenty") + "' or User_PhoneNumber='" + dictBasicDataJson1("User_PhoneNumber") + "'")
                Else
                    dtcheckemailphone = DBManager.Getdatatable("Select * from TblUsers where ( user_indenty='" + dictBasicDataJson1("user_indenty") + "' or User_PhoneNumber='" + dictBasicDataJson1("User_PhoneNumber") + "')  and  Id!='" + user_id + "'")
                End If
                If dtcheckemailphone.Rows.Count = 0 Then
                    If Not PublicFunctions.TransUpdateInsert(dictBasicDataJson1, "tblUsers", user_id, _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return "False|لم يتم الحفظ"
                    End If
                Else
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return "False|رقم الهوية او التلفون مُستخدم"
                End If

                _sqltrans.Commit()
                _sqlconn.Close()
                Return "True"
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return "False|لم يتم الحفظ"
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return "False|لم يتم الحفظ"
        End Try
    End Function
#End Region


#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_data(ByVal comp_id As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select * from ash_advisors where comp_id=" + comp_id)

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
        Names.Add("0")
        Names.Add(" No Results were Found!")
        Return Names.ToArray
    End Function

#End Region

#Region "Check user"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function checkUser() As String

        Try
            Dim dt As New DataTable
            Dim UserId As String = HttpContext.Current.Request.Cookies("UserInfo")("UserId")
            dt = DBManager.Getdatatable("select isNull(related_id,'0') related_id,User_Type  from tblUsers where id=" + UserId)
            If dt.Rows.Count <> 0 Then
                Dim user_type = dt.Rows(0)("User_Type").ToString
                If user_type = "1" Or user_type = "2" Then
                    Return "Superadmin"
                End If
                Return dt.Rows(0)("related_id").ToString
            End If

        Catch ex As Exception

            Return ""
        End Try
        Return ""
    End Function

#End Region


#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Edit(ByVal editItemId As String) As String

        Dim Names As New List(Of String)(10)
        Try
            Return PublicFunctions.ConvertDataTabletoString(DBManager.Getdatatable("SELECT User_Password as 'password',ash_advisors.id,ash_advisors.comp_id ,code ,name ,specialty ,tel ,email ,ash_advisors.active,advisor_identiy  FROM ash_advisors left join tblUsers on ash_advisors.id=tblUsers.related_id where ash_advisors.id=" + editItemId))
        Catch ex As Exception
            Return ""
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
            If PublicFunctions.DeleteFromTable(deleteItem, "ash_advisors") Then
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



#Region "get Advisor Code"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getAdvisorCode() As String

        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select isNull(Max(code),0) from ash_advisors")
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