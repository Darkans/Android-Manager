using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AndroidManagerApplication.Models.Entities
{
    public class Job: BaseEntity
    {
        /*
         *  Constants
         */

        const string DEFAULT_NAME = "N/A";
        const int DEFAULT_COMPLEXITY_LEVEL = (int)ComplexityLevel.Easy;
        const string DEFAULT_DESCRIBTION = "";

        /*
         *  Properties
         */
        [Required]
        [MinLength(1, ErrorMessage = "Name should contain at least 1 characters")]
        [MaxLength(16, ErrorMessage = "Name should contain less than 17 characters")]
        public string Name { get; set; }

        [MaxLength(255, ErrorMessage = "Describtion's length should be less than 256 characters")]
        public string Describtion { get; set; }

        public int Complexity { get; set; }

        public ICollection<Android> Androids { get; set; }

        /*
         *  Constructors
         */
        
        public Job()
        {
            Name = DEFAULT_NAME;
            Complexity = DEFAULT_COMPLEXITY_LEVEL;
            Describtion = DEFAULT_DESCRIBTION;
            Androids = new List<Android>();
        }
    }

    public enum ComplexityLevel
    {
        Safety,
        Easy,
        Medium,
        Hard,
        Suicide
    }
}