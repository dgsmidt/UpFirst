using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Desconto
    {
        public int Id { get; set; }
        [Display(Name = "Description")]
        public string Descricao { get; set; }
        [Display(Name = "(%) Value")]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Valor { get; set; }
    }
}
