<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="AcaoDetalhes" Codebehind="AcaoDetalhes.aspx.cs" %>
<%@ Register Src="~/CustomControls/ucAcoesVinculadasProjeto.ascx" TagName="AcaoVinculada" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
.rolagem{
width: 520px;
height : 100px;
overflow : auto;
}
</style>

<script type="text/javascript">
    function exibirOcultar(panel, link) {
        var element = document.getElementById(panel);
        var a = document.getElementById(link);
        var visible = 'visibleRow';
        var hidden = 'hiddenRow';

        if (element.className == hidden) {
            element.className = visible; ;
            a.innerHTML = a.innerHTML.replace("expand", "collapse");
        }
        else {
            element.className = hidden;
            a.innerHTML = a.innerHTML.replace("collapse", "expand");
        }

        return false;

    }
</script>
<style type="text/css">
.hiddenRow {
 display:none;
 visibility:hidden;
}
.visibleRow {
 display:;
 visibility:visible;
}

</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc:Nav ID="ucNav" runat="server" acao="true"/> 
<uc:NomeProjeto runat="server" ID="ucProjeto" />
<br />
<script src="wz_tooltip.js" type="text/javascript"></script>
<script src="tip_centerwindow.js" type="text/javascript"></script>
    
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Detalhamento da Ação


    
    
    <asp:HyperLink ID="lnkTrocaDeAcao" NavigateUrl="javascript:void(0)" runat="server">
        <asp:Image ID="imgRefresh" ImageUrl="~/images/refresh.gif" runat="server" />
    </asp:HyperLink>





</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
<div style="border-bottom:solid 1px #999; margin-bottom:10px">

<a href="#" title="Detalhes da Ação" style="color:#000000; text-decoration:none; font-size:13px"  
id="linkDetalhes" onclick="exibirOcultar('tbDetalhes', 'linkDetalhes'); return false;">
<img alt="" src="images/btn_collapse.gif" /> <b>Ação:</b> <asp:Label ID="lblnm_acao" runat="server"></asp:Label></a>
&nbsp;

</div>

<asp:Panel ID="PanelAcoes" runat="server" style="display: none;">

</asp:Panel>
    <asp:RadioButtonList ID="rblt08_cd_acao" CssClass="rbltable" AutoPostBack="true" runat="server">
</asp:RadioButtonList>
        <table id="tbDetalhes"
         rules="all" border="1" cellpadding="4" cellspacing="4"
         style="border-collapse:collapse; border-color:#999; width:100%; background:#FFF" 
         class="visibleRow">
        <tr style="vertical-align:top">
        <td style="font-weight:bold; width:80px"> Período:</td>
        <td>
        Início:<asp:Label ID="lbldt_inicio" runat="server"></asp:Label> -
         Término:<asp:Label ID="lbldt_fim" runat="server"></asp:Label></td>
        </tr>
        <tr runat="server" id="trDesc">
        <td  style="font-weight:bold">Descrição:</td>
        <td><asp:Label ID="lblds_acao" runat="server"></asp:Label></td>
        </tr>
        <tr>
        <td  style="font-weight:bold">Responsável:</td>
        <td><asp:Label ID="lblnm_nome" runat="server"></asp:Label></td>
        </tr>
        </table>

         
<h5><a href="#" onmouseover="Tip('Identificação e quantificação do(s) produto(s) resultante(s) da execução da ação, ou seja, as entregas obtidas com a sua execução. <p><u>Exemplo &#8211; Projeto Expansão das UPAs (Ação: Implantação das UPAs da região do Barreiro):</u><br> UPA em funcionamento com recursos humanos selecionados e capacitados.',
WIDTH, 300, TITLE, 'Meta Física', SHADOW, true, BORDERCOLOR, '#C2B17C', FADEIN, 300, FADEOUT, 300, STICKY, 1, CLOSEBTN, true, CLICKCLOSE, true)"><img src="images/information.gif" title=""  /></a>
Meta Física</h5>   
<uc:Produto runat="server" ID="ucProduto" />

<asp:Panel ID="pnlExibeFinanceiro" runat="server">
<h5><a href="#" onmouseover="Tip('Valores expressos em moeda corrente (R$), com a respectiva indicação das fontes, que representam o valor necessário para a realização da ação. As despesas de custeio não devem ser consideradas neste campo.',
WIDTH, 300, TITLE, 'Financeiro', SHADOW, true, BORDERCOLOR, '#C2B17C', FADEIN, 300, FADEOUT, 300, STICKY, 1, CLOSEBTN, true, CLICKCLOSE, true)"><img src="images/information.gif" title=""  /></a>
Financeiro</h5> 
<uc:Financeiro runat="server" ID="ucFinanceiro" />
</asp:Panel>

<h5><img src="images/information.gif" title=""  />
Documentos</h5> 
<uc:Documentos runat="server" ID="ucDocumentos" />

<h5><a href="#" onmouseover="Tip('Projeto(s) Sustentador(es) vinculado(s) a Ação.',
WIDTH, 300, TITLE, 'Projeto', SHADOW, true, BORDERCOLOR, '#C2B17C', FADEIN, 300, FADEOUT, 300, STICKY, 1, CLOSEBTN, true, CLICKCLOSE, true)"><img src="images/information.gif" title=""  /></a>
Projetos Sustentadores Vinculados</h5> 
<uc:AcaoVinculada runat="server" ID="ucAcoesVinculadasProjeto" />

<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />

</asp:Content>

