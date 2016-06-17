
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t21_fase
{
	#region Declarations

	private int _t21_cd_fase;
	private string _nm_fase;
	private string _ds_fase;
	private string _fl_fase;
	private string _nm_arquivo;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _found;

	#endregion

	#region Properties

	public int t21_cd_fase
	{
		get { return _t21_cd_fase; }
		set { _t21_cd_fase = value; }
	}

	public string nm_fase
	{
		get { return _nm_fase; }
		set { _nm_fase = value; }
	}

	public string ds_fase
	{
		get { return _ds_fase; }
		set { _ds_fase = value; }
	}

	public string fl_fase
	{
		get { return _fl_fase; }
		set { _fl_fase = value; }
	}

	public string nm_arquivo
	{
		get { return _nm_arquivo; }
		set { _nm_arquivo = value; }
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

	public bool Found
	{
		get { return _found; }
	}

	#endregion

	
}
