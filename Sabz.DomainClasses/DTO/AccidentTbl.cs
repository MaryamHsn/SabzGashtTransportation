namespace Sabz.DomainClasses.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccidentTbl")]
    public partial class AccidentTbl : BaseEntity<int>
    {
        public int? UseInsurence { get; set; }
        public decimal? Cost { get; set; }
        public string Description { get; set; }
        [ForeignKey("DriverTbl")]
        public int? DriverId { get; set; }
        public virtual DriverTbl DriverTbl { get; set; }
        [ForeignKey("AutomobileTbl")]
        public int? AutomobileId { get; set; }
        public virtual AutomobileTbl AutomobileTbl { get; set; }

    }
}
