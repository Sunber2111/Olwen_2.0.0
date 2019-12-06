using Olwen_2._0._0.Model;
using Olwen_2._0._0.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olwen_2._0._0.Repositories.Implements
{
    public class StoreDetailRepository : IStoreDetail
    {
        public bool AddNewPro(StoreDetail sd)
        {
            try
            {
                using(var db = new DbEntities())
                {
                    db.StoreDetails.Add(sd);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePro(StoreDetail sd)
        {
            try
            {
                using (var db = new DbEntities())
                {
                    var sdo = db.StoreDetails.SingleOrDefault(t => t.ProductID == sd.ProductID && t.StoreID == sd.StoreID);
                    db.StoreDetails.Remove(sdo);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateQty(StoreDetail sd)
        {
            try
            {
                using (var db = new DbEntities())
                {
                    var sdo = db.StoreDetails.SingleOrDefault(t => t.ProductID == sd.ProductID && t.StoreID == sd.StoreID);
                    sdo.Quantity = sd.Quantity;
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
