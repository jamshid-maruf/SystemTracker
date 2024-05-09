using EduOne.Enums;

namespace EduOne.Entities;

public class Enrollment : Auditable
{
    public char? Grade { get; set; }
    public EnrollmentStatus Status { get; set; }
    public long StudentId { get; set; }
    public virtual Student Student { get; set; }
    public long CourseId { get; set; }
    public virtual Course Course { get; set; }
}