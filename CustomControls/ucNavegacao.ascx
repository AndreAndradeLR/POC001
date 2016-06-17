<%@ Control Language="C#" AutoEventWireup="true" Inherits="ucNavegacao" Codebehind="~/CustomControls/ucNavegacao.ascx.cs" %>

<div class="navegacao">
<asp:HyperLink ID="linkNoticiasProjetos" NavigateUrl="~/NoticiasProjetos.aspx" runat="server">Últimas notícias</asp:HyperLink>
<asp:HyperLink ID="linkAgendaProjetos" NavigateUrl="~/AgendaProjetos.aspx" runat="server">Agenda dos Projetos</asp:HyperLink>
<asp:HyperLink ID="linkProjetos" NavigateUrl="~/Projetos.aspx" runat="server">Projetos</asp:HyperLink>
<asp:HyperLink ID="linkArvore" NavigateUrl="~/Arvore.aspx" runat="server">> Árvore do Projeto</asp:HyperLink>
<asp:HyperLink ID="linkDetalhamento" NavigateUrl="~/Detalhamento.aspx" runat="server">> Detalhamento do Projeto</asp:HyperLink>
<asp:HyperLink ID="linkResp" NavigateUrl="~/Responsaveis.aspx" runat="server">> Linha Gerencial</asp:HyperLink>
<asp:HyperLink ID="linkResultado" NavigateUrl="~/Resultados.aspx" runat="server">> Resultados</asp:HyperLink>
<asp:HyperLink ID="linkProjFin" NavigateUrl="~/ProjetoFinanceiro.aspx" runat="server">> Financeiro Acumulado</asp:HyperLink>
<asp:HyperLink ID="linkAcaoFin" NavigateUrl="~/AcaoFinanceiro.aspx" runat="server">> Financeiro Acumulado por Ação</asp:HyperLink>
<asp:HyperLink ID="linkColaboradores" NavigateUrl="~/Colaboradores.aspx" runat="server">> Colaboradores</asp:HyperLink>
<asp:HyperLink ID="linkAgenda" NavigateUrl="~/Agenda.aspx" runat="server">> Agenda</asp:HyperLink>
<asp:HyperLink ID="linkNoticias" NavigateUrl="~/Noticias.aspx" runat="server">> Noticias</asp:HyperLink>
<asp:HyperLink ID="linkParceiros" NavigateUrl="~/Parceiros.aspx" runat="server">> Parceiros</asp:HyperLink>
<asp:HyperLink ID="linkDocumentos" NavigateUrl="~/Documentos.aspx" runat="server">> Documentos</asp:HyperLink>
<asp:HyperLink ID="linkAcao" runat="server" NavigateUrl="~/AcaoDetalhes.aspx">> Detalhamento da Ação</asp:HyperLink>
<asp:HyperLink ID="linkFinanceiro" runat="server" NavigateUrl="~/Financeiro.aspx">> Detalhamento do Financeiro</asp:HyperLink>
<asp:HyperLink ID="linkProduto" runat="server" NavigateUrl="~/ProdutoDetalhes.aspx">> Detalhamento da Meta Física</asp:HyperLink>
<asp:HyperLink ID="linkRelProduto" runat="server" NavigateUrl="~/Rel_MetaFisica.aspx">> Relatório de Metas Físicas</asp:HyperLink>
<asp:HyperLink ID="linkAcaoGraf" runat="server" NavigateUrl="~/AcaoCronograma.aspx">> Cronograma das Ações</asp:HyperLink>
<asp:HyperLink ID="linkRestricaoSup" runat="server" NavigateUrl="~/RestricaoSuperada.aspx">> Restrições Superadas</asp:HyperLink>
<asp:HyperLink ID="linkRestricao" runat="server" NavigateUrl="~/RestricaoDetalhes.aspx">> Detalhamento da Restrição</asp:HyperLink>
</div>