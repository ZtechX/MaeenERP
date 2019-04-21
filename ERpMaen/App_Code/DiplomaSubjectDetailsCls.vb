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
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<System.Web.Script.Services.ScriptService()>
Public Class DiplomaSubjectDetailsCls
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
    Public Function Save(ByVal Id As String, ByVal subjectid As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try
            'CourseId
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable
            'Dim dt_academy As DataTable
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())


            dictBasicDataJson.Add("course_id", subjectid)
            dictBasicDataJson.Add("type", "2")
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_lectures", Id, _sqlconn, _sqltrans) Then
                If Not PublicFunctions.TransUsers_logs("4203", "acd_lectures", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then

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
    Public Function Savenote(ByVal id As String, ByVal diplomeID As String, ByVal code As String, ByVal StudentId As String, ByVal subjectid As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            'dictBasicDataJson.Add("userID", (Context.Request.Cookies("UserInfo").ToString()))

            dictBasicDataJson.Add("course_id", subjectid)
            dictBasicDataJson.Add("student_id", StudentId)
            dictBasicDataJson.Add("type", "2")


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_students_notes", id, _sqlconn, _sqltrans) Then


                Dim dtstudents As DataTable

                dtstudents = DBManager.Getdatatable("select student_id from acd_courses_students where   type=2 and approved=1 and deleted=0 and course_id=" + diplomeID)

                For Each item As DataRow In dtstudents.Rows
                    Dim dictNotification As New Dictionary(Of String, Object)

                    dictNotification.Add("RefCode", subjectid)
                    dictNotification.Add("NotTitle", " ملاحظة جديدة ")
                    dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                    dictNotification.Add("AssignedTo", item("student_id"))
                    dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                    dictNotification.Add("Remarks", "ملاحظة")
                    dictNotification.Add("FormUrl", "Acadmies_module/DiplomaSubjectDetails?code=" + code)
                    If PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then

                        success = True
                    Else
                        success = False
                    End If

                Next

                If Not PublicFunctions.TransUsers_logs("4203", "acd_students_notes", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then

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
    Public Function addHalls(ByVal id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson


            Dim comp_id = LoginInfo.GetComp_id()
            'Dim dt_subject As DataTable

            'dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            'Dim comp_id = dt_user.Rows(0).Item("comp_id").ToString



            dictBasicDataJson.Add("type", "HallNum")
            dictBasicDataJson.Add("comp_id", comp_id)

            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tbllock_up", id, _sqlconn, _sqltrans) Then
                If Not PublicFunctions.TransUsers_logs("4203", "tbllock_up", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then
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
    Public Function sendMesg(ByVal id As String, ByVal code As String, ByVal CourseId As String, ByVal date_m As String, ByVal date_hj As String, ByVal msgTime As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson


            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            dictBasicDataJson.Add("course_id", CourseId)
            dictBasicDataJson.Add("date_hj", date_hj)
            dictBasicDataJson.Add("date_m", date_m)
            dictBasicDataJson.Add("type", "2")
            dictBasicDataJson.Add("message_time", msgTime)
            dictBasicDataJson.Add("user_form", LoginInfo.GetUser__Id())

            '//////////////////////////


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_contact_trainer", id, _sqlconn, _sqltrans) Then

                Dim assignTo = ""
                Dim dt As DataTable

                dt = DBManager.Getdatatable("Select trainer_id from acd_diplome_subjects where id=" + CourseId)
                If dt.Rows.Count <> 0 Then
                    assignTo = dt.Rows(0)(0).ToString
                End If


                Dim username = ""
                Dim dtuser As DataTable

                dtuser = DBManager.Getdatatable("Select full_name from tblUsers where id=" + LoginInfo.GetUser__Id())
                If dtuser.Rows.Count <> 0 Then
                    username = dtuser.Rows(0)(0).ToString
                End If

                Dim dictBasicDataJson2 As New Dictionary(Of String, Object)

                dictBasicDataJson2.Add("RefCode", CourseId)
                dictBasicDataJson2.Add("NotTitle", "رسالة من" + "  " + username)
                dictBasicDataJson2.Add("Date", dictBasicDataJson("date_m"))
                dictBasicDataJson2.Add("AssignedTo", assignTo)
                dictBasicDataJson2.Add("Remarks", dictBasicDataJson("message"))
                dictBasicDataJson2.Add("CreatedBy", LoginInfo.GetUser__Id())
                dictBasicDataJson2.Add("FormUrl", "Acadmies_module/DiplomaSubjectDetails?code=" + code)


                If PublicFunctions.TransUpdateInsert(dictBasicDataJson2, "tblNotifications", "", _sqlconn, _sqltrans) Then

                    If Not PublicFunctions.TransUsers_logs("4203", "acd_contact_trainer", "ادخال", _sqlconn, _sqltrans) Then
                        success = False
                    Else
                        success = True
                    End If
                End If
                success = True

            Else
                success = False
            End If
            If success Then

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
    Public Function sendMesgtoAdmin(ByVal id As String, ByVal CourseId As String, ByVal date_m As String, ByVal date_hj As String, ByVal msgTime As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try



            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson


            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            dictBasicDataJson.Add("course_id", CourseId)
            dictBasicDataJson.Add("date_hj", date_hj)
            dictBasicDataJson.Add("date_m", date_m)
            dictBasicDataJson.Add("type", "2")
            dictBasicDataJson.Add("message_time", msgTime)
            dictBasicDataJson.Add("user_form", LoginInfo.GetUser__Id())



            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_contact_admin", id, _sqlconn, _sqltrans) Then
                If Not PublicFunctions.TransUsers_logs("4203", "acd_contact_admin", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True

                Dim assignTo = ""
                Dim dt As DataTable

                Dim comp_id = LoginInfo.GetComp_id()

                dt = DBManager.Getdatatable("select id from tblUsers where User_Type=7 and comp_id=" + comp_id)
                If dt.Rows.Count <> 0 Then
                    assignTo = dt.Rows(0)(0).ToString
                End If
                If assignTo <> "" Then

                    Dim dictBasicDataJson2 As New Dictionary(Of String, Object)

                    dictBasicDataJson2.Add("RefCode", PublicFunctions.GetIdentity(_sqlconn, _sqltrans))
                    dictBasicDataJson2.Add("NotTitle", "تواصل مع مدير الاكاديمية")
                    dictBasicDataJson2.Add("Date", dictBasicDataJson("date_m"))
                    dictBasicDataJson2.Add("AssignedTo", assignTo)
                    dictBasicDataJson2.Add("Remarks", "رسائل من المستخدمين لمدير الاكاديمية")
                    dictBasicDataJson2.Add("CreatedBy", LoginInfo.GetUser__Id())
                    dictBasicDataJson2.Add("FormUrl", "Acadmies_module/DiplomaSubjectDetails?subject_id=" + CourseId)
                    If Not PublicFunctions.TransUpdateInsert(dictBasicDataJson2, "tblNotifications", "", _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return False
                    End If
                End If
                _sqltrans.Commit()
                _sqlconn.Close()
                Return True

            Else
                success = False
            End If
            If success Then

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
    Public Function SaveDegree(ByVal id As String, ByVal StudentId As String, ByVal subjectId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable


            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            DBManager.ExcuteQuery("DELETE FROM acd_student_degrees where type=2 and student_id=" + StudentId + " and course_id=" + subjectId)

            dictBasicDataJson.Add("course_id", subjectId)
                dictBasicDataJson.Add("student_id", StudentId)
                dictBasicDataJson.Add("type", "2")

                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_student_degrees", id, _sqlconn, _sqltrans) Then

                    If Not PublicFunctions.TransUsers_logs("4203", "acd_student_degrees", "ادخال", _sqlconn, _sqltrans) Then
                        success = False
                    Else
                        success = True
                    End If
                    success = True
                Else
                    success = False

                End If
                If success Then
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
    Public Function SaveEvalution(ByVal id As String, ByVal subjectid As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson

            Dim user_id = LoginInfo.GetUser__Id()

            DBManager.ExcuteQuery("DELETE FROM acd_evaluation where type=2 and course_id=" + subjectid + " and user_id=" + user_id)

            dictBasicDataJson.Add("user_id", user_id)

            dictBasicDataJson.Add("course_id", subjectid)

            dictBasicDataJson.Add("type", 2)


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_evaluation", id, _sqlconn, _sqltrans) Then

                If Not PublicFunctions.TransUsers_logs("4203", "acd_evaluation", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then
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
    Public Function SaveStudent(ByVal subjectId As String, ByVal students As Integer()) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dt_user As DataTable

            Dim id = ""
            Dim success_st As Boolean = True
            For Each item As String In students
                Dim dictBasicDataJson As New Dictionary(Of String, Object)
                dictBasicDataJson.Add("course_id", subjectId)
                dictBasicDataJson.Add("type", 2)
                dictBasicDataJson.Add("approved", 1)
                dictBasicDataJson.Add("student_id", item)

                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_courses_students", id, _sqlconn, _sqltrans) Then
                    If Not PublicFunctions.TransUsers_logs("4203", "acd_courses_students", "ادخال", _sqlconn, _sqltrans) Then
                        success = False
                    Else
                        success = True
                    End If
                    success = True
                Else
                    success = False

                End If
                If success Then
                    success_st = True
                Else
                    success_st = False
                End If
            Next
            If success_st Then

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
    ''' Save  Type addStudentdegree
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveStudentAbsence(ByVal subjectID As String, ByVal CourseLecId As String, ByVal students As Integer()) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dt_user As DataTable
            dt_user = DBManager.Getdatatable("delete from acd_absence where type=2 and lecture_id=" + CourseLecId)

            Dim id = ""
            Dim success2 As Boolean = True
            For Each item As String In students
                Dim dictBasicDataJson As New Dictionary(Of String, Object)
                dictBasicDataJson.Add("course_id", subjectID)
                dictBasicDataJson.Add("lecture_id", CourseLecId)
                dictBasicDataJson.Add("absence", 1)
                dictBasicDataJson.Add("student_id", item)
                dictBasicDataJson.Add("type", "2")

                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_absence", id, _sqlconn, _sqltrans) Then
                    If Not PublicFunctions.TransUsers_logs("4203", "acd_absence", "ادخال", _sqlconn, _sqltrans) Then
                        success = False
                    Else
                        success = True
                    End If
                    success = True
                Else
                    success = False

                End If
                If success Then

                    success2 = True
                Else
                    success2 = False
                End If
            Next
            If success2 Then

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


#Region "get_exams table"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_ExamsTable(ByVal course_id As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            If LoginInfo.getUserType = 8 Then
                dt = DBManager.Getdatatable("select acd_exams.id ,acd_exams.title, isNull(acd_homework_delivery.degree,'0') as 'degree',acd_exams.details,acd_exams.image from acd_exams left join acd_homework_delivery on acd_homework_delivery.homework_id=acd_exams.id where acd_exams.course_id=" + course_id)

            End If


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

#Region "addStudentdegree"
    ''' <summary>
    ''' Save  Type  درجات الطلاب 
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function addStudentdegree(ByVal subjectID As String, ByVal studentDegrees As Object) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dt As DataTable
            dt = DBManager.Getdatatable("delete from acd_student_degrees where type=2 and course_id=" + subjectID)

            Dim id = ""
            Dim success2 As Boolean = True
            For Each item As Object In studentDegrees
                Dim dictBasicDataJson As New Dictionary(Of String, Object)
                dictBasicDataJson.Add("course_id", subjectID)


                dictBasicDataJson.Add("student_id", item("id"))
                dictBasicDataJson.Add("final_degree", item("fdegree"))
                dictBasicDataJson.Add("activity_degree", item("Acdegree"))
                dictBasicDataJson.Add("type", "2")

                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_student_degrees", id, _sqlconn, _sqltrans) Then
                    If Not PublicFunctions.TransUsers_logs("4203", "acd_student_degrees", "ادخال", _sqlconn, _sqltrans) Then
                        success = False
                    Else
                        success = True
                    End If
                    success = True
                Else
                    success = False

                End If
                If success Then

                    success2 = True
                Else
                    success2 = False
                End If
            Next
            If success2 Then

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
    Public Function SaveExam(ByVal id As String, ByVal diplomeId As String, ByVal code As String, ByVal lectureID As String, ByVal subjectId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())



            dictBasicDataJson.Add("course_id", subjectId)
            dictBasicDataJson.Add("lecture_id", lectureID)

            dictBasicDataJson.Add("type", "2")


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_exams", id, _sqlconn, _sqltrans) Then

                Dim dtstudents As DataTable

                dtstudents = DBManager.Getdatatable("select student_id from acd_courses_students where   type=2 And approved=1 And deleted=0 And course_id=" + diplomeId)

                For Each item As DataRow In dtstudents.Rows
                    Dim dictNotification As New Dictionary(Of String, Object)

                    dictNotification.Add("RefCode", subjectId)
                    dictNotification.Add("NotTitle", " اختبار جديد ")
                    dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                    dictNotification.Add("AssignedTo", item("student_id"))
                    dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                    dictNotification.Add("Remarks", dictBasicDataJson("title"))
                    dictNotification.Add("FormUrl", "Acadmies_module/DiplomaSubjectDetails?code=" + code)
                    If PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then

                        success = True
                    Else
                        success = False
                    End If

                Next



                If Not PublicFunctions.TransUsers_logs("4203", "acd_exams", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then

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



#Region "savehomeworkDegree"
    ''' <summary>
    ''' Save  Type  درجات الواجبات للطلاب  
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function savehomeworkDegree(ByVal homeworkID As String, ByVal subjectId As String, ByVal studentDegrees As Object) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction


            Dim success2 As Boolean = True
            For Each item As Object In studentDegrees

                If DBManager.ExcuteQuery("UPDATE  acd_homework_delivery  set  degree=" + item("hwmstuddegree").ToString + "where type=2 and dvtype=1 and student_id=" + item("id").ToString + " And course_id=" + subjectId + " And homework_id=" + homeworkID) <> -1 Then


                    If Not PublicFunctions.TransUsers_logs("4203", "acd_homework_delivery", "تعديل", _sqlconn, _sqltrans) Then
                        success = False
                    Else
                        success = True
                    End If
                    success = True
                Else
                    success = False

                End If
                If success Then

                    success2 = True
                Else
                    success2 = False
                End If
            Next
            If success2 Then

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

#Region "savehomeworkDegree"
    ''' <summary>
    ''' Save  Type  درجات الاختبارات للطلاب  
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function saveExamDegree(ByVal ExamId As String, ByVal code As String, ByVal subjectId As String, ByVal studentDegrees As Object) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction


            Dim success2 As Boolean = True
            For Each item As Object In studentDegrees

                If DBManager.ExcuteQuery("UPDATE  acd_homework_delivery  set  degree=" + item("hwmstuddegree").ToString + "where  dvtype=2 and type=2 and student_id=" + item("id").ToString + " And course_id=" + subjectId + " And homework_id=" + ExamId) <> -1 Then


                    If Not PublicFunctions.TransUsers_logs("4203", "acd_homework_delivery", "تعديل", _sqlconn, _sqltrans) Then
                        success = False
                    Else
                        success = True
                    End If
                    success = True
                Else
                    success = False

                End If
                If success Then

                    success2 = True
                Else
                    success2 = False
                End If
            Next
            If success2 Then

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
    Public Function get_StudentsHomeworkAnswers(ByVal homeworkId As String, ByVal subjectId As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable


            dt = DBManager.Getdatatable("select acd_homework_delivery.homework_id, acd_homework_delivery.student_id,acd_homework_delivery.image as 'homeworkanswer' ,tblUsers.full_name as 'studentname',COALESCE (acd_homework_delivery.degree,0) as 'HMWDegree' from acd_homework_delivery join tblUsers on tblUsers.id=acd_homework_delivery.student_id where  acd_homework_delivery.dvtype=1 and acd_homework_delivery.type=2  and acd_homework_delivery.dvtype=1 and acd_homework_delivery.homework_id=" + homeworkId + " and course_id=" + subjectId)

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
    Public Function get_StudentsExamsAnswers(ByVal examid As String, ByVal subjectId As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable


            dt = DBManager.Getdatatable("select acd_homework_delivery.homework_id, acd_homework_delivery.student_id,acd_homework_delivery.image as 'homeworkanswer' ,tblUsers.full_name as 'studentname',COALESCE (acd_homework_delivery.degree,0) as 'HMWDegree' from acd_homework_delivery join tblUsers on tblUsers.id=acd_homework_delivery.student_id where  acd_homework_delivery.dvtype=2 and acd_homework_delivery.type=2 and acd_homework_delivery.homework_id=" + examid + " and course_id=" + subjectId)

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

#Region "get_homework table"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_homeworkTable(ByVal subjectId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            If LoginInfo.getUserType = 8 Then
                dt = DBManager.Getdatatable("select acd_homeworks.id,acd_homeworks.title,isNull( acd_homework_delivery.degree,'0') as 'degree' ,acd_homeworks.details,acd_homeworks.image from acd_homeworks left join acd_homework_delivery on acd_homework_delivery.homework_id=acd_homeworks.id and acd_homework_delivery.type=2 and acd_homework_delivery.dvtype=1  where  acd_homeworks.type=2 and acd_homeworks.course_id=" + subjectId)

            End If


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

#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveHomeWork(ByVal id As String, ByVal diplomeiD As String, ByVal lectureId As String, ByVal subjectID As String, ByVal code As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())



            dictBasicDataJson.Add("course_id", subjectID)
            dictBasicDataJson.Add("lecture_id", lectureId)
            dictBasicDataJson.Add("type", "2")


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_homeworks", id, _sqlconn, _sqltrans) Then

                Dim dtstudents As DataTable

                dtstudents = DBManager.Getdatatable("select student_id from acd_courses_students where type=2 And approved=1 And deleted=0 And course_id=" + diplomeiD)

                For Each item As DataRow In dtstudents.Rows
                    Dim dictNotification As New Dictionary(Of String, Object)

                    dictNotification.Add("RefCode", subjectID)
                    dictNotification.Add("NotTitle", "واجب جديد ")
                    dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                    dictNotification.Add("AssignedTo", item("student_id"))
                    dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                    dictNotification.Add("Remarks", dictBasicDataJson("title"))
                    dictNotification.Add("FormUrl", "Acadmies_module/DiplomaSubjectDetails?code=" + code)
                    If PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then

                        success = True
                    Else
                        success = False
                    End If

                Next


                If Not PublicFunctions.TransUsers_logs("4203", "acd_homeworks", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then

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
    Public Function SaveCondition(ByVal id As String, ByVal subjectId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            dictBasicDataJson.Add("course_id", subjectId)
            dictBasicDataJson.Add("type", "2")
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_course_conditions", id, _sqlconn, _sqltrans) Then
                If Not PublicFunctions.TransUsers_logs("4203", "acd_course_conditions", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then

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
    Public Function SaveActivit(ByVal id As String, ByVal diplomeID As String, ByVal code As String, ByVal subjectid As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())



            dictBasicDataJson.Add("course_id", subjectid)
            dictBasicDataJson.Add("type", "2")


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_students_activity", id, _sqlconn, _sqltrans) Then


                Dim dtstudents As DataTable

                dtstudents = DBManager.Getdatatable("select student_id from acd_courses_students where   type=2 And approved=1 And deleted=0 And course_id=" + diplomeID)

                For Each item As DataRow In dtstudents.Rows
                    Dim dictNotification As New Dictionary(Of String, Object)

                    dictNotification.Add("RefCode", subjectid)
                    dictNotification.Add("NotTitle", "   نشاط جديد ")
                    dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                    dictNotification.Add("AssignedTo", item("student_id"))
                    dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                    dictNotification.Add("Remarks", "انشطة")
                    dictNotification.Add("FormUrl", "Acadmies_module/DiplomaSubjectDetails?code=" + code)
                    If PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then

                        success = True
                    Else
                        success = False
                    End If

                Next

                If Not PublicFunctions.TransUsers_logs("4203", "acd_students_activity", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then


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
    Public Function SaveCourse(ByVal Id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())



            'dictBasicDataJson.Add("course_id", CourseId)


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_diplome_subjects", Id, _sqlconn, _sqltrans) Then
                If Not PublicFunctions.TransUsers_logs("4203", "acd_diplome_subjects", "تعديل", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then

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
            dictBasicDataJson.Add("type", "2")


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_course_materials", id, _sqlconn, _sqltrans) Then
                If Not PublicFunctions.TransUsers_logs("4203", "acd_course_materials", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then

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

    'SaveComment get_CourseStudentsandDegree


#Region "Save comment"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveComment(ByVal id As String, ByVal CourseId As String, ByVal date_m As String, ByVal date_hj As String, ByVal time As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson

            Dim user_id = LoginInfo.GetUser__Id()


            dictBasicDataJson.Add("user_id", user_id)
            dictBasicDataJson.Add("date_m", date_m)
            dictBasicDataJson.Add("date_hj", date_hj)
            dictBasicDataJson.Add("type", "2")
            dictBasicDataJson.Add("time", time)

            dictBasicDataJson.Add("course_id", CourseId)
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_course_comments", id, _sqlconn, _sqltrans) Then
                If Not PublicFunctions.TransUsers_logs("4203", "acd_course_comments", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then

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


#Region "Save links"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function saveCourseLinks(ByVal id As String, ByVal subjectId As String, ByVal date_m As String, ByVal date_hj As String, ByVal time As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson

            Dim user_id = LoginInfo.GetUser__Id()


            dictBasicDataJson.Add("add_by", user_id)
            dictBasicDataJson.Add("date_m", date_m)
            dictBasicDataJson.Add("date_hj", date_hj)
            dictBasicDataJson.Add("type", "2")
            dictBasicDataJson.Add("course_id", subjectId)
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_links", id, _sqlconn, _sqltrans) Then
                If Not PublicFunctions.TransUsers_logs("4203", "acd_links", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then

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


#Region "get_links"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_courseLinks(ByVal CourseID As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select acd_links.add_by,tblUsers.full_name as 'username',tblUsers.User_Image as 'suerImage', acd_links.title,acd_links.date_hj , acd_links.URL , acd_links.notes from acd_links join tblUsers on acd_links.add_by=tblUsers.id where acd_links.type=2 and acd_links.course_id=" + CourseID)
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


#Region "get_comments"
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

            dt = DBManager.Getdatatable("select acd_course_comments.user_id,tblUsers.full_name as 'username',tblUsers.User_Image as 'suerImage', acd_course_comments.comment,acd_course_comments.date_hj , acd_course_comments.time from acd_course_comments join tblUsers on acd_course_comments.user_id=tblUsers.id where acd_course_comments.type=2 and acd_course_comments.course_id=" + CourseID)
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
    Public Function get_course(ByVal subject_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select acd_diplome_subjects.id, acd_diplome_subjects.lecture_time ,tbllock_up.Description as 'subjectName', tbllock_up2.Description as'semster',acd_diplome_subjects.subject_id,acd_diplome_subjects.semster_id,acd_diplome_subjects.diplome_id,acd_diplome_subjects.created_at_hj,acd_diplome_subjects.trainer_id,tblUsers.full_name as 'trainerName', tblUsers.User_Image as 'trainerImage',acd_diplome_subjects.subject_goal from acd_diplome_subjects join tbllock_up on acd_diplome_subjects.subject_id=tbllock_up.id  join tbllock_up as tbllock_up2 on acd_diplome_subjects.semster_id=tbllock_up2.id join tblUsers on acd_diplome_subjects.trainer_id=tblUsers.id where acd_diplome_subjects.id=" + subject_id)

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

#Region "get_courseDegree table"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_courseDegreesTable(ByVal course_id As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            If LoginInfo.getUserType = 8 Then
                dt = DBManager.Getdatatable("select final_degree ,activity_degree from acd_student_degrees where type=2 and course_id=" + course_id + " and student_id=" + LoginInfo.GetUser__Id())

            End If


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


#Region "update archive"
    ''' <summary>
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Archive_course(ByVal subjectId As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If DBManager.ExcuteQuery("update acd_diplome_subjects set archive=1 where id=" + subjectId) <> -1 Then
                If Not PublicFunctions.TransUsers_logs("4203", "acd_diplome_subjects", "ارشيف", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then
                Names.Add("1")

            Else
                Names.Add("2")

            End If
            Return Names.ToArray
        Catch
            Names.Add("2")
            Names.Add("لا يمكن الحذف!")
            Return Names.ToArray
        End Try
    End Function
#End Region

#Region "get lecture Code"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getlectureCode(ByVal subjectId As String) As String

        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select isNull(Max(lecture_code),0) from  acd_lectures where type=2 and course_id=" + subjectId)
            If dt.Rows.Count <> 0 Then
                Return dt.Rows(0)(0).ToString
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region


#Region "save HW answer"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function saveHWanswer(ByVal id As String, ByVal code As String, ByVal homeworkId As String, ByVal subjectId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            dictBasicDataJson.Add("homework_id", homeworkId)
            dictBasicDataJson.Add("course_id", subjectId)
            dictBasicDataJson.Add("type", "2")
            dictBasicDataJson.Add("dvtype", "1")
            dictBasicDataJson.Add("student_id", LoginInfo.GetUser__Id())



            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_homework_delivery", id, _sqlconn, _sqltrans) Then

                Dim trainer_id As DataTable

                trainer_id = DBManager.Getdatatable("select trainer_id from acd_diplome_subjects where id=" + subjectId)
                Dim hmwname As DataTable
                hmwname = DBManager.Getdatatable("select title from acd_homeworks where id=" + homeworkId + "And course_id=" + subjectId)
                Dim homeworktitle = hmwname.Rows(0)(0).ToString()
                Dim assighnedto = trainer_id.Rows(0)(0).ToString()
                Dim dictNotification As New Dictionary(Of String, Object)

                dictNotification.Add("RefCode", subjectId)
                dictNotification.Add("NotTitle", "   حل الواجب  ")
                dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                dictNotification.Add("AssignedTo", assighnedto)
                dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                dictNotification.Add("Remarks", homeworktitle)
                dictNotification.Add("FormUrl", "Acadmies_module/DiplomaSubjectDetails?code=" + code)
                If PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then

                    success = True
                Else
                    success = False
                End If

                If Not PublicFunctions.TransUsers_logs("4203", "acd_homework_delivery", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then

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


#Region "save exam answer"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function saveExamanswer(ByVal id As String, ByVal code As String, ByVal examId As String, ByVal subjectId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            dictBasicDataJson.Add("homework_id", examId)
            dictBasicDataJson.Add("course_id", subjectId)
            dictBasicDataJson.Add("type", "2")
            dictBasicDataJson.Add("dvtype", "2")
            dictBasicDataJson.Add("student_id", LoginInfo.GetUser__Id())



            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_homework_delivery", id, _sqlconn, _sqltrans) Then

                Dim trainer_id As DataTable

                trainer_id = DBManager.Getdatatable("select trainer_id from acd_diplome_subjects where id=" + subjectId)
                Dim hmwname As DataTable
                hmwname = DBManager.Getdatatable("select title from acd_exams where id=" + examId + "And course_id=" + subjectId)
                Dim homeworktitle = hmwname.Rows(0)(0).ToString()
                Dim assighnedto = trainer_id.Rows(0)(0).ToString()
                Dim dictNotification As New Dictionary(Of String, Object)

                dictNotification.Add("RefCode", subjectId)
                dictNotification.Add("NotTitle", "   حل الاختبار  ")
                dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                dictNotification.Add("AssignedTo", assighnedto)
                dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                dictNotification.Add("Remarks", homeworktitle)
                dictNotification.Add("FormUrl", "Acadmies_module/DiplomaSubjectDetails?code=" + code)
                If PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then

                    success = True
                Else
                    success = False
                End If



                If Not PublicFunctions.TransUsers_logs("4203", "acd_homework_delivery", "ادخال", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then

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
    Public Function get_srchstudentABS(ByVal name As String, ByVal lecture_id As String, ByVal diplomeId As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            Dim dt2 As New DataTable
            If name <> "" Then

                dt = DBManager.Getdatatable("select acd_courses_students.student_id , tblUsers.full_name as 'CourseStudents' from acd_courses_students join tblUsers on acd_courses_students.student_id=tblUsers.id where type=2 and acd_courses_students.course_id=" + diplomeId + " and tblUsers.full_name like '%" + name + "%'")
                dt2 = DBManager.Getdatatable("select student_id from acd_absence where  type=2 and lecture_id=" + lecture_id)
            Else
                dt = DBManager.Getdatatable("select acd_courses_students.student_id , tblUsers.full_name as 'CourseStudents' from acd_courses_students join tblUsers on acd_courses_students.student_id=tblUsers.id where type=2 and acd_courses_students.course_id=" + diplomeId)
                dt2 = DBManager.Getdatatable("select student_id from acd_absence where  type=2 and lecture_id=" + lecture_id)
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
    Public Function get_CourseStudents(ByVal lecture_id As String, ByVal subjectid As String, ByVal diplomeid As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            Dim dt2 As New DataTable

            dt = DBManager.Getdatatable("select acd_courses_students.student_id , tblUsers.full_name as 'CourseStudents' from acd_courses_students join tblUsers on acd_courses_students.student_id=tblUsers.id where  acd_courses_students.approved=1 and type=2 and acd_courses_students.course_id=" + diplomeid)
            dt2 = DBManager.Getdatatable("select student_id from acd_absence where type=2 and lecture_id=" + lecture_id)
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
    Public Function get_student(ByVal name As String, ByVal diplomeId As String) As String()
        Dim dt_user As DataTable

        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
        Dim comp_id = dt_user.Rows(0).Item("comp_id").ToString
        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            Dim condition = ""
            If name <> "" Then
                condition = "  and tblUsers.full_name LIKE '%" + name + "%'"
                dt = DBManager.Getdatatable("select acd_courses_students.student_id ,tblUsers.full_name as 'studentName',tblUsers.User_Image as 'studImag' from acd_courses_students join tblUsers on tblUsers.id=acd_courses_students.student_id where  acd_courses_students.type =2 and  acd_courses_students.approved=1 and acd_courses_students.course_id=" + diplomeId + condition)
            Else

                dt = DBManager.Getdatatable("select acd_courses_students.student_id ,tblUsers.full_name as 'studentName',tblUsers.User_Image as 'studImag' from acd_courses_students join tblUsers on tblUsers.id=acd_courses_students.student_id where  acd_courses_students.type =2 and  acd_courses_students.approved=1 and acd_courses_students.course_id=" + diplomeId)
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




    'student course And degrees 


#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_pub_StudentsDegree(ByVal diplomeID As String, ByVal subjectId As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable


            dt = DBManager.Getdatatable("select acd_courses_students.student_id ,tblUsers.full_name as 'studentname',COALESCE( stdg.final_degree ,0) as 'final',COALESCE (stdg.activity_degree,0) as 'activityDegree' from acd_courses_students join tblUsers on tblUsers.id=acd_courses_students.student_id left  JOIN acd_student_degrees stdg on  acd_courses_students.student_id=stdg.student_id AND stdg.type=2 and stdg.course_id=" + subjectId + " where  acd_courses_students.approved=1 and acd_courses_students.deleted=0 and acd_courses_students.type=2  and acd_courses_students.course_id=" + diplomeID)

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


    'search student In student degrees

#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function search_pub_Students(ByVal studentName As String, ByVal diplomeid As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            Dim condition = ""

            If studentName <> "" Then
                condition = " where acd_courses_students.type=2 and tblUsers.full_name LIKE '%" + studentName + "%' And acd_courses_students.course_id=" + diplomeid


                dt = DBManager.Getdatatable("select acd_courses_students.student_id ,tblUsers.full_name as 'studentname',COALESCE( stdg.final_degree ,0) as 'final',COALESCE (stdg.activity_degree,0) as 'activityDegree' from acd_courses_students join tblUsers on tblUsers.id=acd_courses_students.student_id left  JOIN acd_student_degrees stdg on  acd_courses_students.student_id=stdg.student_id AND stdg.type=2 " + condition)
            Else
                dt = DBManager.Getdatatable("select acd_courses_students.student_id ,tblUsers.full_name as 'studentname',COALESCE( stdg.final_degree ,0) as 'final',COALESCE (stdg.activity_degree,0) as 'activityDegree' from acd_courses_students join tblUsers on tblUsers.id=acd_courses_students.student_id left  JOIN acd_student_degrees stdg on  acd_courses_students.student_id=stdg.student_id AND stdg.type=2 where acd_courses_students.type=2  and acd_courses_students.course_id=" + diplomeid)
            End If

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


    '#Region "get_data"
    '    ''' <summary>
    '    ''' Save  Type
    '    ''' </summary>
    '    <WebMethod(True)>
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function get_LectureTable(ByVal subjectId As String) As String()
    '        Dim dt_user As DataTable
    '        dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

    '        Dim Names As New List(Of String)(10)
    '        Try
    '            Dim dt As New DataTable

    '            dt = DBManager.Getdatatable("Select acd_lectures.id , acd_lectures.lecture_code, acd_lectures.date_hj, acd_lectures.start_time, acd_lectures.hall_id , tbllock_up.Description As 'hallNum' from acd_lectures join tbllock_up on tbllock_up.id=acd_lectures.hall_id  where acd_lectures.type=2 and acd_lectures.course_id=" + subjectId)

    '            If dt IsNot Nothing Then
    '                If dt.Rows.Count <> 0 Then
    '                    Dim Str = PublicFunctions.ConvertDataTabletoString(dt)
    '                    Names.Add("1")
    '                    Names.Add(Str)
    '                    Return Names.ToArray
    '                End If

    '            End If
    '            Names.Add("0")
    '            Names.Add(" No Results were Found!")
    '            Return Names.ToArray
    '        Catch ex As Exception
    '            Names.Add("0")
    '            Names.Add(" No Results were Found!")
    '            Return Names.ToArray
    '        End Try
    '        Names.Add("0")
    '        Names.Add(" No Results were Found!")
    '        Return Names.ToArray
    '    End Function

    '    'get_SyudentTable
    '    'get_Exams

    '#End Region


#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_LectureTable(ByVal subjectId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            If LoginInfo.getUserType = 8 Then
                dt = DBManager.Getdatatable("Select acd_lectures.id , acd_lectures.lecture_code, acd_lectures.date_hj, acd_lectures.start_time, acd_lectures.hall_id , tbllock_up.Description as 'hallNum',acd_absence.absence from acd_lectures join tbllock_up on tbllock_up.id=acd_lectures.hall_id left join acd_absence on acd_lectures.id =acd_absence.lecture_id and acd_absence.student_id=" + LoginInfo.GetUser__Id().ToString + "  where acd_lectures.course_id=" + subjectId)

            Else

                dt = DBManager.Getdatatable("Select acd_lectures.id , acd_lectures.lecture_code, acd_lectures.date_hj, acd_lectures.start_time, acd_lectures.hall_id , tbllock_up.Description as 'hallNum' from acd_lectures join tbllock_up on tbllock_up.id=acd_lectures.hall_id  where acd_lectures.course_id=" + subjectId)

            End If


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
    Public Function get_StudentTable(ByVal diplome_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select acd_courses_students.student_id ,tblUsers.full_name as 'studentName',tblUsers.User_Image as 'studImag' from acd_courses_students join tblUsers on tblUsers.id=acd_courses_students.student_id where  acd_courses_students.type=2 and acd_courses_students.checked=1 and acd_courses_students.deleted=0 and  acd_courses_students.approved=1 and acd_courses_students.course_id=" + diplome_id)

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
    Public Function get_Homework(ByVal subjectId As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("Select  acd_homeworks.id , acd_homeworks.image ,acd_homeworks.title , acd_homeworks.date_hj , acd_homeworks.details from acd_homeworks where type=2 and acd_homeworks.course_id=" + subjectId)

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
    Public Function get_StudentList(ByVal subject_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select id , User_Image,full_name as 'StudentName' from tblUsers where User_Type=8 AND comp_id=" + LoginInfo.GetComp_id() + "and id not in(select student_id from acd_courses_students where course_id=" + subject_id + ")")

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
    Public Function get_Exams(ByVal subjectid As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("Select  acd_exams.id , acd_exams.image, acd_exams.title , acd_exams.date_hj , acd_exams.details from acd_exams where type=2 and acd_exams.course_id=" + subjectid)

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
    Public Function get_StudentNote(ByVal subjectid As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select comment from acd_students_notes where type=2 and course_id=" + subjectid)

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

            dt = DBManager.Getdatatable("select activity from acd_students_activity where type=2 and course_id=" + course_id)

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

            dt = DBManager.Getdatatable("select id, notes,image from acd_course_materials where type=2 and acd_course_materials.course_id=" + course_id)

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
    Public Function get_Condition(ByVal subjectId As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select id, condition,image from acd_course_conditions where type=2 and acd_course_conditions.course_id=" + subjectId)

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
            Dim str As String = PublicFunctions.GetDataForUpdate("acd_diplome_subjects", editItemId)
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

#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function EditstudDegree(ByVal subjectid As String, ByVal studID As String) As String()


        Dim editItemId = ""
        Dim dt As DataTable

        dt = DBManager.Getdatatable("select id from acd_student_degrees where type=2 and  course_id=" + subjectid + "and student_id=" + studID)

        editItemId = dt.Rows(0)(0).ToString
        Dim Names As New List(Of String)(10)
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("acd_student_degrees", editItemId)
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
    Public Function EdituserEvalute(ByVal subjectid As String) As String()


        Dim editItemId = ""
        Dim dt As DataTable

        dt = DBManager.Getdatatable("select id from acd_evaluation where type=2 and course_id=" + subjectid + " and user_id=" + LoginInfo.GetUser__Id())

        editItemId = dt.Rows(0)(0).ToString
        Dim Names As New List(Of String)(10)
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("acd_evaluation", editItemId)
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
                If Not PublicFunctions.TransUsers_logs("3193", "acd_lectures", "حذف", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then
                Names.Add("1")
                Names.Add("تم الحذف بنجاح!")

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
    Public Function Delete_Student(ByVal studentId As String, ByVal diplomeId As String) As String()
        Dim Names As New List(Of String)(10)
        Try

            If DBManager.ExcuteQuery("UPDATE  acd_courses_students  set  deleted=1 where student_id=" + studentId + " and course_id=" + diplomeId) <> -1 Then
                If Not PublicFunctions.TransUsers_logs("4203", "acd_courses_students", "حذف", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then
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
    Public Function Delete_File(ByVal deleteItem As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "acd_course_materials") Then
                If Not PublicFunctions.TransUsers_logs("3193", "acd_course_materials", "حذف", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then
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
    Public Function Delete_condition(ByVal deleteItem As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "acd_course_conditions") Then
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
    Public Function Delete_Homework(ByVal deleteItem As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "acd_homeworks") Then
                If Not PublicFunctions.TransUsers_logs("4203", "acd_homeworks", "حذف", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then
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
                If Not PublicFunctions.TransUsers_logs("3193", "acd_exams", "حذف", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then
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
    Public Function Delete_course(ByVal deleteItem As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "acd_diplome_subjects") Then
                If Not PublicFunctions.TransUsers_logs("4203", "acd_diplome_subjects", "حذف", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then
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
