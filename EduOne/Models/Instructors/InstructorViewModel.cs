using EduOne.Models.Courses;

namespace EduOne.Models.Instructors;

public class InstructorViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Expertise { get; set; }
    public IEnumerable<CourseViewModel> Courses { get; set; }
}