using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

namespace Count_Down.TimeWork
{
    [Serializable]
    class OverallConfigManger : IDeserializationCallback
    {
        public static OverallConfigManger Instence = new OverallConfigManger();

        #region 主题颜色
        public event EventHandler OverallThemeChanged;
        private String overalltheme = "TDefault";
        public String OverallTheme
        {
            get { return overalltheme; }
            set
            {
                overalltheme = value;
                OverallThemeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion

        #region 窗体状态
        public event EventHandler WindowModeChanged;
        [NonSerialized]
        private ApplicationViewMode windowMode;

        public ApplicationViewMode WindowMode
        {
            get => windowMode;
            set
            {
                windowMode = value;
                WindowModeChanged?.Invoke(this, EventArgs.Empty );
            }
        }
        #endregion

        public OverallConfigManger()
        {

        }

        public void OnDeserialization(object sender)
        {
            windowMode = ApplicationViewMode.Default;
        }
    }
}
