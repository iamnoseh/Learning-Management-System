namespace Domain.Filter;

public class LessonFilter : BaseFilter
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int? OrderIndex { get; set; }
    public int? CourseId { get; set; }
}