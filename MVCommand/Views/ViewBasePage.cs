using System.Web.Mvc;

namespace MVCommand.Views
{
    public class ViewBasePage<ModelType> : ViewPage<ModelType> where ModelType : class
    {
        private IViewDataRetriever<ModelType> _viewDataRetriever;

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
            _viewDataRetriever = new ViewDataRetriever<ModelType>(ViewData);
        }

        public new ModelType Model
        {
            get
            {
                return _viewDataRetriever.GetModelFromViewData();
            }
        }
    }
}