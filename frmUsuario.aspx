<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="frmUsuario"  Codebehind="frmUsuario.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7"><asp:Label ID="lblTitle" runat="server"></asp:Label></h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
    
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
    
    <asp:Panel ID="PanelGrid" runat="server">
        <span class="button">
        <asp:Button ID="btnCadastro"  OnClick="btnCadastro_Click" EnableViewState="false" runat="server" Text="Cadastrar Novo" />
        </span>
    
    <asp:Literal ID="LiteralGridView1"  runat="server"></asp:Literal>
    <asp:GridView ID="GridView1" 
    OnRowCreated="GridView1_RowCreated" CssClass="tablesorterYellow" OnPreRender="GridView1_PreRender"  ShowFooter="true" 
    DataKeyNames="t02_cd_usuario" Width="100%"
    EmptyDataText="Nenhum registro encontrado" OnRowDataBound="GridView1_RowDataBound"
    OnRowCommand="GridView1_RowCommand" 
    runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False">
        <Columns>
            <asp:ButtonField ButtonType="Image" CommandName="Editar" AccessibleHeaderText="Editar" 
                ImageUrl="~/_assets/img/edit.png"  Text="Editar"  ItemStyle-Width="1%" 
                ItemStyle-HorizontalAlign="Center" />
            <asp:ButtonField ButtonType="Image" CommandName="Deletar" AccessibleHeaderText="Excluir" 
                ImageUrl="~/_assets/img/del.png"  Text="Excluir"  ItemStyle-Width="1%" 
                ItemStyle-HorizontalAlign="Center" />
        
        <asp:BoundField DataField="t02_cd_usuario" HtmlEncode="false" HeaderText="Usuário" SortExpression="t02_cd_usuario" ></asp:BoundField>
        <asp:BoundField DataField="nm_nome" HtmlEncode="false" HeaderText="Nome" SortExpression="nm_nome" ></asp:BoundField>
        <asp:BoundField DataField="nm_email" HtmlEncode="false" HeaderText="E-mail" SortExpression="nm_email" ></asp:BoundField>
        <asp:BoundField DataField="nm_telefone" HtmlEncode="false" NullDisplayText="-" HeaderText="Telefone" DataFormatString="{0:(##) ####-####}" SortExpression="nu_telefone" ></asp:BoundField>
        <asp:BoundField DataField="nm_celular" HtmlEncode="false" NullDisplayText="-" HeaderText="Celular" DataFormatString="{0:(##) ####-####}" SortExpression="nu_celular" ></asp:BoundField>
        <asp:BoundField DataField="nm_entidade" HtmlEncode="false" HeaderText="Entidade" SortExpression="nm_entidade" ></asp:BoundField>
        <asp:BoundField DataField="nm_cargo" HtmlEncode="false" HeaderText="Cargo" SortExpression="nm_cargo" ></asp:BoundField>
        
        <asp:BoundField DataField="nm_nome" HtmlEncode="false" HeaderText="Perfil" SortExpression="nm_nome" ></asp:BoundField>
        
        <asp:ButtonField ButtonType="Image" CommandName="Senha" AccessibleHeaderText="Alterar Senha" 
                ImageUrl="~/images/ico_key.gif"  Text="Alterar Senha"  ItemStyle-Width="1%" 
                ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
    </asp:Panel>
    
    <asp:Panel ID="PanelForm" Visible="false" runat="server">
    <fieldset>
       <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        <div class="formElement">
        <table>
        <tr>
            <td></td>
            <td></td>
            <td rowspan="10" style="vertical-align:top; padding-left:10px;border-left:solid 1px #999">
                 <div style="padding:5px">
                 <label style="font-weight:bold">Perfil:<br /></label> 
                     <asp:RadioButtonList ID="rblt24_cd_perfil" runat="server">
                     </asp:RadioButtonList>                                               
                 </div>
            </td>
            <td>
             <div id="clean" runat="server">              
              <img src="images/clear.gif" alt="Limpar Perfil" width="16" height="13"/> 
              <asp:LinkButton ID="btnLimparRbl" runat="server" ToolTip="Limpar Perfil" onclick="btnLimparRbl_Click">Limpar</asp:LinkButton>                                       
             </div>
            </td>
        </tr>
        <tr>
            <td><label>Nome:</label></td>
            <td><asp:TextBox  ID="txtnm_nome" Width="350px" MaxLength="200" runat="server"></asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator1" ControlToValidate="txtnm_nome" ErrorMessage="Favor digitar o Nome" runat="server"/></td>
        </tr>
        <tr id="trLogin" visible="false" runat="server">
            <td><label>Usuário:</label></td>
            <td><asp:TextBox  ID="txtt02_cd_usuario" MaxLength="20" runat="server"></asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator3" ControlToValidate="txtt02_cd_usuario" ErrorMessage="Favor digitar o Login" runat="server"/></td>
        </tr>
        <tr id="trSenha" visible="false" runat="server">
            <td><label>Senha:</label></td>
            <td><asp:TextBox  ID="txtpw_senha" TextMode="Password" MaxLength="10" runat="server"></asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator4" ControlToValidate="txtpw_senha" ErrorMessage="Favor digitar a Senha" runat="server"/></td>
        </tr>
        <tr id="trSenhaConf" visible="false" runat="server">
            <td><label>Confirma Senha:</label></td>
            <td><asp:TextBox  ID="txtpw_senha2" TextMode="Password" MaxLength="10" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator5" ControlToValidate="txtpw_senha2" ErrorMessage="Favor confirmar Senha"
                    runat="server" /><asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtpw_senha2"
                        ControlToCompare="txtpw_senha" ErrorMessage="Favor digitar a mesma Senha" runat="server" /></td>
        </tr>            

        <tr>
            <td><label>E-mail:</label></td>
            <td><asp:TextBox  ID="txtnm_email" Width="200px" MaxLength="100" runat="server"></asp:TextBox><asp:RequiredFieldValidator id="RequiredFieldValidator2" ControlToValidate="txtnm_email" ErrorMessage="Favor digitar o E-mail" runat="server"/></td>
        </tr>
        <tr>
            <td><label>Telefone:</label></td>
            <td><asp:TextBox  ID="txtnm_dddt" MaxLength="2" Width="15px" runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="*formato inválido" ControlToValidate="txtnm_dddt"
                 Display="Dynamic" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>   
             <asp:TextBox  ID="txtnm_telefone" MaxLength="8" Width="60px" runat="server"></asp:TextBox> 
             <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="*formato inválido" ControlToValidate="txtnm_telefone"
                 Display="Dynamic" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>   
             <small>(apenas números)</small></td>
        </tr>
        <tr>
            <td><label>Celular:</label></td>
            <td><asp:TextBox  ID="txtnm_dddc" MaxLength="2" Width="15px" runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*formato inválido" ControlToValidate="txtnm_dddc"
                 Display="Dynamic" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>   
             <asp:TextBox  ID="txtnm_celular" MaxLength="8"  Width="60px" runat="server"></asp:TextBox> 
             <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="*formato inválido" ControlToValidate="txtnm_celular"
                 Display="Dynamic" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>   <small>
                (apenas números)</small>
            </td>
        </tr>
        <tr>
            <td><label>Cargo:</label></td>
            <td><asp:TextBox  ID="txtnm_cargo" Width="200px" MaxLength="100" runat="server"></asp:TextBox></td>
        </tr>
        <tr >
            <td><label>Entidade:</label></td>
            <td>
                <asp:DropDownList ID="ddlt01_cd_entidade" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic"
                ErrorMessage="*campo obrigatório" ControlToValidate="ddlt01_cd_entidade"></asp:RequiredFieldValidator>
             </td>   
        </tr>   
        <tr>
            <td colspan="2">
               
                <span class="button"><asp:Button ID="btnAcao" OnClick="btnAcao_Click" runat="server" Text="Salvar"/></span>
                <span class="button"><asp:Button CssClass="btn" ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancelar" CausesValidation="False" /></span>
               
            </td>
        </tr>
        </table>
        </div>
      </fieldset>
    </asp:Panel>
    
    <asp:Panel ID="PanelSenha" Visible="false" runat="server">
    </asp:Panel>
    
    <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

