using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {        
        ddlMenu.PreRender += new EventHandler(ddlMenu_PreRender);
        // disable page caching
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");

        if (Session["nome"] == null)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage("msgerro=Sessão expirou!");
        }
        else
        {
           lblnm_usuario.Text = Session["nome"].ToString();
           if (!IsPostBack)
           {
               ddlMenuBind();
               ShowAlteraSenha();
           }
        }

    }

    protected void ShowAlteraSenha() {
        if (Session["cd_usuario"].ToString() != "Visitante")
        {
            topoUsuario.Visible = true;
        }
        else {
            topoUsuario.Visible = false;
        }
    }

    //
    //** Metodo verifica se é gerente de algum projeto
    //
    public bool GerenteProjeto()
    {
        bool pass = false;
        if (Session["cd_usuario"] != null)
        {            
            int gerente = 0;
            t02_usuarioAction t02a = new t02_usuarioAction();
            gerente = t02a.ListUsuarioGerente(Session["cd_usuario"].ToString()).Rows.Count;
            if (gerente > 0)
            {
                //é gerente do projeto
                pass = true;
            }
            else
            {
                pass = false;
            }            
        }
        return pass;
    }

    //
    //** Metodo verifica se pertence a linha decisoria ou se é coordenador de ação
    //
    public bool ShowRelatorioSituacoes()
    {
        int linhadecisoria = 0;
        int acao = 0;
        bool pass = false;
        if (Session["cd_usuario"] != null)
        {
            //consulta da linha decisória e ação
            t02_usuarioAction t02a = new t02_usuarioAction();
            linhadecisoria = t02a.ListLinhaGerencialUser(Session["cd_usuario"].ToString()).Rows.Count;
            acao = t02a.ListCoordenaAcao(Session["cd_usuario"].ToString()).Rows.Count;
            if ((linhadecisoria > 0) || (acao > 0))
            {
                pass = true;
            }
        }
        return pass;
    }

    protected void btnSair_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage("msgerro=Sessão finalizada!");
    }
 
    private void ddlMenuBind()
    {
        ddlMenu.Items.Add(new ListItem("Selecione", ""));
        ddlMenu.Items.Add(new ListItem("Início", "Projetos.aspx"));
        
        if (Session["cd_usuario"].ToString() != "Visitante")
        {
            if ((ShowRelatorioSituacoes()) || (GerenteProjeto()) || (Convert.ToBoolean(pb.Session("fl_monitora"))) || (Convert.ToBoolean(pb.Session("fl_admin"))))
            {
                ddlMenu.Items.Add(new ListItem("Relatório de Situações", "Rel_ProjSituacoes.aspx"));
            }
            if (Convert.ToBoolean(pb.Session("fl_admin")))
            {
                ddlMenu.Items.Add(new ListItem("Área de Resultado", "frmAreaResultado.aspx"));
                ddlMenu.Items.Add(new ListItem("Entidades", "frmEntidade.aspx"));
                ddlMenu.Items.Add(new ListItem("Projetos", "frmProjeto.aspx"));
                ddlMenu.Items.Add(new ListItem("Usuários", "frmUsuario.aspx"));
                ddlMenu.Items.Add(new ListItem("Fontes", "frmFonte.aspx"));

            }
            if ((ShowRelatorioSituacoes()) || (GerenteProjeto()) || (Convert.ToBoolean(pb.Session("fl_monitora"))) || (Convert.ToBoolean(pb.Session("fl_admin"))))
            {
                ListItem li = ddlMenu.Items.FindByValue("MonPainel.aspx");
                if (li == null)
                {
                    ddlMenu.Items.Add(new ListItem("Resumo Executivo", "MonPainel.aspx"));
                }
            }
            if (Convert.ToBoolean(pb.Session("fl_admin")))
            {
                ddlMenu.Items.Add(new ListItem("Mapa Financeiro", "RelatorioExcel.aspx"));                
            }
            if ((Convert.ToBoolean(pb.Session("fl_admin"))) || (Convert.ToBoolean(pb.Session("fl_monitora"))))
            {
                ddlMenu.Items.Add(new ListItem("Relatório Restrições", "Rel_Restricao.aspx"));
                ddlMenu.Items.Add(new ListItem("Relatório Marco Crítico", "Rel_Marco.aspx"));
                ddlMenu.Items.Add(new ListItem("Relatório Ações", "Rel_Acoes.aspx"));                
            }
            //ddlMenu.Items.Add(new ListItem("Teste", "Fabboci.aspx"));

            if (Convert.ToBoolean(pb.Session("fl_admin")) || (GerenteProjeto()))
            {
                ddlMenu.Items.Add(new ListItem("Relatório Ações Vinculadas", "Rel_AcoesVinculadas.aspx"));
            }
        }        
    }

    void ddlMenu_PreRender(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        ddl.ClearSelection();
        pb.AddColorLines(ddl, "#f7f9ee");
        ListItem li = ddl.Items.FindByValue(pb.GetCurrentPageName());
        if (li != null)
            li.Selected = true;
    }    

    protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        if (ddl.SelectedValue != "")
        {
            if (ddl.SelectedValue == "MonPainel.aspx")
            {
                Session["filtroMon"] = null;
            }
            Response.Redirect("~/" + ddl.SelectedValue);
        }
        else
        {
            ddl.ClearSelection();
        }
    }
}
