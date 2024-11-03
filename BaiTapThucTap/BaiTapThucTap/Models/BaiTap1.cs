using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTap1
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên đơn Vị tính không được để trống.")]
        public string Ten_Don_Vi_Tinh { get; set; }
        public string Ghi_Chu { get; set; }
    }
}
