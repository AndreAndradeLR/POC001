

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t19_marcorestricao
{
	#region Declarations

	private int _t19_cd_acaorestricao;
    private int _t09_cd_marco;
    private int _t07_cd_restricao;
	private DateTime _dt_cadastro;
	

	#endregion

	#region Properties

	public int t19_cd_acaorestricao
	{
		get { return _t19_cd_acaorestricao; }
		set { _t19_cd_acaorestricao = value; }
	}

	public int t07_cd_restricao
	{
		get { return _t07_cd_restricao; }
		set { _t07_cd_restricao = value; }
	}

    public int t09_cd_marco
	{
        get { return _t09_cd_marco; }
        set { _t09_cd_marco = value; }
	}

	public DateTime dt_cadastro
	{
		get { return _dt_cadastro; }
		set { _dt_cadastro = value; }
	}


	#endregion

	
}
