using Microsoft.VisualStudio.TestTools.UnitTesting;
using DepEmpCardAPI.Controllers;
using DepEmpCardAPI.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DepEmpCardAPI.Tests
{
    public class DepartmentsControllerTest
    {
        private DbContextOptions<ApplicationContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
        .UseInMemoryDatabase(databaseName: "PrimeDb")
        .Options;

        private DepartmentsController controller;

        [OneTimeSetUp]
        public void Setup()
        {
            SeedDb();

            controller = new DepartmentsController(new ApplicationContext(dbContextOptions));
        }

        private void SeedDb()
        {
            using var context = new ApplicationContext(dbContextOptions);
            var deps = new List<Department>
        {
            new Department { DepartmentId = 1, DepartmentName = "Demo-Name1", DepartmentLocation = "Demo-City1" },
            new Department { DepartmentId = 2, DepartmentName = "Demo-Name2", DepartmentLocation = "Demo-City2" },
            new Department { DepartmentId = 3, DepartmentName = "Demo-Name3", DepartmentLocation = "Demo-City3" },
            new Department { DepartmentId = 4, DepartmentName = "Demo-Name4", DepartmentLocation = "Demo-City4" }

        };


            context.AddRange(deps);
        

            context.SaveChanges();
        }


        [Test]
        public async Task GetAllDepartments()
        {
            using var context = new ApplicationContext(dbContextOptions);
            var deps = await controller.GetDepartments();

            NUnit.Framework.Assert.IsNotNull(deps);
        }

        [Test]
        public async Task GetById()
        {
            using var context = new ApplicationContext(dbContextOptions);
            var result = await controller.GetDepartment(2);
            var dep = result.Value;

            dep.Should().NotBeNull();
        }

        [Test]
        public async Task Post()
        {
            Department newDep = new Department {DepartmentName = "Demo-Name5", DepartmentLocation = "Demo-City5" };

            using var context = new ApplicationContext(dbContextOptions);
            var result = await controller.PostDepartment(newDep);

            var addedDepartment = await context.Departments
                .FirstOrDefaultAsync(r => r.DepartmentName == newDep.DepartmentName);

            addedDepartment.Should().NotBeNull();
            addedDepartment.DepartmentId.Should().Be(newDep.DepartmentId);
        }


        [Test]
        public async Task Put()
        {
            string fixName = "Demo-Name4";
            var Dep = GetDemoDep();

            using var context = new ApplicationContext(dbContextOptions);
            await controller.PutDepartment(Dep);

            var fixedDepartment = await context.Departments
                .FirstOrDefaultAsync(r => r.DepartmentName == fixName);

            fixedDepartment.Should().NotBeNull();
            fixedDepartment.DepartmentName.Should().Be(fixName);
        }


        [Test]
        public async Task Delete()
        {
            var Dep = GetDemoDep();
            int id = Dep.DepartmentId;
            using var context = new ApplicationContext(dbContextOptions);
            await controller.DeleteDepartment(Dep.DepartmentId);

            var fixedDepartment = await context.Departments
                .FirstOrDefaultAsync(r => r.DepartmentId == id);

            fixedDepartment.Should().BeNull();
        }

        Department GetDemoDep()
        {
            return new Department { DepartmentId = 7, DepartmentName = "Demo-Name4", DepartmentLocation = "Demo-City4" };
        }
    }
}
