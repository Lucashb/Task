using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWeb.Dominio;
using TaskWeb.Repositorio;

namespace TaskWeb.Aplicacao
{
    public class AplicacaoSql
    {
        private contexto contexto;

        private ContextoTaskConsulta contextoTaskConsulta;

        public void Inserir(CadastroSql sql)
        {
            var strQuery = "";
            strQuery += " INSERT INTO SqlConsulta (is_sql,consulta_sql,descricao_sql) ";
            strQuery += string.Format(" VALUES ('{0}','{1}','{2}') ",
                sql.codigo,
                sql.consultasql,
                sql.descricao);
            using (contexto = new contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public string VerificaLogin(Usuarios user)
        {
            var strQuery = "";
            strQuery += "SELECT cd_usuario FROM seg_usuario";
            strQuery += string.Format(" WHERE cd_usuario = '{0}' ",
                user.Nome);

           
                using (contextoTaskConsulta = new ContextoTaskConsulta())
                {
                    contextoTaskConsulta.ExecutaComandoLogin(strQuery);
                    var retornoDataReader = contextoTaskConsulta.ExecutaComandoComRetorno(strQuery);
                    return RecuperaLogin(retornoDataReader);
                }           
        }

   

        private string RecuperaLogin(OracleDataReader reader)
        {
            string valor = null;

            while (reader.Read())
            {
                var temObjeto = new Usuarios()
                {
                    Nome = reader["cd_usuario"].ToString()

                };
                valor = temObjeto.Nome;
            }
            reader.Close();
            return valor;
        }

        //public object Inserir()
        //{
        //    throw new NotImplementedException();
        //}

        //private void Alterar(Aluno aluno)
        //{
        //    var strQuery = "";
        //    strQuery += " UPDATE ALUNO SET ";
        //    strQuery += string.Format(" Nome = '{0}', ", aluno.Nome);
        //    strQuery += string.Format(" Mae = '{0}', ", aluno.Mae);
        //    strQuery += string.Format(" DataNascimento = '{0}' ", aluno.DataNascimento);
        //    strQuery += string.Format(" WHERE AlunoId = {0} ", aluno.Id);
        //    using (contexto = new Contexto())
        //    {
        //        contexto.ExecutaComando(strQuery);
        //    }
        //}

        //public void Salvar(Aluno aluno)
        //{
        //    if (aluno.Id > 0)
        //        Alterar(aluno);
        //    else
        //        Inserir(aluno);
        //}

        //public void Excluir(int id)
        //{
        //    using (contexto = new Contexto())
        //    {
        //        var strQuery = string.Format(" DELETE FROM ALUNO WHERE AlunoId = {0}", id);
        //        contexto.ExecutaComando(strQuery);
        //    }
        //}

        public string TmaGeral(Usuarios user)
        {
            using (contextoTaskConsulta = new ContextoTaskConsulta())
            {
                //var strQuery = "select round(avg(round(avg(tma), 2)),4) TMA from(SELECT distinct t.cd_projeto,t.cd_tarefa,os.ds_tarefa,t.cd_tipotarefa,t.cd_responsavel,TO_DATE(os.dt_cadastro, 'DD/MM/YY') AS Dt_Cadastro, to_date(os.dh_entregacliente, 'dd/mm/yy') as dt_Entrega, (to_date(os.dh_entregacliente, 'dd/mm/yy') - TO_DATE(os.dt_cadastro, 'DD/MM/YY')) as tma, to_char(os.dh_entregacliente, 'MM') as MES_ENTREGA, to_char(os.dh_entregacliente, 'YYYY') as ANO_ENTREGA, os.cd_natureza, os.cd_equipe FROM TRIAGEM t, ordemservico os WHERE T.cd_projeto = os.cd_projeto AND t.cd_tarefa = os.cd_tarefa and os.cd_projeto not in ('ERP_CSERVICO', 'TREIN_ERP','ERP_SHS','SHS_INFRA', 'TREIN_EDIG', 'GINASTICA_LAB') and t.cd_equipe in (9, 13) and os.cd_natureza = (6) and os.dh_entregacliente between '01/11/2016' and '30/11/2016' AND T.CD_RESPONSAVEL = '" + user.Nome + "') group by cd_responsavel order by cd_responsavel";
                var strQuery = "select round(avg(round(avg(tma), 2)),4) TMA from(SELECT distinct t.cd_projeto,t.cd_tarefa,os.ds_tarefa,t.cd_tipotarefa,t.cd_responsavel,TO_DATE(os.dt_cadastro, 'DD/MM/YY') AS Dt_Cadastro, to_date(os.dh_entregacliente, 'dd/mm/yy') as dt_Entrega, (to_date(os.dh_entregacliente, 'dd/mm/yy') - TO_DATE(os.dt_cadastro, 'DD/MM/YY')) as tma, to_char(os.dh_entregacliente, 'MM') as MES_ENTREGA, to_char(os.dh_entregacliente, 'YYYY') as ANO_ENTREGA, os.cd_natureza, os.cd_equipe FROM TRIAGEM t, ordemservico os WHERE T.cd_projeto = os.cd_projeto AND t.cd_tarefa = os.cd_tarefa and os.cd_projeto not in ('ERP_CSERVICO', 'TREIN_ERP','ERP_SHS','SHS_INFRA', 'TREIN_EDIG', 'GINASTICA_LAB') and t.cd_equipe in (9, 13) and os.cd_natureza = (6) and os.dh_entregacliente between '01/11/2016' and '30/11/2016') group by cd_responsavel order by cd_responsavel";
                var retornoDataReader = contextoTaskConsulta.ExecutaComandoComRetorno(strQuery);
                return TransformaTma(retornoDataReader);

            }
        }

        public string TmtGeral(Usuarios user)
        {
            using (contextoTaskConsulta = new ContextoTaskConsulta())
            {
                //var strQuery = "select round(avg(round(avg(tma), 2)),4) TMA from(SELECT distinct t.cd_projeto,t.cd_tarefa,os.ds_tarefa,t.cd_tipotarefa,t.cd_responsavel,TO_DATE(os.dt_cadastro, 'DD/MM/YY') AS Dt_Cadastro, to_date(os.dh_entregacliente, 'dd/mm/yy') as dt_Entrega, (to_date(os.dh_entregacliente, 'dd/mm/yy') - TO_DATE(os.dt_cadastro, 'DD/MM/YY')) as tma, to_char(os.dh_entregacliente, 'MM') as MES_ENTREGA, to_char(os.dh_entregacliente, 'YYYY') as ANO_ENTREGA, os.cd_natureza, os.cd_equipe FROM TRIAGEM t, ordemservico os WHERE T.cd_projeto = os.cd_projeto AND t.cd_tarefa = os.cd_tarefa and os.cd_projeto not in ('ERP_CSERVICO', 'TREIN_ERP','ERP_SHS','SHS_INFRA', 'TREIN_EDIG', 'GINASTICA_LAB') and t.cd_equipe in (9, 13) and os.cd_natureza = (6) and os.dh_entregacliente between '01/11/2016' and '30/11/2016' AND T.CD_RESPONSAVEL = '" + user.Nome + "') group by cd_responsavel order by cd_responsavel";
                var strQuery = "select round(avg(round(avg(tma), 2)),4) TMA from(SELECT distinct t.cd_projeto,t.cd_tarefa,os.ds_tarefa,t.cd_tipotarefa,t.cd_responsavel,TO_DATE(os.dt_cadastro, 'DD/MM/YY') AS Dt_Cadastro, to_date(os.dh_entregacliente, 'dd/mm/yy') as dt_Entrega, (to_date(os.dh_entregacliente, 'dd/mm/yy') - TO_DATE(os.dt_cadastro, 'DD/MM/YY')) as tma, to_char(os.dh_entregacliente, 'MM') as MES_ENTREGA, to_char(os.dh_entregacliente, 'YYYY') as ANO_ENTREGA, os.cd_natureza, os.cd_equipe FROM TRIAGEM t, ordemservico os WHERE T.cd_projeto = os.cd_projeto AND t.cd_tarefa = os.cd_tarefa and os.cd_projeto not in ('ERP_CSERVICO', 'TREIN_ERP','ERP_SHS','SHS_INFRA', 'TREIN_EDIG', 'GINASTICA_LAB') and t.cd_equipe in (9, 13) and os.cd_natureza = (6) and os.dh_entregacliente between '01/11/2016' and '30/11/2016') group by cd_responsavel order by cd_responsavel";
                var retornoDataReader = contextoTaskConsulta.ExecutaComandoComRetorno(strQuery);
                return TransformaTma(retornoDataReader);

            }
        }

        private string TransformaTma(OracleDataReader reader)
        {
            string valor = null;

            while (reader.Read())
            {
                var temObjeto = new Indicadores()
                {
                    tmageral = reader["TMA"].ToString()
                   
                };
                valor = temObjeto.tmageral;
            }
            reader.Close();
            return valor;
         }
        

        public List<CadastroSql> ListarTodos()
        {
            using (contexto = new contexto())
            {
                var strQuery = " SELECT * FROM SqlConsulta ";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retornoDataReader);
            }
        }

        private List<CadastroSql> TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var sqls = new List<CadastroSql>();

            while (reader.Read())
            {
                var temObjeto = new CadastroSql()
                {
                    codigo = int.Parse(reader["is_sql"].ToString()),
                    descricao = reader["descricao_sql"].ToString(),
                    consultasql = reader["consulta_sql"].ToString()
                };
                sqls.Add(temObjeto);
            }
            reader.Close();
            return sqls;
        }

        //public Aluno ListarPorId(int id)
        //{
        //    using (contexto = new Contexto())
        //    {
        //        var strQuery = string.Format(" SELECT * FROM ALUNO WHERE AlunoId = {0} ", id);
        //        var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
        //        return TransformaReaderEmListaDeObjeto(retornoDataReader).FirstOrDefault();
        //    }
        //}

     
    }
}