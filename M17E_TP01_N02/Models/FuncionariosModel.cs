using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace M17E_TP01_N02.Models {
    public class FuncionariosModel {
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

    public class DbFuncionarios {
        public List<FuncionariosModel> ListAllActive() {
            var registos = Database.Instance.SqlQuery("SELECT * FROM funcionarios WHERE ativo = 1 and username <> 'admin'");
            var list = new List<FuncionariosModel>();

            foreach (DataRow dados in registos.Rows) {
                var novo = new FuncionariosModel {
                    IdFuncionario = int.Parse(dados[0].ToString()),
                    Nome = dados[1].ToString(),
                    Username = dados[2].ToString(),
                    Password = dados[3].ToString(),
                    DataNascimento = DateTime.Parse(dados[4].ToString()),
                    CartaoCidadao = dados[6].ToString(),
                    Telefone = dados[7].ToString(),
                    Email = dados[8].ToString(),
                    Comentarios = dados[9].ToString()
                };
                list.Add(novo);
            }

            return list;
        }

        public List<FuncionariosModel> Lista() {
            var registos = Database.Instance.SqlQuery("SELECT * FROM funcionarios WHERE username <> 'admin'");
            var list = new List<FuncionariosModel>();

            foreach (DataRow dados in registos.Rows) {
                var novo = new FuncionariosModel {
                    IdFuncionario = int.Parse(dados[0].ToString()),
                    Nome = dados[1].ToString(),
                    Username = dados[2].ToString(),
                    Password = dados[3].ToString(),
                    DataNascimento = DateTime.Parse(dados[4].ToString()),
                    CartaoCidadao = dados[6].ToString(),
                    Telefone = dados[7].ToString(),
                    Email = dados[8].ToString(),
                    Comentarios = dados[9].ToString()
                };
                list.Add(novo);
            }

            return list;
        }

        public List<FuncionariosModel> Lista(int id) {
            var registos = Database.Instance.SqlQuery($"SELECT * FROM funcionarios WHERE idFuncionario = {id} AND username <> 'admin'");
            var list = new List<FuncionariosModel>();

            foreach (DataRow dados in registos.Rows) {
                var novo = new FuncionariosModel {
                    IdFuncionario = int.Parse(dados[0].ToString()),
                    Nome = dados[1].ToString(),
                    Username = dados[2].ToString(),
                    Password = dados[3].ToString(),
                    DataNascimento = DateTime.Parse(dados[4].ToString()),
                    CartaoCidadao = dados[6].ToString(),
                    Telefone = dados[7].ToString(),
                    Email = dados[8].ToString(),
                    Comentarios = dados[9].ToString()
                };
                list.Add(novo);
            }

            return list;
        }

        public int CreateFuncionario(FuncionariosModel dados) {
            const string sql = "INSERT INTO Funcionarios(nome, username, password, dataNascimento, nCC, telefone, email, observacoes, dataCriacao, ultimoUpdate, ativo) VALUES (@nome, @username, HASHBYTES('SHA2_512', @password), @dataNascimento, @nCC, @telefone, @email, @observacoes, getDate(), getDate(), 1); SELECT cast(scope_identity() as int);";

            var parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome", SqlDbType=SqlDbType.NVarChar, Value=dados.Nome },
                new SqlParameter() {ParameterName="@username", SqlDbType=SqlDbType.NVarChar, Value=dados.Username },
                new SqlParameter() {ParameterName="@password", SqlDbType=SqlDbType.VarChar, Value=dados.Password },
                new SqlParameter() {ParameterName="@dataNascimento", SqlDbType=SqlDbType.DateTime, Value=dados.DataNascimento.ToShortDateString() },
                new SqlParameter() {ParameterName="@nCC", SqlDbType=SqlDbType.NVarChar, Value=dados.CartaoCidadao },
                new SqlParameter() {ParameterName="@telefone", SqlDbType=SqlDbType.NVarChar, Value=dados.Telefone },
                new SqlParameter() {ParameterName="@email", SqlDbType=SqlDbType.NVarChar, Value=dados.Email },
                new SqlParameter() {ParameterName="@observacoes", SqlDbType=SqlDbType.Text, Value=dados.Comentarios}
            };
            int id = Database.Instance.ExecuteScalar(sql, parametros);
            return id;
        }

        public List<FuncionariosModel> UserInfo(int id) {
            var sql = "SELECT * FROM Funcionarios WHERE idFuncionario=@id";
            var parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id", SqlDbType=SqlDbType.Int, Value=id },
            };
            var registos = Database.Instance.SqlQuery(sql, parametros);
            var list = new List<FuncionariosModel>();

            foreach (DataRow dados in registos.Rows) {
                var novo = new FuncionariosModel {
                    IdFuncionario = int.Parse(dados[0].ToString()),
                    Nome = dados[1].ToString(),
                    Username = dados[2].ToString(),
                    Password = dados[3].ToString(),
                    DataNascimento = DateTime.Parse(dados[4].ToString()),
                    CartaoCidadao = dados[6].ToString(),
                    Telefone = dados[7].ToString(),
                    Email = dados[8].ToString(),
                    Comentarios = dados[9].ToString()
                };
                list.Add(novo);
            }

            return list;
        }

        public void AtualizarFuncionario(FuncionariosModel funcionario) {
            const string sql = "UPDATE Funcionarios SET nome=@nome,username=@username,password=@password,dataNascimento=@dataNascimento,nCC=@nCC,telefone=@telefone,email=@email,observacoes=@observacoes WHERE id=@id";
            var parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nome", SqlDbType=SqlDbType.NVarChar, Value=funcionario.Nome },
                new SqlParameter() {ParameterName="@username", SqlDbType=SqlDbType.NVarChar, Value=funcionario.Username },
                new SqlParameter() {ParameterName="@password", SqlDbType=SqlDbType.VarChar, Value=funcionario.Password },
                new SqlParameter() {ParameterName="@dataNascimento", SqlDbType=SqlDbType.DateTime, Value=funcionario.DataNascimento.ToShortDateString() },
                new SqlParameter() {ParameterName="@nCC", SqlDbType=SqlDbType.NVarChar, Value=funcionario.CartaoCidadao },
                new SqlParameter() {ParameterName="@telefone", SqlDbType=SqlDbType.NVarChar, Value=funcionario.Telefone },
                new SqlParameter() {ParameterName="@email", SqlDbType=SqlDbType.NVarChar, Value=funcionario.Email },
                new SqlParameter() {ParameterName="@observacoes", SqlDbType=SqlDbType.Text, Value=funcionario.Comentarios}
            };
            Database.Instance.NonQuery(sql, parametros);
        }

        public bool RemoverFuncionario(int id) {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter {ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = id}
            };
            return Database.Instance.NonQuery("UPDATE Funcionarios SET ativo = 0 WHERE idFuncionario=@id", parametros);
        }
    }
}