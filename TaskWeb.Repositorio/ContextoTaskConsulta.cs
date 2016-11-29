using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;


namespace TaskWeb.Repositorio
{
    public class ContextoTaskConsulta: IDisposable
    {
        private readonly OracleConnection minhaConexao;

        public ContextoTaskConsulta()
        {
            minhaConexao = new OracleConnection(ConfigurationManager.ConnectionStrings["ConexaoTaskConsulta"].ConnectionString);
            minhaConexao.Open();
        }

        public void ExecutaComando(string strQuery)
        {
            var cmdComando = new OracleCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = minhaConexao
            };
            cmdComando.ExecuteNonQuery();
        }

        public void ExecutaComandoLogin(string strQuery)
        {
            var cmdComando = new OracleCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = minhaConexao
            };
            cmdComando.ExecuteScalar();
        }

        public OracleDataReader ExecutaComandoComRetorno(string strQuery)
        {
            var cmdComando = new OracleCommand(strQuery, minhaConexao);
            return cmdComando.ExecuteReader();
        }

        public void Dispose()
        {
            if (minhaConexao.State == ConnectionState.Open)
                minhaConexao.Close();
        }

    }
}
