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
using Controls.NavigationBox;
using System.IO;
using WWebsiteInteration;
using System.Timers;
using AVOSCloud;
using Language;
using WWebsiteInteration.RecordQuery.Actions;
using WWebsiteInteration.RecordQuery.Resaults;
using WWebsiteInteration.RecordQuery;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Logcat;

namespace WowsExclamation
{
    public partial class MainWindow : Window
    {
        private void RefrushUser()
        {
            if (mUser == null) return;
            AVQuery<AVObject> query = new AVQuery<AVObject>("UserAvatars").WhereEqualTo("userObjectId", mUser.ObjectId);
            query.FindAsync().ContinueWith(t => {
                AVObject[] objs = t.Result.Cast<AVObject>().ToArray();
                if (objs.Length != 0)
                {
                    string avatarUrl = objs[0]["avatar"].ToString();
                    if (avatarUrl != "")
                    {
                        //var img = BitmapFrame.Create(new Uri(avatarUrl, UriKind.RelativeOrAbsolute));
                        //img.DownloadCompleted += (sender, e) =>
                        //{
                        //    avatar.Source = (BitmapImage)sender;
                        //};
                        avatar.Dispatcher.Invoke(() => avatar.Source = WebInteraction.GetImage(avatarUrl));
                    }
                }
            });

            userName.Text = mUser.Username;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void closeBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (closeBtn.Background == null)
                closeBtn.Background = new SolidColorBrush(Colors.Black);
            closeBtn.Background.Opacity = 0.4;
        }

        private void closeBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            closeBtn.Background = new SolidColorBrush(Colors.Black);
            closeBtn.Background.Opacity = 0.2;
        }

        private void closeBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            closeBtn.Background = null;
        }

        private void closeBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void userName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (mUser != null) return;
            userName.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void userName_MouseEnter(object sender, MouseEventArgs e)
        {
            if (mUser != null) return;
            userName.Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void userName_MouseLeave(object sender, MouseEventArgs e)
        {
            if (mUser != null) return;
            userName.Foreground = new SolidColorBrush(Colors.White);
        }

        private void userName_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mUser != null) return;
            userName.Foreground = new SolidColorBrush(Colors.White);
            var s = new SignInUp(this);
            loginMp = new MashPopup(this, s, new Thickness(0, titleBar.ActualHeight, 0, 0));
            s.mp = loginMp;
            loginMp.Show();
        }

        private void minisizeBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (minisizeBtn.Background == null)
                minisizeBtn.Background = new SolidColorBrush(Colors.Black);
            minisizeBtn.Background.Opacity = 0.4;
        }

        private void minisizeBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            minisizeBtn.Background = new SolidColorBrush(Colors.Black);
            minisizeBtn.Background.Opacity = 0.2;
        }

        private void minisizeBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            minisizeBtn.Background = null;
        }

        private void minisizeBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
