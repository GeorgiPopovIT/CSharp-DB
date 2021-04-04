namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var xmlSerializer = new XmlSerializer(typeof(List<ProjectImportDTO>), new XmlRootAttribute("Projects"));
            var xmlProjects = (List<ProjectImportDTO>)xmlSerializer.Deserialize(new StringReader(xmlString));

            foreach (var currProject in xmlProjects)
            {
                if (!IsValid(currProject))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var parsedOpenDate = DateTime.TryParseExact(currProject.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var openDate);
                var parsedDueDate = DateTime.TryParseExact(currProject.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dueDate);


                if (!parsedOpenDate  || !parsedDueDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var projectToAdd = new Project()
                {
                    Name = currProject.Name,
                    OpenDate = openDate,
                    DueDate = dueDate
                   
                };
                foreach (var task in currProject.Tasks)
                {
                    if (!IsValid(task))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    var parsedTaskOpenDate = DateTime.TryParseExact(currProject.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var taskOpenDate);
                    var parsedTaskDueDate = DateTime.TryParseExact(currProject.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var taskDueDate);
                    if (!parsedTaskOpenDate || !parsedTaskDueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    projectToAdd.Tasks.Add( new Task
                    {
                        Name = task.Name,
                        OpenDate = taskOpenDate,
                        DueDate = taskDueDate,
                        ExecutionType = (ExecutionType)task.ExecutionType,
                        LabelType = (LabelType)task.LabelType
                    });
                }
                context.Projects.Add(projectToAdd);
                context.SaveChanges();
                sb.AppendLine(String.Format(SuccessfullyImportedProject, currProject.Name, currProject.Tasks.Count()));
            }


            return sb.ToString().Trim();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            var employeesDtos = JsonConvert.DeserializeObject<List<EmployeeImportDTO>>(jsonString);

          

            foreach (var currEmployee in employeesDtos)
            {
                if (!IsValid(currEmployee))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
             
                var employeeToAdd = new Employee()
                {
                    Username = currEmployee.Username,
                    Email = currEmployee.Email,
                    Phone = currEmployee.Phone,
                };

                foreach (int taskId in currEmployee.Tasks.Distinct())
                {
                    var task = context.Tasks
                        .FirstOrDefault(t => t.Id == taskId);

                    if (task == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    employeeToAdd.EmployeesTasks.Add(new EmployeeTask()
                    {
                        Task = task
                    });
                }

                context.Employees.Add(employeeToAdd);
                context.SaveChanges();
                sb.AppendLine(string.Format(SuccessfullyImportedEmployee, employeeToAdd.Username,
                    employeeToAdd.EmployeesTasks.Count));
            }
            return sb.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}