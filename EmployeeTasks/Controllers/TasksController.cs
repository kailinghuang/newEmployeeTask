using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeTasks.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using EmployeeTasks.Clients;

namespace EmployeeTasks.Controllers
{
    public class TasksController : Controller
    {
        
        private EmployeeTaskAPIClient employeeTaskAPIClient;

        public TasksController(EmployeeTaskAPIClient _employeeTaskAPIClient)
        {
            employeeTaskAPIClient = _employeeTaskAPIClient;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            List<TaskModel> EmpInfo = await employeeTaskAPIClient.GetTasksAsync();
                //returning the employee list to view  
            return View(EmpInfo);
            
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskModel = await employeeTaskAPIClient.GetOneTasksAsync(id);
                
            if (taskModel == null)
            {
                return NotFound();
            }

            return View(taskModel);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("taskId,taskName,startTime,dueTime,employeeId")] TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                await employeeTaskAPIClient.CreateTask(taskModel);
                return RedirectToAction(nameof(Index));
            }
            return View(taskModel);
            
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskModel = await employeeTaskAPIClient.GetOneTasksAsync(id);
            if (taskModel == null)
            {
                return NotFound();
            }
            return View(taskModel);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("taskId,taskName,startTime,dueTime,employeeId")] TaskModel taskModel)
        {
            if (id != taskModel.taskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
               
                    await employeeTaskAPIClient.UpdateTask(id,taskModel);
                    
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taskModel);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskModel = await employeeTaskAPIClient.GetOneTasksAsync(id);
            if (taskModel == null)
            {
                return NotFound();
            }

            return View(taskModel);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await employeeTaskAPIClient.DeleteTask(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
