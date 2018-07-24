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
        private RelayCommand<Progress> _addProgressCommand;

        public RelayCommand<Progress> AddProgressCommand =>
            _addProgressCommand ?? (_addProgressCommand = new RelayCommand<Progress>(
                async progress =>
                {
                    await _motherService.NewProgressAsync(progress.ProgressName, progress.DefaultName);
                }));


        /// <summary>
        /// 删除命令。
        /// </summary>
        private RelayCommand<Progress> _deleteProgressCommand;

        public RelayCommand<Progress> DeleteProgressCommand =>
            _deleteProgressCommand ?? (_deleteProgressCommand = new RelayCommand<Progress>(
                async progress => { await _motherService.DeleteProgressAsync(progress.ProgressName); }));






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
