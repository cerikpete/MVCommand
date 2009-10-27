using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MVCommand.Extensions
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Loads the supplied object to the view data dictionary for use by other partials
        /// </summary>
        public static void RenderPartialWithData(this HtmlHelper htmlHelper, string partialName, string key, object objectToLoad, ViewDataDictionary viewDataDictionary)
        {
            if (!viewDataDictionary.ContainsKey(key))
            {
                viewDataDictionary.Add(key, objectToLoad);
            }
            htmlHelper.RenderPartial(partialName);
        }
    }
}