using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaiTapThucTap.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required(ErrorMessage = "Chọn Quyền không được rỗng")]
        public int Kho_ID { set; get; }

    }
}
