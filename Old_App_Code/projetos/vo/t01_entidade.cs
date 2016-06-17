

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t01_entidade
{
	#region Declarations

	private int _t01_cd_entidade;
	private string _nm_entidade;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_deletado;
	

	#endregion

	#region Properties

	public int t01_cd_entidade
	{
		get { return _t01_cd_entidade; }
		set { _t01_cd_entidade = value; }
	}

	public string nm_entidade
	{
		get { return _nm_entidade; }
		set { _nm_entidade = value; }
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

	#endregion

}
