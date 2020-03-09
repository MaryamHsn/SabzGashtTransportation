using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabz.ServiceLayer.Enumration
{ 
    public enum RoutTransactionTypeEnum
    {
        [Display(Name = " ورودی")]
        Enter = 0,
        [Display(Name = "  خروجی")]
        Exit = 1,
            [Display(Name = "خروجی ناقص رفت")]
        EnterExit1 = 2,
        [Display(Name = "خروجی ناقص برگشت")]
        EnterExit2 = 3
    }
}
