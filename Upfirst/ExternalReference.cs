namespace Upfirst
{
    public class ExternalReference
    {
        public string Value { get; set; }
        public int CursoId { get; set; }
        public int AlunoId { get; set; }
        public string CodigoCupom { get; set; }
        public ExternalReference(string valor)
        {
            int posCurso = valor.IndexOf("C");
            int posCupom = valor.IndexOf("X");

            Value = valor;

            AlunoId = int.Parse(valor.Substring(1, posCurso - 1));
            CursoId = int.Parse(valor.Substring(posCurso + 1, posCupom - posCurso - 1));
            CodigoCupom = valor.Substring(posCupom + 1, valor.Length - posCupom - 1).ToUpper();
        }
    }
}
