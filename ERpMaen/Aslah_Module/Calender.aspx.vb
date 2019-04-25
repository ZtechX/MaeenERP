Imports BusinessLayer.BusinessLayer

Public Class test
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
            Login_userType.InnerText = LoginInfo.getUserType()



            'End dropdown instrument




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

    End Sub

End Class