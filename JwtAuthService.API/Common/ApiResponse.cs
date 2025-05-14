namespace JwtAuthService.API.Responses;

public class ApiResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }

    public static ApiResponse SuccessResponse(object data, string? message = null) =>
        new() { Success = true, Data = data, Message = message };

    public static ApiResponse FailResponse(string message) =>
        new() { Success = false, Message = message };
}
