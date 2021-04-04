using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Task")]
    public class TaskImportDTO
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        public string OpenDate { get; set; }
        [Required]
        public string DueDate { get; set; }
        public int ExecutionType { get; set; }
        public int LabelType { get; set; }
    }
}