using System;
using System.Collections.Generic;
using System.Text;

namespace BiaBraga.Domain.Models.Entitys
{
    public class PreOrder
    {
        public int Id { get; set; }
        public string CodePreOrder { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public List<PreOrderProduct> PreOrderProducts { get; set; }
        public DateTime DateTime { get; set; }
    }
}
