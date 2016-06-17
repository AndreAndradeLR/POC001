

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t20_acesso
{
	#region Declarations

	private int _t20_cd_acesso;
	private string _t02_cd_usuario;
	private string _nm_ip;
	private DateTime _dt_data;
	

	#endregion

	#region Properties

	public int t20_cd_acesso
	{
		get { return _t20_cd_acesso; }
		set { _t20_cd_acesso = value; }
	}

	public string t02_cd_usuario
	{
		get { return _t02_cd_usuario; }
		set { _t02_cd_usuario = value; }
	}

	public string nm_ip
	{
		get { return _nm_ip; }
		set { _nm_ip = value; }
	}

	public DateTime dt_data
	{
		get { return _dt_data; }
		set { _dt_data = value; }
	}


	#endregion

    public t20_acesso()
    {
    }

    public t20_acesso(string cd_usuario, string ip)
    {
        t02_cd_usuario = cd_usuario;
        nm_ip = ip;
    }
}
