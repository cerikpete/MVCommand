using System.Web.Mvc;

namespace MVCommand.Views
{
    public class ViewBaseControl<ModelType> : ViewUserControl<ModelType> where ModelType : class
    {
        protected override void SetViewData(ViewDataDictionary viewData)
        {
            if (viewData == null)
                return;
            var viewDataRetriever = new ViewDataRetriever<ModelType>(viewData);
            viewData.Model = viewDataRetriever.GetModelFromViewData();
            base.SetViewData(viewData);
        }
    }
}