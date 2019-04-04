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
Public Class courseDetails_archivedCls
    Inherits System.Web.Services.WebService

#Region "Global_Variables"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
    Dim success As Boolean = False
#End Region




#Region "get lecture Code"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getlectureCode() As String

        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select isNull(Max(lecture_code),0) as 'code' from  acd_lectures where type=1")
            If dt.Rows.Count <> 0 Then
                Return dt.Rows(0)(0).ToString
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region


#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_pub_StudentsDegree(ByVal courseId As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable


            dt = DBManager.Getdatatable("select acd_courses_students.student_id ,tblUsers.full_name as 'studentname',COALESCE( stdg.final_degree ,0) as 'final',COALESCE (stdg.activity_degree,0) as 'activityDegree' from acd_courses_students join tblUsers on tblUsers.id=acd_courses_students.student_id left  JOIN acd_student_degrees stdg on  acd_courses_students.student_id=stdg.student_id and acd_courses_students.course_id=stdg.course_id AND stdg.type=1 where acd_courses_students.approved=1 and acd_courses_students.type=1  and acd_courses_students.course_id=" + courseId)

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
    Public Function get_StudentsHomeworkAnswers(ByVal homeworkId As String, ByVal courseId As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable


            dt = DBManager.Getdatatable("select acd_homework_delivery.homework_id, acd_homework_delivery.student_id,acd_homework_delivery.image as 'homeworkanswer' ,tblUsers.full_name as 'studentname',COALESCE (acd_homework_delivery.degree,0) as 'HMWDegree' from acd_homework_delivery join tblUsers on tblUsers.id=acd_homework_delivery.student_id where  acd_homework_delivery.dvtype=1 and acd_homework_delivery.type=1 and acd_homework_delivery.homework_id=" + homeworkId + " and course_id=" + courseId)

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

            dt = DBManager.Getdatatable("select  acd_courses.id , acd_courses.name , acd_courses.description ,acd_courses.start_dt_hj ,acd_courses.category_id, tbllock_up.Description as 'course_category', acd_courses.status as 'StatusCourse', acd_courses.trainer_id ,tblUsers.full_name as'trainer_name',tblUsers.User_Image as 'trainerImage' ,acd_courses.price , DATEDIFF(Day,CONVERT(DATE,acd_courses.start_dt_hj, 103),CONVERT(DATE,acd_courses.end_dt_hj, 103)) as 'duration',lect_duration as 'lect_duration'  from acd_courses join tblUsers on acd_courses.trainer_id=tblUsers.id join tbllock_up on tbllock_up.id=acd_courses.category_id   where acd_courses.id=" + course_id)

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

            dt = DBManager.Getdatatable("select acd_courses_students.student_id , tblUsers.full_name as 'CourseStudents' from acd_courses_students join tblUsers on acd_courses_students.student_id=tblUsers.id where acd_courses_students.approved=1 and acd_courses_students.course_id=" + CourseID)
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
                dt = DBManager.Getdatatable("Select  acd_courses_students.student_id,tblUsers.full_name As 'studentName',tblUsers.User_Image as 'srImage' from acd_courses_students join tblUsers on acd_courses_students.student_id=tblUsers.id" + condition)
            Else

                dt = DBManager.Getdatatable("Select  acd_courses_students.student_id,tblUsers.full_name As 'studentName' ,tblUsers.User_Image as 'srImage' from acd_courses_students join tblUsers on acd_courses_students.student_id=tblUsers.id where course_id=" + courseID)
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
    Public Function search_pub_Students(ByVal studentName As String, ByVal course_Id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            Dim condition = ""

            If studentName <> "" Then
                condition = " where acd_courses_students.type=1 and tblUsers.full_name LIKE '%" + studentName + "%' And acd_courses_students.course_id=" + course_Id





                dt = DBManager.Getdatatable("select acd_courses_students.student_id ,tblUsers.full_name as 'studentname',COALESCE( stdg.final_degree ,0) as 'final',COALESCE (stdg.activity_degree,0) as 'activityDegree' from acd_courses_students join tblUsers on tblUsers.id=acd_courses_students.student_id left  JOIN acd_student_degrees stdg on  acd_courses_students.student_id=stdg.student_id AND stdg.type=1 " + condition)
            Else
                dt = DBManager.Getdatatable("select acd_courses_students.student_id ,tblUsers.full_name as 'studentname',COALESCE( stdg.final_degree ,0) as 'final',COALESCE (stdg.activity_degree,0) as 'activityDegree' from acd_courses_students join tblUsers on tblUsers.id=acd_courses_students.student_id left  JOIN acd_student_degrees stdg on  acd_courses_students.student_id=stdg.student_id AND stdg.type=1 where acd_courses_students.type=1  and acd_courses_students.course_id=" + course_Id)
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





#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_LectureTable(ByVal course_id As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            If LoginInfo.getUserType = 8 Then
                dt = DBManager.Getdatatable("Select acd_lectures.id , acd_lectures.lecture_code, acd_lectures.date_hj, acd_lectures.start_time, acd_lectures.hall_id , tbllock_up.Description as 'hallNum',acd_absence.absence from acd_lectures join tbllock_up on tbllock_up.id=acd_lectures.hall_id left join acd_absence on acd_lectures.id =acd_absence.lecture_id and acd_absence.student_id=" + LoginInfo.GetUser__Id().ToString + "  where acd_lectures.course_id=" + course_id)

            Else

                dt = DBManager.Getdatatable("Select acd_lectures.id , acd_lectures.lecture_code, acd_lectures.date_hj, acd_lectures.start_time, acd_lectures.hall_id , tbllock_up.Description as 'hallNum' from acd_lectures join tbllock_up on tbllock_up.id=acd_lectures.hall_id  where acd_lectures.course_id=" + course_id)

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

#Region "get_homework table"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_homeworkTable(ByVal course_id As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            If LoginInfo.getUserType = 8 Then
                dt = DBManager.Getdatatable("select acd_homeworks.id,acd_homeworks.title,isNull( acd_homework_delivery.degree,'0') as 'degree' ,acd_homeworks.details,acd_homeworks.image from acd_homeworks left join acd_homework_delivery on acd_homework_delivery.homework_id=acd_homeworks.id where acd_homeworks.course_id=" + course_id)

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
                dt = DBManager.Getdatatable("select acd_exams.id ,acd_exams.title, isNull(acd_homework_delivery.degree,'0') as 'degree',acd_exams.details,acd_exams.image from acd_exams left join acd_homework_delivery on acd_homework_delivery.course_id=acd_exams.course_id where acd_exams.course_id=" + course_id)

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
    Public Function get_StudentTable(ByVal course_id As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("Select acd_courses_students.student_id, tblUsers.full_name as 'studentName' ,tblUsers.User_Image as 'studImag' from acd_courses_students join tblUsers on acd_courses_students.student_id=tblUsers.id where  approved=1 and type=1 and acd_courses_students.course_id=" + course_id)

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
            'DBManager.ExcuteQuery("DELETE FROM acd_courses_students where approved=1 and course_id=" + course_id)

            dt = DBManager.Getdatatable("select acd_courses_students.student_id , acd_courses_students.notes, tblImages.Image_path as 'registerFiles',tblUsers.full_name as 'studentName',tblUsers.User_Image as 'studImag' from acd_courses_students join tblUsers on tblUsers.id=acd_courses_students.student_id join tblImages on tblImages.Source_id=acd_courses_students.student_id and tblImages.related_id= acd_courses_students.course_id where acd_courses_students.approved=0 and acd_courses_students.course_id=" + course_id)

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

            dt = DBManager.Getdatatable("select id, notes,image from acd_course_materials where acd_course_materials.course_id=" + course_id)

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

            dt = DBManager.Getdatatable("select id, condition,image from acd_course_conditions where acd_course_conditions.course_id=" + course_id)

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

#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function EditstudDegree(ByVal courseID As String, ByVal studID As String) As String()


        Dim editItemId = ""
        Dim dt As DataTable

        dt = DBManager.Getdatatable("select id from acd_student_degrees where type=1 and  course_id=" + courseID + "and student_id=" + studID)

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


#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_StudentsExamsAnswers(ByVal examid As String, ByVal courseId As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable


            dt = DBManager.Getdatatable("select acd_homework_delivery.homework_id, acd_homework_delivery.student_id,acd_homework_delivery.image as 'homeworkanswer' ,tblUsers.full_name as 'studentname',COALESCE (acd_homework_delivery.degree,0) as 'HMWDegree' from acd_homework_delivery join tblUsers on tblUsers.id=acd_homework_delivery.student_id where  acd_homework_delivery.dvtype=2 and acd_homework_delivery.type=1 and acd_homework_delivery.homework_id=" + examid + " and course_id=" + courseId)

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



#Region "update archive"
    ''' <summary>
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function unArchive_course(ByVal course_id As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If DBManager.ExcuteQuery("update acd_courses set archive=0 where id=" + course_id) <> -1 Then
                If Not PublicFunctions.TransUsers_logs("4208", "acd_courses", "ارشيف", _sqlconn, _sqltrans) Then
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

End Class
