<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="FinanceiroDetalhes" Codebehind="FinanceiroDetalhes.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" financeiro="true"/> 
<uc:NomeProjeto runat="server" ID="ucProjeto" />
<br />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Detalhamento do Financeiro</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
<table cellpadding="4" cellspacing="4">
   <tr>
       <td><label style="font-weight:bold">Ação:</label> 
       <asp:Label ID="lblNomeAcao" runat="server"></asp:Label></td>
   </tr>
   <tr>
     <td><label style="font-weight:bold">Fonte:</label> 
      <asp:Label ID="lblnm_fonte" Font-Size="Small" runat="server"></asp:Label></td>
   </tr>
   <tr>
    <td>
        <asp:LinkButton ID="LinkExibir" OnClick="linkFiltroExibe_Click" runat="server" Text="Exibir Meses"></asp:LinkButton>
    </td>
   </tr>
  <tr>
     <td>
      <asp:Panel ID="PanelP" Width="700px" ScrollBars="Horizontal" runat="server">
        <uc:LancamentoFinanceiro EnableViewState="false" OnLoad="LancamentoFinanceiro_Load" ID="ucLancFin" runat="server" />
      </asp:Panel>
     </td>
   </tr>   
</table>        

<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

