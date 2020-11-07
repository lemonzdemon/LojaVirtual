using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BiaBraga.Domain.Models.Entitys;
using BiaBraga.Repository.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BiaBraga.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(string name)
        {
            var categories = await _repository.GetAllAsync<Category>();

            if (!string.IsNullOrEmpty(name))
            {
                categories = categories.Where(x => x.Name.ToUpper().Contains(name.ToUpper())).ToList();
            }

            ViewData["Filtro"] = name;

            return View(categories);
        }

        public async Task<IActionResult> Details(int? id, string error)
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

            ViewData["Error"] = error;

            return View(category);
        }


        public async Task<IActionResult> Create()
        {
            ViewData["DepartmentId"] = new SelectList(await _repository.GetAllAsync<Department>(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if(await _repository.CategorieExistAsync(category.Name, null))
                {
                    ViewData["DepartmentId"] = new SelectList(await _repository.GetAllAsync<Department>(), "Id", "Name");
                    ViewData["Error"] = "Ja existe uma categoria com esse nome.";
                    return View(category);
                }

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
            ViewData["DepartmentId"] = new SelectList(await _repository.GetAllAsync<Department>(), "Id", "Name");
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
                    if (await _repository.CategorieExistAsync(category.Name, id))
                    {
                        ViewData["DepartmentId"] = new SelectList(await _repository.GetAllAsync<Department>(), "Id", "Name");
                        ViewData["Error"] = "Ja existe uma categoria com esse nome.";
                        return View(category);
                    }


                    await _repository.UpdateAsync(category);
                }
                catch (Exception er)
                {

                }
                return RedirectToAction(nameof(Details), new { id = category.Id });
            }
            return View(category);
        }

        [HttpPost, Route("DeleteCategorie/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategorie(int id)
        {
            if (await _repository.CategorieExistInProductAsync(id))
            {
                return RedirectToAction(nameof(Details),
                    new
                    {
                        id = id,
                        error = "hfsdasdfffuh29384y"
                    });
            }

            var category = await _repository.GetCategoryByIdAsync(id);

            await _repository.DeleteAsync(category);

            return RedirectToAction(nameof(Index));
        }
    }
}
