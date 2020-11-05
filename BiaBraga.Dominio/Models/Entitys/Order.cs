using System;

namespace BiaBraga.Domain.Models.Entitys
{
    public class Order
    {
        public int Id { get; set; }
        public int PreOrderId { get; set; }
        public PreOrder PreOrder { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        public string Tracking { get; set; }
        public string Invoice { get; set; }
        public DateTime DateTime { get; set; }
    }
}
