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
Public Class Archived_DiplomaCourses
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)



                Dim diplomeCode = Request.QueryString("code")
                lbldiplomecode.InnerHtml = diplomeCode
                Dim dt = DBManager.Getdatatable(" select  acd_diplomes.id from acd_diplomes where acd_diplomes.code='" + diplomeCode.ToString + "'")
                If dt.Rows.Count <> 0 Then
                    Dim deploma_id = dt.Rows(0)(0).ToString
                    Lbldeploma_id.InnerHtml = deploma_id
                End If


                'Dim deploma_id = Request.QueryString("deploma_id")
                'Lbldeploma_id.InnerHtml = deploma_id





                'Dim clsapprove_tainer As New clsFillComboByDataSource("select id , full_name from tblUsers where User_Type='4'and comp_id=" + LoginInfo.GetComp_id(), "full_name", "id", "")
                'clsapprove_tainer.SetComboItems(ddltrainer, "", True, "--اختر--", False)

                'Dim clsapprove_semster As New clsFillComboByDataSource("select * from tblLock_up where type='semster' and IsNull(Deleted,0)=0 and comp_id=" + LoginInfo.GetComp_id(), "Description", "id", "")
                'clsapprove_semster.SetComboItems(ddlsemster, "", True, "--اختر--", False)

                'Dim clsapprove_course As New clsFillComboByDataSource("select * from tblLock_up where type='subj' and IsNull(Deleted,0)=0 and comp_id=" + LoginInfo.GetComp_id(), "Description", "id", "")
                'clsapprove_course.SetComboItems(ddlcourse, "", True, "--اختر--", False)

                'Dim clsapprove_paymentType As New clsFillComboByDataSource("select * from tblLock_up where type='Pay_TP' and IsNull(Deleted,0)=0", "Description", "id", "")
                'clsapprove_paymentType.SetComboItems(ddlpayment_type, "", True, "--اختر--", False)

                'Dim clsapprove_category As New clsFillComboByDataSource("select * from tblLock_up where type='CD' and IsNull(Deleted,0)=0 and comp_id=" + LoginInfo.GetComp_id(), "Description", "id", "")
                'clsapprove_category.SetComboItems(ddlcategory, "", True, "--اختر--", False)






                'LoginInfo.CheckPermisionsNew(cmdAdd, cmdUpdate, cmdDelete, Me.Page, UserId, lblFormName, DynamicTable)


            End If
        Catch ex As Exception
            'clsMessages.ShowErrorMessgage(lblResError, ex.Message, Me)
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
                'Case "fuFile1"
                '    fu = fuFile1

                '    Path = "Acadmies_module/images/"
                '    Prepare_Sheet(fu)
                ' Dim PostedPhoto As System.Drawing.Image = System.Drawing.Image.FromStream(fu.PostedFile.InputStream)
                'Dim ImgHeight As Integer = PostedPhoto.Height
                'Dim ImgWidth As Integer = PostedPhoto.Width
                'CLSImagesHandler.Upload_Me(fu.PostedFile, Session("FileType"), fu.FileContent, Session("FileArray"), Path, 0, 0, 0, 0, "Employees", namer)
                '    ' Session("UserPhoto") = x

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
        'Dim MyImageData2(FileLength) As Byte
        ' byte[] myData = new Byte[nFileLen]
        ' MyFile.InputStream.Read(MyImageData2, 0, FileLength)
        '  Session("FileArray") = MyImageData2
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