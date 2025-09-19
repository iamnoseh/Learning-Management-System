using Domain.Enums;

namespace Domain.Filter;

public class CourseFilter : BaseFilter
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Category? Category { get; set; }
    public decimal? Price { get; set; }
    public bool? IsFree { get; set; }
    public int? CreatedBy { get; set; }
}