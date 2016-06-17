<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Arvore" CodeBehind="Arvore.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="wz_tooltip.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="js/jquery.tipsy.js"></script>
    <script type="text/javascript" src="js/jquery.prettyPhoto.js"></script>
    <script type="text/javascript" src="js/common.js"></script>
    <uc:Nav ID="ucNav" runat="server" arvore="true" />
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    <asp:Panel ID="PanelArvore" runat="server">
        <uc:NomeProjeto ID="ucNomeProjeto" runat="server" />
        <table style="width: 100%" cellspacing="4" cellpadding="4">
            <tr>
                <td style="width: 70%; vertical-align: top">
                    <div class="heading_container">
                        <div class="heading_right_top">
                        </div>
                        <h2 id="H2" title="Público-alvo">
                            <a href="#" onmouseover="Tip('Segmentos que se pretende atender com a execução do projeto e em relação ao qual serão avaliados os resultados. <p><u>Exemplo – Projeto Expansão das UPAs:</u> População de Barreiros, Venda Nova, Oeste e Centro Sul de BH usuária do Sistema Único de Saúde (SUS) que necessita de atendimento de urgência e emergência de baixa e média complexidade.',
             WIDTH, 300, TITLE, 'Público Alvo', SHADOW, true, BORDERCOLOR, '#C2B17C', FADEIN, 300, FADEOUT, 300, STICKY, 1, CLOSEBTN, true, CLICKCLOSE, true)">
                                <img src="images/information.gif" title="" /></a>
                            <asp:Label ID="lblh_publico" runat="server" Text="Público-alvo"></asp:Label></h2>
                    </div>
                    <div class="bucket_container">
                        <div class="bucket_top">
                            <span></span>
                        </div>
                        <div class="bucket_content">
                            <div class="clear">
                            </div>
                            <asp:Panel ID="Panelds_publico" runat="server">
                                <asp:Label ID="lblds_publico" runat="server"></asp:Label><br />
                                <asp:Panel ID="pnlButtonEditPublico" runat="server">
                                    <asp:LinkButton ID="linkds_publico" OnClick="link_Click" CommandArgument="ds_publico"
                                        runat="server">Editar</asp:LinkButton>
                                </asp:Panel>
                            </asp:Panel>
                            <asp:Panel ID="PanelEditds_publico" Visible="false" runat="server">
                                <asp:TextBox ID="txtds_publico" runat="server" Rows="3" TextMode="MultiLine" Width="98%"></asp:TextBox><br />
                                <span class="button">
                                    <asp:Button ID="btnSalvards_publico" OnClick="btnSalvar_Click" CommandArgument="ds_publico"
                                        runat="server" Text="Salvar" /></span> <span class="button">
                                            <asp:Button ID="btnCancelards_publico" OnClick="btnCancelar_Click" CommandArgument="ds_publico"
                                                runat="server" Text="Cancelar" /></span>
                            </asp:Panel>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="bucket_bottom">
                            <span></span>
                        </div>
                    </div>
                    <br />
                    <div class="heading_container">
                        <div class="heading_right_top">
                        </div>
                        <h2 id="H1" title="Objetivo geral">
                            <a href="#" onmouseover="Tip('Descreve a finalidade do projeto em sua totalidade com concisão e precisão. Deve indicar a síntese da transformação no público-alvo a ser alcançada no horizonte de tempo do projeto. <p><u> Exemplo – Projeto Expansão das UPAs:</u> Disponibilizar serviços de qualidade para o atendimento das urgências e emergências de baixa e média complexidade, todos os dias, 24 horas, tornando-se uma opção para a população, visando diminuir a demanda para as emergência nos grandes hospitais.',
            WIDTH, 300, TITLE, 'Objetivo geral', SHADOW, true, BORDERCOLOR, '#C2B17C', FADEIN, 300, FADEOUT, 300, STICKY, 1, CLOSEBTN, true, CLICKCLOSE, true)"
                                onmouseout="UnTip()">
                                <img src="images/information.gif" title="" /></a> Objetivo geral</h2>
                    </div>
                    <div class="bucket_container">
                        <div class="bucket_top">
                            <span></span>
                        </div>
                        <div class="bucket_content">
                            <div class="clear">
                            </div>
                            <asp:Panel ID="Panelds_objetivo" runat="server">
                                <asp:Label ID="lblds_objetivo" runat="server"></asp:Label><br />
                                <asp:Panel ID="pnlButtonEditObjetivo" runat="server">
                                    <asp:LinkButton ID="linkds_objetivo" OnClick="link_Click" CommandArgument="ds_objetivo"
                                        runat="server">Editar</asp:LinkButton>
                                </asp:Panel>
                            </asp:Panel>
                            <asp:Panel ID="PanelEditds_objetivo" Visible="false" runat="server">
                                <asp:TextBox ID="txtds_objetivo" runat="server" Rows="3" TextMode="MultiLine" Width="98%"></asp:TextBox><br />
                                <span class="button">
                                    <asp:Button CssClass="btn" ID="btnSalvards_objetivo" OnClick="btnSalvar_Click" CommandArgument="ds_objetivo"
                                        runat="server" Text="Salvar" /></span> <span class="button">
                                            <asp:Button CssClass="btn" ID="btnCancelards_objetivo" OnClick="btnCancelar_Click"
                                                CommandArgument="ds_objetivo" runat="server" Text="Cancelar" /></span>
                            </asp:Panel>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="bucket_bottom">
                            <span></span>
                        </div>
                    </div>
                    <br />
                    <div class="heading_container">
                        <div class="heading_right_top">
                        </div>
                        <h2 id="H4" title="Resultados">
                            <a href="#" onmouseover="Tip('São as transformações no público-alvo ou o produto resultante da execução do projeto. <p><u>Exemplo de Resultado Finalístico – Projeto Melhoria da qualidade da educação municipal:</u> Aumentar a qualidade da educação municipal, medida pelo IDEB, de 3,4 para 4,1 para os anos finais até 2011. <p><u> Exemplo de Resultado Produto – Projeto Expansão das UPAs:</u> 40 UPAs implantadas até dezembro de 2010. ',
            WIDTH, 300, TITLE, 'Resultados', SHADOW, true, BORDERCOLOR, '#C2B17C', FADEIN, 300, FADEOUT, 300, STICKY, 1, CLOSEBTN, true, CLICKCLOSE, true)"
                                onmouseout="UnTip()">
                                <img src="images/information.gif" title="" /></a> Resultados</h2>
                    </div>
                    <div class="bucket_container">
                        <div class="bucket_top">
                            <span></span>
                        </div>
                        <div class="bucket_content">
                            <div class="clear">
                            </div>
                            <asp:DataList ID="dlResultados" RepeatColumns="2" Width="100%" OnItemDataBound="dlResultado_ItemDataBound"
                                runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="3" GridLines="Both">
                                <ItemTemplate>
                                    <asp:Label ID="lblds_resultado" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle ForeColor="#000066" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1PX"
                                    Width="50%" />
                            </asp:DataList>
                            <asp:HyperLink ID="linkResultados" NavigateUrl="~/Resultados.aspx" runat="server">Exibir Completo</asp:HyperLink>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="bucket_bottom">
                            <span></span>
                        </div>
                    </div>
                    <br />
                    <asp:Panel ID="pnlShowSituacao" runat="server">
                        <div class="heading_container">
                            <div class="heading_right_top">
                            </div>
                            <h2 id="H3" title="Situação atual">
                                <a href="#" onmouseover="Tip('Breve relato sobre a atual situação do projeto, destacando os principais resultados alcançados e o foco atual do trabalho. <p><u>Exemplo – Projeto Expansão das UPAs:</u> A Unidade de Pronto-Atendimento (UPA) Oeste foi inaugurada em fevereiro de 2009, com cerca de 1.300 metros quadrados de construção e capacidade para receber a demanda atual de 300 atendimentos por dia. O foco atual do projeto é a obra na UPA Venda Nova e a construção da UPA Centro-Sul. ',
            WIDTH, 300, TITLE, 'Situação atual', SHADOW, true, BORDERCOLOR, '#C2B17C', FADEIN, 300, FADEOUT, 300, STICKY, 1, CLOSEBTN, true, CLICKCLOSE, true)"
                                    onmouseout="UnTip()">
                                    <img src="images/information.gif" title="" /></a> Situação atual
                            </h2>
                        </div>
                        <div class="bucket_container">
                            <div class="bucket_top">
                                <span></span>
                            </div>
                            <div class="bucket_content">
                                <div class="clear">
                                </div>
                                <asp:Panel ID="Panelds_situacao" runat="server">
                                    <asp:Label ID="lblds_situacao" runat="server"></asp:Label><br />
                                    <asp:LinkButton ID="linkds_situacao" OnClick="link_Click" CommandArgument="ds_situacao"
                                        runat="server">Editar</asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HlkSituacao" runat="server">Exibir Completo</asp:HyperLink>
                                </asp:Panel>
                                <asp:Panel ID="PanelEditds_situacao" Visible="false" runat="server">
                                    <asp:TextBox ID="txtds_situacao" runat="server" Rows="3" TextMode="MultiLine" Width="98%"></asp:TextBox><br />
                                    <span class="button">
                                        <asp:Button CssClass="btn" ID="btnSalvards_situacao" OnClick="btnSalvar_Click" CommandArgument="ds_situacao"
                                            runat="server" Text="Salvar" /></span> <span class="button">
                                                <asp:Button CssClass="btn" ID="btnCancelards_situacao" OnClick="btnCancelar_Click"
                                                    CommandArgument="ds_situacao" runat="server" Text="Cancelar" /></span>
                                </asp:Panel>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="bucket_bottom">
                                <span></span>
                            </div>
                        </div>
                        <br />
                    </asp:Panel>
                    <asp:Panel ID="pnlShowGaleria" runat="server">
                        <div class="heading_container">
                            <div class="heading_right_top">
                            </div>
                            <h2 id="H5" title="Galeria de Imagens">
                                Galeria de Imagens
                            </h2>
                        </div>
                        <div class="bucket_container">
                            <div class="bucket_top">
                                <span></span>
                            </div>
                            <div class="bucket_content">
                                <div class="clear">
                                </div>
                                <asp:Panel ID="Panelgaleria" runat="server">
                                    <asp:Repeater ID="rptFotos" OnItemDataBound="rptFotos_ItemDataBound" runat="server">
                                        <HeaderTemplate>
                                            <ul id="sortable" class="gallery clearfix">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li id="liitem" runat="server" title="Ampliar" class="photolistitem" style="position: relative; opacity: 1; left: 0px; top: 0px; background-color: #EFEBDA; height: 190px;">
                                                <a runat="server" id="prettyPhoto" href="images/fullscreen/2.jpg" rel="prettyPhoto[gallery1]"><img ID="idImg" runat="server" src="images/thumbnails/t_2.jpg" style="width:60; height:60;" alt='<%#Eval("NM_DOCUMENTO")%>' /></a>   
                                                <div class="" style="height: 40px; width: 150px; background-color: #FAF7EE; margin-left: 10px;
                                                    margin-top: 1px">
                                                    <asp:Label runat="server" ID="lblNm_DOCUMENTO"></asp:Label>
                                                </div>
                                            </li>
                                        </ItemTemplate>
			                            <FooterTemplate>
                                            </ul>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <div class="clear"></div>
                                    <div style="width: 40%; float: left;">
                                        <asp:HyperLink ID="hlkGaleria" NavigateUrl="~/Fotos.aspx" runat="server">Editar</asp:HyperLink>
                                    </div>
                                    <div style="width: 40%; float: right; text-align:right;">
                                        <asp:HyperLink ID="hlkExibirGaleria" NavigateUrl="~/FotosGaleria.aspx" runat="server" Visible="false">Exibir Galeria</asp:HyperLink>
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
                                <div class="clear">
                                </div>
                            </div>
                            <div class="bucket_bottom">
                                <span></span>
                            </div>
                        </div>
                        <br />
                    </asp:Panel>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 15%" id="tdParceiros" runat="server">
                                <div class="heading_container">
                                    <div class="heading_right_top">
                                    </div>
                                    <h2 id="H9" title="Parceiros">
                                        <a href="#" onmouseover="Tip('Áreas ou organizações comprometidas com a execução e alcance dos resultados do projeto. Podem ser Secretarias e órgãos da Prefeitura envolvidos no Projeto, além de quaisquer outras instituições públicas ou privadas que também tenham envolvimento direto e responsabilidades na execução do projeto. <p><u>Exemplo – Projeto Expansão das UPAs:</u> <br><br>&nbsp;<img src=\'images/tbl_ExemploUPAs.jpg\' />',
            WIDTH, 550, TITLE, 'Parceiros', SHADOW, true, BORDERCOLOR, '#C2B17C', FADEIN, 300, FADEOUT, 300, STICKY, 1, CLOSEBTN, true, CLICKCLOSE, true)"
                                            onmouseout="UnTip()">
                                            <img src="images/information.gif" title="" /></a> Parceiros</h2>
                                </div>
                                <div class="bucket_container">
                                    <div class="bucket_top">
                                        <span></span>
                                    </div>
                                    <div class="bucket_content">
                                        <div class="clear">
                                        </div>
                                        <table style="width: 100%; font-size: x-small; text-align: center;">
                                            <tr>
                                                <td>
                                                    <asp:HyperLink ID="linkParceiros" Font-Underline="false" NavigateUrl="~/Parceiros.aspx"
                                                        runat="server">
                                                        <img title="Parceiros" src="images/ico_parceiro.gif" /><br />
                                                        Parceiros (<asp:Label ID="lblparceiros" runat="server"></asp:Label>)
                                                    </asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="clear">
                                        </div>
                                    </div>
                                    <div class="bucket_bottom">
                                        <span></span>
                                    </div>
                                </div>
                                <br />
                            </td>
                            <td>
                                <div class="heading_container">
                                    <div class="heading_right_top">
                                    </div>
                                    <h2 id="H12" title="Comunicação">
                                        Comunicação</h2>
                                </div>
                                <div class="bucket_container">
                                    <div class="bucket_top">
                                        <span></span>
                                    </div>
                                    <div class="bucket_content">
                                        <div class="clear">
                                        </div>
                                        <table style="width: 100%; font-size: x-small; text-align: center;">
                                            <tr>
                                                <td style="width: 15%" id="tdAgenda" runat="server">
                                                    <asp:HyperLink ID="linkAgenda" Font-Underline="false" NavigateUrl="~/Agenda.aspx"
                                                        runat="server">
                                                        <img title="Agenda" src="images/ico_agenda.gif" /><br />
                                                        Agenda (<asp:Label ID="lblAgenda" runat="server"></asp:Label>)
                                                    </asp:HyperLink>
                                                </td>
                                                <td style="width: 15%" id="tdNoticias" runat="server">
                                                    <asp:HyperLink ID="linkNoticias" Font-Underline="false" NavigateUrl="~/Noticias.aspx"
                                                        runat="server">
                                                        <img title="Notícias" src="images/ico_noticia.gif" /><br />
                                                        Notícias (<asp:Label ID="lblNoticias" runat="server"></asp:Label>)
                                                    </asp:HyperLink>
                                                </td>
                                                <%--<td style="width: 15%" id="tdFoto" runat="server">
                                                    <asp:HyperLink ID="linkFoto" Font-Underline="false" NavigateUrl="~/Documentos.aspx?tipo=foto"
                                                        runat="server">
                                                        <img title="Fotos" src="images/ico_foto.gif" /><br />
                                                        Fotos (<asp:Label ID="lblFoto" runat="server"></asp:Label>)
                                                    </asp:HyperLink>
                                                </td>--%>
                                            </tr>
                                        </table>
                                        <div class="clear">
                                        </div>
                                    </div>
                                    <div class="bucket_bottom">
                                        <span></span>
                                    </div>
                                </div>
                                <br />
                            </td>
                            <td style="width: 35%" id="tdDocumentos" runat="server">
                                <div class="heading_container">
                                    <div class="heading_right_top">
                                    </div>
                                    <h2 id="H6" title="Documentos">
                                        Documentos</h2>
                                </div>
                                <div class="bucket_container">
                                    <div class="bucket_top">
                                        <span></span>
                                    </div>
                                    <div class="bucket_content">
                                        <div class="clear">
                                        </div>
                                        <table style="width: 100%; font-size: x-small; text-align: center;">
                                            <tr>
                                                <td style="width: 15%" id="tdCronograma" runat="server">
                                                    <asp:HyperLink ID="linkCronograma" Font-Underline="false" NavigateUrl="~/Documentos.aspx?tipo=cronograma"
                                                        runat="server">
                                                        <img title="Cronogramas" src="images/ico_cronograma.gif" /><br />
                                                        Cronogramas (<asp:Label ID="lblCronograma" runat="server"></asp:Label>)
                                                    </asp:HyperLink>
                                                </td>
                                                <td style="width: 15%" id="tdOutros" runat="server">
                                                    <asp:HyperLink ID="linkOutros" Font-Underline="false" NavigateUrl="~/Documentos.aspx?tipo=outros"
                                                        runat="server">
                                                        <img title="Outros" src="images/ico_outros.gif" /><br />
                                                        Outros (<asp:Label ID="lblOutros" runat="server"></asp:Label>)
                                                    </asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="clear">
                                        </div>
                                    </div>
                                    <div class="bucket_bottom">
                                        <span></span>
                                    </div>
                                </div>
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top">
                    <div class="heading_container">
                        <div class="heading_right_top">
                        </div>
                        <h2 id="H7" title="Evolução">
                            Evolução</h2>
                    </div>
                    <div class="bucket_container">
                        <div class="bucket_top">
                            <span></span>
                        </div>
                        <div class="bucket_content">
                            <div class="clear">
                            </div>
                            <div class="category_item_book">
                                <div style="text-align: center">
                                    <asp:Label ID="lblstatus" runat="server"></asp:Label></div>
                            </div>
                            <div class="category_item_book">
                                <div style="text-align: center">
                                    <asp:Label ID="lblgrafico" runat="server"></asp:Label></div>
                            </div>
                            <div class="category_item_book" style="text-align: center">
                                <asp:Label ID="lblrestricao" runat="server"></asp:Label>
                            </div>
                            <asp:Panel ID="Panelt21_cd_fase" CssClass="category_item_book" runat="server">
                                Fase:
                                <asp:Label ID="lblnm_fase" Font-Bold="true" runat="server"></asp:Label>
                                <asp:LinkButton ID="linkt21_cd_fase" OnClick="link_Click" CommandArgument="t21_cd_fase"
                                    runat="server">Editar</asp:LinkButton>
                            </asp:Panel>
                            <asp:Panel ID="PanelEditt21_cd_fase" Visible="false" runat="server">
                                Fase:
                                <asp:DropDownList ID="ddlt21_cd_fase" runat="server">
                                </asp:DropDownList>
                                <br />
                                <span class="button">
                                    <asp:Button ID="btnSalvart21_cd_fase" OnClick="btnSalvar_Click" CommandArgument="t21_cd_fase"
                                        runat="server" Text="Salvar" /></span> <span class="button">
                                            <asp:Button ID="btnCancelart21_cd_fase" OnClick="btnCancelar_Click" CommandArgument="t21_cd_fase"
                                                runat="server" Text="Cancelar" CausesValidation="false" /></span>
                            </asp:Panel>
                            <div class="category_item_book">
                                <asp:Panel ID="Paneldt_inicio" runat="server">
                                    <div class="category_item_book">
                                        Período:
                                        <asp:Label ID="lbldt_inicio" Font-Bold="true" runat="server"></asp:Label>
                                        -
                                        <asp:Label ID="lbldt_fim" Font-Bold="true" runat="server"></asp:Label>
                                        <asp:LinkButton ID="linkdt_inicio" OnClick="link_Click" CommandArgument="dt_inicio"
                                            runat="server">Editar</asp:LinkButton></div>
                                </asp:Panel>
                                <asp:Panel ID="PanelEditdt_inicio" Visible="false" runat="server">
                                    <div class="category_item_book">
                                        Período:
                                        <br />
                                        <b>Início:</b> &nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="txtdt_inicio" Width="100px" CssClass="datepicker" runat="server"></asp:TextBox>
                                        <%--<asp:ImageButton runat="Server" ID="ImageCal1" ImageUrl="~/images/Calendar.gif" 
                            AlternateText="Clique para exibir o calendário" /><br />
                            <ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" TargetControlID="txtdt_inicio"
                            runat="server" ID="calext1" PopupButtonID="ImageCal1"></ajaxToolkit:CalendarExtender>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdt_inicio"
                                            Display="Dynamic" ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>
                                        <br />
                                        <b>Término:</b>
                                        <asp:TextBox ID="txtdt_fim" Width="100px" CssClass="datepicker" runat="server"></asp:TextBox>
                                        <%--<asp:ImageButton runat="Server" ID="ImageCal2" ImageUrl="~/images/Calendar.gif" 
                            AlternateText="Clique para exibir o calendário" /><br />
                            <ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" TargetControlID="txtdt_fim"
                            runat="server" ID="CalendarExtender1" PopupButtonID="ImageCal2"></ajaxToolkit:CalendarExtender>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdt_fim"
                                            Display="Dynamic" ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>
                                        <br />
                                        <asp:CompareValidator ID="ComparaDatas" runat="server" ControlToCompare="txtdt_inicio"
                                            ControlToValidate="txtdt_fim" ErrorMessage="Data de início não pode ser superior a data de término."
                                            Operator="GreaterThanEqual" Type="Date" Display="Dynamic"></asp:CompareValidator><br />
                                        <asp:Button CssClass="btn" ID="btnSalvardt_inicio" OnClick="btnSalvar_Click" CommandArgument="dt_inicio"
                                            runat="server" Text="Salvar" />
                                        <asp:Button CssClass="btn" ID="btnCancelardt_inicio" CausesValidation="false" OnClick="btnCancelar_Click"
                                            CommandArgument="dt_inicio" runat="server" Text="Cancelar" />
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="category_item_book">
                                Data de atualização:
                                <asp:Label ID="lbldt_atualizado" Font-Bold="true" runat="server"></asp:Label></div>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="bucket_bottom">
                            <span></span>
                        </div>
                    </div>
                    <br />
                    <div style="text-align: center;" runat="server" id="DivBtnDetalhamento">
                        <span class="button" id="spanDetalhamento" runat="server">
                            <asp:Button ID="btnDetalhamento" Font-Bold="true" runat="server" Text="Detalhamento"
                                OnClick="btnDetalhamento_Click" />
                        </span>
                    </div>
                    <br />
                    <div class="heading_container">
                        <div class="heading_right_top">
                        </div>
                        <h2 id="H8" title="Linha Gerencial">
                            <a href="#" onmouseover="Tip('Conjunto de decisores (Prefeito, Vice-Prefeito, Secretário, Sub-secretário  e  Gerente) envolvidos na execução do projeto.', WIDTH, 300, TITLE, 'Linha Gerencial', SHADOW, true, BORDERCOLOR, '#C2B17C', FADEIN, 300, FADEOUT, 300, STICKY, 1, CLOSEBTN, true, CLICKCLOSE, true)"
                                onmouseout="UnTip()">
                                <img src="images/information.gif" title="" /></a> Linha Gerencial</h2>
                    </div>
                    <div class="bucket_container">
                        <div class="bucket_top">
                            <span></span>
                        </div>
                        <div class="bucket_content">
                            <div class="clear">
                            </div>
                            --
                            <asp:Literal ID="LiteralLinhaGerencial" runat="server"></asp:Literal>
                            ---
                            <asp:HyperLink ID="linkResponsaveis" NavigateUrl="~/Responsaveis.aspx" runat="server">Editar<br /><br /></asp:HyperLink>
                            <div style="font-weight: bold">
                                Gerente</div>
                            <asp:Label ID="lbldados_gerente" runat="server"></asp:Label>
                            <hr style="margin: 10px 0 10px 0; color: #ccc" />
                            <div style="font-weight: bold">
                                Responsável pelo Monitoramento</div>
                            <asp:Label ID="lbldados_monitor" runat="server"></asp:Label>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="bucket_bottom">
                            <span></span>
                        </div>
                    </div>
                    <br />
                    <asp:Panel ID="PanelInvestimentos" runat="server">
                        <div class="heading_container">
                            <div class="heading_right_top">
                            </div>
                            <h2 id="H11" title="Investimentos">
                                <a href="#" onmouseover="Tip('São os valores expressos em moeda corrente (R$), com a respectiva indicação das fontes, que representam o investimento necessário para a realização do projeto.',
            WIDTH, 300, TITLE, 'Investimentos', SHADOW, true, BORDERCOLOR, '#C2B17C', FADEIN, 300, FADEOUT, 300, STICKY, 1, CLOSEBTN, true, CLICKCLOSE, true)"
                                    onmouseout="UnTip()">
                                    <img src="images/information.gif" title="" /></a> Investimentos</h2>
                        </div>
                        <div class="bucket_container">
                            <div class="bucket_top">
                                <span></span>
                            </div>
                            <div class="bucket_content">
                                <div class="clear">
                                </div>
                                <asp:Literal ID="ltrFinanceiro" runat="server"></asp:Literal>
                                <asp:HyperLink ID="linkDetalheFinanceiro" NavigateUrl="~/ProjetoFinanceiro.aspx"
                                    runat="server">Exibir completo</asp:HyperLink>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="bucket_bottom">
                                <span></span>
                            </div>
                        </div>
                        <br />
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Label ID="lbltempo" runat="server"></asp:Label>
    <script type="text/javascript">
        // Setting initial size of windows
        // These values could be overridden by cookies.
        windowSizeArray[1] = [400, 400]; // Size of first window
        windowPositionArray[1] = [200, 200]; // X and Y position of first window
    </script>
</asp:Content>
