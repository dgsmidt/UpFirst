using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class CursoVM
    {
        public Curso Curso { get; set; }
        public string CheckoutId { get; set; }
    }
}
