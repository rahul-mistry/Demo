using System;
using System.IO;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;


namespace Framework.Init
{
    public class Report
    {
        public static StringBuilder sbHtml = null;
        public static StringBuilder sbSummaryHtml = null;
        public static StringBuilder sbTcHtml = null;
        public static StringBuilder sbFeatureHtml = null;
        public static int TCcnt = 1;
        public static int intFeatureCnt = 1;
        public static Int16 IsPassed = 0;
        public static Int16 IsFtrPassed = 0;
        public static bool IsHeaderUpdated = false;
        public static int _intPassedCnt = 0;
        public static int _intFailedCnt = 0;
        public static int _intWarningCnt = 0;
        public static int _inTotalCnt = 0;
        /////public static DateTime strDtTm = DateTime.Now;
        public static int _intVerificationPassCnt = 0;
        public static int _intVerificationWarningCnt = 0;
        public static int _intVerificationFailCnt = 0;
        /////public static string strPath = AppDomain.CurrentDomain.BaseDirectory + "\\Report\\" + strDtTm.ToString("ddMMMyyyy_HH-mm") + "\\";
        public static string strPath;

        //public Report()
        //{
        //    Console.WriteLine("Rpt Cons Called");
        //    sbHtml = null;
        //    sbSummaryHtml = null;
        //    TCcnt = 1;
        //    IsPassed = true;
        //}

