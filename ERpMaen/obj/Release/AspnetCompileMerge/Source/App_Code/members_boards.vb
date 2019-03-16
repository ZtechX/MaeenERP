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
Public Class members_boards
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
    Public Function save_members(ByVal SaveType As String, ByVal id As String, ByVal basicDataJson As Object, ByVal imagePath As String) As Boolean
        Dim company_id = 0
        Dim dt_user As DataTable
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction

            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            dictBasicDataJson.Add("image", imagePath)
            dictBasicDataJson.Add("comp_id", dt_user.Rows(0).Item("comp_id"))
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblMembers", id, _sqlconn, _sqltrans) Then
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
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("tblMembers", editItemId)
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
#Region "get_member_boards"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_member_boards(ByVal boards As String) As String()
        Dim dt_members As DataTable
        Dim dt_user As DataTable
        Dim dt_boards As DataTable
        Dim dt As DataTable
        Dim Names As New List(Of String)(10)
        Try
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            dt_members = DBManager.Getdatatable("SELECT * from tblMembers  where  comp_id= " + dt_user.Rows(0).Item("comp_id").ToString())
            dt_boards = DBManager.Getdatatable("SELECT * from tblboards_member  where  comp_id= " + dt_user.Rows(0).Item("comp_id").ToString() + " and board_id=" + boards.ToString)
            If dt_boards.Rows.Count = 0 Then
                If dt_members.Rows.Count <> 0 Then
                    Dim id = 0
                    For Each row2 As DataRow In dt_members.Rows
                        Dim dict = New Dictionary(Of String, Object)
                        dict.Add("member_id", row2.Item("id").ToString)
                        dict.Add("board_id", boards.ToString)
                        dict.Add("comp_id", dt_user.Rows(0).Item("comp_id").ToString)
                        dict.Add("status", 0)
                        PublicFunctions.TransUpdateInsert(dict, "tblboards_member", id, _sqlconn, _sqltrans)
                    Next
                End If
            End If
            dt = DBManager.Getdatatable("SELECT  tblMembers.id as member_id,* from tblboards_member right join tblMembers on tblboards_member.member_id=tblMembers.id   where  tblboards_member.board_id=" + boards.ToString)
            If dt.Rows.Count <> 0 Then
                Dim str3 = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add(str3)
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

#Region "save_member_boards"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_member_boards(ByVal basicDataJson As List(Of Object), ByVal boards As String) As Boolean
        Dim dt_members As DataTable
        Dim dt_user As DataTable
        Dim Names As New List(Of String)(10)
        Try
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            For Each DetailsJSON As Dictionary(Of String, Object) In basicDataJson
                Dim dictBasicDataJson As Dictionary(Of String, Object) = DetailsJSON
                Dim dict = New Dictionary(Of String, Object)
                dt_members = DBManager.Getdatatable("SELECT * from tblboards_member  where  comp_id= " + dt_user.Rows(0).Item("comp_id").ToString() + " and board_id=" + boards.ToString + " and member_id=" + dictBasicDataJson("member_id").ToString)
                Dim id = dt_members.Rows(0).Item("id").ToString
                dict.Add("status", 1)
                PublicFunctions.TransUpdateInsert(dict, "tblboards_member", id, _sqlconn, _sqltrans)
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "disapproved"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function disapproved(ByVal basicDataJson As List(Of Object), ByVal boards As String) As Boolean
        Dim dt_members As DataTable
        Dim dt_user As DataTable
        Dim Names As New List(Of String)(10)
        Try
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            For Each DetailsJSON As Dictionary(Of String, Object) In basicDataJson
                Dim dictBasicDataJson As Dictionary(Of String, Object) = DetailsJSON
                Dim dict = New Dictionary(Of String, Object)
                dt_members = DBManager.Getdatatable("SELECT * from tblboards_member  where  comp_id= " + dt_user.Rows(0).Item("comp_id").ToString() + " and board_id=" + boards.ToString + " and member_id=" + dictBasicDataJson("member_id").ToString)
                Dim id = dt_members.Rows(0).Item("id").ToString
                dict.Add("status", 0)
                PublicFunctions.TransUpdateInsert(dict, "tblboards_member", id, _sqlconn, _sqltrans)
            Next
            Return True
        Catch ex As Exception
            Return False
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