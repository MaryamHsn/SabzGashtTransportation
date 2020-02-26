using System.ComponentModel.DataAnnotations;

namespace Sabz.ServiceLayer.Enumration
{
    public enum AutomobileTypeEnum
    {//, ResourceType=typeof(Resources.Enums)
        [Display(Name = "اتوبوس")]
        Bus = 0,
        [Display(Name = "مینی بوس")]
        MiniBus = 1
    }
}
