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
Public Class sms
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
    Public Function Save(ByVal id As String, ByVal basicDataJson As List(Of Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim DetailsJSON1 As Dictionary(Of String, Object) = basicDataJson(0)
            Dim result = 0
            Dim dt_user As DataTable
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            For Each DetailsJSON As Dictionary(Of String, Object) In basicDataJson
                Dim dictBasicDataJson As Dictionary(Of String, Object) = DetailsJSON
                dictBasicDataJson.Add("from_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
                dictBasicDataJson.Add("comp_id", dt_user.Rows(0).Item("comp_id"))
                id = 0
                PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblsms", id, _sqlconn, _sqltrans)
                result = 1
            Next
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
    Public Function get_groups(ByVal flag As String, ByVal dep_Id As String, ByVal type As String) As String()
        Dim Names As New List(Of String)(10)
        Dim str = ""
        Dim str1 = ""
        Try
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            Dim query = ""
            ' TblInvoice.TblInvoiceFields.SAccount_cd
            If flag = 1 Then
                If type = "_3" Then
                    query = "select tblcontacts.id as id, tblcontacts.name_ar as 'name',tblcontacts.tel1 as tel from tblcontacts_groups right join tblcontacts on tblcontacts_groups.contact_id=tblcontacts.id   where ISNUll(tblcontacts_groups.deleted,0)=0 and tblcontacts_groups.group_id=" + dep_Id.ToString + ""
                Else
                    query = "select full_name as 'name',User_PhoneNumber as tel,id from tblUsers where parent_id=" + LoginInfo.GetUser__Id() + " and comp_id = " + LoginInfo.GetComp_id() + " and  User_Type='" + type + "'  "
                End If
            Else
            End If

            dt = DBManager.Getdatatable(query)
            If dt.Rows.Count <> 0 Then
                str = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add(str)
            Else
                Names.Add("")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("")
            Return Names.ToArray
        End Try
        Return Names.ToArray
    End Function

#End Region

#Region "get_template"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_template(ByVal template As String) As String()
        Dim Names As New List(Of String)(10)
        Dim str = ""
        Dim str1 = ""
        Try
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            Dim query = ""
            ' TblInvoice.TblInvoiceFields.SAccount_cd
            query = "select * from tblsms_templates where tblsms_templates.id=" + template.ToString + ""

            dt = DBManager.Getdatatable(query)
            If dt.Rows.Count <> 0 Then
                str = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add(str)
            Else
                Names.Add("0")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("0")
            Return Names.ToArray
        End Try
        Return Names.ToArray
    End Function

#End Region

#Region "get user Types"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_userTypes() As String
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select * from tblUser_Type where isNull(deleted,0) !=1")
            If dt.Rows.Count <> 0 Then
                Return PublicFunctions.ConvertDataTabletoString(dt)
            End If
        Catch ex As Exception
            Return ""
        End Try
        Return ""
    End Function

#End Region

End Class