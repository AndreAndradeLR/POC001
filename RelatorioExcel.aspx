<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="RelatorioExcel" CodeBehind="RelatorioExcel.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <table>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlMes" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAno" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" />
                </td>
            </tr>
        </table>
        <div style="float: right;">
            <asp:Button ID="btnExcel" Visible="false" runat="server" Text="Exportar Excel" OnClick="btnExcel_Click" />
        </div>
        <br /><br />
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound"
            GridLines="None" AutoGenerateColumns="False" HorizontalAlign="Center" BorderColor="#FFFFFF"
            BorderStyle="Double">
            <RowStyle BackColor="#E3EAEB" ForeColor="#333333" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="nm_area" HtmlEncode="false" HeaderText="Área Resultado"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="nm_projeto" HtmlEncode="false" HeaderText="Projeto" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="nm_acao" HtmlEncode="false" HeaderText="Ação" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="nm_fonte" HtmlEncode="false" HeaderText="Fonte" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="nu_ano" HtmlEncode="false" HeaderText="Mês" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="nu_ano" HtmlEncode="false" HeaderText="Ano" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="vl_dotorcado" HtmlEncode="false" HeaderText="Dotação Orçamentária"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="vl_assegurado" HtmlEncode="false" HeaderText="Assegurado"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="vl_assegurado" HtmlEncode="false" HeaderText="Resto a pagar"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="vl_restopagar" HtmlEncode="false" HeaderText="Planejado"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="vl_dotorcado" HtmlEncode="false" HeaderText="Revisado"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="vl_dotorcado" HtmlEncode="false" HeaderText="Provisionado"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="vl_dotorcado" HtmlEncode="false" HeaderText="Empenhado"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="vl_dotorcado" HtmlEncode="false" HeaderText="Liquidado"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView2_RowDataBound"
            GridLines="None" AutoGenerateColumns="False" HorizontalAlign="Center" BorderColor="#FFFFFF"
            BorderStyle="Double">
            <RowStyle BackColor="#E3EAEB" ForeColor="#333333" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="nm_area" HtmlEncode="false" HeaderText="Área Resultado"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="nm_projeto" HtmlEncode="false" HeaderText="Projeto" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="nm_acao" HtmlEncode="false" HeaderText="Ação" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="nm_fonte" HtmlEncode="false" HeaderText="Fonte" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="nu_ano" HtmlEncode="false" HeaderText="Ano" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="vl_dotorcado" HtmlEncode="false" HeaderText="Dotação Orçamentária"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="vl_assegurado" HtmlEncode="false" HeaderText="Assegurado"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="vl_assegurado" HtmlEncode="false" HeaderText="Resto a pagar"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="vl_restopagar" HtmlEncode="false" HeaderText="Planejado"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="vl_dotorcado" HtmlEncode="false" HeaderText="Revisado"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="vl_dotorcado" HtmlEncode="false" HeaderText="Provisionado"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="vl_dotorcado" HtmlEncode="false" HeaderText="Empenhado"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="vl_dotorcado" HtmlEncode="false" HeaderText="Liquidado"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
