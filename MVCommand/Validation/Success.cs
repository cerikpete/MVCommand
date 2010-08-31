namespace MVCommand.Validation
{
    public class Success : ISuccess
    {
        /// <summary>
        /// A success message to return to the view
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>        
        /// <summary>
        /// The optional url to redirect to once the command is complete
        /// </summary>
        public string RedirectUrl { get; set; }
    }
}