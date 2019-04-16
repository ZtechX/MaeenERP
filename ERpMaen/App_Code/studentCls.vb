#Region "Import"
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Web.Script.Services
Imports BusinessLayer.BusinessLayer
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Generic
Imports System
Imports ERpMaen

#End Region

'Imports System.Xml
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<System.Web.Script.Services.ScriptService()>
Public Class studentCls
    Inherits System.Web.Services.WebService

#Region "Global_Variables"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
#End Region

    '#Region "Save"
    '    ''' <summary>
    '    ''' Save  Type
    '    ''' </summary>
    '    <WebMethod(True)>
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function Save(ByVal id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As String

    '        Try
    '            _sqlconn.Open()
    '            _sqltrans = _sqlconn.BeginTransaction
    '            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
    '            dictBasicDataJson.Add("comp_id", LoginInfo.GetComp_id())

    '            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_Students", id, _sqlconn, _sqltrans) Then

    '                Dim dictBasicDataJson1 As New Dictionary(Of String, Object)
    '                dictBasicDataJson1.Add("full_name", dictBasicDataJson("name"))

    '                dictBasicDataJson1.Add("User_Image", dictBasicDataJson("Stud_Image"))
    '                dictBasicDataJson1.Add("User_Email", dictBasicDataJson("email"))
    '                dictBasicDataJson1.Add("Active", dictBasicDataJson("active"))
    '                dictBasicDataJson1.Add("User_Password", dictBasicDataJson("password"))
    '                dictBasicDataJson1.Add("User_PhoneNumber", dictBasicDataJson("tel"))
    '                dictBasicDataJson1.Add("comp_id", LoginInfo.GetComp_id())
    '                dictBasicDataJson1.Add("User_Type", 8)
    '                dictBasicDataJson1.Add("related_id", PublicFunctions.GetIdentity(_sqlconn, _sqltrans))


    '                Dim dt = DBManager.Getdatatable("select id from tbllock_up where RelatedId=4 and comp_id=" + LoginInfo.GetComp_id())
    '                If dt.Rows.Count <> 0 Then
    '                    Dim group_id = dt.Rows(0)(0).ToString
    '                    dictBasicDataJson1.Add("group_id", group_id)
    '                End If

    '                Dim dtcheckemailphone As New DataTable
    '                dtcheckemailphone = DBManager.Getdatatable("Select * from TblUsers where  User_Email='" + dictBasicDataJson1("User_Email") + "' or User_PhoneNumber='" + dictBasicDataJson1("User_PhoneNumber") + "'")
    '                If dtcheckemailphone.Rows.Count = 0 Then
    '                    If Not PublicFunctions.TransUpdateInsert(dictBasicDataJson1, "tblUsers", "", _sqlconn, _sqltrans) Then
    '                        _sqltrans.Rollback()
    '                        _sqlconn.Close()
    '                        Return "False|لم يتم الحفظ"
    '                    End If
    '                Else
    '                    _sqltrans.Rollback()
    '                    _sqlconn.Close()
    '                    Return "False|اسم المستخدم او البريد الالكترونى او التلفون مستخدم"
    '                End If

    '                _sqltrans.Commit()
    '                _sqlconn.Close()
    '                    Return "True"


    '                Else
    '                _sqltrans.Rollback()
    '                _sqlconn.Close()
    '                Return "False|لم يتم الحفظ"
    '            End If

    '        Catch ex As Exception
    '            _sqltrans.Rollback()
    '            _sqlconn.Close()
    '            Return "False|لم يتم الحفظ"
    '        End Try
    '    End Function
    '#End Region

#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Save(ByVal UserId As String, ByVal basicDataJson As Object) As String
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim success As Boolean = False


            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson

            dictBasicDataJson.Add("User_Type", 8)
            dictBasicDataJson.Add("comp_id", LoginInfo.GetComp_id())
            Dim dt = DBManager.Getdatatable("select id from tbllock_up where RelatedId=4 and comp_id=" + LoginInfo.GetComp_id())
            If dt.Rows.Count <> 0 Then
                Dim group_id = dt.Rows(0)(0).ToString
                dictBasicDataJson.Add("group_id", group_id)

                Dim dtcheckemailphone As DataTable
                If UserId = "" Then
                    dtcheckemailphone = DBManager.Getdatatable("Select * from TblUsers where  user_indenty='" + dictBasicDataJson("user_indenty") + "' or User_PhoneNumber='" + dictBasicDataJson("User_PhoneNumber") + "'")
                Else
                    dtcheckemailphone = DBManager.Getdatatable("Select * from TblUsers where ( user_indenty='" + dictBasicDataJson("user_indenty") + "' or User_PhoneNumber='" + dictBasicDataJson("User_PhoneNumber") + "')  and  Id!='" + UserId.ToString + "'")
                End If


                If dtcheckemailphone.Rows.Count = 0 Then

                    If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblUsers", UserId, _sqlconn, _sqltrans) Then
                        success = True
                    Else
                        success = False
                    End If
                Else
                    Return "False|رقم الهوية او التلفون مُستخدم"
                End If

                If UserId = "" And success Then
                    Dim Id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)

                ElseIf UserId <> "" And success Then


                End If
                If success Then
                    _sqltrans.Commit()
                    _sqlconn.Close()
                    Return "True|تم الحفظ بنجاح"
                End If
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return "False|لم يتم الحفظ"
            End If
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return "False|لم يتم الحفظ"
        End Try
    End Function
#End Region


#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_data(ByVal comp_id As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select * from tblUsers where comp_id=" + comp_id)

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
        Names.Add("0")
        Names.Add(" No Results were Found!")
        Return Names.ToArray
    End Function

#End Region

#Region "Check user"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function checkUser() As String

        Try
            Dim dt As New DataTable
            Dim UserId As String = HttpContext.Current.Request.Cookies("UserInfo")("UserId")
            dt = DBManager.Getdatatable("select isNull(comp_id,'0') comp_id,User_Type  from tblUsers where id=" + UserId)
            If dt.Rows.Count <> 0 Then

                If dt.Rows(0)("User_Type").ToString = "13" Then
                    Return "Superadmin"
                End If
                Return dt.Rows(0)("comp_id").ToString
            End If

        Catch ex As Exception

            Return ""
        End Try
        Return ""
    End Function

#End Region


#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Edit(ByVal editItemId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("tblUsers", editItemId)
            Names.Add("1")
            Names.Add(str)
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "Delete"
    ''' <summary>
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Delete(ByVal deleteItem As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "tblUsers") Then
                Names.Add("1")
                Names.Add("تم الحذف بنجاح!")
            Else
                Names.Add("2")
                Names.Add("لا يمكن الحذف!")
            End If
            Return Names.ToArray
        Catch
            Names.Add("2")
            Names.Add("لا يمكن الحذف!")
            Return Names.ToArray
        End Try
    End Function
#End Region





End Class