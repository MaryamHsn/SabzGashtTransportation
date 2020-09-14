using Sabz.DomainClasses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SabzGashtTransportation.ViewModel
{
    public class DriverFullViewModel
    {
        public string SearchString { get; set; }
        public string CurrentFilter { get; set; }
        public string SortOrder { get; set; }
        public int? Page { get; set; }
        public int RegionId { get; set; }
        public IEnumerable<RegionTbl> Regions { get; set; }
        public PagedList.IPagedList<SabzGashtTransportation.ViewModel.DriverViewModel> DriverViewModels { get; set; }

    }
}