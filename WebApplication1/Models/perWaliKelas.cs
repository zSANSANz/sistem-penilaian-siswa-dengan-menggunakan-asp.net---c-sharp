using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class perWaliKelas
    {
        [Key]
        public string nik { get; set; }
        public string username { get; set; }
        public string namaWaliKelas { get; set; }
        public string kotaWaliKelas { get; set; }
        public string alamatWaliKelas { get; set; }
        public string noTeleponWaliKelas { get; set; }
    }
}