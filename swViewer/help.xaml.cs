using System.Diagnostics;
using System.Windows;

namespace swViewer
{
    public partial class help : Window
    {
        public help()
        {
            InitializeComponent();
            tagsBlock.Text = "{{delete|No useful content - ~~~~}}" + System.Environment.NewLine +
                "{{delete|nonsense - ~~~~}}" + System.Environment.NewLine +
                "{{delete|no article - ~~~~}}" + System.Environment.NewLine +
                "{{delete|spam - ~~~~}}" + System.Environment.NewLine +
                "{{delete|Blanked by author - ~~~~}}";
        }

        private void GlobalContrib(object sender, RoutedEventArgs e)
        {
                Process.Start("https://tools.wmflabs.org/guc/?by=date&user=");
        }

        private void SULInfo(object sender, RoutedEventArgs e)
        {
            Process.Start("https://tools.wmflabs.org/quentinv57-tools/tools/sulinfo.php?username=");
        }
        private void Range(object sender, RoutedEventArgs e)
        {
            Process.Start("https://tools.wmflabs.org/rangecontrib/?wiki=");
        }
        private void VR(object sender, RoutedEventArgs e)
        {
            Process.Start("https://meta.wikimedia.org/wiki/Vandalism_reports");
        }
        private void MS(object sender, RoutedEventArgs e)
        {
            Process.Start("https://meta.wikimedia.org/wiki/Steward_requests/Miscellaneous");
        }

        private void GoogleT(object sender, RoutedEventArgs e)
        {
            Process.Start("https://translate.google.com/#auto/en/");
        }
        private void YandexT(object sender, RoutedEventArgs e)
        {
            Process.Start("https://translate.yandex.com/");
        }
        private void PromtT(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.online-translator.com");
        }
        private void BingT(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.bing.com/translator");
        }

        private void PT(object sender, RoutedEventArgs e)
        {
            Process.Start("https://meta.wikimedia.org/wiki/User:Hoo_man/Scripts/Tagger");
        }


    }
}
