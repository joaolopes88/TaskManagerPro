namespace AuthService.Models
{
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Additional fields can be added as needed, e.g., FirstName, LastName, etc.
    }
}