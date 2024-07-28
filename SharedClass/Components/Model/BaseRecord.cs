using System.ComponentModel.DataAnnotations;

namespace SharedClass.Components.Model
{
    public class BaseRecord
    {
        public int ForInsert { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Output { get; set; }
    }
    public class BaseRecordPOS 
    {
        public int CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public int Insert { get; set; }
    }
    public class OutputClass
    {
        public string? Output { get; set; }
        public string? ErrorMessage { get; set; }
        public int RemainingAmount { get; set; }
        public List<dynamic>? Data { get; set; }
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
        public required int? Role { get; set; }
        public required string? Address { get; set; }
        public required string? UserIdentity { get; set; }
        public required string? Gender { get; set; }
        public required DateTime Birthday { get; set; }
        public string? Status { get; set; }
        public int UserIdentityID { get; set; }
    }

    public class UserAuth
    {
        public string? Username { get; set; }
        public string? UserPassword { get; set; }
        public string? UserID { get; set; }
        public string? Role { get; set; }
    }

    public class Report
    {
        public int ReportID { get; set; }
        public string? ReportName { get; set; }
        public byte[]? ReportData { get; set; }
    }

    public class ReportParameters
    {
        public string? InvoiceID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public static class UserIDSession
    {
        public static string? UserID { get; set; }
        public static List<int> PermissionList { get; set; } = new List<int>();
    }

    public class UserPermissions : BaseRecord
    {
        public string? PermissionID { get; set; }
        public string? PermissionName { get; set; }
        public bool selected { get; set; }
    }

    public class UserRoles : BaseRecord
    {
        public string? RoleID { get; set; }
        public string? RoleName { get; set; }
    }

    public class RolePermissions : BaseRecord
    {
        public int ID { get; set; }
        public string? RoleID { get; set; }
        public string? PermissionID { get; set; }
    }
}