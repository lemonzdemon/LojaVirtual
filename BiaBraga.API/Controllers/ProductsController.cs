using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using BiaBraga.API.Models.Enums;
using BiaBraga.API.Services;
using BiaBraga.Business.Dtos;
using BiaBraga.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BiaBraga.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerService
    {
        protected readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper, ILogger<ProductsController> logger) : base(logger)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                NewLog(nameof(GetAllProducts), TypeLogger.StartProcess);
                var products = await _repository.GetAllProductsAsync();

                NewLog(nameof(GetAllProducts), TypeLogger.StartMapping);
                var productsModels = _mapper.Map<ProductViewDto[]>(products);

                NewLog(nameof(GetAllProducts), TypeLogger.FinishSucess);
                return Ok(productsModels);
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(GetAllProducts));
            }
        }

        [HttpGet("images/{idProduto}")]
        [AllowAnonymous]
        public IActionResult GetAllImageProduct(int idProduto)
        {
            try
            {
                NewLog(nameof(GetAllImageProduct), TypeLogger.StartProcess, $"ID:{idProduto}");
                var folderName = Path.Combine("Resources\\images\\products", idProduto.ToString());
                var path = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (Directory.Exists(path))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);

                    NewLog(nameof(GetAllImageProduct), TypeLogger.FinishSucess, $"ID:{idProduto}");
                    return Ok(from _file in files select new { name = _file.Name });
                }
                NewLog(nameof(GetAllImageProduct), TypeLogger.FinishDivergence, $"ID:{idProduto}, directory:{path} notfound");
                return NotFound();
            }
            catch (Exception ex)
            {
                return ErrorException(ex, nameof(GetAllImageProduct), idProduto);
            }
        }
    }
}
