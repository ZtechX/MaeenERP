<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConsultationDetails.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.ConsultDetails" Theme="Theme5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/DynamicTable.ascx" TagPrefix="uc1" TagName="DynamicTable" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>
<%@ Register Src="~/UserControls/PnlConfirm.ascx" TagPrefix="uc1" TagName="PnlConfirm" %>
<%@ Register Src="~/UserControls/CustomerCalendar.ascx" TagPrefix="uc1" TagName="HijriCalendar" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services>
            
            <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
             <asp:ServiceReference Path="~/ASMX_WebServices/ConsultationDetails.asmx" />
        </Services>
    </asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="update-progress02">
                <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="up">
                    <ProgressTemplate>
                        <asp:Image ID="imgProgress" runat="server" ImageUrl="~/App_Themes/images/ajax-loader.gif" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="wraper">
               
                <div>
                  
                    <script src="../JS_Code/ConsultationDetails/ConsultationDetails.js"></script>
                     <link href="../css/cases/cases.css" rel="stylesheet" />
                     <script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
                    <style>
                        .listbox {
                            margin-right:0px;
                        }
                    </style>
                </div>
                <div>
                    <div class="main-title">
                        <asp:Label ID="lblFormName" runat="server" Text="تفاصيل الاستشارات" SkinID="page_title"></asp:Label>
                      
                    </div>
                     <div class="col-md-12" >
                                 <asp:LinkButton OnClientClick="add(); return false;" ID="cmdAdd" runat="server"
                                                SkinID="btn-top" CausesValidation="false">
                                     <i class="fa fa-plus"></i>
                                           إنشاء استشارة 
                                            </asp:LinkButton>
                     </div>
                        
                        <uc1:PnlConfirm runat="server" ID="PnlConfirm" />
                  
                  
                    <div id="" class="newformstyle form_continer">
                        <div class="clear"></div>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="vgroup" />
                        <asp:Label ID="lblmainid" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="id"></asp:Label>
                        

                    </div>
    <div id="ConslutModal" class="modal fade" role="dialog">
  <div class="modal-dialog" style="width:90%;top:2%;">

    <!-- Modal content-->
    <div class="modal-content" style="overflow-y:auto ;height:-webkit-fill-available;overflow-x:hidden;" >
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <label class="modal-title">الاستشارة</label>
          
                                            <asp:LinkButton OnClientClick="save(); return false;" ID="LinkButton1" runat="server" style="float:right;"
                                                SkinID="btn-top" CausesValidation="false">
                                     <i class="fa fa-plus"></i>
                                           حفظ 
                                            </asp:LinkButton>
      </div>
      <div class="modal-body row" style="padding-top:0px;    direction: rtl;">
       <div id="divForm" class="newformstyle">
                       
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="vgroup" />
                        <asp:Label ID="consult_id" ClientIDMode="Static" Style="display: none" runat="server" dbColumn="id"></asp:Label>
                     
                                   <uc1:Result runat="server" ID="Result" />
                     
                     
                                            <div class="col-md-12">
                                                <div class="panel-group" id="accordion">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">بياناتا الاستشارة</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse1" class="panel-collapse collapse in">

                                                            <label id="lblcase_id" clientidmode="static" runat="server" style="display: none" dbcolumn=""></label>
                                                            <div class="panel-body" id="cases_info" >
                                                                <asp:Label ID="Label1" runat="server" ClientIDMode="static" Style="display: none" dbcolumn="id"></asp:Label>
                                                                <%-- start group 1--%>
                                   <div class="col-md-6">
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                            رقم الاستشارة  </lable>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" readonly="readonly" dbcolumn="code" type="text" id="txtNumber"
                                                class="form-control" runat="server" clientidmode="Static" />

                                            <br />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                             اسم الاستشارة  </lable>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <input  dbcolumn="consult_nm" type="text" id="Text1"
                                                class="form-control" runat="server" clientidmode="Static" />

                                            <br />
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group ">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                            تاريخ الاستشارة   </lable>
                                        </div>

                                        <div class="col-md-9 col-sm-12">

                                            <div class="fancy-form" id="divdate2">
                                                <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="start_date" ID="lblstart_date"></asp:Label>
                                                <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="start_date_hj" ID="lblstart_date_hj"></asp:Label>
                                                <uc1:HijriCalendar runat="server" ID="HijriCalendar3" />
                                            </div>

                                        </div>
                                    </div>
                                       </div>
