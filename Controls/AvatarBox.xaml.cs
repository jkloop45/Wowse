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
    /// AvatarBox.xaml 的交互逻辑
    /// </summary>
    public partial class AvatarBox : UserControl
    {
        public ImageSource Source {
            get { return mImage.Source; }
            set {
                mImage.Source = value;
                if (value != null)
                {
                    defaultAvatar.Visibility = Visibility.Collapsed;
                    this.Clip = new RectangleGeometry(new Rect(0, 0, this.Width, this.Height), this.Width / 2, this.Height / 2);
                } else
                {
                    defaultAvatar.Visibility = Visibility.Visible;
                }
            }
        }

        public AvatarBox()
        {
            InitializeComponent();
        }
    }
}
