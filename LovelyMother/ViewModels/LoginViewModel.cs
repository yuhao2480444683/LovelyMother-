using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using LovelyMother.Services;

namespace LovelyMother.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        /// <summary>
        ///     登录错误信息。
        /// </summary>
        public const string LoginErrorMessage =
            "Sorry!!!\n\nAn error occurred when we tried to sign you in.\nPlease screenshot this dialog and send it to your teacher.\n\nError:\n";

        /// <summary>
        ///     对话框服务。
        /// </summary>
        private readonly IDialogService _dialogService;

        /// <summary>
        ///     身份服务。
        /// </summary>
        private readonly IIdentityService _identityService;

        /// <summary>
        ///     根导航服务。
        /// </summary>
        //private readonly IRootNavigationService _rootNavigationService;

        /// <summary>
        ///     登录命令。
        /// </summary>
        private RelayCommand _loginCommand;

        /// <summary>
        ///     正在登录。
        /// </summary>
        private bool _signingIn;

        public LoginViewModel(IIdentityService identityService,
        {

        }
    }
}
