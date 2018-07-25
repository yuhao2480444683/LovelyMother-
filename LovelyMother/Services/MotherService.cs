using System;
using System.Collections.Generic;
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
        public async Task<List<MotherLibrary.Task>> ListTaskAsync( String userName)
        {
            using (var db = new MyDatabaseContext())
            {
                var currentUser = await db.Users.SingleOrDefaultAsync(m => m.UserName == userName);
                if (currentUser != null)
                {
                    var tasks = await db.Tasks.ToListAsync();

                    var count = tasks.Count;

                    foreach (MotherLibrary.Task task in tasks)
                    {
                        if (task.UserId != currentUser.Id)
                        {
                            tasks.Remove(task);
                        }
                    }

                    return tasks;

                }
                else
                {
                    return null;
                }

       

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
        public async Task<bool> NewUserAsync(String username,String password)
        {
            using (var db = new MyDatabaseContext())
            {
                var Announcement = await db.Users.SingleOrDefaultAsync(m => m.UserName == username);
                if (Announcement == null)
                {

                    var user1 = new User { UserName = username, Password = password, TotalTime = 0 };
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

        public async Task<bool> NewTaskAsync(String username,int date,String begin,int defaulttime,String introduction)
        {
            using (var db = new MyDatabaseContext())
            {
                //
                var currentUser = await db.Users.FirstOrDefaultAsync(m => m.UserName == username);
                if (currentUser != null)
                {
                    
                    var Announcement = await db.Tasks.FirstOrDefaultAsync(m =>m.Date == date && m.Begin == begin && m.UserId == currentUser.Id);
                    if (Announcement == null)
                    {
                        var task = new MotherLibrary.Task
                        {
                            Date = date,
                            UserId = currentUser.Id,
                            Begin = begin,
                            End =  begin,
                            TotalTime = 0,
                            DefaultTime = defaulttime,
                            Introduction = introduction,
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

        public async Task<bool> NewProgressAsync(String progressName,String defaultName)
        {
            using (var db = new MyDatabaseContext())
            {
                var Announcement = await db.Progresses.SingleOrDefaultAsync(m => m.ProgressName == progressName);
                if (Announcement == null)
                {

                    var progress = new Progress { ProgressName = progressName,DefaultName = defaultName};
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
        /// 修改项。只能修改DefaultName
        /// </summary>
        /// <returns></returns>
        //TODO  单元测试不通过，可能是类不能直接 = 
        public async Task<bool> UpdateUserAsync(String userName,String passWord,int totalTime)               
        {
            using (var db = new MyDatabaseContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(m => m.UserName == userName);
                if (user != null)
                {
                    user.UserName  = userName;
                    user.Password  = passWord;
                    user.TotalTime = totalTime;

                    await db.SaveChangesAsync();
                    return true;

                }
                else
                {
                    return false;
                }

            }
        }

        public async Task<bool> UpdateTaskAsync(String userName,int date,String begin,String end,int defaultTime,int finish,int totalTime,String introduction)
        {
            using (var db = new MyDatabaseContext())
            {
                var currentUser = await db.Users.FirstOrDefaultAsync(m => m.UserName == userName);
                if (currentUser != null)
                {

                    var task = await db.Tasks.SingleOrDefaultAsync(m => m.Date ==date && m.Begin == begin && m.UserId == currentUser.Id);
                    if (task != null)
                    {
                        task.DefaultTime = defaultTime;
                        task.End = end;
                        task.Finish = finish;
                        task.Introduction = introduction;
                        task.TotalTime = totalTime;

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

        public async Task<bool> UpdateProgressAsync(String progressName,String defaultTime)
        {
            using (var db = new MyDatabaseContext())
            {
                var progress = await db.Progresses.FirstOrDefaultAsync(m => m.ProgressName == progressName);
                if (progress != null)
                {
                    progress.DefaultName = defaultTime;
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


        public async Task<bool> DeleteUserAsync(String userName)
        {
            using (var db = new MyDatabaseContext())
            {
                var user = await db.Users.SingleOrDefaultAsync(m => m.UserName == userName);
                if (user != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }

        public async Task<bool> DeleteTaskAsync(String userName,int date,String begin)
        {
            using (var db = new MyDatabaseContext())
            {

                var currentUser = await db.Users.FirstOrDefaultAsync(m => m.UserName == userName);
                if (currentUser != null)
                {
                    var task = await db.Tasks.SingleOrDefaultAsync(m => m.Date == date && m.Begin == begin && m.UserId == currentUser.Id);
                    if (task != null)
                    {
                        db.Tasks.Remove(task);
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

        public async Task<bool> DeleteProgressAsync(String progressName)
        {
            using (var db = new MyDatabaseContext())
            {
                var progress = await db.Progresses.SingleOrDefaultAsync(m => m.ProgressName == progressName);
                if (progress != null)
                {
                    db.Progresses.Remove(progress);
                    await db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public async Task<bool> RightPairAsync(string username,string password)
        {
            using (var db = new MyDatabaseContext())
            {
                var user = await db.Users.SingleOrDefaultAsync(m => m.UserName == username);
                if (user != null && user.Password == password)
                {
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
