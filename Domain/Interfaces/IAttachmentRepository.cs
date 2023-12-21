using Domain.Entites;

namespace Domain.Interfaces
{
    public interface IAttachmentRepository
    {
        Task<Attachment> GetByIdAsync(int employeeId);
        Task AddAsync(Attachment employee);
     
        Task<IEnumerable<Attachment>> GetEmployeeAttachmentsAsync(int attachmentGroupId);
        Task<Attachment> GetAttachmentGroupAsync(int attachmentGroupId);
    }
}
