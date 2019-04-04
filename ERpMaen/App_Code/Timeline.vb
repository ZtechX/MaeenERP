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
Public Class Timeline
    Inherits System.Web.Services.WebService

#Region "Global_Variables"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
#End Region

#Region "get_data"
    ''' <summary>
    ''' Save  Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function get_data(ByVal case_id As String, ByVal done As String, ByVal start_dt As String, ByVal end_dt As String) As String()
        Dim Names As New List(Of String)(10)
        Names.Add("")
        Names.Add("")
        Names.Add("")
        Names.Add("")
        Names.Add("")
        Names.Add("")
        Dim condation = ""
        Dim condation_done = ""
        If start_dt <> "''" And end_dt <> "''" Then
            condation = " and date_m between " + start_dt + " And " + end_dt + " "
            Dim parts As String() = done.Split(New Char() {"|"c})
            condation_done = " in ('" + parts(0) + "'"
            If parts(1) <> "" Then
                condation_done = condation_done + " , '" + parts(1) + "')"
            Else
                condation_done = condation_done + ")"
            End If

        Else
            condation_done = " = '" + done + "'"
        End If

        Try
            Dim dt_case As New DataTable
            Dim dt As New DataTable
            Dim dt_recieveChildren As New DataTable
            Dim dt_sessionChildren As New DataTable
            Dim dt_sessionCompanions As New DataTable
            Dim dt_caseChildren As New DataTable

            dt_case = DBManager.Getdatatable("SELECT ash_cases.code, C_depart.Description As 'depart',date_m,date_h,C_court_id.Description as 'C_from'" +
            " ,instrument_no,instrument__date_m,instrument_date_h,Powner.name as 'P_from',Pagainst.name as 'P_against'" +
            " ,custody.name as 'child_custody',C_status.Description as 'status',ash_advisors.name as 'advisor_nm'," +
            " isNull(court_details,'') as 'court_details',isNull(ash_cases.details,'') as 'details'" +
            " FROM ash_cases" +
            " left join tbllock_up C_depart on C_depart.id=depart" +
            " left join tbllock_up C_court_id on C_court_id.id=court_id" +
            " Left Join  ash_case_persons Powner on Powner.id=person1_id" +
            " Left Join  ash_case_persons Pagainst on Pagainst.id=person2_id" +
            " Left Join  ash_case_persons custody on custody.id=child_custody" +
            " left join tbllock_up C_status on C_status.id=ash_cases.status" +
            " left join ash_advisors on ash_advisors.id=advisor_id where ash_cases.id=" + case_id)
            If dt_case.Rows.Count <> 0 Then
                Names(0) = PublicFunctions.ConvertDataTabletoString(dt_case)
            End If

            dt = DBManager.Getdatatable("(SELECT 'Session' as 'Tabel', date_m ,date_h,isNull(Pagainst.name,'') as 'agains',isNull(Powner.name,'') as 'owner'," +
            " '' as 'deliver','' as 'recieve',ash_case_sessions.id as 'session_id',isNull(place,'') as place,isNull(entry_time,'') as entry_time," +
            " isNull(exite_time,'') as exite_time,isNull(result,'') as 'result' ,'' as 'type',code,1.5 as 'amount' " +
            " from ash_case_sessions Left Join  ash_case_persons Powner on Powner.id=ash_case_sessions.owner_id" +
            " left join  ash_case_persons Pagainst on Pagainst.id=ash_case_sessions.second_party_id" +
            " where isNull(session_done,0) " + condation_done + " and ash_case_sessions.case_id=" + case_id + condation + " )" +
            " UNION " +
            " (SELECT 'recieve_delivery',date_m ,date_h,'','',isNull(Pdeliver.name,'') as 'deliver',isNull(Precieve.name,'') as 'recieve','','','','',''," +
            " case Type When 1 Then 'تسليم واستلام اولاد'else 'تسليم واستلام نفقة' end  ,details.id," +
            " amount from ash_case_receiving_delivery_details details " +
            " left join  ash_case_persons Pdeliver on Pdeliver.id=details.deliverer_id" +
            " Left Join  ash_case_persons Precieve on Precieve.id=details.reciever_id" +
            " where details.deleted !=1 and isNull(receiving_delivery_done,0)  " + condation_done + "  and details.case_id=" + case_id + condation + ") " +
            " union" +
            " (SELECT 'Conciliation', date_m ,date_h,isNull(Pagainst.name,'') as 'agains',isNull(Powner.name,'') as 'owner'," +
            " '','','','','' ,'',isNull(notes,''),'' ,code,'' from ash_case_conciliation " +
            " Left Join  ash_case_persons Powner on Powner.id=ash_case_conciliation.owner_id" +
            " left join  ash_case_persons Pagainst on Pagainst.id=ash_case_conciliation.second_party_id" +
            " where ash_case_conciliation.case_id=" + case_id + condation + " ) " +
            " union" +
            " (SELECT 'Correspondence' as 'Tabel', date_m ,date_h,'',isNull(Powner.name,'') as 'owner', " +
            " '' ,'','','','', '',isNull(notes,''),Description,ash_case_correspondences.code,'' from ash_case_correspondences " +
            " Left Join  ash_case_persons Powner on Powner.id=ash_case_correspondences.person_id" +
            " Left Join  tbllock_up on tbllock_up.id=ash_case_correspondences.type_correspondences" +
            " where isNull(correspondence_done,0)  " + condation_done + "   and ash_case_correspondences.case_id=" + case_id + condation + ") " +
            " order  by  date_m DESC")
            If dt.Rows.Count <> 0 Then
                Names(1) = PublicFunctions.ConvertDataTabletoString(dt)
            End If
            dt_recieveChildren = DBManager.Getdatatable("SELECT name,details_id , date_m FROM ash_case_children_receiving_details" +
            " left join ash_case_childrens on ash_case_childrens.id=children_id" +
            " left join ash_case_receiving_delivery_details  details on details.id=details_id" +
            " where details_id in(select id from ash_case_receiving_delivery_details details where details.deleted !=1 and case_id=" + case_id + condation + " ) order  by  date_m DESC")
            If dt_recieveChildren.Rows.Count <> 0 Then
                Names(2) = PublicFunctions.ConvertDataTabletoString(dt_recieveChildren)
            End If
            dt_sessionChildren = DBManager.Getdatatable("SELECT name,session_id , date_m FROM ash_session_children" +
            " left join ash_case_childrens on ash_case_childrens.id=children_id" +
            " left join ash_case_sessions on ash_case_sessions.id=session_id" +
            " where session_id in(select id from ash_case_sessions where  case_id=" + case_id + condation + ") order  by  date_m DESC")
            If dt_sessionChildren.Rows.Count <> 0 Then
                Names(3) = PublicFunctions.ConvertDataTabletoString(dt_sessionChildren)
            End If
            dt_sessionCompanions = DBManager.Getdatatable("SELECT name,session_id , date_m FROM ash_session_companions" +
            " left join ash_case_persons on ash_case_persons.id=person_id" +
            " left join ash_case_sessions on ash_case_sessions.id=session_id" +
            " where session_id in(select id from ash_case_sessions where  case_id=" + case_id + condation + ") order  by  date_m DESC")
            If dt_sessionCompanions.Rows.Count <> 0 Then
                Names(4) = PublicFunctions.ConvertDataTabletoString(dt_sessionCompanions)
            End If
            dt_caseChildren = DBManager.Getdatatable("SELECT name FROM ash_case_childrens where case_id=" + case_id)
            If dt_caseChildren.Rows.Count <> 0 Then
                Names(5) = PublicFunctions.ConvertDataTabletoString(dt_caseChildren)
            End If
            Return Names.ToArray
        Catch ex As Exception
            Return Names.ToArray
        End Try
    End Function

#End Region

End Class