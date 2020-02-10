﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.IService
{
    public interface IAutomobileTypeService
    {
        void AddNewAutomobileType(AutomobileTypeTbl automobileType);
        IList<AutomobileTypeTbl> GetAllAutomobileTypes();
    }
}
