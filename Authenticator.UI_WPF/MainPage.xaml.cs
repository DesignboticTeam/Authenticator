using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI_WPF.Components;

namespace Authenticator.UI_WPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        PageDataTempleSelector _pageSelector;
        /*
        public MainPage()
        {
            InitializeComponent();
            MainContent.ContentTemplateSelector = _pageSelector;
        }
        */
        public MainPage(PageDataTempleSelector pageSelector)
        {
            InitializeComponent();
            _pageSelector = pageSelector;  
            MainContent.ContentTemplateSelector = pageSelector;
        }
    }
}
