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

namespace Controls
{
    /// <summary>
    /// HintTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class HintPasswordBox : UserControl
    {
        public string Hint
        {
            get
            {
                return hint.Text;
            }
            set
            {
                hint.Text = value;
            }
        }
        public string Password
        {
            get
            {
                return pBox.Password;
            }
            set
            {
                pBox.Password = value;
            }
        }
        public event RoutedEventHandler PasswordChanged;

        public HintPasswordBox()
        {
            InitializeComponent();
        }

        private void pBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            hint.Visibility = pBox.Password.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
            PasswordChanged(this, e);
        }
    }
}
