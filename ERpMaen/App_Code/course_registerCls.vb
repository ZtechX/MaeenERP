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
Public Class course_registerCls
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
    Public Function SaveRegister(ByVal id As String, ByVal course_id As String, ByVal ImagePath As String, ByVal imageName As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson

            Dim dt1 As DataTable
            dt1 = DBManager.Getdatatable("delete from acd_courses_students where  type=1 and course_id=" + course_id + "and student_id=" + LoginInfo.GetUser__Id())
            dictBasicDataJson.Add("course_id", course_id)
            dictBasicDataJson.Add("type", "1")
            dictBasicDataJson.Add("approved", 0)
            dictBasicDataJson.Add("checked", 0)

            dictBasicDataJson.Add("student_id", LoginInfo.GetUser__Id())

            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_courses_students", id, _sqlconn, _sqltrans) Then
                Dim dictBasicDataJson1 As New Dictionary(Of String, Object)

                Dim dt2 As DataTable
                dt2 = DBManager.Getdatatable("delete from tblImages where Source='registeration' and Source_id=" + LoginInfo.GetUser__Id() + "and related_id=" + course_id)

                dictBasicDataJson1.Add("Source", "registeration")
                dictBasicDataJson1.Add("Source_id", LoginInfo.GetUser__Id())
                dictBasicDataJson1.Add("Image_path", ImagePath)
                dictBasicDataJson1.Add("Image_name", imageName)
                dictBasicDataJson1.Add("related_id", course_id)

                If Not PublicFunctions.TransUpdateInsert(dictBasicDataJson1, "tblImages", "", _sqlconn, _sqltrans) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return "False|لم يتم الحفظ"
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
    Public Function checkstudentregister(ByVal course_id As String) As String()

        Dim Names As New List(Of String)(10)
        Try

            Dim dt3 As New DataTable

            dt3 = DBManager.Getdatatable("select  isnull (approved,0) as 'appoved', isnull (deleted,0) as 'deleted' from acd_courses_students where approved=1 and  course_id =" + course_id + " and student_id=" + LoginInfo.GetUser__Id())

            If dt3 IsNot Nothing Then
                If dt3.Rows(0)(0).ToString = True And dt3.Rows(0)(1).ToString = True Then

                    Names.Add("3")


                End If
            Else

                Names.Add("0")
            End If

            Dim dt2 As New DataTable

            dt2 = DBManager.Getdatatable("select isnull (approved,0) as 'appoved',  isnull(checked,0) as'check' from acd_courses_students where  course_id =" + course_id + " and student_id=" + LoginInfo.GetUser__Id())

            If dt2 IsNot Nothing Then
                If dt2.Rows(0)(0).ToString = False And dt2.Rows(0)(1).ToString = False Then

                    Names.Add("1")


                End If
            Else

                Names.Add("0")
            End If
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select approved ,checked from acd_courses_students where  course_id =" + course_id + " and student_id=" + LoginInfo.GetUser__Id())

            If dt IsNot Nothing Then
                If dt.Rows(0)(0).ToString = False And dt.Rows(0)(1).ToString = True Then

                    Names.Add("2")


                End If
            Else

                Names.Add("0")
            End If



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
    Public Function get_courseFiles(ByVal CourseID As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select image, condition from acd_course_conditions where course_id=" + CourseID)
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




    '#Region "Edit"
    '    ''' <summary>
    '    ''' get  Type data from db when update
    '    ''' </summary>
    '    <WebMethod()>
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function Edit(ByVal editItemId As String) As String()

    '        Dim Names As New List(Of String)(10)
    '        Try
    '            Dim str As String = PublicFunctions.GetDataForUpdate("acd_courses", editItemId)
    '            Names.Add("1")
    '            Names.Add(str)
    '            Return Names.ToArray
    '        Catch ex As Exception
    '            Names.Add("0")
    '            Names.Add(" No Results were Found!")
    '            Return Names.ToArray
    '        End Try
    '    End Function

    '#End Region

    '#Region "Delete"
    '    ''' <summary>
    '    ''' </summary>
    '    <WebMethod()>
    '    <System.Web.Script.Services.ScriptMethod()>
    '    Public Function Delete(ByVal deleteItem As String) As String()
    '        Dim Names As New List(Of String)(10)
    '        Try
    '            If PublicFunctions.DeleteFromTable(deleteItem, "acd_courses") Then
    '                Names.Add("1")
    '                Names.Add("تم الحذف بنجاح!")
    '            Else
    '                Names.Add("2")
    '                Names.Add("لا يمكن الحذف!")
    '            End If
    '            Return Names.ToArray
    '        Catch
    '            Names.Add("2")
    '            Names.Add("لا يمكن الحذف!")
    '            Return Names.ToArray
    '        End Try
    '    End Function
    '#End Region

End Class