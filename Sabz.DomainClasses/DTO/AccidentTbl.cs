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
        [Key]
        public int AccidentId { get; set; }
        public int? UseInsurence { get; set; }
        public decimal? Cost { get; set; }
        public string Description { get; set; }
        public int? DriverId { get; set; }
        public virtual DriverTbl DriverTbl { get; set; }
        public int? AutomobileId { get; set; }
        public virtual AutomobileTbl AutomobileTbl { get; set; }

    }
}
