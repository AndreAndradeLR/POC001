using System;
public class t26_arearesultado
{
	#region Declarations

	private int _t26_cd_arearesultado;
	private string _nm_area;
	private string _ds_area;
	private DateTime _dt_cadastro;
	private DateTime _dt_alterado;
	private bool _fl_deletado;
    private String _nm_arquivo;

	#endregion

	#region Properties

	public int t26_cd_arearesultado
	{
		get { return _t26_cd_arearesultado; }
		set { _t26_cd_arearesultado = value; }
	}

	public string nm_area
	{
		get { return _nm_area; }
		set { _nm_area = value; }
	}

	public string ds_area
	{
		get { return _ds_area; }
		set { _ds_area = value; }
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

    public string nm_arquivo
    {
        get { return _nm_arquivo; }
        set { _nm_arquivo = value; }
    }

	#endregion

}
