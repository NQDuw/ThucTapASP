using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTap2
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Mã loại sản phẩm không được để trống.")]
        public string Ma_LSP { get; set; }
        [Required(ErrorMessage = "Tên loại sản phẩm không được để trống.")]
        public string Ten_LSP { get; set; }
        public string Ghi_Chu { get; set; }
    }
}
