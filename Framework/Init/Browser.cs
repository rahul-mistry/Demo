using System;
using System.Configuration;
using Framework.Init;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;
using System.IO;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using Selenium;
using OpenQA.Selenium.Support;
using System.Diagnostics;



namespace Framework.Init
{
   

    public class Browser
    {
        public static IWebDriver OpenWithSelectedBrowser(IWebDriver driver, string strUrlToOpen, Boolean OpenInNewWindow=false)
        {
            
            //string strPrjUrl = Convert.ToString(ConfigurationSettings.AppSettings.Get("ProjectUrl"));

            //UseBrowser
            if (Convert.ToInt16(ConfigurationSettings.AppSettings.Get("UseBrowser")) == 1)
            {
                driver = OpenFirefoxBrowserWithUrl(driver, strUrlToOpen, OpenInNewWindow);
            }
            else if (Convert.ToInt16(ConfigurationSettings.AppSettings.Get("UseBrowser")) == 2)
            {
                driver = OpenChromeBrowserWithUrl(driver, strUrlToOpen, OpenInNewWindow);
            }
            else if (Convert.ToInt16(ConfigurationSettings.AppSettings.Get("UseBrowser")) == 3)
            {
                driver = OpenIEBrowserWithUrl(driver, strUrlToOpen, OpenInNewWindow);
            }
            else if (Convert.ToInt16(ConfigurationSettings.AppSettings.Get("UseBrowser")) == 4)
            {
                driver = OpenSafariBrowserWithUrl(driver, strUrlToOpen, OpenInNewWindow);
            }
            else if (Convert.ToInt16(ConfigurationSettings.AppSettings.Get("UseBrowser")) == 5)
            {
               driver = OpenOperaBrowserWithUrl(driver, strUrlToOpen, OpenInNewWindow);
            }
            else if (Convert.ToInt16(ConfigurationSettings.AppSettings.Get("UseBrowser")) == 6)
            {
                string strBrowserName = Convert.ToString(ConfigurationSettings.AppSettings.Get("BrowserName"));

                string strView = Convert.ToString(ConfigurationSettings.AppSettings.Get("View"));

                driver = OpenSauceLabWithUrl(driver, strUrlToOpen, strBrowserName, strView, OpenInNewWindow );
            }
            else if (Convert.ToInt16(ConfigurationSettings.AppSettings.Get("UseBrowser")) == 7)
            {
                string strBrowserName = Convert.ToString(ConfigurationSettings.AppSettings.Get("BrowserName"));

                string strView = Convert.ToString(ConfigurationSettings.AppSettings.Get("View"));

                driver = OpenBrowserstackURL(driver, strUrlToOpen, strBrowserName, OpenInNewWindow);
            }
            new Common(driver).pause(10000);
            return driver;
        }
        public static IWebDriver OpenOperaBrowserWithUrl(IWebDriver driver, string strUrlToOpen, Boolean OpenInNewWindow = false)
        {
           try
           {

                Uri remote_grid = new Uri("http://" + "localhost" + ":" + "4444" + "/wd/hub");

                IWebDriver mydriver = new RemoteWebDriver(remote_grid,DesiredCapabilities.Opera());

                DesiredCapabilities mycapability = null;

                mycapability = DesiredCapabilities.Opera();

                mycapability.IsJavaScriptEnabled = true;

                mycapability.SetCapability("opera.host", "127.0.0.1");

                mycapability.SetCapability("opera.binary", "C:\\Program Files\\Opera");

                mycapability.SetCapability("opera.port", -1);

                mycapability.SetCapability("opera.profile", "");

                mycapability.SetCapability(mycapability.BrowserName, DesiredCapabilities.Opera().BrowserName);

                System.Console.WriteLine("step 4");

                //mycapability.SetCapability("opera.profile", @"C:\Program Files\Opera\operadriver-v0.11\docs\com\opera\core\systems\OperaProfile.html");

               Console.WriteLine("step 5");

               mydriver = new RemoteWebDriver(remote_grid, mycapability);
               
               mydriver.Navigate().GoToUrl(strUrlToOpen);
              
               mydriver.Manage().Window.Maximize();

               System.Console.WriteLine("step 8");
               
               Report.AddToHtmlReportPassed("Opera Browser Open for '" + strUrlToOpen + "' .");

         
          }

          catch (Exception ex)
           {
               Console.WriteLine(ex);
                   //Report.AddToHtmlReportFailed(driver, ex, "Opera Browser Open for '" + strUrlToOpen + "' .");
           }
            
            
            return driver;
               
        }
       
