using System.Web;

namespace MVCommand.Commands
{
    /// <summary>
    /// Class that handles returning the path to redirect to, used as a possible result for commands whose job is just to redirect
    /// </summary>
    internal class Redirect : IRedirect
    {
        private readonly string _pathToRedirectTo;

        public Redirect(string pathToRedirectTo)
        {
            _pathToRedirectTo = pathToRedirectTo;
        }

        public void HandleRedirect()
        {
            HttpContext.Current.Response.Redirect(_pathToRedirectTo);
        }
    }
}