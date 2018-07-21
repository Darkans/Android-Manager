using System.ComponentModel.DataAnnotations;

namespace AndroidManagerApplication.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}