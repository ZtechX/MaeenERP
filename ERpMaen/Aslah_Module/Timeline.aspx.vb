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
Public Class Timeline
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                case_id.Text = Request.QueryString("case_id")
                done.Text = Request.QueryString("done")
                start_dt.Text = Request.QueryString("start_dt")
                end_dt.Text = Request.QueryString("end_dt")

                If start_dt.Text <> "''" Then
                    start_dt.Text = PublicFunctions.ConvertDatetoNumber(start_dt.Text)
                    end_dt.Text = PublicFunctions.ConvertDatetoNumber(end_dt.Text)
                End If
                If String.IsNullOrWhiteSpace(case_id.Text) Then
                        Dim script As String = "<script type='text/javascript' defer='defer'> alert('لا يوجد بيانات متاحة للعرض');</script>"
                        ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
                        ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "window.close();", True)
                    End If
                End If
        Catch ex As Exception
            'clsMessages.ShowErrorMessgage(lblResError, ex.Message, Me)
        End Try
    End Sub



End Class