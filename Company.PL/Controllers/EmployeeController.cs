using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Microsoft.AspNetCore.Mvc;

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
            return View(employee);
        }

        
              
        
    }
}
