using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class sysKelas
    {
        [Key]
        [DisplayName("kode kelas yang dipegang")]
        [StringLength(20)]
        [Required(ErrorMessage = "anda harus mengisi kolom kode kelas yang dipegang!")]
        public string kelasCode { get; set; }
        [DisplayName("nik Wali Kelas")]
        [StringLength(20)]
        [Required(ErrorMessage = "anda harus mengisi kolom kode mata kuliah!")]
        public string nik { get; set; }
    }
}