using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.ViewManagement;

namespace Count_Down.TimeWork
{
    public class Time
    {
        

    }
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
                //ApplicationView.GetForCurrentView().TryEnterFullScreenMode();//设为全屏
                OverallConfigManger.Instence.WindowMode = ApplicationViewMode.CompactOverlay;
               
            }
            else
            //if(ApplicationView.GetForCurrentView().TryEnterFullScreenMode())
            {
                //ApplicationView.GetForCurrentView().ExitFullScreenMode();//退出全屏
                await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.Default);
                OverallConfigManger.Instence.WindowMode = ApplicationViewMode.Default;
                
                
            }
        }
    }
}
