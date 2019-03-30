<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Appraisal.ascx.vb" Inherits="ERpMaen.Appraisal" %>
    <style>
  .check {
  color: orange;
}
    </style>
<div class="collapse" id="Appraisal" dir="rtl">
    <div class="card card-body">
        <%-- start group 1--%>
        <ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#">التقييم</a></li>
  </ul>

  <div  class="tab-content">
      <div class="row" style="padding:40px">
        <asp:Label ID="lblitem_id" runat="server" Style="display: none" dbcolumn="id"></asp:Label>
        <asp:label id="lblcase_apprisal_id" clientidmode="static" runat="server" style="display: none" dbcolumn="case_id"></asp:label>
        <asp:label id="lbldetail_id" clientidmode="static" runat="server" style="display: none" dbcolumn="detail_id"></asp:label>
        <asp:label id="lbltype" clientidmode="static" runat="server" style="display: none" dbcolumn="type"></asp:label>
        <asp:label id="lblapprisal_date_m" runat="server" dbcolumn="date_m" style="display: none"></asp:label>
        <asp:label id="lblapprisal_date_h" runat="server" dbcolumn="date_h" style="display: none"></asp:label>

        <div class="col-md-12 form-group ">
            <div class="col-md-3 col-sm-12">
                <label class="label-required">المستوى</label>
            </div>

            <div class="col-md-9 col-sm-12">
                <input type='hidden' dbcolumn="value" class='star_test'><span onclick='rate(this,1)'; id='star1' class='fa fa-star'></span> <span id='star2' onclick='rate(this,2)'; class='fa fa-star'></span> <span id='star3' onclick='rate(this,3)'; class='fa fa-star'></span><span id='star4' onclick='rate(this,4)'; class='fa fa-star'></span><span id='star5' onclick='rate(this,5)'; class='fa fa-star'></span>
            </div>
        </div>

              <div class="col-md-12 form-group ">
            <br />
            <div class="col-md-3 col-sm-12">
                <label for="Name" >تعليق </label>

            </div>
            <div class="col-md-9 col-sm-12">
                <asp:TextBox SkinID="form-control" TextMode="multiline" class="form-control" dbColumn="notes" ClientIDMode="Static" ID="TextBox20" runat="server">
                </asp:TextBox>


            </div>
        </div>
    </div>
        

      </div>
     
     

      
        <div class="col-md-12 form-group" id="save_apprisal">
            <button onclick="save_apprisal();" style="font-family: DroidKufi !important;" class="btn btn-success pull-left">حفظ</button>
        </div>

        <%--end of group1--%>
    </div>

</div>
