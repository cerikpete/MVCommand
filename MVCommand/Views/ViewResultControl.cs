using System.Web.Mvc;
using MVCommand.Logging;
using MVCommand.Validation;

namespace MVCommand.Views
{
    public class ViewResultControl : ViewUserControl
    {
        public IError ErrorResult
        {
            get
            {
                var typeName = typeof(IError).FullName;
                if (ViewData.ContainsKey(typeName))
                {
                    Log<ViewResultControl>.Debug("Error info present in ViewData");
                    return ViewData[typeName] as IError;
                }
                return null;
            }
        }

        public ISuccess SuccessResult
        {
            get
            {
                var typeName = typeof(ISuccess).FullName;
                if (TempData.ContainsKey(typeName))
                {
                    Log<ViewResultControl>.Debug("Success info present in ViewData");
                    return TempData[typeName] as ISuccess;
                }
                return null;
            }
        }
    }
}