        public static void AddToHtmlReportTCHeader(string strLine)
        {
            if (sbHtml == null) sbHtml = new StringBuilder();
            //sbHtml.Append("<h2 style='text-decoration:underline;'>TC" + TCcnt + ":: " + strLine + "</h2>");
            sbHtml.Append("<h2 style='text-decoration:underline;'>::" + strLine + "</h2>");
            TCcnt++;
            IsPassed = 0;
            IsHeaderUpdated = false;
            intFeatureCnt = 1;
            IsFtrPassed = 0;
            _intVerificationPassCnt = 0;
            _intVerificationWarningCnt = 0;
            _intVerificationFailCnt = 0;
            //Console.WriteLine("1TCcnt - 1 :" + (TCcnt - 1));
            /////strDtTm = DateTime.Now;
            /////strPath = AppDomain.CurrentDomain.BaseDirectory + "\\Report\\" + strDtTm.ToString("ddMMMyyyy_HH-mm") + "\\";
        }
        public static void AddToHtmlReportPassed(string strLine, bool IsItalic = false)
        {
            if (sbHtml == null) sbHtml = new StringBuilder();
           // sbHtml.Append("<div class='passed'> " + Common.highlightQuotedHTML(strLine) + " <span class='VrTimeStmp' > (" + DateTime.Now.ToString("HH:mm:ss.fff") + ")</span></div><br>");
            sbHtml.Append("<div class='passed'>"+strLine+" <span class='VrTimeStmp' > (" + DateTime.Now.ToString("HH:mm:ss.fff") + ")</span></div><br>");
           
            if (IsPassed != 2 && IsPassed != 3) IsPassed = 1;
            if (IsFtrPassed != 2 && IsFtrPassed != 3) IsFtrPassed = 1;
            _intVerificationPassCnt++;
            //Console.WriteLine("2TCcnt - 1 :" + (TCcnt - 1));
        }
        public static void AddToHtmlReportOrange(string strLine, bool IsItalic = false)
        {
            if (sbHtml == null) sbHtml = new StringBuilder();
            //sbHtml.Append("<div class='orange'> " + Common.highlightQuotedHTML(strLine) + " <span class='VrTimeStmp' > (" + DateTime.Now.ToString("HH:mm:ss.fff") + ")</span></div><br>");
            sbHtml.Append("<div class='orange'>"+strLine+" <span class='VrTimeStmp' > (" + DateTime.Now.ToString("HH:mm:ss.fff") + ")</span></div><br>");
          
            if (IsPassed != 2) IsPassed = 3;
            if (IsFtrPassed != 2) IsFtrPassed = 3;
            _intVerificationWarningCnt++;
        }
        public static void AddToHtmlReportFailed(IWebDriver driver, Exception ex, string strLine, bool IsItalic = false)
        {
            if (sbHtml == null) sbHtml = new StringBuilder();
            sbHtml.Append("<table cellpadding='2' cellspacing='0'><tr><td style='width:5%;white-space: nowrap;' >");
            //sbHtml.Append("<div class='failed'> " + Common.highlightQuotedHTML(strLine) + " <span class='VrTimeStmp' > (" + DateTime.Now.ToString("HH:mm:ss.fff") + ")</span></div>");
            sbHtml.Append("<div class='failed'>"+strLine+"<span class='VrTimeStmp' > (" + DateTime.Now.ToString("HH:mm:ss.fff") + ")</span></div>");
            

            sbHtml.Append("</td><td>");
            sbHtml.Append("<div class='dvSnap'> [<a href='" + GetScreenShot(driver) + "' target='_blank'> <strong>View Snap</strong> </a>] </div>");
            sbHtml.Append("</td></tr></table>");
            sbHtml.Append("<div class='errTrace' >Error Message: " + ex.Message + "</div>");
            IsPassed = 2;
            IsFtrPassed = 2;
            _intVerificationFailCnt++;
            //Console.WriteLine("3TCcnt - 1 :" + (TCcnt - 1));
        }
        public static void AddToHtmlReportFeatureFinish()
        {
            try
            {
                if (sbHtml == null) sbHtml = new StringBuilder();
                sbHtml.Append("</div></div></div>");

                //Console.WriteLine("4TCcnt - 1 :" + (TCcnt - 1));
                /*if (IsPassed == 1)
                {
                    //Console.WriteLine("1111111111111");
                    if (!sbHtml.ToString().Contains("<div id='TC" + (TCcnt - 1) + "' class='passed'"))
                    {
                        //Console.WriteLine("88888888888888");
                        sbHtml.Insert(((sbHtml.ToString().IndexOf("::"))), "<div id='TC" + (TCcnt - 1) + "' class='passed' style='float:left; height:16px; margin-top:2px;' ></div>");
                    }
                    
                    IsHeaderUpdated = true;
                }
                else if (IsPassed == 2)
                {
                    //Console.WriteLine("7777777777777");

                    IsHeaderUpdated = true;
                    if (sbHtml.ToString().Contains("<div id='TC" + (TCcnt - 1) + "' class='passed'"))
                    {
                        //Console.WriteLine("22222222222222");
                        sbHtml.Replace("<div id='TC" + (TCcnt - 1) + "' class='passed'", "<div id='TC" + (TCcnt - 1) + "' class='failed'");
                    }
                    else if (sbHtml.ToString().Contains("<div id='TC" + (TCcnt - 1) + "' class='failed'"))
                    {
                        //Console.WriteLine("333333333333333");
                        sbHtml.Replace("<div id='TC" + (TCcnt - 1) + "' class='passed'", "<div id='TC" + (TCcnt - 1) + "' class='failed'");
                    }
                    else sbHtml.Insert(((sbHtml.ToString().IndexOf("::"))), "<div id='TC" + (TCcnt - 1) + "' class='failed' style='float:left; height:16px; margin-top:2px;' ></div>");
                    //Console.WriteLine("66666666666666");


                }*/
                if (IsFtrPassed == 1)
                {
                    //Console.WriteLine("4444444444444444");
                    sbHtml.Replace("<div id='dvFeature" + (intFeatureCnt - 1) + "TC" + (TCcnt - 1) + "' class='squareboxcaption'", "<div id='dvFeature" + (intFeatureCnt - 1) + "TC" + (TCcnt - 1) + "' class='squareboxcaption squareboxcaptionP'");
                    sbHtml.Replace("(())", "");
                }
                else if (IsFtrPassed == 2)
                {
                    //Console.WriteLine("5555555555555555");
                    sbHtml.Replace("<div id='dvFeature" + (intFeatureCnt - 1) + "TC" + (TCcnt - 1) + "' class='squareboxcaption'", "<div id='dvFeature" + (intFeatureCnt - 1) + "TC" + (TCcnt - 1) + "' class='squareboxcaption squareboxcaptionF'");
                    sbHtml.Replace("(())", "(" + _intVerificationFailCnt+ ")");
                }
                else if (IsFtrPassed == 3)
                {
                    // Console.WriteLine("5555555555555555");
                    sbHtml.Replace("<div id='dvFeature" + (intFeatureCnt - 1) + "TC" + (TCcnt - 1) + "' class='squareboxcaption'", "<div id='dvFeature" + (intFeatureCnt - 1) + "TC" + (TCcnt - 1) + "' class='squareboxcaption squareboxcaptionO'");
                    sbHtml.Replace("(())", "(" + _intVerificationWarningCnt+ ")");
                }
                _intVerificationPassCnt = 0;
                _intVerificationWarningCnt = 0;
                _intVerificationFailCnt = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
            
        }

        public static void AddToHtmlReport(string strLine, bool IsHeader = false, bool IsBold = false, bool IsUnderline = false, bool IsItalic = false)
        {
            if (sbHtml == null) sbHtml = new StringBuilder();
            string strHtml = string.Empty;
            if (IsHeader)
            {
                strHtml += "<div style='clear:both; '>";
                strHtml += "<div class='squarebox'>";
                strHtml += "<div id='dvFeature" + intFeatureCnt + "TC" + (TCcnt-1)+ "' class='squareboxcaption' onclick='togglePannelStatus(this.nextSibling)'>";
                strHtml += "<div style='padding: 10px 10px 10px 5px; float: left;'>";
                strHtml += "";
                strHtml += "<strong>" + strLine + "</strong> <span class='VrTimeStmp' > (())</span><br>";
                strHtml += "</div>";
                strHtml += "<div class='upDownArrow' >";
                strHtml += "<img src='../images/expand.gif' width='13' height='14' border='0' alt='Show/Hide' title='Show/Hide' /> </div>";
                strHtml += "</div>";
                strHtml += "<div class='squareboxcontent' style='display: none;clear:both;'>";
                sbHtml.Append(strHtml + "<br>");
                intFeatureCnt++;
                IsFtrPassed = 0;
            }
            else
            {
                if (IsBold)
                {
                    strLine = "<strong>" + strLine + "</strong>";
                }
                if (IsUnderline)
                {
                    strLine = "<u>" + strLine + "</u>";
                }
                if (IsItalic)
                {
                    strLine = "<i>" + strLine + "</i>";
                }
                sbHtml.Append(strLine + "<br>");
            }

        }
        private static readonly object syncLock = new object();
        public static void GenerateHtmlReport(string strReportHeader="")
        {
            
            lock (syncLock)
            {
                //string strPath = AppDomain.CurrentDomain.BaseDirectory + "\\Report\\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }

                //strPath = strPath + "\\" + strDtTmStamp + "\\";
                //Directory.CreateDirectory(strPath);
                
                string strFilePath = strPath + "Report.html";

                StringBuilder lines = new StringBuilder();
                System.IO.StreamWriter file = new System.IO.StreamWriter(strFilePath);
                file.Write("<html><head><title>Test Report</title><style type='text/css'> body { background-color:#C0C0C0; color:#3D3D3D; font-size: 12px; font-family:Arial, Courier; padding-left:7px;} h2{margin:23px 0px 4px 0px; font-weight:normal;} hr{border:1px solid lightgray;} table {font-size: 12px; font-family:Arial, Courier; } .dvSnap{margin: -4px 0px -8px -2px; padding: 5px 0px 0px 20px;} .passed {color: #006600; font-weight:normal; text-decoration:none; margin: -1px 0px -8px -2px; padding: 5px 0px 0px 20px; background: url('../Images/passed.png') no-repeat top left; } .orange {color: #FFBB8E; font-weight:normal; text-decoration:none; margin: -1px 0px -8px -2px; padding: 5px 0px 0px 20px; background: url('../Images/warning.png') no-repeat top left; } .failed { color: #CC0000; font-weight:normal; text-decoration:none; margin: -1px 0px -8px -2px; padding: 5px 0px 0px 20px; background: url('../Images/failed.png') no-repeat top left;} a{text-decoration:none} a:hover{text-decoration:underline;} .errTrace {font: 12px serif; padding:5px; margin: 10px 0px 0px 5px; color: #AE0000; background-color: #FFEFC6; border:dashed 1px #AE0000; border-radius: 4px;} .clientHeaderLogo{background: url('../Images/ExpertoryLogo.png') no-repeat top left; width:40%;} .legendPassed{width:20px; background: url('../Images/passed.png') no-repeat top left;} .legendFailed{width:20px; background: url('../Images/failed.png') no-repeat top left;} .legendOrange{width:20px; background: url('../Images/warning.png') no-repeat top left;} .legendInfo{width:20px; background: url('../Images/info.png') no-repeat top left;} .squarebox {width: 100%; overflow: hidden; } .squareboxcaption { margin-top: 5px; margin-left: 5px; float: left; cursor: pointer;} .squareboxcaptionP {background-color: #D2EFD1; border: 1px solid #6F9F6D; } .squareboxcaptionO {background-color: #FA910D; border: 1px solid #AB4302;}  .squareboxcaptionF {background-color: #FFCFCF; border: 1px solid #CF3333;} .squareboxcontent { border:1px solid lightgray; margin-left:5px; padding: 0px 10px; overflow: hidden; } .upDownArrow{float: left; padding: 10px 10px 11px 10px; vertical-align: middle; border-left: 1px dotted #FFFFFF;} iframe{border-top:0px none; border-right:0px none; border-bottom:1px solid gray; border-left:0px none;overflow:auto; display:block; visibility:visible; margin-bottom: 10px; height:200px; width: 99%;} .dvRptFrame{ background-color: #FFFFFF; border: 1px solid gray; border-radius:10px 10px 10px 10px; margin: 2px 15px 30px 15px; padding: 10px; overflow:auto;min-height:auto; } .dvRptHeader{ height:85px; background-color: #FFFFFF; border: 1px solid gray; border-radius:10px 10px 10px 10px; margin: 10px 15px 2px 15px; padding: 5px 15px; } .dvKiwiQaLogo{ background: url('data:image/png;base64,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%3D%3D') no-repeat top left; height:85px; width:63%; text-align:right; font-size: 18px; float:left; } .dvClientLogo{ background: url('../Images/ClientLogo.png') no-repeat left; height:85px; width:193px; float:right; } .VrTimeStmp{color:#696969;font-size:10px;font-style:italic;font-weight:normal;} </style> <script language='javascript' type='text/javascript'> function togglePannelStatus(content) { var expand = (content.style.display == 'none'); content.style.display = (expand ? 'block' : 'none'); var chevron = content.parentNode .firstChild.childNodes[1].childNodes[0]; chevron.src = chevron.src .split(expand ? 'expand.gif' : 'collapse.gif') .join(expand ? 'collapse.gif' : 'expand.gif'); } </script></head><body>");

                file.Write("<div class='dvRptHeader'><div class='dvKiwiQaLogo' ><h1>KiwiQA Automation Test Report</h1></div><div class='dvClientLogo' ></div></div>");
                file.Write("<div class='dvRptFrame'>");

                file.Write("<iframe src='summary.html'>&nbsp;</iframe> ");
                file.Write("<h2 style='text-align:center; border-bottom: 1px dotted lightgray;font-weight:bold;' >Test Result Details</h2> <div style='border:2px solid lightgray; border-radius: 5px; text-align:left; width:230px; padding:3px; margin-right:20px; float:right;'><table cellpadding='3' cellspacing='0'><tr><td class='legendPassed'></td><td>Passed</td><td class='legendOrange'></td><td rowspan='3'>Warning <br> - No records <br> - Session expired <br> - Access denied <br> - Server error</td></tr><tr><td class='legendFailed'></td><td>Failed</td></tr><tr><td class='legendInfo'></td><td>Information</td></tr></table></div><div style='float:left;'><b> &nbsp;Date:</b> " + String.Format("{0:dd/MM/yyyy}", DateTime.Now) + " " + String.Format("{0:HH:mm:ss}", DateTime.Now) + "</div><br style='clear:left;' />");
                //file.Write("<h2>" + strReportHeader + "<br>");
                //file.Write("------------------------------------------------------------- </h2>");
                file.WriteLine(sbHtml.ToString());
                file.WriteLine("<br style='clear:both;'><br><br><hr><div style='text-align:center; font-style:italic' >***** Report Ends Here *****</div><br><br>");
                file.WriteLine("</div></body></html>");
                file.Close(); 
            }
        }
        public static String NewGuid()
        {
            return System.Guid.NewGuid().ToString().Replace("-", "");
        }
        public static String NewFileName()
        {
            return NewGuid() + ".png";
        }
        public static string GetScreenShot(IWebDriver driver)
        {
            /*string retVal = NewFileName();
            try
            {
                //string strPath = AppDomain.CurrentDomain.BaseDirectory + "\\Report";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                
                //strPath = strPath + "\\" + strDtTmStamp + "\\";
                //Directory.CreateDirectory(strPath);
                ss.SaveAsFile(strPath + "\\" + retVal, System.Drawing.Imaging.ImageFormat.Png);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.StackTrace);
            } 
            return retVal;*/

            
            /*Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            bitmap.Save("c:\\screenshot.jpeg", ImageFormat.Jpeg);*/

            /*ScreenCapture sc = new ScreenCapture();
            // capture entire screen, and save it to a file
            Image img = sc.CaptureScreen();
            // display image in a Picture control named imageDisplay
            this.imageDisplay.Image = img;
            // capture this window, and save it
            sc.CaptureWindowToFile(this.Handle, "C:\\temp2.gif", ImageFormat.Gif);*/

            string retVal = NewFileName();
            try
            {
                Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                // Create a graphics object from the bitmap.
                Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot);
                // Take the screenshot from the upper left corner to the right bottom corner.
                gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                // Save the screenshot to the specified path that the user has chosen.
                
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                bmpScreenshot.Save(strPath + "\\" + retVal, ImageFormat.Png);
            }
            catch (Exception)
            {

            }
            return retVal;
        }

