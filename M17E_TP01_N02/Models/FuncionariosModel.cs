using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace M17E_TP01_N02.Models
{
    public class FuncionariosModel
    {
        [Key]
        public int IdFuncionario { get; set; }

        [Required(ErrorMessage = "Tem de indicar o nome do funcionário")]
        public string Nome;

        [Required(ErrorMessage = "Tem de indicar o nome de utilizador do funcionário")]
        public string Username;

        [Required(ErrorMessage = "Tem de indicar uma password para o funcionário")]
        [MinLength(5, ErrorMessage = "Password demasiado insegura. Deve ter, pelo menos, 5 carateres")]
        public string Password;

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
    }
}