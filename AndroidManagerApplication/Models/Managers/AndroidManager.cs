using System.Data.Entity;
using System.Linq;
using AndroidManagerApplication.Models.Entities;

namespace AndroidManagerApplication.Models.Managers
{
    // Provide CRUD operations for Android entities
    public class AndroidManager: BaseManager<Android>
    {
        protected override DbSet<Android> GetDbSet()
        {
            return _dataSource.AndroidList;
        }

        // Use for assign android to job with decreasing Reliability field
        public void ChangeJob(Android android, Job job)
        {
            if (!job.Androids.Any(j => j.Id == android.Id))
                android.ChangeJob(job);
            _dataSource.SaveChanges();
        }

        public void RemoveJob(Android android)
        {
            android.CurrentJob = null;
            _dataSource.SaveChanges();
        }
    }
}