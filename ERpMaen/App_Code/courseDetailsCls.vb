﻿#Region "Import"
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
Public Class courseDetailsCls
    Inherits System.Web.Services.WebService

#Region "Global_Variables"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
    Dim success As Boolean = False
#End Region

#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Save(ByVal Id As String, ByVal CourseId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try
            'CourseId
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable
            'Dim dt_academy As DataTable
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())


            dictBasicDataJson.Add("course_id", CourseId)
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_lectures", Id, _sqlconn, _sqltrans) Then

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


#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Savenote(ByVal id As String, ByVal StudentId As String, ByVal CourseId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            'dictBasicDataJson.Add("userID", (Context.Request.Cookies("UserInfo").ToString()))

            dictBasicDataJson.Add("course_id", CourseId)
            dictBasicDataJson.Add("student_id", StudentId)


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_students_notes", id, _sqlconn, _sqltrans) Then

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


    '////activity//////////

    '


#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveDegree(ByVal id As String, ByVal StudentId As String, ByVal CourseId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            'dictBasicDataJson.Add("userID", (Context.Request.Cookies("UserInfo").ToString()))

            dictBasicDataJson.Add("course_id", CourseId)
            dictBasicDataJson.Add("student_id", StudentId)


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_student_degrees", id, _sqlconn, _sqltrans) Then

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

    'SaveExam
    'SaveHomeWork
    'SaveLecure
    'SaveStudent
    'SaveEvalution
    '

#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveEvalution(ByVal id As String, ByVal CourseId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            'Dim dt_user As DataTable

            'dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            Dim user_id = LoginInfo.GetUser__Id()


            dictBasicDataJson.Add("user_id", user_id)

            dictBasicDataJson.Add("course_id", CourseId)

            dictBasicDataJson.Add("type", 1)

            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_evaluation", id, _sqlconn, _sqltrans) Then

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
#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveStudent(ByVal CourseId As String, ByVal students As Integer()) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dt_user As DataTable

            Dim id = ""
            Dim success As Boolean = True
            For Each item As String In students
                Dim dictBasicDataJson As New Dictionary(Of String, Object)
                dictBasicDataJson.Add("course_id", CourseId)
                dictBasicDataJson.Add("type", 1)
                dictBasicDataJson.Add("approved", 1)
                dictBasicDataJson.Add("student_id", item)

                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_courses_students", id, _sqlconn, _sqltrans) Then
                    success = True
                Else
                    success = False
                End If
            Next
            If success Then

                _sqltrans.Commit()
                _sqlconn.Close()
                Return True
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If
            ' dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            'dictBasicDataJson.Add("userID", (Context.Request.Cookies("UserInfo").ToString()))



        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try
    End Function
#End Region

#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveStudentAbsence(ByVal CourseId As String, ByVal CourseLecId As String, ByVal students As Integer()) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dt_user As DataTable
            dt_user = DBManager.Getdatatable("delete from acd_absence where lecture_id=" + CourseLecId)

            Dim id = ""
            Dim success As Boolean = True
            For Each item As String In students
                Dim dictBasicDataJson As New Dictionary(Of String, Object)
                dictBasicDataJson.Add("course_id", CourseId)
                dictBasicDataJson.Add("lecture_id", CourseLecId)
                dictBasicDataJson.Add("absence", 1)
                dictBasicDataJson.Add("student_id", item)

                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_absence", id, _sqlconn, _sqltrans) Then
                    success = True
                Else
                    success = False
                End If
            Next
            If success Then

                _sqltrans.Commit()
                _sqlconn.Close()
                Return True
            Else
                _sqltrans.Rollback()
                _sqlconn.Close()
                Return False
            End If
            ' dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            'dictBasicDataJson.Add("userID", (Context.Request.Cookies("UserInfo").ToString()))



        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try
    End Function
#End Region

