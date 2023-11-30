using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JobFixa.Entities;

namespace JobFixa.Data
{
    public class JobFixaContext : DbContext
    {
        public JobFixaContext (DbContextOptions<JobFixaContext> options)
            : base(options)
        {
        }

        public DbSet<JobFixaUser> JobFixaUsers { get; set; }
        public DbSet<Employer> Employers { get; set; }

        public DbSet<JobListing> JobListings { get; set; }

        public DbSet<JobSeeker> JobSeekers { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelbuilder);
        }
    }
}
