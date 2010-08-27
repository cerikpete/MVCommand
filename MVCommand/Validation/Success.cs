namespace MVCommand.Validation
{
    public class Success : ISuccess
    {
        /// <summary>
        /// A success message to return to the view
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Optional URL to redirect to after the request is complete
        /// </summary>
        public string RedirectUrl { get; set; }
    }
}