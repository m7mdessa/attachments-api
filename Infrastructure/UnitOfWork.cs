using Domain.Interfaces;
using Domain.SharedKernel;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AttachmentsContext _context;

        public UnitOfWork(AttachmentsContext context)
        {
            _context = context;
        }

        public IEmployeeRepository EmployeeRepository()
        {
            return new EmployeeRepository(_context);
        }

        public IAttachmentRepository AttachmentRepository()
        {
            return new AttachmentRepository(_context);
        }
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}