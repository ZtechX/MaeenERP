<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="companies.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.companies" Theme="Theme5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/ImageSlider.ascx" TagPrefix="uc1" TagName="ImageSlider" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <div>
        <link href="../css/cases/cases.css" rel="stylesheet" />
        <script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
    </div>
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/ASMX_WebServices/companies.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/MultiFileUploader.asmx" />
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
                    <style>
                        #tablePrint table td, #tablePrint table th {
                            text-align: center;
                        }
                    </style>
                    <script type="text/javascript">
                        function UploadComplete2(sender, args) {
                            var fileLength = args.get_length();
                            var fileType = args.get_contentType();
                            var Sender_id = $(sender.get_element()).attr("id");
                            if (Sender_id == "compLogo_fuPhoto1") {
                                document.getElementById('compLogo').src = 'images_company/' + args.get_fileName();
                            } else if (Sender_id == "comp_fuPhoto1") {
                                document.getElementById('Comp_imgItemURL').src = 'images_company/' + args.get_fileName();
                            } else if (Sender_id == "Ac_fuPhoto1") {
                                document.getElementById('Ac_imgItemURL').src = 'images_company/' + args.get_fileName();
                            } else if (Sender_id == "cen_fuPhoto1") {
                                document.getElementById('Cen_imgItemURL').src = 'images_company/' + args.get_fileName();
                            }
                           
                            
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
        

                        function ClearMe(sender) {
                            sender.value = '';
                        }
                        function clearContents(sender) {
                            { $(sender._element).find('input').val(''); }
                        }

                        $(document).ready(function() {
    
    var navListItems = $('ul.setup-panel li a'),
        allWells = $('.setup-content');

    allWells.hide();

    navListItems.click(function(e)
    {
        e.preventDefault();
        var $target = $($(this).attr('href')),
            $item = $(this).closest('li');
        
        if (!$item.hasClass('disabled')) {
            navListItems.closest('li').removeClass('active');
            $item.addClass('active');
            allWells.hide();
            $target.show();
        }
    });
    
    $('ul.setup-panel li.active a').trigger('click');
    
    // DEMO ONLY //
                            $('#activate-step-2').on('click', function (e) {
                              
                               
                                if (!checkRequired("divForm")) { 
                                $('ul.setup-panel li:eq(1)').removeClass('disabled');
                                $('ul.setup-panel li a[href="#step-2"]').trigger('click');
                                $(this).remove();
                            }
                            })    
                            $('#activate-step-3').on('click', function (e) {
                                if ($('#perm_table tbody').find('input[type="checkbox"]:checked').length > 0) {
                                    $('ul.setup-panel li:eq(2)').removeClass('disabled');
                                    $('ul.setup-panel li a[href="#step-3"]').trigger('click');
                                    $(this).remove();
                                }
                                else { alert("اختر على الاقل موديول واحد");}
                            }) 
                            $('#activate-step-4').on('click', function (e) { 
                                if (!checkRequired("div_contract")) {
                                    $('ul.setup-panel li:eq(3)').removeClass('disabled');
                                    $('ul.setup-panel li a[href="#step-4"]').trigger('click');
                                    $(this).remove();
                                }
    }) 
});


                    </script>
                    <script src="../JS_Code/companies/companies.js"></script>
                    <script src="../JS_Code/companies/companies_Upload.js"></script>
                </div>

                <div class="main-title">
                    <asp:Label ID="lblFormName" runat="server" Text="الجهات المشتركة" SkinID="page_title"></asp:Label>
                </div>
                     <div id="SavedivLoader" class="loader" style="display: none; text-align: center;">
                                            <asp:Image ID="img" runat="server" ImageUrl="../App_Themes/images/loader.gif" />
                                        </div>
                          <%--/////////--%>
                
<div class="container" dir="rtl">
	<div class="row form-group">
        <div class="col-xs-12">
            <ul class="nav nav-pills nav-justified thumbnail setup-panel " dir="rtl">
                <li class="active"><a href="#step-1">
                    <h4 class="list-group-item-heading"> بيانات الجهة</h4>
                    <p class="list-group-item-text">البيانات الاساسية للجهة  </p>
                </a></li>
                <li class="disabled"><a href="#step-2">
                    <h4 class="list-group-item-heading">صلاحيات الجهة</h4>
                    <p class="list-group-item-text">الموديولات المشتركة بها الجهة</p>
                </a></li>
                <li class="disabled"><a href="#step-3">
                    <h4 class="list-group-item-heading">التعاقد </h4>
                    <p class="list-group-item-text">بيانات التعاقد للجهة  </p>
                </a></li>
                            <li class="disabled"><a href="#step-4">
                    <h4 class="list-group-item-heading">تاكيد </h4>
                    <p class="list-group-item-text">  حفظ البيانات  </p>
                </a></li>
            </ul>
        </div>
	</div>
     <uc1:Result runat="server" ID="Result" />
    <div class="row setup-content" id="step-1">
        <div class="col-xs-12">
            <div class="col-md-12 well text-center">
                   <input type="text" style="display:none;" id="loginUser"  runat="server"/>
                             
                                <asp:Label ID="lblmainid" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="id"></asp:Label>
                                <asp:TextBox runat="server" ID="txtTemId" Style="display: none"></asp:TextBox>
                                <asp:Label ID="ddlcomp_id" ClientIDMode="Static" Style="display: none" runat="server" dbcolumn=""></asp:Label>
                                <asp:Label ID="lbluser_id" ClientIDMode="Static" Style="display: none" runat="server" dbcolumn=""></asp:Label>
                                <asp:Label ID="lblgroup_id" ClientIDMode="Static" Style="display: none" runat="server" dbcolumn="group_id"></asp:Label>
                                <asp:Label ClientIDMode="Static" runat="server" Style="display: none" ID="lblEdit">1</asp:Label>
                                <div class="fancy-form" id="CurrentDate" style="display:none;"> 
                                <uc1:HijriCalendar runat="server" ID="HijriCalendar1" />  
                                    </div>
                                <div class="panel panel-default" style="width:100%; margin-left: auto;margin-right: auto;margin: 0px;">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">بيانات الجهة</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse1" class="panel-collapse collapse in">
                                                            <div class="panel-body" style="direction:rtl;">
                                                                <%-- start group3--%>
                                              
                                <div class="panel-body" id="divForm">

                                    <div class="col-md-6" id="divComp">
                                         <input type="text"  id="comp_id"  style="display: none" runat="server" dbcolumn="id"/>
                                
                                        <fieldset style="border:none;"><legend >بيانات الجهة</legend>
                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">اسم الجهة</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">

                                               <asp:TextBox  class="form-control" required dbcolumn="name_ar" ClientIDMode="Static" ID="txtname_ar" runat="server">
                                                </asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtname_ar"
                                                    ErrorMessage="من فضلك أدخل الجهة " ValidationGroup="vgroup1"></asp:RequiredFieldValidator>
                                         
                                                </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">رقم التليفون</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox   dbcolumn="tel" required   ID="tele"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tele"
                                                    ErrorMessage="من فضلك أدخل تليفون الجهة " ValidationGroup="vgroup1"></asp:RequiredFieldValidator>
                                         
                                            </div>
                                        </div>
                                            
                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label>رقم الفاكس</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox   dbcolumn="fax" type="text" id="fax"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>
                                            </div>
                                        </div>


                                       
                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                 <label>الموقع الالكترونى</label>
                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox   dbcolumn="site" type="text" id="site"  class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label>الرقم الضريبى</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox   dbcolumn="tax_number"   type="text" id="tax_number"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                                </div>
                                        </div>
                            

                                           <div class="col-md-3 col-sm-12">

                                    <div >
                                        <asp:Image ID="compLogo" ClientIDMode="Static" runat="server" Width="114px" ImageUrl="~/App_Themes/images/add-icon.jpg" />
                                        
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <div class="photo-upload-box" >
                                        <span>تحميل صورة</span>
                                        <asp:AsyncFileUpload ID="compLogo_fuPhoto1" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                            OnClientUploadComplete="UploadComplete2" />

                                    </div>
                                </div>
                                             <div class="col-md-9 form-group" style="margin-top:7%;">
                                            <div class="col-md-1 col-sm-12" style="float:left;">

                                                <label class="switch switch-success">
                                                    <input name="chkManual" id="chkManual" dbcolumn="active" runat="server" checked="checked" type="checkbox" />
                                                    
                                                </label>
                                            </div>
                                                 <div class="col-md-2 col-sm-12" style="margin-top: 23px;float:left;">
                                                <label>نشط   </label>

                                            </div>
                                           
                                        </div>
</fieldset>

                                       
                                          
                                    </div>

                                       <div class="col-md-6" id="divAdminComp">
                                          <input id="comp_AdminID" type="text" style="display: none" runat="server" dbcolumn="id"/>
                                
                                        <fieldset style="border:none;"><legend >بيانات مدير الجهة</legend>
                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">اسم المدير</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox   dbcolumn="full_name" required type="text" id="comp_AdminName"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="comp_AdminName"
                                                    ErrorMessage="من فضلك أدخل اسم مدير الجهة " ValidationGroup="vgroup1"></asp:RequiredFieldValidator>
                                         

                                            </div>
                                        </div>
            <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">رقم التليفون</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox   dbcolumn="User_PhoneNumber" required type="text" id="comp_AdminNum"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="comp_AdminNum"
                                                    ErrorMessage="من فضلك أدخل رقم التليفون لمدير الجهة " ValidationGroup="vgroup1"></asp:RequiredFieldValidator>
                                         
                                            </div>
                                        </div>


                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">البريد الالكترونى</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                         
                                                <asp:TextBox   dbcolumn="User_Email" required  type="email" id="comp_Adminemail"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="comp_Adminemail"
                                                    ErrorMessage="من فضلك أدخل الإميل لمدير الجهة " ValidationGroup="vgroup1"></asp:RequiredFieldValidator>
                                         
                                            </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">اسم المستخدم</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox   dbcolumn="User_Name" required type="text" id="comp_Adminuser_name"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="comp_Adminuser_name"
                                                    ErrorMessage="من فضلك أدخل اسم المستخدم لمدير الجهة " ValidationGroup="vgroup1"></asp:RequiredFieldValidator>
                                         
                                                </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">كلمة المرور</label>
                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox   dbcolumn="User_Password"  required  type="password" id="Comp_user_password"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Comp_user_password"
                                                    ErrorMessage="من فضلك أدخل كلمة المرور لمدير الجهة " ValidationGroup="vgroup1"></asp:RequiredFieldValidator>
                                         
                                            </div>
                                        </div>

                                                           <div class="form-group row">

                                    <div >
                                        <asp:Image ID="Comp_imgItemURL" ClientIDMode="Static" runat="server" Width="114px" ImageUrl="~/App_Themes/images/add-icon.jpg" />
                                       
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <div class="photo-upload-box">
                                        <span>تحميل صورة</span>
                                        <asp:AsyncFileUpload ID="comp_fuPhoto1" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                            OnClientUploadComplete="UploadComplete2"  />

                                    </div>
                                </div>
                                            </fieldset>
                                    </div>
                                
                                </div>
                                                                    <%--end group3--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                        <div id="acPanal" class="panel panel-default" style="width: 95%; margin-left: auto;margin-right: auto;">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">بيانات الاكاديمية</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse2" class="panel-collapse collapse">
                                                            <div class="panel-body" style="direction:rtl;">
                                                                <%-- start group3--%>
                                              <div id="AcadmeyDataDiv">
                                                   <div class="col-md-6 form-group" id="AcadmeyDiv">
                                                         <input id="Aca_id"  style="display: none" runat="server" dbcolumn="id"/>
                                
                                                       <fieldset style="border:none;"><legend>بيانات الاكاديمية</legend>
                                   <div class="row form-group">
                                                           <div class="col-md-3 col-sm-12">
                                        <label for="Name" class="label-required">اسم الاكاديمية</label>

                                    </div>
                                    <div class="col-md-9 col-sm-12">
                                        <asp:TextBox  class="form-control" dbColumn="name" ClientIDMode="Static" ID="Ac_name" runat="server">
                                        </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Ac_name"
                                            ErrorMessage="من فضلك أدخل اسم الاكاديمية " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                                           <div class="row form-group">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" >ملاحظات </label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox  TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static"  runat="server">
                                                                        </asp:TextBox>


                                                                    </div>
                                                               </div>
                                                           </fieldset>
                                                        </div>        

                                <div class="col-md-6" id="Ac_adminDiv">
                                      <input id="Ac_AdminID" type="text" style="display: none" runat="server" dbcolumn="id"/>
                                
                                        <fieldset style="border:none;"><legend >بيانات مدير الاكاديمية</legend>
                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">اسم المدير</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                              
                                                <asp:TextBox   dbcolumn="full_name" type="text" id="Ac_AdminName"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_7" runat="server" ControlToValidate="Ac_AdminName"
                                            ErrorMessage="من فضلك أدخل اسم مدير الاكاديمية " ValidationGroup="vgroup"></asp:RequiredFieldValidator>


                                            </div>
                                        </div>
            <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label>رقم التليفون</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                              
                                                <asp:TextBox   dbcolumn="User_PhoneNumber" type="text" id="Ac_adminTele"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">البريد الالكترونى</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                         
                                                <asp:TextBox   dbcolumn="User_Email"   type="email" id="AcUser_Email"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="AcUser_Email"
                                                    ErrorMessage="من فضلك أدخل إميل مدير الاكاديمية " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                            </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">اسم المستخدم</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                              
                                                <asp:TextBox   dbcolumn="User_Name"   type="text"   id="Acuser_name"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="Acuser_name"
                                                    ErrorMessage=" من فضلك أدخل اسم المستخدم لمدير الاكاديمية " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                                </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">كلمة المرور</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox   dbcolumn="User_Password"   type="password" id="Acuser_password"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="Acuser_password"
                                                    ErrorMessage="من فضلك أدخل كلمة المرور لمدير الاكاديمية " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                            </div>
                                        </div>

                           
                                                           <div class="form-group row">

                                    <div >
                                        <asp:Image ID="Ac_imgItemURL" ClientIDMode="Static" runat="server" Width="114px" ImageUrl="~/App_Themes/images/add-icon.jpg" />
                                        
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <div class="photo-upload-box">
                                        <span>تحميل صورة</span>
                                        <asp:AsyncFileUpload ID="Ac_fuPhoto1" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                            OnClientUploadComplete="UploadComplete2" />
                                        </div>
                                </div>
                                            </fieldset>
                                    </div>
                                        
                                              </div>
                                                                    <%--end group3--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                        <div id="CenPanal" class="panel panel-default" style="width: 95%; margin-left: auto;margin-right: auto;">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">بيانات مركز التدريب</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse3" class="panel-collapse collapse">
                                                            <div class="panel-body"  style="direction:rtl;">
                                                                <%-- start group3--%>
                                       
                                                                       <div id="CenterDataDiv">
                                                      <div class="col-md-6 form-group " id="CenterDiv">
                                                            <input id="cen_id" type="text"  style="display: none" runat="server" dbcolumn="id"/>
                                
                                                            <fieldset style="border:none;"><legend >بيانات مركز التدريب</legend>
                                   <div class="row form-group">
                                                                <div class="col-md-3 col-sm-12">
                                        <label for="Name" class="label-required">اسم المركز</label>

                                    </div>
                                    <div class="col-md-9 col-sm-12">
                                        <asp:TextBox   class="form-control" dbColumn="name" ClientIDMode="Static" ID="Cen_Name" runat="server">
                                        </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Cen_Name"
                                            ErrorMessage="من فضلك أدخل اسم المركز " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                    </div>
                                       </div>
                                                                <div class="row form-group">
                                                                  <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" >ملاحظات </label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox  TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextNotes" runat="server">
                                                                        </asp:TextBox>

                                                                        </div>
                                         
                                                                    </div>

                                                                </fieldset>
                                </div>
                                
                                        
                                                       
                                                                              <div class="col-md-6" id="Cen_adminDiv">
                                                                                    <input id="CenAdminID" style="display: none" runat="server" dbcolumn="id"/>
                                
                                        <fieldset style="border:none;"><legend >بيانات مدير المركز</legend>
                                          <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">اسم المدير</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox   dbcolumn="full_name" type="text" id="Cen_AdminName"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="Cen_AdminName"
                                                    ErrorMessage="من فضلك أدخل اسم مدير المركز " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         

                                            </div>
                                        </div>
            <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">رقم التليفون</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox   dbcolumn="User_PhoneNumber" type="text" id="Cen_AdminNum"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="Cen_AdminNum"
                                                    ErrorMessage="من فضلك أدخل رقم التليفون لمدير المركز " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                            </div>
                                        </div>


                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">البريد الالكترونى</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                         
                                                <asp:TextBox   dbcolumn="User_Email"   type="email" id="cen_Adminemail"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="cen_Adminemail"
                                                    ErrorMessage="من فضلك أدخل الإميل لمدير المركز " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                            </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">اسم المستخدم</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox   dbcolumn="User_Name" type="text" id="cen_Adminuser_name"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="cen_Adminuser_name"
                                                    ErrorMessage="من فضلك أدخل اسم المستخدم لمدير المركز " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                                </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">كلمة المرور</label>
                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <asp:TextBox   dbcolumn="User_Password"   type="password" id="Cen_user_password"   class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="Cen_user_password"
                                                    ErrorMessage="من فضلك أدخل كلمة المرور لمدير المركز " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                            </div>
                                        </div>

                                                           <div class="form-group row">

                                    <div >
                                        <asp:Image ID="Cen_imgItemURL" ClientIDMode="Static" runat="server" Width="114px" ImageUrl="~/App_Themes/images/add-icon.jpg" />
                                       
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <div class="photo-upload-box">
                                        <span>تحميل صورة</span>
                                        <asp:AsyncFileUpload ID="cen_fuPhoto1" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                            OnClientUploadComplete="UploadComplete2"  />

                                    </div>
                                </div>
                                            </fieldset>
                                    </div>
                                    <%--</div>--%>

                                                                      
                                </div>

                                                                    <%--end group3--%>
                                                            </div>
                                                        </div>
                                                    </div>
                <button id="activate-step-2" type="button" class="btn btn-primary btn-lg">التالى</button>
            </div>
        </div>
    </div>
    <div class="row setup-content" id="step-2">
        <div class="col-xs-12">
            <div class="col-md-12 well">
                
                                <asp:Label ID="lblprposal_id" ClientIDMode="Static" Style="display: none" runat="server"></asp:Label>

                                <div id="div_modules">
                                    <h2 class="elipsis">
                                        <!-- panel title -->
                                        <strong>الصلاحيات</strong>
                                    </h2>
                                    <!-- panel content -->
                                    <div class="panel-body" id="proposal_panel">



                                        <div class="row">
                                            

                                            <div id="tablePrint">
                                            </div>
                                        </div>



                                    </div>
                                    <!-- /panel content -->
                                </div>
                                 <button id="activate-step-3" type="button" class="btn btn-primary btn-lg"> التالى</button>

            </div>
        </div>
    </div>
    <div class="row setup-content" id="step-3">
        <div class="col-xs-12">
            <div class="col-md-12 well">
              
                                <div class="panel-body" id="div_contract">
                                                  <div class="col-md-6">
                                                  <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                        <label class="label-required">تاريخ بداية الاتفاق   </label>
                                                </div>
                                    <div class="col-md-9 col-sm-12 fancy-form" id="divdate2">
                                        <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="deal_start_date_m" ID="lblstart_date"></asp:Label>
                                        <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="deal_start_date_hj" ID="lblstart_date_hj"></asp:Label>
                                        <uc1:HijriCalendar runat="server" ID="HijriCalendar2" />
                                    </div>


                                    </div>

                                                   <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                        <label class="label-required">تاريخ نهاية الاتفاق   </label>
                                                </div>

                                    <div class="col-md-9 col-sm-12 fancy-form" id="divdate3">
                                        <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="deal_end_date_m" ID="lblend_date"></asp:Label>
                                        <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="deal_end_date_hj" ID="lblend_date_hj"></asp:Label>
                                         <uc1:HijriCalendar runat="server" ID="HijriCalendar3" />
                                    </div>
                                    </div>
                                           
                                                      <div class="row form-group">
                                                    <div class="col-md-3 col-sm-12">
                                    <label for="txtUploadedFiles">الملفات</label>
                                                        </div>
                                    <div class="col-md-9 col-sm-12">
                                        <asp:Button ID="cmdPOP" runat="server" CssClass="btn btn-primary btn-3d" Style="padding: 0 20px 0 20px;" Text="+" CausesValidation="False" />
                                        <asp:TextBox ID="txtUploadedFiles" CssSkinID="form-control" class="form-control" Style="display: inline-block; width: 40%;" ReadOnly="true" onclick="showUploadedFilesTable(this);" runat="server"
                                            ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                  
                                </div>
                                      

                                   </div>
                                    
                                    <div class="col-md-6">
                                     
                                   <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                             <label>الصيانة   </label>
  </div>
                                    <div class="col-md-9 col-sm-12">

                                        <label class="switch switch-success">
                                            <input name="chkManual" id="maintance" onchange="get_maintance();" checked="" dbcolumn="maintainance" runat="server" type="checkbox" />
                                            

                                        </label>
                                    </div>
                                      </div>
                                    <div id="maintance_company"  style="display: none">
                                    <div class="row form-group" ">
                                          <div class="col-md-3 col-sm-12"><label class="label-required">مدة الصيانة من  </label></div>

                                        <div class="col-md-9 col-sm-12 fancy-form" id="divdate4">
                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="maintainance_start_date_m" ID="lblmaintaianace_start_date"></asp:Label>
                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="maintainance_start_date_hj" ID="lblmaintaianace_start_date_hj"></asp:Label>
                                             <uc1:HijriCalendar runat="server" ID="HijriCalendar4" />
                                        </div>

                                        </div>
                                       
                                        <div class="row form-group">
                                                    <div class="col-md-3 col-sm-12">
                                                        <label class="label-required">الى  </label>
                                                        </div>
                                        <div class="col-md-9 col-sm-12 fancy-form" id="divdate5">
                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="maintainance_end_date_m" ID="lblmaintaianace_end_date"></asp:Label>
                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="maintainance_end_date_hj" ID="lblmaintaianace_end_date_hj"></asp:Label>
                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar5" />
                                        </div>
                                    </div>
                                      </div> 
                                   </div>
                            </div>
             <button id="activate-step-4" type="button" class="btn btn-primary btn-lg"> التالى</button>
            </div>
        </div>
    </div>
    <div class="row setup-content" id="step-4">


         <div class="col-xs-12 well">
                         <div class="row">
                                    <!------ form action button start-->
                                    <div class="form-actions pull-left" style="margin-top: -10px;">
                                        <button validationgroup="vgroup" id="cmdSave" runat="server" clientidmode="Static" commandargument="New" type="button" onclick="save_companies();" class="btn btn-success btn3d">
                                            حفظ البيانات
		                       	<i class="ace-icon fa fa-save "></i>
                                        </button>
                                        <button id="clearBtn" runat="server" clientidmode="Static" style="display: none" type="button" onclick="cancel2(); return false;" class="btn btn-white btn3d">
                                            <i class=" fa fa-eraser "></i>
                                            <span>مسح البيانات</span>
                                        </button>
                                    </div>
                                    <!------ form action button end -->
                                </div>
             </div>
        </div>
</div>
                <%--///////////--%>
             


                <uc1:ImageSlider runat="server" ID="ImageSlider" />
                <uc1:MultiPhotoUpload runat="server" ID="MultiPhotoUpload" />
                <uc1:DynamicTable runat="server" ID="DynamicTable" />
                <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                <asp:HiddenField ID="tblH" runat="server" />
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
