using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeTasks.Models;
using EmployeeTasks.Clients;

namespace EmployeeTasks.Controllers
{
    public class EmployeesController : Controller
    {
        
        private EmployeeTaskAPIClient employeeTaskAPIClient;

        public EmployeesController(EmployeeTaskAPIClient _employeeTaskAPIClient)
        {
            employeeTaskAPIClient = _employeeTaskAPIClient;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            List<EmployeeModel> EmpInfo = await employeeTaskAPIClient.GetEmployeesAsync();
            //returning the employee list to view  
            return View(EmpInfo);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await employeeTaskAPIClient.GetOneEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("employeeId,firstName,lastName,hiredDate")] EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                await employeeTaskAPIClient.CreateEmployee(employeeModel);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDetail = await employeeTaskAPIClient.GetOneEmployeeAsync(id);
            var employeeModel = new EmployeeModel()
            {
                employeeId = employeeDetail.employeeId,
                firstName = employeeDetail.firstName,
                lastName = employeeDetail.lastName,
                hiredDate = employeeDetail.hiredDate
            };
            if (employeeModel == null)
            {
                return NotFound();
            }
            return View(employeeModel);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("employeeId,firstName,lastName,hiredDate")] EmployeeModel employeeModel)
        {
            if (id != employeeModel.employeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await employeeTaskAPIClient.UpdateEmployee(id, employeeModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModel);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDetail =  await employeeTaskAPIClient.GetOneEmployeeAsync(id);
            var employeeModel = new EmployeeModel()
            {
                employeeId = employeeDetail.employeeId,
                firstName = employeeDetail.firstName,
                lastName = employeeDetail.lastName,
                hiredDate = employeeDetail.hiredDate
            };
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await employeeTaskAPIClient.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
