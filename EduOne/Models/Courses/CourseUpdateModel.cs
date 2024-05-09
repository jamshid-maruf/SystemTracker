namespace EduOne.Models.Courses;

public class CourseUpdateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public long InstructorId { get; set; }
}