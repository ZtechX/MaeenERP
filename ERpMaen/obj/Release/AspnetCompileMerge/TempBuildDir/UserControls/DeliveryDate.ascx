<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DeliveryDate.ascx.vb" Inherits="ERpMaen.DeliveryDate" %>
<%@ Register Src="~/UserControls/Person.ascx" TagPrefix="uc1" TagName="Person" %>
<style>
    table th {
        text-align:right;
    }
</style>
 <script type="text/javascript" src="../Clock/ng_all.js"></script>
<script type="text/javascript" src="../Clock/ng_ui.js"></script>
<script type="text/javascript" src="../Clock/components/timepicker.js"></script>

<script type="text/javascript">
    ng.ready(function () {
     
            var tp2 = new ng.TimePicker({
            input: 'txtentry_time',  // the input field id
            start: '12:00 am',  // what's the first available hour
            end: '11:00 pm',  // what's the last avaliable hour
            top_hour: 12  // what's the top hour (in the clock face, 0 = midnight)
        });
        //   var tp3 = new ng.TimePicker({
        //    input: 'txtentry_time',  // the input field id
        //    start: '12:00 am',  // what's the first available hour
        //    end: '11:00 pm',  // what's the last avaliable hour
        //    top_hour: 12  // what's the top hour (in the clock face, 0 = midnight)
        //});
         var tp4= new ng.TimePicker({
            input: 'txtexite_time',  // the input field id
            start: '12:00 am',  // what's the first available hour
            end: '11:00 pm',  // what's the last avaliable hour
            top_hour: 12  // what's the top hour (in the clock face, 0 = midnight)
        });
        
    });
