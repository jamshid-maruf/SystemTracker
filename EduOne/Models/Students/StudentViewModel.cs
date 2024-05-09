using EduOne.Entities;
using EduOne.Models.Courses;

namespace EduOne.Models.Students;

public class StudentViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public ICollection<CourseViewModel> Courses { get; set; }
}