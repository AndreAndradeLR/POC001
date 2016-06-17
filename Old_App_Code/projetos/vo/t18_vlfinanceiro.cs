using System;
public class t18b_vlfinanceiro
{
	#region Declarations

	private int _t18b_cd_vlfinanceiro;
	private int _t11_cd_financeiro;
	private int _nu_ano;

    private decimal _vl_restopagar;
    private decimal _vl_dotorcado;
    private decimal _vl_assegurado;

	private decimal _vl_planejado1;
	private decimal _vl_revisado1;
	private decimal _vl_provisionado1;
	private decimal _vl_empenhado1;
	private decimal _vl_liquidado1;
	private decimal _vl_planejado2;
	private decimal _vl_revisado2;
    private decimal _vl_provisionado2;
	private decimal _vl_empenhado2;
	private decimal _vl_liquidado2;
	private decimal _vl_planejado3;
	private decimal _vl_revisado3;
    private decimal _vl_provisionado3;
	private decimal _vl_empenhado3;
	private decimal _vl_liquidado3;
	private decimal _vl_planejado4;
	private decimal _vl_revisado4;
    private decimal _vl_provisionado4;
	private decimal _vl_empenhado4;
	private decimal _vl_liquidado4;
	private decimal _vl_planejado5;
	private decimal _vl_revisado5;
    private decimal _vl_provisionado5;
	private decimal _vl_empenhado5;
	private decimal _vl_liquidado5;
	private decimal _vl_planejado6;
	private decimal _vl_revisado6;
    private decimal _vl_provisionado6;
	private decimal _vl_empenhado6;
	private decimal _vl_liquidado6;
	private decimal _vl_planejado7;
	private decimal _vl_revisado7;
    private decimal _vl_provisionado7;
	private decimal _vl_empenhado7;
	private decimal _vl_liquidado7;
	private decimal _vl_planejado8;
	private decimal _vl_revisado8;
    private decimal _vl_provisionado8;
	private decimal _vl_empenhado8;
	private decimal _vl_liquidado8;
	private decimal _vl_planejado9;
	private decimal _vl_revisado9;
    private decimal _vl_provisionado9;
	private decimal _vl_empenhado9;
	private decimal _vl_liquidado9;
	private decimal _vl_planejado10;
	private decimal _vl_revisado10;
    private decimal _vl_provisionado10;
	private decimal _vl_empenhado10;
	private decimal _vl_liquidado10;
	private decimal _vl_planejado11;
	private decimal _vl_revisado11;
    private decimal _vl_provisionado11;
	private decimal _vl_empenhado11;
	private decimal _vl_liquidado11;
	private decimal _vl_planejado12;
	private decimal _vl_revisado12;
    private decimal _vl_provisionado12;
	private decimal _vl_empenhado12;
	private decimal _vl_liquidado12;
	private DateTime _dt_cadastro;

	#endregion

	#region Properties

	public int t18b_cd_vlfinanceiro
	{
		get { return _t18b_cd_vlfinanceiro; }
		set { _t18b_cd_vlfinanceiro = value; }
	}

	public int t11_cd_financeiro
	{
		get { return _t11_cd_financeiro; }
		set { _t11_cd_financeiro = value; }
	}

	public int nu_ano
	{
		get { return _nu_ano; }
		set { _nu_ano = value; }
	}

    public decimal vl_restopagar
	{
		get { return _vl_restopagar; }
        set { _vl_restopagar = value; }
	}

    public decimal vl_dotorcado
    {
        get { return _vl_dotorcado; }
        set { _vl_dotorcado = value; }
    }

    public decimal vl_assegurado
    {
        get { return _vl_assegurado; }
        set { _vl_assegurado = value; }
    }

    public decimal vl_planejado1
    {
        get { return _vl_planejado1; }
        set { _vl_planejado1 = value; }
    }

	public decimal vl_revisado1
	{
		get { return _vl_revisado1; }
		set { _vl_revisado1 = value; }
	}

