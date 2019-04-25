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
Public Class register_company
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
    Public Function Save(ByVal id As String, ByVal basicDataJson As List(Of Object), ByVal attch_file_DataJsonList As List(Of Object)) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim DetailsJSON1 As Dictionary(Of String, Object) = basicDataJson(0)
            Dim dt_user As DataTable
            Dim result = 0
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If DBManager.ExcuteQueryTransaction("delete  from tblregist_settings where category_id=" + DetailsJSON1("category_id").ToString, _sqlconn, _sqltrans) = -1 Then
                _sqltrans.Rollback()
                Return False
            End If
            For Each DetailsJSON As Dictionary(Of String, Object) In basicDataJson
                Dim dictBasicDataJson As Dictionary(Of String, Object) = DetailsJSON
                dictBasicDataJson.Add("comp_id", dt_user.Rows(0).Item("comp_id"))
                id = 0
                PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblregist_settings", id, _sqlconn, _sqltrans)
                result = 1
            Next
            Dim letter_id = 0
            letter_id = DetailsJSON1("category_id").ToString
            Savea_ttch_file(attch_file_DataJsonList, letter_id)

            If result = 1 Then
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


#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Edit(ByVal editItemId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("tblcontacts_groups", editItemId)
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
        Try
            If PublicFunctions.DeleteFromTable(deleteItems, "tblcontacts_groups") Then
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
#Region "get_groups"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_groups(ByVal dep_Id As String) As String()
        Dim Names As New List(Of String)(10)
        Dim str = ""
        Dim str2 = ""
        Try
            Dim dt As New DataTable
            Dim dt2 As New DataTable
            ' TblInvoice.TblInvoiceFields.SAccount_cd
            Dim query As String = "select * from tblcontacts where ISNUll(tblcontacts.deleted,0)=0 "
            Dim query2 As String = "select * from tblcontacts_groups where group_id=" + dep_Id + ""
            'If (dep_Id <> 0) Then
            '    query = query + " and group_id=" + dep_Id
            'End If
            'query = query + " order by IInvoice_typ"

            dt = DBManager.Getdatatable(query)
            dt2 = DBManager.Getdatatable(query2)
            If dt.Rows.Count <> 0 Then
                str = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add(str)
            Else
                Names.Add("0")
                Return Names.ToArray
            End If

            If dt2.Rows.Count <> 0 Then
                str2 = PublicFunctions.ConvertDataTabletoString(dt2)
                Names.Add(str2)
            Else
                Names.Add("0")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("")
            Return Names.ToArray
        End Try
        Return Names.ToArray
    End Function

#End Region

#Region "get_category"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_category(ByVal category_id As String) As String()
        Dim Names As New List(Of String)(10)
        Dim str = ""
        Dim str2 = ""
        Try
            Dim dt As New DataTable
            Dim dt2 As New DataTable
            Dim dt_user As DataTable
            Dim dt_category As DataTable
            Dim dtattch_file As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            dt_category = DBManager.Getdatatable("select * from tblregist_settings where category_id=" + category_id.ToString() + " and comp_id=" + dt_user.Rows(0).Item("comp_id").ToString())
            dtattch_file = DBManager.Getdatatable("Select * from tblImages where Source='register_company' and isnull(deleted,0)=0  and Source_id=" + category_id.ToString())
            If dt_category.Rows.Count <> 0 Then
                str = PublicFunctions.ConvertDataTabletoString(dt_category)
                Names.Add(str)
            Else
                Names.Add("0")
            End If
            If dtattch_file.Rows.Count <> 0 Then
                str2 = PublicFunctions.ConvertDataTabletoString(dtattch_file)
                Names.Add(str2)
            Else
                Names.Add("0")
            End If
        Catch ex As Exception
            Names.Add("")
            Return Names.ToArray
        End Try
        Return Names.ToArray
    End Function

#End Region
#Region "save_file"
    ''' <summary>"
    ''' Save About images into db 
    ''' </summary>
    Private Function Savea_ttch_file(ByVal attch_file_DataJsonList As List(Of Object), ByVal letter_Id As String) As Boolean
        Try
            DBManager.ExcuteQuery("delete from tblImages where Source_id=" + letter_Id.ToString + " and Source='register_company' ")
            For Each file_JSON As Object In attch_file_DataJsonList

                Dim dictfile As Dictionary(Of String, Object) = file_JSON
                Dim Source_id = letter_Id
                Dim Source = "register_company"
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



End Class