﻿using System.Web.Mvc;

namespace MVCommand.Views
{
    public class RazorViewBasePage<ModelType> : WebViewPage<ModelType> where ModelType : class
    {
        protected override void SetViewData(ViewDataDictionary viewData)
        {
            if (viewData == null)
                return;
            var viewDataRetriever = new ViewDataRetriever<ModelType>(viewData);
            viewData.Model = viewDataRetriever.GetModelFromViewData();
            base.SetViewData(viewData);
        }

        public override void Execute() {  }
    }
}