    public decimal vl_provisionado1
	{
        get { return _vl_provisionado1; }
        set { _vl_provisionado1 = value; }
	}

	public decimal vl_empenhado1
	{
		get { return _vl_empenhado1; }
		set { _vl_empenhado1 = value; }
	}

	public decimal vl_liquidado1
	{
        get { return _vl_liquidado1; }
        set { _vl_liquidado1 = value; }
	}

	public decimal vl_planejado2
	{
		get { return _vl_planejado2; }
		set { _vl_planejado2 = value; }
	}

	public decimal vl_revisado2
	{
		get { return _vl_revisado2; }
		set { _vl_revisado2 = value; }
	}

    public decimal vl_provisionado2
    {
        get { return _vl_provisionado2; }
        set { _vl_provisionado2 = value; }
    }

	public decimal vl_empenhado2
	{
		get { return _vl_empenhado2; }
		set { _vl_empenhado2 = value; }
	}

	public decimal vl_liquidado2
	{
		get { return _vl_liquidado2; }
		set { _vl_liquidado2 = value; }
	}

	public decimal vl_planejado3
	{
		get { return _vl_planejado3; }
		set { _vl_planejado3 = value; }
	}

	public decimal vl_revisado3
	{
		get { return _vl_revisado3; }
		set { _vl_revisado3 = value; }
	}

    public decimal vl_provisionado3
    {
        get { return _vl_provisionado3; }
        set { _vl_provisionado3 = value; }
    }

	public decimal vl_empenhado3
	{
		get { return _vl_empenhado3; }
		set { _vl_empenhado3 = value; }
	}

	public decimal vl_liquidado3
	{
		get { return _vl_liquidado3; }
		set { _vl_liquidado3 = value; }
	}

	public decimal vl_planejado4
	{
		get { return _vl_planejado4; }
		set { _vl_planejado4 = value; }
	}

	public decimal vl_revisado4
	{
		get { return _vl_revisado4; }
		set { _vl_revisado4 = value; }
	}

    public decimal vl_provisionado4
    {
        get { return _vl_provisionado4; }
        set { _vl_provisionado4 = value; }
    }

	public decimal vl_empenhado4
	{
		get { return _vl_empenhado4; }
		set { _vl_empenhado4 = value; }
	}

	public decimal vl_liquidado4
	{
		get { return _vl_liquidado4; }
		set { _vl_liquidado4 = value; }
	}

	public decimal vl_planejado5
	{
		get { return _vl_planejado5; }
		set { _vl_planejado5 = value; }
	}

	public decimal vl_revisado5
	{
		get { return _vl_revisado5; }
		set { _vl_revisado5 = value; }
	}

    public decimal vl_provisionado5
    {
        get { return _vl_provisionado5; }
        set { _vl_provisionado5 = value; }
    }

	public decimal vl_empenhado5
	{
		get { return _vl_empenhado5; }
		set { _vl_empenhado5 = value; }
	}

	public decimal vl_liquidado5
	{
		get { return _vl_liquidado5; }
		set { _vl_liquidado5 = value; }
	}

	public decimal vl_planejado6
	{
		get { return _vl_planejado6; }
		set { _vl_planejado6 = value; }
	}

	public decimal vl_revisado6
	{
		get { return _vl_revisado6; }
		set { _vl_revisado6 = value; }
	}

    public decimal vl_provisionado6
    {
        get { return _vl_provisionado6; }
        set { _vl_provisionado6 = value; }
    }

	public decimal vl_empenhado6
	{
		get { return _vl_empenhado6; }
		set { _vl_empenhado6 = value; }
	}

	public decimal vl_liquidado6
	{
		get { return _vl_liquidado6; }
		set { _vl_liquidado6 = value; }
	}

	public decimal vl_planejado7
	{
		get { return _vl_planejado7; }
		set { _vl_planejado7 = value; }
	}

	public decimal vl_revisado7
	{
		get { return _vl_revisado7; }
		set { _vl_revisado7 = value; }
	}

