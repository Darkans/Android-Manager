using AndroidManagerApplication.Models.Entities;
using System.Data.Entity;

namespace AndroidManagerApplication.Models.Managers
{
    // Provide CRUD operations for Job entities
    public class JobManager: BaseManager<Job>
    {
        protected override DbSet<Job> GetDbSet()
        {
            return _dataSource.JobList;
        }

        // Use for assign android to job without influence on Reliability
        public void AssignAndroidToJob(Job job, Android android)
        {
            android.CurrentJob = job;
            _dataSource.SaveChanges();
        }
    }
}