using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BiaBraga.Domain.Models.Entitys;
using BiaBraga.Repository.Interfaces;

namespace BiaBraga.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync<Category>());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var category = await _repository.GetCategoryByIdAsync(id.Value);

            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(category);

                return RedirectToAction(nameof(Details), new { id = category.Id });
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var category = await _repository.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(category);
                }
                catch (Exception er)
                {
                   
                }
                return RedirectToAction(nameof(Details), new { id = category.Id });
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var category = await _repository.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);

            await _repository.DeleteAsync(category);

            return RedirectToAction(nameof(Index));
        }
    }
}
