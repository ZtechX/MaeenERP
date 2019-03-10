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
Public Class cases
    Inherits System.Web.Services.WebService

#Region "Global_Variables"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
#End Region

#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Save(ByVal id As String, ByVal basicDataJson As Dictionary(Of String, Object), ByVal persons_owner As Dictionary(Of String, Object), ByVal person_against As Dictionary(Of String, Object), ByVal children As Dictionary(Of String, Object), ByVal status As Dictionary(Of String, Object), ByVal tabs As List(Of Object), ByVal attch_file_DataJsonList As List(Of Object)) As Integer
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dictchildren As Dictionary(Of String, Object) = children
            Dim dictstatus As Dictionary(Of String, Object) = status
            Dim dictperson_owner As Dictionary(Of String, Object) = persons_owner
            Dim dictperson_against As Dictionary(Of String, Object) = person_against
            Dim dt_user As DataTable
            Dim dt_person_cases As DataTable
            Dim result = 0
            Dim person1_id = 0
            Dim person2_id = 0
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If id <> "" Then
                'DBManager.ExcuteQuery("delete from ash_cases where case_id=" + id.ToString)
                dt_person_cases = DBManager.Getdatatable("select * from ash_cases where id=" + id.ToString())
                person1_id = dt_person_cases.Rows(0).Item("person1_id")
                person2_id = dt_person_cases.Rows(0).Item("person2_id")
            End If
            dictBasicDataJson.Add("comp_id", dt_user.Rows(0).Item("comp_id"))
            dictBasicDataJson.Add("person1_id", Save_person(persons_owner, person1_id, ""))
            dictBasicDataJson.Add("person2_id", Save_person(person_against, person2_id, ""))
            dictBasicDataJson.Add("boys_no", dictchildren("boys_no"))
            dictBasicDataJson.Add("girls_no", dictchildren("girls_no"))
            dictBasicDataJson.Add("childrens_no", dictchildren("childrens_no"))
            dictBasicDataJson.Add("status", dictstatus("status"))
            dictBasicDataJson.Add("depart", dictstatus("depart"))
            dictBasicDataJson.Add("advisor_id", dictstatus("advisor_id"))
            dictBasicDataJson.Add("court_details", dictstatus("court_details"))
            dictBasicDataJson.Add("details", dictstatus("details"))
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If id = "" Then
                If dictchildren("child_custody").ToString = dictperson_owner("name").ToString Then
                    dictBasicDataJson.Add("child_custody", dictBasicDataJson("person1_id"))
                ElseIf dictchildren("child_custody").ToString = dictperson_against("name").ToString Then
                    dictBasicDataJson.Add("child_custody", dictBasicDataJson("person2_id"))
                End If
            End If

            PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_cases", id, _sqlconn, _sqltrans)
            Dim case_id = 0
            If id <> "" Then
                case_id = id
            Else
                case_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
            End If

            ' Savea_ttch_file(attch_file_DataJsonList, case_id)
            Save_person(persons_owner, dictBasicDataJson("person1_id"), case_id)
            Save_person(person_against, dictBasicDataJson("person2_id"), case_id)
            DBManager.ExcuteQuery("delete from ash_case_tabs where case_id=" + case_id.ToString)
            Dim tab_id = 0
            For Each tab As Object In tabs
                Dim dict_tabs As Dictionary(Of String, Object) = tab
                dict_tabs.Add("case_id", case_id)
                PublicFunctions.TransUpdateInsert(dict_tabs, "ash_case_tabs", tab_id, _sqlconn, _sqltrans)
            Next
            result = 1
            'Dim letter_id = 0
            'Savea_ttch_file(attch_file_DataJsonList, letter_id)
            If result = 1 Then
                _sqltrans.Commit()
                _sqlconn.Close()
                Return case_id
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return 0
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return 0
        End Try
    End Function
#End Region
#Region "new_person"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Save_person(ByVal basicDataJson As Dictionary(Of String, Object), ByVal id As String, ByVal case_id As String) As Integer
        Try
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson

            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            Dim result = 0
            Dim person = 0

            PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_persons", id, _sqlconn, _sqltrans)
            dictBasicDataJson.Remove("case_id")
            dictBasicDataJson.Remove("user_id")
            If id = "" Or id = 0 Then
                person = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
            Else
                person = id
            End If
            result = 1
            If result = 1 Then
                Return person
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try
    End Function
