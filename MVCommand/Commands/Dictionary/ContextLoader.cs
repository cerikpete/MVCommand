using System;
using System.Collections.Generic;

namespace MVCommand.Commands.Dictionary
{
    public class ContextLoader
    {
        private readonly IDictionary<string, IEnumerable<Type>> _commandDictionary;

        public ContextLoader(IDictionary<string, IEnumerable<Type>> commandDictionary)
        {
            _commandDictionary = commandDictionary;
        }

        public ActionLoader CreateEntryForContext(string contextName)
        {
            return new ActionLoader(contextName, _commandDictionary);
        }
    }
}