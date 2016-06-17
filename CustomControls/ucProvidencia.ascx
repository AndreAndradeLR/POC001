<%@ Control Language="C#" AutoEventWireup="true" Inherits="CustomControls_ucProvidencia" Codebehind="ucProvidencia.ascx.cs" %>
<asp:UpdatePanel ID="updatePanel" runat="server">
<ContentTemplate> 

<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

<span class="button" runat="server" id="spanbtnNovo"><asp:Button ID="btnNovo" CausesValidation="false" runat="server" Text="Nova Providência" onclick="btnNovo_Click" /></span>

 <asp:GridView ID="GridView1" DataKeyNames="t23_cd_providencia" Width="100%" GridLines="Both"
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
         <asp:BoundField DataField="ds_providencia" ItemStyle-Wrap="true" HeaderText="Providência" />
         <asp:TemplateField HeaderText="Responsável pela providência">
         <ItemTemplate></ItemTemplate>
         </asp:TemplateField>
         <asp:BoundField DataField="dt_limite" ItemStyle-Wrap="true" HeaderText="Data Limite" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" />
         <asp:BoundField DataField="dt_cadastro" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" HeaderText="Data de Inclusão" />
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
                            <td style="vertical-align:top"><label>Providência:</label></td>
                            <td><asp:TextBox  ID="txtds_providencia" Width="400px" runat="server"  TextMode="MultiLine"
                            Height="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="t23" ControlToValidate="txtds_providencia" runat="server" ErrorMessage="*Campo obrigatório"></asp:RequiredFieldValidator>
                            </td>
                            
                        </tr>
                                              
                         <tr>
                        <tr>
                            <td><label>Data limite da Providencia:</label></td>
                            <td>          
                            <asp:TextBox ID="hdDataAtual" Width="100px" style="display:none"  runat="server"></asp:TextBox>                                                      
                            <asp:TextBox ID="txtdt_limiteProv" Width="100px" CssClass="datepicker" runat="server"></asp:TextBox>
                            <%--<asp:ImageButton runat="Server" ID="ImageButton1" ImageUrl="~/images/Calendar.gif" 
                            AlternateText="Clique para exibir o calendário" />--%>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator5" ControlToValidate="txtdt_limiteProv"
                            ErrorMessage="*campo obrigatório" runat="server" Display="Dynamic" ValidationGroup="t23"/>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic"
                            Type="Date" ControlToValidate="txtdt_limiteProv"  Operator="GreaterThanEqual" ControlToCompare="hdDataAtual" 
                            ErrorMessage="*data deve ser maior que data atual" ValidationGroup="t23"></asp:CompareValidator>
                            <br />
                            <%--<ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" TargetControlID="txtdt_limiteProv"
                            runat="server"  ID="CalendarExtender2" PopupButtonID="ImageButton1"></ajaxToolkit:CalendarExtender>--%>
                            </td>
                        </tr>

                        <tr>
                            <td><label>Responsável pela providência:</label></td>
                            <td>
                            <asp:DropDownList ID="ddlt02_cd_usuario" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator4" ControlToValidate="ddlt02_cd_usuario" ValidationGroup="t23" ErrorMessage="*campo obrigatório" runat="server"/>
                            </td>
                        </tr>
                        <td></td>
                        <td>
                        
                        <span class="button"><asp:Button ID="btnSalvar" ValidationGroup="t23" runat="server" Text="Salvar" onclick="btnSalvar_Click" /></span>&nbsp;
                         <span class="button"><asp:Button ID="btnCancelar"  ValidationGroup="t23" runat="server" Text="Cancelar" 
                              CausesValidation="false" onclick="btnCancelar_Click" /></span>
                        
                              </td>
                        </tr>
                        </table>
                       
                    </div>
                </div>
            </asp:Panel>
            
</ContentTemplate>
<Triggers>
 <asp:PostBackTrigger ControlID="btnSalvar" />
</Triggers>
</asp:UpdatePanel>

<asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="updatePanel" 
runat="server">
<ProgressTemplate>
    <asp:Image ID="Image1" ImageUrl="~/images/pleasewait.gif" 
    ToolTip="Aguarde" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>  