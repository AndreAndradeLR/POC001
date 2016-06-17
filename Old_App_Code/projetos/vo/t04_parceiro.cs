

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t04_parceiro
{
	#region Declarations

	private int _t04_cd_parceiro;
	private int _t03_cd_projeto;
	private int _t01_cd_entidade;
	private string _nm_nome;
	private string _ds_atuacao;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	

	#endregion

	#region Properties

	public int t04_cd_parceiro
	{
		get { return _t04_cd_parceiro; }
		set { _t04_cd_parceiro = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
	}

	public int t01_cd_entidade
	{
		get { return _t01_cd_entidade; }
		set { _t01_cd_entidade = value; }
	}

	public string nm_nome
	{
		get { return _nm_nome; }
		set { _nm_nome = value; }
	}

	public string ds_atuacao
	{
		get { return _ds_atuacao; }
		set { _ds_atuacao = value; }
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
