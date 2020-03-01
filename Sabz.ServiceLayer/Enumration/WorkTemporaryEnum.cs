using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabz.ServiceLayer.Enumration
{ 
    public enum WorkTemporaryEnum
    {
        [Display(Name = "دائمی")]
        IsNotTemporary = 0,
        [Display(Name = "موقتی")]
        IsTemporary = 1
    }
}
