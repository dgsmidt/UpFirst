using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Configuracao
    {
        public int Id { get; set; }
        [Display(Name = "Header")]
        public string CabecalhoTexto1_Index { get; set; }
        [Display(Name = "Text")]
        public string Texto1_Index { get; set; }
        public string Logo { get; set; }
        [Display(Name = "Passing Score")]
        [Column(TypeName = "decimal(3, 1)")]
        public decimal NotaDeCorte { get; set; }
    }
}
