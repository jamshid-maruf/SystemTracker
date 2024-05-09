using EduOne.Entities;
using EduOne.DataAccess;
using EduOne.Services.Courses;
using EduOne.Models.Instructors;
using Microsoft.EntityFrameworkCore;
using EduOne.Mappers;

namespace EduOne.Services.Instructors;

public class InstructorService : IInstructorService
{
    private readonly AppDbContext context;
    private readonly ICourseService courseService;
    public InstructorService(AppDbContext context, ICourseService courseService)
    {
        this.context = context;
        this.courseService = courseService;
    }

    public async Task<InstructorViewModel> CreateAsync(InstructorCreateModel createModel)
    {
        var existInstructor = await context.Instructors.FirstOrDefaultAsync(i => i.Phone == createModel.Phone && !i.IsDeleted);
        if (existInstructor is not null)
            throw new Exception($"Instructor is already exists with this phone={createModel.Phone}");

        var createdInstructor = (await context.Instructors.AddAsync(Mapper.Map(createModel))).Entity;

        await context.SaveChangesAsync();

        return Mapper.Map(existInstructor);
    }

    public async Task<InstructorViewModel> UpdateAsync(long id, InstructorUpdateModel updateModel)
    {
        var existInstructor = await context.Instructors
            .Include(i => i.Courses)
            .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted)
            ?? throw new Exception($"Instructor is not found with this ID={id}");
        
        var similarInstructor = await context.Instructors
            .FirstOrDefaultAsync(c => c.Phone == updateModel.Phone && c.Id != id);

        if (similarInstructor is not null)
            throw new Exception($"This course is already exists with this name={updateModel.Phone}");

        existInstructor.Phone = updateModel.Phone;
        existInstructor.UpdatedAt = DateTime.UtcNow;
        existInstructor.LastName = updateModel.LastName;
        existInstructor.FirstName = updateModel.FirstName;

        context.Instructors.Update(existInstructor);
        await context.SaveChangesAsync();

        return Mapper.Map(existInstructor);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existInstructor = await context.Instructors.FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted)
            ?? throw new Exception($"Instructor is not found with this ID={id}");

        existInstructor.IsDeleted = true;
        existInstructor.DeletedAt = DateTime.UtcNow;

        context.Instructors.Update(existInstructor);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<InstructorViewModel> GetByIdAsync(long id)
    {
        var existInstructor = await context.Instructors.FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted)
            ?? throw new Exception($"Instructor is not found with this ID={id}");

        return Mapper.Map(existInstructor);
    }

    public async Task<IEnumerable<InstructorViewModel>> GetAllAsync()
    {
        var instructors = await context.Instructors
            .Include(instructor => instructor.Courses)
            .ToListAsync();

        return Mapper.Map(instructors);
    }
}