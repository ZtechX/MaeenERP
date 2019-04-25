Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Web.HttpContext
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Math
Imports System.Drawing.Drawing2D

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
    Public Shared Function Upload_Me(ByVal file As HttpPostedFile, ByVal ext As String, ByVal st As Stream, ByVal myimageData() As Byte, ByVal DestPath As String, ByVal imgthumbWidth As Integer, ByVal ImgThumbHeight As Integer, _
                              ByVal OrigWidth As Integer, ByVal OrigHeight As Integer, ByVal Imagepage As String, ByVal Namer As String) As String
        Dim x As Integer = 1000
        Dim y As Integer = 1000
        Dim SavePath As String
        Dim MyFile As HttpPostedFile = file
        '  Dim FileLength As Integer = File.ContentLength
        Dim thumbIMGWidth As Integer = OrigWidth
        Dim thumbimgheight As Integer = OrigHeight
        SavePath = Current.Server.MapPath("~") + "/" + DestPath
        Select Case ext
            Case "application/msword"
                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                '    myfilename = myfilename.Replace(" ", "-")
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".doc"
                'myfilename = myfilename.Replace(" ", "-")
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    NewFile.Write(myimageData, 0, file.ContentLength)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    '    UrlPath = "http://www.whatthepost.com/" & UrlPath
                    Return UrlPath
                Catch ex As Exception

                End Try
            Case "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                '    myfilename = myfilename.Replace(" ", "-")
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".docx"
                'myfilename = myfilename.Replace(" ", "-")
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    NewFile.Write(myimageData, 0, file.ContentLength)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    '    UrlPath = "http://www.whatthepost.com/" & UrlPath
                    Return UrlPath
                Catch ex As Exception

                End Try
            Case ".docx"
                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                '    myfilename = myfilename.Replace(" ", "-")
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".docx"
                ' myfilename = myfilename.Replace(" ", "-")
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    NewFile.Write(myimageData, 0, file.ContentLength)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    '    UrlPath = "http://www.whatthepost.com/" & UrlPath
                    Return UrlPath
                Catch ex As Exception

                End Try
            Case "application/vnd.ms-powerpoint"
                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                '    myfilename = myfilename.Replace(" ", "-")
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".ppt"
                'myfilename = myfilename.Replace(" ", "-")
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    NewFile.Write(myimageData, 0, file.ContentLength)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    '    UrlPath = "http://www.whatthepost.com/" & UrlPath
                    Return UrlPath
                Catch ex As Exception

                End Try
            Case "application/vnd.openxmlformats-officedocument.presentationml.presentation"
                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                '    myfilename = myfilename.Replace(" ", "-")
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".pptx"
                'myfilename = myfilename.Replace(" ", "-")
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    NewFile.Write(myimageData, 0, file.ContentLength)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    '    UrlPath = "http://www.whatthepost.com/" & UrlPath
                    Return UrlPath
                Catch ex As Exception

                End Try
            Case ".pptx"
                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                '    myfilename = myfilename.Replace(" ", "-")
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".pptx"
                ' myfilename = myfilename.Replace(" ", "-")
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    NewFile.Write(myimageData, 0, file.ContentLength)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    '    UrlPath = "http://www.whatthepost.com/" & UrlPath
                    Return UrlPath
                Catch ex As Exception

                End Try

            Case "application/vnd.ms-excel"
                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                '    myfilename = myfilename.Replace(" ", "-")
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".xls"
                'myfilename = myfilename.Replace(" ", "-")
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    NewFile.Write(myimageData, 0, file.ContentLength)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    '    UrlPath = "http://www.whatthepost.com/" & UrlPath
                    Return UrlPath
                Catch ex As Exception

                End Try
            Case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                '    myfilename = myfilename.Replace(" ", "-")
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".xlsx"
                'myfilename = myfilename.Replace(" ", "-")
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    NewFile.Write(myimageData, 0, file.ContentLength)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    '    UrlPath = "http://www.whatthepost.com/" & UrlPath
                    Return UrlPath
                Catch ex As Exception

                End Try
            Case ".xlsx"
                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                '    myfilename = myfilename.Replace(" ", "-")
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".xlsx"
                ' myfilename = myfilename.Replace(" ", "-")
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    NewFile.Write(myimageData, 0, file.ContentLength)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    '    UrlPath = "http://www.whatthepost.com/" & UrlPath
                    Return UrlPath
                Catch ex As Exception

                End Try
            Case "image/jpeg"
                '      MyFile.InputStream.Read(myimageData, 0, FileLength)

                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".jpg"
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    '    NewFile.Write(myimageData, 0, FileLength)
                    Dim BIT As Bitmap = New Bitmap(st)
                    Dim thumb As Image = BIT.GetThumbnailImage(imgthumbWidth, ImgThumbHeight, Nothing, New IntPtr)
                    Dim oGraphics As Graphics = Graphics.FromImage(thumb)

                    ' Set the properties for the new graphic file
                    oGraphics.SmoothingMode = SmoothingMode.AntiAlias
                    oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic
                    ' Draw the new graphic based on the resized bitmap
                    oGraphics.DrawImage(BIT, 0, 0, thumbIMGWidth, ImgThumbHeight)
                    thumb.Save(NewFile, Imaging.ImageFormat.Jpeg)

                    'BIT.Save(NewFile, Imaging.ImageFormat.Jpeg)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    Return UrlPath
                Catch ex As Exception

                End Try
            Case "image/tiff"
                '      MyFile.InputStream.Read(myimageData, 0, FileLength)

                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".tif"
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    '    NewFile.Write(myimageData, 0, FileLength)
                    Dim BIT As Bitmap = New Bitmap(st)
                    Dim thumb As Image = BIT.GetThumbnailImage(imgthumbWidth, ImgThumbHeight, Nothing, New IntPtr)
                    Dim oGraphics As Graphics = Graphics.FromImage(thumb)

                    ' Set the properties for the new graphic file
                    oGraphics.SmoothingMode = SmoothingMode.AntiAlias
                    oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic
                    ' Draw the new graphic based on the resized bitmap
                    oGraphics.DrawImage(BIT, 0, 0, thumbIMGWidth, ImgThumbHeight)
                    thumb.Save(NewFile, Imaging.ImageFormat.Jpeg)

                    'BIT.Save(NewFile, Imaging.ImageFormat.Jpeg)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    Return UrlPath
                Catch ex As Exception

                End Try
            Case "image/png"

                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".png"
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    '    NewFile.Write(myimageData, 0, FileLength)
                    Dim BIT As Bitmap = New Bitmap(st)
                    Dim thumb As Image = BIT.GetThumbnailImage(OrigWidth, OrigHeight, Nothing, New IntPtr)
                    Dim oGraphics As Graphics = Graphics.FromImage(thumb)

                    ' Set the properties for the new graphic file
                    oGraphics.SmoothingMode = SmoothingMode.AntiAlias
                    oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic
                    ' Draw the new graphic based on the resized bitmap
                    oGraphics.DrawImage(BIT, 0, 0, OrigWidth, OrigHeight)
                    thumb.Save(NewFile, Imaging.ImageFormat.Png)

                    'BIT.Save(NewFile, Imaging.ImageFormat.Jpeg)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    Return UrlPath
                Catch ex As Exception

                End Try
            Case "image/bmp"

                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".bmp"
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    '    NewFile.Write(myimageData, 0, FileLength)
                    Dim BIT As Bitmap = New Bitmap(st)
                    Dim thumb As Image = BIT.GetThumbnailImage(imgthumbWidth, ImgThumbHeight, Nothing, New IntPtr)
                    Dim oGraphics As Graphics = Graphics.FromImage(thumb)

                    ' Set the properties for the new graphic file
                    oGraphics.SmoothingMode = SmoothingMode.AntiAlias
                    oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic
                    ' Draw the new graphic based on the resized bitmap
                    oGraphics.DrawImage(BIT, 0, 0, thumbIMGWidth, ImgThumbHeight)
                    thumb.Save(NewFile, Imaging.ImageFormat.Jpeg)

                    'BIT.Save(NewFile, Imaging.ImageFormat.Jpeg)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    Return UrlPath
                Catch ex As Exception

                End Try
            Case "image/gif"

                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".gif"
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    '    NewFile.Write(myimageData, 0, FileLength)
                    Dim BIT As Bitmap = New Bitmap(st)
                    Dim thumb As Image = BIT.GetThumbnailImage(imgthumbWidth, ImgThumbHeight, Nothing, New IntPtr)
                    Dim oGraphics As Graphics = Graphics.FromImage(thumb)

                    ' Set the properties for the new graphic file
                    oGraphics.SmoothingMode = SmoothingMode.AntiAlias
                    oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic
                    ' Draw the new graphic based on the resized bitmap
                    oGraphics.DrawImage(BIT, 0, 0, thumbIMGWidth, ImgThumbHeight)
                    thumb.Save(NewFile, Imaging.ImageFormat.Jpeg)

                    'BIT.Save(NewFile, Imaging.ImageFormat.Jpeg)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    Return UrlPath
                Catch ex As Exception

                End Try

            Case ".png"

                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".png"
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    '    NewFile.Write(myimageData, 0, FileLength)
                    Dim BIT As Bitmap = New Bitmap(st)
                    Dim thumb As Image = BIT.GetThumbnailImage(OrigWidth, OrigHeight, Nothing, New IntPtr)
                    Dim oGraphics As Graphics = Graphics.FromImage(thumb)

                    ' Set the properties for the new graphic file
                    oGraphics.SmoothingMode = SmoothingMode.AntiAlias
                    oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic
                    ' Draw the new graphic based on the resized bitmap
                    oGraphics.DrawImage(BIT, 0, 0, OrigWidth, OrigHeight)
                    thumb.Save(NewFile, Imaging.ImageFormat.Png)

                    'BIT.Save(NewFile, Imaging.ImageFormat.Jpeg)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    Return UrlPath
                Catch ex As Exception

                End Try

            Case ".gif"
                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".gif"
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    '    NewFile.Write(myimageData, 0, FileLength)
                    Dim BIT As Bitmap = New Bitmap(st)
                    Dim thumb As Image = BIT.GetThumbnailImage(imgthumbWidth, ImgThumbHeight, Nothing, New IntPtr)
                    Dim oGraphics As Graphics = Graphics.FromImage(thumb)

                    ' Set the properties for the new graphic file
                    oGraphics.SmoothingMode = SmoothingMode.AntiAlias
                    oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic
                    ' Draw the new graphic based on the resized bitmap
                    oGraphics.DrawImage(BIT, 0, 0, thumbIMGWidth, ImgThumbHeight)
                    thumb.Save(NewFile, Imaging.ImageFormat.Jpeg)

                    'BIT.Save(NewFile, Imaging.ImageFormat.Jpeg)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    Return UrlPath
                Catch ex As Exception

                End Try
            Case "application/pdf"
                Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                Dim FileAppend As Integer = 0
                Dim i As Integer = 0
                myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".pdf"
                Dim NewFile As System.IO.FileStream
                Try
                    NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                    NewFile.Write(myimageData, 0, file.ContentLength)
                    NewFile.Close()
                    Dim UrlPath As String '= "NewsImages/" & myfilename
                    UrlPath = DestPath & myfilename
                    Return UrlPath
                Catch ex As Exception

                End Try
                'Case "application/vnd.ms-excel"
                '    MyFile.InputStream.Read(myimageData, 0, FileLength)
                '    Dim myfilename As String = System.IO.Path.GetFileName(MyFile.FileName)
                '    Dim FileAppend As Integer = 0
                '    Dim i As Integer = 0
                '    myfilename = System.IO.Path.GetFileNameWithoutExtension(MyFile.FileName) & Namer & ".xls"
                '    Dim NewFile As System.IO.FileStream
                '    Try
                '        NewFile = New System.IO.FileStream(SavePath & myfilename, FileMode.Create)
                '        NewFile.Write(myimageData, 0, FileLength)
                '        Dim BIT As Bitmap = New Bitmap(400, 400, PixelFormat.Format24bppRgb)
                '        BIT.Save(NewFile, Imaging.ImageFormat.Jpeg)
                '        NewFile.Close()
                '        Dim UrlPath As String '= "NewsImages/" & myfilename
                '        UrlPath = DestPath & myfilename
                '        Return UrlPath
                '    Catch ex As Exception

                '    End Try





            Case Else
                Return ""
                Exit Function
        End Select
        Return ""
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
