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
Public Class archive_files
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
    Public Function Save(ByVal id As String, ByVal basicDataJson As Object, ByVal attch_file_DataJsonList As List(Of Object)) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            dictBasicDataJson.Add("comp_id", dt_user.Rows(0).Item("comp_id"))
            dictBasicDataJson("add_by") = Context.Request.Cookies("UserInfo")("UserId").ToString()
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblarchive_files", id, _sqlconn, _sqltrans) Then
                Dim letter_id
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
            Dim str As String = PublicFunctions.GetDataForUpdate("tblarchive_files", editItemId)
            Names.Add("1")
            Names.Add(str)


            Dim dtattch_file As New DataTable
            Dim str1 As String = ""
            dtattch_file = DBManager.Getdatatable("Select * from tblImages where Source='archive_files' and isnull(deleted,0)=0  and Source_id=" + editItemId.ToString())
            If dtattch_file.Rows.Count <> 0 Then
                str1 = PublicFunctions.ConvertDataTabletoString(dtattch_file)
                Names.Add(str1)
            Else
                Names.Add("")
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
    Public Function Delete(ByVal deleteItem As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "tblarchive_files") Then
                DBManager.ExcuteQuery("delete from tblImages where Source_id=" + deleteItem.ToString + " and Source='archive_files' ")
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

    ''' <summary>"
    ''' Save About images into db 
    ''' </summary>
    Private Function Savea_ttch_file(ByVal attch_file_DataJsonList As List(Of Object), ByVal letter_Id As String) As Boolean
        Try
            DBManager.ExcuteQuery("delete from tblImages where Source_id=" + letter_Id.ToString + " and Source='archive_files' ")
            For Each file_JSON As Object In attch_file_DataJsonList

                Dim dictfile As Dictionary(Of String, Object) = file_JSON
                Dim Source_id = letter_Id
                Dim Source = "archive_files"
                Dim Image_path = dictfile("url").ToString
                Dim Image_name = dictfile("Name").ToString
                If dictfile("Name").ToString <> "" Then
                    Image_name = dictfile("Name").ToString.Substring(0, Math.Min(dictfile("Name").ToString.Length, 50))
                End If

                DBManager.ExcuteQuery("insert into tblImages(Source_id,Source,Image_path,Image_name) values(" + Source_id.ToString + ",'" + Source.ToString + "','" + Image_path.ToString + "','" + Image_name.ToString + "')")



            Next

            Return True

        Catch ex As Exception
            DBManager.ExcuteQuery("delete from tblImages where Source_id=" + letter_Id.ToString + " and Source='archive_files' ")
            Return False
        End Try
    End Function

    <WebMethod()>
   <System.Web.Script.Services.ScriptMethod()>
    Public Function getBarCode() As String
        Dim dt As New DataTable
        Dim val = ""
        dt = DBManager.Getdatatable("SELECT isNull (parcode,'') parcode FROM tblarchive_files where id= (select max(id) from tblarchive_files)")
        If dt.Rows.Count <> 0 Then
            val = dt.Rows(0)("parcode").ToString
        End If
        Dim curr_dt = Date.Now
        Dim year = curr_dt.Year.ToString
        If val <> "" Then
            Dim arr = val.Split("-")
            If arr.Length = 2 Then
                If year = arr(0) Then
                    Return year + "-" + (Convert.ToInt32(arr(1)) + 1).ToString
                Else
                    Return year + "-1"
                End If
            Else
                Return year + "-1"
            End If
        Else
            Return year + "-1"
        End If
    End Function
End Class