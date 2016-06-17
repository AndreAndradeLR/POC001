using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CustomControls_ucProduto : System.Web.UI.UserControl, ICrud
{
    pageBase pb = new pageBase();

    public void Page_Load(object sender, EventArgs e)
    {
        FormBind();
		    GridBind();
        if (!IsPostBack)
        {
         
            ViewState["cod"] = "0";

            //verifica se o usuario é gerente para cadastrar novo
            if (pb.fl_gerente())
            {
                PanelbtnNovo.Visible = true;
                GridView1.Columns[0].Visible = true;
                GridView1.Columns[1].Visible = true;
            }
            else
            {
                PanelbtnNovo.Visible = false;
                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].Visible = false;
            }

            //verifica se é gerente e coordenador da ação
            //if (pb.fl_gerente() || pb.fl_respacao())
            //{
                
            //}
            //else
            //{
                
            //}

            t21_fase t21 = new t21_fase();
            t21 = (new t21_faseAction().Retrieve(
                new t22_faseprojetoAction().Retrieve(Convert.ToInt32(pb.Session("cd_projeto"))).t21_cd_fase));

            if (t21.t21_cd_fase == 2)
            {
                PanelbtnNovo.Visible = false;
                GridView1.Columns[1].Visible = false;
                txtds_produto.Enabled = false;
                txtnm_medida.Enabled = false;
            }
        }
    }

    #region ICrud Members

    public void SugestaoOrdem()
    {
        int numerico = 0;
        t10_produto t10 = new t10_produtoAction().RetrieveMax(Convert.ToInt32(pb.Session("cd_acao")));
        numerico = (t10.nu_ordem + 1);
        txtnu_ordem.Text = numerico.ToString();
    }

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

    public void ExibirForm()
    {
        SugestaoOrdem();
        PanelForm.Visible = true;
        PanelGrid.Visible = false;
    }

    public void FormBind()
    {
        t08_acao t08 = new t08_acaoAction().Retrieve(Convert.ToInt32(pb.Session("cd_acao")));
        ucPrevisto.inicio = t08.dt_inicio.Year;
        ucPrevisto.fim = t08.dt_fim.Year;
        ucPrevisto.editar = true;

        ucRealizado.inicio = t08.dt_inicio.Year;
        ucRealizado.fim = t08.dt_fim.Year;
        ucRealizado.editar = true;

    }

    public void GridBind()
    {
        GridView1.DataSource = new t10_produtoAction().ListTodos(Convert.ToInt32(pb.Session("cd_acao")));
        GridView1.DataBind();
    }

    public void GridView1_PreRender(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.CommandName != "Sort")
            {
                ViewState["cod"] = Int32.Parse(gv.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString());

                switch (e.CommandName)
                {
                    case "Editar":
                        lblHeader.Text = "Alteração";
                        ExibirForm();
                        t21_fase t21 = new t21_fase();
                        t21 = (new t21_faseAction().Retrieve(
                            new t22_faseprojetoAction().Retrieve(Convert.ToInt32(pb.Session("cd_projeto"))).t21_cd_fase));
                        if (t21.t21_cd_fase == 2)
                        {
                            ucPrevisto.PrevistoBloqueado = true;
                            //ucPrevisto.TableClear();
                        }
                        Retrieve();
                        trReal.Visible = true;

                        break;
                    case "Deletar":
                        new t10_produtoAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        GridBind();
                        ViewState["cod"] = "0";
                        //atualiza a data da ação 
                        new t08_acaoAction().UpdateAlteracao(Convert.ToInt32(pb.Session("cd_acao")));
                        break;
                    case "Selecionar":
                        Session["cd_produto"] = Convert.ToInt32(ViewState["cod"]);
                        Retrieve();
                        Response.Redirect("~/ProdutoDetalhes.aspx");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RowCommand: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }

    }

    public void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = ((DataRowView)e.Row.DataItem);
                decimal totvl_p = 0;
                decimal totvl_r = 0;
                int casadecimal = 0;
                if (e.Row.Cells[6] != null)
                {
                    char c = char.Parse(",");
                    foreach (t17_vlproduto t17 in new t10_produtoAction().Retrieve(
                        Convert.ToInt32(drv["t10_cd_produto"])).t17)
                    {
                        totvl_p += (t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 +
                    t17.vl_p6 + t17.vl_p7 + t17.vl_p8 + t17.vl_p9 +
                    t17.vl_p10 + t17.vl_p11 + t17.vl_p12);



                        totvl_r += (t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 +
                    t17.vl_r6 + t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 +
                    t17.vl_r11 + t17.vl_r12);


                    }
                    casadecimal = Int32.Parse(totvl_p.ToString("N2").Split(c)[1].ToString());
                    if (casadecimal > 0)
                        e.Row.Cells[5].Text = (totvl_p).ToString("N2");
                    else
                        e.Row.Cells[5].Text = (totvl_p).ToString("N0");

                    casadecimal = Int32.Parse(totvl_r.ToString("N2").Split(c)[1].ToString());
                    if (casadecimal > 0)
                        e.Row.Cells[6].Text = (totvl_r).ToString("N2");
                    else
                        e.Row.Cells[6].Text = (totvl_r).ToString("N0");
                }

                //Adicionar mensagem de alerta antes da exclusão
                ImageButton btn = (ImageButton)e.Row.Cells[1].Controls[0];
                if (btn != null)
                {
                    btn.Attributes.Add("OnClick", "if (confirm('Tem certeza que deseja excluir?') == false) return false;");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RowDataBound: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }

    public void LimparForm()
    {
        t08_acao t08 = new t08_acaoAction().Retrieve(Convert.ToInt32(pb.Session("cd_acao")));
        txtds_produto.Text = "";
        txtnm_medida.Text = "";
        txtnu_ordem.Text = "";
        ucPrevisto.TableClear();
        ucRealizado.TableClear();
    }

    public void OcultarForm()
    {
        LimparForm();
        PanelForm.Visible = false;
        PanelGrid.Visible = true;
        ViewState["cod"] = 0;

    }

    public void Retrieve()
    {
        LimparForm();
        t10_produto t10 = new t10_produtoAction().Retrieve(Convert.ToInt32(ViewState["cod"]));
        txtds_produto.Text = t10.ds_produto;
        txtnm_medida.Text = t10.nm_medida;
        txtnu_ordem.Text = t10.nu_ordem.ToString();

        foreach (t17_vlproduto t17 in t10.t17)
        {
            TextBox txtvl_p1 = (TextBox)ucPrevisto.FindControl("txtvl_p1" + t17.nu_ano);
            TextBox txtvl_p2 = (TextBox)ucPrevisto.FindControl("txtvl_p2" + t17.nu_ano);
            TextBox txtvl_p3 = (TextBox)ucPrevisto.FindControl("txtvl_p3" + t17.nu_ano);
            TextBox txtvl_p4 = (TextBox)ucPrevisto.FindControl("txtvl_p4" + t17.nu_ano);
            TextBox txtvl_p5 = (TextBox)ucPrevisto.FindControl("txtvl_p5" + t17.nu_ano);
            TextBox txtvl_p6 = (TextBox)ucPrevisto.FindControl("txtvl_p6" + t17.nu_ano);
            TextBox txtvl_p7 = (TextBox)ucPrevisto.FindControl("txtvl_p7" + t17.nu_ano);
            TextBox txtvl_p8 = (TextBox)ucPrevisto.FindControl("txtvl_p8" + t17.nu_ano);
            TextBox txtvl_p9 = (TextBox)ucPrevisto.FindControl("txtvl_p9" + t17.nu_ano);
            TextBox txtvl_p10 = (TextBox)ucPrevisto.FindControl("txtvl_p10" + t17.nu_ano);
            TextBox txtvl_p11 = (TextBox)ucPrevisto.FindControl("txtvl_p11" + t17.nu_ano);
            TextBox txtvl_p12 = (TextBox)ucPrevisto.FindControl("txtvl_p12" + t17.nu_ano);
            if (txtvl_p1 != null)
            {
                char c = char.Parse(",");
                int casadecimal = 0;
                casadecimal = Int32.Parse(t17.vl_p1.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p1.Text = t17.vl_p1.ToString("N2");
                else
                    txtvl_p1.Text = t17.vl_p1.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p2.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p2.Text = t17.vl_p2.ToString("N2");
                else
                    txtvl_p2.Text = t17.vl_p2.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p3.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p3.Text = t17.vl_p3.ToString("N2");
                else
                    txtvl_p3.Text = t17.vl_p3.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p4.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p4.Text = t17.vl_p4.ToString("N2");
                else
                    txtvl_p4.Text = t17.vl_p4.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p5.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p5.Text = t17.vl_p5.ToString("N2");
                else
                    txtvl_p5.Text = t17.vl_p5.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p6.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p6.Text = t17.vl_p6.ToString("N2");
                else
                    txtvl_p6.Text = t17.vl_p6.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p7.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p7.Text = t17.vl_p7.ToString("N2");
                else
                    txtvl_p7.Text = t17.vl_p7.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p8.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p8.Text = t17.vl_p8.ToString("N2");
                else
                    txtvl_p8.Text = t17.vl_p8.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p9.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p9.Text = t17.vl_p9.ToString("N2");
                else
                    txtvl_p9.Text = t17.vl_p9.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p10.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p10.Text = t17.vl_p10.ToString("N2");
                else
                    txtvl_p10.Text = t17.vl_p10.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p11.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p11.Text = t17.vl_p11.ToString("N2");
                else
                    txtvl_p11.Text = t17.vl_p11.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p12.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p12.Text = t17.vl_p12.ToString("N2");
                else
                    txtvl_p12.Text = t17.vl_p12.ToString("N0");

            }            

            TextBox txtvl_r1 = (TextBox)ucRealizado.FindControl("txtvl_r1" + t17.nu_ano);
            TextBox txtvl_r2 = (TextBox)ucRealizado.FindControl("txtvl_r2" + t17.nu_ano);
            TextBox txtvl_r3 = (TextBox)ucRealizado.FindControl("txtvl_r3" + t17.nu_ano);
            TextBox txtvl_r4 = (TextBox)ucRealizado.FindControl("txtvl_r4" + t17.nu_ano);
            TextBox txtvl_r5 = (TextBox)ucRealizado.FindControl("txtvl_r5" + t17.nu_ano);
            TextBox txtvl_r6 = (TextBox)ucRealizado.FindControl("txtvl_r6" + t17.nu_ano);
            TextBox txtvl_r7 = (TextBox)ucRealizado.FindControl("txtvl_r7" + t17.nu_ano);
            TextBox txtvl_r8 = (TextBox)ucRealizado.FindControl("txtvl_r8" + t17.nu_ano);
            TextBox txtvl_r9 = (TextBox)ucRealizado.FindControl("txtvl_r9" + t17.nu_ano);
            TextBox txtvl_r10 = (TextBox)ucRealizado.FindControl("txtvl_r10" + t17.nu_ano);
            TextBox txtvl_r11 = (TextBox)ucRealizado.FindControl("txtvl_r11" + t17.nu_ano);
            TextBox txtvl_r12 = (TextBox)ucRealizado.FindControl("txtvl_r12" + t17.nu_ano);

            if (txtvl_r1 != null)
            {
                char c = char.Parse(",");
                int casadecimal = 0;
                casadecimal = Int32.Parse(t17.vl_r1.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r1.Text = t17.vl_r1.ToString("N2");
                else
                    txtvl_r1.Text = t17.vl_r1.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r2.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r2.Text = t17.vl_r2.ToString("N2");
                else
                    txtvl_r2.Text = t17.vl_r2.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r3.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r3.Text = t17.vl_r3.ToString("N2");
                else
                    txtvl_r3.Text = t17.vl_r3.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r4.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r4.Text = t17.vl_r4.ToString("N2");
                else
                    txtvl_r4.Text = t17.vl_r4.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r5.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r5.Text = t17.vl_r5.ToString("N2");
                else
                    txtvl_r5.Text = t17.vl_r5.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r6.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r6.Text = t17.vl_r6.ToString("N2");
                else
                    txtvl_r6.Text = t17.vl_r6.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r7.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r7.Text = t17.vl_r7.ToString("N2");
                else
                    txtvl_r7.Text = t17.vl_r7.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r8.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r8.Text = t17.vl_r8.ToString("N2");
                else
                    txtvl_r8.Text = t17.vl_r8.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r9.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r9.Text = t17.vl_r9.ToString("N2");
                else
                    txtvl_r9.Text = t17.vl_r9.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r10.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r10.Text = t17.vl_r10.ToString("N2");
                else
                    txtvl_r10.Text = t17.vl_r10.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r11.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r11.Text = t17.vl_r11.ToString("N2");
                else
                    txtvl_r11.Text = t17.vl_r11.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r12.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r12.Text = t17.vl_r12.ToString("N2");
                else
                    txtvl_r12.Text = t17.vl_r12.ToString("N0");
            }
        }
    }

    public void btnCancelar_Click(object sender, EventArgs e)
    {
        OcultarForm();
    }

    public void btnNovo_Click(object sender, EventArgs e)
    {
        ViewState["cod"] = "0";
        ExibirForm();
        lblHeader.Text = "Cadastrar Novo";
        trReal.Visible = false;
        txtds_produto.Enabled = true;
        txtnm_medida.Enabled = true;
    }

    public void btnSalvar_Click(object sender, EventArgs e)
    {
        //script que verifica se conexão caiu antes de executar ação
        if (pb.Session("cd_usuario") != "0")
        {
            int result = 0;
            string msg = "";
            try
            {
                t10_produto t10 = new t10_produto();
                t10_produtoAction t10a = new t10_produtoAction();

                t10.t10_cd_produto = Convert.ToInt32(ViewState["cod"]);
                t10.t08_cd_acao = Convert.ToInt32(pb.Session("cd_acao"));
                t10.ds_produto = txtds_produto.Text;
                t10.nm_medida = txtnm_medida.Text;
                t10.nu_ordem = Convert.ToInt32(txtnu_ordem.Text);

                if (t10.t10_cd_produto > 0)
                {
                    result = t10a.UpdateDB(t10);
                }
                else
                {
                    result = t10a.InsertDB(t10);
                    t10.t10_cd_produto = new t10_produtoAction().RetrieveIDENTITY(t10);
                }

                if (result > 0)
                {
                    //atualiza a data da ação
                    new t08_acaoAction().UpdateAlteracao(Convert.ToInt32(pb.Session("cd_acao")));

                    t08_acao t08 = new t08_acaoAction().Retrieve(Convert.ToInt32(pb.Session("cd_acao")));

                    new t17_vlprodutoAction().DeleteDB(t10.t10_cd_produto);

                    for (int i = t08.dt_inicio.Year; i <= t08.dt_fim.Year; i++)
                    {
                        t17_vlproduto t17 = new t17_vlproduto();
                        t17.t10_cd_produto = t10.t10_cd_produto;
                        t17.nu_ano = i;

                        TextBox txtvl_p1 = (TextBox)ucPrevisto.FindControl("txtvl_p1" + i.ToString());
                        TextBox txtvl_p2 = (TextBox)ucPrevisto.FindControl("txtvl_p2" + i.ToString());
                        TextBox txtvl_p3 = (TextBox)ucPrevisto.FindControl("txtvl_p3" + i.ToString());
                        TextBox txtvl_p4 = (TextBox)ucPrevisto.FindControl("txtvl_p4" + i.ToString());
                        TextBox txtvl_p5 = (TextBox)ucPrevisto.FindControl("txtvl_p5" + i.ToString());
                        TextBox txtvl_p6 = (TextBox)ucPrevisto.FindControl("txtvl_p6" + i.ToString());
                        TextBox txtvl_p7 = (TextBox)ucPrevisto.FindControl("txtvl_p7" + i.ToString());
                        TextBox txtvl_p8 = (TextBox)ucPrevisto.FindControl("txtvl_p8" + i.ToString());
                        TextBox txtvl_p9 = (TextBox)ucPrevisto.FindControl("txtvl_p9" + i.ToString());
                        TextBox txtvl_p10 = (TextBox)ucPrevisto.FindControl("txtvl_p10" + i.ToString());
                        TextBox txtvl_p11 = (TextBox)ucPrevisto.FindControl("txtvl_p11" + i.ToString());
                        TextBox txtvl_p12 = (TextBox)ucPrevisto.FindControl("txtvl_p12" + i.ToString());

                        if (txtvl_p1 != null)
                        {
                            if (txtvl_p1.Text == "") txtvl_p1.Text = "0";
                            if (txtvl_p2.Text == "") txtvl_p2.Text = "0";
                            if (txtvl_p3.Text == "") txtvl_p3.Text = "0";
                            if (txtvl_p4.Text == "") txtvl_p4.Text = "0";
                            if (txtvl_p5.Text == "") txtvl_p5.Text = "0";
                            if (txtvl_p6.Text == "") txtvl_p6.Text = "0";
                            if (txtvl_p7.Text == "") txtvl_p7.Text = "0";
                            if (txtvl_p8.Text == "") txtvl_p8.Text = "0";
                            if (txtvl_p9.Text == "") txtvl_p9.Text = "0";
                            if (txtvl_p10.Text == "") txtvl_p10.Text = "0";
                            if (txtvl_p11.Text == "") txtvl_p11.Text = "0";
                            if (txtvl_p12.Text == "") txtvl_p12.Text = "0";

                            t17.vl_p1 = Convert.ToDecimal(txtvl_p1.Text);
                            t17.vl_p2 = Convert.ToDecimal(txtvl_p2.Text);
                            t17.vl_p3 = Convert.ToDecimal(txtvl_p3.Text);
                            t17.vl_p4 = Convert.ToDecimal(txtvl_p4.Text);
                            t17.vl_p5 = Convert.ToDecimal(txtvl_p5.Text);
                            t17.vl_p6 = Convert.ToDecimal(txtvl_p6.Text);
                            t17.vl_p7 = Convert.ToDecimal(txtvl_p7.Text);
                            t17.vl_p8 = Convert.ToDecimal(txtvl_p8.Text);
                            t17.vl_p9 = Convert.ToDecimal(txtvl_p9.Text);
                            t17.vl_p10 = Convert.ToDecimal(txtvl_p10.Text);
                            t17.vl_p11 = Convert.ToDecimal(txtvl_p11.Text);
                            t17.vl_p12 = Convert.ToDecimal(txtvl_p12.Text);
                        }

                        TextBox txtvl_r1 = (TextBox)ucRealizado.FindControl("txtvl_r1" + i.ToString());
                        TextBox txtvl_r2 = (TextBox)ucRealizado.FindControl("txtvl_r2" + i.ToString());
                        TextBox txtvl_r3 = (TextBox)ucRealizado.FindControl("txtvl_r3" + i.ToString());
                        TextBox txtvl_r4 = (TextBox)ucRealizado.FindControl("txtvl_r4" + i.ToString());
                        TextBox txtvl_r5 = (TextBox)ucRealizado.FindControl("txtvl_r5" + i.ToString());
                        TextBox txtvl_r6 = (TextBox)ucRealizado.FindControl("txtvl_r6" + i.ToString());
                        TextBox txtvl_r7 = (TextBox)ucRealizado.FindControl("txtvl_r7" + i.ToString());
                        TextBox txtvl_r8 = (TextBox)ucRealizado.FindControl("txtvl_r8" + i.ToString());
                        TextBox txtvl_r9 = (TextBox)ucRealizado.FindControl("txtvl_r9" + i.ToString());
                        TextBox txtvl_r10 = (TextBox)ucRealizado.FindControl("txtvl_r10" + i.ToString());
                        TextBox txtvl_r11 = (TextBox)ucRealizado.FindControl("txtvl_r11" + i.ToString());
                        TextBox txtvl_r12 = (TextBox)ucRealizado.FindControl("txtvl_r12" + i.ToString());

                        if (txtvl_r1 != null)
                        {
                            if (txtvl_r1.Text == "") txtvl_r1.Text = "0";
                            if (txtvl_r2.Text == "") txtvl_r2.Text = "0";
                            if (txtvl_r3.Text == "") txtvl_r3.Text = "0";
                            if (txtvl_r4.Text == "") txtvl_r4.Text = "0";
                            if (txtvl_r5.Text == "") txtvl_r5.Text = "0";
                            if (txtvl_r6.Text == "") txtvl_r6.Text = "0";
                            if (txtvl_r7.Text == "") txtvl_r7.Text = "0";
                            if (txtvl_r8.Text == "") txtvl_r8.Text = "0";
                            if (txtvl_r9.Text == "") txtvl_r9.Text = "0";
                            if (txtvl_r10.Text == "") txtvl_r10.Text = "0";
                            if (txtvl_r11.Text == "") txtvl_r11.Text = "0";
                            if (txtvl_r12.Text == "") txtvl_r12.Text = "0";

                            t17.vl_r1 = Convert.ToDecimal(txtvl_r1.Text);
                            t17.vl_r2 = Convert.ToDecimal(txtvl_r2.Text);
                            t17.vl_r3 = Convert.ToDecimal(txtvl_r3.Text);
                            t17.vl_r4 = Convert.ToDecimal(txtvl_r4.Text);
                            t17.vl_r5 = Convert.ToDecimal(txtvl_r5.Text);
                            t17.vl_r6 = Convert.ToDecimal(txtvl_r6.Text);
                            t17.vl_r7 = Convert.ToDecimal(txtvl_r7.Text);
                            t17.vl_r8 = Convert.ToDecimal(txtvl_r8.Text);
                            t17.vl_r9 = Convert.ToDecimal(txtvl_r9.Text);
                            t17.vl_r10 = Convert.ToDecimal(txtvl_r10.Text);
                            t17.vl_r11 = Convert.ToDecimal(txtvl_r11.Text);
                            t17.vl_r12 = Convert.ToDecimal(txtvl_r12.Text);
                        }

                        new t17_vlprodutoAction().InsertDB(t17);
                    }

                    msg = pb.Message("Salvo com sucesso", "ok");
                    OcultarForm();
                    GridBind();
                    ViewState["cod"] = "0";
                }
            }
            catch (Exception ex)
            {
                msg = pb.Message(ex.Message, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;
        }
    }

    #endregion
}
