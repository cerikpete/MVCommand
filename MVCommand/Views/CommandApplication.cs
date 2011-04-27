using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.ServiceLocation;
using MVCommand.Views.ViewEngine;

namespace MVCommand.Views
{
    public abstract class CommandApplication : System.Web.HttpApplication
    {
        protected abstract void RegisterCommandsWithIoCContainer();
        protected abstract void RegisterRoutes(RouteCollection routes);

        protected virtual void SetupControllerFactory()
        {
            RegisterCommandsWithIoCContainer();
            ControllerBuilder.Current.SetControllerFactory(CommandControllerFactoryType);
        }

        protected void Application_Start()
        {
            SetupControllerFactory();
            RegisterRoutes(RouteTable.Routes);

            // Register the command view engine
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CommandViewEngine());
            ViewEngines.Engines.Add(new CommandRazorViewEngine());

            // Register the IoC container for MVCommand
            ServiceLocator.SetLocatorProvider(() => ServiceLocatorProvider);

            SetUpAdditionalItemsForApplicationStart();
        }

        protected abstract ServiceLocatorImplBase ServiceLocatorProvider { get; }

        protected virtual void SetUpAdditionalItemsForApplicationStart(){}

        protected abstract Type CommandControllerFactoryType { get; }
    }
}