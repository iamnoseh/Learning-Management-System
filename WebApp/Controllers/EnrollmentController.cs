using Domain.Dtos.Enrollment;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class EnrollmentController(IEnrollmentService service) :  Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateEnrollment(CreateEnrollmentDto dto)
    {
        var res = await service.CreateEnrollment(dto);
        return Ok(res);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEnrollment(UpdateEnrollmentDto dto)
    {
        var res = await service.UpdateEnrollment(dto);
        return Ok(res);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEnrollment(int id)
    {
        var res = await service.DeleteEnrollment(id);
        return Ok(res);
    }

    [HttpGet("{studentId}")]
    public async Task<IActionResult> GetEnrollmentsByStudentId(int studentId)
    {
        var res = await service.GetEnrollmentBystudentId(studentId);
        return Ok(res);
    }

    [HttpGet]
    [Authorize(Roles = "Teacher, Admin, SuperAdmin")]
    public async Task<IActionResult> GetEnrollments()
    {
        var res = await service.GetEnrollments();
        return Ok(res);
    }
}