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
    public class UnitModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnitModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UnitModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnitList.ToListAsync());
        }

        // GET: UnitModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitModel = await _context.UnitList
                .FirstOrDefaultAsync(m => m.ID == id);
            if (unitModel == null)
            {
                return NotFound();
            }

            return View(unitModel);
        }

        // GET: UnitModels/Create
        public IActionResult Create()
        {
            List<CourseModel> courses = _context.CourseList.ToList();
            ViewBag.ListOfCourses = courses;
            return View();
        }

        // POST: UnitModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UnitName,UnitCode,CourseID")] UnitModel unitModel)
        {
            if (ModelState.IsValid)
            {
                var courseObj = _context.CourseList.Find(unitModel.CourseID);
                if (courseObj.ID > 0)
                {
                    unitModel.CourseName = courseObj.CourseName;
                    _context.Add(unitModel);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitModel);
        }

        // GET: UnitModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitModel = await _context.UnitList.FindAsync(id);
            if (unitModel == null)
            {
                return NotFound();
            }
            return View(unitModel);
        }

        // POST: UnitModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UnitName,UnitCode,CourseID,CourseName")] UnitModel unitModel)
        {
            if (id != unitModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitModel.CourseName = _context.CourseList.Find(unitModel.CourseID).CourseName;
                    _context.Update(unitModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitModelExists(unitModel.ID))
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
            return View(unitModel);
        }

        // GET: UnitModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitModel = await _context.UnitList
                .FirstOrDefaultAsync(m => m.ID == id);
            if (unitModel == null)
            {
                return NotFound();
            }

            return View(unitModel);
        }

        // POST: UnitModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unitModel = await _context.UnitList.FindAsync(id);
            _context.UnitList.Remove(unitModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitModelExists(int id)
        {
            return _context.UnitList.Any(e => e.ID == id);
        }
    }
}
