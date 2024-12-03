using api.domain;

public class ApiResponseDTO
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? Token { get; set; }
    public IEnumerable<string>? Errors { get; set; }
    public List<User>? Users { get; set; }
}
