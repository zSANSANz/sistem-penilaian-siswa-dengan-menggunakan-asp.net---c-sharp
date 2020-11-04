using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class nilUtsUas
    {
        [Key]
        public int utsUasCode { get; set; }
        public string sekolahCode { get; set; }
        public string kelasCode { get; set; }
        public string nis { get; set; }
        public string mapelCode { get; set; }
        public string nik { get; set; }
        public int nilaiUts { get; set; }
        public int nilaiUas { get; set; }
    }
}