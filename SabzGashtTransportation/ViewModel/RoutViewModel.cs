using System.Collections.Generic;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.Enumration;

namespace SabzGashtTransportation.ViewModel
{
    using System;
    public class RoutViewModel
    {
        public int RoutID { get; set; }
        //  public string Name { get; set; }
        public int ShiftType { get; set; }
        public ShiftTypeEnum ShiftTypeEnum { get; set; }
        public int RoutTransactionType { get; set; }
        public RoutTransactionTypeEnum RoutTransactionTypeEnum { get; set; }
        public TimeSpan EnterTime { get; set; }
        public string EnterTimeString { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateString { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int AutomobileTypeId { get; set; }
        public int HasCooler { get; set; }
        public int IsBus { get; set; }
        public int Count { get; set; }
        public int Allocate { get; set; }
        public bool IsActive { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public string CreatedDateString { get; set; }
        //public DateTime ModifiedDate { get; set; }
        //public string ModifiedDateString { get; set; }

        // public int EnterShiftType { get; set; }
        // public int ExitShiftType { get; set; }
        // public int HasCoolerEnter { get; set; }
        // public int HasNotCoolerEnter { get; set; }
        // public int HasCoolerExit { get; set; }
        // public int HasNotCoolerExit { get; set; } 
        //public int HasCoolerEnterBus { get; set; }
        //public int HasCoolerEnterMiniBus { get; set; }
        // public int HasNotCoolerEnterBus { get; set; }
        // public int HasNotCoolerEnterMiniBus { get; set; }
        // public int HasCoolerExitBus { get; set; }
        // public int HasCoolerExitMiniBus { get; set; }
        // public int HasNotCoolerExitBus { get; set; }  
        // public int HasNotCoolerExitMiniBus { get; set; }


        //public int HasCoolerEnterBusRoutTransactionSingle { get; set; }
        //public int HasCoolerEnterBusRoutTransactionRegular { get; set; }
        //public int HasCoolerEnterBusRoutTransactionThereeFour { get; set; }
        //public int HasCoolerEnterBusRoutTransactionFiveSeven { get; set; }

        //public int HasCoolerEnterMiniBusRoutTransactionSingle { get; set; }
        //public int HasCoolerEnterMiniBusRoutTransactionRegular { get; set; }
        //public int HasCoolerEnterMiniBusRoutTransactionThereeFour { get; set; }
        //public int HasCoolerEnterMiniBusRoutTransactionFiveSeven { get; set; }

        //public int HasNotCoolerEnterBusRoutTransactionSingle { get; set; }
        //public int HasNotCoolerEnterBusRoutTransactionRegular { get; set; }
        //public int HasNotCoolerEnterBusRoutTransactionThereeFour { get; set; }
        //public int HasNotCoolerEnterBusRoutTransactionFiveSeven { get; set; }

        //public int HasNotCoolerEnterMiniBusRoutTransactionSingle { get; set; }
        //public int HasNotCoolerEnterMiniBusRoutTransactionRegular { get; set; }
        //public int HasNotCoolerEnterMiniBusRoutTransactionThereeFour { get; set; }
        //public int HasNotCoolerEnterMiniBusRoutTransactionFiveSeven { get; set; }

        //public int HasCoolerExitBusRoutTransactionSingle { get; set; }
        //public int HasCoolerExitBusRoutTransactionRegular { get; set; }
        //public int HasCoolerExitBusRoutTransactionThereeFour { get; set; }
        //public int HasCoolerExitBusRoutTransactionFiveSeven { get; set; }

        //public int HasCoolerExitMiniBusRoutTransactionSingle { get; set; }
        //public int HasCoolerExitMiniBusRoutTransactionRegular { get; set; }
        //public int HasCoolerExitMiniBusRoutTransactionThereeFour { get; set; }
        //public int HasCoolerExitMiniBusRoutTransactionFiveSeven { get; set; }

        //public int HasNotCoolerExitBusRoutTransactionSingle { get; set; }
        //public int HasNotCoolerExitBusRoutTransactionRegular { get; set; }
        //public int HasNotCoolerExitBusRoutTransactionThereeFour { get; set; }
        //public int HasNotCoolerExitBusRoutTransactionFiveSeven { get; set; }

        //public int HasNotCoolerExitMiniBusRoutTransactionSingle { get; set; }
        //public int HasNotCoolerExitMiniBusRoutTransactionRegular { get; set; }
        //public int HasNotCoolerExitMiniBusRoutTransactionThereeFour { get; set; }
        //public int HasNotCoolerExitMiniBusRoutTransactionFiveSeven { get; set; }


        public int RoutTransactionSingleBus { get; set; }
        public int RoutTransactionSingleMiniBus { get; set; }
        public int RoutTransactionSingleHasCoolerBus { get; set; }
        public int RoutTransactionSingleHasCoolerMiniBus { get; set; }
        public int RoutTransactionRegularBus { get; set; }
        public int RoutTransactionRegularMiniBus { get; set; }
        public int RoutTransactionRegularHasCoolerBus { get; set; }
        public int RoutTransactionRegularHasCoolerMiniBus { get; set; }
        public int RoutTransactionThereeFourBus { get; set; }
        public int RoutTransactionThereeFourMiniBus { get; set; }
        public int RoutTransactionThereeFourHasCoolerBus { get; set; }
        public int RoutTransactionThereeFourHasCoolerMiniBus { get; set; }
        public int RoutTransactionFiveSevenBus { get; set; }
        public int RoutTransactionFiveSevenMiniBus { get; set; }
        public int RoutTransactionFiveSevenHasCoolerBus { get; set; }
        public int RoutTransactionFiveSevenHasCoolerMiniBus { get; set; }




        public AutomobileTypeTbl AutomobileTypeTbl { get; set; }
        public ICollection<RegionTbl> RegionTblList { get; set; }
        public AutomobileTypeEnum IsBusEnum { get; set; }
        public HasCoolerEnum HasCoolerEnum { get; set; }

        //public virtual ICollection<DriverRoutTbl> DriverRoutTbls { get; set; }
        //public virtual RegionTbl RegionTbl { get; set; }
    }
}
