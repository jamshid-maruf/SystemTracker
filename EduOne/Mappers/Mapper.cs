using EduOne.Entities;
using EduOne.Enums;
using EduOne.Models.Courses;
using EduOne.Models.Enrollments;
using EduOne.Models.Instructors;
using EduOne.Models.Students;
using EduOne.Services.Courses;
using EduOne.Services.Students;

namespace EduOne.Mappers;

public static class Mapper
{
    #region Courses

    public static CourseViewModel Map(Course course, Instructor instructor)
    {
        return new CourseViewModel
        {
            Id = course.Id,
            Description = course.Description,
            StartTime = course.StartTime,
            EndTime = course.EndTime,
            Instructor = new InstructorViewModel
            {
                Id = instructor.Id,
                Phone = instructor.Phone,
                LastName = instructor.LastName,
                FirstName = instructor.FirstName,
                Expertise = instructor.Expertise,
            }
        };
    }

    public static IEnumerable<CourseViewModel> Map(List<Course> courses)
    {
        var result = new List<CourseViewModel>();

        courses.ForEach(course =>
            result.Add(new CourseViewModel
            {
                Id = course.Id,
                Description = course.Description,
                EndTime = course.EndTime,
                Name = course.Name,
                StartTime = course.StartTime,
                Instructor = new InstructorViewModel
                {
                    Id = course.Instructor.Id,
                    Phone = course.Instructor.Phone,
                    LastName = course.Instructor.LastName,
                    FirstName = course.Instructor.FirstName,
                },
                // TODO Map students
            }));

        return result;
    }

    private static List<CourseViewModel> InnerMap(List<Course> courses)
    {
        var result = new List<CourseViewModel>();
        courses.ForEach(course => 
            result.Add(new CourseViewModel
            {
                Id = course.Id,
                EndTime = course.EndTime,
                StartTime = course.StartTime,
                Description = course.Description
            }));

        return result;
    }

    public static Course Map(CourseCreateModel createModel)
    {
        return new Course
        {
            Name = createModel.Name,
            EndTime = createModel.EndTime,
            StartTime = createModel.StartTime,
            Description = createModel.Description,
            InstructorId = createModel.InstructorId,
        };
    }

    #endregion

    #region Enrollments

    public static EnrollmentViewModel Map(Enrollment enrollment, Course course, Student student)
    {
        return new EnrollmentViewModel
        {
            Id = enrollment.Id,
            Status = enrollment.Status,
            Student = new StudentViewModel
            {
                Id = student.Id,
                Phone = student.Phone,
                LastName = student.LastName,
                FirstName = student.FirstName,
            },
            Course = new CourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                EndTime = course.EndTime,
                StartTime = course.StartTime,
                Description = course.Description,
                Instructor = new InstructorViewModel
                {
                    Id = course.Instructor.Id,
                    Phone = course.Instructor.Phone,
                    LastName = course.Instructor.LastName,
                    FirstName = course.Instructor.FirstName,
                }
            },
        };
    }

    public static EnrollmentViewModel Map(Enrollment enrollment, CourseViewModel course)
    {
        return new EnrollmentViewModel
        {
            Id = enrollment.Id,
            Status = enrollment.Status,
            Course = new CourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                EndTime = course.EndTime,
                StartTime = course.StartTime,
                Description = course.Description,
                Instructor = new InstructorViewModel
                {
                    Id = course.Instructor.Id,
                    Phone = course.Instructor.Phone,
                    LastName = course.Instructor.LastName,
                    FirstName = course.Instructor.FirstName,
                }
            },
        };
    }

    public static EnrollmentViewModel Map(Enrollment enrollment, Course course)
    {
        return new EnrollmentViewModel
        {
            Id = enrollment.Id,
            Status = enrollment.Status,
            Course = new CourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                EndTime = course.EndTime,
                StartTime = course.StartTime,
                Description = course.Description,
                Instructor = new InstructorViewModel
                {
                    Id = course.Instructor.Id,
                    Phone = course.Instructor.Phone,
                    LastName = course.Instructor.LastName,
                    FirstName = course.Instructor.FirstName,
                }
            },
        };
    }

    public static Enrollment Map(EnrollmentCreateModel createModel)
    {
        return new Enrollment
        {
            StudentId = createModel.StudentId,
            Status = EnrollmentStatus.InProsses,
            CourseId = createModel.CourseId,
        };
    }

    #endregion

    #region Instructors

    public static InstructorViewModel Map(Instructor instructor)
    {
        return new InstructorViewModel
        {
            Id = instructor.Id,
            Phone = instructor.Phone,
            LastName = instructor.LastName,
            FirstName = instructor.FirstName,
            Expertise = instructor.Expertise,
            Courses = InnerMap(instructor.Courses.ToList())
        };
    }

    public static IEnumerable<InstructorViewModel> Map(ICollection<Instructor> instructors)
    {
        var result = new List<InstructorViewModel>();

        instructors.ToList().ForEach(i =>
            result.Add(new InstructorViewModel
            {
                Id = i.Id,
                Phone = i.Phone,
                LastName = i.LastName,
                FirstName = i.FirstName,
                Expertise = i.Expertise,
                Courses = Map(i.Courses.ToList())
            }));

        return result; 
    }

    public static Instructor Map(InstructorCreateModel createModel)
    {
        return new Instructor
        {
            Phone = createModel.Phone,
            LastName = createModel.LastName,
            FirstName = createModel.FirstName,
            Expertise = createModel.Expertise,
        };
    }

    #endregion

    #region Students

    public static StudentViewModel Map(Student student)
    {
        return new StudentViewModel
        {
            Id = student.Id,
            Phone = student.Phone,
            LastName = student.LastName,
            FirstName = student.FirstName,
        };
    }

    public static IEnumerable<StudentViewModel> Map(List<Student> students)
    {
        var result = new List<StudentViewModel>();
        students.ForEach(student =>
            result.Add(new StudentViewModel
            {
                Id = student.Id,
                Phone = student.Phone,
                LastName = student.LastName,
                FirstName = student.FirstName,
            }));

        return result;
    }

    public static Student Map(StudentCreateModel createModel)
    {
        return new Student
        {
            Phone = createModel.Phone,
            LastName = createModel.LastName,
            FirstName = createModel.FirstName
        };
    }

    #endregion
}
