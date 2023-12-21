using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class AttachmentDTO
    {
        public string? FileName { get;  set; }

        public byte[]? FileData { get;  set; }

        public int AttachmentGroupId { get; set; }
        public int Id { get; set; }

    }
}
