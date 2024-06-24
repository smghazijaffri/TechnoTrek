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
        public required string? Role { get; set; }
        public required string? Address { get; set; }
        public required string? UserIdentity { get; set; }
        public required string? Gender { get; set; }
        public required DateTime Birthday { get; set; }
        public string? Status { get; set; }
    }

    public class UserAuth
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }

    public class ReportUpload
    {
        public int ReportID { get; set; }
        public string? ReportName { get; set; }
        public byte[]? RDLData { get; set; }
        public string? Status { get; set; }
    }
}
