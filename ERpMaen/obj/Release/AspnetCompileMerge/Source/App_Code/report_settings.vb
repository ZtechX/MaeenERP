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
Public Class report_settings
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
    Public Function save(ByVal header_img As String, ByVal footer_img As String) As Boolean

        Try
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim comp_id As String = LoginInfo.GetComp_id()
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select * from tblreport_settings where comp_id=" + comp_id)

            Dim dictBasicDataJson As New Dictionary(Of String, Object)
            Dim count As Integer = dt.Rows.Count
            If count <> 0 Then
                dictBasicDataJson.Add("deleted", 1)
                For index As Integer = 0 To (count - 1)
                    If Not PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblreport_settings", dt.Rows(index).Item("id").ToString, _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return False
                    End If
                Next
                dictBasicDataJson.Remove("deleted")
            End If
            dictBasicDataJson.Add("header_img", header_img)
            dictBasicDataJson.Add("footer_img", footer_img)
            dictBasicDataJson.Add("comp_id", comp_id)
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "tblreport_settings", "", _sqlconn, _sqltrans) Then
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
    Public Function get_Data() As String()
        Dim Names As New List(Of String)(10)
        Names.Add("")
        Names.Add("")
        Try
            Dim dt As New DataTable

            dt = DBManager.Getdatatable("select isNull(header_img,'') header_img,isNull(footer_img,'') footer_img from tblreport_settings where isNull(deleted,0) != 1 and comp_id=" + LoginInfo.GetComp_id())
            If dt.Rows.Count <> 0 Then
                Names(0) = dt.Rows(0).Item("header_img").ToString()
                Names(1) = dt.Rows(0).Item("footer_img").ToString()
            End If
            Return Names.ToArray
        Catch ex As Exception
            Return Names.ToArray
        End Try
    End Function

#End Region

End Class