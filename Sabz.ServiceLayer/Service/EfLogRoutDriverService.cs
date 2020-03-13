﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabz.DataLayer.Context;
using Sabz.DomainClasses.DTO;
using Sabz.ServiceLayer.IService;

namespace Sabz.ServiceLayer.Service
{
    public class EfLogRoutDriverService : ILogRoutDriverService
    {
        IUnitOfWork _uow;
        readonly IDbSet<LogRoutDriverTbl> _logRoutDrivers;
        protected readonly DateTime _now;

        public EfLogRoutDriverService(IUnitOfWork uow)
        {
            _uow = uow;
            _logRoutDrivers = _uow.Set<LogRoutDriverTbl>();
            _now = DateTime.Now;

        }

        public void AddNewLogRoutDriver(LogRoutDriverTbl driverRout)
        {
            driverRout.CreatedDate = _now;
            driverRout.ModifiedDate = _now;
            driverRout.IsActive = true;
            _logRoutDrivers.Add(driverRout);
        }

        public IList<LogRoutDriverTbl> GetAllLogRoutDrivers()
        {
            return _logRoutDrivers.Where(x => x.IsActive).ToList();
        }
        public LogRoutDriverTbl GetLogRoutDriver(int? id)
        {
            return _logRoutDrivers.Find(id);
        }

        public int Delete(int id)
        {
            LogRoutDriverTbl logRoutDriver = _logRoutDrivers.Find(id);
            logRoutDriver.IsActive = false;
            return logRoutDriver.Id;
        }
        

    }
}
