<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Archived_DiplomaCourses.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.Archived_DiplomaCourses" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Result.ascx" TagPrefix="uc1" TagName="Result" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services> 
            <asp:ServiceReference Path="~/ASMX_WebServices/Archived_Diploma_CoursesCls.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/MultiFileUploader.asmx" />
        </Services>
    </asp:ScriptManager>

 

    <style>
        .wrap {
            margin-top: 50px;
            direction: rtl;
        }
        .btn {
            float:left;
        }

        .widget-navigation {
            text-align: center;
        }

        .white-block {
            background: white;
            border: 2px solid white;
            border-radius: 4px;
            padding: 10px;
            margin: 10px 20px 10px 20px;
        }

        .block-title {
            color: #fff;
            background-color: #3b3e47;
            border-color: #f5f7f9;
            padding: 16px;
            border-bottom: 1px solid transparent;
            border-top-right-radius: 3px;
            border-top-left-radius: 3px;
        }

        .block {
            margin-bottom: 15px;
        }

        .block-title a {
            color: white;
        }

            .block-title a:hover {
                color: #428bca;
                cursor: pointer;
            }

        .block-desc {
            background: white;
            border: 2px solid white;
            border-bottom-right-radius: 4px;
            border-bottom-left-radius: 4px;
            padding: 10px;
        }

        .desc-inner {
            margin: 10px
        }

        p.desc {
            border-bottom: 1px dashed;
            padding: 5px;
            font-weight: normal;
            font-size: 12px;
        }

        img.avatar {
            border-radius: 100% !important;
            width: 30px;
            height: 30px;
            margin-left: 9px;
        }

        .btn-group {
            margin: 0px !important;
            width: auto;
        }
        /*popup edit*/
        .modal-dialog {
            
    position: absolute;
    width: 70%; 
    height: auto;
    left:15%;
   
    border: solid 2px #cccccc;
    background-color: #ffffff;
  
        }
       


.modal-body {
    padding: 15px;
    display: -webkit-box;
}
          
    </style>


    <div class="wrap">
         <div>
                    <script src="../JS_Code/acadmies/Archived_diplomaCourses.js"></script>
                </div>

        <section>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 ">
                    <div class="white-block row">
                        <div class="col-md-4">
                             <h3>
                                <i class="fa fa-book"></i>

                                <span id="diplome_title">  </span>
                            </h3>
                        </div>
                        <div class="col-md-6" >
                            <input  id="txt_Search" onkeypress="searchCourses();" type="text" class="form-control" placeholder="بحث عن دورة" />
                        </div>
                        <div class="col-md-2">


                        </div>
                    </div>
                </div>

                 
            </div>
        </section>
        <section>
            <div class="row">
                <label style="display:none" id="Lbldeploma_id" runat="server" ></label>
                <label  style="display:none" id="lbldiplomecode" runat="server" ></label>
                <div class="col-md-12 col-sm-12 col-xs-12 " id="courses-list">

                </div>
            </div>
        </section>
        <div class="widget-navigation">
            <ul class="pagination">
                <li class="paginate_button previous"><a>السابق</a></li>
                <li class="paginate_button next" id="default-datatable_next"><a>التالي</a></li>
            </ul>
        </div>

        
        </div>


   
</asp:Content>