#End Region


#Region "save_children"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_children(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Integer
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_cases As New DataTable
            Dim dt_cases_girls As New DataTable
            Dim dt_childrens As New DataTable
            Dim boys = 0
            Dim girls = 0
            Dim children = 1
            Dim gender = dictBasicDataJson("gender").ToString
            dt_cases = DBManager.Getdatatable("select * from ash_cases where  id=" + case_id.ToString())
            dt_childrens = DBManager.Getdatatable(" select (select count(id) as boy from ash_case_childrens where gender=1 and case_id=" + case_id.ToString() + " ) as boy,(select count(id) as girl from ash_case_childrens where gender=2 and case_id=" + case_id.ToString() + ") As girl from ash_case_childrens")
            If dt_childrens.Rows.Count <> 0 Then
                boys = dt_childrens.Rows(0).Item("boy").ToString
                girls = dt_childrens.Rows(0).Item("girl").ToString
            End If
            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If gender = 1 Then
                If boys = dt_cases.Rows(0).Item("boys_no").ToString Then
                    children = -1
                Else
                End If
            ElseIf gender = 2 Then
                If girls = dt_cases.Rows(0).Item("girls_no").ToString Then
                    children = -2
                End If
            End If
            If children <> -1 And children <> -2 Then

                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_childrens", id, _sqlconn, _sqltrans) Then
                    _sqltrans.Commit()
                    _sqlconn.Close()
                    Return children
                Else
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return -3
                End If
            Else
                Return children
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return -3
        End Try

    End Function
#End Region

#Region "save_children_receive"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_children_receive(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_receiving_delivery_basic", id, _sqlconn, _sqltrans) Then

                Dim basic_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                _sqltrans.Commit()
                _sqlconn.Close()
                Return True
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try

    End Function
#End Region

#Region "save_delivery_details"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_delivery_details(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object), ByVal childrens As List(Of Object), ByVal new_date As String) As String
        Dim Names As New List(Of String)(10)
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson

            'dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            'dictBasicDataJson("date_m") = PublicFunctions.ConvertDatetoNumber(dictBasicDataJson("date_m"))
            'dictBasicDataJson("new_date_m") = PublicFunctions.ConvertDatetoNumber(dictBasicDataJson("new_date_m"))
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_receiving_delivery_details", id, _sqlconn, _sqltrans) Then
                Dim details_id = 0
                If id <> "" Then
                    details_id = id
                Else
                    details_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                End If
                DBManager.ExcuteQuery("delete  from ash_case_children_receiving_details  where details_id=" + details_id.ToString)
                For Each children As Object In childrens
                    id = 0
                    Dim dict_children As Dictionary(Of String, Object) = children
                    dict_children.Add("details_id", details_id)
                    PublicFunctions.TransUpdateInsert(dict_children, "ash_case_children_receiving_details", id, _sqlconn, _sqltrans)
                Next
                _sqltrans.Commit()
                _sqlconn.Close()
                Return details_id.ToString + "|" + dictBasicDataJson("type").ToString
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return 0
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return 0

        End Try

    End Function
#End Region
#Region "save_conciliation"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_conciliation(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_conciliation", id, _sqlconn, _sqltrans) Then
                _sqltrans.Commit()
                _sqlconn.Close()
                Return True
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try

    End Function
#End Region

#Region "save_correspondences"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_correspondences(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_correspondences", id, _sqlconn, _sqltrans) Then
                _sqltrans.Commit()
                _sqlconn.Close()
                Return True
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try

    End Function
#End Region

#Region "save_sessions"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_sessions(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object), ByVal childrens As List(Of Object), ByVal persons As List(Of Object)) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_sessions", id, _sqlconn, _sqltrans) Then
                Dim session_id = 0
                If id <> "" Then
                    session_id = id
                Else
                    session_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                End If
                DBManager.ExcuteQuery("delete  from ash_session_children  where session_id=" + session_id.ToString)
                For Each children As Object In childrens
                    id = 0
                    Dim dict_children As Dictionary(Of String, Object) = children
                    dict_children.Add("session_id", session_id)
                    PublicFunctions.TransUpdateInsert(dict_children, "ash_session_children", id, _sqlconn, _sqltrans)
                Next

                DBManager.ExcuteQuery("delete  from ash_session_companions  where session_id=" + session_id.ToString)
                For Each person As Object In persons
                    id = 0
                    Dim dict_persons As Dictionary(Of String, Object) = person
                    dict_persons.Add("session_id", session_id)
                    PublicFunctions.TransUpdateInsert(dict_persons, "ash_session_companions", id, _sqlconn, _sqltrans)
                Next
                _sqltrans.Commit()
                _sqlconn.Close()
                Return True
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try

    End Function
