using Windows.ApplicationModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using LovelyMother.Services;
using UvpClient.Services;

namespace LovelyMother.ViewModels
{
    public class BindingViewModel:ViewModelBase
    {

        public const string CheckNotFoundErrorMessage =
            "We could not find your User Name in our database.\nPlease check if you have entered a correct User Name.\n\nIf this error continues, please contact us.";
        
        /// <summary>
        ///     对话框服务。
        /// </summary>
        private readonly Services.IDialogService _dialogService;


        /// <summary>
        ///     根导航服务。
        /// </summary>
        private readonly IRootNavigationService _rootNavigationService;

        /// <summary>
        /// 用户服务。
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        ///     绑定命令。
        /// </summary>
        private RelayCommand _bindCommand;

        /// <summary>
        ///     正在绑定用户名。
        /// </summary>
        private bool _binding;


        /// <summary>
        ///     检查命令。
        /// </summary>
        private RelayCommand _checkCommand;

        /// <summary>
        ///     正在检查用户名。
        /// </summary>
        private bool _checking;


        /// <summary>
        ///     用户名。
        /// </summary>
        private string _userName;

        /// <summary>
        /// 总时长。
        /// </summary>
        private int _totalTime;


        /// <summary>
        ///     用户名。
        /// </summary>
        public string UserName
        {
            get => _userName;
            set => Set(nameof(UserName),ref _userName, value);
        }

        /// <summary>
        /// 总时长。
        /// </summary>
        public int TotalTime
        {
            get => _totalTime;
            set => Set(nameof(TotalTime), ref _totalTime, value);
        }

        /// <summary>
        ///     正在绑定用户名。
        /// </summary>
        public bool Binding
        {
            get => _binding;
            set => Set(nameof(Binding), ref _binding, value);
        }

        /// <summary>
        ///     正在检查用户名。
        /// </summary>
        public bool Checking
        {
            get => _checking;
            set => Set(nameof(Checking), ref _checking, value);
        }





        /// <summary>
        ///     构造函数。
        /// </summary>
        /// <param name="rootNavigationService">根导航服务。</param>
        /// <param name="userService">用户服务。</param>
        [PreferredConstructor]
        public BindingViewModel(IRootNavigationService rootNavigationService,
            Services.IDialogService dialogService, IUserService userService)
        {
            _rootNavigationService = rootNavigationService;
            _dialogService = dialogService;
            _userService = userService;
        }



        /// <summary>
        ///     绑定命令。
        /// </summary>
        public RelayCommand BindCommand =>
            _bindCommand ?? (_bindCommand = new RelayCommand(async () => {
                Binding = true;
                _bindCommand.RaiseCanExecuteChanged();
                var serviceResult =
                    await _userService.BindAccountAsync(UserName);
                Binding = false;
                _bindCommand.RaiseCanExecuteChanged();

                switch (serviceResult.Status)
                {
                    case ServiceResultStatus.Unauthorized:
                        break;
                    case ServiceResultStatus.NoContent:
                        _rootNavigationService.Navigate(typeof(MainPage), null,
                            NavigationTransition.EntranceNavigationTransition);
                        break;
                    case ServiceResultStatus.BadRequest:
                        await _dialogService.ShowAsync(serviceResult.Message);
                        break;
                    default:
                        await _dialogService.ShowAsync(
                            App.HttpClientErrorMessage + serviceResult.Message);
                        break;
                }
            }, () => !Binding));









    }
}
