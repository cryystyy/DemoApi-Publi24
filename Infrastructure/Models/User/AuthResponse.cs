namespace Infrastructure.Models.Users
{
    public class AuthResponse
    {

        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }   
        
    }
}
