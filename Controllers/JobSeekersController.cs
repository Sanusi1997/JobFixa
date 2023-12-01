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
    public class JobSeekersController : Controller
    {
        private readonly JobFixaContext _context;

        public JobSeekersController(JobFixaContext context)
        {
            _context = context;
        }

 

        // GET: JobSeekers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.JobSeekers == null)
            {
                return NotFound();
            }

            var jobSeeker = await _context.JobSeekers
                .Include(j => j.JobFixaUser)
                .FirstOrDefaultAsync(m => m.JobSeekerId == id);
            if (jobSeeker == null)
            {
                return NotFound();
            }

            return View(jobSeeker);
        }


       

        // GET: JobSeekers/Edit/5
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
            ViewData["JobFixaUserId"] = new SelectList(_context.JobFixaUsers, "JobFixaUserId", "JobFixaUserId", jobSeeker.JobFixaUserId);
            return View(jobSeeker);
        }

        // POST: JobSeekers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("JobSeekerId,EducationalLevel,Name,Skills,DateCreated,DateModified,JobFIxaUserId")] JobSeeker jobSeeker)
        {
            if (id != jobSeeker.JobSeekerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    jobSeeker.DateModified = DateTime.UtcNow;
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
            ViewData["JobFixaUserId"] = new SelectList(_context.JobFixaUsers, "JobFixaUserId", "JobFixaUserId", jobSeeker.JobFixaUserId);
            return View(jobSeeker);
        }

        // GET: JobSeekers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.JobSeekers == null)
            {
                return NotFound();
            }

            var jobSeeker = await _context.JobSeekers
                .Include(j => j.JobFixaUser)
                .FirstOrDefaultAsync(m => m.JobSeekerId == id);
            if (jobSeeker == null)
            {
                return NotFound();
            }

            return View(jobSeeker);
        }

        // POST: JobSeekers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.JobSeekers == null)
            {
                return Problem("Entity set 'JobFixaContext.JobSeekers'  is null.");
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
