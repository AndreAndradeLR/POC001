<%@ Control Language="C#" AutoEventWireup="true" Inherits="CustomControls_ucSituacoes"
    CodeBehind="ucSituacoes.ascx.cs" %>
<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
<b>Projeto: </b>
<asp:DropDownList ID="ddlt03_cd_projeto" runat="server">
</asp:DropDownList>
&nbsp; <span id="invalid-project" style="color: Red"></span>
<br />
<b>Período: A partir de</b>
<asp:DropDownList ID="ddlMes" runat="server">
    <asp:ListItem Text="Todos" Value="" Selected="True"></asp:ListItem>
    <asp:ListItem Text="Janeiro" Value="1"></asp:ListItem>
    <asp:ListItem Text="Fevereiro" Value="2"></asp:ListItem>
    <asp:ListItem Text="Março" Value="3"></asp:ListItem>
    <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
    <asp:ListItem Text="Maio" Value="5"></asp:ListItem>
    <asp:ListItem Text="Junho" Value="6"></asp:ListItem>
    <asp:ListItem Text="Julho" Value="7"></asp:ListItem>
    <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
    <asp:ListItem Text="Setembro" Value="9"></asp:ListItem>
    <asp:ListItem Text="Outubro" Value="10"></asp:ListItem>
    <asp:ListItem Text="Novembro" Value="11"></asp:ListItem>
    <asp:ListItem Text="Dezembro" Value="12"></asp:ListItem>
</asp:DropDownList>
<b>/</b>
<asp:DropDownList ID="ddlAno" runat="server">
</asp:DropDownList>
&nbsp; <span id="invalid-date" style="color: Red"></span>
<br />
<asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" ValidationGroup="btnFiltrar"
    OnClick="ddlt03_cd_projeto_SelectedIndexChanged" OnClientClick="return Validator();" />
<asp:GridView ID="GridView1" DataKeyNames="t05_cd_situacao" Width="100%" GridLines="Both"
    OnRowCreated="GridView1_RowCreated" CssClass="tablesorterYellow" OnPreRender="GridView1_PreRender"
    ShowFooter="true" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand"
    EmptyDataText="Nenhum registro encontrado" runat="server" CellPadding="4" AutoGenerateColumns="False">
    <AlternatingRowStyle CssClass="odd" />
    <Columns>
        <asp:ButtonField ButtonType="Image" CommandName="Deletar" AccessibleHeaderText="Excluir"
            ImageUrl="~/_assets/img/del.png" Text="Excluir" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" />
        <asp:BoundField DataField="ds_situacao" HtmlEncode="false" HeaderText="Situação" />
        <asp:BoundField DataField="dt_cadastro" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
            NullDisplayText="-" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" HeaderText="Data" />
    </Columns>
</asp:GridView>
