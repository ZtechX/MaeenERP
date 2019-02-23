

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
Public Class members
    Inherits System.Web.Services.WebService

#Region "Global_Variables"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
#End Region
#Region "save_members"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_members(ByVal id As String, ByVal basicDataJson As Object, ByVal imagePath As String, ByVal user_id As String) As Boolean
        Dim company_id = 0
        Dim dt_user As DataTable
        Dim dt As DataTable
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("image", imagePath)
            dictBasicDataJson.Add("comp_id", dt_user.Rows(0).Item("comp_id"))
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblMembers", id, _sqlconn, _sqltrans) Then
                Dim related = 0
                If id <> "" Then
                    related = id
                Else
                    related = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                End If
                If dictBasicDataJson("system_enter").ToString = "True" Or dictBasicDataJson("system_enter").ToString = "1" Then
                    Dim dict_user = New Dictionary(Of String, Object)
                    dict_user.Add("User_Type", 3)
                    dict_user.Add("managment_id", 1)
                    dict_user.Add("jop_id", 1)
                    dict_user.Add("User_Image", imagePath)
                    dict_user.Add("related_id", related)
                    dict_user.Add("full_name", dictBasicDataJson("name"))
                    dict_user.Add("User_Name", dictBasicDataJson("User_Name"))
                    dict_user.Add("User_Password", dictBasicDataJson("User_Password"))
                    If id <> "" Then
                        dt = DBManager.Getdatatable("Select * from tblUsers where related_id=" + id.ToString)
                        If dt.Rows.Count <> 0 Then
                            id = dt.Rows(0).Item("id")
                        Else
                            id = ""
                        End If
                    Else
                        id = ""
                    End If


                    PublicFunctions.TransUpdateInsert(dict_user, "tblUsers", id, _sqlconn, _sqltrans)
                    DBManager.ExcuteQuery("delete from tblPermissions where FormId=2132 and UserId=" + user_id.ToString)
                    Dim dict = New Dictionary(Of String, Object)
                    dict.Add("UserId", user_id)
                    dict.Add("FormId", "2131")
                    dict.Add("PAccess", 1)
                    dict.Add("PAdd", 1)
                    dict.Add("PEdite", 1)
                    dict.Add("PDelete", 1)
                    dict.Add("PSearch", 1)
                    dict.Add("PPrint", 1)
                    PublicFunctions.TransUpdateInsert(dict, "tblPermissions", id, _sqlconn, _sqltrans)
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
        Dim Names As New List(Of String)(10)
        Dim dtm As DataTable
        Dim dt As DataTable
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("tblMembers", editItemId)
            dt = DBManager.Getdatatable("Select * from tblMembers where id=" + editItemId.ToString)
            Names.Add("1")
            Names.Add(str)
            If dt.Rows(0).Item("system_enter") = "1" Then
                dtm = DBManager.Getdatatable("Select * from tblUsers where related_id=" + editItemId.ToString)
                Dim str2 = PublicFunctions.ConvertDataTabletoString(dtm)
                Names.Add(str2)
            End If
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
    Public Function Delete(ByVal deleteItems As String) As String()
        Dim Names As New List(Of String)(10)
        Dim dt As DataTable
        Try

            If PublicFunctions.DeleteFromTable(deleteItems, "tblMembers") Then
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
            DBManager.ExcuteQuery("delete from tblImages where Source_id=" + letter_Id.ToString + " and Source='images_contact' ")
            For Each file_JSON As Object In attch_file_DataJsonList

                Dim dictfile As Dictionary(Of String, Object) = file_JSON
                Dim Source_id = letter_Id
                Dim Source = "images_contact"
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