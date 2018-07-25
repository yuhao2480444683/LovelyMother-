using LovelyMother.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.System.Diagnostics;

namespace LovelyMother.Services
{
    /** TODO **/
    public class ProcessService : IProcessService
    {
        /// <summary>
        /// 获取所有进程
        /// </summary>
        public ObservableCollection<RunningProcess> GetProcessNow()
        {
            bool judge;
            int j;

            ObservableCollection <RunningProcess> processes = new ObservableCollection<RunningProcess>();
            var details = ProcessDiagnosticInfo.GetForProcesses().OrderByDescending(x => x.ExecutableFileName);
            foreach (var detail in details)
            {
                if(detail.Parent != null)
                {
                    if ((!detail.ExecutableFileName.Equals("winlogon.exe")) && (!detail.ExecutableFileName.Equals("System")) && (!detail.ExecutableFileName.Equals("svchost.exe"))&&(!detail.Parent.ExecutableFileName.Equals("svchost.exe") && ( ! detail.Parent.ExecutableFileName.Equals("wininit.exe"))))
                    {
                        for(j = 0,judge = false; j < processes.Count; j++)
                        {
                            if(processes[j].id == detail.Parent.ProcessId)
                            {
                                judge = true;
                            }
                        }
                        if (judge == false)
                        {
                            var temp2 = new RunningProcess(detail.ExecutableFileName, detail.ProcessId, detail.Parent.ExecutableFileName, detail.Parent.ProcessId);
                            processes.Add(temp2);
                        }

                    }
                }
             }
            return processes;
        }

        /// <summary>
        /// 是否有新进程出现
        /// </summary>
        /// <returns>是 ：true / 否 ： false</returns>
        public bool IfNewProgramExist(List<String> appName)
        {
            int i;
            int j;
            var Process = GetProcessNow();
            bool result = false;

            for (i = 0; i < Process.Count; i++)
            {
                for (j = 0; j < appName.Count; j++)
                {
                    //若进程名相同，则不存在新进程
                    if (Process[i].fileName.Equals(appName[j]))
                    {
                        result = true;
                        break;
                    }
                }
                if (result == true)
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// override : If NewProgram Exist
        /// </summary>
        /// <param name="before"></param>
        /// <param name="now"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool IfNewProgramExist(ObservableCollection<RunningProcess> before, ObservableCollection<RunningProcess> now, List< String > filename)
        {

            int i = 0;
            int j = 0;
            bool result = false;

            long systemId = new long();

            //预处理：先获得system.exe的id
            for (i = 0; i < before.Count; i++)
            {
                //TODO : 检索出真正的程序
                if (before[i].fileName == "System.exe")
                {
                    systemId = before[i].id;
                    break;
                }
            }

            //检索now数组
            for (i = 0; i < now.Count; i++)
            {
                for (j = 0, result = true; j < before.Count; j++)
                {
                    if (before[j].fileName == now[i].fileName)
                    {
                            
                        result = false;
                        break;
                    }
                }
                
                for(j = 0; (j < filename.Count) && (result == true); j++)
                {
                    if (filename[i].Equals(result))
                    {
                        result = false;
                        break;
                    }
                }
                
                if (result == true)
                {
                    break;
                }
            }

            return result;
        }
    }

}

//Service层本质 : 获得数据 / 对数据的基本操作
