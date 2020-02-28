using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabz.ServiceLayer.Enumration
{
    public enum WorkDoneEnum
    {
        [Display(Name = "انجام شده")]
        NotDone = 0,
        [Display(Name = "انجام نشده")]
        Done = 1
    }
}
