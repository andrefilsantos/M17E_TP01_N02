using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace M17E_TP01_N02.Models
{
    public class ClientesModel
    {
        [Key]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Tem de indicar o nome do cliente")]
        public string Nome { get; set; }

        public string Morada { get; set; }

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

    public class DbClientes
    {
        public List<ClientesModel> lista()
        {
            var registos = Database.Instance.SqlQuery("SELECT * FROM clientes WHERE active = 1");
            var lista = new List<ClientesModel>();

            foreach (DataRow dados in registos.Rows)
            {

                var novo = new ClientesModel();
                novo.IdCliente = int.Parse(dados[0].ToString());
                //TODO: Outros Parametros
                lista.Add(novo);
            }

            return lista;
        }
    }
}