

using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class ProjectDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string URL { get; set; }
        public string GitHub { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        [Required]
        public int ProjectCategoryId { get; set; }
    }
}
