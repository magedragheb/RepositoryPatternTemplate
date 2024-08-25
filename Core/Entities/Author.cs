namespace Core.Entities;

public record Author
{
    public int Id { get; set; }
    public required string Name { get; set; }
}