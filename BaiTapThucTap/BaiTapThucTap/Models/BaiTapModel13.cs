using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTapModel13:ViewModelBai11
    {
        public BaiTap11 Bai11 { get; set; }
        public List<BaiTapModel11_2> Bai11_2 { get; set; }
        public decimal TriGia { get; set; }
    }
}
