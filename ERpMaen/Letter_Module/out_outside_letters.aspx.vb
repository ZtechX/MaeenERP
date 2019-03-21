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
Public Class out_outside_letters
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
                'Dim managment_id = LoginInfo.GetManagment_id()
                user_id.Text = UserId.ToString
                Dim clsddltype1 As New clsFillComboByDataSource("  select * from tbllock_up where type='MG'", "description", "id", "")
                clsddltype1.SetComboItems(ddlto_dep, "", True, "--اختر--", False)
                Dim clsddltype2 As New clsFillComboByDataSource("  select * from tbllock_up where type='MG'", "description", "id", "")
                clsddltype2.SetComboItems(ddlfrom_dep, "", True, "--اختر--", False)
                Dim clsddltype3 As New clsFillComboByDataSource("  select * from tbl_letter_priority", "name", "id", "")
                clsddltype3.SetComboItems(ddlpriority, "", True, "--اختر--", False)
                Dim clsddltype4 As New clsFillComboByDataSource("  select * from tbl_letter_secrecy", "name", "id", "")
                clsddltype4.SetComboItems(ddlsecrecy, "", True, "--اختر--", False)
                Dim clsddltype5 As New clsFillComboByDataSource("  select * from tbl_letter_stutse", "name", "id", "")
                clsddltype5.SetComboItems(ddlstutse, "", True, "--اختر--", False)
                Dim clsddltype6 As New clsFillComboByDataSource("  select * from tbl_letter_Continue", "name", "id", "")
                clsddltype6.SetComboItems(ddlContinue, "", False, "--اختر--", False)

                Dim cls_courts As New clsFillComboByDataSource("select * from ash_courts where IsNull(Deleted,0)=0", "name", "id", "")
                cls_courts.SetComboItems(ddlto_dep, "", True, "--اختر--", False)
                cls_courts.SetComboItems(ddlfrom_dep, "", False, "--اختر--", False)
                'Dim clsddltype3 As New clsFillComboByDataSource("  select * from tbltypes where ISNUll(deleted,0)=0", "name_ar", "id", "")
                'clsddltype2.SetComboItems(ddltype_id, "", True, "--اختر--", False)
                Dim letter_id = Request.QueryString("letter_id")
                If letter_id <> "" Then
                    related_letter_id.Text = letter_id.ToString
                End If
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