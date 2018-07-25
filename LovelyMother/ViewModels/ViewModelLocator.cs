using GalaSoft.MvvmLight.Ioc;
using LovelyMother.Services;
using UvpClient.Services;

namespace LovelyMother.ViewModels
{
    /// <summary>
    ///     ViewModel定位器。
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        ///     ViewModel定位器单件。
        /// </summary>
        public static readonly ViewModelLocator Instance =
            new ViewModelLocator();

        /// <summary>
        ///     构造函数。
        /// </summary>
        private ViewModelLocator()
        {
            SimpleIoc.Default.Register<IMotherService, MotherService>();
            SimpleIoc.Default.Register<IIdentityService, IdentityService>();
            SimpleIoc.Default.Register<IProcessService, ProcessService>();
            SimpleIoc.Default.Register<ProcessViewModel>();
            SimpleIoc.Default.Register<ProgressViewModel>();
            SimpleIoc.Default.Register<TaskViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
        }



        /// <summary>
        ///     获得登录ViewModel。
        /// </summary>
        public LoginViewModel LoginViewModel =>
            SimpleIoc.Default.GetInstance<LoginViewModel>();

        public ProcessViewModel ProcessViewModel =>
            SimpleIoc.Default.GetInstance<ProcessViewModel>();

        public ProgressViewModel ProgressViewModel =>
            SimpleIoc.Default.GetInstance<ProgressViewModel>();

        public TaskViewModel TaskViewModel =>
            SimpleIoc.Default.GetInstance<TaskViewModel>();




    }



}
