using Company.BLL.Interfaces;
using Company.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Add(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Details(int? Id, string ViewName = "Details")
        {
            if (Id is null)
            {
                return BadRequest();
            }
            var dept = _departmentRepository.GetById(Id.Value);
            if (dept is null)
                return NotFound();
            else
                return View(ViewName, dept);
        }

        public IActionResult Edit(int? id)
        {
            //if (id is null) { return BadRequest(); }
            //var department = _departmentRepository.GetbyId(id.Value);
            //if (department is null) { return NotFound(); }
            //else
            //    return View(department);
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department , [FromRoute] int Id)
        {
            if(department.Id != Id) { return BadRequest(); }

            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));

                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);

        }
    }
}
