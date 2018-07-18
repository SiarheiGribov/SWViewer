using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.IO;
using System.ComponentModel;
using DotNetWikiBot;
using System.Security.Cryptography;
using System.Reflection;
using System.Threading;
using EventSource4Net;
using System.Windows.Controls;
using Microsoft.VisualBasic;

// License: https://github.com/SiarheiGribov/SWViewer/blob/master/LICENSE

namespace swViewer
{
    public partial class MainWindow : Window
    {
        public bool check, checkUpd;
        public int countPast, countEls;
        public string useragent, customReason, rights, scriptUrl, pageTitle, newid, oldid, project, server_url, user, pageUrl, comment, page_namespace,
            summary, diffLink, jsonDiff, fromTitle, fromUser, diff, diffSize, prediffEnd, preDiff, diffStart, diffEnd;
        string[] wikis = { "afwiki", "alswiki", "amwiki", "anwiki", "angwiki", "arwikisource", "arwikiversity", "aswikisource", "astwiki", "avwiki", "azwiki", "bat_smgwiki", "bgwikibooks", "brwiki", "bswiki", "bswikiquote", "cawikiquote", "cswiktionary", "csbwiki", "csbwiktionary", "cywiki", "dawikisource", "dewikisource", "dinwiki", "diqwiki", "elwikiquote", "elwikisource", "enwikiversity", "eowikinews", "eowiki", "eowikisource", "eswiktionary", "etwikibooks", "fawikiquote", "fawikivoyage", "fiwikisource", "frwikinews", "frpwiki", "gawiki", "glwikibooks", "hewikibooks", "hewikiquote", "hrwikisource", "hrwiktionary", "htwiki", "huwikibooks", "huwikiquote", "huwiktionary", "hywiktionary", "iawiktionary", "idwikiquote", "iewiktionary", "iowiki", "iswiktionary", "jvwiki", "kawiki", "kkwiki", "kowikibooks", "kowikisource", "kuwiki", "kywiki", "lawiki", "lawikisource", "lawiktionary", "ladwiki", "lbwiki", "liwiki", "lmowiki", "ltgwiki", "lvwiki", "maiwiki", "map_bmswiki", "metawiki", "mgwiki", "mkwikibooks", "mlwiki", "mlwiktionary", "mrwikisource", "mrjwiki", "mswiktionary", "ndswiktionary", "nds_nlwiki", "newwiki", "nlwikisource", "nowikisource", "orwikisource", "pamwiki", "pdcwiki", "plwikinews", "plwikiquote", "pnbwiktionary", "pswiki", "rmywiki", "rowikinews", "ruewiki", "sawiki", "sawiktionary", "sdwiktionary", "siwiki", "skwiktionary", "sqwiki", "srwikisource", "stwiktionary", "stqwiki", "svwikibooks", "svwikinews", "svwikiversity", "tawikinews", "tawiki", "tewiki", "ttwiki", "tyvwiki", "ugwiki", "urwiki", "vepwiki", "vlswiki", "yiwiktionary", "zhwikiquote", "zh_classicalwiki", "zh_yuewiki", "abwiki", "adywiki", "afwiktionary", "angwiktionary", "arcwiki", "aywiki", "aywiktionary", "barwiki", "be_x_oldwiki", "biwiki", "bnwikisource", "bswikisource", "cawikibooks", "crwiki", "crhwiki", "cuwiki", "cvwiki", "dewikibooks", "dewiktionary", "dvwiki", "dvwiktionary", "eewiki", "eowikibooks", "eswikibooks", "eswikinews", "eswikiquote", "eswikisource", "eswikiversity", "euwikiquote", "fawiktionary", "fjwiki", "fowiktionary", "fywiki", "glwiktionary", "guwiki", "hewikisource", "hewiktionary", "hiwiktionary", "hsbwiki", "hsbwiktionary", "huwikisource", "hywikibooks", "iawiki", "idwikibooks", "iewiki", "jbowiki", "kawiktionary", "klwiki", "kmwikibooks", "kmwiktionary", "knwiki", "kuwikiquote", "kvwiki", "kywikibooks", "ltwikisource", "ltwiktionary", "lvwiktionary", "mediawikiwiki", "mznwiki", "nawiktionary", "nahwiktionary", "ndswiki", "newiki", "newiktionary", "nlwiktionary", "nnwikiquote", "nnwiktionary", "ocwiki", "outreachwiki", "pagwiki", "papwiki", "piwiki", "plwikibooks", "ptwikibooks", "ptwikinews", "ptwikiquote", "ptwikisource", "ptwikivoyage", "ptwiktionary", "quwiki", "rmwiki", "ruwikiquote", "ruwikisource", "rwwiki", "sawikisource", "sahwikisource", "scowiki", "skwikibooks", "skwiki", "skwikiquote", "slwikibooks", "slwiki", "snwiki", "sowiki", "specieswiki", "sqwikibooks", "sqwikiquote", "stwiki", "suwiktionary", "swwiktionary", "tawikiquote", "tawikisource", "tgwiki", "tgwiktionary", "thwikibooks", "thwikiquote", "thwikisource", "thwiktionary", "tkwiki", "tnwiki", "towiki", "tpiwiki", "twwiki", "tywiki", "udmwiki", "ukwikiquote", "uzwiki", "viwikibooks", "viwikisource", "vowiki", "vowiktionary", "wowiki", "xhwiki", "yiwikisource", "yowiki", "zh_min_nanwiki", "amwiktionary", "arwikinews", "arwiktionary", "astwiktionary", "bewikisource", "betawikiversity", "bmwiki", "bnwiki", "brwiktionary", "bswikibooks", "bswiktionary", "bxrwiki", "cawiktionary", "cswikinews", "cswikiquote", "cswikiversity", "dawikibooks", "dewikinews", "dewikiquote", "dtywiki", "enwikibooks", "enwikiquote", "etwiki", "etwikiquote", "euwikibooks", "extwiki", "fawikibooks", "ffwiki", "fiwikinews", "fiwiktionary", "fjwiktionary", "frwikibooks", "frwikiversity", "ganwiki", "gdwiki", "glwikiquote", "gnwiktionary", "gotwiki", "guwikisource", "gvwiki", "hewikinews", "hiwikibooks", "hiwiki", "hifwiki", "hrwiki", "iawikibooks", "idwikisource", "ikwiki", "incubatorwiki", "jamwiki", "kaawiki", "kabwiki", "kbdwiki", "kgwiki", "kiwiki", "knwikiquote", "kswiki", "kuwikibooks", "kuwiktionary", "kwwiki", "kywikiquote", "liwikisource", "ltwikibooks", "ltwikiquote", "mdfwiki", "mgwikibooks", "miwiki", "mlwikiquote", "mrwikibooks", "mrwikiquote", "mrwiktionary", "mtwiki", "myvwiki", "nlwikibooks", "nowikibooks", "nowiktionary", "novwiki", "nvwiki", "olowiki", "omwiki", "oswiki", "plwikisource", "plwikivoyage", "ptwikiversity", "quwiktionary", "rowikiquote", "rowikisource", "ruwikibooks", "ruwikimedia", "ruwikinews", "ruwiktionary", "sahwiki", "sewiki", "sgwiki", "slwiktionary", "sqwikinews", "srwikibooks", "srwikinews", "srnwiki", "sswiki", "suwiki", "svwikiquote", "svwikisource", "swwiki", "szlwiki", "tcywiki", "tewikisource", "tewiktionary", "thwiki", "tiwiki", "tlwiki", "ttwiktionary", "ukwikinews", "ukwiktionary", "urwikiquote", "sourceswiki", "wuuwiki", "xalwiki", "zhwikibooks", "acewiki", "afwikiquote", "anwiktionary", "arwikibooks", "azwikiquote", "azwiktionary", "bewiki", "bewiktionary", "bgwiki", "bgwikiquote", "bgwiktionary", "bjnwiki", "bpywiki", "brwikisource", "bswikinews", "bugwiki", "cdowiki", "chrwiki", "chywiki", "ckbwiki", "cswikibooks", "cswikisource", "dawiktionary", "dsbwiki", "elwikinews", "elwikiversity", "elwiktionary", "eowiktionary", "etwikisource", "etwiktionary", "fawikinews", "fiwikibooks", "fiwikiversity", "fiwikivoyage", "fiu_vrowiki", "fowiki", "fowikisource", "fywikibooks", "gagwiki", "glkwiki", "guwikiquote", "guwiktionary", "hawiki", "hywiki", "hywikiquote", "idwiktionary", "ilowiki", "iowiktionary", "iswiki", "jvwiktionary", "kawikibooks", "kkwikibooks", "kkwiktionary", "kmwiki", "kowikiversity", "koiwiki", "krcwiki", "kwwiktionary", "lezwiki", "lgwiki", "liwikibooks", "liwiktionary", "lnwiktionary", "mhrwiki", "mnwiki", "mnwiktionary", "mswikibooks", "mwlwiki", "newikibooks", "nlwikiquote", "nowikiquote", "nsowiki", "nycwikimedia", "orwiktionary", "pawikibooks", "pawikisource", "pflwiki", "pihwiki", "plwiktionary", "pnbwiki", "pntwiki", "pswiktionary", "rowiktionary", "ruwikiversity", "rwwiktionary", "sawikibooks", "sawikiquote", "sdwiki", "siwikibooks", "siwiktionary", "slwikiquote", "slwikiversity", "specieswiki", "srwiktionary", "suwikiquote", "svwiktionary", "tawikibooks", "tewikiquote", "tgwikibooks", "trwikibooks", "trwikimedia", "trwikinews", "trwikiquote", "trwikisource", "trwiktionary", "tswiki", "ukwikibooks", "uzwikiquote", "vewiki", "vecwiktionary", "viwikiquote", "viwiktionary", "wawiki", "xmfwiki", "yiwiki", "zeawiki", "zhwikinews", "zhwikisource", "zh_min_nanwiktionary", "afwikibooks", "akwiki", "arwikiquote", "arzwiki", "aswiki", "azwikibooks", "azwikisource", "bawiki", "bclwiki", "bewikibooks", "bewikiquote", "bgwikinews", "bgwikisource", "bhwiki", "bnwikibooks", "bnwiktionary", "bowiki", "brwikiquote", "cawikinews", "cawikisource", "cbk_zamwiki", "cewiki", "chwiki", "chrwiktionary", "cowiki", "cvwikibooks", "cywikibooks", "cywikiquote", "cywikisource", "cywiktionary", "dawikiquote", "dewikiversity", "dkwikimedia", "dzwiki", "elwikibooks", "eowikiquote", "euwiki", "euwiktionary", "fawikisource", "fiwikiquote", "frwikiquote", "frrwiki", "fywiktionary", "gawiktionary", "gdwiktionary", "glwiki", "glwikisource", "gnwiki", "gvwiktionary", "hawiktionary", "hakwiki", "hawwiki", "hiwikiquote", "hrwikibooks", "hrwikiquote", "hywikisource", "igwiki", "iswikibooks", "iswikiquote", "iswikisource", "iuwiki", "iuwiktionary", "jbowiktionary", "kawikiquote", "klwiktionary", "knwikisource", "knwiktionary", "kowikinews", "kowikiquote", "kowiktionary", "kswiktionary", "kshwiki", "kywiktionary", "lawikibooks", "lawikiquote", "lbwiktionary", "lbewiki", "liwikiquote", "lnwiki", "lowiki", "lowiktionary", "ltwiki", "miwiktionary", "minwiki", "mkwikisource", "mkwiktionary", "mlwikibooks", "mlwikisource", "mswiki", "mtwiktionary", "mywiki", "mywiktionary", "nawiki", "nahwiki", "nlwikimedia", "nowikinews", "nrmwiki", "nywiki", "ocwikibooks", "ocwiktionary", "omwiktionary", "orwiki", "pawiki", "pawiktionary", "pcdwiki", "plwikimedia", "rnwiki", "rowikibooks", "roa_rupwiki", "roa_rupwiktionary", "sgwiktionary", "shwiktionary", "skwikisource", "slwikisource", "smwiki", "smwiktionary", "sowiktionary", "sqwiktionary", "srwikiquote", "sswiktionary", "tawiktionary", "tewikibooks", "tetwiki", "tiwiktionary", "tkwiktionary", "tlwikibooks", "tlwiktionary", "tnwiktionary", "tpiwiktionary", "tswiktionary", "ttwikibooks", "tumwiki", "uawikimedia", "ugwiktionary", "ukwikisource", "urwikibooks", "urwiktionary", "uzwiktionary", "wawiktionary", "wowikiquote", "wowiktionary", "zawiki", "zh_min_nanwikisource", "zuwiki", "zuwiktionary", "arwikimedia", "bdwikimedia", "bewikimedia", "brwikimedia", "cawikimedia", "cowikimedia", "etwikimedia", "fiwikimedia", "mkwikimedia", "mxwikimedia", "nowikimedia", "sewikimedia", "testwikidatawiki", "wikimania2018wiki" };
        string[] namespaces = { "Main", "Talk", "User", "User talk", "Project", "Project talk", "File", "File talk", "MediaWiki", "MediaWiki talk", "Template", "Template talk", "Help", "Help talk", "Category", "Category talk" };

