using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTap8:ViewModelBai7
    {
        [Key]
        public int Id { get; set; }
        public string So_Phieu_Nhap_Kho { get; set; }
        public int Kho_ID { get; set; }
        public int NCC_ID { get; set; }
        public DateTime Ngay_Nhap_Kho { get; set; }
        public string Ghi_Chu { get; set; }
    }
}
