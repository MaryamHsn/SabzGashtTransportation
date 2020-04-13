using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabz.ServiceLayer.Enumration
{ 
    public enum ShiftTypeEnum
    {
        [Display(Name = "ورودی")]
        Enter = 0,
        [Display(Name = "خروجی")]
        Exit = 1
    }
}
