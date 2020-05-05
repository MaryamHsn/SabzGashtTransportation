using Sabz.DomainClasses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SabzGashtTransportation.ViewModel
{
    public class RoutFullViewModel
    {

        public string SearchDateFrom { get; set; }
        public string SearchDateTo { get; set; }
        public int RegionId { get; set; }
        public int DriverId { get; set; }

        public IEnumerable<RegionTbl> Regions { get; set; }
        public PagedList.IPagedList<SabzGashtTransportation.ViewModel.RoutViewModel> RoutViewModels { get; set; }

    }
}