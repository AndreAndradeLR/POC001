<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="AcaoFinanceiro" Codebehind="AcaoFinanceiro.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" acaoFin="true"/> 
<uc:NomeProjeto runat="server" ID="ucProjeto" />
<br />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Financeiro Acumulado por Ação</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
    
<div style="margin-bottom:10px">
<b>Fonte:</b> <asp:DropDownList ID="ddlt27_cd_fonte" runat="server">
    </asp:DropDownList> 
    &nbsp;
    <b>Ano</b>: 
    <asp:DropDownList ID="ddlAno" runat="server">
    </asp:DropDownList> 
    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" 
        onclick="btnFiltrar_Click" /><br /><br />
   <asp:LinkButton Visible="false" ID="LinkExibir" OnClick="linkFiltroExibe_Click" runat="server" Text="Exibir Meses"></asp:LinkButton>
</div>    
<asp:Panel ID="PanelFin" Width="700px" ScrollBars="Horizontal" runat="server">
         <asp:Literal ID="LiteralGrid" runat="server"></asp:Literal>
</asp:Panel>         

<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>  

 <div style="text-align:right; padding-top:5px; margin-right:3%">
    <asp:HyperLink ID="linkFin" Font-Bold="true" NavigateUrl="~/ProjetoFinanceiro.aspx" runat="server">Financeiro Acumulado do Projeto</asp:HyperLink>
 </div>
    
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

