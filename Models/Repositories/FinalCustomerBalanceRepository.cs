 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class FinalCustomerBalanceRepository
    {
        DataBase.DB db;
        public FinalCustomerBalanceRepository(DataBase.DB db)
        {
            this.db = db;
        } 
    }
}
