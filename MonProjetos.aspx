<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="MonProjetos" Codebehind="MonProjetos.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:NavMon runat="server" ID="navmon" projetos="true" />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H1">
    Projetos analisados</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>    
   <asp:GridView ID="gvProjetos" DataKeyNames="t03_cd_projeto" Width="100%" GridLines="Both"
    CssClass="tablesorterdisable" 
   EmptyDataText="Nenhum registro encontrado" runat="server" CellPadding="4" AutoGenerateColumns="False">
   <AlternatingRowStyle CssClass="odd" />
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
         
         <asp:TemplateField HeaderText="Restrição" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate></ItemTemplate>
         </asp:TemplateField>                  
         
         <asp:TemplateField HeaderText="Marcos Críticos" HeaderStyle-Width="120px">
         <ItemTemplate></ItemTemplate>
         </asp:TemplateField>                           
     </Columns>
  </asp:GridView> 
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

