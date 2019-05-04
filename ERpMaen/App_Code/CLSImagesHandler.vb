Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Web.HttpContext
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Math
Imports System.Drawing.Drawing2D
Imports ERpMaen

Public Class CLSImagesHandler

    Private Function ResizeImage(streamImage As Stream, ByRef maxWidth As Integer, ByRef maxHeight As Integer, OrigWidth As Integer, origHeight As Integer) As Boolean

        Try
            Dim originalImage As New Bitmap(streamImage)
            Dim newWidth As Integer = OrigWidth
            Dim newHeight As Integer = origHeight
            Dim aspectRatio As Double = CDbl(OrigWidth) / CDbl(origHeight)

            If aspectRatio <= 1 AndAlso OrigWidth > maxWidth Then
                newWidth = maxWidth
                newHeight = CInt(Math.Round(newWidth / aspectRatio))
            ElseIf aspectRatio > 1 AndAlso origHeight > maxHeight Then
                newHeight = maxHeight
                newWidth = CInt(Math.Round(newHeight * aspectRatio))
            End If
            maxWidth = newWidth
            maxHeight = newHeight
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function Upload_Me(ByVal file As HttpPostedFile, ByVal ext As String, ByVal st As Stream, ByVal myimageData() As Byte, ByVal DestPath As String, ByVal imgthumbWidth As Integer, ByVal ImgThumbHeight As Integer,
                              ByVal OrigWidth As Integer, ByVal OrigHeight As Integer) As String
        Dim x As Integer = 1000
        Dim y As Integer = 1000
        Dim SavePath As String
        Dim MyFile As HttpPostedFile = file
        '  Dim FileLength As Integer = File.ContentLength
        Dim thumbIMGWidth As Integer = OrigWidth
        Dim thumbimgheight As Integer = OrigHeight
        SavePath = Current.Server.MapPath("~") + "/" + DestPath
        Dim file_nm = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName)
        Dim uset_id = LoginInfo.GetUser__Id()

        Dim saved_file_nm = uset_id & "_" & file_nm & "_" & PublicFunctions.GetRandom(6)
        Dim FileAppend As Integer = 0
        Dim i As Integer = 0
        Dim NewFile As System.IO.FileStream
        Dim file_NotImage As Boolean = True

        Select Case ext
            Case "application/pdf"
                saved_file_nm = saved_file_nm & ".pdf"
            Case "application/msword"
                saved_file_nm = saved_file_nm & ".doc"
            Case "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                saved_file_nm = saved_file_nm & ".docx"
            Case ".docx"
                saved_file_nm = saved_file_nm & ".docx"
            Case "application/vnd.ms-powerpoint"
                saved_file_nm = saved_file_nm & ".ppt"
            Case "application/vnd.openxmlformats-officedocument.presentationml.presentation"
                saved_file_nm = saved_file_nm & ".pptx"
            Case ".pptx"
                saved_file_nm = saved_file_nm & ".pptx"
            Case "application/vnd.ms-excel"
                saved_file_nm = saved_file_nm & ".xls"
            Case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                saved_file_nm = saved_file_nm & ".xlsx"
            Case ".xlsx"
                saved_file_nm = saved_file_nm & ".xlsx"
            Case "image/jpeg"
                file_NotImage = False
                saved_file_nm = saved_file_nm & ".jpg"
            Case "image/tiff"
                file_NotImage = False
                saved_file_nm = saved_file_nm & ".tif"
            Case "image/png"
                file_NotImage = False
                saved_file_nm = saved_file_nm & ".png"
            Case "image/bmp"
                file_NotImage = False
                saved_file_nm = saved_file_nm & ".bmp"
            Case "image/gif"
                file_NotImage = False
                saved_file_nm = saved_file_nm & ".gif"
            Case ".png"
                file_NotImage = False
                saved_file_nm = saved_file_nm & ".png"
            Case ".gif"
                file_NotImage = False
                saved_file_nm = saved_file_nm & ".gif"
            Case Else

        End Select
        Try
            NewFile = New System.IO.FileStream(SavePath & saved_file_nm, FileMode.Create)
            If file_NotImage Then
                NewFile.Write(myimageData, 0, file.ContentLength)
            Else
                Dim BIT As Bitmap = New Bitmap(st)
                Dim thumb As Image = BIT.GetThumbnailImage(imgthumbWidth, ImgThumbHeight, Nothing, New IntPtr)
                Dim oGraphics As Graphics = Graphics.FromImage(thumb)
                ' Set the properties for the new graphic file
                oGraphics.SmoothingMode = SmoothingMode.AntiAlias
                oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic
                ' Draw the new graphic based on the resized bitmap
                oGraphics.DrawImage(BIT, 0, 0, thumbIMGWidth, ImgThumbHeight)
                thumb.Save(NewFile, Imaging.ImageFormat.Jpeg)

            End If
        Catch ex As Exception
            NewFile.Close()
            Return ""
        End Try
        NewFile.Close()
        Return saved_file_nm
    End Function

    Public Function FileUpload(ByVal id As String, ByVal FU As FileUpload, ByVal DestPath As String, ByRef type As String) As String
        Dim SavePath As String '= DestPath
        Dim ext As String
        Dim MyFile As HttpPostedFile = FU.PostedFile
        Dim FileLength As Integer = MyFile.ContentLength
        '    ImagePath = MapPath("~/");
        'ImagePath += "../Test/Mypicture.jpg";

        '###########################################
        SavePath = Current.Server.MapPath("~") + DestPath
        '##############################################
        ' SavePath = "~" + DestPath


        'If Imagepage = "Cats" Then
        '    SavePath += "/Images/Cats/"
        'ElseIf Imagepage = "SubCats" Then
        '    SavePath += "/Images/SubCats/"
        'ElseIf Imagepage = "Items" Then
        '    SavePath += "/Images/Items/"
        'End If
        '  SavePath += "/Images/Cats/"
        If FileLength = 0 Then
            Return ""
            Exit Function
        End If
        If System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".exe" Then
            Return ""
            Exit Function
        End If
        If System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".jpg" Or System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".bmp" Or System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".png" Or System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".gif" Then
            type = "Image"
        ElseIf System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".pdf" Then
            type = "PDF"
        ElseIf System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".rar" Or System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".zip" Then
            type = "Compressed"
        ElseIf System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".doc" Or System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".docx" Or System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".rtf" Then
            type = "Document"
        End If


        Dim MyImageData(FileLength) As Byte
        ' byte[] myData = new Byte[nFileLen]
        MyFile.InputStream.Read(MyImageData, 0, FileLength)
        Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
        Dim FileAppend As Integer = 0
        Dim i As Integer = 0
        ext = System.IO.Path.GetExtension(MyFile.FileName).ToLower()
        While i < FileLength
            i += 1
            myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) + "_" + FileAppend.ToString + id + ext
        End While
        '"../resources/allImages/products/fullsize") & "\" & imgID & ".jpg"
        Dim NewFile As System.IO.FileStream ' = New System.IO.FileStream(Current.Server.MapPath(Current.Server.MapPath("~") + "\Images\Cats" + "\" & myfilename), System.IO.FileMode.Create)
        Try


            NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
            NewFile.Write(MyImageData, 0, FileLength)
            NewFile.Close()
            ' Creating the Image Thumb
            'System.Drawing.Image(myThumbnail)
            '            = myBitmap.GetThumbnailImage(intThumbWidth, 
            '                                         intThumbHeight, myCallBack, IntPtr.Zero);
            '    myThumbnail.Save (Server.MapPath(sSavePath + sThumbFile));
            '    imgPicture.ImageUrl = sSavePath + sThumbFile;
            ' myfilename += "_Thumb"
            'Dim BIT As Bitmap = New Bitmap(SavePath & myfilename)
            '  Dim MyCallBack As System.Drawing.Image.GetThumbnailImageAbort
            ' Dim MyThumbImg As System.Drawing.Image = BIT.GetThumbnailImage(thumbIMGWidth, thumbimgheight, MyCallBack, IntPtr.Zero)
            '    Dim thumbpath As String = SavePath & "_Thumb" & myfilename
            ' myfilename = myfilename.Insert(0, "_Thumb")
            Dim UrlPath As String = DestPath & myfilename

            Return UrlPath
        Catch ex As Exception

        End Try
    End Function
    Public Function FileUploader(ByVal id As String, ByVal FU As AjaxControlToolkit.AsyncFileUpload, ByVal DestPath As String, ByRef type As String) As String
        Dim SavePath As String '= DestPath
        Dim ext As String
        Dim MyFile As HttpPostedFile = FU.PostedFile
        Dim FileLength As Integer = MyFile.ContentLength
        Dim rnd As New Random()
        Dim x As Integer = rnd.Next(0, 99999)
        '    ImagePath = MapPath("~/");
        'ImagePath += "../Test/Mypicture.jpg";
        Try


            '###########################################
            SavePath = Current.Server.MapPath("~") + DestPath
            '##############################################
            ' SavePath = "~" + DestPath


            'If Imagepage = "Cats" Then
            '    SavePath += "/Images/Cats/"
            'ElseIf Imagepage = "SubCats" Then
            '    SavePath += "/Images/SubCats/"
            'ElseIf Imagepage = "Items" Then
            '    SavePath += "/Images/Items/"
            'End If
            '  SavePath += "/Images/Cats/"
            If FileLength = 0 Then
                Return vbNullString
                Exit Function
            End If
            If System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".exe" Then
                Return vbNullString
                Exit Function
            End If

            If System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".jpg" Or System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".bmp" Or System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".png" Or System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".gif" Then
                type = "Image"
            ElseIf System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".pdf" Then
                type = "PDF"
            ElseIf System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".rar" Or System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".zip" Then
                type = "Compressed"
            ElseIf System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".doc" Or System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".docx" Or System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".rtf" Then
                type = "Document"
            ElseIf System.IO.Path.GetExtension(MyFile.FileName).ToLower() = ".txt" Then
                type = "Text"
            End If


            Dim MyImageData(FileLength) As Byte
            ' byte[] myData = new Byte[nFileLen]
            MyFile.InputStream.Read(MyImageData, 0, MyImageData.Length)
            Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
            Dim FileAppend As Integer = 0
            Dim i As Integer = 0
            ext = System.IO.Path.GetExtension(MyFile.FileName).ToLower()
            While i < FileLength
                i += 1
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) + "_" + FileAppend.ToString + id + ext
            End While
            '"../resources/allImages/products/fullsize") & "\" & imgID & ".jpg"
            ' Dim NewFile As System.IO.FileStream ' = New System.IO.FileStream(Current.Server.MapPath(Current.Server.MapPath("~") + "\Images\Cats" + "\" & myfilename), System.IO.FileMode.Create)
            Try

                FU.SaveAs(SavePath & x.ToString & FU.FileName)
                'NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                'NewFile.Write(MyImageData, 0, MyImageData.Length)

                'NewFile.Close()
                '' Creating the Image Thumb
                ''System.Drawing.Image(myThumbnail)
                ''            = myBitmap.GetThumbnailImage(intThumbWidth, 
                ''                                         intThumbHeight, myCallBack, IntPtr.Zero);
                ''    myThumbnail.Save (Server.MapPath(sSavePath + sThumbFile));
                ''    imgPicture.ImageUrl = sSavePath + sThumbFile;
                '' myfilename += "_Thumb"
                ''Dim BIT As Bitmap = New Bitmap(SavePath & myfilename)
                ''  Dim MyCallBack As System.Drawing.Image.GetThumbnailImageAbort
                '' Dim MyThumbImg As System.Drawing.Image = BIT.GetThumbnailImage(thumbIMGWidth, thumbimgheight, MyCallBack, IntPtr.Zero)
                ''    Dim thumbpath As String = SavePath & "_Thumb" & myfilename
                '' myfilename = myfilename.Insert(0, "_Thumb")
                'Dim UrlPath As String = DestPath & myfilename

                Return "/CVS/" & x.ToString & FU.FileName
            Catch ex As Exception

            End Try
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function Delete_Image(ByVal ImagePath As String) As String
        'Dim path As String = ImagePath
        ''Dim ImageFile As FileInfo =New FileInfo(
        'Dim MyFile As IO.File
        'If path <> vbNullString Then
        '    MyFile.Delete(path)
        'End If
    End Function


End Class
