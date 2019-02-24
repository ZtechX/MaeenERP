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
Public Class sms_setting
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
                '  
                Dim clsddltype1 As New clsFillComboByDataSource("  select * from tblcompanies where ISNUll(active,0)=1 ", "name_ar", "id", "")
                clsddltype1.SetComboItems(ddlComps, "", True, "--اختر--", False)

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




End Class