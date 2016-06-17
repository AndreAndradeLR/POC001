<%@ Control Language="C#" AutoEventWireup="true" Inherits="CustomControls_ucDocs" Codebehind="~/CustomControls/ucDocs.ascx.cs" %>
<asp:UpdatePanel ID="updatePanel" runat="server">
<ContentTemplate> 

<h3>
    <asp:Image ID="imgTipo" runat="server" /> 
    <asp:Label ID="lblTipo" runat="server"></asp:Label>
  
</h3>

<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>

<span class="button" runat="server" id="spanbtnNovo"><asp:Button ID="btnNovo" CausesValidation="false" runat="server" Text="Cadastrar Novo" onclick="btnNovo_Click" /></span>
 <asp:GridView ID="GridView1" DataKeyNames="t14_cd_documento" Width="100%" GridLines="Both"
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
        <asp:HyperLinkField DataNavigateUrlFields="nm_arquivo" DataTextField="nm_documento"  Target="_blank"
            DataNavigateUrlFormatString="~/Documentos/{0}" HeaderText="Arquivo" />
        <asp:BoundField DataField="dt_cadastro" DataFormatString="{0:d}" HeaderText="Data Publicação" HtmlEncode="false" />     
      </Columns>
  </asp:GridView>  
  
  
  <asp:DataList ID="dlFotos" OnItemCommand="dlFotos_ItemCommand" DataKeyField="t14_cd_documento" 
     runat="server" HorizontalAlign="Center" Width="100%" GridLines="Both"
    RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal">
    <ItemTemplate>
     <div class="box_cinza">
            <a href='Documentos/<%#Eval("nm_arquivo")%>' target="_blank">
            <img src='Thumbnail.aspx?NomeImagem=<%#Eval("nm_arquivo")%>&Largura=180' alt='Clique para ver no tamanho original' style="border:none" />
            </a></div>
            <asp:ImageButton ID="ImageButton1" CommandName="Editar" OnLoad="perfil_Load" CausesValidation="false" 
            ImageUrl="~/_assets/img/edit.png" ToolTip="Editar" runat="server" />&nbsp; 
             <asp:ImageButton ID="ImageButton2" CommandName="Excluir" OnLoad="perfil_Load" CausesValidation="false" 
             ImageUrl="~/_assets/img/del.png" ToolTip="Excluir" runat="server"  
             OnClientClick="javascript:return confirm('Tem certeza que deseja excluir?')"/>

            <b><%#Eval("nm_documento")%></b>
            <div style="font-size:11px">(Publicado em <%#Eval("dt_cadastro", "{0:d}")%>)</div>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Center" />
    </asp:DataList>
    
    
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
                        <td><label>Título</label></td>
                        <td>  
                            <asp:TextBox ID="txtnm_documento" runat="server" 
                            Width="325px" MaxLength="300"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  ValidationGroup="t14"
                            runat="server" ErrorMessage="*campo obrigatório" Display="Dynamic" ControlToValidate="txtnm_documento"></asp:RequiredFieldValidator>                        </td>
                        </tr>                        
                        <tr runat="server" id="trArquivo">
                        <td><label>Arquivo</label></td>
                        <td>  
                          <asp:FileUpload ID="funm_arquivo" runat="server" />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  ValidationGroup="t14"
                            runat="server" ErrorMessage="*campo obrigatório" Display="Dynamic" 
                            ControlToValidate="funm_arquivo"></asp:RequiredFieldValidator>
                            <small>*tamanho máximo de arquivo para upload é de 20 MB.</small>
                        </td>
                        </tr>
                        <tr runat="server" visible="false" id="trOpcao">
                        <td><label>Novo Arquivo?</label></td>
                        <td>  
                        <asp:RadioButtonList ID="rblArquivo" AutoPostBack="true" runat="server" 
                             onselectedindexchanged="rblArquivo_SelectedIndexChanged" 
                             RepeatDirection="Horizontal" RepeatLayout="Flow">
                         <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                         <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                         </asp:RadioButtonList>
                        </td>
                        </tr>
                        <tr>
                        <td></td>
                        <td>
                        
                        <span class="button"><asp:Button ID="btnSalvar" ValidationGroup="t14" runat="server" Text="Salvar" onclick="btnSalvar_Click" /></span>&nbsp;
                         <span class="button"><asp:Button ID="btnCancelar"  ValidationGroup="t14" runat="server" Text="Cancelar" 
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