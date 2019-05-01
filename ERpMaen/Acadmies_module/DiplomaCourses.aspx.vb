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
Public Class DiplomaCourses
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)


                Dim code = Request.QueryString("code")
                lbldiplomeCode.InnerHtml = code
                Dim dt = DBManager.Getdatatable(" select  acd_diplomes.id,acd_diplomes.name from acd_diplomes where acd_diplomes.code='" + code.ToString + "'")
                If dt.Rows.Count <> 0 Then
                    Dim deploma_id = dt.Rows(0)(0).ToString
                    Lbldeploma_id.InnerHtml = deploma_id
                    diplome_title.InnerHtml = dt.Rows(0)(1).ToString

                    If ERpMaen.LoginInfo.getUserType = 8 Then

                        Dim dtd = DBManager.Getdatatable("select deleted   from acd_courses_students where type=2 and  student_id=" + LoginInfo.GetUser__Id() + " and approved=1 and course_id=" + deploma_id)
                        If dtd.Rows.Count <> 0 Then
                            If dtd.Rows(0)(0).ToString = True Then
                                Page.Response.Redirect("diplome_register.aspx?code=" + code)

                            End If
                        End If

                        Dim dtc = DBManager.Getdatatable("select course_id from acd_courses_students where type=2 and student_id=" + LoginInfo.GetUser__Id() + " and approved=1 and course_id=" + deploma_id)
                        If dtc.Rows.Count = 0 Then
                            'condition page 
                            'Page.Response.Redirect("course_register.aspx?code=" + code)
                            Page.Response.Redirect("diplome_register.aspx?code=" + code)

                        End If
                    End If
                Else
                    Page.Response.Redirect("AccessDenied.aspx")

                End If

                'Dim deploma_id = Request.QueryString("deploma_id")
                'Lbldeploma_id.InnerHtml = deploma_id

                Dim clsapprove_tainer As New clsFillComboByDataSource("select id , full_name from tblUsers where User_Type='4'and comp_id=" + LoginInfo.GetComp_id(), "full_name", "id", "")
                clsapprove_tainer.SetComboItems(ddltrainer, "", True, "--اختر--", False)

                Dim clsapprove_semster As New clsFillComboByDataSource("select id , name from acd_semester where  comp_id=" + LoginInfo.GetComp_id(), "name", "id", "")
                clsapprove_semster.SetComboItems(ddlsemster, "", True, "--اختر--", False)


                Dim clsapprove_subjectsemster As New clsFillComboByDataSource("select id , name from acd_semester where  comp_id=" + LoginInfo.GetComp_id(), "name", "id", "")
                clsapprove_subjectsemster.SetComboItems(ddlsubject_Semester, "", True, "--اختر--", False)

                Dim clsapprove_semster2 As New clsFillComboByDataSource("select id , name from acd_semester where  comp_id=" + LoginInfo.GetComp_id(), "name", "id", "")
                clsapprove_semster2.SetComboItems(ddlsemster2, "", True, "--اختر--", False)

                Dim clsapprove_course As New clsFillComboByDataSource("select * from tblLock_up where type='subj' and IsNull(Deleted,0)=0 and comp_id=" + LoginInfo.GetComp_id(), "Description", "id", "")
                clsapprove_course.SetComboItems(ddlcourse, "", True, "--اختر--", False)

                Dim clsapprove_paymentType As New clsFillComboByDataSource("select * from tblLock_up where type='PT' and IsNull(Deleted,0)=0 and comp_id=" + LoginInfo.GetComp_id(), "Description", "id", "")
                clsapprove_paymentType.SetComboItems(ddlpayment_type, "", True, "--اختر--", False)

                Dim clsapprove_category As New clsFillComboByDataSource("select * from tblLock_up where type='CD' and IsNull(Deleted,0)=0 and comp_id=" + LoginInfo.GetComp_id(), "Description", "id", "")
                clsapprove_category.SetComboItems(ddlcategory, "", True, "--اختر--", False)

                Dim clsDiplomeCoordinator As New clsFillComboByDataSource("select id , full_name from tblUsers where User_Type='14' and comp_id=" + LoginInfo.GetComp_id, "full_name", "id", "")
                clsDiplomeCoordinator.SetComboItems(ddlcoordinator, "", True, "--اختر--", False)

                dplm_delete_condtion.InnerHtml = ERpMaen.LoginInfo.get_form_operation("5")



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
                Case "fuFile1"
                    fu = fuFile1


                    Path = "Acadmies_module/images/"
                    Prepare_Sheet(fu)

                    CLSImagesHandler.Upload_Me(fu.PostedFile, Session("FileType"), fu.FileContent, Session("FileArray"), Path, 0, 0, 0, 0, "Employees", namer)


                Case "fufile3"
                    fu = fuFile1

                    Path = "Acadmies_module/coursefiles /"
                    Prepare_Sheet(fu)

                    CLSImagesHandler.Upload_Me(fu.PostedFile, Session("FileType"), fu.FileContent, Session("FileArray"), Path, 0, 0, 0, 0, "Employees", namer)
                Case "fuPhoto1"
                    fu = fuPhoto1
                    CLSImagesHandler.Upload_Me(fu.PostedFile, Session("FileType"), fu.FileContent, Session("FileArray"), Path, 0, 0, 0, 0, "Employees", namer)

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