

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t15_noticia
{
	#region Declarations

	private int _t15_cd_noticia;
	private int _t03_cd_projeto;
	private string _nm_noticia;
	private string _ds_noticia;
	private DateTime _dt_data;
    private string _nm_arquivo;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_ativa;
	

	#endregion

	#region Properties

	public int t15_cd_noticia
	{
		get { return _t15_cd_noticia; }
		set { _t15_cd_noticia = value; }
	}

	public int t03_cd_projeto
	{
		get { return _t03_cd_projeto; }
		set { _t03_cd_projeto = value; }
	}

	public string nm_noticia
	{
		get { return _nm_noticia; }
		set { _nm_noticia = value; }
	}

	public string ds_noticia
	{
		get { return _ds_noticia; }
		set { _ds_noticia = value; }
	}

	public DateTime dt_data
	{
		get { return _dt_data; }
		set { _dt_data = value; }
	}

    public string nm_arquivo
    {
        get { return _nm_arquivo; }
        set { _nm_arquivo = value; }
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
