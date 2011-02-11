namespace MVCommand.Models
{
    public class FileDownloadResponse
    {
        /// <summary>
        /// The path to the file being downloaded
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The content type of the file to send to the response
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// The value to name the file when downloaded
        /// </summary>
        public string DownloadFileName { get; set; }
    }
}