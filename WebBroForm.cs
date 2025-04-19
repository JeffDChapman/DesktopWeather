using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System;
using System.Linq;

namespace HeadlessBrowser
{
    public partial class WebBroForm : Form
    {
        #region Private Variables
        private bool allowScripts = false;
        private bool stopClick = false;
        private bool isSpying;
        private int navLoopCount = 0;
        private string histPath = @"wBhist.txt";
        private string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        private bool internalRedirect;
        private bool hadRecovery;
        private string offLineFile = "nothing.htm";
        private bool atHome;
        private bool naviErr;
        private string saveOldPage = "";
        private bool docTooShort;
        private string chosenFont = "Candara";
        private string chosenSize = "24";
        private bool stopPopUps = false;
        private bool intRptdFlag = false;
        private bool ctrlNavigated = false;
        private bool googleFailed = false;
        private bool isAsearch;
        private string currentSearchEng = "bing";
        private string searchEngines = "bing;google;duckduckgo;metasearx;mojeek";
        private string searchNeedsSearch = "1,1,0,0,1";
        string[] sEngList;
        string[] sNsList;
        private int sEngIndex;
        #endregion

        public System.Windows.Forms.WebBrowser myBrowser;
        public System.Windows.Forms.TextBox myAddrBar;
        public string passedStartDoc = "";
        public string CurrentStatus = "Ready";
        public string webPageTitle;

        private System.Windows.Forms.Timer tmrPopUps;
        private System.Windows.Forms.Timer tmrReroute;
        private System.Windows.Forms.Timer tmrNavDone;
        private DesktopWeather.weatherForm myParent;

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        //--------- system fired events ---------//

        public WebBroForm(DesktopWeather.weatherForm UIparent)
        {
            InitializeFormElements();
            myParent = UIparent;
        }

        public WebBroForm(DesktopWeather.weatherForm UIparent, string startDoc)
        {
            InitializeFormElements();
            passedStartDoc = startDoc;
            myParent = UIparent;
        }

        private void InitializeFormElements()
        {
            myBrowser = new WebBrowser();
            tmrPopUps = new System.Windows.Forms.Timer();
            tmrReroute = new System.Windows.Forms.Timer();
            tmrNavDone = new System.Windows.Forms.Timer();
            myAddrBar = new System.Windows.Forms.TextBox();

            tmrPopUps.Interval = 10000;
            tmrPopUps.Tick += tmrPopUps_Tick;
            tmrReroute.Interval = 1000;
            tmrReroute.Tick += tmrReroute_Tick;
            tmrNavDone.Interval = 2000;
            tmrNavDone.Tick += tmrNavDone_Tick;

            myBrowser.DocumentCompleted += myBrowser_DocumentCompleted;
            myBrowser.Navigated += myBrowser_Navigated;
            myBrowser.Navigating += myBrowser_Navigating;
            myBrowser.ScriptErrorsSuppressed = true;

            webPageTitle = "";
            WebBroForm_Load(this, null);
        }

        private void WebBroForm_Load(object sender, EventArgs e)
        {
            sEngList = searchEngines.Split(new char[] { ';' });
            sEngIndex = sEngList.ToList().IndexOf(currentSearchEng);
            sNsList = searchNeedsSearch.Split(new char[] { ',' });

            int findWL = strExeFilePath.IndexOf("DesktopWeather", strExeFilePath.Length - 25);
            strExeFilePath = strExeFilePath.Substring(0, findWL);
            internalRedirect = false;

            if (passedStartDoc != "")
                { this.myBrowser.Navigate(passedStartDoc); }
        }

        private void myBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            naviErr = false;
            if (stopClick == true) { return; }
            ctrlNavigated = false;
            if (Control.ModifierKeys == Keys.Control) { ctrlNavigated = true; }

            string reDirLoc = e.Url.ToString();
            if (reDirLoc == "about:blank")
            {
                naviErr = true;
                string reDirect = FixAboutUrl(reDirLoc);
                reDirLoc = FixDoubleSlash(reDirect);
                if (!stopPopUps)
                { StartnewForm(reDirLoc); }
                e.Cancel = true;
                return;
            }

            bool regularNonHomeClick = false;
            if ((internalRedirect) && (!ctrlNavigated) && (!atHome)) { regularNonHomeClick = true; }

            if ((stopPopUps) && (regularNonHomeClick))
            {
                e.Cancel = true;
                return;
            }

            if ((regularNonHomeClick) && (Control.ModifierKeys != Keys.Shift))
            {
                string reDirect = FixAboutUrl(reDirLoc);
                reDirect = FixDoubleSlash(reDirect);
                StartnewForm(reDirect);
                e.Cancel = true;
                return;
            }

