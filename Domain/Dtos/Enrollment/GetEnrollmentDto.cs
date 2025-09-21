namespace Domain.Dtos.Enrollment;

public class GetEnrollmentDto : CreateEnrollmentDto
{
    public int Id { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}