        public static void GenerateHtmlSummaryReport()
        {
            lock (syncLock)
            {
                //string strPath = AppDomain.CurrentDomain.BaseDirectory + "\\Report\\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }

                //strPath = strPath + "\\" + strDtTmStamp + "\\";
                //Directory.CreateDirectory(strPath);

                string strFilePath = strPath + "summary.html";

                StringBuilder lines = new StringBuilder();
                System.IO.StreamWriter file = new System.IO.StreamWriter(strFilePath);
                file.Write("<html><head><title>Test Report</title><style type='text/css'> body { color:#3D3D3D; font-size: 12px; font-family:Arial, Courier; } h2{margin:5px 0px 0px 0px;} hr{border:1px solid lightgray;} table {font-size: 12px; font-family:Arial, Courier; } .passed {color: #006600; font-weight:normal; text-decoration:none; margin: -1px 0px -8px -2px; padding: 5px 0px 0px 20px; background: url('../Images/passed.png') no-repeat top left; } .orange {color: #006600; font-weight:normal; text-decoration:none; margin: -1px 0px -8px -2px; padding: 5px 0px 0px 20px; background: url('../Images/passed.png') no-repeat top left; } .failed { color: #CC0000; font-weight:bold; text-decoration:none; margin: -1px 0px -8px -2px; padding: 5px 0px 0px 20px; background: url('../Images/failed.png') no-repeat top left;} a{text-decoration:none} a:hover{text-decoration:underline;} .tblSummary{border:2px solid lightgray; border-radius: 5px 5px 5px 5px} .tblSummary th{border-bottom:1px solid lightgray; background-color: silver;} .tdSummary{ text-align:right; padding-right:15px; width:100px;} .tdBleft{border-left:1px solid lightgray; } .tdBbottom{border-bottom:1px solid lightgray;}</style> </head><body><div style='text-align: center;'><h2>Test Result Summary</h2>");
                file.Write("<table cellpadding='4' cellspacing='0' class='tblSummary' align='center' >");
                file.Write("<tr><th style='width:250px;' class='tdBbottom' >Test Name</th><th class='tdBleft tdBbottom' > Passed</th><th class='tdBleft tdBbottom'> Failed </th><th class='tdBleft tdBbottom' > Warning</th><th class='tdBleft tdBbottom'> Total </th></tr>");
                file.WriteLine(sbSummaryHtml.ToString());
                file.Write("</table>");
                file.WriteLine("</body></html>");
                file.Close(); 
            }
        }

