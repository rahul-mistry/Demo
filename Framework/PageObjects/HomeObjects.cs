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
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Framework.PageObjects
{
    public class HomeObjects
    {
        IWebDriver _driver;
      
        public static string strFirstName;
        public static string strLastName;

        public IWebDriver Demo_MouseHover(IWebDriver driver)
        {
            _driver = driver;

            Demo_MouseHover();
           
            return _driver;
        }

        public void Demo_MouseHover()
        {
            //Windows Resizing
            Report.AddToHtmlReport("STEP 2: Mouse hover on 'Menu' on Demo Qa Home Page.", false, true);

            Actions myAction = new Actions(_driver);
            myAction.MoveToElement(_driver.FindElement(By.XPath(ElementLocators.Menu_txt_Home))).Build().Perform();
               
            myAction.MoveToElement(_driver.FindElement(By.XPath(ElementLocators.Menu_txt_AboutUs))).Build().Perform();
   
            myAction.MoveToElement(_driver.FindElement(By.XPath(ElementLocators.Menu_txt_Services))).Build().Perform();

            myAction.MoveToElement(_driver.FindElement(By.XPath(ElementLocators.Menu_txt_Demo))).Build().Perform();

            myAction.MoveToElement(_driver.FindElement(By.XPath(ElementLocators.Menu_txt_Blog))).Build().Perform();

            myAction.MoveToElement(_driver.FindElement(By.XPath(ElementLocators.Menu_txt_Contact))).Build().Perform();
            
            //IWebElement Home_tab_Accordion = new Common(_driver).FindElement(By.XPath(ElementLocators.Home_tab_Accordion), "To verify user click on 'Accordion' tab on DemoQa Home page.");
            IWebElement Home_tab_Accordian = new Common(_driver).FindElementWithDynamicWait(_driver, By.XPath(ElementLocators.Home_tab_Accordion),"Accordian Tab verify");
            Home_tab_Accordian.Click();
             
            IWebElement Accordion_tab_customize = new Common(_driver).FindElementWithDynamicWait(_driver,By.XPath(ElementLocators.Accordion_tab_customize), "To verify user can click on 'Customize icon' tab on Accordion page");
            Accordion_tab_customize.Click();
            IWebElement Registration_tab = new Common(_driver).FindElementWithDynamicWait(_driver,By.XPath(ElementLocators.Registration_tab), "To verify 'Registration' tab on DemoQa Home page.");
            Registration_tab.Click();

            //new Common(_driver).pause(2000);
            //IWebElement Home_txt_firstname = new Common(_driver).FindElement(By.XPath(ElementLocators.Home_txt_firstname), "'First Name' textbox on DemoQa Registration page.", true, 2000);
            //IWebElement Home_txt_lastname = new Common(_driver).FindElement(By.XPath(ElementLocators.Home_txt_lastname), "'Last Name' textbox on DemoQa Registration page.", true, 2000);

            string fname = "test1";
            string lname = "Test2";
            string phone = "9898989856";
            string uname = "test_testing";
            string email = "test@gmail.com";
            string about = "Hi how are you?";
            string password = "test@123";
            string Confirmpassword = "test@123";

            IWebElement FName = new Common(_driver).FindElementWithDynamicWait(_driver, By.XPath(ElementLocators.Home_txt_firstname), "'First Name' textbox on DemoQa Registration page.");
            IWebElement LName = new Common(_driver).FindElementWithDynamicWait(_driver, By.XPath(ElementLocators.Home_txt_lastname), "'Last Name' textbox on DemoQa Registration page.");

            IWebElement the_checkbox = _driver.FindElement(By.XPath("//input[@value='dance']")); 
            Assert.IsFalse(the_checkbox.Selected);
            the_checkbox.Click(); 
            Assert.IsTrue(the_checkbox.Selected);

            IWebElement Phone = new Common(_driver).FindElementWithDynamicWait(_driver, By.XPath("//input[@name='phone_9']"), "'Phone Number' textbox on DemoQa Registration page.");
            IWebElement UName = new Common(_driver).FindElementWithDynamicWait(_driver, By.XPath("//input[@name='username']"), "'Username' textbox on DemoQa Registration page.");
            IWebElement Email = new Common(_driver).FindElementWithDynamicWait(_driver, By.XPath("//input[@id='email_1']"), "'Email' textbox on DemoQa Registration page.");
            IWebElement About = new Common(_driver).FindElementWithDynamicWait(_driver, By.XPath("//textarea[@id='description']"), "'About' textbox on DemoQa Registration page.");
            IWebElement Password = new Common(_driver).FindElementWithDynamicWait(_driver, By.XPath("//input[@id='password_2']"), "'Password' textbox on DemoQa Registration page.");
            IWebElement ConfirmPassword = new Common(_driver).FindElementWithDynamicWait(_driver, By.XPath("//input[@id='confirm_password_password_2']"), "'Confirm Password' textbox on DemoQa Registration page.");

            Common.enterText(FName, fname);
            Common.enterText(LName, lname);
            Common.enterText(Phone, phone);
            Common.enterText(UName, uname);
            Common.enterText(Email, email);
            Common.enterText(About, about);
            Common.enterText(Password, password);
            Common.enterText(ConfirmPassword, Confirmpassword);

           }
       
    }
}
