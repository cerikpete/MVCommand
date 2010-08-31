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
                    var successResult = TempData[typeName] as ISuccess;
                    Log<ViewResultControl>.Debug("Success info present in ViewData, message: " + successResult.Message);
                    return successResult;
                }
                return null;
            }
        }
    }
}