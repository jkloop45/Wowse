using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using System.IO;
using Loger;

namespace WWebsiteInteration
{
    static public class Event
    {
        const string EVENT_IMG_REGEX = @"<ul\sclass=""imgL"">[\s\S]+?</ul>";
        const string IMG_REGEX = @"(?<=<li><img src="").+?(?="")";
        const string WOWS_URL_CHINA = @"http://wows.kongzhong.com/wows.html";
        public static readonly string PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApp‌​licationData) + @"\Wows!\Image\";

        /// <summary>
        /// 获取Wows官网活动的图片, 会将获取到的图片保存到PATH文件夹中.
        /// </summary>
        /// <returns>图片路径</returns>
        static public List<string> SaveEventImages_China()
        {
            string source = WebInteraction.GET(WOWS_URL_CHINA);
            string paragraph = Regex.Match(source, EVENT_IMG_REGEX).Value;
            MatchCollection matches = Regex.Matches(paragraph, IMG_REGEX);
            List<string> imgPathes = new List<string>();

            for (int i = 0; i < matches.Count; i++)
            {
                Match m = matches[i];
                imgPathes.Add(SaveImg(m.Value));
            }
            return imgPathes;
        }
        private static string SaveImg(string Url)
        {
            var tmp = Url.Split('/');
            var filename = tmp[tmp.Length - 1];
            var path = PATH + filename;
            if (!Directory.Exists(PATH))
                Directory.CreateDirectory(PATH);

            BitmapImage img = new BitmapImage(new Uri(Url, UriKind.RelativeOrAbsolute));
            img.DownloadCompleted += (sender, e) =>
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapImage)sender));
                try
                {
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        encoder.Save(filestream);
                    }
                } catch(Exception ex) {
                    Loger.Loger.Log(ex.Message);
                }
            };
            return path;
        }
    }
}
