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
Public Class Archived_Diploma_CoursesCls
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




    '#Region "update archive"
    '    ''' <summary>
    '    ''' </summary>
    '    <WebMethod()>
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function Archive_Diplome(ByVal diplomeId As String) As String()
    '        Dim Names As New List(Of String)(10)
    '        Try
    '            If DBManager.ExcuteQuery("update acd_diplomes set archive=1 where id=" + diplomeId) <> -1 Then
    '                If Not PublicFunctions.TransUsers_logs("4209", "acd_diplomes", "ارشيف", _sqlconn, _sqltrans) Then
    '                    success = False
    '                Else
    '                    success = True
    '                End If
    '                success = True
    '            Else
    '                success = False

    '            End If
    '            If success Then
    '                Names.Add("1")

    '            Else
    '                Names.Add("2")

    '            End If
    '            Return Names.ToArray
    '        Catch
    '            Names.Add("2")

    '            Return Names.ToArray
    '        End Try
    '    End Function
    '#End Region


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
            dt = DBManager.Getdatatable("select  acd_diplome_subjects.code , acd_diplome_subjects.id , acd_diplome_subjects.subject_id , acd_diplome_subjects.semster_id , acd_diplome_subjects.created_at_hj , acd_diplome_subjects.subject_goal , acd_diplome_subjects.trainer_id,tblUsers.full_name,tblUsers.User_Image as 'trainerImage' ,tbllock_up.Description as 'subjectName' , lockup2.Description  as 'semster' from acd_diplome_subjects join tblUsers on acd_diplome_subjects.trainer_id=tblUsers.id join tbllock_up on acd_diplome_subjects.subject_id=tbllock_up.id  join tbllock_up as lockup2 on acd_diplome_subjects.semster_id=lockup2.id where acd_diplome_subjects.archive=1 and  diplome_id=" + diploma_id + condation)

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

            dt = DBManager.Getdatatable("select acd_courses_students.student_id , acd_courses_students.notes, tblImages.Image_path as 'registerFiles',tblUsers.full_name as 'studentName',tblUsers.User_Image as 'studImag' from acd_courses_students join tblUsers on tblUsers.id=acd_courses_students.student_id join tblImages on tblImages.Source_id=acd_courses_students.student_id and tblImages.related_id= acd_courses_students.course_id where acd_courses_students.type=2 and  acd_courses_students.approved=0 and acd_courses_students.course_id=" + diplomeID)

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
    Public Function SaveStudent(ByVal diplomeID As String, ByVal students_action As Object) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dt As DataTable
            dt = DBManager.Getdatatable("delete from acd_courses_students where type=2 And course_id=" + diplomeID)

            Dim id = ""
            Dim success2 As Boolean = True
            For Each item As Object In students_action
                Dim dictBasicDataJson As New Dictionary(Of String, Object)
                dictBasicDataJson.Add("course_id", diplomeID)

                dictBasicDataJson.Add("student_id", item("id"))
                dictBasicDataJson.Add("notes", item("std_notes"))

                dictBasicDataJson.Add("type", "2")
                dictBasicDataJson.Add("approved", item("action_Student"))

                If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_courses_students", id, _sqlconn, _sqltrans) Then
                    If Not PublicFunctions.TransUsers_logs("3193", "acd_courses_students", "ادخال", _sqlconn, _sqltrans) Then
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