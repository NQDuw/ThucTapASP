using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTap11
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Chưa nhập số phiếu kho")]
        public string So_Phieu_Xuat_Kho { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn Kho")]
        public int Kho_ID { get; set; }
        [ForeignKey("Kho_ID")]
        public virtual BaiTap5 Kho { get; set; }

        [Required(ErrorMessage = "Chưa nhập ngày xuất kho")]
        [DataType(DataType.Date, ErrorMessage = "sai kiểu dữ liệu datetime")]
        public DateTime Ngay_Xuat_Kho { get; set; }
        public string Ghi_Chu { get; set; }
    }
}
