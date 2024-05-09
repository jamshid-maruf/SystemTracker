using EduOne.Enums;
using EduOne.Models.Students;
using EduOne.Models.Instructors;
using EduOne.Models.Courses;

namespace EduOne.Models.Enrollments;

public class EnrollmentViewModel
{
    public long Id { get; set; }
    public char Grade { get; set; }
    public EnrollmentStatus Status { get; set; }
    public StudentViewModel Student { get; set; }
    public CourseViewModel Course { get; set; }
}