<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Rel_Restricao" CodeBehind="Rel_Restricao.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        th.titulos { background-color: #0C3E8F; text-align: left; color: #FFF; font-weight: bold; border: 1px solid #666666; }
        th.subtitulos { background-color: #70A2F3; text-align: left; color: #FFF; font-weight: bold; border: 1px solid #666666; }
        .data { text-align: center; }
        table.tbRestricao { border: 1px solid #CCC; }
        table.tbRestricao tr td { border: 1px solid #CCC; }
        .filtro { font-weight: bold; font-size: 12px; padding-bottom: 20px; }
        .filtro p { padding-bottom: 10px; }
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
            Relatório de Monitoramento - Restrições</h2>
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
                            <asp:Label ID="lblFiltro3" runat="server" Text="Responsável:"></asp:Label>&nbsp;
                            <asp:DropDownList ID="ddlResponsavel" runat="server">
                            </asp:DropDownList>
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <p>
                    <asp:Label ID="lblFiltro4" runat="server" Text="Filtro Por Data:"></asp:Label>&nbsp;
                    <asp:DropDownList ID="ddlData" runat="server">
                        <asp:ListItem Text="Todas" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Igual a Data Atual" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Maior ou Igual a Data Atual" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Menor ou Igual a Data Atual" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </p>
                <p>
                    <asp:Label ID="lblFiltro5" runat="server" Text="Filtrar:"></asp:Label>&nbsp;
                    <asp:RadioButtonList ID="rblEstado" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Todas" Value="" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Restrições Superadas" Value="Superadas"></asp:ListItem>
                        <asp:ListItem Text="Restrições Não Superadas" Value="NSuperadas"></asp:ListItem>
                    </asp:RadioButtonList>
                </p>
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" />
            </div>
            <asp:Repeater ID="rptProjetos" runat="server" OnItemDataBound="rptProjetos_OnItemDataBound">
                <HeaderTemplate>
                    <table cellpadding="2" width="100%" cellspacing="2" class="tbRestricao">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="border: 1px solid Red;">
                        <td colspan="9">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th class="titulos" width="12%">
                            Área de Resultado:
                        </th>
                        <td colspan="4">
                            <asp:Literal ID="ltrAreaResultado" runat="server"></asp:Literal>
                        </td>
                        <th class="titulos" colspan="3">
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
                        <td colspan="4">
                            <asp:Literal ID="ltrProjetoSustentador" runat="server"></asp:Literal>
                        </td>
                        <th class="titulos" colspan="3">
                            Gerente:
                        </th>
                        <td>
                            <asp:Literal ID="ltrGerente" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <!-- Divisao -->
                    <tr>
                        <td colspan="9">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9">
                            <asp:GridView ID="GridView1" Width="100%" GridLines="Both" CssClass="tablesorterYellow"
                                ShowFooter="true" OnRowDataBound="GridView1_RowDataBound" OnPreRender="GridView1_PreRender"
                                EmptyDataText="Nenhum registro encontrado" runat="server" CellPadding="4" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Restrição" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data Inclusão" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data Limite Restrição" Visible="false" HeaderStyle-Width="50px"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Providência" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Responsável" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data Limite" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Marcos Críticos Relacionados" HeaderStyle-Width="120px">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <!-- repeat projeto -->
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
