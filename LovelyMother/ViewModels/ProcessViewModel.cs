using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LovelyMother.Model;
using LovelyMother.Models;
using LovelyMother.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LovelyMother.ViewModels
{
    public class ProcessViewModel : ViewModelBase
    {

        private IProcessService _processService;

        public ObservableCollection<RunningProcess> ProcessAtFirst { get; private set; }

        public Information information { get; private set; }

        public int template = 0;

        /// <summary>
        /// template value for the judge
        /// </summary>
        private List<RunningProcess> _condition1;

        public List<RunningProcess> condition1
        {
            get => _condition1;
            set => Set(nameof(condition1), ref _condition1, value);
        }

        private List<RunningProcess> _condition2;

        /// <summary>
        /// Final Result for Click
        /// </summary>
        public List<RunningProcess> condition2
        {
            get => _condition2;
            set => Set(nameof(condition2), ref _condition2, value);
        }

        //刷新操作

        private RelayCommand _refreshCommand;

        private async void RefreshAsync()
        {
            int i = 0;
             do
            {
                var ProcessNow = _processService.GetProcessNow();
                var judge = _processService.IfNewProgramExist(ProcessAtFirst, ProcessNow);
                if (judge == true)
                {
                    information.ifNewProgramExist = "True";
                    i = 1;
                    var currentAV = ApplicationView.GetForCurrentView();
                    var newAV = CoreApplication.CreateNewView();
                    await newAV.Dispatcher.RunAsync(
                                    CoreDispatcherPriority.Normal,
                                    async () =>
                                    {
                                        var newWindow = Window.Current;
                                        var newAppView = ApplicationView.GetForCurrentView();
                                        newAppView.Title = "你怎么回事弟弟？";

                                        var frame = new Frame();
                                        frame.Navigate(typeof(Bling), null);
                                        newWindow.Content = frame;
                                        newWindow.Activate();

                                        await ApplicationViewSwitcher.TryShowAsStandaloneAsync(
                                            newAppView.Id,
                                            ViewSizePreference.UseMinimum,
                                            currentAV.Id,
                                            ViewSizePreference.UseMinimum);
                                    });
                }
                else
                {
                    information.ifNewProgramExist = "False";
                    i = 0;
                }
                information.refreshTime++;
                if(i == 0)
                {
                    await Task.Delay(10000);
                }
                else
                {
                    await Task.Delay(2000);
                }
                if(template == 1)
                {
                    template = 0;
                    break;
                }
            }
            while (true);

        }

        public RelayCommand RefreshCommand  => _refreshCommand ?? 
            ( _refreshCommand = new RelayCommand( () => { RefreshAsync(); }));


        //公开方法

        public ProcessViewModel(IProcessService processService)
        {
            _processService = processService;
            information = new Information();

            information.refreshTime = 0;
            ProcessAtFirst = _processService.GetProcessNow();
            information.ifNewProgramExist = "False";

            condition1 = new List<RunningProcess>();
            condition2 = new List<RunningProcess>();

            Messenger.Default.Register<ListenMessage>(this, (message) =>
            {
                this.template = 1;
            });

            Messenger.Default.Register<RefreshMessage>(this, (message) =>
            {
               RefreshAsync();
            });

            Messenger.Default.Register<ReadNowProcess>(this, (message) =>
             {
                 condition1.Clear();
                 condition2.Clear();
                 condition1 = _processService.GetProcessNow().ToList();
             });

            Messenger.Default.Register<ReadNewProcess>(this, (message) =>
            {
                condition2 = _processService.GetProcessNow().ToList();
                foreach( RunningProcess process in condition2 )
                {
                    for (int i = 0; i < condition1.Count; i++)
                    {
                        if (condition1[i].fileName == process.fileName)
                        {
                            condition2.Remove(process);
                        }
                    }
                }
            });

            Messenger.Default.Register<SaveProcess>(this, async (message) =>
            {
                //TODO : 发送信息时直接封装
                var ms = new MotherService();
                await ms.NewProgressAsync(message.ExectableFileName, message.thisName);
            });
        }

        private void ReadProcess_two()
        {
            throw new NotImplementedException();
        }

        private void ReadProcess_one()
        {
            throw new NotImplementedException();
        }

        private void GetProcess()
        {
            throw new NotImplementedException();
        }

        public ProcessViewModel() : this(DesignMode.DesignModeEnabled ?
            ( IProcessService ) new ProcessService() :
            new ProcessService())
        {
#if DEBUG
            if (DesignMode.DesignModeEnabled)
            {
                RefreshAsync();
            }
#endif
        }


    }

}
//ViewModel层本质：从Service层获得数据后进行数据绑定/更新
//非Obeservable类型：MVVM下不可见