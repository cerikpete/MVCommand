namespace MVCommand.Validation
{
    /// <summary>
    /// Interface for wrapping a message to return when a command completed successfully.
    /// </summary>
    public interface ISuccess
    {
        /// <summary>
        /// A success message to return to the view
        /// </summary>
        string Message { get; set; }
    }
}