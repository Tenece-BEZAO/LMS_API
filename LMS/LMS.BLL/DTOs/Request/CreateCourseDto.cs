using System.ComponentModel.DataAnnotations;

namespace LMS.BLL.DTOs.Request
{
    public class CreateCourseDto
    {
        [Required(ErrorMessage = "Course title is required")]
        [StringLength(50, ErrorMessage = "Title length should be between 5 to 50 characters", MinimumLength = 5)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Course detail is required")]
        [StringLength(1000, ErrorMessage = "Detail length should be between 20 to 1000 characters", MinimumLength = 20)]
        public string Detail { get; set; }
        [Required(ErrorMessage = "Course Header Image is required")]
        public string HeaderImageUrl { get; set; }
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Course Video resource is required")]

        public string VideoResourceUrl { get; set; }
        [Required(ErrorMessage = "Course Text resource is required")]
        public string TextResourceUrl { get; set; }
        public string? AdditionalResourcesUrl { get; set; }
        [Required(ErrorMessage = "Course type is required")]
        public string CourseType { get; set; }
        public int InstructorId { get; set; }
    }
}
