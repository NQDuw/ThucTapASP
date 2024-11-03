using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTap5
    {
        [Key]
        public int Id { get; set; }
      
        [Required(ErrorMessage = "Tên kho không được để trống.")]
        public string Ten_Kho { get; set; }
        public string Ghi_Chu { get; set; }
    }
}
