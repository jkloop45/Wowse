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
using Controls.NavigationBox;
using System.IO;
using WWebsiteInteration;
using System.Timers;
using AVOSCloud;
using Language;
using Loger;

namespace WowsExclamation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string[] IMG_PATHES;
        int ImgIndex = 0;
        AVUser _mUser;
        internal AVUser mUser
        {
            get { return _mUser; }
            set
            {
                _mUser = value;
                RefrushUser();
            }
        }
        internal LanguagePackage langPackage { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LeanCloudInteraction.LeanCloudInteraction.Initialize();

            Loger.Loger.Log("Wowse Started.");

            langPackage = new LanguagePackage(@"lang\zh-cn.lang");
            langPackage.LoadPackage();

            userName.Text = langPackage[LanguageSign.SignIn];
            Loger.Loger.Log("Language package loaded, count: " + langPackage.Count);

            mNavigationBox.AddItem(new NavigationItem() { Text = langPackage[LanguageSign.Home], BackColor = Colors.Black, TextColor = Colors.White, BackColorOpacity = 0 });
            mNavigationBox.AddItem(new NavigationItem() { Text = langPackage[LanguageSign.InstallPlugin], BackColor = Colors.Black, TextColor = Colors.White, BackColorOpacity = 0 });
            mNavigationBox.AddItem(new NavigationItem() { Text = langPackage[LanguageSign.InstalledPlugin], BackColor = Colors.Black, TextColor = Colors.White, BackColorOpacity = 0 });
            mNavigationBox.AddItem(new NavigationItem() { Text = langPackage[LanguageSign.GameSettings], BackColor = Colors.Black, TextColor = Colors.White, BackColorOpacity = 0 });
            mNavigationBox.AddItem(new NavigationItem() { Text = langPackage[LanguageSign.Settings], BackColor = Colors.Black, TextColor = Colors.White, BackColorOpacity = 0 });
            mNavigationBox.AddItem(new NavigationItem() { Text = langPackage[LanguageSign.Account], BackColor = Colors.Black, TextColor = Colors.White, BackColorOpacity = 0 });

            IMG_PATHES = Event.SaveEventImages_China().ToArray();

            Loger.Loger.Log("Event image count: " + IMG_PATHES.Length);

            if (File.Exists(IMG_PATHES[ImgIndex]) && IMG_PATHES.Length != 0)
            {
                blur.Background = new ImageBrush(new BitmapImage(new Uri(IMG_PATHES[ImgIndex])));
                ImgIndex = ImgIndex == IMG_PATHES.Length - 1 ? 0 : ImgIndex++;
            }

            var timer = new Timer();
            timer.Interval = 10000;
            timer.Elapsed += (sender, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (IMG_PATHES.Length != 0) {
                        while (true)
                        {
                            if (File.Exists(IMG_PATHES[ImgIndex]))
                            {
                                blur.Background = new ImageBrush(new BitmapImage(new Uri(IMG_PATHES[ImgIndex])));
                                ImgIndex = ImgIndex == IMG_PATHES.Length - 1 ? 0 : ImgIndex + 1;
                                break;
                            }
                            if (ImgIndex == IMG_PATHES.Length - 1)
                            {
                                ImgIndex = 0;
                                break;
                            } else
                                ImgIndex++;
                        }
                    }
                });
            };
            timer.Start();
        }

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
            new SignInUp(this).ShowDialog();
        }
    }
}
