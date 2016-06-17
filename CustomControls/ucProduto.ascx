<%@ Control Language="C#" AutoEventWireup="true" Inherits="CustomControls_ucProduto" Codebehind="ucProduto.ascx.cs" %>
<asp:Literal ID="Literal1" runat="server"></asp:Literal>
<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

<asp:Panel ID="PanelGrid" runat="server">
 <asp:Panel ID="PanelbtnNovo" runat="server">
  <span class="button"><asp:Button ID="btnNovo" UseSubmitBehavior="false" CausesValidation="false" runat="server" Text="Cadastrar Novo" onclick="btnNovo_Click" /></span>
 </asp:Panel>

 <asp:GridView ID="GridView1" DataKeyNames="t10_cd_produto" Width="100%" GridLines="Both"
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
        
         <asp:ButtonField ButtonType="Image" CommandName="Selecionar" ImageUrl="~/images/lupa.gif" Text="Exibir Detalhes"  ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" />
         <asp:BoundField DataField="ds_produto" ItemStyle-Wrap="true" HeaderText="Título" />
         <asp:BoundField DataField="nm_medida" ItemStyle-Wrap="true" HeaderText="Unidade de Medida" />
         
         <asp:TemplateField HeaderText="Previsto">
         <ItemTemplate></ItemTemplate>
         </asp:TemplateField>
         
         <asp:TemplateField  HeaderText="Realizado">
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
                    <td style="vertical-align:top"><label>Título:</label></td>
                    <td>
                    <asp:TextBox  ID="txtds_produto" Width="400px" runat="server"  TextMode="MultiLine"
                            Height="40px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="t10"
                     ControlToValidate="txtds_produto" Display="Dynamic" runat="server" 
                     ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>                               
                            </td>
                </tr>
                <tr>
                    <td style="vertical-align:top"><label>Unidade de Medida:</label></td>
                    <td>
                    <asp:TextBox  ID="txtnm_medida" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="t10"
                     ControlToValidate="txtnm_medida" Display="Dynamic" runat="server" 
                     ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>                               
                    </td>
                </tr>
                 <tr>
                    <td><label>Ordem:</label></td>
                    <td><asp:TextBox  ID="txtnu_ordem" EnableViewState="false" Width="60px" MaxLength="4" runat="server"></asp:TextBox>                 
                    </td>
                 </tr>
                    <tr>
                        <td colspan="2"><label style="font-weight:bold">Previsto:</label><br />
                            <asp:Panel ID="PanelP" Width="910" ScrollBars="Horizontal" runat="server">
                            <uc:LancamentoMensal runat="server" prefix="p" ID="ucPrevisto" />
                            </asp:Panel>
                        </td>
                    </tr>                                       
                    <tr runat="server" id="trReal">
                        <td colspan="2"><label style="font-weight:bold">Realizado:</label><br />
                            <asp:Panel ID="PanelR" Width="910" ScrollBars="Horizontal" runat="server">
                            <uc:LancamentoMensal runat="server"  prefix="r" ID="ucRealizado" />
                            </asp:Panel>
                        </td>
                    </tr>                                                       
                    <tr>
                     <td></td>
                     <td>
                        <span class="button"><asp:Button ID="btnSalvar" ValidationGroup="t10" runat="server" Text="Salvar" onclick="btnSalvar_Click" /></span>&nbsp;
                         <span class="button"><asp:Button ID="btnCancelar"  ValidationGroup="t10" runat="server" Text="Cancelar" 
                              CausesValidation="false" onclick="btnCancelar_Click" /></span>
                        
                              </td>
                    </tr>
                    </table>
                       
                    </div>
                </div>
            </asp:Panel>
            
