<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CommonQuest.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.CommonQuest" Theme="Theme5"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/ImageSlider.ascx" TagPrefix="uc1" TagName="ImageSlider" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/CommonQuest.asmx" />
            
                 <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
        </Services>
    </asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="update-progress02">
                <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="up">
                    <ProgressTemplate>
                        <asp:Image ID="imgProgress" runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="wraper">

                <div>
                    <script src="../JS_Code/CommonQuest/CommonQuest.js"></script>
                    <style>
                        #tbl_questions thead tr {
                            background-color:#C09C67 !important;
                        }
                        #tbl_questions th{
                            text-align:center;
                                height: 35px;
    font-size: large;
                        }
                         #tbl_questions .textbox{
                         margin-right:0px !important;
                         width:95% !important;
                        }
                       
                       #tbl_questions tbody td{
                            padding:8px !important;
                        }
                    </style>
                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="الإسئلة الشائعة" SkinID="page_title"></asp:Label>
                    </div>
             <div id="SavedivLoader" class="loader" style="display: none; text-align: center;">
                                            <asp:Image ID="img" runat="server" ImageUrl="../App_Themes/images/loader.gif" />
                                        </div>
                    <uc1:Result runat="server" ID="Result" />
                    <div id="divForm" class="newformstyle form_continer">
                        <div class="clear"></div>
                        <asp:Label ID="lblmainid" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="id"></asp:Label>
                        <div class="cp_margin pad10">
                            <div class="clear"></div>
                            <asp:Panel ID="pnlForm" runat="server">
                                <div  class="row" >
                                                                  <button class="btn btn-info pull-right btn-lg" type="button" onclick="addRow()">سؤال جديد</button>
          
                               <table id="tbl_questions" class=" dataTable dataTables_wrapper " style="border:1px solid #C09C67;">
                                   <thead>
                                       <tr>
                                           <th style="border-left:1px solid;width:45%;">السوال</th>
                                            <th style="width:45%">الجواب</th>
                                           <th style="width:8%">الترتيب</th>
                                           <th>حذف</th>
                                       </tr>
                                   </thead>
                                   <tbody>
                                   
                                   </tbody>
                               </table>
                                  
                                </div>
                                 
           <button class=" btn btn-success btn-lg"" type="button" onclick="save()" style="margin-top:20px;margin-left:50%;margin-right:50%;">حفظ</button>
                                      
                                  
                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>

                    </div>
                   
                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                   
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
