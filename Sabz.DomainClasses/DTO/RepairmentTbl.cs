namespace Sabz.DomainClasses.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RepairmentTbl")]
    public partial class RepairmentTbl : BaseEntity<int>
    {
        [ForeignKey("DriverTbl")]
        public int DriverId { get; set; }
        public string Descrition { get; set; }
        public decimal? Cost { get; set; }
        public virtual DriverTbl DriverTbl { get; set; }
    }
}
