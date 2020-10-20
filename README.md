# newEmployeeTask
Usage:
1. could run EmployeeTaskAPI Directly. then it could suplly RESTFUL API.
2. if you want to run EmployeeTasks web application, it need to trigger the EmployeeTaskAPI (Multiple Startup Project) together. all data show in EmployeeTasks is from EmployeeTaskAPI.

# Project:
## 1. EmployeeTaskAPI
•	Create RESTful API project with following functionalities:
1.	CRUD for Employee. 
a.	Employee has 4 properties:
i.	First Name
ii.	Last Name
iii.	Hired Date
iv.	List of Employee Task which belongs to the employee.
2.	CRUD for Employee Task.
a.	Employee Task has 3 properties:
i.	Task Name
ii.	Start Time
iii.	Deadline

## 2. EmployeeTasks
web application and get the data from EmployeeTaskAPI by httpclient
then display page here. could do CRUD operation in web UI.


## 3. EmployeeTaskAPIUnitTest
the test for EmployeeTaskAPI's 2 controller
the controller unit test coverage is 100 %.


## 4. EmployeeTaskAPIIntegrationTest

the test base on the TestServer + EF Core In Memory DB Together.
I did CRUD for the TaskController and EmployeeController

<video id="video" controls="" preload="none">
      <source id="mp4" src="https://github.com/kailinghuang/newEmployeeTask/blob/main/RunVideo.mp4" type="video/mp4">
      <p>Your user agent does not support the HTML5 Video element.</p>
</video>
