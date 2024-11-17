using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTap17 : BaiTap3 
    { 
        [Required(ErrorMessage = "Chưa nhập ngày bắt đầu nhập kho")]
        [DataType(DataType.Date, ErrorMessage = "sai kiểu dữ liệu datetime")]
        public DateTime NgayBatDau { get; set; }

        [Required(ErrorMessage = "Chưa nhập ngày kết thúc nhập kho")]
        [DataType(DataType.Date, ErrorMessage = "sai kiểu dữ liệu datetime")]
        public DateTime NgayKetThuc { get; set; }
        public BaiTap7 Bai7 { get; set; }
        public BaiTap7_2 Bai7_2 { get; set; }
        public BaiTap11 Bai11 { get; set; }
        public BaiTapModel11_2 Bai11_2 { get; set; }
        public int SL_Ky_Truoc { get; set; }
        public int SL_Dau_Ky { get; set; }
        public int SL_Cuoi_Ky { get; set; }
        public int SL_Nhap { get; set; }
        public int SL_Xuat { get; set; }
        public BaiTap3 Bai3{ get; set; }

    }
}
