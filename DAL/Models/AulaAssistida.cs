using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class AulaAssistida
    {
        public int Id { get; set; }
        public int AulaId { get; set; }
        public int AlunoId { get; set; }
    }
}
