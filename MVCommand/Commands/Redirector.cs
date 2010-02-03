namespace MVCommand.Commands
{
    /// <summary>
    /// Class used by commands to redirect to a givem context/event once they execute.  It populates an IRedirect object, which the front controller
    /// then uses to redirect to the appropriate url
    /// </summary>
    public static class Redirector
    {
        public static IRedirect Redirect(string context, string @event)
        {            
            var redirect = new Redirect(UrlGenerator.GetUrlFor(context, @event));
            return redirect;
        }

        public static IRedirect Redirect(string pathToRedirectTo)
        {
            return new Redirect(pathToRedirectTo);
        }
    }
}