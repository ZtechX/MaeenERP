Public Class DeliveryDate
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                Dim UserId = LoginInfo.GetUserId(Request.Cookies("UserInfo"), Me.Page)
                Dim cls_employee As New clsFillComboByDataSource("select * from tblUsers where IsNull(Deleted,0)=0 and User_Type=5 and comp_id=" + LoginInfo.GetComp_id(), "full_name", "id", "")
                'cls_employee.SetComboItems(ddlemployee_id, "", True, "--اختر--", False)
                cls_employee.SetComboItems(ddlemployee_id, "", True, "--اختر--", False)
                Dim clstype As New clsFillComboByDataSource("select * from tbllock_up where type='CT' and IsNull(Deleted,0)=0", "Description", "id", "")
                clstype.SetComboItems(ddltype2, "", True, "--اختر--", False)


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