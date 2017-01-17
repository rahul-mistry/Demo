using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Init;
using NUnit.Framework;
using TestCases;
using System.IO;
using System.Text.RegularExpressions;

namespace TestCases
{
    [TestFixture]

    public class TestSuites
    {

        static bool IsTestFinished;
        static Int32 intLoginPassCnt = 0;
        static Int32 intLoginFailCnt = 0;
        static Int32 intLoginWarningCnt = 0;

        public TestSuites()
        {
            Report.sbHtml = null;
            Report.sbSummaryHtml = null;
            IsTestFinished = true;
            intLoginPassCnt = 0;
            intLoginFailCnt = 0;

            Report.TCcnt = 1;
            Report.IsPassed = 0;


            Home.IsTcAdded = false;
            Report.IsHeaderUpdated = false;
            Report.sbTcHtml = null;
            Report.sbFeatureHtml = null;

        }
        
        [TestFixtureSetUp]
        public void Init()
        {
            DateTime strDtTm = DateTime.Now;
            Report.strPath = AppDomain.CurrentDomain.BaseDirectory + "\\Report\\" + strDtTm.ToString("ddMMMyyyy_HH-mm") + "\\";
        }

        #region "DemoQa"

      [Test]

        public void Demo_MouseHover()
        {
            try
            {
                waitForPrevTestToFinish();
                IsTestFinished = false;
                Home objHome = new Home();
                objHome.Demo_MouseHover();
            }
            finally
            {
                Report.AddToHtmlReportFeatureFinish();
                Report.GenerateHtmlReport();
                IsTestFinished = true;
                if (Report.IsFtrPassed == 1) intLoginPassCnt++;
                else if (Report.IsFtrPassed == 2) intLoginFailCnt++;
                else if (Report.IsFtrPassed == 3) intLoginWarningCnt++;
                Home.IsTcAdded = true;

            }

        }

        #endregion

         [TestFixtureTearDown]
        public void zzzGenerateSummaryReport()
        {

            //Pass
            string text1 = File.ReadAllText(@"C:\Users\HP-PC\Desktop\Demo1\bin\Debug\Report\pages\Automation Dashboard.html");
            string input1 = text1;
            string pattern1 = @"var Pass = (\d+)";
            Regex rex1 = new Regex(pattern1);
            string rersult1 = rex1.Replace(input1, "var Pass = " + intLoginPassCnt);
            File.WriteAllText(@"C:\Users\HP-PC\Desktop\Demo1\bin\Debug\Report\pages\Automation Dashboard.html", rersult1);

            //Fail
            string text2 = File.ReadAllText(@"C:\Users\HP-PC\Desktop\Demo1\bin\Debug\Report\pages\Automation Dashboard.html");
            string input2 = text2;
            string pattern2 = @"var Fail = (\d+)";
            Regex rex2 = new Regex(pattern2);
            string rersult2 = rex2.Replace(input2, "var Fail = " + intLoginFailCnt);
            File.WriteAllText(@"C:\Users\HP-PC\Desktop\Demo1\bin\Debug\Report\pages\Automation Dashboard.html", rersult2);

            //Not Tested
            string text3 = File.ReadAllText(@"C:\Users\HP-PC\Desktop\Demo1\bin\Debug\Report\pages\Automation Dashboard.html");
            string input3 = text3;
            string pattern3 = @"var NotTested = (\d+)";
            Regex rex3 = new Regex(pattern3);
            string rersult3 = rex3.Replace(input3, "var NotTested = " + intLoginWarningCnt);
            File.WriteAllText(@"C:\Users\HP-PC\Desktop\Demo1\bin\Debug\Report\pages\Automation Dashboard.html", rersult3);

            
            try
            {
                Report.AddToHtmlSummaryReport("Gmail Test Cases", intLoginPassCnt, intLoginFailCnt, intLoginWarningCnt);
                Report.AddToHtmlSummaryReportTotal();
                Report.GenerateHtmlSummaryReport();
            }
            finally
            {
                Report._intPassedCnt = 0;
                Report._intFailedCnt = 0;
                Report._inTotalCnt = 0;
            }
        }

        private void waitForPrevTestToFinish()
        {
            while (true)
            {
                if (!IsTestFinished) Common.pauseStatic(2000);
                else break;
            }
        }
    }




}
