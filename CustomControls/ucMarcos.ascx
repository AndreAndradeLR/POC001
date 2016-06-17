<%@ Control Language="C#" AutoEventWireup="true" Inherits="CustomControls_ucMarcos" Codebehind="ucMarcos.ascx.cs" %>
<asp:UpdatePanel ID="updatePanel" runat="server">
<ContentTemplate> 

<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

<span class="button" runat="server" id="spanbtnNovo"><asp:Button ID="btnNovo" CausesValidation="false" runat="server" Text="Cadastrar Novo" onclick="btnNovo_Click" /></span>

 <asp:GridView ID="GridView1" DataKeyNames="t09_cd_marco" Width="100%" GridLines="Both"
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
        
        <asp:ImageField DataImageUrlField="fl_status" HeaderStyle-Width="56px" ItemStyle-HorizontalAlign="Center" DataImageUrlFormatString="~/images/{0}.gif" HeaderText="Status"></asp:ImageField>
        <asp:BoundField DataField="ds_marco"  HtmlEncode="false" HeaderText="Marco Crítico" />
        <asp:BoundField DataField="nu_esforco" HeaderStyle-Width="80px" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" HeaderText="% esforço" SortExpression="nu_esforco" />
        <asp:TemplateField HeaderText="Data Prevista" ControlStyle-CssClass="header headerSortDown" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
        <ItemTemplate><%#String.Format("{0:dd/MM/yyyy}", Eval("dt_prevista"))%> 
            <asp:Image ID="imgOriginal" ImageUrl="~/images/ico_versao.gif" 
            runat="server" Visible="false" />
        </ItemTemplate>
        </asp:TemplateField>      
        <asp:BoundField DataField="dt_realizada" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Center" NullDisplayText="-" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" HeaderText="Data Realizada" SortExpression="dt_realizada" />
        <asp:BoundField DataField="ds_comentario" HeaderStyle-Width="90px" HtmlEncode="false" HeaderText="Comentários" />
     </Columns>
  </asp:GridView>  
    <asp:Label ID="lblMsgEsforco" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label>
    
            <ajaxToolKit:ModalPopupExtender ID="mdlPopup" 
                runat="server" TargetControlID="btnShow" 
                PopupControlID="pnlPopup" BackgroundCssClass="modalBackground" />
            <asp:Button ID="btnShow" style="display:none;" runat="server" />
            
            <asp:Panel ID="pnlPopup" runat="server" CssClass="confirm-dialog" style="display:none;">
                <div class="inner">

                    <h2><asp:Label ID="lblHeader" runat="server"></asp:Label></h2>                   
                    <asp:Label ID="lblDatas" runat="server" Text="Datas do Projeto:"></asp:Label>
                    <br />
                    <div class="base">
                        <table class="table">
                     <tr>
                           <td><label>Marco Crítico:</label></td>
                                <td><asp:TextBox  ID="txtds_marco" Width="350px" runat="server"  TextMode="MultiLine"
                                        Height="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="t09"
                                    ControlToValidate="txtds_marco" Display="Dynamic" runat="server" 
                                    ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>   
                               </td>
                            </tr>
                                    <tr>
                                <td><label>Esforço:</label></td>
                                <td><asp:TextBox  ID="txtnu_esforco" Width="50px" MaxLength="3" runat="server"></asp:TextBox>%
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator6" 
                                        ControlToValidate="txtnu_esforco" ErrorMessage="*campo obrigatório" runat="server" 
                                        Display="Dynamic" ValidationGroup="t09"/>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*formato inválido" ControlToValidate="txtnu_esforco"
                                     Display="Dynamic" Operator="DataTypeCheck" Type="Integer" ValidationGroup="t09"></asp:CompareValidator>    
                                        </td>
                            </tr>
                            <tr>                                                                                                                                                                
                                <asp:TextBox ID="dt_fimProjeto" style="display:none" runat="server" ></asp:TextBox>
                                <td><label>Data Prevista:</label></td>
                                <td><asp:TextBox ID="txtdt_prevista" Width="100px" CssClass="datepicker" runat="server"></asp:TextBox>
                                <%--<asp:ImageButton runat="Server" ID="ImageCal1" ImageUrl="~/images/Calendar.gif" 
                                AlternateText="Clique para exibir o calendário" />--%>
                                
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtdt_prevista" 
                                ErrorMessage="*data deve ser menor que o término do projeto." ControlToCompare="dt_fimProjeto" 
                                Operator="LessThanEqual" Type="Date" ValidationGroup="t09">
                                </asp:CompareValidator>
                                
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="t09"
                                    ControlToValidate="txtdt_prevista" Display="Dynamic" runat="server" 
                                    ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>   
                                    
                                <asp:CompareValidator ID="CompareValidator3" runat="server" Display="Dynamic"
                                Type="Date" ControlToValidate="txtdt_prevista" Operator="DataTypeCheck" 
                                ErrorMessage="*data inválida" ValidationGroup="t09"></asp:CompareValidator>    
                                <br />
                                <%--<ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" TargetControlID="txtdt_prevista"
                                    runat="server" ID="CalendarExtender1" PopupButtonID="ImageCal1"></ajaxToolkit:CalendarExtender>--%>
                                </td>
                            </tr>      
                            <tr id="trReal" visible="false" runat="server">
                                <td><label>Data Realizada:</label></td>                                
                                <td><asp:TextBox ID="dtHoje" style="display:none" runat="server"></asp:TextBox><asp:TextBox ID="txtdt_realizada" Width="100px" CssClass="datepicker" runat="server"></asp:TextBox>
                                <%--<asp:ImageButton runat="Server" ID="ImageCal2" ImageUrl="~/images/Calendar.gif" 
                                AlternateText="Clique para exibir o calendário" />--%>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" Display="Dynamic"
                                Type="Date" ControlToValidate="txtdt_realizada" ControlToCompare="dtHoje" Operator="LessThanEqual" 
                                ErrorMessage="*data não pode ser maior que data atual." ValidationGroup="t09"></asp:CompareValidator>   
                                
                                <asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic"
                                Type="Date" ControlToValidate="txtdt_realizada" Operator="DataTypeCheck" 
                                ErrorMessage="*data inválida" ValidationGroup="t09"></asp:CompareValidator>    
                                <br />
                                <%--<ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" TargetControlID="txtdt_realizada"
                                    runat="server" ID="CalendarExtender2" PopupButtonID="ImageCal2"></ajaxToolkit:CalendarExtender>--%>
                            </td>
                            </tr>
                            <tr>
                                <td><label>Comentários:</label></td>
                                <td><asp:TextBox  ID="txtds_comentario" Width="350px" runat="server"  TextMode="MultiLine"
                                        Height="50px"></asp:TextBox></td>
                            </tr>                                      
                         <tr>
                        <td></td>
                        <td>
                        
                        <span class="button"><asp:Button ID="btnSalvar" ValidationGroup="t09" runat="server" Text="Salvar" onclick="btnSalvar_Click" /></span>&nbsp;
                         <span class="button"><asp:Button ID="btnCancelar"  ValidationGroup="t09" runat="server" Text="Cancelar" 
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