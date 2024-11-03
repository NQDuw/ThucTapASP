using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTap15:ViewModelBai7
    {
        [Required(ErrorMessage = "Chưa nhập ngày bắt đầu nhập kho")]
        [DataType(DataType.Date, ErrorMessage = "sai kiểu dữ liệu datetime")]
        public DateTime NgayBatDau { get; set; }
        [Required(ErrorMessage = "Chưa nhập ngày kết thúc nhập kho")]
        [DataType(DataType.Date, ErrorMessage = "sai kiểu dữ liệu datetime")]
        public DateTime NgayKetThuc { get; set; } 
    }
}
