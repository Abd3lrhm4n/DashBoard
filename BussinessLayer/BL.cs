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
                _myContext.Database.ExecuteSqlCommand(
                    $"Item_Insert @Name = {item.Name}, @BarCode = {item.BarCode}, @Price = {item.Price}");

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
                _myContext.Database.ExecuteSqlCommand(
                    $"Item_Update @Name = {item.Name}, @BarCode = {item.BarCode}, @Price = {item.Price}, @Id = {item.Id}");

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
                _myContext.Database.ExecuteSqlCommand($"Item_Delete @Id = {item.Id}");
                return $"تم حذف الصنف بنجاح";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
