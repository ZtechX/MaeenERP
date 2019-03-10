<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PhotosUpload.ascx.vb" Inherits="ERpMaen.PhotosUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Panel ID="pnlImagesUpload" runat="server">
    <div class="row">
        <asp:ModalPopupExtender ID="mduImages" runat="server" BackgroundCssClass="modalBackground" TargetControlID="cmdImagesPOP" PopupControlID="pnlImages" ClientIDMode="Static" OkControlID="cmdPhotook" CancelControlID="lbImagesClose" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Panel ID="pnlImages" runat="server" CssClass="modalPopup">
            <asp:UpdatePanel ID="upImagesUpload" runat="server">
                <ContentTemplate>
                    <div style="float: right; padding-right: 10px;">
                        <asp:LinkButton ID="lbImagesClose" runat="server" CausesValidation="false">x</asp:LinkButton>
                    </div>
                    <asp:AjaxFileUpload ID="AjaxPhotoUploadImages" runat="server" ThrobberID="myThrobber" OnClientUploadComplete="onUploadFileComplete"
                        MaximumNumberOfFiles="10" AllowedFileTypes="jpg,jpeg,png" />
                    <div id="MainDivImages" style="display: none">
                        <div class="clear"></div>
                        <asp:Button ID="cmdImagesok" runat="server" OnClientClick="appendUploadedImages(); return false;" Text="Ok" CausesValidation="false" />
                        <div class="clear"></div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <div id="divUploadedImages" runat="server" clientidmode="Static" style="display: none;"></div>
    </div>
</asp:Panel>