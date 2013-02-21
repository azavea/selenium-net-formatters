using System;
using System.Text;
using OpenQA.Selenium;

/* Copyright (c) David Zwarg <dzwarg@azavea.com>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

namespace SeleniumTest
{
    /// <summary>
    /// An abstract class that manages setup and teardown operations
    /// common to all selenium tests.
    /// 
    /// Common setup operations are:
    ///   - Set class variables:
    ///     - driver
    ///     - baseURL
    ///     - verificationErrors
    ///     - acceptNextAlert
    ///     - appURL
    ///   - Detect if this test is running in Jenkins CI, and modify the
    ///     application URL from the development URL to the deployed CI
    ///     URL.
    /// </summary>
    public abstract class BaseTest
    {
        /// <summary>
        /// The driver to use for the test. A list of available drivers
        /// are available at http://docs.seleniumhq.org/download/
        /// </summary>
        protected IWebDriver driver;

        /// <summary>
        /// A list of verification errors -- these do not fail tests,
        /// but will be reported to the test output.
        /// </summary>
        protected StringBuilder verificationErrors;

        /// <summary>
        /// The base URL to start from.
        /// </summary>
        protected string baseURL;

        /// <summary>
        /// The appURL to open initially.
        /// </summary>
        protected string appURL;

        /// <summary>
        /// A flag to accept any window.alert() dialogs.
        /// </summary>
        protected bool acceptNextAlert;

        /// <summary>
        /// A method that sets up the class variables, and
        /// detects if the test is running inside of Jenkins CI.
        /// </summary>
        /// <param name="driver">An instance of the driver to use.</param>
        protected void InternalSetup(IWebDriver driver)
        {
            this.driver = driver;
            baseURL = "http://localhost";
            verificationErrors = new StringBuilder();
            acceptNextAlert = false;

            // Jenkins CI will set an environment variable if this test
            // is running on the CI server -- that's the appURL we should use.
            appURL = Environment.GetEnvironmentVariable("BUILD_NUMBER");
            if (appURL == null)
            {
                // if the environment variable wasn't set, use the solution
                // web site name
                appURL = "/WebApplication1/";
            }
            else
            {
                // if the environment variable was set, use the URL of the
                // deployed solution
                appURL = "/WebApplication1_" + appURL + "/";
            }
        }

        /// <summary>
        /// Quit the driver, and close the browser after the test.
        /// </summary>
        protected void InternalTeardown()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        /// <summary>
        /// Check if an element is present.
        /// 
        /// IsElementPresent doesn't need to be generated in every
        /// source file, just use this common implementation.
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        protected bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get the text from an alert box, and close it.
        /// 
        /// CloseAlertAndGetItsText doesn't need to be generated
        /// in every source file, just use this common implementation.
        /// </summary>
        /// <returns></returns>
        protected string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alert.Text;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}