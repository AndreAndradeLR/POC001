using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Text;
/// <summary>
/// Summary description for t09_marcoAction
/// </summary>
public class t09_marcoAction : SQLServerBase
{
    pageBase pb = new pageBase();
	public t09_marcoAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t09_marco t09)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "insert into t09_marco (t03_cd_projeto, ds_marco, "+
            "nu_esforco, dt_prevista, ds_comentario, fl_status, "+
            "dt_cadastro, dt_alterado, fl_deletado) " +
            "values(@t03_cd_projeto, @ds_marco, @nu_esforco, "+
            "@dt_prevista, @ds_comentario, @fl_status, getdate(), getdate(), 0)";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t09.t03_cd_projeto),
                MakeInParam("@ds_marco", SqlDbType.Text, 0, t09.ds_marco),
                MakeInParam("@nu_esforco", SqlDbType.Int, 0, t09.nu_esforco),
                MakeInParam("@dt_prevista", SqlDbType.DateTime, 0, t09.dt_prevista),
                MakeInParam("@ds_comentario", SqlDbType.Text, 0, t09.ds_comentario),
                MakeInParam("@fl_status", SqlDbType.Char, 0, t09.fl_status)
            };
        return this.RunCommand(query, param);
    }

    public int UpdateDB(t09_marco t09)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t09_marco set " +
            " ds_marco=@ds_marco, nu_esforco=@nu_esforco, " +
            " dt_prevista=@dt_prevista, ds_comentario=@ds_comentario, "+
            " dt_realizada=@dt_realizada, " +
            " dt_alterado=getdate() " +
            "where t09_cd_marco=@t09_cd_marco";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t09_cd_marco", SqlDbType.Int, 0, t09.t09_cd_marco),
                MakeInParam("@ds_marco", SqlDbType.Text, 0, t09.ds_marco),
                MakeInParam("@nu_esforco", SqlDbType.Int, 0, t09.nu_esforco),
                MakeInParam("@dt_prevista", SqlDbType.DateTime, 0, t09.dt_prevista),
                MakeInParam("@dt_realizada", SqlDbType.DateTime, 0, t09.dt_realizada),
                MakeInParam("@ds_comentario", SqlDbType.Text, 0, t09.ds_comentario),
                MakeInParam("@fl_status", SqlDbType.Char, 0, t09.fl_status)
            };
        return this.RunCommand(query, param);
    }
    public int UpdateDtRealizadaNullDB(t09_marco t09)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t09_marco set " +
            " ds_marco=@ds_marco, nu_esforco=@nu_esforco, " +
            " dt_prevista=@dt_prevista, ds_comentario=@ds_comentario, " +
            " dt_realizada=null, " +
            " dt_alterado=getdate() " +
            "where t09_cd_marco=@t09_cd_marco";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t09_cd_marco", SqlDbType.Int, 0, t09.t09_cd_marco),
                MakeInParam("@ds_marco", SqlDbType.Text, 0, t09.ds_marco),
                MakeInParam("@nu_esforco", SqlDbType.Int, 0, t09.nu_esforco),
                MakeInParam("@dt_prevista", SqlDbType.DateTime, 0, t09.dt_prevista),
                MakeInParam("@ds_comentario", SqlDbType.Text, 0, t09.ds_comentario),
                MakeInParam("@fl_status", SqlDbType.Char, 0, t09.fl_status)
            };
        return this.RunCommand(query, param);
    }

    public int UpdateCorBarra()
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        
        //MUDA PARA VERDE OS MARCOS QUE NÃO TEM RESTRIÇÕES VINCULADAS
        sb.Append("update t09_marco set fl_status='G' where fl_deletado=0 and "+
            "t09_cd_marco not in (select t09_cd_marco from t19_marcorestricao "+
             "where t07_cd_restricao " +
             "in (select t07_cd_restricao from t07_restricao " +
             "where fl_deletado=0)); ");
        
        //MUDA PARA VERDE MARCOS QUE ESTAVAM AMARELOS E QUE FORAM SUPERADOS
        sb.Append("update t09_marco set fl_status='G' where t09_cd_marco in (select t09_cd_marco from t19_marcorestricao " +
                            "where t07_cd_restricao " +
                            "in (select t07_cd_restricao from t07_restricao " +
                            "where (dt_superada is not null) and (fl_deletado=0))); ");

        //MUDA PARA AMARELO MARCOS QUE ESTÃO COM RESTRIÇÕES NÃO SUPERADAS
        sb.Append("update t09_marco set fl_status='Y' where t09_cd_marco in (select t09_cd_marco from t19_marcorestricao " +
                            "where t07_cd_restricao " +
                            "in (select t07_cd_restricao from t07_restricao " +
                            "where (dt_superada is null) and (fl_deletado=0))); ");

        //MUDA PARA VERDE MARCOS QUE ESTAVAM VERMELHAS E QUE FORAM REALIZADOS
        sb.Append("update t09_marco set fl_status='G' where fl_status = 'R' AND dt_realizada is not null; ");

        //MUDA PARA AZUL MARCOS QUE FORAM REALIZADOS
        sb.Append("update t09_marco set fl_status='B' where dt_realizada is not null; ");

        //MUDA PARA VERMELHA MARCOS QUE ESTAVAM VERDES OU AMARELAS E QUE ULTRAPASSARAM O PRAZO PARA REALIZAÇÃO
        sb.Append("update t09_marco set fl_status='R' where dt_prevista < getdate()-1 and dt_realizada is null;");
        
        string query = sb.ToString();
        return this.RunCommand(query);
    }

    public int UpdateCorBarra(int cd_projeto)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //MUDA PARA VERDE OS MARCOS QUE NÃO TEM RESTRIÇÕES VINCULADAS
        sb.Append("update t09_marco set fl_status='G' where fl_deletado=0 and " +
            "t03_cd_projeto=" + cd_projeto + " and "+
            "t09_cd_marco not in (select t09_cd_marco from t19_marcorestricao "+
            "where t07_cd_restricao " +
             "in (select t07_cd_restricao from t07_restricao " +
             "where fl_deletado=0)); ");

        //MUDA PARA VERDE MARCOS QUE ESTAVAM AMARELOS E QUE FORAM SUPERADOS
        sb.Append("update t09_marco set fl_status='G' where  t03_cd_projeto=" + cd_projeto +
            " and t09_cd_marco in (select t09_cd_marco from t19_marcorestricao " +
                            "where t07_cd_restricao " +
                            "in (select t07_cd_restricao from t07_restricao " +
                            "where (dt_superada is not null) and (fl_deletado=0))); ");

        //MUDA PARA AMARELO MARCOS QUE ESTÃO COM RESTRIÇÕES NÃO SUPERADAS
        sb.Append("update t09_marco set fl_status='Y' where t03_cd_projeto=" + cd_projeto + 
            " and t09_cd_marco in (select t09_cd_marco from t19_marcorestricao " +
                            "where t07_cd_restricao " +
                            "in (select t07_cd_restricao from t07_restricao " +
                            "where (dt_superada is null) and (fl_deletado=0))); ");

        //MUDA PARA VERDE MARCOS QUE ESTAVAM VERMELHAS E QUE FORAM REALIZADOS
        sb.Append("update t09_marco set fl_status='G' where t03_cd_projeto=" + cd_projeto + 
            " and fl_status = 'R' AND dt_realizada is not null; ");

        //MUDA PARA AZUL MARCOS QUE FORAM REALIZADOS
        sb.Append("update t09_marco set fl_status='B' where t03_cd_projeto=" + cd_projeto + 
            " and dt_realizada is not null; ");

        //MUDA PARA VERMELHA MARCOS QUE ESTAVAM VERDES OU AMARELAS E QUE ULTRAPASSARAM O PRAZO PARA REALIZAÇÃO
        sb.Append("update t09_marco set fl_status='R' where t03_cd_projeto=" + cd_projeto + 
            " and dt_prevista < getdate()-1 and dt_realizada is null; ");


        string query = sb.ToString();
        return this.RunCommand(query);
    }

    public int UpdateCorRestricao(int cd_marco)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t09_marco set fl_status='R' where t09_cd_marco = "+ cd_marco; 
        return this.RunCommand(query);
    }

    public int DeleteDB(int id)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t09_marco set  " +
            " fl_deletado=1 " +
            " where t09_cd_marco =" + id;

        return this.RunCommand(query);
    }

    public t09_marco Retrieve(int id)
    {
        string query = "select * from t09_marco where t09_cd_marco=" + id;
        DataTable dt = this.GetDataTable(query);
        t09_marco t09 = new t09_marco();
        foreach (DataRow dr in dt.Rows)
        {
            t09.t09_cd_marco = int.Parse(dr["t09_cd_marco"].ToString());

            if (dr["ds_marco"] != DBNull.Value)
                t09.ds_marco = dr["ds_marco"].ToString();

            if (dr["nu_esforco"] != DBNull.Value)
                t09.nu_esforco = Convert.ToInt32(dr["nu_esforco"]);

            if (dr["dt_prevista"] != DBNull.Value)
                t09.dt_prevista = (DateTime)dr["dt_prevista"];

            if (dr["dt_realizada"] != DBNull.Value)
                t09.dt_realizada = (DateTime)dr["dt_realizada"];

            if (dr["ds_comentario"] != DBNull.Value)
                t09.ds_comentario = dr["ds_comentario"].ToString();

            if (dr["fl_status"] != DBNull.Value)
                t09.fl_status = dr["fl_status"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t09.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t09.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t09;
    }

    public int RetrieveDatas(string inicio, string fim, int cd_projeto)
    {
        int result = 0;
        string query = "select * from t09_marco where t03_cd_projeto = @t03_cd_projeto "+
                       "and dt_prevista not between @dt_inicio and @dt_fim and fl_deletado=0";
        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, cd_projeto),
                MakeInParam("@dt_inicio", SqlDbType.DateTime, 0, DateTime.Parse(inicio)),
                MakeInParam("@dt_fim", SqlDbType.DateTime, 0, DateTime.Parse(fim))
            };
         DataTable dt = this.GetDataTable(query,param);
         t09_marco t09 = new t09_marco();
         foreach (DataRow dr in dt.Rows)
         {
             t09.t09_cd_marco = int.Parse(dr["t09_cd_marco"].ToString());                     
            result = 1;
            break;
        }
        return result;
    }

    public List<t09_marco> ListObjRestricao(int cd_restricao)
    {
        string query = "select * from t09_marco "+
                "where  t09_cd_marco in "+
                "(select t09_cd_marco from t19_marcorestricao where "+
                "t07_cd_restricao="+cd_restricao+" and fl_deletado=0)";

        DataTable dt = this.GetDataTable(query);
        List<t09_marco> t09list = new List<t09_marco>();
        foreach (DataRow dr in dt.Rows)
        {
            t09_marco t09 = new t09_marco();
            t09.t09_cd_marco = int.Parse(dr["t09_cd_marco"].ToString());
            t09.ds_marco = dr["ds_marco"].ToString();
            t09list.Add(t09);
        }
        return t09list;
    }

    public List<t09_marco> ListObjTodos(int cd_projeto)
    {
        string query = "select * from t09_marco where t03_cd_projeto=" + cd_projeto +
            " and fl_deletado=0 order by dt_prevista desc";
        DataTable dt = this.GetDataTable(query);
        List<t09_marco> t09list = new List<t09_marco>();
        foreach (DataRow dr in dt.Rows)
        {
            t09_marco t09 = new t09_marco();
            t09.t09_cd_marco = int.Parse(dr["t09_cd_marco"].ToString());
            t09.ds_marco = dr["ds_marco"].ToString();
            t09list.Add(t09);
        }
        return t09list;
    }


    public List<t09_marco> ListObjTodosMarcos(int cd_projeto, string status)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from t09_marco where t03_cd_projeto=" + cd_projeto);        
        if (status != "")
        {
            sb.Append(" and fl_status='" + status + "'");
        }
        sb.Append(" and fl_deletado=0 order by dt_prevista desc ");

        DataTable dt = this.GetDataTable(sb.ToString());
        List<t09_marco> t09list = new List<t09_marco>();

        foreach (DataRow dr in dt.Rows)
        {
            t09_marco t09 = new t09_marco();

            t09.t09_cd_marco = int.Parse(dr["t09_cd_marco"].ToString());

            if (dr["ds_marco"] != DBNull.Value)
                t09.ds_marco = dr["ds_marco"].ToString();

            if (dr["nu_esforco"] != DBNull.Value)
                t09.nu_esforco = Convert.ToInt32(dr["nu_esforco"]);

            if (dr["dt_prevista"] != DBNull.Value)
                t09.dt_prevista = (DateTime)dr["dt_prevista"];

            if (dr["dt_realizada"] != DBNull.Value)
                t09.dt_realizada = (DateTime)dr["dt_realizada"];

            if (dr["ds_comentario"] != DBNull.Value)
                t09.ds_comentario = dr["ds_comentario"].ToString();

            if (dr["fl_status"] != DBNull.Value)
                t09.fl_status = dr["fl_status"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t09.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t09.dt_alterado = (DateTime)dr["dt_alterado"];

            t09list.Add(t09);
        }        
        return t09list;
    }

    public List<t09_marco> ListMarcosDaRestricao(int cd_restricao)
    {
        try
        {
            string query = "select t09.ds_marco from t07_restricao t07 " +
                           "inner join t19_marcorestricao t19 " +
                           "on t19.t07_cd_restricao=t07.t07_cd_restricao " +
                           "inner join t09_marco t09 " +
                           "on t09.t09_cd_marco=t19.t09_cd_marco " +
                           "where t07.t07_cd_restricao=" + cd_restricao;

            DataTable dt = this.GetDataTable(query);
            List<t09_marco> t09List = new List<t09_marco>();
            foreach (DataRow dr in dt.Rows)
            {
                t09_marco t09 = new t09_marco();

                if (dr["ds_marco"] != DBNull.Value)
                    t09.ds_marco = dr["ds_marco"].ToString();

                t09List.Add(t09);
            }
            return t09List;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public DataTable ListRestricoesDoMarco(int cd_projeto, string status)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("select CONVERT(varchar(8000),t09.ds_marco)as Descricao ,t09.t03_cd_projeto, t09.t09_cd_marco,t09.fl_status, t09.dt_prevista ");
        sb.Append("from  t09_marco t09 ");
        sb.Append("where t09.fl_deletado=0 ");
        sb.Append(" and t09.t03_cd_projeto='" + cd_projeto + "' ");
        if (status != "")
        {
            if (status == "Y")
            {
                sb.Append(" and ((t09.fl_status='" + status + "')");
                sb.Append(" OR ");
                sb.Append(" t09.t09_cd_marco in(select t09_cd_marco from t19_marcorestricao ");
                sb.Append(" where t07_cd_restricao in (select t07_cd_restricao from t07_restricao where fl_deletado=0) ))");
            }
            else
            {
                sb.Append(" and t09.fl_status='" + status + "'");
            }
        }
        sb.Append(" order by t09.dt_prevista asc ");
        string a = sb.ToString();
        return this.GetDataTable(sb.ToString());
    }

    public DataTable ListTodos(int cd_projeto)
    {
        string query = "select * from t09_marco where t03_cd_projeto="+cd_projeto +
            " and fl_deletado=0 order by dt_prevista desc";
        return this.GetDataTable(query);
    }
    public DataTable ListStatusAzul(int cd_projeto)
    {
        string query = "select * from t09_marco where t03_cd_projeto=" + cd_projeto +
            " and fl_deletado=0 and fl_status='B' order by dt_prevista desc";
        return this.GetDataTable(query);
    }
    public DataTable ListStatusAmarelo(int cd_projeto)
    {
        string query = "select * from t09_marco where t03_cd_projeto=" + cd_projeto +
            " and fl_deletado=0 and fl_status='Y' order by dt_prevista desc";
        return this.GetDataTable(query);
    }
    public DataTable ListStatusVerde(int cd_projeto)
    {
        string query = "select * from t09_marco where t03_cd_projeto=" + cd_projeto +
            " and fl_deletado=0 and fl_status='G' order by dt_prevista desc";
        return this.GetDataTable(query);
    }
    public DataTable ListStatusVermelho(int cd_projeto)
    {
        string query = "select * from t09_marco where t03_cd_projeto=" + cd_projeto +
            " and fl_deletado=0 and fl_status='R' order by dt_prevista desc";
        return this.GetDataTable(query);
    }
    public string Status(int cd_projeto)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        StatusProjeto stp = new StatusProjeto();
        stp.QtdVermelho = 0;
        stp.QtdAzul = 0;
        stp.QtdAmarelo = 0;
        stp.QtdVerde = 0;
        DataTable List = ListTodos(cd_projeto);
        foreach (DataRow dr in List.Rows)
        {
            switch (dr["fl_status"].ToString())
            {
                //atrasado
                case "R":
                    stp.QtdVermelho += Convert.ToInt32(dr["nu_esforco"]);
                    break;
                //no prazo
                case "G":
                    stp.QtdVerde += Convert.ToInt32(dr["nu_esforco"]);
                    break;
                //concluido
                case "B":
                    stp.QtdAzul += Convert.ToInt32(dr["nu_esforco"]);
                    break;
                //com restrição
                case "Y":
                    stp.QtdAmarelo += Convert.ToInt32(dr["nu_esforco"]);
                    break;
            }
        }
        return stp.tbGraficoStatus();
    }

}
