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
    /// ImageBtn.xaml 的交互逻辑
    /// </summary>
    public partial class ImageBtn : UserControl
    {
        public string Text { get; set; }
        public ImageSource Source { get; set; }
        public ImageBtn()
        {
            InitializeComponent();
        }

        private void content_MouseEnter(object sender, MouseEventArgs e)
        {
            content.Background = new SolidColorBrush(Colors.White);
            content.Background.Opacity = 0.2;
        }

        private void content_MouseLeave(object sender, MouseEventArgs e)
        {
            content.Background = null;
        }

        private void content_MouseDown(object sender, MouseButtonEventArgs e)
        {
            content.Background = new SolidColorBrush(Colors.Black);
            content.Background.Opacity = 0.2;
        }

        private void content_MouseUp(object sender, MouseButtonEventArgs e)
        {
            content.Background = new SolidColorBrush(Colors.White);
            content.Background.Opacity = 0.2;
        }
    }
}
