

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t05_situacao
{
	#region Declarations

	private int _t05_cd_situacao;
	private int _t03_cd_projeto;
	private string _ds_situacao;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	

	#endregion

	#region Properties

	public int t05_cd_situacao
	{
		get { return _t05_cd_situacao; }
		set { _t05_cd_situacao = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
	}

	public string ds_situacao
	{
		get { return _ds_situacao; }
		set { _ds_situacao = value; }
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


	#endregion

	
}
