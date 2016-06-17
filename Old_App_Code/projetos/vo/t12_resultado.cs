

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class t12_resultado
{
	#region Declarations

	private int _t12_cd_resultado;
	private int _t03_cd_projeto;
	private string _nm_resultado;
	private string _ds_resultado;
	private string _nm_medida;
    private string _nm_respmedicao = "";
    private string _nm_fonte="";
	private int _nu_ano;
    private int _nu_ordem; 
	private decimal _vl_t0;
	private bool _fl_acumulado;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_deletado;
    private List<t13_vlresultado> _t13;

	#endregion

	#region Properties

	public int t12_cd_resultado
	{
		get { return _t12_cd_resultado; }
		set { _t12_cd_resultado = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
	}

	public string nm_resultado
	{
		get { return _nm_resultado; }
		set { _nm_resultado = value; }
	}

	public string ds_resultado
	{
		get { return _ds_resultado; }
		set { _ds_resultado = value; }
	}

	public string nm_medida
	{
		get { return _nm_medida; }
		set { _nm_medida = value; }
	}

    public string nm_respmedicao
    {
        get { return _nm_respmedicao; }
        set { _nm_respmedicao = value; }
    }

    public string nm_fonte
    {
        get { return _nm_fonte; }
        set { _nm_fonte = value; }
    }

	public int nu_ano
	{
		get { return _nu_ano; }
		set { _nu_ano = value; }
	}

    public int nu_ordem
    {
        get { return _nu_ordem; }
        set { _nu_ordem = value; }
    }

	public decimal vl_t0
	{
		get { return _vl_t0; }
		set { _vl_t0 = value; }
	}

	public bool fl_acumulado
	{
		get { return _fl_acumulado; }
		set { _fl_acumulado = value; }
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
    public List<t13_vlresultado> t13
    {
        get { return _t13; }
        set { _t13 = value; }
    }

	#endregion

    public t12_resultado()
    {
        _t13 = new List<t13_vlresultado>();
    }

}
