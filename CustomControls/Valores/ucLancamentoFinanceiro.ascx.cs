using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomControls_Valores_ucLancamentoFinanceiro : System.Web.UI.UserControl
{
    pageBase pb = new pageBase();
    private string _prefix;
    private int _inicio;
    private int _fim;
    private bool _editar;
    private bool _PrevistoBloqueado;
    private string _ExibirMeses;

    public string prefix
    {
        get { return _prefix; }
        set { _prefix = value; }
    }

    public int inicio
    {
        get { return _inicio; }
        set { _inicio = value; }
    }

    public int fim
    {
        get { return _fim; }
        set { _fim = value; }
    }

    public bool editar
    {
        get { return _editar; }
        set { _editar = value; }
    }

    public bool PrevistoBloqueado
    {
        get { return _PrevistoBloqueado; }
        set { _PrevistoBloqueado = value; }
    }

    public string ExibirMeses
    {
        get { return _ExibirMeses; }
        set { _ExibirMeses = value; }
    }

    string[] mes = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"};

    protected void Page_Load(object sender, EventArgs e)
    {
        _ExibirMeses = "none";
        TableBind();
        

    }

    private TextBox txtNovo(string prefix, string sufix, string tooltip)
    {
        TextBox txt = new TextBox();
        txt.ID = prefix + sufix;
        txt.Columns = 13;
        txt.Text = "0";
        txt.MaxLength = 18;
        txt.ToolTip = tooltip;
        txt.ReadOnly = !_editar;
        if (prefix == "txtvl_planejado")
        {
            if (_PrevistoBloqueado)
            {
                txt.Attributes.Add("readonly", "readonly");
            }            
        }
        return txt;
    }
    private Label lblNovo(string prefix, string sufix, string tooltip)
    {
        Label lbl = new Label();
        lbl.ID = prefix + sufix;
        lbl.Text = "0";
        //lbl.Text = lbl.ID.ToString();
        lbl.ToolTip = tooltip;
        return lbl;
    }

    public void TableClear()
    {
        Panel1.Controls.Clear();
        TableBind();
    }

    private void TableBind()
    {

        string display_meses = "";
        if (!_editar)
        {
            display_meses = _ExibirMeses;
        }
        else
        {
            display_meses = "";
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("<table width=\"100%\" rules=\"all\" border=\"1\" ");
        sb.AppendLine(" style=\"border: 1px solid #ccc; border-collapse: collapse;\" ");
        sb.AppendLine(" cellpadding=\"5\" cellspacing=\"0\">");
        sb.AppendLine("<tr>");
        sb.AppendLine("<td colspan=\"4\">&nbsp;</td>");
        for (int i = 0; i < 12; i++)
        {
            sb.AppendLine("<td colspan=\"5\" style=\"background:#daf0b8; display:"+ display_meses +"\"><b>" + mes[i] + "</b></td>");
        }
        if (!_editar)
        {
            sb.AppendLine("<td colspan=\"10\" style=\"background:#d9dad7\"><b>Total</b></td>");
        }
        sb.AppendLine("</tr>");
        sb.AppendLine("<tr style=\"background:#eaf6d8\">");
        sb.AppendLine("<td>Ano</td>");
        sb.AppendLine("<td>Restos a Pagar </td>");
        sb.AppendLine("<td>Dotação Orçamentária </td>");
        sb.AppendLine("<td>Assegurado </td>");
        for (int i = 0; i < 12; i++)
        {
            sb.AppendLine("<td style=\"display:" + display_meses + "\">Planejado </td>");
            sb.AppendLine("<td style=\"display:" + display_meses + "\">Revisado </td>");
            sb.AppendLine("<td style=\"display:" + display_meses + "\">Realizado </td>");
            sb.AppendLine("<td style=\"display:" + display_meses + "\">Empenhado </td>");
            sb.AppendLine("<td style=\"display:" + display_meses + "\">Liquidado </td>");            
        }
        if (!_editar)
        {
            sb.AppendLine("<td style=\"background:#ecece9\">Planejado </td>");
            sb.AppendLine("<td style=\"background:#ecece9\">Revisado </td>");
            sb.AppendLine("<td style=\"background:#ecece9\">Realizado </td>");
            sb.AppendLine("<td style=\"background:#ecece9\">Empenhado </td>");
            sb.AppendLine("<td style=\"background:#ecece9\">Liquidado </td>");            
        }
        sb.AppendLine("</tr>");

        for (int ano = _inicio; ano <= _fim; ano++)
        {
            sb.AppendLine("<tr>");
            sb.AppendLine("<td><b>" + ano + "</b></td>");
            sb.AppendLine("<td>");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
            Panel1.Controls.Add(txtNovo("txtvl_restopagar", ano.ToString(), "Restos a Pagar " + ano.ToString()));
            sb = new System.Text.StringBuilder();
            sb.AppendLine("</td>");
            sb.AppendLine("<td>");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
            Panel1.Controls.Add(txtNovo("txtvl_dotorcado", ano.ToString(), "Dotação Orçamentária " + ano.ToString()));
            sb = new System.Text.StringBuilder();
            sb.AppendLine("</td>");
            sb.AppendLine("<td>");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
            Panel1.Controls.Add(txtNovo("txtvl_assegurado", ano.ToString(), "Assegurado " + ano.ToString()));
            sb = new System.Text.StringBuilder();
            sb.AppendLine("</td>");

            for (int i = 0; i < 12; i++)
            {
                sb.AppendLine("<td style=\"display:" + display_meses + "\">");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(txtNovo("txtvl_planejado", (i + 1).ToString() + ano.ToString(), "Planejado " + mes[i] + "/" + ano.ToString()));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");

                sb.AppendLine("<td style=\"display:" + display_meses + "\">");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(txtNovo("txtvl_revisado", (i + 1).ToString() + ano.ToString(), "Revisado " + mes[i] + "/" + ano.ToString()));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");

                sb.AppendLine("<td style=\"display:" + display_meses + "\">");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(txtNovo("txtvl_provisionado", (i + 1).ToString() + ano.ToString(), "Realizado " + mes[i] + "/" + ano.ToString()));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");

                sb.AppendLine("<td style=\"display:" + display_meses + "\">");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(txtNovo("txtvl_empenhado", (i + 1).ToString() + ano.ToString(), "Empenhado " + mes[i] + "/" + ano.ToString()));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");

                sb.AppendLine("<td style=\"display:" + display_meses + "\">");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(txtNovo("txtvl_liquidado", (i + 1).ToString() + ano.ToString(), "Liquidado " + mes[i] + "/" + ano.ToString()));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");                
            }
            if (!_editar)
            {
                sb.AppendLine("<td>");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(lblNovo("lblvl_planejado", ano.ToString(), "Total Planejado " + ano.ToString()));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");

                sb.AppendLine("<td>");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(lblNovo("lblvl_revisado", ano.ToString(), "Total Revisado " + ano.ToString()));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");

                sb.AppendLine("<td>");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(lblNovo("lblvl_provisionado", ano.ToString(), "Total Realizado " + ano.ToString()));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");

                sb.AppendLine("<td>");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(lblNovo("lblvl_empenhado", ano.ToString(), "Total Empenhado " + ano.ToString()));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");

                sb.AppendLine("<td>");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(lblNovo("lblvl_liquidado", ano.ToString(), "Total Liquidado " + ano.ToString()));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");
                
            }
            sb.AppendLine("</tr>");
        }
        if (!_editar)
        {
            sb.AppendLine("<tr style=\"background:#ecece9\">");
            sb.AppendLine("<td ><b>Total</b></td>");
            sb.AppendLine("<td>");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
            Panel1.Controls.Add(lblNovo("lblvl_restopagar", "", "Total Restos a Pagar"));
            sb = new System.Text.StringBuilder();
            sb.AppendLine("</td>");
            sb.AppendLine("<td>");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
            Panel1.Controls.Add(lblNovo("lblvl_dotorcado", "", "Total Dotação Orçamentária"));
            sb = new System.Text.StringBuilder();
            sb.AppendLine("</td>");
            sb.AppendLine("<td>");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
            Panel1.Controls.Add(lblNovo("lblvl_assegurado", "", "Total Assegurado"));
            sb = new System.Text.StringBuilder();
            sb.AppendLine("</td>");
            for (int i = 0; i < 12; i++)
            {
                sb.AppendLine("<td style=\"display:" + display_meses + "\">");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(lblNovo("lblvl_planejado", (i+1).ToString(), "Total Planejado " + mes[i]));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");

                sb.AppendLine("<td style=\"display:" + display_meses + "\">");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(lblNovo("lblvl_revisado", (i + 1).ToString(), "Total Revisado " + mes[i]));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");

                sb.AppendLine("<td style=\"display:" + display_meses + "\">");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(lblNovo("lblvl_provisionado", (i + 1).ToString(), "Total Realizado " + mes[i]));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");

                sb.AppendLine("<td style=\"display:" + display_meses + "\">");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(lblNovo("lblvl_empenhado", (i+1).ToString(), "Total Empenhado " + mes[i]));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");

                sb.AppendLine("<td style=\"display:" + display_meses + "\">");
                Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
                Panel1.Controls.Add(lblNovo("lblvl_liquidado", (i+1).ToString(), "Total Liquidado " + mes[i]));
                sb = new System.Text.StringBuilder();
                sb.AppendLine("</td>");
                
            }
            sb.AppendLine("<td>");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
            Panel1.Controls.Add(lblNovo("lblvl_planejado", "", "Total Planejado "));
            sb = new System.Text.StringBuilder();
            sb.AppendLine("</td>");

            sb.AppendLine("<td>");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
            Panel1.Controls.Add(lblNovo("lblvl_revisado", "", "Total Revisado "));
            sb = new System.Text.StringBuilder();
            sb.AppendLine("</td>");            

            sb.AppendLine("<td>");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
            Panel1.Controls.Add(lblNovo("lblvl_provisionado", "", "Total Realizado " ));
            sb = new System.Text.StringBuilder();
            sb.AppendLine("</td>");

            sb.AppendLine("<td>");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
            Panel1.Controls.Add(lblNovo("lblvl_empenhado", "", "Total Empenhado "));
            sb = new System.Text.StringBuilder();
            sb.AppendLine("</td>");

            sb.AppendLine("<td>");
            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
            Panel1.Controls.Add(lblNovo("lblvl_liquidado", "", "Total Liquidado "));
            sb = new System.Text.StringBuilder();
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");

        }
        sb.AppendLine("</table>");
        Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));
    }


}
