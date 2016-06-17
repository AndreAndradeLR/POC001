using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FiltroProjeto
/// </summary>
public class FiltroProjeto
{

    private Nullable<Int32> _t01_cd_entidade_resp;
	public Nullable<int> T01_cd_entidade_resp {
		get { return _t01_cd_entidade_resp; }
		set { _t01_cd_entidade_resp = value; }
	}

    private Nullable<Int32> _t01_cd_entidade_parc;
	public Nullable<int> T01_cd_entidade_parc {
		get { return _t01_cd_entidade_parc; }
		set { _t01_cd_entidade_parc = value; }
	}

    private Nullable<Int32> _t21_cd_fase;
	public Nullable<int> T21_cd_fase {
		get { return _t21_cd_fase; }
		set { _t21_cd_fase = value; }
	}

    private Nullable<Int32> _t26_cd_arearesultado;
    public Nullable<int> T26_cd_arearesultado
    {
        get { return _t26_cd_arearesultado; }
        set { _t26_cd_arearesultado = value; }
    }

    private Nullable<Int32> _t03_cd_projeto;
    public Nullable<int> T03_cd_projeto
    {
        get { return _t03_cd_projeto; }
        set { _t03_cd_projeto = value; }
    }

    private string _array_cd_projeto;
    public string array_cd_projeto{
        get { return _array_cd_projeto; }
        set { _array_cd_projeto = value; }
    }

	public FiltroProjeto()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
