<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Detalhamento" Codebehind="Detalhamento.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="wz_tooltip.js" type="text/javascript"></script>
<uc:Nav ID="ucNav" runat="server" detalhamento="true" /> 
<uc:NomeProjeto runat="server" ID="ucProjeto" />
<br />
<div class="heading_container"><div class="heading_right_top"></div>
<h2 id="H7">Detalhamento do Projeto</h2></div>
<div class="bucket_container"><div class="bucket_top"><span></span></div><div class="bucket_content"><div class="clear"></div> 


<h5><a href="#" onmouseover="Tip('Eventos mais significativos que correspondem à ultrapassagem de pontos importantes cuja superação intensifica a dinâmica de implantação do projeto. Sua redação deve ser expressa na forma de eventos realizados e superados.<p> <u>Exemplos – Projeto Expansão das UPAs:</u> projeto de engenharia elaborado; cessão dos terrenos concluída; equipamentos comprados.',
WIDTH, 300, TITLE, 'Marcos Críticos', SHADOW, true, BORDERCOLOR, '#C2B17C', FADEIN, 300, FADEOUT, 300, STICKY, 1, CLOSEBTN, true, CLICKCLOSE, true)"><img src="images/information.gif" title=""  /></a>
Marcos Críticos</h5>         
<uc:MC runat="server" ID="ucMarco" />

<h5><a href="#" onmouseover="Tip('Dificuldades atuais, fora da alçada do gerente do projeto, que são um obstáculo real a execução do projeto, só podendo ser resolvidas pelos outros níveis hierárquicos da Linha Gerencial.<p><u>Exemplos – Projeto Expansão das UPAs:</u><br> Os equipamentos estão retidos na alfândega, aguardando documentação. Providência: acionamento da área jurídica para entrega da documentação requisitada e negociação junto à alfândega em andamento.',
WIDTH, 300, TITLE, 'Restrições', SHADOW, true, BORDERCOLOR, '#C2B17C', FADEIN, 300, FADEOUT, 300, STICKY, 1, CLOSEBTN, true, CLICKCLOSE, true)"><img src="images/information.gif" title=""  /></a>
Restrições</h5>    
<uc:Restricoes runat="server" ID="ucRestricoes" />
    
<h5><a href="#" onmouseover="Tip('Iniciativas específicas que devem ser executadas para em seu conjunto produzir os resultados estabelecidos no projeto.<p><u>Exemplos – Projeto Expansão das UPAs:</u> Implantação das UPAs da região do Barreiro',
WIDTH, 300, TITLE, 'Ações', SHADOW, true, BORDERCOLOR, '#C2B17C', FADEIN, 300, FADEOUT, 300, STICKY, 1, CLOSEBTN, true, CLICKCLOSE, true)"><img src="images/information.gif" title=""  /></a>
Ações</h5>         
<uc:Acoes runat="server" ID="ucAcoes" />

<div class="clear"></div></div><div class="bucket_bottom"><span></span></div></div><br />


</asp:Content>

