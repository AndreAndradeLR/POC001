using System;
public class t27_fonte
{
	#region Declarations

	private int _t27_cd_fonte;
	private string _nm_fonte;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_deletado;

	#endregion

	#region Properties

	public int t27_cd_fonte
	{
		get { return _t27_cd_fonte; }
		set { _t27_cd_fonte = value; }
	}

	public string nm_fonte
	{
		get { return _nm_fonte; }
		set { _nm_fonte = value; }
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
