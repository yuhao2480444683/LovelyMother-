using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.ViewManagement;

namespace LovelyMother.TimeWrok
{
    class Time
    {
    }
    /// <summary>
    /// 进入全屏
    /// </summary>
    public class KeepTopCommand /*窗口置顶与取消*/ : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (ApplicationView.GetForCurrentView().ViewMode.Equals(ApplicationViewMode.Default))
            {

                await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay);
                OverallConfigManger.Instence.WindowMode = ApplicationViewMode.CompactOverlay;
               
            }
           else
          
            {
                
                await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.Default);
                OverallConfigManger.Instence.WindowMode = ApplicationViewMode.Default;


            }
        }
    }
    /// <summary>
    /// 退出全屏
    /// </summary>
    public class ExitFull : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ApplicationView.GetForCurrentView().ExitFullScreenMode();//退出全屏
        }
    }
}
