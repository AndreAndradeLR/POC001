using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Text;
/// <summary>
/// Summary description for t07_restricaoAction
/// </summary>
public class t07_restricaoAction : SQLServerBase
{
    pageBase pb = new pageBase();
	public t07_restricaoAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t07_restricao t07)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        //INSERT INTO t07_restricao () VALUES(
        string query = "insert into t07_restricao ( "+
            "t03_cd_projeto, t02_cd_usuario, ds_restricao, dt_limite, " +
            "dt_cadastro, dt_alterado, fl_deletado) " +
            "values(@t03_cd_projeto, @t02_cd_usuario, @ds_restricao, " +
            "@dt_limite, getdate(), getdate(), 0)";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t07.t03_cd_projeto),
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t07.t02_cd_usuario),
                MakeInParam("@ds_restricao", SqlDbType.Text, 0, t07.ds_restricao),
                //MakeInParam("@ds_medida", SqlDbType.Text, 0, t07.ds_medida),
                MakeInParam("@dt_limite", SqlDbType.DateTime, 0, t07.dt_limite)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t07_restricao t07)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t07_restricao set " +
            "ds_restricao=@ds_restricao, " +
            "dt_alterado=getdate(), " +
            "t02_cd_usuario=@t02_cd_usuario " +
            "where t07_cd_restricao=@t07_cd_restricao";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t07_cd_restricao", SqlDbType.Int, 0, t07.t07_cd_restricao),
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t07.t02_cd_usuario),
                MakeInParam("@ds_restricao", SqlDbType.Text, 0, t07.ds_restricao)
                //MakeInParam("@ds_medida", SqlDbType.Text, 0, t07.ds_medida),
                //MakeInParam("@dt_limite", SqlDbType.DateTime, 0, t07.dt_limite)
            };
        return this.RunCommand(query, param);
    }
    public int UpdateSuperarDB(int cd_restricao)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t07_restricao set " +
            "dt_superada=getdate(), dt_alterado=getdate()  " +
            "where t07_cd_restricao=@t07_cd_restricao";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t07_cd_restricao", SqlDbType.Int, 0, cd_restricao)
            };
        return this.RunCommand(query, param);
    }

    public int UpdateDataLimite(int cd_restricao, DateTime DataLimiteProvidencia)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t07_restricao set " +
            "dt_limite=@dt_limite  where t07_cd_restricao=@t07_cd_restricao";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t07_cd_restricao", SqlDbType.Int, 0, cd_restricao),
                MakeInParam("@dt_limite", SqlDbType.DateTime, 0, DataLimiteProvidencia)
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t07_restricao set  " +
            " fl_deletado=1 " +
            " where t07_cd_restricao =" + id;

        return this.RunCommand(query);
    }

    public int RetrieveIDENTITY(t07_restricao t07)
    {
        string query = "select top 1 * from t07_restricao where dt_limite=@dt_limite " +
            " and fl_deletado=0 order by t07_cd_restricao desc";
        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@dt_limite", SqlDbType.DateTime, 0, t07.dt_limite)
            };
        DataTable dt = this.GetDataTable(query, param);
        int identity = 0;
        foreach (DataRow dr in dt.Rows)
        {
            identity = int.Parse(dr["t07_cd_restricao"].ToString());
            break;
        }
        return identity;
    }


    public t07_restricao Retrieve(int id)
    {
        string query = "select * from t07_restricao where t07_cd_restricao=" + id;
        DataTable dt = this.GetDataTable(query);
        t07_restricao t07 = new t07_restricao();
        foreach (DataRow dr in dt.Rows)
        {
            t07.t07_cd_restricao = int.Parse(dr["t07_cd_restricao"].ToString());

            t07.t09 = new t09_marcoAction().ListObjRestricao(t07.t07_cd_restricao);

            if (dr["ds_restricao"] != DBNull.Value)
                t07.ds_restricao = dr["ds_restricao"].ToString();
            
            if (dr["t03_cd_projeto"] != DBNull.Value)
                t07.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t07.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["dt_superada"] != DBNull.Value)
                t07.dt_superada = (DateTime)dr["dt_superada"];
            
            if (dr["dt_limite"] != DBNull.Value)
                t07.dt_limite = (DateTime)dr["dt_limite"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t07.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t07.dt_alterado = (DateTime)dr["dt_alterado"];



            break;
        }
        return t07;
    }

    public List<t07_restricao> ListObjTodos(int cd_projeto)
    {
        string query = "select * from t07_restricao "+
            "where t03_cd_projeto="+cd_projeto+" and fl_deletado=0 "+
            "order by dt_limite desc";
        DataTable dt = this.GetDataTable(query);
        List<t07_restricao> t07list = new List<t07_restricao>();
        foreach (DataRow dr in dt.Rows)
        {
            t07_restricao t07 = new t07_restricao();
            t07.t07_cd_restricao = int.Parse(dr["t07_cd_restricao"].ToString());
            if (dr["ds_restricao"] != DBNull.Value)
                t07.ds_restricao = dr["ds_restricao"].ToString();

            if (dr["t03_cd_projeto"] != DBNull.Value)
                t07.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t07.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["dt_superada"] != DBNull.Value)
                t07.dt_superada = (DateTime)dr["dt_superada"];

            if (dr["dt_limite"] != DBNull.Value)
                t07.dt_limite = (DateTime)dr["dt_limite"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t07.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t07.dt_alterado = (DateTime)dr["dt_alterado"];
            t07list.Add(t07);
        }
        return t07list;
    }    

    public DataTable ListTodos(int cd_projeto)
    {
        string query = "select * from t07_restricao "+
            "where t03_cd_projeto=" + cd_projeto + " and fl_deletado=0 and dt_superada is null " +
            "order by dt_limite desc";
        return this.GetDataTable(query);
    }

    public List<t07_restricao> ListTodasRestricoes(int cd_projeto)
    {
        string query = "select * from t07_restricao " +
            "where t03_cd_projeto=" + cd_projeto + " and fl_deletado=0 " +
            "order by dt_limite desc";        
        DataTable dt = this.GetDataTable(query);
        List<t07_restricao> t07list = new List<t07_restricao>();
        foreach (DataRow dr in dt.Rows)
        {
            t07_restricao t07 = new t07_restricao();
            t07.t07_cd_restricao = int.Parse(dr["t07_cd_restricao"].ToString());
            if (dr["ds_restricao"] != DBNull.Value)
                t07.ds_restricao = dr["ds_restricao"].ToString();

            if (dr["t03_cd_projeto"] != DBNull.Value)
                t07.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t07.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["dt_superada"] != DBNull.Value)
                t07.dt_superada = (DateTime)dr["dt_superada"];

            if (dr["dt_limite"] != DBNull.Value)
                t07.dt_limite = (DateTime)dr["dt_limite"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t07.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t07.dt_alterado = (DateTime)dr["dt_alterado"];
            t07list.Add(t07);
        }
        return t07list; ;
    }

    public List<t07_restricao> ListRestricoesSuperadas(int cd_projeto)
    {
        string query = "select * from t07_restricao " +
            "where t03_cd_projeto=" + cd_projeto + " and fl_deletado=0 and dt_superada is not null " +
            "order by dt_superada desc";        
        DataTable dt = this.GetDataTable(query);
        List<t07_restricao> t07list = new List<t07_restricao>();
        foreach (DataRow dr in dt.Rows)
        {
            t07_restricao t07 = new t07_restricao();
            t07.t07_cd_restricao = int.Parse(dr["t07_cd_restricao"].ToString());
            if (dr["ds_restricao"] != DBNull.Value)
                t07.ds_restricao = dr["ds_restricao"].ToString();

            if (dr["t03_cd_projeto"] != DBNull.Value)
                t07.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t07.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["dt_superada"] != DBNull.Value)
                t07.dt_superada = (DateTime)dr["dt_superada"];

            if (dr["dt_limite"] != DBNull.Value)
                t07.dt_limite = (DateTime)dr["dt_limite"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t07.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t07.dt_alterado = (DateTime)dr["dt_alterado"];
            t07list.Add(t07);
        }
        return t07list; ;
    }

    public List<t07_restricao> ListRestricoesdoMarco(int cd_projeto, int cd_marco)
    {
        string query = "select * from t07_restricao t07 " +
                       "left join t19_marcorestricao t19 on  t19.t07_cd_restricao=t07.t07_cd_restricao " +
                       "left join t09_marco t09 on t09.t09_cd_marco=t19.t09_cd_marco " +
                       "Where t09.t03_cd_projeto='" + cd_projeto + "' And t09.t09_cd_marco='" + cd_marco + "' " +
                       "And t07.fl_deletado=0 order by dt_superada desc";

        DataTable dt = this.GetDataTable(query);
        List<t07_restricao> t07list = new List<t07_restricao>();
        foreach (DataRow dr in dt.Rows)
        {
            t07_restricao t07 = new t07_restricao();
            t07.t07_cd_restricao = int.Parse(dr["t07_cd_restricao"].ToString());
            if (dr["ds_restricao"] != DBNull.Value)
                t07.ds_restricao = dr["ds_restricao"].ToString();

            if (dr["t03_cd_projeto"] != DBNull.Value)
                t07.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t07.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["dt_superada"] != DBNull.Value)
                t07.dt_superada = (DateTime)dr["dt_superada"];

            if (dr["dt_limite"] != DBNull.Value)
                t07.dt_limite = (DateTime)dr["dt_limite"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t07.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t07.dt_alterado = (DateTime)dr["dt_alterado"];
            t07list.Add(t07);
        }
        return t07list; ;
    }

    public DataTable ListProvidenciasRestricoes(int cd_projeto, List<String> lstFiltros)
    {
        string query = MontaQueryFiltrosRestricao(cd_projeto,lstFiltros);         
        return this.GetDataTable(query);
    }

    public string MontaQueryFiltrosRestricao(int cd_projeto, List<string> lstFiltros)
    {
        //string FiltroArea = "";
        //string FiltroProjeto = "";
        string FiltroData = "";
        string FiltroEstado = "";

        if (lstFiltros.Count() > 0)
        {
            //if (lstFiltros[0] != "") FiltroArea = " and t26_cd_arearesultado =" + lstFiltros[0];
            //if (lstFiltros[1] != "") FiltroProjeto = " and t03_cd_projeto =" + lstFiltros[1];

            switch (lstFiltros[2])
            {
                //Igual a Data Atual" Value="1"
                case "1":
                    FiltroData = " and CAST( ( STR( YEAR( t07.dt_limite ) ) + '/' +  STR( MONTH( t07.dt_limite ) ) + '/' + STR( DAY( t07.dt_limite ) )  ) AS DATETIME ) " +
                                 "  =  CAST( ( STR( YEAR( GETDATE() ) ) + '/' +  STR( MONTH( GETDATE() ) ) + '/' + STR( DAY( GETDATE() ) )  ) AS DATETIME )";                    
                    break;
                //Maior ou Igual a Data Atual" Value="2"
                case "2":
                    FiltroData = " and CAST( ( STR( YEAR( t07.dt_limite ) ) + '/' +  STR( MONTH( t07.dt_limite ) ) + '/' + STR( DAY( t07.dt_limite ) )  ) AS DATETIME ) " +
                                 "  >=  CAST( ( STR( YEAR( GETDATE() ) ) + '/' +  STR( MONTH( GETDATE() ) ) + '/' + STR( DAY( GETDATE() ) )  ) AS DATETIME )";              
                    break;
                //"Menor ou Igual a Data Atual" Value="3"
                case "3":
                    FiltroData = " and CAST( ( STR( YEAR( t07.dt_limite ) ) + '/' +  STR( MONTH( t07.dt_limite ) ) + '/' + STR( DAY( t07.dt_limite ) )  ) AS DATETIME ) " +
                                 "  <=  CAST( ( STR( YEAR( GETDATE() ) ) + '/' +  STR( MONTH( GETDATE() ) ) + '/' + STR( DAY( GETDATE() ) )  ) AS DATETIME )";              
                    break;
                default:
                    FiltroData = "";
                    break;
            }

            if (lstFiltros[4] != "")
            {
                if (lstFiltros[4] == "Superadas")
                    FiltroEstado = " and t07.dt_superada is not null";
                else
                    FiltroEstado = " and t07.dt_superada is null";
            }

        }

        string sql = String.Format("select * from t23_providencia t23 " +
                       "inner join t07_restricao t07 " +
                       "on t07.t07_cd_restricao=t23.t07_cd_restricao " +
                       "where t07.t03_cd_projeto=" + cd_projeto + " "+
                       " {0} {1} " +
                       "and t07.fl_deletado=0 " +
                       "order by t07.dt_limite desc", FiltroData, FiltroEstado);

        return sql;
    }


    public DataTable ListSuperadas(int cd_projeto)
    {
        string query = "select * from t07_restricao " +
            "where t03_cd_projeto=" + cd_projeto + " and fl_deletado=0 and dt_superada is not null " +
            "order by dt_superada desc";
        return this.GetDataTable(query);
    }

    public List<t07_restricao> ListObjMonVermelho(int cd_projeto)
    {
        string query = "select * from t07_restricao " +
            "where t03_cd_projeto=" + cd_projeto + " and fl_deletado=0 " +
            "and dt_limite < getdate() and dt_superada is null "+ 
            "order by dt_limite desc";
        DataTable dt = this.GetDataTable(query);
        List<t07_restricao> t07list = new List<t07_restricao>();
        foreach (DataRow dr in dt.Rows)
        {
            t07_restricao t07 = new t07_restricao();
            t07.t07_cd_restricao = int.Parse(dr["t07_cd_restricao"].ToString());
            if (dr["ds_restricao"] != DBNull.Value)
                t07.ds_restricao = dr["ds_restricao"].ToString();

            if (dr["t03_cd_projeto"] != DBNull.Value)
                t07.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t07.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["dt_superada"] != DBNull.Value)
                t07.dt_superada = (DateTime)dr["dt_superada"];

            if (dr["dt_limite"] != DBNull.Value)
                t07.dt_limite = (DateTime)dr["dt_limite"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t07.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t07.dt_alterado = (DateTime)dr["dt_alterado"];
            t07list.Add(t07);
        }
        return t07list; ;
    }
    public List<t07_restricao> ListObjMonVerde(int cd_projeto)
    {
        string query = "select * from t07_restricao " +
            "where t03_cd_projeto=" + cd_projeto + " and fl_deletado=0 " +
            "and dt_limite > getdate() " + 
            "order by dt_limite desc";
        DataTable dt = this.GetDataTable(query);
        List<t07_restricao> t07list = new List<t07_restricao>();
        foreach (DataRow dr in dt.Rows)
        {
            t07_restricao t07 = new t07_restricao();
            t07.t07_cd_restricao = int.Parse(dr["t07_cd_restricao"].ToString());
            if (dr["ds_restricao"] != DBNull.Value)
                t07.ds_restricao = dr["ds_restricao"].ToString();

            if (dr["t03_cd_projeto"] != DBNull.Value)
                t07.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t07.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["dt_superada"] != DBNull.Value)
                t07.dt_superada = (DateTime)dr["dt_superada"];

            if (dr["dt_limite"] != DBNull.Value)
                t07.dt_limite = (DateTime)dr["dt_limite"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t07.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t07.dt_alterado = (DateTime)dr["dt_alterado"];
            t07list.Add(t07);
        }
        return t07list;
    }
    public List<t07_restricao> ListObjMonAzul(int cd_projeto)
    {
        string query = "select * from t07_restricao " +
            "where t03_cd_projeto=" + cd_projeto + " and fl_deletado=0 " +
            "and dt_superada is not null " + 
            "order by dt_limite desc";
        DataTable dt = this.GetDataTable(query);
        List<t07_restricao> t07list = new List<t07_restricao>();
        foreach (DataRow dr in dt.Rows)
        {
            t07_restricao t07 = new t07_restricao();
            t07.t07_cd_restricao = int.Parse(dr["t07_cd_restricao"].ToString());
            if (dr["ds_restricao"] != DBNull.Value)
                t07.ds_restricao = dr["ds_restricao"].ToString();

            if (dr["t03_cd_projeto"] != DBNull.Value)
                t07.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t07.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["dt_superada"] != DBNull.Value)
                t07.dt_superada = (DateTime)dr["dt_superada"];

            if (dr["dt_limite"] != DBNull.Value)
                t07.dt_limite = (DateTime)dr["dt_limite"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t07.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t07.dt_alterado = (DateTime)dr["dt_alterado"];
            t07list.Add(t07);
        }
        return t07list;
    }
}
