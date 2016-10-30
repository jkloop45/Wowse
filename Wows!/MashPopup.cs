using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace WowsExclamation
{
    public class MashPopup
    {
        Window window;
        UserControl control;
        Thickness mashRect;
        Grid mash;

        public MashPopup(Window window, UserControl control, Thickness mashRect)
        {
            this.window = window;
            this.control = control;
            this.mashRect = mashRect;
        }
        public void Show()
        {
            if (mash != null) return;
            mash = new Grid();
            mash.Margin = mashRect;
            mash.Background = new SolidColorBrush(Colors.Black);
            mash.Background.Opacity = 0.5;
            mash.Opacity = 0;
            var h = (mash.ActualWidth - control.Width) / 2;
            var v = (mash.ActualHeight - control.Height) / 2;

            var grid = new Grid();
            grid.Margin = new Thickness(h, v, h, v);
            grid.Children.Add(control);

            var effect = new DropShadowEffect();
            effect.Opacity = 0.5;
            effect.ShadowDepth = 3;
            effect.Color = Colors.Black;
            grid.Effect = effect;

            var da = new DoubleAnimation();
            da.To = 1;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            mash.Children.Add(grid);
            ((Grid)window.Content).Children.Add(mash);
            mash.BeginAnimation(Grid.OpacityProperty, da);
        }
        public void Hide()
        {
            if (mash == null) return;
            mash.Dispatcher.Invoke(() => {
                var da = new DoubleAnimation();
                da.To = 0;
                da.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                da.Completed += (sender, e) => ((Grid)window.Content).Children.Remove(mash);

                mash.BeginAnimation(Grid.OpacityProperty, da);
            });
        }
        //~MashPopup()
        //{
        //    Hide();
        //}
    }
}
