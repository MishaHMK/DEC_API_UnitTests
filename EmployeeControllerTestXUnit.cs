using Microsoft.VisualStudio.TestTools.UnitTesting;
using DepEmpCardAPI.Controllers;
using DepEmpCardAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Xunit;
using Microsoft.AspNetCore.Hosting;

namespace DepEmpCardAPI.Tests
{
    public class EmployeeControllerTestXUnit
    {

        private DbContextOptions<ApplicationContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
      .UseInMemoryDatabase(databaseName: "PrimeDb")
      .Options;

        private EmployeeController controller;
        private IWebHostEnvironment _env;

        private void SeedDb()
        {
            using var context = new ApplicationContext(dbContextOptions);

            var emps = new List<Employee>
        {
            new Employee { EmployeeId = 1, EmployeeName = "Demo-Name1", EmployeeSurname = "Demo-City1", DepartmentName = "Demo-City1", DateOfJoining = new DateTime(2015, 7, 20), PhotoFileName = "anonymous.png"},
            new Employee { EmployeeId = 2, EmployeeName = "Demo-Name2", EmployeeSurname = "Demo-City2", DepartmentName = "Demo-City2", DateOfJoining = new DateTime(2015, 7, 20), PhotoFileName = "anonymous.png"},
            new Employee { EmployeeId = 3, EmployeeName = "Demo-Name3", EmployeeSurname = "Demo-City3", DepartmentName = "Demo-City3", DateOfJoining = new DateTime(2015, 7, 20), PhotoFileName = "anonymous.png"}
        };


            context.AddRange(emps);


            context.SaveChanges();
        }

        public EmployeeControllerTestXUnit()
        {
            SeedDb();
            controller = new EmployeeController(new ApplicationContext(dbContextOptions), _env);
        }


        //[Fact]
        //public async Task GetAllEmployees()
        //{
        //    // Arrange
        //    controller = new EmployeeController(new ApplicationContext(dbContextOptions), _env);
        //    // Act
        //    var emps = controller.GetEmployees();

        //    // Assert
        //    Xunit.Assert.NotNull(emps);
        //}

        //[Fact]
        //public async Task GetEmpById()
        //{
        //    // Arrange
        //    controller = new EmployeeController(new ApplicationContext(dbContextOptions), _env);
        //    // Act
        //    var result = controller.GetEmployee(1);

        //    // Assert
        //    Xunit.Assert.NotNull(result);
        //}


        //[Fact]
        //public async Task Post()
        //{
        //    // Arrange
        //    Employee newEmp = new Employee { EmployeeName = "Demo-Name6", EmployeeSurname = "Demo-City6", DepartmentName = "Demo-City1", DateOfJoining = new DateTime(2015, 7, 20), PhotoFileName = "anonymous.png" };
        //    using var context = new ApplicationContext(dbContextOptions);

        //    // Act
        //    var result = controller.PostEmployee(newEmp);
        //    var addedDepartment = await context.Employees
        //        .FirstOrDefaultAsync(r => r.EmployeeName == newEmp.EmployeeName);

        //    // Assert
        //    Xunit.Assert.NotNull(result);
        //    Xunit.Assert.Equal(newEmp.EmployeeId, addedDepartment.EmployeeId);
        //}


        //[Fact]
        //public async Task Put()
        //{
        //    // Arrange
        //    string fixName = "Demo-Name8";
        //    var Emp = GetDemoEmp();

        //    // Act
        //    using var context = new ApplicationContext(dbContextOptions);
        //    await controller.PutEmployee(Emp);

        //    var fixedEmployee = await context.Employees
        //        .FirstOrDefaultAsync(r => r.EmployeeName == fixName);

        //    // Assert
        //    Xunit.Assert.NotNull(fixedEmployee);
        //    Xunit.Assert.Equal(fixedEmployee.EmployeeName, fixName);
        //}


        [Fact]
        public async Task Delete()
        {
            // Arrange
            var Emp = GetDemoEmp();
            int id = Emp.EmployeeId;
            using var context = new ApplicationContext(dbContextOptions);

            // Act
            await controller.DeleteEmployee(id);

            var fixedEmployee = await context.Employees
                .FirstOrDefaultAsync(r => r.EmployeeId == id);

            // Assert
            Xunit.Assert.Null(fixedEmployee);
        }

        Employee GetDemoEmp()
        {
            return new Employee { EmployeeId = 8, EmployeeName = "Demo-Name8", EmployeeSurname = "Demo-City1", DepartmentName = "Demo-City1", DateOfJoining = new DateTime(2015, 7, 20), PhotoFileName = "anonymous.png" };
        }

    }
}
