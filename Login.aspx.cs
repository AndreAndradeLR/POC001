using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class frmLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        erro
            Login1.InstructionText = Request["msgerro"].ToString();
        }
       if (Login1.FindControl("UserName")!=null)Login1.FindControl("UserName").Focus();
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        Session.Clear();
        bool logado;
        logado = LogarUsuario(Login1.UserName, Login1.Password);
        e.Authenticated = logado;

        if (e.Authenticated)
        {
            new t09_marcoAction().UpdateCorBarra();
            new t20_acessoAction().InsertDB(new t20_acesso(Login1.UserName, Request.UserHostAddress));

            FormsAuthentication.RedirectFromLoginPage(Login1.UserName, true);
            Response.Redirect("DetectScreen.aspx");

        }
    }

    protected bool LogarUsuario(string usuario, string senha)
    {
        bool boolReturn = false;

        t02_usuario t02 = new t02_usuario();
        t02_usuarioAction t02a = new t02_usuarioAction();

        t02.t02_cd_usuario = usuario;
        t02.pw_senha = senha;

       if (t02a.ValidaSenha(t02) > 0)
        {
            boolReturn = true;
            t02 = t02a.Retrieve(usuario);
            Session["cd_usuario"] = t02.t02_cd_usuario;
            Session["nome"] = t02.nm_nome;
            
            /*
             * Atribuir false para todas as sessões de perfil
             */
            foreach (t24_perfil t24 in new t24_perfilAction().ListObjTodos())
            {
                Session[t24.fl_perfil] = false;
            }

            /*
             * Atribuir true para as sessões de perfil do usuário
             */
            foreach (t24_perfil t24 in t02.t24l)
            {
                Session[t24.fl_perfil] = true;
            }
        }
        return boolReturn;
    }

    protected void Login1_LoggingIn(object sender, System.Web.UI.WebControls.LoginCancelEventArgs e)
    {

    }    

    protected void Login1_LoginError(object sender, EventArgs e)
    {
        //Login1.HelpPageText = "Help with logging in...";
        //Login1.PasswordRecoveryText = "Forgot your password?";
        Login1.FailureText = "usuário ou senha incorreto";
    }

    //protected void btnVisitante_Click1(object sender, EventArgs e)
    //{
    //    Session["cd_usuario"] = "Visitante";
    //    Session["nome"] = "Visitante";
    //    FormsAuthentication.RedirectFromLoginPage(Session["cd_usuario"].ToString(), true);
    //    foreach (t24_perfil t24 in new t24_perfilAction().ListObjTodos())
    //    {
    //        Session[t24.fl_perfil] = false;
    //    }
    //    Response.Redirect("DetectScreen.aspx");

    //}
}
