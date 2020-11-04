using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class perSiswa
    {
        [Key]
        public string nis { get; set; }
        public string username { get; set; }
        public string namaSiswa { get; set; }
        public string kotaSiswa { get; set; }
        public string alamatSiswa { get; set; }
        public string noTeleponSiswa { get; set; }
    }
}