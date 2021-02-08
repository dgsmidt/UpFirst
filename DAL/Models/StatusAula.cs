using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class StatusAulas
    {
        public int Id { get; set; }
        public int MatriculaId { get; set; }
        public int AulaAssistindoId { get; set; }
        public int AulaPodeMarcarAssistidaId { get; set; }
        public int UltimoModuloLiberadoId { get; set; }
        public int? AvaliacaoLiberadaId { get; set; }
        public Matricula Matricula { get; set; }
        public List<AulaAssistida> AulasAssistidas { get; set; }
    }
}
