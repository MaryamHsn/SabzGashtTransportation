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
        [Display(Name = " تک نیمراه")]
        Single = 0,
        [Display(Name = "عادی")]
        Regular = 1,
        [Display(Name = "سه و چهار نیمراه")]
        ThereeFour = 2,
        [Display(Name = "پنج و هفت نیمراه")]
        FiveSeven = 3
    }
}
