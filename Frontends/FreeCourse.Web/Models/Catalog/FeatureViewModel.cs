using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Catalog
{
    public class FeatureViewModel
    {
        [Required]
        [Display(Name = "Course Duration")]
        public int Duration { get; set; }
    }
}
