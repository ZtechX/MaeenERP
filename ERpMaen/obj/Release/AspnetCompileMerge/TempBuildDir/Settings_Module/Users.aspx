<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Users.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.Users" Theme="Theme5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="result" %>
<%@ Register Src="~/UserControls/pnlConfirm.ascx" TagPrefix="uc1" TagName="pnlconfirm" %>

<asp:Content runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/User.asmx" />
      <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/MultiFileUploader.asmx" />
        </Services>
    </asp:ScriptManager>

    <asp:UpdatePanel ID="up" runat="server">

        <ContentTemplate>
            <div class="update-progress02">
                <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="up">
                    <ProgressTemplate>
                        <asp:Image ID="imgProgress" runat="server" ImageUrl="~/App_Themes/Images/loader.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="wraper">
                <div>
                    <script src="../JS_Code/User/User.js"></script>
                    <script src="../JS_Code/User/User_Edit.js"></script>
                    <script src="../JS_Code/User/User_Save.js"></script>
                    <script src="../JS_Code/User/User_Tree.js"></script>
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
                            var img = document.getElementById('imgLoader');
                            img.style.display = 'none';
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
                        function UploadStarted2() {
                            //var fileName = args.get_fileName();
                            var img = document.getElementById('imgLoader');
                            img.style.display = 'block';
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
                    <asp:Label ID="lblFormName" runat="server" Text="المستخدمين" SkinID="page_title"></asp:Label>
                </div>
                <div class="strip_menu">
                    <asp:Panel ID="pnlOps" runat="server" Style="text-align: right">
                        <asp:Panel ID="pnlFunctions" runat="server" CssClass="row" Enabled="true">
                            <div class="col-md-9 col-sm-12">
                                <ul>
                                    <li>
                                        <asp:LinkButton OnClientClick="add(); return false;" ID="cmdSave" runat="server"
                                            SkinID="btn-top" CausesValidation="false">
                                     <i class="fa fa-plus"></i>
                                           جديد
                                        </asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="cmdDelete" OnClientClick="deleteItem(); return false;" ToolTip="Delete Community" runat="server" SkinID="btn-top">
                                               <i class="fa fa-trash-o"></i>
                                           حذف
                                        </asp:LinkButton>
                                        <asp:Panel ID="pnlDelete" runat="server">
                                        </asp:Panel>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="cmdUpdate" OnClientClick="setformforupdate(); return false;" runat="server" CommandArgument="1"
                                            SkinID="btn-top">
                                               <i class="fa fa-pencil-square-o"></i>
                                           تعديل
                                        </asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                    <uc1:Result runat="server" ID="Result" />
                    <uc1:pnlconfirm runat="server" ID="PnlConfirm" />
                </div>
                <!-- page content -->
                <div id="content" class="padding-20 newformstyle form_continer">
                    <!--     -------tabs start-------->
              
                     
                                <div class="row">
                                 
                                    <asp:Label ID="lblmainid" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="id"></asp:Label>



                                    <div class="col-md-12" id="divForm">
                                        <!----------------column 1 -------------------->

                                     <%--   <div id="SavedivLoader" class="loader" style="display: none; text-align: center;">
                                            <asp:Image ID="img" runat="server" ImageUrl="../Images/loader.gif" />
                                        </div>--%>

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
                                                 <label for="Name" class="label-required">نوع المستخدم </label>

                                                  </div>
                                                 <div class="col-md-9 col-sm-12">
                                                   <asp:DropDownList dbcolumn="User_Type" required class="form-control" ClientIDMode="Static" ID="ddlUser_Type" runat="server"> </asp:DropDownList>
                                                  </div>
                                                  </div>
                                            <div class="form-group row">
                                                  <div class="col-md-3 col-sm-12">
                                                 <label for="Name" class="label-required">المجموعة </label>

                                                  </div>
                                                 <div class="col-md-9 col-sm-12">
                                                   <asp:DropDownList dbcolumn="group_id" required class="form-control" ClientIDMode="Static" ID="ddlgroup_id" runat="server"> </asp:DropDownList>
                                                  </div>
                                                  </div>
                                               <div class="form-group row">
                                                <div class="col-md-6">
                                                    <div class="col-md-3 col-sm-12">

                                                    <label>نشط   </label>
                                                </div>
                                                <div class="col-md-9 col-sm-12">
                                                     <input name="chkManual" id="Checkbox1" dbcolumn="Active" runat="server"  type="checkbox" />

                                                </div>
                                                </div>
                                                
                                                     <div class="col-md-6">
                                                
                                                <div class="col-md-5 col-sm-12">

                                                    <label>استلام استشارات   </label>
                                                </div>
                                                <div class="col-md-7 col-sm-12">
                                                     <input name="chkManual" id="Checkbox2" dbcolumn="recieve_Consult" runat="server"  type="checkbox" />

                                                </div>


                                            </div>

                                            </div>
                                           
                                                      <div id="divResearcher" runat="server" class="form-group row">
                                                
                                                <div  class="col-md-3 col-sm-12">

                                                    <label>باحث   </label>
                                                </div>
                                                <div class="col-md-9 col-sm-12">
                                                     <input name="chkManual" id="Researcher" onchange="changeResearcher();" dbcolumn="Researcher" runat="server" runat="server"  type="checkbox" />

                                                </div>


                                            </div>
                                                                         
                             
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <div class="col-md-3 col-sm-12">
                                                    <label >الإدارة</label>
                                                </div>
                                                <div class="col-md-9 col-sm-12">
                                                    <asp:DropDownList dbcolumn="managment_id"   class="form-control" ClientIDMode="Static" ID="ddlmanagment_id" runat="server">
                                                    </asp:DropDownList>
                                                    <i class="fancy-arrow"></i>
                                                </div>
                                            </div>


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


                                            <div class="form-group row" id="Comp_div">
                                                <div class="col-md-3 col-sm-12">
                                                    <div id="divcomp_id" runat="server">
                                                        <label class="label-required">اسم الجمعية</label>
                                                        <label style="display: none;" id="comp" runat="server"></label>
                                                        
                                                    </div>
                                                    </div>
                                                    <div class="col-md-9 col-sm-12">
                                                
                                                         <asp:DropDownList required class="form-control" dbColumn="comp_id" ClientIDMode="Static" ID="ddlcomp_id" runat="server"  >
                                                          </asp:DropDownList>
                                                        <i class="fancy-arrow"></i>
                                                    </div>
                                                </div>
                                                


                                              
                                 
                                                                            <div class="form-group row">
<div class="col-md-6 col-md-offset-3">
                                    <div >
                                        <asp:Image ID="imgItemURL" ClientIDMode="Static" runat="server" Width="114px" ImageUrl="~/App_Themes/images/add-icon.jpg" />
                                        <div class="update-progress-img">
                                             <asp:Image ID="imgLoader" runat="server" ClientIDMode ="Static" style="display: none;"  ImageUrl="../App_Themes/images/loader.gif" />
                                        </div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <div class="photo-upload-box">
                                        <span>تحميل صورة</span>
                                        <asp:AsyncFileUpload ID="fuPhoto1" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                            OnClientUploadComplete="UploadComplete2" OnClientUploadStarted="UploadStarted2"
                                            FailedValidation="False"  />
                                    </div>
                                </div>
                          
                                            </div>
                                            </div>

                                        </div>
                                     

                        <div class="col-md-12" id="LiPlaces">
                            <div class="col-md-3">
                                <table id="CTY" class="table  table-bordered ">
                                  <thead class="thead-dark">
                                        <tr>
                                            <th>#</th>
                                            <th>المحافظة</th>
                                        </tr>
                                    </thead>
                                    <tbody>

                                    </tbody>
                                </table>
                                  </div>
                        
                            <div class="col-md-3">
                            
                                     <table id="CEN" class="table  table-bordered ">
                                    <thead class="thead-dark">
                                        <tr>
                                            <th>#</th>
                                            <th>المركز</th>
                                        </tr>
                                    </thead>
                                         <tbody>

                                    </tbody>
                                </table>
                            
                               </div>
                                <div class="col-md-3">
                                 <table id="VILL" class="table table-bordered ">
                                    <thead class="thead-dark">
                                        <tr>
                                            <th>#</th>
                                            <th>القرية</th>
                                        </tr>
                                    </thead>
                                     <tbody>

                                    </tbody>
                                </table>
                                
                           </div>
                            <div class="col-md-3">
                                <table id="BIO" class="table table-bordered ">
                                  <thead class="thead-dark">
                                        <tr>
                                            <th>#</th>
                                            <th>الحى</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <input type="checkbox" />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                   
                                    </div>

                



                </div>
                <uc1:DynamicTable runat="server" ID="DynamicTable" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
