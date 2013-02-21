selenium-net-formatters
=======================

A set of formatters to the [Selenium IDE](http://docs.seleniumhq.org/docs/02_selenium_ide.jsp) that generate friendly C# tests.

These formatters will generate tests that share common code, such as setup,
and teardown. In addition, common methods emitted by the default C# / WebDriver
format are included as well:

* bool IsElementPresent(By by)
* string CloseAlertAndGetItsText()

These formatters are also able to detect if the tests are running in the
[Jenkins CI](http://jenkins-ci.org/) environment, and they will change the
application URL being tested to test the designated build.  These application
URLs will need to be changed to suit your solution and CI configuration.


Getting Started
===============


Solution Setup
--------------

1. Create a class library project to your solution.
2. Add the BaseTest.cs file to this project.
    * Adjust the namespace of the class to match your project
    * Adjust the solution name/web application to test
    * Adjust the deployed name/web application to test
    * You may rename this BaseTest class name if you choose.
    
Selenium IDE Setup
------------------

1. Open Firefox and the Selenium IDE
2. In the Selenium IDE, select Options from the Options menu.
3. Select the "Formats" tab.
4. Click on the "Add" button.
5. Name the format: I prefer "C# / WebDriver / Chrome"
6. Copy the content of the formatter file: c#-webdriver-ch.js
7. Paste the contents of the file in the main text area of the "Selenium IDE 
   Format Source" window.
8. Click on the "Save" button. **Note:** There is currently a bug where the
   format doesn't appear immediately. Close the Options window and re-open
   it, and the format will appear in the list.
9. Select the format from the list.
10. Change the "Namespace" field to match your project's namespace.
11. I like to change the "Category" field to match the browser driver used.
12. Change the "Base Class" field if you renamed the BaseTest class.
13. Repeat steps 4-12 for additional drivers (Firefox, Explorer).


Writing Tests
=============

Using the Selenium IDE, create your browser tests like usual. When it comes
time to export them, export the tests with the new formats:

1. In the Selenium IDE, select "Export Test Case as ..." from the File menu.
2. Choose the format. In the example above, the format would be named "C# /
   WebDriver / Chrome"
3. Save the file as a .cs file in your test class library project. I like to
   give the class a suffix of "-chrome", to distinguish which driver is used
   for each test class.  The filename would then be "TheTest-chrome.cs".
4. Go back to your solution, and add the new source file to your test project.
5. Compile and run tests!


Getting Help
============

Please note any issues with these formats in the issues section of this project:

https://github.com/azavea/selenium-net-formatters/issues