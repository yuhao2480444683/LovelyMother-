using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovelyMother.Models
{
    public class RunningProcess : ObservableObject
    {
        /// <summary>
        /// 进程可执行文件名。
        /// </summary>
        private string _fileName;

        public string fileName
        {
            get => _fileName;
            set => Set(nameof(fileName), ref _fileName, value);
        }

        /// <summary>
        /// 进程Id
        /// </summary>
        private uint _id;
        public uint id
        {
            get => _id;
            set => Set(nameof(id), ref _id, value);
        }

        private string _parentName;
        public string parentName
        {
            get => _parentName;
            set => Set(nameof(parentName), ref _parentName, value);
        }

        private uint _parentId;
        public uint parentId
        {
            get => parentId;
            set => Set(nameof(parentId), ref _parentId, value);
        }

        public RunningProcess(string fileName, uint id, string parentName,uint parentId)
        {
            this.fileName = fileName;
            this.id = id;
            this.parentName = parentName;
            this.parentId = parentId;
        }
    }

    public class Information : ObservableObject
    {

        private int _refreshTime;
        public int refreshTime
        {
            get => _refreshTime;
            set => Set(nameof(refreshTime), ref _refreshTime, value);
        }

        private string _ifNewProgramExist;
        public string ifNewProgramExist
        {
            get => _ifNewProgramExist; 
            set => Set( nameof(ifNewProgramExist), ref _ifNewProgramExist, value);
        }

    }

}
//Model层本质：定义数据类型
//Binding 必须是现在iNotifyProperty接口中
//MVVMLights : 省略了繁琐的实现过程
//索引和字段有什么区别
