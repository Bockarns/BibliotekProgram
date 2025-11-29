using IndividuelltProjekt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IndividuelltProjekt.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public List<Loan> Loans { get; set; } = new();
        public User()
        {
            if (Username != null)
                Username = "";
            if (Password != null)
                Password = "";
        }
        public static void AddUser(string username, string password, bool admin)
        {
            using (var context = new UserContext())
            {
                var user = new User { Username = username, Password = password, Admin = admin };
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        public static bool CheckUser(string username)
        {
            using (var context = new UserContext())
            {
                return context.Users.Any(u => u.Username == username);
            }
        }
        public static bool CheckUserAdmin(string username)
        {
            using (var context = new UserContext())
            {
                return context.Users.Where(u => u.Username == username).Select(u => u.Admin).FirstOrDefault();
            }
        }
        public static User GetUser(string username)
        {
            using (var context = new UserContext())
            {
                return context.Users.FirstOrDefault(u => u.Username == username)!;
            }
        }
        public static int GetUserId(string username)
        {
            using (var context = new UserContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                return user.Id;
            }
        }
        public static User UpdateUsername(string username, string updateusername)
        {
            using (var context = new UserContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                    return null!;

                user.Username = updateusername;

                context.SaveChanges();

                return user;
            }
        }
        public static User UpdatePassword(string username, string password, string updatepassword)
        {
            using (var context = new UserContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Password == password);
                if (user == null)
                    return null!;

                user.Password = updatepassword;

                context.SaveChanges();

                return user;
            }
        }
        public static void DeleteUser(string username)
        {
            using (var context = new UserContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                context.Users.Remove(user!);
                context.SaveChanges();
            }
        }
    }
    
}
