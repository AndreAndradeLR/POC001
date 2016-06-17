<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Documentos" Codebehind="Documentos.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:Nav ID="ucNav" runat="server" documentos="true"/> 
<uc:NomeProjeto runat="server" ID="ucProjeto" />
<br />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7"><asp:Label ID="lblTitle" runat="server" Text="Documentos"></asp:Label></h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 
<uc:Doc runat="server" ID="ucDoc" />
<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />
</asp:Content>

