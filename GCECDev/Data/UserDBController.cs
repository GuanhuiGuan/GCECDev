using System;
using System.Collections.Generic;
using System.IO;
using GCECDev.Models;
using SQLite;
using Xamarin.Forms;

namespace GCECDev.Data
{
    public class UserDBController
    {
        readonly SQLiteConnection _db;

        static object locker = new object();


        public UserDBController()
        {
            _db = DependencyService.Get<ISQLite>().GetConnection();
            _db.CreateTable<User>();
        }


        public List<User> GetUsers()
        {
            lock (locker)
            {
                return _db.Table<User>().ToList();
            }
        }


        public User GetUser(string username)
        {
            lock (locker)
            {
                return _db.Table<User>()
                .Where(i => ((User)i).GetUsername() == username)
                .FirstOrDefault();
            }
        }


        public int SaveUser(User user)
        {
            lock (locker)
            {
                var userFromDB = GetUser(user.GetUsername());
                if (userFromDB.GetUsername() == "")
                {
                    return _db.Insert(user);
                }
                return _db.Update(user);
            }
        }


        public int DeleteUser(User user)
        {
            lock (locker)
            {
                return _db.Delete(user);
            }
        }
    }
}
