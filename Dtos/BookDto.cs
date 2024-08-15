
public class BookDto
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Gender { get; set; }
    public int NumPages { get; set; }
    public string? Editorial { get; set; }
    public long Isbn { get; set; }
    public int YearEdition { get; set; }
    public DateTime DateEdition { get; set; }
    public string? Writer { get; set; }
    public string? Image { get; set; }
    public List<string> Tags { get; set; }
    public List<String> CategoryIds { get; set; }
}