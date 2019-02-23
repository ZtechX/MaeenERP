Public Class Person
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsapprove_instrumRel As New clsFillComboByDataSource("select * from tbllock_up where type='RT' and IsNull(Deleted,0)=0", "Description", "id", "")
        clsapprove_instrumRel.SetComboItems(ddlrealtion, "", True, "--اختر--", False)
    End Sub

End Class