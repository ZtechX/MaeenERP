<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Settings.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.Settings" Theme="Theme7" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager  ID="ToolkitScriptManager1" runat="server">
        <Services>
           <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
             <asp:ServiceReference Path="~/ASMX_WebServices/MultiFileUploader.asmx" />
        </Services>
    </asp:ScriptManager>

    <style type="text/css">
        .HiddenText label {
            display: none;
        }

        .style1 {
            width: 218px;
        }

        .modalBackground {
            background-color: rgba(0,0,0,.6);
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding: 10px;
            width: 1000px;
            max-height: 700px;
        }
    </style>
<!-- page title -->
    <%--<header id="page-header">
        <h1>اعدادات عامة</h1>
        <asp:Label ID="lblmainid" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="id"></asp:Label>
    </header>--%>
    <!-- page content -->
    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="اعدادات عامة" SkinID="page_title"></asp:Label>
                    </div>
    <div id="content" class="padding-20">
        <asp:UpdatePanel ID="up2" runat="server">
            <ContentTemplate>
         
               
                <div class="form_continer">
                    
                    <div class="update-progress02">
                        <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="up2">
                            <ProgressTemplate>
                                <asp:Image ID="imgLoader" ClientIDMode="Static" runat="server" ImageUrl="~/Images/loader.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>

                    <div class="res-label-margin">
                        <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    </div>

                    <div class="clear"></div>

                    <div>
                        <div class="col-md-3 col-sm-12">
                            <asp:Panel ID="gvPanel" runat="server" CssClass="col-md-5 pull-right zeroall">
                              
                                <asp:TextBox ID="txtpager" runat="server" AutoPostBack="true" Text="20" SkinID="" onkeypress="return isNumber(event);"></asp:TextBox>
                            </asp:Panel>
                            <div class="col-md-7 col-sm-12 search_continer">
                                <asp:TextBox ID="txtSearchAll" AutoPostBack="true" placeholder="البحث بالنوع" OnTextChanged="ValidateCode" SkinID="searchRt" runat="server"></asp:TextBox>                            
                                <asp:AutoCompleteExtender ID="acebasicSearch" BehaviorID="txtsaerchbasic" runat="server" FirstRowSelected="false"
                                    EnableCaching="false" Enabled="True" MinimumPrefixLength="1" CompletionListCssClass="acl"
                                    CompletionListItemCssClass="li" CompletionListHighlightedItemCssClass="li-hover"
                                    ServiceMethod="GetLookupDataTypes" ServicePath="~/ASMX_WebServices/WebService.asmx" TargetControlID="txtSearchAll"
                                    CompletionInterval="500">
                                </asp:AutoCompleteExtender>
                            </div>
                            <asp:GridView ID="GVDataType"  runat="server" AutoGenerateColumns="False" AllowSorting="true" OnSorting="gvDataTypes_Sorting" AllowPaging="true"
                                PageSize='<%# txtpager.Text %>' OnPageIndexChanging="GVDataType_PageIndexChanging" SkinID="gv_light_blue">
                                <Columns>
                                    <asp:TemplateField HeaderText="النوع" SortExpression="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="النوع" SortExpression="Type" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDataTypes" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="تعديل">
                                        <ItemTemplate>
                                            <asp:LinkButton SkinID="file-update"  ID="lbShow" runat="server" CommandArgument='<%# Eval("Type")%>' OnClick="ShowType" ToolTip="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="pad-left-right20 col-md-9 col-sm-12">

                            <div class="clear"></div>
                            <asp:Panel runat="server" ID="pnlValue" Visible="false">
                                <asp:Label ID="LookupId" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblType" runat="server" Visible="false"></asp:Label>
                                <div class="col-md-8">
                                    <asp:Label ID="lblTypesName" runat="server" SkinID="normal_title" Style="float:right;"></asp:Label>
                                </div>
                                <div class="marg_top_5 col-md-4">

                                    <asp:Panel ID="pnlOps" runat="server" Visible="False">

                                        <asp:LinkButton ID="lbNewType" runat="server" OnClick="NewType" SkinID="btn btn-primary  btn3d" class="btn btn-primary  btn3d" ToolTip="New" >جديد</asp:LinkButton>
                                    </asp:Panel>
                                </div>

                                <div class="clear"></div>
                                
                                <asp:Panel ID="pnlTypes" runat="server" Visible="False">
                                    <div style="display:none">
                                        <label for="ddlTypes">اختر</label>
                                        <asp:DropDownList ID="ddlTypes" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="clear"></div>
                                    <asp:GridView ID="gvValues" runat="server" AutoGenerateColumns="False" SkinID="gv_light_blue"
                                        AllowSorting="true" OnSorting="gvValues_Sorting" AllowPaging ="true" PageSize ="10" OnPageIndexChanging ="GVValues_PageIndexChanging">
                                        <Columns>
                                              <asp:TemplateField HeaderText="الترتيب" SortExpression="OrderNo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrderNo" runat="server" Text='<%# Eval("OrderNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Color" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblColor" runat="server" Text='<%# Eval("Color")%>' Visible="false"></asp:Label>
                                                    <asp:Panel ID="pnlColor" runat="server" Height="12" Width="30">
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="الأسم" SortExpression="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="تعديل">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbUpdate" SkinID="file-update" runat="server"
                                                        CommandArgument='<%# Eval("Id") %>' OnClick="UpdateValue" ToolTip="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="حذف">
                                                <ItemTemplate>
                                                    <asp:LinkButton SkinID="file-delete" ID="lbDelete" runat="server"
                                                        CommandArgument='<%# Eval("Id") %>' OnClick="DeleteValue" ToolTip="Delete"></asp:LinkButton>
                                                    <asp:ConfirmButtonExtender ID="lbDelete_ConfirmButtonExtender" runat="server"
                                                        ConfirmText="Are You Sure To Delete?" Enabled="True" TargetControlID="lbDelete">
                                                    </asp:ConfirmButtonExtender>
                                                    <asp:Panel ID="pnlGVDelete" runat="server" Visible="false" CssClass="delete_grid_inactive"></asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Icon" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgICON" Width="50px" Height="50px" ImageUrl='<%# "../" + Eval("Icon")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="clear"></div>
                                    <asp:Label ID="lblValueRes" runat="server" Visible="false" CssClass="res-label-info" Text="لا توجد نتائج"></asp:Label>
                                </asp:Panel>
                                <asp:Panel ID="pnlNewType" runat="server" Visible="False" CssClass="row">
                                    <div class="col-md-2 col-sm-12">
                                        <div class="clear">
                                        </div>
                                       <%-- <div class="photo" style="margin: 0; text-align: center; width: 156px; height: 160px;">
                                            <asp:Image ID="imgItemURL" ClientIDMode="Static" runat="server" style="width: 156px; height: 160px;"
                                                ImageUrl="~/App_Themes/images/add-icon.jpg" />
                                            <div class="update-progress-img">
                                                <img id="imgLoader" alt="uploading" style="display: none;" src="images\fancy-zoom/zoom-spin-2.png" />
                                            </div>
                                        </div>--%>
                                        <div class="clear">
                                        </div>
                                        <asp:Panel ID="Panel2" runat="server"></asp:Panel>

                                       
                                    </div>
                                    <div class="col-md-10 col-sm-12">
                                        <div class="col-md-8 col-sm-12">
                                            <label for="txtDescription" class="required">الأسم</label>
                                            <input type ="text" class="form-control"  ID="txtDescription"  runat="server" MaxLength="200">
                                        </div>
                                       
                                        <div class="clear"></div>
                                        <div class="col-md-8 col-sm-12">
                                            <asp:Panel ID="pnlRType" runat="server" CssClass="col-md-12 col-sm-12">
                                                 <label for="ddlRType">مرتبط ب</label>
                                                <asp:DropDownList ID="ddlRType" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
                                               
                                            </asp:Panel>
                                            <asp:Panel ID="pnlRType2" runat="server" CssClass="col-md-12 col-sm-12" Style="padding-top:10px;padding-bottom:10px;">
                                                <asp:RadioButtonList ID="rbrelateTo" AutoPostBack="true" Style="margin-left:165px;"  runat="server" OnSelectedIndexChanged="rbrelateTo_SelectedIndexChanged" >
                                                    <asp:ListItem Text="مرتبط بمحافظة" Value="CTY"  />
                                                    <asp:ListItem Text="مرتبط بمركز" Value="CEN" />
                                                </asp:RadioButtonList>
                                                <asp:DropDownList ID="ddlRType1" Visible="false" runat="server" Style="margin-right:0px;" AppendDataBoundItems="true"></asp:DropDownList>
                                                
                                                <asp:DropDownList ID="ddlRType2" Visible="false" runat="server" Style="margin-right:0px;" AppendDataBoundItems="true"></asp:DropDownList>
                                            </asp:Panel>
                                            </div>
                                            <div class="col-md-8" style="padding: 0">
                                                <label for="txtOrderNo">الترتيب</label>
                                                <asp:TextBox ID="txtOrderNo" Style="height:40px;border:#ddd 2px solid;padding: 6px 12px;font-size: 19px;margin-right: 4px;" runat="server" MaxLength="20" ></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="txtOrderNo_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" TargetControlID="txtOrderNo" ValidChars="0123456789">
                                                </asp:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-8 col-sm-12 pad-top" style="padding-right: 0">
                                                <asp:Button ID="Button1" runat="server" Text="حفظ" OnClick="SaveType" />
                                                <asp:LinkButton ID="lbCancelType" runat="server" OnClick="CancelType" SkinID="ButtonStyle" ToolTip="Cancel">الغاء</asp:LinkButton>
                                                <asp:ConfirmButtonExtender ID="lbCancel_ConfirmButtonExtender" runat="server"
                                                    ConfirmText="Are You Sure To Cancel?" Enabled="True" TargetControlID="lbCancelType">
                                                </asp:ConfirmButtonExtender>
                                            </div>
                                        
                                       
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>