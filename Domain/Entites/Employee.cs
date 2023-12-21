using Domain.SharedKernel;

namespace Domain.Entites
{
    public class Employee : BaseEntity
    {

        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? Email { get; private set; }
        public string? Image { get; private set; }
        public decimal? Salary { get; private set; }
        public string? Phone { get; private set; }
        public string? EmployeeName { get; private set; }
        public DateTime HireDate { get; private set; }
        public Address? Address { get; private set; }
        public int AttachmentGroupId { get; private set; }
        public AttachmentGroup AttachmentGroup { get; private set; }

        private Employee() { }

        public Employee(string? employeeName, string? firstName, string? lastName, string? email, string? phone, decimal? salary, Address address, AttachmentGroup attachmentGroup)
        {

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            HireDate = DateTime.UtcNow;
            Salary = salary;
            Address = address;
            EmployeeName = employeeName;
            AttachmentGroup = attachmentGroup;

        }



        public void SetImage(string image)
        {
            Image = image;
        }
    
    }
}