        public static void AddToHtmlSummaryReport(string strTestName, int intPassedCnt = 0, int intFailedCnt = 0, int intWarningCnt = 0)
        {
            if (sbSummaryHtml == null) sbSummaryHtml = new StringBuilder();
            if (intPassedCnt > 0 || intFailedCnt > 0 || intWarningCnt > 0)
            {
                _intPassedCnt += intPassedCnt;
                _intFailedCnt += intFailedCnt;
                _intWarningCnt += intWarningCnt;
                _inTotalCnt += (intPassedCnt + intFailedCnt + intWarningCnt);
                sbSummaryHtml.Append("<tr><td class='tdBbottom' >");
                sbSummaryHtml.Append(strTestName);
                sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' >");
                sbSummaryHtml.Append(intPassedCnt);
                sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' >");
                sbSummaryHtml.Append(intFailedCnt);
                sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' >");
                sbSummaryHtml.Append(intWarningCnt);
                sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' style='font-weight:bold;'>");
                sbSummaryHtml.Append(intPassedCnt + intFailedCnt + intWarningCnt);
                sbSummaryHtml.Append("</td></tr>");
            }
            
        }

        public static void AddToHtmlSummaryReportTotal()
        {
            sbSummaryHtml.Append("<tr><td class='tdBbottom' style='font-weight:bold;' >");
            sbSummaryHtml.Append("Total");
            sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' style='font-weight:bold;'>");
            sbSummaryHtml.Append(_intPassedCnt);
            sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' style='font-weight:bold;'>");
            sbSummaryHtml.Append(_intFailedCnt);
            sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' style='font-weight:bold;'>");
            sbSummaryHtml.Append(_intWarningCnt);
            sbSummaryHtml.Append("</td><td class='tdSummary tdBleft tdBbottom' style='font-weight:bold;'>");
            sbSummaryHtml.Append(_inTotalCnt);
            sbSummaryHtml.Append("</td></tr>");
        }

    }

