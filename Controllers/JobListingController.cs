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
    public class JobListingController : Controller
    {
        private readonly JobFixaContext _context;

        public JobListingController(JobFixaContext context)
        {
            _context = context;
        }

        // GET: JobListing
        public async Task<IActionResult> Index()
        {
              return _context.JobListings != null ? 
                          View(await _context.JobListings.ToListAsync()) :
                          Problem("Entity set 'JobFixaContext.JobListing'  is null.");
        }

        // GET: JobListing/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.JobListings == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (jobListing == null)
            {
                return NotFound();
            }

            return View(jobListing);
        }

        // GET: JobListing/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobListing/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,JobTitle,Location,Description,SalaryRange,JobStatus,DatePosted,DateClosed,DateCreated,DateModified")] JobListing jobListing)
        {
            if (ModelState.IsValid)
            {
                jobListing.JobId = Guid.NewGuid();
                _context.Add(jobListing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobListing);
        }

        // GET: JobListing/Edit/5
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
            return View(jobListing);
        }

        // POST: JobListing/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("JobId,JobTitle,Location,Description,SalaryRange,JobStatus,DatePosted,DateClosed,DateCreated,DateModified")] JobListing jobListing)
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
            return View(jobListing);
        }

        // GET: JobListing/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.JobListings == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (jobListing == null)
            {
                return NotFound();
            }

            return View(jobListing);
        }

        // POST: JobListing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.JobListings == null)
            {
                return Problem("Entity set 'JobFixaContext.JobListing'  is null.");
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
