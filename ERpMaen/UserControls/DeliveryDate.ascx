<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DeliveryDate.ascx.vb" Inherits="ERpMaen.DeliveryDate" %>
<%@ Register Src="~/UserControls/Person.ascx" TagPrefix="uc1" TagName="Person" %>
<style>
    table th {
        text-align:right;
    }
</style>
<div class="collapse" id="receiving_delivery_details" dir="rtl">
      <button class="btn btn-info btn-lg pull-left" onclick="getReceive_and_deliver(); return false;">تقرير استلام وتسليم</button>
                                         
    <div class="card card-body">
        <%-- start group 1--%>
        <ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#menu0">اختر الحالة</a></li>
    <li><a data-toggle="tab" href="#menu1">المسلم و المستلم</a></li>
    <li><a data-toggle="tab" href="#menu2" id="child_info">الاولاد</a></li>
    <li><a data-toggle="tab" href="#menu3" style="display: none" id="money_data"> المبلغ</a></li>
    <li><a data-toggle="tab" href="#menu4"> محضر الاستلام</a></li>
  </ul>

  <div class="tab-content">
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
                <asp:DropDownList onchange="define_type()" dbcolumn="type" required SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddltype" runat="server">
                    <asp:ListItem Value="1">تسليم واستلام الاولاد </asp:ListItem>
                    <asp:ListItem Value="2">تسليم واستلام النفقة</asp:ListItem>
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
                <asp:DropDownList SkinID="form-control" class="form-control" dbColumn="employee_id" ClientIDMode="Static" ID="ddlemployee_id" runat="server">
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
                <label for="Name" class="label-required">ملاحظات </label>

            </div>
            <div class="col-md-9 col-sm-12">
                <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextBox20" runat="server">
                </asp:TextBox>


            </div>
        </div>
            </div>
            </div>
      </div>
     
     

      
        <div class="col-md-12 form-group" id="save_delivery_details">
            <button onclick="save_delivery_details();" style="font-family: DroidKufi !important;" class="btn btn-success pull-left">حفظ</button>
        </div>

        <%--end of group1--%>
    </div>

</div>
<uc1:Person runat="server" ID="Person" />