#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveExam(ByVal id As String, ByVal CourseId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())



            dictBasicDataJson.Add("course_id", CourseId)


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_exams", id, _sqlconn, _sqltrans) Then

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

    '#Region "Save"
    '    ''' <summary>
    '    ''' Save  Type
    '    ''' </summary>
    '    <WebMethod(True)>
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function SaveLecture(ByVal id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
    '        Try

    '            _sqlconn.Open()
    '            _sqltrans = _sqlconn.BeginTransaction
    '            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
    '            Dim dt_user As DataTable

    '            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

    '            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_lectures", id, _sqlconn, _sqltrans) Then

    '                _sqltrans.Commit()
    '                _sqlconn.Close()
    '                Return True
    '            Else
    '                _sqltrans.Rollback()
    '                _sqlconn.Close()
    '                Return False
    '            End If

    '        Catch ex As Exception
    '            _sqltrans.Rollback()
    '            _sqlconn.Close()
    '            Return False
    '        End Try
    '    End Function
    '#End Region


#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveHomeWork(ByVal id As String, ByVal CourseId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())



            dictBasicDataJson.Add("course_id", CourseId)


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_homeworks", id, _sqlconn, _sqltrans) Then

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



#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveCondition(ByVal id As String, ByVal CourseId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            'dictBasicDataJson.Add("userID", (Context.Request.Cookies("UserInfo").ToString()))

            dictBasicDataJson.Add("course_id", CourseId)


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_course_conditions", id, _sqlconn, _sqltrans) Then

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

#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveActivit(ByVal id As String, ByVal CourseId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            'dictBasicDataJson.Add("userID", (Context.Request.Cookies("UserInfo").ToString()))

            dictBasicDataJson.Add("course_id", CourseId)


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_students_activity", id, _sqlconn, _sqltrans) Then

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
#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveCourse(ByVal CourseId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())



            'dictBasicDataJson.Add("course_id", CourseId)


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_courses", CourseId, _sqlconn, _sqltrans) Then

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


#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Savefiles(ByVal id As String, ByVal lectureId As String, ByVal CourseId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            dictBasicDataJson.Add("course_id", CourseId)
            dictBasicDataJson.Add("lecture_id", lectureId)


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_course_materials", id, _sqlconn, _sqltrans) Then

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

    'SaveComment


#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveComment(ByVal id As String, ByVal CourseId As String, ByVal date_m As String, ByVal date_hj As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            'Dim time_m = h + "" + t
            'Dim date_m = PublicFunctions.ConvertDatetoNumber(Date.Now.ToShortDateString.ToString)

            'Dim comp_id = LoginInfo.GetComp_id()
            Dim user_id = LoginInfo.GetUser__Id()


            dictBasicDataJson.Add("user_id", user_id)
            dictBasicDataJson.Add("date_m", date_m)
            dictBasicDataJson.Add("date_hj", date_hj)

            dictBasicDataJson.Add("course_id", CourseId)
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_course_comments", id, _sqlconn, _sqltrans) Then

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


#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_courseComments(ByVal CourseID As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select acd_course_comments.user_id,tblUsers.full_name as 'username' , acd_course_comments.comment,acd_course_comments.date_hj from acd_course_comments join tblUsers on acd_course_comments.user_id=tblUsers.id where acd_course_comments.course_id=" + CourseID)
            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
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

#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_course(ByVal course_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select  acd_courses.id , acd_courses.name , acd_courses.description ,acd_courses.start_dt_hj ,acd_courses.category_id, tbllock_up.Description as 'course_category', acd_courses.status, lockup2.Description as 'StatusCourse', acd_courses.trainer_id ,tblUsers.full_name as'trainer_name' ,acd_courses.price , DATEDIFF(Day,CONVERT(DATE,acd_courses.start_dt_hj, 103),CONVERT(DATE,acd_courses.end_dt_hj, 103)) as 'duration'  from acd_courses join tblUsers on acd_courses.trainer_id=tblUsers.id join tbllock_up on tbllock_up.id=acd_courses.category_id join tbllock_up as lockup2 on lockup2.id=acd_courses.status  where acd_courses.id=" + course_id)

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
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

    '

#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_srchstudentABS(ByVal name As String, ByVal lecture_id As String, ByVal CourseID As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            Dim dt2 As New DataTable
            If name <> "" Then

                dt = DBManager.Getdatatable("select acd_courses_students.student_id , tblUsers.full_name as 'CourseStudents' from acd_courses_students join tblUsers on acd_courses_students.student_id=tblUsers.id where acd_courses_students.course_id=" + CourseID + " and tblUsers.full_name like '%" + name + "%'")
                dt2 = DBManager.Getdatatable("select student_id from acd_absence where lecture_id=" + lecture_id)
            Else
                dt = DBManager.Getdatatable("select acd_courses_students.student_id , tblUsers.full_name as 'CourseStudents' from acd_courses_students join tblUsers on acd_courses_students.student_id=tblUsers.id where acd_courses_students.course_id=" + CourseID)
                dt2 = DBManager.Getdatatable("select student_id from acd_absence where lecture_id=" + lecture_id)
            End If
            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Dim str2 = PublicFunctions.ConvertDataTabletoString(dt2)
                    Names.Add("1")

                    Names.Add(Str)
                    Names.Add(str2)
                    Return Names.ToArray
                Else
                    Names.Add("0")
                    Names.Add(" No Results were Found!")
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

#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_CourseStudents(ByVal lecture_id As String, ByVal CourseID As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            Dim dt2 As New DataTable

            dt = DBManager.Getdatatable("select acd_courses_students.student_id , tblUsers.full_name as 'CourseStudents' from acd_courses_students join tblUsers on acd_courses_students.student_id=tblUsers.id where acd_courses_students.course_id=" + CourseID)
            dt2 = DBManager.Getdatatable("select student_id from acd_absence where lecture_id=" + lecture_id)
            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Dim str2 = PublicFunctions.ConvertDataTabletoString(dt2)
                    Names.Add("1")

                    Names.Add(Str)
                    Names.Add(str2)
                    Return Names.ToArray
                End If

            End If
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
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

    'get_AbsenceTable 
#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_student(ByVal name As String, ByVal courseID As String) As String()
        Dim dt_user As DataTable

        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
        Dim comp_id = dt_user.Rows(0).Item("comp_id").ToString
        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            Dim condition = ""
            If name <> "" Then
                condition = " where tblUsers.full_name LIKE '%" + name + "%' And course_id=" + courseID
                dt = DBManager.Getdatatable("Select  acd_courses_students.student_id,tblUsers.full_name As 'studentName' from acd_courses_students join tblUsers on acd_courses_students.student_id=tblUsers.id" + condition)
            Else

                dt = DBManager.Getdatatable("Select  acd_courses_students.student_id,tblUsers.full_name As 'studentName' from acd_courses_students join tblUsers on acd_courses_students.student_id=tblUsers.id where course_id=" + courseID)
            End If
            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray

                Else

                    Names.Add("0")
                    Names.Add(" No Results were Found!")
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









#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_LectureTable(ByVal course_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("Select acd_lectures.id , acd_lectures.lecture_code, acd_lectures.date_hj, acd_lectures.start_time, acd_lectures.hall_id , tbllock_up.Description as 'hallNum' from acd_lectures join tbllock_up on tbllock_up.id=acd_lectures.hall_id  where acd_lectures.course_id=" + course_id)

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        Catch ex As Exception
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
        End Try
        Names.Add("0")
        Names.Add(" No Results were Found!")
        Return Names.ToArray
    End Function

    'get_SyudentTable
    'get_Exams

#End Region


#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_StudentTable(ByVal course_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("Select acd_courses_students.student_id, tblUsers.full_name as 'studentName' from acd_courses_students join tblUsers on acd_courses_students.student_id=tblUsers.id where acd_courses_students.course_id=" + course_id)

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
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
#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_Homework(ByVal course_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("Select  acd_homeworks.id , acd_homeworks.image ,acd_homeworks.title , acd_homeworks.date_hj , acd_homeworks.details from acd_homeworks where acd_homeworks.course_id=" + course_id)

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
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
#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_StudentList(ByVal course_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select id , full_name as 'StudentName' from tblUsers where User_Type=8 AND comp_id=4 and id not in(select student_id from acd_courses_students where course_id=" + course_id + ")")

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
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

#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_Exams(ByVal course_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("Select  acd_exams.id , acd_exams.image, acd_exams.title , acd_exams.date_hj , acd_exams.details from acd_exams where acd_exams.course_id=" + course_id)

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
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



#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_StudentNote(ByVal course_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select comment from acd_students_notes where course_id=" + course_id)

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
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

#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_StudentActivity(ByVal course_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select activity from acd_students_activity where course_id=" + course_id)

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
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
    '


#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_CourseFiles(ByVal course_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select notes,image from acd_course_materials where acd_course_materials.course_id=" + course_id)

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
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

#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_Condition(ByVal course_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select condition,image from acd_course_conditions where acd_course_conditions.course_id=" + course_id)

            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
                    Names.Add("1")
                    Names.Add(Str)
                    Return Names.ToArray
                End If

            End If
            Names.Add("0")
            Names.Add(" No Results were Found!")
            Return Names.ToArray
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



    '/////////////////////////////////
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
            Dim str As String = PublicFunctions.GetDataForUpdate("acd_courses", editItemId)
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

#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function EditLec(ByVal editItemId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("acd_lectures", editItemId)
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

    'EditAbsence
    'Delete_Homework


#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function EditAbsence(ByVal editItemId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("acd_absence", editItemId)
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

#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function EditExam(ByVal editItemId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("acd_exams", editItemId)
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


#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function EditHmw(ByVal editItemId As String) As String()


        Dim Names As New List(Of String)(10)
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("acd_homeworks", editItemId)
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
    Public Function Delete_Lecture(ByVal deleteItem As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "acd_lectures") Then
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
#Region "Delete"
    ''' <summary>
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Delete_Student(ByVal studentId As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("DELETE FROM acd_courses_students where student_id=" + studentId)
            If dt IsNot Nothing Then
                If dt.Rows.Count <> 0 Then
                    Names.Add("1")
                    Names.Add("تم الحذف بنجاح!")
                Else
                    Names.Add("2")
                    Names.Add("لا يمكن الحذف!")
                End If
                Return Names.ToArray
            End If

        Catch
            Names.Add("2")
            Names.Add("لا يمكن الحذف!")
            Return Names.ToArray
        End Try
    End Function
#End Region

#Region "Delete"
    ''' <summary>
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Delete_Homework(ByVal deleteItem As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "acd_homeworks") Then
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
#Region "Delete"
    ''' <summary>
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Delete_Exam(ByVal deleteItem As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "acd_exams") Then
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