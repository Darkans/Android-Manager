using System.Collections.Generic;

namespace AndroidManagerApplication.Models.Entities
{
    public class Skill: BaseEntity
    {
        /*
         * Constants
         */

        const string DEFAULT_NAME = "N/A";

        /*
         *  Properties
         */

        public string Name { get; set; }

        public ICollection<Android> Androids { get; set; }

        /*
         *  Constructors
         */

        public Skill()
        {
            Name = DEFAULT_NAME;
            Androids = new List<Android>();
        }
    }
}