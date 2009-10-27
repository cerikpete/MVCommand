using System;
using System.Collections.Generic;
using MVCommand.Controllers;

namespace MVCommand.Commands.Dictionary
{
    public class ActionLoader
    {
        private readonly string _contextName;
        private readonly IDictionary<string, IEnumerable<Type>> _commandDictionary;

        public ActionLoader(string contextName, IDictionary<string, IEnumerable<Type>> commandDictionary)
        {
            _contextName = contextName;
            _commandDictionary = commandDictionary;
        }

        public DictionaryLoader AndAction(string actionName)
        {
            var key = string.Format(CommandController.NamespaceFormat, _contextName, actionName).ToLower();
            return new DictionaryLoader(key, _commandDictionary);
        }
    }
}