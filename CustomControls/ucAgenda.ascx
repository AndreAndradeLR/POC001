<%@ Control Language="C#" AutoEventWireup="true" Inherits="CustomControls_ucAgenda" Codebehind="~/CustomControls/ucAgenda.ascx.cs" %>
<asp:UpdatePanel ID="updatePanel" runat="server">
<ContentTemplate> 

<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

<span class="button" runat="server" id="spanbtnNovo"><asp:Button ID="btnNovo" CausesValidation="false" runat="server" Text="Cadastrar Novo" onclick="btnNovo_Click" /></span>

 <asp:GridView ID="GridView1" DataKeyNames="t16_cd_agenda" Width="100%" GridLines="Both"
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
        
         <asp:ButtonField ButtonType="Image" Visible="false" CommandName="Projeto" ImageUrl="~/images/lupa.gif" Text="Exibir Projeto"  ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" />
         <asp:BoundField DataField="ds_agenda" ItemStyle-Wrap="true" HeaderText="Título" />
         <asp:BoundField DataField="dt_data" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" HeaderText="Data" SortExpression="dt_data" />
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
                    <td style="vertical-align:top"><label>Título:</label></td>
                    <td><asp:TextBox  ID="txtds_agenda" Width="400px" runat="server"  TextMode="MultiLine"
                            Height="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><label>Data:</label></td>
                    <td><asp:TextBox ID="txtdt_data" Width="100px" CssClass="datepicker" runat="server"></asp:TextBox>
                    <%--<asp:ImageButton runat="Server" ID="ImageCal1" ImageUrl="~/images/Calendar.gif" 
                    AlternateText="Clique para exibir o calendário" /><br />
                    <ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" TargetControlID="txtdt_data"
                    runat="server" ID="calext1" PopupButtonID="ImageCal1"></ajaxToolkit:CalendarExtender>--%>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator1" 
                            ControlToValidate="txtdt_data" ErrorMessage="*campo obrigatório" 
                            runat="server" Display="Dynamic"/>
                        </td>
                    </tr>                                               
                         <tr>
                        <td></td>
                        <td>
                        
                        <span class="button"><asp:Button ID="btnSalvar" ValidationGroup="t16" runat="server" Text="Salvar" onclick="btnSalvar_Click" /></span>&nbsp;
                         <span class="button"><asp:Button ID="btnCancelar"  ValidationGroup="t16" runat="server" Text="Cancelar" 
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