<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="MonParceiros" Codebehind="MonParceiros.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:NavMon runat="server" ID="navmon" parceiros="true" />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H1">
    Quantidade média de parceiros </h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>    
<asp:GridView ID="gvParceiros" CssClass="tablesorterdisable"  DataKeyNames="t03_cd_projeto" 
Width="100%" GridLines="Both"
  runat="server" CellPadding="4"  ShowFooter="true"
  AutoGenerateColumns="False">
  <AlternatingRowStyle CssClass="odd" />
  <FooterStyle BackColor="#f4f5f5" Font-Bold="true" />
  <Columns>
  <asp:BoundField DataField="nm_projeto" HtmlEncode="false" 
                HeaderText="Projeto"  FooterStyle-HorizontalAlign="Right"/>
  <asp:TemplateField HeaderText="Parcerias" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
  <ItemTemplate>
  
  </ItemTemplate>
  </asp:TemplateField>                  
  </Columns>
</asp:GridView>
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />


</asp:Content>

