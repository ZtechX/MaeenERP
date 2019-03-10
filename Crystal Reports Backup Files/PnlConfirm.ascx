<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PnlConfirm.ascx.vb" Inherits="ERpMaen.PnlConfirm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Panel ID="pnlConfirm" runat="server">
    <ul>
        <li>
            <asp:LinkButton ID="cmdSave" runat="server" OnClientClick="save(); return false;" ValidationGroup="vgroup"
                SkinID="btn-top" TabIndex="10">
                <i class="fa fa-check"></i>حفظ
            </asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="cmdCancel" OnClientClick="cancelClick(); return false;" runat="server"
                TabIndex="11" CausesValidation="False" SkinID="btn-top">
                <i class="fa fa-times"></i>الغاء
            </asp:LinkButton>
            
        </li>
    </ul>
</asp:Panel>