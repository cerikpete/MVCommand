using System.Web.Mvc;

namespace MVCommand.Controllers
{
    public abstract class CommandControllerFactory : DefaultControllerFactory
    {
        public abstract override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName);
    }
}