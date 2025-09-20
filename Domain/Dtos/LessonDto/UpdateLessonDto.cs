namespace Domain.Dtos.LessonDto;

public class UpdateLessonDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string VideoUrl { get; set; }
    public string FileUrl { get; set; }
    public int OrderIndex { get; set; }
}