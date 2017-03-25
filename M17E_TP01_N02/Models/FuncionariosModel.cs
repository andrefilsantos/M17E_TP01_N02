using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace M17E_TP01_N02.Models
{
    public class FuncionariosModel
    {
        [Key]
        public int IdFuncionario { get; set; }

        [Required(ErrorMessage = "Tem de indicar o nome do funcionário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Tem de indicar o nome de utilizador do funcionário")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Tem de indicar uma password para o funcionário")]
        [MinLength(5, ErrorMessage = "Password demasiado insegura. Deve ter, pelo menos, 5 carateres")]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public string CartaoCidadao { get; set; }

        public string Telefone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Comentarios { get; set; }
    }

    public class DbFuncionarios
    {
        public List<FuncionariosModel> lista()
        {
            var registos = Database.Instance.SqlQuery("SELECT * FROM funcionarios WHERE active = 1");
            var lista = new List<FuncionariosModel>();

            foreach (DataRow dados in registos.Rows)
            {

                var novo = new FuncionariosModel();
                novo.IdFuncionario = int.Parse(dados[0].ToString());
                //TODO: Outros Parametros
                lista.Add(novo);
            }

            return lista;
        }

        public int CreateFuncionario(FuncionariosModel dados)
        {
            string sql = "INSERT INTO Funcionarios(nome, username, password, dataNascimento, nCC, telefone, email, observacoes, dataCriacao, ultimoUpdate, ativo) VALUES (@nome, @username, HASHBYTES('SHA2_512', @password), @dataNascimento, @nCC, @telefone, @email, @observacoes, getDate(), getDate(), 1); SELECT cast(scope_identity() as int);";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName="@nome", SqlDbType=SqlDbType.VarChar, Value=dados.Nome },
                new SqlParameter() {ParameterName="@username", SqlDbType=SqlDbType.VarChar, Value=dados.Username },
                new SqlParameter() {ParameterName="@password", SqlDbType=SqlDbType.VarChar, Value=dados.Password },
                new SqlParameter() {ParameterName="@dataNascimento", SqlDbType=SqlDbType.VarChar, Value=dados.DataNascimento },
                new SqlParameter() {ParameterName="@nCC", SqlDbType=SqlDbType.VarChar, Value=dados.CartaoCidadao },
                new SqlParameter() {ParameterName="@telefone", SqlDbType=SqlDbType.VarChar, Value=dados.Telefone },
                new SqlParameter() {ParameterName="@email", SqlDbType=SqlDbType.VarChar, Value=dados.Email },
                new SqlParameter() {ParameterName="@observacoes", SqlDbType=SqlDbType.VarChar, Value=dados.Comentarios}
            };
            int id = Database.Instance.executeScalar(sql, parametros);
            return id;
        }
    }
}