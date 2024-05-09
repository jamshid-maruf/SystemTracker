using EduOne.Enums;
using EduOne.Models.Students;
using EduOne.Models.Instructors;

namespace EduOne.Models.Courses;

public class CourseViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public EnrollmentStatus Status { get; set; }
    public InstructorViewModel Instructor { get; set; }
    public IEnumerable<StudentViewModel> Students { get; set; }
}
