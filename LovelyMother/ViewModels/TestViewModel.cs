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
    public class TestViewModel:ViewModelBase
    {

        public ProcessViewModel processModel;

        public ObservableCollection<Progress> ProgressCollection
        {
            get;
            private set;
        }

        /// <summary>
        /// 选择的程序。
        /// </summary>
        private Progress _selectedProgress;

        public Progress SelectedProgress
        {
            get => _selectedProgress;
            set => Set(nameof(SelectedProgress), ref _selectedProgress, value);
        }


        /// <summary>
        /// 更新程序列表。(刷新。)
        /// </summary>
        private RelayCommand _listProgressCommand;

        public RelayCommand ListProgressCommand => _listProgressCommand ?? (_listProgressCommand = new RelayCommand(async () =>
        {
            ProgressCollection.Clear();
            var progresses = await _motherService.ListProgressAsync();

            foreach (var contact in progresses)
            {
                ProgressCollection.Add(contact);
            }

        }));

        /// <summary>
        /// 程序服务。（初始化程序列表。）
        /// </summary>
        private IMotherService _motherService;

        public TestViewModel(IMotherService motherService)
        {
            _motherService = motherService;
            ProgressCollection = new ObservableCollection<Progress>();
            processModel = new ProcessViewModel(new ProcessService());
        }






        /// <summary>
        /// 空构造函数。
        /// </summary>
        public TestViewModel() : this(new MotherService())
        {

        }





    }


}
