<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="frmProjeto"  Codebehind="frmProjeto.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7"><asp:Label ID="lblTitle" runat="server"></asp:Label></h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
    
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

    <asp:Panel ID="PanelGrid" runat="server">
        <span class="button"><asp:Button ID="btnCadastro"  OnClick="btnCadastro_Click" EnableViewState="false" runat="server" Text="Cadastrar Novo" /></span>
        <asp:Literal ID="LiteralGridView1"  runat="server"></asp:Literal>
        <asp:GridView ID="GridView1" DataKeyNames="t03_cd_projeto" Width="100%" GridLines="Both"
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
            <asp:BoundField DataField="nm_projeto" HtmlEncode="false" 
                HeaderText="Projeto"  />
            <asp:BoundField DataField="nm_area" HtmlEncode="false" 
                HeaderText="Área de Resultado"  />                                                
            <asp:BoundField DataField="nm_entidade" HtmlEncode="false" 
                HeaderText="Entidade"  />                
            <asp:BoundField DataField="nome_gerente" HtmlEncode="false" 
                HeaderText="Gerente" />                
            <asp:BoundField DataField="nome_monitoramento" HtmlEncode="false" 
                HeaderText="Responsável pelo Monitoramento"  />                                
            </Columns>
            </asp:GridView>        
    </asp:Panel>
    
    <asp:Panel ID="PanelForm" Visible="false" runat="server">
    <fieldset>
       <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        <div class="formElement">
        <table>
        <tr>
            <td><label>Título:</label></td>
            <td><asp:TextBox  ID="txtnm_projeto" Width="350px" MaxLength="200" runat="server"></asp:TextBox> <asp:RequiredFieldValidator id="RequiredFieldValidator1" ControlToValidate="txtnm_projeto" ErrorMessage="*campo obrigatório" runat="server"/></td>
        </tr>
        <tr>
            <td><label>Área de Resultado:</label></td>
            <td>
            <asp:DropDownList ID="ddlt26_cd_arearesultado" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator id="RequiredFieldValidator5" ControlToValidate="ddlt26_cd_arearesultado" ErrorMessage="*campo obrigatório" runat="server"/>
            </td>
        </tr>         
        <tr>
            <td><label>Entidade:</label></td>
            <td>
            <asp:DropDownList ID="ddlt01_cd_entidade" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator id="RequiredFieldValidator2" ControlToValidate="ddlt01_cd_entidade" ErrorMessage="*campo obrigatório" runat="server"/>
            </td>
        </tr>        
        <tr>
            <td><label>Gerente:</label></td>
            <td>
            <asp:DropDownList ID="ddlt02_cd_usuario" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator id="RequiredFieldValidator3" ControlToValidate="ddlt02_cd_usuario" ErrorMessage="*campo obrigatório" runat="server"/>
            </td>
        </tr>
        <tr>
            <td><label>Responsável <br />pelo Monitoramento:</label></td>
            <td>
            <asp:DropDownList ID="ddlt02_cd_usuario_monitoramento" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator id="RequiredFieldValidator4" ControlToValidate="ddlt02_cd_usuario_monitoramento" ErrorMessage="*campo obrigatório" runat="server"/>            
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <span class="button"><asp:Button ID="btnAcao" OnClick="btnAcao_Click" runat="server" Text="Salvar"/></span>
                <span class="button"><asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancelar" CausesValidation="False" /></span>
            </td>
        </tr>
        </table>
        </div>
      </fieldset>
    </asp:Panel>
    
    <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

