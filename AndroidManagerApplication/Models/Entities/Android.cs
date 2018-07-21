using System.Collections.Generic;

namespace AndroidManagerApplication.Models.Entities
{
    public class Android: BaseEntity
    {
        /*
         *  Constants
         */

        const string DEFAULT_NAME = "N/A";

        const int DEFAULT_RELIABILITY = 10;
        const int MIN_RELIABILITY = 0;

        const int DEFAULT_STATUS = GOOD_STATUS;
        const int GOOD_STATUS = 1;
        const int BAD_STATUS = 0;

        /*
         *  Properties
         */

        public string Name { get; set; }
        public Image Avatar { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public int Reliability { get; set; }

        public bool Available
        {
            get { return Reliability != MIN_RELIABILITY; }
        }

        public Job CurrentJob { get; set; }

        /*
         *  Constructors
         */

        public Android()
        {
            Name = DEFAULT_NAME;
            Skills = new List<Skill>();
            Reliability = DEFAULT_RELIABILITY;
        }

        /*
         *  Methods
         */

        public void ChangeJob(Job newJob)
        {
            // Affect on current job and reliability
            if (Available)
            {
                CurrentJob = newJob;
                Reliability--;
            }
        }
    }
}