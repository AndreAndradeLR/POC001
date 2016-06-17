<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="FotosGaleria" Codebehind="FotosGaleria.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <script src="scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
		<link rel="stylesheet" href="css/prettyPhoto.css" type="text/css" media="screen" title="prettyPhoto main stylesheet" charset="utf-8" />
		<script src="js/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>
        <script type="text/javascript" src="js/jquery.flot.js"></script>
        <script type="text/javascript" src="js/jquery.tipsy.js"></script>
        <script type="text/javascript" src="js/common.js"></script>
        <script type="text/javascript" src="js/plot-init.js"></script>
        <script src="js/uploadify/swfobject.js" type="text/javascript"></script>
        <script src="js/uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>
    <style type="text/css">
        .imgg-edit {
	        background-image:url(images/icons/pencil.png);
	        background-repeat: no-repeat;
	        width: 20px;
	        height: 16px;
	        cursor:pointer;
	        display: block;
	        float: left;
        }

        .imgg-delete {
	        background-image:url(images/icons/remove.png);
	        background-repeat: no-repeat;
	        width: 20px;
	        height: 16px;
	        cursor:pointer;
	        display: block;
	        float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="module-box omega">
		<div id="main">
			<asp:Panel ID="PanelGaleria" runat="server">
                <div id="divPhotoList">
                    <h2 class="title show_hide">
                        Galeria de imagens<span class="close-icon"></span>
                    </h2>
                    <asp:Repeater ID="rptFotos" OnItemDataBound="rptFotos_ItemDataBound" runat="server">
                        <HeaderTemplate>
                            <ul class="gallery clearfix">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li id="<%#Eval("T14_CD_DOCUMENTO")%>" title="Ampliar" class="photolistitem" style="position: relative; opacity: 1; left: 0px; top: 0px; background-color: #EFEBDA; height: 230px;">
                                <a runat="server" id="prettyPhoto" href="images/fullscreen/2.jpg" rel="prettyPhoto[gallery1]"><img ID="idImg" runat="server" src="images/thumbnails/t_2.jpg" style="width:60; height:60;" alt='<%#Eval("NM_DOCUMENTO")%>' /></a>   
                                <div class="" style="height: 60px; width: 150px; background-color: #FAF7EE; margin-left: 10px;
                                    margin-top: 1px">
                                    <asp:Label runat="server" ID="lblNm_DOCUMENTO"></asp:Label>
                                </div>
                            </li>
                        </ItemTemplate>
			            <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
	            </div>
            </asp:Panel>
			<script type="text/javascript" charset="utf-8">
			    $(document).ready(function () {
			        $("area[rel^='prettyPhoto']").prettyPhoto();

			        $(".gallery:first a[rel^='prettyPhoto']").prettyPhoto({ animation_speed: 'normal', theme: 'light_square', slideshow: 3000, autoplay_slideshow: true });
			        $(".gallery:gt(0) a[rel^='prettyPhoto']").prettyPhoto({ animation_speed: 'fast', slideshow: 10000, hideflash: true });

			        $("#custom_content a[rel^='prettyPhoto']:first").prettyPhoto({
			            custom_markup: '<div id="map_canvas" style="width:260px; height:265px"></div>',
			            changepicturecallback: function () { initialize(); }
			        });

			        $("#custom_content a[rel^='prettyPhoto']:last").prettyPhoto({
			            custom_markup: '<div id="bsap_1259344" class="bsarocks bsap_d49a0984d0f377271ccbf01a33f2b6d6"></div><div id="bsap_1237859" class="bsarocks bsap_d49a0984d0f377271ccbf01a33f2b6d6" style="height:260px"></div><div id="bsap_1251710" class="bsarocks bsap_d49a0984d0f377271ccbf01a33f2b6d6"></div>',
			            changepicturecallback: function () { _bsap.exec(); }
			        });
			    });
			</script>			
	    </div>
    </div>
    <div class="clear"></div>
    <br />
        <center>
            <asp:Button ID="btnVoltar" CssClass="silver" runat="server" PostBackUrl="~/Arvore.aspx"
                Text="Voltar"></asp:Button>
        </center>
    <br />
</asp:Content>
