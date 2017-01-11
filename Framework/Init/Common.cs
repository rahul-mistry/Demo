using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using Framework.PageObjects;
using NUnit.Framework;

namespace Framework.Init
{
    public class Common
    {
        DateTime date = new DateTime();
        protected IWait<IWebDriver> wait;
        protected IWebDriver driver;

        public Common(IWebDriver driver)
        {

            this.driver = driver;
        }
        /* "isElement" method is used for checking is element present on the page or not */

        //    public boolean isElementPresent (WebElement webElement){
        //		return webElement != null;		
        //    }

        public Boolean isElementPresent(IWebElement webElement)
        {
            if (webElement != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void createNewCookie(IWebDriver driver, String key, String value)
        {
            Cookie cookie = new Cookie(key, value);
            driver.Manage().Cookies.AddCookie(cookie);
        }

    //    public ExpectedCondition<IWebElement> visibilityOfElementLocated(final By by) {
    //    return new ExpectedCondition<IWebElement>() {
          
    //    public IWebElement apply(IWebDriver driver) {
    //        IWebElement element = driver.FindElement( by);
    //        return element.isDisplayed() ? element : null;
    //      }
    //    };
    //}

        public void waitForElement(String selector)
        {
            TimeSpan ts = new TimeSpan(0, 0, 20);
            wait = new WebDriverWait(driver, ts);
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(selector)));
            }
            catch (TimeoutException e)
            {
            }
        }

        public void waitForElement(By element)
        {
            TimeSpan t1 = new TimeSpan(00, 01, 00);
            wait = new WebDriverWait(driver, t1);
            try
            {
                wait.PollingInterval = new TimeSpan(00, 00, 01);
                wait.Until(ExpectedConditions.ElementIsVisible(element));
            }
            catch (TimeoutException e)
            {
            }
            catch (StaleElementReferenceException staleElementReferenceException)
            {

            }
        }

        public IWebElement FindElementWithDynamicWait(IWebDriver driver, By elementXpath, string strReportMessage = "")
        {
            IWebElement element = null;

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(5));
                element = wait.Until<IWebElement>((d) =>
                {
                    return d.FindElement(elementXpath);
                });

