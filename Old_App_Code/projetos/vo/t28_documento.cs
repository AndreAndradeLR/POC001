using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class t28_documento
{
    #region Declarations

    private int _t28_cd_documento;
    private int _t08_cd_acao;
    private string _nm_documento;
    private string _ds_descricao;
    private string _nm_arquivo;    
    private DateTime _dt_cadastro;
    private DateTime _dt_alterado;
    private bool _fl_deletedo;


    #endregion

    #region Properties

    public int t28_cd_documento
    {
        get { return _t28_cd_documento; }
        set { _t28_cd_documento = value; }
    }

    public int t08_cd_acao
    {
        get { return _t08_cd_acao; }
        set { _t08_cd_acao = value; }
    }

    public string nm_documento
    {
        get { return _nm_documento; }
        set { _nm_documento = value; }
    }

    public string ds_descricao
    {
        get { return _ds_descricao; }
        set { _ds_descricao = value; }
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

    public bool fl_deletedo
    {
        get { return _fl_deletedo; }
        set { _fl_deletedo = value; }
    }


    #endregion

}
