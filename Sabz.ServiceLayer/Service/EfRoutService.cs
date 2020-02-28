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
    public class EfRoutService : IRoutService
    {
        IUnitOfWork _uow;
        readonly IDbSet<RoutTbl> _routs;
        public EfRoutService(IUnitOfWork uow)
        {
            _uow = uow;
            _routs = _uow.Set<RoutTbl>();
        }

        public void AddNewRout(RoutTbl rout)
        {
            _routs.Add(rout);
        }

        public IList<RoutTbl> GetAllRouts()
        {
            return _routs.Where(x => x.IsActive).ToList();
        }

        public RoutTbl GetRout(int? id)
        {
            return _routs.Where(x => x.IsActive&& x.RoutID==id).FirstOrDefault();
        }

        public RoutTbl GetRoutByName(string name)
        {
            return _routs.Where(x => x.IsActive && x.Name == name).FirstOrDefault();
        }

        public int Delete(int id)
        {
            RoutTbl rout = _routs.Find(id);
            rout.IsActive = false;
            return rout.RoutID;
        }
    }
}
