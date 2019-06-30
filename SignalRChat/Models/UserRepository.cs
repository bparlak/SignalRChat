using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Models
{
    public class UserRepository
    {
        static UserRepository()
        {
            UserList = new List<string>();
        }
        public static List<string> UserList { get; set; }
        public static void AddUser(string userName)
        {
            UserList.Add(userName);
        }
        public static void RemoveUser(string userName)
        {
            UserList.Remove(userName);
        }
    }
}
