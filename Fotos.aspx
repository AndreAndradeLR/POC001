<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Fotos" Codebehind="Fotos.aspx.cs" %>

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
        <script type="text/javascript">

        $(function () {
            // Uploadify File Upload System
            // SessionSync data is sent in scriptData for security reasons, see UploadifySessionSync() in global.asax
            $(".file_upload").uploadify({
                'hideButton': false,       // We use a trick below to overlay a fake html upload button with this hidden flash button                         
                'wmode': 'transparent',
                'uploader': '<%= ResolveClientUrl("~/js/Uploadify/uploadify.swf") %>',
                'cancelImg': '<%= ResolveClientUrl("~/js/Uploadify/cancel.png") %>',
                'width': '136',
                'buttonText': 'Varias Fotos',
                'script': '<%= ResolveClientUrl("~/UploadFotos.aspx") %>',
                'multi': true,
                'auto': true,
                'queueID': 'custom-queue',
                'fileExt': '*.jpg;*.gif;*.png;*.jpeg',
                'fileDesc': 'Image Files',
                'scriptData': { RequireUploadifySessionSync: true, cd_projeto: <%= cd_projeto %> },
                'onAllComplete': function (event, data) {
                    document.location = "Fotos.aspx";                  
                }
            });

            $("#sortable").sortable({ opacity: 0.6, cursor: 'move', update: function () {
                //converter os IDs da imagens em array
                var order = $(this).sortable("toArray"); 
                //Mandando um POST para o webservice com a nova ordem
                $.ajax({
                    type: 'POST'
                    , url: "WebService.asmx/Reorder"
                    , contentType: 'application/json; charset=utf-8'
                    , dataType: 'json'
                    , data: "{objOrdem:'" + order + "'}" //Envia a nova ordem
                    , success: function (data, status) {

                         document.location = "Fotos.aspx";
                    }
                    , error: function (xmlHttpRequest, status, err) {
                        alert('error'); //No caso de ocorrer algum erro.
                    }
                    });
                }
            });
            $( "#sortable", ".photolistitem" ).disableSelection();
        });
    </script>
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
            <asp:Panel ID="PanelEdit" runat="server" Visible="false" Style="width: 50%;">
                <div class="heading_container">
                    <div class="heading_right_top">
                    </div>
                    <h2 id="H1" title="Objetivo geral">
                        <asp:Literal ID="lblHeader" runat="server"></asp:Literal>
                       </h2>
                </div>
                <div class="bucket_container">
                    <div class="bucket_top">
                        <span></span>
                    </div>
                    <div class="bucket_content">
                        <div class="clear">
                        </div>
                    
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <b>Título: </b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNM_DOCUMENTO" runat="server" Width="325px" MaxLength="300"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*campo obrigatório"
                                        Display="Dynamic" ControlToValidate="txtNM_DOCUMENTO"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr runat="server" id="PanelArquivo">
                                <td>
                                    <b>Arquivo:</b>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fuNM_ARQUIVO" runat="server" AllowMultiple="False" /><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="*campo obrigatório"
                                        Display="Dynamic" ControlToValidate="fuNM_ARQUIVO"></asp:RequiredFieldValidator>
                                    <small>*tamanho máximo de arquivo para upload é de 10 MB.</small>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button CssClass="silver" ID="btnAcao" OnClick="btnAcao_Click" runat="server"
                                        Text="Cadastrar" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelar" CssClass="silver" OnClick="btnCancelar_Click" runat="server"
                                        Text="Cancelar" CausesValidation="False" />
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="cod" Value="0" runat="server" />
                    
                        <div class="clear">
                        </div>
                    </div>
                    <div class="bucket_bottom">
                        <span></span>
                    </div>
                </div>
            </asp:Panel>
			<asp:Panel ID="PanelGaleria" runat="server">
                <div id="divPhotoList">
                    <h2 class="title show_hide">
                        Galeria de imagens<span class="close-icon"></span>
                    </h2>
                    <div style="margin-left: 4px; margin-top: 4px">
                        <label style="float: left; padding-top: 10px; padding-right: 5px;">
                            Cadastrar:
                        </label>
                        <asp:ImageButton ID="imgbtnNovo" runat="server" Text="" CssClass="silver"
                            OnClick="btnNovo_Click" Style="float: left;" ImageUrl="images/UmaFoto.jpg" />
                        <div style="float: left; padding-left: 5px;">
                            <input id="file_upload" class="file_upload" width="120" type="file" height="30" name="file_upload"
                                style="display: none;" />
                            <object id="file_uploadUploader" width="130" height="30" type="application/x-shockwave-flash"
                                data="scripts/uploadify/uploadify.swf">
                            </object>
                        </div>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <asp:Repeater ID="rptFotos" OnItemDataBound="rptFotos_ItemDataBound" runat="server">
                        <HeaderTemplate>
                            <ul id="sortable" class="gallery clearfix">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li id="<%#Eval("T14_CD_DOCUMENTO")%>" title="Ampliar" class="photolistitem" style="position: relative; opacity: 1; left: 0px; top: 0px; background-color: #EFEBDA; height: 230px;">
                                <a runat="server" id="prettyPhoto" href="images/fullscreen/2.jpg" rel="prettyPhoto[gallery1]"><img ID="idImg" runat="server" src="images/thumbnails/t_2.jpg" style="width:60; height:60;" alt='<%#Eval("NM_DOCUMENTO")%>' /></a>   
                                <div class="" style="height: 60px; width: 150px; background-color: #FAF7EE; margin-left: 10px;
                                    margin-top: 1px">
                                    <asp:Label runat="server" ID="lblNm_DOCUMENTO"></asp:Label>
                                </div>
                                <div style="height: 20px; margin: 5px 0px 0px 70px;">
                                    <asp:LinkButton ID="lkbEditImage" CommandArgument='<%#Eval("T14_CD_DOCUMENTO") %>'
                                        CssClass="imgg-edit" runat="server" OnClick="lkbEditImage_Click"></asp:LinkButton> 
                                    <asp:LinkButton ID="lkbDelImage" CommandArgument='<%#Eval("T14_CD_DOCUMENTO") %>'
                                        CssClass="imgg-delete" runat="server" OnClick="DelImage_Click"></asp:LinkButton>
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
