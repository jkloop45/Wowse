using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVOSCloud;
using System.Security.Cryptography;

namespace LeanCloudInteraction
{
    static public class LeanCloudInteraction
    {
        static public void Initialize()
        {
            try
            {
                AVClient.Initialize(Consts.APP_ID, Consts.APP_KEY);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        static public async Task<AVUser> SignUp(string userName, string password, string email)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    var data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                    var sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                        sBuilder.Append(data[i].ToString("x2"));
                    password = sBuilder.ToString();
                }

                var user = new AVUser()
                {
                    Username = userName,
                    Password = password,
                    Email = email,
                };
                await user.SignUpAsync();
                return user;
            }catch(AVException ex)
            {
                throw ex;
            }
        }

        static async public Task<AVUser> SingIn(string userName, string password)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    var data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                    var sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                        sBuilder.Append(data[i].ToString("x2"));
                    password = sBuilder.ToString();
                }

                AVUser user = await AVUser.LogInAsync(userName, password);
                return user;
            } catch (AVException ex)
            {
                throw ex;
            }
        }
    }
}
