using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class nilPengetahuanKognitifKI3
    {
        [Key]
        public int KI3Code { get; set; }
        public string sekolahCode { get; set; }
        public string kelasCode { get; set; }
        public string nis { get; set; }
        public string mapelCode { get; set; }
        public string nik { get; set; }
        public int KI3nilaiSatu { get; set; }
        public int KI3nilaiDua { get; set; }
        public int KI3nilaiTiga { get; set; }
        public int KI3nilaiEmpat { get; set; }
        public int KI3nilaiTotal { get; set; }
    }
}