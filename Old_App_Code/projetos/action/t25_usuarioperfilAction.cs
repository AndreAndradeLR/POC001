using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for t25_usuarioperfilAction
/// </summary>
public class t25_usuarioperfilAction : SQLServerBase
{
	public t25_usuarioperfilAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t25_usuarioperfil t25)
    {
        //INSERT INTO t25_usuarioperfil () VALUES(
        string query = "insert into t25_usuarioperfil "+
            "(t02_cd_usuario, t24_cd_perfil, dt_cadastro) "+
            "values(@t02_cd_usuario, @t24_cd_perfil, getdate())";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t25.t02_cd_usuario),
                MakeInParam("@t24_cd_perfil", SqlDbType.Int, 0, t25.t24_cd_perfil)
            };


        return this.RunCommand(query, param);
    }

    public int DeleteDB(string cd_usuario)
    {
        string query = "delete from t25_usuarioperfil " +
            " where t02_cd_usuario='" + cd_usuario + "'";

        return this.RunCommand(query);
    }
}
