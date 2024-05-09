using EduOne.Enums;
using EduOne.Entities;
using EduOne.DataAccess;
using EduOne.Services.Courses;
using EduOne.Services.Students;
using EduOne.Models.Enrollments;
using Microsoft.EntityFrameworkCore;
using EduOne.Mappers;

namespace EduOne.Services.Enrollments;

public class EnrollmentService : IEnrollmentService
{
    private readonly AppDbContext context;
    private readonly IStudentService studentService;
    private readonly ICourseService courseService;
    public EnrollmentService(AppDbContext context, IStudentService studentService, ICourseService courseService)
    {
        this.context = context;
        this.studentService = studentService;
        this.courseService = courseService;
    }

    public async Task<EnrollmentViewModel> CreateAsync(EnrollmentCreateModel createModel)
    {
        var existEnrollment = await context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == createModel.StudentId && e.CourseId == createModel.CourseId);
        if (existEnrollment is not null)
            throw new Exception("This enrollment is already exists!");

        var existCourse = await courseService.GetByIdAsync(createModel.CourseId);
        var existStudent = await studentService.GetByIdAsync(createModel.StudentId);

        var createdEnrollment = (await context.Enrollments.AddAsync(Mapper.Map(createModel))).Entity;

        await context.SaveChangesAsync();

        return Mapper.Map(existEnrollment, existCourse);
    }

    public async Task<EnrollmentViewModel> UpdateAsync(long id, EnrollmentUpdateModel updateModel)
    {
        var existEnrollment = await context.Enrollments.FirstOrDefaultAsync(e => e.Id == id)
            ?? throw new Exception($"Enrollment is not found with this ID={id}");

        var existStudent = await studentService.GetByIdAsync(updateModel.StudentId);
        var existCourse = await courseService.GetByIdAsync(updateModel.CourseId);

        existEnrollment.Grade = updateModel.Grade;
        existEnrollment.UpdatedAt = DateTime.UtcNow;
        existEnrollment.CourseId = updateModel.CourseId;
        existEnrollment.StudentId = updateModel.StudentId;

        context.Enrollments.Update(existEnrollment);
        await context.SaveChangesAsync();

        return Mapper.Map(existEnrollment, existCourse);
    }
    
    public async Task<bool> DeleteAsync(long id)
    {
        var existEnrollment = await context.Enrollments.FirstOrDefaultAsync(e => e.Id == id)
            ?? throw new Exception($"Enrollment is not found with this ID={id}");

        existEnrollment.IsDeleted = true;
        existEnrollment.DeletedAt = DateTime.UtcNow;

        context.Enrollments.Update(existEnrollment);
        await context.SaveChangesAsync();

        return true;
    }
   
    public async Task<EnrollmentViewModel> GetByIdAsync(long id)
    {
        var existEnrollment = await context.Enrollments.FirstOrDefaultAsync(e => e.Id == id)
            ?? throw new Exception($"Enrollment is not found with this ID={id}");

        return Mapper.Map(existEnrollment, existEnrollment.Course);
    }

    public async Task<IEnumerable<EnrollmentViewModel>> GetAllAsync(long? courseId = null, long? studentId = null, EnrollmentStatus? status = null)
    {
        var enrollments = await context.Enrollments.ToListAsync();

        if(courseId is not null)
            enrollments = enrollments.Where(e => e.CourseId == courseId).ToList();

        if (studentId is not null)
            enrollments = enrollments.Where(e => e.StudentId == studentId).ToList();

        if (status is not null)
            enrollments = enrollments.Where(e => e.Status == status).ToList();

        var result = new List<EnrollmentViewModel>();
        foreach (var enrollment in enrollments)
        {
            var existStudent = await studentService.GetByIdAsync(enrollment.StudentId);
            var existCourse = await courseService.GetByIdAsync(enrollment.CourseId);
            result.Add(Mapper.Map(enrollment, existCourse));
        }

        return result;
    }

    public async Task<EnrollmentViewModel> SetGradeAsync(char grade, long courseId, long enrollmentId, long studentId)
    {
        var existEnrollment = await context.Enrollments.FirstOrDefaultAsync(e => e.Id == enrollmentId)
            ?? throw new Exception($"Enrollment is not found with this ID={enrollmentId}");

        var existCourse = await courseService.GetByIdAsync(courseId);
        var existStudent = await studentService.GetByIdAsync(studentId);

        if (existCourse.Status != EnrollmentStatus.Completed)
            throw new Exception("This course is not yet completed");

        existEnrollment.Grade = grade;
        context.Enrollments.Update(existEnrollment);
        await context.SaveChangesAsync();
        
        return Mapper.Map(existEnrollment, existCourse);
    }

    public async Task<EnrollmentViewModel> ChangeStatusAsync(long enrollmentId, EnrollmentStatus status)
    {
        var existEnrollment = await context.Enrollments.FirstOrDefaultAsync(e => e.Id == enrollmentId)
            ?? throw new Exception($"Enrollment is not found with this ID={enrollmentId}");

        existEnrollment.Status = status;
        context.Enrollments.Update(existEnrollment);
        await context.SaveChangesAsync();

        var existCourse = await courseService.GetByIdAsync(existEnrollment.CourseId);
        var existStudent = await studentService.GetByIdAsync(existEnrollment.StudentId);

        return Mapper.Map(existEnrollment, existCourse);
    }
}