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
using AVOSCloud;
using Language;
using System.Text.RegularExpressions;

namespace WowsExclamation
{
    /// <summary>
    /// SignInUp.xaml 的交互逻辑
    /// </summary>
    public partial class SignInUp : Window
    {
        bool IsSignUp = false;
        MainWindow mWindow = null;
        bool IN_usernamePassed, IN_passwordPassed, UP_usernamePassed, UP_passwordPassed, UP_emailPassed;

        private const string USERNAME_REGEX = @"[0-9a-zA-Z_]{4,16}";
        private const string PASSWORD_REGEX = @"[0-9a-zA-Z!@#$%^&*()_+\-=\[\]\\\{\}|;':"",./<>\?]{6,30}";
        private const string EMAIL_REGEX = @".+?@.+\..{2,4}";

        public SignInUp(MainWindow mWindow)
        {
            InitializeComponent();
            this.mWindow = mWindow;
            IN_usernameHint.Text = mWindow.langPackage[LanguageSign.UserName];
            UP_usernameHint.Text = mWindow.langPackage[LanguageSign.UserName];
            IN_passwordHint.Text = mWindow.langPackage[LanguageSign.Password];
            UP_passwordHint.Text = mWindow.langPackage[LanguageSign.Password];
            UP_signupBtn.Content = mWindow.langPackage[LanguageSign.SignUp];
            UP_cancleBtn.Content = mWindow.langPackage[LanguageSign.Cancle];
            IN_cancleBtn.Content = mWindow.langPackage[LanguageSign.Cancle];
            IN_signinBtn.Content = mWindow.langPackage[LanguageSign.SignIn];
            UP_emailHint.Text = mWindow.langPackage[LanguageSign.Email];
            this.Title = mWindow.langPackage[LanguageSign.SignUpOrSignIn];
            UP_usernameInput.Hint = mWindow.langPackage[LanguageSign.UsernameClaim];
            UP_passwordInput.Hint = mWindow.langPackage[LanguageSign.PasswordClaim];
        }

        private void switchToSignup_MouseEnter(object sender, MouseEventArgs e)
        {
            switchToSignup.Background = new SolidColorBrush(Colors.Black);
            switchToSignup.Background.Opacity = 0.2;
        }

        private void switchToSignup_MouseLeave(object sender, MouseEventArgs e)
        {
            switchToSignup.Background = null;
        }

        private void switchToSignup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(switchToSignup.Background == null)
                switchToSignup.Background = new SolidColorBrush(Colors.Black);
            switchToSignup.Background.Opacity = 0.4;
        }

