<%@ Control Language="C#" AutoEventWireup="true" Inherits="CustomControls_ucResponsaveis" Codebehind="~/CustomControls/ucResponsaveis.ascx.cs" %>
<div style="border: solid 1px #829AAF; padding:5px;margin: 5px 0 5px 0">
<h3 style="color:#113455;margin: 5px 0 10px 0;">Gerente</h3>
<asp:Label ID="lbldados_gerente" runat="server"></asp:Label>
<div style="display:none">
<h3 style="color:#113455;margin: 5px 0 10px 0;">Resposável pelo Monitoranto</h3>
<asp:Label ID="lbldados_monitoramento" runat="server"></asp:Label>
</div>
</div>
<asp:UpdatePanel ID="updatePanel" runat="server">
<ContentTemplate> 
<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

<span class="button" runat="server" id="spanbtnNovo"><asp:Button ID="btnNovo" CausesValidation="false" runat="server" Text="Cadastrar Novo" onclick="btnNovo_Click" /></span>
 <asp:GridView ID="GridView1" DataKeyNames="t06_cd_colaborador" Width="100%" GridLines="Both"
     CssClass="tablesorterdisable" 
     OnRowDataBound="GridView1_RowDataBound"  OnRowCommand="GridView1_RowCommand" 
     EmptyDataText="Nenhum registro encontrado" runat="server" CellPadding="4" 
     AutoGenerateColumns="False">
     <AlternatingRowStyle CssClass="odd" />
     <Columns>
        <asp:ButtonField ButtonType="Image" CommandName="Editar" AccessibleHeaderText="Editar" 
            ImageUrl="~/_assets/img/edit.png"  Text="Editar"  ItemStyle-Width="1%" 
            ItemStyle-HorizontalAlign="Center" />
        <asp:ButtonField ButtonType="Image" CommandName="Deletar" AccessibleHeaderText="Excluir" 
            ImageUrl="~/_assets/img/del.png"  Text="Excluir"  ItemStyle-Width="1%" 
            ItemStyle-HorizontalAlign="Center" />  
        <asp:TemplateField HeaderText="Ordem" ItemStyle-Width="1%">
            <ItemTemplate>
                <asp:TextBox id="txtnu_ordem" Width="30px"  MaxLength="3" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>                     
        <asp:BoundField DataField="nm_funcao" HeaderStyle-Width="30%" HeaderText="Cargo" />
        <asp:BoundField DataField="nm_entidade" HeaderText="Entidade" />
        <asp:BoundField DataField="nm_nome" HeaderStyle-Width="30%" HeaderText="Responsável" />
     </Columns>
  </asp:GridView>  
<span class="button" runat="server" id="spanbtnOrdem"><asp:Button ID="btnOrdem" CausesValidation="false" runat="server" Text="Salvar ordem" onclick="btnOrdem_Click" /></span>
            <ajaxToolKit:ModalPopupExtender ID="mdlPopup" 
                runat="server" TargetControlID="btnShow" 
                PopupControlID="pnlPopup" BackgroundCssClass="modalBackground" />
            <asp:Button ID="btnShow" style="display:none;" runat="server" />
            
            <asp:Panel ID="pnlPopup" runat="server" CssClass="confirm-dialog" style="display:none;">
                <div class="inner">
                    <h2><asp:Label ID="lblHeader" runat="server"></asp:Label></h2>
                    <div class="base">
                       <table class="table">                                              
                        <tr>
                        <td><label>Responsável</label></td>
                        <td>  
                            <asp:DropDownList ID="ddlt02_cd_usuario" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator  ValidationGroup="t06" ID="RequiredFieldValidator7" runat="server" Display="Dynamic"
                            ErrorMessage="*campo obrigatório" ControlToValidate="ddlt02_cd_usuario"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                        <tr>
                        <td></td>
                        <td>
                        
                        <span class="button"><asp:Button ID="btnSalvar" ValidationGroup="t06" runat="server" Text="Salvar" onclick="btnSalvar_Click" /></span>&nbsp;
                         <span class="button"><asp:Button ID="btnCancelar"  ValidationGroup="t06" runat="server" Text="Cancelar" 
                              CausesValidation="false" onclick="btnCancelar_Click" /></span>
                        
                              </td>
                        </tr>
                        </table>
                       
                    </div>
                </div>
            </asp:Panel>
            
</ContentTemplate>
</asp:UpdatePanel>

<asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="updatePanel" 
runat="server">
<ProgressTemplate>
    <asp:Image ID="Image1" ImageUrl="~/images/pleasewait.gif" 
    ToolTip="Aguarde" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>