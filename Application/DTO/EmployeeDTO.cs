


using Microsoft.AspNetCore.Http;

namespace Application.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public decimal? Salary { get; set; }
        public string? Phone { get; set; }
        public string? EmployeeName { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public DateTime HireDate { get; set; }
        public int AttachmentGroupId { get;  set; }

        public long MaxFileSize => 5 * 1024 * 1024;

    }
}
