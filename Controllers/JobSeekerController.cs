using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobFixa.Data;
using JobFixa.Entities;

namespace JobFixa.Controllers
{
    public class JobSeekerController : Controller
    {
        private readonly JobFixaContext _context;

        public JobSeekerController(JobFixaContext context)
        {
            _context = context;
        }

        // GET: JobSeeker
        public async Task<IActionResult> Index()
        {
              return _context.JobSeekers != null ? 
                          View(await _context.JobSeekers.ToListAsync()) :
                          Problem("Entity set 'JobFixaContext.JobSeeker'  is null.");
        }

        // GET: JobSeeker/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.JobSeekers == null)
            {
                return NotFound();
            }

            var jobSeeker = await _context.JobSeekers
                .FirstOrDefaultAsync(m => m.JobSeekerId == id);
            if (jobSeeker == null)
            {
                return NotFound();
            }

            return View(jobSeeker);
        }

        // GET: JobSeeker/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobSeeker/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobSeekerId,EducationalLevel,Name,Skills,DateCreated,DateModified")] JobSeeker jobSeeker)
        {
            if (ModelState.IsValid)
            {
                jobSeeker.JobSeekerId = Guid.NewGuid();
                _context.Add(jobSeeker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobSeeker);
        }

        // GET: JobSeeker/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.JobSeekers == null)
            {
                return NotFound();
            }

            var jobSeeker = await _context.JobSeekers.FindAsync(id);
            if (jobSeeker == null)
            {
                return NotFound();
            }
            return View(jobSeeker);
        }

        // POST: JobSeeker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("JobSeekerId,EducationalLevel,Name,Skills,DateCreated,DateModified")] JobSeeker jobSeeker)
        {
            if (id != jobSeeker.JobSeekerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobSeeker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobSeekerExists(jobSeeker.JobSeekerId))
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
            return View(jobSeeker);
        }

        // GET: JobSeeker/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.JobSeekers == null)
            {
                return NotFound();
            }

            var jobSeeker = await _context.JobSeekers
                .FirstOrDefaultAsync(m => m.JobSeekerId == id);
            if (jobSeeker == null)
            {
                return NotFound();
            }

            return View(jobSeeker);
        }

        // POST: JobSeeker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.JobSeekers == null)
            {
                return Problem("Entity set 'JobFixaContext.JobSeeker'  is null.");
            }
            var jobSeeker = await _context.JobSeekers.FindAsync(id);
            if (jobSeeker != null)
            {
                _context.JobSeekers.Remove(jobSeeker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobSeekerExists(Guid id)
        {
          return (_context.JobSeekers?.Any(e => e.JobSeekerId == id)).GetValueOrDefault();
        }
    }
}
