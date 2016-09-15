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
using System.Windows.Shapes;

namespace WowsExclamation
{
    /// <summary>
    /// RecordQuery.xaml 的交互逻辑
    /// </summary>
    public partial class RecordQuery : Window
    {
        public RecordQuery()
        {
            InitializeComponent();
            winningPie.LabelFormatter = (value) =>
            {
                return value.ToString() + " %";
            };
            shipLevelAxis.LabelFormatter = (value) =>
            {
                string s = "";
                if (value < 5d)
                    for (int i = 1; i <= value; i++)
                        s += "I";
                if (value >= 5d && value < 9d)
                {
                    s = "V";
                    for (int i = 1; i <= value - 5; i++)
                        s += "I";
                }
                if (value == 9)
                    s = "IX";
                if (value == 10)
                    s = "X";
                return s;
            };
        }
    }
}
