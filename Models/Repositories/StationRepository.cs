
using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.Models.Repositories
{
    public class StationRepository : IMainRepository<Station>
    {
        DataBase.DB db;
        public StationRepository()
        {
            db = new DataBase.DB();
        }
         
        public async Task<bool> AddAsync(Station entity)
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
         
        public async Task<Station> FindByIdAsync(object Id)
        {
            string sql = " select * from Stations where nId=" + Id + "";
            DataTable dt =await db.GetDataTableAsync(sql);
            if (dt.Rows.Count > 0)
            {
                return new Station
                {
                    Id = ObjectConverter.CInt(dt.Rows[0]["nId"]),
                    Name = ObjectConverter.Cstr(dt.Rows[0]["sName"]),
                    NameEng = ObjectConverter.Cstr(dt.Rows[0]["sNameEng"]),
                    Adress = ObjectConverter.Cstr(dt.Rows[0]["sAdress"]),
                    Tel = ObjectConverter.Cstr(dt.Rows[0]["sTel"]),
                    ResponsiblePerson = ObjectConverter.Cstr(dt.Rows[0]["sPerson"]),
                    Desc = ObjectConverter.Cstr(dt.Rows[0]["sDesc"]),

                };
            }
            return null;
        }
         
        public async Task<List<Station>> ListAsync()
        {
            string sql = " select * from Stations ";
            DataTable dt = await db.GetDataTableAsync(sql);
            List<Station> lst = new List<Station>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new Station
                    { 
                        Id = ObjectConverter.CInt(row["nId"]),
                        Name = ObjectConverter.Cstr(row["sName"]),
                        NameEng = ObjectConverter.Cstr(row["sNameEng"]),
                        Adress = ObjectConverter.Cstr(row["sAdress"]),
                        Tel = ObjectConverter.Cstr(row["sTel"]),
                        ResponsiblePerson = ObjectConverter.Cstr(row["sPerson"]),
                        Desc = ObjectConverter.Cstr(row["sDesc"]),

                    });
                }
            }
            return lst;
        }
         
        public async Task<bool> ObjectExists(object Id)
        {
            Station entity = await FindByIdAsync(Id);
            if (entity != null)
                return true;
            return false;
        }

        public List<Station> Search(object term)
        {
            throw new NotImplementedException();
        }
         
        public async Task<bool> UpdateAsync(object Id, Station entity)
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

        public bool ExecInsert(Station entity)
        {
            try
            {
                db.NewFields();
                db.Table = "Stations";
                entity.Id = int.Parse(db.GetNewKey("nId", "Stations"));
                db.AddField("nId", entity.Id.ToString());
                db.AddField("nBranchId", CommonFolder.Common.Current.BranchId.ToString());
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldstr("sNameEng", ObjectConverter.Cstr(entity.NameEng));
                db.AddFieldstr("sTel", ObjectConverter.Cstr(entity.Tel));
                db.AddFieldstr("sPerson", ObjectConverter.Cstr(entity.ResponsiblePerson));
                db.AddFieldstr("sAdress", ObjectConverter.Cstr(entity.Adress));
                db.AddFieldstr("sDesc", ObjectConverter.Cstr(entity.Desc));
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
                db.Table = "Stations";
                db.sCondition = " nId=" + Id + "";
                db.ExecDeleteInTrans();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExecUpdate(object Id, Station entity) 
        {
            try
            {
                db.NewFields();
                db.Table = "Stations";
                db.AddFieldstr("sName", ObjectConverter.Cstr(entity.Name));
                db.AddFieldstr("sNameEng", ObjectConverter.Cstr(entity.NameEng));
                db.AddFieldstr("sTel", ObjectConverter.Cstr(entity.Tel));
                db.AddFieldstr("sPerson", ObjectConverter.Cstr(entity.ResponsiblePerson));
                db.AddFieldstr("sAdress", ObjectConverter.Cstr(entity.Adress));
                db.AddFieldstr("sDesc", ObjectConverter.Cstr(entity.Desc)); 
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
