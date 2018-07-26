using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace LovelyMother.Services
{
    /// <summary>
    ///     对话框服务。
    /// </summary>
    public class DialogService
    {
        /// <summary>
        ///     显示。
        /// </summary>
        /// <param name="message">消息。</param>
        public async Task ShowAsync(string message)
        {
            await new MessageDialog(message).ShowAsync();
        }
    }
}
