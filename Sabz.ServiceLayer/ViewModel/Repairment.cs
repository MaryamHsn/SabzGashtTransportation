namespace Sabz.ServiceLayer.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RepairmentTbl")]
    public partial class Repairment
    {
        [Key]
        public int RepairmentId { get; set; }

        public int? DriverId { get; set; }

        public string Descrition { get; set; }

        public decimal? Cost { get; set; }
        public bool IsActive { get; set; }
        public DateTime CFDate { get; set; }
        public DateTime LFDate { get; set; }
        public virtual Driver DriverTbl { get; set; }
    }
}
