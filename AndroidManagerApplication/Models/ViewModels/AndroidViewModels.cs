using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AndroidManagerApplication.Models.ViewModels
{
    public class EditAndroidViewModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter name")]
        [MinLength(1, ErrorMessage = "Name must contain at least 1 characters")]
        [MaxLength(24, ErrorMessage = "Name must contain less than 25 characters")]
        public string Name { get; set; }

        public HttpPostedFileBase Avatar { get; set; }

        [Display(Name = "List of skills")]
        [Required(ErrorMessage = "Add some skills")]
        public string Skills { get; set; }

        [Display(Name = "Reliability")]
        [Range(0, 100, ErrorMessage = "Reliability value can be from 0 to 100")]
        public int Reliability { get; set; }

        [Display(Name = "Assign to job")]
        public int? JobId { get; set; }
    }
}