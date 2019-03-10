<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MultiFileUpload.ascx.vb" Inherits="ERpMaen.MultiFileUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Panel ID="pnlMultiFileUpload" runat="server">
    <div class="row">
        <asp:ModalPopupExtender ID="mdu" runat="server" BackgroundCssClass="modalBackground" TargetControlID="cmdPOP" PopupControlID="pnl" ClientIDMode="Static" OkControlID="cmdok" CancelControlID="lbClose" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Panel ID="pnl" runat="server" CssClass="modalPopup">
            <asp:UpdatePanel ID="upPhotos" runat="server">
                <ContentTemplate>
                    <div style="float: right; padding-right: 10px;">
                        <asp:LinkButton ID="lbClose" runat="server" CausesValidation="false">x</asp:LinkButton>
                    </div>
                    <asp:AjaxFileUpload ID="AjaxFileUpload1" runat="server" ThrobberID="myThrobber" OnClientUploadComplete="onUploadFileComplete"
                        MaximumNumberOfFiles="10"    AllowedFileTypes="jpg,jpeg,pdf,doc,docx,xlsx,xls,png" />
                    <div id="MainDiv" style="display: none">
                        <div class="clear"></div>
                        <asp:Button ID="cmdok" runat="server" OnClientClick="appendUploadedFiles(); return false;" Text="Ok" CausesValidation="false" />
                        <div class="clear"></div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <div id="divUploadedFiles" runat="server" clientidmode="Static" style="display: none;"></div>
    </div>
</asp:Panel>