<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Notification.aspx.vb" MasterPageFile="~/Site.Master"  Inherits="ERpMaen.Notification1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="result" %>
<%@ Register Src="~/UserControls/pnlConfirm.ascx" TagPrefix="uc1" TagName="pnlconfirm" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
        </Services>
    </asp:ScriptManager>

    <asp:UpdatePanel ID="up" runat="server">

        <ContentTemplate>
          
            <div class="res-label-margin">
                <asp:Label ID="lblRes" runat="server"></asp:Label>
            </div>
            <asp:Label ID="lblUserId" runat="server" Visible="false"></asp:Label>


            <div class="container-fluid">
                <%--<h3 class="page-header">
                        <asp:Label ID="lblTitle" runat="server" Text="Notification"></asp:Label>
                    </h3>--%>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12 zero pull-right">
                        <asp:Panel ID="pnlSearch" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td class="marg_top_5">
                                        <asp:TextBox ID="txtSearch" runat="server" SkinID="searchRt" AutoPostBack="true" OnTextChanged="Search" placeholder="Search By Title Or Form Name"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div class="sm pull-right">
                        <small class="unit">سجلات / الصفحة</small>
                        <asp:TextBox ID="txtPager" runat="server" AutoPostBack="true" Text="10" SkinID="txt-number" OnTextChanged="Txtpager_TextChanged"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="txtpagerFilteredTextBoxExtender"
                            runat="server" Enabled="True" TargetControlID="txtpager"
                            ValidChars="0123456789">
                        </asp:FilteredTextBoxExtender>
                    </div>
                </div>


                <div class="scroll-x">
                    <asp:GridView ID="gvNotification" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize='<%# txtPager.Text%>' OnPageIndexChanging="GvItems_PageIndexChanging" AllowSorting="true"
                        OnSorting="gv_Sorting" CssClass="table table-striped table-hover" GridLines="None" ClientIDMode="Static" OnDataBound="gvNotifications_DataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="NotificationTitle" SortExpression="NotificationTitle">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblRefCode" runat="server" Text='<%# Eval("RefCode")%>' Visible="false"></asp:Label>
                                    <asp:HyperLink ID="lbNotTitle" runat="server" Text='<%# Eval("NotTitle")%>'
                                        NavigateUrl='<%#"~/Work_Module/" + Eval("RefType") + ".aspx?Operation=Search&Code=" + Eval("RefCode").ToString%>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FormTitle" SortExpression="RefType">
                                <ItemTemplate>
                                    <asp:Label ID="lblFormTitle" runat="server" Text='<%# Eval("RefType")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AssignDate" SortExpression="NotDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("NotDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AssignedBy" SortExpression="AssignedBy">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedBy" runat="server" Text='<%# Eval("AssignedBy")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="NotStatus">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeen" runat="server" Text='<%# Eval("IsSeen")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>لا يوجد بيانات</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
