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
using System.Windows.Media.Animation;
using LiveCharts;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Language;
using WWebsiteInteration.RecordQuery.Resaults;
using WWebsiteInteration.RecordQuery.Actions;

namespace WowsExclamation
{
    /// <summary>
    /// RecordQuery.xaml 的交互逻辑
    /// </summary>
    public partial class RecordQuery : Window
    {
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        public bool IsClosing = false;

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        public RecordQuery(MainWindow parent)
        {
            InitializeComponent();
            rankingPie.LabelFormatter = (value) => value.ToString() + " %";
            shipLevelAxis.LabelFormatter = (value) =>
            {
                value++;
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

            

            this.Top = (SystemParameters.WorkArea.Height - this.Height) / 2;
            this.Left = SystemParameters.FullPrimaryScreenWidth;

            rankingPie.Margin = GetLastMargin(rankingPie);
            shipLvl.Margin = GetLastMargin(shipLvl);
            shipTypes.Margin = GetLastMargin(shipTypes);
            rankingPieHint.Margin = GetLastMargin(rankingPieHint);
            idLbl.Margin = GetLastMargin(idLbl);

            this.rankingPieHint.Text = parent.langPackage[LanguageSign.Ranking];
        }

        private void DoShowRecordAnimation()
        {
            idLbl.Text = idInput.Text;
            Keyboard.ClearFocus();
            SetWindowExTransparent();

            var ta = new ThicknessAnimation();
            ta.DecelerationRatio = 1;
            ta.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            ta.To = GetNextMargin(idHint);
            idHint.BeginAnimation(MarginProperty, ta);

            ta.To = GetNextMargin(idInput);
            idInput.BeginAnimation(MarginProperty, ta);

            ta.To = GetNextMargin(rankingPie);
            rankingPie.BeginAnimation(MarginProperty, ta);

            ta.To = GetNextMargin(shipLvl);
            shipLvl.BeginAnimation(MarginProperty, ta);

            ta.To = GetNextMargin(shipTypes);
            shipTypes.BeginAnimation(MarginProperty, ta);

            ta.To = GetNextMargin(rankingPieHint);
            rankingPieHint.BeginAnimation(MarginProperty, ta);

            ta.To = GetNextMargin(idLbl);
            idLbl.BeginAnimation(MarginProperty, ta);
        }

        private void SetWindowExTransparent()
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT | WS_EX_NOACTIVATE);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var da = new DoubleAnimation();
            da.From = SystemParameters.FullPrimaryScreenWidth;
            da.To = SystemParameters.WorkArea.Width - this.Width;
            da.DecelerationRatio = 1;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            this.BeginAnimation(Window.LeftProperty, da);

            this.Activate();
            idInput.Focus();
        }

        private Thickness GetNextMargin(FrameworkElement ele)
        {
            return new Thickness(ele.Margin.Left - this.Width, ele.Margin.Top, ele.Margin.Right + this.Width, ele.Margin.Bottom);
        }
        private Thickness GetLastMargin(FrameworkElement ele)
        {
            return new Thickness(ele.Margin.Left + this.Width, ele.Margin.Top, ele.Margin.Right - this.Width, ele.Margin.Bottom);
        }

        private async void idInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (idInput.Text == string.Empty) return;
            if (e.Key == Key.Enter)
            {
                idInput.IsEnabled = false;
                RecordQuerier rq;
                if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                {
                    // 北区
                    rq = new RecordQuerier(idInput.Text.Trim(), false);
                }else
                {
                    // 南区
                    rq = new RecordQuerier(idInput.Text.Trim(), true);
                }
                var data = await rq.LoadRecordAsync();
                if (data == null)
                {
                    idInput.Hint = "用户不存在";
                    idInput.Text = "";
                    idInput.IsEnabled = true;
                    return;
                }
                LoadRecord(data);
            }
        }

        private void LoadRecord(QueryResult data)
        {
            rankingPie.Value = data.Ranking;
            shipLevels.Values = new ChartValues<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 10; i++)
            {
                shipLevels.Values[i] = data.LvlShipCount[i];

                if (data.LvlShipVicPer[i] == 0) continue;
                shipPov.Values[i] = Convert.ToDouble(data.LvlShipVicPer[i]) / Convert.ToDouble(data.LvlShipCount[i]);
                shipPov.Values[i] = (double)shipPov.Values[i] * 100d;
            }
            cvCount.Values = new ChartValues<int> { data.CV };
            caCount.Values = new ChartValues<int> { data.CA };
            bbCount.Values = new ChartValues<int> { data.BB };
            ddCount.Values = new ChartValues<int> { data.DD };

            DoShowRecordAnimation();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.Left == SystemParameters.FullPrimaryScreenWidth) return;
            IsClosing = true;
            e.Cancel = true;
            var da = new DoubleAnimation();
            da.To = SystemParameters.FullPrimaryScreenWidth;
            da.AccelerationRatio = 1;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            da.Completed += (s, ev) => this.Close();

            this.BeginAnimation(Window.LeftProperty, da);
        }
    }
}
