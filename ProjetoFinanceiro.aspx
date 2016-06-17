<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ProjetoFinanceiro" Codebehind="ProjetoFinanceiro.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" projFin="true"/> 
<uc:NomeProjeto runat="server" ID="ucProjeto" />
<br />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Financeiro Acumulado</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 

         <asp:Literal ID="LiteralGrid" runat="server"></asp:Literal>
 <div style="text-align:right; padding-top:5px;">
    <asp:HyperLink ID="linkFin" Font-Bold="true" NavigateUrl="~/AcaoFinanceiro.aspx" runat="server">Financeiro Acumulado por Ação</asp:HyperLink>
 </div>
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

