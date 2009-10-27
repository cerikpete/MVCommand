using System.Collections.Generic;

namespace MVCommand.Validation
{
    /// <summary>
    /// Interface for wrapping results needed when an error is returned.
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// List of error messages to return to the view
        /// </summary>
        IEnumerable<string> ErrorMessages { get; }

        /// <summary>
        /// Dictionary object containing previously filled in values so that they can be returned to the view, so that
        /// data entered is not lost
        /// </summary>
        IDictionary<string, object> ErrorDataDictionary { get; set; }
    }
}