using System.Web.Mvc;

namespace MVCommand.Views
{
    public class ViewBasePage<ModelType> : ViewPage<ModelType> where ModelType : class
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