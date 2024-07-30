using System.ComponentModel.DataAnnotations;

namespace EstoquePecas.Models
{
    public class Peca
    {
        public int Id_Peca { get; set; }

        [Required]
        [MaxLength(45)]
        public string? Descricao { get; set; }

        [Required]
        public int Saldo { get; set; }
        
        [Required]
        public decimal Preco_Medio { get; set; }

        [Required]
        public int Part_Number { get; set; }

        [Required]
        public int Ativa { get; set; }
    }
}
