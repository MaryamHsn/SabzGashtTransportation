﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabz.DomainClasses.DTO;

namespace Sabz.ServiceLayer.IService
{
    public interface IAccidentService
    {
        void AddNewAccident(AccidentTbl accident);
        IList<AccidentTbl> GetAllAccidents();
        AccidentTbl GetAccident(int? id);
      //  int Delete(int id);
        bool Delete(int  id);
    }
}
