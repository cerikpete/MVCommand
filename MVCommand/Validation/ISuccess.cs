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
        
        /// <summary>
        /// Optional URL to redirect to after the request is complete
        /// </summary>
        string RedirectUrl { get; set; }
    }
}