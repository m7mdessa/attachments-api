using Domain.SharedKernel;


namespace Domain.Entites
{
    public class Attachment : BaseEntity
    {

        public string? FileName { get;  set; }

        public byte[]? FileData { get;  set; }
        public int AttachmentGroupId { get;  set; }


        private Attachment() { }

        public Attachment(int attachmentGroupId)
        {
            AttachmentGroupId = attachmentGroupId;
        }

        public void SetFileData(byte[] fileData)
        {
            FileData = fileData;
        }

        public void SetFileName(string fileName)
        {
            FileName = fileName;
        }


    }
}
