<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="RelAcoes" CodeBehind="Rel_Acoes.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        th.titulos
        {
            background-color: #0C3E8F;
            text-align: left;
            color: #FFF;
            font-weight: bold;
            border: 1px solid #666666;
        }
        th.subtituloAcao
        {
            background-color: #CCC;
            text-align: left;
            color: #333;
            font-weight: bold;
            border: 1px solid #666666;
        }
        th.subtitulos
        {
            background-color: #70A2F3;
            text-align: left;
            color: #FFF;
            font-weight: bold;
            border: 1px solid #666666;
        }
        .data
        {
            text-align: center;
        }
        table.tbRestricao
        {
            border: 1px solid #CCC;
        }
        table.tbRestricao tr td
        {
            border: 1px solid #CCC;
        }
        .filtro
        {
            font-weight: bold;
            font-size: 12px;
            padding-bottom: 20px;
        }
        .filtro p
        {
            padding-bottom: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc:Nav ID="ucNav" runat="server" Visible="false" restricaoSup="true" />
    <br />
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    <div class="heading_container">
        <div class="heading_right_top">
        </div>
        <h2 id="H7">
            Relatório de Monitoramento - Ações</h2>
    </div>
    <div class="bucket_container">
        <div class="bucket_top">
            <span></span>
        </div>
        <div class="bucket_content">
            <div class="clear">
            </div>
            <div class="filtro">
                <asp:UpdatePanel ID="uppFiltros" runat="server">
                    <ContentTemplate>
                        <p>
                            <asp:Label ID="lblFiltro1" runat="server" Text="Área de Resultado:"></asp:Label>&nbsp;
                            <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                            </asp:DropDownList>
                        </p>
                        <p>
                            <asp:Label ID="lblFiltro2" runat="server" Text="Projeto Sustentador:"></asp:Label>&nbsp;
                            <asp:DropDownList ID="ddlProjeto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProjeto_SelectedIndexChanged">
                            </asp:DropDownList>
                        </p>
                        <p>
                            <asp:Label ID="lblfiltro3" runat="server" Text="Responsável:"></asp:Label>&nbsp;
                            <asp:DropDownList ID="ddlResponsavel" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" />
            </div>
            <asp:Repeater ID="rptProjetos" runat="server" OnItemDataBound="rptProjetos_OnItemDataBound">
                <HeaderTemplate>
                    <table cellpadding="2" cellspacing="2" class="tbRestricao">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="border: 1px solid Red;">
                        <td colspan="7">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th class="titulos" width="12%">
                            Área de Resultado:
                        </th>
                        <td>
                            <asp:Literal ID="ltrAreaResultado" runat="server"></asp:Literal>
                        </td>
                        <th class="titulos">
                            Data Impressão:
                        </th>
                        <td>
                            <asp:Literal ID="ltrDataImpressao" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th class="titulos" width="12%">
                            Projeto Sustentador:
                        </th>
                        <td>
                            <asp:Literal ID="ltrProjetoSustentador" runat="server"></asp:Literal>
                        </td>
                        <th class="titulos">
                            Gerente:
                        </th>
                        <td>
                            <asp:Literal ID="ltrGerente" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <!-- Divisao -->
                    <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <asp:Repeater ID="rptAcoes" runat="server" OnItemDataBound="rptAcoes_OnItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <th class="subtitulos">
                                    Ação:
                                </th>
                                <td>
                                    <asp:Literal ID="ltrAcao" runat="server"></asp:Literal>
                                </td>
                                <th class="subtitulos" style="text-align: center;">
                                    Data Prevista para Término
                                </th>
                                <td>
                                    <asp:Literal ID="ltrDataPrevTermino" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <th class="subtitulos">
                                    Responsável:
                                </th>
                                <td colspan="3">
                                    <%#Eval("nm_responsavel")%>
                                </td>
                            </tr>
                            <asp:Repeater ID="rptMetas" runat="server" OnItemDataBound="rptMetas_OnItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <th colspan="3" class="subtituloAcao" height="20">
                                            Meta Física:
                                        </th>
                                        <th class="subtituloAcao">
                                            Realizado / Previsto(%):
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td width="27%" colspan="3">
                                            <asp:Literal ID="ltrDescricaoMeta" Text="Meta física não cadastrada" runat="server"></asp:Literal>
                                        </td>
                                        <td width="27%">
                                            <asp:Literal ID="ltrPorcento" Text="0,00" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Panel ID="pnlMetaFisica" runat="server" Visible="false">
                                <tr>
                                    <td width="27%" colspan="3">
                                        <asp:Literal ID="ltrDescricaoMeta" Text="Meta física não cadastrada" runat="server"></asp:Literal>
                                    </td>
                                    <td width="27%">
                                        <asp:Literal ID="ltrPorcento" Text="0,00" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Label ID="lblEmptyData" runat="server" Visible="false" EnableViewState="false"></asp:Label>
            <div class="clear">
            </div>
        </div>
        <div class="bucket_bottom">
            <span></span>
        </div>
    </div>
    <br />
</asp:Content>
