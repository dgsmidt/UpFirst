using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ArquivoApoio
    {
        public int Id { get; set; }
        public int AulaId { get; set; }
        public string FileName { get; set; }
    }
}
