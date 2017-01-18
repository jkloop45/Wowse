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
    /// ImageButton.xaml 的交互逻辑
    /// </summary>
    public partial class ImageButton : UserControl
    {
        public string Text
        {
            get
            {
                return content.Text;
            }
            set
            {
                content.Text = value;
            }
        }
        public Brush TextForeground
        {
            get
            {
                return content.Foreground;
            }
            set
            {
                content.Foreground = value;
            }
        }
        public double TextFontSize
        {
            get
            {
                return content.FontSize;
            }
            set
            {
                content.FontSize = value;
            }
        }
        public ImageSource Source
        {
            get
            {
                return bg.Source;
            }
            set
            {
                bg.Source = value;
            }
        }
        public ImageButton()
        {
            InitializeComponent();
        }
    }
}
