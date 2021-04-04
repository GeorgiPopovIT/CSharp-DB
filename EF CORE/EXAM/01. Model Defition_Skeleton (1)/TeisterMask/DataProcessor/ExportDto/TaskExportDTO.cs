using System;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ExportDto
{
    public class TaskExportDTO
    {
        public string TaskName { get; set; }
        public string OpenDate { get; set; }
        public string DueDate { get; set; }
        public string ExecutionType { get; set; }
        public string LabelType { get; set; }

    }
}