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

namespace Controls.NavigationBox
{
    /// <summary>
    /// NavigationItem.xaml 的交互逻辑
    /// </summary>
    public partial class NavigationItem : UserControl
    {
        public string Text
        {
            get { return name.Text; }
            set { name.Text = value; }
        }
        public Color TextColor
        {
            get { return ((SolidColorBrush)name.Foreground).Color; }
            set { name.Foreground = new SolidColorBrush(value); }
        }
        public Color BackColor
        {
            get { return ((SolidColorBrush)mGrid.Background).Color; }
            set { mGrid.Background = new SolidColorBrush(value); }
        }
        public Color SideRectColor
        {
            get { return ((SolidColorBrush)sideRect.Fill).Color; }
            set { sideRect.Fill = new SolidColorBrush(value); }
        }
        public double BackColorOpacity
        {
            get { return mGrid.Background.Opacity; }
            set { mGrid.Background.Opacity = value; }
        }

        public Grid Page { get; set; }

        internal void HideRect()
        {
            sideRect.Visibility = Visibility.Hidden;
        }

        internal void ShowRect()
        {
            sideRect.Visibility = Visibility.Visible;
        }

        public NavigationItem()
        {
            InitializeComponent();
        }
    }
}
