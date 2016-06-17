<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="MonFinanceiroGraf" Codebehind="MonFinanceiroGraf.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:NavMon runat="server" ID="navmon" fingraf="true"/>
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H1">
    Acompanhamento Financeiro  </h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>    

    <div style="text-align:center"><asp:Literal ID="Literal1" runat="server"></asp:Literal></div>
    <br />    
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
        <asp:BoundField DataField="vl_planejado" HtmlEncode="false" DataFormatString="{0:N0}" 
                HeaderText="Planejado" ItemStyle-HorizontalAlign="Right" />      
       <asp:BoundField DataField="vl_liquidado" HtmlEncode="false" DataFormatString="{0:N0}"  
                    HeaderText="Liquidado" ItemStyle-HorizontalAlign="Right" />  
         <asp:BoundField DataField="vl_prev" HtmlEncode="false" DataFormatString="{0:N2}" 
                HeaderText="Dotação atual (%)" ItemStyle-HorizontalAlign="Right" Visible="false" />      
        <asp:BoundField DataField="vl_real" HtmlEncode="false" DataFormatString="{0:N2}"  
                    HeaderText="Pago (%)" ItemStyle-HorizontalAlign="Right" Visible="false" />
        <asp:TemplateField  HeaderText="Liquidado/Planejado (%)" ItemStyle-HorizontalAlign="Right">
        <ItemTemplate>
        
        </ItemTemplate>
        </asp:TemplateField>                                 
     </Columns>
    </asp:GridView>
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

