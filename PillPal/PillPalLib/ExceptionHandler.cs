using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PillPalLib
{
    internal static class ExceptionHandler
    {

        /// <summary>
        /// Checks whether the HttpRequest was successful and throws an ArgumentException with the error message.
        /// </summary>
        /// <param name="message">The message that's needed to be checked</param>
        /// <exception cref="ArgumentException"></exception>
        internal static void CheckHttpResponse(HttpResponseMessage message)
        {
            if (!message.IsSuccessStatusCode)
            {
                string json = message.Content.ReadAsStringAsync().Result;
                if (json == "") throw new ArgumentException(message.ReasonPhrase); // built-in IActionResult

                string errorMessage;
                try
                {
                    //Custom error message from validator
                    ErrorMessages error = JsonSerializer.Deserialize<ErrorMessages>(json);
                    errorMessage = string.Join("; ", error.errors.Select(x => x.errorMessage));
                }
                catch
                {
                    //Custom error message from IActionResult parameter
                    errorMessage = json;
                }
                throw new ArgumentException(errorMessage);
            }
        }

        private record ErrorMessages(ErrorMessage[] errors);
        private record ErrorMessage(string errorMessage);
    }
}
