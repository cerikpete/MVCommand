using System;
using System.Collections.Generic;

namespace MVCommand.Commands.Dictionary
{
    public static class CommandDictionary
    {
        private static IDictionary<string, IEnumerable<Type>> _commandDictionary;

        public static ContextLoader Initialize()
        {
            _commandDictionary = new Dictionary<string, IEnumerable<Type>>();
            return new ContextLoader(_commandDictionary);
        }

        public static IDictionary<string, IEnumerable<Type>> GetDictionary()
        {
            return _commandDictionary;
        }
    }
}