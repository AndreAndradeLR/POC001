<%@ Control Language="C#" AutoEventWireup="true" Inherits="CustomControls_ucParceiros" Codebehind="~/CustomControls/ucParceiros.ascx.cs" %>
 

 
<asp:UpdatePanel ID="updatePanel" runat="server">
<ContentTemplate> 
<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

<span class="button" runat="server" id="spanbtnNovo"><asp:Button ID="btnNovo" CausesValidation="false" runat="server" Text="Cadastrar Novo" onclick="btnNovo_Click" /></span>

 <asp:GridView ID="GridView1" DataKeyNames="t04_cd_parceiro" Width="100%" GridLines="Both"
     OnRowCreated="GridView1_RowCreated" CssClass="tablesorterYellow" OnPreRender="GridView1_PreRender"  ShowFooter="true" 
     OnRowDataBound="GridView1_RowDataBound"  OnRowCommand="GridView1_RowCommand" 
     EmptyDataText="Nenhum registro encontrado" runat="server" CellPadding="4" 
     AutoGenerateColumns="False">
     <RowStyle CssClass="even" />
     <AlternatingRowStyle CssClass="odd" />
     <Columns>
            <asp:ButtonField ButtonType="Image" CommandName="Editar" AccessibleHeaderText="Editar" 
                ImageUrl="~/_assets/img/edit.png"  Text="Editar"  ItemStyle-Width="1%" 
                ItemStyle-HorizontalAlign="Center" />
            <asp:ButtonField ButtonType="Image" CommandName="Deletar" AccessibleHeaderText="Excluir" 
                ImageUrl="~/_assets/img/del.png"  Text="Excluir"  ItemStyle-Width="1%" 
                ItemStyle-HorizontalAlign="Center" />     
         <asp:BoundField DataField="nm_entidade" HeaderText="Entidade" />
         <asp:BoundField DataField="ds_atuacao" HeaderText="Forma de atuação" />
         <asp:BoundField DataField="nm_nome" HeaderText="Interlocutor" />
     </Columns>
  </asp:GridView>  

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
                        <td><label>Entidade</label></td>
                        <td>  
                            <asp:DropDownList ID="ddlt01_cd_entidade" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator  ValidationGroup="t04" ID="RequiredFieldValidator7" runat="server" Display="Dynamic"
                            ErrorMessage="*campo obrigatório" ControlToValidate="ddlt01_cd_entidade"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                        <tr>
                        <td><label>Forma de atuação</label></td>
                        <td>  
                            <asp:TextBox ID="txtds_atuacao" Width="350px" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                        <td><label>Interlocutor</label></td>
                        <td>  
                            <asp:TextBox  ID="txtnm_nome" Width="350px"  MaxLength="200" runat="server"></asp:TextBox>
                        </td>
                        </tr>                                                
                        <tr>
                        <td></td>
                        <td>
                        
                        <span class="button"><asp:Button ID="btnSalvar" ValidationGroup="t04" runat="server" Text="Salvar" onclick="btnSalvar_Click" /></span>&nbsp;
                         <span class="button"><asp:Button ID="btnCancelar"  ValidationGroup="t04" runat="server" Text="Cancelar" 
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