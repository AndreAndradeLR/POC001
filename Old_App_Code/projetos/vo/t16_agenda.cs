

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t16_agenda
{
	#region Declarations

	private int _t16_cd_agenda;
	private int _t03_cd_projeto;
	private string _nm_agenda;
	private string _ds_agenda;
	private DateTime _dt_data;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_ativa;
	

	#endregion

	#region Properties

	public int t16_cd_agenda
	{
		get { return _t16_cd_agenda; }
		set { _t16_cd_agenda = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
	}

	public string nm_agenda
	{
		get { return _nm_agenda; }
		set { _nm_agenda = value; }
	}

	public string ds_agenda
	{
		get { return _ds_agenda; }
		set { _ds_agenda = value; }
	}

	public DateTime dt_data
	{
		get { return _dt_data; }
		set { _dt_data = value; }
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

	public bool fl_ativa
	{
		get { return _fl_ativa; }
		set { _fl_ativa = value; }
	}


	#endregion

	
}
