using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                throw new ArgumentException(message.Content.ReadAsStringAsync().Result);
            }
        }

    }
}
