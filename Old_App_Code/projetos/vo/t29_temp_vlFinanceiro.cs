using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t29_temp_vlFinanceiro
{
	#region Declarations

	private int _t03_cd_projeto;
    private int _codRelatorio;
    private string _nm_fonte;
	private string _nm_unidade;

	private decimal _PlanejadoMes;
	private decimal _RevisadoMes;
	private decimal _RealizadoMes;
	private decimal _EmpenhadoMes;
	private decimal _LiquidadoMes;

	private decimal _PlanejadoAcu;
	private decimal _RevisadoAcu;
	private decimal _RealizadoAcu;
	private decimal _EmpenhadoAcu;
	private decimal _LiquidadoAcu;

	private decimal _PlanejadoTot;
	private decimal _RevisadoTot;
	private decimal _RealizadoTot;
	private decimal _EmpenhadoTot;
	private decimal _LiquidadoTot;	

	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	

	#endregion

	#region Properties

    public int t03_cd_projeto
    {
        get { return _t03_cd_projeto; }
        set { _t03_cd_projeto = value; }
    }

    public int CodRelatorio
    {
        get { return _codRelatorio; }
        set { _codRelatorio = value; }
    }

    public string nm_fonte
    {
        get { return _nm_fonte; }
        set { _nm_fonte = value; }
    }

    public string nm_unidade
    {
        get { return _nm_unidade; }
        set { _nm_unidade = value; }
    }

	public decimal PlanejadoMes
	{
		get { return _PlanejadoMes; }
		set { _PlanejadoMes = value; }
	}

	public decimal RevisadoMes
	{
		get { return _RevisadoMes; }
		set { _RevisadoMes = value; }
	}

	public decimal RealizadoMes
	{
		get { return _RealizadoMes; }
		set { _RealizadoMes = value; }
	}

	public decimal EmpenhadoMes
	{
		get { return _EmpenhadoMes; }
		set { _EmpenhadoMes = value; }
	}

	public decimal LiquidadoMes
	{
		get { return _LiquidadoMes; }
		set { _LiquidadoMes = value; }
	}

	public decimal PlanejadoAcu
	{
		get { return _PlanejadoAcu; }
		set { _PlanejadoAcu = value; }
	}

	public decimal RevisadoAcu
	{
		get { return _RevisadoAcu; }
		set { _RevisadoAcu = value; }
	}

	public decimal RealizadoAcu
	{
		get { return _RealizadoAcu; }
		set { _RealizadoAcu = value; }
	}

	public decimal EmpenhadoAcu
	{
		get { return _EmpenhadoAcu; }
		set { _EmpenhadoAcu = value; }
	}

	public decimal LiquidadoAcu
	{
		get { return _LiquidadoAcu; }
		set { _LiquidadoAcu = value; }
	}

	public decimal PlanejadoTot
	{
		get { return _PlanejadoTot; }
		set { _PlanejadoTot = value; }
	}

	public decimal RevisadoTot
	{
		get { return _RevisadoTot; }
		set { _RevisadoTot = value; }
	}

	public decimal RealizadoTot
	{
		get { return _RealizadoTot; }
		set { _RealizadoTot = value; }
	}

	public decimal EmpenhadoTot
	{
		get { return _EmpenhadoTot; }
		set { _EmpenhadoTot = value; }
	}

	public decimal LiquidadoTot
	{
		get { return _LiquidadoTot; }
		set { _LiquidadoTot = value; }
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
