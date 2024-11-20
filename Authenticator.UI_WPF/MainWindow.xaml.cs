using System.Windows;
using System.Windows.Media.Imaging;
using Connector.DataModels;
using Microsoft.Extensions.Options;

namespace Authenticator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //SettingsAndProps?
        public MainWindow(IOptions<AppSettings> appSettings)
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri(appSettings.Value.FullIconPath));
        }
    }
}