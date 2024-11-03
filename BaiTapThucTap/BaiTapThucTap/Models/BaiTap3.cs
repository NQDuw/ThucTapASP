using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class BaiTap3
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã sản phẩm không được để trống.")]
        public string Ma_San_Pham { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        public string Ten_San_Pham { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại sản phẩm.")]
        public int Loai_San_Pham_ID { get; set; }

        [ForeignKey("Loai_San_Pham_ID")]
        public virtual BaiTap2 LoaiSP { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn đơn vị tính.")]
        public int Don_Vi_Tin_ID { get; set; }

        [ForeignKey("Don_Vi_Tin_ID")]
        public virtual BaiTap1 DonViTinh { get; set; }

        public string Ghi_Chu { get; set; }
    }

}
