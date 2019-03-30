﻿#Region "Signature"
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
Public Class DiplomaCourses
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
                Dim deploma_id = Request.QueryString("deploma_id")
                Lbldeploma_id.InnerHtml = deploma_id





                Dim clsapprove_tainer As New clsFillComboByDataSource("select id , full_name from tblUsers where User_Type='4'and comp_id=" + LoginInfo.GetComp_id(), "full_name", "id", "")
                clsapprove_tainer.SetComboItems(ddltrainer, "", True, "--اختر--", False)

                Dim clsapprove_semster As New clsFillComboByDataSource("select * from tblLock_up where type='semster' and IsNull(Deleted,0)=0 and comp_id=" + LoginInfo.GetComp_id(), "Description", "id", "")
                clsapprove_semster.SetComboItems(ddlsemster, "", True, "--اختر--", False)

                Dim clsapprove_course As New clsFillComboByDataSource("select * from tblLock_up where type='subj' and IsNull(Deleted,0)=0 and comp_id=" + LoginInfo.GetComp_id(), "Description", "id", "")
                clsapprove_course.SetComboItems(ddlcourse, "", True, "--اختر--", False)






                'LoginInfo.CheckPermisionsNew(cmdAdd, cmdUpdate, cmdDelete, Me.Page, UserId, lblFormName, DynamicTable)


            End If
        Catch ex As Exception
            'clsMessages.ShowErrorMessgage(lblResError, ex.Message, Me)
        End Try
    End Sub




End Class