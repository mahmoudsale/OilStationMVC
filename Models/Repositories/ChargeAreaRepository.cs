
using Newtonsoft.Json;
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class ChargeAreaRepository  :IMainRepository<ChargeArea>
    {
        private readonly DataBase.DB db;

        public ChargeAreaRepository()
        {
            this.db = new DataBase.DB();
        }
      
        public async Task<bool> AddAsync(ChargeArea entity)
        {
            try
            {
                ExecInsert(entity);
                DataBase.ActionResult ActionR = await db.Apply();
                if (ActionR.Success == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
         
        public async Task<bool> DeleteByIdAsync(object Id)
        {
            try
            {
                ExecDelete(Id);
                DataBase.ActionResult ActionR = await db.Apply();
                if (ActionR.Success == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
 
        public async Task<ChargeArea> FindByIdAsync(object Id)
        {
            string sql = " select ChargeArea.*,OilCompany.sName as CompanyName from ChargeArea inner join OilCompany on ChargeArea.nCompany=OilCompany.nId where ChargeArea.nId="+Id+"";
            DataTable dt =await db.GetDataTableAsync(sql);
            if (dt.Rows.Count > 0)
            {
                return new ChargeArea
                {
                    Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                    Name = ObjectConverter.Cstr(dt.Rows[0]["sName"]),
                    Cost = ObjectConverter.CDec(dt.Rows[0]["nCost"]),
                    Company = new Company
                    {
                        Id = ObjectConverter.CInt(dt.Rows[0]["nCompany"]),
                        Name = ObjectConverter.Cstr(dt.Rows[0]["CompanyName"]),
                    },

                };
            }
            return null;
        }
         
        public async Task<List<ChargeArea>> ListAsync()
        {
            string sql = " select ChargeArea.*,OilCompany.sName as CompanyName from ChargeArea inner join OilCompany on ChargeArea.nCompany=OilCompany.nId";
            DataTable dt =await db.GetDataTableAsync(sql);
            List<ChargeArea> lst = new List<ChargeArea>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new ChargeArea
                    {
                        Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                        Name = ObjectConverter.Cstr(dt.Rows[0]["sName"]),
                        Cost = ObjectConverter.CDec(dt.Rows[0]["nCost"]),
                        Company = new Company
                        {
                            Id = ObjectConverter.CInt(dt.Rows[0]["nCompany"]),
                            Name = ObjectConverter.Cstr(dt.Rows[0]["CompanyName"]),
                        },

                    });
                }
            }
            return lst;
        }
         

        public async Task<bool> ObjectExists(object Id)
        {
            ChargeArea entity = await FindByIdAsync(Id);
            if (entity != null)
                return true;
            return false;
        }

        public List<ChargeArea> Search(object term)
        {
            throw new NotImplementedException();
        }
 
        public async Task<bool> UpdateAsync(object Id, ChargeArea entity)
        {
            try
            {
                ExecUpdate(Id, entity);
                DataBase.ActionResult ActionR = await db.Apply();
                if (ActionR.Success == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ExecInsert(ChargeArea entity)
        {
            try
            {
                db.NewFields();
                db.Table = "ChargeArea";
                entity.Id = int.Parse(db.GetNewKey("nId", "ChargeArea"));
                db.AddField("nId", entity.Id.ToString());
                db.AddField("nBranchId", CommonFolder.Common.Current.BranchId.ToString());
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldG("nCost", ObjectConverter.Cstr(entity.Cost));
                db.AddField("nCompany", ObjectConverter.Cstr(entity.CompanyId));
                db.ExecInsertInTrans();
            }
            catch (Exception ex)
            {
                return false; ;
            }
            return true;
        }

        public bool ExecDelete(object Id)
        {
            try
            {
                db.NewFields();
                db.Table = "ChargeArea";
                db.sCondition = " nId=" + Id + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExecUpdate(object Id, ChargeArea entity)
        {
            try
            {
                db.NewFields();
                db.Table = "ChargeArea";
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldG("nCost", ObjectConverter.Cstr(entity.Cost));
                db.AddField("nCompany", ObjectConverter.Cstr(entity.CompanyId));
                db.sCondition = " nId=" + Id + " ";
                db.ExecUpdateInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}
