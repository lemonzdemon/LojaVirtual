using System;

namespace BiaBraga.Domain.Models.Entitys
{
    public class PreOrderProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductQuantity { get; set; }
        public int PreOrderId { get; set; }
        public PreOrder PreOrder { get; set; }
        public decimal UnitaryValue { get; set; }
        public DateTime DateTime { get; set; }
    }
}