        private static MD5 md5 = MD5.Create();

        WindowWhitelist wind2 = new WindowWhitelist();

        List<string> gusers = new List<string>();
        List<string> wUList = new List<string>();
        List<string> wPList = new List<string>();
        List<List<string>> diffsHistory = new List<List<string>>();
        public List<string> WUList { get => wUList; set => wUList = value; }
        public List<string> WPList { get => wPList; set => wPList = value; }

        private ObservableCollection<edit> edits;
        public class edit
        {
            public string Image { get; set; }
            public string Project { get; set; }
            public string old_id { get; set; }
            public string new_id { get; set; }
            public string script_url { get; set; }
            public string page_url { get; set; }
            public string title { get; set; }
            public string User { get; set; }
            public string comment { get; set; }
            public string serverName { get; set; }
            public string surl { get; set; }
            public string ns { get; set; }
        }

        private static BackgroundWorker BackgroundWorker1 = new BackgroundWorker();


        public MainWindow()
        {
            InitializeComponent();
            Queue.SelectionMode = SelectionMode.Single;

            // Проверка версии
            if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "swViewer_Setup.exe"))
            {
                Thread.Sleep(2000);
                File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + "swViewer_Setup.exe");
            }
            string versionNow = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            customReason = "";
            try
            {
                var ver = new WebClient();
                ver.Headers.Add("user-agent: SW-Viewer: iluvatar@tools.wmflabs.org");
                var verRaw = ver.DownloadString("https://tools.wmflabs.org/iluvatarbot/swviewer/version.php");
                Regex verReg = new Regex(@"^ver=(.*?)\|");
                Match verMatch = verReg.Match(verRaw);
                string versionNew = verMatch.Groups[1].ToString();
                Regex hashReg = new Regex(@"hash=(.*?)$");
                Match hashMatch = hashReg.Match(verRaw);
                string Hash = hashMatch.Groups[1].ToString();
                if (versionNow != versionNew)
                {
                    if (MessageBox.Show("Please download and install the latest version of SWViewer. Update now?", "Update", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        checkUpd = true;
                        while (checkUpd == true)
                        {
                            try
                            {
                                var newSetup = new WebClient();
                                newSetup.Headers.Add("user-agent: SW-Viewer: iluvatar@tools.wmflabs.org");
                                newSetup.DownloadFile("https://tools.wmflabs.org/iluvatarbot/swviewer/swViewer_Setup.exe", System.AppDomain.CurrentDomain.BaseDirectory + "swViewer_Setup.exe");
                            }
                            catch (WebException) { }
                            string hashCheck = CalculateChecksum(System.AppDomain.CurrentDomain.BaseDirectory + "swViewer_Setup.exe");
                            if (hashCheck == Hash)
                            {
                                checkUpd = false;
                                Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + "swViewer_Setup.exe");
                                Application.Current.Shutdown();
                            }
                            else
                            {
                                if (MessageBox.Show("Downloaded file is corrupted. Try again?", "Update", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                                    checkUpd = true;
                                else
                                {
                                    if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "swViewer_Setup.exe"))
                                        File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + "swViewer_Setup.exe");
                                    checkUpd = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (WebException) { }

            Queue.Visibility = Visibility.Hidden;
            webBrowser.Visibility = Visibility.Hidden;
            Stack.Visibility = Visibility.Hidden;
            StackReverts.Visibility = Visibility.Hidden;
            statusText1.Visibility = Visibility.Hidden;
            buttonPrev.IsEnabled = false;
            buttonNext.IsEnabled = false;
            buttonRevert.IsEnabled = false;
            buttonRevertCustom.IsEnabled = false;
            buttonOpen.IsEnabled = false;
            clearMenu.IsEnabled = false;

            (webBrowser.Child as System.Windows.Forms.WebBrowser).ScriptErrorsSuppressed = true;
            (webBrowser.Child as System.Windows.Forms.WebBrowser).DocumentCompleted += MainWindow_DocumentCompleted;

            this.Closing += MainWindow_Closing;
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

            edits = new ObservableCollection<edit>();
            Queue.ItemsSource = edits;

            diffStart = Properties.Resources.diffStart;
            diffEnd = Properties.Resources.diffEnd;

            BackgroundWorker1.DoWork += BackgroundWorker1_DoWork;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            wind2.userAddEvent += new EventHandler(wind2_userAddEvent);
            wind2.userDeleteEvent += new EventHandler(wind2_userDeleteEvent);
            wind2.projectAddEvent += new EventHandler(wind2_projectAddEvent);
            wind2.projectDeleteEvent += new EventHandler(wind2_projectDeleteEvent);
        }

        void wind2_userAddEvent(object sender, EventArgs e)
        {
            WUList.Add(wind2.usertList.Text.ToString());
        }
        void wind2_userDeleteEvent(object sender, EventArgs e)
        {
            WUList.Remove(wind2.Users_List.SelectedItem.ToString());
        }
        void wind2_projectAddEvent(object sender, EventArgs e)
        {
            WPList.Add(wind2.projectList.Text.ToString());
        }
        void wind2_projectDeleteEvent(object sender, EventArgs e)
        {
            WPList.Remove(wind2.Projects_List.SelectedItem.ToString());
        }

        private void MainWindow_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            Queue.IsEnabled = true;
            webBrowser.IsEnabled = true;
            buttonOpen.IsEnabled = true;
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker th = sender as BackgroundWorker;
            e.Result = EventStream(th);
        }

        public object EventStream(BackgroundWorker th)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                EventSource es = new EventSource(new Uri(@"https://stream.wikimedia.org/v2/stream/recentchange"), 50000);
                es.EventReceived += new EventHandler<ServerSentEventReceivedEventArgs>((o, e) =>
                {
                    dynamic stuff = JObject.Parse(e.Message.Data);
                    // Только правки, только создания страниц, только неотпатрулированные правки; проект не числится в приватном whitlist'е,
                    // проект входит в список наблюдаемых
                    if (stuff.bot == false && wPList.Contains(stuff.wiki.ToString()) != true && (stuff.type.ToString() == "edit" || stuff.type.ToString() == "new") && stuff.patrolled != true && Array.IndexOf(wikis, stuff.wiki.ToString()) != -1)
                    {
                        string diffnamespace = "";
                        if (stuff.@namespace >= 0 && stuff.@namespace <= 15)
                            diffnamespace = namespaces[stuff.@namespace].ToString();
                        string diffp = stuff.wiki.ToString();
                        string diffsurl = stuff.server_url.ToString();
                        string diffservername = stuff.server_name.ToString();
                        string diffuser = stuff.user.ToString();
                        string diffold = stuff.revision.old.ToString();
                        string diffnew = stuff.revision.@new.ToString();
                        string diffscipturl = stuff.server_url.ToString() + stuff.server_script_path.ToString();
                        string diffpage = stuff.meta.uri.ToString();
                        string difftitle = stuff.title.ToString();
                        string diffcomment = stuff.comment.ToString();

                        try
                        {
                            // Фильтры
                            var userInfo = new WebClient();
                            userInfo.Headers.Add(useragent);
                            var info = userInfo.DownloadString(stuff.server_url.ToString() + stuff.server_script_path.ToString() + "/api.php?action=query&list=users&ususers=" + diffuser + "&usprop=groups|registration|editcount&utf8&format=json");
                            stuff = JObject.Parse(info);
                            
                            // Проверка даты регистрации юзера
                            if ((stuff["query"]["users"][0]["registration"].ToString() == "" || stuff["query"]["users"][0]["registration"].ToString() == null) || (DateTime.UtcNow - DateTime.Parse(stuff["query"]["users"][0]["registration"].ToString())).TotalDays >= 5) {  return; }
                            // Проверка количества правок юзера
                            if (Convert.ToInt32(stuff["query"]["users"][0]["editcount"].ToString()) >= 100) { return; }
                            // Проверка отсутствия локальных флагов
                            string groups = stuff["query"]["users"][0]["groups"].ToString();
                            if (groups.Contains("sysop") || groups.Contains("editor") || groups.Contains("autoreviewer") || groups.Contains("autoconfirmed") || groups.Contains("confirmed") || groups.Contains("extendedconfirmed") || groups.Contains("filemover") || groups.Contains("patroller") || groups.Contains("templateeditor") || groups.Contains("autopatrolled") || groups.Contains("autoeditor") || groups.Contains("closer") || groups.Contains("rollbacker") || groups.Contains("translator") || groups.Contains("translationadmin") || groups.Contains("engineer") || groups.Contains("global-renamer") || groups.Contains("oversight") || groups.Contains("reviewer") || groups.Contains("bureaucrat")) { return; }
                        } catch { }
                        // Проверка отсутствия юзера в whitelist CVN
                        try
                        {
                            var cvnWl = new WebClient();
                            cvnWl.Headers.Add(useragent);
                            var cvn = cvnWl.DownloadString("https://cvn.wmflabs.org/api.php?users=" + diffuser);
                            stuff = JObject.Parse(cvn);
                            if (stuff["users"][diffuser]["type"].ToString() == "whitelist") { return; }
                        } catch { }
                        // Проверка отсутствия юзера в приватном whitelist'е и отсутствие глобальных флагов
                        if (wUList.Contains(diffuser) != true && gusers.Contains(diffuser) != true)
                        {
                            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                            {
                                edits.Insert(0, (new edit() { Image = @"pack://application:,,,/Resources/image/Searchtoo-right-dark.bmp", Project = diffp, ns = diffnamespace, surl = diffsurl, serverName = diffservername, User = diffuser, old_id = diffold, new_id = diffnew, script_url = diffscipturl, page_url = diffpage, title = difftitle, comment = diffcomment }));
                                buttonNext.IsEnabled = true;
                                countEls++;
                                statusText1.Text = "Unverified edits: " + countEls;
                                statusText1.Visibility = Visibility.Visible;
                            }));
                        }
                    }
                });
                es.Start(cts.Token);
            }));
            while (true) ;
        }

        // Подключение
        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            sPanel1.IsEnabled = false;
            try
            {
                Site wiki = new Site("https://meta.wikimedia.org", loginWikiText.Text, loginPassWikiText.Password);
            }
            catch (DotNetWikiBot.WikiBotException)
            {
                MessageBox.Show("Login failed! Please check username and password");
                sPanel1.IsEnabled = true;
                return;
            }
            catch (WebException)
            {
                MessageBox.Show("No internet connection or server is not available");
                sPanel1.IsEnabled = true;
                return;
            }
            try
            {
                var userInfo = new WebClient();
                userInfo.Headers.Add("user-agent: [[:meta: user:Iluvatar/SWViewer]]; Login as: " + loginWikiText.Text.ToString());
                var globalUsers = userInfo.DownloadString("http://meta.wikimedia.org/w/api.php?action=query&format=json&list=globalallusers&utf8=1&formatversion=2&agugroup=global-bot|staff|steward|global-sysop|global-rollbacker|abusefilter-helper|founder|ombudsman|global-interface-editor&&aguprop=groups&agulimit=500");
                dynamic stuff = JObject.Parse(globalUsers);
                rights = "none";
                foreach (JObject globaluser in stuff["query"]["globalallusers"])
                {
                    if (globaluser["name"].ToString() == loginWikiText.Text)
                    {
                        if (globaluser["groups"].ToString().Contains("global-rollbacker"))
                            rights = "GR";
                        if (globaluser["groups"].ToString().Contains("global-sysop"))
                            rights = "GS";
                        if (globaluser["groups"].ToString().Contains("steward"))
                            rights = "S";
                    }
                    gusers.Add(globaluser["name"].ToString());
                }
            }
            catch (WebException)
            {
                MessageBox.Show("No internet connection or server is not available");
                sPanel1.IsEnabled = true;
                return;
            }

            if (rights == "none")
            {
                MessageBox.Show("Sorry, but uses of app is permitted only for Global rollbackers, Global sysops and Stewards", "Login is denied");
                Application.Current.Shutdown();
            }
            else
                statusText2.Text = "Logged as " + loginWikiText.Text + " (" + rights + ")";

            useragent = "user-agent: [[:meta:user:Iluvatar/SWViewer]]; account: " + loginWikiText.Text.ToString();

            try
            {
                // Обновление статистики использования
                var userAddStat = new WebClient();
                userAddStat.Headers.Add(useragent);
                var userStat = userAddStat.DownloadString("https://tools.wmflabs.org/iluvatarbot/swviewer/index.php?user=" + loginWikiText.Text + "&key=fkrMpQNmAC");
            }
            catch (WebException) { }

            Stack.Visibility = Visibility.Visible;
            StackReverts.Visibility = Visibility.Visible;
            Queue.Visibility = Visibility.Visible;
            webBrowser.Visibility = Visibility.Visible;
            sPanel1.Visibility = Visibility.Hidden;
            clearMenu.IsEnabled = true;

            BackgroundWorker1.RunWorkerAsync();
        }

        // Клик по элементу в очереди
        private void Queue_Selected(object sender, EventArgs e)
        {
            var editItem = Queue.SelectedItem as edit;
            if (editItem == null)
                return;
            Queue.IsEnabled = false;
            webBrowser.IsEnabled = false;
            edits.Remove(editItem);
            countEls--;
            statusText1.Text = "Unverified edits: " + countEls.ToString();
            countPast = 0;
            if (diffsHistory.Count == 6)
                diffsHistory.RemoveAt(5);
            diffsHistory.Insert(0, new List<string> { editItem.Project, editItem.surl, editItem.old_id, editItem.new_id, editItem.User, editItem.page_url, editItem.script_url, editItem.title, editItem.comment, editItem.ns});
            openDiff(editItem.Project, editItem.surl, editItem.old_id, editItem.new_id, editItem.User, editItem.page_url, editItem.script_url, editItem.title, editItem.comment, editItem.ns);
            if (Queue.Items.Count == 0)
                buttonNext.IsEnabled = false;
            if (diffsHistory.Count > 1)
                buttonPrev.IsEnabled = true;
        }

        // Предыдущая досмотренная правка
        private void buttonPrev_Click(object sender, RoutedEventArgs e)
        {
            countPast++;
            if (countPast == diffsHistory.Count - 1)
                buttonPrev.IsEnabled = false;
            Queue.IsEnabled = false;
            webBrowser.IsEnabled = false;
            openDiff(diffsHistory[countPast][0], diffsHistory[countPast][1], diffsHistory[countPast][2], diffsHistory[countPast][3], diffsHistory[countPast][4], diffsHistory[countPast][5], diffsHistory[countPast][6], diffsHistory[countPast][7], diffsHistory[countPast][8], diffsHistory[countPast][9]);
        }

        // Следующая правка в очереди
        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            if (Queue.Items.Count > 0)
            {
                var editItem = Queue.Items[0] as edit;
                if (editItem == null)
                    return;
                edits.RemoveAt(0);
                countEls--;
                statusText1.Text = "Unverified edits: " + countEls.ToString();
                countPast = 0;
                if (diffsHistory.Count == 6)
                    diffsHistory.RemoveAt(5);
                diffsHistory.Insert(0, new List<string> { editItem.Project, editItem.surl, editItem.old_id, editItem.new_id, editItem.User, editItem.page_url, editItem.script_url, editItem.title, editItem.comment, editItem.ns});

                openDiff(editItem.Project, editItem.surl, editItem.old_id, editItem.new_id, editItem.User, editItem.page_url, editItem.script_url, editItem.title, editItem.comment, editItem.ns);

                if (Queue.Items.Count == 0)
                    buttonNext.IsEnabled = false;
                if (diffsHistory.Count > 1)
                    buttonPrev.IsEnabled = true;
            }
        }

        // Функция проверки перед откатом актуальности ревизии
        public void isCurrent()
        {
            try
            {
                var w = new WebClient();
                w.Headers.Add(useragent);
                string revisions = w.DownloadString(scriptUrl + "/api.php?action=query&prop=revisions&titles=" + pageTitle + "&rvprop=ids&format=json&utf8=1");
                dynamic stuff = JObject.Parse(revisions);
                string r = stuff["query"]["pages"].ToString();

                Match match = Regex.Match(r, @"""revisions"":\s*?\[\s*?\{\s*?""revid"":\s *?(\d*?),", RegexOptions.IgnoreCase);
                string lastRev = match.Groups[1].Value;

                if (newid != lastRev)
                {
                    oldid = newid;
                    newid = lastRev;
                    Queue.IsEnabled = false;
                    webBrowser.IsEnabled = false;
                    MessageBox.Show("This revision is not last. Loading new revision...");
                    openDiff(project, server_url, oldid, newid, user, pageUrl, scriptUrl, pageTitle, comment, page_namespace);
                    check = false;
                }
                else
                    check = true;
            }
            catch (WebException)
            { MessageBox.Show("No internet connection or server is not available"); check = false; return; }
            catch (DotNetWikiBot.WikiBotException)
            { MessageBox.Show("No internet connection or server is not available"); check = false; return; }
        }

        // Функция отката, используемая на обоеих кнопках отката
        public void Revert()
        {
            isCurrent();
            if (check == true)
            {
                Site wiki = new Site(server_url, loginWikiText.Text, loginPassWikiText.Password);
                try
                {
                    // Если у юзера нет флага GR
                    //if (rights != "none")  // if user not GR/GS/S 
                    //{
                    dynamic token = JObject.Parse(wiki.GetWebPage("/w/api.php?action=query&meta=tokens&type=rollback&format=json"));
                    string rollbacktoken = token["query"]["tokens"]["rollbacktoken"];                 

                    if (customReason != "" && customReason != null) {
                        summary = "Reverted edits by [[User:" + user + "|" + user + "]] using [[metawiki:User:Iluvatar/SW-Viewer|SW-Viewer]]. " + customReason.ToString();
                        customReason = "";
                    } else
                        summary = "Reverted edits by [[User:" + user + "|" + user + "]] using [[metawiki:User:Iluvatar/SW-Viewer|SW-Viewer]]";
                    JObject rollback = JObject.Parse(wiki.PostDataAndGetResult("/w/api.php?action=rollback&format=json", "title=" + pageTitle + "&user=" + user + "&summary=" + summary + "&token=" + Uri.EscapeDataString(rollbacktoken)));
                    if (rollback["error"]?["code"]?.ToString() != null)
                    {
                        if (rollback["error"]["code"].ToString() == "onlyauthor" || rollback["error"]["code"].ToString() == "protectednamespace-interface" || rollback["error"]["code"].ToString() == "protectednamespace" || rollback["error"]["code"].ToString() == "customcssjsprotected" || rollback["error"]["code"].ToString() == "cascadeprotected" || rollback["error"]["code"].ToString() == "protectedpage" || rollback["error"]["code"].ToString() == "permissiondenied" || rollback["error"]["code"].ToString() == "blocked" || rollback["error"]["code"].ToString() == "autoblocked" || rollback["error"]["code"].ToString() == "missingtitle" || rollback["error"]["code"].ToString() == "hookaborted" || rollback["error"]["code"].ToString() == "invalidtitle" || rollback["error"]["code"].ToString() == "ratelimited")
                            MessageBox.Show(rollback["error"]["info"].ToString());

                        if (rollback["error"]["code"].ToString() == "alreadyrolled")
                        {
                            var w2 = new WebClient();
                            w2.Headers.Add(useragent);
                            string revisions = w2.DownloadString(scriptUrl + "/api.php?action=query&prop=revisions&titles=" + pageTitle + "&rvprop=ids&format=json&utf8=1");
                            dynamic stuff = JObject.Parse(revisions);
                            string r = stuff["query"]["pages"].ToString();

                            Match match = Regex.Match(r, @"""revisions"":\s*?\[\s*?\{\s*?""revid"":\s *?(\d*?),", RegexOptions.IgnoreCase);
                            string lastRev = match.Groups[1].Value;

                            if (newid != lastRev)
                            {
                                oldid = newid;
                                newid = lastRev;
                                Queue.IsEnabled = false;
                                webBrowser.IsEnabled = false;
                                MessageBox.Show("This revision is not last. Loading new revision.");
                                openDiff(project, server_url, oldid, newid, user, pageUrl, scriptUrl, pageTitle, comment, page_namespace);
                                check = false;
                            }
                        }
                    }
                    else
                    {
                        string title = rollback["rollback"]["title"].ToString();
                        oldid = newid;
                        newid = rollback["rollback"]["revid"].ToString();
                        string comment = rollback["rollback"]["summary"].ToString();

                        openDiff(project, server_url, oldid, newid, loginWikiText.Text, pageUrl, scriptUrl, title, comment, page_namespace);
                    }
                    //}
                    //else
                    //p.UndoLastEdits("Reverted edits by [[User:" + toUser + "|" + toUser + "]] using [[:ru:User:Iluvatar/SW-Viewer|SW-Viewer]].", true);
                }
                catch (DotNetWikiBot.WikiBotException)
                {
                    MessageBox.Show("Page is not found or internal error of app");
                    (webBrowser.Child as System.Windows.Forms.WebBrowser).Navigate(diffLink);
                }
                catch (WebException)
                {
                    MessageBox.Show("No internet connection or server is not available");
                    sPanel1.IsEnabled = true;
                    return;
                }
            }
        }

        // Открытие диффа
        public void openDiff(string projectName, string serv_url, string diffOld, string diffNew, string diffUser, string diffpageUrl, string diffscriptUrl, string diffTitle, string diffComment, string pagens)
        {
            page_namespace = pagens;
            server_url = serv_url;
            project = projectName;
            scriptUrl = diffscriptUrl;
            pageUrl = diffpageUrl;
            pageTitle = diffTitle;
            oldid = diffOld;
            newid = diffNew;
            comment = diffComment;
            user = diffUser;

            buttonRevert.IsEnabled = false;
            buttonRevertCustom.IsEnabled = false;
            if (diffOld == null || diffOld == "")
                (webBrowser.Child as System.Windows.Forms.WebBrowser).Navigate(pageUrl);
            else
            {
                string urlAPI = diffscriptUrl + "/api.php?action=compare&format=json&fromrev=" + diffOld + "&torev=" + diffNew + "&tocontentformat=text%2Fcss&tocontentmodel=text&prop=diff|size|title|user|ids";
                bool isFail;
                do
                {
                    isFail = false;
                    try
                    {
                        var web3 = new WebClient();
                        web3.Headers.Add(useragent);
                        jsonDiff = web3.DownloadString(urlAPI);
                    }
                    catch (WebException)
                    {
                        isFail = true;
                        Thread.Sleep(1000);
                    }
                }
                while (isFail);
                JObject rawDiff = JObject.Parse(jsonDiff);

                diff = rawDiff["compare"]["*"].ToString();
                fromTitle = rawDiff["compare"]["fromtitle"].ToString();
                fromUser = rawDiff["compare"]["fromuser"].ToString();
                comment = diffComment;

                int rw = Int32.Parse(rawDiff["compare"]["tosize"].ToString()) - Int32.Parse(rawDiff["compare"]["fromsize"].ToString());
                if (rw > 0)
                {
                    if (rw >= 5000)
                        diffSize = "<font color='#0E8700'>+" + rw.ToString() + "</font>";
                    else
                        diffSize = "<font color='Green'>+" + rw.ToString() + "</font>";
                }
                else
                {
                    if (rw == 0)
                        diffSize = "<font color='Grey'>0</font>";
                    if (rw <= -5000)
                        diffSize = "<font color='#A50000'>" + rw.ToString() + "</font>";
                    else
                        diffSize = "<font color='Red'>" + rw.ToString() + "</font>";
                }
                if (page_namespace != "")
                    page_namespace = "; NS: " + page_namespace;
                if (comment == null)
                    prediffEnd = "</font><br><hr style='margin-bottom:1px; margin-top:1px'>";
                else
                    prediffEnd = "<br><p style='text-align:center; display:inline;'>Section and comment: " + comment + "</p></font><br><hr style='margin-bottom:1px; margin-top:1px'>";
                preDiff =
"<i><font size='2' face='verdana'><p style='text-align:left; display:inline;'><span style='float:left;'>" + projectName + page_namespace + "</span><span style='float:right;'>" + projectName + page_namespace + "</span></p></font></i><br>" +
"<b><font size='5'><p style='text-align:left; display:inline;'><span style='float:left;'>" + fromTitle + "</span><span style='float:right;'>" + diffTitle + "</span></p></font></b><br><br>" +
"<font size='3'><p style='text-align:left; display:inline;'><span style='float:left;'><a href='" + serv_url + "/w/index.php?oldid=" + diffOld + "'>" + diffOld + "</a></span><span style='float:right;'>(" + diffSize + ")&nbsp;&nbsp;<a href='" + serv_url + "/w/index.php?oldid=" + diffNew + "'>" + diffNew + "</a></span></p><br>" +
"<p style='text-align:left; display:inline;'><span style='float:left;'><a href='" + serv_url + "/wiki/User:" + fromUser + "'>" + fromUser + "</a></span><span style='float:right;'><a href='" + serv_url + "/wiki/User:" + diffUser + "'>" + diffUser + "</a></span></p>";

                (webBrowser.Child as System.Windows.Forms.WebBrowser).DocumentText = diffStart + preDiff + prediffEnd + diff + diffEnd;
                buttonRevert.IsEnabled = true;
                buttonRevertCustom.IsEnabled = true;
            }
        }
    
        private void buttonRevert_Click(object sender, RoutedEventArgs e)
        {
            Revert();
        }
        private void buttonRevertCustom_Click(object sender, RoutedEventArgs e)
        {
            customReason = Interaction.InputBox("Custom reason", "Write here the reason", "");
            if (customReason != "" && customReason != null)
                Revert();
        }
        private void openBrowser_Click(object sender, RoutedEventArgs e)
        {
            if (oldid == null || oldid == "")
                Process.Start(pageUrl);
            else
            {
                if (newid != null || newid == "")
                    Process.Start(scriptUrl + "/index.php?diff=" + newid + "&oldid=" + oldid);
            }
        }
        private void help_Click(object sender, RoutedEventArgs e)
        {
            help help = new help();
            help.ShowDialog();
        }
        private void about_Click(object sender, RoutedEventArgs e)
        {
            AboutBox1 abotBox = new AboutBox1();
            abotBox.ShowDialog();
        }
        private void clear_Click(object sender, RoutedEventArgs e)
        {
            edits.Clear();
            countEls = 0;
            statusText1.Text = "Unverified edits: " + countEls.ToString();
        }
        private void whitelist_Click(object sender, RoutedEventArgs e)
        {
            wind2.ShowDialog();
        }
        private void feedback_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://meta.wikimedia.org/wiki/User_talk:Iluvatar");
        }
        private void stat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Вызов статистики использования
                var getStat = new WebClient();
                getStat.Headers.Add(useragent);
                var stats = getStat.DownloadString("https://tools.wmflabs.org/iluvatarbot/swviewer/index.php?key=fkrMpQNmAC");
                MessageBox.Show("App was runs by " + stats + "users", "Statistics");
            }
            catch (WebException) { MessageBox.Show("No internet connection or server is not available"); }
        }
        private static string CalculateChecksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                byte[] checksum = md5.ComputeHash(stream);
                return (BitConverter.ToString(checksum).Replace("-", string.Empty));
            }
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}