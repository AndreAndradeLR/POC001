<%@ Control Language="C#" AutoEventWireup="true" Inherits="CustomControls_ucLegenda" Codebehind="~/CustomControls/ucLegenda.ascx.cs" %>
<table class="tblegenda" cellspacing="0">
    <tr>
        <td colspan="5" class="header">
            Legenda</td>
    </tr>
    <tr style="height:70px; vertical-align:top">
        <td  id="tdG" runat="server">
        <br />
            <img alt="" src="images/G.gif" style="width: 54px; height: 20px" /><br />
            Desenvolvimento normal dentro dos prazos previstos</td>
        <td  id="tdR" runat="server">
        <br />
            <img alt="" alt="" src="images/R.gif" style="width: 54px; height: 20px" /><br />
            Desenvolvimento com atraso</td>
        <td  id="tdB" runat="server">
        <br />
            <img alt="" src="images/B.gif" style="width: 54px; height: 20px" /><br />
            Desenvolvimento concluído</td>
        <td id="tdY" runat="server">
        <br />
            <img alt="" src="images/Y.gif" style="width: 54px; height: 20px" /><br />
            Desenvolvimento com dificuldade</td>
        <td id="tdRestricao" runat="server">
        <br />
            <img src="images/Restricao.gif" /><br />
            Projeto possui uma ou mais restrições</td>
    </tr>
    </table>
