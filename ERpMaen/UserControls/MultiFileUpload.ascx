<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MultiFileUpload.ascx.vb" Inherits="ERpMaen.MultiFileUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
   <style>
        .comp-logo {
    width: 100%;
    height: auto;
    min-height: 70px;
    max-height:70px;
    background-color: #efefef;
    overflow-y: hidden;
    border:2px solid #efefef;
}

.up-btn:hover {
    background-color: #0b4d73;
}
.up-btn {
    width: 100%;
    height: 30px;
    background: #0b669a;
    color: #fff;
    border: none;
    border-radius: 0px 0px 10px 10px;
    transition: all ease 01s;
    display: inline-block;
    vertical-align: middle;
    line-height: 30px;
    text-align: center;
    cursor: pointer;
}

.comp-logo
{
   background-size: contain!important;
    background-repeat: no-repeat !important;
    background-position: center !important;
}
        .btn-group {
            margin: 0px !important;
            width: auto;
        }
    </style>
<script>
    var curr_pnlMultiFileUpload;
    function uploadClick(ele) {
        debugger
        curr_pnlMultiFileUpload = $(ele).closest("#pnlMultiFileUpload");
        $(ele).closest("#uploader").find('input[type="file"]').click();

    }
    function MFUComplete() {

    }
     
</script>
<asp:Panel ID="pnlMultiFileUpload" runat="server">
 
        <asp:AsyncFileUpload ID="uploader" SkinID="image-upload" runat="server" OnUploadedComplete="MFUUpload"
                                                    OnClientUploadComplete="MFUComplete" 
                                                    FailedValidation="False" />
        <div style="background:url('../images/multiple-file-upload.png')" class="comp-logo"></div>
       <div class="up-btn" id="upload" onclick="uploadClick(this);">رفع</div>
        <asp:Panel ID="pnl" runat="server">
            <asp:UpdatePanel ID="upPhotos" runat="server">
                <ContentTemplate>
                    <div style="float: right; padding-right: 10px;">
                        <asp:LinkButton ID="lbClose" runat="server" CausesValidation="false">x</asp:LinkButton>
                    </div>
                  <%--  <asp:AjaxFileUpload ID="AjaxFileUpload1" runat="server" ThrobberID="myThrobber" OnClientUploadComplete="onUploadFileComplete"
                        MaximumNumberOfFiles="10"    AllowedFileTypes="jpg,jpeg,pdf,doc,docx,xlsx,xls,png" />--%>
                    
                    
                    <div id="MainDiv" style="display: none">
                        <div class="clear"></div>
                        <asp:Button ID="cmdok" runat="server" OnClientClick="appendUploadedFiles(); return false;" Text="Ok" CausesValidation="false" />
                        <div class="clear"></div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <div id="divUploadedFiles" runat="server" clientidmode="Static" style="display: none;"></div>
   
</asp:Panel>