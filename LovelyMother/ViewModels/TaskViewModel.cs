using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LovelyMother.Services;
using MotherLibrary;
using Task = MotherLibrary.Task;

namespace LovelyMother.ViewModels
{
    public class TaskViewModel:ViewModelBase
    {


        public ObservableCollection<MotherLibrary.Task> TaskCollection
        {
            get;
            private set;
        }

        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set => Set(nameof(CurrentUser), ref _currentUser, value);
        }


        /// <summary>
        /// 选择的任务。
        /// </summary>
        private Task _selectedTask;

        public Task SelectedTask
        {
            get => _selectedTask;
            set => Set(nameof(SelectedTask), ref _selectedTask, value);
        }



        /// <summary>
        /// 刷新命令。
        /// </summary>
        private RelayCommand _listTaskCommand;

        public RelayCommand ListProgressCommand => _listTaskCommand ?? (_listTaskCommand = new RelayCommand(async () =>
        {
            TaskCollection.Clear();
            var tasks = await _motherService.ListTaskAsync(_currentUser.UserName);

            foreach (var progress in tasks)
            {
                TaskCollection.Add(progress);
            }

        }));


        /// <summary>
        /// 新添命令。
        /// </summary>
        private RelayCommand<Task> _addTaskCommand;

        public RelayCommand<Task> AddTaskCommand =>
            _addTaskCommand ?? (_addTaskCommand = new RelayCommand<Task>(
                async task =>
                {
                    await _motherService.NewTaskAsync(_currentUser.UserName,task.Date,task.Begin,task.DefaultTime,task.Introduction);
                }));



        /// <summary>
        /// 删除命令。
        /// </summary>
        private RelayCommand<Task> _deleteTaskCommand;

        public RelayCommand<Task> DeleteTaskCommand =>
            _deleteTaskCommand ?? (_deleteTaskCommand = new RelayCommand<Task>(
                async task => { await _motherService.DeleteTaskAsync(_currentUser.UserName,task.Date,task.Begin); }));







        /// <summary>
        /// 联系人服务。
        /// </summary>
        private IMotherService _motherService;


        public TaskViewModel(IMotherService motherService)
        {
            _motherService = motherService;
            TaskCollection = new ObservableCollection<Task>();
        }

        public TaskViewModel() : this(new MotherService())
        {

        }




    }
}
