

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class t07_restricao
{
	#region Declarations

	private int _t07_cd_restricao;
	private int _t03_cd_projeto;
    private string _t02_cd_usuario;
	private string _ds_restricao;
	private string _ds_medida;
	private string _ds_providencia;
	private DateTime _dt_superada;
	private DateTime _dt_limite;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_deletado;
    private List<t09_marco> _t09;

	#endregion

	#region Properties

	public int t07_cd_restricao
	{
		get { return _t07_cd_restricao; }
		set { _t07_cd_restricao = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
	}

    public string t02_cd_usuario
    {
        get { return _t02_cd_usuario; }
        set { _t02_cd_usuario = value; }
    }   

	public string ds_restricao
	{
		get { return _ds_restricao; }
		set { _ds_restricao = value; }
	}

	public string ds_medida
	{
		get { return _ds_medida; }
		set { _ds_medida = value; }
	}

	public string ds_providencia
	{
		get { return _ds_providencia; }
		set { _ds_providencia = value; }
	}

	public DateTime dt_superada
	{
		get { return _dt_superada; }
		set { _dt_superada = value; }
	}

	public DateTime dt_limite
	{
		get { return _dt_limite; }
		set { _dt_limite = value; }
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

    public List<t09_marco> t09
    {
        get { return _t09; }
        set { _t09 = value; }
    }


	#endregion

    public t07_restricao()
    {
        _t09 = new List<t09_marco>();
    }
	
}
