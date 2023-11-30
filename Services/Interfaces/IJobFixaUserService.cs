using JobFixa.Entities;

namespace JobFixa.Services.Interfaces
{
    public interface IJobFixaUserService
    {
        void AddUser(JobFixaUser user);
        Task<JobFixaUser> Update(JobFixaUser user);
        Task<IEnumerable<JobFixaUser>> GetJobFixaUsers();
        Task<JobFixaUser> GetUserByEmail(string email);
        Task<JobFixaUser> GetUserById(string userId);
        Task<JobFixaUser> AuthenticateUser(string email, string password);

    }
}
