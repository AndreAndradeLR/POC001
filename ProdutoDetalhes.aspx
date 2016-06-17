<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ProdutoDetalhes" Codebehind="ProdutoDetalhes.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" produto="true"/> 
<uc:NomeProjeto runat="server" ID="ucProjeto" />
<br />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Detalhamento da Meta Física</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 

<table cellpadding="4" cellspacing="4">
        <tr>
            <td><label style="font-weight:bold">Ação:</label> 
            <asp:Label ID="lblNomeAcao" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><label style="font-weight:bold">Título:</label> 
            <asp:Label ID="lblds_produto" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><label style="font-weight:bold">Unidade de Medida:</label>
            <asp:Label ID="lblnm_medida" runat="server"></asp:Label></td>
        </tr>
        <tr>
        <td><label style="font-weight:bold">Previsto:</label><br />
        <asp:Panel ID="PanelP" Width="700px" ScrollBars="Horizontal" runat="server">
            <uc:LancamentoMensal runat="server" prefix="p" ID="ucPrevisto" />
            </asp:Panel>
        </td>
        </tr>
        <tr>
        <td><label style="font-weight:bold">Realizado:</label><br />
            <asp:Panel ID="PanelR" Width="700px" ScrollBars="Horizontal" runat="server">
             <uc:LancamentoMensal runat="server"  prefix="r" ID="ucRealizado" />
            </asp:Panel>
           
        </td>
        </tr>        
      </table>     
      
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

