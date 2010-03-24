using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using MVCommand.Commands;
using MVCommand.Logging;
using MVCommand.Validation;

namespace MVCommand.Controllers
{
    public abstract class CommandController : Controller
    {
        public const string NamespaceFormat = "{0}.{1}";
        private object result;

        /// <summary>
        /// This override allows us to point to the correct view, which lives under the folder with the same name
        /// as the "context" property in the route.
        /// </summary>
        protected override ViewResult View(string viewName, string masterName, object viewData)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = Event;
            }
            string fullViewName = string.Format("~/Views/{0}/{1}.aspx", Context, viewName);
            return base.View(fullViewName, masterName, viewData);
        }

        public ActionResult DefaultAction()
        {
            return View();
        }

        public JsonResult JsonAction()
        {
            return Json(result);
        }

        public ContentResult ContentAction()
        {
            return Content(result.ToString());
        }

        public IDictionary<string, IEnumerable<Type>> CommandDictionary { get; set; }

        /// <summary>
        /// This override allows us to execute the commands relevant to this request, instead of executing
        /// an action on a controller.
        /// </summary>
        protected override void ExecuteCore()
        {
            TempData.Load(ControllerContext, TempDataProvider);

            LoadSuccessOrErrorDataToViewDataIfApplicable();

            if (CommandDictionary != null && CommandDictionary.ContainsKey(ContextActionKey))
            {
                ExecuteCommandsInDictionary();
            }
            else
            {
                // Get all commands in specific assembly and execute them
                ExecuteCommandsInSpecifiedNamespace();
            }     

            InvokeAppropriateControllerAction();
            TempData.Save(ControllerContext, TempDataProvider);
        }

        /// <summary>
        /// Handle if the result returned from a command was a success or error message and related data.  If so, it would have been loaded
        /// to TempData for storage between requests.
        /// </summary>
        private void LoadSuccessOrErrorDataToViewDataIfApplicable()
        {
            var errorType = typeof(IError).FullName;
            var errorHasOccurred = TempData.ContainsKey(errorType);
            if (errorHasOccurred)
            {
                // Load error messages to ViewData, as well as Dictionary of objects to be loaded to the view so any data entered before
                // the error is saved
                LoadErrorDataToViewDataDictionary(errorType);
            }

            var successType = typeof(ISuccess).FullName;
            var successHasOccurred = TempData.ContainsKey(successType);
            if (successHasOccurred)
            {
                // Load the success message to view data
                ViewData.Add(successType, TempData[successType]);
            }
        }

        private void LoadErrorDataToViewDataDictionary(string errorType)
        {
            var error = TempData[errorType] as IError;
            ViewData.Add(errorType, error);
            foreach (var dataItem in error.ErrorDataDictionary)
            {
                ViewData.Add(dataItem.Key, dataItem.Value);
            }
        }

        private void InvokeAppropriateControllerAction()
        {
            string nameOfActionToInvoke = "DefaultAction";
            if (Request.Headers["Accept"].Contains("json"))
            {
                nameOfActionToInvoke = "JsonAction";
            }
            else if (Request.Headers["X-Requested-With"] != null && Request.Headers["X-Requested-With"].Contains("XMLHttpRequest"))
            {
                // Accepting "text\html" indicates we don't want to use a ContentActoion
                if (!Request.Headers["Accept"].Contains("text/html"))
                {
                    nameOfActionToInvoke = "ContentAction";
                }
            }
            ActionInvoker.InvokeAction(ControllerContext, nameOfActionToInvoke);
        }

        private void ExecuteCommandsInDictionary()
        {
            IEnumerable<Type> commandsToExecute = CommandDictionary[ContextActionKey];
            foreach (var command in commandsToExecute)
            {
                var commandObject = ServiceLocator.Current.GetInstance(command) as ICommand;
                ExecuteCommandAndLoadResultToViewData(commandObject);
            }
        }

        public abstract Type[] CommandTypes { get; }

        public abstract Type BindableCommandType { get; }

        /// <summary>
        /// The convention here is to retrieve and execute commands in the namespace that matches the context and action
        /// </summary>
        private void ExecuteCommandsInSpecifiedNamespace()
        {            
            foreach (var commandType in CommandTypes)
            {
                // If it's a command, execute it
                if (typeof(ICommand).IsAssignableFrom(commandType) && !commandType.IsInterface)
                {
                    // Ensure the command is in the correct namespace for the view - we only want to execute commands related to the current view
                    if (commandType.Namespace.ToLower().EndsWith(ContextActionKey))
                    {
                        var commandObject = ServiceLocator.Current.GetInstance(commandType) as ICommand;

                        // Add result to ViewData if one was returned
                        ExecuteCommandAndLoadResultToViewData(commandObject);                        
                    }
                }
            }
        }

        private void ExecuteCommandAndLoadResultToViewData(ICommand commandToExecute)
        {
            if (commandToExecute.GetType().BaseType.IsGenericType) // This means the command needs a model to be loaded from the view before executing
            {
                LoadModelToCommand(commandToExecute);
            }
            result = commandToExecute.Execute();
            try
            {
                if (result != null)
                {
                    if (typeof(IError).IsAssignableFrom(result.GetType()))
                    {
                        LoadErrorDataToTempDataAndRedirect();
                    }
                    else
                    {
                        if (typeof(ISuccess).IsAssignableFrom(result.GetType()))
                        {
                            LoadObjectToTempData(typeof(ISuccess).FullName, result);
                        }
                        else if (typeof(IRedirect).IsAssignableFrom(result.GetType()))
                        {
                            var redirect = result as IRedirect;
                            redirect.HandleRedirect();
                        }
                        else
                        {
                            ViewData.Add(result.GetType().FullName, result);
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Log<CommandController>.Error("Error attempting to execute command", ex);
                throw new ArgumentException(string.Format("The type: {0} has already been added to the ViewData collection", result.GetType().Name));
            }
            finally
            {
                DisposeCommand(commandToExecute);                
            }
        }

        /// <summary>
        /// Dispose command if supports it
        /// </summary>
        private void DisposeCommand(ICommand commandToDispose)
        {
            IDisposable disposableCommand = commandToDispose as IDisposable;
            if (disposableCommand != null)
            {
                disposableCommand.Dispose();
            }
        }

        private void LoadObjectToTempData(string key, object objectToLoad)
        {
            // This is an implementation of TempData.Add which for some reason does not persist the TempData
            // between requests
            IDictionary<string, object> dictionary = ControllerContext.HttpContext.Session["__ControllerTempData"] as IDictionary<string, object> ?? new Dictionary<string, object>();
            dictionary.Add(key, objectToLoad);
            ControllerContext.HttpContext.Session["__ControllerTempData"] = dictionary;
        }

        private void LoadErrorDataToTempDataAndRedirect()
        {
            LoadObjectToTempData(typeof(IError).FullName, result);
            
            // Redirect to the previous url which is where the error would have occurred
            Response.Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        private void LoadModelToCommand(ICommand commandObject)
        {
            var modelType = commandObject.GetType().BaseType.GetGenericArguments()[0].UnderlyingSystemType;
            var model = Activator.CreateInstance(modelType);
            BindModel(new DefaultModelBinder(), modelType, model);

            // Set model property on bindable command object
            Type fullType = BindableCommandType.MakeGenericType(model.GetType());
            var bindableCommand = Activator.CreateInstance(fullType);
            var propertyInfo = bindableCommand.GetType().GetProperty("Model");
            propertyInfo.SetValue(commandObject, model, null);
        }

        private void BindModel(IModelBinder modelBinder, Type modelType, object model)
        {
            ModelBindingContext modelBindingContext = new ModelBindingContext();
            modelBindingContext.Model = model;
            modelBindingContext.ModelState = ModelState;
            modelBindingContext.ModelType = modelType;
            modelBindingContext.ValueProvider = ValueProvider;
            modelBinder.BindModel(ControllerContext, modelBindingContext);
        }

        private string Context
        {
            get { return RouteData.GetRequiredString("context"); }
        }

        private string Event
        {
            get { return RouteData.GetRequiredString("event"); }
        }

        private string ContextActionKey
        {
            get { return string.Format(NamespaceFormat, Context.ToLower(), Event.ToLower()); }
        }
    }
}