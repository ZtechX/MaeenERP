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
Public Class Pub_Instructions
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
                '  

                'Dim clsapprove_type As New clsFillComboByDataSource("select * from tblLock_up where type='is' and IsNull(Deleted,0)=0", "Description", "id", "")
                'clsapprove_type.SetComboItems(ddlspecial_id, "", True, "--اختر--", False)



                Dim clsapprove_tainer As New clsFillComboByDataSource("select id , name from tblUser_Type   where  IsNull(Deleted,0)=0", "name", "id", "")
                clsapprove_tainer.SetComboItems(lblUsers, "", True, "--اختر--", False)



                'Dim clsapprove_category As New clsFillComboByDataSource("select * from tblLock_up where type='CD' and IsNull(Deleted,0)=0 and comp_id=" + LoginInfo.GetComp_id(), "Description", "id", "")
                'clsapprove_category.SetComboItems(ddlcategory, "", True, "--اختر--", False)


                inst_edit.InnerHtml = ERpMaen.LoginInfo.get_form_operation("66")
                inst_del.InnerHtml = ERpMaen.LoginInfo.get_form_operation("67")




                'LoginInfo.CheckPermisionsNew(cmdAdd, cmdUpdate, cmdDelete, Me.Page, UserId, lblFormName, DynamicTable)


            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PhotoUploaded(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim x As String = ""
        Dim rnd As New Random
        Dim namer As String = ""
        Dim i As Integer = 0
        Dim Path As String = ""
        Dim fu As New AjaxControlToolkit.AsyncFileUpload
        Try
            i = rnd.Next(10000, 99999)
            '  namer = i.ToString
            Select Case sender.id.ToString
                Case "fuFile1"
                    fu = fuFile1

                    Path = "Acadmies_module/images/"
                    Prepare_Sheet(fu)
                    ' Dim PostedPhoto As System.Drawing.Image = System.Drawing.Image.FromStream(fu.PostedFile.InputStream)
                    'Dim ImgHeight As Integer = PostedPhoto.Height
                    'Dim ImgWidth As Integer = PostedPhoto.Width
                    CLSImagesHandler.Upload_Me(fu.PostedFile, Session("FileType"), fu.FileContent, Session("FileArray"), Path, 0, 0, 0, 0)
                    ' Session("UserPhoto") = x


            End Select
            ClearContents(sender)
            '    ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "newfile", "document.getElementById('imgEmployee').src = '" & url & "';", True)
        Catch ex As Exception
            'lblRes.Text = "Failure with Message " & ex.Message.ToString
            'lblRes.CssClass = "res-label-error"
            ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "", "$('#lblRes').fadeIn(3000);", True)
        End Try
    End Sub
    Private Function Prepare_Sheet(ByVal f As AjaxControlToolkit.AsyncFileUpload) As Boolean
        Dim filename As String = System.IO.Path.GetFileName(f.FileName)
        Dim FileLength As Integer = f.PostedFile.ContentLength
        Dim MyFile As HttpPostedFile = f.PostedFile
        Session("fu2Name") = f.FileName

        Session("fu2File") = f.PostedFile
        Session("FileType") = System.IO.Path.GetExtension(f.FileName).ToLower()

        Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
        Dim FileAppend As Integer = 0
        Dim i As Integer = 0
        Session("FileType") = f.PostedFile.ContentType
        Return True
    End Function
    Private Sub ClearContents(ByVal control As Control)
        For i = 0 To Session.Keys.Count - 1

            If Session.Keys(i).Contains(control.ClientID) Then
                Session.Remove(Session.Keys(i))
                Exit For
            End If
        Next
    End Sub


End Class