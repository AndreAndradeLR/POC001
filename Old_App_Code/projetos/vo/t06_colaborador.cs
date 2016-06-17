

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t06_colaborador
{
	#region Declarations

	private int _t06_cd_colaborador;
	private int _t03_cd_projeto;
	private string _t02_cd_usuario;
	private string _nm_funcao;
	private int _nu_ordem;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;

    private t02_usuario _t02;

	#endregion

	#region Properties

	public int t06_cd_colaborador
	{
		get { return _t06_cd_colaborador; }
		set { _t06_cd_colaborador = value; }
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

	public string nm_funcao
	{
		get { return _nm_funcao; }
		set { _nm_funcao = value; }
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
    public t02_usuario t02
    {
        get { return _t02; }
        set { _t02 = value; }
    }

	#endregion

    public t06_colaborador()
    {
        _t02 = new t02_usuario();
    }
 
}
