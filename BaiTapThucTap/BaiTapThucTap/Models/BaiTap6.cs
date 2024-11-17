using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTap6
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "User không được để trống.")]
        public string Ma_Dang_Nhap { get; set; } 
        [ForeignKey("Ma_Dang_Nhap")]
        public ApplicationUser User { set; get; }

        [Required(ErrorMessage = "Kho Id không được để trống.")]
        public int? Kho_ID { set; get; }
        [ForeignKey("Kho_ID")]
        public BaiTap5 Kho { set; get; }
    }
}
