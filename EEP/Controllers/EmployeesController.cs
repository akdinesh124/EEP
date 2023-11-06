using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EEP.Data;
using EEP.Models;
using EEP.Services;

namespace EEP.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EEPContext _context;
         

        private readonly EmployeeService _employeeService; 

        public EmployeesController(EEPContext context, EmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }

        // GET: Employees
        public IActionResult Index()
        {
            var allEmployees=_employeeService.GetAllEmployees();
            return View(allEmployees);
        }

        // GET: Employees/Details/5
      
        public IActionResult Details(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            var employeeCreated=_employeeService.CreateEmployee(employee);
            return View(employeeCreated);
        }
        public IActionResult ActiveEmployees()
        {
            
            return View();
        }
        public List<Employee> ActiveEmployeesList()
        {
            var activeEmployees = _employeeService.GetAllActiveEmployees();
            return activeEmployees;
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(Employee employee,int id)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                     _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var employeeDeleted=_employeeService.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
