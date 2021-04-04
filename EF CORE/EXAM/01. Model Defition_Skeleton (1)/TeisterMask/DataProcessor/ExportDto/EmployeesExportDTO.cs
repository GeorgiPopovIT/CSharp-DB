using System;
using System.Collections.Generic;
using System.Text;

namespace TeisterMask.DataProcessor.ExportDto
{
   public class EmployeesExportDTO
    {
        public string Username { get; set; }
        public List<TaskExportDTO> Tasks { get; set; }
    }
}
