<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="frmFonte"  Codebehind="frmFonte.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7"><asp:Label ID="lblTitle" runat="server"></asp:Label></h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 

    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
   
    <asp:Panel ID="PanelForm" Visible="false" runat="server">
    <fieldset>
    <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
    <div class="formElement">
        
        <table>
        <tr>
        <td><label>Entidade</label></td>
        <td>  <asp:TextBox ID="txtnm_fonte" Width="250px" MaxLength="500" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtnm_fonte"
                 runat="server" ErrorMessage="*campo obrigatório" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
        <td></td>
        <td>
        
        <span class="button"><asp:Button ID="btnSalvar" runat="server" Text="Salvar" onclick="btnSalvar_Click" /></span>&nbsp;
         <span class="button"><asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
              CausesValidation="false" onclick="btnCancelar_Click" /></span>
        
              </td>
        </tr>
        </table>
        </div>                
        </fieldset>  
                
    </asp:Panel>
        
        <asp:Panel ID="PanelGrid" runat="server">
        <span class="button"><asp:Button ID="btnNovo" runat="server" Text="Cadastrar Novo" onclick="btnNovo_Click" /></span>
        <asp:Literal ID="LiteralGridView1"  runat="server"></asp:Literal>
        <asp:GridView ID="GridView1" DataKeyNames="t27_cd_fonte" Width="100%" GridLines="Both"
            OnRowCreated="GridView1_RowCreated" CssClass="tablesorterYellow" OnPreRender="GridView1_PreRender"  ShowFooter="true" 
             OnRowDataBound="GridView1_RowDataBound"  OnRowCommand="GridView1_RowCommand"
            EmptyDataText="Nenhum registro encontrado" runat="server" CellPadding="4" 
            ForeColor="#333333" AutoGenerateColumns="False">
            <Columns>
            <asp:ButtonField ButtonType="Image" CommandName="Editar" AccessibleHeaderText="Editar" 
                ImageUrl="~/_assets/img/edit.png"  Text="Editar"  ItemStyle-Width="1%" 
                ItemStyle-HorizontalAlign="Center" />
            <asp:ButtonField ButtonType="Image" CommandName="Deletar" AccessibleHeaderText="Excluir" 
                ImageUrl="~/_assets/img/del.png"  Text="Excluir"  ItemStyle-Width="1%" 
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="nm_fonte" HtmlEncode="false" 
                HeaderText="Entidade" SortExpression="nm_fonte" />
            </Columns>
            </asp:GridView>
        </asp:Panel>
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />     
</asp:Content>

