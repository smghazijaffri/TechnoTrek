namespace SharedClass.Components.Model
{
    public class BaseRecord
    {
        public int ForInsert { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Output { get; set; }
    }

    public class OutputClass
    {
        public string? Output { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class Users : BaseRecord
    {
        public string? UserID { get; set; }
        public required string? UserName { get; set; }
        public required string? FirstName { get; set; }
        public required string? LastName { get; set; }
        public required string? UserPassword { get; set; }
        public required string? UserEmail { get; set; }
        public required string? UserPhone { get; set; }
        public required string? UserRole { get; set; }
        public required string? UserAddress { get; set; }
        public required string? UserIdentity { get; set; }
        public required string? UserGender { get; set; }
        public required DateTime UserBirthday { get; set; }
        public string? Status { get; set; }
    }
}
