 
 
using OilStationMVC.Helper;
using OilStationMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class CustomerTypeViewModel
    {
        private readonly DataBase.DB db;

        public CustomerTypeViewModel(DataBase.DB db)
        {
            this.db = db;
        }
        public async Task<List<CustomerType>> ListAsync()
        {
            var lst = new List<CustomerType>();
            //lst = await db.CustomerTypes.ToListAsync();
            return lst;
        }
        //public async Task<CustomerType> FindByIdAsync(object Id)
        //{
        //    return await db.CustomerTypes.SingleOrDefaultAsync(A => A.Id == ObjectConverter.CInt(Id));
        //}
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
