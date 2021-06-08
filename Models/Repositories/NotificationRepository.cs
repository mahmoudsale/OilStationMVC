using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OilStationMVC.Models.Repositories
{
    public static class NotificationRepository
    {
        public static async Task<bool> MarkAsReadAsync(int id, string userid)
        {
            try
            {
                DataBase.DB db = new DataBase.DB();
                db.NewFields();
                db.Table = "NotificationRead";
                db.AddFieldstr("UserId", userid);
                db.AddField("NotificationId", id.ToString());
                await db.ExecInsertAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static async Task<List<Notification>> ListAsync()
        {
            DataBase.DB db = new DataBase.DB();
            string sql = " select * from Notification  ";
            DataTable dt = await db.GetDataTableAsync(sql);
            List<Notification> lst = new List<Notification>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new Notification
                    {
                        Id = ObjectConverter.Cstr(row["Id"]),
                        Type = ObjectConverter.Cstr(row["Type"]),
                        Details = ObjectConverter.Cstr(row["Details"]),
                        Title = ObjectConverter.Cstr(row["Title"]),
                        DetailsURL = ObjectConverter.Cstr(row["DetailsURL"]),
                        SentTo = ObjectConverter.Cstr(row["SentTo"]),
                        Date = ObjectConverter.Cstr(row["Date"]),
                        SinceFor = GetSinceFor(ObjectConverter.Cstr(row["Date"])),
                        IsRead = ObjectConverter.CBool(row["IsRead"]),
                        IsDeleted = ObjectConverter.CBool(row["IsDeleted"]),
                        IsReminder = ObjectConverter.CBool(row["IsReminder"]),
                        Code = ObjectConverter.Cstr(row["Code"]),
                        NotificationType = ObjectConverter.Cstr(row["NotificationType"]),
                    });

                }
            }
            return lst;
        }

        public static Notification GenerateNotificationModel(ModelType modelType, ChargeArea model)
        {
            switch (modelType)
            {
                case ModelType.New:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "new stock :" + model.Name,
                        Details = "new stock :" + model.Name,
                        SentTo = "All",
                    };
                case ModelType.Edit:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "stock edited :" + model.Name,
                        Details = "stock edited:" + model.Name,
                        SentTo = "All",
                    };

                case ModelType.Delete:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "stock deleted:" + model.Name,
                        Details = "new delete :" + model.Name,
                        SentTo = "All",
                    };

                default:
                    return new Notification();

            }
        }

        public static Notification GenerateNotificationModel(ModelType modelType, Car model)
        {
            switch (modelType)
            {
                case ModelType.New:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "new stock :" + model.Name,
                        Details = "new stock :" + model.Name,
                        SentTo = "All",
                    };
                case ModelType.Edit:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "stock edited :" + model.Name,
                        Details = "stock edited:" + model.Name,
                        SentTo = "All",
                    };

                case ModelType.Delete:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "stock deleted:" + model.Name,
                        Details = "new delete :" + model.Name,
                        SentTo = "All",
                    };

                default:
                    return new Notification();

            }
        }

        public static Notification GenerateNotificationModel(ModelType modelType, Stock model)
        {
            switch (modelType)
            {
                case ModelType.New:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "new stock :" + model.Name,
                        Details = "new stock :" + model.Name,
                        SentTo = "All",
                    };
                case ModelType.Edit:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "stock edited :" + model.Name,
                        Details = "stock edited:" + model.Name,
                        SentTo = "All",
                    };

                case ModelType.Delete:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "stock deleted:" + model.Name,
                        Details = "new delete :" + model.Name,
                        SentTo = "All",
                    };

                default:
                    return new Notification();

            }
        }

        public static Notification GenerateNotificationModel(ModelType modelType, AreaTransport model)
        {
            switch (modelType)  
            {
                case ModelType.New:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "new stock :" + model.Name,
                        Details = "new stock :" + model.Name,
                        SentTo = "All", 
                    };
                case ModelType.Edit:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "stock edited :" + model.Name,
                        Details = "stock edited:" + model.Name,
                        SentTo = "All", 
                    };
                   
                case ModelType.Delete:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "stock deleted:" + model.Name,
                        Details = "new delete :" + model.Name,
                        SentTo = "All", 
                    };
                   
                default:
                    return new Notification();
            
            }
        }

        internal static Notification GenerateNotificationModel(ModelType modelType, CustomerInvoice model)
        {
            switch (modelType)
            {
                case ModelType.New:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "new invoice :" + model.CustomerId,
                        Details = "new invoice :" + model.CustomerId,
                        SentTo = "All",
                    };
                case ModelType.Edit:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "edit invoice :" + model.CustomerId,
                        Details = "edit invoice :" + model.CustomerId,
                        SentTo = "All",
                    };

                case ModelType.Delete:
                    return new Notification
                    {
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Title = "delete invoice :" + model.CustomerId,
                        Details = "delete invoice :" + model.CustomerId,
                        SentTo = "All",
                    };

                default:
                    return new Notification();

            }
        }

        public static async Task<List<Notification>> ListUnReadAsync(string UserId, DateTime LastUpdateDate)
        {
            DataBase.DB db = new DataBase.DB();
            string sql = " select * from Notification ";
            //sql += " where Date>'"+ LastUpdateDate + "'";
            //sql += " and  Id not in (select NotificationId from NotificationRead where UserId='" + UserId + "') ";
            sql += " order by  Date desc";
            DataTable dt = await db.GetDataTableAsync(sql);
            List<Notification> lst = new List<Notification>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lst.Add(new Notification
                    {
                        Id = ObjectConverter.Cstr(row["Id"]),
                        Type = ObjectConverter.Cstr(row["Type"]),
                        Details = ObjectConverter.Cstr(row["Details"]),
                        Title = ObjectConverter.Cstr(row["Title"]),
                        DetailsURL = ObjectConverter.Cstr(row["DetailsURL"]),
                        SentTo = ObjectConverter.Cstr(row["SentTo"]),
                        Date = ObjectConverter.Cstr(row["Date"]),
                        SinceFor = GetSinceFor(ObjectConverter.Cstr(row["Date"])),
                        IsRead = ObjectConverter.CBool(row["IsRead"]),
                        IsDeleted = ObjectConverter.CBool(row["IsDeleted"]),
                        IsReminder = ObjectConverter.CBool(row["IsReminder"]),
                        Code = ObjectConverter.Cstr(row["Code"]),
                        NotificationType = ObjectConverter.Cstr(row["NotificationType"]),
                        IsNew = CheckIsNew(ObjectConverter.Cstr(row["Date"]), LastUpdateDate)
                    });

                }
            }
            return lst;
        }

        private static bool CheckIsNew(string AddingDate, DateTime lastUpdateDate)
        {
            if (Convert.ToDateTime(AddingDate) > Convert.ToDateTime(lastUpdateDate))
            {
                return true;
            }
            return false;
        }

        private static string GetSinceFor(string Date)
        {
            string SinceFor = "now";
            TimeSpan diff = DateTime.Now - ObjectConverter.CDate(Date);
            if (diff.Days > 0)
            {
                SinceFor = diff.Days.ToString() + "  day ago";
            }
            else
            {
                if (diff.Hours > 0)
                {
                    SinceFor = diff.Hours + "  hour ago";
                }
                else
                {
                    if (diff.Minutes > 0)
                    {
                        SinceFor = diff.Minutes + "  minute ago";
                    }
                    else
                    {
                        SinceFor = diff.Seconds + "  second ago";
                    }
                }
            }
            return SinceFor;
        }

        public static async Task<bool> AddAsync(Notification entity)
        {
            try
            {
                DataBase.DB db = new DataBase.DB();
                db.NewFields();
                db.Table = "Notification";
                db.AddField("nBranchId", CommonFolder.Common.Current.BranchId.ToString());
                db.AddFieldstr("Type", ObjectConverter.Cstr(entity.Type));
                db.AddFieldstr("Details", ObjectConverter.Cstr(entity.Details));
                db.AddFieldstr("Title", ObjectConverter.Cstr(entity.Title));
                db.AddFieldstr("DetailsURL", ObjectConverter.Cstr(entity.DetailsURL));
                db.AddFieldstr("SentTo", ObjectConverter.Cstr(entity.SentTo));
                db.AddFieldstr("Date", DateTime.Now.ToString());
                db.AddFieldstr("Code", ObjectConverter.Cstr(entity.Code));
                db.AddFieldstr("NotificationType", ObjectConverter.Cstr(entity.NotificationType));
                await db.ExecInsertAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }

    public enum ModelType
    {
        New=1,
        Edit=2,
        Delete=3
    }
}