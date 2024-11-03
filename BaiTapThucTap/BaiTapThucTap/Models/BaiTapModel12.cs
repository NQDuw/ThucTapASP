using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTapModel12:ViewModelBai11
    {
        [Key]
        public int Id { get; set; }
        public string Ghi_Chu { get; set; }
        public int Kho_ID { get; set; }
        public int NCC_ID { get; set; }
        public DateTime Ngay_Xuat_Kho { get; set; }
        public int SL_Xuat { get; set; }
        public int Don_Gia_Xuat { get; set; }
        public int San_Pham_ID { get; set; }
        public string So_Phieu_Xuat_Kho { get; set; }
    }
}
