//using IndividuelltProjekt.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace IndividuelltProjekt.Models
//{
//    public class Admin
//    {
//        public int Id { get; set; }
//        public string Username { get; set; }
//        public string Password { get; set; }
//        public Admin()
//        {
//            if (Username != null)
//                Username = "";
//            if (Password != null)
//                Password = "";
//        }
//        public static void AddUser(string username, string password)
//        {
//            using (var context = new AdminContext())
//            {
//                var admin = new Admin { Username = username, Password = password };
//                context.Admins.Add(admin);
//                context.SaveChanges();
//            }
//        }
//        public static bool CheckUser(string username)
//        {
//            using (var context = new AdminContext())
//            {
//                return context.Admins.Any(u => u.Username == username);
//            }
//        }
//        public static Admin GetUser(string username)
//        {
//            using (var context = new AdminContext())
//            {
//                return context.Admins.FirstOrDefault(u => u.Username == username)!;
//            }
//        }
//        public static Admin UpdateUsername(string username, string updateusername)
//        {
//            using (var context = new AdminContext())
//            {
//                var admin = context.Admins.FirstOrDefault(u => u.Username == username);
//                if (admin == null)
//                    return null!;

//                admin.Username = updateusername;

//                context.SaveChanges();

//                return admin;
//            }
//        }
//        public static Admin UpdatePassword(string username, string password, string updatepassword)
//        {
//            using (var context = new AdminContext())
//            {
//                var admin = context.Admins.FirstOrDefault(u => u.Password == password);
//                if (admin == null)
//                    return null!;

//                admin.Password = updatepassword;

//                context.SaveChanges();

//                return admin;
//            }
//        }
//        public static void DeleteUser(string username)
//        {
//            using (var context = new AdminContext())
//            {
//                var admin = context.Admins.FirstOrDefault(u => u.Username == username);
//                context.Admins.Remove(admin!);
//                context.SaveChanges();
//            }
//        }
//    }
//}
