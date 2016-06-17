using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AcaoDetalhes : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {        
        FormBind();
        ShowFinanceiro();
        if (!IsPostBack)
        {            
            Retrieve();
        }
    }

    private void ShowFinanceiro() {
        //chama o metodo q mostra financeiro
        if((Session["cd_usuario"] != null)&&(Session["cd_area"]!=null))
        {                      
            bool result = false;
            result = pb.RestricaoFinanceiro(Session["cd_usuario"].ToString(), Convert.ToInt32(Session["cd_area"]));            
            if (result)
            {
                //exibe financeiro
                pnlExibeFinanceiro.Visible = true;
            }
            else
            {
                pnlExibeFinanceiro.Visible = false;
            }
        }
    }

    private void FormBind()
    {
        foreach (t08_acao t08 in new t08_acaoAction().ListObjTodos(Convert.ToInt32(pb.Session("cd_projeto"))))
        {
            PanelAcoes.Controls.Add(pb.GetLiteral("<div style=\"margin:8px 0 8px 10px\">"));
            LinkButton lbtnAcao = new LinkButton();
            lbtnAcao.ForeColor = System.Drawing.Color.Black;
            lbtnAcao.Font.Overline = false;
            lbtnAcao.Click += new EventHandler(lbtnAcao_Click);
            lbtnAcao.CommandArgument = t08.t08_cd_acao.ToString();
            lbtnAcao.ID = "lbtnAcao" + t08.t08_cd_acao.ToString();
            lbtnAcao.Text = "<img src=\"images/arrow_right.gif\" /> " + t08.nm_acao;
            lbtnAcao.ToolTip = "Ir para o detalhamento da ação";
            if (Convert.ToInt32(pb.Session("cd_acao")) == t08.t08_cd_acao)
            {
                lbtnAcao.Font.Bold = true;
            }
            PanelAcoes.Controls.Add(lbtnAcao);
            PanelAcoes.Controls.Add(pb.GetLiteral("</div>"));
        }


        //adicionar javascript para troca de ação
        lnkTrocaDeAcao.Attributes.Add("onmouseover", "TagToTip('"+ PanelAcoes.ClientID +"', OFFSETX, -5, OFFSETY, -5, CLOSEBTN, true, STICKY, true, WIDTH, 600, TITLE, 'Detalhamento das demais Ações')");
        lnkTrocaDeAcao.Attributes.Add("onmouseout", "UnTip()");
       
    }

    void lbtnAcao_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)sender;
        Session["cd_acao"] = lbtn.CommandArgument;
        Response.Redirect("AcaoDetalhes.aspx");
    }

    private void Retrieve()
    {
        t08_acao t08 = new t08_acaoAction().Retrieve(Convert.ToInt32(pb.Session("cd_acao")));

        if (t08.ds_acao == null) { t08.ds_acao = ""; }

        if (t08.ds_acao.Length > 0)
        {
            lblds_acao.Text = t08.ds_acao;
        }
        else
        {
            trDesc.Visible = false;
        }

        lblnm_acao.Text = t08.nm_acao;
        lblnm_nome.Text = pb.dadosUsuario(new t02_usuarioAction().Retrieve(t08.t02_cd_usuario), 0);
        lbldt_inicio.Text = t08.dt_inicio.ToShortDateString();
        lbldt_fim.Text = t08.dt_fim.ToShortDateString();

        if (pb.cd_usuario() == t08.t02_cd_usuario)
        {
            Session["fl_respacao"] = true;
        }
        else
        {
            Session["fl_respacao"] = false;
        }
    }
}
