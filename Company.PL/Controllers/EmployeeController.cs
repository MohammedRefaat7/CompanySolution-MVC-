using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _iemployeeRepository;
        private readonly IDepartmentRepository _iDepartmentRepository;
        public EmployeeController(IEmployeeRepository IemployeeRepository , IDepartmentRepository departmentRepository) 
        {
            _iemployeeRepository = IemployeeRepository;
            _iDepartmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var AllEmployees = _iemployeeRepository.GetAll();
            return View(AllEmployees);
        }
        public IActionResult Create()
        {
            ViewBag.department = _iDepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _iemployeeRepository.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            else
            return View(employee);
        }

        public IActionResult Details(int? id , string viewname = "Details")
        {
            
            if (id is null)
            {
                return BadRequest();
            }
            var emp = _iemployeeRepository.GetById(id.Value);
            if (emp is null)
            {
                return NotFound();
            }
            //ViewData["DepartmentName"] = _iDepartmentRepository.GetById(id.Value);
            return View(viewname ,emp);
        }

        public IActionResult Edit(int? id)
        {
            //if (id is null) { return BadRequest(); }
            //var department = _departmentRepository.GetbyId(id.Value);
            //if (department is null) { return NotFound(); }
            //else
            //    return View(department);
            ViewBag.department = _iDepartmentRepository.GetAll();
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee, [FromRoute] int Id)
        {
            if (employee.Id != Id) { return BadRequest(); }

            if (ModelState.IsValid)
            {
                try
                {
                    _iemployeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));

                }
                
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employee);

        }

        public IActionResult Delete(int? id)
        {
            //if (id is null)
            //{
            //    return BadRequest();
            //}
            //var dept = _departmentRepository.GetbyId(id.Value);
            //if (dept is null)
            //{ return NotFound(); }

            //return View(dept);
            
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee , [FromRoute] int id)
        {
            if(employee.Id != id)
            { return BadRequest(); }
            if (ModelState.IsValid) 
            {
                try
                {
                    _iemployeeRepository.Delete(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employee);
        }

        
    }
}
