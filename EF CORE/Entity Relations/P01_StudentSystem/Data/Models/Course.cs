using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        public Course()
        {
            StudentsEnrolled = new HashSet<StudentCourse>();
            Resources = new HashSet<Resource>();
            HomeworkSubmissions = new HashSet<Homework>();
        }
        public int CourseId { get; set; }
        [MaxLength(80)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public  ICollection<StudentCourse> StudentsEnrolled { get; set; } 
        public  ICollection<Resource> Resources { get; set; }
        public  ICollection<Homework> HomeworkSubmissions { get; set; }
    }
}
