using System.IO;

namespace MVCommand.Models
{
    public class FileStreamResponse
    {
        public Stream FileStream { get; set; }
        public string ContentType { get; set; }
    }
}