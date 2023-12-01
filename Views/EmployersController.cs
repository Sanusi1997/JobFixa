using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobFixa.Data;
using JobFixa.Entities;

namespace JobFixa.Views
{
    public class EmployersController : Controller
    {
        private readonly JobFixaContext _context;

        public EmployersController(JobFixaContext context)
        {
            _context = context;
        }

        // GET: Employers
        public async Task<IActionResult> Index()
        {
            var jobFixaContext = _context.Employers.Include(e => e.JobFixaUser);
            return View(await jobFixaContext.ToListAsync());
        }

        // GET: Employers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Employers == null)
            {
                return NotFound();
            }

            var employer = await _context.Employers
                .Include(e => e.JobFixaUser)
                .FirstOrDefaultAsync(m => m.EmployerId == id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }

        // GET: Employers/Create
        public IActionResult Create()
        {
            ViewData["JobFIxaUserId"] = new SelectList(_context.JobFixaUsers, "JobFixaUserId", "JobFixaUserId");
            return View();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployerId,Name,Industry,Address,Logo,DateCreated,DateModified,JobFIxaUserId")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                employer.EmployerId = Guid.NewGuid();
                _context.Add(employer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobFixaUserId"] = new SelectList(_context.JobFixaUsers, "JobFixaUserId", "JobFixaUserId", employer.JobFixaUserId);
            return View(employer);
        }

        // GET: Employers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Employers == null)
            {
                return NotFound();
            }

            var employer = await _context.Employers.FindAsync(id);
            if (employer == null)
            {
                return NotFound();
            }
            ViewData["JobFixaUserId"] = new SelectList(_context.JobFixaUsers, "JobFixaUserId", "JobFixaUserId", employer.JobFixaUserId);
            return View(employer);
        }

        // POST: Employers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EmployerId,Name,Industry,Address,Logo,DateCreated,DateModified,JobFIxaUserId")] Employer employer)
        {
            if (id != employer.EmployerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployerExists(employer.EmployerId))
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
            ViewData["JobFixaUserId"] = new SelectList(_context.JobFixaUsers, "JobFixaUserId", "JobFixaUserId", employer.JobFixaUserId);
            return View(employer);
        }

        // GET: Employers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Employers == null)
            {
                return NotFound();
            }

            var employer = await _context.Employers
                .Include(e => e.JobFixaUser)
                .FirstOrDefaultAsync(m => m.EmployerId == id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }

        // POST: Employers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Employers == null)
            {
                return Problem("Entity set 'JobFixaContext.Employers'  is null.");
            }
            var employer = await _context.Employers.FindAsync(id);
            if (employer != null)
            {
                _context.Employers.Remove(employer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployerExists(Guid id)
        {
          return (_context.Employers?.Any(e => e.EmployerId == id)).GetValueOrDefault();
        }
    }
}
