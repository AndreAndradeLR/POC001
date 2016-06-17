using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomControls_Valores_ucLancamentoMensal : System.Web.UI.UserControl
{
    private string _prefix;
    private int _inicio;
    private int _fim;
    private bool _editar;
    private bool _PrevistoBloqueado;

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

    protected void Page_Load(object sender, System.EventArgs e)
    {
        TableBind();
    }

    public void TableClear()
    {
        tbAnos.Controls.Clear();
        TableBind();
    }

    public void TableBind()
    {        
        if (tbAnos.Rows.Count < 1)
        {
            int numcells = 14;
            int j;

            TableRow HeaderRow = new TableRow();
            HeaderRow.Style["color"] = "white";
            HeaderRow.Style["font-weight"] = "bold";
            HeaderRow.Style["text-align"] = "center";
            HeaderRow.Style["background-color"] = "#5D7B9D";
            tbAnos.Rows.Add(HeaderRow);

            TableCell HeaderCell_1 = new TableCell();
            HeaderCell_1.Text = "Ano";
            HeaderRow.Cells.Add(HeaderCell_1);

            TableCell HeaderCell_2 = new TableCell();
            HeaderCell_2.Text = "Jan";
            HeaderRow.Cells.Add(HeaderCell_2);

            TableCell HeaderCell_3 = new TableCell();
            HeaderCell_3.Text = "Fev";
            HeaderRow.Cells.Add(HeaderCell_3);

            TableCell HeaderCell_4 = new TableCell();
            HeaderCell_4.Text = "Mar";
            HeaderRow.Cells.Add(HeaderCell_4);

            TableCell HeaderCell_5 = new TableCell();
            HeaderCell_5.Text = "Abr";
            HeaderRow.Cells.Add(HeaderCell_5);

            TableCell HeaderCell_6 = new TableCell();
            HeaderCell_6.Text = "Mai";
            HeaderRow.Cells.Add(HeaderCell_6);

            TableCell HeaderCell_7 = new TableCell();
            HeaderCell_7.Text = "Jun";
            HeaderRow.Cells.Add(HeaderCell_7);

            TableCell HeaderCell_8 = new TableCell();
            HeaderCell_8.Text = "Jul";
            HeaderRow.Cells.Add(HeaderCell_8);

            TableCell HeaderCell_9 = new TableCell();
            HeaderCell_9.Text = "Ago";
            HeaderRow.Cells.Add(HeaderCell_9);

            TableCell HeaderCell_10 = new TableCell();
            HeaderCell_10.Text = "Set";
            HeaderRow.Cells.Add(HeaderCell_10);

            TableCell HeaderCell_11 = new TableCell();
            HeaderCell_11.Text = "Out";
            HeaderRow.Cells.Add(HeaderCell_11);

            TableCell HeaderCell_12 = new TableCell();
            HeaderCell_12.Text = "Nov";
            HeaderRow.Cells.Add(HeaderCell_12);

            TableCell HeaderCell_13 = new TableCell();
            HeaderCell_13.Text = "Dez";
            HeaderRow.Cells.Add(HeaderCell_13);

            TableCell HeaderCell_14 = new TableCell();
            HeaderCell_14.Text = "Total";
            HeaderRow.Cells.Add(HeaderCell_14);
            if (_editar)
                HeaderCell_14.Visible = false;



            for (j = _inicio; j <= _fim; j++)
            {
                TableRow r = new TableRow();
                r.Style["background-color"] = "#F1F5F5";
                int i;
                for (i = 0; i <= numcells - 1; i++)
                {
                    TableCell c = new TableCell();
                    TextBox UserTextBox = new TextBox();

                    if (!_editar)
                    {
                        UserTextBox.Attributes.Add("style", "background:#F1F5F5;border:none;text-align:right;");
                        UserTextBox.ReadOnly = true;
                    }

                    if (i == 0)
                    {
                        c.Controls.Add(new LiteralControl(j.ToString()));
                        r.Cells.Add(c);
                    }
                    else if (i < 13)
                    {                                                
                        UserTextBox.ID = "txtvl_" + _prefix + i + j.ToString();                        
                        if (_prefix == "p")
                        {
                            if (_PrevistoBloqueado)
                            {
                                UserTextBox.Attributes.Add("readonly", "readonly");
                            }
                        }
                        UserTextBox.Columns = 13;
                        UserTextBox.Text = "0";
                        UserTextBox.MaxLength = 18;
                        c.Controls.Add(UserTextBox);

                        if (_editar)
                        {
                            CompareValidator val = new CompareValidator();
                            val.ID = "valp" + i.ToString() + j.ToString();
                            val.ControlToValidate = UserTextBox.ID;
                            val.ErrorMessage = "<br>*formato inválido";
                            val.Display = ValidatorDisplay.Dynamic;
                            val.Operator = ValidationCompareOperator.DataTypeCheck;
                            val.Type = ValidationDataType.Currency;
                            c.Controls.Add(val);
                        }
                        r.Cells.Add(c);
                    }
                    else if ((i == 13) && (!_editar))
                    {
                        UserTextBox.ID = "txtvl_" + _prefix + "Total" + j.ToString();
                        UserTextBox.ReadOnly = true;
                        c.Controls.Add(UserTextBox);
                        r.Cells.Add(c);
                    }

                    tbAnos.Rows.Add(r);
                }
            }
        }
    }
}
