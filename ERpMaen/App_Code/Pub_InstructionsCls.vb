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
Public Class Pub_InstructionsCls
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
    Public Function Save(ByVal id As String, ByVal date_m As String, ByVal date_hj As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson

            Dim rnd As New Random
            Dim code = "CRS" & rnd.Next(10000000, 99999999).ToString
            dictBasicDataJson.Add("code", code)

            dictBasicDataJson.Add("Created_at_m", date_m)
            dictBasicDataJson.Add("Created_at_hj", date_hj)
            dictBasicDataJson.Add("comp_id", LoginInfo.GetComp_id())

            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_Pub_Instructions", id, _sqlconn, _sqltrans) Then
                If Not PublicFunctions.TransUsers_logs("4215", "acd_Pub_Instructions", "ادخال", _sqlconn, _sqltrans) Then
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
    Public Function get_Courses(ByVal name As String, ByVal currentdate As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            'Dim dtstatus As New DataTable

            'dtstatus = DBManager.Getdatatable("select id , status, start_dt_m,  end_dt_m from acd_courses where archive=0 and comp_id=" + LoginInfo.GetComp_id())
            'For Each item As DataRow In dtstatus.Rows
            '    Dim start_date = item("start_dt_m").ToString()
            '    Dim end_date = item("end_dt_m").ToString()
            '    Dim currentDt = PublicFunctions.ConvertDatetoNumber(currentdate)
            '    Dim ST_DT = PublicFunctions.ConvertDatetoNumber(start_date)
            '    Dim END_DT = PublicFunctions.ConvertDatetoNumber(end_date)



            '    If END_DT < currentDt Then
            '        DBManager.ExcuteQuery("update acd_courses set status=2 where  id=" + item("id").ToString() + " and archive=0 and comp_id=" + LoginInfo.GetComp_id())
            '    ElseIf ST_DT < currentDt And END_DT > currentDt Then
            '        DBManager.ExcuteQuery("update acd_courses set status=1 where id=" + item("id").ToString() + "and  archive=0 and comp_id=" + LoginInfo.GetComp_id())
            '    Else
            '        DBManager.ExcuteQuery("update acd_courses set status=0 where  id=" + item("id").ToString() + "and archive=0 and comp_id=" + LoginInfo.GetComp_id())

            '    End If

            'Next

            Dim dt As New DataTable
            Dim condation = ""
            If name <> "" Then
                condation = " AND title LIKE '%" + name + "%'"
            End If

            'If filter = "" Then
            If LoginInfo.getUserType = 8 Then
                dt = DBManager.Getdatatable("select  acd_Pub_Instructions.id, acd_Pub_Instructions.code, acd_Pub_Instructions.image, acd_Pub_Instructions.User_Type ,acd_Pub_Instructions.title, acd_Pub_Instructions.description , acd_Pub_Instructions.Created_at_m from acd_Pub_Instructions where acd_Pub_Instructions.User_Type=8 and acd_Pub_Instructions.comp_id=" + LoginInfo.GetComp_id() + condation + "ORDER BY acd_Pub_Instructions.id DESC")

            ElseIf LoginInfo.getUserType = 4 Then
                dt = DBManager.Getdatatable("select  acd_Pub_Instructions.id,  acd_Pub_Instructions.code, acd_Pub_Instructions.image,acd_Pub_Instructions.User_Type ,acd_Pub_Instructions.title, acd_Pub_Instructions.description , acd_Pub_Instructions.Created_at_m from acd_Pub_Instructions where acd_Pub_Instructions.User_Type=4 and acd_Pub_Instructions.comp_id=" + LoginInfo.GetComp_id() + condation + "ORDER BY acd_Pub_Instructions.id DESC")

            Else
                dt = DBManager.Getdatatable("select  acd_Pub_Instructions.id, acd_Pub_Instructions.code, acd_Pub_Instructions.image,acd_Pub_Instructions.User_Type ,acd_Pub_Instructions.title, acd_Pub_Instructions.description , acd_Pub_Instructions.Created_at_m from acd_Pub_Instructions where acd_Pub_Instructions.comp_id=" + LoginInfo.GetComp_id() + condation + "ORDER BY acd_Pub_Instructions.id DESC")

            End If


            'ElseIf filter <> "" And filter <> 4 Then
            '    If LoginInfo.getUserType = 4 Then
            '        dt = DBManager.Getdatatable("select  acd_courses.id, acd_courses.status ,acd_courses.code as 'code' , acd_courses.name , acd_courses.description, acd_courses.start_dt_m, acd_courses.trainer_id,tblUsers.full_name ,tblUsers.User_Image as 'trImage',IsNull(price,0) as price,tbllock_up.Description as 'department' from acd_courses join tbllock_up on acd_courses.category_id=tbllock_up.id join tblUsers on acd_courses.trainer_id=tblUsers.id where  acd_courses.trainer_id=" + LoginInfo.GetUser__Id() + " and acd_courses.archive=0 and acd_courses.comp_id=" + LoginInfo.GetComp_id() + " and acd_courses.status=" + filter + condation + "ORDER BY acd_courses.id DESC")
            '    Else
            '        dt = DBManager.Getdatatable("select  acd_courses.id, acd_courses.status ,acd_courses.code as 'code' , acd_courses.name , acd_courses.description, acd_courses.start_dt_m, acd_courses.trainer_id,tblUsers.full_name ,tblUsers.User_Image as 'trImage',IsNull(price,0) as price,tbllock_up.Description as 'department' from acd_courses join tbllock_up on acd_courses.category_id=tbllock_up.id join tblUsers on acd_courses.trainer_id=tblUsers.id where acd_courses.archive=0 and acd_courses.comp_id=" + LoginInfo.GetComp_id() + " and acd_courses.status=" + filter + condation + "ORDER BY acd_courses.id DESC")
            '    End If
            'Else
            '    dt = DBManager.Getdatatable("select acd_courses_students.course_id, acd_courses.status,acd_courses.code as 'code' , acd_courses.name , acd_courses.description, acd_courses.start_dt_m, acd_courses.trainer_id,tblUsers.full_name ,tblUsers.User_Image as 'trImage',IsNull(price,0) as price,tbllock_up.Description as 'department'  from acd_courses_students join acd_courses  on acd_courses_students.course_id=acd_courses.id join tbllock_up on acd_courses.category_id=tbllock_up.id join tblUsers on acd_courses.trainer_id=tblUsers.id where  acd_courses_students.approved=1 and acd_courses.archive=0 and acd_courses_students.student_id=" + LoginInfo.GetUser__Id() + condation + "ORDER BY acd_courses.id DESC")


            'End If

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

#Region "get course Code"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getcourseCode() As String

        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select isNull(Max(code),0) as 'code' from  acd_courses ")
            If dt.Rows.Count <> 0 Then
                Return dt.Rows(0)(0).ToString
            End If
            Return ""
        Catch ex As Exception
            Return ""
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

#Region "Delete"
    ''' <summary>
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Delete(ByVal deleteItem As String) As String()
        Dim Names As New List(Of String)(10)
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "acd_courses") Then
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