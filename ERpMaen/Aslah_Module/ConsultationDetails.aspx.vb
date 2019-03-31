#Region "Signature"
'################################### Signature ####################################
'############# Version: v1.0
'############# Start Date:
'############# Form Name: Contacts 
'############# Your Name:  Ahmed Nayl
'############# Create date and time 21-01-2019 10:00 AM
'############# Form Description:  
'################################ End of Signature ################################
#End Region

#Region "Imports"
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports System.Data.OleDb
Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Data.SqlTypes
Imports System.Net
Imports System.Net.Mail
Imports System.Collections
Imports System.ComponentModel
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports AjaxControlToolkit
Imports Newtonsoft.Json
#End Region
Public Class ConsultDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                loginUser.Value = LoginInfo.GetUser__Id()
                loginUser_type.Value = LoginInfo.getUserType()
                Dim clsapprove_stSorc As New clsFillComboByDataSource("select * from tblLock_up where type='Ref' and IsNull(Deleted,0)=0", "Description", "id", "")
                clsapprove_stSorc.SetComboItems(ddlStatsrce, "", True, "--اختر--", False)

                'تصنيف الحاله
                Dim clsapprove_stCatg As New clsFillComboByDataSource("select * from tblLock_up where type='CType' and IsNull(Deleted,0)=0", "Description", "id", "")
                clsapprove_stCatg.SetComboItems(ddlcategory, "", True, "--اختر--", False)
                'حالة الاستشارة
                Dim clsStat As New clsFillComboByDataSource("select * from tblLock_up where type='CStat' and IsNull(Deleted,0)=0", "Description", "id", "")
                clsStat.SetComboItems(ddlstatus, "", True, "--اختر--", False)
                'النوع
                Dim clsgender As New clsFillComboByDataSource("select * from tblLock_up where type='Gen' and IsNull(Deleted,0)=0", "Description", "id", "")
                clsgender.SetComboItems(ddlgender, "", True, "--اختر--", False)

                'الحالة الاجتماعية
                Dim clsapprove_marStat As New clsFillComboByDataSource("select * from tblLock_up where type='sStat' and IsNull(Deleted,0)=0", "Description", "id", "")
                clsapprove_marStat.SetComboItems(ddlmaritalstat, "", True, "--اختر--", False)
                'نوع السكن
                Dim clsapprove_housType As New clsFillComboByDataSource("select * from tblLock_up where type='Hom' and IsNull(Deleted,0)=0", "Description", "id", "")
                clsapprove_housType.SetComboItems(ddlhouse_type, "", True, "--اختر--", False)
                'نوع الامتلاك
                Dim clsapprove_ownerType As New clsFillComboByDataSource("select * from tblLock_up where type='own' and IsNull(Deleted,0)=0", "Description", "id", "")
                clsapprove_ownerType.SetComboItems(ddltype_of_ownership, "", True, "--اختر--", False)
                'المستوى التعليمى
                Dim clsapprove_educLevel As New clsFillComboByDataSource("select * from tblLock_up where type='edu' and IsNull(Deleted,0)=0", "Description", "id", "")
                clsapprove_educLevel.SetComboItems(ddleducation_level, "", True, "--اختر--", False)
                'مستوي الدخل
                Dim clsapprove_incomStat As New clsFillComboByDataSource("select * from tblLock_up where type='Linc' and IsNull(Deleted,0)=0", "Description", "id", "")
                clsapprove_incomStat.SetComboItems(ddlincome_status, "", True, "--اختر--", False)
                Dim clsadvisors As New clsFillComboByDataSource("select * from ash_advisors where isNull(active,0)=1 and comp_id=" + LoginInfo.GetComp_id(), "name", "id", "")
                clsadvisors.SetComboItems(ddlAdvisors, "", True, "--اختر--", False)
                Dim clsnationality As New clsFillComboByDataSource("select * from tblLock_up where type='nat' and IsNull(Deleted,0)=0", "Description", "id", "")
                clsnationality.SetComboItems(ddlnationality, "", True, "--اختر--", False)
            End If
        Catch ex As Exception
            'clsMessages.ShowErrorMessgage(lblResError, ex.Message, Me)
        End Try
    End Sub




End Class