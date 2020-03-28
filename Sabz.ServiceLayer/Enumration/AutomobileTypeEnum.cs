using System.ComponentModel.DataAnnotations;

namespace Sabz.ServiceLayer.Enumration
{
    public enum AutomobileTypeEnum
    {
        [Display(Name = "مینی بوس")]
        MiniBus = 0,
        [Display(Name = "اتوبوس")]
        Bus = 1
    }
}
