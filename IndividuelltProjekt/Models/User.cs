using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividuelltProjekt.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public User()
        {
            if (Username != null)
                Username = "";
            if (Password != null)
                Password = "";
        }
    }
    
}
