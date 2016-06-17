

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t14_documento
{
	#region Declarations

	private int _t14_cd_documento;
	private int _t03_cd_projeto;
	private string _nm_documento;
	private string _ds_descricao;
	private string _nm_arquivo;
	private bool _fl_foto;
	private bool _fl_video;
	private bool _fl_cronograma;
	private bool _fl_outros;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_deletedo;
    private int _nu_ordem;

	#endregion

	#region Properties

	public int t14_cd_documento
	{
		get { return _t14_cd_documento; }
		set { _t14_cd_documento = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
	}

	public string nm_documento
	{
		get { return _nm_documento; }
		set { _nm_documento = value; }
	}

	public string ds_descricao
	{
		get { return _ds_descricao; }
		set { _ds_descricao = value; }
	}

	public string nm_arquivo
	{
		get { return _nm_arquivo; }
		set { _nm_arquivo = value; }
	}

	public bool fl_foto
	{
		get { return _fl_foto; }
		set { _fl_foto = value; }
	}

	public bool fl_video
	{
		get { return _fl_video; }
		set { _fl_video = value; }
	}

	public bool fl_cronograma
	{
		get { return _fl_cronograma; }
		set { _fl_cronograma = value; }
	}

	public bool fl_outros
	{
		get { return _fl_outros; }
		set { _fl_outros = value; }
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

	public bool fl_deletedo
	{
		get { return _fl_deletedo; }
		set { _fl_deletedo = value; }
	}

    public int nu_ordem
    {
        get { return _nu_ordem; }
        set { _nu_ordem = value; }
    }

	#endregion

	
}
