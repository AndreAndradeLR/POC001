using System;
public class t24_perfil
{
	#region Declarations

	private int _t24_cd_perfil;
	private string _nm_perfil;
	private string _ds_perfil;
	private string _fl_perfil;
	private bool _fl_ativa;
	private int _nu_ordem;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;

	#endregion

	#region Properties

	public int t24_cd_perfil
	{
		get { return _t24_cd_perfil; }
		set { _t24_cd_perfil = value; }
	}

	public string nm_perfil
	{
		get { return _nm_perfil; }
		set { _nm_perfil = value; }
	}

	public string ds_perfil
	{
		get { return _ds_perfil; }
		set { _ds_perfil = value; }
	}

	public string fl_perfil
	{
		get { return _fl_perfil; }
		set { _fl_perfil = value; }
	}

	public bool fl_ativa
	{
		get { return _fl_ativa; }
		set { _fl_ativa = value; }
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

	#endregion

}
