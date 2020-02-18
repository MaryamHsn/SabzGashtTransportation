﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.IService
{
    public interface IAutomobileService
    {
        void AddNewAutomobile(AutomobileTbl automobile);
        IList<AutomobileTbl> GetAllAutomobiles();
        AutomobileTbl GetAutomobile(int? id);
        int Delete(int id);
    }
}
