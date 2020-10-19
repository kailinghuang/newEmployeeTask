using EmployeeTasks.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTasks.Clients
{
    public class EmployeeTaskAPIClient
    {
 
        private HttpClient client;

        public EmployeeTaskAPIClient( string baseUrl)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
        }
        public async Task<List<TaskModel>> GetTasksAsync()
        {
            List<TaskModel> EmpInfo = new List<TaskModel>();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("api/task");

            if (Res.IsSuccessStatusCode)
            {
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                EmpInfo = JsonConvert.DeserializeObject<List<TaskModel>>(EmpResponse);

            }
            return EmpInfo;
        }


        public async Task<TaskModel> GetOneTasksAsync(int? id)
        {
            TaskModel EmpInfo = new TaskModel();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
 
            HttpResponseMessage Res = await client.GetAsync($"api/task/{id}");
  
            if (Res.IsSuccessStatusCode)
            {
 
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                EmpInfo = JsonConvert.DeserializeObject<TaskModel>(EmpResponse);

            }
            return EmpInfo;
        }

        public async Task CreateTask(TaskModel taskModel)
        {
            client.DefaultRequestHeaders.Clear();

            var myContent = JsonConvert.SerializeObject(taskModel);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json"); 
            HttpResponseMessage Res = await client.PostAsync("api/task", stringContent);
         
        }

        public async Task UpdateTask(int? id, TaskModel taskModel)
        {
            client.DefaultRequestHeaders.Clear();

            var myContent = JsonConvert.SerializeObject(taskModel);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json"); 
            HttpResponseMessage Res = await client.PutAsync($"api/task/{id}", stringContent);
        }


        public async Task DeleteTask(int id)
        {
            client.DefaultRequestHeaders.Clear();
            HttpResponseMessage Res = await client.DeleteAsync($"api/task/{id}");

        }



        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<EmployeeModel> EmpInfo = new List<EmployeeModel>();

            client.DefaultRequestHeaders.Clear(); 
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("api/employee");
 
            if (Res.IsSuccessStatusCode)
            {
 
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                EmpInfo = JsonConvert.DeserializeObject<List<EmployeeModel>>(EmpResponse);
            }
            return EmpInfo;
        }

        public async Task<EmployeeTaskDetail> GetOneEmployeeAsync(int? id)
        {
            EmployeeTaskDetail EmpInfo = new EmployeeTaskDetail();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync($"api/Employee/{id}");
 
            if (Res.IsSuccessStatusCode)
            { 
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                EmpInfo = JsonConvert.DeserializeObject<EmployeeTaskDetail>(EmpResponse);
            }
            
            return EmpInfo;
        }

        public async Task CreateEmployee(EmployeeModel employee)
        {
            client.DefaultRequestHeaders.Clear();

            var myContent = JsonConvert.SerializeObject(employee);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json"); 
            HttpResponseMessage Res = await client.PostAsync("api/Employee", stringContent);

        }

        public async Task UpdateEmployee(int? id, EmployeeModel employee)
        {
            client.DefaultRequestHeaders.Clear();
            var myContent = JsonConvert.SerializeObject(employee);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage Res = await client.PutAsync($"api/Employee/{id}", stringContent);

        }

        public async Task DeleteEmployee(int id)
        {
            client.DefaultRequestHeaders.Clear();
            HttpResponseMessage Res = await client.DeleteAsync($"api/Employee/{id}");

        }


    }
}
