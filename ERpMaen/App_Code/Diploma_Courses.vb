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
Public Class Diploma_Courses
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
    Public Function Save(ByVal id As String, ByVal basicDataJson As Dictionary(Of String, Object)) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As Dictionary(Of String, Object) = basicDataJson
            Dim dt_user As DataTable
            Dim dt_tr As DataTable
            dt_user = DBManager.Getdatatable("select * from tblUsers where id=" + LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")).ToString())
            Dim comp_id = dt_user.Rows(0).Item("comp_id").ToString

            dt_tr = DBManager.Getdatatable("select * from acd_training_centers where comp_id=" + comp_id)
            Dim tr_id = ""
            If dt_tr.Rows.Count <> 0 Then
                tr_id = dt_tr.Rows(0).Item("id").ToString
            End If


            dictBasicDataJson.Add("status", "60")
            dictBasicDataJson.Add("training_center_id", tr_id)
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "acd_courses", id, _sqlconn, _sqltrans) Then
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
    Public Function get_Courses(ByVal name As String) As String()
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
            dt = DBManager.Getdatatable("select  acd_courses.id , acd_courses.name , acd_courses.description, acd_courses.start_date_hj,acd_courses.trainer_id,tblUsers.full_name from acd_courses join tblUsers on acd_courses.trainer_id=tblUsers.id " + condation)

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