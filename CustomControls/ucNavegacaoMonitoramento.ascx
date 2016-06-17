<%@ Control Language="C#" AutoEventWireup="true" Inherits="ucNavegacaoMonitoramento" Codebehind="~/CustomControls/ucNavegacaoMonitoramento.ascx.cs" %>
<div class="navegacao">
<asp:HyperLink ID="linkPainel"  NavigateUrl="~/MonPainel.aspx" runat="server">Resumo executivo</asp:HyperLink>
<asp:HyperLink ID="linkMonRestricoes" Visible="false" NavigateUrl="~/MonRestricoes.aspx" runat="server">> Restrições</asp:HyperLink>
<asp:HyperLink ID="linkMonAlavancagem" Visible="false" NavigateUrl="~/MonAlavancagem.aspx" runat="server">> Índice de alavancagem </asp:HyperLink>
<asp:HyperLink ID="linkMonFinanceiroGraf" Visible="false" NavigateUrl="~/MonFinanceiroGraf.aspx" runat="server">> Acompanhamento financeiro</asp:HyperLink>
<asp:HyperLink ID="linkMonFisicoGraf" Visible="false" NavigateUrl="~/MonFisicoGraf.aspx" runat="server">> Acompanhamento físico</asp:HyperLink>
<asp:HyperLink ID="linkMonFinanceiroInd" Visible="false" NavigateUrl="~/MonFinanceiroInd.aspx" runat="server">> Índice de realização financeira </asp:HyperLink>
<asp:HyperLink ID="linkMonFisicoInd" Visible="false" NavigateUrl="~/MonFisicoInd.aspx" runat="server">> Índice de realização física </asp:HyperLink>
<asp:HyperLink ID="linkMonMarcos" Visible="false" NavigateUrl="~/MonMarcos.aspx" runat="server">> Marcos Críticos</asp:HyperLink>
<asp:HyperLink ID="linkMonMediaDias" Visible="false" NavigateUrl="~/MonMediaDias.aspx" runat="server">> Índice médio de atualização dos projetos</asp:HyperLink>
<asp:HyperLink ID="linkMonParceiros" Visible="false" NavigateUrl="~/MonParceiros.aspx" runat="server">> Quantidade média de parceiros</asp:HyperLink>
<asp:HyperLink ID="linkMonProjetos" Visible="false" NavigateUrl="~/MonProjetos.aspx" runat="server">> Projetos analisados</asp:HyperLink>
</div>