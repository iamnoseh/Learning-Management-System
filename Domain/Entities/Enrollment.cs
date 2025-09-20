namespace Domain.Entities;

public class Enrollment : BaseEntities
{
    public DateTime EnrollmentDate { get; set; }
    public bool IsPremium { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    
    public Course? Course { get; set; }
    public User? Student { get; set; }
    public List<Progres>? ProgresList { get; set; }
}