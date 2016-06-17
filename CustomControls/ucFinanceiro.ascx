<%@ Control Language="C#" AutoEventWireup="true" Inherits="CustomControls_ucFinanceiro" Codebehind="ucFinanceiro.ascx.cs" %>
<asp:Literal ID="Literal1" runat="server"></asp:Literal>
<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

<asp:Panel ID="PanelGrid" runat="server">
 <asp:Panel ID="PanelbtnNovo" runat="server">
  <span class="button"><asp:Button ID="btnNovo" CausesValidation="false" runat="server" Text="Cadastrar Novo" onclick="btnNovo_Click" /></span>
 </asp:Panel>
 <asp:GridView ID="GridView1" DataKeyNames="t11_cd_financeiro" Width="100%" GridLines="Both"
     CssClass="tablesorterdisable" 
     OnRowDataBound="GridView1_RowDataBound"  OnRowCommand="GridView1_RowCommand" 
     EmptyDataText="Nenhum registro encontrado" runat="server" CellPadding="4" 
     AutoGenerateColumns="False" ShowFooter="true">
     <AlternatingRowStyle CssClass="odd" />
     <FooterStyle HorizontalAlign="Right" Font-Bold="true" BackColor="#efefef" />
     <Columns>
            <asp:ButtonField ButtonType="Image" CommandName="Editar" AccessibleHeaderText="Editar" 
                ImageUrl="~/_assets/img/edit.png"  Text="Editar"  ItemStyle-Width="1%" 
                ItemStyle-HorizontalAlign="Center" />
            <asp:ButtonField ButtonType="Image" CommandName="Deletar" AccessibleHeaderText="Excluir" 
                ImageUrl="~/_assets/img/del.png"  Text="Excluir"  ItemStyle-Width="1%" 
                ItemStyle-HorizontalAlign="Center" />     
        
         <asp:ButtonField ButtonType="Image" CommandName="Selecionar" ImageUrl="~/images/lupa.gif" Text="Exibir Detalhes"  ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" />
         <asp:BoundField DataField="nm_fonte" ItemStyle-Wrap="true" HeaderText="Fonte" />
         <asp:TemplateField HeaderText="Assegurado" ItemStyle-HorizontalAlign="Right">
         <ItemTemplate></ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Planejado" ItemStyle-HorizontalAlign="Right">
         <ItemTemplate></ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Revisado" ItemStyle-HorizontalAlign="Right">
         <ItemTemplate></ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField  HeaderText="Liquidado" ItemStyle-HorizontalAlign="Right">
         <ItemTemplate></ItemTemplate>
         </asp:TemplateField>
          <asp:TemplateField  HeaderText="Liquidado/ Planejado (%)" ItemStyle-HorizontalAlign="Right">
         <ItemTemplate></ItemTemplate>
         </asp:TemplateField>
     </Columns>
  </asp:GridView>  
</asp:Panel>
    <asp:Panel ID="PanelForm" Visible="false" runat="server">
    <fieldset>
    <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
    <div class="formElement">
                    <table>
                        <tr>
                    <td>
                       <label>Fonte:</label> 
                       <asp:DropDownList ID="ddlt27_cd_fonte" runat="server">
                        </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="t11"
                     ControlToValidate="ddlt27_cd_fonte" Display="Dynamic" runat="server" 
                     ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>                               
                            </td>
                </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelP" Width="700px" ScrollBars="Horizontal" runat="server">
                            <uc:LancamentoFinanceiro ID="ucLancFin" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>                                       
                                               
                    <tr>
                     <td>
                        <span class="button"><asp:Button ID="btnSalvar" ValidationGroup="t11" runat="server" Text="Salvar" onclick="btnSalvar_Click" /></span>&nbsp;
                         <span class="button"><asp:Button ID="btnCancelar"  ValidationGroup="t11" runat="server" Text="Cancelar" 
                              CausesValidation="false" onclick="btnCancelar_Click" /></span>
                        
                              </td>
                    </tr>
                    </table>
                       
                    </div>
                </div>
        </fieldset>
      </asp:Panel>
            
