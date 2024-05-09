namespace EduOne.Entities;

public class Instructor : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Expertise { get; set; }
    public virtual ICollection<Course> Courses { get; set; }
}