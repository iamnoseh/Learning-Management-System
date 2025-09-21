namespace Domain.Dtos.Enrollment;

public class CreateEnrollmentDto
{
    public bool IsPremium { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
}