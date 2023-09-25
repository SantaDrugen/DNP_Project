namespace Domain.DTOs;

public class TodoGetDto
{
    public string? Username { get; set; }
    public int? TodoId { get; set; }
    public bool? CompletedStatus { get; set; }
    public string? Title { get; set; }
    
    public TodoGetDto()
    {
    }
    
    
}