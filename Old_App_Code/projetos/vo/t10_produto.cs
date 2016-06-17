

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class t10_produto
{
	#region Declarations

	private int _t10_cd_produto;
	private int _t08_cd_acao;
	private string _ds_produto;
	private string _nm_medida;
    private int _nu_ordem = 0;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_deletado;
    private List<t17_vlproduto> _t17;

	#endregion

	#region Properties

	public int t10_cd_produto
	{
		get { return _t10_cd_produto; }
		set { _t10_cd_produto = value; }
	}

	public int t08_cd_acao
	{
		get { return _t08_cd_acao; }
		set { _t08_cd_acao = value; }
	}

	public string ds_produto
	{
		get { return _ds_produto; }
		set { _ds_produto = value; }
	}

	public string nm_medida
	{
		get { return _nm_medida; }
		set { _nm_medida = value; }
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

	public bool fl_deletado
	{
		get { return _fl_deletado; }
		set { _fl_deletado = value; }
	}

    public List<t17_vlproduto> t17
    {
        get { return _t17; }
        set { _t17 = value; }
    }
    

	#endregion

    public t10_produto()
    {
        t17 = new List<t17_vlproduto>();
    }
	
}
