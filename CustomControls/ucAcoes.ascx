<%@ Control Language="C#" AutoEventWireup="true" Inherits="CustomControls_ucAcoes" Codebehind="ucAcoes.ascx.cs" %>
<asp:UpdatePanel ID="updatePanel" runat="server">
<ContentTemplate> 

<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

<span class="button" runat="server" id="spanbtnNovo"><asp:Button ID="btnNovo" CausesValidation="false" runat="server" Text="Cadastrar Novo" onclick="btnNovo_Click" /></span>

 <asp:GridView ID="GridView1" DataKeyNames="t08_cd_acao" Width="100%" GridLines="Both"
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
           
        <asp:BoundField DataField="nm_acao" HeaderStyle-Width="40%" HtmlEncode="false" HeaderText="Ação" />
        
        <asp:TemplateField HeaderText="Responsável">
        <ItemTemplate>
        </ItemTemplate>
        </asp:TemplateField>
        
        <asp:BoundField DataField="dt_inicio" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" NullDisplayText="-" DataFormatString="{0:dd/MM/yyyy}" 
        HeaderText="Início"/>
        <asp:BoundField DataField="dt_fim" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" NullDisplayText="-" DataFormatString="{0:dd/MM/yyyy}" 
        HeaderText="Fim"/>
              
        <asp:BoundField DataField="dt_alterado" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" NullDisplayText="-" DataFormatString="{0:dd/MM/yyyy}"
        HeaderText="Atualização"/>
        
        <asp:TemplateField HeaderText="Evolução (%)" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="205px">
        <ItemTemplate></ItemTemplate>
        </asp:TemplateField> 
        
     </Columns>
  </asp:GridView>  
    <div style="text-align:right; padding-top:5px;">
    <asp:HyperLink ID="linkGraf" Font-Bold="true" NavigateUrl="~/AcaoCronograma.aspx" runat="server">Cronograma das Ações</asp:HyperLink> | 
    <asp:HyperLink ID="HyperLink2" Font-Bold="true" NavigateUrl="~/Rel_Financeiro.aspx" runat="server">Financeiro Acumulado por Ação</asp:HyperLink> |
    <asp:HyperLink ID="linkFin" Font-Bold="true" NavigateUrl="~/AcaoFinanceiro.aspx" runat="server">Financeiro Acumulado por Fonte/Ano</asp:HyperLink> |
    <asp:HyperLink ID="HyperLink1" Font-Bold="true" NavigateUrl="~/Rel_MetaFisica.aspx" runat="server">Relatório de Metas Físicas</asp:HyperLink>
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
                                <td><label>Ação:</label></td>
                                <td><asp:TextBox  ID="txtnm_acao" Width="350px" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                ControlToValidate="txtnm_acao" Display="Dynamic" runat="server" 
                                ErrorMessage="*campo obrigatório" ValidationGroup="t08"></asp:RequiredFieldValidator>   
                                </td>
                            </tr>  
                        <tr>
                           <td><label>Descrição:</label></td>
                                <td><asp:TextBox  ID="txtds_acao" Width="350px" runat="server"  TextMode="MultiLine"
                                        Height="50px"></asp:TextBox>
                                    
                               </td>
                            </tr>
                            <tr>
                              <td><label>Coordenador:</label></td>
                                <td>
                                <asp:DropDownList ID="ddlt02_cd_usuario" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator id="RequiredFieldValidator1" ControlToValidate="ddlt02_cd_usuario"
                                ErrorMessage="*campo obrigatório" runat="server" Display="Dynamic" ValidationGroup="t08"/>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Início:</label></td>
                                <td><asp:TextBox ID="txtdt_inicio" Width="100px" CssClass="datepicker" runat="server"></asp:TextBox>
                                <%--<asp:ImageButton runat="Server" ID="ImageCal1" ImageUrl="~/images/Calendar.gif" 
                                AlternateText="Clique para exibir o calendário" />--%>
                                <asp:RequiredFieldValidator id="RequiredFieldValidator2" ControlToValidate="txtdt_inicio"
                                ErrorMessage="*campo obrigatório" runat="server" Display="Dynamic" ValidationGroup="t08"/>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic"
                                Type="Date" ControlToValidate="txtdt_inicio"  Operator="DataTypeCheck" 
                                ErrorMessage="*data inválida" ValidationGroup="t08"></asp:CompareValidator>
                                <br />
                                <%--<ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" TargetControlID="txtdt_inicio"
                                    runat="server"  ID="CalendarExtender1" PopupButtonID="ImageCal1"></ajaxToolkit:CalendarExtender>--%>
                                </td>
                            </tr>      
                            <tr>
                                <td><label>Fim:</label></td>
                                <td><asp:TextBox ID="txtdt_fim" Width="100px" CssClass="datepicker" runat="server"></asp:TextBox>
                                <%--<asp:ImageButton runat="Server" ID="ImageCal2" ImageUrl="~/images/Calendar.gif" 
                                AlternateText="Clique para exibir o calendário" />--%>
                                 <asp:RequiredFieldValidator id="RequiredFieldValidator4" ControlToValidate="txtdt_inicio"
                                ErrorMessage="*campo obrigatório" runat="server" Display="Dynamic" ValidationGroup="t08"/>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic"
                                Type="Date" ControlToValidate="txtdt_fim" Operator="DataTypeCheck" 
                                ErrorMessage="*data inválida" ValidationGroup="t08"></asp:CompareValidator>               
                                <br />
                                <%--<ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" TargetControlID="txtdt_fim"
                                    runat="server" ID="CalendarExtender2" PopupButtonID="ImageCal2"></ajaxToolkit:CalendarExtender>--%>
                                 
                                 <asp:CompareValidator ID="ComparaDatas" runat="server" ControlToCompare="txtdt_inicio"
                            ControlToValidate="txtdt_fim" ErrorMessage="Data de início não pode ser superior a data de término."
                            Operator="GreaterThanEqual" Type="Date" ValidationGroup="t08"></asp:CompareValidator>                    
                            </td>
                            </tr>
                                                                
                         <tr>
                        <td></td>
                        <td>
                        
                        <span class="button"><asp:Button ID="btnSalvar" ValidationGroup="t08" runat="server" Text="Salvar" onclick="btnSalvar_Click" /></span>&nbsp;
                         <span class="button"><asp:Button ID="btnCancelar"  ValidationGroup="t08" runat="server" Text="Cancelar" 
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