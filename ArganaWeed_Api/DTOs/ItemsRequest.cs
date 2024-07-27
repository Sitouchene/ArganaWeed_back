namespace ArganaWeedApp.DTOs
{
    public class ItemsRequest
    {
    }

    public class LoginRequest : BaseRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
