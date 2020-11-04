using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class perAdmin
    {
        [Key]
        public string adminCode { get; set; }
        public string username { get; set; }
        public string namaAdmin { get; set; }
        public string kotaAdmin { get; set; }
        public string alamatAdmin { get; set; }
        public string noTeleponAdmin { get; set; }
    }
}