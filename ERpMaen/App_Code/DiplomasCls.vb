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
Public Class DiplomasCls
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
    Public Function Save(ByVal id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable
            Dim dt_academy As DataTable
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            Dim comp_id = dt_user.Rows(0).Item("comp_id").ToString

            dt_academy = DBManager.Getdatatable("select * from acd_acadmies where comp_id=" + comp_id)
            Dim academy_id = ""
            If dt_academy.Rows.Count <> 0 Then
                academy_id = dt_academy.Rows(0).Item("id").ToString
            End If

            Dim userId = LoginInfo.GetUser__Id()
            Dim rnd As New Random
            Dim code = "CRS" & rnd.Next(10000000, 99999999).ToString
            dictBasicDataJson.Add("code", code)
            dictBasicDataJson.Add("add_by", userId)
            dictBasicDataJson.Add("comp_id", LoginInfo.GetComp_id())
            dictBasicDataJson.Add("status", "0")
            dictBasicDataJson.Add("acd_acadmies", academy_id)
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_diplomes", id, _sqlconn, _sqltrans) Then
                If Not PublicFunctions.TransUsers_logs("3181", "acd_diplomes", "ادخال", _sqlconn, _sqltrans) Then
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
    Public Function get_deplomas(ByVal filter As String, ByVal name As String) As String()
        Dim dt_user As DataTable
        dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable

            Dim condation1 = ""

            If name <> "" Then
                condation1 = " AND name LIKE '%" + name + "%'"
            End If
            'dt = DBManager.Getdatatable(" select  acd_diplomes.id,acd_diplomes.add_by ,tblUsers.full_name as 'username', tblUsers.User_Image as'userImage',acd_diplomes.name , acd_diplomes.description , acd_diplomes.price from acd_diplomes join tblUsers on acd_diplomes.add_by=tblUsers.id " + condation1)
            'Else
            '    dt = DBManager.Getdatatable(" select  acd_diplomes.id,acd_diplomes.add_by ,tblUsers.full_name as 'username', tblUsers.User_Image as'userImage',acd_diplomes.name , acd_diplomes.description , acd_diplomes.price from acd_diplomes  join tblUsers on acd_diplomes.add_by=tblUsers.id where acd_diplomes.comp_id=" + LoginInfo.GetComp_id())
            'End If

            If filter = "" Then
                dt = DBManager.Getdatatable(" select  acd_diplomes.id,  acd_diplomes.status, acd_diplomes.code, acd_diplomes.add_by ,tblUsers.full_name as 'username', tblUsers.User_Image as'userImage',acd_diplomes.name , acd_diplomes.description , acd_diplomes.price from acd_diplomes  join tblUsers on acd_diplomes.add_by=tblUsers.id where acd_diplomes.archive=0 and acd_diplomes.comp_id=" + LoginInfo.GetComp_id() + condation1 + "ORDER BY acd_diplomes.id DESC")


            ElseIf filter <> "" And filter <> 4 Then
                dt = DBManager.Getdatatable(" select  acd_diplomes.id,  acd_diplomes.status, acd_diplomes.code ,acd_diplomes.add_by ,tblUsers.full_name as 'username', tblUsers.User_Image as'userImage',acd_diplomes.name , acd_diplomes.description , acd_diplomes.price from acd_diplomes  join tblUsers on acd_diplomes.add_by=tblUsers.id where acd_diplomes.archive=0 and acd_diplomes.comp_id=" + LoginInfo.GetComp_id() + " and acd_diplomes.status=" + filter + condation1 + "ORDER BY acd_diplomes.id DESC")
            Else
                dt = DBManager.Getdatatable("select acd_courses_students.course_id, acd_diplomes.code,acd_diplomes.status,acd_diplomes.price,  acd_diplomes.name , acd_diplomes.description,  acd_diplomes.add_by,tblUsers.full_name ,tblUsers.User_Image as 'trImage'  from acd_courses_students join acd_diplomes  on acd_courses_students.course_id=acd_diplomes.id join tblUsers on acd_diplomes.add_by=tblUsers.id where acd_courses_students.approved=1 and acd_courses_students.checked=1 and acd_courses_students.deleted=0 and  acd_courses_students.type=2 and acd_diplomes.archive=0 and acd_courses_students.student_id=" + LoginInfo.GetUser__Id() + condation1 + "ORDER BY acd_diplomes.id DESC")


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