#End Region
#Region "save_expense_basic"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_expense_basic(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_expense_basic", id, _sqlconn, _sqltrans) Then
                Dim basic_id = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)

                _sqltrans.Commit()
                    _sqlconn.Close()
                    Return True
                Else
                    _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try

    End Function
#End Region
#Region "save_expense_details"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function save_expense_details(ByVal id As String, ByVal case_id As String, ByVal basicDataJson As Dictionary(Of String, Object), ByVal new_date_m As String, ByVal new_date_h As String) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            dictBasicDataJson.Add("case_id", case_id)
            dictBasicDataJson.Add("user_id", LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_case_expenses_details", id, _sqlconn, _sqltrans) Then
                If dictBasicDataJson("done") = True Then
                    id = 0
                    Dim dict = New Dictionary(Of String, Object)
                    dict.Add("date_m", PublicFunctions.ConvertDatetoNumber(new_date_m))
                    dict.Add("date_h", new_date_h)
                    dict.Add("case_id", case_id)
                    PublicFunctions.TransUpdateInsert(dict, "ash_case_expenses_details", id, _sqlconn, _sqltrans)

                End If
                _sqltrans.Commit()
                _sqlconn.Close()
                Return True
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return True
        End Try

    End Function
#End Region


#Region "Get Serial"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getSerial() As Integer
        Dim dtm As New DataTable
        dtm = DBManager.Getdatatable("Select isNull(max(code)+1,1) FROM ash_cases")
        If dtm.Rows.Count <> 0 Then
            Return dtm.Rows(0)(0)
        End If
        Return 1
    End Function
#End Region

#Region "Get Serial_conciliation"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getSerial_conciliation() As Integer
        Dim dtm As New DataTable
        dtm = DBManager.Getdatatable("Select isNull(max(code)+1,1) FROM ash_case_conciliation")
        If dtm.Rows.Count <> 0 Then
            Return dtm.Rows(0)(0)
        End If
        Return 1
    End Function
#End Region

#Region "Get getSerial_correspondencesn"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getSerial_correspondencesn() As Integer
        Dim dtm As New DataTable
        dtm = DBManager.Getdatatable("Select isNull(max(code)+1,1) FROM ash_case_correspondences")
        If dtm.Rows.Count <> 0 Then
            Return dtm.Rows(0)(0)
        End If
        Return 1
    End Function
#End Region

#Region "getSerial_sessions"
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getSerial_sessions() As Integer
        Dim dtm As New DataTable
        dtm = DBManager.Getdatatable("Select isNull(max(code)+1,1) FROM ash_case_sessions")
        If dtm.Rows.Count <> 0 Then
            Return dtm.Rows(0)(0)
        End If
        Return 1
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
            Dim str As String = PublicFunctions.GetDataForUpdate("tblcontacts_groups", editItemId)
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
    Public Function Delete(ByVal deleteItems As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItems, "tblcontacts_groups") Then
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
#Region "save_file"
    ''' <summary>"
    ''' Save About images into db 
    ''' </summary>
    Private Function Savea_ttch_file(ByVal attch_file_DataJsonList As List(Of Object), ByVal letter_Id As String) As Boolean
        Try
            DBManager.ExcuteQuery("delete from tblImages where Source_id=" + letter_Id.ToString + " and Source='cases' ")
            For Each file_JSON As Object In attch_file_DataJsonList

                Dim dictfile As Dictionary(Of String, Object) = file_JSON
                Dim Source_id = letter_Id
                Dim Source = "cases"
                Dim Image_path = dictfile("url").ToString
                Dim Image_name = dictfile("Name").ToString
                If dictfile("Name").ToString <> "" Then
                    Image_name = dictfile("Name").ToString.Substring(0, Math.Min(dictfile("Name").ToString.Length, 50))
                End If

                DBManager.ExcuteQuery("insert into tblImages(Source_id,Source,Image_path,Image_name) values(" + Source_id.ToString + ",'" + Source.ToString + "','" + Image_path.ToString + "','" + Image_name.ToString + "')")



            Next

            Return True

        Catch ex As Exception
            DBManager.ExcuteQuery("delete from tblImages where Source_id=" + letter_Id.ToString + " and Source='out_letter' ")
            Return False
        End Try
    End Function
#End Region
#Region "get_tabs"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_tabs() As String()
        Dim Names As New List(Of String)(10)
        Dim str = ""
        Dim str1 = ""
        Try
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            ' TblInvoice.TblInvoiceFields.SAccount_cd
            Dim query As String = "select * from ash_tabs"
            Dim query1 As String = "select * from ash_case_childrens"

            dt = DBManager.Getdatatable(query)
            If dt.Rows.Count <> 0 Then
                str = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add(str)
            Else
                Names.Add("")
                Return Names.ToArray
            End If
            If dt1.Rows.Count <> 0 Then
                str1 = PublicFunctions.ConvertDataTabletoString(dt1)
                Names.Add(str1)
            Else
                Names.Add("")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("")
            Return Names.ToArray
        End Try
        Return Names.ToArray
    End Function

#End Region


#Region "get_checked_tab"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_checked_tab(ByVal id As String) As String()
        Dim Names As New List(Of String)(10)
        Dim str = ""
        Dim str1 = ""
        Try
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            ' TblInvoice.TblInvoiceFields.SAccount_cd
            Dim query As String = "select * from ash_case_tabs where case_id=" + id.ToString

            dt = DBManager.Getdatatable(query)
            If dt.Rows.Count <> 0 Then
                str = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add(str)
            Else
                Names.Add("")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("")
            Return Names.ToArray
        End Try
        Return Names.ToArray
    End Function

#End Region

#Region "get_dates"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_dates(ByVal new_date As String) As String()
        Dim Names As New List(Of String)(10)
        Dim str = ""
        Dim str1 = ""
        Try
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            ' TblInvoice.TblInvoiceFields.SAccount_cd
            Dim query As String = "SELECT * FROM   ash_case_receiving_delivery_details where date_h LIKE '___" + new_date.ToString + "%' "

            dt = DBManager.Getdatatable(query)
            If dt.Rows.Count <> 0 Then
                str = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add(str)
            Else
                Names.Add("0")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("")
            Return Names.ToArray
        End Try
        Return Names.ToArray
    End Function

#End Region


#Region "get_option_cases"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_option_cases() As String()
        Dim Names As New List(Of String)(10)
        Dim str = ""
        Dim str1 = ""
        Try
            Dim dt As New DataTable
            Dim dt1 As New DataTable
            ' TblInvoice.TblInvoiceFields.SAccount_cd
            Dim query As String = "select ash_cases.id as case_id,ash_cases.code as cases,ash_cases.date_m as date,ash_case_persons.name as person from ash_cases join ash_case_persons on ash_cases.person1_id=ash_case_persons.id"

            dt = DBManager.Getdatatable(query)
            If dt.Rows.Count <> 0 Then
                str = PublicFunctions.ConvertDataTabletoString(dt)
                Names.Add(str)
            Else
                Names.Add("")
                Return Names.ToArray
            End If
        Catch ex As Exception
            Names.Add("")
            Return Names.ToArray
        End Try
        Return Names.ToArray
    End Function

#End Region

#Region "show_all"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function show_all(ByVal printItemId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt_cases As New DataTable
            Dim dt_person_owner As New DataTable
            Dim dt_person_against As New DataTable
            Dim dt_children As New DataTable
            Dim dt_receiving_delivery_basic As New DataTable
            Dim dt_receiving_delivery_details As New DataTable
            Dim dt_conciliation As New DataTable
            Dim dt_correspondences As New DataTable
            Dim dt_sessions As New DataTable
            Dim dt_expenses As New DataTable
            Dim dt_expenses_details As New DataTable


            dt_cases = DBManager.Getdatatable("SELECT * from ash_cases  where  ash_cases.id = " + printItemId)
            dt_person_owner = DBManager.Getdatatable("select ash_case_persons.name as name,ash_case_persons.indenty as indenty,ash_case_persons.relationship_id as relationship_id,ash_case_persons.authorization_no as authorization_no from ash_cases join ash_case_persons on ash_cases.person1_id=ash_case_persons.id where ash_cases.id=" + printItemId)
            dt_person_against = DBManager.Getdatatable("select ash_case_persons.name as name,ash_case_persons.indenty as indenty,ash_case_persons.relationship_id as relationship_id,ash_case_persons.authorization_no as authorization_no from ash_cases join ash_case_persons on ash_cases.person2_id=ash_case_persons.id where ash_cases.id=" + printItemId)
            dt_children = DBManager.Getdatatable("SELECT * from ash_case_childrens where  ash_case_childrens.case_id = " + printItemId)
            dt_receiving_delivery_basic = DBManager.Getdatatable("SELECT * from ash_case_receiving_delivery_basic where  ash_case_receiving_delivery_basic.case_id = " + printItemId)
            dt_receiving_delivery_details = DBManager.Getdatatable("SELECT * from ash_case_receiving_delivery_details where  id = " + printItemId)
            dt_conciliation = DBManager.Getdatatable("SELECT * from ash_case_conciliation where  case_id = " + printItemId)
            dt_correspondences = DBManager.Getdatatable("SELECT  * from ash_case_correspondences where  case_id = " + printItemId)
            dt_sessions = DBManager.Getdatatable("SELECT  * from ash_case_sessions where  case_id = " + printItemId)
            dt_expenses = DBManager.Getdatatable("SELECT  * from ash_case_expense_basic where  case_id = " + printItemId)
            dt_expenses_details = DBManager.Getdatatable("SELECT  * from ash_case_expenses_details where  case_id = " + printItemId)
            '           dt_finance = DBManager.Getdatatable("SELECT tblproject_finance.id as 'finance_id', * from tblproject_finance left join  tbllock_up on tblproject_finance.payment_id=tbllock_up.id  where  project_id = " + printItemId)
            '           dt_prposal = DBManager.Getdatatable("SELECT tblproject_proposals.id as 'propsal_id', * from tblproject_proposals join  tbllock_up on tblproject_proposals.send_type=tbllock_up.id  where type='ST' and  project_id = " + printItemId)
            '           dt_info = DBManager.Getdatatable("SELECT isNull(servicePrice,0) servicePrice,tblproject_info.id,project_id,description as info_name" +
            '    " ,info_details,alarm,alarm_date,alarm_date_hj,user_id ,entry_date ,entry_date_h,image" +
            '" FROM tblproject_info  left join tbllock_up on info_name=tbllock_up.id where  project_id = " + printItemId)
            '           dt_workers = DBManager.Getdatatable("SELECT tblworker.name_ar as worker,tblworker.id as id,tblproject_workers.deal_date_hj as deal_date_hj,tblproject_workers.start_date_hj as start_date_hj,tblproject_workers.end_date_hj as end_date_hj,tblproject_workers.total_budget as total_budget from tblproject_workers join tblprojects on tblprojects.id=tblproject_workers.project_id join tblworker on tblworker.id=tblproject_workers.worker_id  where tblprojects.id = " + printItemId)
            '           dt_archive = DBManager.Getdatatable("SELECT * from tblProject_archive  where Project_id = " + printItemId)

            If dt_cases.Rows.Count <> 0 Then
                Dim str As String = PublicFunctions.ConvertDataTabletoString(dt_cases)
                Names.Add(str)
            Else
                Names.Add("0")
            End If
            If dt_person_owner.Rows.Count <> 0 Then
                Dim str2 As String = PublicFunctions.ConvertDataTabletoString(dt_person_owner)
                Names.Add(str2)
            Else
                Names.Add("0")
            End If
            If dt_person_against.Rows.Count <> 0 Then
                Dim str3 As String = PublicFunctions.ConvertDataTabletoString(dt_person_against)
                Names.Add(str3)
            Else
                Names.Add("0")
            End If

            If dt_children.Rows.Count <> 0 Then
                Dim str4 As String = PublicFunctions.ConvertDataTabletoString(dt_children)
                Names.Add(str4)
            Else
                Names.Add("0")
            End If

            If dt_receiving_delivery_basic.Rows.Count <> 0 Then
                Dim str5 As String = PublicFunctions.ConvertDataTabletoString(dt_receiving_delivery_basic)
                Names.Add(str5)
            Else
                Names.Add("0")
            End If

            If dt_receiving_delivery_details.Rows.Count <> 0 Then
                Dim str6 As String = PublicFunctions.ConvertDataTabletoString(dt_receiving_delivery_details)
                Names.Add(str6)
            Else
                Names.Add("0")
            End If
            If dt_conciliation.Rows.Count <> 0 Then
                Dim str7 As String = PublicFunctions.ConvertDataTabletoString(dt_conciliation)
                Names.Add(str7)
            Else
                Names.Add("0")
            End If

            If dt_correspondences.Rows.Count <> 0 Then
                Dim str8 As String = PublicFunctions.ConvertDataTabletoString(dt_correspondences)
                Names.Add(str8)
            Else
                Names.Add("0")
            End If

            If dt_sessions.Rows.Count <> 0 Then
                Dim str9 As String = PublicFunctions.ConvertDataTabletoString(dt_sessions)
                Names.Add(str9)
            Else
                Names.Add("0")
            End If
            If dt_expenses.Rows.Count <> 0 Then
                Dim str10 As String = PublicFunctions.ConvertDataTabletoString(dt_expenses)
                Names.Add(str10)
            Else
                Names.Add("0")
            End If
            If dt_expenses_details.Rows.Count <> 0 Then
                Dim str11 As String = PublicFunctions.ConvertDataTabletoString(dt_expenses_details)
                Names.Add(str11)
            Else
                Names.Add("0")
            End If




            'If dt_finance.Rows.Count <> 0 Then
            '    Dim str1 As String = PublicFunctions.ConvertDataTabletoString(dt_finance)
            '    Names.Add(str1)
            'Else
            '    Names.Add("0")
            'End If
            'If dt_prposal.Rows.Count <> 0 Then
            '    Dim str2 As String = PublicFunctions.ConvertDataTabletoString(dt_prposal)
            '    Names.Add(str2)
            'Else
            '    Names.Add("0")
            'End If
            'If dt_info.Rows.Count <> 0 Then
            '    Dim str3 As String = PublicFunctions.ConvertDataTabletoString(dt_info)
            '    Names.Add(str3)
            'Else
            '    Names.Add("0")
            'End If
            'If dt_workers.Rows.Count <> 0 Then
            '    Dim str4 As String = PublicFunctions.ConvertDataTabletoString(dt_workers)
            '    Names.Add(str4)
            'Else
            '    Names.Add("0")
            'End If

            'If dt_archive.Rows.Count <> 0 Then
            '    Dim str5 As String = PublicFunctions.ConvertDataTabletoString(dt_archive)
            '    Names.Add(str5)
            'Else
            '    Names.Add("0")
            'End If
            'Dim index = InStr(str1, ",")
            'Dim substr = str1.Substring(index - 1, str1.Length - index - 1)
            'Dim Result = str.Insert(str.Length - 2, substr)


            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
    End Function

#End Region
#Region "show_cases_details"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function show_cases_details(ByVal printItemId As String, ByVal type As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt_details As New DataTable
            Dim dt_details_childrens As New DataTable
            Dim dt_details_persons As New DataTable
            If type = 1 Then
                dt_details = DBManager.Getdatatable("select * from ash_case_childrens  where id=" + printItemId.ToString)
            ElseIf type = 2 Then
                dt_details = DBManager.Getdatatable("SELECT  * from ash_case_receiving_delivery_details where id= " + printItemId.ToString)
                dt_details_childrens = DBManager.Getdatatable("SELECT  * from ash_case_children_receiving_details where details_id= " + printItemId.ToString)
            ElseIf type = 3 Then
                dt_details = DBManager.Getdatatable("SELECT * from ash_case_correspondences  where   id=" + printItemId.ToString)
            ElseIf type = 4 Then
                dt_details = DBManager.Getdatatable("SELECT  * from ash_case_sessions where id= " + printItemId.ToString)
                dt_details_childrens = DBManager.Getdatatable("SELECT  * from ash_session_children where session_id= " + printItemId.ToString)
                dt_details_persons = DBManager.Getdatatable("SELECT  * from ash_session_companions where session_id= " + printItemId.ToString)
            ElseIf type = 5 Then
                dt_details = DBManager.Getdatatable("SELECT  * from ash_case_expenses_details where id= " + printItemId.ToString)
            End If
            If dt_details.Rows.Count <> 0 Then
                Dim str As String = PublicFunctions.ConvertDataTabletoString(dt_details)

                Names.Add(str)
            Else
                Names.Add("0")
            End If

            If dt_details_childrens.Rows.Count <> 0 Then
                Dim str1 As String = PublicFunctions.ConvertDataTabletoString(dt_details_childrens)

                Names.Add(str1)
            Else
                Names.Add("0")
            End If
            If dt_details_persons.Rows.Count <> 0 Then
                Dim str2 As String = PublicFunctions.ConvertDataTabletoString(dt_details_persons)

                Names.Add(str2)
            Else
                Names.Add("0")
            End If
            'Dim index = InStr(str1, ",")
            'Dim substr = str1.Substring(index - 1, str1.Length - index - 1)
            'Dim Result = str.Insert(str.Length - 2, substr)


            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "delete_details"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function delete_details(ByVal printItemId As String, ByVal project_id As String, ByVal type As String) As Boolean

        Dim Names As New List(Of String)(10)
        Try
            Dim dt_details As New DataTable
            Dim dt_finance As New DataTable
            Dim dt_prposal As New DataTable
            Dim result = 0
            If type = 1 Then
                If DBManager.ExcuteQueryTransaction("delete from tblproject_workers  where tblproject_workers.worker_id=" + printItemId.ToString + " and  tblproject_workers.project_id=" + project_id.ToString, _sqlconn, _sqltrans) = -1 Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If
                If DBManager.ExcuteQueryTransaction("delete from tblproject_worker_finance  where project_worker_id=" + printItemId.ToString + " and  project_id=" + project_id.ToString, _sqlconn, _sqltrans) = -1 Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If
            ElseIf type = 2 Then
                If DBManager.ExcuteQueryTransaction("delete from tblproject_proposals  where  project_id = " + project_id + " and id=" + printItemId.ToString, _sqlconn, _sqltrans) = -1 Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If
            ElseIf type = 3 Then
                If DBManager.ExcuteQueryTransaction("delete from tblproject_info  where  project_id = " + project_id + " and id=" + printItemId.ToString, _sqlconn, _sqltrans) = -1 Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If
            ElseIf type = 4 Then
                If DBManager.ExcuteQueryTransaction("delete from tblProject_archive  where  project_id = " + project_id + " and id=" + printItemId.ToString, _sqlconn, _sqltrans) = -1 Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try
    End Function

#End Region

#Region "get_date_delivery"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_date_delivery(ByVal case_id As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt_details As New DataTable
            Dim dt_finance As New DataTable
            Dim dt_prposal As New DataTable
            dt_details = DBManager.Getdatatable("select TOP 1 [date_m] as date from ash_case_receiving_delivery_details where receiving_delivery_done=0 and case_id=" + case_id.ToString + " ORDER BY ID DESC")
            If dt_details.Rows.Count <> 0 Then
                Dim str = PublicFunctions.ConvertDataTabletoString(dt_details)
                Names.Add(str)
            Else
                Names.Add("0")
            End If
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "get_date_expenses"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_date_expenses(ByVal case_id As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt_details As New DataTable
            Dim dt_finance As New DataTable
            Dim dt_prposal As New DataTable
            dt_details = DBManager.Getdatatable("select TOP 1 [date_m] as date from ash_case_expenses_details where done=0 and case_id=" + case_id.ToString + " ORDER BY ID DESC")
            If dt_details.Rows.Count <> 0 Then
                Dim str = PublicFunctions.ConvertDataTabletoString(dt_details)
                Names.Add(str)
            Else
                Names.Add("0")
            End If
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "get_persons"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_persons(ByVal id As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            Dim dt_details As New DataTable
            dt_details = DBManager.Getdatatable("select ash_case_persons.id as person_id,ash_case_persons.name as person_name from ash_case_persons join ash_cases on ash_case_persons.case_id=ash_cases.id where ash_cases.id=" + id)
            If dt_details.Rows.Count <> 0 Then
                Dim str = PublicFunctions.ConvertDataTabletoString(dt_details)
                Names.Add(str)
            Else
                Names.Add("0")
            End If
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Return Names.ToArray
        End Try
    End Function

#End Region

#Region "get_date_children"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_date_children(ByVal case_id As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            Dim dt_children As New DataTable
            dt_children = DBManager.Getdatatable("SELECT * from ash_case_childrens where  ash_case_childrens.case_id = " + case_id)
            If dt_children.Rows.Count <> 0 Then
                Dim str = PublicFunctions.ConvertDataTabletoString(dt_children)
                Names.Add(str)
            Else
                Names.Add("0")
            End If
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Return Names.ToArray
        End Try
    End Function

#End Region




End Class