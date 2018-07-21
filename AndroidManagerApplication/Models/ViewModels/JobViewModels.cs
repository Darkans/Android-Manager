using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AndroidManagerApplication.Models.Entities;

namespace AndroidManagerApplication.Models.ViewModels
{
    public class JobViewModel
    {
        [Required(ErrorMessage = "Enter name")]
        [RegularExpression(@"^[a-zA-Z0-9\x20]*$", ErrorMessage = "Name must contain only alpha-numeric characters")]
        [MinLength(1, ErrorMessage = "Name should contain at least 1 characters")]
        [MaxLength(16, ErrorMessage = "Name should contain less than 17 characters")]
        public string Name { get; set; }

        [MaxLength(255, ErrorMessage = "Describtion's length should be less than 256 characters")]
        public string Describtion { get; set; }

        [Required(ErrorMessage = "Set complexity")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Reliability value must be an integer number")]
        [Range(0, 100, ErrorMessage ="Complexity value must be between 0 and 100")]
        public int Complexity { get; set; }
    }
    
    public class AndroidsAssignedToJobViewModel
    {
        public ICollection<Android> AndroidList { get; set; }
        public Job CurrentJob { get; set; }

        public bool IsAndroidAssigned(Android android)
        {
            return CurrentJob.Androids.Any(a => a.Id == android.Id);
        }
    }
}