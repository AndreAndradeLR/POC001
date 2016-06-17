<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="MonFinanceiroInd" Codebehind="MonFinanceiroInd.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:NavMon runat="server" ID="navmon" finind="true" />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H1">
    Índice de realização financeira</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>    
    <div style="text-align:right;padding-top:10px;padding-bottom:10px;font-weight:bold;"> 
     <asp:Label ID="lblObs" runat="server" Text="Label"></asp:Label>
    </div> 
    <asp:GridView ID="GridView1" CssClass="tablesorterdisable"  DataKeyNames="t03_cd_projeto" 
    Width="100%" GridLines="Both" 
      runat="server" CellPadding="4" 
     ShowFooter="true"
  AutoGenerateColumns="False">
  <AlternatingRowStyle CssClass="odd" />
  <FooterStyle BackColor="#f4f5f5" Font-Bold="true" HorizontalAlign="Right" />
     <Columns>
        <asp:BoundField DataField="nm_projeto" HtmlEncode="false" 
                HeaderText="Projeto"  />
        <asp:BoundField DataField="vl_fplanejado" HtmlEncode="false" DataFormatString="{0:N0}" 
                HeaderText="Planejado" ItemStyle-HorizontalAlign="Right" />      
        <asp:BoundField DataField="vl_fliquidado" HtmlEncode="false" DataFormatString="{0:N0}"  
                    HeaderText="Liquidado" ItemStyle-HorizontalAlign="Right" />  
        <asp:TemplateField  HeaderText="Liquidado / Planejado (%)" ItemStyle-HorizontalAlign="Right">
        <ItemTemplate>
        
        </ItemTemplate>
        </asp:TemplateField>                                 
     </Columns>
    </asp:GridView>
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

