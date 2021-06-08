using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class MenuRepository
    {
        private readonly DataBase.DB db;
        public MenuRepository(DataBase.DB db)
        {
            this.db = db;
        }
        private List<menu> GetMenuTree(List<menu> lst, int? ParentId)
        {
            return lst.Where(x => x.parent == ParentId).Select(x => new menu()
            {
                id = x.id,
                Name = x.Name,
                parent = x.parent,
                IsForm = x.IsForm,
                Action = x.Action,
                Controller = x.Controller,
                FormParameter=x.FormParameter,
                LstChild = GetMenuTree(lst, x.id)
            }).ToList();
        }

        public List<menu> Menus()
        {
            return db.Menus.ToList();

        }


        public List<menu> MenusList()
        {
            List<menu> lst = Menus();
            List<menu> listMenu = GetMenuTree(lst, 0);
            return listMenu;
        }
    }
}
