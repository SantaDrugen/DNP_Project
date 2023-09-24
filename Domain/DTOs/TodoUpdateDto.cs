namespace Domain.DTOs;

public class TodoUpdateDto
{
    public int Id { get; }
    public int? OwnerId { get; set; }
    public string? Title { get; set; }
    public bool? IsComplete { get; set; }

    public TodoUpdateDto(int id)
    {
        Id = id;
    }
}