using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofwork;

        public EmployeeController(IMapper mapper, IUnitOfWork Unitofwork) 
        {
            _mapper = mapper;
            _unitofwork = Unitofwork;
        }

        public IActionResult Index(string? SearchValue)
        {
            IEnumerable<Employee> AllEmployees;
            if (String.IsNullOrEmpty(SearchValue))
            {
                AllEmployees = _unitofwork.EmployeeRepository.GetAll();
                
            }
            else
            {
                AllEmployees= _unitofwork.EmployeeRepository.GetEmployeesByName(SearchValue);
            }
            var MappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(AllEmployees);
            return View(MappedEmployees);
        }
        public IActionResult Create()
        {
            ViewBag.department = _unitofwork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                #region Manual Mapping 

                /* var emp = new Employee();      
                {
                    emp.Name = employeeVM.Name;
                    emp.Age = employeeVM.Age;

                } */
                #endregion
                
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitofwork.EmployeeRepository.Add(MappedEmp);
                int result = _unitofwork.Complete();
                if (result > 0)
                {
                    TempData["CreatedMsg"] = "Employee Is Created";
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(employeeVM);
        }

        public IActionResult Details(int? id , string viewname = "Details")
        {
            
            if (id is null)
            {
                return BadRequest();
            }
            var emp = _unitofwork.EmployeeRepository.GetById(id.Value);
            if (emp is null)
            {
                return NotFound();
            }
            //ViewData["DepartmentName"] = _iDepartmentRepository.GetById(id.Value);

            var MappedEmp = _mapper.Map<Employee, EmployeeViewModel>(emp);
            return View(viewname , MappedEmp);
        }

        public IActionResult Edit(int? id)
        {
            //if (id is null) { return BadRequest(); }
            //var department = _departmentRepository.GetbyId(id.Value);
            //if (department is null) { return NotFound(); }
            //else
            //    return View(department);
            ViewBag.department = _unitofwork.DepartmentRepository.GetAll();
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel employeeVM, [FromRoute] int Id)
        {
            if (employeeVM.Id != Id) { return BadRequest(); }

            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitofwork.EmployeeRepository.Update(MappedEmp);
                    _unitofwork.Complete();
                    return RedirectToAction(nameof(Index));

                }
                
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);

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
        public IActionResult Delete(EmployeeViewModel employeeVM , [FromRoute] int id)
        {
            if(employeeVM.Id != id)
            { return BadRequest(); }
            if (ModelState.IsValid) 
            {
                try
                {
                    var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitofwork.EmployeeRepository.Delete(MappedEmp);
                    int Result = _unitofwork.Complete();
                    if (Result > 0)
                    { TempData["DeletedMsg"] = "Employee Is Deleted";  }
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }

        public IActionResult Search(string? SearchValue)
        {
            IEnumerable<Employee> AllEmployees;
            if (String.IsNullOrEmpty(SearchValue))
            {
                AllEmployees = _unitofwork.EmployeeRepository.GetAll();

            }
            else
            {
                AllEmployees = _unitofwork.EmployeeRepository.GetEmployeesByName(SearchValue);
            }
            var MappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(AllEmployees);
            return PartialView("_EmployeeTablePartialView",MappedEmployees);
        }
    }
}
