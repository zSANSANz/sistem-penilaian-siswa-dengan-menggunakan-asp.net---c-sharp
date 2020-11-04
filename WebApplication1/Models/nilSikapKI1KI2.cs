using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class nilSikapKI1KI2
    {
        [Key]
        public int KI1KI2Code { get; set; }
        public string sekolahCode { get; set; }
        public string kelasCode { get; set; }
        public string nis { get; set; }
        public string mapelCode { get; set; }
        public string nik { get; set; }
        [Range(0, 100,
            ErrorMessage = "nilai harus berisi angka dan diantara 0-100")]
        public int KI1KI2nilaiSatu { get; set; }
        [Range(0, 100,
            ErrorMessage = "nilai harus berisi angka dan diantara 0-100")]
        public int KI1KI2nilaiDua { get; set; }
        [Range(0, 100,
            ErrorMessage = "nilai harus berisi angka dan diantara 0-100")]
        public int KI1KI2nilaiTiga { get; set; }
        [Range(0, 100,
            ErrorMessage = "nilai harus berisi angka dan diantara 0-100")]
        public int KI1KI2nilaiEmpat { get; set; }
        [Range(0, 100,
            ErrorMessage = "nilai harus berisi angka dan diantara 0-100")]
        public int KI1KI2nilaiTotal { get; set; }
    }
}