    public class ScreenShotRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        public ScreenShotRemoteWebDriver(Uri RemoteAdress, ICapabilities capabilities)
            : base(RemoteAdress, capabilities)
        {
        }

        /// <summary>
        /// Gets a <see cref="Screenshot"/> object representing the image of the page on the screen.
        /// </summary>
        /// <returns>A <see cref="Screenshot"/> object containing the image.</returns>
        public Screenshot GetScreenshot()
        {
            // Get the screenshot as base64.
            Response screenshotResponse = this.Execute(DriverCommand.Screenshot, null);
            string base64 = screenshotResponse.Value.ToString();

            // ... and convert it.
            return new Screenshot(base64);
        }
    }

    //###########################################################################################################################################################
    /*
    public class ScreenCapture
    {
        /// <summary>
        /// Creates an Image object containing a screen shot of the entire desktop
        /// </summary>
        /// <returns></returns>
        public Image CaptureScreen()
        {
            return CaptureWindow(User32.GetDesktopWindow());
        }
        /// <summary>
        /// Creates an Image object containing a screen shot of a specific window
        /// </summary>
        /// <param name="handle">The handle to the window. (In windows forms, this is obtained by the Handle property)</param>
        /// <returns></returns>
        public Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up 
            GDI32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);
            return img;
        }
        /// <summary>
        /// Captures a screen shot of a specific window, and saves it to a file
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);
        }
        /// <summary>
        /// Captures a screen shot of the entire desktop, and saves it to a file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        public void CaptureScreenToFile(string filename, ImageFormat format)
        {
            Image img = CaptureScreen();
            img.Save(filename, format);
        }

        /// <summary>
        /// Helper class containing Gdi32 API functions
        /// </summary>
        private class GDI32
        {

            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);
            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);
            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);
            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }
    }
    */
}
