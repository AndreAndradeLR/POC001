<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rel_Financeiro" Codebehind="Rel_Financeiro.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="style/print.css" media="print" rel="stylesheet" type="text/css" />
 <style>
 table.tbRestricao{border:1px solid #CCC; }
 table.tbRestricao tr td{border:1px solid #CCC; } 
 table.tbRestricao tr th{border:1px solid #CCC; }  
 </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="DivPanel" class="conteudoPrint" >
<div id="Nav" class="NavPrint">
<uc:Nav ID="ucNav" runat="server" relProduto="true" />
</div> 
<uc:NomeProjeto runat="server" ID="ucProjeto" />
<br />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Relatório Financeiro Acumulado por Ação</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div>

<div style="margin-bottom:10px">
<b>Acumulado até:</b> 
    <asp:DropDownList ID="ddlMes" runat="server">
    </asp:DropDownList> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="filtro" ControlToValidate="ddlMes" runat="server" ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>
    &nbsp;
    <b>Ano</b>: 
    <asp:DropDownList ID="ddlAno" runat="server">
    </asp:DropDownList> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="filtro" ControlToValidate="ddlAno" runat="server" ErrorMessage="*campo obrigatório"></asp:RequiredFieldValidator>
    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" onclick="btnFiltrar_Click" ValidationGroup="filtro" /><br /><br />   
    <div id="divPrint" class="btnPrint">
    <%--<div><asp:Button ID="btnPrint" CssClass="button" Visible="false" OnClick="cmdImprimir_Click" runat="server" Text="Imprimir" /></div>--%>
    <div style="text-align:right"><asp:Button ID="btnPrint" CssClass="button" Visible="false" OnClick="cmdImprimir_Click" runat="server" Text="Imprimir" /><asp:Button ID="btnExportar" Visible="false" runat="server" Text="Exportar para Excel" OnClick="cmdExportar_Click" /></div>
    </div>
</div>  
<%--<div id="div1" class="btnPrint">
<div><asp:Button ID="btnPrint" CssClass="button" OnClick="cmdImprimir_Click" runat="server" Text="Imprimir" /></div>
<div style="text-align:right"><asp:Button ID="btnExportar" Visible="false" runat="server" Text="Exportar para Excel" OnClick="cmdExportar_Click" /></div>
</div--%>
<asp:Panel ID="PanelGrid" Width="100%" runat="server">        
      <asp:Literal ID="LiteralGrid" runat="server"></asp:Literal>
</asp:Panel>

<%--<div id="divPrint" class="btnPrint">
<asp:Button ID="btnPrint" CssClass="button" OnClick="cmdImprimir_Click" runat="server" Text="Imprimir" />
<asp:Button ID="btnExportar" CssClass="button" OnClick="cmdExportar_Click" runat="server" Text="Exportar" />
</div>--%>
<asp:Label ID="lblMsg" Visible="false" runat="server" Text="" EnableViewState="false"></asp:Label>   
    
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</div>
</asp:Content>