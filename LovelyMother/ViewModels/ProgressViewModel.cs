using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LovelyMother.Services;
using MotherLibrary;


namespace LovelyMother.ViewModels
{
    public class ProgressViewModel:ViewModelBase
    {

        public ProcessViewModel processModel;

        public ObservableCollection<Progress> ProgressCollection
        {
            get;
            private set;
        }

        private Progress _currentProgress;
        public Progress CurrentProgress
        {
            get => _currentProgress;
            set => Set(nameof(CurrentProgress), ref _currentProgress, value);
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

        private RelayCommand _updateProgressCommand;

        public RelayCommand UpdateProgressCommand => _updateProgressCommand ?? (_updateProgressCommand = new RelayCommand(async () =>
                                                         {
                                                             await _motherService.UpdateProgressAsync(
                                                                 _currentProgress.ProgressName,
                                                                 _currentProgress.DefaultName);
                                                         }));




        /// <summary>
        /// 新添命令。
        /// </summary>
        private RelayCommand _addProgressCommand;

        public RelayCommand AddProgressCommand =>
            _addProgressCommand ?? (_addProgressCommand = new RelayCommand(
                async () =>
                {
                    await _motherService.NewProgressAsync(_currentProgress.ProgressName, _currentProgress.DefaultName);
                }));


        /// <summary>
        /// 删除命令。
        /// </summary>
        private RelayCommand _deleteProgressCommand;

        public RelayCommand DeleteProgressCommand =>
            _deleteProgressCommand ?? (_deleteProgressCommand = new RelayCommand(
                async () => { await _motherService.DeleteProgressAsync(_currentProgress.ProgressName); }));






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
