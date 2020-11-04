using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class nilKetrampilanPsikomotorikKI4
    {
        [Key]
        public int KI4Code { get; set; }
        public string sekolahCode { get; set; }
        public string kelasCode { get; set; }
        public string nis { get; set; }
        public string mapelCode { get; set; }
        public string nik { get; set; }
        public int KI4nilaiSatu { get; set; }
        public int KI4nilaiDua { get; set; }
        public int KI4nilaiTiga { get; set; }
        public int KI4nilaiEmpat { get; set; }
        public int KI4nilaiTotal { get; set; }
    }
}