<div class="col-md-6">
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">مصدر الاحالة</label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="source_id" class="form-control" ClientIDMode="Static" ID="ddlStatsrce" runat="server">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">تصنيف الحالة </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="category_id" class="form-control" ClientIDMode="Static" ID="ddlcategory" runat="server">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required"> حالة الاستشارة </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="status" class="form-control" ClientIDMode="Static" ID="ddlstatus" runat="server">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                       <div class="col-md-12 form-group ">
                                                                    <div class="col-md-3 col-sm-12">
                                                                        <label>ملاحظات  حول الاستشارة</label>

                                                                    </div>
                                                                    <div class="col-md-9 col-sm-12">
                                                                        <asp:TextBox  TextMode="multiline" class="form-control" dbColumn="income_notes" ClientIDMode="Static" ID="income_notes" runat="server" style="margin-right: 0px;">
                                                                        </asp:TextBox>


                                                                    </div>
                                                                </div>
                                </div>   
                                       
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">البيانات الشخصية</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse2" class="panel-collapse collapse">
                                                            <div class="panel-body" id="person_owner">

                                                                <%--stsrt group2--%>
                                                                     <div class="col-md-6">
                                                                <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required">الاسم الرباعى </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <input id="Name" type="text"  class="form-control" dbColumn="name"  runat="server"/>
                                       

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Name"
                                                ErrorMessage="من فضلك أدخل الاسم رباعى  " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                            <br />
                                        </div>
                                    </div>   
                                                                            <div class="col-md-12 form-group ">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                            تاريخ الميلاد   </lable>
                                        </div>

                                        <div class="col-md-9 col-sm-12">

                                            <div class="fancy-form" id="divdate4">
                                                <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="dob_date" ID="lbl_dob_m"></asp:Label>
                                                <asp:Label runat="server" ClientIDMode="static" Style="display: none" dbColumn="dob_date_hj" ID="lbl_dob_h"></asp:Label>
                                                <uc1:HijriCalendar runat="server" ID="HijriCalendar1" />
                                            </div>

                                        </div>
                                    </div>
                                    
                                                                           <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                            رقم الهوية  </lable>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="identiy" type="text" id="TextNUMId"
                                                class="form-control" runat="server" clientidmode="Static" />

                                            <br />
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label ">المستوى التعليمى   </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="education_level" class="form-control" ClientIDMode="Static" ID="ddleducation_level" runat="server">
                                            </asp:DropDownList>
                                            <br />
                                        </div>
                                    </div>   
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" >اسم ولى الامر  </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <input id="respnosible_name"  type="text"  class="form-control" dbColumn="respnosible_name"  runat="server"/>
                                         

                                            <br />
                                        </div>
                                    </div>
                                                                      
                                   <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">الحالة الاجتماعية </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="marital_status" class="form-control" ClientIDMode="Static" ID="ddlmaritalstat" runat="server">
                                            </asp:DropDownList>
                                            <br />
                                        </div>
                                    </div>
                                                <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                             عدد الابناء الذكور  </lable>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="boys_no" type="text" id="boysId"
                                                class="form-control" runat="server" clientidmode="Static" />

                                            <br />
                                        </div>
                                    </div>

                                              
                                    
                                                                         </div>
                                                                     <div class="col-md-6">
                                          <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label for="Name" class="label-required">العنوان </label>

                                        </div>
                                        <div class="col-md-9 col-sm-12">
                                            <input id="address" class="form-control" dbColumn="address"  runat="server"/>
                                          

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="address"
                                                ErrorMessage="من فضلك أدخل العنوان   " ValidationGroup="vgroup"></asp:RequiredFieldValidator>
                                            <br />
                                        </div>
                                    </div>
                                                                           <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">الجنسية </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">

                                            <input id="nationality"  runat="server" dbcolumn="nationality_id" class="form-control" />
                                            <br />
                                        </div>
                                    </div>
                                                                             
                                  
                                                                           <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                        <label  class="label-required">رقم الهاتف </label>

                                        </div>

                                         <div class="col-md-9 col-sm-12">
                                            <input id="tel" onkeypress="return isMobilePhoneFax(event);" dbcolumn="tel2" runat="server" type="text"  class="form-control" />
                                            <asp:RequiredFieldValidator CssClass="validator" ID="RequiredFieldValidator1" runat="server" ControlToValidate="tel"
                                                ErrorMessage="من فضلك ادخل رقم الهاتف " ValidationGroup="vgroup" ForeColor="red"></asp:RequiredFieldValidator>
                                             <br />
                                        </div>
                                    </div>
                                                                                                      
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">مستوى الدخل   </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="income_status" class="form-control" ClientIDMode="Static" ID="ddlincome_status" runat="server">
                                            </asp:DropDownList>
                                            <br />
                                        </div>
                                    </div>
                                     
                                                                            <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required"> الجنس </label>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <asp:DropDownList dbcolumn="gender" class="form-control" ClientIDMode="Static" ID="ddlgender" runat="server">
                                            </asp:DropDownList>
                                            <br />
                                        </div>
                                    </div>
                                                                           <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                             عدد الزوجات  </lable>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="wifes_no" type="text" id="wifesId"
                                                class="form-control" runat="server" clientidmode="Static" />

                                            <br />
                                        </div>
                                    </div>
                                                                           <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                             عدد الابناء الاناث  </lable>
                                        </div>

                                        <div class="col-md-9 col-sm-12">
                                            <input onkeypress="return isNumber(event);" dbcolumn="girls_no" type="text" id="girlsId"
                                                class="form-control" runat="server" clientidmode="Static" />

                                            <br />
                                        </div>
                                    </div>
                                               
                                        
