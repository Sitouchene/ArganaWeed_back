namespace ArganaWeedApi.Models
{

    public class AuthRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
        public List<string> Roles { get; set; }
        public string CurrentUser { get; set; }
        public int? UserId { get; set; }
    }


    public class TokenRequest
    {
        public string Token { get; set; }
    }

    public class AuthResult
    {
        public bool Authenticated { get; set; }
        public string Message { get; set; }
        public string CurrentUser { get; set; }
        public int? UserId { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAgent { get; set; }
        public bool IsViewer { get; set; }
    }
    
    public class JwtTokenInfo
    {
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }

}
