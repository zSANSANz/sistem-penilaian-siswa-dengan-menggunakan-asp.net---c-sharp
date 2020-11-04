using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class nilAkhir
    {
        [Key]
        public int nilAkhirCode { get; set; }
        public string sekolahCode { get; set; }
        public string kelasCode { get; set; }
        public string nis { get; set; }
        public string mapelCode { get; set; }
        public string nik { get; set; }
        public double nilKI3 { get; set; }
        public double nilKI4 { get; set; }
        public double nilSikap { get; set; }
        public double nilUts { get; set; }
        public double nilUas { get; set; }
        public double nilNaKI3 { get; set; }
    }
}