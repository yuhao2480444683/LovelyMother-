using System;
using Microsoft.EntityFrameworkCore;

namespace MotherLibrary
{
    /// <summary>
    /// 用户类。
    /// </summary>
    public class User
    {
        /// <summary>
        /// 主键。
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名。
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码。
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户任务完成总时间。
        /// </summary>
        public int TotalTime { get; set; }
    }
    /// <summary>
    /// 进程类。
    /// </summary>
    public class Progress
    {
        /// <summary>
        /// 主键。
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 进程名称。
        /// </summary>
        public string ProgressName { get; set; }
        /// <summary>
        /// 用户定义名称。
        /// </summary>
        public string DefaultName { get; set; }
    }
    /// <summary>
    /// 任务类。
    /// </summary>
    public class Task
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 开始时间。
        /// </summary>
        public String Begin { get; set; }
        /// <summary>
        /// 结束时间。
        /// </summary>
        public String End { get; set; }
        /// <summary>
        /// 任务当前执行总时间。
        /// </summary>
        public int TotalTime { get; set; }
        /// <summary>
        /// 任务总时间。
        /// </summary>
        public int DefaultTime { get; set; }
        /// <summary>
        /// 任务说明。
        /// </summary>
        public String Introduction { get; set; }
        /// <summary>
        /// 是否完成任务。-1表示非正常退出，0代表放弃，1代表完成或提前完成
        /// </summary>
        public int Finish { get; set; }

        public int Date { get; set; }

        /// <summary>
        /// 与用户之间的联系。
        /// </summary>
        public int UserId { get; set; }
        public User User { get; set; }

    }
    public class MyDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DB.db");
        }


    }
}
