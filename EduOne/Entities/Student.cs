namespace EduOne.Entities;

public class Student : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public virtual ICollection<Course> Courses { get; set; }
}