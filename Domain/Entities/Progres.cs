namespace Domain.Entities;

public class Progres : BaseEntities
{
    public bool IsCompleted { get; set; }
    public DateTime CompletedAt { get; set; }
    public int LessonId { get; set; }
    public int EnrollmentId { get; set; }
    public Enrollment? Enrollment { get; set; }
    public Lesson? Lesson { get; set; }
}