</script>
<div class="collapse" id="receiving_delivery_details" dir="rtl">
          <div id="SavedivLoader" class="loader" style="    position:absolute; padding-top:15%;  text-align: center; z-index:1000;">
                  <asp:Image ID="img" runat="server" ImageUrl="../App_Themes/images/loader.gif" />
                </div>
    <div class="row">
      <button class="btn btn-info btn-lg pull-left" onclick="getReceive_and_deliver(); return false;" style="margin-right:10px;">تقرير استلام وتسليم</button>
               <button class="btn btn-info btn-lg pull-left" onclick="getagreement(); return false;">طباعة اتفاق تنفيذ استلام وتسليم</button>
          </div>                                
    <div class="card card-body">
        <%-- start group 1--%>
        <ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#menu0">اختر الحالة</a></li>
    <li><a data-toggle="tab" id="receiv_deliver" href="#menu1">المسلم و المستلم</a></li>
    <li><a data-toggle="tab" href="#menu2" id="child_info">الاولاد</a></li>
    <li><a data-toggle="tab" href="#menu3" style="display: none" id="money_data"> المبلغ</a></li>
    <li><a data-toggle="tab" href="#menu6" style="display: none" id="person_data"> المرافقين</a></li>
    <li><a data-toggle="tab" href="#menu7" style="display: none" id="session_data"> تفاصيل الجلسة</a></li>
    <li><a data-toggle="tab" href="#menu08" style="display:none " id="corresponde_data"> نوع الاجراء</a></li>
    <li><a data-toggle="tab" id="record_case" href="#menu4"> محضر الاستلام</a></li>
    <li><a data-toggle="tab" id="ready_case" href="#menu5"> جاهزية الاستلام والتسليم</a></li>
  </ul>

  <div class="tab-content">
    <%--  <div id="receiving_delivery_details">--%>
    <div id="menu0" class="tab-pane fade in active">
        <div class="row" style="padding:40px">
        <asp:Label ID="lbldelivery_details" runat="server" Style="display: none" dbcolumn="id"></asp:Label>
        <asp:label id="lblcase_id" clientidmode="static" runat="server" style="display: none" dbcolumn=""></asp:label>
        <asp:label id="lblcell_id" clientidmode="static" runat="server" style="display: none" dbcolumn=""></asp:label>
        <asp:label id="lbldelivery_date_m" runat="server" dbcolumn="date_m" style="display: none"></asp:label>
        <asp:label id="lbldelivery_date_h" runat="server" dbcolumn="date_h" style="display: none"></asp:label>
         
       
        <div class="col-md-12 form-group ">
            <div class="col-md-3 col-sm-12">
                <label class="label-required">الحالات</label>
            </div>

            <div class="col-md-9 col-sm-12">
                <asp:DropDownList dbcolumn="case_id" onchange='show_calender_details();' required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlcase_id" runat="server">
                    <asp:ListItem Value="0">اختر</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-md-12 form-group ">
            <div class="col-md-3 col-sm-12">
                <label class="label-required">النوع</label>
            </div>

            <div class="col-md-9 col-sm-12">
                <asp:DropDownList onchange="define_type('',1)" dbcolumn="type" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddltype" runat="server">
                    <asp:ListItem Value="1">تسليم واستلام الاولاد </asp:ListItem>
                    <asp:ListItem Value="2">تسليم واستلام النفقة</asp:ListItem>
                    <asp:ListItem Value="3">جلسات التهيئة والتدرج</asp:ListItem>
                    <asp:ListItem Value="4">اجرائات العضو المباشر للحالة</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
        </div>
      <div id="menu1" class="tab-pane fade">
           <div class="row" style="padding:40px">
        <div class="col-md-12 form-group ">
            <div class="col-md-3 col-sm-12">
                <label class="label-required">بيانات المسلم </label>
            </div>

            <div class="col-md-9 col-sm-12">
                <asp:DropDownList runat="server" dbcolumn="deliverer_id" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddldeliverer_id">
                    <asp:ListItem Value="0">اختر</asp:ListItem>
                </asp:DropDownList>
                <button class="btn btn-primary" type="button" onclick="find_persons('ddldeliverer_id',1)">اضافة مسلم </button>
                 <button class="btn btn-primary pull-left"" type="button" onclick="getReview('deliverer_id')">طباعة إفادة مراجعة </button>
           
            </div>
        </div>


        <%--end of group1--%>
        <div class="col-md-12 form-group ">
            <div class="col-md-3 col-sm-12">
                <label class="label-required">بيانات المستلم </label>
            </div>

            <div class="col-md-9 col-sm-12">
                <asp:DropDownList dbcolumn="reciever_id" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlreciever_id" runat="server">
                    <asp:ListItem Value="0">اختر</asp:ListItem>
                </asp:DropDownList>
                <button class="btn btn-primary" type="button" onclick="find_persons('ddlreciever_id',1)">اضافة مستلم </button>
           <button class="btn btn-primary pull-left"" type="button" onclick="getReview('reciever_id')">طباعة إفادة مراجعة </button>
                </div>
        </div>
          </div>
          </div>
      <div id="menu2" class="tab-pane fade">
           <div class="row" style="padding:40px">
    
            <div class="col-md-12 col-sm-12  tbl_auto">
                <table dir="rtl" class="table table-borderd">
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>اسم الولد</th>
                            <th class="checkbox-all"><span>
                                <input type="checkbox" id="check_child" onchange="mark_all(this,'tab_children')">الكل</span></th>
                        </tr>

                    </thead>
                    <tbody id="tab_children">
                    </tbody>

                </table>
            </div>

          </div>
          </div>
      <div id="menu3" class="tab-pane fade">
          <div class="row" style="padding:40px">
            <div class=" col-md-12 form-group" >

            <div class="col-md-3 col-sm-12">
                <label class="label-required">
                المبلغ    </lable>
            </div>

            <div class="col-md-9 col-sm-12">
                <input onkeypress="return isNumber(event);" dbcolumn="amount" type="text" id="Text11"
                    class="form-control" runat="server" clientidmode="Static" />
            </div>
        </div>
          </div>
          </div>
        <div id="menu4" class="tab-pane fade">
            <div class="row" style="padding:40px">
        <div class="col-md-12 form-group ">
            <div class="col-md-3 col-sm-12">
                <label for="Name" class="label-required">الموظف المسؤل</label>

            </div>
            <div class="col-md-9 col-sm-12">
                <asp:DropDownList SkinID="form-control" class="form-control" required dbColumn="employee_id" ClientIDMode="Static" ID="ddlemployee_id" runat="server">
                </asp:DropDownList>
            </div>



        </div>
        <div class="col-md-9 col-sm-12">
            <asp:LinkButton OnClientClick="getProceedingReps(DeliveryProceeding); return false;" ID="LinkButton1" runat="server"
                SkinID="btn-top pull-left" CssClass="pull-left" CausesValidation="false">
                                                                  <i class="fa fa-print"></i>
                                                                   طباعة محضر الاستلام
            </asp:LinkButton>
        </div>

        <div class="col-md-9 col-sm-12" id="receiving_done">

            <div class="col-md-3 col-sm-12">
                <label for="email">تم الاستلام والتسليم </label>
            </div>
            <div class="col-md-3 col-sm-12">
                <input dbcolumn="receiving_delivery_done" type="checkbox" id="doneId" style="width: 40px;" class="form-control" />
                <br />
            </div>
        </div>
        <div class="col-md-9 col-sm-12">
            <br />
            <div class="col-md-3 col-sm-12">
                <label for="email">تمت الموافقة بالجوال   </label>
            </div>
            <div class="col-md-3 col-sm-12">
                <input dbcolumn="moble_app_accept" type="checkbox" id="txtmoble_app_accept" style="width: 40px;" class="form-control" />

            </div>
        </div>
        <div class="col-md-12 form-group ">
            <br />
            <div class="col-md-3 col-sm-12">
                <label for="Name" >ملاحظات </label>

            </div>
            <div class="col-md-9 col-sm-12">
                <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextBox20" runat="server">
                </asp:TextBox>


            </div>
        </div>
            </div>
            </div>
        <div id="menu5" class="tab-pane fade">
            <div class="row" style="padding:40px">
                  <div class="col-md-12 ">
                         <div class="col-md-6 form-group" style="pointer-events: none;" >
                                            <div class="col-md-1 col-sm-12" >

                                                <label class="switch switch-success">
                                                    <input  id="deliverer_ready"  runat="server"  type="checkbox" />
                                                    
                                                </label>
                                            </div>
                                                 <div class="col-md-2 col-sm-12" style="margin-top: 23px;">
                                                <label>المسلم جاهز</label>

                                            </div>
                                           
                                        </div>
                  <div class="col-md-6 form-group" style="pointer-events: none;" >
                                            <div class="col-md-1 col-sm-12" >

                                                <label class="switch switch-success">
                                                    <input  id="deliverer_accept" runat="server"  type="checkbox" />
                                                    
                                                </label>
                                            </div>
                                                 <div class="col-md-2 col-sm-12" style="margin-top: 23px;">
                                                <label>المسلم موافق</label>

                                            </div>
                                           
                                        </div>

                  </div>
               
                  <div class="col-md-12 ">
                         <div class="col-md-6 form-group" style="pointer-events: none;" >
                                            <div class="col-md-1 col-sm-12" >

                                                <label class="switch switch-success">
                                                    <input  id="reciever_ready"  runat="server"  type="checkbox" />
                                                    
                                                </label>
                                            </div>
                                                 <div class="col-md-2 col-sm-12" style="margin-top: 23px;">
                                                <label>المستلم جاهز</label>

                                            </div>
                                           
                                        </div>
                  <div class="col-md-6 form-group" style="pointer-events: none;" >
                                            <div class="col-md-1 col-sm-12" >

                                                <label class="switch switch-success">
                                                    <input  id="reciever_accept"  runat="server"  type="checkbox" />
                                                    
                                                </label>
                                            </div>
                                                 <div class="col-md-2 col-sm-12" style="margin-top: 23px;">
                                                <label>المستلم موافق</label>

                                            </div>
                                           
                                        </div>

                  </div>
                <div class="col-md-12 form-group" style=" margin-right:auto;margin-left:auto;">
                                            <div class="col-md-1 col-sm-12" >

                                                <label class="switch switch-success">
                                                    <input id="receiving_delivery_done" runat="server"  type="checkbox" />
                                                    
                                                </label>
                                            </div>
                                                 <div class="col-md-2 col-sm-12" style="margin-top: 23px;">
                                                <label>موافقة الادارة</label>

                                            </div>
                                           
                                        </div>
            </div>
            </div>
          <%--</div>--%>
          <div id="menu6" class="tab-pane fade">
          <div class="row" style="padding:40px">
              <div class="col-md-12 form-group ">
                  <div class="col-md-3 col-sm-12">
                      <label for="Name" class="label-required">المرافقيين</label>

                  </div>
                  <div class="col-md-9 col-sm-12 tbl_auto">
                      <table dir="rtl" class="table table-bordered">

                          <thead>
                              <tr>

                                  <th>#</th>
                                  <th>الاسم</th>
                                  <th class="checkbox-all"><span>
                                      <input type="checkbox" id="check_child2" onchange="mark_all(this,'persons_sessions')">الكل</span></th>

                              </tr>
                          </thead>
                          <tbody id="persons_sessions">
                              <tr>
                              </tr>
                          </tbody>


                      </table>
                  </div>
                  <button class="btn btn-primary" type="button" onclick="find_persons('persons_sessions',2)">اضافة مرافق </button>
              </div>
          </div>
          </div>
      <div id="menu7" class="tab-pane fade">
          <div class="row" style="padding:40px" id="case_sessions">
               <asp:label id="lbldelivery_date_m1" runat="server" dbcolumn="date_m" style="display: none"></asp:label>
        <asp:label id="lbldelivery_date_h1" runat="server" dbcolumn="date_h" style="display: none"></asp:label>
               <asp:Label ID="lbl_sessions_id" ClientIDMode="static" runat="server" Style="display: none" dbcolumn="id"></asp:Label>
              <div class="col-md-12 form-group ">
                  <div class="col-md-3 col-sm-12">
                      <label>
                      رقم الجلسة  </lable>
                  </div>

                  <div class="col-md-9 col-sm-12">
                      <input onkeypress="return isNumber(event);" disabled dbcolumn="code" type="text" id="txtcode_sessions"
                          class="form-control" runat="server" clientidmode="Static" />

                      <br />
                  </div>
              </div>
              <div class="col-md-12 form-group ">
                  <div class="col-md-3 col-sm-12">
                      <label for="Name" class="label-required">مكان الجلسة</label>

                  </div>
                  <div class="col-md-9 col-sm-12">
                      <asp:TextBox SkinID="form-control" required class="form-control" dbColumn="place" ClientIDMode="Static" ID="txtplace" runat="server">
                      </asp:TextBox>

                  </div>
              </div>
              <div class="col-md-12 form-group ">
                  <div class="col-md-3 col-sm-12">
                      <label class="label-required">
                      وقت الدخول </lable>
                  </div>

                  <div class="col-md-9 col-sm-12" id="time_entry_time">
                      <input onkeypress="return isNumber(event);" readonly dbcolumn="entry_time" type="time" id="txtentry_time"
                          class="form-control" runat="server" clientidmode="Static" />

                      <br />
                  </div>
              </div>
              <div class="col-md-12 form-group ">
                  <div class="col-md-3 col-sm-12">
                      <label class="label-required">
                      وقت الخروج </lable>
                  </div>

                  <div class="col-md-9 col-sm-12" id="time_exite_time">
                      <input onkeypress="return isNumber(event);" dbcolumn="exite_time" type="time" id="txtexite_time"
                          class="form-control" runat="server" readonly clientidmode="Static" />

                      <br />
                  </div>
              </div>

                <div class="col-sm-12 form-group">

            <div class="col-md-3 col-sm-12">
                <label for="email">تمت الموافقة </label>
            </div>
            <div class="col-md-3 col-sm-12">
                <input dbcolumn="session_done" type="checkbox" id="txtsession_done"  style="width: 40px;" class="form-control" />
                <br />
            </div>
        </div>

          </div>

            </div>
      <div id="menu08" class="tab-pane fade">
          <div class="row" style="padding:40px" id="case_correspondences">
                     <asp:label id="lbldelivery_date_m2" runat="server" dbcolumn="date_m" style="display: none"></asp:label>
        <asp:label id="lbldelivery_date_h2" runat="server" dbcolumn="date_h" style="display: none"></asp:label>
               <asp:Label ID="lblcorrespondences_id" ClientIDMode="static" runat="server" Style="display: none" dbcolumn="id"></asp:Label>

              <div class=" col-md-12 form-group ">

                  <div class="col-md-3 col-sm-12">
                      <label class="label-required">
                      رقم الاجراء  </lable>
                  </div>

                  <div class="col-md-9 col-sm-12">
                      <input onkeypress="return isNumber(event);" disabled dbcolumn="code" type="text" id="txtcode_correspondences"
                          class="form-control" runat="server" clientidmode="Static" />
                  </div>
              </div>
              <div class="col-md-12 form-group ">
                  <div class="col-md-3 col-sm-12">
                      <label class="label-required">الطرف الاخر </label>
                  </div>

                  <div class="col-md-9 col-sm-12">
                      <asp:DropDownList dbcolumn="person_id" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlperson_id" runat="server">
                      </asp:DropDownList>
                      <button class="btn btn-primary" type="button" onclick="find_persons('ddlperson_id',1)">اضافة طرف اخر </button>
                  </div>
              </div>
              <div class="col-md-12 form-group ">
                  <div class="col-md-3 col-sm-12">
                      <label class="label-required">نوع الاجراء </label>
                  </div>

                  <div class="col-md-9 col-sm-12">
                      <asp:DropDownList dbcolumn="type_correspondences" SkinID="form-control" required class="form-control" ClientIDMode="Static" ID="ddltype2" runat="server">
                      </asp:DropDownList>

                  </div>
              </div>

                 <div class="col-sm-12 form-group">

            <div class="col-md-3 col-sm-12">
                <label for="email">تمت الموافقة </label>
            </div>
            <div class="col-md-3 col-sm-12">
                <input dbcolumn="correspondence_done" type="checkbox" id="txtcorrespondence_done"  style="width: 40px;" class="form-control" />
                <br />
            </div>
        </div>

          </div>

      </div>
      </div>
     
     

      
        <div class="col-md-12 form-group" id="save_delivery_details">
            <button onclick="save_delivery_details()" style="font-family: DroidKufi !important;" class="btn btn-success pull-left">حفظ</button>
        </div>
         
        <div class="col-md-12 form-group" style="display:none" id="save_sessions">
            <button onclick="save_sessions()" style="font-family: DroidKufi !important;" class="btn btn-success pull-left">حفظ</button>
        </div>
          <div class="col-md-12 form-group" style="display:none"  id="save_correspondences">
            <button onclick="save_correspondences()" style="font-family: DroidKufi !important;" class="btn btn-success pull-left">حفظ</button>
        </div>

        <%--end of group1--%>
    </div>

