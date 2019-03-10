<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ImageSlider.ascx.vb" Inherits="ERpMaen.ImageSlider" %>
<asp:Panel runat="server" ID="pnlImageSlider" ClientIDMode="Static" Style="display: none">
    <div id="carousel-example-generic1" class="carousel slide" data-ride="carousel">
        <!-- Indicators -->
        <ol id="olImageSlider" runat="server" clientidmode="Static" class="carousel-indicators">
            <li data-target="#carousel-example-generic1" data-slide-to="0" class="active"></li>
            <li data-target="#carousel-example-generic1" data-slide-to="1"></li>
            <li data-target="#carousel-example-generic1" data-slide-to="2"></li>
        </ol>
        <!-- Wrapper for slides -->
        <div id="dvImageSlider" runat="server" clientidmode="Static" class="carousel-inner" role="listbox">
            <div class="item active">
                <img src="../images/watermark (5).jpg" alt="...">
            </div>
            <div class="item">
                <img src="../images/watermark (6).jpg" alt="...">
            </div>
            <div class="item">
                <img src="../images/watermark (6).jpg" alt="...">
            </div>
        </div>
        <!-- Controls -->
        <a class="left carousel-control" href="#carousel-example-generic1" role="button" data-slide="prev">
            <span class="fa fa-chevron-left" aria-hidden="true"></span>
            <span class="sr-only">السابق</span>
        </a>
        <a class="right carousel-control" href="#carousel-example-generic1" role="button" data-slide="next">
            <span class="fa fa-chevron-right" aria-hidden="true"></span>
            <span class="sr-only">التالى</span>
        </a>
    </div>
</asp:Panel>