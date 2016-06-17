<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="RelAcoesVinculadas" CodeBehind="Rel_AcoesVinculadas.aspx.cs" ResponseEncoding="ISO-8859-1"
    UICulture="pt-BR" Culture="pt-BR" %>

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
        th.titulos1
        {
            background-color: #6A5ACD;
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
        .ico_excel
        {
            background: url(images/ico_excel.png) no-repeat 5px center;
            padding-left: 25px;
            margin-bottom: 5px;
            line-height: 30px;
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
            <asp:Label ID="lblTituloProjeto" runat="server"></asp:Label>
        </h2>
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
                            <asp:Label ID="lblFiltro2" runat="server" Text="Projeto Sustentador:"></asp:Label>&nbsp;
                            <asp:DropDownList ID="ddlProjeto" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProjeto_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlProjeto"
                                Display="Dynamic" runat="server" ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:Label ID="lblFiltro1" runat="server" Text="Apresentar apenas projetos sustentadores da &aacute;rea de resultado:"></asp:Label>&nbsp;
                            <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;
                            <%--Ou--%>
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" />
            </div>
            <div style="text-align: right">
                <asp:Button ID="btnExportar" Visible="false" runat="server" Text="Exportar para Excel"
                    OnClick="btnExportar_Click" />
            </div>
            <br />
            <asp:Repeater ID="rptProjetos" runat="server" OnItemDataBound="rptProjetos_OnItemDataBound">
                <HeaderTemplate>
                    <table cellpadding="2" cellspacing="2" class="tbRestricao" width="100%">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td colspan="3" style="color: White; background-color: #8B7D6B; font-size: 10pt;
                            font-weight: bold; line-height: 35px; width: 80%">
                            <asp:Label ID="lblProjetoTitulo" runat="server"></asp:Label>
                        </td>
                        <td style="color: White; background-color: #8B7D6B; font-size: 10pt; font-weight: bold;
                            width: 20%">
                            Data Impress&atilde;o:&nbsp;<asp:Literal ID="ltrDataImpressao" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptProjetosPai" runat="server" OnItemDataBound="rptProjetosPai_OnItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <th style="background-color: #FFA500; text-align: left; color: #FFF; font-weight: bold;
                                    border: 1px solid #666666;" width="12%">
                                    &Aacute;rea de Resultado:
                                </th>
                                <td colspan="3" style="width: 80%; background-color: #FFA500">
                                    <asp:Literal ID="ltrAreaResultado" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <th style="background-color: #0C3E8F; text-align: left; color: #FFF; font-weight: bold;
                                    border: 1px solid #666666;" width="12%">
                                    Projeto Sustentador:
                                </th>
                                <td style="width: 50%; background-color: #F0FFFF">
                                    <asp:Literal ID="ltrProjetoSustentador" runat="server"></asp:Literal>
                                </td>
                                <th style="background-color: #0C3E8F; text-align: left; color: #FFF; font-weight: bold;
                                    border: 1px solid #666666;">
                                    Gerente:
                                </th>
                                <td style="width: 20%; background-color: #F0FFFF">
                                    <%--line-height:5%;--%>
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
                                        <th style="background-color: #70A2F3; text-align: left; color: #FFF; font-weight: bold;
                                            border: 1px solid #666666;">
                                            A&ccedil;&atilde;o:
                                        </th>
                                        <td style="width: 50%">
                                            <asp:Literal ID="ltrAcao" runat="server"></asp:Literal>
                                        </td>
                                        <th style="background-color: #70A2F3; text-align: left; color: #FFF; font-weight: bold;
                                            border: 1px solid #666666;" style="text-align: left" colspan="1">
                                            Data Prevista para T&eacute;rmino
                                        </th>
                                        <td style="width: 20%">
                                            <asp:Literal ID="ltrDataPrevTermino" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptMetas" runat="server" OnItemDataBound="rptMetas_OnItemDataBound">
                                        <HeaderTemplate>
                                            <tr>
                                                <th colspan="2" style="background-color: #CCC; text-align: left; color: #333; font-weight: bold;
                                                    border: 1px solid #666666; width: 50%" height="20">
                                                    Meta F&iacute;sica:
                                                </th>
                                                <th style="background-color: #CCC; text-align: left; color: #333; font-weight: bold;
                                                    border: 1px solid #666666;">
                                                    <asp:Literal ID="ltrHeadAteMes" runat="server"></asp:Literal><br />
                                                    Realizado / Previsto(%):
                                                </th>
                                                <th style="background-color: #CCC; text-align: left; color: #333; font-weight: bold;
                                                    border: 1px solid #666666; width: 20%">
                                                    Total Geral
                                                    <br />
                                                    Realizado / Previsto(%):
                                                </th>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Literal ID="ltrDescricaoMeta" runat="server"></asp:Literal>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Literal ID="ltrPorcentoAteMes" runat="server"></asp:Literal>
                                                </td>
                                                <td style="width: 20%; text-align: right;">
                                                    <asp:Literal ID="ltrPorcento" runat="server"></asp:Literal>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:Repeater>
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
