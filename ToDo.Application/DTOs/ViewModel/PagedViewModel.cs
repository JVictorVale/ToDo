namespace ToDo.Application.DTOs.ViewModel;

public class PagedViewModel<T>
{
    public List<T> List { get; set; } = new();
    public int Page { get; set; }
    public int Total { get; set; }
    public int PerPage { get; set; }
    public int PageCount { get; set; }
}