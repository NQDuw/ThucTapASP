using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTap16:ViewModelBai11
    {
        [Required(ErrorMessage = "Chưa nhập ngày bắt đầu xuất kho")]
        [DataType(DataType.Date, ErrorMessage = "sai kiểu dữ liệu datetime")]
        public DateTime NgayBatDau { get; set; }
        [Required(ErrorMessage = "Chưa nhập ngày kết thúc xuất kho")]
        [DataType(DataType.Date, ErrorMessage = "sai kiểu dữ liệu datetime")]
        public DateTime NgayKetThuc { get; set; } 
    }
}
