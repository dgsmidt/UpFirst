using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class QuestionarioVM
    {
        public Questionario Questionario { get; set; }
        public int  AlunoId { get; set; }
    }
}
