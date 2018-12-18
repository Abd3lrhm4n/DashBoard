using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataAccessLayer;

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
                _myContext.Items.Attach(item);
                _myContext.Entry(item).State = EntityState.Modified;
                _myContext.SaveChanges();

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
                var value = _myContext.Items.Find(item);
                _myContext.Entry(value).State = EntityState.Deleted;
                _myContext.SaveChanges();

                return $"تم حذف الصنف بنجاح";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