                if (strReportMessage.Trim().Length > 0)
                {
                    {
                        Report.AddToHtmlReportPassed(strReportMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(driver, ex, strReportMessage);
                driver.Close();
                Assert.Fail();
            }
            return element;
        }

        public void ScrolltoView(IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView();", element);
           
        }


        /** 
	 * Pauses the thread for a period of time.
	 * 
	 * @param mseconds			mseconds  to pause the thread.
	 */
        public void pause(int mseconds)
        {
            try
            {
                Thread.Sleep(mseconds);
            }
            catch (ThreadInterruptedException e)
            {
            }
        }

        public static void pauseStatic(int mseconds)
        {
            try
            {
                Thread.Sleep(mseconds);
            }
            catch (ThreadInterruptedException e)
            {
            }
        }

        public void waitForElementHide(String xpath)
        {
            IWebElement elementToWait;
            int counter = 0;
            elementToWait = driver.FindElement(By.XPath(xpath));
            Boolean isDisplayed = this.isElementDisplayed(elementToWait);
            do
            {
                this.pause(3000);
                isDisplayed = this.isElementDisplayed(elementToWait);
                counter++;
            }
            while (isDisplayed && counter < 10);

        }

        /**
	 * Checks whether the needed WebElement is displayed or not.
	 * 
	 * @param element	Needed element
	 * 
	 * @return	true or false.
	 */
        public Boolean isElementDisplayed(IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void waitUntilElementIsVisible(By by)
        {
            TimeSpan t1 = new TimeSpan(00, 00, 20);
            WebDriverWait wait = new WebDriverWait(driver, t1);
            wait.Until(ExpectedConditions.ElementIsVisible(by));
        }

        public void waitForElementHide(IWebElement elementToWait)
        {
            Boolean isDisplayed = false;
            int counter = 0;
            try
            {
                isDisplayed = elementToWait.Displayed;
            }
            catch (Exception e)
            {

            }
            while (isDisplayed && counter < 10)
            {
                this.pause(3000);
                isDisplayed = this.isElementDisplayed(elementToWait);
                counter++;
            }
        }

        public String ajaxFinised(String num)
        {
            Object isAjaxFinished;
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            isAjaxFinished = js.ExecuteScript("return jQuery.active == " + num);
            return isAjaxFinished.ToString();
        }

        public String getInnerHTML(String elementID)
        {
            Object innerHTML;
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            innerHTML = js.ExecuteScript("return document.getElementById('" + elementID + "').innerHTML");
            return innerHTML.ToString();
        }

        public void waitForAjax(String num)
        {
            String ajax;
            for (int second = 0; ; second++)
            {
                ajax = this.ajaxFinised(num);

                if (second >= 15)
                {
                    break;
                }
                else if (ajax.Equals("true"))
                {
                    break;
                }
                this.pause(1000);
            }
        }

        public void waitForAjax()
        {
            String ajax;
            for (int second = 0; ; second++)
            {
                ajax = this.ajaxFinised("0");
                if (second >= 15)
                {
                    break;
                }
                else if (ajax.Equals("true"))
                {
                    break;
                }
                this.pause(1000);
            }
        }

        public string ElementGetText(IWebElement element, string strReportMessage = "", int PauseTime = 2000)
        {
            string strRetVal = string.Empty;
            try
            {
                pause(PauseTime);
                if (element != null)
                {
                    strRetVal = element.Text;
                    if (strRetVal.Trim().Length == 0) strRetVal = element.GetAttribute("value");
                }
                //if (strReportMessage.Trim().Length > 0) Report.AddToHtmlReportPassed(strReportMessage);
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(driver, ex, "Error while fetching text from given element.");
            }
            return strRetVal;
        }

        /**
	 * Generates random symbols;
	 * 
	 * @param length	Length of the generated symbols.
	 * 
	 * @return	StringBuffer object.
	 */
        public static String generateRandomSymbols(int length)
        {
            String total = "TheQuickBrownFoxJumpsOverTheLazyDog1234567890";
            //StringBuffer buf = new StringBuffer();
            StringBuilder buf = new StringBuilder();
            char _char;
            for (int i = 0; i < length; i++)
            {
                //char _char = total.charAt((int)(Math.random() * 100) % total.length());
                Random r1 = new Random();
                _char = total[r1.Next(0, total.Length-1)];
                buf.Append(_char);
            }
            return buf.ToString();
        }

        /**
	 * Generates random symbols;
	 * 
	 * @param length	Length of the generated symbols.
	 * 
	 * @return	StringBuffer object.
	 */
        public String generateRandomChars(int length, bool IsSpacReqd = false)
        {
            String total = string.Empty;
            if (IsSpacReqd) total = "The QuickBrownFoxJumpsOverTheLazyDog";
            else total = "TheQuickBrownFoxJumpsOverTheLazyDog";

            //StringBuffer buf = new StringBuffer();
            StringBuilder buf = new StringBuilder();
            char _char;
            Random r1 = new Random();
            for (int i = 0; i < length; i++)
            {
                //char _char = total.charAt((int)(Math.random() * 100) % total.length());

                _char = total[r1.Next(0, total.Length - 1)];
                buf.Append(_char);
            }
            return buf.ToString();
        }

        /**
	 * Gets current time in the following format
	 * Month, Date, Hours, Minutes, Seconds, Millisecond.
	 * 
	 * @return Current date.
	 */
        //public String getCurrentTimeStampString()
        //{

        //    DateTime date = new DateTime();

        //    //SimpleDateFormat sd = new SimpleDateFormat("MMddHHmmssSS");
        //    TimeZone timeZone = TimeZone.CurrentTimeZone;

        //    Calendar cal = new Calendar();
        //    cal.get
        //    Calendar cal = Calendar.getInstance(new SimpleTimeZone(timeZone.getOffset(date.getTime()), "GMT"));
        //    sd.setCalendar(cal);
        //    return sd.format(date);




        //    java.util.Date date = new java.util.Date();

        //    SimpleDateFormat sd = new SimpleDateFormat("MMddHHmmssSS");
        //    TimeZone timeZone = TimeZone.getDefault();
        //    Calendar cal = Calendar.getInstance(new SimpleTimeZone
        //                (timeZone.getOffset(date.getTime()), "GMT"));
        //    sd.setCalendar(cal);
        //    return sd.format(date);
        //}


        public String generateRandomString(int length)
        {
            Random random = new Random();
            String result = "";
            for (int i = 0; i < length; i++)
            {
                int rndChrCode = random.Next(1,52);
                if (rndChrCode > 25)
                {
                    rndChrCode += 6;
                }
                rndChrCode += 65;
                result += (char)rndChrCode;
            }
            return result.ToLower();
        }

        /**
	 * Gets name with current date.
	 * 
	 * @param name Ethalon value. 
	 * @return Random name.
	 */
        public String getRandomName(String name)
        {
            Random random = new Random();
            //String randomName = name + this.generateRandomString(10) + this.getCurrentTimeStampString() + random.Next(100 - 2);
            String randomName = name + this.generateRandomString(10) + random.Next(100 - 2);
            return randomName;
        }

        /**
	 * Clicks on each existed toggle.
	 */
        public void clicksOnAllToggles(List<IWebElement> elementList)
        {
            foreach (IWebElement element in elementList)
            {
                element.Click();
                waitForAjax();
            }
        }

        /**
	 * Generated random element from the put WebElement list.
	 * 
	 * @param element Needed WebElement list.
	 * 
	 * @return random element.
	 */
        public IWebElement randomElementFromTheList(List<IWebElement> element)
        {

            IWebElement randomElement = null;
            int seconds = 0;
            do
            {
                try
                {
                    int size = element.Count;
                    randomElement = element[(int)(new Random().Next(0, size-1))];
                    break;
                }
                catch (ArithmeticException e)
                {
                    this.pause(1000);
                    seconds++;
                }
            }
            while (seconds < 10);
            return randomElement;
        }

        /**
	 * Generated random element from the list.
	 * 
	 * @param element Needed WebElement list.
	 * 
	 * @return random element.
	 */
        public Object randomElement(IList element)
        {
            Object randomObject;
            randomObject = element[(int)(new Random().Next(0, element.Count -1))];
            return randomObject;
        }

        /**
	 * Makes select from the drop-down list.
	 * 
	 * @param element        Element on the page
	 * @param optionToChoose Visible text in drop-down list
	 */
        public void select(IWebElement element, String optionToChoose)
        {
            SelectElement select = new SelectElement(element);
            //Select select = new Select(element);
            //select.selectByVisibleText(optionToChoose);
            select.SelectByText(optionToChoose);
        }

        /**
	 * Opens link from the element
	 * on the same browser instance.
	 * 
	 * @param element Element that contains the link.
	 */

        public void selectByIndex(IWebElement element, int optionToChoose)
        {
            SelectElement select = new SelectElement(element);
            //Select select = new Select(element);
            //select.selectByVisibleText(optionToChoose);
            select.SelectByIndex(optionToChoose);
        }
        public void openLinkInTheSameWindow(IWebElement element)
        {
            String url;
            url = element.GetAttribute("href");
            driver.Url = url;
        }

        /**
	 * Finds element from the list of
	 * elements.
	 * 
	 * @param elements    List of the elements.
	 * @param elementName Name of the element.
	 * @return			  WebElement object.
	 */
        public IWebElement findElementInList(List<IWebElement> elements,
                  String elementName)
        {

            String name;
            Boolean isElementFound;
            IWebElement elementFound;

            elementFound = null;

            foreach (IWebElement element in elements)
            {

                name = element.Text;
                isElementFound = name.Contains(elementName);

                /* If brand was found create new webElement for this 
                 * brand.
                 */
                if (isElementFound)
                {
                    elementFound = element;
                    break;
                }
            }
            return elementFound;
        }

        public void scrollToElement(IWebElement element)
        {
            (driver as IJavaScriptExecutor).ExecuteScript(string.Format("window.scrollTo(0, {0});", (element.Location.Y * -1)));
        }

        public static void enterText(IWebElement element, string strText, bool IsClearContents = true)
        {
            element.Click();
            pauseStatic(500);
            if (IsClearContents) element.Clear();
            element.SendKeys(strText);
        }
        private static readonly object syncLock1 = new object();
        public static DataTable ExcelReadData(string selectCmd, string strFilePath = "", bool IsHeader = true)
        {
            string connString;
            if(IsHeader) connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties='Excel 12.0;HDR=YES'";
            else connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties='Excel 12.0;HDR=NO'";

            DataTable dt = new DataTable();

            lock (syncLock1)
            {
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connString))
                    {
                        OleDbDataAdapter adapter = new OleDbDataAdapter(selectCmd, conn);
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex) { throw ex; }
            }
            return dt;
        }
     
