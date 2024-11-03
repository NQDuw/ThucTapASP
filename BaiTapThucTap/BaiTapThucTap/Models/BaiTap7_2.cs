using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTap7_2
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Nhập kho")]
        public int Nhap_Kho_ID { get; set; }

        [ForeignKey("Nhap_Kho_ID")]
        public virtual BaiTap7 NhapKho { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Sản Phẩm")]
        public int San_Pham_ID { get; set; }
        [ForeignKey("San_Pham_ID")]
        public virtual BaiTap3 sanpham { get; set; }
        [Required(ErrorMessage = "Chưa nhập số lượng nhập")]
        public int SL_Nhap { get; set; }

        [Required(ErrorMessage = "Chưa nhập ngày nhập kho")]
        public int Don_Gia_Nhap { get; set; }

    }
}
