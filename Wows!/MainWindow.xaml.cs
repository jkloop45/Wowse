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
using WWebsiteInteration.RecordQuery.Actions;
using WWebsiteInteration.RecordQuery.Resaults;
using WWebsiteInteration.RecordQuery;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Logcat;

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
        MashPopup loginMp;
        internal AVUser mUser
        {
            get { return _mUser; }
            set
            {
                _mUser = value;
                RefrushUser();
            }
        }
        private RecordQuery mRQuery;
        internal LanguagePackage langPackage { get; set; }

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint control, System.Windows.Forms.Keys keys);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int WM_HOTKEY = 0x0312;
        private const int HOTKEY_ID = 2333;
        private const int MOD_ALT = 0x0001;
        private const int MOD_CONTROL = 0x0002;

        #region 热键
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            try
            {
                IntPtr hWnd = new WindowInteropHelper(this).Handle;
                RegisterHotKey(hWnd, HOTKEY_ID, MOD_ALT | MOD_CONTROL, System.Windows.Forms.Keys.W);

                HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
                source.AddHook(WndProc);
            } catch (Exception ex)
            {
                Loger.Log(ex.ToString());
            }
        }
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY)
            {
                if (wParam.ToInt32() == HOTKEY_ID)
                {
                    if(mRQuery == null)
                    {
                        mRQuery = new RecordQuery(this);
                        mRQuery.Show();
                    }else
                    {
                        if (mRQuery.IsClosing) return IntPtr.Zero;
                        mRQuery.Closed += (sender, e) => mRQuery = null;
                        mRQuery.Close();
                    }
                    handled = true;
                }
            }
            return IntPtr.Zero;
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                LeanCloudInteraction.LeanCloudInteraction.Initialize();
            }catch
            {
                System.Windows.Forms.MessageBox.Show("Can not reach the server.");
            }

            Loger.Log("Wowse Started.");

            try
            {
                langPackage = new LanguagePackage(AppDomain.CurrentDomain.BaseDirectory + @"locale", @"zh-CN");
            }
            catch (Exception ex)
            {
                Loger.Log("Can't load language package. Detail: " + ex.ToString());
                System.Windows.Forms.MessageBox.Show("Can't load language package.");
                Environment.Exit(0);
            }

            //mNavigationBox.AddItem(new NavigationItem() { Text = langPackage[LanguageSign.Home], BackColor = Colors.Black, TextColor = Colors.White, BackColorOpacity = 0 });
            mNavigationBox.AddItem(new NavigationItem() { Text = langPackage[LanguageSign.InstallPlugin], BackColor = Colors.Black, TextColor = Colors.White, BackColorOpacity = 0 });
            mNavigationBox.AddItem(new NavigationItem() { Text = langPackage[LanguageSign.InstalledPlugin], BackColor = Colors.Black, TextColor = Colors.White, BackColorOpacity = 0 });
            mNavigationBox.AddItem(new NavigationItem() { Text = langPackage[LanguageSign.GameSettings], BackColor = Colors.Black, TextColor = Colors.White, BackColorOpacity = 0 });
            mNavigationBox.AddItem(new NavigationItem() { Text = langPackage[LanguageSign.Settings], BackColor = Colors.Black, TextColor = Colors.White, BackColorOpacity = 0 });
            mNavigationBox.AddItem(new NavigationItem() { Text = langPackage[LanguageSign.Account], BackColor = Colors.Black, TextColor = Colors.White, BackColorOpacity = 0 });

            gameLanchBtn.Content = langPackage[LanguageSign.LanchGame];
            userName.Text = langPackage[LanguageSign.SignIn];

            try
            {
                IMG_PATHES = Event.SaveEventImages_China().ToArray();

                Loger.Log("Event image count: " + IMG_PATHES.Length);

                //RecordQuerier rq = new RecordQuerier("QwQ_11", true);
                //rq.LoadRecord();
                //new RecordQuery().Show();

                if (IMG_PATHES.Length != 0 && File.Exists(IMG_PATHES[ImgIndex]))
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
                        if (IMG_PATHES.Length != 0)
                        {
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
            } catch { }
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
            var s = new SignInUp(this);
            loginMp = new MashPopup(this, s, new Thickness(0, titleBar.ActualHeight, 0, 0));
            s.mp = loginMp;
            loginMp.Show();
        }
    }
}
