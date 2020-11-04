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
    public class person
    {
        [Key]
        [DisplayName("username")]
        [StringLength(20)]
        [Required(ErrorMessage = "anda harus mengisi kolom username!")]
        public string username { get; set; }
        [Required(ErrorMessage = "anda harus mengisi kolom password!")]
        [DisplayName("password")]
        [StringLength(50)]
        public string password { get; set; }
        [Required(ErrorMessage = "anda harus mengisi kolom jabatan!")]
        [DisplayName("jabatan")]
        [StringLength(50)]
        public string jabatan { get; set; }
        [Required(ErrorMessage = "anda harus mengisi kolom pertanyaan!")]
        [DisplayName("pertanyaan")]
        [StringLength(50)]
        public string pertanyaan { get; set; }
        [Required(ErrorMessage = "anda harus mengisi kolom jawaban!")]
        [DisplayName("jawaban")]
        [StringLength(50)]
        public string jawaban { get; set; }
    }
}