namespace EduOne.Models.Enrollments;

public class EnrollmentUpdateModel
{
    public long StudentId { get; set; }
    public long CourseId { get; set; }
    public char Grade { get; set; }
}