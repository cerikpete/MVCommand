using System;
using System.Web.Mvc;

namespace MVCommand.Views
{
    /// <summary>
    /// Control class that simply cleanly handles retrieving null models from the ViewDataDictionary.
    /// </summary>
    public class NullableModelMVCControl<ModelType> : ViewUserControl<ModelType> where ModelType : class
    {
        /// <summary>
        /// Retrieves the string value from the supplied property on the model
        /// </summary>
        /// <param name="modelProperty">Property on the model to retrieve the value from</param>
        /// <returns>String value of property if model is not null, otherwise an empty string</returns>
        public string GetValue(Func<ModelType, object> modelProperty)
        {
            if (Model != null)
            {
                return modelProperty(Model) != null ? modelProperty(Model).ToString() : string.Empty;
            }
            return string.Empty;
        }
    }
}