using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class sysSekolah
    {
        [Key]
        [DisplayName("kode sekolah")]
        [StringLength(20)]
        [Required(ErrorMessage = "anda harus mengisi kolom kode sekolah!")]
        public string sekolahCode { get; set; }
        [DisplayName("nama sekolah")]
        [StringLength(50)]
        [Required(ErrorMessage = "anda harus mengisi kolom nama sekolah!")]
        public string namaSekolah { get; set; }
        [DisplayName("kota sekolah")]
        [StringLength(50)]
        [Required(ErrorMessage = "anda harus mengisi kolom kota sekolah!")]
        public string kotaSekolah { get; set; }
        [DisplayName("alamat sekolah")]
        [StringLength(50)]
        [Required(ErrorMessage = "anda harus mengisi kolom alamat sekolah!")]
        public string alamatSekolah { get; set; }
        [DisplayName("nomer telepon sekolah")]
        [StringLength(12)]
        [Required(ErrorMessage = "anda harus mengisi kolom nomer telepon sekolah!")]
        public string noTeleponSekolah { get; set; }
    }
}