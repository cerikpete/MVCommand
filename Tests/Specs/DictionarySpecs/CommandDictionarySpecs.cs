using System;
using System.Linq;
using MVCommand.Commands;
using MVCommand.Commands.Dictionary;
using NUnit.Framework;

namespace Tests.Specs.DictionarySpecs
{
    public class BaseCommandDictionarySpecs
    {
        protected string _key1 = "context1.action1";
        protected string _key2 = "context2.action2";

        [SetUp]
        public void SetUp()
        {
            // Set up the dictionary with a couple of items so we can assert them below.  For now we'll set
            // two items on key1, and one on key2
            CommandDictionary.Initialize()
                .CreateEntryForContext("context1").AndAction("action1")
                    .AddCommand<MyClass1>()
                    .AddCommand<MyClass2>()
                .CreateEntryForContext("context2").AndAction("action2")
                    .AddCommand<MyClass3>();
        }

        protected class MyClass1 : ICommand
        {
            public object Execute()
            {
                throw new NotImplementedException();
            }
        }

        protected class MyClass2 : ICommand
        {
            public object Execute()
            {
                throw new NotImplementedException();
            }
        }

        protected class MyClass3 : ICommand
        {
            public object Execute()
            {
                throw new NotImplementedException();
            }
        }
    }

    [TestFixture]
    public class When_retrieving_items_from_the_command_dictionary : BaseCommandDictionarySpecs
    {
        [Test]
        public void The_dictionary_should_be_populated_when_we_ask_for_it_from_the_command_dictionary_class()
        {
            var dictionary = CommandDictionary.GetDictionary();
            Assert.IsNotNull(dictionary);
        }

        [Test]
        public void The_dictionary_should_have_the_correct_number_of_keys()
        {
            // Two entries were added above, so two should be returned
            var dictionary = CommandDictionary.GetDictionary();
            Assert.AreEqual(2, dictionary.Keys.Count);
        }

        [Test]
        public void Should_return_the_correct_number_of_commands_associated_with_the_asked_for_key()
        {
            var dictionary = CommandDictionary.GetDictionary();

            // The first key has two commands associated with it, MyClass1 and MyClass2
            Assert.AreEqual(2, dictionary[_key1].Count());

            // The second key only had one item associated with it, and the type is MyClass3
            Assert.AreEqual(1, dictionary[_key2].Count());
        }
    }
}