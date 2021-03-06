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
Public Class DiplomaSubjectDetails
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
                'Dim subject_id = Request.QueryString("subject_id")
                'Lblsubject_id.InnerHtml = subject_id

                Dim code = Request.QueryString("code")
                lblcode.InnerHtml = code

                Dim dt = DBManager.Getdatatable(" select acd_diplome_subjects.id , tbllock_up.Description as 'subjectName' from acd_diplome_subjects join tbllock_up on acd_diplome_subjects.subject_id=tbllock_up.id where acd_diplome_subjects.code='" + code.ToString + "'")
                If dt.Rows.Count <> 0 Then
                    Dim subject_id = dt.Rows(0)(0).ToString
                    Lblsubject_id.InnerHtml = subject_id
                    subjectTitle.InnerHtml = dt.Rows(0)(1).ToString

                End If
                Dim dt2 = DBManager.Getdatatable(" select acd_diplome_subjects.diplome_id from acd_diplome_subjects where acd_diplome_subjects.code='" + code.ToString + "'")
                If dt2.Rows.Count <> 0 Then
                    Dim diplomeID = dt2.Rows(0)(0).ToString
                    LblDiplome_id.InnerHtml = diplomeID
                    Dim dt3 = DBManager.Getdatatable(" select code from acd_diplomes where id=" + diplomeID)
                    If dt3.Rows.Count <> 0 Then
                        Dim diplomeCode = dt3.Rows(0)(0).ToString
                        lbldiplomeCode.InnerHtml = diplomeCode

                    End If
                End If



                Dim clsapprove_tainer As New clsFillComboByDataSource("select id , full_name from tblUsers where User_Type='4' and comp_id=" + LoginInfo.GetComp_id(), "full_name", "id", "")
                clsapprove_tainer.SetComboItems(ddlsubTrainer, "", True, "--اختر--", False)

                Dim clsapprove_semster As New clsFillComboByDataSource("select id , name from acd_semester where  comp_id=" + LoginInfo.GetComp_id(), "name", "id", "")
                clsapprove_semster.SetComboItems(ddlsemster, "", True, "--اختر--", False)

                Dim clsapprove_course As New clsFillComboByDataSource("select * from tblLock_up where type='subj' and IsNull(Deleted,0)=0 and comp_id=" + LoginInfo.GetComp_id(), "Description", "id", "")
                clsapprove_course.SetComboItems(ddlsubject, "", True, "--اختر--", False)

                Dim clsapprove_halls As New clsFillComboByDataSource("select * from tblLock_up where type='HallNum' and IsNull(Deleted,0)=0 and comp_id=" + LoginInfo.GetComp_id(), "Description", "id", "")
                clsapprove_halls.SetComboItems(ddlhallNum, "", True, "--اختر--", False)

                dplm_subj_edit_lect.InnerHtml = ERpMaen.LoginInfo.get_form_operation("20")
                dplm_subj_delete_lect.InnerHtml = ERpMaen.LoginInfo.get_form_operation("21")
                dplm_subj_lect_absence.InnerHtml = ERpMaen.LoginInfo.get_form_operation("22")
                dplm_subj_lect_file.InnerHtml = ERpMaen.LoginInfo.get_form_operation("23")
                dplm_subj_lect_homework.InnerHtml = ERpMaen.LoginInfo.get_form_operation("24")
                dplm_subj_delete_file.InnerHtml = ERpMaen.LoginInfo.get_form_operation("30")
                dplm_subj_del_url.InnerHtml = ERpMaen.LoginInfo.get_form_operation("39")
                dplm_subj_del_comment.InnerHtml = ERpMaen.LoginInfo.get_form_operation("44")
                dblm_subj_stud_degrees.InnerHtml = ERpMaen.LoginInfo.get_form_operation("46")
                dplm_subj_delete_stud.InnerHtml = ERpMaen.LoginInfo.get_form_operation("47")
                dplm_subj_edit_hw.InnerHtml = ERpMaen.LoginInfo.get_form_operation("51")
                dplm_subj_del_hw.InnerHtml = ERpMaen.LoginInfo.get_form_operation("52")
                dplm_subj_sol_hw.InnerHtml = ERpMaen.LoginInfo.get_form_operation("53")
                dplm_subj_dwn_hw.InnerHtml = ERpMaen.LoginInfo.get_form_operation("54")

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
                Case "fuFile2"
                    fu = fuFile2

                    Path = "Acadmies_module/exams/"
                    Prepare_Sheet(fu)
                    CLSImagesHandler.Upload_Me(fu.PostedFile, Session("FileType"), fu.FileContent, Session("FileArray"), Path, 0, 0, 0, 0)

                Case "fuFile6"
                    fu = fuFile6

                    Path = "Acadmies_module/coursefiles/"
                    Prepare_Sheet(fu)
                    CLSImagesHandler.Upload_Me(fu.PostedFile, Session("FileType"), fu.FileContent, Session("FileArray"), Path, 0, 0, 0, 0)

                    'Case "fuFile3"
                    '    fu = fuFile3

                    '    Path = "Acadmies_module/homework/"
                    '    Prepare_Sheet(fu)
                    'CLSImagesHandler.Upload_Me(fu.PostedFile, Session("FileType"), fu.FileContent, Session("FileArray"), Path, 0, 0, 0, 0, "Employees", namer)


                Case "fuFile5"
                    fu = fuFile5

                    Path = "Acadmies_module/coursefiles/"
                    Prepare_Sheet(fu)
                    CLSImagesHandler.Upload_Me(fu.PostedFile, Session("FileType"), fu.FileContent, Session("FileArray"), Path, 0, 0, 0, 0)
                    'Case "fuFile4"
                    '    fu = fuFile4

                    '    Path = "Acadmies_module/coursefiles/"
                    '    Prepare_Sheet(fu)
                    '    CLSImagesHandler.Upload_Me(fu.PostedFile, Session("FileType"), fu.FileContent, Session("FileArray"), Path, 0, 0, 0, 0, "Employees", namer)

            End Select
            ClearContents(sender)
            'ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "newfile", "document.getElementById('imgEmployee').src = '" & url & "';", True)
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