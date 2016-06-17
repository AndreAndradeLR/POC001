<%@ Control Language="C#" AutoEventWireup="true" Inherits="CustomControls_ucAcoesVinculadasProjeto" Codebehind="ucAcoesVinculadasProjeto.ascx.cs" %>
<asp:Literal ID="Literal1" runat="server"></asp:Literal>
<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

<asp:Panel ID="PanelGridAcoesVincProj" Visible="true" runat="server">
 <asp:Panel ID="PanelbtnNovo" Visible="true" runat="server">
   <span class="button"><asp:Button ID="btnNovo1" CausesValidation="false" runat="server" Text="Cadastrar Novo" Visible="true"  onclick="btnNovo1_Click" /></span>
 </asp:Panel>
 <asp:GridView ID="GridView1" DataKeyNames="cd_acoes_vinculadas_projeto" Width="100%"
     CssClass="tablesorterdisable" 
     OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand"  
     EmptyDataText="Nenhum registro encontrado" runat="server" CellPadding="4" 
     AutoGenerateColumns="False" EnableModelValidation="True">
     <AlternatingRowStyle CssClass="odd" />
     <FooterStyle HorizontalAlign="Right" Font-Bold="true" BackColor="#efefef" />
     <Columns>
            <asp:ButtonField ButtonType="Image" CommandName="Deletar" AccessibleHeaderText="Excluir" 
                ImageUrl="~/_assets/img/del.png"  Text="Excluir"  ItemStyle-Width="1%" 
                ItemStyle-HorizontalAlign="Center">
            <ItemStyle HorizontalAlign="Center" Width="1%" />
            </asp:ButtonField>
            <asp:BoundField HeaderText="Projetos" DataField="nm_projeto"/>

     </Columns>
  </asp:GridView>  

  <ajaxToolKit:ModalPopupExtender ID="mdlPopup" 
                runat="server" TargetControlID="btnNovo" 
                PopupControlID="pnlPopup" BackgroundCssClass="modalBackground" />
            <asp:Button ID="btnNovo" style="display:none;" runat="server" />
            
            <asp:Panel ID="pnlPopup" runat="server" CssClass="confirm-dialog" style="display:none;">
                <div class="inner">
                    
    <h2><asp:Label ID="lblHeader" Text="Cadastrar Novo" runat="server"></asp:Label></h2>
       <div class="base">
                       <table class="table">
                        <tr>
                    <td>
                       <label>Projeto:</label> 
                       <asp:DropDownList ID="ddlt03_projeto" runat="server" Height="23px" Width="417px">
                        </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="t30"
                     ControlToValidate="ddlt03_projeto" Display="Dynamic" runat="server" 
                     ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>                               
                            </td>
                </tr>
                    <tr>
                     <td>
                        <span class="button"><asp:Button ID="btnSalvar" ValidationGroup="t30" runat="server" Text="Salvar" onclick="btnSalvar_Click" /></span>&nbsp;
                         <span class="button"><asp:Button ID="btnCancelar"  ValidationGroup="t30" runat="server" Text="Cancelar" 
                              CausesValidation="false" onclick="btnCancelar_Click" /></span>
                        
                              </td>
                    </tr>
                    </table>
                       
                    </div>
                </div>
            </asp:Panel>

</asp:Panel>
   
            
