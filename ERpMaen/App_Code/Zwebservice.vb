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
Public Class Zwebservicex
    Inherits System.Web.Services.WebService

#Region "Global_Variables"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
    Dim success As Boolean = False
#End Region

#Region "DynamicTable"
    ''' <summary>
    ''' get data based on passed quary
    ''' </summary>
    ''' 
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetListData(ByVal formName As String) As Array
        Try
            Dim archived As String = "0"
            If formName.Contains("|") Then
                archived = formName.Split("|")(1)
                formName = formName.Split("|")(0)
            End If
            Dim daForm As New TblFormsFactory
            Dim quaryStr = daForm.GetAllBy(TblForms.TblFormsFields.FormName, formName)(0).FormQueryAr
            If formName = "ConsultationDetails" Then
                quaryStr = quaryStr + " where ash_consultings.comp_id=" + LoginInfo.GetComp_id()
                If Not LoginInfo.ConsultationSuperAdmin() Then
                    If LoginInfo.getUserType() = "6" Then
                        quaryStr = quaryStr + " and advisor_id=" + LoginInfo.getrelatedId()
                    Else
                        quaryStr = quaryStr + " and created_By = " + LoginInfo.GetUser__Id()

                    End If

                End If
            ElseIf formName = "CommonQuest" Then
                quaryStr = quaryStr + " where comp_id=" + LoginInfo.GetComp_id()
            End If
            Dim DataAR As New List(Of String)
            Dim dt As New DataTable
            dt = DBManager.Getdatatable(quaryStr)
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim k As Integer = 0
            Dim head As String = ""
            Dim cnt As Integer = dt.Rows.Count
            Dim arlist As New ArrayList(cnt)

            While k < dt.Columns.Count
                head += dt.Columns(k).ColumnName.ToString + "|"
                k += 1
            End While
            DataAR.Add(head)
            arlist.Add(head)
            If dt.Rows.Count <> 0 Then
                While i < dt.Rows.Count
                    arlist.Add(dt.Rows(i).ItemArray().ToArray())
                    i += 1
                End While
            End If
            Return arlist.ToArray
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' get form quary string based on passed form name
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetFormQuaryString(ByVal formName As String) As String
        Try
            Dim archived As String = "0"
            If formName.Contains("|") Then
                archived = formName.Split("|")(1)
                formName = formName.Split("|")(0)
            End If
            Dim daForm As New TblFormsFactory
            Dim comp_id = LoginInfo.GetComp_id()
            Dim user_id = LoginInfo.GetUser__Id()

            Dim quaryStr = daForm.GetAllBy(TblForms.TblFormsFields.FormName, formName)(0).FormQueryAr


            If formName = "Files" Then
                If archived = "1" Then
                    quaryStr = quaryStr + " and tblfiles.user_id=" + Context.Request.Cookies("UserInfo")("UserId")
                Else
                    quaryStr = quaryStr + " and tblshare.user_id=" + Context.Request.Cookies("UserInfo")("UserId")
                End If
            End If
            If formName = "Users" Then
                If user_id = "1" Then
                    quaryStr = quaryStr
                Else
                    quaryStr = quaryStr + " and tblUsers.comp_id=" + comp_id.ToString
                End If
            End If

            Return quaryStr
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function


    ''' <summary>
    ''' get form keys based on form name
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetFormKeys(ByVal formName As String) As String
        Try
            Dim daForm As New TblFormsFactory
            Dim formId As String = daForm.GetAllBy(TblForms.TblFormsFields.FormName, formName)(0).Id.ToString
            Dim dt = DBManager.Getdatatable("select KeyName, KeyForm from TblFormKeys where FormId ='" + formId + "'")
            Return PublicFunctions.ConvertDataTabletoString(dt)
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' get login user Type|RelatedSalesmanCode
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetLoginUserTypeAndCode() As String
        Try
            Dim pf As New PublicFunctions
            Dim AgentID = LoginInfo.GetUserCode(Context.Request.Cookies("UserInfo")("UserID"))
            Dim Usertype = pf.GetUserType(AgentID)
            Return Usertype + "|" + AgentID
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region
#Region "Notifications"
    ''' <summary>
    ''' get notifications
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetNotifications(ByVal UserId As String) As String
        Try
            Dim dtNotifications = DBManager.Getdatatable("select ID, RefCode, RefType, NotTitle, Remarks, Date as NotDate, IsSeen from tblNotifications where AssignedTo='" + UserId + "' and Status=1  order by date desc")
            If dtNotifications.Rows.Count > 0 Then
                Return ConvertDataTabletoString(dtNotifications)
            End If
            Return String.Empty
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' update notification IsSeen
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function UpdateNotificationIsSeen(ByVal NotID_SeenValue As String) As Integer
        Try
            Dim NotId = NotID_SeenValue.Split("|")(0)
            Dim SeenValue = NotID_SeenValue.Split("|")(1)
            Dim UserId = NotID_SeenValue.Split("|")(2)

            If SeenValue = "True" Or SeenValue = "true" Then
                SeenValue = "False"
            Else
                SeenValue = "True"
            End If
            If DBManager.ExcuteQuery("update tblNotifications set IsSeen = '" + SeenValue + "' where Id = '" + NotId + "'") = 1 Then
                Dim dtCount = DBManager.Getdatatable("select Count(ID) as NotCount from tblNotifications where AssignedTo='" + UserId + "' and Status=1 and CONVERT(DATE,tblNotifications.Date)=CONVERT(DATE,getdate()) and (IsSeen = 'False' or IsSeen is null)")
                Return CInt(dtCount.Rows(0)("NotCount").ToString)
            Else
                Return -1
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' get notifications count
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetNotificationsCount(ByVal UserId As String) As Integer
        Try
            Dim dtCount = DBManager.Getdatatable("select Count(ID) as NotCount from tblNotifications where AssignedTo='" + UserId + "' and Status =1  and (IsSeen = 'False' or IsSeen is null)")
            Return CInt(dtCount.Rows(0)("NotCount").ToString)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' get refresh interval for notifications from lookup
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function GetRefreshInterval() As Integer
        Try
            'Dim dt = DBManager.Getdatatable("select Description from TblLockup where Type='NI'")
            Return CInt(30000)
        Catch ex As Exception
            Return 0
        End Try
    End Function
#End Region
    ''' <summary>
    ''' get refresh interval for notifications from lookup
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function ConvertDataTabletoString(ByVal dt As DataTable) As String
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim rows As New List(Of Dictionary(Of String, Object))()
        Dim row As Dictionary(Of String, Object)
        For Each dr As DataRow In dt.Rows
            row = New Dictionary(Of String, Object)()
            For Each col As DataColumn In dt.Columns
                row.Add(col.ColumnName, dr(col))
            Next
            rows.Add(row)
        Next
        Return serializer.Serialize(rows)
    End Function
    ''' <summary>
    ''' Set Menu Type
    ''' </summary>
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Function SetMenuType(ByVal mType As String) As Boolean
        Try
            Dim user_id = Context.Request.Cookies("UserInfo")("UserId")
            Dim Updated As Integer = DBManager.ExcuteQuery("update tblUsers set MenuType='" + mType + "' where id='" + user_id + "'")
            If Updated = 1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    <WebMethod(True)>
    <System.Web.Script.Services.ScriptMethod()>
    Public Sub SetUserDetailsToGlobalVar(ByVal UserDetails As String)
        clsGeneralVariables.userDetails = UserDetails
    End Sub
End Class