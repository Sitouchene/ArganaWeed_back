namespace ArganaWeedRest.Models

{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public string UserEmail { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAgent { get; set; }
        public bool IsViewer { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserRoleUpdateModel
    {
        public bool IsAdministrator { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAgent { get; set; }
        public bool IsViewer { get; set; }
    }
}
