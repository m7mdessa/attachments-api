using Domain.Interfaces;

namespace Domain.SharedKernel
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IEmployeeRepository EmployeeRepository();
        IAttachmentRepository AttachmentRepository();


    }
}