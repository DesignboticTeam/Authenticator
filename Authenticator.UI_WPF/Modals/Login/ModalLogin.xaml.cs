using System.Windows.Controls;
using WPFLocalizeExtension.Providers;

namespace Authenticator.Components
{
    /// <summary>
    /// Interaction logic for ModalLogin.xaml  
    /// </summary>
    public partial class ModalLogin : UserControl
    {
        public ModalLogin()
        {

            InitializeComponent();

            var test = ResxLocalizationProvider.GetDefaultDictionary(this);
           // var test2 = ResxLocalizationProvider.Instance.GetLocalizedObject();
        }
    }
}
