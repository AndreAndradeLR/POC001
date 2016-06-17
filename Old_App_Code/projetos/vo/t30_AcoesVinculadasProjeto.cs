using System;
using System.Collections.Generic;
public class t30_AcoesVinculadasProjeto
{
	#region Declarations

    private int _cd_acoes_vinculadas_projeto;
	private int _t08_cd_acao;
    private int _t03_cd_projeto;
	private bool _fl_deletado;

	#endregion

	#region Properties

    public int cd_acoes_vinculadas_projeto
	{
        get { return _cd_acoes_vinculadas_projeto; }
        set { _cd_acoes_vinculadas_projeto = value; }
	}

	public int t08_cd_acao
	{
		get { return _t08_cd_acao; }
		set { _t08_cd_acao = value; }
	}

    public int t03_cd_projeto
    {
        get { return _t03_cd_projeto; }
        set { _t03_cd_projeto = value; }
    }

	public bool fl_deletado
	{
		get { return _fl_deletado; }
		set { _fl_deletado = value; }
	}

	
	#endregion
}
