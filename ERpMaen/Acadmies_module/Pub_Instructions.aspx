<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Pub_Instructions.aspx.vb" MasterPageFile="~/Site.Master" Inherits="ERpMaen.Pub_Instructions" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        <Services> 
            <asp:ServiceReference Path="~/ASMX_WebServices/Pub_InstructionsCls.asmx" />
       <asp:ServiceReference Path="~/ASMX_WebServices/WebService.asmx" />
            <asp:ServiceReference Path="~/ASMX_WebServices/MultiFileUploader.asmx" />

        </Services>
    </asp:ScriptManager>

 <style>

         .event-list {
		list-style: none;
		font-family: 'Lato', sans-serif;
		margin: 0px;
		padding: 0px;
	}
         .event-list > li > a > img {
		width: 100%;
	}
         .event-list > li > a > img  {
			display: inline-block;
		}
	.event-list > li {
		background-color: rgb(255, 255, 255);
		box-shadow: 0px 0px 5px rgb(51, 51, 51);
		box-shadow: 0px 0px 5px rgba(51, 51, 51, 0.7);
		padding: 0px;
		margin: 0px 0px 20px;
	}
	.event-list > li > time {
		display: inline-block;
		width: 100%;
		color: rgb(255, 255, 255);
		background-color: rgb(197, 44, 102);
		padding: 5px;
		text-align: center;
		text-transform: uppercase;
	}
	.event-list > li:nth-child(even) > time {
		background-color: rgb(165, 82, 167);
	}
	.event-list > li > time > span {
		display: none;
	}
	.event-list > li > time > .day {
		display: block;
		font-size: 56pt;
		font-weight: 100;
		line-height: 1;
	}
	.event-list > li time > .month {
		display: block;
		font-size: 24pt;
		font-weight: 900;
		line-height: 1;
	}
	.event-list > li > a > img {
		width: 100%;
	}
	.event-list > li > .info {
		padding-top: 5px;
		text-align: center;
	}
	.event-list > li > .info > .title {
		font-size: 17pt;
		font-weight: 700;
		margin: 0px;
	}
	.event-list > li > .info > .desc {
		font-size: 13pt;
		font-weight: 300;
		margin: 0px;
	}
	.event-list > li > .info > ul,
	.event-list > li > .social > ul {
		display: table;
		list-style: none;
		margin: 10px 0px 0px;
		padding: 0px;
		width: 100%;
		text-align: center;
	}
	.event-list > li > .social > ul {
		margin: 0px;
	}
	.event-list > li > .info > ul > li,
	.event-list > li > .social > ul > li {
		display: table-cell;
		cursor: pointer;
		color: rgb(30, 30, 30);
		font-size: 11pt;
		font-weight: 300;
        padding: 3px 0px;
	}
    .event-list > li > .info > ul > li > a {
		display: block;
		width: 100%;
		color: rgb(30, 30, 30);
		text-decoration: none;
	} 
    .event-list > li > .social > ul > li {    
        padding: 0px;
    }
    .event-list > li > .social > ul > li > a {
        padding: 3px 0px;
	} 
	.event-list > li > .info > ul > li:hover,
	.event-list > li > .social > ul > li:hover {
		color: rgb(30, 30, 30);
		background-color: rgb(200, 200, 200);
	}
	.facebook a,
	.twitter a,
	.google-plus a {
		display: block;
		width: 100%;
		color: rgb(75, 110, 168) !important;
	}
	.twitter a {
		color: rgb(79, 213, 248) !important;
	}
	.google-plus a {
		color: rgb(221, 75, 57) !important;
	}
	.facebook:hover a {
		color: rgb(255, 255, 255) !important;
		background-color: rgb(75, 110, 168) !important;
	}
	.twitter:hover a {
		color: rgb(255, 255, 255) !important;
		background-color: rgb(79, 213, 248) !important;
	}
	.google-plus:hover a {
		color: rgb(255, 255, 255) !important;
		background-color: rgb(221, 75, 57) !important;
	}

	@media (min-width: 768px) {
		.event-list > li {
			position: relative;
			display: block;
			width: 100%;
			height: 120px;
			padding: 0px;
		}
		.event-list > li > time,
		.event-list > li > a > img  {
			display: inline-block;
		}
		.event-list > li > time,
		.event-list > li > a > img {
			width: 120px;
			float: right;
		}
		.event-list > li > .info {
			background-color: rgb(245, 245, 245);
			overflow: hidden;
		}
		.event-list > li > time,
		.event-list > li > a > img {
			width: 120px;
			height: 120px;
			padding: 0px;
			margin: 0px;
		}
		.event-list > li > .info {
			position: relative;
			height: 120px;
			text-align: right;
			padding-left: 40px;
		}	
		.event-list > li > .info > .title, 
		.event-list > li > .info > .desc {
			padding: 0px 10px;
		}
		.event-list > li > .info > ul {
			position: absolute;
			left: 0px;
			bottom: 0px;
		}
		.event-list > li > .social {
			position: absolute;
			top: 0px;
			left: 0px;
			display: block;
			width: 40px;
		}
        .event-list > li > .social > ul {
            border-right: 1px solid rgb(230, 230, 230);
        }
		.event-list > li > .social > ul > li {			
			display: block;
            padding: 0px;
		}
		.event-list > li > .social > ul > li > a {
			display: block;
			width: 40px;
			padding: 10px 0px 9px;
		}
	}
         /*.form-control { direction:rtl;
                    }*/
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
            border-top-right-radius: 7px;
            border-top-left-radius: 7px;
        }

        .block {
           box-shadow: 0 7px 3px 0 rgba(0, 0, 0, 0.2), 0 6px 9px 0 rgba(0, 0, 0, 0.19);
           margin-bottom:25px;
            border-radius:7px;
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
                    <script src="../JS_Code/acadmies/Pub_Instructions.js"></script>
                                <label hidden runat="server" id="inst_edit"></label>
                                <label hidden runat="server" id="inst_del"></label>

         
                </div>

        <section>
            <div class="row">

                 <label style="display:none" id="lblInstruction_id" ></label>
                <div class="col-md-12 col-sm-12 col-xs-12 ">
                    <div class="white-block row">
                        <div class="col-md-4">
                            <h3>
                                <i class="fa fa-book"></i>
                                <span>  التعاميم</span>
                            </h3>
                        </div>

                        <div class="col-md-6" >
                            <input  id="txt_Search" onkeyup="searchCourses();" type="text" class="form-control" placeholder="بحث  " />
                        </div>
                        <div class="col-md-2">

                            <div class="btn-group pull-left">
                                      <% If ERpMaen.LoginInfo.get_form_operation("64") = True Or ERpMaen.LoginInfo.get_form_operation_group("64") = True Then

                                              %>
                                <button type="button" class="btn btn-info " data-toggle="modal" data-target="#addInstruction" >اضافة تعميم <i class="fa fa-plus"></i></button>
                                <% End If %>
                            </div>
                        </div>
                    </div>

                 
                </div>
            </div>
            
        </section>
        <section>
            <div class="container">
		<div class="row">

             <div class="col-md-12" style="text-align: center; ">
							<div class="btn-group">
							
								<button type="button" class="btn btn-success"  style="width:100px; float: right;" onclick="drawCourses(0);">جديدة</button>
								<button type="button" class="btn btn-primary"  style="width:100px; float: right;" onclick=" drawCourses(1);">ارشيف</button>
                                	<button type="button" class="btn btn-secondary" style="width:100px; float: right;" onclick="drawAllCourses();">الكل</button>
			
                                
                              <br />
                                <br />
                                <br />
							</div>
						</div>
			<div class="[ col-xs-12 col-md-12 col-sm-8 ]">
				<ul class="event-list" id="Instructions-list">
				

					

				</ul>
			</div>
		</div>
	</div>
           <%-- <div class="row" style="margin-left:5px;margin-right:10px;">
                <div class="col-md-12 col-sm-12 col-xs-12 " >

                </div>

            </div>--%>
        </section>
        <div class="widget-navigation">
            <ul class="pagination">
                <li class="paginate_button previous"><a>السابق</a></li>
                <li class="paginate_button next" id="default-datatable_next"><a >التالي</a></li>
            </ul>
        </div>
        </div>
        <div class="modal" id="addInstruction" tabindex="-1" role="dialog"  aria-labelledby="modalLabel" aria-hidden="true" dir="rtl">
                 <div id="SavedivLoader" class="loader" style="display: none;  text-align: center;">
                 <asp:Image ID="img" runat="server" ImageUrl="../App_Themes/images/loader.gif" />

                         </div>
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">اضافة تعميم</h4>
                    </div>
                    <div class="modal-body">
                                        
                        <div id="divForm">
                            <div class="col-md-6">
                                      <div class=" row form-group ">
                                     <div class="col-md-3 col-sm-12">
                                         <label for="Name" class="label-required">العنوان  </label>
                                     </div>
                                     <div class="col-md-9 col-sm-12">
                                         <asp:TextBox SkinID="form-control" required class="form-control" dbColumn="title" ClientIDMode="Static" ID="inst_title" runat="server">
                                         </asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="inst_title"
                                             ErrorMessage="من فضلك أدخل العنوان   " ValidationGroup="vgroup"></asp:RequiredFieldValidator>

                                     </div>
                                 </div>

                                 <div class="row form-group">
                                <div class="col-md-3 col-sm-12">
                                    <label for="Name" >تفاصيل التعميم </label>

                                </div>
                                <div class="col-md-9 col-sm-12">
                                    <asp:TextBox SkinID="form-control" TextMode="multiline" required class="form-control" dbColumn="description" ClientIDMode="Static" ID="description" runat="server">
                                    </asp:TextBox>

                                </div>
                            </div>
                           
                                 </div>
                             <div class="col-md-6">
                         <%--   <div class=" row form-group">
                                            <div class="col-md-3 col-sm-12">
                                                <label class="">مسنده الى     </label>
                                            </div>

                                            <div class="col-md-9 col-sm-12" >
                                                <select class="form-control" dbcolumn="User_Type">
                                                    <option value="8">طلاب</option>
                                                    <option value="4">مدربين </option>
                                                     <option value="5">موظفين </option>
                                                     <option value="4">مدربين </option>
                                                    <option value="2">لكل</option>
                                                   
                                                </select>


                                            </div>
                                        </div>--%>

                                 <div class="row form-group ">
                                <div class="col-md-3 col-sm-12">
                                    <label class="label-required">مسنده الى  </label>
                                </div>

                                <div class="col-md-9 col-sm-12">
                                    <asp:DropDownList dbcolumn="User_Type" class="form-control" required ClientIDMode="Static" ID="lblUsers" runat="server">
                                    </asp:DropDownList>
                                   
                                </div>
                            </div>

                            <div class=" row form-group ">
                                <div class="col-md-3 col-sm-12">
                                                <label class="">المرفق </label>
                                            </div>
                                    
                                         <div class="col-md-9 col-sm-12" >
                                        <input id="fileURL" type="hidden"  dbcolumn="image" runat="server" />
                                        <input id="FName" type="text" required readonly="readonly" runat="server" />

                                    </div>
                                    <div class="clear">
                                    </div>
                                    <asp:AsyncFileUpload ID="fuFile1" SkinID="image-upload" runat="server" OnUploadedComplete="PhotoUploaded"
                                        OnClientUploadComplete="UploadComplete2" />
                                </div>
                           
                              </div>
                             </div>
                                 </div>

                   
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary"  onclick="saveInstruction();">حفظ </button>
                </div>
                </div>
            </div>
        </div>
    
   
</asp:Content>
