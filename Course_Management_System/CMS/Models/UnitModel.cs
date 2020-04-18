using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Models
{
    [Table("Unit")]
    public class UnitModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }
        [Display(Name = "Unit Code")]
        public string UnitCode { get; set; }
        public int CourseID { get; set; }
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
    }
}
