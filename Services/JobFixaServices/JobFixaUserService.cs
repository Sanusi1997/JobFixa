﻿using JobFixa.Data;
using JobFixa.Entities;
using JobFixa.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobFixa.Services.JobFixaServices
{
    public class JobFixaUserService : IJobFixaUserService
    {
        private readonly JobFixaContext _context;
        public JobFixaUserService(JobFixaContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public JobFixaUser AddUser(JobFixaUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            // the repository fills the id (instead of using identity columns)
            user.JobFixaUserId = Guid.NewGuid();
            user.DateCreated = DateTime.UtcNow;
            _context.JobFixaUsers.Add(user);
            _context.SaveChanges();

            //create a new entity of either employer or job seeker 
            if (user.UserType == "Employer")
            {

                Employer emp = new Employer();
                emp.JobFixaUserId = user.JobFixaUserId;
                emp.EmployerId = new Guid();
                emp.DateCreated = DateTime.UtcNow;
                _context.Employers.Add(emp);
                _context.SaveChanges();
                return user;
            }
            else {
            
                JobSeeker seeker = new JobSeeker();
                seeker.DateCreated = DateTime.UtcNow;
                seeker.JobFixaUserId= user.JobFixaUserId;
                seeker.DateCreated= DateTime.UtcNow;
                _context.JobSeekers.Add(seeker);
                _context.SaveChanges();
                return user;
            }
        }

        public async Task<IEnumerable<JobFixaUser>> GetJobFixaUsers()
        {
            return await _context.JobFixaUsers.AsQueryable().ToListAsync();
        }

        public async Task<JobFixaUser> Update(JobFixaUser user)
        {
            user.DateModified = DateTime.UtcNow;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;

        }
        public async Task<JobFixaUser> GetUserByEmail(string email)
        {
#pragma warning disable CS8603 // Possible null reference argument.
            return await _context.JobFixaUsers.AsQueryable().Where(u => u.Email == email).FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference argument.
        }
        public async Task<JobFixaUser> GetUserById(string userId)
        {
#pragma warning disable CS8603 // Possible null reference argument.
            return await _context.JobFixaUsers.AsQueryable().Where(u => u.JobFixaUserId.ToString() == userId).FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference argument.
        }
        public async Task<JobFixaUser> AuthenticateUser(string email, string password)
        {
#pragma warning disable CS8603 // Possible null reference argument.
            return await _context.JobFixaUsers.AsQueryable().Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference argument.
        }

    }
}