        public static IWebDriver OpenFirefoxBrowserWithUrl(IWebDriver driver, string strUrlToOpen, Boolean OpenInNewWindow = false)
        {

            try
            {
                //driver = new FirefoxDriver();

                Uri remote_grid = new Uri("http://" + "localhost" + ":" + "4444" + "/wd/hub");
                DesiredCapabilities capability = null;
                //browser = System.getProperty("browser");
        
                string browser = "firefox";

                if (browser == null || browser.Contains("firefox"))
                {

                    FirefoxProfile profile = new FirefoxProfile();

                    capability = DesiredCapabilities.Firefox();
                    capability.IsJavaScriptEnabled = true;
                    profile.EnableNativeEvents = true;

                    //webdriverEx
                    
                    capability.SetCapability(FirefoxDriver.ProfileCapabilityName, profile);
                }

                //driver.Manage().Cookies.DeleteCookieNamed("logmeonce_user");
                //driver.Manage().Cookies.DeleteCookieNamed("_jsuid");
                //driver.Manage().Cookies.DeleteCookieNamed("logmeonce_session");
                //driver.Manage().Cookies.DeleteCookieNamed("__unam");
                //driver.Manage().Cookies.DeleteCookieNamed("logmeonce_extension");
                //driver.Manage().Cookies.DeleteCookieNamed("logmeonce_policy");
                //driver.Manage().Cookies.DeleteCookieNamed("_eventqueue");
                //driver.Manage().Cookies.DeleteCookieNamed("logmeonce_timeout");

                driver = new ScreenShotRemoteWebDriver(remote_grid, capability);

                //Console.WriteLine(strUrlToOpen);

                driver.Navigate().GoToUrl(strUrlToOpen);
                Report.AddToHtmlReportPassed("FireFox Browser Open for " + strUrlToOpen + ".");
                /* **** ORIGINAL WORKING 
               try{
*/
                //////DesiredCapabilities cap = new DesiredCapabilities();
                //////cap.IsJavaScriptEnabled = true;
                //////if (OpenInNewWindow) driver = new FirefoxDriver(cap);
                //////driver.Navigate().GoToUrl(strUrlToOpen);
                //////Report.AddToHtmlReportPassed("FireFox Browser Open for '" + strUrlToOpen + "'.");
                
                /*   }
                    */
                
                driver.Manage().Window.Maximize();
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(driver, ex, "FireFox Browser Open for " + strUrlToOpen + " .");
            }
            return driver;
        }
        //public static void createAndStartService()
        //{
        //    ChromeDriverService service;
        //    IWebDriver driver;
        //    service = ChromeDriverService.CreateDefaultService()
        //        .usingChromeDriverExecutable(new File("path/to/my/chromedriver"))
        //        .usingAnyFreePort()
        //        .build();
        //    service.Start();
        //}
        public static IWebDriver OpenChromeBrowserWithUrl(IWebDriver driver, string strUrlToOpen, Boolean OpenInNewWindow = false)
        {
            try
            {

                
                DesiredCapabilities capability = DesiredCapabilities.Chrome();

                //ChromeOptions options = new ChromeOptions();
                //string[] strPaths = { @"C:\Users\kiwi\Downloads\logmeonce_2.6.8.crx" };
                //options.AddExtensions(strPaths);
                //capability.SetCapability(ChromeOptions.Capability, options);

                capability.IsJavaScriptEnabled = true;
                capability.SetCapability(capability.BrowserName, DesiredCapabilities.Chrome().BrowserName);
                capability.SetCapability("webdriver.chrome.driver", @"C:\chromedriver.exe");

                Uri remote_grid = new Uri("http://localhost:4444/wd/hub");
               
                
                driver = new ScreenShotRemoteWebDriver(remote_grid, capability);
                //if (OpenInNewWindow) driver = new ScreenShotRemoteWebDriver(remote_grid, capability);
                driver.Navigate().GoToUrl(strUrlToOpen);
                driver.Manage().Window.Maximize();

                Report.AddToHtmlReportPassed("Chrome Browser Open for " + strUrlToOpen + " .");
            }
            catch (Exception ex)
            {
                //Console.WriteLine("ex::", ex.Message);
                Report.AddToHtmlReportFailed(driver, ex, "Chrome Browser Open for " + strUrlToOpen + " .");
            }
            
            return driver;
        }
        public static IWebDriver OpenIEBrowserWithUrl(IWebDriver driver, string strUrlToOpen, Boolean OpenInNewWindow = false)
        {
            try
            {
                /*Uri remote_grid = new Uri("http://" + "localhost" + ":" + "4444" + "/wd/hub");
                DesiredCapabilities capability = null;
                capability = DesiredCapabilities.InternetExplorer();
                capability.IsJavaScriptEnabled = true;

                InternetExplorerOptions ieo = new InternetExplorerOptions();
                ieo.EnableNativeEvents = true;
                ieo.IntroduceInstabilityByIgnoringProtectedModeSettings = true;

                if (OpenInNewWindow) driver = new InternetExplorerDriver("C:\\Program Files\\Internet Explorer", ieo);
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("c:\\tmp\\screenshot.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                driver.Navigate().GoToUrl(strUrlToOpen);
                Report.AddToHtmlReportPassed("Internet Explorer Open for '" + strUrlToOpen + "'.");
                driver.Manage().Window.Maximize();
                new Common(driver).pause(10000);*/

                //DesiredCapabilities capability = DesiredCapabilities.InternetExplorer();
                //capability.IsJavaScriptEnabled = true;
                //capability.SetCapability(capability.BrowserName, DesiredCapabilities.InternetExplorer().BrowserName);
                //Uri remote_grid = new Uri("http://localhost:4444/wd/hub");
                ////driver = new RemoteWebDriver(remote_grid, capability);
                ////driver = new ScreenShotRemoteWebDriver(remote_grid, capability);
                ////driver.Navigate().GoToUrl(strUrlToOpen);
                ////driver.Manage().Window.Maximize();

                //InternetExplorerOptions options = new InternetExplorerOptions();
                //options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;


                ////System.Environment.SetEnvironmentVariable("webdriver.ie.driver", "C:\\Program Files\\Internet Explorer\\IEDriverServer.exe");
                ////InternetExplorerDriver(Capabilities capabilities)
                //driver = new InternetExplorerDriver(@"C:\IEDriverServer.exe", options);
                //driver.Navigate().GoToUrl(strUrlToOpen);
                //Report.AddToHtmlReportPassed("Internet Explorer Open for " + strUrlToOpen + ".");
                //driver.Manage().Window.Maximize();

                DesiredCapabilities capability = DesiredCapabilities.InternetExplorer();

                //ChromeOptions options = new ChromeOptions();
                //string[] strPaths = { @"C:\Users\kiwi\Downloads\logmeonce_2.6.8.crx" };
                //options.AddExtensions(strPaths);
                //capability.SetCapability(ChromeOptions.Capability, options);

                capability.IsJavaScriptEnabled = true;
                capability.SetCapability(capability.BrowserName, DesiredCapabilities.InternetExplorer().BrowserName);
                capability.SetCapability("webdriver.ie.driver", @"C:\IEDriverServer.exe");

                Uri remote_grid = new Uri("http://localhost:4444/wd/hub");


                driver = new ScreenShotRemoteWebDriver(remote_grid, capability);
                //if (OpenInNewWindow) driver = new ScreenShotRemoteWebDriver(remote_grid, capability);
                driver.Navigate().GoToUrl(strUrlToOpen);
                driver.Manage().Window.Maximize();

                Report.AddToHtmlReportPassed("Internet Explorer Browser Open for " + strUrlToOpen + " .");
                
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(driver, ex, "Internet Explorer Browser Open for " + strUrlToOpen + ".");
            }
            return driver;

        }

