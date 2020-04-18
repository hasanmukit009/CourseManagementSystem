using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    [Table("StudentCourse")]
    public class StudentCourseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string CourseID { get; set; }
        public string StudentID { get; set; }
        public string UnitID { get; set; }

        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
        [Display(Name = "Unit Code")]
        public string UnitCode { get; set; }
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }
        [Display(Name = "Email")]
        public string StudentEmail { get; set; }
    }
}
