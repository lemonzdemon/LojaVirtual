using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BiaBraga.Domain.Models.Entitys;
using BiaBraga.Admin.Services;
using BiaBraga.Domain.Enums;
using BiaBraga.Repository.Interfaces;
using System.Linq;

namespace BiaBraga.Admin.Controllers
{
    [AuthService(Role.Administrador, Role.Supervisor)]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(int? category, string name)
        {

            var categoriesSelectList = new SelectList(await _repository.GetAllAsync<Category>(), "Id", "Name");

            var products = await _repository.GetAllProductsAsync();

            if (category.HasValue)
            {
                products = products.Where(x => x.CategoryId == category.Value).ToList();

                var categorySelected = categoriesSelectList.FirstOrDefault(x => x.Value == category.Value.ToString());

                if (categorySelected != null)
                {
                    categorySelected.Selected = true;
                }
            }

            ViewData["CategoryId"] = categoriesSelectList;
            ViewData["Filtro"] = name;

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(x => x.Name.ToUpper().Contains(name.ToUpper())).ToList();
            }

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var product = await _repository.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }

            product.Date = product.Date.ToLocalTime();

            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _repository.GetAllAsync<Category>(), "Id", "Name");
            return View();
        }

        private (decimal, decimal) FormatDecimalPrices(decimal price, decimal priceOld, bool save)
        {
            if (!save)
            {
                price = decimal.Round(price, 2, MidpointRounding.AwayFromZero);
                priceOld = decimal.Round(priceOld, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                var priceString = price.ToString();
                var priceOldString = priceOld.ToString();

                price = Convert.ToDecimal(priceString.Insert(priceString.Length - 2, "."), System.Globalization.CultureInfo.InvariantCulture);
                priceOld = Convert.ToDecimal(priceOldString.Insert(priceOldString.Length - 2, "."), System.Globalization.CultureInfo.InvariantCulture);
            }

            return (price, priceOld);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Date = DateTime.UtcNow;

                var decimals = FormatDecimalPrices(product.Price, product.OldPrice, true);

                product.Price = decimals.Item1;
                product.OldPrice = decimals.Item2;


                await _repository.AddAsync(product);
                return RedirectToAction(nameof(Details), new { id = product.ID });
            }
            ViewData["CategoryId"] = new SelectList(await _repository.GetAllAsync<Category>(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var product = await _repository.GetProductByIdAsync(id.Value);

            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var decimals = FormatDecimalPrices(product.Price, product.OldPrice, false);

            product.Price = decimals.Item1;
            product.OldPrice = decimals.Item2;

            ViewData["CategoryId"] = new SelectList(await _repository.GetAllAsync<Category>(), "Id", "Name", product.CategoryId);
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ID)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var decimals = FormatDecimalPrices(product.Price, product.OldPrice, true);

                    product.Price = decimals.Item1;
                    product.OldPrice = decimals.Item2;


                    await _repository.UpdateAsync(product);
                }
                catch (Exception er)
                {

                }

                return RedirectToAction(nameof(Details), new { id = product.ID });
            }
            ViewData["CategoryId"] = new SelectList(await _repository.GetAllAsync<Category>(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost, Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);

            await _repository.DeleteAsync(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
