using System;
using System.Web.Mvc;

namespace MVCommand.Views
{
    /// <summary>
    /// Base control for controls that don't require the model to be present in the ViewDataDictionary.
    /// </summary>
    /// <typeparam name="ModelType">Type of model that is optionally used by the control</typeparam>
    public class NullableModelControl<ModelType> : ViewUserControl<ModelType> where ModelType : class
    {
        private IViewDataRetriever<ModelType> _viewDataRetriever;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _viewDataRetriever = new ViewDataRetriever<ModelType>(ViewData);
        }

        public new ModelType Model
        {
            get
            {
                return _viewDataRetriever.GetNullableModelFromViewData();
            }
        }

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