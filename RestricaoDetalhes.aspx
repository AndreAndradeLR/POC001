<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="RestricaoDetalhes" Codebehind="RestricaoDetalhes.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" restricao="true"/> 
<uc:NomeProjeto runat="server" ID="ucProjeto" />
<br />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Detalhamento da Restrição</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
         
      <table cellpadding="4" cellspacing="4">
        <tr>
            <td><label style="font-weight:bold">Restrição:</label><br />
            <asp:Label ID="lblds_restricao" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><label style="font-weight:bold">Data de Inclusão:</label>
            <asp:Label ID="lbldt_cadastro" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><label style="font-weight:bold">Data limite para solução:</label>
            
            <asp:Label ID="lbldt_limite" runat="server"></asp:Label>
            </td>
            
        </tr> 
        <tr runat="server" id="trMC"  visible="false">
            <td><label style="font-weight:bold">Marcos Críticos relacionados:</label><br />
            <ul>
                <asp:Literal ID="LiteralMC" runat="server"></asp:Literal>
            </ul>
            </td>
        </tr> 
        <tr>
        <td><label style="font-weight:bold">Providências:</label><br />
            <uc:Providencia runat="server" ID="ucProvidencia" />
        </td>
        </tr>
        <tr>
        <td>
            <span class="button" runat="server" id="spanbtnSuperar">
            <asp:Button ID="btnSuperar" runat="server" 
            OnClientClick="if (confirm('Tem certeza que deseja superar a restrição?') == false) return false;"  
            Text="Superar Restrição" onclick="btnSuperar_Click" />
            </span>
            <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>  
        </td>
        </tr>
      </table>

<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

