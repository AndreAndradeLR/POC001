using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Resultados : System.Web.UI.Page, ICrud
{
    private bool _editar;
    public bool Editar
    {
        get { return _editar; }
        set { _editar = value; }
    }

    pageBase pb = new pageBase();

    public void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridBind();
            ViewState["cod"] = "0";

            if (pb.fl_gerente())
            {
                _editar = true;
            }
            if (_editar)
            {
                spanbtnNovo.Visible = true;
                GridView1.Columns[0].Visible = true;
                GridView1.Columns[1].Visible = true;
            }
            else
            {
                spanbtnNovo.Visible = false;
                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].Visible = false;
            }

            t21_fase t21 = new t21_fase();
            t21 = (new t21_faseAction().Retrieve(
                new t22_faseprojetoAction().Retrieve(Convert.ToInt32(pb.Session("cd_projeto"))).t21_cd_fase));

            if (t21.t21_cd_fase == 2)
            {
                spanbtnNovo.Visible = false;
                GridView1.Columns[1].Visible = false;
                txtds_resultado.Enabled = false;
                txtnm_resultado.Enabled = false;
                txtnm_medida.Enabled = false;
                txtnm_respmedicao.Enabled = false;
                txtvl_t0.Enabled = false;
                txtnm_fonte.Enabled = false;
                txtnu_ano.Enabled = false;
                txtnu_ordem.Enabled = false;
                rblfl_acumulado.Enabled = false;

            }
        }
    }


    #region ICrud Members


    public void SugestaoOrdem()
    {
        int numerico = 0;
        t12_resultado t12 = new t12_resultadoAction().RetrieveMax(Convert.ToInt32(pb.Session("cd_projeto")));
        numerico = (t12.nu_ordem + 1);
        txtnu_ordem.Text = numerico.ToString();

        t21_fase t21 = new t21_fase();
            t21 = (new t21_faseAction().Retrieve(
                new t22_faseprojetoAction().Retrieve(Convert.ToInt32(pb.Session("cd_projeto"))).t21_cd_fase));

            if (t21.t21_cd_fase == 2)
            {
                txtnu_ordem.Enabled = false;
            }
    }

    public void ExibirForm()
    {
        SugestaoOrdem();
        this.mdlPopup.Show();
    }

    public void FormBind()
    {
        throw new NotImplementedException();
    }

    public void GridBind()
    {
        GridView1.DataSource = new t12_resultadoAction().ListTodos(
            Convert.ToInt32(pb.Session("cd_projeto")));
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
                        Retrieve();
                        break;
                    case "Deletar":
                        new t12_resultadoAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        GridBind();
                        ViewState["cod"] = "0";
                        break;
                    case "Projeto":
                        t12_resultado t12 = new
                            t12_resultadoAction().Retrieve(
                            Convert.ToInt32(ViewState["cod"]));
                        Session["cd_projeto"] = t12.t03_cd_projeto;
                        Response.Redirect("Arvore.aspx");
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

    private Literal DadosIndicador(t12_resultado t12)
    {
        Literal lit = new Literal();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("<p style=\"margin-bottom:8px\"><b>Descrição: </b>" + t12.ds_resultado + "</p>");
        sb.AppendLine("<p style=\"margin-bottom:8px\"><b>Indicador: </b>" + t12.nm_resultado + "</p>");
        sb.AppendLine("<p style=\"margin-bottom:8px\"><b>Unidade de Medida: </b>" + t12.nm_medida + "</p>");
        sb.AppendLine("<p style=\"margin-bottom:8px\"><b>Responsável pela Medição: </b>" + t12.nm_respmedicao + "</p>");
        sb.AppendLine("<p style=\"margin-bottom:8px\"><b>Fonte: </b>" + t12.nm_fonte + "</p>");
        sb.AppendLine("<p style=\"margin-bottom:8px\"><b>Valor de referência: </b>" + t12.vl_t0.ToString("N") + "</p>");
        sb.AppendLine("<p style=\"margin-bottom:8px\"><b>Ano: </b>" + t12.nu_ano + "</p>");
        lit.Text = sb.ToString();
        return lit;
    }

    private string GetXmlValue(t12_resultado t12)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (!t12.fl_acumulado)
        {
            sb.Append("<graph showValues='0' caption='Valores Anuais' decimalPrecision='2' " +
                "anchorRadius='4' anchorBgAlpha='0' lineThickness='2' " +
                "numberPrefix='' limitsDecimalPrecision='2' divLineDecimalPrecision='2'>");
        }
        else
        {
            sb.Append("<graph caption='Valores Acumulados' showValues='0' " +
                "decimalPrecision='2' anchorRadius='4' anchorBgAlpha='0' " +
                "lineThickness='2' numberPrefix='' limitsDecimalPrecision='2' " +
                "divLineDecimalPrecision='2'>");
        }
        sb.Append("<categories>");

        DateTime dt_inicio = Convert.ToDateTime(pb.Session("dt_inicio", DateTime.Now));
        DateTime dt_fim = Convert.ToDateTime(pb.Session("dt_fim", DateTime.Now));
        for (int i = dt_inicio.Year; i <= dt_fim.Year; i++)
        {
            sb.Append("<category name='" + i + "' />");
        }
        sb.Append("</categories>");
        sb.Append("<dataset seriesName='Previsto' color='66CC66'>");
        for (int i = dt_inicio.Year; i <= dt_fim.Year; i++)
        {
            int num = 0;
            foreach (t13_vlresultado t13 in t12.t13)
            {
                if (t13.nu_ano == i)
                {
                    num ++;
                    if ((t13.nu_ano >= dt_inicio.Year) && (t13.nu_ano <= dt_fim.Year))//somente os anos do projeto
                    {
                        if (t13.vl_previsto > 0)
                        {
                            sb.Append("<set value='" + t13.vl_previsto.ToString().Replace(",", ".") + "' alpha='100' />");
                        }
                        else
                        {
                            sb.Append("<set />");
                        }
                    }
                }
            }
            if (num == 0) {
                sb.Append("<set />");
            }
        }
        
        sb.Append("</dataset>");

        sb.Append("<dataset seriesName='Realizado' color='81BCE9'>");
        for (int i = dt_inicio.Year; i <= dt_fim.Year; i++)
        {
            int encrement = 0;
            foreach (t13_vlresultado t13 in t12.t13)
            {
                if (t13.nu_ano == i)
                {
                    encrement++;
                    if ((t13.nu_ano >= dt_inicio.Year) && (t13.nu_ano <= dt_fim.Year))//somente os anos do projeto
                    {
                        if (t13.vl_realizado > 0)
                        {
                            sb.Append("<set value='" + t13.vl_realizado.ToString().Replace(",", ".") + "' alpha='100' />");
                        }
                        else
                        {
                            sb.Append("<set />");
                        }
                    }
                }
            }
            if (encrement == 0) {
                sb.Append("<set />");
            }
        }
        sb.Append("</dataset>");
        sb.Append("</graph>");
        return sb.ToString();
    }

    private string GetTableValue(t12_resultado t12)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        decimal prev = 0;
        decimal real = 0;
        decimal prevac = 0;
        decimal realac = 0;

        sb.AppendLine("<table cellspacing='0' cellpadding='5' rules='all' border='1' style='color:#333333;border-color:#20669B;border-width:1px;border-style:solid;width:400px;border-collapse:collapse;'>");
        sb.AppendLine("<tr style='color:white;font-weight:bold;text-align:center;background-color:#5D7B9D;'>");
        sb.AppendLine("<td>Ano</td><td>Previsto</td><td>Realizado</td></tr>");

        DateTime dt_inicio = Convert.ToDateTime(pb.Session("dt_inicio", DateTime.Now));
        DateTime dt_fim = Convert.ToDateTime(pb.Session("dt_fim", DateTime.Now));
        for (int i = dt_inicio.Year; i <= dt_fim.Year; i++)
        {
            sb.AppendLine("<tr style='background-color:#F1F5F5;text-align:center;'>");
            sb.AppendLine("<td>");
            sb.AppendLine(i.ToString());//Ano
            sb.AppendLine("</td>");
            int cont = 0;
            foreach (t13_vlresultado t13 in t12.t13)
            {
                if (t13.nu_ano == i)
                {
                    cont++;
                    sb.AppendLine("<td>");
                    prev += t13.vl_previsto;
                    prevac = t13.vl_previsto;
                    //verifica se é maior q zero para exibição
                    if (t13.vl_previsto > 0)
                    {
                        sb.AppendLine(t13.vl_previsto.ToString("N"));
                    }
                    else
                    {
                        sb.AppendLine("0");
                    }
                    sb.AppendLine("</td>");

                    sb.AppendLine("<td>");
                    real += t13.vl_realizado;
                    realac = t13.vl_realizado;
                    //verifica se é maior q zero para exibição
                    if (t13.vl_realizado > 0)
                    {
                        sb.AppendLine(t13.vl_realizado.ToString("N"));
                    }
                    else
                    {
                        sb.AppendLine("0");
                    }
                    sb.AppendLine("</td>");
                }
            }
            if (cont == 0)
            {
                sb.AppendLine("<td>");
                sb.AppendLine("0");
                sb.AppendLine("</td>");
                sb.AppendLine("<td>");
                sb.AppendLine("0");
                sb.AppendLine("</td>");
            }
            sb.AppendLine("</tr>");
        }

        //sb.AppendLine("<tr style='color:white;font-weight:bold;text-align:center;background-color:#5D7B9D;'>");
        //sb.AppendLine("<td>Total:");
        //sb.AppendLine("</td><td>");
        //if (t12.fl_acumulado)
        //{
        //    //Previsto
        //    sb.AppendLine(prevac.ToString());
        //    sb.AppendLine("</td><td>");
        //    //Realizado
        //    sb.AppendLine(realac.ToString());
        //    sb.AppendLine("</td>");
        //}
        //else
        //{
        //    //Previsto
        //    sb.AppendLine(prev.ToString());
        //    sb.AppendLine("</td><td>");
        //    //Realizado
        //    sb.AppendLine(real.ToString());
        //    sb.AppendLine("</td>");
        //}
        //sb.AppendLine("</tr>");

        sb.AppendLine("</table>");
        return sb.ToString().Replace(",00", "");
    }

    private Literal ValoresIndicador(t12_resultado t12)
    {
        Literal lit = new Literal();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string idgrafico = "G" + t12.t12_cd_resultado;
        string idtabela = "T" + t12.t12_cd_resultado;
        sb.AppendLine("<div id=\"" + idgrafico + "\">");
        sb.AppendLine(pb.GetFlash(200, 400, "Charts/FC_2_3_MSColumn3D.swf", this.GetXmlValue(t12), 0, "resultado" + idgrafico));
        sb.AppendLine("<br /><span class=\"button\">");
        sb.AppendLine("<input id=\"btn" + idgrafico + "\" type=\"button\" onclick=\"exibirOcultar('" + idtabela + "', '" + idgrafico + "')\" value=\"Exibir Detalhes\" />");
        sb.AppendLine("</span>");
        sb.AppendLine("</div>");
        sb.AppendLine("<div id=\"" + idtabela + "\" style=\"display:none\">");
        sb.AppendLine(GetTableValue(t12));
        sb.AppendLine("<br /><span class=\"button\">");
        sb.AppendLine("<input id=\"btn" + idtabela + "\" type=\"button\" onclick=\"exibirOcultar('" + idgrafico + "', '" + idtabela + "')\" value=\"Exibir Gráfico\"/>");
        sb.AppendLine("</span>");
        sb.AppendLine("</div>");
        lit.Text = sb.ToString();

        return lit;
    }

    

    public void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = ((DataRowView)e.Row.DataItem);
                
                t12_resultado t12 = new t12_resultadoAction().Retrieve(
                    Convert.ToInt32(drv["t12_cd_resultado"]));
                e.Row.Cells[2].Text = DadosIndicador(t12).Text;
                e.Row.Cells[3].Text = ValoresIndicador(t12).Text;

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
        txtds_resultado.Text = "";
        txtvl_t0.Text = "";
        txtnu_ano.Text = "";
        txtnu_ordem.Text = "";
        txtnm_resultado.Text = "";
        txtnm_medida.Text = "";
        txtnm_respmedicao.Text = "";
        txtnm_fonte.Text = "";
        rblfl_acumulado.ClearSelection();

        DateTime dt_inicio = Convert.ToDateTime(pb.Session("dt_inicio", DateTime.Now));
        DateTime dt_fim = Convert.ToDateTime(pb.Session("dt_fim", DateTime.Now));
        for (int i = dt_inicio.Year; i <= dt_fim.Year; i++)
        {
            TextBox txtPrev = (TextBox)ucAnos.FindControl("txtPrev" + i.ToString());
            if (txtPrev != null) txtPrev.Text = "0";
            TextBox txtReal = (TextBox)ucAnos.FindControl("txtReal" + i.ToString());
            if (txtReal != null) txtReal.Text = "0";

        }
    }

    public void OcultarForm()
    {
        LimparForm();
        this.mdlPopup.Hide();

    }

    public void Retrieve()
    {
        LimparForm();
        t12_resultado t12 = new t12_resultadoAction().Retrieve(Convert.ToInt32(ViewState["cod"]));
        txtds_resultado.Text = t12.ds_resultado;
        txtvl_t0.Text = t12.vl_t0.ToString();
        txtnu_ano.Text = t12.nu_ano.ToString();       
        txtnu_ordem.Text = t12.nu_ordem.ToString();
        txtnm_respmedicao.Text = t12.nm_respmedicao;
        txtnm_fonte.Text = t12.nm_fonte;
        txtnm_resultado.Text = t12.nm_resultado;
        txtnm_medida.Text = t12.nm_medida;
        ListItem li = rblfl_acumulado.Items.FindByValue(t12.fl_acumulado.ToString());
        if (li != null)
            li.Selected = true;

        DateTime dt_inicio = Convert.ToDateTime(pb.Session("dt_inicio", DateTime.Now));
        DateTime dt_fim = Convert.ToDateTime(pb.Session("dt_fim", DateTime.Now));
        for (int i = dt_inicio.Year; i <= dt_fim.Year; i++)
        {
            t12.nu_ano = i;
            t13_vlresultado t13 = new t13_vlresultadoAction().Retrieve(t12);
            TextBox txtPrev = (TextBox)ucAnos.FindControl("txtPrev" + i.ToString());
            TextBox txtReal = (TextBox)ucAnos.FindControl("txtReal" + i.ToString());
            if (txtPrev != null) txtPrev.Text = t13.vl_previsto.ToString("N");
            if (txtReal != null) txtReal.Text = t13.vl_realizado.ToString("N");

            t21_fase t21 = new t21_fase();
            t21 = (new t21_faseAction().Retrieve(
                new t22_faseprojetoAction().Retrieve(Convert.ToInt32(pb.Session("cd_projeto"))).t21_cd_fase));
            if (t21.t21_cd_fase == 2)
            {
                txtPrev.Enabled = false;
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
    }

    public void btnSalvar_Click(object sender, EventArgs e)
    {
        int result = 0;
        string msg = "";
        try
        {
            t12_resultado t12 = new t12_resultado();
            t12_resultadoAction t12a = new t12_resultadoAction();

            t12.t12_cd_resultado = Convert.ToInt32(ViewState["cod"]);
            t12.t03_cd_projeto = Convert.ToInt32(pb.Session("cd_projeto"));
            t12.ds_resultado = txtds_resultado.Text;
            t12.nu_ano = Convert.ToInt32(txtnu_ano.Text);

            if (txtnu_ordem.Text != "")
            {
                t12.nu_ordem = Convert.ToInt32(txtnu_ordem.Text);
            }
            else {                
                t12.nu_ordem = 0;
            }

            t12.nm_resultado = txtnm_resultado.Text;
            t12.nm_medida = txtnm_medida.Text;
            t12.nm_respmedicao = txtnm_respmedicao.Text;
            t12.nm_fonte = txtnm_fonte.Text;

            if (txtvl_t0.Text == "")
            {
                t12.vl_t0 = Convert.ToDecimal(0.ToString("N2"));
            }
            else 
            {
                t12.vl_t0 = Convert.ToDecimal(txtvl_t0.Text);
            }

            t12.fl_acumulado = Convert.ToBoolean(rblfl_acumulado.SelectedValue);

            if (t12.t12_cd_resultado > 0)
            {
                result = t12a.UpdateDB(t12);
                
                //apaga todos os valores do indicador
                new t13_vlresultadoAction().DeleteDB(t12.t12_cd_resultado);
            }
            else
            {
                result = t12a.InsertDB(t12);
                t12.t12_cd_resultado = new t12_resultadoAction().RetrieveIDENTITY(t12);
            }

            DateTime dt_inicio = Convert.ToDateTime(pb.Session("dt_inicio", DateTime.Now));
            DateTime dt_fim = Convert.ToDateTime(pb.Session("dt_fim", DateTime.Now));
            for (int i = dt_inicio.Year; i <= dt_fim.Year; i++)
            {
                TextBox txtPrev = (TextBox)ucAnos.FindControl("txtPrev" + i.ToString());
                TextBox txtReal = (TextBox)ucAnos.FindControl("txtReal" + i.ToString());
                if (txtPrev != null)
                {
                    if (txtPrev.Text == "") txtPrev.Text = "0";
                    if (txtReal.Text == "") txtReal.Text = "0";
                    
                    t13_vlresultado t13 = new t13_vlresultado();
                    t13.t12_cd_resultado = t12.t12_cd_resultado;
                    t13.nu_ano = i;
                    t13.vl_previsto = decimal.Parse(txtPrev.Text);
                    t13.vl_realizado = decimal.Parse(txtReal.Text);
                    new t13_vlresultadoAction().InsertDB(t13);
                }
            }

            if (result > 0)
            {
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

    #endregion
}
