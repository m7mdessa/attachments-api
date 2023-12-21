using Domain.SharedKernel;
using static System.Net.Mime.MediaTypeNames;


namespace Domain.Entites
{
    public class AttachmentGroup:BaseEntity
    {

        public List<Attachment> Attachments { get; private set; } = new List<Attachment>();


        //public void SetImage(string image)
        //{
        //    Attachments = image;
        //}


    }
}
