using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text;

namespace DAL.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public int AlunoId { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        [Display(Name = "Amount")]
        public decimal Valor { get; set; }
        [Display(Name = "Date")]
        public DateTime Data { get; set; }
        [Display(Name = "Form")]
        public string Forma { get; set; }
        [Display(Name = "Order Id")]
        // Num Operacao Mercado Pago
        public string OrderId { get; set; }
        [Display(Name = "Payment Id")]
        public long PaymentId { get; set; }
        [Display(Name = "Payment Type")]
        public string TipoPagamento { get; set; }
        [Display(Name = "Status Detail")]
        public string StatusDetail { get; set; }
        // Status da opecao no Mercado Pago
        public string Status { get; set; }
        [Display(Name = "Course")]
        public Curso Curso { get; set; }
        [Display(Name = "Student")]
        public Aluno Aluno { get; set; }
    }
}
