

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t03_projeto
{
	#region Declarations

	private int _t03_cd_projeto;
	private string _t02_cd_usuario;
	private int _t01_cd_entidade;
	private string _t02_cd_usuario_monitoramento;
	private string _nm_projeto;
	private string _ds_publico;
	private string _ds_objetivo;
	private DateTime _dt_inicio;
	private DateTime _dt_fim;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
    private DateTime _dt_atualizado;
	private bool _fl_deletado;
    private int _t26_cd_arearesultado;
    private t26_arearesultado _t26;
	#endregion

	#region Properties

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

	public int t01_cd_entidade
	{
		get { return _t01_cd_entidade; }
		set { _t01_cd_entidade = value; }
	}

	public string t02_cd_usuario_monitoramento
	{
		get { return _t02_cd_usuario_monitoramento; }
		set { _t02_cd_usuario_monitoramento = value; }
	}

	public string nm_projeto
	{
		get { return _nm_projeto; }
		set { _nm_projeto = value; }
	}

	public string ds_publico
	{
		get { return _ds_publico; }
		set { _ds_publico = value; }
	}

	public string ds_objetivo
	{
		get { return _ds_objetivo; }
		set { _ds_objetivo = value; }
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

    public DateTime dt_atualizado
    {
        get { return _dt_atualizado; }
        set { _dt_atualizado = value; }
    }

	public bool fl_deletado
	{
		get { return _fl_deletado; }
		set { _fl_deletado = value; }
	}
    public int t26_cd_arearesultado
    {
        get { return _t26_cd_arearesultado; }
        set { _t26_cd_arearesultado = value; }
    }
    public t26_arearesultado t26
    {
        get { return _t26; }
        set { _t26 = value; }
    }

	#endregion

    public t03_projeto()
    {
        _t26 = new t26_arearesultado();
    }
	
}
