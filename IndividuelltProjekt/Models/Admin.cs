using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividuelltProjekt.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Adminusername { get; set; }
        public string Adminpassword { get; set; }
        public Admin()
        {
            if (Adminusername != null)
                Adminusername = "";
            if (Adminpassword != null)
                Adminpassword = "";
        }
    }
}
