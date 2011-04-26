using System.IO;

namespace MVCommand.Models
{
    /// <summary>
    /// Model used to return a file stream 
    /// </summary>
    public class FileStreamResponse
    {
        /// <summary>
        /// The file stream to send to the response
        /// </summary>
        public Stream FileStream { get; set; }

        /// <summary>
        /// The content type to set on the response header
        /// </summary>
        public string ContentType { get; set; }
    }
}