        private void switchToSignup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsSignUp) return;
            IsSignUp = true;
            switchToSignup.Background = null;

            ThicknessAnimation animation = new ThicknessAnimation();
            animation.To = new Thickness(0, signupPage.Margin.Top, 0, signupPage.Margin.Bottom);
            animation.DecelerationRatio = 1;

            signupPage.BeginAnimation(MarginProperty, animation);
        }

        private void Path_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (back.Background == null)
                back.Background = new SolidColorBrush(Colors.Black);
            back.Background.Opacity = 0.4;
        }

        private void Path_MouseEnter(object sender, MouseEventArgs e)
        {
            back.Background = new SolidColorBrush(Colors.Black);
            back.Background.Opacity = 0.2;
        }

        private void Path_MouseLeave(object sender, MouseEventArgs e)
        {
            back.Background = null;
        }

        private void Path_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsSignUp) return;
            IsSignUp = false;
            switchToSignup.Background = null;

            ThicknessAnimation animation = new ThicknessAnimation();
            animation.To = new Thickness(signupPage.ActualWidth, signupPage.Margin.Top, -signupPage.ActualWidth, signupPage.Margin.Bottom);
            animation.AccelerationRatio = 1;

            signupPage.BeginAnimation(MarginProperty, animation);
        }

        private async void IN_signinBtn_Click(object sender, RoutedEventArgs e)
        {
            IN_usernameInput.IsEnabled = false;
            IN_passwordInput.IsEnabled = false;
            IN_signinBtn.IsEnabled = false;
            IN_cancleBtn.IsEnabled = false;
            switchToSignup.Visibility = Visibility.Collapsed;
            try
            {
                var user = await LeanCloudInteraction.LeanCloudInteraction.SingIn(IN_usernameInput.Text, IN_passwordInput.Password);
                this.mWindow.mUser = user;
                this.Close();
            }catch(AVException ex)
            {
                switch ((int)ex.Code)
                {
                    case (int)LeanCloudInteraction.ErrorCodes.CantConnect:
                        System.Windows.Forms.MessageBox.Show(mWindow.langPackage[LanguageSign.CantConnect]);
                        break;
                    case (int)LeanCloudInteraction.ErrorCodes.UnPwError:
                        System.Windows.Forms.MessageBox.Show(mWindow.langPackage[LanguageSign.UnPsError]);
                        break;
                    default:
                        System.Windows.Forms.MessageBox.Show(ex.Message + " 错误代码:" + ((int)ex.Code).ToString() + "");
                        break;
                }
                Loger.Loger.Log("Signin faild, code: " + ex.Code + "(" + (int)ex.Code + ")");
            }
            IN_usernameInput.IsEnabled = true;
            IN_passwordInput.IsEnabled = true;
            IN_signinBtn.IsEnabled = true;
            IN_cancleBtn.IsEnabled = true;
            switchToSignup.Visibility = Visibility.Visible;
        }

        private async void UP_signupBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = await LeanCloudInteraction.LeanCloudInteraction.SignUp(UP_usernameInput.Text, UP_passwordInput.Password, UP_emailInput.Text);
                mWindow.mUser = user;
                this.Close();
            } catch (AVException ex)
            {
                switch ((int)ex.Code)
                {
                    case (int)LeanCloudInteraction.ErrorCodes.CantConnect:
                        System.Windows.Forms.MessageBox.Show(mWindow.langPackage[LanguageSign.CantConnect]);
                        break;
                    case (int)LeanCloudInteraction.ErrorCodes.EmailFormatErr:
                        System.Windows.Forms.MessageBox.Show(mWindow.langPackage[LanguageSign.EmailFormatErr]);
                        break;
                    case (int)LeanCloudInteraction.ErrorCodes.EmailToken:
                        System.Windows.Forms.MessageBox.Show(mWindow.langPackage[LanguageSign.EmailAddressToken]);
                        break;
                    default:
                        System.Windows.Forms.MessageBox.Show(ex.Message + " 错误代码:" + ((int)ex.Code).ToString() + "");
                        break;
                }
                Loger.Loger.Log("Signup faild, code: " + ex.Code + "(" + (int)ex.Code + ")");
            }
        }

        private void cancleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IN_usernameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            IN_usernamePassed = CheckUsername(IN_usernameInput.Text);
            if (IN_usernamePassed)
            {
                IN_usernameHint.Foreground = new SolidColorBrush(Colors.Black);
            }else
            {
                IN_usernameHint.Foreground = new SolidColorBrush(Colors.Red);
            }
            IN_signinBtn.IsEnabled = IN_usernamePassed && IN_passwordPassed;
        }

        private void IN_passwordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            IN_passwordPassed = CheckPassword(IN_passwordInput.Password);
            if (IN_passwordPassed)
            {
                IN_passwordHint.Foreground = new SolidColorBrush(Colors.Black);
            } else
            {
                IN_passwordHint.Foreground = new SolidColorBrush(Colors.Red);
            }
            IN_signinBtn.IsEnabled = IN_usernamePassed && IN_passwordPassed;
        }

        private void UP_emailInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            UP_emailPassed = CheckEmail(UP_emailInput.Text);
            if (UP_emailPassed)
            {
                UP_emailHint.Foreground = new SolidColorBrush(Colors.Black);
            } else
            {
                UP_emailHint.Foreground = new SolidColorBrush(Colors.Red);
            }
            UP_signupBtn.IsEnabled = UP_usernamePassed && UP_passwordPassed && UP_emailPassed;
        }

        private void UP_passwordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UP_passwordPassed = CheckPassword(UP_passwordInput.Password);
            if (UP_passwordPassed)
            {
                UP_passwordHint.Foreground = new SolidColorBrush(Colors.Black);
            } else
            {
                UP_passwordHint.Foreground = new SolidColorBrush(Colors.Red);
            }
            UP_signupBtn.IsEnabled = UP_usernamePassed && UP_passwordPassed && UP_emailPassed;
        }

        private void UP_usernameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            UP_usernamePassed = CheckUsername(UP_usernameInput.Text);
            if (UP_usernamePassed)
            {
                UP_usernameHint.Foreground = new SolidColorBrush(Colors.Black);
            } else
            {
                UP_usernameHint.Foreground = new SolidColorBrush(Colors.Red);
            }
            UP_signupBtn.IsEnabled = UP_usernamePassed && UP_passwordPassed && UP_emailPassed;
        }

        private bool CheckUsername(string username)
        {
            if (username == "") return false;
            return Regex.Match(username, USERNAME_REGEX).Value == username;
        }
        private bool CheckPassword(string password)
        {
            if (password == "") return false;
            return Regex.Match(password, PASSWORD_REGEX).Value == password;
        }
        private bool CheckEmail(string email)
        {
            if (email == "") return false;
            return Regex.Match(email, EMAIL_REGEX).Value == email;
        }
    }
}
