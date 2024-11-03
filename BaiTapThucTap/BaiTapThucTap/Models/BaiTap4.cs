using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTap4
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Mã nhà cung cấp không được để trống.")]
        public string Ma_NCC { get; set; }
        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống.")]
        public string Ten_NCC { get; set; }
        public string Ghi_Chu { get; set; }
    }
}
