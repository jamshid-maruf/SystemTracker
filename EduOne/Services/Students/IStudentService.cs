using EduOne.Models.Students;

namespace EduOne.Services.Students;

public interface IStudentService
{
    Task<StudentViewModel> CreateAsync(StudentCreateModel createModel);
    Task<StudentViewModel> UpdateAsync(long id, StudentUpdateModel updateModel);
    Task<bool> DeleteAsync(long id);
    Task<StudentViewModel> GetByIdAsync(long id);
    Task<IEnumerable<StudentViewModel>> GetAllAsync();
    Task<IEnumerable<StudentViewModel>> GetAllByCourseIdAsync(long courseId);
}