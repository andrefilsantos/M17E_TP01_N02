using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace M17E_TP01_N02.Models {
    public class MaquinasModel {
        [Key]
        public int IdMaquina { get; set; }

        [Required(ErrorMessage = "Tem de associar a máquina a algum cliente")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Tem de indicar a descrição da máquina")]
        public string Descricao { get; set; }

        public string IpMaquina { get; set; }

        public string LoginAcesso { get; set; }

        public string PasswordAcesso { get; set; }
    }

    public class DbMaquinas {
        public List<MaquinasModel> ListaAllAtive() {
            var registos = Database.Instance.SqlQuery("SELECT * FROM maquinas WHERE active = 1");
            var lista = new List<MaquinasModel>();

            foreach (DataRow dados in registos.Rows) {

                var novo = new MaquinasModel {
                    IdCliente = int.Parse(dados[0].ToString()),
                    Descricao = dados[0].ToString(),
                    IpMaquina = dados[0].ToString(),
                    LoginAcesso = dados[0].ToString(),
                    PasswordAcesso = dados[0].ToString()
                };
                lista.Add(novo);
            }

            return lista;
        }

        public List<MaquinasModel> Lista() {
            var registos = Database.Instance.SqlQuery("SELECT * FROM maquinas");
            var lista = new List<MaquinasModel>();

            foreach (DataRow dados in registos.Rows) {

                var novo = new MaquinasModel {
                    IdCliente = int.Parse(dados[0].ToString()),
                    Descricao = dados[0].ToString(),
                    IpMaquina = dados[0].ToString(),
                    LoginAcesso = dados[0].ToString(),
                    PasswordAcesso = dados[0].ToString()
                };
                lista.Add(novo);
            }

            return lista;
        }

        public List<MaquinasModel> Lista(int id) {
            var registos = Database.Instance.SqlQuery($"SELECT * FROM maquinas WHERE idMaquina = {id}");
            var lista = new List<MaquinasModel>();

            foreach (DataRow dados in registos.Rows) {

                var novo = new MaquinasModel {
                    IdCliente = int.Parse(dados[1].ToString()),
                    Descricao = dados[2].ToString(),
                    IpMaquina = dados[3].ToString(),
                    LoginAcesso = dados[4].ToString(),
                    PasswordAcesso = dados[5].ToString()
                };
                lista.Add(novo);
            }

            return lista;
        }

        public bool AdicionarMaquina(MaquinasModel dados) {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter{ParameterName = "@idCliente",      SqlDbType = SqlDbType.Int,      Value = dados.IdCliente},
                new SqlParameter{ParameterName = "@descricao",      SqlDbType = SqlDbType.NVarChar, Value = dados.Descricao},
                new SqlParameter{ParameterName = "@ip",             SqlDbType = SqlDbType.NVarChar, Value = dados.IpMaquina},
                new SqlParameter{ParameterName = "@loginAcesso",    SqlDbType = SqlDbType.NVarChar, Value = dados.LoginAcesso},
                new SqlParameter{ParameterName = "@passwordAcesso", SqlDbType = SqlDbType.NVarChar, Value = dados.PasswordAcesso},
            };
            return
                Database.Instance.NonQuery(
                    "INSERT INTO Maquinas(idCliente, descricao, ip, loginAcesso, passwordAcesso, dataCriacao, ultimoUpdate, ativo) VALUES (@idCliente, @descricao, @ip, @loginAcesso, @passwordAcesso, getDate(), getDate(), 1)", parametros);
        }

        public bool AtualizarMaquina(MaquinasModel dados) {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter{ParameterName = "@idCliente",      SqlDbType = SqlDbType.Int,      Value = dados.IdCliente},
                new SqlParameter{ParameterName = "@descricao",      SqlDbType = SqlDbType.NVarChar, Value = dados.Descricao},
                new SqlParameter{ParameterName = "@ip",             SqlDbType = SqlDbType.NVarChar, Value = dados.IpMaquina},
                new SqlParameter{ParameterName = "@loginAcesso",    SqlDbType = SqlDbType.NVarChar, Value = dados.LoginAcesso},
                new SqlParameter{ParameterName = "@passwordAcesso", SqlDbType = SqlDbType.NVarChar, Value = dados.PasswordAcesso},
            };
            return
                Database.Instance.NonQuery(
                    "UPDATE Maquinas SET idCliente = @idCliente, descricao = @descricao, ip = @ip, loginAcesso = @loginAcesso, passwordAcesso = @passwordAcesso",
                    parametros);
        }

        public bool RemoverMaquina(int id) => Database.Instance.NonQuery($"UPDATE Maquinas SET ativo = 0 WHERE idMaquina={id}");
    }
}