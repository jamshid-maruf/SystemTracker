namespace EduOne.Entities;

public class Course : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public long InstructorId { get; set; }
    public virtual Instructor Instructor { get; set; }
    public virtual ICollection<Student> Students { get; set; }
}