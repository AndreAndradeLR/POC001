using System;
using System.Collections.Generic;
public class t11_financeiro
{
	#region Declarations

	private int _t11_cd_financeiro;
	private int _t08_cd_acao;
    private int _t27_cd_fonte;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_deletado;

    private List<t18b_vlfinanceiro> _t18l;

	#endregion

	#region Properties

	public int t11_cd_financeiro
	{
		get { return _t11_cd_financeiro; }
		set { _t11_cd_financeiro = value; }
	}

	public int t08_cd_acao
	{
		get { return _t08_cd_acao; }
		set { _t08_cd_acao = value; }
	}

    public int t27_cd_fonte
    {
        get { return _t27_cd_fonte; }
        set { _t27_cd_fonte = value; }
    }

	public DateTime dt_cadastro
	{
		get { return _dt_cadastro; }
		set { _dt_cadastro = value; }
	}

	public DateTime dt_alterado
	{
		get { return _dt_alterado; }
		set { _dt_alterado = value; }
	}

	public bool fl_deletado
	{
		get { return _fl_deletado; }
		set { _fl_deletado = value; }
	}

    public List<t18b_vlfinanceiro> t18l
    {
        get { return _t18l; }
        set { _t18l = value; }
    }
	
	#endregion

    public t11_financeiro()
    {
        _t18l = new List<t18b_vlfinanceiro>();
    }
}
