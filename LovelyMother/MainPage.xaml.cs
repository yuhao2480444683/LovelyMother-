using System;
using Windows.ApplicationModel;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Task = System.Threading.Tasks.Task;
using Windows.System;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Popups;
using GalaSoft.MvvmLight.Messaging;
using LovelyMother.Models;
using LovelyMother.ViewModels;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace LovelyMother
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public async Task LoadState()
        {
            var task = await StartupTask.GetAsync("AppAutoRun");
            this.tbState.Text = $"Status: {task.State}";
            switch (task.State)
            {
                case StartupTaskState.Disabled:
                    // 禁用状态
                    this.btnSetState.Content = "启用";
                    this.btnSetState.IsEnabled = true;
                    break;
                case StartupTaskState.DisabledByPolicy:
                    // 由管理员或组策略禁用
                    this.btnSetState.Content = "被系统策略禁用";
                    this.btnSetState.IsEnabled = false;
                    break;
                case StartupTaskState.DisabledByUser:
                    // 由用户手工禁用
                    this.btnSetState.Content = "被用户禁用";
                    this.btnSetState.IsEnabled = false;
                    break;
                case StartupTaskState.Enabled:
                    // 当前状态为已启用
                    this.btnSetState.Content = "已启用";
                    this.btnSetState.IsEnabled = false;
                    break;
            }
        }

        private async void btnSetState1_Click(object sender, RoutedEventArgs e)
        {
            var task = await StartupTask.GetAsync("LovelyMother");
            if (task.State == StartupTaskState.Disabled)
            {
                await task.RequestEnableAsync();
            }

            // 重新加载状态
           // await LoadState();
        }

        
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        public void Register()

        {
            this.InitializeComponent();
        }
        private async void ChoosePicture_Click(object sender, RoutedEventArgs e)

        {

            // 创建和自定义 FileOpenPicker
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail; //可通过使用图片缩略图创建丰富的视觉显示，以显示文件选取器中的文件
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".gif");

            //选取单个文件

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            //文件处理
            if (file != null)
            {
                var inputFile = SharedStorageAccessManager.AddFile(file);
                var destination = await ApplicationData.Current.LocalFolder.CreateFileAsync("Cropped.jpg", CreationCollisionOption.ReplaceExisting);//在应用文件夹中建立文件用来存储裁剪后的图像
                var destinationFile = SharedStorageAccessManager.AddFile(destination);
                var options = new LauncherOptions();
                options.TargetApplicationPackageFamilyName = "Microsoft.Windows.Photos_8wekyb3d8bbwe";
                //待会要传入的参数
                

                var parameters = new ValueSet();
                parameters.Add("InputToken", inputFile);                //输入文件
                parameters.Add("DestinationToken", destinationFile);    //输出文件            
                parameters.Add("ShowCamera", false);                    //它允许我们显示一个按钮，以允许用户采取当场图象
                parameters.Add("EllipticalCrop", true);                 //截图区域显示为圆（最后截出来还是方形）
                parameters.Add("CropWidthPixals", 300);
                parameters.Add("CropHeightPixals", 300);
                //调用系统自带截图并返回结果
                var result = await Launcher.LaunchUriForResultsAsync(new Uri("microsoft.windows.photos.crop:"), options, parameters);
                //按理说下面这个判断应该没问题呀，但是如果裁剪界面点了取消的话后面会出现异常，所以后面我加了try catch
                if (result.Status == LaunchUriStatus.Success && result.Result != null)
                {
                    //对裁剪后图像的下一步处理
                    try
                    {
                        // 载入已保存的裁剪后图片
                        var stream = await destination.OpenReadAsync();
                        var bitmap = new BitmapImage();
                        await bitmap.SetSourceAsync(stream);
                        // 显示
                        img.ImageSource = bitmap;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message + ex.StackTrace);
                    }
                }
            }
        }

       

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUp));
        }
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
        /// <summary>
        /// 倒计时。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Time_Click(object sender, RoutedEventArgs e)
        {
            if (E.Time <= S.Time)
            {
                await new MessageDialog("时间设置错误！！").ShowAsync();//弹窗。
            }
            else
            {
                Messenger.Default.Send(new RefreshMessage() { refreshMessage = "Begin" });
                int i = 0;
                var setTime = E.Time - S.Time;
                
                int TimeCount = setTime.Hours * 3600 + setTime.Minutes * 60;//倒计时秒数
                
                //传入的参数。
               
                    
                //await new MessageDialog().ShowAsync();
                //int TimeCount = 10;
                timer.Tick += new EventHandler<object>(async (sende, ei) =>
                {
                    await Dispatcher.TryRunAsync
                    (CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
                    {
                        i += 1;
                        double temp = (740 * Math.PI) * i / TimeCount / 15;

                        MyEllipse.StrokeDashArray = new DoubleCollection() { temp, 1000 };

                        txt.Text = ((TimeCount - i) / 3600).ToString("00") + ":"//文本显示。
                        + (((TimeCount - i) % 3600) / 60).ToString("00") + ":"
                        + (((TimeCount - i) % 3600) % 60).ToString("00");

                        if (i == TimeCount)
                        {
                            timer.Stop();
                            Messenger.Default.Send(new ListenMessage() { listenMessage = "End" });
                        }
                    }));

                });
                timer.Start();
            }

        }
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void End_Click(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send(new ListenMessage() { listenMessage = "End" });
            timer.Stop();
        }
        /// <summary>
        /// 骚扰。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Bling));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DiagnosticAccessStatus diagnosticAccessStatus = await AppDiagnosticInfo.RequestAccessAsync();
        }


        
        /// <summary>
        /// 跳转。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProgress_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Add));
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignIn));
        }
        /// <summary>
        /// ListView传递参数。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void List_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickTask = e.ClickedItem as MotherLibrary.Task;
            (this.DataContext as TaskViewModel).Begin = clickTask.Begin;
            (this.DataContext as TaskViewModel).Date = clickTask.Date;

        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TestPage));
        }
    }
}
