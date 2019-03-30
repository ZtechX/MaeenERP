<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Person.ascx.vb" Inherits="ERpMaen.Person" %>
<div class="collapse" id="collapseExample" dir="rtl">
    <div class="card card-body">
        <fieldset style="border: 4px groove white; border-radius: 8px; background: #f1f0f0;">
            <legend>اضافة جديد</legend>
            <asp:Label runat="server" ID="lblnew_person" Style="display: none" dbcolumn=""></asp:Label>
            <asp:Label runat="server" ID="lbldiv_person" Style="display: none" ></asp:Label>
            <asp:Label runat="server" ID="lbldiv_type" Style="display: none" ></asp:Label>
            <div class="col-md-12 form-group ">
                <div class="col-md-3 col-sm-12">
                    <label for="Name" class="label-required">الاسم</label>

                </div>
                <div class="col-md-9 col-sm-12">
                    <asp:TextBox required SkinID="form-control" class="form-control" dbColumn="name" ClientIDMode="Static" ID="person_name" runat="server">
                    </asp:TextBox>

                </div>
            </div>
            <div class="col-md-12 form-group ">
                <div class="col-md-3 col-sm-12">
                    <label class="label-required">صلة القرابة</label>
                </div>

                <div class="col-md-9 col-sm-12">
                    <asp:DropDownList required dbcolumn="relationship_id" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlrealtion" runat="server">
                    </asp:DropDownList>

                </div>
            </div>
              <div class="col-md-12 form-group ">
                <div class="col-md-3 col-sm-12">
                    <label class="label-required">
                    رقم الجوال   </label>
                </div>

                <div class="col-md-9 col-sm-12">
                    <input onkeypress="return cust_chkNumber(event,this,10);" dbcolumn="phone" type="text" id="txt_phone"
                        class="form-control" runat="server" clientidmode="Static" required/>

                    <br />
                </div>
            </div>
            <div class="col-md-12 form-group ">
                <div class="col-md-3 col-sm-12">
                    <label class="label-required">
                    رقم الهوية  </label>
                </div>

                <div class="col-md-9 col-sm-12">
                    <input required onkeypress="return cust_chkNumber(event,this,10);" dbcolumn="indenty" type="text" id="txt_indenty"
                        class="form-control" runat="server" clientidmode="Static" />

                    <br />
                </div>
            </div>
            <div class="col-md-12 form-group ">
                <div class="col-md-3 col-sm-12">
                    <label >
                    رقم الوكالة  </label>
                </div>

                <div class="col-md-9 col-sm-12">
                    <input onkeypress="return isNumber(event);" dbcolumn="authorization_no" type="text" id="Text2"
                        class="form-control" runat="server" clientidmode="Static" />

                    <br />
                </div>
            </div>
            <div class="col-md-12 form-group">
                <button onclick="save_new_person(); return false" style="font-family: DroidKufi !important;" class="btn btn-success pull-left">حفظ</button>
            </div>
        </fieldset>
    </div>
</div>
