
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t22_faseprojeto
{
	#region Declarations

	private int _t22_cd_faseprojeto;
	private int _t21_cd_fase;
	private int _t03_cd_projeto;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _found;

	#endregion

	#region Properties

	public int t22_cd_faseprojeto
	{
		get { return _t22_cd_faseprojeto; }
		set { _t22_cd_faseprojeto = value; }
	}

	public int t21_cd_fase
	{
		get { return _t21_cd_fase; }
		set { _t21_cd_fase = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
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
