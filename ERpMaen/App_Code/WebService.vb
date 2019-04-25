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
Imports OpenPop.Mime.Header
Imports System.Net.Mail
Imports ERpMaen
#End Region
'Imports System.Xml
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<System.Web.Script.Services.ScriptService()>
Public Class WebService
    Inherits System.Web.Services.WebService

#Region "Global_Varaibles"
    Dim urlOnline As String = "-"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
    Dim pF As New PublicFunctions
#End Region

    '#Region "General"

    '    ' ''' <summary>
    '    ' '''Get User id of loged user
    '    ' ''' </summary>
    '    ' ''' 
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetSalesmanFromCoockies(ByVal prifix As String) As String
    '        Try
    '            Dim UserId = Context.Request.Cookies("UserInfo")("UserId")
    '            Return UserId
    '        Catch ex As Exception
    '            Return "0"
    '        End Try
    '    End Function


    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Sub update_user_status(ByVal prefixText As String)
    '        Dim status = prefixText
    '        Dim user_id = Context.Request.Cookies("UserInfo")("UserId")
    '        PublicFunctions.Update_user_status(user_id, status)
    '    End Sub
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Sub savechat(ByVal prefixText As String)
    '        Dim reciever_id = prefixText.Split("|")(0)
    '        Dim message = prefixText.Split("|")(1)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Dim dt As DateTime = Now.Date
    '        dt = dt.ToShortDateString()
    '        dt = Date.ParseExact(dt, "dd/MM/yyyy", Nothing)
    '        Dim sender_id = Context.Request.Cookies("UserInfo")("UserId")
    '        Try
    '            DBManager.ExcuteQuery("insert into tblchat (sender_id,message,reciever_id,time,seen) values (" + sender_id + ",'" + message + "'," + reciever_id + "," + dt + ",0)")
    '            DBManager.ExcuteQuery("update tblchat set seen = 1 where reciever_id =" + sender_id + "and sender_id= " + reciever_id) '  values (" + sender_id + ",'" + message + "'," + reciever_id + ",'" + DateAndTime.Now + "',0)")
    '        Catch ex As Exception
    '        End Try
    '    End Sub
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Sub saveistyping(ByVal prefixText As String)
    '        Dim reciever_id = prefixText
    '        Dim sender_id = Context.Request.Cookies("UserInfo")("UserId")
    '        Try
    '            DBManager.ExcuteQuery("insert into istyping (sender_id,reciever_id,istyping) values (" + sender_id + "," + reciever_id + ", 1)")
    '        Catch ex As Exception
    '        End Try
    '    End Sub
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Sub empty_istyping()
    '        Dim sender_id = Context.Request.Cookies("UserInfo")("UserId")
    '        Try
    '            DBManager.ExcuteQuery("delete from istyping where sender_id =" + sender_id)
    '        Catch ex As Exception
    '        End Try
    '    End Sub
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function getchat(ByVal prefixText As String) As String()
    '        Dim reciever_id = prefixText.Split("|")(0)
    '        Dim message = prefixText.Split("|")(1)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Dim sender_id = Context.Request.Cookies("UserInfo")("UserId")
    '        Try
    '            Dim dt As New DataTable
    '            dt = DBManager.Getdatatable(" select *,(select name from tblUsers where id = tblchat.sender_id) as sender_name ,(select name from tblUsers where id =" + reciever_id + ") as other_name  from tblchat where (sender_id =" + sender_id + " and reciever_id =" + reciever_id + ") or (sender_id = " + reciever_id + "and reciever_id =" + sender_id + ") order by id")
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("sender_name").ToString & "|" & dt(x)("message").ToString & "|" & dt(x)("time").ToString & "|" & dt(x)("other_name").ToString)
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                'Names.Add(xvalue & "|" & yvalue & "|" & "No Property Fount In This Points" & "|" & "No Property Fount In This Points" & "|" & "No Property Fount In This Points" & "|" & "No Property Fount In This Points")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function chek_new_messages(ByVal prefixText As String) As String()
    '        Dim reciever_id = prefixText.Split("|")(0)
    '        Dim message = prefixText.Split("|")(1)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Dim sender_id = Context.Request.Cookies("UserInfo")("UserId")
    '        Try
    '            Dim dt As New DataTable
    '            dt = DBManager.Getdatatable(" select * from tblchat where reciever_id=" + sender_id + " and seen = 0")
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("sender_id").ToString)
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                'Names.Add(xvalue & "|" & yvalue & "|" & "No Property Fount In This Points" & "|" & "No Property Fount In This Points" & "|" & "No Property Fount In This Points" & "|" & "No Property Fount In This Points")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function Getistyping(ByVal prefixText As String) As String
    '        Dim sender_id = prefixText
    '        Dim reciever_id = Context.Request.Cookies("UserInfo")("UserId")
    '        Try
    '            Dim dt As New DataTable
    '            dt = DBManager.Getdatatable(" select * from istyping where reciever_id = " + reciever_id + " and sender_id=" + sender_id)
    '            If dt.Rows.Count <> 0 Then
    '                Return "True"
    '            Else
    '                Return "False"
    '            End If
    '        Catch ex As Exception
    '            Return "False"
    '        End Try

    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function get_online_users(ByVal prefixText As String) As String()
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Dim user_id = Context.Request.Cookies("UserInfo")("UserId")
    '        Try
    '            Dim dt As New DataTable
    '            dt = DBManager.Getdatatable(" select * from tblusers where id !=" + user_id)
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Dim photo = dt(x)("Photo").ToString
    '                    If photo = "" Then
    '                        photo = urlOnline + "App_Themes/images/Salesman.png"
    '                    End If
    '                    Names.Add(dt(x)("id").ToString & "|" & dt(x)("name").ToString & "|" & photo & "|" & dt(x)("On_Off").ToString)
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                'Names.Add(xvalue & "|" & yvalue & "|" & "No Property Fount In This Points" & "|" & "No Property Fount In This Points" & "|" & "No Property Fount In This Points" & "|" & "No Property Fount In This Points")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function

    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetLookupDataTypes(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select distinct(TblLockup.Type), Name from TblLockup inner join tblLockupSettings on TblLockup.Type = tblLockupSettings.Type order by Name")
    '            Else
    '                dt = DBManager.Getdatatable("select distinct(TblLockup.Type), Name from TblLockup inner join tblLockupSettings on TblLockup.Type = tblLockupSettings.Type where Name Like '%" + prefixText + "%' order by Name")
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Type").ToString.Trim + "|" + dt(x)("Name").ToString.Trim)
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add("No results found!")
    '        Return Names.ToArray
    '    End Function

    '    <WebMethod()> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetPropertiesMapsOne(ByVal prefixText As String) As String()
    '        Dim xvalue = prefixText.Split("|")(0)
    '        Dim yvalue = prefixText.Split("|")(1)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            Dim dt As DataTable = DBManager.Getdatatable("select *,TblProperties.Price,TblPropertiesImage.OnlineUrl,TblPropertiesDetails.GeoPoint1,TblPropertiesDetails.GeoPoint2 from TblProperties INNER JOIN  dbo.TblPropertiesDetails ON dbo.TblProperties.Id = dbo.TblPropertiesDetails.PropId left JOIN TblPropertiesImage on dbo.TblProperties.Id = TblPropertiesImage.AutoCode where TblPropertiesDetails.GeoPoint1 ='" + xvalue + "'  and TblPropertiesDetails.GeoPoint2 ='" + yvalue + "'")
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("GeoPoint1").ToString & "|" & dt(x)("GeoPoint2").ToString & "|" & dt(x)("Name").ToString & "|" & dt(x)("Description").ToString & "|" & dt(x)("OnlineUrl").ToString & "|" & dt(x)("CostPrice").ToString)
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add(xvalue & "|" & yvalue & "|" & "No Property Fount In This Points" & "|" & "No Property Fount In This Points" & "|" & "No Property Fount In This Points" & "|" & "No Property Fount In This Points")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '        <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUserCity(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='C'")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='C' and Description Like '%" + prefixText + "%'")
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '     <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetPropertyStatus(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='PS'")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='PS' and Description Like '%" + prefixText + "%'")
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '       <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUserNationality(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='NA'")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='NA' and Description Like '%" + prefixText + "%'")
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUsers(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblUsers)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = New TblUsersFactory().GetAllByCustom("deleted is null or deleted ='false'")
    '            Else
    '                dt = New TblUsersFactory().GetAllByCustom("Id like'%" & prefixText & "%' or FirstName like '%" & prefixText & "%' and (deleted is null or deleted ='false')")
    '            End If
    '            If dt.Count <> 0 Then
    '                For Each s As TblUsers In dt
    '                    Names.Add(s.Id.ToString + "|" + s.FirstName + " " + s.LastName)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function


    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function Commercialtype(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='V'")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='V' and Description Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '   <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetCategory(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='CA'")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='CA' and Description Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '    Public Function ScopeOfView(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='SV'")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='SV' and Description Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetSalesmanCode(ByVal prefixText As String) As String()
    '        Dim dt As DataTable
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select TblUsers.Code from TblUsers Inner Join TblUsers on dbo.TblUsers.AssignUserID = TblUsers.Id where (TblUsers.deleted ='False' or TblUsers.deleted is null)")
    '            Else
    '                dt = DBManager.Getdatatable("select TblUsers.Code from TblUsers Inner Join TblUsers on dbo.TblUsers.AssignUserID = TblUsers.Id where TblUsers.Code like'%" & prefixText & "%' and (TblUsers.deleted ='False' or TblUsers.deleted is null)")
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt.Rows(x).Item("Code"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetSalesmanName(ByVal prefixText As String) As String()
    '        Dim dt As DataTable
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select TblUsers.Name from TblUsers Inner Join TblUsers on dbo.TblUsers.AssignUserID = TblUsers.Id where (TblUsers.deleted ='False' or TblUsers.deleted is null)")
    '            Else
    '                dt = DBManager.Getdatatable("select TblUsers.Name from TblUsers Inner Join TblUsers on dbo.TblUsers.AssignUserID = TblUsers.Id where TblUsers.Name like'%" & prefixText & "%' and (TblUsers.deleted ='False' or TblUsers.deleted is null)")
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt.Rows(x).Item("Name"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function


    '    <WebMethod()> _
    '   <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetProperties(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblProperties)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = New TblPropertiesFactory().GetAll()
    '            Else
    '                dt = New TblPropertiesFactory().GetAllByCustom("Code like'%" & prefixText & "%' or Name like '%" & prefixText & "%'")
    '            End If
    '            If dt.Count <> 0 Then
    '                For Each s As TblProperties In dt
    '                    Names.Add(s.Code + "|" + s.Name)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetRentProperties(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '        Dim usertype = clsGeneralVariables.usertype
    '        Try
    '            If prefixText = "*" Then
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("select * from TblProperties inner join TblPropertiesDetails ON dbo.TblProperties.Id = dbo.TblPropertiesDetails.PropId where  RentOrSale ='False' and salesman='" + salesmancode + "'")
    '                Else
    '                    dt = DBManager.Getdatatable("select * from TblProperties inner join TblPropertiesDetails ON dbo.TblProperties.Id = dbo.TblPropertiesDetails.PropId where  RentOrSale ='False'")
    '                End If
    '            Else
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("select * from TblProperties inner join TblPropertiesDetails ON dbo.TblProperties.Id = dbo.TblPropertiesDetails.PropId where  (Code like'%" & prefixText & "%' or Name like '%" & prefixText & "%') and RentOrSale ='False' and salesman='" + salesmancode + "' ")
    '                Else
    '                    dt = DBManager.Getdatatable("select * from TblProperties where  (Code like'%" & prefixText & "%' or Name like '%" & prefixText & "%') and RentOrSale ='False' ")
    '                End If
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Code").ToString + "|" + dt(x)("Name"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetShortTermProperties(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '        Dim usertype = clsGeneralVariables.usertype
    '        Try
    '            If prefixText = "*" Then
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("select * from TblProperties inner join TblPropertiesDetails ON dbo.TblProperties.Id = dbo.TblPropertiesDetails.PropId where  TblPropertiesDetails.ShortTerm='true' and salesman='" + salesmancode + "'")
    '                Else
    '                    dt = DBManager.Getdatatable("select * from TblProperties inner join TblPropertiesDetails ON dbo.TblProperties.Id = dbo.TblPropertiesDetails.PropId where  TblPropertiesDetails.ShortTerm='true'")
    '                End If
    '            Else
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("select * from TblProperties inner join TblPropertiesDetails ON dbo.TblProperties.Id = dbo.TblPropertiesDetails.PropId where  (Code like'%" & prefixText & "%' or Name like '%" & prefixText & "%') and RentOrSale ='False' and salesman='" + salesmancode + "' ")
    '                Else
    '                    dt = DBManager.Getdatatable("select * from TblProperties where  (Code like'%" & prefixText & "%' or Name like '%" & prefixText & "%') and RentOrSale ='False' ")
    '                End If
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Code").ToString + "|" + dt(x)("Name"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetSaleProperties(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '        Dim usertype = clsGeneralVariables.usertype
    '        Try
    '            If prefixText = "*" Then
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("select * from TblProperties inner join TblPropertiesDetails ON dbo.TblProperties.Id = dbo.TblPropertiesDetails.PropId where  RentOrSale ='True' and salesman='" + salesmancode + "'")
    '                Else
    '                    dt = DBManager.Getdatatable("select * from TblProperties inner join TblPropertiesDetails ON dbo.TblProperties.Id = dbo.TblPropertiesDetails.PropId where  RentOrSale ='True'")
    '                End If
    '            Else
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("select * from TblProperties inner join TblPropertiesDetails ON dbo.TblProperties.Id = dbo.TblPropertiesDetails.PropId where  (Code like'%" & prefixText & "%' or Name like '%" & prefixText & "%') and RentOrSale ='True' and salesman='" + salesmancode + "' ")
    '                Else
    '                    dt = DBManager.Getdatatable("select * from TblProperties where  (Code like'%" & prefixText & "%' or Name like '%" & prefixText & "%') and RentOrSale ='True' ")
    '                End If
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Code").ToString + "|" + dt(x)("Name"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetSalePropertiesCoresspondance(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '        Dim usertype = clsGeneralVariables.usertype
    '        Try
    '            If prefixText = "*" Then
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("select * from TblProperties inner join TblPropertiesDetails ON dbo.TblProperties.Id = dbo.TblPropertiesDetails.PropId where dbo.TblProperties.Id in (select Unitid from TblCorrespondence)  and salesman='" + salesmancode + "'")
    '                Else
    '                    dt = DBManager.Getdatatable("select * from TblProperties inner join TblPropertiesDetails ON dbo.TblProperties.Id = dbo.TblPropertiesDetails.PropId where dbo.TblProperties.Id in (select Unitid from TblCorrespondence) ")
    '                End If
    '            Else
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("select * from TblProperties inner join TblPropertiesDetails ON dbo.TblProperties.Id = dbo.TblPropertiesDetails.PropId where  (Code like'%" & prefixText & "%' or Name like '%" & prefixText & "%') and dbo.TblProperties.Id in (select Unitid from TblCorrespondence)  and salesman='" + salesmancode + "' ")
    '                Else
    '                    dt = DBManager.Getdatatable("select * from TblProperties where  (Code like'%" & prefixText & "%' or Name like '%" & prefixText & "%')  and dbo.TblProperties.Id in (select Unitid from TblCorrespondence) ")
    '                End If
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Code").ToString + "|" + dt(x)("Name"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetContracts(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblContracts)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0

    '        Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '        Dim usertype = clsGeneralVariables.usertype
    '        Try
    '            If prefixText = "*" Then
    '                If usertype <> "Admin" Then
    '                    dt = New TblContractsFactory().GetAllByCustom("SalesmanCode='" + salesmancode + "' ")
    '                Else
    '                    dt = New TblContractsFactory().GetAll()
    '                End If
    '            Else
    '                If usertype <> "Admin" Then
    '                    dt = New TblContractsFactory().GetAllByCustom("SalesmanCode='" + salesmancode + "' and  (Code like'%" & prefixText & "%' or Description like '%" & prefixText & "%' )")
    '                Else
    '                    dt = New TblContractsFactory().GetAllByCustom("Code like'%" & prefixText & "%' or Description like '%" & prefixText & "%'")
    '                End If
    '            End If
    '            If dt.Count <> 0 Then
    '                For Each s As TblContracts In dt
    '                    Names.Add(s.Code + "|" + s.Description)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnit(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblProperties)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = New TblPropertiesFactory().GetAll()
    '            Else
    '                dt = New TblPropertiesFactory().GetAllByCustom("AutoCode like'%" & prefixText & "%' or Name like '%" & prefixText & "%'")
    '            End If
    '            If dt.Count <> 0 Then
    '                For Each s As TblProperties In dt
    '                    Names.Add(s.Name)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetContract(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblContracts)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = New TblContractsFactory().GetAll()
    '            Else
    '                dt = New TblContractsFactory().GetAllByCustom("Code like'%" & prefixText & "%' or Description like '%" & prefixText & "%'")
    '            End If
    '            If dt.Count <> 0 Then
    '                For Each s As TblContracts In dt
    '                    Names.Add(s.Description)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetFax(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select Tc.*,case when TC.Type='C' then TcC.FaxNo else TcD.FaxNo end as Fax from TblContacts TC" &
    '" left outer join dbo.TblContactsDetails TCD on(tc.autocode=tcd.autocode)" &
    '" left outer join dbo.tblCRMCompaniesDetails TCC on(tc.autocode=tcd.autocode)" &
    '  " where owner=1 and ( TcD.FaxNo is not null or TcC.FaxNo is not null)")
    '            Else
    '                dt = DBManager.Getdatatable("select Tc.*,case when TC.Type='C' then TcC.FaxNo else TcD.FaxNo end as Fax from TblContacts TC" &
    '" left outer join dbo.TblContactsDetails TCD on(tc.autocode=tcd.autocode)" &
    '" left outer join dbo.tblCRMCompaniesDetails TCC on(tc.autocode=tcd.autocode)" &
    '  " where owner=1 and ( TcD.FaxNo is not null or TcC.FaxNo is not null) and ( TcD.FaxNo like '%" + prefixText + "%' or TcC.FaxNo like '%" + prefixText + "%')")

    '                'DBManager.Getdatatable("select * from TblLockup where Type ='SV' and Description Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Fax"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetNationality(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='NA' and (deleted ='False' or deleted is null)")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='NA' and Description Like '%" + prefixText + "%' and (deleted ='False' or deleted is null)")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    '  <WebMethod()> _
    '    '<System.Web.Script.Services.ScriptMethod()>
    '    '  Public Function GetContacts(ByVal prefixText As String) As String()
    '    '      Dim SalesManCode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '    '      Dim UserId = Context.Request.Cookies("UserInfo")("UserId")
    '    '      Dim dt As New List(Of TblContacts)
    '    '      Dim Names As New List(Of String)
    '    '      Dim x As Integer = 0
    '    '      Dim usertype = clsGeneralVariables.usertype
    '    '      Try
    '    '          If prefixText = "*" Then
    '    '              If usertype <> "Admin" Then
    '    '                  dt = New TblContactsFactory().GetAllByCustom("salesman='" + SalesManCode + "'")
    '    '              Else
    '    '                  dt = New TblContactsFactory().GetAll()
    '    '              End If
    '    '          Else
    '    '              If usertype <> "Admin" Then
    '    '                  dt = New TblContactsFactory().GetAllByCustom("salesman='" + SalesManCode + "' and (Code like'%" & prefixText & "%' or EFullName like '%" & prefixText & "%')")
    '    '              Else
    '    '                  dt = New TblContactsFactory().GetAllByCustom("Code like'%" & prefixText & "%' or EFullName like '%" & prefixText & "%'")
    '    '              End If
    '    '          End If
    '    '          If dt.Count <> 0 Then
    '    '              For Each s As TblContacts In dt
    '    '                  Names.Add(s.Code + "|" + s.EFullName)
    '    '              Next
    '    '              Return Names.ToArray
    '    '          Else
    '    '              Names.Add("No results found!")
    '    '              Return Names.ToArray
    '    '          End If
    '    '      Catch ex As Exception
    '    '      End Try
    '    '      Return Nothing
    '    '  End Function
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetPropertyCode(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
    '        Dim dt As New DataTable
    '        Dim Names As New List(Of String)(10)
    '        If contextKey = "Sale" Then
    '            contextKey = PublicFunctions.GetLockupId("Sale", "UP")
    '        ElseIf contextKey = "Rent" Then
    '            contextKey = PublicFunctions.GetLockupId("Rent", "UP") + "' or Purpose='" + PublicFunctions.GetLockupId("Short Term", "UP") + ""
    '        Else
    '            contextKey = String.Empty
    '        End If
    '        Dim x As Integer = 0
    '        Try

    '            If contextKey = String.Empty Then
    '                If prefixText = "*" Then
    '                    dt = DBManager.Getdatatable("SELECT Code from tblProperties")
    '                Else
    '                    dt = DBManager.Getdatatable("SELECT Code from tblProperties where Code Like '%" + prefixText + "%'")
    '                End If
    '            Else
    '                If prefixText = "*" Then
    '                    dt = DBManager.Getdatatable("SELECT Code from tblProperties where Purpose='" + contextKey + "'")
    '                Else
    '                    dt = DBManager.Getdatatable("SELECT Code from tblProperties where (Purpose='" + contextKey + "') and Code Like '%" + prefixText + "%'")
    '                End If
    '            End If


    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Code"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitCodeCommunity(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("SELECT prop.Code,(select Description from TblLockup where ID=prop.Community) as Community,prop.Community as CommunityCode FROM TblLeads prop")
    '            Else
    '                dt = DBManager.Getdatatable("SELECT prop.Code,(select Description from TblLockup where ID=prop.Community) as Community,prop.Community as CommunityCode FROM TblLeads prop where prop.code like '%" + prefixText + "%' or (select Description from TblLockup where ID=prop.Community) Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Code") + "|" + dt(x)("Community"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitCodeCommunityCoresspondance(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '        Dim usertype = clsGeneralVariables.usertype
    '        Dim x As Integer = 0
    '        Try

    '            If prefixText = "*" Then
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("SELECT TblProperties.Code,(select Description from TblLockup where ID=TblPropertiesDetails.Community) as Community,TblPropertiesDetails.Community as CommunityCode FROM TblProperties inner join TblPropertiesDetails on TblProperties.Id= TblPropertiesDetails.PropId where salesman='" + salesmancode + "'")
    '                Else
    '                    dt = DBManager.Getdatatable("SELECT TblProperties.Code,(select Description from TblLockup where ID=TblPropertiesDetails.Community) as Community,TblPropertiesDetails.Community as CommunityCode FROM TblProperties inner join TblPropertiesDetails on TblProperties.Id= TblPropertiesDetails.PropId ")
    '                End If
    '            Else
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("SELECT TblProperties.Code,(select Description from TblLockup where ID=TblPropertiesDetails.Community) as Community,TblPropertiesDetails.Community as CommunityCode FROM TblProperties inner join TblPropertiesDetails on TblProperties.Id= TblPropertiesDetails.PropId  where salesman='" + salesmancode + "' and ( TblProperties.code like '%" + prefixText + "%' or (select Description from TblLockup where ID=TblPropertiesDetails.Community) Like '%" + prefixText + "%')")
    '                Else
    '                    dt = DBManager.Getdatatable("SELECT TblProperties.Code,(select Description from TblLockup where ID=TblPropertiesDetails.Community) as Community,TblPropertiesDetails.Community as CommunityCode FROM TblProperties inner join TblPropertiesDetails on TblProperties.Id= TblPropertiesDetails.PropId  where ( TblProperties.code like '%" + prefixText + "%' or (select Description from TblLockup where ID=TblPropertiesDetails.Community) Like '%" + prefixText + "%')")
    '                End If
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Code") + "|" + dt(x)("Community"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitnumbersubCommunityCoresspondance(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '        Dim usertype = clsGeneralVariables.usertype
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("SELECT TblPropertiesDetails.UnitNo,(select Description from TblLockup where ID=TblPropertiesDetails.SubCommunity) as SubCommunity,TblPropertiesDetails.SubCommunity as CommunityCode FROM TblProperties inner join TblPropertiesDetails on TblProperties.Id= TblPropertiesDetails.PropId where salesman='" + salesmancode + "'")
    '                Else
    '                    dt = DBManager.Getdatatable("SELECT TblPropertiesDetails.UnitNumber,(select Description from TblLockup where ID=TblPropertiesDetails.SubCommunity) as SubCommunity,TblPropertiesDetails.SubCommunity as CommunityCode FROM TblProperties inner join TblPropertiesDetails on TblProperties.Id= TblPropertiesDetails.PropId ")
    '                End If
    '            Else
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("SELECT TblPropertiesDetails.UnitNumber,(select Description from TblLockup where ID=TblPropertiesDetails.SubCommunity) as SubCommunity,TblPropertiesDetails.SubCommunity as CommunityCode FROM TblProperties inner join TblPropertiesDetails on TblProperties.Id= TblPropertiesDetails.PropId  where salesman='" + salesmancode + "' and ( TblPropertiesDetails.UnitNumber like '%" + prefixText + "%' or (select Description from TblLockup where ID=TblPropertiesDetails.SubCommunity) Like '%" + prefixText + "%')")
    '                Else
    '                    dt = DBManager.Getdatatable("SELECT TblPropertiesDetails.UnitNumber,(select Description from TblLockup where ID=TblPropertiesDetails.SubCommunity) as SubCommunity,TblPropertiesDetails.SubCommunity as CommunityCode FROM TblProperties inner join TblPropertiesDetails on TblProperties.Id= TblPropertiesDetails.PropId  where ( TblPropertiesDetails.UnitNumber like '%" + prefixText + "%' or (select Description from TblLockup where ID=TblPropertiesDetails.SubCommunity) Like '%" + prefixText + "%')")
    '                End If
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("UnitNumber") + "|" + dt(x)("SubCommunity"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitNameCoresspondance(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '        Dim usertype = clsGeneralVariables.usertype
    '        Dim x As Integer = 0
    '        Try

    '            If prefixText = "*" Then
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("SELECT TblProperties.Name,(select Description from TblLockup where ID=TblPropertiesDetails.Community) as Community,TblPropertiesDetails.Community as CommunityCode FROM TblProperties inner join TblPropertiesDetails on TblProperties.Id= TblPropertiesDetails.PropId where salesman='" + salesmancode + "'")
    '                Else
    '                    dt = DBManager.Getdatatable("SELECT TblProperties.Name,(select Description from TblLockup where ID=TblPropertiesDetails.Community) as Community,TblPropertiesDetails.Community as CommunityCode FROM TblProperties inner join TblPropertiesDetails on TblProperties.Id= TblPropertiesDetails.PropId ")
    '                End If
    '            Else
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("SELECT TblProperties.Name,(select Description from TblLockup where ID=TblPropertiesDetails.Community) as Community,TblPropertiesDetails.Community as CommunityCode FROM TblProperties inner join TblPropertiesDetails on TblProperties.Id= TblPropertiesDetails.PropId  where salesman='" + salesmancode + "' and ( TblProperties.Name like '%" + prefixText + "%')")
    '                Else
    '                    dt = DBManager.Getdatatable("SELECT TblProperties.Name,(select Description from TblLockup where ID=TblPropertiesDetails.Community) as Community,TblPropertiesDetails.Community as CommunityCode FROM TblProperties inner join TblPropertiesDetails on TblProperties.Id= TblPropertiesDetails.PropId  where  TblProperties.Name like '%" + prefixText + "%'")
    '                End If
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Name") + "|" + dt(x)("Community"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    ' <WebMethod()> _
    '    '<System.Web.Script.Services.ScriptMethod()>
    '    ' Public Function GetUnitName(ByVal prefixText As String) As String()
    '    '     Dim dt As New List(Of TblLeads)
    '    '     Dim Names As New List(Of String)
    '    '     Dim x As Integer = 0
    '    '     Try
    '    '         If prefixText = "*" Then
    '    '             dt = New TblLeadsFactory().GetAllByCustom("name is not null")
    '    '         Else
    '    '             dt = New TblLeadsFactory().GetAllByCustom("Name like'%" & prefixText & "%'")
    '    '         End If
    '    '         If dt.Count <> 0 Then
    '    '             For Each s As TblLeads In dt
    '    '                 Names.Add(s.Name)
    '    '             Next
    '    '             Return Names.ToArray
    '    '         Else
    '    '             Names.Add("No results found!")
    '    '             Return Names.ToArray
    '    '         End If
    '    '     Catch ex As Exception
    '    '     End Try
    '    '     Return Nothing
    '    ' End Function
    '    <WebMethod()> _
    '   <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetCustomersCalls(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            Dim userid = ""
    '            Dim usertype = clsGeneralVariables.usertype
    '            Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '            Dim dtsales As DataTable = DBManager.Getdatatable("select AssignUserID from TblUsers where Code='" + salesmancode + "'")
    '            If dtsales.Rows.Count <> 0 Then
    '                userid = dtsales.Rows(0).Item("AssignUserID").ToString
    '            End If
    '            If prefixText = "*" Then
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("SELECT *, CONVERT(VARCHAR(10),CallDate , 103) + ' '  + convert(VARCHAR(8), CallDate, 14) as dateCall,CONVERT(VARCHAR(10),FollowUpDate , 103) + ' '  + convert(VARCHAR(8), FollowUpDate, 14) as DateFollow, (select description from TblLockup where Id= CmmType) as 'CorresType', case when InOut = 'I' THEN 'IN' when InOut = 'O' THEN 'OUT' End as 'InOutCall'  FROM TblCorrespondence inner join TblContacts on TblContacts.AutoCode=TblCorrespondence.ClientId where unitId is null and TblCorrespondence.UserId='" + userid + "'")
    '                Else
    '                    dt = DBManager.Getdatatable("SELECT *, CONVERT(VARCHAR(10),CallDate , 103) + ' '  + convert(VARCHAR(8), CallDate, 14) as dateCall,CONVERT(VARCHAR(10),FollowUpDate , 103) + ' '  + convert(VARCHAR(8), FollowUpDate, 14) as DateFollow, (select description from TblLockup where Id= CmmType) as 'CorresType', case when InOut = 'I' THEN 'IN' when InOut = 'O' THEN 'OUT' End as 'InOutCall'  FROM TblCorrespondence inner join TblContacts on TblContacts.AutoCode=TblCorrespondence.ClientId where unitId is null")
    '                End If
    '            Else
    '                If usertype <> "Admin" Then
    '                    dt = DBManager.Getdatatable("SELECT *, CONVERT(VARCHAR(10),CallDate , 103) + ' '  + convert(VARCHAR(8), CallDate, 14) as dateCall,CONVERT(VARCHAR(10),FollowUpDate , 103) + ' '  + convert(VARCHAR(8), FollowUpDate, 14) as DateFollow, (select description from TblLockup where Id= CmmType) as 'CorresType', case when InOut = 'I' THEN 'IN' when InOut = 'O' THEN 'OUT' End as 'InOutCall'  FROM TblCorrespondence inner join TblContacts on TblContacts.AutoCode=TblCorrespondence.ClientId where unitId is null and TblCorrespondence.UserId='" + userid + "' and ( EFullName like '%" + prefixText + "%' or Subject like '%" + prefixText + "%' or Mobile like '%" + prefixText + "%')")
    '                Else
    '                    dt = DBManager.Getdatatable("SELECT *, CONVERT(VARCHAR(10),CallDate , 103) + ' '  + convert(VARCHAR(8), CallDate, 14) as dateCall,CONVERT(VARCHAR(10),FollowUpDate , 103) + ' '  + convert(VARCHAR(8), FollowUpDate, 14) as DateFollow, (select description from TblLockup where Id= CmmType) as 'CorresType', case when InOut = 'I' THEN 'IN' when InOut = 'O' THEN 'OUT' End as 'InOutCall'  FROM TblCorrespondence inner join TblContacts on TblContacts.AutoCode=TblCorrespondence.ClientId where unitId is null and  ( EFullName like '%" + prefixText + "%' or Subject like '%" + prefixText + "%' or Mobile like '%" + prefixText + "%')")
    '                End If
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Id").ToString + "|" + dt(x)("EFullName") + "|" + dt(x)("Subject") + "|" + dt(x)("Mobile"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetCustomersCallsLead(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblCorrespondence)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            Dim userid = ""
    '            Dim usertype = clsGeneralVariables.usertype
    '            Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '            Dim dtsales As DataTable = DBManager.Getdatatable("select AssignUserID from TblUsers where Code='" + salesmancode + "'")
    '            If dtsales.Rows.Count <> 0 Then
    '                userid = dtsales.Rows(0).Item("AssignUserID").ToString
    '            End If
    '            If prefixText = "*" Then
    '                If usertype <> "Admin" Then
    '                    dt = New TblCorrespondenceFactory().GetAllByCustom("Islead='True' and UserId='" + userid + "'")
    '                Else
    '                    dt = New TblCorrespondenceFactory().GetAllByCustom("Islead='True'")
    '                End If
    '            Else
    '                If usertype <> "Admin" Then
    '                    dt = New TblCorrespondenceFactory().GetAllByCustom("(CmmType like'%" & prefixText & "%' or Subject like '%" & prefixText & "%') and Islead='True' and userid='" + userid + "'")
    '                Else
    '                    dt = New TblCorrespondenceFactory().GetAllByCustom("(CmmType like'%" & prefixText & "%' or Subject like '%" & prefixText & "%') and Islead='True'")
    '                End If
    '            End If
    '            If dt.Count <> 0 Then
    '                For Each s As TblCorrespondence In dt
    '                    Dim type As String = ""
    '                    type = PublicFunctions.GetLockupValue(s.CorresType)
    '                    'If s.CmmType = "CA" Then
    '                    '    type = "CALL"
    '                    'ElseIf s.CmmType = "EM" Then
    '                    '    type = "EMAIL"
    '                    'ElseIf s.CmmType = "CO" Then
    '                    '    type = "COURIER"
    '                    'ElseIf s.CmmType = "FA" Then
    '                    '    type = "FAX"
    '                    'ElseIf s.CmmType = "VI" Then
    '                    '    type = "VISIT"
    '                    'End If
    '                    Names.Add(s.Id.ToString + "|" + s.CorresType.ToString + "|" + s.Subject)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            Names.Add("No results found!")
    '            Return Names.ToArray
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetCustomersCallsProperty(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblCorrespondence)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            Dim userid = ""
    '            Dim usertype = clsGeneralVariables.usertype
    '            Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '            Dim dtsales As DataTable = DBManager.Getdatatable("select AssignUserID from TblUsers where Code='" + salesmancode + "'")
    '            If dtsales.Rows.Count <> 0 Then
    '                userid = dtsales.Rows(0).Item("AssignUserID").ToString
    '            End If
    '            If prefixText = "*" Then
    '                If usertype <> "Admin" Then
    '                    dt = New TblCorrespondenceFactory().GetAllByCustom("Islead='False' and UserId ='" + userid + "'")
    '                Else
    '                    dt = New TblCorrespondenceFactory().GetAllByCustom("Islead='False'")
    '                End If
    '            Else
    '                If usertype <> "Admin" Then
    '                    dt = New TblCorrespondenceFactory().GetAllByCustom("(CmmType like'%" & prefixText & "%' or Subject like '%" & prefixText & "%') and Islead='False' and UserId ='" + userid + "'")
    '                Else
    '                    dt = New TblCorrespondenceFactory().GetAllByCustom("(CmmType like'%" & prefixText & "%' or Subject like '%" & prefixText & "%') and Islead='False'")
    '                End If
    '            End If

    '            If dt.Count <> 0 Then
    '                For Each s As TblCorrespondence In dt
    '                    Dim type As String = ""
    '                    If s.CorresType = "CA" Then
    '                        type = "CALL"
    '                    ElseIf s.CorresType = "EM" Then
    '                        type = "EMAIL"
    '                    ElseIf s.CorresType = "CO" Then
    '                        type = "COURIER"
    '                    ElseIf s.CorresType = "FA" Then
    '                        type = "FAX"
    '                    ElseIf s.CorresType = "VI" Then
    '                        type = "VISIT"
    '                    End If
    '                    Names.Add(s.Id.ToString + "|" + s.CorresType + "|" + s.Subject)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '   <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitCodePropertyCommunity(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select *,(select Description from TblLockup where ID=prok.Community) as NewCommunity from TblProperties prop inner join TblPropertiesDetails prok on prop.autoCode=prok.Propcode")
    '            Else
    '                dt = DBManager.Getdatatable("select *,(select Description from TblLockup where ID=prok.Community) as NewCommunity from TblProperties prop inner join TblPropertiesDetails prok on prop.autoCode=prok.Propcode where prop.Code like '%" + prefixText + "%' or (select Description from TblLockup where ID=prok.Community) Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Code") + "|" + dt(x)("NewCommunity"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitCodePropertySubCommunity(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select *,(select Description from TblLockup where ID=prok.SubCommunity) as NewCommunity from TblProperties prop inner join TblPropertiesDetails prok on prop.autoCode=prok.Propcode")
    '            Else
    '                dt = DBManager.Getdatatable("select *,(select Description from TblLockup where ID=prok.SubCommunity) as NewCommunity from TblProperties prop inner join TblPropertiesDetails prok on prop.autoCode=prok.Propcode where prop.Code like '%" + prefixText + "%' or (select Description from TblLockup where ID=prok.SubCommunity) Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Code") + "|" + dt(x)("NewCommunity"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '   <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitNumberCommunity(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select *,(select Description from TblLockup where ID=prok.Community) as NewCommunity from TblProperties prop inner join TblPropertiesDetails prok on prop.autoCode=prok.Propcode")
    '            Else
    '                dt = DBManager.Getdatatable("select *,(select Description from TblLockup where ID=prok.Community) as NewCommunity from TblProperties prop inner join TblPropertiesDetails prok on prop.autoCode=prok.Propcode where prok.UnitNumber like '%" + prefixText + "%' or (select Description from TblLockup where ID=prok.Community) Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("UnitNumber") + "|" + dt(x)("NewCommunity"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '   <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitNameProperty(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblProperties)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = New TblPropertiesFactory().GetAll()
    '            Else
    '                dt = New TblPropertiesFactory().GetAllByCustom("Name like'%" & prefixText & "%'")
    '            End If
    '            If dt.Count <> 0 Then
    '                For Each s As TblProperties In dt
    '                    Names.Add(s.Name)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod(True)> _
    '     <System.Web.Script.Services.ScriptMethod()>
    '    Public Function UploadFile(ByVal args As Object) As String
    '        Try
    '            Dim dt As New DataTable
    '            'Dim r As DataRow
    '            Dim FolderName As String = "Peoperty_Uploads"
    '            Dim filepath As Object = args("_fileId")
    '            Dim FileName As String = args("_fileName")
    '            Dim PhotoName As String = args("_fileName").ToString.Split(".")(0)
    '            If PhotoName.Length > 30 Then
    '                PhotoName = args("_fileName").ToString.Split(".")(0).Substring(0, 30)
    '            End If
    '            Dim FileExt As String = args("_fileName").ToString.Split(".").Last
    '            Dim FileSize As String = Val(args("_fileSize")) / 1000000
    '            Dim LocaldirectoryPath As String = Server.MapPath(String.Format("~/{0}/", FolderName))
    '            Dim XMLDirectoryPath As String = Server.MapPath(String.Format("~/{0}/", "XmlUploads"))
    '            Dim objDR As DataRow
    '            If Not Directory.Exists(LocaldirectoryPath) Then
    '                Directory.CreateDirectory(LocaldirectoryPath)
    '            End If
    '            Dim fp As String = Path.GetTempPath() + "_AjaxFileUpload" + "\" + filepath + "\" + FileName

    '            objDR = dt.NewRow ' Create new datarow for the new phot
    '            Dim rnd As New Random
    '            While File.Exists(LocaldirectoryPath + FileName)
    '                FileName = rnd.Next(10000000, 99999999).ToString & "Copy_" & FileName
    '            End While
    '            If File.Exists(fp) Then
    '                If Not File.Exists(LocaldirectoryPath + FileName) Then
    '                    File.Copy(fp, LocaldirectoryPath + FileName)
    '                End If
    '                If Not File.Exists(XMLDirectoryPath + FileName) Then
    '                    File.Copy(fp, XMLDirectoryPath + FileName)
    '                End If
    '            Else
    '                Return ""
    '            End If
    '            ''####################################################  Upload Files to Remote Server ########################################
    '            ''Dim uploadClient As RemoteUpload = Nothing
    '            ''Dim stream As FileStream = File.OpenRead(fp)
    '            ''Dim fileBytes As Byte() = New Byte(stream.Length - 1) {}
    '            ''stream.Read(fileBytes, 0, fileBytes.Length)
    '            ''stream.Close()
    '            'uploadClient = New FtpRemoteUpload(fileBytes, FileName, "ftp://Always.blueberry.software/XmlUploads/")
    '            ''If Not File.Exists("http://Always.blueberry.software/XmlUploads/" & FileName) Then
    '            'uploadClient.UploadFile()
    '            ''End If
    '            '' ##################################################  End of Upload Files to Remote Server #########################################
    '            If File.Exists(fp) Then
    '                File.Delete(fp)
    '            End If
    '            Return FolderName & "/" & FileName
    '            '     End If
    '        Catch ex As Exception
    '            Return ex.Message.ToString
    '        End Try
    '    End Function
    '    <WebMethod(True)> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '    Public Function UploadCompanyDcument(ByVal args As Object) As String
    '        Try
    '            Dim dt As New DataTable
    '            'Dim r As DataRow
    '            Dim FolderName As String = "Documents"
    '            Dim filepath As Object = args("_fileId")
    '            Dim FileName As String = args("_fileName")
    '            Dim PhotoName As String = args("_fileName").ToString.Split(".")(0)
    '            If PhotoName.Length > 30 Then
    '                PhotoName = args("_fileName").ToString.Split(".")(0).Substring(0, 30)
    '            End If
    '            Dim FileExt As String = args("_fileName").ToString.Split(".").Last
    '            Dim FileSize As String = Val(args("_fileSize")) / 1000000
    '            Dim LocaldirectoryPath As String = Server.MapPath(String.Format("~/{0}/", FolderName))
    '            Dim objDR As DataRow
    '            If Not Directory.Exists(LocaldirectoryPath) Then
    '                Directory.CreateDirectory(LocaldirectoryPath)
    '            End If
    '            Dim fp As String = Path.GetTempPath() + "_AjaxFileUpload" + "\" + filepath + "\" + FileName

    '            'Check if there is a datatable with images in session("dt")
    '            If Not Session("dt") Is Nothing Then
    '                dt = Session("dt")
    '            Else
    '                ' If not Images were Uploaded Before; Create New Datatable
    '                dt.Columns.Add("Id", GetType(String))
    '                dt.Columns.Add("Image", GetType(String))
    '                dt.Columns.Add("ImageName", GetType(String))
    '                dt.Columns.Add("Category", GetType(String))
    '            End If
    '            objDR = dt.NewRow ' Create new datarow for the new photo
    '            Dim rnd As New Random
    '            While File.Exists(LocaldirectoryPath + FileName)
    '                FileName = rnd.Next(10000000, 99999999).ToString & "Copy_" & FileName
    '            End While
    '            If File.Exists(fp) Then
    '                If Not File.Exists(LocaldirectoryPath + FileName) Then
    '                    File.Copy(fp, LocaldirectoryPath + FileName)
    '                End If
    '            Else
    '                Return ""
    '            End If
    '            If File.Exists(fp) Then
    '                File.Delete(fp)
    '            End If
    '            objDR("Id") = ""
    '            objDR("Image") = FolderName & "/" & FileName
    '            objDR("ImageName") = PhotoName
    '            objDR("Category") = ""
    '            dt.Rows.Add(objDR)
    '            Session("dt") = dt
    '            Return FolderName & "/" & FileName
    '        Catch ex As Exception
    '            Return ex.Message.ToString
    '        End Try
    '    End Function
    '    <WebMethod(True)> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function UploadCustomerFile(ByVal args As Object) As String
    '        Try
    '            Dim dt As New DataTable
    '            'Dim r As DataRow
    '            Dim FolderName As String = "User_Uploads"
    '            Dim filepath As Object = args("_fileId")
    '            Dim FileName As String = args("_fileName")
    '            Dim PhotoName As String = args("_fileName").ToString.Split(".")(0)
    '            If PhotoName.Length > 30 Then
    '                PhotoName = args("_fileName").ToString.Split(".")(0).Substring(0, 30)
    '            End If
    '            'Dim FileExt As String = args("_contentType")

    '            Dim FileExt As String = args("_fileName").ToString.Split(".")(1)
    '            Dim FileSize As String = Val(args("_fileSize")) / 1000000
    '            Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", FolderName))
    '            Dim objDR As DataRow

    '            'Defining the DataTable


    '            If Not Directory.Exists(directoryPath) Then
    '                Directory.CreateDirectory(directoryPath)
    '            End If

    '            Dim fp As String = Path.GetTempPath() + "_AjaxFileUpload" + "\" + filepath + "\" + FileName
    '            If Not Session("dt") Is Nothing Then
    '                dt = Session("dt")
    '            Else
    '                dt.Columns.Add("Id", GetType(String))
    '                dt.Columns.Add("ClientCode", GetType(String))
    '                dt.Columns.Add("Image", GetType(String))
    '                dt.Columns.Add("ImageName", GetType(String))
    '                dt.Columns.Add("FileType", GetType(String))
    '                dt.Columns.Add("FileSize", GetType(String))
    '                dt.Columns.Add("Category", GetType(String))
    '            End If
    '            objDR = dt.NewRow
    '            If Not File.Exists(fp) Then
    '                File.Copy(fp, directoryPath + FileName)
    '                File.Delete(fp)

    '                objDR("Id") = ""
    '                objDR("ClientCode") = ""
    '                objDR("Image") = FolderName & "/" & FileName
    '                objDR("ImageName") = PhotoName
    '                objDR("FileType") = FileExt
    '                objDR("FileSize") = FileSize
    '                objDR("Category") = ""
    '                dt.Rows.Add(objDR)
    '                Session("dt") = dt
    '                Return FolderName & "/" & FileName
    '            Else
    '                Dim rnd As New Random
    '                FileName = rnd.Next(10000000, 99999999).ToString & "Copy_" & FileName
    '                File.Copy(fp, directoryPath + FileName)
    '                File.Delete(fp)

    '                objDR("Id") = ""
    '                objDR("ClientCode") = ""
    '                objDR("Image") = FolderName & "/" & FileName
    '                objDR("ImageName") = PhotoName
    '                objDR("FileType") = FileExt
    '                objDR("FileSize") = FileSize
    '                objDR("Category") = ""
    '                dt.Rows.Add(objDR)
    '                Session("dt") = dt
    '                Return FolderName & "/" & FileName
    '            End If



    '        Catch ex As Exception
    '            Return ""
    '        End Try
    '    End Function
    '    <WebMethod(True)> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '    Public Function UploadContractFile(ByVal args As Object) As String
    '        Try
    '            Dim dt As New DataTable
    '            'Dim r As DataRow
    '            Dim FolderName As String = "Contract_Uploads"
    '            Dim filepath As Object = args("_fileId")
    '            Dim FileName As String = args("_fileName")
    '            Dim PhotoName As String = args("_fileName").ToString.Split(".")(0)
    '            If PhotoName.Length > 30 Then
    '                PhotoName = args("_fileName").ToString.Split(".")(0).Substring(0, 30)
    '            End If
    '            'Dim FileExt As String = args("_contentType")
    '            Dim FileExt As String = args("_fileName").ToString.Split(".")(1)
    '            Dim FileSize As String = Val(args("_fileSize")) / 1000000
    '            Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", FolderName))
    '            Dim objDR As DataRow

    '            'Defining the DataTable


    '            If Not Directory.Exists(directoryPath) Then
    '                Directory.CreateDirectory(directoryPath)
    '            End If

    '            Dim fp As String = Path.GetTempPath() + "_AjaxFileUpload" + "\" + filepath + "\" + FileName
    '            If Not Session("dt") Is Nothing Then
    '                dt = Session("dt")
    '            Else
    '                dt.Columns.Add("Id", GetType(String))
    '                dt.Columns.Add("Code", GetType(String))
    '                dt.Columns.Add("Image", GetType(String))
    '                dt.Columns.Add("ImageName", GetType(String))
    '                dt.Columns.Add("FileType", GetType(String))
    '                dt.Columns.Add("FileSize", GetType(String))
    '                dt.Columns.Add("Category", GetType(String))
    '            End If
    '            objDR = dt.NewRow
    '            If Not File.Exists(fp) Then
    '                File.Copy(fp, directoryPath + FileName)
    '                File.Delete(fp)

    '                objDR("Id") = ""
    '                objDR("Code") = ""
    '                objDR("Image") = FolderName & "/" & FileName
    '                objDR("ImageName") = PhotoName
    '                objDR("FileType") = FileExt
    '                objDR("FileSize") = FileSize
    '                objDR("Category") = ""
    '                dt.Rows.Add(objDR)
    '                Session("dt") = dt
    '                Return FolderName & "/" & FileName
    '            Else
    '                Dim rnd As New Random
    '                FileName = rnd.Next(10000000, 99999999).ToString & "Copy_" & FileName
    '                File.Copy(fp, directoryPath + FileName)
    '                File.Delete(fp)

    '                objDR("Id") = ""
    '                objDR("Code") = ""
    '                objDR("Image") = FolderName & "/" & FileName
    '                objDR("ImageName") = PhotoName
    '                objDR("FileType") = FileExt
    '                objDR("FileSize") = FileSize
    '                objDR("Category") = ""
    '                dt.Rows.Add(objDR)
    '                Session("dt") = dt
    '                Return FolderName & "/" & FileName
    '            End If



    '        Catch ex As Exception
    '            Return ""
    '        End Try
    '    End Function
    '    <WebMethod(True)> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '    Public Function DeletePhoto(ByVal photo As String) As String

    '        Try

    '            Dim r As DataRow
    '            Dim FolderName As String = "Peoperty_Uploads"
    '            Dim filepath As String = Server.MapPath(String.Format("~/{0}", photo))
    '            Dim dt As DataTable
    '            If Not Session("dt") Is Nothing Then
    '                dt = Session("dt")
    '                Dim i As Integer = 0
    '                While i < dt.Rows.Count
    '                    If (dt.Rows(i).Item("Image") = photo) Then
    '                        dt.Rows(i).Delete()
    '                        Session("dt") = dt
    '                        Exit While
    '                    End If
    '                    i += 1
    '                End While
    '            Else
    '                Return ""
    '            End If

    '            'Delete File from Directory 
    '            If File.Exists(filepath) Then

    '                File.Delete(filepath)
    '                Return photo
    '            Else
    '                Return ""
    '            End If



    '        Catch ex As Exception
    '            Return ""
    '        End Try
    '    End Function
    '    <WebMethod(True)> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '    Public Function DeleteUserPhoto(ByVal photo As String) As String

    '        Try

    '            Dim r As DataRow
    '            Dim FolderName As String = "User_Uploads"
    '            Dim filepath As String = Server.MapPath(String.Format("~/{0}", photo))
    '            Dim dt As DataTable
    '            If Not Session("dt") Is Nothing Then
    '                dt = Session("dt")
    '                Dim i As Integer = 0
    '                While i < dt.Rows.Count
    '                    If (dt.Rows(i).Item("Image") = photo) Then
    '                        dt.Rows(i).Delete()
    '                        Session("dt") = dt
    '                        Exit While
    '                    End If
    '                    i += 1
    '                End While
    '            Else
    '                Return ""
    '            End If

    '            'Delete File from Directory 
    '            If File.Exists(filepath) Then

    '                File.Delete(filepath)
    '                Return photo
    '            Else
    '                Return ""
    '            End If



    '        Catch ex As Exception
    '            Return ""
    '        End Try
    '    End Function
    '    <WebMethod(True)> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function DeleteContractPhoto(ByVal photo As String) As String

    '        Try

    '            Dim r As DataRow
    '            Dim FolderName As String = "Contract_Uploads"
    '            Dim filepath As String = Server.MapPath(String.Format("~/{0}", photo))
    '            Dim dt As DataTable
    '            If Not Session("dt") Is Nothing Then
    '                dt = Session("dt")
    '                Dim i As Integer = 0
    '                While i < dt.Rows.Count
    '                    If (dt.Rows(i).Item("Image") = photo) Then
    '                        dt.Rows(i).Delete()
    '                        Session("dt") = dt
    '                        Exit While
    '                    End If
    '                    i += 1
    '                End While
    '            Else
    '                Return ""
    '            End If

    '            'Delete File from Directory 
    '            If File.Exists(filepath) Then

    '                File.Delete(filepath)
    '                Return photo
    '            Else
    '                Return ""
    '            End If



    '        Catch ex As Exception
    '            Return ""
    '        End Try
    '    End Function
    '    <WebMethod()> _
    '      <System.Web.Script.Services.ScriptMethod()>
    '    Public Function CheckAllFiles(ByVal args As Object) As String
    '        Try


    '        Catch ex As Exception
    '            Return False
    '        End Try
    '    End Function
    '    '<WebMethod()> _
    '    ' <System.Web.Script.Services.ScriptMethod()>
    '    'Public Function GetLeadsProperties(ByVal prefixText As String) As String()
    '    '    Dim dt As New List(Of TblLeads)
    '    '    Dim Names As New List(Of String)
    '    '    Dim x As Integer = 0
    '    '    Try
    '    '        If prefixText = "*" Then
    '    '            dt = New TblLeadsFactory().GetAll()
    '    '        Else
    '    '            dt = New TblLeadsFactory().GetAllByCustom("Name like'%" & prefixText & "%' or  AutoCode like '%" & prefixText & "%' ")
    '    '        End If
    '    '        If dt.Count <> 0 Then
    '    '            For Each s As TblLeads In dt
    '    '                Names.Add(s.AutoCode & " | " & s.Name)
    '    '            Next
    '    '            Return Names.ToArray
    '    '        End If
    '    '    Catch ex As Exception
    '    '    End Try
    '    '    Names.Add(" No Results were Found!")
    '    '    Return Names.ToArray
    '    'End Function

    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUserName(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblUsers)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = New TblUsersFactory().GetAll()
    '            Else
    '                dt = New TblUsersFactory().GetAllByCustom("FirstName like'%" & prefixText & "%' or LastName like'%" & prefixText & "%' or id like '%" & prefixText & "%' ")
    '            End If
    '            If dt.Count <> 0 Then
    '                For Each s As TblUsers In dt
    '                    Names.Add(s.Id & " | " & s.FirstName + " " + s.LastName)
    '                Next
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitPurpose(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='UP' and Description ='Rent' or Description='Sale'")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='UP' and Description Like '%" + prefixText + "%' and Description ='Rent' or Description = 'Sale'")
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '       <System.Web.Script.Services.ScriptMethod()>
    '    Public Function LookupValueExists(ByVal prefixText() As String) As Boolean
    '        Try

    '            Dim daLookup As New TblLockupFactory
    '            Dim dtLookup As New TblLockup
    '            Dim ID As Integer = 0
    '            Dim Description As String = prefixText(0)
    '            Dim LookupType As String = prefixText(1)
    '            '   MsgBox(Description & " " & LookupType)
    '            Dim temp As New DataTable
    '            temp = DBManager.Getdatatable("Select ID from TblLockup where Type='" & LookupType & "' and Description='" & Description & "'")
    '            If temp.Rows.Count <> 0 Then
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        Catch ex As Exception
    '            Return True
    '        End Try
    '        Return " No Results were Found!"
    '    End Function
    '    <WebMethod()> _
    '     <System.Web.Script.Services.ScriptMethod()>
    '    Public Function InsertNewLookupValue(ByVal prefixText() As String) As Boolean
    '        Try

    '            Dim daLookup As New TblLockupFactory
    '            Dim dtLookup As New TblLockup
    '            Dim ID As Integer = 0
    '            Dim Description As String = prefixText(0)
    '            Dim LookupType As String = prefixText(1)
    '            '   MsgBox(Description & " " & LookupType)
    '            Dim temp As New DataTable
    '            temp = DBManager.Getdatatable("Select Max(CONVERT(int,ID)) from TblLockup")
    '            Description = Description.Trim()

    '            ID = temp.Rows(0).Item(0) + 1
    '            '  MsgBox(ID)
    '            ' Check if the value exists
    '            temp = DBManager.Getdatatable("Select Description from TblLockup where Type='" & LookupType & "' and Description='" & Description & "'")
    '            If temp.Rows.Count <> 0 Then
    '                Return False
    '            End If
    '            dtLookup.ID = ID.ToString
    '            dtLookup.Description = Description
    '            dtLookup.Type = LookupType

    '            If DBManager.ExcuteQuery("insert into TblLockup (Id,Type,Description) values ('" & ID & "', '" & LookupType & "', '" & Description & "')") Then
    '                Return True
    '            Else
    '                Return False
    '            End If

    '            Return True
    '        Catch ex As Exception
    '            Return False
    '        End Try
    '        Return " No Results were Found!"
    '    End Function
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitType(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='UT'")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='UT' and Description Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitCity(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='C' and (deleted ='False' or deleted is null)")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='C' and Description Like '%" + prefixText + "%' and (deleted ='False' or deleted is null)")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitLocation(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='LC'")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='LC' and Description Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitCommunity(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Dim value = contextKey.Split("|")(0)
    '        Dim relatedtype = contextKey.Split("|")(1)
    '        Dim valueid As String
    '        If PublicFunctions.GetLockupId(value, relatedtype) <> "" Then
    '            valueid = PublicFunctions.GetLockupId(value, relatedtype)
    '            Try
    '                If prefixText = "*" Then
    '                    dt = DBManager.Getdatatable("select * from TblLockup where Type ='UC' and RelatedId='" + valueid + "'and RelatedType='" + relatedtype + "'")
    '                Else
    '                    dt = DBManager.Getdatatable("select * from TblLockup where Type ='UC' and Description Like '%" + prefixText + "%' and RelatedId='" + valueid + "'and RelatedType='" + relatedtype + "'")
    '                End If

    '                If dt.Rows.Count <> 0 Then
    '                    While x < dt.Rows.Count
    '                        Names.Add(dt(x)("Description"))
    '                        x += 1
    '                    End While
    '                    Return Names.ToArray
    '                Else
    '                    Names.Add("No results found!")
    '                    Return Names.ToArray
    '                End If
    '            Catch ex As Exception
    '                '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '            End Try
    '            Names.Add(" No Results were Found!")
    '            Return Names.ToArray
    '        Else
    '            Names.Add(" Please Insert City First!")
    '            Return Names.ToArray
    '        End If
    '    End Function
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitSubCommunity(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Dim value = contextKey.Split("|")(0)
    '        Dim relatedtype = contextKey.Split("|")(1)
    '        Dim valueid As String
    '        If PublicFunctions.GetLockupId(value, relatedtype) <> "" Then

    '            valueid = PublicFunctions.GetLockupId(value, relatedtype)
    '            Try
    '                If prefixText = "*" Then
    '                    dt = DBManager.Getdatatable("select * from TblLockup where Type ='US' and RelatedId='" + valueid + "'and RelatedType='" + relatedtype + "'")
    '                Else
    '                    dt = DBManager.Getdatatable("select * from TblLockup where Type ='US' and Description Like '%" + prefixText + "%' and RelatedId='" + valueid + "'and RelatedType='" + relatedtype + "'")
    '                End If

    '                If dt.Rows.Count <> 0 Then
    '                    While x < dt.Rows.Count
    '                        Names.Add(dt(x)("Description"))
    '                        x += 1
    '                    End While
    '                    Return Names.ToArray
    '                Else
    '                    Names.Add("No results found!")
    '                    Return Names.ToArray
    '                End If
    '            Catch ex As Exception
    '                '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '            End Try
    '            Names.Add(" No Results were Found!")
    '            Return Names.ToArray
    '        Else
    '            Names.Add(" Please Insert Community First!")
    '            Return Names.ToArray
    '        End If
    '    End Function
    '    <WebMethod()> _
    '   <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetLeadCode(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblLeads)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = New TblLeadsFactory().GetAllByCustom("(dbo.TblLeads.deleted ='False' or dbo.TblLeads.deleted is null)")
    '            Else
    '                dt = New TblLeadsFactory().GetAllByCustom("Code like '%" & prefixText & "%' and  (dbo.TblLeads.deleted ='False' or dbo.TblLeads.deleted is null)")
    '            End If
    '            If dt.Count <> 0 Then
    '                For Each s As TblLeads In dt
    '                    Names.Add(s.Code)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    ' <WebMethod()> _
    '    '<System.Web.Script.Services.ScriptMethod()>
    '    ' Public Function GetLeads(ByVal prefixText As String) As String()
    '    '     Dim dt As New List(Of tblLeads)
    '    '     Dim Names As New List(Of String)
    '    '     Dim x As Integer = 0
    '    '     Dim usertype = clsGeneralVariables.usertype
    '    '     Try
    '    '         Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '    '         If prefixText = "*" Then
    '    '             If usertype <> "Admin" Then
    '    '                 dt = New tblLeadsFactory().GetAllByCustom("Salesman ='" + salesmancode + "'")
    '    '             Else
    '    '                 dt = New tblLeadsFactory().GetAll()
    '    '             End If
    '    '         Else
    '    '             If usertype <> "Admin" Then
    '    '                 dt = New tblLeadsFactory().GetAllByCustom("(Code like'%" & prefixText & "%' or Description like '%" & prefixText & "%') and Salesman ='" + salesmancode + "'")
    '    '             Else
    '    '                 dt = New tblLeadsFactory().GetAllByCustom("Code like'%" & prefixText & "%' or Description like '%" & prefixText & "%'")
    '    '             End If
    '    '         End If
    '    '         If dt.Count <> 0 Then
    '    '             For Each s As tblLeads In dt
    '    '                 Names.Add(s.Code + "|" + s.Description)
    '    '             Next
    '    '             Return Names.ToArray
    '    '         Else
    '    '             Names.Add("No results found!")
    '    '             Return Names.ToArray
    '    '         End If
    '    '     Catch ex As Exception
    '    '     End Try
    '    '     Return Nothing
    '    ' End Function
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetLeadSource(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='SS'")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='SS' and Description Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetLeadType(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='LP'")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='LP' and Description Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function



    '    '   <WebMethod()> _
    '    '<System.Web.Script.Services.ScriptMethod()>
    '    '   Public Function GetEnquiry(ByVal prefixText As String) As String()
    '    '       Dim dt As New List(Of tblLeads)
    '    '       Dim Names As New List(Of String)
    '    '       Dim x As Integer = 0
    '    '       Try
    '    '           If prefixText = "*" Then
    '    '               dt = New tblLeadsFactory().GetAllByCustom("Deleted = 'false' or Deleted is null")
    '    '           Else
    '    '               dt = New tblLeadsFactory().GetAllByCustom("Code like'%" & prefixText & "%' or Description like '%" & prefixText & "%' and (Deleted= 'false' or Deleted is null)")
    '    '           End If
    '    '           If dt.Count <> 0 Then
    '    '               For Each s As tblLeads In dt
    '    '                   Names.Add(s.Code + "|" + s.Description)
    '    '               Next
    '    '               Return Names.ToArray
    '    '           End If
    '    '       Catch ex As Exception
    '    '       End Try
    '    '       Return Nothing
    '    '   End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetRentiSpaid(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='RS'")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='RS' and Description Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()>
    '    Public Function getCompaniesSearch(ByVal prefixText As String) As System.String()
    '        Dim dt As New DataTable
    '        Dim CompanyList As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblCompany")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblCompany where Id like '%" + prefixText + "%' or CompanyName like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    CompanyList.Add(dt(x)("Id").ToString + "|" + dt(x)("CompanyName").ToString())
    '                    x += 1
    '                End While
    '                Return CompanyList.ToArray
    '            Else
    '                CompanyList.Add("No results found!")
    '                Return CompanyList.ToArray
    '            End If
    '        Catch ex As Exception
    '            CompanyList.Clear()
    '            CompanyList.Add("No results found!")
    '            Return CompanyList.ToArray
    '        End Try
    '    End Function
    '    <WebMethod()> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUserType()
    '        Dim usertype As String = "salesman"
    '        Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '        Dim dtsales = DBManager.Getdatatable("select UserType from TblUsers where Id in (select AssignUserID from TblUsers where Code='" + salesmancode + "')")
    '        If dtsales.Rows.Count <> 0 Then
    '            usertype = dtsales.Rows(0).Item("UserType").ToString
    '        End If
    '        Return usertype
    '    End Function
    '    <WebMethod()> _
    '     <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetLeadsPropertiesSales(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblLeads)
    '        Dim dt1 As New DataTable
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Dim usertype As String = ""
    '        Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '        Try
    '            usertype = clsGeneralVariables.usertype
    '            If prefixText = "*" Then
    '                If usertype <> "Admin" Then
    '                    dt1 = DBManager.Getdatatable("select *,(select Description from TblLockup where  Id =TblLeads.SubCommunity  ) as SubCommunityName from TblLeads inner join TblLockup on TblLeads.Purpose=TblLockup.id inner join tblLeads on TblLeads.LeadCode=tblLeads.Code where   Purpose='" + PublicFunctions.GetLockupId("Sale", "UP") + "' and tblLeads.salesman='" + salesmancode + "'")
    '                Else
    '                    dt1 = DBManager.Getdatatable("select *,(select Description from TblLockup where  Id =TblLeads.SubCommunity  ) as SubCommunityName from TblLeads inner join TblLockup on TblLeads.Purpose=TblLockup.id inner join tblLeads on TblLeads.LeadCode=tblLeads.Code  where Purpose='" + PublicFunctions.GetLockupId("Sale", "UP") + "'")
    '                End If
    '            Else
    '                If usertype <> "Admin" Then
    '                    dt1 = DBManager.Getdatatable("select *,(select Description from TblLockup where  Id =TblLeads.SubCommunity  ) as SubCommunityName from TblLeads inner join TblLockup on TblLeads.Purpose=TblLockup.id inner join tblLeads on TblLeads.LeadCode=tblLeads.Code   where Purpose='" + PublicFunctions.GetLockupId("Sale", "UP") + "'and tblLeads.salesman='" + salesmancode + "' and  (Name like'%" & prefixText & "%' or  TblLeads.Code like '%" & prefixText & "%')")
    '                Else
    '                    dt1 = DBManager.Getdatatable("select *,(select Description from TblLockup where  Id =TblLeads.SubCommunity  ) as SubCommunityName from TblLeads inner join TblLockup on TblLeads.Purpose=TblLockup.id   where    Purpose='" + PublicFunctions.GetLockupId("Sale", "UP") + "'  Name and (like'%" & prefixText & "%' or  Code like '%" & prefixText & "%')")
    '                End If
    '            End If
    '            If dt1.Rows.Count <> 0 Then
    '                For Each s As DataRow In dt1.Rows
    '                    Names.Add(s("Code").ToString & " | " & s("SubCommunityName").ToString)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add(" No Results  Found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Names.Add(" No Results  Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetLeadsPropertiesRent(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblLeads)
    '        Dim dt1 As New DataTable
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Dim usertype As String
    '        Try
    '            Dim salesmancode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '            usertype = clsGeneralVariables.usertype
    '            If prefixText = "*" Then
    '                If usertype <> "Admin" Then
    '                    dt1 = DBManager.Getdatatable("select *,(select Description from TblLockup where  Id =TblLeads.SubCommunity  ) as SubCommunityName from TblLeads inner join TblLockup on TblLeads.Purpose=TblLockup.id inner join tblLeads on TblLeads.LeadCode=tblLeads.Code where Purpose='" + PublicFunctions.GetLockupId("Rent", "UP") + "' and tblLeads.salesman='" + salesmancode + "'")
    '                Else
    '                    dt1 = DBManager.Getdatatable("select *,(select Description from TblLockup where  Id =TblLeads.SubCommunity  ) as SubCommunityName from TblLeads inner join TblLockup on TblLeads.Purpose=TblLockup.id inner join tblLeads on TblLeads.LeadCode=tblLeads.Code where Purpose='" + PublicFunctions.GetLockupId("Rent", "UP") + "'")
    '                End If
    '            Else
    '                If usertype <> "Admin" Then
    '                    dt1 = DBManager.Getdatatable("select *,(select Description from TblLockup where  Id =TblLeads.SubCommunity  ) as SubCommunityName from TblLeads inner join TblLockup on TblLeads.Purpose=TblLockup.id inner join tblLeads on TblLeads.LeadCode=tblLeads.Code   where   (Name like'%" & prefixText & "%' or  TblLeads.Code like '%" & prefixText & "%') and  Purpose='" + PublicFunctions.GetLockupId("Rent", "UP") + "' and tblLeads.salesman='" + salesmancode + "'")
    '                Else
    '                    dt1 = DBManager.Getdatatable("select *,(select Description from TblLockup where  Id =TblLeads.SubCommunity  ) as SubCommunityName from TblLeads inner join TblLockup on TblLeads.Purpose=TblLockup.id   where   Name like'%" & prefixText & "%' or  Code like '%" & prefixText & "%' and Purpose='" + PublicFunctions.GetLockupId("Rent", "UP") + "'")
    '                End If
    '            End If
    '            If dt1.Rows.Count <> 0 Then
    '                For Each s As DataRow In dt1.Rows
    '                    Names.Add(s("Code").ToString & " | " & s("SubCommunityName").ToString)
    '                Next
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitNumberSubCommunity(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select *,(select Description from TblLockup where ID=prok.SubCommunity) as NewCommunity from TblProperties prop inner join TblPropertiesDetails prok on prop.autoCode=prok.Propcode")
    '            Else
    '                dt = DBManager.Getdatatable("select *,(select Description from TblLockup where ID=prok.SubCommunity) as NewCommunity from TblProperties prop inner join TblPropertiesDetails prok on prop.autoCode=prok.Propcode where prok.UnitNumber like '%" + prefixText + "%' or (select Description from TblLockup where ID=prok.SubCommunity) Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("UnitNumber") + "|" + dt(x)("NewCommunity"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function VillaBuiltType(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='V'")
    '            Else
    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='V' and Description Like '%" + prefixText + "%'")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Description"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '     <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetBuildingName(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Dim value = contextKey.Split("|")(0)
    '        Dim relatedtype = contextKey.Split("|")(1)
    '        Dim valueid As String
    '        If PublicFunctions.GetLockupId(value, relatedtype) <> "" Then

    '            valueid = PublicFunctions.GetLockupId(value, relatedtype)
    '            Try
    '                If prefixText = "*" Then
    '                    dt = DBManager.Getdatatable("select * from TblLockup where Type ='BN' and RelatedId='" + valueid + "'and RelatedType='" + relatedtype + "'")
    '                Else
    '                    dt = DBManager.Getdatatable("select * from TblLockup where Type ='BN' and Description Like '%" + prefixText + "%' and RelatedId='" + valueid + "'and RelatedType='" + relatedtype + "'")
    '                End If

    '                If dt.Rows.Count <> 0 Then
    '                    While x < dt.Rows.Count
    '                        Names.Add(dt(x)("Description"))
    '                        x += 1
    '                    End While
    '                    Return Names.ToArray
    '                Else
    '                    Names.Add("No results found!")
    '                    Return Names.ToArray
    '                End If
    '            Catch ex As Exception
    '                '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '            End Try
    '            Names.Add(" No Results were Found!")
    '            Return Names.ToArray
    '        Else
    '            Names.Add(" Please Insert Sub Community First!")
    '            Return Names.ToArray
    '        End If
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitCodeSalesPropertySubCommunity(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select *,(select Description from TblLockup where ID=prok.SubCommunity) as NewCommunity from TblProperties prop inner join TblPropertiesDetails prok on prop.autoCode=prok.Propcode where  (prop.deleted='false' or prop.deleted is null)")
    '            Else
    '                dt = DBManager.Getdatatable("select *,(select Description from TblLockup where ID=prok.SubCommunity) as NewCommunity from TblProperties prop inner join TblPropertiesDetails prok on prop.autoCode=prok.Propcode where  ( prop.Code like '%" + prefixText + "%' or (select Description from TblLockup where ID=prok.SubCommunity) Like '%" + prefixText + "%') and (prop.deleted='false' or prop.deleted is null)")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Code"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetLeadPropertyCodeSubCommunity(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("SELECT prop.Code,(select Description from TblLockup where ID=prop.SubCommunity) as SubCommunity FROM TblLeads prop where prop.deleted is null or prop.deleted ='false'")
    '            Else
    '                dt = DBManager.Getdatatable("SELECT prop.Code,(select Description from TblLockup where ID=prop.SubCommunity) as SubCommunity FROM TblLeads prop where prop.code like '%" + prefixText + "%' or (select Description from TblLockup where ID=prop.SubCommunity) Like '%" + prefixText + "%' and (prop.deleted is null or prop.deleted ='false')")
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Code") + "|" + dt(x)("SubCommunity"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetLeadPropertyTitle(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("SELECT Name FROM TblLeads  where TblLeads.deleted is null or TblLeads.deleted ='false' and Name is not null and name !='' order by Name")
    '            Else
    '                dt = DBManager.Getdatatable("SELECT Name FROM TblLeads prop where prop.Name like '%" + prefixText + "%' and (prop.deleted is null or prop.deleted ='false') and Name is not null and name !='' order by Name")
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Name").ToString)
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '            Names.Add("Error Loading Data !")
    '        End Try
    '        Return Names.ToArray()
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitCodeRentPropertySubCommunity(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select *,(select Description from TblLockup where ID=prok.SubCommunity) as NewCommunity from TblProperties prop inner join TblPropertiesDetails prok on prop.autoCode=prok.Propcode where RentOrSale = 'false'")
    '            Else
    '                dt = DBManager.Getdatatable("select *,(select Description from TblLockup where ID=prok.SubCommunity) as NewCommunity from TblProperties prop inner join TblPropertiesDetails prok on prop.autoCode=prok.Propcode where RentOrSale = 'false' and ( prop.Code like '%" + prefixText + "%' or (select Description from TblLockup where ID=prok.SubCommunity) Like '%" + prefixText + "%')")
    '            End If

    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("Code") + "|" + dt(x)("NewCommunity"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitNameSalesProperty(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblProperties)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = New TblPropertiesFactory().GetAllByCustom("deleted ='false' or deleted is null order by Name ")
    '            Else
    '                dt = New TblPropertiesFactory().GetAllByCustom("Name like'%" & prefixText & "%'  and (deleted ='false' or deleted is null) order by Name")
    '            End If
    '            If dt.Count <> 0 Then
    '                For Each s As TblProperties In dt
    '                    Names.Add(s.Name)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitNameRntProperty(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblProperties)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = New TblPropertiesFactory().GetAllByCustom("RentOrSale = 'false'")
    '            Else
    '                dt = New TblPropertiesFactory().GetAllByCustom("Name like'%" & prefixText & "%' and RentOrSale = 'false'")
    '            End If
    '            If dt.Count <> 0 Then
    '                For Each s As TblProperties In dt
    '                    Names.Add(s.Name)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '   <System.Web.Script.Services.ScriptMethod()>
    '    Public Function CheckLookup(ByVal prefixText() As String) As String
    '        Try
    '            Dim daLookup As New TblLockupFactory
    '            Dim dtLookup As New TblLockup
    '            Dim ID As Integer = 0
    '            Dim Description As String = prefixText(0)
    '            Dim LookupType As String = prefixText(1)
    '            Dim senderID As String = prefixText(2)
    '            Dim value = prefixText(3)
    '            Dim relatedtype = prefixText(4)
    '            Dim valueid As String
    '            If PublicFunctions.GetLockupId(value, relatedtype) <> "" Then
    '                valueid = PublicFunctions.GetLockupId(value, relatedtype)
    '                Dim temp As New DataTable
    '                temp = DBManager.Getdatatable("Select ID from TblLockup where type ='" & LookupType & "' and Description ='" & Description & "' and RelatedId='" + valueid + "'and RelatedType='" + relatedtype + "'")
    '                If temp.Rows.Count <> 0 Then
    '                    Return "True" + "-" + senderID
    '                Else
    '                    Return "False" + "-" + senderID
    '                End If
    '            Else
    '                Return "False" + "-" + senderID
    '            End If
    '        Catch ex As Exception
    '            Return False
    '        End Try
    '    End Function
    '    '    <WebMethod()> _
    '    '<System.Web.Script.Services.ScriptMethod()>
    '    '    Public Function GetMailchimpFName(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
    '    '        Dim dt As New DataTable
    '    '        ' MsgBox(sender)
    '    '        Dim Names As New List(Of String)(10)
    '    '        Dim x As Integer = 0
    '    '        Dim value = contextKey
    '    '        Dim valueid As String
    '    '            Try
    '    '            If prefixText = "*" Then
    '    '                If value = "Agents" Then
    '    '                    TblUsers()
    '    '                    dt = DBManager.Getdatatable("select * from TblUsers")
    '    '                ElseIf value = "Contacts" Then
    '    '                    dt = DBManager.Getdatatable("select * from TblUsers")
    '    '                ElseIf value = "" Then
    '    '                Else
    '    '                End If
    '    '            Else
    '    '                dt = DBManager.Getdatatable("select * from TblLockup where Type ='UC' and Description Like '%" + prefixText + "%' and RelatedId='" + valueid + "'and RelatedType='" + relatedtype + "'")
    '    '            End If

    '    '                If dt.Rows.Count <> 0 Then
    '    '                    While x < dt.Rows.Count
    '    '                        Names.Add(dt(x)("Description"))
    '    '                        x += 1
    '    '                    End While
    '    '                    Return Names.ToArray
    '    '                Else
    '    '                    Names.Add("No results found!")
    '    '                    Return Names.ToArray
    '    '                End If
    '    '            Catch ex As Exception
    '    '                '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '    '            End Try
    '    '            Names.Add(" No Results were Found!")
    '    '            Return Names.ToArray
    '    '        Else
    '    '            Names.Add(" Please Insert City First!")
    '    '            Return Names.ToArray
    '    '        End If
    '    '    End Function
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetMailchimp(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
    '        Dim Names As New List(Of String)(10)
    '        Dim value = prefixText
    '        Dim type = contextKey.Split("|")(0)
    '        Dim field = contextKey.Split("|")(1)
    '        If type = "Agents" Then
    '            GetAgents(Names, field, value)
    '        ElseIf type = "Clients" Then
    '            GetClients(Names, field, value)
    '        ElseIf type = "Owners" Then
    '            GetOwners(Names, field, value)
    '        Else
    '            GetContacts(Names, field, value)
    '        End If
    '        Return Names.ToArray
    '    End Function
    '    Private Sub GetAgents(ByRef Names As List(Of String), ByVal field As String, ByVal value As String)
    '        Dim dt As New DataTable
    '        Try
    '            If value = "*" Then
    '                If field = "fname" Then
    '                    dt = DBManager.Getdatatable("select FirstName  from TblUsers ")
    '                ElseIf field = "lname" Then
    '                    dt = DBManager.Getdatatable("select LastName  from TblUsers")
    '                Else
    '                    dt = DBManager.Getdatatable("select email  from TblUsers")
    '                End If
    '            Else
    '                If field = "fname" Then
    '                    dt = DBManager.Getdatatable("select FirstName  from TblUsers where FirstName like '%" + value + "%'")
    '                ElseIf field = "lname" Then
    '                    dt = DBManager.Getdatatable("select LastName  from TblUsers where LastName like'%" + value + "%'")
    '                Else
    '                    dt = DBManager.Getdatatable("select email  from TblUsers where email like'%" + value + "%'")
    '                End If
    '            End If
    '            Dim x = 0
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x).Item(0))
    '                    x += 1
    '                End While
    '            Else
    '                Names.Add("No results found!")
    '            End If
    '        Catch ex As Exception
    '            Names.Add("No results found!")
    '        End Try
    '    End Sub
    '    Private Sub GetContacts(ByRef Names As List(Of String), ByVal field As String, ByVal value As String)
    '        Dim dt As New DataTable
    '        Try
    '            If value = "*" Then
    '                If field = "fname" Then
    '                    dt = DBManager.Getdatatable("select EFullName  from TblContacts ")
    '                ElseIf field = "lname" Then
    '                    dt = DBManager.Getdatatable("select EFullName  from TblContacts")
    '                Else
    '                    dt = DBManager.Getdatatable("select email  from TblContacts")
    '                End If
    '            Else
    '                If field = "fname" Then
    '                    dt = DBManager.Getdatatable("select EFullName  from TblContacts where EFullName like'%" + value + "%'")
    '                ElseIf field = "lname" Then
    '                    dt = DBManager.Getdatatable("select EFullName  from TblContacts where EFullName like'%" + value + "%'")
    '                Else
    '                    dt = DBManager.Getdatatable("select email  from TblContacts where email like '%" + value + "%'")
    '                End If
    '            End If
    '            Dim x = 0
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x).Item(0))
    '                    x += 1
    '                End While
    '            Else
    '                Names.Add("No results found!")
    '            End If
    '        Catch ex As Exception
    '            Names.Add("No results found!")
    '        End Try
    '    End Sub
    '    Private Sub GetClients(ByRef Names As List(Of String), ByVal field As String, ByVal value As String)
    '        Dim dt As New DataTable
    '        Try
    '            If value = "*" Then
    '                If field = "fname" Then
    '                    dt = DBManager.Getdatatable("select EFullName from TblContacts where Client=1 and (deleted = 'false' or deleted is null)")
    '                ElseIf field = "lname" Then
    '                    dt = DBManager.Getdatatable("select EFullName from TblContacts where Client=1 and (deleted = 'false' or deleted is null)")
    '                Else
    '                    dt = DBManager.Getdatatable("select email from TblContacts where Client=1 and (deleted = 'false' or deleted is null)")
    '                End If
    '            Else
    '                If field = "fname" Then
    '                    dt = DBManager.Getdatatable("select EFullName from TblContacts where (deleted = 'false' or deleted is null) and  EFullName like'%" + value + "%' and  Client=1 ")
    '                ElseIf field = "lname" Then
    '                    dt = DBManager.Getdatatable("select EFullName from TblContacts where (deleted = 'false' or deleted is null) and EFullName like'%" + value + "%' and  Client=1")
    '                Else
    '                    dt = DBManager.Getdatatable("select email from TblContacts where (deleted = 'false' or deleted is null) and email like '%" + value + "%' and  Client=1")
    '                End If
    '            End If
    '            Dim x = 0
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x).Item(0))
    '                    x += 1
    '                End While
    '            Else
    '                Names.Add("No results found!")
    '            End If
    '        Catch ex As Exception
    '            Names.Add("No results found!")
    '        End Try
    '    End Sub
    '    Private Sub GetOwners(ByRef Names As List(Of String), ByVal field As String, ByVal value As String)
    '        Dim dt As New DataTable
    '        Try
    '            If value = "*" Then
    '                If field = "fname" Then
    '                    dt = DBManager.Getdatatable("select EFullName  from TblContacts  where Owner=1")
    '                ElseIf field = "lname" Then
    '                    dt = DBManager.Getdatatable("select EFullName  from TblContacts where Owner=1")
    '                Else
    '                    dt = DBManager.Getdatatable("select email  from TblContacts where Owner=1")
    '                End If
    '            Else
    '                If field = "fname" Then
    '                    dt = DBManager.Getdatatable("select EFullName  from TblContacts where EFullName like'%" + value + "%' and  Owner=1")
    '                ElseIf field = "lname" Then
    '                    dt = DBManager.Getdatatable("select EFullName  from TblContacts where EFullName like'%" + value + "%' and  Owner=1")
    '                Else
    '                    dt = DBManager.Getdatatable("select email  from TblContacts where email like '%" + value + "%' and  Owner=1")
    '                End If
    '            End If
    '            Dim x = 0
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x).Item(0))
    '                    x += 1
    '                End While
    '            Else
    '                Names.Add("No results found!")
    '            End If
    '        Catch ex As Exception
    '            Names.Add("No results found!")
    '        End Try
    '    End Sub
    '    <WebMethod()> _
    '   <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetOwnerName(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblContacts)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = New TblContactsFactory().GetAllByCustom(" (ContactType=10928 or ContactType=10929) and (deleted = 'false' or deleted is null) order by FirstName")
    '            Else
    '                dt = New TblContactsFactory().GetAllByCustom(" (ContactType=10928 or ContactType=10929) and (deleted = 'false' or deleted is null) and (FirstName like'%" & prefixText & "%' or LastName like'%" & prefixText & "%') order by FirstName")
    '            End If
    '            If dt.Count <> 0 Then
    '                For Each s As TblContacts In dt
    '                    Names.Add(s.FirstName + " " + s.LastName)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function
    '    <WebMethod()> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetClientName(ByVal prefixText As String) As String()
    '        Dim dt As New List(Of TblContacts)
    '        Dim Names As New List(Of String)
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = New TblContactsFactory().GetAllByCustom("(ContactType=10926 or ContactType=10927) and (deleted = 'false' or deleted is null) order by FirstName")
    '            Else
    '                dt = New TblContactsFactory().GetAllByCustom("(ContactType=10926 or ContactType=10927) and (deleted = 'false' or deleted is null) and (FirstName like'%" & prefixText & "%' or LastName like'%" & prefixText & "%') order by FirstName")
    '            End If
    '            If dt.Count <> 0 Then
    '                For Each s As TblContacts In dt
    '                    Names.Add(s.FirstName + " " + s.LastName)
    '                Next
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '        End Try
    '        Return Nothing
    '    End Function

    '    'Private Function GetExistSubscriber() As String
    '    '    Try
    '    '        Dim Results = ""
    '    '        Dim listid = ddlList.SelectedValue.ToString
    '    '        Dim listIn As listMembersInput = New listMembersInput(MCAPISettings.default_apikey, listid, EnumValues.listMembers_status.subscribed, 0, 100)
    '    '        Dim listMem As listMembers = New listMembers(listIn)
    '    '        Dim listOut = listMem.Execute
    '    '        Dim j = 0
    '    '        While j < listOut.result.Count()
    '    '            Dim row = listOut.result(j)
    '    '            Dim email = row.email
    '    '            Results = Results + "'" + email + "',"
    '    '            j = j + 1
    '    '        End While
    '    '        Return Results
    '    '    Catch ex As Exception
    '    '        lblRes.Text = "Fail " + ex.Message.ToString
    '    '        lblRes.Visible = True
    '    '        lblRes.CssClass = "res-label-error"
    '    '    End Try
    '    'End Function
    '    <WebMethod()> _
    '   <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetUnitCommunityMap(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim city = prefixText
    '        Dim cityid = PublicFunctions.GetLockupId(city, "C")
    '        Dim x As Integer = 0
    '        Dim valueid As String
    '        If PublicFunctions.GetLockupId(city, "C") <> "" Then
    '            valueid = PublicFunctions.GetLockupId(city, "C")
    '            Try
    '                If city <> "" Then
    '                    dt = DBManager.Getdatatable("select * from TblLockup where Type ='UC' and  RelatedId='" + valueid + "'and RelatedType='C'")
    '                End If
    '                If dt.Rows.Count <> 0 Then
    '                    While x < dt.Rows.Count
    '                        Names.Add(dt(x)("Description"))
    '                        x += 1
    '                    End While
    '                    Return Names.ToArray
    '                Else
    '                    Names.Add("No results found!")
    '                    Return Names.ToArray
    '                End If
    '            Catch ex As Exception
    '                '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '            End Try
    '            Names.Add(" No Results were Found!")
    '            Return Names.ToArray
    '        Else
    '            Names.Add(" Please Insert City First!")
    '            Return Names.ToArray
    '        End If
    '    End Function
    '    '    <WebMethod()> _
    '    '<System.Web.Script.Services.ScriptMethod()>
    '    '    Public Function GetContactsMobile(ByVal prefixText As String) As String()
    '    '        Dim SalesManCode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '    '        Dim UserId = Context.Request.Cookies("UserInfo")("UserId")
    '    '        Dim dt As New List(Of TblContacts)
    '    '        Dim Names As New List(Of String)
    '    '        Dim x As Integer = 0
    '    '        Dim usertype = clsGeneralVariables.usertype
    '    '        Try
    '    '            If prefixText = "*" Then
    '    '                If usertype <> "Admin" Then
    '    '                    dt = New TblContactsFactory().GetAllByCustom("salesman='" + SalesManCode + "'")
    '    '                Else
    '    '                    dt = New TblContactsFactory().GetAll()
    '    '                End If
    '    '            Else
    '    '                If usertype <> "Admin" Then
    '    '                    dt = New TblContactsFactory().GetAllByCustom("salesman='" + SalesManCode + "' and (Code like'%" & prefixText & "%' or EFullName like '%" & prefixText & "%')")
    '    '                Else
    '    '                    dt = New TblContactsFactory().GetAllByCustom("Code like'%" & prefixText & "%' or EFullName like '%" & prefixText & "%'")
    '    '                End If
    '    '            End If
    '    '            If dt.Count <> 0 Then
    '    '                For Each s As TblContacts In dt
    '    '                    Names.Add(s.Mobile)
    '    '                Next
    '    '                Return Names.ToArray
    '    '            Else
    '    '                Names.Add("No results found!")
    '    '                Return Names.ToArray
    '    '            End If
    '    '        Catch ex As Exception
    '    '        End Try
    '    '        Return Nothing
    '    '    End Function
    '    '    <WebMethod()> _
    '    '<System.Web.Script.Services.ScriptMethod()>
    '    '    Public Function GetContactsEmail(ByVal prefixText As String) As String()
    '    '        Dim SalesManCode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '    '        Dim UserId = Context.Request.Cookies("UserInfo")("UserId")
    '    '        Dim dt As New List(Of TblContacts)
    '    '        Dim Names As New List(Of String)
    '    '        Dim x As Integer = 0
    '    '        Dim usertype = clsGeneralVariables.usertype
    '    '        Try
    '    '            If prefixText = "*" Then
    '    '                If usertype <> "Admin" Then
    '    '                    dt = New TblContactsFactory().GetAllByCustom("salesman='" + SalesManCode + "'")
    '    '                Else
    '    '                    dt = New TblContactsFactory().GetAll()
    '    '                End If
    '    '            Else
    '    '                If usertype <> "Admin" Then
    '    '                    dt = New TblContactsFactory().GetAllByCustom("salesman='" + SalesManCode + "' and (Code like'%" & prefixText & "%' or EFullName like '%" & prefixText & "%')")
    '    '                Else
    '    '                    dt = New TblContactsFactory().GetAllByCustom("Code like'%" & prefixText & "%' or EFullName like '%" & prefixText & "%'")
    '    '                End If
    '    '            End If
    '    '            If dt.Count <> 0 Then
    '    '                For Each s As TblContacts In dt
    '    '                    Names.Add(s.Email)
    '    '                Next
    '    '                Return Names.ToArray
    '    '            Else
    '    '                Names.Add("No results found!")
    '    '                Return Names.ToArray
    '    '            End If
    '    '        Catch ex As Exception
    '    '        End Try
    '    '        Return Nothing
    '    '    End Function
    '    '    <WebMethod()> _
    '    '  <System.Web.Script.Services.ScriptMethod()>
    '    '    Public Function GetContactsNames(ByVal prefixText As String) As String()
    '    '        Dim SalesManCode = Context.Request.Cookies("UserInfo")("SalesmanCode")
    '    '        Dim UserId = Context.Request.Cookies("UserInfo")("UserId")
    '    '        Dim dt As New List(Of TblContacts)
    '    '        Dim Names As New List(Of String)
    '    '        Dim x As Integer = 0
    '    '        Dim usertype = clsGeneralVariables.usertype
    '    '        Try
    '    '            If prefixText = "*" Then
    '    '                If usertype <> "Admin" Then
    '    '                    dt = New TblContactsFactory().GetAllByCustom("salesman='" + SalesManCode + "'")
    '    '                Else
    '    '                    dt = New TblContactsFactory().GetAll()
    '    '                End If
    '    '            Else
    '    '                If usertype <> "Admin" Then
    '    '                    dt = New TblContactsFactory().GetAllByCustom("salesman='" + SalesManCode + "' and (Code like'%" & prefixText & "%' or EFullName like '%" & prefixText & "%')")
    '    '                Else
    '    '                    dt = New TblContactsFactory().GetAllByCustom("Code like'%" & prefixText & "%' or EFullName like '%" & prefixText & "%'")
    '    '                End If
    '    '            End If
    '    '            If dt.Count <> 0 Then
    '    '                For Each s As TblContacts In dt
    '    '                    Names.Add(s.EFullName)
    '    '                Next
    '    '                Return Names.ToArray
    '    '            Else
    '    '                Names.Add("No results found!")
    '    '                Return Names.ToArray
    '    '            End If
    '    '        Catch ex As Exception
    '    '        End Try
    '    '        Return Nothing
    '    '    End Function
    '    <WebMethod()> _
    '       <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetForms(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        ' MsgBox(sender)
    '        Dim da As New TblFormsFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim UserId As String = HttpContext.Current.Request.Cookies("UserInfo")("UserId")
    '        Dim x As Integer = 0
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("SELECT  TF.Id as FormID, TF.FormName as FormName, TF.FormTitle as FormTitle, TF.ArFormTitle as ArFormTitle, TF.FormUrl as FormUrl, TF.GroupId as FormGroupId , TF.Icon as FormIcon, TF.OPeration as FormOperation" &
    '                        " , TM.Id as MenuID, TM.MenuName, TM.ArMenuName, TM.OrderId as MenuOrderID, TM.Icon as MenuIcon,TM.ParentMenuId as MenuParentId, TM.Report as MenuReport" &
    '                        " ,TMP.Id as ParentMenuID, TMP.MenuName as ParentMenuName, TMP.ArMenuName as ParentArMenuName, TMP.OrderId as ParentMenuOrderID, TMP.Icon as ParentMenuIcon, TMP.Report as ParentMenuReport" &
    '                        " FROM  tblforms AS TF" &
    '                        " left outer join tblMenus AS TM  on(TF.MenueId  = TM.ID )" &
    '                        " left outer join tblMenus AS TMP on(TM.ParentMenuId =TMP .ID )" &
    '                            " inner join TblPermissions TP on(TP.FormId =TF.Id )" & _
    '                      " where TP.UserId='" & UserId & "' and TP.PAccess =1 and TF.FormName not in ('frmLeadSalesProperty','frmLeadRentProperty','frmSellingPaymentSchedule')" & _
    '                                        " order by TF.FormTitle,TF.operation desc")
    '            Else
    '                dt = DBManager.Getdatatable("SELECT  TF.Id as FormID, TF.FormName as FormName, TF.FormTitle as FormTitle, TF.ArFormTitle as ArFormTitle, TF.FormUrl as FormUrl, TF.GroupId as FormGroupId , TF.Icon as FormIcon, TF.OPeration as FormOperation" &
    '                         " , TM.Id as MenuID, TM.MenuName, TM.ArMenuName, TM.OrderId as MenuOrderID, TM.Icon as MenuIcon,TM.ParentMenuId as MenuParentId, TM.Report as MenuReport" &
    '                         " ,TMP.Id as ParentMenuID, TMP.MenuName as ParentMenuName, TMP.ArMenuName as ParentArMenuName, TMP.OrderId as ParentMenuOrderID, TMP.Icon as ParentMenuIcon, TMP.Report as ParentMenuReport" &
    '                         " FROM  tblforms AS TF" &
    '                         " left outer join tblMenus AS TM  on(TF.MenueId  = TM.ID )" &
    '                         " left outer join tblMenus AS TMP on(TM.ParentMenuId =TMP .ID )" &
    '                             " inner join TblPermissions TP on(TP.FormId =TF.Id )" & _
    '                       " where TF.FormTitle like '%" + prefixText + "%' and TP.UserId='" & UserId & "' and TP.PAccess =1 and TF.FormName not in ('frmLeadSalesProperty','frmLeadRentProperty','frmSellingPaymentSchedule')" & _
    '                                         " order by TF.FormTitle,TF.operation desc")
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("FormTitle"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '     <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetFormUrl(ByVal prefixText As String) As String
    '        Try
    '            If prefixText <> "" Then
    '                Dim dtForms As DataTable = DBManager.Getdatatable("select * from tblForms where FormTitle ='" + prefixText + "'")
    '                If dtForms.Rows.Count > 0 Then
    '                    Dim FormUrl As String = dtForms.Rows(0).Item("FormUrl").ToString
    '                    Dim operation As String = dtForms.Rows(0).Item("operation").ToString

    '                    If operation = "Add" Then
    '                        FormUrl = FormUrl & "?Operation=Add"
    '                    End If
    '                    If operation = "Addc" Then
    '                        FormUrl = FormUrl & "?Operation=Add&type=Client"
    '                    End If
    '                    If operation = "Addo" Then
    '                        FormUrl = FormUrl & "?Operation=Add&type=Owner"
    '                    End If
    '                    Return FormUrl
    '                Else
    '                    Return String.Empty
    '                End If
    '            Else
    '                Return "Dashboard.aspx"
    '            End If
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    End Function
    '#End Region

    '#Region "Map"
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetPropertiesBySearch(ByVal values As String) As String
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            Dim dt As DataTable = DBManager.Getdatatable("SELECT TblProperties.Id, TblProperties.Code, " &
    '                " TblPropertiesDetails.GeoPoint1, TblPropertiesDetails.GeoPoint2, TblLockup.Description as CategoryName " +
    '                " FROM TblProperties INNER JOIN TblLockup ON TblProperties.Category = TblLockup.Id " &
    '                " INNER JOIN TblPropertiesDetails ON TblProperties.Id = TblPropertiesDetails.PropId " +
    '                CollectPropertySearchCondition(values))
    '            Return PublicFunctions.ConvertDataTabletoString(dt)
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    End Function

    '    Public Function CollectPropertySearchCondition(ByVal values As String) As String
    '        Try
    '            Dim city = values.Split("|")(0)
    '            Dim community = values.Split("|")(1)
    '            Dim type = values.Split("|")(2)
    '            Dim category = values.Split("|")(3)
    '            Dim purpose = values.Split("|")(4)

    '            Dim query As String = " where TblPropertiesDetails.GeoPoint1 !=''  and TblPropertiesDetails.GeoPoint2 !=''"
    '            If city <> "0" Then
    '                query += " and TblProperties.City=" + city
    '            End If
    '            If community <> "0" Then
    '                query += " and TblProperties.Community=" + community
    '            End If
    '            If type <> "0" Then
    '                query += " and TblProperties.UnitType=" + type
    '            End If
    '            If category <> "0" Then
    '                query += " and TblProperties.Category=" + category
    '            End If
    '            If purpose <> "0" Then
    '                query += " and TblProperties.Purpose=" + purpose
    '            End If
    '            query += " and isnull(TblProperties.archived,0)=0 and isnull(TblProperties.Deleted,0)=0"
    '            Return query
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    End Function

    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetMapPropertyDetails(ByVal propId As String) As List(Of String)
    '        Try
    '            Dim dtProp As DataTable = DBManager.Getdatatable("SELECT TP.Code, TP.PropertyTitle, TP.Description, TP.Price, TPI.OnlineUrl, " &
    '                " dbo.GetEnValue(TP.City) as CityValue, dbo.GetEnValue(TP.Community) as CommunityValue, " &
    '                " dbo.GetEnValue(TP.UnitType) as TypeValue, dbo.GetEnValue(TP.Category) as CategoryValue, " &
    '                " dbo.GetEnValue(TP.Purpose) as PurposeValue, " &
    '                " (select count(*) from TblPropertiesImages where TP.Id = TblPropertiesImages.PropId and " &
    '                " dbo.GetEnValue(TblPropertiesImages.Category) = 'Customer Image')  as CustomerImagesCount " &
    '                " FROM TblProperties As TP " &
    '                " INNER JOIN TblPropertiesDetails AS TPD ON TP.Id = TPD.PropId " &
    '                " LEFT JOIN TblPropertiesImages AS TPI ON TP.Id = TPI.PropId " &
    '                " WHERE TP.Id = '" + propId + "' and (TP.Deleted = 'False' or TP.Deleted is null)")
    '            If dtProp.Rows.Count <> 0 Then
    '                Dim details As New List(Of String)
    '                details.Add(dtProp(0)("Code").ToString)
    '                details.Add(dtProp(0)("PropertyTitle").ToString)
    '                details.Add(dtProp(0)("Description").ToString)
    '                details.Add(dtProp(0)("Price").ToString)
    '                details.Add(dtProp(0)("CityValue").ToString)
    '                details.Add(dtProp(0)("CommunityValue").ToString)
    '                details.Add(dtProp(0)("TypeValue").ToString)
    '                details.Add(dtProp(0)("CategoryValue").ToString)
    '                details.Add(dtProp(0)("PurposeValue").ToString)
    '                details.Add(dtProp(0)("CustomerImagesCount").ToString)

    '                Dim dtImgs As DataTable = DBManager.Getdatatable("SELECT ImagePath FROM TblPropertiesImages WHERE TblPropertiesImages.PropId = '" + propId + "'")
    '                If dtImgs.Rows.Count <> 0 Then
    '                    Dim i As Integer = 0
    '                    While i < dtImgs.Rows.Count
    '                        details.Add(PublicFunctions.GetAppRootUrl() + "/" + dtImgs(i)("ImagePath").ToString)
    '                        i += 1
    '                    End While
    '                End If
    '                Return details
    '            Else
    '                Dim Empty As New List(Of String)
    '                Return Empty
    '            End If
    '        Catch ex As Exception
    '            Dim Empty As New List(Of String)
    '            Return Empty
    '        End Try
    '    End Function

    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetMapPropertyDdlValues(ByVal propCode As String) As String
    '        Try
    '            Dim dtProp As DataTable = DBManager.Getdatatable("SELECT TP.Code, TP.Purpose, TP.City, TP.Community, TP.UnitType, TP.Category, TPD.GeoPoint1, TPD.GeoPoint2 " &
    '                " FROM TblProperties AS TP LEFT JOIN TblPropertiesDetails AS TPD ON TP.Id = TPD.PropId WHERE TP.Code = '" + propCode + "' and (TP.Deleted = 'False' or TP.Deleted is null)")
    '            Return PublicFunctions.ConvertDataTabletoString(dtProp)
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    End Function
    '#End Region

    '#Region "Binding Ddl From Lookup"
    '    ''' <summary>
    '    ''' Get ID, Description from lookup based on lookupType
    '    ''' </summary>
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetLookupData(ByVal LookupType As String) As String
    '        Try
    '            Dim dt As DataTable = DBManager.Getdatatable("select ID, Description from TblLockup where Type ='" + LookupType + "'")
    '            Return ConvertDataTabletoString(dt)
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    End Function

    '    ''' <summary>
    '    ''' Get related ID, Description from lookup based on value, type, lookupType
    '    ''' </summary>
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetRelatedLookupData(ByVal dataJSON As Object) As String
    '        Dim data As Dictionary(Of String, Object) = dataJSON
    '        Dim value = data("Value").ToString
    '        Dim type = data("Type").ToString
    '        Dim relatedtype = data("RelatedType").ToString
    '        Try
    '            Dim dt As New DataTable
    '            If value <> "" Then
    '                dt = DBManager.Getdatatable("select ID, Description from TblLockup where Type ='" + type + "' and  RelatedId='" + value + "'and RelatedType='" + relatedtype + "'")
    '            End If
    '            Return ConvertDataTabletoString(dt)
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    End Function
    '#End Region

#Region "Notifications"
    ''' <summary>
    ''' get notifications
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetNotifications(ByVal UserId As String) As String
        Try
            Dim currdate = PublicFunctions.ConvertDatetoNumber(DateTime.Now.ToString("dd/MM/yyyy")).ToString()
            Dim dtNotifications = DBManager.Getdatatable("select ID, RefCode, RefType, NotTitle, Remarks, Date as NotDate, IsSeen,FormUrl from tblNotifications where AssignedTo='" + UserId + "' and isNull(IsSeen,0) !=1 and  (RefType not in (1,2,3,4)  or (RefType in (1,2,3,4) and Date=" + currdate.ToString + "))  order by date desc")
            If dtNotifications.Rows.Count > 0 Then
                Return ConvertDataTabletoString(dtNotifications)
            End If
            checkSMSMessages()
            Return String.Empty
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    Private Sub checkSMSMessages()
        Try
            Dim dtsms = LoginInfo.GetSMSConfig()
            Dim dateno = PublicFunctions.ConvertDatetoNumber(DateTime.Now.ToString("dd/MM/yyyy"))
            Dim dtphonelist = DBManager.Getdatatable("Select * from tblsms_archive where isnull(Send,0)=0 and date_m='" + dateno.ToString + "' and comp_id=" + LoginInfo.GetComp_id().ToString)
            For Each row As DataRow In dtphonelist.Rows
                Dim phone = row.Item("Send_To")
                Dim message = row.Item("Message")
                Dim id = row.Item("id")
                Dim smsres = PublicFunctions.DoSendSMS(phone, message, id, dtsms)
            Next
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' update notification IsSeen
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function UpdateNotificationIsSeen(ByVal NotID_SeenValue As String) As Integer
        Try
            Dim NotId = NotID_SeenValue.Split("|")(0)
            Dim SeenValue = NotID_SeenValue.Split("|")(1)
            Dim UserId = NotID_SeenValue.Split("|")(2)

            If SeenValue = "True" Or SeenValue = "true" Then
                SeenValue = "False"
            Else
                SeenValue = "True"
            End If
            If DBManager.ExcuteQuery("update tblNotifications set IsSeen = '" + SeenValue + "' where Id = '" + NotId + "'") = 1 Then
            Dim dtCount = DBManager.Getdatatable("select Count(ID) as NotCount from tblNotifications where AssignedTo='" + UserId + "' and isNull(Deleted,0) !=1 and CONVERT(DATE,tblNotifications.Date)=CONVERT(DATE,getdate()) and (IsSeen = 'False' or IsSeen is null)")
                Return CInt(dtCount.Rows(0)("NotCount").ToString)
            Else
                Return -1
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' get notifications count
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetNotificationsCount(ByVal UserId As String) As Integer
        Try
            Dim currdate = PublicFunctions.ConvertDatetoNumber(DateTime.Now.ToString("dd/MM/yyyy")).ToString()
            Dim dtCount = DBManager.Getdatatable("select Count(ID) as NotCount from tblNotifications where AssignedTo='" + UserId + "'  and isNULL(IsSeen,0) != 1 and  (RefType not in (1,2,3,4)  or (RefType in (1,2,3,4) and Date=" + currdate.ToString + "))")
            Return CInt(dtCount.Rows(0)("NotCount").ToString)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' get refresh interval for notifications from lookup
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetRefreshInterval() As Integer
        Try
            'Dim dt = DBManager.Getdatatable("select Description from TblLockup where Type='NI'")
            Return CInt(100000)
        Catch ex As Exception
            Return 0
        End Try
    End Function
#End Region

    '#Region "Ahmed Nayl"
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetData(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim value = prefixText.Split("|")(0)
    '        Dim nextcontrolid = prefixText.Split("|")(1)
    '        Dim type = prefixText.Split("|")(2)
    '        Dim relatedtype = prefixText.Split("|")(3)
    '        Dim x As Integer = 1
    '        If value <> "" Then
    '            Try
    '                If value <> "" Then
    '                    dt = DBManager.Getdatatable("select * from TblLockup where Type ='" + type + "' and  RelatedId='" + value + "'and RelatedType='" + relatedtype + "'")
    '                End If
    '                If dt.Rows.Count <> 0 Then
    '                    Names.Add(nextcontrolid)
    '                    While x < dt.Rows.Count + 1
    '                        Names.Add(dt(x - 1)("Description").ToString() & "|" & dt(x - 1)("id").ToString())
    '                        x += 1
    '                    End While
    '                    Return Names.ToArray
    '                Else
    '                    Names.Add("No results found!")
    '                    Return Names.ToArray
    '                End If
    '            Catch ex As Exception
    '                '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '            End Try
    '            Names.Add(" No Results were Found!")
    '            Return Names.ToArray
    '        Else
    '            Names.Add(" No Results were Found!")
    '            Return Names.ToArray
    '        End If
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetDataWithSelectid(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim value = prefixText.Split("|")(0)
    '        Dim nextcontrolid = prefixText.Split("|")(1)
    '        Dim type = prefixText.Split("|")(2)
    '        Dim relatedtype = prefixText.Split("|")(3)
    '        Dim selectedid = prefixText.Split("|")(4)
    '        Dim x As Integer = 1
    '        If value <> "" Then
    '            Try
    '                If value <> "" Then
    '                    dt = DBManager.Getdatatable("select * from TblLockup where Type ='" + type + "' and  RelatedId='" + value + "'and RelatedType='" + relatedtype + "'")
    '                End If
    '                If dt.Rows.Count <> 0 Then
    '                    Names.Add(nextcontrolid & "|" & selectedid)
    '                    While x < dt.Rows.Count + 1
    '                        Names.Add(dt(x - 1)("Description").ToString() & "|" & dt(x - 1)("id").ToString())
    '                        x += 1
    '                    End While
    '                    Return Names.ToArray
    '                Else
    '                    Names.Add("No results found!")
    '                    Return Names.ToArray
    '                End If
    '            Catch ex As Exception
    '                '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '            End Try
    '            Names.Add(" No Results were Found!")
    '            Return Names.ToArray
    '        Else
    '            Names.Add(" No Results were Found!")
    '            Return Names.ToArray
    '        End If
    '    End Function
    '    '    <WebMethod()> _
    '    '<System.Web.Script.Services.ScriptMethod()>
    '    '    Public Function GetSubCommunity(ByVal prefixText As String) As String()
    '    '        Dim dt As New DataTable
    '    '        Dim da As New TblLockupFactory
    '    '        Dim Names As New List(Of String)(10)
    '    '        Dim Communityid = prefixText
    '    '        Dim x As Integer = 0
    '    '        If Communityid <> "" Then
    '    '            Try
    '    '                If Communityid <> "" Then
    '    '                    dt = DBManager.Getdatatable("select * from TblLockup where Type ='US' and  RelatedId='" + Communityid + "'and RelatedType='UC'")
    '    '                End If
    '    '                If dt.Rows.Count <> 0 Then
    '    '                    While x < dt.Rows.Count
    '    '                        Names.Add(dt(x)("Description"))
    '    '                        x += 1
    '    '                    End While
    '    '                    Return Names.ToArray
    '    '                Else
    '    '                    Names.Add("No results found!")
    '    '                    Return Names.ToArray
    '    '                End If
    '    '            Catch ex As Exception
    '    '                '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '    '            End Try
    '    '            Names.Add(" No Results were Found!")
    '    '            Return Names.ToArray
    '    '        Else
    '    '            Names.Add(" Please Insert Community First!")
    '    '            Return Names.ToArray
    '    '        End If
    '    '    End Function
    '    '    <WebMethod()> _
    '    '<System.Web.Script.Services.ScriptMethod()>
    '    '    Public Function GetBuilding(ByVal prefixText As String) As String()
    '    '        Dim dt As New DataTable
    '    '        Dim da As New TblLockupFactory
    '    '        Dim Names As New List(Of String)(10)
    '    '        Dim Communityid = prefixText
    '    '        Dim x As Integer = 0
    '    '        If Communityid <> "" Then
    '    '            Try
    '    '                If Communityid <> "" Then
    '    '                    dt = DBManager.Getdatatable("select * from TblLockup where Type ='BN' and  RelatedId='" + Communityid + "'and RelatedType='US'")
    '    '                End If
    '    '                If dt.Rows.Count <> 0 Then
    '    '                    While x < dt.Rows.Count
    '    '                        Names.Add(dt(x)("Description"))
    '    '                        x += 1
    '    '                    End While
    '    '                    Return Names.ToArray
    '    '                Else
    '    '                    Names.Add("No results found!")
    '    '                    Return Names.ToArray
    '    '                End If
    '    '            Catch ex As Exception
    '    '                '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '    '            End Try
    '    '            Names.Add(" No Results were Found!")
    '    '            Return Names.ToArray
    '    '        Else
    '    '            Names.Add(" Please Insert Community First!")
    '    '            Return Names.ToArray
    '    '        End If
    '    '    End Function

    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function EditProperties(ByVal prefixText As String) As String()
    '        Dim propertiesautocode = prefixText
    '        Dim Names As New List(Of String)(10)
    '        Try
    '            Dim dt As New DataTable
    '            dt = DBManager.Getdatatable("select * from tblProperties P inner Join tblPropertiesDetails PD on P.Id=PD.PropId left outer Join tblContacts C on P.OwnerCode=C.Id left outer Join tblUsers U on U.id=P.AgentID where P.Id=" + propertiesautocode)
    '            If dt.Rows.Count <> 0 Then
    '                Dim str As String = ConvertDataTabletoString(dt)
    '                Names.Add("1")
    '                Names.Add(str)
    '                Return Names.ToArray
    '            Else
    '                Names.Add("0")
    '                Names.Add(" No Results were Found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            Names.Add("0")
    '            Names.Add(" No Results were Found!")
    '            Return Names.ToArray
    '        End Try
    '    End Function
    Public Function ConvertDataTabletoString(ByVal dt As DataTable) As String
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim rows As New List(Of Dictionary(Of String, Object))()
        Dim row As Dictionary(Of String, Object)
        For Each dr As DataRow In dt.Rows
            row = New Dictionary(Of String, Object)()
            For Each col As DataColumn In dt.Columns
                row.Add(col.ColumnName, dr(col))
            Next
            rows.Add(row)
        Next
        Return serializer.Serialize(rows)
    End Function
    '    ''' <summary>
    '    ''' Get drawings 'files' from tblPhotosAndDocs
    '    ''' </summary>
    '    <WebMethod(True)> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function getFiles(ByVal PropAutoCode As String) As String
    '        Try
    '            Dim dtImages As DataTable = DBManager.Getdatatable("SELECT * FROM TblPropertiesImage where AutoCode='" + PropAutoCode + "' ")
    '            Return ConvertDataTabletoString(dtImages)
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    End Function
    '    <WebMethod(True)> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '    Public Function getFacility(ByVal type As String) As String
    '        Try
    '            Dim dt As New DataTable
    '            If type = "Commercial" Then
    '                dt = DBManager.Getdatatable("select ID,Description  from TblLockup where Type= 'FO' order by Description")
    '            Else
    '                dt = DBManager.Getdatatable("select ID,Description  from TblLockup where Type= 'FR' order by Description")
    '            End If
    '            Return ConvertDataTabletoString(dt)
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    End Function
    '    <WebMethod(True)> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetPropertyFacilities(ByVal propertyAutoCode As String) As String
    '        Try
    '            Dim dt As New DataTable
    '            dt = DBManager.Getdatatable("select PropId,Facility from TblPropertiesFacility where  PropId='" + propertyAutoCode + "'")
    '            Return ConvertDataTabletoString(dt)
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    End Function

    '#End Region

    '#Region "Email"
    '    <WebMethod()> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetEmails(ByVal prefix As String, ByVal type As String) As String()
    '        Try
    '            Dim dtemail As New DataTable
    '            Dim user_id = Context.Request.Cookies("UserInfo")("UserId")
    '            dtemail = DBManager.Getdatatable("select * from TblEmailsSettings where UserId='" & user_id & "'")
    '            Dim Emails As New List(Of String)

    '            If dtemail.Rows.Count <> 0 Then
    '                Dim email As String = dtemail.Rows(0)("Email").ToString()
    '                ' Dim user_id = "55"
    '                Dim pop3Client As New Pop3Client
    '                pop3Client.Connect(dtemail.Rows(0).Item("POP3").ToString(), "110", False)
    '                pop3Client.Authenticate(dtemail.Rows(0).Item("Email").ToString(), PublicFunctions.Decrypt(dtemail.Rows(0).Item("Password").ToString()), AuthenticationMethod.UsernameAndPassword)
    '                Dim EmailsCount As Integer = pop3Client.GetMessageCount()
    '                Dim uids As List(Of String) = pop3Client.GetMessageUids()
    '                Dim dt As New DataTable
    '                Dim dtEmailConfigured As DataTable = DBManager.Getdatatable("Select * from TblEmails where UserId='" & user_id & "'")
    '                If (type = "Inbox") Then
    '                    dt = DBManager.Getdatatable("select * from TblEmails where UserId='" & user_id & "' and ( folder='" + type + "' or folder='Deleted' or Folder='DeletedPermanently') ORDER BY DateSent DESC")
    '                Else
    '                    dt = DBManager.Getdatatable("select * from TblEmails where UserId='" & user_id & "' and folder='" + type + "' ORDER BY DateSent DESC")
    '                End If

    '                Dim i As Integer = 0
    '                Dim k As Integer = 0
    '                Dim UID As String = ""
    '                Dim index As Integer
    '                Dim messageNumber As String = ""
    '                Dim strAttachments As String = ""
    '                If dtEmailConfigured.Rows.Count <> 0 Then
    '                    While i < dt.Rows.Count
    '                        Dim EmailID As String = dt.Rows(i)("ID").ToString()
    '                        Dim dtAttachments As New List(Of TblEmailAttachments)
    '                        Dim dfAttachments As New TblEmailAttachmentsFactory
    '                        dtAttachments = dfAttachments.GetAllBy(TblEmailAttachments.TblEmailAttachmentsFields.MessageID, EmailID)
    '                        If Not (type = "Deleted" Or type = "Sent") Then
    '                            UID = dt.Rows(i)("UID").ToString()
    '                            If uids.Contains(UID) Then
    '                                index = uids.IndexOf(UID)
    '                                uids(index) = "Removed"
    '                            End If
    '                            Dim folder As String = dt.Rows(i)("Folder").ToString()

    '                            If dtAttachments.Count > 0 Then
    '                                strAttachments = ""
    '                                Dim a As Integer = 0
    '                                While a < dtAttachments.Count
    '                                    strAttachments += dtAttachments(a).FilePath.ToString() + ";"
    '                                    a += 1
    '                                End While
    '                            End If
    '                            If Not (folder = "Deleted" Or folder = "DeletedPermanently") Then
    '                                Emails.Add(dt.Rows(i)("ID").ToString + "|" + dt.Rows(i)("MessageID").ToString + "|" + dt.Rows(i)("Subject").ToString + "|" + dt.Rows(i)("DateSent").ToString + "|" + dt.Rows(i)("EmailAddress").ToString + "|" + dt.Rows(i)("Body").ToString + "|" + email + "|" + dt.Rows(i)("UserID").ToString + "|" + messageNumber.ToString + "|" + strAttachments)
    '                            End If
    '                        Else
    '                            If dtAttachments.Count > 0 Then
    '                                strAttachments = ""
    '                                Dim a As Integer = 0
    '                                While a < dtAttachments.Count
    '                                    strAttachments += dtAttachments(a).FilePath.ToString() + ";"
    '                                    a += 1
    '                                End While
    '                            End If
    '                            Emails.Add(dt.Rows(i)("ID").ToString + "|" + dt.Rows(i)("MessageID").ToString + "|" + dt.Rows(i)("Subject").ToString + "|" + dt.Rows(i)("DateSent").ToString + "|" + dt.Rows(i)("EmailAddress").ToString + "|" + dt.Rows(i)("Body").ToString + "|" + email + "|" + dt.Rows(i)("UserID").ToString + "|" + messageNumber.ToString + "|" + strAttachments)
    '                        End If
    '                        strAttachments = ""
    '                        i += 1
    '                    End While
    '                    If Not (type = "Deleted" Or type = "Sent") And Not (uids.Count = 0) Then
    '                        Dim newuids As String() = uids.ToArray
    '                        Dim value As String = String.Join(",", newuids)
    '                        Emails.Add(value)
    '                    Else
    '                        Emails.Add("Removed")
    '                    End If
    '                Else
    '                    Dim j As Integer = 0
    '                    While j < uids.Count
    '                        UID = uids(j).ToString
    '                        messageNumber = j
    '                        Emails.Add(UID + "|" + messageNumber)
    '                        j += 1
    '                    End While

    '                End If
    '                pop3Client.Disconnect()
    '                Return Emails.ToArray
    '            Else
    '                Return Nothing
    '            End If
    '        Catch ex As Exception
    '            Return Nothing
    '        End Try
    '    End Function
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function ReadNewEmail(ByVal messageNo As String) As String
    '        Try
    '            Dim dtemail As New DataTable
    '            Dim Emails As String = " "
    '            Dim user_id = Context.Request.Cookies("UserInfo")("UserId")
    '            dtemail = DBManager.Getdatatable("select * from TblEmailsSettings where UserId='" & user_id & "'")
    '            If dtemail.Rows.Count <> 0 Then
    '                Dim email As String = dtemail.Rows(0)("Email").ToString()
    '                Dim pop3Client As New Pop3Client
    '                pop3Client.Connect(dtemail.Rows(0).Item("POP3").ToString(), "110", False)
    '                pop3Client.Authenticate(dtemail.Rows(0).Item("Email").ToString(), PublicFunctions.Decrypt(dtemail.Rows(0).Item("Password").ToString()), AuthenticationMethod.UsernameAndPassword)
    '                Dim msgNo As Integer = CInt(messageNo) + 1
    '                Dim message As Message = pop3Client.GetMessage(msgNo)
    '                Dim messageId As String = ""
    '                Dim ToEmails As New List(Of RfcMailAddress)
    '                Dim BccEmails As New List(Of RfcMailAddress)
    '                Dim CcEmails As New List(Of RfcMailAddress)
    '                Dim fromAddres As String = ""
    '                Dim Subject As String = ""
    '                Dim body As String = ""
    '                Dim files As String = ""
    '                Dim DateSent As New DateTime
    '                Dim exists As Boolean = True

    '                Dim uid As String = pop3Client.GetMessageUid(msgNo)
    '                Dim em As New TblEmails
    '                Dim emf As New TblEmailsFactory
    '                em.UID = uid
    '                em.UserId = user_id
    '                If message.Headers.MessageId <> vbNullString Then
    '                    messageId = message.Headers.MessageId.ToString
    '                    em.MessageID = messageId
    '                End If
    '                If message.Headers.Subject <> vbNullString Then
    '                    Subject = message.Headers.Subject.ToString
    '                    em.Subject = Subject
    '                End If
    '                If Not IsNothing(message.Headers.DateSent) Then
    '                    Dim d As String = message.Headers.DateSent.ToString
    '                    DateSent = Convert.ToDateTime(d)
    '                    em.DateSent = DateSent
    '                End If
    '                If message.Headers.From.Address <> vbNullString Then
    '                    fromAddres = message.Headers.From.Address.ToString
    '                    em.EmailAddress = fromAddres
    '                End If


    '                '  Fetching the Cc ,To and Bcc address

    '                CcEmails = message.Headers.Cc
    '                Dim strAllCcEmails As String = ""
    '                For Each tempCc In CcEmails
    '                    If tempCc.HasValidMailAddress Then
    '                        strAllCcEmails += tempCc.MailAddress.ToString() + ","
    '                    End If
    '                Next
    '                If (strAllCcEmails <> vbNullString) Then
    '                    em.CC = strAllCcEmails
    '                End If

    '                BccEmails = message.Headers.Bcc
    '                Dim strAllBccEmails As String = ""
    '                For Each tempBcc In BccEmails
    '                    If tempBcc.HasValidMailAddress Then
    '                        strAllBccEmails += tempBcc.MailAddress.ToString() + ","
    '                    End If
    '                Next
    '                If (strAllBccEmails <> vbNullString) Then
    '                    em.CC = strAllBccEmails
    '                End If

    '                ToEmails = message.Headers.To
    '                Dim strAllToEmails As String = ""
    '                For Each tempTo In ToEmails
    '                    If tempTo.HasValidMailAddress Then
    '                        strAllToEmails += tempTo.MailAddress.ToString() + ","
    '                    End If

    '                Next
    '                Dim bodyText As OpenPop.Mime.MessagePart = message.FindFirstPlainTextVersion()
    '                If Not (bodyText Is Nothing) Then
    '                    body = message.FindFirstPlainTextVersion().GetBodyAsText()
    '                    em.Body = body
    '                End If


    '                Dim attachments As List(Of MessagePart) = message.FindAllAttachments()
    '                Dim index As Integer = 0
    '                Dim dt As DataTable = DBManager.Getdatatable("select * from TblEmails where UID= '" & uid & "' and userId='" & user_id & "'")
    '                ' i think the issue here , try now ,ok i will try but it was fetching the max value anyways
    '                'em.ID = DBManager.SelectMax("ID", "TblEmails")

    '                If dt.Rows.Count = 0 Then
    '                    em.Folder = "Inbox"
    '                    emf.Insert(em)
    '                    exists = False
    '                    For Each attachment As MessagePart In attachments
    '                        If attachment.ContentType.MediaType = "image/jpeg" OrElse attachment.ContentType.MediaType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document" OrElse attachment.ContentType.MediaType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" OrElse attachment.ContentType.MediaType = "application/msword" OrElse attachment.ContentType.MediaType = "application/pdf" OrElse attachment.ContentType.MediaType = "application/octet-stream" Then
    '                            Dim at As New TblEmailAttachments
    '                            Dim af As New TblEmailAttachmentsFactory
    '                            at.FileName = attachment.FileName
    '                            at.FileType = attachment.ContentType.MediaType
    '                            at.MessageID = em.ID.ToString()
    '                            Dim file As New FileInfo(Server.MapPath("EmailAttachments") + "/" + attachment.FileName)

    '                            message.FindAllAttachments.Item(index).Save(file)
    '                            at.FilePath = "EmailAttachments" + "/" + attachment.FileName
    '                            at.FileSize = attachment.Body.Length
    '                            af.Insert(at)
    '                            files += at.FilePath.ToString + ";"
    '                        End If
    '                        index += 1
    '                    Next
    '                End If
    '                Emails = em.ID.ToString() + "|" + messageId + "|" + Subject + "|" + DateSent + "|" + fromAddres + "|" + body + "|" + email + "|" + uid + "|" + files
    '                pop3Client.Disconnect()
    '            End If
    '            Return Emails
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    End Function




    '    ''' <summary>
    '    ''' Get all email addresses in EmailAddress, CC, BCC from TblEmails
    '    ''' </summary>
    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetEmailsToSend(ByVal prefixText As String) As String()
    '        Dim dt As New DataTable
    '        Dim da As New TblLockupFactory
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Try
    '            Dim arr() As String = prefixText.Split(",")
    '            If arr(arr.Length - 1).Trim = "*" Then
    '                dt = DBManager.Getdatatable("SELECT DISTINCT EmailAddress FROM TblEmails")
    '            Else
    '                dt = DBManager.Getdatatable("SELECT DISTINCT EmailAddress FROM TblEmails WHERE EmailAddress LIKE '%" + arr(arr.Length - 1).Trim + "%' OR CC LIKE '%" + arr(arr.Length - 1).Trim + "%' OR BCC LIKE '%" + arr(arr.Length - 1).Trim + "%'")
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)("EmailAddress"))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception

    '        End Try
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetInterval(ByVal str As String) As Integer
    '        Dim dtinterval = DBManager.Getdatatable("select reloadtime from TblEmailsSettings ")
    '        If dtinterval.Rows.Count <> 0 Then
    '            Return dtinterval.Rows(0).Item("reloadtime") * 5
    '        Else
    '            Return 0
    '        End If
    '    End Function
    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetCounts(ByVal str As String) As String()
    '        Dim user_id = Context.Request.Cookies("UserInfo")("UserId")
    '        ' Dim user_id = "55"
    '        Dim counts As New List(Of String)
    '        Dim dtInbox = DBManager.Getdatatable("select id from TblEmails where folder='Inbox' and UserId='" & user_id & "' ")
    '        Dim dtSent = DBManager.Getdatatable("select id from TblEmails where folder='Sent' and UserId='" & user_id & "' ")
    '        Dim dtDeleted = DBManager.Getdatatable("select id from TblEmails where folder='Deleted' and UserId='" & user_id & "' ")
    '        counts.Add(dtInbox.Rows.Count)
    '        counts.Add(dtSent.Rows.Count)
    '        counts.Add(dtDeleted.Rows.Count)
    '        'counts = dtInbox.Rows.Count + "|" + dtSent.Rows.Count + "|" + dtDeleted.Rows.Count
    '        Return counts.ToArray()
    '    End Function

    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function DeleteEmails(ByVal str As String()) As Boolean
    '        Try
    '            Dim dt As New List(Of TblEmails)
    '            Dim df As New TblEmailsFactory
    '            Dim at As New List(Of TblEmailAttachments)
    '            Dim af As New TblEmailAttachmentsFactory
    '            'Check the status(Folder of the Selected Email and if its deleted delete the email permanently) of the 
    '            For Each Id As String In str
    '                dt = df.GetAllBy(TblEmails.TblEmailsFields.ID, Id)
    '                If (dt(0).Folder = "Deleted") Then
    '                    DBManager.ExcuteQuery("UPDATE TblEmails SET Folder='DeletedPermanently' where ID='" & Id & "' ")
    '                    'DBManager.ExcuteQuery("Delete from TblEmailAttachments where MessageID='" & Id & "'")
    '                Else
    '                    DBManager.ExcuteQuery("UPDATE TblEmails SET Folder='Deleted' where ID='" & Id & "' ")
    '                End If

    '            Next
    '            Return True
    '        Catch ex As Exception
    '            Return False
    '        End Try
    '    End Function

    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Function MoveEmailsToInbox(ByVal str As String()) As Boolean
    '        Try
    '            Dim dt As New List(Of TblEmails)
    '            Dim df As New TblEmailsFactory
    '            Dim at As New List(Of TblEmailAttachments)
    '            Dim af As New TblEmailAttachmentsFactory

    '            For Each Id As String In str
    '                dt = df.GetAllBy(TblEmails.TblEmailsFields.ID, Id)

    '                DBManager.ExcuteQuery("UPDATE TblEmails SET Folder='Inbox' where ID='" & Id & "' ")
    '            Next
    '            Return True
    '        Catch ex As Exception
    '            Return False
    '        End Try
    '    End Function

    '    <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '    Public Shared Sub RemoveFile(fileName As String, key As String)
    '        Dim files As List(Of HttpPostedFile) = DirectCast(HttpContext.Current.Cache(key), List(Of HttpPostedFile))
    '        files.RemoveAll(Function(f) f.FileName.ToLower().EndsWith(fileName.ToLower()))
    '    End Sub


    '#End Region

#Region "DynamicTable"
    ''' <summary>
    ''' get data based on passed quary
    ''' </summary>
    ''' 
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetListData(ByVal formName As String) As Array
        Try
            Dim archived As String = "0"
            If formName.Contains("|") Then
                archived = formName.Split("|")(1)
                formName = formName.Split("|")(0)
            End If
            Dim daForm As New TblFormsFactory
            Dim quaryStr = daForm.GetAllBy(TblForms.TblFormsFields.FormName, formName)(0).FormQueryAr
            If formName = "ConsultationDetails" Then
                quaryStr = quaryStr + " where ash_consultings.comp_id=" + LoginInfo.GetComp_id()
                If Not LoginInfo.ConsultationSuperAdmin() Then
                    If LoginInfo.getUserType() = "6" Then
                        quaryStr = quaryStr + " and advisor_id=" + LoginInfo.getrelatedId()
                    Else
                        quaryStr = quaryStr + " and created_By = " + LoginInfo.GetUser__Id()

                    End If

                End If
            ElseIf formName = "Notification" Then
                Dim currdate = PublicFunctions.ConvertDatetoNumber(DateTime.Now.ToString("dd/MM/yyyy")).ToString()
                quaryStr = quaryStr + " where AssignedTo=" + LoginInfo.GetUser__Id() + " and  (RefType not in (1,2,3,4)  or (RefType in (1,2,3,4) and Date=" + currdate.ToString + ")) order by date desc"
            ElseIf Not LoginInfo.isSuperAdmin() Then
                If formName = "Users" Then
                    quaryStr = "Select tblUsers.id As 'AutoCodeHide', full_name as 'الاسم بالكامل', isNull(name, '') as 'دور المستخدم', user_indenty as 'رقم الهوية', User_PhoneNumber as 'رقم الجوال'," +
               " Description as 'الصلاحية' from tblUsers Left Join tblUser_Type on tblUser_Type.id = User_Type Left Join tbllock_up on tbllock_up.id = group_id " +
               " where isNull(superAdmin, 0) = 0 And TblUsers.comp_id =" + LoginInfo.GetComp_id()
                ElseIf formName = "Advisors" Then
                    quaryStr = quaryStr + " where ash_advisors.comp_id=" + LoginInfo.GetComp_id()
                ElseIf formName = "Students" Then
                    quaryStr = quaryStr + " and tblUsers.comp_id=" + LoginInfo.GetComp_id()
                ElseIf formName = "Trainers" Then
                    quaryStr = quaryStr + " and tblUsers.comp_id=" + LoginInfo.GetComp_id()
                ElseIf formName = "orders" Then
                    quaryStr = quaryStr + " ,case owner_id when " + LoginInfo.GetUser__Id() + " then '11_'+convert(varchar,ash_orders.status) else '1_'+convert(varchar,ash_orders.status) end as 'موافقة / رفض' from ash_orders left join tblUsers owner on owner.id=owner_id" +
  " left join ash_case_receiving_delivery_details on ash_case_receiving_delivery_details.id=event_id " +
  " left join ash_cases on ash_case_receiving_delivery_details.case_id=ash_cases.id" +
  " left join tblUsers person on person.related_id=ash_cases.person1_id and person.User_Type=9 "
                    Dim userType = LoginInfo.getUserType()
                    If userType = "2" Or userType = "7" Then
                        quaryStr = quaryStr + " where ash_orders.comp_id=" + LoginInfo.GetComp_id()
                    ElseIf userType = "6" Then
                        quaryStr = quaryStr + "  where ash_orders.case_id in(select id from ash_cases where advisor_id = " + LoginInfo.getrelatedId() + ")"
                    ElseIf userType = "9" Then
                        quaryStr = quaryStr + "  where (ash_orders.owner_id=" + LoginInfo.GetUser__Id() + " or (ash_orders.status=2 and otherP_Accept is NULL)) and ash_orders.case_id in(select id from ash_cases where person1_id = " + LoginInfo.getrelatedId() + " or person2_id =" + LoginInfo.getrelatedId() + ")"
                    End If
                ElseIf formName = "Semester" Then
                    quaryStr = quaryStr + " where acd_semester.comp_id=" + LoginInfo.GetComp_id()
                ElseIf formName = "CommonQuest" Or formName = "email_setting" Or formName = "Signature_setting" Or formName = "sms_setting" Then
                    quaryStr = quaryStr + " where comp_id=" + LoginInfo.GetComp_id()
                End If
            End If

            Dim DataAR As New List(Of String)
            Dim dt As New DataTable
            dt = DBManager.Getdatatable(quaryStr)
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim k As Integer = 0
            Dim head As String = ""
            Dim cnt As Integer = dt.Rows.Count
            Dim arlist As New ArrayList(cnt)

            While k < dt.Columns.Count
                head += dt.Columns(k).ColumnName.ToString + "|"
                k += 1
            End While
            DataAR.Add(head)
            arlist.Add(head)
            If dt.Rows.Count <> 0 Then
                While i < dt.Rows.Count
                    arlist.Add(dt.Rows(i).ItemArray().ToArray())
                    i += 1
                End While
            End If
            Return arlist.ToArray
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' get form quary string based on passed form name
    ''' </summary>
    <WebMethod(EnableSession:=True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetFormQuaryString(ByVal formName As String) As String
        Try
            Dim archived As String = "0"
            If formName.Contains("|") Then
                archived = formName.Split("|")(1)
                formName = formName.Split("|")(0)
            End If
            Dim daForm As New TblFormsFactory
            Dim comp_id = LoginInfo.GetComp_id()
            Dim user_id = LoginInfo.GetUser__Id()

            Dim quaryStr = daForm.GetAllBy(TblForms.TblFormsFields.FormName, formName)(0).FormQueryAr


            If formName = "Files" Then
                If archived = "1" Then
                    quaryStr = quaryStr + " and tblfiles.user_id=" + Context.Request.Cookies("UserInfo")("UserId")
                Else
                    quaryStr = quaryStr + " and tblshare.user_id=" + Context.Request.Cookies("UserInfo")("UserId")
                End If
            End If
            If formName = "Users" Then
                If user_id = "1" Then
                    quaryStr = quaryStr
                Else
                    quaryStr = quaryStr + " and tblUsers.comp_id=" + comp_id.ToString
                End If
            End If

            Return quaryStr
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function


    ''' <summary>
    ''' get form keys based on form name
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetFormKeys(ByVal formName As String) As String
        Try
            Dim daForm As New TblFormsFactory
            Dim formId As String = daForm.GetAllBy(TblForms.TblFormsFields.FormName, formName)(0).Id.ToString
            Dim dt = DBManager.Getdatatable("select KeyName, KeyForm from TblFormKeys where FormId ='" + formId + "'")
            Return PublicFunctions.ConvertDataTabletoString(dt)
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' get login user Type|RelatedSalesmanCode
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetLoginUserTypeAndCode() As String
        Try
            Dim pf As New PublicFunctions
            Dim AgentID = LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")("UserID"))
            Dim Usertype = pf.GetUserType(AgentID)
            Return Usertype + "|" + AgentID
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region

    '  '#Region "Contact Details"
    '  <WebMethod(True)> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '  Public Function SaveNewContact(ByVal contactJSON As Object) As String
    '      Try
    '          _sqlconn.Open()
    '          _sqltrans = _sqlconn.BeginTransaction
    '          Dim dtCustomers As New Tblcustomers
    '          Dim daCustomers As New TblcustomersFactory
    '          Dim dictBasicDataJson As Dictionary(Of String, Object) = contactJSON
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("address")) Then
    '              dtCustomers.Address = dictBasicDataJson("address").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("approve_type")) Then
    '              dtCustomers.Approve_type = dictBasicDataJson("approve_type").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("card_version")) Then
    '              dtCustomers.Card_version = dictBasicDataJson("card_version").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("category")) Then
    '              dtCustomers.Category = dictBasicDataJson("category").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("email")) Then
    '              dtCustomers.Email = dictBasicDataJson("email").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("expire_date")) Then
    '              dtCustomers.Expire_date = dictBasicDataJson("expire_date").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("fname")) Then
    '              dtCustomers.Fname = dictBasicDataJson("fname").ToString
    '          End If
    '          dtCustomers.Hotel_id = 1
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("lname")) Then
    '              dtCustomers.Lname = dictBasicDataJson("lname").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("mname")) Then
    '              dtCustomers.Mname = dictBasicDataJson("mname").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("national_id")) Then
    '              dtCustomers.National_id = dictBasicDataJson("national_id").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("phone")) Then
    '              dtCustomers.Phone = dictBasicDataJson("phone").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("pleace")) Then
    '              dtCustomers.Pleace = dictBasicDataJson("pleace").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("public_notes")) Then
    '              dtCustomers.Public_notes = dictBasicDataJson("public_notes").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("special_notes")) Then
    '              dtCustomers.Special_notes = dictBasicDataJson("special_notes").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("tel")) Then
    '              dtCustomers.Tel = dictBasicDataJson("tel").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("type")) Then
    '              dtCustomers.Type = dictBasicDataJson("type").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("work_place")) Then
    '              dtCustomers.Work_place = dictBasicDataJson("work_place").ToString
    '          End If
    '          If Not daCustomers.InsertTrans(dtCustomers, _sqlconn, _sqltrans) Then
    '              _sqltrans.Rollback()
    '              _sqlconn.Close()
    '              Return "Error occured while adding new contact"
    '          End If
    '          _sqltrans.Commit()
    '          _sqlconn.Close()
    '          Return "True"
    '      Catch ex As Exception
    '          _sqltrans.Rollback()
    '          _sqlconn.Close()
    '          Return ex.Message
    '      End Try
    '  End Function

    '  '#Region "Contact Details"
    '  <WebMethod(True)> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '  Public Function SaveNewcompany(ByVal contactJSON As Object) As String
    '      Try
    '          _sqlconn.Open()
    '          _sqltrans = _sqlconn.BeginTransaction
    '          Dim dttravel_agencies As New Tbltravelagency
    '          Dim datravel_agencies As New TbltravelagencyFactory
    '          Dim dictBasicDataJson As Dictionary(Of String, Object) = contactJSON
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("addcompany_address")) Then
    '              dttravel_agencies.Address = dictBasicDataJson("addcompany_address").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("addcompany_disscount")) Then
    '              dttravel_agencies.Disscount = dictBasicDataJson("addcompany_disscount").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("addcompany_email")) Then
    '              dttravel_agencies.Email = dictBasicDataJson("addcompany_email").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("addcompany_tel")) Then
    '              dttravel_agencies.Tel = dictBasicDataJson("addcompany_tel").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("addcompany_city")) Then
    '              dttravel_agencies.City = dictBasicDataJson("addcompany_city").ToString
    '          End If
    '          If Not String.IsNullOrWhiteSpace(dictBasicDataJson("addcompany_name")) Then
    '              dttravel_agencies.Name = dictBasicDataJson("addcompany_name").ToString
    '          End If
    '          If Not datravel_agencies.InsertTrans(dttravel_agencies, _sqlconn, _sqltrans) Then
    '              _sqltrans.Rollback()
    '              _sqlconn.Close()
    '              Return "Error occured while adding new contact"
    '          End If
    '          _sqltrans.Commit()
    '          _sqlconn.Close()
    '          Return "True"
    '      Catch ex As Exception
    '          _sqltrans.Rollback()
    '          _sqlconn.Close()
    '          Return ex.Message
    '      End Try
    '  End Function

    '  <WebMethod(True)> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '  Public Function GetContactDetails(ByVal conditionJSON As Object) As String
    '      Try
    '          Dim condition As Dictionary(Of String, Object) = conditionJSON
    '          Dim colName = condition("ColName").ToString
    '          Dim value = condition("Value").ToString
    '          Dim ColumnFilter As String = ""
    '              colName = "TC." + colName
    '          Dim dt As DataTable = DBManager.Getdatatable("Select * from tblcustomers TC where isNull(Deleted,0)=0 and " + colName + "='" + value + "'")
    '                  Return PublicFunctions.ConvertDataTabletoString(dt)
    '      Catch ex As Exception
    '          Return String.Empty
    '      End Try
    '  End Function
    '  <WebMethod(True)> _
    '<System.Web.Script.Services.ScriptMethod()>
    '  Public Function GetcompanyDetails(ByVal conditionJSON As Object) As String
    '      Try
    '          Dim condition As Dictionary(Of String, Object) = conditionJSON
    '          Dim colName = condition("ColName").ToString
    '          Dim value = condition("Value").ToString
    '          Dim ColumnFilter As String = ""
    '          colName = "TC." + colName
    '          Dim dt As DataTable = DBManager.Getdatatable("Select * from tbltravelagency TC where isNull(Deleted,0)=0 and " + colName + "='" + value + "'")
    '          Return PublicFunctions.ConvertDataTabletoString(dt)
    '      Catch ex As Exception
    '          Return String.Empty
    '      End Try
    '  End Function

    '  <WebMethod()> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '  Public Function GetContactFirstName(ByVal prefixText As String) As String()
    '      Dim dt As New DataTable
    '      Dim Names As New List(Of String)
    '      Dim x As Integer = 0
    '      Try
    '          Dim UserId = Context.Request.Cookies("UserInfo")("UserId")
    '          Dim usertype = pF.GetUserType(UserId)
    '          Dim AgentQuery = ""
    '          Dim Query As String = "Select fname from tblcustomers TC where isNull(Deleted,0)=0 "
    '          If prefixText = "*" Then
    '              dt = DBManager.Getdatatable(Query + " order by TC.fname")
    '          Else
    '              dt = DBManager.Getdatatable(Query + " and TC.fname like'%" & prefixText & "%' order by TC.fname")
    '          End If
    '          If dt.Rows.Count <> 0 Then
    '              Dim i = 0
    '              While i < dt.Rows.Count
    '                  Names.Add(dt.Rows(i).Item("fname").ToString)
    '                  i += 1
    '              End While
    '              Return Names.ToArray
    '          Else
    '              Names.Add("No results found!")
    '              Return Names.ToArray
    '          End If
    '      Catch ex As Exception
    '          Dim res As New List(Of String)
    '          res.Add("No results found!")
    '          Return res.ToArray
    '      End Try
    '      Return Nothing
    '  End Function

    '  <WebMethod()> _
    '   <System.Web.Script.Services.ScriptMethod()>
    '  Public Function GetContactmidlleName(ByVal prefixText As String) As String()
    '      Dim dt As New DataTable
    '      Dim Names As New List(Of String)
    '      Dim x As Integer = 0
    '      Try
    '          Dim UserId = Context.Request.Cookies("UserInfo")("UserId")
    '          Dim usertype = pF.GetUserType(UserId)
    '          Dim AgentQuery = ""
    '          Dim Query As String = "Select mname from tblcustomers TC where isNull(Deleted,0)=0 "
    '          If prefixText = "*" Then
    '              dt = DBManager.Getdatatable(Query + " order by TC.mname")
    '          Else
    '              dt = DBManager.Getdatatable(Query + " and TC.mname like'%" & prefixText & "%' order by TC.mname")
    '          End If
    '          If dt.Rows.Count <> 0 Then
    '              Dim i = 0
    '              While i < dt.Rows.Count
    '                  Names.Add(dt.Rows(i).Item("mname").ToString)
    '                  i += 1
    '              End While
    '              Return Names.ToArray
    '          Else
    '              Names.Add("No results found!")
    '              Return Names.ToArray
    '          End If
    '      Catch ex As Exception
    '          Dim res As New List(Of String)
    '          res.Add("No results found!")
    '          Return res.ToArray
    '      End Try
    '      Return Nothing
    '  End Function
    '  <WebMethod()> _
    '  <System.Web.Script.Services.ScriptMethod()>
    '  Public Function GetContactlastName(ByVal prefixText As String) As String()
    '      Dim dt As New DataTable
    '      Dim Names As New List(Of String)
    '      Dim x As Integer = 0
    '      Try
    '          Dim UserId = Context.Request.Cookies("UserInfo")("UserId")
    '          Dim usertype = pF.GetUserType(UserId)
    '          Dim AgentQuery = ""
    '          Dim Query As String = "Select lname from tblcustomers TC where isNull(Deleted,0)=0 "
    '          If prefixText = "*" Then
    '              dt = DBManager.Getdatatable(Query + " order by TC.lname")
    '          Else
    '              dt = DBManager.Getdatatable(Query + " and TC.lname like'%" & prefixText & "%' order by TC.lname")
    '          End If
    '          If dt.Rows.Count <> 0 Then
    '              Dim i = 0
    '              While i < dt.Rows.Count
    '                  Names.Add(dt.Rows(i).Item("lname").ToString)
    '                  i += 1
    '              End While
    '              Return Names.ToArray
    '          Else
    '              Names.Add("No results found!")
    '              Return Names.ToArray
    '          End If
    '      Catch ex As Exception
    '          Dim res As New List(Of String)
    '          res.Add("No results found!")
    '          Return res.ToArray
    '      End Try
    '      Return Nothing
    '  End Function
    '  <WebMethod()> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '  Public Function GetContactapproveno(ByVal prefixText As String) As String()
    '      Dim dt As New DataTable
    '      Dim Names As New List(Of String)
    '      Dim x As Integer = 0
    '      Try
    '          Dim UserId = Context.Request.Cookies("UserInfo")("UserId")
    '          Dim usertype = pF.GetUserType(UserId)
    '          Dim AgentQuery = ""
    '          Dim Query As String = "Select approve_no from tblcustomers TC where isNull(Deleted,0)=0 "
    '          If prefixText = "*" Then
    '              dt = DBManager.Getdatatable(Query + " order by TC.approve_no")
    '          Else
    '              dt = DBManager.Getdatatable(Query + " and TC.approve_no like'%" & prefixText & "%' order by TC.approve_no")
    '          End If
    '          If dt.Rows.Count <> 0 Then
    '              Dim i = 0
    '              While i < dt.Rows.Count
    '                  Names.Add(dt.Rows(i).Item("approve_no").ToString)
    '                  i += 1
    '              End While
    '              Return Names.ToArray
    '          Else
    '              Names.Add("No results found!")
    '              Return Names.ToArray
    '          End If
    '      Catch ex As Exception
    '          Dim res As New List(Of String)
    '          res.Add("No results found!")
    '          Return res.ToArray
    '      End Try
    '      Return Nothing
    '  End Function
    '  <WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()>
    '  Public Function GetContacttel(ByVal prefixText As String) As String()
    '      Dim dt As New DataTable
    '      Dim Names As New List(Of String)
    '      Dim x As Integer = 0
    '      Try
    '          Dim UserId = Context.Request.Cookies("UserInfo")("UserId")
    '          Dim usertype = pF.GetUserType(UserId)
    '          Dim AgentQuery = ""
    '          Dim Query As String = "Select tel from tblcustomers TC where isNull(Deleted,0)=0 "
    '          If prefixText = "*" Then
    '              dt = DBManager.Getdatatable(Query + " order by TC.tel")
    '          Else
    '              dt = DBManager.Getdatatable(Query + " and TC.tel like'%" & prefixText & "%' order by TC.tel")
    '          End If
    '          If dt.Rows.Count <> 0 Then
    '              Dim i = 0
    '              While i < dt.Rows.Count
    '                  Names.Add(dt.Rows(i).Item("tel").ToString)
    '                  i += 1
    '              End While
    '              Return Names.ToArray
    '          Else
    '              Names.Add("No results found!")
    '              Return Names.ToArray
    '          End If
    '      Catch ex As Exception
    '          Dim res As New List(Of String)
    '          res.Add("No results found!")
    '          Return res.ToArray
    '      End Try
    '      Return Nothing
    '  End Function

    '  <WebMethod()> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '  Public Function Getcompanyname(ByVal prefixText As String) As String()
    '      Dim dt As New DataTable
    '      Dim Names As New List(Of String)
    '      Dim x As Integer = 0
    '      Try
    '          Dim UserId = Context.Request.Cookies("UserInfo")("UserId")
    '          Dim usertype = pF.GetUserType(UserId)
    '          Dim AgentQuery = ""
    '          Dim Query As String = "Select name from tbltravelagency TC where isNull(Deleted,0)=0 "
    '          If prefixText = "*" Then
    '              dt = DBManager.Getdatatable(Query + " order by TC.name")
    '          Else
    '              dt = DBManager.Getdatatable(Query + " and TC.name like'%" & prefixText & "%' order by TC.name")
    '          End If
    '          If dt.Rows.Count <> 0 Then
    '              Dim i = 0
    '              While i < dt.Rows.Count
    '                  Names.Add(dt.Rows(i).Item("name").ToString)
    '                  i += 1
    '              End While
    '              Return Names.ToArray
    '          Else
    '              Names.Add("No results found!")
    '              Return Names.ToArray
    '          End If
    '      Catch ex As Exception
    '          Dim res As New List(Of String)
    '          res.Add("No results found!")
    '          Return res.ToArray
    '      End Try
    '      Return Nothing
    '  End Function
    '  <WebMethod()> _
    ' <System.Web.Script.Services.ScriptMethod()>
    '  Public Function Getcompanytel(ByVal prefixText As String) As String()
    '      Dim dt As New DataTable
    '      Dim Names As New List(Of String)
    '      Dim x As Integer = 0
    '      Try
    '          Dim UserId = Context.Request.Cookies("UserInfo")("UserId")
    '          Dim usertype = pF.GetUserType(UserId)
    '          Dim AgentQuery = ""
    '          Dim Query As String = "Select tel from tbltravelagency TC where isNull(Deleted,0)=0 "
    '          If prefixText = "*" Then
    '              dt = DBManager.Getdatatable(Query + " order by TC.tel")
    '          Else
    '              dt = DBManager.Getdatatable(Query + " and TC.tel like'%" & prefixText & "%' order by TC.tel")
    '          End If
    '          If dt.Rows.Count <> 0 Then
    '              Dim i = 0
    '              While i < dt.Rows.Count
    '                  Names.Add(dt.Rows(i).Item("tel").ToString)
    '                  i += 1
    '              End While
    '              Return Names.ToArray
    '          Else
    '              Names.Add("No results found!")
    '              Return Names.ToArray
    '          End If
    '      Catch ex As Exception
    '          Dim res As New List(Of String)
    '          res.Add("No results found!")
    '          Return res.ToArray
    '      End Try
    '      Return Nothing
    '  End Function
    ''' <summary>
    ''' Set Menu Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SetMenuType(ByVal mType As String) As Boolean
        Try
            Dim user_id = Context.Request.Cookies("UserInfo")("UserId")
            Dim Updated As Integer = DBManager.ExcuteQuery("update tblUsers set MenuType='" + mType + "' where id='" + user_id + "'")
            If Updated = 1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    '    ''' <summary>
    '    ''' Get loged in agent detaials
    '    ''' </summary>
    '    <WebMethod(True)> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function getAgentDetails(ByVal str As String) As String
    '        Try
    '            Dim AgentID As String = LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")("UserID"))
    '            Dim dt As DataTable = DBManager.Getdatatable("select * from TblUsers where code='" + AgentID + "'")
    '            Return PublicFunctions.ConvertDataTabletoString(dt)
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    End Function

    '    <WebMethod()> _
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetCustomers(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
    '        Dim dt As New DataTable
    '        Dim Names As New List(Of String)(10)
    '        Dim x As Integer = 0
    '        Dim ColName = contextKey.Split("|")(1)
    '        Dim CustomerType = contextKey.Split("|")(0)
    '        Dim query As String = ""
    '        If CustomerType = "Buyer/Tenant" Then
    '            query = "where client = 1 and (deleted ='False' or deleted is null)"
    '        ElseIf CustomerType = "Landlord" Then
    '            query = "where owner = 1 and (deleted ='False' or deleted is null)"
    '        End If
    '        If query = "" Then
    '            query = "where 1=1 and (deleted ='False' or deleted is null) "
    '        End If
    '        Try
    '            If prefixText = "*" Then
    '                dt = DBManager.Getdatatable("select " + ColName + " from TblContacts " + query + " order by  " + ColName)
    '            Else
    '                dt = DBManager.Getdatatable("select " + ColName + " from TblContacts " + query + " and " + ColName + " Like '%" + prefixText + "%' " + " order by  " + ColName)
    '            End If
    '            If dt.Rows.Count <> 0 Then
    '                While x < dt.Rows.Count
    '                    Names.Add(dt(x)(ColName))
    '                    x += 1
    '                End While
    '                Return Names.ToArray
    '            Else
    '                Names.Add("No results found!")
    '                Return Names.ToArray
    '            End If
    '        Catch ex As Exception
    '            '  System.Web.HttpContext.Current.Trace.Warn("Sitemap error: " & ex.Message)
    '        End Try
    '        Names.Add("No results found!")
    '        Return Names.ToArray
    '    End Function
    '#End Region

    '#Region "Lockup"
    '    ''' <summary>
    '    ''' Get Lockup value for dynamic table select
    '    ''' </summary>
    '    <WebMethod()> _
    '   <System.Web.Script.Services.ScriptMethod()>
    '    Public Function GetlockupValues(ByVal type As String) As String
    '        Try
    '            Dim Values As String = "["
    '            Dim dtLockup As DataTable = DBManager.Getdatatable("select * from tblLockup where Type='" + type + "'")
    '            If dtLockup.Rows.Count > 0 Then
    '                For i As Integer = 0 To dtLockup.Rows.Count - 1
    '                    Dim Description = dtLockup.Rows(i)("Description").ToString()
    '                    Values += "'" + Description + "',"
    '                Next
    '                Values = Values.Remove(Values.Length - 1) + "]"
    '                Return Values
    '            Else
    '                Values = "['']"
    '                Return Values
    '            End If
    '        Catch ex As Exception
    '            Return String.Empty
    '        End Try
    '    End Function
    '#End Region
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Sub SetUserDetailsToGlobalVar(ByVal UserDetails As String)
        clsGeneralVariables.userDetails = UserDetails
    End Sub

    ''' <summary>
    ''' get form keys based on form name
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function resetPassword(ByVal userName As String) As String
        Try
            Dim dtsms = LoginInfo.GetSMSConfig()
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select User_Password from tblUsers where isNull(deleted,0) != 1 and User_PhoneNumber ='" + userName + "'")
            If dt.Rows.Count <> 0 Then
                Dim message = "كلمة المرور هى : " & dt.Rows(0)(0).ToString()
                Dim res = PublicFunctions.DoSendSMS(userName, message, "", dtsms)
                Return res.ToString
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Class
