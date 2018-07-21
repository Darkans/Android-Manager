using System.Data.Entity;
using System.Linq;
using AndroidManagerApplication.Models.Entities;

namespace AndroidManagerApplication.Models.Managers
{
    public class SkillManager: BaseManager<Skill>
    {
        protected override DbSet<Skill> GetDbSet()
        {
            return _dataSource.SkillList;
        }

        public Skill GetOrCreateSkillByName(string skillName)
        {
            if (!IsSkillExists(skillName)) Add(new Skill() { Name = skillName});
            return _dataSource.SkillList.FirstOrDefault(s => s.Name == skillName);
        }

        private bool IsSkillExists(string skillName)
        {
            return _dataSource.SkillList.Any(s => s.Name == skillName);
        }
    }
}