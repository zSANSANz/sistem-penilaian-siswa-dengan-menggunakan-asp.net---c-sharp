using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class sysMapel
    {
        [Key]
        [DisplayName("kode mata kuliah")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Harus diantara 2 sampai 10 karakter.")]
        [Required(ErrorMessage = "anda harus mengisi kolom kode mata kuliah!")]
        
        public string mapelCode { get; set; }
        [DisplayName("nama Mata Pelajaran")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Harus diantara 4 sampai 40 karakter.")]
        [Required(ErrorMessage = "anda harus mengisi kolom nama mata pelajaran!")]
        public string namaMapel { get; set; }
    }
}