using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using Framework.Init;
using OpenQA.Selenium.Firefox;
using Framework.PageObjects;
using System.Data;

namespace TestCases
{
    public class Home
    {
        public static bool IsTcAdded = false;

        IWebDriver driver;

        string ProjectUrl = Convert.ToString(ConfigurationSettings.AppSettings.Get("ProjectUrl"));

      
        public void Demo_MouseHover()
        {

            if (!IsTcAdded)
            {
                Report.AddToHtmlReportTCHeader("Home Page Test Cases");
                IsTcAdded = true;
            }
          
            Report.AddToHtmlReport("TC_DM_01 : To verify that user is able to Mouse hover on Menu.", true, false, true);

            Report.AddToHtmlReport("STEP 1: Insert Url in Browser Addressbar.", false, true);

            driver = Browser.OpenWithSelectedBrowser(driver, ProjectUrl, true);

            HomeObjects objHome = new HomeObjects();

            driver = objHome.Demo_MouseHover(driver);

            if (Convert.ToInt16(ConfigurationSettings.AppSettings.Get("CloseBrowser")) == 1)
            {
                Browser.CloseBrowser(driver);
            }

        }

    }
}

