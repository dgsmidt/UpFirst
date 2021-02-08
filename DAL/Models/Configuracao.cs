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
        public string EmailContato { get; set; }
        [Display(Name = "Título da Página")]
        public string Titulo { get; set; }
        [Display(Name = "Header")]
        public string CabecalhoTexto1_Index { get; set; }
        [Display(Name = "Text")]
        public string Texto1_Index { get; set; }
        public string Logo { get; set; }
        [Display(Name = "Vídeo Institucional")]
        public string Video_Index { get; set; }
        [Display(Name = "Passing Score")]
        [Column(TypeName = "decimal(3, 1)")]
        public decimal NotaDeCorte { get; set; }
        [Display(Name = "Cabeçalho 2")]
        public string CabecalhoTexto2_Index { get; set; }
        [Display(Name = "Texto 2")]
        public string Texto2_Index { get; set; }
        [Display(Name = "Cabeçalho 3")]
        public string CabecalhoTexto3_Index { get; set; }
        [Display(Name = "Texto 3")]
        public string Texto3_Index { get; set; }
        [Display(Name = "Texto Alvo")]
        public string TextoAlvo_Index { get; set; }
        [Display(Name = "Texto Gráfico")]
        public string TextoGrafico_Index { get; set; }
        [Display(Name = "Texto Computador")]
        public string TextoComputador_Index { get; set; }
        [Display(Name = "Endereço Linha 1")]
        public string EnderecoLinha1 { get; set; }
        [Display(Name = "Endereço Linha 2")]
        public string EnderecoLinha2 { get; set; }
        [Display(Name = "Endereço Linha 3")]
        public string EnderecoLinha3 { get; set; }
        public string LogoBackground { get; set; }
    }
}
