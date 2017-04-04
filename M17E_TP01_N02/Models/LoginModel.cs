using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace M17E_TP01_N02.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Tem de preencher o campo \"Nome de Utilizador\"")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Tem de preencher o campo \"Password\"")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class LoginDb
    {
        public ClientesModel LoginCliente(LoginModel login)
        {
            string sql = "SELECT * FROM Clientes WHERE username=@username AND password=HASHBYTES('SHA2_512',@password)"; //TODO: Refazer Query
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName="@username", SqlDbType=SqlDbType.NVarChar, Value=login.Username },
                new SqlParameter() { ParameterName="@password", SqlDbType=SqlDbType.VarChar, Value=login.Password },
            };
            var dados = Database.Instance.SqlQuery(sql, parametros);
            ClientesModel utilizador = null;

            if (dados != null && dados.Rows.Count > 0)
            {
                utilizador = new ClientesModel();
                utilizador.Nome = dados.Rows[0][0].ToString();
            }
            return utilizador;
        }

        public FuncionariosModel LoginFuncionario(LoginModel login)
        {
            string sql = "SELECT * FROM Funcionarios WHERE username=@username AND password=HASHBYTES('SHA2_512',@password)"; 
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@username",SqlDbType=SqlDbType.NVarChar,Value=login.Username },
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=login.Password },
            };
            var dados = Database.Instance.SqlQuery(sql, parametros);
            FuncionariosModel utilizador = null;

            if (dados != null && dados.Rows.Count > 0)
            {
                utilizador = new FuncionariosModel();
                utilizador.Nome = dados.Rows[0][0].ToString();
            }
            return utilizador;
        }
    }
}