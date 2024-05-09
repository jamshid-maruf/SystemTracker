using EduOne.Enums;
using EduOne.Models.Enrollments;

namespace EduOne.Services.Enrollments;

public interface IEnrollmentService
{
    Task<EnrollmentViewModel> CreateAsync(EnrollmentCreateModel createModel);
    Task<EnrollmentViewModel> UpdateAsync(long id, EnrollmentUpdateModel updateModel);
    Task<bool> DeleteAsync(long id);
    Task<EnrollmentViewModel> GetByIdAsync(long id);
    Task<IEnumerable<EnrollmentViewModel>> GetAllAsync(long? instructorId = null, long? studentId = null, EnrollmentStatus? status = null);
    Task<EnrollmentViewModel> SetGradeAsync(char grade, long courseId, long enrollmentId, long studentId);
    Task<EnrollmentViewModel> ChangeStatusAsync(long enrollmentId, EnrollmentStatus status);
}