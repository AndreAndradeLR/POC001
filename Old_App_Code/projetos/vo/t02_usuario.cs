using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class t02_usuario
{
	#region Declarations

	private string _t02_cd_usuario;
	private int _t01_cd_entidade;
	private string _nm_nome;
	private string _nm_cargo;
	private string _nm_email;
	private string _nm_telefone;
	private string _nm_celular;
	private string _pw_senha;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;

    private t01_entidade _t01;

    private List<t24_perfil> _t24l;


	#endregion

	#region Properties

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

	public string nm_nome
	{
		get { return _nm_nome; }
		set { _nm_nome = value; }
	}

	public string nm_cargo
	{
		get { return _nm_cargo; }
		set { _nm_cargo = value; }
	}

	public string nm_email
	{
		get { return _nm_email; }
		set { _nm_email = value; }
	}

	public string nm_telefone
	{
		get { return _nm_telefone; }
		set { _nm_telefone = value; }
	}

	public string nm_celular
	{
		get { return _nm_celular; }
		set { _nm_celular = value; }
	}

	public string pw_senha
	{
		get { return _pw_senha; }
		set { _pw_senha = value; }
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

    public t01_entidade t01
    {
        get { return _t01; }
        set { _t01 = value; }
    }

    public List<t24_perfil> t24l
    {
        get { return _t24l; }
        set { _t24l = value; }
    }

	#endregion

    public t02_usuario()
    {
        _t01 = new t01_entidade();
        _t24l = new List<t24_perfil>();
    }
}
