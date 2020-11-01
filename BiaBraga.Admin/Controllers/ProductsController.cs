using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BiaBraga.Domain.Models.Entitys;
using BiaBraga.Admin.Services;
using BiaBraga.Domain.Enums;
using BiaBraga.Repository.Interfaces;

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

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync<Product>());
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

            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _repository.GetAllAsync<Category>(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Date = DateTime.UtcNow;

                var priceString = product.Price.ToString();
                var priceOldString = product.OldPrice.ToString();

                product.Price = Convert.ToDecimal(priceString.Insert(priceString.Length - 2, "."), System.Globalization.CultureInfo.InvariantCulture);
                product.OldPrice = Convert.ToDecimal(priceOldString.Insert(priceOldString.Length - 2, "."), System.Globalization.CultureInfo.InvariantCulture);


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

        public async Task<IActionResult> Delete(int? id)
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

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);

            await _repository.DeleteAsync(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
