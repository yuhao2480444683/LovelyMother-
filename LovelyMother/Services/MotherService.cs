using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MotherLibrary;



namespace LovelyMother.Services
{
    public class MotherService : IMotherService
    {

        /// <summary>
        /// 列出所有。
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> ListUserAsync()
        {
            using (var db = new MyDatabaseContext())
            {
                return await db.Users.ToListAsync();
            }
        }
        public async Task<List<Progress>> ListProgressAsync()
        {
            using (var db = new MyDatabaseContext())
            {
                return await db.Progresses.ToListAsync();
            }
        }

        //TODO     Need Test
        public async Task<List<MotherLibrary.Task>> ListTaskAsync(User User)
        {
            using (var db = new MyDatabaseContext())
            {
                var tasks = await db.Tasks.ToListAsync();
                var count = tasks.Count;

                foreach (MotherLibrary.Task task in tasks)
                {
                    if (task.UserId != User.Id)
                    {
                        tasks.Remove(task);
                    }
                }
                
                return tasks;

            }
        }

        public async Task<List<MotherLibrary.Task>> ListTasksAsync()
        {
            using (var db = new MyDatabaseContext())
            {
                return await db.Tasks.ToListAsync();
            }
        }




        /// <summary>
        /// 新建项。
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<bool> NewUserAsync(User User)
        {
            using (var db = new MyDatabaseContext())
            {
                var Announcement = await db.Users.SingleOrDefaultAsync(m => m.UserName == User.UserName);
                if (Announcement == null)
                {

                    var user1 = new User { UserName = User.UserName, Password = User.Password, TotalTime = 0 };
                    db.Users.Add(user1);
                    await db.SaveChangesAsync();
                    return true;

                }
                else
                {
                    return false;
                }

            }

        }

        public async System.Threading.Tasks.Task<bool> NewTaskAsync(MotherLibrary.Task Task,User User)
        {
            using (var db = new MyDatabaseContext())
            {
                //
                var currentUser = await db.Users.FirstOrDefaultAsync(m => m.Id == User.Id);
                if (currentUser != null)
                {
                    
                    var Announcement = await db.Tasks.FirstOrDefaultAsync(m =>m.Date == Task.Date && m.Begin == Task.Begin && m.UserId == User.Id);
                    if (Announcement == null)
                    {
                        var task = new MotherLibrary.Task
                        {
                            Date = Task.Date,
                            UserId = currentUser.Id,
                            Begin = Task.Begin,
                           // End = Task.End,
                            TotalTime = 0,
                            DefaultTime = Task.DefaultTime,
                            Introduction = Task.Introduction,
                            Finish = -1
                        };
                        db.Tasks.Add(task);
                        await db.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                else
                {
                    return false;
                }
            }
        }

        public async System.Threading.Tasks.Task<bool> NewProgressAsync(Progress Progress)
        {
            using (var db = new MyDatabaseContext())
            {
                var Announcement = await db.Progresses.SingleOrDefaultAsync(m => m.ProgressName == Progress.ProgressName);
                if (Announcement == null)
                {

                    var progress = new Progress { ProgressName = Progress.ProgressName,DefaultName = Progress.DefaultName};
                    db.Progresses.Add(progress);
                    await db.SaveChangesAsync();
                    return true;

                }
                else
                {
                    return false;
                }

            }
        }





        /// <summary>
        /// 修改项。
        /// </summary>
        /// <returns></returns>
        //TODO  单元测试不通过，可能是类不能直接 = 
        public async Task<bool> UpdateUserAsync(User User)
        {
            using (var db = new MyDatabaseContext())
            {
                var Announcement = await db.Users.FirstOrDefaultAsync(m => m.Id == User.Id);
                if (Announcement != null)
                {
                    Announcement.UserName = User.UserName;
                    Announcement.Password = User.Password;
                    Announcement.TotalTime = User.TotalTime;

                    await db.SaveChangesAsync();
                    return true;

                }
                else
                {
                    return false;
                }

            }
        }

        public async Task<bool> UpdateTaskAsync(MotherLibrary.Task Task,User User)
        {
            using (var db = new MyDatabaseContext())
            {
                var CurrentUser = await db.Users.FirstOrDefaultAsync(m => m.Id == User.Id);
                if (CurrentUser != null)
                {

                    var Announcement = await db.Tasks.SingleOrDefaultAsync(m => m.Id == Task.Id && m.UserId == User.Id);
                    if (Announcement != null)
                    {
                        Announcement.DefaultTime = Task.DefaultTime;
                        Announcement.End = Task.End;
                        Announcement.Finish = Task.Finish;
                        Announcement.Introduction = Task.Introduction;
                        Announcement.TotalTime = Task.TotalTime;

                        await db.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }


               
            }
        }

        public async Task<bool> UpdateProgressAsync(Progress Progress)
        {
            using (var db = new MyDatabaseContext())
            {
                var Announcement = await db.Progresses.SingleOrDefaultAsync(m => m.Id == Progress.Id);
                if (Announcement != null)
                {
                    Announcement = Progress;
                    db.Progresses.Update(Announcement);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除项。
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>


        public async Task<bool> DeleteUserAsync(User User)
        {
            using (var db = new MyDatabaseContext())
            {
                var Announcement = await db.Users.SingleOrDefaultAsync(m => m.Id == User.Id);
                if (Announcement != null)
                {
                    db.Users.Remove(Announcement);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }

        public async Task<bool> DeleteTaskAsync(MotherLibrary.Task Task,User User)
        {
            using (var db = new MyDatabaseContext())
            {

                var CurrentUser = await db.Users.FirstOrDefaultAsync(m => m.Id == User.Id);
                if (CurrentUser != null)
                {
                    var Announcement = await db.Tasks.SingleOrDefaultAsync(m => m.Id == Task.Id && m.UserId == User.Id);
                    if (Announcement != null)
                    {
                        db.Tasks.Remove(Announcement);
                        await db.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }

                

            }
        }

        public async Task<bool> DeleteProgressAsync(Progress Progress)
        {
            using (var db = new MyDatabaseContext())
            {
                var Announcement = await db.Progresses.SingleOrDefaultAsync(m => m.Id == Progress.Id);
                if (Announcement != null)
                {
                    db.Progresses.Remove(Announcement);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}
