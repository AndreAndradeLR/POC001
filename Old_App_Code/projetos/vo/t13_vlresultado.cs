

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t13_vlresultado
{
	#region Declarations

	private int _t13_cd_vlresultado;
	private int _t12_cd_resultado;
	private int _nu_ano;
	private decimal _vl_previsto;
	private decimal _vl_realizado;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	

	#endregion

	#region Properties

	public int t13_cd_vlresultado
	{
		get { return _t13_cd_vlresultado; }
		set { _t13_cd_vlresultado = value; }
	}

	public int t12_cd_resultado
	{
		get { return _t12_cd_resultado; }
		set { _t12_cd_resultado = value; }
	}

	public int nu_ano
	{
		get { return _nu_ano; }
		set { _nu_ano = value; }
	}

	public decimal vl_previsto
	{
		get { return _vl_previsto; }
		set { _vl_previsto = value; }
	}

	public decimal vl_realizado
	{
		get { return _vl_realizado; }
		set { _vl_realizado = value; }
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
