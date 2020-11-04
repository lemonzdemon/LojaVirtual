using BiaBraga.Domain.Models.Entitys;
using System.Collections.Generic;

namespace BiaBraga.Admin.Models.FormViewModel.Products
{
    public class DetailsViewModel
    {
        public Product Product { get; set; }
        public List<string> Images { get; set; }
    }
}
