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
Public Class reports_academy
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
                '  

                Dim clsapprove_type As New clsFillComboByDataSource("select * from acd_diplomes where IsNull(Deleted,0)=0 and Comp_id =" + LoginInfo.GetComp_id(), "name", "id", "")
                clsapprove_type.SetComboItems(ddldiplomes, "", True, "--اختر--", False)
                clsapprove_type.SetComboItems(ddldiplomes1, "", False, "--اختر--", False)
                clsapprove_type.SetComboItems(ddldiplomes2, "", False, "--اختر--", False)
                clsapprove_type.SetComboItems(ddldiplomes3, "", False, "--اختر--", False)
                clsapprove_type.SetComboItems(ddldiplomes4, "", False, "--اختر--", False)
                clsapprove_type.SetComboItems(ddldiplomes5, "", False, "--اختر--", False)
                clsapprove_type.SetComboItems(ddldiplomes6, "", False, "--اختر--", False)

                Dim cls_type As New clsFillComboByDataSource("select * from acd_semester where  Comp_id =" + LoginInfo.GetComp_id(), "name", "id", "")
                cls_type.SetComboItems(ddlsemster, "", True, "--اختر--", False)
                cls_type.SetComboItems(ddlsemster1, "", False, "--اختر--", False)
                cls_type.SetComboItems(ddlsemster2, "", False, "--اختر--", False)
                cls_type.SetComboItems(ddlsemster3, "", False, "--اختر--", False)
                'LoginInfo.CheckPermisionsNew(cmdAdd, cmdUpdate, cmdDelete, Me.Page, UserId, lblFormName, DynamicTable)


            End If
        Catch ex As Exception
            'clsMessages.ShowErrorMessgage(lblResError, ex.Message, Me)
        End Try
    End Sub




End Class