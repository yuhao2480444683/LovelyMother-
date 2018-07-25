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


namespace LovelyMother.ViewModels
{
    public class TaskViewModel:ViewModelBase
    {


        public ObservableCollection<MotherLibrary.Task> TaskCollection
        {
            get;
            private set;
        }

        private ProgressViewModel progressViewModel;



        /// <summary>
        /// 绑定属性。
        /// </summary>

        private String _inPutUserName;

        public String InPutUserName
        {
            get => _inPutUserName;
            set => Set(nameof(InPutUserName), ref _inPutUserName, value);
        }

        private String _inPutPassword;

        public String InPutPassword
        {
            get => _inPutPassword;
            set => Set(nameof(InPutPassword), ref _inPutPassword, value);
        }


        


        private TimeSpan _defaultBegin;
        public TimeSpan DefaultBegin
        {
            get => _defaultBegin;
            set => Set(nameof(DefaultBegin), ref _defaultBegin, value);
        }

        private TimeSpan _defaultend;
        public TimeSpan DefaultEnd
        {
            get => _defaultend;
            set => Set(nameof(DefaultEnd), ref _defaultend, value);
        }

 

        private string _introduction;

        public string Introduction
        {
            get => _introduction;
            set => Set(nameof(Introduction), ref _introduction, value);
        }

        /*private int _defaultTime;
        public int DefaultTime
        {
            get => _defaultTime;
            set => Set(nameof(DefaultTime), ref _defaultTime, value);
        }*/

        private string _userName;
        public string UserName
        {
            get=> _userName;
            set => Set(nameof(UserName), ref _userName, value);
        }

        public int Date;
        public int DefaultTime;
        public string Begin;

        /// <summary>
        /// 当前登录用户。
        /// </summary>
        //public User CurrentUser;

        //public Task CurrentTask



        private RelayCommand _signInCommand;
        public RelayCommand SignInCommand => _signInCommand ?? (_signInCommand = new RelayCommand(async () =>
        {
            var result = await _motherService.RightPairAsync(InPutUserName,InPutPassword);
            if (result)
            {
                UserName = InPutUserName;
            }
            else
            {
                UserName = null;
            }
        }));


        
        /*private RelayCommand _updateCommand;
        public RelayCommand UpdateCommand => _updateCommand ?? (_updateCommand = new RelayCommand(async () =>
        {
            //todo
            //Date的计算在这里转换

            //Begin的Tostring在这里转换

            //获取当前时间的End的计算在完成或放弃时触发赋值

            //DefaultTime的计算在这里计算 todo
            //CurrentTask.DefaultTime = ;

            //TotalTime的计算应该在点击完成或放弃时触发赋值  todo

            //Introduction更新在更改任务说明时点保存时赋值
            CurrentTask.Introduction = Introduction;

            await _motherService.UpdateTaskAsync(
                CurrentUser.UserName,
                CurrentTask.Date, 
                CurrentTask.Begin, 
                CurrentTask.End,
                CurrentTask.DefaultTime,
                CurrentTask.Finish,
                CurrentTask.TotalTime,
                CurrentTask.Introduction);

        }));*/





        /// <summary>
        /// 刷新命令。
        /// </summary>
        private RelayCommand _listTaskCommand;

        public RelayCommand ListProgressCommand => _listTaskCommand ?? (_listTaskCommand = new RelayCommand(async () =>
        {
            TaskCollection.Clear();
            var tasks = await _motherService.ListTaskAsync(UserName);

            foreach (var task in tasks)
            {
                TaskCollection.Add(task);
            }

        }));


        /// <summary>
        /// 新添命令。
        /// </summary>
        private RelayCommand _addTaskCommand;

        public RelayCommand AddTaskCommand =>
            _addTaskCommand ?? (_addTaskCommand = new RelayCommand(
                async () =>
                {
                    //todo
                    //Date的计算在这里转换
                    Date = DateTime.Now.Year*10000+DateTime.Now.Month*100+DateTime.Now.Day;
                    //Date =  DateTime.Now; 
                    //Begin的Tostring在这里转换
                    //Begin = (DefaultBegin.Hour * 100 + DefaultBegin.Minute).ToString();
                    //DefaultTime的计算在这里计算
                    DefaultTime=(DefaultEnd.Hours * 60 + DefaultEnd.Minutes) - (DefaultBegin.Hours * 60 + DefaultBegin.Minutes);//转换成分钟。
                    //Introduction更新在更改任务说明时点保存时赋值
                    //CurrentTask.Introduction = null;//暂时为空。
                    Begin = (DefaultBegin.Hours * 60 + DefaultBegin.Minutes).ToString();//转换成分钟
                    await _motherService.NewTaskAsync(UserName,Date, Begin, DefaultTime, Introduction);
                }));



        /// <summary>
        /// 删除命令。
        /// 
        /// </summary>
        private RelayCommand<MotherLibrary.Task> _deleteTaskCommand;

        public RelayCommand<MotherLibrary.Task> DeleteTaskCommand =>
            _deleteTaskCommand ?? (_deleteTaskCommand = new RelayCommand<MotherLibrary.Task>(
                async task =>
                {
                    //ItemClick触发时更新当前Task各项数据  todo
                    await _motherService.DeleteTaskAsync(UserName, Date, Begin);
                }));


        

        /// <summary>
        /// 联系人服务。
        /// </summary>
        private IMotherService _motherService;


        public TaskViewModel(IMotherService motherService)
        {
            _motherService = motherService;
            TaskCollection = new ObservableCollection<MotherLibrary.Task>();
            progressViewModel = new ProgressViewModel();
           
           
        }

        public TaskViewModel() : this(new MotherService())
        {
           
        }




    }
}