        public static IWebDriver OpenSafariBrowserWithUrl(IWebDriver driver, string strUrlToOpen, Boolean OpenInNewWindow = false)
        {

            try
            {
                Uri remote_grid = new Uri("http://" + "localhost" + ":" + "4444" + "/wd/hub");
                DesiredCapabilities capability = null;
                
                string browser = "safari";
                //if (browser == null || browser.Contains("safari"))
                {
                    //SafariProfile profile = new SafariProfile();

                    capability = DesiredCapabilities.Safari();
                    capability.IsJavaScriptEnabled = true;

                    //profile.EnableNativeEvents = true;

                    //capability.SetCapability(SafariDriver.ProfileCapabilityName, profile);
                }

                SafariOptions so = new SafariOptions();
                so.AddAdditionalCapability(DesiredCapabilities.Safari().IsJavaScriptEnabled.ToString(), true);

                //DesiredCapabilities capabilities = DesiredCapabilities.Safari();
                //capabilities.BrowserName.Insert(0, "safari");
                //ICommandExecutor executor = new SeleneseCommandExecutor( "http:localhost:4444/" ,"http://www.google.com/" , capabilities);
                //IWebDriver driver1 = new RemoteWebDriver(executor, capabilities);

                driver = new SafariDriver(so); // ScreenShotRemoteWebDriver(remote_grid, capability);
                //driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(20));

                WebDriverBackedSelenium BackedSelenium = new WebDriverBackedSelenium(driver, remote_grid);
                BackedSelenium.Start();

                driver.Navigate().GoToUrl(strUrlToOpen);
                Report.AddToHtmlReportPassed("Safari Browser Open for '" + strUrlToOpen + "' .");
                new Common(driver).pause(70000);
                driver.Manage().Window.Maximize();

                //ScreenShotRemoteWebDriver sd = new ScreenShotRemoteWebDriver(remote_grid, capability); 
                //sd.GetScreenshot().SaveAsFile()
            }
            catch (Exception ex)
            {
                //Console.WriteLine("ex::" + ex);
                //Console.WriteLine("ex.Message::" + ex.Message);
                //Console.WriteLine("ex.InnerException::" + ex.InnerException);
                //Console.WriteLine("ex.StackTrace::" + ex.StackTrace);
                //Report.AddToHtmlReportFailed(driver, ex, "Safari Browser Open for '" + strUrlToOpen + "' .");
            }
            return driver;
        }

