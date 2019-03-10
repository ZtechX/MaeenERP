<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Person.ascx.vb" Inherits="ERpMaen.Person" %>
<div class="collapse" id="collapseExample">
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
                    <asp:TextBox SkinID="form-control" class="form-control" dbColumn="name" ClientIDMode="Static" ID="person_name" runat="server">
                    </asp:TextBox>

                </div>
            </div>
            <div class="col-md-12 form-group ">
                <div class="col-md-3 col-sm-12">
                    <label>صلة القرابة للمنفذ ضده</label>
                </div>

                <div class="col-md-9 col-sm-12">
                    <asp:DropDownList dbcolumn="relationship_id" SkinID="form-control" class="form-control" ClientIDMode="Static" ID="ddlrealtion" runat="server">
                    </asp:DropDownList>

                </div>
            </div>
            <div class="col-md-12 form-group ">
                <div class="col-md-3 col-sm-12">
                    <label class="label-required">
                    رقم هوية المنفذ  </label>
                </div>

                <div class="col-md-9 col-sm-12">
                    <input onkeypress="return isNumber(event);" dbcolumn="indenty" type="text" id="Text1"
                        class="form-control" runat="server" clientidmode="Static" />

                    <br />
                </div>
            </div>
            <div class="col-md-12 form-group ">
                <div class="col-md-3 col-sm-12">
                    <label class="label-required">
                    رقم الوكالة  </label>
                </div>

                <div class="col-md-9 col-sm-12">
                    <input onkeypress="return isNumber(event);" dbcolumn="authorization_no" type="text" id="Text2"
                        class="form-control" runat="server" clientidmode="Static" />

                    <br />
                </div>
            </div>
            <div class="col-md-12 form-group">
                <button onclick="save_new_person(); return false" class="btn btn-success">حفظ</button>
            </div>
        </fieldset>
    </div>
</div>
