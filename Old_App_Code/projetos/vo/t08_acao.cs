

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t08_acao
{
	#region Declarations

	private int _t08_cd_acao;
	private int _t03_cd_projeto;
    private string _t02_cd_usuario;
	private string _nm_acao;
	private string _ds_acao;
	private DateTime _dt_inicio;
	private DateTime _dt_fim;
	private DateTime _dt_original;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_deletado;
    private string _nm_responsavel;
	

	#endregion

	#region Properties

	public int t08_cd_acao
	{
		get { return _t08_cd_acao; }
		set { _t08_cd_acao = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
	}

    public string nm_responsavel
    {
        get { return _nm_responsavel; }
        set { _nm_responsavel = value; }
    }
    public string t02_cd_usuario
    {
        get { return _t02_cd_usuario; }
        set { _t02_cd_usuario = value; }
    }

	public string nm_acao
	{
		get { return _nm_acao; }
		set { _nm_acao = value; }
	}

	public string ds_acao
	{
		get { return _ds_acao; }
		set { _ds_acao = value; }
	}

	public DateTime dt_inicio
	{
		get { return _dt_inicio; }
		set { _dt_inicio = value; }
	}

	public DateTime dt_fim
	{
		get { return _dt_fim; }
		set { _dt_fim = value; }
	}

	public DateTime dt_original
	{
		get { return _dt_original; }
		set { _dt_original = value; }
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
