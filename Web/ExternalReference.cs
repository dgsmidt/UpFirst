using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web
{
    public class ExternalReference
    {
        public string Value { get; set; }
        public int CursoId { get; set; }
        public int AlunoId { get; set; }

        public ExternalReference(string valor)
        {
            int posCurso = Value.IndexOf("C");

            Value = valor;

            AlunoId = int.Parse(valor.Substring(1, posCurso - 1));
            CursoId = int.Parse(valor.Substring(posCurso + 1, valor.Length - posCurso - 1));
        }
    }
}
