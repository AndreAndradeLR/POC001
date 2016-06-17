<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Rel_ProjSituacoes" CodeBehind="Rel_ProjSituacoes.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function Validator() {
            var valid = true;
            var msgData = "";
            var msgProject = "";

            if ($("#ctl00_ContentPlaceHolder1_ucSituacoes_ddlMes").val() == "" && $("#ctl00_ContentPlaceHolder1_ucSituacoes_ddlAno").val() != "") {
                msgData += 'Informe o mês!';
                valid = false;
            }
            if ($("#ctl00_ContentPlaceHolder1_ucSituacoes_ddlMes").val() != "" && $("#ctl00_ContentPlaceHolder1_ucSituacoes_ddlAno").val() == "") {
                msgData += 'Informe o ano!';
                valid = false;
            }
            if ($("#ctl00_ContentPlaceHolder1_ucSituacoes_ddlt03_cd_projeto").val() == "") {
                msgProject += 'Informe o Projeto';
                valid = false;
            }
            $("#invalid-date").text(msgData);
            $("#invalid-project").text(msgProject);
            return valid;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="heading_container">
        <div class="heading_right_top">
        </div>
        <h2 id="H7">
            Relatório - Situações de Projetos</h2>
    </div>
    <div class="bucket_container">
        <div class="bucket_top">
            <span></span>
        </div>
        <div class="bucket_content">
            <div class="clear">
            </div>
            <uc:Situacoes runat="server" ID="ucSituacoes" />
            <div class="clear">
            </div>
        </div>
        <div class="bucket_bottom">
            <span></span>
        </div>
    </div>
    <br />
</asp:Content>
