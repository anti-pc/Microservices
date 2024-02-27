using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Catalog
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }


        public FeatureViewModel Feature { get; set; }

        [Required]
        [Display(Name = "Course Category")]
        public string CategoryId { get; set; }

        [Display(Name = "Course Photo")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
