using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace M17E_TP01_N02.Models
{
    public class MaquinasModel
    {
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

    public class DbMaquinas
    {
        public List<MaquinasModel> Lista()
        {
            var registos = Database.Instance.SqlQuery("SELECT * FROM maquinas WHERE active = 1");
            var lista = new List<MaquinasModel>();

            foreach (DataRow dados in registos.Rows)
            {

                var novo = new MaquinasModel();
                novo.IdCliente = int.Parse(dados[0].ToString());
                //TODO: Outros Parametros
                lista.Add(novo);
            }

            return lista;
        }
    }
}