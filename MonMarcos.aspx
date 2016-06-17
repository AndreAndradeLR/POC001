<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="MonMarcos" Codebehind="MonMarcos.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:NavMon runat="server" ID="navmon" marcos="true" />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H1">
    <asp:Label ID="lblHeader" runat="server" Text="Label"></asp:Label></h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>    
    <asp:Repeater ID="rptMarcos"  runat="server">
    <ItemTemplate>
    <asp:Label ID="lblnm_projeto" runat="server"></asp:Label>
    <asp:GridView ID="gvMarcos" CssClass="tablesorterdisable"  DataKeyNames="t09_cd_marco" Width="100%" GridLines="Both"
      runat="server" CellPadding="4" 
     AutoGenerateColumns="False">
     <AlternatingRowStyle CssClass="odd" />
     <Columns>
        <asp:ImageField DataImageUrlField="fl_status" HeaderStyle-Width="56px" ItemStyle-HorizontalAlign="Center" DataImageUrlFormatString="~/images/{0}.gif" HeaderText="Status"></asp:ImageField>
        <asp:BoundField DataField="ds_marco"  HtmlEncode="false" HeaderText="Marco Crítico" />
        <asp:TemplateField HeaderText="Data Prevista" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
        <ItemTemplate><%#String.Format("{0:dd/MM/yyyy}", Eval("dt_prevista"))%> 
            <asp:Image ID="imgOriginal" ImageUrl="~/images/ico_versao.gif" 
            runat="server" Visible="false" />
        </ItemTemplate>
        </asp:TemplateField>      
        <asp:BoundField DataField="dt_realizada" Visible="false" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Center" NullDisplayText="-" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" HeaderText="Data Realizada" SortExpression="dt_realizada" />
     </Columns>
  </asp:GridView> 
    </ItemTemplate>
    </asp:Repeater>
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

