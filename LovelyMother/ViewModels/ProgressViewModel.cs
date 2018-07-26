using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LovelyMother.Models;
using LovelyMother.Services;
using MotherLibrary;


namespace LovelyMother.ViewModels
{
    public class ProgressViewModel : ViewModelBase
    {

        /// <summary>
        /// 读取指定进程所用的三个数组
        /// </summary>

        /// <summary>
        /// 进程服务
        /// </summary>

        private ProcessViewModel _processModel;

        public ProcessViewModel processModel
        {
            get => _processModel;
            set => Set(nameof(processModel), ref _processModel, value);
        }

        public ObservableCollection<Progress> ProgressCollection
        {
            get;
            private set;
        }

        /// <summary>
        /// 属性。
        /// </summary>
        private string _progressName;
        public string ProgressName
        {
            get => _progressName;
            set => Set(nameof(ProgressName),ref _progressName,value);
        }


        private string _defaultName;
        public string DefaultName
        {
            get => _defaultName;
            set => Set(nameof(DefaultName), ref _defaultName, value);
        }



        /// <summary>
        /// 刷新命令。
        /// </summary>
        private RelayCommand _listProgressCommand;

        public RelayCommand ListProgressCommand => _listProgressCommand ?? (_listProgressCommand = new RelayCommand(async () =>
        {
            ProgressCollection.Clear();
            var progresses = await _motherService.ListProgressAsync();

            foreach (var progress in progresses)
            {
                ProgressCollection.Add(progress);
            }

        }));
        /// <summary>
        /// 更新。
        /// </summary>
        private RelayCommand _updateProgressCommand;

        public RelayCommand UpdateProgressCommand => _updateProgressCommand ?? (_updateProgressCommand = new RelayCommand(async () =>
                                                         {
                                                             await _motherService.UpdateProgressAsync(
                                                                 ProgressName,
                                                                 DefaultName);
                                                         }));
        /// <summary>
        /// 新添命令。
        /// </summary>
        private RelayCommand _addProgressCommand;

        public RelayCommand AddProgressCommand =>
            _addProgressCommand ?? (_addProgressCommand = new RelayCommand(
                async () =>
                {
                    await _motherService.NewProgressAsync(ProgressName, DefaultName);
                }));


        /// <summary>
        /// 删除命令。
        /// </summary>
        private RelayCommand _deleteProgressCommand;

        public RelayCommand DeleteProgressCommand =>
            _deleteProgressCommand ?? (_deleteProgressCommand = new RelayCommand(
                async () => { await _motherService.DeleteProgressAsync(ProgressName); }));


        /// <summary>
        /// 绑定开始。
        /// </summary>
        private RelayCommand _progress1;
        public RelayCommand Progress1 =>
            _progress1 ?? (_progress1 = new RelayCommand(
                 () => {
                     processModel.getNewListPage_ReadProcessNow();
                }));
        /// <summary>
        /// 绑定关闭。
        /// </summary>
        private RelayCommand _progress2;
        public RelayCommand Progress2 =>
            _progress2 ?? (_progress2 = new RelayCommand(
                 () => {
                     processModel.getNewListPage_ReadProcessWanted();
                 }));
        /// <summary>
        /// 绑定保存。
        /// </summary>
        private RelayCommand _progress3;
        public RelayCommand Progress3;


        /// <summary>
        /// 联系人服务。
        /// </summary>
        private IMotherService _motherService;


        public ProgressViewModel(IMotherService motherService)
        {
            _motherService = motherService;
            ProgressCollection = new ObservableCollection<Progress>();
            processModel = new ProcessViewModel(new ProcessService());
        }

        public ProgressViewModel() : this(new MotherService())
        {

        }
    }


}
