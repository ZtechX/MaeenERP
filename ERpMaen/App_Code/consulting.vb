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
Public Class consulting
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
    Public Function Save(ByVal Consult_id As String, ByVal mess As String) As Boolean
        Try

            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            Dim dictBasicDataJson As New Dictionary(Of String, Object)
            dictBasicDataJson.Add("consulting_id", Consult_id)
            dictBasicDataJson.Add("message", mess)
            dictBasicDataJson.Add("user_from", LoginInfo.GetUser__Id())
            dictBasicDataJson.Add("message_date", DateTime.Now.ToString("dd/MM/yyyy"))
            dictBasicDataJson.Add("messageTime", String.Format("{0:hh:mm:ss tt}", DateTime.Now))
            If PublicFunctions.TransUpdateInsert(dictBasicDataJson, "ash_consulting_messages", "", _sqlconn, _sqltrans) Then
                Dim assignTo = ""
                Dim consult_nm = ""
                Dim dt As DataTable
                If LoginInfo.getUserType() = "6" Then
                    dt = DBManager.Getdatatable("select created_By,consult_nm from ash_consultings  where id=" + Consult_id)
                    If dt.Rows.Count <> 0 Then
                        assignTo = dt.Rows(0)(0).ToString
                        consult_nm = dt.Rows(0)(1).ToString
                    End If
                Else
                    dt = DBManager.Getdatatable("select tblUsers.id,consult_nm from ash_consultings left join tblUsers on related_id=advisor_id where ash_consultings.id=" + Consult_id + " and User_Type =6")
                    If dt.Rows.Count <> 0 Then
                        assignTo = dt.Rows(0)(0).ToString
                        consult_nm = dt.Rows(0)(1).ToString
                    End If

                End If
                If assignTo <> "" Then
                    Dim dictBasicDataJson1 As New Dictionary(Of String, Object)
                    dictBasicDataJson1.Add("RefCode", PublicFunctions.GetIdentity(_sqlconn, _sqltrans))
                    dictBasicDataJson1.Add("RefType", 6)
                    dictBasicDataJson1.Add("NotTitle", "رد استشارة " + consult_nm)
                    dictBasicDataJson1.Add("Date", dictBasicDataJson("message_date"))
                    dictBasicDataJson1.Add("AssignedTo", assignTo)
                    dictBasicDataJson1.Add("CreatedBy", LoginInfo.GetUser__Id())
                    dictBasicDataJson1.Add("Remarks", "رسائل استشارة")
                    dictBasicDataJson1.Add("FormUrl", "Aslah_Module/consultings?id=" + Consult_id)
                    If Not PublicFunctions.TransUpdateInsert(dictBasicDataJson1, "tblNotifications", "", _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        _sqlconn.Close()
                        Return False
                    End If
                End If
                _sqltrans.Commit()
                _sqlconn.Close()
                Return True

            End If
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            Return False
        End Try
    End Function
#End Region
#Region "get advisor User_id"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getadvisorUser_id(ByVal id As String) As String
        Dim dt As New DataTable
        dt = DBManager.Getdatatable("select id from tblUsers where User_Type=6 and related_id=" + id)
        Dim str = ""
        If dt.Rows.Count <> 0 Then
            str = dt.Rows(0)(0).ToString
        End If
        Return str
    End Function

#End Region

#Region "get Conslute"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getConslute(ByVal id As String) As String()

        Dim Names As New List(Of String)(10)
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("SELECT created_By,ash_consultings.code,consult_nm,lokk3.Description as 'source_id',lokk1.Description as 'category_id' ," +
" lokk2.Description as 'status', income_notes,start_date,start_date_hj," +
" isNull(ash_consultings.name,'') as 'From'," +
" isNull(ash_advisors.name,'')  as 'To'" +
" from ash_consultings" +
" left join tbllock_up lokk1 on lokk1.id=ash_consultings.category_id" +
" left join tbllock_up lokk2 on lokk2.id=ash_consultings.status" +
" left join ash_advisors  on ash_advisors.id=ash_consultings.advisor_id" +
" left join tbllock_up lokk3 on lokk3.id=ash_consultings.source_id where ash_consultings.id=" + id)
            If dt.Rows.Count <> 0 Then
                Names.Add("1")
                Names.Add(PublicFunctions.ConvertDataTabletoString(dt))
                Return Names.ToArray
            End If
            Names.Add("0")
            Return Names.ToArray

        Catch ex As Exception
            Names.Add("0")
            Return Names.ToArray
        End Try

    End Function

#End Region
#Region "Check user is SuperAdmin"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function isSuperAdmin() As Boolean
        Return LoginInfo.ConsultationSuperAdmin()
    End Function

#End Region
#Region "get Conslute_messages"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function getConsluteMess(ByVal id As String) As String
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select messageTime,message,message_date,case User_Type when 6 then full_name else name end as name from ash_consulting_messages " +
 " left join ash_consultings on consulting_id=ash_consultings.id " +
 " left join tblUsers on tblUsers.id=ash_consulting_messages.user_from " +
 "  where consulting_id=" + id)
            If dt.Rows.Count <> 0 Then
                Return PublicFunctions.ConvertDataTabletoString(dt)
            End If
            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region




End Class