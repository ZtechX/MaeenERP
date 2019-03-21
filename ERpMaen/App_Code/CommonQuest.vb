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
Public Class CommonQuest
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
    Public Function Save(ByVal arr As Object()) As Boolean
        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            For Each basicDataJson As Object In arr
                Dim dic As Dictionary(Of String, Object) = basicDataJson
                dic.Add("comp_id", LoginInfo.GetComp_id)
                If Not PublicFunctions.TransUpdateInsert(dic, "tblCommonQuestions", "", _sqlconn, _sqltrans) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    Return False
                End If
            Next
            _sqltrans.Commit()
            _sqlconn.Close()
            Return True
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try
    End Function
#End Region

#Region "Delete"
    ''' <summary>
    ''' </summary>
    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function Delete(ByVal deleteItem As String) As Boolean
        Try
            If PublicFunctions.DeleteFromTable(deleteItem, "tblCommonQuestions") Then
                Return True
            End If
            Return False
        Catch
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
    Public Function Edit(ByVal editItemId As String) As String
        Try
            Return PublicFunctions.GetDataForUpdate("tblCommonQuestions", editItemId)
        Catch ex As Exception
            Return ""
        End Try
    End Function
#End Region


End Class