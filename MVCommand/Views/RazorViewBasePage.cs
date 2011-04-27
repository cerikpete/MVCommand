using System.Web.Mvc;

namespace MVCommand.Views
{
    public class RazorViewBasePage<ModelType> : WebViewPage<ModelType> where ModelType : class
    {
        private IViewDataRetriever<ModelType> _viewDataRetriever;

        protected override void InitializePage()
        {
            _viewDataRetriever = new ViewDataRetriever<ModelType>(ViewData);
            base.InitializePage();
        }

        public override void Execute() {  }

        public new ModelType Model
        {
            get
            {
                return _viewDataRetriever.GetModelFromViewData();
            }
        }
    }
}