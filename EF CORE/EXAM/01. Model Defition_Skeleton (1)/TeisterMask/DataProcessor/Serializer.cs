namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var sb = new StringBuilder();

            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ProjectsExportDTO[]), new XmlRootAttribute("Projects"));

            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            
               var  projects = context.Projects
                    .ToArray()
                    .Where(p => p.Tasks.Count > 0)
                    .Select(p => new ProjectsExportDTO()
                    {
                        HasEndDate = p.DueDate.HasValue ? "Yes" : "No",
                        Name = p.Name,
                        TasksCount = p.Tasks.Count,
                        Tasks = p.Tasks.ToArray().Select(t => new TaskXmlDto()
                        {
                            Name = t.Name,
                            Label = t.LabelType.ToString()
                        })
                            .OrderBy(t => t.Name)
                            .ToArray()
                    })
                    .OrderByDescending(p => p.TasksCount)
                    .ThenBy(p => p.Name)
                    .ToArray();

                xmlSerializer.Serialize(new StringWriter(sb), projects, ns);
            

            return sb.ToString().Trim();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context.Employees
                .Where(x => x.EmployeesTasks.Any(y => y.Task.OpenDate >= date))
                .ToArray()
                .Select(x => new
                {
                    Username = x.Username,
                    Tasks = x.EmployeesTasks.ToArray()
                        .Where(y => y.Task.OpenDate >= date)
                        .OrderByDescending(y => y.Task.DueDate)
                        .ThenBy(y => y.Task.Name)
                        .Select(y => new
                        {
                            TaskName = y.Task.Name,
                            OpenDate = y.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                            DueDate = y.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = Enum.GetName(typeof(LabelType),y.Task.LabelType),
                            ExecutionType = Enum.GetName(typeof(ExecutionType), y.Task.ExecutionType)
                        })
                        .ToArray()

                })
                .OrderByDescending(e => e.Tasks.Length)
                .ThenBy(e => e.Username)
                .Take(10)
                .ToArray();

            string json = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return json;

        }
    }
}