<%@ Control Language="C#" AutoEventWireup="true" Inherits="CustomControls_ucRestricoes" Codebehind="ucRestricoes.ascx.cs" %>
<asp:UpdatePanel ID="updatePanel" runat="server">
<ContentTemplate> 

<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

<span class="button" runat="server" id="spanbtnNovo"><asp:Button ID="btnNovo" CausesValidation="false" runat="server" Text="Cadastrar Novo" onclick="btnNovo_Click" /></span>

 <asp:GridView ID="GridView1" DataKeyNames="t07_cd_restricao" Width="100%" GridLines="Both"
     CssClass="tablesorterYellow" OnPreRender="GridView1_PreRender" ShowFooter="false"
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
        <asp:ButtonField ButtonType="Image" CommandName="Selecionar"  AccessibleHeaderText="Selecionar" 
                ImageUrl="~/images/lupa.gif" Text="Exibir detalhes"  ItemStyle-Width="25px" 
                ItemStyle-HorizontalAlign="Center" />             
        <asp:BoundField DataField="ds_restricao"  HtmlEncode="false" HeaderText="Restrição" />
        <asp:TemplateField HeaderText="Marco Crítico">
        <ItemTemplate></ItemTemplate>
        </asp:TemplateField>		
        <asp:TemplateField HeaderText="Providência" HeaderStyle-Width="200px">
        <ItemTemplate></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Responsável pela Providência" HeaderStyle-Width="200px">
        <ItemTemplate></ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="dt_limite" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" NullDisplayText="-" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" 
        HeaderText="Data limite"/>
        <asp:BoundField DataField="dt_superada" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" NullDisplayText="-" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" 
        HeaderText="Data superada"/>
     </Columns>
  </asp:GridView>  
    <div style="text-align:right; padding-top:5px;" runat="server" id="divLinkSup">
    <asp:HyperLink ID="linkSup" Font-Bold="true" NavigateUrl="~/RestricaoSuperada.aspx" runat="server">Restrições Superadas</asp:HyperLink>    
    </div>
            <ajaxToolKit:ModalPopupExtender ID="mdlPopup" 
                runat="server" TargetControlID="btnShow" 
                PopupControlID="pnlPopup" BackgroundCssClass="modalBackground" />
            <asp:Button ID="btnShow" style="display:none;" runat="server" />
            
            <asp:Panel ID="pnlPopup" runat="server" CssClass="confirm-dialog" style="display:none;">                
                <div class="inner">

                    <h2><asp:Label ID="lblHeader" runat="server"></asp:Label></h2>
                    <div class="base">
                     <asp:Label ID="lblMsgPopUp" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
                        <table class="table">
                        <tr>
                           <td><label>Restrição:</label></td>
                                <td><asp:TextBox  ID="txtds_restricao" Width="350px" runat="server"  TextMode="MultiLine"
                                        Height="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                ControlToValidate="txtds_restricao" Display="Dynamic" runat="server" 
                                ErrorMessage="*campo obrigatório" ValidationGroup="t07"></asp:RequiredFieldValidator>       
                               </td>
                            </tr>                                                        
                            <asp:PlaceHolder ID="phProv1" runat="server">
                                <tr>
                                    <td><label>Providência:</label></td>                                                                        
                                    <td>
                                    <asp:TextBox ID="hdDataAtual" Width="100px" style="display:none"  runat="server"></asp:TextBox>
                                    <asp:TextBox  ID="txtds_providencia" Width="350px" runat="server"  TextMode="MultiLine"
                                            Height="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                    ControlToValidate="txtds_providencia" Display="Dynamic" runat="server" 
                                    ErrorMessage="*campo obrigatório" ValidationGroup="t07"></asp:RequiredFieldValidator>       
                                   </td>
                                </tr>                                                                              
                                <tr>
                                    <td><label>Data limite da Providencia:</label></td>
                                    <td>                                                                
                                    <asp:TextBox ID="txtdt_limiteProv" Width="100px" CssClass="datepicker" runat="server"></asp:TextBox>
                                    <%--<asp:ImageButton runat="Server" ID="ImageButton1" ImageUrl="~/images/Calendar.gif" 
                                    AlternateText="Clique para exibir o calendário" />--%>
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator5" ControlToValidate="txtdt_limiteProv"
                                    ErrorMessage="*campo obrigatório" runat="server" Display="Dynamic" ValidationGroup="t07"/>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic"
                                    Type="Date" ControlToValidate="txtdt_limiteProv"  Operator="GreaterThanEqual" ControlToCompare="hdDataAtual" 
                                    ErrorMessage="*data deve ser maior que data atual" ValidationGroup="t07"></asp:CompareValidator>
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
                                  <asp:RequiredFieldValidator id="RequiredFieldValidator4" ControlToValidate="ddlt02_cd_usuario" ValidationGroup="t07" ErrorMessage="*campo obrigatório" runat="server"/>
                                 </td>
                                </tr>
                           </asp:PlaceHolder>
                       <tr style="vertical-align:top">
                       <td style="padding-top:15px"><label>Marco Crítico relacionado a restrição: </label></td>
                       <td style="padding-top:10px">
                        <div style="overflow:auto;height:200px;width:350px;">
                           <asp:CheckBoxList ID="cblt09_cd_marco" runat="server">
                           </asp:CheckBoxList>                                                           
                        </div>   
                       </td>
                       </tr>     
                                  
                         <tr>
                        <td></td>
                        <td>
                        
                        <span class="button"><asp:Button ID="btnSalvar" ValidationGroup="t07" CausesValidation="true" runat="server" Text="Salvar" onclick="btnSalvar_Click" /></span>&nbsp;
                         <span class="button"><asp:Button ID="btnCancelar"  ValidationGroup="t07" runat="server" Text="Cancelar" 
                              CausesValidation="false" onclick="btnCancelar_Click" /></span>
                        
                              </td>
                        </tr>
                        </table>
                       
                    </div>
                </div>
            </asp:Panel>
            
</ContentTemplate>
  <Triggers>
    <asp:PostBackTrigger ControlID="btnNovo" />
    <asp:PostBackTrigger ControlID="btnSalvar" />
    <asp:PostBackTrigger ControlID="btnCancelar" />
  </Triggers>  
</asp:UpdatePanel>

<asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="updatePanel" 
runat="server">
<ProgressTemplate>
    <asp:Image ID="Image1" ImageUrl="~/images/pleasewait.gif" 
    ToolTip="Aguarde" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>    