using System.Windows;
using Windows.UI.Xaml;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using LovelyMother.Services;
using LovelyMother.ViewModels;


namespace LovelyMother
{
    public class ViewModelLocator
    {
        public static ViewModelLocator Instance
        {
            get
            {
                return Application.Current.Resources["Locator"] as ViewModelLocator;
            }
        }

        public MainPageViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<IMotherService, MotherService>();

            /*SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();*/
            SimpleIoc.Default.Register<MainPageViewModel>();
            //SimpleIoc.Default.Register<>();

        }
    }
}
