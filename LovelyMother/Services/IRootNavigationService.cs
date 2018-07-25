using System;

namespace UvpClient.Services
{
    /// <summary>
    ///     根导航服务接口。
    /// </summary>
    public interface IRootNavigationService
    {
        /// <summary>
        ///     导航。
        /// </summary>
        void Navigate(Type sourcePageType);

        /// <summary>
        ///     导航。
        /// </summary>
        void Navigate(Type sourcePageType, object parameter);

        /// <summary>
        ///     导航。
        /// </summary>
        void Navigate(Type sourcePageType, object parameter,
            NavigationTransition navigationTransition);
    }

    /// <summary>
    ///     导航动画枚举。
    /// </summary>
    public enum NavigationTransition
    {
        EntranceNavigationTransition,
        DrillInNavigationTransition
    }
}