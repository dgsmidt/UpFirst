namespace Web.Models
{
    public class RetornoPagamentoMP
    {
        public string preference_id { get; set; }
        public string external_reference { get; set; }
        public string merchant_order_id { get; set; }
        public string payment_id { get; set; }
        public string payment_status { get; set; }
        public string payment_status_detail { get; set; }
        public string processing_mode { get; set; }
        public string Descricao { get; set; }
    }
}
