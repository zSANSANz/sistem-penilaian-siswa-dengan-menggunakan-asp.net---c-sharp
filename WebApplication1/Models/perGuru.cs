using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class perGuru
    {
        [Key]
        public string nik { get; set; }
        public string username { get; set; }
        public string namaGuru { get; set; }
        public string kotaGuru { get; set; }
        public string alamatGuru { get; set; }
        public string noTeleponGuru { get; set; }
    }
}