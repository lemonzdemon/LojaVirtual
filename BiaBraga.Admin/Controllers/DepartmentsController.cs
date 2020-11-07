using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiaBraga.Domain.Models.Entitys;
using BiaBraga.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BiaBraga.Admin.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentsController(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index(string name)
        {
            var departments = await _repository.GetAllAsync<Department>();

            if (!string.IsNullOrEmpty(name))
            {
                departments = departments.Where(x => x.Name.ToUpper().Contains(name.ToUpper())).ToList();
            }

            ViewData["Filtro"] = name;

            return View(departments);
        }

        public async Task<IActionResult> Details(int? id, string error)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var department = await _repository.GetDepartmentByIdAsync(id.Value);

            if (department == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["Error"] = error;

            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                if (await _repository.DepartmentExistAsync(department.Name, null))
                {
                    ViewData["Error"] = "Ja existe uma categoria com esse nome.";
                    return View(department);
                }

                await _repository.AddAsync(department);

                return RedirectToAction(nameof(Details), new { id = department.Id });
            }

            return View(department);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var category = await _repository.GetDepartmentByIdAsync(id.Value);
            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.Id)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (await _repository.DepartmentExistAsync(department.Name, id))
                    {
                        ViewData["Error"] = "Ja existe um departamento com esse nome.";
                        return View(department);
                    }


                    await _repository.UpdateAsync(department);
                }
                catch (Exception er)
                {

                }
                return RedirectToAction(nameof(Details), new { id = department.Id });
            }
            return View(department);
        }

        [HttpPost, Route("DeleteDepartment/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (await _repository.DepartmentExistInCategoryAsync(id))
            {
                return RedirectToAction(nameof(Details),
                    new
                    {
                        id = id,
                        error = "hfsdasdfffuh29384y"
                    });
            }

            var department = await _repository.GetDepartmentByIdAsync(id);

            await _repository.DeleteAsync(department);

            return RedirectToAction(nameof(Index));
        }
    }
}
