using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Routing;
using MVCommand.Commands;

namespace MVCommand.Extensions
{
    public static class UrlExtensions
    {
        public static string For<CommandType>(this UrlHelper helper) where CommandType : ICommand
        {
            var commandNamespace = typeof (CommandType).Namespace;
            var actionContext = new ActionContext(commandNamespace);
            return ReturnActionPath(helper, actionContext.Context, actionContext.Action, null);
        }

        public static string For<CommandType>(this UrlHelper helper, object routeData) where CommandType : ICommand
        {
            var commandNamespace = typeof(CommandType).Namespace;
            var actionContext = new ActionContext(commandNamespace);
            return ReturnActionPath(helper, actionContext.Context, actionContext.Action, routeData);
        }

        public static string For(this UrlHelper helper, string context, string action)
        {
            return ReturnActionPath(helper, context, action, null);
        }

        public static string For(this UrlHelper helper, string context, string action, object routeData)
        {
            return ReturnActionPath(helper, context, action, routeData);
        }

        private static string ReturnActionPath(UrlHelper helper, string context, string action, object routeData)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("context", context);
            routeValues.Add("event", action);

            if (routeData != null)
            {
                foreach (var value in GetProperties(routeData))
                {
                    routeValues.Add(value.Name, value.Value);
                }
            }

            return helper.Action(null, null, routeValues);
        }

        private static IEnumerable<PropertyValue> GetProperties(object routeData)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(routeData);
            foreach (PropertyDescriptor prop in props)
            {
                object val = prop.GetValue(routeData);
                if (val != null)
                {
                    yield return new PropertyValue { Name = prop.Name, Value = val };
                }
            }
        }
    }

    class ActionContext
    {
        private string[] _namespaceValues;

        internal ActionContext(string commandNamespace)
        {
            _namespaceValues = commandNamespace.Split('.');
        }

        internal string Context
        {
            get { return _namespaceValues[_namespaceValues.Length - 2]; }
        }

        internal string Action
        {
            get { return _namespaceValues[_namespaceValues.Length - 1]; }
        }
    }

    class PropertyValue
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}