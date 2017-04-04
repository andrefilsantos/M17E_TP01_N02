using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace M17E_TP01_N02.Models {
    public class ClientesModel {
        [Key]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Tem de indicar o nome do cliente")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Tem de indicar o nome de utilizador do cliente")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Tem de indicar uma password para o cliente")]
        [MinLength(5, ErrorMessage = "Password demasiado insegura. Deve ter, pelo menos, 5 carateres")]
        public string Password { get; set; }

        public string Morada { get; set; }

        public string CodigoPostal { get; set; }

        public string Localidade { get; set; }

        public string Telefone { get; set; }

        [Display(Name = "Telemóvel")]
        public string Telemovel { get; set; }

        public string Fax { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Site { get; set; }

        [Display(Name = "Responsável")]
        public string Responsavel { get; set; }

        [Display(Name = "Comentários")]
        public string Comentarios { get; set; }
    }

    public class DbClientes {
        public List<ClientesModel> Lista() {
            var registos = Database.Instance.SqlQuery("SELECT * FROM clientes");
            var lista = new List<ClientesModel>();

            foreach (DataRow dados in registos.Rows) {

                var novo = new ClientesModel {
                    IdCliente = int.Parse(dados[0].ToString()),
                    Nome = dados[1].ToString(),
                    Username = dados[2].ToString(),
                    Password = dados[3].ToString(),
                    Morada = dados[4].ToString(),
                    CodigoPostal = dados[5].ToString(),
                    Localidade = dados[6].ToString(),
                    Telefone = dados[7].ToString(),
                    Fax = dados[8].ToString(),
                    Telemovel = dados[9].ToString(),
                    Email = dados[10].ToString(),
                    Site = dados[11].ToString(),
                    Responsavel = dados[12].ToString(),
                    Comentarios = dados[13].ToString()
                };
                lista.Add(novo);
            }

            return lista;
        }



        public object ListaAllActive() {
            var registos = Database.Instance.SqlQuery("SELECT * FROM clientes WHERE ativo = 1");
            var lista = new List<ClientesModel>();

            foreach (DataRow dados in registos.Rows) {

                var novo = new ClientesModel {
                    IdCliente = int.Parse(dados[0].ToString()),
                    Nome = dados[1].ToString(),
                    Username = dados[2].ToString(),
                    Password = dados[3].ToString(),
                    Morada = dados[4].ToString(),
                    CodigoPostal = dados[5].ToString(),
                    Localidade = dados[6].ToString(),
                    Telefone = dados[7].ToString(),
                    Fax = dados[8].ToString(),
                    Telemovel = dados[9].ToString(),
                    Email = dados[10].ToString(),
                    Site = dados[11].ToString(),
                    Responsavel = dados[12].ToString(),
                    Comentarios = dados[13].ToString()
                };
                lista.Add(novo);
            }

            return lista;
        }

        public List<ClientesModel> Lista(int id) {
            var sql = "SELECT * FROM Clientes WHERE idCliente=@id";
            var parametros = new List<SqlParameter>()
            {
                new SqlParameter {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=id },
            };
            var registos = Database.Instance.SqlQuery(sql, parametros);
            var lista = new List<ClientesModel>();

            foreach (DataRow dados in registos.Rows) {
                var novo = new ClientesModel {
                    Nome = dados[1].ToString(),
                    Username = dados[2].ToString(),
                    Password = dados[3].ToString(),
                    Morada = dados[4].ToString(),
                    CodigoPostal = dados[5].ToString(),
                    Localidade = dados[6].ToString(),
                    Telefone = dados[7].ToString(),
                    Fax = dados[8].ToString(),
                    Telemovel = dados[9].ToString(),
                    Email = dados[10].ToString(),
                    Site = dados[11].ToString(),
                    Responsavel = dados[12].ToString(),
                    Comentarios = dados[13].ToString()
                };
                lista.Add(novo);
            }

            return lista;
        }

        public bool AdicionarCliente(ClientesModel dados) {
            const string sql = "INSERT INTO Clientes(nome, username, password, morada, codigoPostal, localidade, telefone, fax, telemovel, email, site, representante, observacoes, dataCriacao, ultimoUpdate, ativo) VALUES(@nome, @username, HASHBYTES('SHA2_512', @password), @morada, @codigoPostal, @localidade, @telefone, @fax, @telemovel, @email, @site, @representante, @observacoes, getDate(), getDate(), 1)";
            var parametros = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@nome",          SqlDbType=SqlDbType.NVarChar, Value = dados.Nome},
                new SqlParameter { ParameterName = "@username",      SqlDbType=SqlDbType.NVarChar, Value = dados.Username},
                new SqlParameter { ParameterName = "@password",      SqlDbType=SqlDbType.VarChar,  Value = dados.Password},
                new SqlParameter { ParameterName = "@morada",        SqlDbType=SqlDbType.NVarChar, Value = dados.Morada},
                new SqlParameter { ParameterName = "@codigoPostal",  SqlDbType=SqlDbType.NVarChar, Value = dados.CodigoPostal},
                new SqlParameter { ParameterName = "@localidade",    SqlDbType=SqlDbType.NVarChar, Value = dados.Localidade},
                new SqlParameter { ParameterName = "@telefone",      SqlDbType=SqlDbType.NVarChar, Value = dados.Telefone},
                new SqlParameter { ParameterName = "@fax",           SqlDbType=SqlDbType.NVarChar, Value = dados.Fax},
                new SqlParameter { ParameterName = "@telemovel",     SqlDbType=SqlDbType.NVarChar, Value = dados.Telemovel},
                new SqlParameter { ParameterName = "@email",         SqlDbType=SqlDbType.NVarChar, Value = dados.Email},
                new SqlParameter { ParameterName = "@site",          SqlDbType=SqlDbType.NVarChar, Value = dados.Site},
                new SqlParameter { ParameterName = "@representante", SqlDbType=SqlDbType.NVarChar, Value = dados.Responsavel},
                new SqlParameter { ParameterName = "@observacoes",   SqlDbType=SqlDbType.Text,     Value = dados.Comentarios}
            };

            return Database.Instance.NonQuery(sql, parametros);
        }

        public bool AtualizarCliente(ClientesModel dados) {
            const string sql = "UPDATE Clientes SET nome = @nome, username = @username, password = HASHBYTES('SHA2_512', @password), morada = @morada, codigoPostal = @codigoPostal, localidade = @localidade, telefone = @telefone, fax = @fax, telemovel = @telemovel, email = @email, site = @site, representante = @representante, observacoes = @observacoes, ultimoUpdate = getDate() WHERE idCliente = @idCliente";
            var parametros = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@nome",          SqlDbType=SqlDbType.NVarChar, Value = dados.Nome},
                new SqlParameter { ParameterName = "@username",      SqlDbType=SqlDbType.NVarChar, Value = dados.Username},
                new SqlParameter { ParameterName = "@password",      SqlDbType=SqlDbType.VarChar,  Value = dados.Password},
                new SqlParameter { ParameterName = "@morada",        SqlDbType=SqlDbType.NVarChar, Value = dados.Morada},
                new SqlParameter { ParameterName = "@codigoPostal",  SqlDbType=SqlDbType.NVarChar, Value = dados.CodigoPostal},
                new SqlParameter { ParameterName = "@localidade",    SqlDbType=SqlDbType.NVarChar, Value = dados.Localidade},
                new SqlParameter { ParameterName = "@telefone",      SqlDbType=SqlDbType.NVarChar, Value = dados.Telefone},
                new SqlParameter { ParameterName = "@fax",           SqlDbType=SqlDbType.NVarChar, Value = dados.Fax},
                new SqlParameter { ParameterName = "@telemovel",     SqlDbType=SqlDbType.NVarChar, Value = dados.Telemovel},
                new SqlParameter { ParameterName = "@email",         SqlDbType=SqlDbType.NVarChar, Value = dados.Email},
                new SqlParameter { ParameterName = "@site",          SqlDbType=SqlDbType.NVarChar, Value = dados.Site},
                new SqlParameter { ParameterName = "@representante", SqlDbType=SqlDbType.NVarChar, Value = dados.Responsavel},
                new SqlParameter { ParameterName = "@observacoes",   SqlDbType=SqlDbType.Text,     Value = dados.Comentarios},
                new SqlParameter { ParameterName = "@idCliente",     SqlDbType=SqlDbType.Int,      Value = dados.IdCliente}
            };

            return Database.Instance.NonQuery(sql, parametros);

        }

        public bool RemoverCliente(int id) {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter {ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = id}
            };
            return Database.Instance.NonQuery("UPDATE Clientes SET ativo = 0 WHERE idCliente=@id", parametros);
        }
    }
}