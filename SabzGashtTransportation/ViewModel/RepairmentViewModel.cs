using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
     
    public partial class RepairmentViewModel
    { 
        public int RepairmentId { get; set; }

        public int? DriverId { get; set; }

        public string Descrition { get; set; }

        public decimal? Cost { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual DriverTbl DriverTbl { get; set; }
    }
}
