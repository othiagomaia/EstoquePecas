using System.ComponentModel.DataAnnotations;

namespace EstoquePecas.Models
{
    public class ConsumoModel
    {
        public int Id_Consumo { get; set; }

        [Required]
        public DateTime Data_Consumo { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public int Id_Peca { get; set; }

        [Required]
        public int Ativo { get; set; }
    }
}
