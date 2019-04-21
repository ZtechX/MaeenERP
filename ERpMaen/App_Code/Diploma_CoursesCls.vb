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
Public Class Diploma_CoursesCls
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
    Public Function Save(ByVal id As String, ByVal diploma_id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable
            Dim dt_tr As DataTable
            'Dim dt_subject As DataTable

            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            Dim comp_id = dt_user.Rows(0).Item("comp_id").ToString

            dt_tr = DBManager.Getdatatable("select * from acd_training_centers where comp_id=" + comp_id)
            Dim tr_id = ""
            If dt_tr.Rows.Count <> 0 Then
                tr_id = dt_tr.Rows(0).Item("id").ToString
            End If

            'dt_subject = DBManager.Getdatatable("select * from tblLock_up where type='subj'")
            'Dim subject_id = ""
            'If dt_subject.Rows.Count <> 0 Then
            '    subject_id = dt_subject.Rows(0).Item("id").ToString
            'End If

            Dim rnd As New Random
            Dim code = "CRS" & rnd.Next(10000000, 99999999).ToString
            dictBasicDataJson.Add("code", code)

            dictBasicDataJson.Add("status", "60")
            dictBasicDataJson.Add("training_center_id", tr_id)
            dictBasicDataJson.Add("diplome_id", diploma_id)

            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_diplome_subjects", id, _sqlconn, _sqltrans) Then
                If Not PublicFunctions.TransUsers_logs("3182", "acd_diplome_subjects", "ادخال", _sqlconn, _sqltrans) Then
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



#Region "Save financial"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Savefinanc(ByVal id As String, ByVal diplomeId As String, ByVal fin_date_m As String, ByVal fin_date_hj As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())



            dictBasicDataJson.Add("course_id", diplomeId)
            dictBasicDataJson.Add("student_id", LoginInfo.GetUser__Id())
            dictBasicDataJson.Add("date_m", fin_date_m)
            dictBasicDataJson.Add("date_hj", fin_date_hj)
            dictBasicDataJson.Add("type", "2")
            If LoginInfo.getUserType = 8 Then

                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_payments", id, _sqlconn, _sqltrans) Then
                    If Not PublicFunctions.TransUsers_logs("4209", "acd_payments", "ادخال", _sqlconn, _sqltrans) Then
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
    Public Function SaveDiplome(ByVal diplomeId As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())



            'dictBasicDataJson.Add("course_id", CourseId)


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_diplomes", diplomeId, _sqlconn, _sqltrans) Then
                If Not PublicFunctions.TransUsers_logs("4209", "acd_courses", "تعديل", _sqlconn, _sqltrans) Then
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
    '

#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function addsubject(ByVal id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson


            Dim comp_id = LoginInfo.GetComp_id()
            'Dim dt_subject As DataTable

            'dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            'Dim comp_id = dt_user.Rows(0).Item("comp_id").ToString



            dictBasicDataJson.Add("type", "subj")
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


#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_StudentFnance(ByVal diplomeID As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select amount , approved from acd_payments where type=2 and course_id=" + diplomeID + "and student_id=" + LoginInfo.GetUser__Id())

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
    Public Function checkSemesterDate(ByVal semester_id As String, ByVal diplomeId As String, ByVal currentDate As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            Dim dtsemsubj As DataTable

            dtsemsubj = DBManager.Getdatatable("select subject_id from  acd_diplome_subjects where semster_id=" + semester_id + " and diplome_id=" + diplomeId + "and archive=0")
            If dtsemsubj.Rows.Count <> 0 Then

                Dim dtEndDate As DataTable

                dtEndDate = DBManager.Getdatatable("select end_date_m from acd_semester where id=" + semester_id + "and comp_id=" + LoginInfo.GetComp_id())
                Dim end_date = dtEndDate.Rows(0)(0).ToString()
                Dim current = PublicFunctions.ConvertDatetoNumber(currentDate)
                If end_date > current Then
                    Names.Add("1")
                Else
                    Names.Add("2")

                End If
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



#Region "get_StudentFinanceAdmin"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_StudentFinanceAdmin(ByVal diplomeId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select acd_payments.id ,acd_payments.approved ,acd_payments.amount,acd_payments.image,acd_payments.student_id ,tblUsers.full_name as 'name' from acd_payments join tblUsers on tblUsers.id=acd_payments.student_id  where type=2 and course_id=" + diplomeId)

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



#Region "approv_finance"
    ''' <summary>
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function approv_finance(ByVal studentId As String, ByVal diplomeid As String, ByVal code As String, ByVal payId As String) As String()
        Dim Names As New List(Of String)(10)
        Try

            If DBManager.ExcuteQuery("UPDATE  acd_payments  set  approved=1 where type=2 and id=" + payId + " and student_id=" + studentId + " and course_id=" + diplomeid) <> -1 Then


                Dim dictNotification As New Dictionary(Of String, Object)

                dictNotification.Add("RefCode", diplomeid)
                dictNotification.Add("NotTitle", "  تاكيد المبلغ ")
                dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                dictNotification.Add("AssignedTo", studentId)
                dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                dictNotification.Add("Remarks", "تاكيد المبلغ المرسل")
                dictNotification.Add("FormUrl", "Acadmies_module/DiplomaCourses?code=" + code)
                If PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then

                    success = True
                Else
                    success = False
                End If

                If Not PublicFunctions.TransUsers_logs("4209", "acd_payments", "حذف", _sqlconn, _sqltrans) Then
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
                Names.Add("تم الحفظ بنجاح!")
            Else
                Names.Add("2")
                Names.Add("لم يتم التاكيد")

            End If
            Return Names.ToArray
        Catch
            Names.Add("2")
            Names.Add("لا يمكن الحذف!")
            Return Names.ToArray
        End Try
    End Function
#End Region


#Region "refuse_finance"
    ''' <summary>
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function refuse_finance(ByVal studentId As String, ByVal diplomeId As String, ByVal code As String, ByVal payId As String) As String()
        Dim Names As New List(Of String)(10)
        Try

            If DBManager.ExcuteQuery("UPDATE  acd_payments  set  approved=2 where type=2 and  id=" + payId + " and student_id=" + studentId + " and course_id=" + diplomeId) <> -1 Then


                Dim dictNotification As New Dictionary(Of String, Object)

                dictNotification.Add("RefCode", diplomeId)
                dictNotification.Add("NotTitle", " رفض المبلغ ")
                dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                dictNotification.Add("AssignedTo", studentId)
                dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                dictNotification.Add("Remarks", "  رفض المبلغ المرسل ")
                dictNotification.Add("FormUrl", "Acadmies_module/DiplomaCourses?code=" + code)
                If PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then

                    success = True
                Else
                    success = False
                End If

                If Not PublicFunctions.TransUsers_logs("4209", "acd_payments", "حذف", _sqlconn, _sqltrans) Then
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
                Names.Add("تم الحفظ بنجاح!")
            Else
                Names.Add("2")
                Names.Add("لم يتم التاكيد")

            End If
            Return Names.ToArray
        Catch
            Names.Add("2")
            Names.Add("لا يمكن التاكيد!")
            Return Names.ToArray
        End Try
    End Function
#End Region


#Region "get_diplome_Degree table"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_diplomeDegree(ByVal diplomeId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            If LoginInfo.getUserType = 8 Then
                dt = DBManager.Getdatatable("select  acd_diplome_subjects.final_exam_degrees , acd_diplome_subjects.activity_degrees ,acd_diplome_subjects.id, acd_student_degrees.final_degree,  acd_student_degrees.activity_degree,acd_diplome_subjects.subject_id ,tbllock_up.Description as 'subjectName'  from  acd_diplome_subjects join tbllock_up on tbllock_up.id =acd_diplome_subjects.subject_id join acd_student_degrees on acd_student_degrees.course_id=acd_diplome_subjects.id and student_id=" + LoginInfo.GetUser__Id() + " and acd_student_degrees.type=2 where diplome_id=" + diplomeId)

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
    Public Function Archive_Semester(ByVal semesterID As String, ByVal diplomeId As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If DBManager.ExcuteQuery("update acd_diplome_subjects set archive=1 where diplome_id=" + diplomeId + " and semster_id=" + semesterID) <> -1 Then
                If Not PublicFunctions.TransUsers_logs("4209", "acd_diplome_subjects", "ارشيف", _sqlconn, _sqltrans) Then
                    success = False
                Else
                    success = True
                End If
                success = True
            Else
                success = False

            End If
            If success Then
                'Dim dtEndDate As DataTable

                'dtEndDate = DBManager.Getdatatable("select end_date_m from acd_semester where id=" + semesterID + "and comp_id=" + LoginInfo.GetComp_id())
                'Dim end_date = dtEndDate.Rows(0)(0).ToString()
                'Dim current = PublicFunctions.ConvertDatetoNumber(currentDate)
                'If end_date > current Then
                '    Names.Add("2")
                'Else
                '    Names.Add("1")

                'End If
                Names.Add("1")

            Else
                Names.Add("0")

            End If
            Return Names.ToArray
        Catch
            Names.Add("0")

            Return Names.ToArray
        End Try
    End Function
#End Region


#Region "Save"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveCondition(ByVal id As String, ByVal diplomeID As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable

            dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

            'dictBasicDataJson.Add("userID", (Context.Request.Cookies("UserInfo").ToString()))

            dictBasicDataJson.Add("course_id", diplomeID)

            dictBasicDataJson.Add("type", "2")


            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_course_conditions", id, _sqlconn, _sqltrans) Then
                If Not PublicFunctions.TransUsers_logs("4209", "acd_course_conditions", "ادخال", _sqlconn, _sqltrans) Then
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
    Public Function get_Courses(ByVal diploma_id As String, ByVal name As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
        Dim comp_id = dt_user.Rows(0).Item("comp_id").ToString
        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            Dim condation = ""
            If name <> "" Then
                condation = " where name LIKE '%" + name + "%'"
            End If
            dt = DBManager.Getdatatable("select acd_diplomes.name as 'dpname' , acd_diplome_subjects.code , acd_diplome_subjects.id , acd_diplome_subjects.subject_id , acd_diplome_subjects.semster_id , acd_diplome_subjects.created_at_hj , acd_diplome_subjects.subject_goal , acd_diplome_subjects.trainer_id,tblUsers.full_name,tblUsers.User_Image as 'trainerImage' ,tbllock_up.Description as 'subjectName' , acd_semester.name  as 'semster' from acd_diplome_subjects join tblUsers on acd_diplome_subjects.trainer_id=tblUsers.id join tbllock_up on acd_diplome_subjects.subject_id=tbllock_up.id  join acd_semester  on acd_diplome_subjects.semster_id=acd_semester.id join acd_diplomes on acd_diplomes.id=acd_diplome_subjects.diplome_id where acd_diplome_subjects.archive=0 and  diplome_id=" + diploma_id + condation)

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


#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_StudentList(ByVal diplomeID As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("Select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            'DBManager.ExcuteQuery("DELETE FROM acd_courses_students where approved=1 and course_id=" + course_id)

            dt = DBManager.Getdatatable("select acd_courses_students.student_id , acd_courses_students.notes, tblImages.Image_path as 'registerFiles',tblUsers.full_name as 'studentName',tblUsers.User_Image as 'studImag' from acd_courses_students join tblUsers on tblUsers.id=acd_courses_students.student_id join tblImages on tblImages.Source_id=acd_courses_students.student_id and tblImages.related_id= acd_courses_students.course_id where acd_courses_students.type=2 and  acd_courses_students.checked=0 and acd_courses_students.course_id=" + diplomeID)

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


#Region "addStudents_to_diplome"
    ''' <summary>
    ''' Save  Type قبول الطلاب فى الدبلوم
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SaveStudent(ByVal diplomeID As String, ByVal code As String, ByVal students_action As Object) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dt As DataTable
            dt = DBManager.Getdatatable("delete from acd_courses_students where checked=0  And type=2 And course_id=" + diplomeID)

            Dim id = ""
            Dim success2 As Boolean = True
            For Each item As Object In students_action
                Dim dictBasicDataJson As New Dictionary(Of String, Object)
                dictBasicDataJson.Add("course_id", diplomeID)

                dictBasicDataJson.Add("student_id", item("id"))
                dictBasicDataJson.Add("notes", item("std_notes"))

                dictBasicDataJson.Add("type", "2")
                dictBasicDataJson.Add("approved", item("action_Student"))
                dictBasicDataJson.Add("checked", True)

                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_courses_students", id, _sqlconn, _sqltrans) Then

                    Dim dictNotification As New Dictionary(Of String, Object)
                    Dim action = item("action_Student")
                    Dim remak = ""
                    If action = "0" Then
                        remak = "رفض"
                    Else
                        remak = "قبول"
                    End If
                    Dim frmURL = ""
                    If action = "1" Then
                        frmURL = "Acadmies_module/DiplomaCourses?code=" + code
                    Else
                        frmURL = "Acadmies_module/diplome_register?code=" + code
                    End If

                    dictNotification.Add("RefCode", diplomeID)
                    dictNotification.Add("NotTitle", "  نتيجة التقديم")
                    dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                    dictNotification.Add("AssignedTo", item("id"))
                    dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                    dictNotification.Add("Remarks", remak)
                    dictNotification.Add("FormUrl", frmURL)
                    If PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then

                        If Not PublicFunctions.TransUsers_logs("3193", "acd_courses_students", "ادخال", _sqlconn, _sqltrans) Then
                            success = False
                        Else
                            success = True
                        End If
                        success = True
                    Else
                        success = False
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

#Region "Edit"
    ''' <summary>
    ''' get  Type data from db when update
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Edit(ByVal editItemId As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim str As String = PublicFunctions.GetDataForUpdate("acd_diplomes", editItemId)
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
    Public Function Delete_deploma(ByVal deleteItem As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "acd_diplomes") Then
                DBManager.ExcuteQuery("DELETE FROM acd_diplome_subjects where diplome_id=" + deleteItem)
                If Not PublicFunctions.TransUsers_logs("4203", "acd_diplomes", "حذف", _sqlconn, _sqltrans) Then
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