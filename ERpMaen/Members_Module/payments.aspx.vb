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
Public Class payments
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim comp = get_admin()
                Dim clsddltype3 As New clsFillComboByDataSource("select * from tblMembers where main=1 and ISNUll(deleted,0)=0 and comp_id=" + comp.ToString, "name", "id", "")
                clsddltype3.SetComboItems(member_id, "", True, "--اختر--", False)
                Dim clsddltype2 As New clsFillComboByDataSource("select * from tbllock_up where ISNUll(deleted,0)=0 and type='PT'", "description", "id", "")
                clsddltype2.SetComboItems(payment_method, "", True, "--اختر--", False)

                'Dim clsddltype5 As New clsFillComboByDataSource("select * from tbllock_up where ISNUll(deleted,0)=0 and type='RS'", "description", "id", "")
                'clsddltype5.SetComboItems(ddltype_id, "", True, "--اختر--", False)

                'Dim clsddltype1 As New clsFillComboByDataSource("  select * from tblcompanies where ISNUll(active,0)=1 ", "name_ar", "id", "")
                'clsddltype1.SetComboItems(ddltype_id, "", True, "--اختر--", False)

                '  LoginInfo.CheckPermisionsNew(cmdAdd, cmdUpdate, cmdDelete, Me.Page, UserId, lblFormName, DynamicTable)


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