using System;
using System.Collections.Generic;
using System.Text;
using CMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CourseModel> CourseList { get; set; }
        public DbSet<UnitModel> UnitList { get; set; }
        public DbSet<StudentCourseModel> StudentCourseModel { get; set; }
    }
}
