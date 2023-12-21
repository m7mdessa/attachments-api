using Domain.Entites;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly AttachmentsContext _context;

        public AttachmentRepository(AttachmentsContext context)
        {

            _context = context;
        }


        public async Task<Attachment> GetByIdAsync(int attachmentId)
        {
            return await _context.Attachments
                .FirstOrDefaultAsync(e => e.Id == attachmentId);
        }
       
        public async Task AddAsync(Attachment attachment)
        {
            _context.Add(attachment);
            await _context.SaveChangesAsync();
        }

        public async Task<Attachment> GetAttachmentGroupAsync(int attachmentGroupId)
        {
            return await _context.Attachments
                .FirstOrDefaultAsync(e => e.AttachmentGroupId == attachmentGroupId);
        }
        public async Task<IEnumerable<Attachment>> GetEmployeeAttachmentsAsync(int attachmentGroupId)
        {
            return await _context.Attachments
                .Where(e => e.AttachmentGroupId == attachmentGroupId)
                .ToListAsync();
        }


    
    }
}
