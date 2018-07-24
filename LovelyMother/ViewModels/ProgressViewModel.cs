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

        private string _progressName;
        public string ProgressName
        {
            get => _progressName;
            set => Set(nameof(ProgressName), ref _progressName, value);
        }


        private string _defaultName;
        public string DefaultName
        {
            get => _defaultName;
            set => Set(nameof(DefaultName), ref _defaultName, value);
        }


        /// <summary>
        /// 选择的联系人。
        /// </summary>
        private Progress _selectedProgress;

        public Progress SelectedProgress
        {
            get => _selectedProgress;
            set => Set(nameof(SelectedProgress), ref _selectedProgress, value);
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
        /// 新添命令。
        /// </summary>
        private RelayCommand _addProgressCommand;

        public RelayCommand AddProgressCommand =>
            _addProgressCommand ?? (_addProgressCommand = new RelayCommand(
                async () =>
                {
                    await _motherService.NewProgressAsync(_progressName, _defaultName);
                }));


        /// <summary>
        /// 删除命令。
        /// </summary>
        private RelayCommand _deleteProgressCommand;

        public RelayCommand DeleteProgressCommand =>
            _deleteProgressCommand ?? (_deleteProgressCommand = new RelayCommand(
                async () => { await _motherService.DeleteProgressAsync(_progressName); }));






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
