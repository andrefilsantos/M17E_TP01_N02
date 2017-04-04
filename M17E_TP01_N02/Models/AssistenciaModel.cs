using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace M17E_TP01_N02.Models {
    public class AssistenciaModel {
        [Key]
        public int IdAssistencia { get; set; }

        [Required(ErrorMessage = "Por favor, indique o cliente.")]
        public int Cliente { get; set; }

        [Required(ErrorMessage = "Por favor, indique a máquina.")]
        [Display(Name = "Máquina")]
        public int Maquina { get; set; }

        [Required(ErrorMessage = "Por favor, indique o funcionário.")]
        [Display(Name = "Funcionário")]
        public int Funcionario { get; set; }

        [Display(Name = "Data de Início da Assistência")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data de Início da Assistência")]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Display(Name = "Data de Início da Assistência")]
        [DataType(DataType.Time)]
        public DateTime HoraInicio { get; set; }

        [Display(Name = "Data de Início da Assistência")]
        [DataType(DataType.Time)]
        public DateTime HoraFim{ get; set; }

        public bool Concluida;

        [Display(Name = "Preço")]
        public double Preco { get; set; }

        [Display(Name = "Comentários")]
        public string Comentarios { get; set; }
    }

    public class DbAssistencia {
        public List<AssistenciaModel> ListaAllActive() {
            var registos = Database.Instance.SqlQuery("SELECT * FROM assistencias WHERE active = 1");
            var lista = new List<AssistenciaModel>();

            foreach (DataRow dados in registos.Rows) {

                var novo = new AssistenciaModel { IdAssistencia = int.Parse(dados[0].ToString()) };

                //TODO: Outros Parametros
                lista.Add(novo);
            }

            return lista;
        }

        public bool AdicionarAssistencia(AssistenciaModel dados) {
            var parametros = new List<SqlParameter>
            {
                new SqlParameter {ParameterName = "@idCliente",     SqlDbType = SqlDbType.Int,      Value = dados.Cliente },
                new SqlParameter {ParameterName = "@idMaquina",     SqlDbType = SqlDbType.Int,      Value = dados.Maquina },
                new SqlParameter {ParameterName = "@idFuncionario", SqlDbType = SqlDbType.Int,      Value = dados.Funcionario },
                new SqlParameter {ParameterName = "@dataInicio",    SqlDbType = SqlDbType.DateTime, Value = dados.DataInicio },
                new SqlParameter {ParameterName = "@dataFim",       SqlDbType = SqlDbType.DateTime, Value = dados.DataFim },
                new SqlParameter {ParameterName = "@horaInicio",    SqlDbType = SqlDbType.DateTime, Value = dados.HoraInicio },
                new SqlParameter {ParameterName = "@horaFim",       SqlDbType = SqlDbType.DateTime, Value = dados.HoraFim },
                new SqlParameter {ParameterName = "@concluida",     SqlDbType = SqlDbType.Bit,      Value = dados.Concluida },
                new SqlParameter {ParameterName = "@preco",         SqlDbType = SqlDbType.Decimal,  Value = dados.Preco },
                new SqlParameter {ParameterName = "@observacoes",   SqlDbType = SqlDbType.NVarChar, Value = dados.Comentarios }
            };

            return
                Database.Instance.NonQuery(
                    "INSERT INTO Assistencias(idCliente, idMaquina, idFuncionario, dataInicio, dataFim, horaInicio, horaFim, concluida, preco, observacoes) VALUES(@idCliente, @idMaquina, @idFuncionario, @dataInicio, @dataFim, @horaInicio, @horaFim, @concluida, @preco, @observacoes)", parametros);
        }
    }
}