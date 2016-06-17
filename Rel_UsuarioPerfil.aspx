<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rel_UsuarioPerfil" Codebehind="Rel_UsuarioPerfil.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:GridView ID="GridView1" ShowFooter="True" 
    DataKeyNames="t02_cd_usuario" Width="100%"
    EmptyDataText="Nenhum registro encontrado" OnRowDataBound="GridView1_RowDataBound"
    runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" 
        GridLines="None">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
        <asp:BoundField DataField="t02_cd_usuario" HtmlEncode="false" HeaderText="Usuário" SortExpression="t02_cd_usuario" ></asp:BoundField>
        <asp:BoundField DataField="nm_nome" HtmlEncode="false" HeaderText="Nome" SortExpression="nm_nome" ></asp:BoundField>
        <asp:BoundField DataField="nm_entidade" HtmlEncode="false" HeaderText="Entidade" SortExpression="nm_entidade" ></asp:BoundField>
        <asp:BoundField DataField="nm_nome" HtmlEncode="false" HeaderText="Perfil" SortExpression="nm_nome" ></asp:BoundField>
        <asp:TemplateField HeaderText="Gerente">
        <ItemTemplate>
        
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Responsável pelo Monitoramento">
        <ItemTemplate>
        
        </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Linha Gerencial">
        <ItemTemplate>
        
        </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Coordenador de ação">
        <ItemTemplate>
        
        </ItemTemplate>
        </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
</asp:Content>

