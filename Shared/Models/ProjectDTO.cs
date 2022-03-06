

using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class ProjectDTO
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        public string Description { get; set; }
        public string ScreenshotImagePath { get; set; }
        public string URL { get; set; }
        public string GitHub { get; set; }
        [Required]
        public bool IsPublished { get; set; }
       
        [Required]
        public int ProjectCategoryId { get; set; }
    }
}
