using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BiaBraga.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BiaBraga.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        protected readonly IProductRepository _repository;
        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _repository.GetAllProductsAsync();
                if (products.Any())
                {
                    return Ok(products);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("images/{idProduto}")]
        public IActionResult GetAllImageProduct(int idProduto)
        {
            try
            {
                var folderName = Path.Combine("Resources\\images\\products", idProduto.ToString());
                var path = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (System.IO.Directory.Exists(path))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);

                    return Ok(from _file in files
                              select new { name = _file.Name });
                }

                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
