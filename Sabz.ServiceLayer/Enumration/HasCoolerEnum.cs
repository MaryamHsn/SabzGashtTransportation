using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabz.ServiceLayer.Enumration
{
    public enum HasCoolerEnum
    {
        [Display(Name = "معمولی")]
        HasNotCooler = 0,
        [Display(Name = "کولر دارد")]
        HasCooler = 1
    }
}
