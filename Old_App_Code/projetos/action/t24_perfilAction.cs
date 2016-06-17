using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for t24_perfilAction
/// </summary>
public class t24_perfilAction : SQLServerBase
{
    public t24_perfilAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<t24_perfil> ListObjTodos()
    {
        string query = "select * from t24_perfil " +
            "where fl_ativa=1 order by nu_ordem";
        DataTable dt = this.GetDataTable(query);
        List<t24_perfil> t24list = new List<t24_perfil>();
        foreach (DataRow dr in dt.Rows)
        {
            t24_perfil t24 = new t24_perfil();
            t24.t24_cd_perfil = Convert.ToInt32(dr["t24_cd_perfil"]);
            if (dr["nm_perfil"] != DBNull.Value)
                t24.nm_perfil = dr["nm_perfil"].ToString();
            if (dr["fl_perfil"] != DBNull.Value)
                t24.fl_perfil = dr["fl_perfil"].ToString();
            if (dr["ds_perfil"] != DBNull.Value)
                t24.ds_perfil = dr["ds_perfil"].ToString();
            t24list.Add(t24);
        }
        return t24list;
    }

    public List<t24_perfil> ListObjTodos(string cd_usuario)
    {
        string query = "select * from t24_perfil " +
            "where t24_cd_perfil in (select t24_cd_perfil from t25_usuarioperfil " +
            "where t02_cd_usuario='" + cd_usuario + "') and fl_ativa=1 order by nu_ordem";
        DataTable dt = this.GetDataTable(query);
        List<t24_perfil> t24list = new List<t24_perfil>();
        foreach (DataRow dr in dt.Rows)
        {
            t24_perfil t24 = new t24_perfil();
            t24.t24_cd_perfil = Convert.ToInt32(dr["t24_cd_perfil"]);
            if (dr["nm_perfil"] != DBNull.Value)
                t24.nm_perfil = dr["nm_perfil"].ToString();
            if (dr["fl_perfil"] != DBNull.Value)
                t24.fl_perfil = dr["fl_perfil"].ToString();
            if (dr["ds_perfil"] != DBNull.Value)
                t24.ds_perfil = dr["ds_perfil"].ToString();
            t24list.Add(t24);
        }
        return t24list;
    }

    public DataTable ListTodos()
    {
        string query = "select * from t24_perfil " +
            "where fl_ativa=1 " +
            "order by nu_ordem";
        return this.GetDataTable(query);
    }
}
