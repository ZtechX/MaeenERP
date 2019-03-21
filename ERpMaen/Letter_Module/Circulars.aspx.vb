#Region "Signature"
'################################### Signature ####################################
'############# Version: v1.0
'############# Start Date:
'############# Form Name: Contacts 
'############# Your Name:  Ahmed Nayl
'############# Create date and time 21-01-2019 10:00 AM
'############# Form Description:  
'################################ End of Signature ################################
#End Region

#Region "Imports"
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports System.Data.OleDb
Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Data.SqlTypes
Imports System.Net
Imports System.Net.Mail
Imports System.Collections
Imports System.ComponentModel
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports AjaxControlToolkit
Imports Newtonsoft.Json
#End Region
Public Class Circulars
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
                'Dim managment_id = LoginInfo.GetManagment_id()
                'user_id.Text = UserId.ToString

                Dim clsddltype3 As New clsFillComboByDataSource("  select * from tbl_letter_priority", "name", "id", "")
                clsddltype3.SetComboItems(ddlpriority, "", True, "--اختر--", False)


                'Dim clsddltype1 As New clsFillComboByDataSource("  select * from tbldepartmen where   ISNUll(deleted,0)=0", "name", "id", "")
                'clsddltype1.SetComboItems(ddlto_dep, "", True, "--اختر--", False)
                'Dim clsddltype2 As New clsFillComboByDataSource("  select * from tbldepartmen where   ISNUll(deleted,0)=0", "name", "id", "")
                'clsddltype2.SetComboItems(ddlfrom_dep, "", True, "--اختر--", False)

                'Dim clsddltype4 As New clsFillComboByDataSource("  select * from tbl_letter_secrecy", "name", "id", "")
                'clsddltype4.SetComboItems(ddlsecrecy, "", True, "--اختر--", False)
                'Dim clsddltype5 As New clsFillComboByDataSource("  select * from tbl_letter_stutse", "name", "id", "")
                'clsddltype5.SetComboItems(ddlstutse, "", True, "--اختر--", False)
                'Dim clsddltype3 As New clsFillComboByDataSource("  select * from tbltypes where ISNUll(deleted,0)=0", "name_ar", "id", "")
                'clsddltype2.SetComboItems(ddltype_id, "", True, "--اختر--", False)
            End If
        Catch ex As Exception
            'clsMessages.ShowErrorMessgage(lblResError, ex.Message, Me)
        End Try
    End Sub

    Function get_admin() As String
        Dim dtd As Data.DataTable
        Dim _id = 0
        dtd = BusinessLayer.BusinessLayer.DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
        Return dtd.Rows(0).Item("comp_id")
    End Function


End Class