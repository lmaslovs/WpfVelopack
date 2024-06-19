using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Velopack;
using Velopack.Sources;

namespace WpfVelopack.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UpdateManager mgr;
        private UpdateInfo? updateInfo;
        public MainWindow()
        {
            InitializeComponent();
            var source = new GithubSource("https://github.com/lmaslovs/WpfVelopack/releases/latest/download", "", false);
            mgr = new UpdateManager(source);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            try
            {
                updateInfo = await mgr.CheckForUpdatesAsync();
                if (updateInfo != null)
                {
                    Update.Visibility = Visibility.Visible;
                    var releaseEntry = updateInfo.TargetFullRelease;
                    NewVersion.Text = $"Update Version: {releaseEntry?.Version.ToString() ?? "No update"}";
                }
                else
                {
                    NewVersion.Text = "Update Version: No update!";
                }
                CurrentVersion.Text = $"Current version: {mgr.CurrentVersion}";
            }
            catch (Exception ex)
            {
                // Will catch exception while in development
                Debug.WriteLine("Application not installed!");
                Debug.WriteLine(ex.Message);

                CurrentVersion.Text = $"Current version: Not installed";
            }
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            // download new version
            NewVersion.Text = "Downloading!";
            Update.IsEnabled = false;
            if (updateInfo != null)
            {
                await mgr.DownloadUpdatesAsync(updateInfo);

                // install new version and restart app
                mgr.ApplyUpdatesAndRestart(updateInfo);
            }
        }
    }
}