        public IWebElement FindElement(By elementXpath, string strReportMessage = "", Boolean WaitForElement = true, int PauseTime = 0, bool isOrangeReqd = false)
        {
            IWebElement element = null;
            try
            {
                if (PauseTime > 0) pause(PauseTime);
                if (WaitForElement) waitForElement(elementXpath);
                element = driver.FindElement(elementXpath);
                if (strReportMessage.Trim().Length > 0)
                {
                    //if (isOrangeReqd == true)
                    //{
                    //    Report.AddToHtmlReportOrange(strReportMessage);
                    //}
                    //else
                    {
                        Report.AddToHtmlReportPassed(strReportMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(driver, ex, strReportMessage);
            }
            return element;
        }
        public IWebElement FindElementClick(By elementXpath, string strReportMessage = "", Boolean WaitForElement = true, int PauseTime = 0)
        {
            IWebElement element = null;
            try
            {
                element = FindElement(elementXpath, strReportMessage, WaitForElement, PauseTime);
                pause(1000);
                if (element != null) element.Click();

                //if (strReportMessage.Trim().Length > 0) Report.AddToHtmlReportPassed(strReportMessage);
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(driver, ex, "Error while clicking element.");
            }
            return element;
        }
        
        
        public static int ExcelWriteData(string selectCmd, string strFilePath = "")
        {
            var connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties='Excel 12.0;HDR=YES'";
            int retVal = 0;
            DataTable dt = new DataTable();
            lock (syncLock1)
            {

                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connString))
                    {
                        if (conn.State != ConnectionState.Open) conn.Open();
                        OleDbCommand cmd = new OleDbCommand(selectCmd, conn);
                        retVal = cmd.ExecuteNonQuery();
                        conn.Close();
                        //OleDbDataAdapter adapter = new OleDbDataAdapter(selectCmd, conn);
                        //adapter.Fill(dt);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex); } 
            }
            return retVal;
        }

        //Function to get random number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static string GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return Convert.ToString(getrandom.Next(min, max));
            }
        }

