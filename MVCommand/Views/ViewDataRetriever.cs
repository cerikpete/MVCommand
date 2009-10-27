using System;
using System.Web.Mvc;

namespace MVCommand.Views
{
    /// <summary>
    /// Class for retrieving the appropriate model with the given type from the ViewDataDictioanry.
    /// </summary>
    /// <typeparam name="ModelType">Type of model to retrieve from the ViewDataDictionary</typeparam>
    public class ViewDataRetriever<ModelType> : IViewDataRetriever<ModelType> where ModelType : class
    {
        private readonly ViewDataDictionary _viewDataDictionary;

        public ViewDataRetriever(ViewDataDictionary viewDataDictionary)
        {
            _viewDataDictionary = viewDataDictionary;
        }

        public ModelType GetModelFromViewData()
        {
            var typeName = typeof(ModelType).FullName;
            if (_viewDataDictionary.ContainsKey(typeName))
            {
                return _viewDataDictionary[typeName] as ModelType;
            }
            throw new Exception(string.Format("A model of type {0} was not found in the ViewDataDictionary", typeName));
        }

        /// <summary>
        /// Returns a model if it exists, otherwise returns null (as opposed to throwing an exception).
        /// </summary>
        public ModelType GetNullableModelFromViewData()
        {
            var typeName = typeof(ModelType).FullName;
            if (_viewDataDictionary.ContainsKey(typeName))
            {
                return _viewDataDictionary[typeName] as ModelType;
            }
            return null;
        }
    }
}