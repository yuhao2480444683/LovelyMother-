using LovelyMother.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovelyMother.Services
{
    public interface IProcessService
    {
        ObservableCollection<RunningProcess> GetProcessNow();
        bool IfNewProgramExist(List<String> appName);
        ObservableCollection<RunningProcess> GetProcessDifferent(ObservableCollection<RunningProcess> processFirst, ObservableCollection<RunningProcess> processSecond);
    }
}
//定义Service应该实现的操作
