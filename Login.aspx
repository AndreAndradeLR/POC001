<%@ Page Language="C#" AutoEventWireup="true" Inherits="frmLogin" Codebehind="Login.aspx.cs" %>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Sistema de Gestão Estratégica da Carteira de Projetos Sustentadores do Governo do Estado Maranhão</title>

<style type="text/css">
body {
margin:0;
background-color:#fbfcef;
background-image: url('images/BG_BH2.jpg');
background-repeat:no-repeat;
}
.style2 {		font-family: Helvetica, Verdana, Arial, sans-serif;
	font-size: 12px;
}
.btnLogin{ border: solid 1px #3F3F3F; font-family: Helvetica, Verdana, Arial, sans-serif;
           font-size: 10px; padding:0px; margin-right:5px;}

a
{
    color:#0240A3;
    text-decoration:none;    
}
a:hover
{
   color:#0240A3;
   text-decoration:underline;
}

    .bg_bh
    {
        
        
        padding-top:450px;
        padding-left:50px;
 
    }  
           
           
</style>
<link href="style/fan.css" rel="stylesheet" type="text/css" />
<link href="_assets/round-button/round-button.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">

<div class="bg_bh">

<asp:Login ID="Login1" runat="server"  
        OnAuthenticate="Login1_Authenticate"
        OnLoggingIn="Login1_LoggingIn"
        OnLoginError="Login1_LoginError">
            <LayoutTemplate>
            <div class="style2">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">
                                       Usuário:</asp:Label>

                                        <br />

                                        <asp:TextBox ID="UserName" Width="100px" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                            ControlToValidate="UserName" ErrorMessage="*campo obrigatório" 
                                            ToolTip="*campo obrigatório" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                 <br />
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Senha:</asp:Label>
                                    
                                        <br />
                                    
                                        <asp:TextBox ID="Password" Width="100px"  runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                            ControlToValidate="Password" ErrorMessage="*campo obrigatório" 
                                            ToolTip="*campo obrigatório" ValidationGroup="Login1">*</asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;                                                                                
                                            <br />
                                  </div>
                                   <span class="button">
                                   <asp:Button ID="LoginButton" runat="server" 
                                   CommandName="Login" Text="Entrar Na versão atualizada!!! 28-06-2016 UBUNTU" ValidationGroup="Login1" />
                                   </span>
                                            
                                        <asp:CheckBox ID="RememberMe" Visible="false" runat="server" Text="Remember me next time." />
                                  
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                  
                                   
                                        
            
            </LayoutTemplate>
        </asp:Login>

    <div style="margin-top:50px; font-family:Verdana; color:Gray;font-size:x-small">
        Versão: 1.225
    </div>   
</div>



</form>

</body>
</html>
