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
    public partial class HintTextBox : UserControl
    {
        public string Hint {
            get
            {
                return hint.Text;
            }
            set
            {
                hint.Text = value;
            }
        }
        public string Text
        {
            get
            {
                return tBox.Text;
            }
            set
            {
                tBox.Text = value;
            }
        }
        public Brush TboxForeground
        {
            get { return tBox.Foreground; }
            set { tBox.Foreground = value; }
        }
        public Brush HintForground
        {
            get { return hint.Foreground; }
            set { hint.Foreground = value; }
        }
        public event TextChangedEventHandler TextChanged;

        public HintTextBox()
        {
            InitializeComponent();
        }

        private void tBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged(this, e);
        }
    }
}
