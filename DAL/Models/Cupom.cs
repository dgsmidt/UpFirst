using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Cupom
    {
        public int Id { get; set; }
        [Display(Name = "Code")]
        public string Codigo { get; set; }
        public DateTime Data { get; set; }
        public DateTime Validade { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        [Display(Name = "(%) Discount")]
        public decimal Desconto { get; set; }
        public bool Utilizado { get; set; }
    }
}
