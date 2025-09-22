namespace Domain.Entities;

public class Enrollment
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public int StudentId { get; set; }
    public User Student { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    public bool IsPremium { get; set; }
    public DateTime? ExpiryDate { get; set; }

    public ICollection<Progress> Progresses { get; set; } = new List<Progress>();
}


