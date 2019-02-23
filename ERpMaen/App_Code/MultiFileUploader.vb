#Region "Import"
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Web.Script.Services
Imports BusinessLayer.BusinessLayer
Imports System.Data.SqlClient
Imports System.IO
Imports OpenPop.Pop3
Imports OpenPop.Mime
#End Region

'Imports System.Xml
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<System.Web.Script.Services.ScriptService()> _
Public Class MultiFileUploader
    Inherits System.Web.Services.WebService

    ''' <summary>
    ''' Upload file into directory
    ''' </summary>
    <WebMethod(True)> _
    <System.Web.Script.Services.ScriptMethod()>
    Public Function UploadFile(ByVal args As Object, ByVal folders As String) As String
        Try
            Dim localFolder As String
            Dim xmlFolder As String
            If folders.Contains("|") Then
                localFolder = folders.Split("|")(0)
                xmlFolder = folders.Split("|")(1)
            Else
                localFolder = folders
                xmlFolder = ""
            End If

            Dim filepath As Object = args("_fileId")
            Dim fileName As String = args("_fileName")
            Dim PhotoName As String = args("_fileName").ToString.Split(".")(0)
            If PhotoName.Length > 30 Then
                PhotoName = args("_fileName").ToString.Split(".")(0).Substring(0, 30)
            End If
            Dim FileExt As String = args("_fileName").ToString.Split(".").Last
            Dim FileSize As String = Val(args("_fileSize")) / 1000000
            Dim LocaldirectoryPath As String = Server.MapPath(String.Format("~/{0}/", localFolder))

            If Not Directory.Exists(LocaldirectoryPath) Then
                Directory.CreateDirectory(LocaldirectoryPath)
            End If

            Dim fp As String = Path.GetTempPath() + "_AjaxFileUpload" + "\" + filepath + "\" + fileName
            Dim rnd As New Random
            While File.Exists(LocaldirectoryPath + fileName)
                fileName = rnd.Next(10000000, 99999999).ToString & "Copy_" & fileName
            End While

            If File.Exists(fp) Then
                If Not File.Exists(LocaldirectoryPath + fileName) Then
                    File.Copy(fp, LocaldirectoryPath + fileName)
                End If
                If (Not xmlFolder = "") Then
                    Dim XMLDirectoryPath As String = Server.MapPath(String.Format("~/{0}/", xmlFolder))
                    If Not File.Exists(XMLDirectoryPath + fileName) Then
                        File.Copy(fp, XMLDirectoryPath + fileName)
                    End If
                End If
            Else
                Return ""
            End If

            If File.Exists(fp) Then
                File.Delete(fp)
            End If
            Return localFolder.Split("/")(1) & "/" & fileName + "|" + filepath
        Catch ex As Exception
            Return ex.Message.ToString
        End Try
    End Function

    ''' <summary>
    ''' Delete file from directory
    ''' </summary>
    <WebMethod(True)> _
    <System.Web.Script.Services.ScriptMethod()>
    Public Function DeleteFile(ByVal fileName As String, ByVal folderName As String) As String
        Try
            Dim fileKey = fileName.Split("|")(1)
            fileName = fileName.Split("|")(0)
            Dim filepath As String = Server.MapPath(String.Format("~/{0}", folderName + "/" + fileName))
            If File.Exists(filepath) Then
                File.Delete(filepath)
                Return fileName + "|" + fileKey
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Class