</div>
                                                                <%--            end group2--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">بيانات السكن</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse3" class="panel-collapse collapse">
                                                            <div class="panel-body" id="person_against">
                                                                <%-- start group3--%>
                                               <div class="col-md-12">
                                               <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">نوع السكن  </label>
                                        </div>

                                        <div class="col-md-4 col-sm-12">
                                            <asp:DropDownList dbcolumn="house_type" class="form-control" ClientIDMode="Static" ID="ddlhouse_type" runat="server">
                                            </asp:DropDownList>
                                            <br />
                                        </div>
                                    </div>
                                                   </div>
                                                                <div class="col-md-12">
                                         <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">نوع الامتلاك  </label>
                                        </div>

                                        <div class="col-md-4 col-sm-12">
                                            <asp:DropDownList dbcolumn="type_of_ownership" class="form-control" ClientIDMode="Static" ID="ddltype_of_ownership" runat="server">
                                            </asp:DropDownList>
                                            <br />
                                        </div>
                                    </div>
                                                              </div>
                                                                <div class="col-md-12">
                                      

                                     <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                        <label  class="label-required">هاتف المنزل</label>

                                        </div>

                                         <div class="col-md-4 col-sm-12">
                                            <input onkeypress="return isMobilePhoneFax(event);" dbcolumn="house_tele" runat="server" type="text" id="house_tele" class="form-control" />
                                            <asp:RequiredFieldValidator CssClass="validator" ID="_tele" runat="server" ControlToValidate="house_tele"
                                                ErrorMessage="من فضلك ادخل هاتف المنزل " ValidationGroup="vgroup" ForeColor="red"></asp:RequiredFieldValidator>
                                             <br />
                                        </div>
                                    </div>
                                                                    </div>
                                                                    <%--end group3--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        
                    </div>
           </div>
    
    </div>

  </div>
</div>
 <div id="assignAdvisorModal" class="modal fade" role="dialog">
  <div class="modal-dialog" style="width:40%;top:20%;">

    <!-- Modal content-->
    <div class="modal-content" >
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" onclick="clearConslut_id()">&times;</button>
        <label class="modal-title">إسناد استشارة</label>
          
                                            <asp:LinkButton OnClientClick="addAdvisor(); return false;" ID="LinkButton2" runat="server" style="float:right;"
                                                SkinID="btn-top" CausesValidation="false">
                                     <i class="fa fa-plus"></i>
                                           حفظ 
                                            </asp:LinkButton>
      </div>
      <div class="modal-body row" >
          <uc1:Result runat="server" ID="Result1" />
          <div class="newformstyle" style="padding:25px;">
     <div class="form-group">
                                        <div class="col-md-3 col-sm-12">
                                            <label class="label-required">
                                            المستشارين  </lable>
                                        </div>

                                        <div class="col-md-7 col-sm-12">
                                           <asp:DropDownList  class="form-control" ClientIDMode="Static" ID="ddlAdvisors" runat="server">
                                            </asp:DropDownList>

                                            <br />
                                        </div>
                                    </div>
              </div>
          </div>
    
    </div>

  </div>
</div>

                    <uc1:DynamicTable runat="server" ID="DynamicTable" />
                    <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="tblH" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
