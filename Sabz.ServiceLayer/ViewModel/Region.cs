namespace Sabz.ServiceLayer.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema; 
    public   class Region
    { 
        public int RegionId { get; set; } 
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CFDate { get; set; }
        public string CFDateString { get; set; }
        public DateTime LFDate { get; set; }
        public string LFDateString { get; set; }

        public virtual ICollection<Rout> RoutTbls { get; set; }
    }
}
