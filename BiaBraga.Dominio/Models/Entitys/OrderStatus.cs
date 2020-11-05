using BiaBraga.Domain.Enums;
using System;

namespace BiaBraga.Domain.Models.Entitys
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public OrderStatusEnum OrderStatusEnum { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Note { get; set; }
        public DateTime DateTime { get; set; }
    }
}
