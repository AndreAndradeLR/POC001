<%@ Page  Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Projetos" Codebehind="Projetos.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" projetos="true" /><br />

  <asp:UpdatePanel ID="updatePanel" UpdateMode="always" runat="server">
  <ContentTemplate>
    <asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
 <div class="bucket_container" style="background:none">
 <span class="button"><asp:Button ID="btnShow" Text="Exibir Filtros" runat="server" /></span>
 </div>
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Projetos Sustentadores</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>     
<div style="margin-bottom:10px;"><asp:Literal ID="litFiltros" runat="server"></asp:Literal></div>

  <ajaxToolKit:ModalPopupExtender ID="mdlPopup" 
   runat="server" TargetControlID="btnShow" 
   PopupControlID="pnlPopup" BackgroundCssClass="modalBackground" />
 
  <asp:Panel ID="pnlPopup" runat="server" CssClass="confirm-dialog" style="display:none;">
       <div class="inner">
        <h2>Filtros de Projetos</h2>
         <div class="base">
         <div class="table" >
         <asp:Label ID="lblMsgPopUp" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>
           <table >
           <tr>
            <td>
            <h5 style="margin:0; padding-bottom:0">Área de Resultado</h5>
                <asp:DropDownList Font-Size="Small" Width="500px" ID="ddlt26_cd_arearesultado" runat="server">
                </asp:DropDownList>
            </td>
           </tr>
           <tr>
            <td>
            <h5 style="margin:0; padding-bottom:0">Entidade Responsável</h5>
                <asp:DropDownList Font-Size="Small" Width="500px" ID="ddlt01_cd_entidade_resp" runat="server">
                </asp:DropDownList>
            </td>
           </tr>
           <tr>
            <td>
            <h5 style="margin:0; padding-bottom:0">Entidade Parceira</h5>
                <asp:DropDownList Font-Size="Small" Width="500px" ID="ddlt01_cd_entidade_parc" runat="server">
                </asp:DropDownList>
            </td>
           </tr>
           <tr>
            <td>
            <h5 style="margin:0; padding-bottom:0">Fase do Projeto</h5>
                <asp:DropDownList Font-Size="Small" Width="500px" ID="ddlt21_cd_fase" runat="server">
                </asp:DropDownList>
            </td>
           </tr> 
           <tr>
            <td> 
            <br />  
           </td>
           </tr>                  
           <tr>
               <td>          
               <span class="button"><asp:Button ID="btnFiltrar" OnClick="btnFiltrar_Click" 
               CausesValidation="false" runat="server" Text="Filtrar" /></span>&nbsp;
               <span class="button"><asp:Button ID="btnCancelar"  runat="server" 
               Text="Cancelar" CausesValidation="false" /></span>
               <span style="margin-left:80px">
                <span class="button"><asp:Button ID="btnTodos" OnClick="btnTodos_Click"  
                runat="server" Text="Todos os Projetos"  CausesValidation="false" /></span>                              
                     </span>         
              </td>
           </tr>
           </table>
           </div>
         </div>
      </div>
</asp:Panel>
                    
  <div style="margin-bottom:3px">
  <span><asp:Literal ID="LiteralGridView1"  runat="server"></asp:Literal></span>
  
  </div>
  <div>
   <asp:GridView ID="GridView1" DataKeyNames="t03_cd_projeto" Width="100%" GridLines="Both"
   OnRowCreated="GridView1_RowCreated" CssClass="tablesorterYellow" OnPreRender="GridView1_PreRender"  ShowFooter="true" 
   OnRowDataBound="GridView1_RowDataBound"  OnRowCommand="GridView1_RowCommand"
   EmptyDataText="Nenhum registro encontrado" runat="server" CellPadding="4" AutoGenerateColumns="False">
     <Columns>
        <asp:ButtonField ButtonType="Link" DataTextField="nm_projeto" 
         CommandName="Arvore" AccessibleHeaderText="Detalhamento do projeto" 
         HeaderText="Projeto" />
         
         <asp:TemplateField HeaderText="Fase" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate></ItemTemplate>
         </asp:TemplateField>
         
         <asp:TemplateField HeaderText="Atualizado" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate></ItemTemplate>
         </asp:TemplateField>
         
         <asp:TemplateField HeaderText="Período" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate></ItemTemplate>
         </asp:TemplateField>                                   
                
         <asp:ButtonField ButtonType="Image" CommandName="Restricao" HeaderText="Restrição"
                ImageUrl="~/images/Restricao.gif" HeaderStyle-Width="60px" 
                ItemStyle-HorizontalAlign="Center"  /> 
         
         <asp:TemplateField HeaderText="Marcos Críticos" HeaderStyle-Width="120px">
         <ItemTemplate></ItemTemplate>
         </asp:TemplateField>                           
     </Columns>
  </asp:GridView> 
  
  
  </div>
  <div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />  
  </ContentTemplate> 
  <Triggers>
    <asp:PostBackTrigger ControlID="btnShow" />
    <asp:PostBackTrigger ControlID="btnFiltrar" />
    <asp:PostBackTrigger ControlID="btnCancelar" />
    <asp:PostBackTrigger ControlID="btnTodos" />
  </Triggers>  
  </asp:UpdatePanel>
<uc:Legenda runat="server" ID="ucLegenda"/><br /><br />
</asp:Content>

