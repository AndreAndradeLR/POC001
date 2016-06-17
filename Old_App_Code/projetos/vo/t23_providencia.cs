using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for t23_providencia
/// </summary>
public class t23_providencia
{
    private int _t23_cd_providencia;
    private int _t07_cd_restricao;
    private string _t02_cd_usuario;
    private string _ds_providencia;
    private bool _fl_gerente;
    private DateTime _dt_limite;
    private DateTime _dt_cadastro;

    public int t23_cd_providencia
    {
        get { return _t23_cd_providencia; }
        set { _t23_cd_providencia = value; }
    }


    public int t07_cd_restricao
    {
        get { return _t07_cd_restricao; }
        set { _t07_cd_restricao = value; }
    }


    public string t02_cd_usuario
    {
        get { return _t02_cd_usuario; }
        set { _t02_cd_usuario = value; }
    }


    public string ds_providencia
    {
        get { return _ds_providencia; }
        set { _ds_providencia = value; }
    }


    public bool fl_gerente
    {
        get { return _fl_gerente; }
        set { _fl_gerente = value; }
    }


    public DateTime dt_cadastro
    {
        get { return _dt_cadastro; }
        set { _dt_cadastro = value; }
    }

    public DateTime dt_limite
    {
        get { return _dt_limite; }
        set { _dt_limite = value; }
    }


    public t23_providencia()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