            if ((ctrlNavigated) || (atHome))
            { ResetOfflineCkbox(); }
            CurrentStatus = "Navigating...";
            stopPopUps = true;
            navLoopCount++;
            if (navLoopCount > 10)
            {
                CurrentStatus = "Loop Count Exceeded...";
                PoshPageBrackets();
                navLoopCount = 0;
                return;
            }

            string newRouteTo = FixAboutUrl(reDirLoc);
            string newRouteTo2 = FixDoubleSlash(newRouteTo);
            myAddrBar.Text = fixURLspellings(newRouteTo2);
        }

        private void myBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (naviErr) { return; }
            CurrentStatus = "Navigation Done";

            if (stopClick)
            {
                CleanHTML();
                return;
            }

            tmrNavDone.Enabled = true;

            if (!isSpying)
            { return; }

            tmrNavDone.Enabled = false;
            PoshPageBrackets();
        }

        private void myBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            FinishWithTheDocument(e);
            myParent.BrowserIsFinished();
        }

        //--------- internal subroutines ---------//

        private void FinishWithTheDocument(WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                if (myBrowser.Document.Body.InnerText.Contains("Navigation to the webpage was canceled"))
                {
                    this.myBrowser.Navigate("about:blank");
                    SetupEndFlagging();
                    return;
                }
            }
            catch
            {
                this.myBrowser.Navigate("about:blank");
                SetupEndFlagging();
                CurrentStatus = "Empty Document";
                return;
            }
            if ((e == null) && (!intRptdFlag)) { return; }
            if (isSpying)
            { return; }

            if (hadRecovery) { return; }
            if ((naviErr) && (!intRptdFlag)) { return; }

            docTooShort = false;
            CheckShortDoc(myAddrBar.Text, 2);
            if ((docTooShort) && (ctrlNavigated))
            {
                CurrentStatus = "Delay reroute ...";
                tmrReroute.Enabled = true;
                tmrNavDone.Enabled = false;
                return;
            }
            if (docTooShort) { return; }

            CleanHTML();
        }

        private void CheckShortDoc(string GoToUrl, int Occurrence)
        {
            if (myBrowser.DocumentText.Length < 200)
            {
                docTooShort = true;
                if (!stopPopUps)
                { StartnewFormWseed(GoToUrl, myBrowser.DocumentText); }
                myBrowser.DocumentText = saveOldPage;
                if ((ctrlNavigated) && (Occurrence == 2)) { return; }
                string boxMsg = "Load Delayed at Server (" + Occurrence.ToString() + ")"
                    + "\nPage Script still Enabled!";
                MessageBox.Show(boxMsg);
                SetupEndFlagging();
            }
        }

        private void CleanHTML()
        {
            if (hadRecovery) { return; }
            if ((naviErr) && (!intRptdFlag)) { return; }
            hadRecovery = false;
            CurrentStatus = "Closing sockets...";
            try { CloseAllSocks(); }
            catch { }
            string pageBodyMod = "";
            CurrentStatus = "Code Replacement in process...";
            HtmlDocument fixDoc = myBrowser.Document;
            string recovD = tryRecovery(myBrowser.DocumentStream);
            int foundTitle = recovD.ToLower().IndexOf("<title");
            bool badLoad = (fixDoc.Body == null) || (fixDoc.Body.InnerHtml == null);

            if (badLoad)
            {
                hadRecovery = true;
                try { pageBodyMod = recovD.Substring(foundTitle); }
                catch { pageBodyMod = recovD; }
                SaveFileOffline(pageBodyMod, recovD, foundTitle);
            }
            else
            {
                pageBodyMod = fixDoc.Body.InnerHtml.ToString();
            }

            pageBodyMod = RemoveScriptCode(pageBodyMod, "HEAD");
            pageBodyMod = RemoveScriptCode(pageBodyMod, "head");

            string StopRecur = pageBodyMod.Substring(1, 4).ToLower();
            if (StopRecur == "font")
            {
                CurrentStatus = "Ready";
                internalRedirect = true;
                tmrPopUps.Enabled = true;
                return;
            }

            docTooShort = false;
            CheckShortDoc(fixDoc.Url.ToString(), 1);
            if (docTooShort) { return; }

            string checkGoogle = pageBodyMod.ToLower();
            string searchFail = "trouble accessing Google Search";
            if (checkGoogle.IndexOf(searchFail.ToLower()) > -1) { googleFailed = true; }

            string pageBodyModshow = MakeFinalAdjustments(ref pageBodyMod);
            webPageTitle = myBrowser.DocumentTitle;
            myBrowser.Document.OpenNew(false);
            myBrowser.Document.Write(pageBodyModshow);

            CurrentStatus = "Closing sockets again...";
            try { CloseAllSocks(); }
            catch { }

            SetupEndFlagging();
        }

        private static void CloseAllSocks()
        {
            int nProcessID = Process.GetCurrentProcess().Id;

            var p = new Process
            {
                StartInfo = { FileName = @"cmd.exe",
                    Arguments = "/C netstat -a -n -o >activeSocks.txt", UseShellExecute = false,
                    CreateNoWindow = true}
            };
            p.Start();
            p.WaitForExit();
            p.Close();

            var lines = File.ReadLines("activeSocks.txt");
            foreach (var line in lines)
            {
                string[] sockParms = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (sockParms.Length < 5) { continue; }
                if (sockParms[4] != nProcessID.ToString()) { continue; }
                string myEndPoint = sockParms[1];
                string[] myIPsplit = myEndPoint.Split(new char[] { ':' });
                Socket aSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);

                int myPort = 8080;
                if (myIPsplit.Length > 0) { myPort = Convert.ToInt32(myIPsplit[myIPsplit.Length - 1]); }
                var myEP = new IPEndPoint(IPAddress.Parse(myEndPoint), myPort);
                TcpClient tcpClient = new TcpClient(myEP);
                aSocket.Bind(myEP);
                try
                { aSocket.Shutdown(SocketShutdown.Both); }
                finally
                {
                    aSocket.Close();
                    tcpClient.Close();
                }
                aSocket.Dispose();
            }
        }

        private void SaveFileOffline(string pageBodyMod, string recovD, int foundTitle)
        {
            offLineFile = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\";
            if (foundTitle < 0)
            { offLineFile += "noName.htm"; }
            else
            { offLineFile += recovD.Substring(foundTitle + 7, 8) + ".htm"; }
            offLineFile = offLineFile.Replace("'", "");
            offLineFile = offLineFile.Replace("\n", "");
            offLineFile = offLineFile.Replace("\r", "");
            try
            {
                File.AppendAllText(offLineFile, pageBodyMod);
            }
            catch { }
        }

        private void SetupEndFlagging()
        {
            CurrentStatus = "Ready";
            tmrNavDone.Enabled = false;
            internalRedirect = true;
            hadRecovery = false;
            tmrPopUps.Enabled = true;
            if (myBrowser.Url != null)
            {
                this.myAddrBar.Text = fixURLspellings(myBrowser.Url.ToString());
            }
            if (this.myAddrBar.Text.Substring(0, 5) != "file:")
            {
                string appendText = this.myAddrBar.Text + "<br />" + Environment.NewLine;
                File.AppendAllText(strExeFilePath + histPath, appendText);
            }
            atHome = false;
            stopClick = false;
            intRptdFlag = false;
            ctrlNavigated = false;
            saveOldPage = myBrowser.DocumentText;
            CheckSearchSitch();
        }

        private void CheckSearchSitch()
        {
            if (isAsearch)
            {
                isAsearch = false;
            }

            if (googleFailed)
            {
                googleFailed = false;
                string saveUrl = myAddrBar.Text;
                myAddrBar.Text = saveUrl.Replace("google", "bing");
                string cseCapitalized = currentSearchEng[0].ToString().ToUpper() + currentSearchEng.Substring(1);
                CurrentStatus = "Google failed, click again to Bing...";
            }
        }

        private string MakeFinalAdjustments(ref string pageBodyMod)
        {
            pageBodyMod = pageBodyMod.Replace("font", "fnot");
            pageBodyMod = pageBodyMod.Replace("FONT", "FNOT");
            pageBodyMod = pageBodyMod.Replace("widt", "wdit");
            pageBodyMod = pageBodyMod.Replace("WIDT", "WDIT");
            pageBodyMod = pageBodyMod.Replace("H1", "br/");
            pageBodyMod = pageBodyMod.Replace("H2", "br/");
            pageBodyMod = pageBodyMod.Replace("H3", "br/");
            pageBodyMod = pageBodyMod.Replace("H4", "br/");

            if (allowScripts == false)
                pageBodyMod = ScriptReplacements(pageBodyMod);
            int webSize = (int)Convert.ToSingle(chosenSize) / 4;
            string pageBodyModshow = "<!DOCTYPE html>";
            pageBodyModshow += "<font size=\"" + webSize + "\" face=\"" + chosenFont + "\"/>" + pageBodyMod;
            return pageBodyModshow;
        }

        private static string tryRecovery(Stream htmlStream)
        {
            string RecoveredDoc = "";
            byte[] bufferData = new byte[2048];
            int countRead = 1;
            int lastread = 0;
            while (countRead > 0)
            {
                countRead = htmlStream.Read(bufferData, 0, 2048);
                lastread += countRead;
                htmlStream.Position = lastread;
                RecoveredDoc += Encoding.UTF8.GetString(bufferData);
            }
            return RecoveredDoc;
        }

        private static string ScriptReplacements(string pageBodyMod)
        {
            pageBodyMod = RemoveScriptCode(pageBodyMod, "HEAD");
            pageBodyMod = RemoveScriptCode(pageBodyMod, "head");
            pageBodyMod = RemoveScriptCode(pageBodyMod, "SCRIPT");
            pageBodyMod = RemoveScriptCode(pageBodyMod, "script");
            pageBodyMod = RemoveScriptCode(pageBodyMod, "STYLE");
            pageBodyMod = RemoveScriptCode(pageBodyMod, "style");
            pageBodyMod = pageBodyMod.Replace("REDIR", "REDRI");
            pageBodyMod = pageBodyMod.Replace("redir", "redri");
            pageBodyMod = pageBodyMod.Replace("styl", "stly");
            pageBodyMod = pageBodyMod.Replace("STYL", "STLY");
            pageBodyMod = pageBodyMod.Replace("scri", "srci");
            pageBodyMod = pageBodyMod.Replace("SCRI", "SRCI");
            pageBodyMod = pageBodyMod.Replace("Scri", "Srci");
            pageBodyMod = pageBodyMod.Replace("java", "jav");
            pageBodyMod = pageBodyMod.Replace("JAVA", "JAV");
            pageBodyMod = pageBodyMod.Replace("func", "fucn");
            pageBodyMod = pageBodyMod.Replace("FUNC", "FUCN");
            pageBodyMod = pageBodyMod.Replace("onload", "onlaod");
            pageBodyMod = pageBodyMod.Replace("over", "ovre");
            pageBodyMod = pageBodyMod.Replace("OVER", "OVRE");
            pageBodyMod = pageBodyMod.Replace("target", "tagret");
            pageBodyMod = pageBodyMod.Replace("mouseout", "mouesout");
            pageBodyMod = pageBodyMod.Replace("MOUSEOUT", "MOUESOUT");
            pageBodyMod = pageBodyMod.Replace("widg", "wigd");
            pageBodyMod = pageBodyMod.Replace("WIDG", "WIGD");
            pageBodyMod = pageBodyMod.Replace("img", "igm");
            pageBodyMod = pageBodyMod.Replace("IMG", "IGM");
            pageBodyMod = pageBodyMod.Replace("t>", "  ");
            return pageBodyMod;
        }

        private static string RemoveScriptCode(string pageBodyMod, string checkWord)
        {
            string bodyReturn = pageBodyMod;
            int skipSpaceOffset = 2;
            int endScr = 0;
            int startScr = 0 - skipSpaceOffset;
            while (true)
            {
                if (startScr > bodyReturn.Length - skipSpaceOffset - 1) { return bodyReturn; }
                startScr = bodyReturn.IndexOf("<" + checkWord, startScr + skipSpaceOffset);
                if (startScr < 0) { return bodyReturn; }
                endScr = bodyReturn.IndexOf("</" + checkWord);
                if (endScr < startScr)
                { endScr = bodyReturn.IndexOf(checkWord, startScr + skipSpaceOffset + 1); }
                if (endScr < 0) { return bodyReturn; }
                bodyReturn = bodyReturn.Substring(0, startScr) + bodyReturn.Substring(endScr + 1 + checkWord.Length);
            }
            return bodyReturn;
        }

        private void ResetOfflineCkbox()
        {
            internalRedirect = false;
            tmrPopUps.Enabled = false;   // reset time to full value
            tmrPopUps.Enabled = true;
        }

        private string FixAboutUrl(string reDirLoc)
        {
            string reDirect = reDirLoc;
            if (reDirLoc.Substring(0, 6) == "about:")
            {
                string baseAddr = myAddrBar.Text;
                int lastSlashLoc = baseAddr.LastIndexOf("/");
                if ((reDirLoc.Substring(6, 1) == "/") && (baseAddr.IndexOf(reDirLoc.Substring(6, 4)) >= 0))
                {
                    string shorterBase = baseAddr.Substring(0, lastSlashLoc + 1);
                    lastSlashLoc = shorterBase.LastIndexOf("/") - 1;
                }
                string newBaseAddr = baseAddr.Substring(0, lastSlashLoc + 1);
                reDirect = newBaseAddr + reDirLoc.Substring(6);
                if (reDirect.IndexOf("blank") < 0)
                {
                    reDirect = RemoveDupsInPath(reDirect);
                    if (!internalRedirect)
                    {
                        myAddrBar.Text = FixDoubleSlash(reDirect);
                    }
                }
            }
            return reDirect;
        }

        private void StartnewForm(string reDirLoc)
        {
            if (reDirLoc.ToLower().IndexOf("blank") > -1) { return; }
            CurrentStatus = "Requested: " + reDirLoc;
            tmrPopUps.Enabled = true;
        }

        private void StartnewFormWseed(string reDirLoc, string DocText)
        {
            CurrentStatus = "Requested: " + reDirLoc;
            tmrPopUps.Enabled = true;
            myBrowser.DocumentText = DocText;
        }

        private static string fixURLspellings(string inputAddr)
        {
            string holdAddr = inputAddr.Replace("ovre", "over");
            string holdAddr2 = holdAddr.Replace("%2F", "/");
            string holdAddr3 = holdAddr2.Replace("stlye", "style");
            string fixedAddrBack = holdAddr3.Replace("%2f", "/");
            return fixedAddrBack;
        }

        private static string RemoveDupsInPath(string inRedirect)
        {
            string reDirectBack = "";
            string priorSet = "";
            string[] backSep = new string[] { "/" };
            string[] result = inRedirect.Split(backSep, StringSplitOptions.None);
            foreach (string oneSet in result)
            {
                if (oneSet != priorSet)
                { reDirectBack += oneSet + "/"; }
                priorSet = oneSet;
            }
            return (reDirectBack.Substring(0, reDirectBack.Length - 1));
        }

        private static string FixDoubleSlash(string inRedirect)
        {
            if (inRedirect.Length < 8) { return inRedirect; }
            int dSloc = inRedirect.IndexOf("//", 8);
            if (dSloc < 0)
            { return inRedirect; }
            int fSloc = inRedirect.IndexOf("/", 8);
            string reDirectBack = "";
            reDirectBack = inRedirect.Substring(0, fSloc) + inRedirect.Substring(dSloc + 1);
            return (reDirectBack);
        }

        private void PoshPageBrackets()
        {
            HtmlDocument fixDoc = myBrowser.Document;
            if ((fixDoc.Body == null) || (fixDoc.Body.InnerHtml == null))
            {
                CurrentStatus = "Empty Document";
                return;
            }

            //docTooShort = false;
            //CheckShortDoc(fixDoc.Url.ToString(), 1);
            //if (docTooShort) { return; }

            string pageBodyMod = fixDoc.Body.InnerHtml.ToString();
            pageBodyMod = pageBodyMod.Replace("<", "[");
            pageBodyMod = pageBodyMod.Replace(">", "]");
            myBrowser.Document.OpenNew(false);
            myBrowser.Document.Write(pageBodyMod);
        }

        private void SetupNavigAddress()
        {
            stopPopUps = true;
            stopClick = false;
            isSpying = false;
            navLoopCount = 0;
            ResetOfflineCkbox();
            if (myAddrBar.Text.Contains(" "))
            {
                isAsearch = true;
                string holdAddr = myAddrBar.Text;
                myAddrBar.Text = "https://www." + currentSearchEng + ".com/search?q=" + holdAddr.Replace(" ", "+");
            }
        }

        public void processAforceStop()
        {
            if (stopClick == true)
            {
                PoshPageBrackets();
                stopClick = false;
                return;
            }
            stopClick = true;
            CurrentStatus = "Interrupting...";
            tmrNavDone.Enabled = false;
            try { this.myBrowser.Stop(); }
            catch { }

            if (isSpying)
            {
                PoshPageBrackets();
                stopClick = false;
            }
            else try
                { 
                    intRptdFlag = true;
                    myBrowser_DocumentCompleted(this, null);
                }
            catch { }
        }

        //--------- timer events ---------//

        private void tmrPopUps_Tick(object sender, EventArgs e)
        {
            tmrPopUps.Enabled = false;
            stopPopUps = false;
        }

        private void tmrReroute_Tick(object sender, EventArgs e)
        {
            tmrReroute.Enabled = false;
            SetupNavigAddress();
            myBrowser.Navigate(myAddrBar.Text);
            //btnGoTo_Click(this, null);
        }

        private void tmrNavDone_Tick(object sender, EventArgs e)
        {
            tmrNavDone.Enabled = false;
            HtmlDocument fixDoc = myBrowser.Document;
            if ((fixDoc.Body == null) || (fixDoc.Body.InnerHtml == null))
            { return; }
            processAforceStop();
        }

    }
}