        public static IWebDriver OpenSauceLabWithUrl(IWebDriver driver, string strUrlToOpen, string strBrowsername, string strView, Boolean OpenInNewWindow = false)
        {
            try
            {
                if (strBrowsername == "Firefox")
                {
                    DesiredCapabilities caps = DesiredCapabilities.Firefox();
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "33.0");
                    caps.SetCapability("name", "TFC Automation Script - SauceLab");
                    caps.SetCapability("username", "msrahul2912");
                    caps.SetCapability("accessKey", "103ece15-e13e-474b-9988-9228fb8d07d3");
                    driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps);
                }
                else if (strBrowsername == "Chrome")
                {
                    DesiredCapabilities caps = DesiredCapabilities.Chrome();
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "43.0");
                    caps.SetCapability("name", "TFC Automation Script - SauceLab");
                    caps.SetCapability("username", "msrahul2912");
                    caps.SetCapability("accessKey", "103ece15-e13e-474b-9988-9228fb8d07d3");
                    driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps);
                }
                if (strBrowsername == "IE")
                {
                    DesiredCapabilities caps = DesiredCapabilities.InternetExplorer();
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "10.0s");
                    caps.SetCapability("name", "TFC Automation Script - SauceLab");
                    caps.SetCapability("username", "msrahul2912");
                    caps.SetCapability("accessKey", "103ece15-e13e-474b-9988-9228fb8d07d3");
                    driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps);
                }
                if (strBrowsername == "Android")
                {
                    DesiredCapabilities caps = DesiredCapabilities.Android();
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "4.3");
                    caps.SetCapability("deviceName", "Google Nexus 7C Emulator");
                    caps.SetCapability("device-orientation", strView); 
                    caps.SetCapability("name", "TFC Automation Script - SauceLab");
                    caps.SetCapability("username", "msrahul2912");
                    caps.SetCapability("accessKey", "103ece15-e13e-474b-9988-9228fb8d07d3");
                    driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps);
                }
                
                if (strBrowsername == "IOS")
                {
                    DesiredCapabilities caps = DesiredCapabilities.IPad();
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6"); 
                    caps.SetCapability(CapabilityType.Version, "4");
                   // caps.SetCapability("deviceName", "iPad");
                    caps.SetCapability("device-orientation", strView);
                    caps.SetCapability("name", "TFC Automation Script - SauceLab");
                    caps.SetCapability("username", "msrahul2912");
                    caps.SetCapability("accessKey", "103ece15-e13e-474b-9988-9228fb8d07d3");
                    driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps);
                }

                driver.Navigate().GoToUrl(strUrlToOpen);
              
                Report.AddToHtmlReportPassed("Sauce Lab Open for " + strUrlToOpen + " .");
            }
            catch (Exception ex)
            {
                //Console.WriteLine("ex::", ex.Message);
                Report.AddToHtmlReportFailed(driver, ex, "Sauce Lab Open for " + strUrlToOpen + " .");
            }
            
            return driver;

        }

        public static IWebDriver OpenBrowserstackURL(IWebDriver driver, string strUrlToOpen, string strBrowsername, Boolean OpenInNewWindow = false)
        {
            try
            {
                if (strBrowsername == "Firefox")
                {
                    DesiredCapabilities capability = DesiredCapabilities.Firefox();
                    capability.SetCapability("browserstack.user", "rahul1702");
                    capability.SetCapability("browserstack.key", "1KH9bA3VFtQszDUxacKq");

                    driver = new RemoteWebDriver(
                      new Uri("http://hub-cloud.browserstack.com/wd/hub/"), capability
                    );
                }
                driver.Navigate().GoToUrl(strUrlToOpen);

                Report.AddToHtmlReportPassed("Browserstack Open for " + strUrlToOpen + " .");
            }
            catch (Exception ex)
            {
                //Console.WriteLine("ex::", ex.Message);
                Report.AddToHtmlReportFailed(driver, ex, "Browserstack Open for " + strUrlToOpen + " .");
            }

            return driver;

        }

        public static void CloseBrowser(IWebDriver driver)
        {
            string strDriverUrl = string.Empty; ;
            try
            {
                new Common(driver).pause(10000);
                
                //new Common(driver).pause(10000);

                //try
                //{
                //    driver.SwitchTo().ActiveElement().SendKeys(Keys.Tab);
                //    driver.SwitchTo().ActiveElement().SendKeys(Keys.Enter);
                //    Console.WriteLine("PP1:: ");
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine("EX1:: " + ex);
                //}
                //try
                //{
                //    driver.SwitchTo().Window("Internet Explorer").Close();
                //    Console.WriteLine("PP2:: ");
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine("EX2:: " + ex);
                //}
                //driver.Close();
                driver.Quit();
                //new Common(driver).pause(10000);
                //KillProcessByName("iexplore");
                //KillProcessByName("IEDriverServer");

                Report.AddToHtmlReportPassed("Browser close.");
            }
            catch (Exception ex)
            {
                Report.AddToHtmlReportFailed(driver, ex, "Browser close.");
            }

        }

        private static void KillProcessByName(string processName)
        {

            try
            {
                foreach (Process process in Process.GetProcessesByName(processName))
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Kill EX:: " + ex);
            }
        }
    }
}
