using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabz.ServiceLayer.Enumration
{
    public enum RoutTypeEnum
    {
        [Display(Name = "مسیر همیشگی")]
        Always = 0,
        [Display(Name = "مسیر موقتی")]
        Temporary = 1
    }
}
