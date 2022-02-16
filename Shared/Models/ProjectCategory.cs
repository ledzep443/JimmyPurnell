using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class ProjectCategory
    {
        [Key]
        public int ProjectCategoryId { get; set; }
        [Required]
        [MaxLength(256)]
        public string ThumbnailImagePath { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }
        public List<Project> Projects { get; set; }
    }
}
