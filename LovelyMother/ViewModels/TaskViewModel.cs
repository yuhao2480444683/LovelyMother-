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


        private User _inPutUser;

        public User InPutUser
        {
            get => _inPutUser;
            set => Set(nameof(InPutUser), ref _inPutUser, value);
        }


        /// <summary>
        /// 当前登录用户。
        /// </summary>
        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set => Set(nameof(CurrentUser), ref _currentUser, value);
        }





        /// <summary>
        /// 选择的任务。
        /// </summary>
        private Task _currentTask;

        public Task CurrentTask
        {
            get => _currentTask;
            set => Set(nameof(CurrentTask), ref _currentTask, value);
        }


        private RelayCommand _signInCommand;
        public RelayCommand SignInCommand => _signInCommand ?? (_signInCommand = new RelayCommand(async () =>
        {
            var result = await _motherService.ExistUserAsync(_inPutUser.UserName);
            if (result)
            {
                _currentUser.UserName = _inPutUser.UserName;
            }
            else
            {
                _currentUser = null;
            }


        }));

        private RelayCommand _updateCommand;
        public RelayCommand UpdateCommand => _updateCommand ?? (_updateCommand = new RelayCommand(async () =>
        {
            await _motherService.UpdateTaskAsync(_currentUser.UserName,_currentTask.Date, _currentTask.Begin, _currentTask.End, _currentTask.DefaultTime, _currentTask.Finish, _currentTask.TotalTime, _currentTask.Introduction)

        }));





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
                    await _motherService.NewTaskAsync(_currentUser.UserName,_currentTask.Date,_currentTask.Begin, _currentTask.DefaultTime, _currentTask.Introduction);
                }));



        /// <summary>
        /// 删除命令。
        /// </summary>
        private RelayCommand<Task> _deleteTaskCommand;

        public RelayCommand<Task> DeleteTaskCommand =>
            _deleteTaskCommand ?? (_deleteTaskCommand = new RelayCommand<Task>(
                async task => { await _motherService.DeleteTaskAsync(_currentUser.UserName, _currentTask.Date, _currentTask.Begin); }));


        

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
