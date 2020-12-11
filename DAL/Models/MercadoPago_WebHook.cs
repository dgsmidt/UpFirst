using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class MercadoPago_WebHook
    {
        [Key]
        public int MercadoPago_WebHookId { get; set; }
        public DateTime Data { get; set; }
        public long DataId { get; set; }
        public string Type { get; set; }
    }
}
