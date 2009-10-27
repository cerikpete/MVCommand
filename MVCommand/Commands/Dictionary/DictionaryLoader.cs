using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCommand.Commands.Dictionary
{
    public class DictionaryLoader
    {
        private readonly string _key;
        private readonly IDictionary<string, IEnumerable<Type>> _commandDictionary;

        public DictionaryLoader(string key, IDictionary<string, IEnumerable<Type>> commandDictionary)
        {
            _key = key;
            _commandDictionary = commandDictionary;
        }

        public DictionaryLoader AddCommand<Command>() where Command : ICommand
        {
            if (_commandDictionary.ContainsKey(_key))
            {
                var commandsForKey = _commandDictionary[_key];
                var newCommandsForKey = commandsForKey.Cast<Type>().ToList();
                newCommandsForKey.Add(typeof(Command));
                _commandDictionary.Remove(_key);
                _commandDictionary.Add(_key, newCommandsForKey);
            }
            else
            {
                var commandsForKey = new List<Type>();
                commandsForKey.Add(typeof(Command));
                _commandDictionary.Add(_key, commandsForKey);
            }
            return this;
        }

        public ActionLoader CreateEntryForContext(string contextName)
        {
            return new ActionLoader(contextName, _commandDictionary);
        }
    }
}