<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="userPorfile.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.userPorfile" Theme="Theme5"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="result" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
          <asp:ServiceReference Path="~/ASMX_WebServices/userPorfile.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
        </Services>
    </asp:ScriptManager>

    <asp:UpdatePanel ID="up" runat="server">

        <ContentTemplate>
            <div class="update-progress02">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up">
                    <ProgressTemplate>
                        <asp:Image ID="imgProgress" runat="server" ImageUrl="~/App_Themes/Images/loader.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="wraper">
                <div>
                  <script src="../JS_Code/userPorfile/userPorfile.js"></script>
                    <style>
                        .checkbox {
                            margin: 0px;
                        }

                        #perm_table .btn {
                            font-size: 10px;
                            padding: 6px 12px;
                        }

                      .form-group{ direction: rtl;}
                      .thead-dark {     background: beige;text-align:right;}
                      .thead-dark tr th{     background: beige;text-align:right;}
                   
                    </style>

                    <script type="text/javascript">
                        $(function () {
                            $("#fuPhoto1").find('input[type="file"]').removeClass("form-control");
                        });
                        function UploadComplete2(sender, args) {
                           
                            var fileLength = args.get_length();
                            var fileType = args.get_contentType();
                            //alert(sender);
                            document.getElementById('imgItemURL').src = 'images/' + args.get_fileName();
                           
                            switch (true) {
                                case (fileLength > 1000000):

                                    fileLength = fileLength / 1000000 + 'MB';
                                    break;

                                case (fileLength < 1000000):

                                    fileLength = fileLength / 1000000 + 'KB';
                                    break;

                                default:
                                    fileLength = '1 MB';
                                    break;
                            }
                            clearContents(sender);
                        }
                      
                        //var prm = Sys.WebForms.PageRequestManager.getInstance();
                        //prm.add_pageLoaded(setupSB);
                        //function setupSB() {
                        //    Shadowbox.init({ skipSetup: true });
                        //    Shadowbox.clearCache();
                        //    Shadowbox.setup();
                        //}
                        function ClearMe(sender) {
                            sender.value = '';
                        }
                        function clearContents(sender) {
                            { $(sender._element).find('input').val(''); }
                        }

                    </script>
                </div>

                <div class="main-title">
                    <asp:Label ID="lblFormName" runat="server" Text="الصفحة الشخصية" SkinID="page_title"></asp:Label>
                </div>
                <div class="strip_menu">
                    <asp:Panel ID="pnlOps" runat="server" Style="text-align: right">
                        <asp:Panel ID="pnlFunctions" runat="server" CssClass="row" Enabled="true">
                            <div class="col-md-9 col-sm-12">
                                <ul>
                                    
                                    <li>
                                        <asp:LinkButton  OnClientClick="save(); return false;" runat="server" CommandArgument="1"
                                            SkinID="btn-top">
                                               <i class="fa fa-save"></i>
                                           حفظ
                                        </asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                    <uc1:result runat="server" ID="Result" />
                </div>
                <!-- page content -->
                <div id="content" class="padding-20 newformstyle form_continer">
                    <!--     -------tabs start-------->
              
                     <style>
                         #userType {
                             display:block;
                             margin-bottom:40px;
                             margin-top:0px;
                             margin-left:auto;
                             margin-right:auto;
                             text-align:center;
                             font-size:18px;
                             color:#148083;
                             box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 8px 0 rgba(0, 0, 0, 0.19);
                             border-radius: 0px;
                             border-top: 1px solid #adadad;
                             padding: 7px;
                             width :25%;
                         }
                     </style>
                                <div class="row">
                                 
                                    <asp:Label ID="lblmainid" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="id"></asp:Label>



                                    <div class="col-md-12" id="divForm">
                                        <!----------------column 1 -------------------->

                                        <div id="SavedivLoader" class="loader" style="display: none; text-align: center;">
                                            <asp:Image ID="img" runat="server" ImageUrl="../App_Themes/images/loader.gif" />
                                        </div>
                                        <label  id="userType"></label>
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <div class="col-md-3 col-sm-12">
                                                    <label class="label-required">الاسم بالكامل</label>
                                                </div>
                                                <div class="col-md-9 col-sm-12">
                                                    <asp:TextBox  required class="form-control" dbColumn="full_name" ClientIDMode="Static" ID="TextBox1" runat="server">
                                                    </asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                           
                                            <div class="form-group row">
                                                 
                                                <div class="col-md-3 col-sm-12">
                                                    <label >البريد الإلكترونى</label>
                                                </div>
                                                <div class="col-md-9 col-sm-12">
                                                    <asp:TextBox SkinID="form-control" class="form-control"  dbColumn="User_Email" ClientIDMode="Static" ID="TextBox2" runat="server">
                                                    </asp:TextBox>

                                                </div>

                                            </div>
                                           <div class="form-group row">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label class="label-required">
                                                                        رقم هوية المستخدم  </lable>
                                                                    </div>

                                                                    <div class="col-md-9 col-sm-12">
                                                                        <input onkeypress="return cust_chkNumber(event,this,10);"  required dbcolumn="user_indenty" type="text" id="txtindenty"
                                                                            class="form-control" runat="server" clientidmode="Static" />

                                                                        <br />
                                                                    </div>
                                                                </div>
                                               <div class="form-group row">
                                                
                                                <div class="col-md-3 col-sm-12">

                                                    <label>نشط   </label>
                                                </div>
                                                <div class="col-md-9 col-sm-12">
                                                     <input name="chkManual" id="Checkbox1" dbcolumn="Active" runat="server"  type="checkbox" />

                                                </div>


                                            </div>
                                                      
                                                                         
                             
                                        </div>
                                        <div class="col-md-6">
                                           
                                            <div class="form-group row">
                                                <div class="col-md-3 col-sm-12">
                                                    <label class="label-required">كلمة المرور</label>
                                                </div>
                                                <div class="col-md-9 col-sm-12">
                                                    <asp:TextBox required class="form-control"   TextMode="Password" dbColumn="User_Password" ClientIDMode="Static" ID="txtUserPassword" runat="server">
                                                    </asp:TextBox>
                                                  

                                                </div>
                                            </div>


                                            <div class="form-group row">
                                                
                                                <div class="col-md-3 col-sm-12">

                                                    <label class="label-required">رقم الجوال</label>
                                                </div>
                                                <div class="col-md-9 col-sm-12">
                                                    <asp:TextBox onkeypress="return cust_chkNumber(event,this,10);" class="form-control" required dbColumn="User_PhoneNumber" ClientIDMode="Static" ID="phone" runat="server">
                                                    </asp:TextBox>

                                                </div>
                                            </div>

                                                                           <%-- <div class="form-group row">--%>
<div class="col-md-6">
    
                                    <div>
                                                <asp:Image ID="imgItemURL" ClientIDMode="Static" runat="server" Width="114px" ImageUrl="~/App_Themes/images/add-icon.jpg" />
                                               
                                            </div>
                                            <div class="clear">
                                            </div>
                                            <div class="photo-upload-box">
                                                <span> تحميل صورة</span>
                                                <asp:AsyncFileUpload ID="fuPhoto1" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                                    OnClientUploadComplete="UploadComplete2" 
                                                    FailedValidation="False" />
                                                <asp:TextBox ID="photo_nm_footer" runat="server" ClientIDMode="Static" type="text" class="form-control" Style="display: none;"></asp:TextBox>

                                            </div>
                                </div>
                          
                                          <%--  </div>--%>
                                            </div>

                                        </div>
                                     

                    
                   
                                    </div>

                



                </div>
                <uc1:DynamicTable runat="server" ID="DynamicTable" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
