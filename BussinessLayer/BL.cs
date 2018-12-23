using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using DataAccessLayer;
using Microsoft.Office.Core;

namespace BussinessLayer
{
    public class BL
    {
        private static MyContext _myContext = new MyContext();

        //insert
        public static string InsertItem(Item item)
        {
            try
            {
                _myContext.Items.Add(item);
                _myContext.SaveChanges();

                return $"تم إدخال صنف جديد بنجاح";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        //Update
        public static string UpdateItem(Item item)
        {
            try
            {
                //_myContext.Entry(item).State = EntityState.Modified;
                //_myContext.SaveChanges();
                //_myContext.Entry(item).State = EntityState.Detached;

                List<SqlParameter> param = new List<SqlParameter>();

                param.Add(new SqlParameter("Name", item.Name));
                param.Add(new SqlParameter("Id", item.Id));
                param.Add(new SqlParameter("BarCode", item.BarCode));
                param.Add(new SqlParameter("Price", item.Price));

                _myContext.Database.SqlQuery<Item>("Item_Update @Id, @BarCode, @Name, @Price", param.ToArray());

                return $"تم تعديل الصنف بنجاح";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //delete
        public static string DeleteItem(Item item)
        {
            try
            {
                _myContext.Entry(item).State = EntityState.Deleted;
                _myContext.SaveChanges();
                _myContext.Entry(item).State = EntityState.Detached;
                return $"تم حذف الصنف بنجاح";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
