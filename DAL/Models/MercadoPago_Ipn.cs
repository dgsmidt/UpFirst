using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class MercadoPago_Ipn
    {
        [Key]
        public int MercadoPago_IpnId { get; set; }
        public DateTime Data { get; set; }
        public string Topic { get; set; }
        public long Id { get; set; }
    }
}
