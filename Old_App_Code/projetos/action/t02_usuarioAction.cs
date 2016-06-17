using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for t02_usuarioAction
/// </summary>
public class t02_usuarioAction : SQLServerBase
{
	public t02_usuarioAction() 
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t02_usuario t02)
    {
        //INSERT INTO t02_usuario () VALUES(
        string query = "insert into t02_usuario "+
            "(t02_cd_usuario, "+ 
            "t01_cd_entidade, "+
            "nm_nome, "+
            "nm_cargo, "+
            "nm_email, "+
            "nm_telefone, "+
            "nm_celular, "+
            "pw_senha, "+
            "dt_cadastro,"+
            "dt_alterado) " +
            "values"+
            "(@t02_cd_usuario, "+ 
            "@t01_cd_entidade, "+
            "@nm_nome, "+
            "@nm_cargo, "+
            "@nm_email, "+
            "@nm_telefone, "+
            "@nm_celular, "+
            "pwdencrypt(@pw_senha), " +
            "getdate(),"+
            "getdate()) ";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t02.t02_cd_usuario),
                MakeInParam("@t01_cd_entidade", SqlDbType.Int, 0, t02.t01_cd_entidade),
                MakeInParam("@nm_nome", SqlDbType.VarChar, 200, t02.nm_nome),
                MakeInParam("@nm_cargo", SqlDbType.VarChar, 100, t02.nm_cargo),
                MakeInParam("@nm_email", SqlDbType.VarChar, 100, t02.nm_email),
                MakeInParam("@nm_telefone", SqlDbType.VarChar, 20, t02.nm_telefone),
                MakeInParam("@nm_celular", SqlDbType.VarChar, 20, t02.nm_celular),
                MakeInParam("@pw_senha", SqlDbType.VarChar, 50, t02.pw_senha)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t02_usuario t02)
    {
        string query = "update t02_usuario set " +
            "t01_cd_entidade=@t01_cd_entidade, " +
            "nm_nome=@nm_nome, " +
            "nm_cargo=@nm_cargo, " +
            "nm_email=@nm_email, " +
            "nm_telefone=@nm_telefone, " +
            "nm_celular=@nm_celular, " +
            "dt_alterado=getdate() " +
            "where t02_cd_usuario=@t02_cd_usuario";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t02.t02_cd_usuario),
                MakeInParam("@t01_cd_entidade", SqlDbType.Int, 0, t02.t01_cd_entidade),
                MakeInParam("@nm_nome", SqlDbType.VarChar, 200, t02.nm_nome),
                MakeInParam("@nm_cargo", SqlDbType.VarChar, 100, t02.nm_cargo),
                MakeInParam("@nm_email", SqlDbType.VarChar, 100, t02.nm_email),
                MakeInParam("@nm_telefone", SqlDbType.VarChar, 20, t02.nm_telefone),
                MakeInParam("@nm_celular", SqlDbType.VarChar, 20, t02.nm_celular),
            };
        return this.RunCommand(query, param);
    }

    public int UpdateSenhaDB(t02_usuario t02)
    {
        string query = "update t02_usuario set pw_senha=pwdencrypt(@pw_senha), " + 
            "dt_alterado=getdate() where t02_cd_usuario=@t02_cd_usuario";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t02.t02_cd_usuario),
                MakeInParam("@pw_senha", SqlDbType.VarChar, 50, t02.pw_senha),
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(string id)
    {
        string query = "delete from t02_usuario " +
            " where t02_cd_usuario ='" + id + "'";

        return this.RunCommand(query);
    }

    public t02_usuario Retrieve(string id)
    {
        string query = "select * from t02_usuario where t02_cd_usuario='" + id + "'";
        DataTable dt = this.GetDataTable(query);
        t02_usuario t02 = new t02_usuario();
        foreach (DataRow dr in dt.Rows)
        {
            t02.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            t02.t24l = new t24_perfilAction().ListObjTodos(t02.t02_cd_usuario);

            if (dr["t01_cd_entidade"] != DBNull.Value)
            {
                t02.t01_cd_entidade = (int)dr["t01_cd_entidade"];
                t01_entidadeAction t01a = new t01_entidadeAction();
                t02.t01 = t01a.Retrieve(t02.t01_cd_entidade);
            }


            if (dr["nm_nome"] != DBNull.Value)
                t02.nm_nome = dr["nm_nome"].ToString();

            if (dr["nm_cargo"] != DBNull.Value)
                t02.nm_cargo = dr["nm_cargo"].ToString();

            if (dr["nm_email"] != DBNull.Value)
                t02.nm_email = dr["nm_email"].ToString();

            if (dr["nm_telefone"] != DBNull.Value)
                t02.nm_telefone = dr["nm_telefone"].ToString();

            if (dr["nm_celular"] != DBNull.Value)
                t02.nm_celular = dr["nm_celular"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t02.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t02.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t02;
    }

    public int ValidaSenha(t02_usuario t02)
    {
        string query = "select t02.*, " + 
            "pwdcompare(@pw_senha, convert(nvarchar(50), pw_senha), 0) " +
            "as resultado from t02_usuario t02 " + 
            "where (t02_cd_usuario=@t02_cd_usuario)";
        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t02.t02_cd_usuario),
                MakeInParam("@pw_senha", SqlDbType.VarChar, 50, t02.pw_senha)
            };
        DataTable dt = this.GetDataTable(query, param);
        int result = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["resultado"] != DBNull.Value)
                result = (int)dr["resultado"];
            break;
        }
        return result;
    }

    public List<t02_usuario> ListObjTodos()
    {
        string query = "select * from t02_usuario order by nm_nome";
        DataTable dt = this.GetDataTable(query);
        List<t02_usuario> t02list = new List<t02_usuario>();
        foreach (DataRow dr in dt.Rows)
        {
            t02_usuario t02 = new t02_usuario();
            t02.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            t02.t24l = new t24_perfilAction().ListObjTodos(t02.t02_cd_usuario);

            if (dr["nm_nome"] != DBNull.Value)
                t02.nm_nome = dr["nm_nome"].ToString();

            if (dr["nm_cargo"] != DBNull.Value)
                t02.nm_cargo = dr["nm_cargo"].ToString();

            if (dr["nm_email"] != DBNull.Value)
                t02.nm_email = dr["nm_email"].ToString();

            if (dr["nm_telefone"] != DBNull.Value)
                t02.nm_telefone = dr["nm_telefone"].ToString();

            if (dr["nm_celular"] != DBNull.Value)
                t02.nm_celular = dr["nm_celular"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t02.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t02.dt_alterado = (DateTime)dr["dt_alterado"];

            t02list.Add(t02);
        }
        return t02list;
    }

    //public DataTable ListTodos()
    //{
    //    string query = "select * from t02_usuario t02 "+
    //        "left join t01_entidade t01 "+
    //        "on t02.t01_cd_entidade=t01.t01_cd_entidade "+
    //        "order by nm_nome";
    //    return this.GetDataTable(query);
    //}

    public string DuplicidadeUsuario(string id)
    {
        string query = "select t02_cd_usuario from t02_usuario " +
                       "where t02_cd_usuario = '" + id + "'";
        DataTable dt = this.GetDataTable(query);
        string login = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["t02_cd_usuario"] != DBNull.Value)
                login = dr["t02_cd_usuario"].ToString();
            break;
        }
        return login;
    }

    public DataTable RetrievePerfil(string id)
    {
        string query = "select t02.t02_cd_usuario,t24.nm_perfil " +
                       "from t02_usuario t02 inner join t25_usuarioperfil t25 " +
                       "on t02.t02_cd_usuario = t25.t02_cd_usuario " +
                       "inner join t24_perfil t24 " +
                       "on t24.t24_cd_perfil = t25.t24_cd_perfil " +
                       "where t02.t02_cd_usuario = '" + id + "'";
        return this.GetDataTable(query);
    }    

    public DataTable ListTodos()
    {
        string query = "select t02.*,t01.nm_entidade "+
                       "from t02_usuario t02 left join t01_entidade t01 "+
                       "on t02.t01_cd_entidade=t01.t01_cd_entidade order by t02.nm_nome ";
        return this.GetDataTable(query);
    }

    public DataTable ListProjetoGerente(int cd_projeto,string cd_usuario)
    {
        string query = "select * from t03_projeto " +
                       "where t02_cd_usuario = '" + cd_usuario + "' " +
                       "and fl_deletado=0 and t03_cd_projeto=" + cd_projeto;
        return this.GetDataTable(query);
    }

    /*
     * VALIDAÇÂO ACESSO
     */

    //lista os projetos do qual ele é gerente, coordenador da acao ou linha decisoria
    public DataTable ListRelatorioSituacoes(string cd_usuario)
    {
        string query = "select * from t03_projeto " +
                       "where t02_cd_usuario='" + cd_usuario + "' " +
                       "or t03_cd_projeto in(select t03_cd_projeto from t06_colaborador where t02_cd_usuario='" + cd_usuario + "') " +
                       "or t03_cd_projeto in(select t03_cd_projeto from t08_acao where t02_cd_usuario='" + cd_usuario + "') " +
                       "and fl_deletado=0";
        return this.GetDataTable(query);
    }
    //verifica se é gerente de algum projeto
    public DataTable ListUsuarioGerente(string cd_usuario)
    {
        string query = "select * from t03_projeto " +
                       "where t02_cd_usuario = '" + cd_usuario + "' " +
                       "and fl_deletado=0";
        return this.GetDataTable(query);
    }
    //verifica se pertence a area de resultado do projeto
    public DataTable ListRestricaoFinanceiro(string cd_usuario, int cd_area)
    {
        string query = "select * from t03_projeto where t02_cd_usuario='" + cd_usuario + "' " +
                       "and t26_cd_arearesultado=" + cd_area + " ";
        return this.GetDataTable(query);
    }
    
    //verifica se pertence a linha decisória(linha gerencial) de algum projeto
    public DataTable ListLinhaGerencialUser(string cd_usuario)
    {
        string query = "select * from t06_colaborador where t02_cd_usuario='" + cd_usuario + "'";
        return this.GetDataTable(query);
    }
    //verifica se pertence a linha decisória(linha gerencial) do projeto selecionado
    public DataTable ListFinanceiroLinhaGerencial(string cd_usuario, int cd_projeto)
    {
        string query = "select * from t06_colaborador " +
                       "where t03_cd_projeto = " + cd_projeto + " and t02_cd_usuario='" + cd_usuario + "'";
        return this.GetDataTable(query);
    }
        
    //verifica se é cordenador de alguma ação de algum projeto
    public DataTable ListCoordenaAcao(string cd_usuario)
    {
        string query = "select * from t08_acao where t02_cd_usuario='"+cd_usuario+"'";
        return this.GetDataTable(query);
    }
    //verifica se é cordenador de alguma ação do projeto selecionado
    public DataTable ListRestricaoFinanceiroAcao(string cd_usuario, int cd_projeto)
    {
        string query = "select * from t08_acao " +
                       "where t03_cd_projeto = " + cd_projeto + " and t02_cd_usuario='" + cd_usuario + "'";
        return this.GetDataTable(query);
    }

    /*
     * FIM VALIDAÇÂO ACESSO
     */

    public DataTable ListProjetoPerfilGerente(string id)
    {
        string query = "select * " +
                       "from t03_projeto " +
                       "where t02_cd_usuario='" + id + "' and fl_deletado=0 order by nm_projeto";
        return this.GetDataTable(query);
    }

    public DataTable ListProjetoPerfilMonitora(string id)
    {
        string query = "select * " +
                       "from t03_projeto " +
                       "where t02_cd_usuario_monitoramento='" + id + "' and fl_deletado=0 order by nm_projeto";
        return this.GetDataTable(query);
    }

    public DataTable ListAcaoPerfilCoordenador(string id)
    {
        string query = "select t08.*, t03.nm_projeto " +
                       "from t08_acao t08 left join t03_projeto t03 " +
                       "on t03.t03_cd_projeto=t08.t03_cd_projeto " +
                       "where t08.t02_cd_usuario='" + id + "' and t08.fl_deletado=0 order by nm_projeto, nm_acao";
        return this.GetDataTable(query);
    }
    public DataTable ListLinhaGerencial(string id)
    {
        string query = "select t06.*, t03.nm_projeto " +
                       "from t06_colaborador t06 left join t03_projeto t03 " +
                       "on t03.t03_cd_projeto=t06.t03_cd_projeto " +
                       "where t06.t02_cd_usuario='" + id + "'  and t03.fl_deletado=0 order by t03.nm_projeto, t06.nm_funcao";
        return this.GetDataTable(query);
    }

    public string DadosUsuario(t02_usuario t02, int index)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        {
            if (t02.t02_cd_usuario != null)
            {
                sb.Append("<div class='userInfo'>");
                
                sb.Append("<div class='userName'><a href=\"javascript:switchMenu('userID" + index +
                    "');\" title='Detalhes de " + t02.nm_nome +
                    "' class=link >" + t02.nm_nome + "</a></div>");

                sb.Append("<div id='userID" + index + "' style='display:none'>");
                sb.Append("<div class='userMail'><a href=mailto:" + t02.nm_email + ">" +
                    t02.nm_email + "</a></div>");
                if ((t02.nm_telefone.Length > 0) || (t02.nm_celular.Length > 0))
                {
                    sb.Append("<div class='userFone'>");

                    if (t02.nm_telefone.Length > 0)
                    {
                        //sb.Append(" (" + NM_DDDT + ") " + NM_TELEFONE);

                        sb.Append(String.Format("{0:(##) ####-####}", Convert.ToInt64(t02.nm_telefone)) + " ");
                    }
                    if (t02.nm_celular.Length > 0)
                    {
                        //sb.Append(" (" + NM_DDDT + ") " + NM_TELEFONE);
                        sb.Append(String.Format("{0:(##) ####-####}", Convert.ToInt64(t02.nm_celular)) + " ");
                    }
                    sb.Append("</div>"); //userFone
                }
                sb.Append("<div class='userEntidade'>" + t02.t01.nm_entidade + "</div>");//userEntidade
                sb.Append("</div>"); //userID
                sb.Append("</div>"); //userInfo
            }
        }
        return sb.ToString();
    }

    //Lista os gerentes do projeto
    public DataTable ListGerenteDoProjeto(int cd_projeto)
    {
        string filter = string.Empty;

        if (cd_projeto > 0)
            filter = "and t03.t03_cd_projeto=" + cd_projeto;

        string query = "select distinct t02.* from t02_usuario t02 " +
                       "inner join t03_projeto t03 on t02.t02_cd_usuario = t03.t02_cd_usuario " +
                       "where fl_deletado=0 " + filter + " order by nm_nome";
        return this.GetDataTable(query);
    }

    //Lista os responsaveis da acao
    public DataTable ListResponsavelDaAcao(int cd_projeto)
    {
        string filter = string.Empty;

        if (cd_projeto > 0)
            filter = "and t08.t03_cd_projeto=" + cd_projeto;

        string query = "select distinct t02.* from t02_usuario t02 " +
                       "inner join t08_acao t08 on t02.t02_cd_usuario = t08.t02_cd_usuario " +
                       "where fl_deletado=0 " + filter + " order by nm_nome";
        return this.GetDataTable(query);
    }
}
