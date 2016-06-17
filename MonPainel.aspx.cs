using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MonPainel : System.Web.UI.Page
{
    string[] month = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {        
        ddlt01_cd_entidade_parc.PreRender += new EventHandler(ddl_PreRender);
        ddlt01_cd_entidade_resp.PreRender += new EventHandler(ddl_PreRender);
        ddlt21_cd_fase.PreRender += new EventHandler(ddl_PreRender);
        ddlt26_cd_arearesultado.PreRender += new EventHandler(ddl_PreRender);
        ddlt03_cd_projeto.PreRender += new EventHandler(ddl_PreRender);

        if (!IsPostBack)
        {
            divFiltro.Visible = true;
            FormBind();

            if (Session["filtroMon"] != null)
            {
                LabelsFiltro();
            }
            Filtro();
        }
    }
   
    private void Filtro()
    {
        FiltroProjeto filtro = new FiltroProjeto();
        if (Session["filtroMon"] != null)
        {
            filtro = (FiltroProjeto)Session["filtroMon"];
            ListItem li = null;
            if (filtro.T01_cd_entidade_resp != null)
            {
                ddlt01_cd_entidade_resp.ClearSelection();
                li = ddlt01_cd_entidade_resp.Items.FindByValue(filtro.T01_cd_entidade_resp.ToString());
                if (li != null)
                    li.Selected = true;
            }
            if (filtro.T01_cd_entidade_parc != null)
            {
                ddlt01_cd_entidade_parc.ClearSelection();
                li = ddlt01_cd_entidade_parc.Items.FindByValue(filtro.T01_cd_entidade_parc.ToString());
                if (li != null)
                    li.Selected = true;
            }
            if (filtro.T21_cd_fase != null)
            {
                ddlt21_cd_fase.ClearSelection();
                li = ddlt21_cd_fase.Items.FindByValue(filtro.T21_cd_fase.ToString());
                if (li != null)
                    li.Selected = true;
            }
            if (filtro.T26_cd_arearesultado != null)
            {
                ddlt26_cd_arearesultado.ClearSelection();
                li = ddlt26_cd_arearesultado.Items.FindByValue(filtro.T26_cd_arearesultado.ToString());
                if (li != null)
                    li.Selected = true;
            }
            PanelBind(filtro);

        }
        else
        {
            this.mdlPopup.Show();
        }
    }

    private void PanelBind(FiltroProjeto filtro)
    {        
        /*******************************************************/
                
        //verifica se existe resultado para o filtro
        int n = new t03_projetoAction().ListTodos(filtro).Rows.Count;

        //verifica se ele é Gerente ou da mesma area de resultado.                
        DataTable dtbl = new t03_projetoAction().ListMon(filtro);
        int m = dtbl.Rows.Count;

        if (n == 0)
        {
            mdlPopup.Show();
            lblMsgPopUp.Text = pb.Message("A seleção efetuada não possui informações. Tente novamente.", "erro");
            //lblMsgPopUp.Text = pb.Message("Acesso restrito", "erro");
            lblMsgPopUp.Visible = true;
        }
        else if (n > 0 && m > 0)
        {
            linkfiltroprojetos.Text = dtbl.Rows.Count.ToString();
            GrafAtualizacao(filtro);
            MarcoCritico(filtro);
            Parceiros(filtro);
            GrafFisicoFinanceiro(filtro);
            t03_projeto t03 = new t03_projetoAction().RetriveMonPeriodoAnalisado(filtro);
            lblfiltroperiodo.Text = t03.dt_inicio.ToShortDateString() +
                " - " + t03.dt_fim.ToShortDateString();
        }
        else
        {
            mdlPopup.Show();
            lblMsgPopUp.Text = pb.Message("Acesso restrito", "erro");
            lblMsgPopUp.Visible = true;
        }
        /*******************************************************/        
        
    }

    private void LabelsFiltro()
    {
        if ((Session["filtroentidadeparc"] != "") && (Session["filtroentidadeparc"] != null))
        { lblfiltroentidadeparc.Text = Session["filtroentidadeparc"].ToString(); }
        else { lblfiltroentidadeparc.Text = ddlt01_cd_entidade_parc.SelectedItem.Text; }

        if ((Session["filtroentidaderesp"] != "") && (Session["filtroentidaderesp"] != null))
        { lblfiltroentidaderesp.Text = Session["filtroentidaderesp"].ToString(); }
        else { lblfiltroentidaderesp.Text = ddlt01_cd_entidade_resp.SelectedItem.Text; }

        if ((Session["TextProjeto"] != "") && (Session["TextProjeto"] != null))
        { lblfiltroprojeto.Text = Session["TextProjeto"].ToString(); }
        else { lblfiltroprojeto.Text = ddlt03_cd_projeto.SelectedItem.Text; }

        if ((Session["filtrofase"] != "") && (Session["filtrofase"] != null))
        { lblfiltrofase.Text = Session["filtrofase"].ToString(); }
        else { lblfiltrofase.Text = ddlt21_cd_fase.SelectedItem.Text; }

        if ((Session["filtroarearesultado"] != "") && (Session["filtroarearesultado"] != null))
        {  lblfiltroarearesultado.Text = Session["filtroarearesultado"].ToString();}
        else { lblfiltroarearesultado.Text = ddlt26_cd_arearesultado.SelectedItem.Text; }
        lblfiltrogerado.Text = DateTime.Now.ToShortDateString();
    }

    void ddl_PreRender(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        pb.AddColorLines(ddl, "#f7f9ee");

        if (ddl.SelectedValue != "")
        {
            ddl.Attributes.Add("style", "border: solid 1px #fa9600");
        }
        else
        {
            ddl.Attributes.Add("style", "border: solid 1px #999999");
        }

    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        //zera a cessao do id para pegar novo id
        Session["ValueProjeto"] = null;
        Session["TextProjeto"] = null;
        Session["filtroentidaderesp"] = null;
        Session["filtroentidadeparc"] = null;        
        Session["filtrofase"] = null;
        Session["filtroarearesultado"] = null;

        LabelsFiltro();

        FiltroProjeto filtro = new FiltroProjeto();
        if (ddlt03_cd_projeto.SelectedValue != "")
        {
            if (ddlt03_cd_projeto.SelectedItem.Text == "Todos")
            {
                filtro.array_cd_projeto = ddlt03_cd_projeto.SelectedValue.ToString();
            }
            else {
                if ((Session["ValueProjeto"] != "") && (Session["ValueProjeto"] != null))
                {
                    filtro.T03_cd_projeto = Convert.ToInt32(Session["ValueProjeto"]);
                }
                else
                {
                    filtro.T03_cd_projeto = Convert.ToInt32(ddlt03_cd_projeto.SelectedValue);
                    Session["ValueProjeto"] = Convert.ToInt32(ddlt03_cd_projeto.SelectedValue);
                    Session["TextProjeto"] = ddlt03_cd_projeto.SelectedItem.Text;
                }
            }
        }
        if (ddlt01_cd_entidade_resp.SelectedValue != "")
        {
            filtro.T01_cd_entidade_resp =
                Convert.ToInt32(ddlt01_cd_entidade_resp.SelectedValue);
            Session["filtroentidaderesp"] = ddlt01_cd_entidade_resp.SelectedItem.Text;
        }
        if (ddlt01_cd_entidade_parc.SelectedValue != "")
        {
            filtro.T01_cd_entidade_parc =
                Convert.ToInt32(ddlt01_cd_entidade_parc.SelectedValue);
            Session["filtroentidadeparc"] = ddlt01_cd_entidade_parc.SelectedItem.Text;
        }
        if (ddlt21_cd_fase.SelectedValue != "")
        {
            filtro.T21_cd_fase =
                Convert.ToInt32(ddlt21_cd_fase.SelectedValue);
            Session["filtrofase"] = ddlt21_cd_fase.SelectedItem.Text;
        }
        if (ddlt26_cd_arearesultado.SelectedValue != "")
        {
            filtro.T26_cd_arearesultado =
                Convert.ToInt32(ddlt26_cd_arearesultado.SelectedValue);
            Session["filtroarearesultado"] = ddlt26_cd_arearesultado.SelectedItem.Text;
        }
        Session["filtroMon"] = filtro;
        Filtro();

        //msg de erro quando n resulta nada
        //if (linkfiltroprojetos.Text == "0")
        //{
        //    mdlPopup.Show();
        //    lblMsgPopUp.Text = pb.Message("A seleção efetuada não possui informações. Tente novamente.", "erro");
        //    //lblMsgPopUp.Text = pb.Message("Acesso restrito", "erro");
        //    lblMsgPopUp.Visible = true;
        //}
    }

    private void FormBind()
    {
        lblObs.Text = "*Valores até " + month[pb.NomeMes(DateTime.Now.Month - 2)] + " de " +  pb.NomeAno(DateTime.Now.Month - 2);
        DropDownList ddl;

        t21_faseAction t21a = new t21_faseAction();
        ddl = ddlt21_cd_fase;
        ddl.DataSource = t21a.ListTodos();
        ddl.DataTextField = "nm_fase";
        ddl.DataValueField = "t21_cd_fase";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Todas");

        ddl = ddlt01_cd_entidade_parc;
        t01_entidadeAction t01a = new t01_entidadeAction();
        ddl.DataSource = t01a.ListTodos();
        ddl.DataTextField = "nm_entidade";
        ddl.DataValueField = "t01_cd_entidade";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Todas");

        ddl = ddlt01_cd_entidade_resp;
        ddl.DataSource = ddlt01_cd_entidade_parc.DataSource;
        ddl.DataTextField = "nm_entidade";
        ddl.DataValueField = "t01_cd_entidade";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Todas");

        //--- Área de Resultado ---//
        ddl = ddlt26_cd_arearesultado;
        ddl.DataSource = new t26_arearesultadoAction().ListTodos();
        ddl.DataTextField = "nm_area";
        ddl.DataValueField = "t26_cd_arearesultado";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Todas");


        //--- Projetos ---//
        if (Session["nome"] != null)
        {
            if ((Convert.ToBoolean(pb.Session("fl_monitora"))) || (Convert.ToBoolean(pb.Session("fl_admin"))))
            {
                ddl = ddlt03_cd_projeto;
                ddl.DataSource = new t03_projetoAction().ListTodos();
                ddl.DataTextField = "nm_projeto";
                ddl.DataValueField = "t03_cd_projeto";
                ddl.DataBind();
                pb.AddEmptyItem(ddl, "Todos");
            }
            else
            {
                RestricaoRelatorioSituacoes();
            }
        }
    }

    public void RestricaoRelatorioSituacoes()
    {
        ddlt03_cd_projeto.ClearSelection();
        t02_usuarioAction t02a = new t02_usuarioAction();
        DataTable dt = t02a.ListRelatorioSituacoes(Session["cd_usuario"].ToString());
        string todos = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["t03_cd_projeto"] != DBNull.Value)
            {
                ddlt03_cd_projeto.Items.Add(new ListItem(dr["nm_projeto"].ToString(), dr["t03_cd_projeto"].ToString()));
                todos += dr["t03_cd_projeto"].ToString();
                if (dt.Rows.Count > 0) {
                    todos += ",";
                }
            }
        }
        if (todos.Length > 0)
        {
            todos = todos.Substring(0, todos.Length - 1);
        }  
        //Response.Write(todos);
        Session["allproject"] = todos;
        ddlt03_cd_projeto.DataBind();
        ListItem item = new ListItem("Todos", todos);
        ddlt03_cd_projeto.Items.Insert(0, item);
        //pb.AddEmptyItem(ddlt03_cd_projeto, "Todos");

    }

    public void GrafAtualizacao(FiltroProjeto filtro)
    {
        try
        {
            /*
             * Semanas
             */
            int zeroAduas = 0; 
            int duasAquatro = 0;
            int maisDquatro = 0;

            int i = 0;
            double totaldias = 0;
            int dias = 0;

            foreach (t03_projeto t03 in new t03_projetoAction().ListObjAtualizadoMon(filtro))
            {
                dias = (DateTime.Now - t03.dt_atualizado).Days + 1;
                if (dias <= 14)
                    zeroAduas++;
                if (dias > 14 && dias <= 28)
                    duasAquatro++;
                if (dias > 28)
                    maisDquatro++;

                totaldias += dias;
                i++;
            }

            if ((totaldias / i) == 1)
            {
                linkAtualiza.Text = "Média de dias sem atualizar: " + (totaldias / i).ToString("N0") + " dia";
            }
            else if (double.IsNaN(totaldias / i))
            {
                if (i == 1)
                {
                    linkAtualiza.Text = "Projeto não foi atualizado.";
                }
                else
                {
                    linkAtualiza.Text = "Projetos não foram atualizados.";
                }

                linkAtualiza.Enabled = false;
            }
            else
            {
                linkAtualiza.Text = "Média de dias sem atualizar: " + (totaldias / i).ToString("N0") + " dias";
            }
            linkAtualiza.ToolTip = linkAtualiza.Text;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<graph caption='' bgColor='' shownames='1' showvalues='1' formatNumber='1' decimalSeparator=',' decimalPrecision='0' numberSuffix=''>");

            string periodostr = "0 a 2 semanas";
            string cormen = "#008000";
            if (zeroAduas== 0)
            {
                sb.Append("<set name='" + periodostr + "' color='" + cormen + "'/>");
            }
            else
            {
                sb.Append("<set name='" + periodostr + "' value='" + zeroAduas.ToString().Replace(",", ".") + "' color='" + cormen + "'/>");
            }

            periodostr = "2 a 4 semanas";
            cormen = "#FFFF00";
            if (duasAquatro == 0)
            {
                sb.Append("<set name='" + periodostr + "' color='" + cormen + "'/>");
            }
            else
            {
                sb.Append("<set name='" + periodostr + "' value='" + duasAquatro.ToString().Replace(",", ".") + "' color='" + cormen + "'/>");
            }

            periodostr = "mais de 4 semanas";
            cormen = "#FF0000";
            if (maisDquatro == 0)
            {
               sb.Append("<set name='" + periodostr + "' color='" + cormen + "'/>");
            }
            else
            {
                sb.Append("<set name='" + periodostr + "' value='" + maisDquatro.ToString().Replace(",", ".") + "' color='" + cormen + "'/>");
            }
            sb.Append("</graph>");
            pnAtualizacao.Controls.Add(pb.GetLiteral(pb.GetFlash(150, 400, "Charts/FC_2_3_Column3D.swf", sb.ToString(), 0, "GrafAtualizacao")));

        }
        catch (Exception ex)
        {
            Response.Write("GrafAtualizacao: " + ex.Message);
        }
    }

    private void MarcoCritico(FiltroProjeto filtro)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        StatusProjeto stp = new StatusProjeto();
        stp.QtdVermelho = 0;
        stp.QtdAzul = 0;
        stp.QtdAmarelo = 0;
        stp.QtdVerde = 0;
        foreach (t03_projeto t03 in new t03_projetoAction().ListObjMon(filtro))
        {
            foreach (DataRow dr in new t09_marcoAction().ListTodos(t03.t03_cd_projeto).Rows)
            {
                switch (dr["fl_status"].ToString())
                {
                    case "R":
                        stp.QtdVermelho++;
                        break;
                    case "G":
                        stp.QtdVerde++;
                        break;
                    case "B":
                        stp.QtdAzul++;
                        break;
                    case "Y":
                        stp.QtdAmarelo++;
                        break;
                }
            }
        }

        pnMarcosStatus.Controls.Add(pb.GetLiteral(stp.tbGraficoStatus()));

        lblAzul.Text = stp.QtdAzul.ToString();
        lblVerde.Text = stp.QtdVerde.ToString();
        lblAmarela.Text = stp.QtdAmarelo.ToString();
        lblVermelha.Text = stp.QtdVermelho.ToString();

        lblFatiaAzul.Text = stp.PercAzul.ToString();
        lblFatiaVerde.Text = stp.PercVerde.ToString();
        lblFatiaAmarela.Text = stp.PercAmarelo.ToString();
        lblFatiaVermelha.Text = stp.PercVermelho.ToString();

        if (stp.QtdAzul > 0) { linkConcluidos.NavigateUrl = "~/MonMarcos.aspx?fl_status=B"; }
        else { linkConcluidos.NavigateUrl = ""; }

        if (stp.QtdVerde > 0) { linkPrazos.NavigateUrl = "~/MonMarcos.aspx?fl_status=G"; }
        else { linkPrazos.NavigateUrl = ""; }

        if (stp.QtdAmarelo > 0) { linkComRestricoes.NavigateUrl = "~/MonMarcos.aspx?fl_status=Y"; }
        else { linkComRestricoes.NavigateUrl = ""; }

        if (stp.QtdVermelho > 0) { linkAtraso.NavigateUrl = "~/MonMarcos.aspx?fl_status=R"; }
        else { linkAtraso.NavigateUrl = ""; }

    }

    private void Parceiros(FiltroProjeto filtro)
    {
        double i = 0;
        double p = 0;
        foreach (t03_projeto t03 in new t03_projetoAction().ListObjMon(filtro))
        {
            i++;
            p += new t04_parceiroAction().ListTodos(t03.t03_cd_projeto).Rows.Count;
        }
        if (i > 0)
        {
            lblimobilizacao.Text = (p / i).ToString("N2");
        }
    }

    private List<decimal> CalcFisico(FiltroProjeto filtro)
    {
        List<decimal> fisico = new List<decimal>();
        int ano = DateTime.Now.Year;
        int mes = DateTime.Now.Month;
        decimal prev = 0;
        decimal real = 0;
        DataTable dtb = new DataTable();
        dtb = MakeDataTable();

        List<t03_projeto> t03List = new t03_projetoAction().ListObjMon(filtro);
        foreach (t03_projeto t03 in t03List)
        {
            DataRow dr = dtb.NewRow();
            dr[0] = t03.t03_cd_projeto;
            dr[1] = t03.nm_projeto;
            decimal prevproj = 0;
            decimal realproj = 0;
            int c = 0; //contador de produtos de apenas 1 projeto
            foreach (t08_acao t08 in new t08_acaoAction().ListObjTodos(t03.t03_cd_projeto))
            {
                foreach (t10_produto t10 in new t10_produtoAction().ListObjTodos(t08.t08_cd_acao))
                {
                    c++;
                    decimal tprev = 0;
                    decimal treal = 0;
                    decimal prevtotal = 0;
                    ViewState["tprev_fisico"] = 0;
                    ViewState["treal_fisico"] = 0;
                    foreach (t17_vlproduto t17 in t10.t17)
                    {
                        if (ano > t17.nu_ano)
                        {
                            tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                 t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11 + t17.vl_p12;
                            treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                 t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11 + t17.vl_r12;
                            ViewState["tprev_fisico"] = tprev;
                            ViewState["treal_fisico"] = treal;

                        }
                        else if (ano == t17.nu_ano)
                        {
                            if (mes == 1)
                            {
                                tprev = Convert.ToDecimal(ViewState["tprev_fisico"]);
                                treal = Convert.ToDecimal(ViewState["treal_fisico"]);                                
                                //tprev += t17.vl_p1;
                                //treal += t17.vl_r1;
                            }
                            else if (mes == 2)
                            {
                                tprev += t17.vl_p1;
                                treal += t17.vl_r1;
                            }
                            else if (mes == 3)
                            {
                                tprev += t17.vl_p1 + t17.vl_p2;
                                treal += t17.vl_r1 + t17.vl_r2;
                            }
                            else if (mes == 4)
                            {
                                tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3;
                                treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3;
                            }
                            else if (mes == 5)
                            {
                                tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4;
                                treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4;
                            }
                            else if (mes == 6)
                            {
                                tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5;
                                treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5;
                            }
                            else if (mes == 7)
                            {
                                tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6;
                                treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6;
                            }
                            else if (mes == 8)
                            {
                                tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                    t17.vl_p7;
                                treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                     t17.vl_r7;
                            }
                            else if (mes == 9)
                            {
                                tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                    t17.vl_p7 + t17.vl_p8;
                                treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                     t17.vl_r7 + t17.vl_r8;
                            }
                            else if (mes == 10)
                            {
                                tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                    t17.vl_p7 + t17.vl_p8 + t17.vl_p9;
                                treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                     t17.vl_r7 + t17.vl_r8 + t17.vl_r9;
                            }
                            else if (mes == 11)
                            {
                                tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                    t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10;
                                treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                     t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10;
                            }
                            else if (mes == 12)
                            {
                                tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                    t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11;
                                treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                     t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11;
                            }
                        }
                        prevtotal += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                 t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11 + t17.vl_p12;
                    }
                    if (prevtotal > 0)
                    {
                        prevproj += (tprev * 100) / prevtotal;
                        realproj += (treal * 100) / prevtotal;
                    }
                }
            }
            if (c > 0)
            {
                prevproj = prevproj / c;
                realproj = realproj / c;

                prev += prevproj;
                real += realproj;
            }
            dr[2] = prevproj;
            dr[3] = realproj;

            dtb.Rows.Add(dr);
        }

        Session["DataTableProduto"] = dtb;

        if (t03List.Count > 0)
        {
            prev = (prev / t03List.Count);
            real = (real / t03List.Count);
        }
        fisico.Add(prev);
        fisico.Add(real);
        return fisico;
    }

    private List<decimal> CalcFinanceiro(FiltroProjeto filtro)
    {
        t03_projeto t03a = new t03_projetoAction().RetriveMonPeriodoAnalisado(filtro);
        int ano_ini = t03a.dt_inicio.Year;
        int ano_fim = t03a.dt_fim.Year;

        List<decimal> financeiro = new List<decimal>();
        int ano = DateTime.Now.Year;
        int mes = DateTime.Now.Month;
        decimal planejado = 0;
        decimal liquidado = 0;
        decimal fplanejado = 0;
        decimal fliquidado = 0;
        decimal totalplanejado = 0;
        decimal totalmesplanejado = 0;
        decimal totalmesliquidado = 0;


        decimal[] arplanejado = new decimal[ano_fim + 1];
        decimal[] arliquidado = new decimal[ano_fim + 1];

        DataTable dtb = new DataTable();
        dtb = MakeDataTable();

        List<t03_projeto> t03List = new t03_projetoAction().ListObjMon(filtro);
        foreach (t03_projeto t03 in t03List)
        {
            DataRow dr = dtb.NewRow();
            dr[0] = t03.t03_cd_projeto;
            dr[1] = t03.nm_projeto;
            decimal planejadoproj = 0;
            decimal liquidadoproj = 0;
            decimal planejadototal = 0;
            fplanejado = 0;
            fliquidado = 0;

            int c = 0; //contador de produtos de apenas 1 projeto
            foreach (t08_acao t08 in new t08_acaoAction().ListObjTodos(t03.t03_cd_projeto))
            {
                foreach (t11_financeiro t11 in new t11_financeiroAction().ListObjTodos(t08.t08_cd_acao))
                {
                    c++;
                    decimal tplanejado = 0;
                    decimal tliquidado = 0;
                    ViewState["tplanejado_financeiro"] = 0;
                    ViewState["tliquidado_financeiro"] = 0;
                    foreach (t18b_vlfinanceiro t18 in t11.t18l)
                    {
                        if (ano > t18.nu_ano)
                        {
                            tplanejado += (t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 +
                            t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 +
                            t18.vl_planejado10 + t18.vl_planejado11 + t18.vl_planejado12);

                            tliquidado += (t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 +
                        t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 + t18.vl_liquidado10 +
                        t18.vl_liquidado11 + t18.vl_liquidado12);

                            ViewState["tplanejado_financeiro"] = tplanejado;
                            ViewState["tliquidado_financeiro"] = tliquidado;

                        }
                        else if (ano == t18.nu_ano)
                        {
                            if (mes == 1)
                            {
                               tplanejado += Convert.ToDecimal(ViewState["tplanejado_financeiro"]);
                               tliquidado += Convert.ToDecimal(ViewState["tliquidado_financeiro"]);
                                //tplanejado += t18.vl_planejado1;
                                //tliquidado += t18.vl_liquidado1;
                            }
                            else if (mes == 2)
                            {
                                tplanejado += t18.vl_planejado1;
                                tliquidado += t18.vl_liquidado1;
                            }
                            else if (mes == 3)
                            {
                                tplanejado += t18.vl_planejado1 + t18.vl_planejado2;
                                tliquidado += t18.vl_liquidado1 + t18.vl_liquidado2;
                            }
                            else if (mes == 4)
                            {
                                tplanejado += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3;
                                tliquidado += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3;
                            }
                            else if (mes == 5)
                            {
                                tplanejado += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4;
                                tliquidado += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4;
                            }
                            else if (mes == 6)
                            {
                                tplanejado += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5;
                                tliquidado += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5;
                            }
                            else if (mes == 7)
                            {
                                tplanejado += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 +
                       t18.vl_planejado6;
                                tliquidado += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 +
                       t18.vl_liquidado6;
                            }
                            else if (mes == 8)
                            {
                                tplanejado += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 +
                        t18.vl_planejado6 + t18.vl_planejado7;
                                tliquidado += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 +
                       t18.vl_liquidado6 + t18.vl_liquidado7;
                            }
                            else if (mes == 9)
                            {
                                tplanejado += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 +
                            t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8;
                                tliquidado += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 +
                        t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8;                        
                            }
                            else if (mes == 10)
                            {
                                tplanejado += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 +
                            t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9;
                                tliquidado += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 +
                        t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9;
                            }
                            else if (mes == 11)
                            {
                                tplanejado += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 +
                            t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 +
                            t18.vl_planejado10;
                                tliquidado += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 +
                         t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 + t18.vl_liquidado10;
                            }
                            else if (mes == 12)
                            {
                                tplanejado += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 +
                            t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 +
                            t18.vl_planejado10 + t18.vl_planejado11;
                                tliquidado += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 +
                        t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 + t18.vl_liquidado10 +
                        t18.vl_liquidado11;
                            }
                        }
                        
                        planejadototal += (t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 +
                            t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 +
                            t18.vl_planejado10 + t18.vl_planejado11 + t18.vl_planejado12);

                        //array para o gráfico
                        arplanejado[t18.nu_ano] += (t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 +
                            t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 +
                            t18.vl_planejado10 + t18.vl_planejado11 + t18.vl_planejado12);

                        arliquidado[t18.nu_ano] += (t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 +
                        t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 + t18.vl_liquidado10 +
                        t18.vl_liquidado11 + t18.vl_liquidado12);
                        
                    }
                    //adiciona total mes-1                    
                    fplanejado += tplanejado;
                    fliquidado += tliquidado;                   
                }//fim laço financeiro

                //Response.Write("planejadoTotal -" + planejadototal + "<br>");
                

            }//fim laço ação

            //calcula a porcentagem de cada projeto
            if (planejadototal > 0)
            {
                //soma total de projetos
                {
                    totalplanejado += planejadototal;
                    totalmesplanejado += fplanejado;
                    totalmesliquidado += fliquidado;
                }

                planejadoproj += (fplanejado / planejadototal) * 100;
                liquidadoproj += (fliquidado / planejadototal) * 100;

                //Response.Write("planejado porcento -" + planejadoproj + "<br>");
                //Response.Write("Liquidado porcento-" + liquidadoproj + "<br>");
            }
            if (c > 0)
            {                
                //planejadoproj = planejadoproj / c;
                //liquidadoproj = liquidadoproj / c;
                planejado += planejadoproj;
                liquidado += liquidadoproj;
            }
            
            dr[2] = planejadoproj;
            dr[3] = liquidadoproj;

            foreach (t18b_vlfinanceiro t18 in new t18b_vlfinanceiroAction().ListObjProjetoTotal(t03.t03_cd_projeto))
            {
                dr[4] = t18.vl_planejado1;
                dr[5] = t18.vl_liquidado1;                
                break;
            }
            //Response.Write("fplanejado -" + fplanejado+"<br>");
           
           dr[6] = fplanejado;
           dr[7] = fliquidado;
           

            dtb.Rows.Add(dr);
        }

        Session["DataTableFinanceiro"] = dtb;
        CalculaIndiceFianceiro(Session["DataTableFinanceiro"]);

        //calcula a media
        //if (t03List.Count > 0)
        //{            
        //    planejado = (planejado / t03List.Count);
        //    liquidado = (liquidado / t03List.Count);            
        //}        

        //atribui o valor total
        if (totalmesplanejado > 0)
            planejado = (totalmesplanejado / totalplanejado) * 100;
        if (totalmesliquidado > 0)
            liquidado = (totalmesliquidado / totalplanejado) * 100;
                    
        financeiro.Add(planejado);
        financeiro.Add(liquidado);

        /*
         * INÍCIO GRÁFICO FINANCEIRO
         */
        System.Text.StringBuilder sbgraf = new System.Text.StringBuilder();
        sbgraf.Append("<graph formatNumberScale='0' ");
        sbgraf.Append(" decimalPrecision='2' xAxisName='' yAxisName='' numberPrefix='' ");
        sbgraf.Append("numberSuffix='' showNames='1' showvalues='0'>");//numberSuffix='%25'
        sbgraf.Append("<categories>");
        for (int i = ano_ini; i <= ano_fim; i++)
        {
            sbgraf.Append("<category name='" + i + "' showName='1'/>");
        }
        sbgraf.Append("</categories>");
        sbgraf.Append("<dataset seriesName='Planejado' color='00A900' >");        
        for (int i = ano_ini; i <= ano_fim; i++)
        {
            if (arplanejado[i] > 0)
            {
                sbgraf.Append("<set value='" + (arplanejado[i]).ToString().Replace(",", ".") + "' />");
            }
            else
            {
                sbgraf.Append("<set />");
            }
        }        
        sbgraf.Append("</dataset>");
        sbgraf.Append("<dataset seriesName='Liquidado' color='0000FF' >");
        for (int i = ano_ini; i <= ano_fim; i++)
        {
            if (arliquidado[i] > 0)
            {
                sbgraf.Append("<set value='" + (arliquidado[i]).ToString().Replace(",", ".") + "' />");
            }
            else
            {
                sbgraf.Append("<set />");
            }
        }
        sbgraf.Append("</dataset>");
        sbgraf.Append("</graph>");

        Session["GraficoFinanceiro"] = pb.GetFlash(300, 600, "Charts/FC_2_3_MSColumn3D.swf", sbgraf.ToString(), 0, "GrafFin");
        /*
         * FIM GRÁFICO FINANCEIRO
         */

        return financeiro;
    }

    private void CalculaIndiceFianceiro(object table)
    {
        if (table != null)
        {
            decimal planejado = 0;
            decimal liquidado = 0;
            decimal total = 0;
            DataTable dt = (DataTable)table;
            if (dt.Rows.Count > 1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    planejado += Convert.ToDecimal(dr["vl_fplanejado"]);
                    liquidado += Convert.ToDecimal(dr["vl_fliquidado"]);
                }   
            }
            else if (dt.Rows.Count == 1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    planejado = Convert.ToDecimal(dr["vl_fplanejado"]);
                    liquidado = Convert.ToDecimal(dr["vl_fliquidado"]);
                    break;
                }
            }
            if (planejado > 0)
            {
                total = (liquidado / planejado) * 100;
            }
            else
            {
                total = 0;
            }
            lblifinanceira.Text = total.ToString("N2");
        }
    }

    private DataTable MakeDataTable()
    {
        DataTable dtb = new DataTable();

        DataColumn c = new DataColumn();
        c.ColumnName = "t03_cd_projeto";
        dtb.Columns.Add(c);

        c = new DataColumn();
        c.ColumnName = "nm_projeto";
        dtb.Columns.Add(c);

        c = new DataColumn();
        c.DataType = System.Type.GetType("System.Decimal");
        c.ColumnName = "vl_prev";
        dtb.Columns.Add(c);

        c = new DataColumn();
        c.DataType = System.Type.GetType("System.Decimal");
        c.ColumnName = "vl_real";
        dtb.Columns.Add(c);

        c = new DataColumn();
        c.DataType = System.Type.GetType("System.Decimal");
        c.ColumnName = "vl_planejado";
        dtb.Columns.Add(c);

        c = new DataColumn();
        c.DataType = System.Type.GetType("System.Decimal");
        c.ColumnName = "vl_liquidado";
        dtb.Columns.Add(c);

        c = new DataColumn();
        c.DataType = System.Type.GetType("System.Decimal");
        c.ColumnName = "vl_fplanejado";
        dtb.Columns.Add(c);

        c = new DataColumn();
        c.DataType = System.Type.GetType("System.Decimal");
        c.ColumnName = "vl_fliquidado";
        dtb.Columns.Add(c);

        DataColumn[] PrimaryKeyColumns = new DataColumn[] { dtb.Columns[0] };
        dtb.PrimaryKey = PrimaryKeyColumns;

        return dtb;
    }

    public void GrafFisicoFinanceiro(FiltroProjeto filtro)
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("<graph caption='' chartRightMargin='55' subCaption='' yaxisname='' xaxisname='' showAlternateVGridColor='1' showvalues='1' yAxisMaxValue='100' numberSuffix='%25'");
            sb.Append("shownames='1' alternateVGridAlpha='30' alternateVGridColor='AFD8F8' numDivLines='4' ");
            sb.Append("decimalPrecision='2' canvasBorderThickness='1' canvasBorderColor='114B78' baseFontColor='114B78'");
            sb.Append("hoverCapBorderColor='114B78' hoverCapBgColor='E7EFF6'>");

            List<decimal> fisico = new List<decimal>();
            fisico = CalcFisico(filtro);
            decimal s = 0;
            if (fisico[0] > 0)
                s = (fisico[1] / fisico[0]) * 100;

            lblifisica.Text = s.ToString("N2");
            //lblifisica.Text = fisico[1].ToString("N2");
            sb.Append("<set name='Físico Previsto' hoverText='Físico Previsto' value='" + fisico[0].ToString().Replace(",", ".") + "' color='00A900' alpha='70'/> ");
            sb.Append("<set name='Físico Realizado' hoverText='Físico Realizado' value='" + fisico[1].ToString().Replace(",", ".") + "' color='0000FF' alpha='70'/>");
            

            List<decimal> financeiro = new List<decimal>();
            financeiro = CalcFinanceiro(filtro);

            //lblifinanceira.Text = financeiro[1].ToString("N2");
            sb.Append("<set name='Financeiro Planejado' hoverText='Financeiro Planejado' value='" + financeiro[0].ToString().Replace(",", ".") + "' color='00A900' alpha='70'/> ");
            sb.Append("<set name='Financeiro Liquidado' hoverText='Financeiro Liquidado' value='" + financeiro[1].ToString().Replace(",", ".") + "' color='0000FF' alpha='70'/> ");
            sb.Append("</graph>");
            
            pnFisicoFinanceiro.Controls.Add(pb.GetLiteral(pb.GetFlash(130, 430, "Charts/FC_2_3_Bar2D.swf", sb.ToString(), 0, "GrafFisFin")));
            
        }
        catch (Exception ex)
        {
            Response.Write("GrafFisFin: " + ex.Message);
        }
    }    

}
