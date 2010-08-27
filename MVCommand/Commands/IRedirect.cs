using System.Web.Mvc;

namespace MVCommand.Commands
{
    public interface IRedirect
    {
        void HandleRedirect(ControllerContext controllerContext);
    }
}