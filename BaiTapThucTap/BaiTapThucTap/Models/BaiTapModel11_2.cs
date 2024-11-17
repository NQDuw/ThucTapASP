using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTapModel11_2
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn xuất kho Id")]
        public int Xuat_Kho_ID { get; set; }

        [ForeignKey("Xuat_Kho_ID")]
        public virtual BaiTap11 XuatKho { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Sản Phẩm")]
        public int San_Pham_ID { get; set; }
        [ForeignKey("San_Pham_ID")]
        public virtual BaiTap3 sanpham { get; set; }
        [Required(ErrorMessage = "Chưa nhập số lượng Xuất")]
        public int SL_Xuat { get; set; }

        [Required(ErrorMessage = "Chưa nhập đơn giá xuất kho")]
        public int Don_Gia_Xuat { get; set; }
    }
}
