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
Public Class Hall
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
                LoginInfo.CheckPermisionsNew(cmdAdd, cmdUpdate, cmdDelete, Me.Page, UserId, lblFormName, DynamicTable)

                Dim clsapprove_type As New clsFillComboByDataSource("select * from tblLockup where type='HT' and IsNull(Deleted,0)=0", "aDescription", "id", "")
                clsapprove_type.SetComboItems(ddlType, "", True, "--اختر--", False)

                Dim clsddlStaff As New clsFillComboByDataSource("select * from tblusers where IsNull(Deleted,0)=0", "Name", "id", "")
                clsddlStaff.SetComboItems(ddlStaff, "", True, "--اختر--", False)

                Dim clsddlStatus As New clsFillComboByDataSource("select * from tblLockup where type='HS' and IsNull(Deleted,0)=0", "aDescription", "id", "")
                clsddlStatus.SetComboItems(ddlStatus, "", True, "--اختر--", False)
            End If
        Catch ex As Exception
            'clsMessages.ShowErrorMessgage(lblResError, ex.Message, Me)
        End Try
    End Sub




End Class