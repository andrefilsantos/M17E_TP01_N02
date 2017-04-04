using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace M17E_TP01_N02.Models
{
    public class AssistenciaModel
    {
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
        public DateTime Inicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fim { get; set; }

        public bool Concluida;

        [Display(Name = "Preço")]
        public double Preco { get; set; }

        [Display(Name = "Comentários")]
        public string Comentarios { get; set; }
    }

    public class DbAssistencia
    {
        public List<AssistenciaModel> Lista()
        {
            var registos = Database.Instance.SqlQuery("SELECT * FROM assistencias WHERE active = 1");
            var lista = new List<AssistenciaModel>();

            foreach (DataRow dados in registos.Rows)
            {

                var novo = new AssistenciaModel();
                novo.IdAssistencia = int.Parse(dados[0].ToString());    
                //TODO: Outros Parametros
                lista.Add(novo);
            }

            return lista;
        }
    }
}