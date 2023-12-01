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
    public class JobListingsController : Controller
    {
        private readonly JobFixaContext _context;

        public JobListingsController(JobFixaContext context)
        {
            _context = context;
        }

        // GET: JobListings
        public async Task<IActionResult> Index()
        {
            var jobFixaContext = _context.JobListings.Include(j => j.Employer);
            return View(await jobFixaContext.ToListAsync());
        }

        // GET: JobListings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.JobListings == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings
                .Include(j => j.Employer)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (jobListing == null)
            {
                return NotFound();
            }

            return View(jobListing);
        }

        // GET: JobListings/Create
        public IActionResult Create()
        {
            ViewData["EmployerId"] = new SelectList(_context.Employers, "EmployerId", "EmployerId");
            return View();
        }

        // POST: JobListings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,JobTitle,Location,Description,SalaryRange,JobStatus,DatePosted,DateClosed,DateCreated,DateModified,EmployerId")] JobListing jobListing)
        {
            if (ModelState.IsValid)
            {
                jobListing.JobId = Guid.NewGuid();
                _context.Add(jobListing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployerId"] = new SelectList(_context.Employers, "EmployerId", "EmployerId", jobListing.EmployerId);
            return View(jobListing);
        }

        // GET: JobListings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.JobListings == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings.FindAsync(id);
            if (jobListing == null)
            {
                return NotFound();
            }
            ViewData["EmployerId"] = new SelectList(_context.Employers, "EmployerId", "EmployerId", jobListing.EmployerId);
            return View(jobListing);
        }

        // POST: JobListings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("JobId,JobTitle,Location,Description,SalaryRange,JobStatus,DatePosted,DateClosed,DateCreated,DateModified,EmployerId")] JobListing jobListing)
        {
            if (id != jobListing.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobListing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobListingExists(jobListing.JobId))
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
            ViewData["EmployerId"] = new SelectList(_context.Employers, "EmployerId", "EmployerId", jobListing.EmployerId);
            return View(jobListing);
        }

        // GET: JobListings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.JobListings == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings
                .Include(j => j.Employer)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (jobListing == null)
            {
                return NotFound();
            }

            return View(jobListing);
        }

        // POST: JobListings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.JobListings == null)
            {
                return Problem("Entity set 'JobFixaContext.JobListings'  is null.");
            }
            var jobListing = await _context.JobListings.FindAsync(id);
            if (jobListing != null)
            {
                _context.JobListings.Remove(jobListing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobListingExists(Guid id)
        {
          return (_context.JobListings?.Any(e => e.JobId == id)).GetValueOrDefault();
        }
    }
}
