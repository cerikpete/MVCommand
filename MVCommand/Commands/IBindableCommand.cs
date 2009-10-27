using System.Collections.Generic;
using MVCommand.Validation;

namespace MVCommand.Commands
{
    public interface IBindableCommand<ModelType>
    {
        /// <summary>
        /// Gets or sets the model on the command
        /// </summary>
        ModelType Model { get; set; }

        /// <summary>
        /// Loads and returns an IError object populated with the appropriate values
        /// </summary>
        /// <param name="errorDictionaryItems">Dictionary items used to load the ErrorDataDictionary property on IError with values that were entered in the
        /// view so that they can be returned back to the view</param>
        /// <returns>Populated IError instance</returns>
        IError ErrorData(params KeyValuePair<string, object>[] errorDictionaryItems);

        /// <summary>
        /// Loads and returns an ISuccess object populated with the given success message
        /// </summary>
        /// <param name="successMessage">Message to display on the view indicating the command completed successfully</param>
        /// <returns>Populated ISuccess instance</returns>
        ISuccess SuccessData(string successMessage);
    }
}