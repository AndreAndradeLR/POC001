<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Resultados" Codebehind="Resultados.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc:Nav ID="ucNav" runat="server" resultado="true"/> 
<uc:NomeProjeto runat="server" ID="ucProjeto" />
<br />

<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Resultados</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 

<asp:UpdatePanel ID="updatePanel" runat="server">
<ContentTemplate> 

<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

<span class="button" runat="server" id="spanbtnNovo"><asp:Button ID="btnNovo" CausesValidation="false" runat="server" Text="Cadastrar Novo" onclick="btnNovo_Click" /></span>

 <asp:GridView ID="GridView1" DataKeyNames="t12_cd_resultado" Width="100%" GridLines="Both"
    CssClass="tablesorterdisable"  
     OnRowDataBound="GridView1_RowDataBound"  OnRowCommand="GridView1_RowCommand" 
     EmptyDataText="Nenhum registro encontrado" runat="server" CellPadding="4" 
     AutoGenerateColumns="False">
     <AlternatingRowStyle BackColor="#f8f7f7" />
     <Columns>
     <asp:ButtonField ButtonType="Image" CommandName="Editar" AccessibleHeaderText="Editar" 
        ImageUrl="~/_assets/img/edit.png"  Text="Editar"  ItemStyle-Width="1%" 
        ItemStyle-HorizontalAlign="Center" />
     <asp:ButtonField ButtonType="Image" CommandName="Deletar" AccessibleHeaderText="Excluir" 
        ImageUrl="~/_assets/img/del.png"  Text="Excluir"  ItemStyle-Width="1%" 
        ItemStyle-HorizontalAlign="Center" />     
     <asp:TemplateField HeaderText="Resultados" >
     <ItemTemplate>
     </ItemTemplate>
     </asp:TemplateField>
     
     <asp:TemplateField HeaderText="Gráfico/Detalhes" HeaderStyle-Width="40%">
     <ItemTemplate>
     </ItemTemplate>
     </asp:TemplateField>     
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
            <td><label>Descrição:</label></td>
            <td colspan="3"><asp:TextBox  ID="txtds_resultado" Width="350px" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator  ValidationGroup="t12" id="RequiredFieldValidator2" ControlToValidate="txtds_resultado" Display="Dynamic" ErrorMessage="*campo obrigatório" runat="server"/></td>            
        </tr>
        <tr>
            <td><label>Indicador:</label></td>
            <td colspan="3">
                <asp:TextBox  ID="txtnm_resultado" Width="350px" MaxLength="300" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator  ValidationGroup="t12" id="RequiredFieldValidator1" ControlToValidate="txtnm_resultado" Display="Dynamic" ErrorMessage="*campo obrigatório" runat="server"/>
                </td>
        </tr>
        <tr>
            <td><label>Unidade de Medida:</label></td>
            <td><asp:TextBox  ID="txtnm_medida" MaxLength="100" Width="80px" runat="server"></asp:TextBox><p>
            <asp:RequiredFieldValidator ValidationGroup="t12" id="RequiredFieldValidator3" ControlToValidate="txtnm_medida" Display="Dynamic" ErrorMessage="*campo obrigatório" runat="server"/></p>
             </td>
             <td><label>&nbsp;Responsável pela Medição:</label></td>
             <td width="180px"><asp:TextBox  ID="txtnm_respmedicao" MaxLength="100" Width="120px" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><label>Valor de referência:</label></td>
            <td>
             <asp:TextBox ID="txtvl_t0" MaxLength="18" Width="60px" runat="server"></asp:TextBox> 
             <asp:CompareValidator  ValidationGroup="t12" ID="CompareValidator2" runat="server" Display="Dynamic" Type="Currency"
                ControlToValidate="txtvl_t0" Operator="DataTypeCheck" ErrorMessage="*formato inválido"></asp:CompareValidator></td>
             <td><label>&nbsp;Fonte:</label></td>
             <td width="180px"><asp:TextBox  ID="txtnm_fonte" MaxLength="100" Width="120px" runat="server"></asp:TextBox></td>                   
        </tr>
        
        <tr>
            <td><label>Ano:</label></td>
            <td colspan="3"><asp:TextBox  ID="txtnu_ano" Width="60px" MaxLength="4" runat="server"></asp:TextBox>
                <asp:CompareValidator  ValidationGroup="t12" ID="CompareValidator1" Operator="DataTypeCheck" runat="server" Display="Dynamic" Type="Integer" 
                ControlToValidate="txtnu_ano" ErrorMessage="*formato inválido"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="t12" runat="server" ControlToValidate="txtnu_ano" ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><label>Ordem:</label></td>
             <td colspan="3"><asp:TextBox  ID="txtnu_ordem" EnableViewState="false" Width="60px" MaxLength="4" runat="server"></asp:TextBox>                 
            </td>
        </tr>
        <tr id="Tr1" runat="server">
            <td><label>Valores anuais:</label></td>
            <td colspan="3">
             <div style="overflow:auto;height:150px;padding-left:20px;">
               <uc:Anos ID="ucAnos" runat="server" />
             </div> 
            </td>   
        </tr> 
        <tr>
            <td><label>Os valores da tabela:</label></td>
            <td colspan="3"><asp:RadioButtonList ID="rblfl_acumulado"  RepeatLayout="Flow" 
                    RepeatDirection="Horizontal" runat="server">
         <asp:ListItem Text="Estão Acumulados" Value="True"></asp:ListItem>
         <asp:ListItem Text="Não estão acumulados" Value="False"></asp:ListItem>
         </asp:RadioButtonList>
         <asp:RequiredFieldValidator  ValidationGroup="t12" id="RequiredFieldValidator4" ControlToValidate="rblfl_acumulado" Display="Dynamic" ErrorMessage="*campo obrigatório" runat="server"/>
         </td>
        </tr>                                                             
                         <tr>
                        <td></td>
                        <td colspan="3">
                        
                        <span class="button"><asp:Button ID="btnSalvar" ValidationGroup="t12" runat="server" Text="Salvar" onclick="btnSalvar_Click" /></span>&nbsp;
                         <span class="button"><asp:Button ID="btnCancelar"  ValidationGroup="t12" runat="server" Text="Cancelar" 
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

<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

