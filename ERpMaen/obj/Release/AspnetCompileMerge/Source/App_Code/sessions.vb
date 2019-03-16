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
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<System.Web.Script.Services.ScriptService()>
Public Class sessions
    Inherits System.Web.Services.WebService

#Region "Global_Variables"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
#End Region
#Region "save_sessions"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_sessions(ByVal id As String, ByVal basicDataJson As Object, ByVal attch_file_DataJsonList As List(Of Object)) As Boolean
        Dim company_id = 0
        Dim dt_user As DataTable
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            dictBasicDataJson.Add("comp_id", dt_user.Rows(0).Item("comp_id"))
            dictBasicDataJson("date_m") = PublicFunctions.ConvertDatetoNumber(dictBasicDataJson("date_m"))
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblSessions", id, _sqlconn, _sqltrans) Then
                Dim letter_id = 0
                If id <> "" Then
                    letter_id = id
                Else
                    letter_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                End If

                If Savea_ttch_file(attch_file_DataJsonList, letter_id) Then
                    _sqltrans.Commit()
                    _sqlconn.Close()
                    Return True
                Else
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If
                _sqltrans.Commit()
                _sqlconn.Close()
                Return True
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try
    End Function
#End Region

#Region "Get data"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Get_data() As String()
        Dim dtm As New DataTable
        Dim dtm2 As New DataTable
        Dim dtm3 As New DataTable
        Dim Names As New List(Of String)(10)
        dtm = DBManager.Getdatatable("Select * from tbllock_up where ISNUll(deleted, 0) = 0 And Type ='PPT'")

        Dim str As String = PublicFunctions.ConvertDataTabletoString(dtm)
        If dtm.Rows.Count <> 0 Then
            Names.Add(str)
        Else
            Names.Add("1")
        End If
        Return Names.ToArray
    End Function
#End Region

#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Edit(ByVal editItemId As String) As String()
        Dim unitsId = editItemId
        Dim dtattch_file As DataTable
        Dim dt_sessions As DataTable
        Dim Names As New List(Of String)(10)
        Try
            dt_sessions = DBManager.Getdatatable("SELECT * from tblSessions  where  tblSessions.id = " + editItemId)
            dtattch_file = DBManager.Getdatatable("Select * from tblImages where Source='session_images' and isnull(deleted,0)=0  and Source_id=" + editItemId.ToString())
            If dt_sessions.Rows.Count <> 0 Then
                Dim str As String = PublicFunctions.ConvertDataTabletoString(dt_sessions)
                Names.Add(str)
            Else
                Names.Add("0")
            End If

            If dtattch_file.Rows.Count <> 0 Then
                Dim str1 As String = PublicFunctions.ConvertDataTabletoString(dtattch_file)
                Names.Add(str1)
            Else
                Names.Add("0")
            End If

        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
        Return Names.ToArray
    End Function

#End Region
#Region "Delete"
    ''' <summary>
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Delete(ByVal deleteItems As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItems, "tblSessions") Then
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

#Region "get_main_menu"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_main_menu(ByVal editItemId As String) As String()
        Dim UserId = editItemId
        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("SELECT  * from tblMenus ")
            If dt.Rows.Count <> 0 Then
                Dim str As String = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add(str)
                Return Names.ToArray
            Else
                Names.Add("0")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "save_file"
    ''' <summary>"
    ''' Save About images into db 
    ''' </summary>
    Private Function Savea_ttch_file(ByVal attch_file_DataJsonList As List(Of Object), ByVal letter_Id As String) As Boolean
        Try
            DBManager.ExcuteQuery("delete from tblImages where Source_id=" + letter_Id.ToString + " and Source='session_images' ")
            For Each file_JSON As Object In attch_file_DataJsonList

                Dim dictfile As Dictionary(Of String, Object) = file_JSON
                Dim Source_id = letter_Id
                Dim Source = "session_images"
                Dim Image_path = dictfile("url").ToString
                Dim Image_name = dictfile("Name").ToString
                If dictfile("Name").ToString <> "" Then
                    Image_name = dictfile("Name").ToString.Substring(0, Math.Min(dictfile("Name").ToString.Length, 50))
                End If

                DBManager.ExcuteQuery("insert into tblImages(Source_id,Source,Image_path,Image_name) values(" + Source_id.ToString + ",'" + Source.ToString + "','" + Image_path.ToString + "','" + Image_name.ToString + "')")
            Next

            Return True

        Catch ex As Exception
            DBManager.ExcuteQuery("delete from tblImages where Source_id=" + letter_Id.ToString + " and Source='out_letter' ")
            Return False
        End Try
    End Function
#End Region
#Region "get_admin"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_admin() As String()
        Dim dt_companies As DataTable
        Dim dt_user As DataTable
        Dim Names As New List(Of String)(10)
        Try
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            dt_companies = DBManager.Getdatatable("SELECT * from tblcompanies  where  id= " + dt_user.Rows(0).Item("comp_id").ToString())
            If dt_companies.Rows.Count <> 0 Then
                Dim str2 = PublicFunctions.ConvertDataTabletoString(dt_companies)
                Names.Add(str2)
            Else
                Names.Add("0")
            End If

            If dt_user.Rows.Count <> 0 Then
                Dim str3 = PublicFunctions.ConvertDataTabletoString(dt_user)
                Names.Add(str3)
            Else
                Names.Add("0")
            End If
            If LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")) <> 0 Then
                Names.Add(LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")))
            Else
                Names.Add("0")
            End If
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "check_user"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function check_user(ByVal user_name As String) As Boolean
        Dim Names As New List(Of String)(10)
        Dim str = ""
        Dim str2 = ""
        Try
            Dim dt As New DataTable
            Dim dt2 As New DataTable
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where User_Name='" + user_name.ToString + "'")
            If dt_user.Rows.Count <> 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region




End Class