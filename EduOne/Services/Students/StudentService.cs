using EduOne.Entities;
using EduOne.DataAccess;
using EduOne.Models.Students;
using Microsoft.EntityFrameworkCore;
using EduOne.Mappers;

namespace EduOne.Services.Students;

public class StudentService : IStudentService
{
    private readonly AppDbContext context;
    public StudentService(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<StudentViewModel> CreateAsync(StudentCreateModel createModel)
    {
        var existStudent = await context.Students.FirstOrDefaultAsync(student => student.Phone == createModel.Phone);
        if (existStudent is not null)
            throw new Exception($"This student is already exists with this phone={createModel.Phone}");

        var createdStudent = (await context.Students.AddAsync(Mapper.Map(createModel))).Entity;
        await context.SaveChangesAsync();

        return Mapper.Map(existStudent);
    }
    
    public async Task<StudentViewModel> UpdateAsync(long id, StudentUpdateModel updateModel)
    {
        var existStudent = await context.Students.FirstOrDefaultAsync(student => student.Id == id)
            ?? throw new Exception($"Student is not found with this ID={id}");

        var similarStudent = await context.Students.FirstOrDefaultAsync(student => student.Phone == updateModel.Phone && student.Id != id);
        if (similarStudent is not null)
            throw new Exception($"This student is already exists with this phone={updateModel.Phone}");

        existStudent.Phone = updateModel.Phone;
        existStudent.LastName = updateModel.LastName;
        existStudent.FirstName = updateModel.FirstName;
        existStudent.UpdatedAt = DateTime.UtcNow;

        context.Students.Update(existStudent);
        await context.SaveChangesAsync();

        return Mapper.Map(existStudent);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existStudent = await context.Students.FirstOrDefaultAsync(student => student.Id == id)
            ?? throw new Exception($"Student is not found with this ID={id}");

        existStudent.IsDeleted = true;
        existStudent.DeletedAt = DateTime.UtcNow;

        context.Students.Update(existStudent);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<StudentViewModel> GetByIdAsync(long id)
    {
        var existStudent = await context.Students.FirstOrDefaultAsync(student => student.Id == id)
            ?? throw new Exception($"Student is not found with this ID={id}");

        return Mapper.Map(existStudent);
    }

    public async Task<IEnumerable<StudentViewModel>> GetAllAsync()
    {
        return Mapper.Map(await context.Students.ToListAsync());
    }

    public async Task<IEnumerable<StudentViewModel>> GetAllByCourseIdAsync(long courseId)
    {
        var existCourse = await context.Courses.FirstOrDefaultAsync(course => course.Id == courseId)
            ?? throw new Exception($"Course is not found with this ID={courseId}");

        var students = await context.Students.ToListAsync();
        var result = new List<StudentViewModel>();

        foreach (var student in students)
        {
            var existEnrollment = await context.Enrollments
                .FirstOrDefaultAsync(enrollment => enrollment.StudentId == student.Id);
            if (existEnrollment is not null)
                result.Add(Mapper.Map(student));
        }

        return result;
    }
}