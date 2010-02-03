using System.Collections.Generic;
using System.ComponentModel;

namespace MVCommand.Commands
{
    /// <summary>
    /// Class used for looping through a route value object dictionary and retrieving its values
    /// </summary>
    internal class PropertyRetriever
    {
        public static IEnumerable<PropertyValue> GetProperties(object routeData)
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

    internal class PropertyValue
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}