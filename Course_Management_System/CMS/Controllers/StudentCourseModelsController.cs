using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Models;

namespace CMS.Controllers
{
    public class StudentCourseModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentCourseModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentCourseModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentCourseModel.Where(a => a.StudentEmail == User.Identity.Name).ToListAsync());
        }

        // GET: StudentCourseModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentCourseModel = await _context.StudentCourseModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentCourseModel == null)
            {
                return NotFound();
            }

            return View(studentCourseModel);
        }

        // GET: StudentCourseModels/Create
        public IActionResult Create()
        {
            List<CourseModel> courses = _context.CourseList.ToList();
            ViewBag.ListOfCourses = courses;
            List<UnitModel> units = _context.UnitList.ToList();
            ViewBag.ListOfUnits = units;
            return View();
        }

        // POST: StudentCourseModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CourseID,StudentID,UnitID,CourseName,UnitCode,UnitName,StudentEmail")] StudentCourseModel studentCourseModel)
        {
            if (ModelState.IsValid)
            {
                var courseObj = _context.CourseList.Find(Convert.ToInt32(studentCourseModel.CourseID));
                if (courseObj.ID > 0)
                {
                    studentCourseModel.CourseName = courseObj.CourseName;
                    studentCourseModel.CourseID = courseObj.ID.ToString();
                }
                var unitObj = _context.UnitList.Find(Convert.ToInt32(studentCourseModel.UnitID));
                if (unitObj.ID > 0)
                {
                    studentCourseModel.UnitCode = unitObj.UnitCode;
                    studentCourseModel.UnitName = unitObj.UnitName;
                }

                studentCourseModel.StudentEmail = User.Identity.Name;
                _context.Add(studentCourseModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentCourseModel);
        }

        // GET: StudentCourseModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentCourseModel = await _context.StudentCourseModel.FindAsync(id);
            if (studentCourseModel == null)
            {
                return NotFound();
            }
            return View(studentCourseModel);
        }

        // POST: StudentCourseModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CourseID,StudentID,UnitID,CourseName,UnitCode,UnitName,StudentEmail")] StudentCourseModel studentCourseModel)
        {
            if (id != studentCourseModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentCourseModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentCourseModelExists(studentCourseModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(studentCourseModel);
        }

        // GET: StudentCourseModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentCourseModel = await _context.StudentCourseModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentCourseModel == null)
            {
                return NotFound();
            }

            return View(studentCourseModel);
        }

        // POST: StudentCourseModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentCourseModel = await _context.StudentCourseModel.FindAsync(id);
            _context.StudentCourseModel.Remove(studentCourseModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentCourseModelExists(int id)
        {
            return _context.StudentCourseModel.Any(e => e.ID == id);
        }
    }
}