</div>
<div class="collapse" id="multi_cases">
    <div class="row" style="max-height: 300px;">
        <button onclick="add_new_date();" class="btn btn-success">اضافة جديد</button>
            <div class="col-md-4 col-sm-12" dir="rtl">
                <asp:DropDownList onchange="search_type()" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlsearch_type" runat="server">
                    <asp:ListItem Value="0">بحث بالنوع</asp:ListItem>
                    <asp:ListItem Value="تسليم واستلام الاولاد">تسليم واستلام الاولاد </asp:ListItem>
                    <asp:ListItem Value="تسليم واستلام النفقة">تسليم واستلام النفقة</asp:ListItem>
                    <asp:ListItem Value="جلسات التهيئة والتدرج">جلسات التهيئة والتدرج</asp:ListItem>
                    <asp:ListItem Value="اجرائات العضو المباشر للحالة">اجرائات العضو المباشر للحالة</asp:ListItem>
                </asp:DropDownList>
            </div>
        <div class="col-md-4" dir="rtl">
            <input type="text" id="case_name" onkeyup="search_by_case();" placeholder="بحث بالحالة" class="form-control" />
        </div>

        <table class="table table-bordered">
            <thead>
                <tr>
                <th>#</th>
                <th>الحالة</th>
                <th>النوع</th>
                <th>مشاهدة</th>
                    </tr>
                 </thead>
                <tbody id="cases_dates">


                </tbody>
           

        </table>
    </div>
</div>
<uc1:Person runat="server" ID="Person" />
