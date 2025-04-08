using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalLib.StaticTools
{
    public static class DriverExtension
    {
        /// <summary>
        /// Waits for an element to be found in the DOM and returns it.
        /// </summary>
        /// <param name="driver">The WebDriver instance.</param>
        /// <param name="by">The By locator for the element.</param>
        /// <param name="tries">Number of attempts to find the element.</param>
        /// <param name="secondsBeforeRetry">Seconds to wait before retrying.</param>
        /// <returns>The found IWebElement.</returns>
        public static IWebElement AwaitedFindElement(this WebDriver driver, By by, int tries = 5, int secondsBeforeRetry = 5)
        {
            for (int i = 0; i < tries; i++)
            {
                try
                {
                    var element = driver.FindElement(by);
                    return element;
                }
                catch (Exception) { }
                Thread.Sleep(secondsBeforeRetry * 1000);
            }
            throw new Exception($"Element not found after {tries} tries");
        }

        /// <summary>
        /// Waits for more elements to be found in the DOM and returns them.
        /// </summary>
        /// <param name="driver">The WebDriver instance.</param>
        /// <param name="by">The By locator for the elements.</param>
        /// <param name="tries">Number of attempts to find the elements.</param>
        /// <param name="secondsBeforeRetry">Seconds to wait before retrying.</param>
        /// <returns>The collection of found IWebElement.</returns>
        public static ReadOnlyCollection<IWebElement> AwaitedFindElements(this WebDriver driver, By by, int tries = 5, int secondsBeforeRetry = 5)
        {
            for (int i = 0; i < tries; i++)
            {
                var elements = driver.FindElements(by);
                if (elements.Count > 0)
                    return elements;
                Thread.Sleep(secondsBeforeRetry * 1000);
            }
            throw new Exception($"Element not found after {tries} tries");
        }
    }
}