    public decimal vl_provisionado7
    {
        get { return _vl_provisionado7; }
        set { _vl_provisionado7 = value; }
    }

	public decimal vl_empenhado7
	{
		get { return _vl_empenhado7; }
		set { _vl_empenhado7 = value; }
	}

	public decimal vl_liquidado7
	{
		get { return _vl_liquidado7; }
		set { _vl_liquidado7 = value; }
	}

	public decimal vl_planejado8
	{
		get { return _vl_planejado8; }
		set { _vl_planejado8 = value; }
	}

	public decimal vl_revisado8
	{
		get { return _vl_revisado8; }
		set { _vl_revisado8 = value; }
	}

    public decimal vl_provisionado8
    {
        get { return _vl_provisionado8; }
        set { _vl_provisionado8 = value; }
    }

	public decimal vl_empenhado8
	{
		get { return _vl_empenhado8; }
		set { _vl_empenhado8 = value; }
	}

	public decimal vl_liquidado8
	{
		get { return _vl_liquidado8; }
		set { _vl_liquidado8 = value; }
	}

	public decimal vl_planejado9
	{
		get { return _vl_planejado9; }
		set { _vl_planejado9 = value; }
	}

	public decimal vl_revisado9
	{
		get { return _vl_revisado9; }
		set { _vl_revisado9 = value; }
	}

    public decimal vl_provisionado9
    {
        get { return _vl_provisionado9; }
        set { _vl_provisionado9 = value; }
    }

	public decimal vl_empenhado9
	{
		get { return _vl_empenhado9; }
		set { _vl_empenhado9 = value; }
	}

	public decimal vl_liquidado9
	{
		get { return _vl_liquidado9; }
		set { _vl_liquidado9 = value; }
	}

	public decimal vl_planejado10
	{
		get { return _vl_planejado10; }
		set { _vl_planejado10 = value; }
	}

	public decimal vl_revisado10
	{
		get { return _vl_revisado10; }
		set { _vl_revisado10 = value; }
	}

    public decimal vl_provisionado10
    {
        get { return _vl_provisionado10; }
        set { _vl_provisionado10 = value; }
    }

	public decimal vl_empenhado10
	{
		get { return _vl_empenhado10; }
		set { _vl_empenhado10 = value; }
	}

	public decimal vl_liquidado10
	{
		get { return _vl_liquidado10; }
		set { _vl_liquidado10 = value; }
	}

	public decimal vl_planejado11
	{
		get { return _vl_planejado11; }
		set { _vl_planejado11 = value; }
	}

	public decimal vl_revisado11
	{
		get { return _vl_revisado11; }
		set { _vl_revisado11 = value; }
	}

    public decimal vl_provisionado11
    {
        get { return _vl_provisionado11; }
        set { _vl_provisionado11 = value; }
    }

	public decimal vl_empenhado11
	{
		get { return _vl_empenhado11; }
		set { _vl_empenhado11 = value; }
	}

	public decimal vl_liquidado11
	{
		get { return _vl_liquidado11; }
		set { _vl_liquidado11 = value; }
	}

	public decimal vl_planejado12
	{
		get { return _vl_planejado12; }
		set { _vl_planejado12 = value; }
	}

	public decimal vl_revisado12
	{
		get { return _vl_revisado12; }
		set { _vl_revisado12 = value; }
	}

    public decimal vl_provisionado12
    {
        get { return _vl_provisionado12; }
        set { _vl_provisionado12 = value; }
    }

	public decimal vl_empenhado12
	{
		get { return _vl_empenhado12; }
		set { _vl_empenhado12 = value; }
	}

	public decimal vl_liquidado12
	{
		get { return _vl_liquidado12; }
		set { _vl_liquidado12 = value; }
	}

	public DateTime dt_cadastro
	{
		get { return _dt_cadastro; }
		set { _dt_cadastro = value; }
	}


	#endregion

}
