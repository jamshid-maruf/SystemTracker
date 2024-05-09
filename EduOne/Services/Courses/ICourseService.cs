using EduOne.Entities;
using EduOne.Models.Courses;

namespace EduOne.Services.Courses;

public interface ICourseService
{
    Task<CourseViewModel> CreateAsync(CourseCreateModel createModel);
    Task<CourseViewModel> UpdateAsync(long id, CourseUpdateModel updateModel);
    Task<bool> DeleteAsync(long id);
    Task<CourseViewModel> GetByIdAsync(long id);
    Task<IEnumerable<CourseViewModel>> GetAllAsync(long? instructorId = null, DateTime? date = null);
}