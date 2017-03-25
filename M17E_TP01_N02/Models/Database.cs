using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace M17E_TP01_N02.Models
{
    public class Database
    {
        private static Database _instance;
        private readonly SqlConnection _ligacaoDb;

        public static Database Instance => _instance ?? (_instance = new Database());
        public Database()
        {
            var strLigacao = ConfigurationManager.ConnectionStrings["sql"].ToString();
            _ligacaoDb = new SqlConnection(strLigacao);
            _ligacaoDb.Open();
        }
        ~Database()
        {
            try
            {
                _ligacaoDb.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        #region Generic Function
        public DataTable SqlQuery(string sql)
        {
            var comando = new SqlCommand(sql, _ligacaoDb);
            var registos = new DataTable();
            var dados = comando.ExecuteReader();
            registos.Load(dados);
            dados.Dispose();
            comando.Dispose();
            return registos;
        }

        public DataTable SqlQuery(string sql, List<SqlParameter> parametros)
        {
            var comando = new SqlCommand(sql, _ligacaoDb);
            var registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            var dados = comando.ExecuteReader();
            registos.Load(dados);
            dados.Dispose();
            comando.Dispose();
            return registos;
        }


        public DataTable SqlQuery(string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            var comando = new SqlCommand(sql, _ligacaoDb) { Transaction = transacao };
            var registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            var dados = comando.ExecuteReader();
            registos.Load(dados);
            dados.Dispose();
            comando.Dispose();
            return registos;
        }

        public bool NonQuery(string sql)
        {
            try
            {
                var comando = new SqlCommand(sql, _ligacaoDb);
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Debug.WriteLine(erro.Message);
                return false;
            }
            return true;
        }

        public bool NonQuery(string sql, List<SqlParameter> parametros)
        {
            try
            {
                var comando = new SqlCommand(sql, _ligacaoDb);
                comando.Parameters.AddRange(parametros.ToArray());
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                return false;
            }
            return true;
        }

        public bool NonQuery(string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            try
            {
                var comando = new SqlCommand(sql, _ligacaoDb);
                comando.Parameters.AddRange(parametros.ToArray());
                comando.Transaction = transacao;
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                return false;
            }
            return true;
        }
        #endregion

        public int executeScalar(string sql)
        {
            int valor = -1;
            try
            {
                SqlCommand comando = new SqlCommand(sql, _ligacaoDb);
                valor = (int)comando.ExecuteScalar();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                return valor;
            }
            return valor;
        }

        public int executeScalar(string sql, List<SqlParameter> parametros)
        {
            int valor = -1;
            try
            {
                SqlCommand comando = new SqlCommand(sql, _ligacaoDb);
                comando.Parameters.AddRange(parametros.ToArray());
                valor = (int)comando.ExecuteScalar();
                comando.Dispose();
            }
            catch (Exception erro)
            {
                Console.Write(erro.Message);
                return valor;
            }
            return valor;
        }
    }
}