        public static string highlightQuotedHTML(string strLine)
        {
            int j = 0;
            for (int i = 0; i < strLine.Length; i++)
            {
                if (strLine.Substring(i, 1) == "'")
                {
                    if (strLine.Substring(i, 3) == "'s ") { i = i + 2; }
                    else
                    {
                        if (j % 2 == 0)
                        {
                            strLine = strLine.Substring(0, i) + strLine.Substring(i).Substring(0, 1).Replace("'", "<strong>") + strLine.Substring(i + 1);
                            j++;
                        }
                        else
                        {
                            strLine = strLine.Substring(0, i) + strLine.Substring(i).Substring(0, 1).Replace("'", "</strong>") + strLine.Substring(i + 1);
                            j++;
                        }
                    }
                }
            }
            strLine = strLine.Replace("<strong>", "<strong>'");
            strLine = strLine.Replace("</strong>", "'</strong>");
            
            return strLine;
        }
    }

    public static class ExtMethods
    {
        #region ##### Experimental #####
        public static void WaitForPageToLoad(this IWebDriver driver)
        {
            TimeSpan timeout = new TimeSpan(0, 0, 180);
            WebDriverWait wait = new WebDriverWait(driver, timeout);

            IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
            if (javascript == null)
                throw new ArgumentException("driver", "Driver must support javascript execution");

            wait.Until((d) =>
            {
                try
                {
                    string readyState = javascript.ExecuteScript(
                    "if (document.readyState) return document.readyState;").ToString();
                    return readyState.ToLower() == "complete";
                }
                catch (InvalidOperationException e)
                {
                    //Window is no longer available
                    return e.Message.ToLower().Contains("unable to get browser");
                }
                catch (WebDriverException e)
                {
                    //Browser is no longer available
                    return e.Message.ToLower().Contains("unable to connect");
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
        #endregion
    }
}
