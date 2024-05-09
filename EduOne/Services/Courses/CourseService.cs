using EduOne.Entities;
using EduOne.DataAccess;
using EduOne.Models.Courses;
using EduOne.Services.Students;
using EduOne.Models.Instructors;
using Microsoft.EntityFrameworkCore;
using EduOne.Mappers;

namespace EduOne.Services.Courses;

public class CourseService : ICourseService
{
    private readonly AppDbContext context;
    private readonly IStudentService studentsService;
    public CourseService(AppDbContext context, IStudentService studentsService)
    {
        this.context = context;
        this.studentsService = studentsService;
    }

    public async Task<CourseViewModel> CreateAsync(CourseCreateModel createModel)
    {
        var existCourse = await context.Courses
            .FirstOrDefaultAsync(c => c.Name.ToLower() == createModel.Name.ToLower());

        if (existCourse is not null)
            throw new Exception($"Course is already exists with this name={createModel.Name}");

        var existInstructor = await context.Instructors.FirstOrDefaultAsync(i => i.Id == createModel.InstructorId && !i.IsDeleted)
            ?? throw new Exception($"Instructor is not found with this ID={createModel.InstructorId}");

        var createdCourse = (await context.Courses.AddAsync(Mapper.Map(createModel))).Entity;
        await context.SaveChangesAsync();

        return Mapper.Map(createdCourse, existInstructor);
    }
    
    public async Task<CourseViewModel> UpdateAsync(long id, CourseUpdateModel updateModel)
    {
        var existCourse = await context.Courses.FirstOrDefaultAsync(course => course.Id == id)
            ?? throw new Exception($"Course is not found with this ID={id}");

        var similarCourse = await context.Courses
            .FirstOrDefaultAsync(c => c.Name.ToLower() == updateModel.Name.ToLower() && c.Id != id);
        
        if (similarCourse is not null)
            throw new Exception($"This course is already exists with this name={updateModel.Name}");

        var existInstructor = await context.Instructors.FirstOrDefaultAsync(i => i.Id == updateModel.InstructorId)
            ?? throw new Exception($"Instructor is not found with this ID={updateModel.InstructorId}");

        existCourse.Name = updateModel.Name;
        existCourse.EndTime = updateModel.EndTime;
        existCourse.StartTime = updateModel.StartTime;
        existCourse.Description = updateModel.Description;
        existCourse.InstructorId = updateModel.InstructorId;
        existCourse.UpdatedAt = DateTime.UtcNow;
        context.Courses.Update(existCourse);
        await context.SaveChangesAsync();

        return Mapper.Map(existCourse, existInstructor);
    }
    
    public async Task<bool> DeleteAsync(long id)
    {
        var existCourse = await context.Courses.FirstOrDefaultAsync(course => course.Id == id)
            ?? throw new Exception($"Course is not found with this ID={id}");

        context.Courses.Remove(existCourse);
        await context.SaveChangesAsync();

        return true;
    }
   
    public async Task<CourseViewModel> GetByIdAsync(long id)
    {
        var existCourse = await context.Courses.FirstOrDefaultAsync(course => course.Id == id)
            ?? throw new Exception($"Course is not found with this ID={id}");

        var existInstructor = await context.Instructors.FirstOrDefaultAsync(i => i.Id == existCourse.InstructorId)
            ?? throw new Exception($"Instructor is not found with this ID={existCourse.InstructorId}");

        var students = await studentsService.GetAllByCourseIdAsync(existCourse.Id);

        return Mapper.Map(existCourse, existInstructor);
    }

    public async Task<IEnumerable<CourseViewModel>> GetAllAsync(long? instructorId = null, DateTime? date = null)
    {
        var courses = await context.Courses
            .Include(course => course.Instructor)
            .Include(course => course.Students)
            .ToListAsync();

        if (instructorId is not null)
        {
            var existInstructor = await context.Instructors.FirstOrDefaultAsync(i => i.Id == instructorId)
                ?? throw new Exception($"Instructor is not found with this ID={instructorId}");
            courses = courses.Where(course => course.InstructorId == instructorId).ToList();
        }

        if (date is not null)
            courses = courses.Where(course => course.StartTime == date).ToList();

        return Mapper.Map(courses);
    }
}