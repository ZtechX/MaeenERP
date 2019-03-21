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
    Public Function get_data(ByVal case_id As String) As String()
        Dim Names As New List(Of String)(10)
        Names.Add("")
        Names.Add("")
        Names.Add("")
        Names.Add("")
        Try
            Dim dt_case As New DataTable
            Dim dt As New DataTable
            Dim dt_recieveChildren As New DataTable
            Dim dt_sessionChildren As New DataTable
            dt_case = DBManager.Getdatatable("select * from ash_cases where id=" + case_id)
            If dt_case.Rows.Count <> 0 Then
                Names(0) = PublicFunctions.ConvertDataTabletoString(dt_case)
            End If

            dt = DBManager.Getdatatable("(SELECT 'Session' as 'Tabel', date_m ,date_h,isNull(Pagainst.name,'') as 'agains',isNull(Powner.name,'') as 'owner'," +
" '' as 'deliver','' as 'recieve',isNull(place,'') as place,isNull(entry_time,'') as entry_time ,isNull(exite_time,'') as exite_time,isNull(result,'') as 'result' ,'' as'type',code,1.5 as 'amount' from ash_case_sessions " +
" Left Join  ash_case_persons Powner on Powner.id=ash_case_sessions.owner_id" +
" left join  ash_case_persons Pagainst on Pagainst.id=ash_case_sessions.second_party_id" +
" where ash_case_sessions.case_id=" + case_id + " )" +
" UNION " +
" (SELECT 'recieve_delivery',date_m ,date_h,'','',isNull(Pdeliver.name,'') as 'deliver',isNull(Precieve.name,'') as 'recieve','','','',''," +
" case Type When 1 Then 'تسليم واستلام اولاد'else 'تسليم واستلام نفقة' end  ,ash_case_receiving_delivery_details.id,amount from ash_case_receiving_delivery_details " +
" left join  ash_case_persons Pdeliver on Pdeliver.id=ash_case_receiving_delivery_details.deliverer_id" +
" Left Join  ash_case_persons Precieve on Precieve.id=ash_case_receiving_delivery_details.reciever_id" +
" where ash_case_receiving_delivery_details.case_id=" + case_id + ") order  by  date_m DESC")
            If dt.Rows.Count <> 0 Then
                Names(1) = PublicFunctions.ConvertDataTabletoString(dt)
            End If
            dt_recieveChildren = DBManager.Getdatatable("SELECT name,details_id , date_m FROM ash_case_children_receiving_details" +
" left join ash_case_childrens on ash_case_childrens.id=children_id" +
" left join ash_case_receiving_delivery_details on ash_case_receiving_delivery_details.id=details_id" +
" where details_id in(select id from ash_case_receiving_delivery_details where  case_id=" + case_id + " ) order  by  date_m DESC")
            If dt_recieveChildren.Rows.Count <> 0 Then
                Names(2) = PublicFunctions.ConvertDataTabletoString(dt_recieveChildren)
            End If
            'dt_sessionChildren = DBManager.Getdatatable("")
            'If dt_sessionChildren.Rows.Count <> 0 Then
            '    Names(3) = PublicFunctions.ConvertDataTabletoString(dt_sessionChildren)
            'End If
            Return Names.ToArray
        Catch ex As Exception
            Return Names.ToArray
        End Try
    End Function

#End Region

End Class