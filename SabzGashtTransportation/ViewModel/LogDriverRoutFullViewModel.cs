using Sabz.DomainClasses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SabzGashtTransportation.ViewModel
{
    public class LogDriverRoutFullViewModel
    {
        public string SearchDateFrom { get; set; }
        public string SearchDateTo { get; set; }
        public int RegionId { get; set; }
        public int DriverId { get; set; }
        public IEnumerable<RegionTbl> Regions { get; set; }
        public IEnumerable<DriverTbl> Drivers{ get; set; }
        public PagedList.IPagedList<SabzGashtTransportation.ViewModel.LogDriverRoutViewModel> LogDriverRoutViewModels { get; set; }
    }
}