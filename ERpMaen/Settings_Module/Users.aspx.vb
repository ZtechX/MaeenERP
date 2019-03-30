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
Public Class Users
    Inherits System.Web.UI.Page
    Dim UserId As String
    Dim comp_id As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
            comp_id = LoginInfo.GetComp_id()

            If Page.IsPostBack = False Then
                Dim clsddltypes As New clsFillComboByDataSource("  select * from tbllock_up where ISNUll(deleted,0)=0 and type='MG' and Comp_id=" + comp_id.ToString, "description", "id", "")
                clsddltypes.SetComboItems(ddlmanagment_id, "", True, "--اختر--", False)
                Dim clsddlcomps As New clsFillComboByDataSource("  select * from tblcompanies where ISNUll(deleted,0)=0 ", "name_ar", "id", "")
                clsddlcomps.SetComboItems(ddlcomp_id, "", True, "--اختر--", False)
                Dim clsddltype5 As New clsFillComboByDataSource("select * from tbllock_up where ISNUll(deleted,0)=0 and type='PG' and Comp_id=" + comp_id.ToString, "description", "id", "")
                clsddltype5.SetComboItems(ddlgroup_id, "", True, "--اختر--", False)
                Dim clsddlusertype As New clsFillComboByDataSource("select * from tblUser_Type where ISNUll(deleted,0)=0", "name", "id", "")
                clsddlusertype.SetComboItems(ddlUser_Type, "", True, "--اختر--", False)
            End If
            If isSuperAdmin() Then
                divcomp_id.Attributes.Remove("style")
                ' divResearcher.Attributes.Add("style", "display:none;")

            Else
                ddlcomp_id.SelectedValue = comp_id
                comp.InnerHtml = comp_id

                divcomp_id.Attributes.Add("style", "display:none;")
                ' divResearcher.Attributes.Remove("style")

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
                Case "fuPhoto1"
                    fu = fuPhoto1
                    Path = "Settings_Module/images/"
                    Prepare_Sheet(fu)
                    Dim PostedPhoto As System.Drawing.Image = System.Drawing.Image.FromStream(fu.PostedFile.InputStream)
                    Dim ImgHeight As Integer = PostedPhoto.Height
                    Dim ImgWidth As Integer = PostedPhoto.Width
                    x = CLSImagesHandler.Upload_Me(fu.PostedFile, Session("FileType"), fu.FileContent, Session("FileArray"), Path, ImgWidth, ImgHeight, ImgWidth, ImgHeight, "Employees", namer)
                    Session("UserPhoto") = x
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
        Dim MyImageData2(FileLength) As Byte
        ' byte[] myData = new Byte[nFileLen]
        MyFile.InputStream.Read(MyImageData2, 0, FileLength)
        Session("FileArray") = MyImageData2
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

    Protected Function isSuperAdmin() As Boolean
        Dim dt As DataTable = DBManager.Getdatatable("Select isNull(superAdmin,0) as superAdmin from tblUsers where id=" + UserId)
        Return dt.Rows(0)(0)
    End Function

End Class