using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Company.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            var MappedDept = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(MappedDept);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var MappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Add(MappedDept);
                int Result = _unitOfWork.Complete();
                if (Result > 0)
                {
                    TempData["CreatedMsg"] = "Department Is Created";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        public IActionResult Details(int? Id, string ViewName = "Details")
        {
            if (Id is null)
            {
                return BadRequest();
            }
            var dept = _unitOfWork.DepartmentRepository.GetById(Id.Value);
            if (dept is null)
                return NotFound();
            else
            {
                var MappedDept = _mapper.Map<Department, DepartmentViewModel>(dept);
                return View(ViewName, MappedDept);
            }
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
        public IActionResult Edit(DepartmentViewModel departmentVM , [FromRoute] int Id)
        {
            if(departmentVM.Id != Id) { return BadRequest(); }

            if (ModelState.IsValid)
            {
                try
                {
                    var MappedDept = _mapper.Map< DepartmentViewModel , Department>(departmentVM);
                    _unitOfWork.DepartmentRepository.Update(MappedDept);
                    _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));

                }
                catch(DbUpdateException ex) when(ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
                {
                    return BadRequest("The Name field must be unique. A record with this name already exists.");
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(departmentVM);

        }


        //{{{  TASK  }}} ...
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
        public IActionResult Delete(DepartmentViewModel departmentVM, [FromRoute] int id)
        {
            if (id != departmentVM.Id) { return BadRequest(); }

            if (ModelState.IsValid)
            {
                try
                {
                    var MappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    _unitOfWork.DepartmentRepository.Delete(MappedDept);
                    int Result = _unitOfWork.Complete();
                    if (Result > 0)
                    { TempData["DeletedMsg"] = "Department Is Deleted"; }
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(departmentVM);
        }
    }
}
