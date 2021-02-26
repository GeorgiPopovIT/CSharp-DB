using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var dbcontext = new SoftUniContext();
            Console.WriteLine(DeleteProjectById(dbcontext));
        }
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var address = new Address();
            address.AddressText = "Vitoshka 15";
            address.TownId = 4;

            context.Addresses.Add(address);

            context.SaveChanges();

            var nakov = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");

            nakov.AddressId = address.AddressId;
            context.SaveChanges();

            var addreses = context.Employees
                .Select(e => new { e.Address.AddressText, e.Address.AddressId })
                .OrderByDescending(e => e.AddressId)
                .Take(10).ToList();

            foreach (var addres in addreses)
            {
                sb.AppendLine(addres.AddressText);
            }
            return sb.ToString().Trim();
        }
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees
                .Include(e => e.EmployeesProjects)
                .ThenInclude(e => e.Project)
                .Where(e => e.EmployeesProjects
                .Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    EmployeeName = e.FirstName,
                    EmployeeLastName = e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects.Select(e => new
                    {
                        ProjectName = e.Project.Name,
                        StartDate = e.Project.StartDate,
                        EndDate = e.Project.EndDate
                    })
                })
                .Take(10).ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.EmployeeName} {employee.EmployeeLastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");
                foreach (var project in employee.Projects)
                {
                    var startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                    var endDate = project.EndDate.HasValue
                        ? project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                        : "not finished";
                    sb.AppendLine($"--{project.ProjectName} - {startDate} - {endDate}");

                }
            }
            return sb.ToString();
        }
        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var addreses = context.Addresses.Include(t => t.Town).Select(a => new
            {
                AddressText = a.AddressText,
                TownName = a.Town.Name,
                EmployeeCount = a.Employees.Count
            })
            .OrderByDescending(a => a.EmployeeCount)
            .ThenBy(a => a.TownName)
            .ThenBy(a => a.AddressText)
            .Take(10)
            .ToList();

            foreach (var address in addreses)
            {
                sb.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeeCount} employees");
            }
            return sb.ToString();
        }
        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employee = context.Employees.Select(e => new
            {
                e.EmployeeId,
                e.FirstName,
                e.LastName,
                e.JobTitle,
                Projects = e.EmployeesProjects.Select(p => new { p.Project.Name })
                .OrderBy(p => p.Name)
                .ToList()
            }).FirstOrDefault(e => e.EmployeeId == 147);

            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            foreach (var project in employee.Projects)
            {
                sb.AppendLine(project.Name);
            }

            return sb.ToString().Trim();
        }
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    d.Name,
                    ManagerFirstName = d.Manager.FirstName,
                    ManagerLastName = d.Manager.LastName,
                    DepEmployees = d.Employees
                        .Select(e => new
                        {
                            EmployeeFirstName = e.FirstName,
                            EmployeeLastName = e.LastName,
                            e.JobTitle
                        })
                    .OrderBy(e => e.EmployeeFirstName)
                    .ThenBy(e => e.EmployeeLastName)
                    .ToList()
                })
                .ToList();


            foreach (var department in departments)
            {
                sb.AppendLine($"{department.Name} - {department.ManagerFirstName} {department.ManagerLastName}");
                foreach (var employee in department.DepEmployees)
                {
                    sb.AppendLine($"{employee.EmployeeFirstName} {employee.EmployeeLastName} - {employee.JobTitle}");
                }
            }
            return sb.ToString().Trim();
        }
        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var latestProjects = context.Projects
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                })
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .ToList();

            foreach (var project in latestProjects)
            {
                sb
                    .AppendLine(project.Name)
                    .AppendLine(project.Description)
                    .AppendLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
            }

            return sb.ToString().Trim();
        }
        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.Department.Name == "Engineering"
                || e.Department.Name == "Tool Design"
                || e.Department.Name == "Marketing"
                || e.Department.Name == "Information Services");

            foreach (var employee in employees)
            {
                employee.Salary += employee.Salary * 0.12m;
            }
            context.SaveChanges();

            var getEmployees = employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var employee in getEmployees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F2})");
            }
            return sb.ToString().Trim();
        }
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:F2})");
            }
            return sb.ToString().Trim();
        }
        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();


            var empl = context.EmployeesProjects.Where(p => p.Project.ProjectId == 2).ToList();
            foreach (var item in empl)
            {
                context.Remove(item);
            }
            context.Projects.Remove(context.Projects.FirstOrDefault(p => p.ProjectId == 2));
            context.SaveChanges();

            var getProjects = context.Projects.Take(10).ToList();
            foreach (var project1 in getProjects)
            {
                sb.AppendLine(project1.Name);
            }    
                context.SaveChanges();

            return sb.ToString().Trim();
        }
        public static string RemoveTown(SoftUniContext context)
        {
            var currTown = context.Towns.First(t => t.Name == "Seattle");

            var addresses = context.Addresses.Where(a => a.Town == currTown);

            int addressesCount = addresses.Count();

            var employeesAddreses = context.Employees
             .Where(e => addresses.Any(a => a.AddressId == e.AddressId));
            foreach (var e in employeesAddreses)
            {
                e.AddressId = null;
            }
            foreach (var address in addresses)
            {
                context.Addresses.Remove(address);
            }
            context.Towns.Remove(currTown);
            context.SaveChanges();

            return $"{addressesCount} addresses in Seattle were deleted";
        }
    }
}
