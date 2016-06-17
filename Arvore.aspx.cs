using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Arvore : System.Web.UI.Page
{
    pageBase pb = new pageBase();

    bool fl_troca_arq;

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["financeiro"] = 0;
        if (!(IsPostBack))
        {
            fl_troca_arq = false;

            FormBind();
            ArvoreBind("");
            GeraGaleriaFotos();
        }
        
    }    
    //
    // bloco = representa o bloco que será atualizado na arvore
    // caso esse bloco estaja vazio ArvoreBind("") todos os blocos
    // serão atualizados
    //
    private void ArvoreBind(string bloco)
    {
        //mostraos botoes de edicao publico alvo...
        ShowButtonEdit();
                
        t03_projeto t03 = new t03_projeto();
        t03_projetoAction t03a = new t03_projetoAction();
        t03 = t03a.Retrieve(Convert.ToInt32(pb.Session("cd_projeto")));

        SessionsBind(t03);

        PerfilBind(t03);

        LinhaGerencialBind(t03);
        
        EvolucaoBind(t03);

        IconsValuesBind(t03.t03_cd_projeto);

        FinanceiroBind();

        dlResultados.DataSource = new t12_resultadoAction().ListTodos(t03.t03_cd_projeto);
        dlResultados.DataBind();

        if ((bloco == "ds_publico") || (bloco == "")) {
            lblds_publico.Text = pb.ReplaceNewLines(t03.ds_publico);
            txtds_publico.Text = t03.ds_publico;
        }
        if ((bloco == "ds_objetivo") || (bloco == "")) {
            lblds_objetivo.Text = pb.ReplaceNewLines(t03.ds_objetivo);
            txtds_objetivo.Text = t03.ds_objetivo;
        }
        if ((bloco == "dt_inicio") || (bloco == "")) {
            if (t03.dt_inicio.Year > 1)
            {
                lbldt_inicio.Text = t03.dt_inicio.ToShortDateString();
                txtdt_inicio.Text = t03.dt_inicio.ToShortDateString();

                if (pb.fl_gerente())
                {
                    linkResultados.Visible = true;
                }
            }
            else
            {
                if (pb.fl_gerente())
                {
                    linkResultados.Visible = false;
                }
            }
            

            if (t03.dt_fim.Year > 1)
            {
                lbldt_fim.Text = t03.dt_fim.ToShortDateString();
                txtdt_fim.Text = t03.dt_fim.ToShortDateString();
            }
        }
        if ((bloco == "ds_situacao") || (bloco == ""))
        {
            t05_situacaoAction t05m = new t05_situacaoAction();
            t05_situacao t05 = new t05_situacao();
            t05 = t05m.RetrieveByProjeto(t03.t03_cd_projeto);
            string dataSituacao = "";

            if (t05.dt_cadastro.Year > 1)
            {
                dataSituacao = "<br>Data: " + t05.dt_cadastro.ToShortDateString();
            }
            lblds_situacao.Text = pb.ReplaceNewLines(t05.ds_situacao) + dataSituacao;


            txtds_situacao.Text = t05.ds_situacao;

            HlkSituacao.Visible = false;
            //inclui link
            //if (Session["cd_projeto"] != null)
            //    HlkSituacao.NavigateUrl = "Rel_ProjSituacoes.aspx?id=" + Session["cd_projeto"] + "";

        }
        
        if ((bloco == "t21_cd_fase") || (bloco == ""))
        {
            t21_fase t21 = new t21_fase();
            t21 = (new t21_faseAction().Retrieve(
                new t22_faseprojetoAction().Retrieve(t03.t03_cd_projeto).t21_cd_fase));         

            lblnm_fase.Text = t21.nm_fase;
            ddlt21_cd_fase.ClearSelection();
            ListItem li = ddlt21_cd_fase.Items.FindByValue(t21.t21_cd_fase.ToString());
            if (li != null)
                li.Selected = true;
        }

        //double endCounter = Convert.ToDouble(DateTime.Now.TimeOfDay.TotalSeconds);
        //double totalSeconds = endCounter - startCounter;
        //lbltempo.Text = "Duração: " + totalSeconds + "segundos";

        //chama restricao financeiro
        ShowFinanceiro();

        //oculta links da arvore por perfil
        //HideLinksArvore();

         //condição visitante
        ShowSituacao();                   

    }

    private void ShowButtonEdit()
    {

        t21_fase t21 = new t21_fase();
        t21 = (new t21_faseAction().Retrieve(
            new t22_faseprojetoAction().Retrieve(Convert.ToInt32(pb.Session("cd_projeto"))).t21_cd_fase));

        if (t21.nm_fase == "Em Estruturação")
        {           
            pnlButtonEditObjetivo.Visible = true;
            pnlButtonEditPublico.Visible = true;         
        }
        else {            
            pnlButtonEditObjetivo.Visible = false;
            pnlButtonEditPublico.Visible = false;            

        }
        if (t21.t21_cd_fase == 2)
        {
            linkds_publico.Visible = false;
            linkdt_inicio.Visible = false;
            linkResponsaveis.Visible = false;
        }
        else
        {
            linkds_publico.Visible = true;
            linkdt_inicio.Visible = true;
            linkResponsaveis.Visible = true;
        }

    }

    private void SessionsBind(t03_projeto t03)
    {
        Util.CodProjetoSustentador = t03.t03_cd_projeto.ToString(); //guardo o codigo do projeto sustentador q tem varias acoes ligadas a ele
        Session["codigoProjeto"] = t03.t03_cd_projeto;
        Session["nm_projeto"] = t03.nm_projeto;
        Session["cd_area"] = t03.t26_cd_arearesultado;
        Session["nm_area"] = t03.t26.nm_area;
        Session["nm_arquivo"] = t03.t26.nm_arquivo;
        Session["dt_inicio"] = t03.dt_inicio;
        Session["dt_fim"] = t03.dt_fim;
    }

    private void IconsValuesBind(int cd_projeto)
    {
        lblparceiros.Text = new t04_parceiroAction().ListTodos(cd_projeto).Rows.Count.ToString();

        lblCronograma.Text = new t14_documentoAction().ListCrono(cd_projeto).Rows.Count.ToString();
        //lblFoto.Text = new t14_documentoAction().ListFoto(cd_projeto).Rows.Count.ToString();
        lblOutros.Text = new t14_documentoAction().ListDoc(cd_projeto).Rows.Count.ToString();
        lblAgenda.Text = new t16_agendaAction().ListDoProjeto(cd_projeto).Rows.Count.ToString();
        lblNoticias.Text = new t15_noticiaAction().ListDoProjeto(cd_projeto).Rows.Count.ToString();
    }

    private void EvolucaoBind(t03_projeto t03)
    {
        if (t03.dt_fim.Year > 1)
        {
            string status = new t09_marcoAction().Status(t03.t03_cd_projeto);
            if (status.Length > 0)
            {
                lblstatus.Text = "<div style=\"width:200px\">" +
                    "<div style=\"font-size:x-small;color:#114b78;text-align:left\">Marcos Críticos</div>" +
                    status + "</div>";
            }

            lblgrafico.Text = pb.GetFlash(90, 250, "Charts/FC_2_3_Bar2D.swf", GetXmlValue(t03), 0, "arvore");

            if (new t07_restricaoAction().ListTodos(t03.t03_cd_projeto).Rows.Count > 0)
            {
                lblrestricao.Text = "<img src=\"images/Restricao.gif\" title=\"Projeto possui uma ou mais restrições\" />";
            }
        }

        if (t03.dt_atualizado.Year > 1)
        {
            lbldt_atualizado.Text = t03.dt_atualizado.ToShortDateString();
        }
        else
        {
            lbldt_atualizado.Text = "-";
        }
    }

    private void PerfilBind(t03_projeto t03)
    {
        if (pb.cd_usuario() == t03.t02_cd_usuario)
        {
            Session["fl_gerente"] = true;
        }
        else
        {
            Session["fl_gerente"] = false;
            if (t03.dt_inicio.Year == 1)
            {
                spanDetalhamento.Visible = false;
                linkResultados.Visible = false;
            }
            linkds_objetivo.Visible = false;
            linkds_publico.Visible = false;
            linkds_situacao.Visible = false;
            linkdt_inicio.Visible = false;
            linkResponsaveis.Visible = false;
            hlkGaleria.Visible = false;
            hlkExibirGaleria.Visible = true;
        }

        if (pb.cd_usuario() == t03.t02_cd_usuario_monitoramento)
        {
            linkt21_cd_fase.Visible = true;
            Session["fl_respmonitora"] = true;
        }
        else
        {
            linkt21_cd_fase.Visible = false;
            Session["fl_respmonitora"] = false;
        }
    }

    private void LinhaGerencialBind(t03_projeto t03)
    {
        int index = 0;
        t02_usuario t02 = new t02_usuarioAction().Retrieve(t03.t02_cd_usuario);
        lbldados_gerente.Text = new t02_usuarioAction().DadosUsuario(t02, index);

        index = 1;
        t02 = new t02_usuarioAction().Retrieve(t03.t02_cd_usuario_monitoramento);
        lbldados_monitor.Text = new t02_usuarioAction().DadosUsuario(t02, index);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<div style=\"font-weight:bold; margin-bottom:5px\">Responsáveis</div>");
        foreach (t06_colaborador t06 
            in new t06_colaboradorAction().ListObjTodos(t03.t03_cd_projeto))
        {
            index++;
            t02 = new t02_usuario(); 
            t02 = t06.t02;

            //t02.nm_nome = t02.nm_nome + " <br> <b>" + t06.nm_funcao + "</b>";
            t02.nm_nome = t02.nm_nome + " <br> <b>" + t02.nm_cargo + "</b>";
            sb.AppendLine(new t02_usuarioAction().DadosUsuario(t02,index)); 
        }

        if (index == 1)
        {
            //então nenhum responsável 
            if (!pb.fl_gerente())
            {
                sb = new StringBuilder();
            }
        }

        LiteralLinhaGerencial.Text = sb.ToString();
    }

    private void FormBind()
    {
        DropDownList ddl;

        t21_faseAction t21a = new t21_faseAction();
        ddl = ddlt21_cd_fase;
        ddl.DataSource = t21a.ListTodos();
        ddl.DataTextField = "nm_fase";
        ddl.DataValueField = "t21_cd_fase";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Selecione");

    }

    protected void link_Click(object sender, System.EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        Panel panel = (Panel)PanelArvore.FindControl("Panel" + link.CommandArgument);
        Panel PanelEdit = (Panel)PanelArvore.FindControl("PanelEdit" + link.CommandArgument);
        Button btn = (Button)PanelArvore.FindControl("btnSalvar" + link.CommandArgument);
        if (PanelEdit != null)
        {
            PanelEdit.Visible = true;
            if (PanelEdit.Controls[1] != null)
            {
                RegisterStartupScript("clientScript",
                      "<script>document.getElementById('" + PanelEdit.Controls[1].ClientID +
                      "').focus();</script>");
            }
        }
        if (panel != null)
        {
            panel.Visible = false;
        }

    }

    protected void btnSalvar_Click(object sender, System.EventArgs e)
    {
        int marco = 0;
        int acoes = 0;
        int resultados = 0;
        int result = 0;
        bool erro = false;
        string msg = "";
        StringBuilder sb = new StringBuilder();
        Button btnSalvar = (Button)sender;

        Panel panel = (Panel)PanelArvore.FindControl("Panel" + btnSalvar.CommandArgument);
        Panel PanelEdit = (Panel)PanelArvore.FindControl("PanelEdit" + btnSalvar.CommandArgument);
       
        t03_projetoAction t03a = new t03_projetoAction();
        int cd_projeto = Convert.ToInt32(pb.Session("cd_projeto"));
        switch (btnSalvar.CommandArgument)
        {
            case "ds_publico":
                result = t03a.UpdateCampoTextDB("ds_publico", txtds_publico.Text, cd_projeto);
                break;
            case "ds_objetivo":
                result = t03a.UpdateCampoTextDB("ds_objetivo", txtds_objetivo.Text, cd_projeto);
                break;
            case "dt_inicio":
                sb.Append("Não foi possível alterar o período do projeto:<br>");
                //validação dedados ( marco crítico, ações e resultados)                          
                t09_marcoAction t09a = new t09_marcoAction();
                marco = t09a.RetrieveDatas(txtdt_inicio.Text, txtdt_fim.Text, cd_projeto);
                if (marco != 0)
                {
                    erro = true;
                    sb.Append("&nbsp;- Marco Crítico: data prevista fora do período selecionado, antes de continuar, altere a data prevista.<br>");
                }
                //validação de ações
                t08_acaoAction t08a = new t08_acaoAction();
                acoes = t08a.RetrieveDatas(txtdt_inicio.Text, txtdt_fim.Text, cd_projeto);
                if (acoes != 0)
                {
                    erro = true;
                    sb.Append("&nbsp;- Ação: datas fora do período selecionado, antes de continuar, altere as datas de início e término.<br>");
                }
                //validação de resultados                
                t12_resultadoAction t12a = new t12_resultadoAction();
                t12_resultado t12 = new t12_resultado();
                t13_vlresultadoAction t13a = new t13_vlresultadoAction();
                int dt_begin = Convert.ToDateTime(txtdt_inicio.Text).Year;
                int dt_end = Convert.ToDateTime(txtdt_fim.Text).Year;

                DataTable dt = t13a.ListSomaValores(dt_begin, dt_end, cd_projeto);
                foreach (DataRow dr in dt.Rows)
                {                    
                    if ((dr["valor"] != DBNull.Value) && (Convert.ToInt32(dr["valor"]) != 0))
                    {
                        erro = true;
                        resultados = 1;
                        sb.Append("&nbsp;- Resultados: dados em anos fora do período selecionado, antes de continuar, apague os dados dos anos.");
                        //sb.Append(pb.Message("Não é possivel atualizar data. Dados do Resultado com data fora do range do projeto.<br>", "erro"));
                    }                
                    break;
                }                

                //validação geral
                if ((marco == 0) && (acoes == 0) && (resultados == 0))
                {
                    result = t03a.UpdatePrazoDB(txtdt_inicio.Text, txtdt_fim.Text, cd_projeto);
                }
                break;
            case "ds_situacao":
                t05_situacao t05 = new t05_situacao();
                t05.ds_situacao = txtds_situacao.Text;
                t05.t03_cd_projeto = cd_projeto;

                t05_situacaoAction t05m = new t05_situacaoAction();
                result = t05m.InsertDB(t05);

                break;
            case "t21_cd_fase":
                t22_faseprojeto t22 = new t22_faseprojeto();
                t22.t21_cd_fase = Convert.ToInt32(ddlt21_cd_fase.SelectedValue);
                t22.t03_cd_projeto = cd_projeto;
                result = new t22_faseprojetoAction().InsertDB(t22);
                break;
        }

        if (result > 0)
        {
            t03a.UpdateAtualizaDB(cd_projeto);
            msg = pb.Message("Alteração realizada com sucesso!", "ok");
            ArvoreBind(btnSalvar.CommandArgument);
            if (PanelEdit != null)
            {
                PanelEdit.Visible = false;
            }
            if (panel != null)
            {
                panel.Visible = true;
            }
        }
        else
        {
            if (erro)
            {
                msg = pb.Message(sb.ToString(), "erro");
            }
            else
            {
                msg = pb.Message(pb.msgerro, "erro");
            }
        }

        lblMsg.Visible = true;
        lblMsg.Text = msg;

    }

    protected void btnCancelar_Click(object sender, System.EventArgs e)
    {
        Button btnCancelar = (Button)sender;
        Panel panel = (Panel)PanelArvore.FindControl("Panel" + btnCancelar.CommandArgument);
        Panel PanelEdit = (Panel)PanelArvore.FindControl("PanelEdit" + btnCancelar.CommandArgument);
        if (PanelEdit != null)
        {
            PanelEdit.Visible = false;
        }
        if (panel != null)
        {
            panel.Visible = true;
        }
        //ArvoreBind();
    }

    protected void dlResultado_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataRowView drv = ((DataRowView)e.Item.DataItem);

        Label lbl;
        lbl = (Label)e.Item.FindControl("lblds_resultado");
        if (lbl != null) lbl.Text = drv["ds_resultado"].ToString();

    }

    protected void btnDetalhamento_Click(object sender, EventArgs e)
    {
        DateTime dt_inicio = DateTime.Now;

        dt_inicio = Convert.ToDateTime(pb.Session("dt_inicio", DateTime.Now));

        if (dt_inicio.Year > 1)
        {
            Response.Redirect("Detalhamento.aspx");
        }
        else
        {
            lblMsg.Text = pb.Message("Desculpe, mas é necessário definir as datas de início é término do projeto antes de continuar!", "erro");
            lblMsg.Visible = true;
        }
    }

    private string GetXmlValue(t03_projeto t03)
    {
        int i;
        decimal fisico = 0;
        decimal financeiro = 0;
        double difdias = 0;
        double difhoje = 0;
        double crono = 0;

        /*
         * TEMPO
         */
        difdias = t03.dt_fim.Subtract(t03.dt_inicio).Days;
        difhoje = t03.dt_fim.Subtract(DateTime.Now).Days;

        if (DateTime.Now.Date > t03.dt_inicio.Date)
        {
            crono = (((difhoje / difdias) * 100) - 100) * -1;
            if (crono < 0)
            {
                crono = 0;
            }
            else if (crono > 100)
            {
                crono = 100;
            }
        }
        else
        {
            crono = 0;
        }

        /*
         * FÍSICO
         */
        i = 0;
        foreach (t08_acao t08 in new t08_acaoAction().ListObjTodos(t03.t03_cd_projeto))
        {
            foreach (t10_produto t10 in new t10_produtoAction().ListObjTodos(t08.t08_cd_acao))
            {
                i++;
                decimal tprev = 0;
                decimal treal = 0;
                foreach (t17_vlproduto t17 in t10.t17)
                {
                    tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                         t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11 + t17.vl_p12;
                    treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                         t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11 + t17.vl_r12;
                }
                if (tprev > 0)
                {
                    fisico += ((treal / tprev) * 100);
                }
                else
                {
                    fisico += 0;
                }
                
            }
        }
        if (i > 0) fisico = fisico / i;

        if (fisico > 100)
        {
            fisico = 100;
        }
        else if (fisico < 0)
        {
            fisico = 0;
        }

        foreach (t18b_vlfinanceiro t18 in new t18b_vlfinanceiroAction().ListObjProjetoTotal(t03.t03_cd_projeto))
        {            
            if (t18.vl_liquidado1 > 0)
            {
                financeiro = (t18.vl_liquidado1 / t18.vl_planejado1) * 100;
            }
            break;
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<graph chartRightMargin='30' numberSuffix='%25' " +
            "yAxisMaxValue='100'  showAlternateVGridColor='1' " +
            "alternateVGridAlpha='10' alternateVGridColor='AFD8F8' " +
            " numDivLines='4' decimalPrecision='0' canvasBorderThickness='1' " +
            "canvasBorderColor='114B78' baseFontColor='114B78' " +
            "hoverCapBorderColor='114B78' hoverCapBgColor='E7EFF6'>");
        sb.Append("<set name='Tempo' value='" + crono.ToString().Replace(",", ".") + "' color='AFD8F8' alpha='70'/> ");
        if (fisico > 0)
        {
            sb.Append("<set name='Físico' value='" + fisico.ToString().Replace(",", ".") + "' color='AFD8F8' alpha='70'/> ");
        }
        else
        {
            sb.Append("<set name='Físico' color='AFD8F8' alpha='70'/>");
        }
        if (ShowFinanceiro())
        {
            if (financeiro > 0)
            {
                //metodo para exibir financeiro

                sb.Append("<set name='Financeiro' value='" + financeiro.ToString().Replace(",", ".") + "' color='AFD8F8' alpha='70'/> ");

            }
            else
            {
                sb.Append("<set name='Financeiro' color='AFD8F8' alpha='70'/>");
            }

        }
        sb.Append("</graph>");
        return sb.ToString();
    }

    private void FinanceiroBind()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("<table width=\"100%\" rules=\"all\" border=\"1\" ");
        sb.AppendLine(" style=\"border: 1px solid #ccc; border-collapse: collapse;\" ");
        sb.AppendLine(" cellpadding=\"5\" cellspacing=\"0\">");
        sb.AppendLine("<tr style=\"background:#eaf6d8;text-align:center;font-weight:bold;\">");
        sb.AppendLine("<td>Ano</td>");
        sb.AppendLine("<td>Planejado</td>");
        sb.AppendLine("<td>Liquidado</td>");
        sb.AppendLine("</tr>");

        t18b_vlfinanceiro t18 = new t18b_vlfinanceiro();
        t18b_vlfinanceiro t18t = new t18b_vlfinanceiro(); //total geral
        DataTable dtb = new t11_financeiroAction().ListProjetoAnoFin(Convert.ToInt32(pb.Session("cd_projeto")));
        if (dtb.Rows.Count > 0)
        {
         //   ViewState["financeiro"] = 1;
            foreach (DataRow dr in dtb.Rows)
            {
                if (Convert.ToInt32(dr["nu_ano"]) > 1)
                {
                    t18.nu_ano = Convert.ToInt32(dr["nu_ano"]);
                    t18.vl_planejado1 = Convert.ToDecimal(dr["vl_planejado"]);
                    t18.vl_liquidado1 = Convert.ToDecimal(dr["vl_liquidado"]);

                    t18t.vl_planejado1 += t18.vl_planejado1;
                    t18t.vl_liquidado1 += t18.vl_liquidado1;


                    sb.AppendLine("<tr style=\"text-align:right\">");
                    sb.AppendLine("<td>" + t18.nu_ano + "</td>");
                    sb.AppendLine("<td>" + t18.vl_planejado1.ToString("N0") + "</td>");
                    sb.AppendLine("<td>" + t18.vl_liquidado1.ToString("N0") + "</td>");
                    sb.AppendLine("</tr>");
                }

            }

            sb.AppendLine("<tr style=\"background:#eaf6d8;font-weight:bold;text-align:right\">");
            sb.AppendLine("<td>Total</td>");
            sb.AppendLine("<td>" + t18t.vl_planejado1.ToString("N0") + "</td>");
            sb.AppendLine("<td>" + t18t.vl_liquidado1.ToString("N0") + "</td>");
            sb.AppendLine("</tr>");

            sb.AppendLine("</table>");

            ltrFinanceiro.Text = sb.ToString().Replace(",00", "");
        }
        else
        {            
            PanelInvestimentos.Visible = false;
        }
    }

    private void ShowSituacao()
    {
        if (Convert.ToBoolean(Convert.ToInt32((pb.Session("fl_visitante")))))
        {            
            int gerente = 0;
            int linhadecisoria = 0;
            int acao = 0;
            //consulta se é gerente 
            t02_usuarioAction t02a = new t02_usuarioAction();
            gerente = t02a.ListProjetoGerente(Convert.ToInt32(pb.Session("cd_projeto")), Session["cd_usuario"].ToString()).Rows.Count;
            linhadecisoria = t02a.ListFinanceiroLinhaGerencial(Session["cd_usuario"].ToString(), Convert.ToInt32(pb.Session("cd_projeto"))).Rows.Count;
            acao = t02a.ListRestricaoFinanceiroAcao(Session["cd_usuario"].ToString(), Convert.ToInt32(pb.Session("cd_projeto"))).Rows.Count;
            if ((gerente > 0) || (linhadecisoria > 0) || (acao > 0))
            {
                pnlShowSituacao.Visible = true;
            }
            else
            {
                pnlShowSituacao.Visible = false;
            }
        }
    }  

    private bool ShowFinanceiro()
    {
        bool resp = false;
        //chama o metodo q mostra financeiro
        if ((Session["cd_usuario"] != null) && (Session["cd_area"] != null))
        {
            bool result = false;
            result = pb.RestricaoFinanceiro(Session["cd_usuario"].ToString(), Convert.ToInt32(Session["cd_area"]));
            if (result)
            {                
                //exibe financeiro
                resp = true;
                if (Convert.ToInt32(ViewState["financeiro"]) > 0)
                {
                    PanelInvestimentos.Visible = true;
                }
            }
            else
            {
                //oculta links da arvore por perfil do projeto
                HideLinksArvore();
                PanelInvestimentos.Visible = false;
            }
        }
        return resp;
    }

    public void HideLinksArvore()
    {
        //botao detalhamento
        DivBtnDetalhamento.Visible = false;
        //hyperlink resultados
        linkResultados.Visible = false;
        //hyperlink parceiros 
        linkParceiros.Enabled = false;
        //hyperlink agenda, noticias, fotos
        linkAgenda.Enabled = false;
        linkNoticias.Enabled = false;
        //linkFoto.Enabled = false;
        //hyperlink cronogramas, outros
        linkCronograma.Enabled = false;
        linkOutros.Enabled = false;
    }

    public void GeraGaleriaFotos()
    {
        var cd_projeto = Convert.ToInt32(pb.Session("cd_projeto"));
        var fotosCount = new t14_documentoAction().ListFoto(cd_projeto).Rows.Count;

        if (fotosCount < 4)
            hlkExibirGaleria.Visible = false;        

        rptFotos.DataSource = new t14_documentoAction().ListFoto(cd_projeto);
        rptFotos.DataBind();

        if (fl_troca_arq)
        {
            IconsValuesBind(cd_projeto);

            rptFotos.DataSource = new t14_documentoAction().ListFoto(cd_projeto);
            rptFotos.DataBind();
        }
    }

    protected void rptFotos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string NM_DOCUMENTO = DataBinder.Eval(e.Item.DataItem, "NM_DOCUMENTO").ToString();
            var NM_ARQUIVO = DataBinder.Eval(e.Item.DataItem, "NM_ARQUIVO").ToString();

            int ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "T14_CD_DOCUMENTO").ToString());

            HtmlGenericControl liItem = (HtmlGenericControl)e.Item.FindControl("liitem");

            if (e.Item.ItemIndex > 2)
            {
                liItem.Attributes.Remove("style");
                liItem.Attributes.Add("style", "display: none;");
            }               

            Label lblNm_DOCUMENTO = (Label)e.Item.FindControl("lblNm_DOCUMENTO");
            if (NM_DOCUMENTO.ToString().Length > 77)
            {
                lblNm_DOCUMENTO.Text = NM_DOCUMENTO.Substring(0, 77);
            }
            else
                lblNm_DOCUMENTO.Text = NM_DOCUMENTO.ToString();

            HtmlAnchor a = (HtmlAnchor)e.Item.FindControl("prettyPhoto");         
            HtmlImage img = (HtmlImage)e.Item.FindControl("idImg");            

            string arquivo = Server.MapPath(".") + @"\Documentos\" + NM_ARQUIVO;
            if (!System.IO.File.Exists(arquivo))
            {
                a.Attributes.Add("href", "Documentos/img.jpg");
                img.Attributes.Add("src", "Documentos/img.jpg");
            }
            else
            {
                // método que obtém a extensão 
                string extension = System.IO.Path.GetExtension(arquivo);

                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".gif") || extension.ToLower().Equals(".png"))
                {
                    a.Attributes.Add("href", "Documentos/" + NM_ARQUIVO);
                    img.Attributes.Add("src", "Documentos/" + NM_ARQUIVO);
                }
                else
                {
                    t14_documento t14 = new t14_documento();                    
                    t14.t14_cd_documento = (int)DataBinder.Eval(e.Item.DataItem, "t14_cd_documento");

                    t14_documentoAction t14DAO = new t14_documentoAction();

                    if (extension.ToLower().Equals(".xls") || extension.ToLower().Equals(".xlsx"))
                    {
                        fl_troca_arq = true;
                        t14DAO.UpdateToCronograma(t14);
                    }
                    else
                    {
                        fl_troca_arq = true;
                        t14DAO.UpdateToOutros(t14);
                    }
                }
            }

            img.Attributes.Add("width", "150px");
            img.Attributes.Add("height", "150px");
        }
    }
}
