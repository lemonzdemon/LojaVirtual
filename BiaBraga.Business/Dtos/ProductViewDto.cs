using BiaBraga.Domain.Models.Entitys;

namespace BiaBraga.Business.Dtos
{
    public class ProductViewDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
        public Category Categoria { get; set; }
    }
}
