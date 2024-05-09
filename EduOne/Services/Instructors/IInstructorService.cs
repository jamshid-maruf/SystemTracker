using EduOne.Models.Instructors;

namespace EduOne.Services.Instructors;

public interface IInstructorService
{
    Task<InstructorViewModel> CreateAsync(InstructorCreateModel createModel);
    Task<InstructorViewModel> UpdateAsync(long id, InstructorUpdateModel updateModel);
    Task<bool> DeleteAsync(long id);
    Task<InstructorViewModel> GetByIdAsync(long id);
    Task<IEnumerable<InstructorViewModel>> GetAllAsync();
}