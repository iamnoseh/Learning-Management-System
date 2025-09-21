using Domain.Dtos.Enrollment;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IEnrollmentService
{
    public Task<Response<string>> CreateEnrollment(CreateEnrollmentDto dto);
    public Task<Response<string>> UpdateEnrollment(UpdateEnrollmentDto dto);
    public Task<Response<string>> DeleteEnrollment(int id);
    public Task<Response<List<GetEnrollmentDto>>> GetEnrollmentBystudentId(int stidentId);
    public Task<Response<List<GetEnrollmentDto>>> GetEnrollments();
}