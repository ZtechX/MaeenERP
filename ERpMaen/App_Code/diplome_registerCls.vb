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
Public Class diplome_registerCls

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
    Public Function SaveRegister(ByVal id As String, ByVal diplomeId As String, ByVal code As String, ByVal ImagePath As String, ByVal imageName As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson


            dictBasicDataJson.Add("course_id", diplomeId)
            dictBasicDataJson.Add("type", "2")
            dictBasicDataJson.Add("approved", 0)
            dictBasicDataJson.Add("checked", 0)

            dictBasicDataJson.Add("student_id", LoginInfo.GetUser__Id())

            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_courses_students", id, _sqlconn, _sqltrans) Then
                Dim dictBasicDataJson1 As New Dictionary(Of String, Object)


                Dim dt2 As DataTable
                dt2 = DBManager.Getdatatable("delete from tblImages where Source='registeration' and Source_id=" + LoginInfo.GetUser__Id() + "and related_id=" + diplomeId)

                dictBasicDataJson1.Add("Source", "registeration")
                dictBasicDataJson1.Add("Source_id", LoginInfo.GetUser__Id())
                dictBasicDataJson1.Add("Image_path", ImagePath)
                dictBasicDataJson1.Add("Image_name", imageName)
                dictBasicDataJson1.Add("related_id", diplomeId)
                'If Not PublicFunctions.TransUsers_logs("3177", "acd_courses", "ادخال", _sqlconn, _sqltrans) Then
                '    success = False
                'Else
                '    success = True
                'End If
                If PublicFunctions.TransUpdateInsert(dictBasicDataJson1, "tblImages", "", _sqlconn, _sqltrans) Then

                    Dim dictNotification As New Dictionary(Of String, Object)

                    Dim dt3 As DataTable
                    Dim dt4 As DataTable
                    dt3 = DBManager.Getdatatable("select id from tblUsers where  User_Type=2  and comp_id=" + LoginInfo.GetComp_id())
                    Dim admin = dt3.Rows(0)(0).ToString
                    dt4 = DBManager.Getdatatable("select full_name from tblUsers where id=" + LoginInfo.GetUser__Id())
                    Dim studentName = dt4.Rows(0)(0).ToString
                    dictNotification.Add("RefCode", diplomeId)
                    dictNotification.Add("NotTitle", " تقديمات الطلاب")
                    dictNotification.Add("Date", DateTime.Now.ToString("dd/MM/yyyy"))
                    dictNotification.Add("AssignedTo", admin)
                    dictNotification.Add("CreatedBy", LoginInfo.GetUser__Id())
                    dictNotification.Add("Remarks", studentName)
                    dictNotification.Add("FormUrl", "Acadmies_module/DiplomaCourses?code=" + code)
                    If Not PublicFunctions.TransUpdateInsert(dictNotification, "tblNotifications", "", _sqlconn, _sqltrans) Then

                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return "False|لم يتم الحفظ"
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

#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_course(ByVal diplomeId As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select  acd_diplomes.id , acd_diplomes.name , acd_diplomes.description  ,acd_diplomes.category_id, tbllock_up.Description as 'course_category', acd_diplomes.status as 'StatusCourse', acd_diplomes.add_by ,tblUsers.full_name as'trainer_name',tblUsers.User_Image as 'trainerImage' ,acd_diplomes.price   from acd_diplomes join tblUsers on acd_diplomes.add_by=tblUsers.id join tbllock_up on tbllock_up.id=acd_diplomes.category_id   where acd_diplomes.id=" + diplomeId + "and acd_diplomes.comp_id=" + LoginInfo.GetComp_id())

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
    Public Function checkstudentregister(ByVal diplome_id As String) As String()

        Dim Names As New List(Of String)(10)
        Try



            Dim dt4 As New DataTable

            dt4 = DBManager.Getdatatable("select  isnull (approved,0) as 'appoved', isnull (deleted,0) as 'deleted'  , isnull (checked,0) as 'checked' from acd_courses_students where type=2  and approved=1 and  course_id =" + diplome_id + " and student_id=" + LoginInfo.GetUser__Id())

            If dt4.Rows.Count <> 0 Then
                If dt4.Rows(0)(0).ToString = True And dt4.Rows(0)(1).ToString = False And dt4.Rows(0)(2).ToString = True Then

                    Names.Add("4")



                End If

            End If

            Dim dt3 As New DataTable

            dt3 = DBManager.Getdatatable("select  isnull (approved,0) as 'appoved', isnull (deleted,0) as 'deleted' from acd_courses_students where  type=2 and approved=1 and  course_id =" + diplome_id + " and student_id=" + LoginInfo.GetUser__Id())

            If dt3.Rows.Count <> 0 Then
                If dt3.Rows(0)(0).ToString = True And dt3.Rows(0)(1).ToString = True Then

                    Names.Add("3")



                End If

            End If
            Dim dt2 As New DataTable

            dt2 = DBManager.Getdatatable("select isnull (approved,0) as 'appoved',  isnull(checked,0) as'check' from acd_courses_students where  course_id =" + diplome_id + " and student_id=" + LoginInfo.GetUser__Id())

            If dt2.Rows.Count <> 0 Then
                If dt2.Rows(0)(0).ToString = False And dt2.Rows(0)(1).ToString = False Then

                    Names.Add("1")


                End If

            End If



            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select approved ,checked from acd_courses_students where  course_id =" + diplome_id + " and student_id=" + LoginInfo.GetUser__Id())

            If dt.Rows.Count <> 0 Then
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
    Public Function get_courseFiles(ByVal diplomeId As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select image, condition from acd_course_conditions where type=2 and course_id=" + diplomeId)
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



End Class