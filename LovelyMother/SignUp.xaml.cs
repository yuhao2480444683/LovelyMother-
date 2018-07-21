using LovelyMother.Services;
using Microsoft.EntityFrameworkCore;
using MotherLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Ioc;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace LovelyMother
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SignUp : Page
    {
        public SignUp()
        {
            this.InitializeComponent();
        }
        private async void Button1_OnClick(object sender, RoutedEventArgs e)

        {

            MotherService motherService = new MotherService();

            var result = await motherService.NewUserAsync(SignupUser.Text, SignupPassword.Text);

            if (result == true)

            {
                SignupUser.Text = "创建成功！";
            }

            else

            {
                SignupUser.Text = "创建失败！";
            }

        }



        private async void QButton_OnClick(object sender, RoutedEventArgs e)

        {

            using (var db = new MyDatabaseContext())

            {
                QButton.Content = db.Users.Count();
                /*测试用
                var Announcement = await db.Users.FirstOrDefaultAsync(m => m.UserName == SignupUser.Text);
                
                if (Announcement != null)

                {

                    SignupUser.Text = Announcement.Id.ToString();
                }
                */
            }

        }



        private async void DButton_OnClick(object sender, RoutedEventArgs e)

        {

            using (var db = new MyDatabaseContext())

            {

                var Announcement = await db.Users.FirstOrDefaultAsync(m => m.UserName == SignupUser.Text);

                if (Announcement != null)
                {
                    db.Users.Remove(Announcement);

                    await db.SaveChangesAsync();
                }           
                else

                {
                    SignupUser.Text = "该用户不存在";
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
