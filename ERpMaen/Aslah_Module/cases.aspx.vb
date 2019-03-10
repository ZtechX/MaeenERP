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
Public Class cases
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
                Dim cls_courts As New clsFillComboByDataSource("select * from ash_courts where IsNull(Deleted,0)=0", "name", "id", "")
                cls_courts.SetComboItems(ddlcourt_id, "", True, "--اختر--", False)
                Dim cls_employee As New clsFillComboByDataSource("select * from tblUsers where IsNull(Deleted,0)=0 and User_Type=4", "User_Name", "id", "")
                cls_employee.SetComboItems(ddlemployee_id, "", True, "--اختر--", False)
                cls_employee.SetComboItems(ddlemployee_id2, "", False, "--اختر--", False)
                cls_employee.SetComboItems(ddlemployee_id3, "", False, "--اختر--", False)
                cls_employee.SetComboItems(ddlemployee_id4, "", False, "--اختر--", False)


                'DropDownList Load for instrument rel
                Dim clsapprove_instrumRel As New clsFillComboByDataSource("select * from tbllock_up where type='RT' and IsNull(Deleted,0)=0", "Description", "id", "")
                clsapprove_instrumRel.SetComboItems(ddlrelationship_id, "", True, "--اختر--", False)
                clsapprove_instrumRel.SetComboItems(ddrelationship_id2, "", False, "--اختر--", False)

                'End dropdown instrument

                'DropDownList Load for current state الحاله الحالية
                Dim clsapprove_CurrState As New clsFillComboByDataSource("select * from tbllock_up where type='C_S' and IsNull(Deleted,0)=0", "Description", "id", "")
                clsapprove_CurrState.SetComboItems(ddlstatus, "", True, "--اختر--", False)

                Dim cls_DAY As New clsFillComboByDataSource("select * from tbllock_up where type='DY' and IsNull(Deleted,0)=0", "Description", "id", "")
                cls_DAY.SetComboItems(ddlday_nam1, "", True, "--اختر--", False)

                'End dropdown current state ddlservice

                'DropDownList Load for service الخدمة المقدمة
                Dim cls_ddldepart As New clsFillComboByDataSource("select * from tbllock_up where type='ST' and IsNull(Deleted,0)=0", "Description", "id", "")
                cls_ddldepart.SetComboItems(ddldepart, "", True, "--اختر--", False)


                'End dropdown  service ddlAdvisor

                ''DropDownList Load for Advisor المستشار المسؤل
                Dim clsapprove_Advisor As New clsFillComboByDataSource("select * from ash_advisors", "name", "id", "")
                clsapprove_Advisor.SetComboItems(ddlAdvisor, "", True, "--اختر--", False)

                '' الحالة الصحية
                Dim clshealthy As New clsFillComboByDataSource("select * from tbllock_up where type='HT' and IsNull(Deleted,0)=0", "Description", "id", "")
                clshealthy.SetComboItems(ddlhealth_status, "", True, "--اختر--", False)
                '' حالة الاجراء
                Dim clstype As New clsFillComboByDataSource("select * from tbllock_up where type='CT' and IsNull(Deleted,0)=0", "Description", "id", "")
                clstype.SetComboItems(ddltype, "", True, "--اختر--", False)

                'End dropdown  service ddlAdvisor 

                ''DropDownList Load for gender 
                'Dim clsapprove_Gender As New clsFillComboByDataSource("select * from ash_court where type='is' and IsNull(Deleted,0)=0", "Description", "id", "")
                'clsapprove_Gender.SetComboItems(ddlGender, "", True, "--اختر--", False)

                ''End dropdown Gender 

                ''DropDownList Load for health state 
                'Dim clsapprove_healthStat As New clsFillComboByDataSource("select * from ash_court where type='is' and IsNull(Deleted,0)=0", "Description", "id", "")
                'clsapprove_healthStat.SetComboItems(ddlHealthStat, "", True, "--اختر--", False)

                ''End dropdown health state ddlType

                ''DropDownList Load for deliverType
                'Dim clsapprove_typedelvir As New clsFillComboByDataSource("select * from ash_court where type='is' and IsNull(Deleted,0)=0", "Description", "id", "")
                'clsapprove_typedelvir.SetComboItems(ddlDelvType, "", True, "--اختر--", False)

                ''End dropdown deliverType ddlgiver

                ''DropDownList Load for giver بيانات المسلم
                'Dim clsapprove_giver As New clsFillComboByDataSource("select * from ash_court where type='is' and IsNull(Deleted,0)=0", "Description", "id", "")
                'clsapprove_giver.SetComboItems(ddlgiver, "", True, "--اختر--", False)

                ''End dropdown giver  

                ''DropDownList Load for Recipient بيانات المستلم
                'Dim clsapprove_Recipient As New clsFillComboByDataSource("select * from ash_court where type='is' and IsNull(Deleted,0)=0", "Description", "id", "")
                'clsapprove_Recipient.SetComboItems(ddlRecip, "", True, "--اختر--", False)

                ''End dropdown Recipient

                ''DropDownList Load for طالب التنفيذ
                'Dim clsapprove_implemenCaller As New clsFillComboByDataSource("select * from ash_court where type='is' and IsNull(Deleted,0)=0", "Description", "id", "")
                'clsapprove_implemenCaller.SetComboItems(ddlImplemCall, "", True, "--اختر--", False)

                ''End dropdown Recipient

                ''DropDownList Load for الموظف المختص 
                'Dim clsapprove_CompetEmploy As New clsFillComboByDataSource("select * from ash_court where type='is' and IsNull(Deleted,0)=0", "Description", "id", "")
                'clsapprove_CompetEmploy.SetComboItems(ddlCompetEmploy, "", True, "--اختر--", False)

                'End dropdown CompetEmploy الموظف المختص







                'LoginInfo.CheckPermisionsNew(cmdAdd, cmdUpdate, cmdDelete, Me.Page, UserId, lblFormName, DynamicTable)


            End If
        Catch ex As Exception
            'clsMessages.ShowErrorMessgage(lblResError, ex.Message, Me)
        End Try
    End Sub




End Class