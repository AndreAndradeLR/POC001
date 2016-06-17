

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t09_marco
{
	#region Declarations

	private int _t09_cd_marco;
	private int _t03_cd_projeto;
	private string _ds_marco;
	private int _nu_esforco;
	private DateTime _dt_prevista;
	private DateTime _dt_realizada;
	private DateTime _dt_original;
	private string _ds_comentario;
	private string _fl_status;
	private int _nu_ordem;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_deletado;
	

	#endregion

	#region Properties

	public int t09_cd_marco
	{
		get { return _t09_cd_marco; }
		set { _t09_cd_marco = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
	}

	public string ds_marco
	{
		get { return _ds_marco; }
		set { _ds_marco = value; }
	}

	public int nu_esforco
	{
		get { return _nu_esforco; }
		set { _nu_esforco = value; }
	}

	public DateTime dt_prevista
	{
		get { return _dt_prevista; }
		set { _dt_prevista = value; }
	}

	public DateTime dt_realizada
	{
		get { return _dt_realizada; }
		set { _dt_realizada = value; }
	}

	public DateTime dt_original
	{
		get { return _dt_original; }
		set { _dt_original = value; }
	}

	public string ds_comentario
	{
		get { return _ds_comentario; }
		set { _ds_comentario = value; }
	}

	public string fl_status
	{
		get { return _fl_status; }
		set { _fl_status = value; }
	}

	public int nu_ordem
	{
		get { return _nu_ordem; }
		set { _nu_ordem = value; }
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
