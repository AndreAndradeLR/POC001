<%@ Control Language="C#" AutoEventWireup="true" Inherits="ucNomeProjeto" Codebehind="~/CustomControls/ucNomeProjeto.ascx.cs" %>
<div class="nomeAreaResultado">
<table>
 <tr>
  <td style="text-align:left;font-size:15px;font-weight:bold;"><small>Área de Resultado:</small>&nbsp;</td>
  <td style="text-align:left;"><asp:Image ID="imgArea" ToolTip="imagem" runat="server" /></td>
  <!-- <td style="text-align:left;font-size:14px;width:auto;">&nbsp;<asp:Label ID="lblnm_area" runat="server"></asp:Label></td> -->
 </tr>
</table>
</div>
<div class="nomeProjeto">
<small>Projeto Sustentador:</small> <asp:Label ID="lblnm_projeto" EnableViewState="false" runat="server"></asp:Label>
</div>
