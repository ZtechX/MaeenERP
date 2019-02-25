<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="companies.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.companies" Theme="Theme5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/MultiPhotoUpload.ascx" TagPrefix="uc1" TagName="MultiPhotoUpload" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/ImageSlider.ascx" TagPrefix="uc1" TagName="ImageSlider" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <div>
          <link href="../css/cases/cases.css" rel="stylesheet" />
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
                            //alert(sender);
                            document.getElementById('imgItemURL').src = 'images_company/' + args.get_fileName();
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

                        function ClearMe(sender) {
                            sender.value = '';
                        }
                        function clearContents(sender) {
                            { $(sender._element).find('input').val(''); }
                        }
                    </script>
                    <script src="../JS_Code/companies/companies.js"></script>
                    <script src="../JS_Code/companies/companies_Upload.js"></script>
                </div>

                <div class="main-title">
                    <asp:Label ID="lblFormName" runat="server" Text="الجهات المشتركة" SkinID="page_title"></asp:Label>
                </div>
                <div class="strip_menu">
                    <!-- #  tab list start  -->
                    <ul class="nav nav-tabs" id="myTab">
                        <li runat="server" id="ladd" class="active">
                            <a data-toggle="tab" href="#home">
                                <i class="green ace-icon fa fa-home bigger-120"></i>
                                <!-- # عنوان التبويب -->
                                المعلومات الاساسية للجهة
                            </a>

                        </li>
                        <li runat="server" id="Li1" style="display: block">
                            <a data-toggle="tab" href="#tab2">
                                <i class="green ace-icon fa fa-tasks"></i>
                                <!-- # عنوان التبويب -->
                                صلاحيات الجهة
                            </a>

                        </li>
                        <li runat="server" id="Li2" style="display: block">
                            <a data-toggle="tab" href="#tab3">
                                <i class="green ace-icon fa fa-money"></i>
                                <!-- # عنوان التبويب -->
                                بيانات التعاقد 
                            </a>

                        </li>

                    </ul>
                    <!-- #  tab list end  -->

                    <%--                        <uc1:PnlConfirm runat="server" ID="PnlConfirm" />--%>
                </div>
                <uc1:Result runat="server" ID="Result" />

                <div id="content" class="padding-20" dir="rtl">
                    <!--     -------tabs start-------->
                    <div class="tabbable">

                        <!-- #  tab list content start  -->

                        <div class="tab-content" style="background: white;">


                            <div id="home" class="tab-pane fade in active">
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
                                <asp:Label ID="lblmainid" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="id"></asp:Label>
                                <asp:TextBox runat="server" ID="txtTemId" Style="display: none"></asp:TextBox>
                                <asp:Label ID="ddlcomp_id" ClientIDMode="Static" Style="display: none" runat="server" dbcolumn=""></asp:Label>
                                <asp:Label ID="lbluser_id" ClientIDMode="Static" Style="display: none" runat="server" dbcolumn=""></asp:Label>
                                <asp:Label ID="lblgroup_id" ClientIDMode="Static" Style="display: none" runat="server" dbcolumn="group_id"></asp:Label>
                                <asp:Label ClientIDMode="Static" runat="server" Style="display: none" ID="lblEdit">1</asp:Label>
                                       <div class="panel panel-default" style="width:95%; margin-left: auto;margin-right: auto;margin-top: 25px;">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">بيانات الجهة</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse1" class="panel-collapse collapse">
                                                            <div class="panel-body" style="direction:rtl;">
                                                                <%-- start group3--%>
                                              
                                <div class="panel-body" id="divForm">
                                    <div class="col-md-6">
                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">اسم الجهة</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">

                                               <asp:TextBox SkinID="form-control" class="form-control" dbcolumn="name_ar" ClientIDMode="Static" ID="txtname_ar" runat="server">
                                                </asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtname_ar"
                                                    ErrorMessage="من فضلك أدخل الجهة " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                                </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">رقم التليفون</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                               <%-- <input dbcolumn="tel"  type="text" id="tele" placeholder="" skinid="form-control" class="form-control" onkeypress="return isNumber(event);" />
                                                 --%>    
                                                <asp:TextBox   dbcolumn="tel"   ID="tele"  SkinID="form-control" class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tele"
                                                    ErrorMessage="من فضلك أدخل التليفون " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                            </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label>رقم التليفون(2)</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <%--<input dbcolumn="tel1" type="text" id="Text8" placeholder="" skinid="form-control" class="form-control" onkeypress="return isNumber(event);" />
                                            --%>
                                                <asp:TextBox   dbcolumn="tel1" type="text" id="Text8"  SkinID="form-control" class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">البريد الالكترونى</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                               <%-- <input dbcolumn="email" required type="email" id="email" placeholder="example@example.com" skinid="form-control" class="form-control" />
                                            --%>
                                                <asp:TextBox   dbcolumn="email"   type="email" id="email"  SkinID="form-control" class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="email"
                                                    ErrorMessage="من فضلك أدخل الاميل " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                            </div>
                                        </div>

                                       

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label>نشط   </label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">

                                                <label class="switch switch-success">
                                                    <input name="chkManual" id="chkManual" dbcolumn="active" runat="server" checked type="checkbox" />
                                                    
                                                </label>
                                            </div>
                                        </div>

                                    </div>


                                    <div class="col-md-6">

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label>المسئول</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                               <%-- <input dbcolumn="person" type="text" id="person" placeholder="" skinid="form-control" class="form-control" />
                                            --%>
                                                <asp:TextBox   dbcolumn="person" type="text" id="person"  SkinID="form-control" class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label>رقم الفاكس</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                               <%-- <input dbcolumn="fax" type="text" id="fax" placeholder="" skinid="form-control" class="form-control" />
                                            --%>
                                                <asp:TextBox   dbcolumn="fax" type="text" id="fax"  SkinID="form-control" class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>
                                            </div>
                                        </div>


                                       
                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                 <label>الموقع الالكترونى</label>
                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <%--<input dbcolumn="site" type="text" id="sie" placeholder="" skinid="form-control" class="form-control" />
                                            --%>
                                                <asp:TextBox   dbcolumn="site" type="text" id="site"  SkinID="form-control" class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label>الرقم الضريبى</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <%--<input dbcolumn="tax_number" type="text" id="tax_number" placeholder="" skinid="form-control" class="form-control" />
                                           --%>
                                                <asp:TextBox   dbcolumn="tax_number"   type="text" id="tax_number"  SkinID="form-control" class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                                </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">اسم المستخدم</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                                <%--<input dbcolumn="User_Name" type="text" onchange="check_user();"  id="user_name" placeholder="" skinid="form-control" class="form-control" />
                                         --%>
                                                <asp:TextBox   dbcolumn="User_Name"   type="text" onchange="check_user();"  id="user_name"  SkinID="form-control" class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="user_name"
                                                    ErrorMessage="من فضلك أدخل اسم المستخدم " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                                </div>
                                        </div>

                                        <div class="row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="label-required">كلمة المرور</label>

                                            </div>
                                            <div class="col-md-9 col-sm-12">
                                               <%-- <input dbcolumn="User_Password"   type="password" id="user_password" placeholder="" skinid="form-control" class="form-control" />
                                           --%>
                                                <asp:TextBox   dbcolumn="User_Password"   type="password" id="user_password"  SkinID="form-control" class="form-control"  ClientIDMode="Static" runat="server">
                                                </asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="user_password"
                                                    ErrorMessage="من فضلك أدخل كلمة المرور " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                         
                                            </div>
                                        </div>

                           
                                                           <div class="form-group row">

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
<%--                                                                                    <input id="photo_nm" type="text" SkinID="form-control" class="form-control" style="margin-top: -40px;" readonly="" />--%>

                                    </div>
                                </div>

                                    </div>
                                </div>
                                                                    <%--end group3--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                        <div class="panel panel-default" style="width: 95%; margin-left: auto;margin-right: auto;">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">بيانات الاكاديمية</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse2" class="panel-collapse collapse">
                                                            <div class="panel-body" style="direction:rtl;">
                                                                <%-- start group3--%>
                                              <div id="AcadmeyDataDiv">
                                                   <div class="col-md-6 form-group ">
                                    <div class="col-md-3 col-sm-12">
                                        <label for="Name" class="label-required">اسم الاكاديمية</label>

                                    </div>
                                    <div class="col-md-9 col-sm-12">
                                        <asp:TextBox SkinID="form-control" class="form-control" dbColumn="name" ClientIDMode="Static" ID="TextBox1" runat="server">
                                        </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Name"
                                            ErrorMessage="من فضلك أدخل اسم الاكاديمية " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                    </div>
                                </div>

                                <div class="col-md-6 form-group ">
                                    <div class="col-md-3 col-sm-12">
                                        <label>المدير</label>
                                    </div>

                                    <div class="col-md-9 col-sm-12">
                                        <asp:DropDownList dbcolumn="admin" class="form-control" ClientIDMode="Static" ID="AcadmeyAdmin" runat="server" style="margin-right:0px;">
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="col-md-6 form-group ">
                                    <div class="col-md-3 col-sm-12">
                                        <label class="label-required">
                                        التاريخ   </lable>
                                    </div>

                                    <div class="col-md-9 col-sm-12">

                                        <div class="fancy-form" id="divdate_3">
                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="start_date" ID="Label3"></asp:Label>
                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="start_date_hj" ID="Label4"></asp:Label>
                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar1" />
                                        </div>
                                        <br />
                                    </div>
                                    </div>
                                          <div class="col-md-6 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">ملاحظات </label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextBox2" runat="server">
                                                                        </asp:TextBox>


                                                                    </div>
                                                                </div>
                                              </div>
                                                                    <%--end group3--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                        <div class="panel panel-default" style="width: 95%; margin-left: auto;margin-right: auto;">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">بيانات مركز التدريب</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse3" class="panel-collapse collapse">
                                                            <div class="panel-body"  style="direction:rtl;">
                                                                <%-- start group3--%>
                                       
                                                                       <div id="CenterDataDiv">
                                                      <div class="col-md-6 form-group ">
                                    <div class="col-md-3 col-sm-12">
                                        <label for="Name" class="label-required">اسم المركز</label>

                                    </div>
                                    <div class="col-md-9 col-sm-12">
                                        <asp:TextBox SkinID="form-control" class="form-control" dbColumn="name" ClientIDMode="Static" ID="Name" runat="server">
                                        </asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Name"
                                            ErrorMessage="من فضلك أدخل اسم المركز " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                    </div>
                                </div>

                                <div class="col-md-6 form-group ">
                                    <div class="col-md-3 col-sm-12">
                                        <label>المدير</label>
                                    </div>

                                    <div class="col-md-9 col-sm-12">
                                        <asp:DropDownList dbcolumn="admin" class="form-control" ClientIDMode="Static" ID="Centeradmin" runat="server" style="margin-right:0px;">
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="col-md-6 form-group ">
                                    <div class="col-md-3 col-sm-12">
                                        <label class="label-required">
                                        التاريخ   </lable>
                                    </div>

                                    <div class="col-md-9 col-sm-12">

                                        <div class="fancy-form" id="divdate_4">
                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_m" ID="Label1"></asp:Label>
                                            <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="date_hj" ID="Label2"></asp:Label>
                                            <uc1:HijriCalendar runat="server" ID="HijriCalendar" />
                                        </div>
                                        <br />
                                    </div>

                                </div>
                                                                       
                                           <div class="col-md-6 form-group">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label for="Name" class="label-required">ملاحظات </label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextNotes" runat="server">
                                                                        </asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                               
                                    <%--</div>--%>

                                                                      
                                </div>

                                                                    <%--end group3--%>
                                                            </div>
                                                        </div>
                                                    </div>

                           </div>
                            <div id="tab2" class="tab-pane fade ">
                                <div class="row">
                                    <div class="form-actions pull-left" style="margin-top: -10px;">
                                        <button validationgroup="vgroup" id="Button2" runat="server" clientidmode="Static" commandargument="New" type="button" onclick="save_modules();" class="btn btn-success btn3d">
                                            حفظ البيانات
		                       	<i class="ace-icon fa fa-save "></i>
                                        </button>
                                        <button id="Button3" runat="server" clientidmode="Static" type="button" onclick="cancel(); return false;" class="btn btn-white btn3d">
                                            <i class=" fa fa-eraser "></i>
                                            <span>مسح البيانات</span>
                                        </button>
                                    </div>
                                </div>
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

                            </div>
                            <div id="tab3" class="tab-pane fade ">
                                <div class="row">
                                    <!------ form action button start-->
                                    <div class="form-actions pull-left" style="margin-top: -10px;">
                                        <button validationgroup="vgroup" id="Button1" runat="server" clientidmode="Static" commandargument="New" type="button" onclick="save_contract(); return false;" class="btn btn-success btn3d">
                                            حفظ التعاقد
		                       	<i class="ace-icon fa fa-save "></i>
                                        </button>

                                    </div>
                                    <!------ form action button end -->
                                </div>
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
                           </div>
                                <!------tabs end ----->
            </div>

            <!------tabs end ----->
                        <!-- --------------------- dynamic table start ---------------------->
                        <%--<uc1:ImageSlider runat="server" ID="ImageSlider1" />
                                <uc1:MultiPhotoUpload runat="server" ID="MultiPhotoUpload1" />--%>
                        <%--<uc1:DynamicTable runat="server" ID="DynamicTable1" />--%>

                        <!-- --------------------- dynamic table end ---------------------->
                    </div>


                </div>






                <uc1:ImageSlider runat="server" ID="ImageSlider" />
                <uc1:MultiPhotoUpload runat="server" ID="MultiPhotoUpload" />
                <uc1:DynamicTable runat="server" ID="DynamicTable" />
                <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                <asp:HiddenField ID="tblH" runat="server" />
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
