using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net; 
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyRestaurantDB.Model
{
    internal class MenuItem
    {
        private DataRow _Row;
        public MenuItem(DataRow aRow)
        {
            int ItemID = (int)aRow["ItemID"];
            DataTable dt = DBEngine.GetTable("Select * From menuitem where ItemID=" + ItemID.ToString());

            _Row = dt.Rows[0];
        }

        public static DataTable search(string filter)
        {
            DataTable Tbl = new DataTable();
            string SQL = "select * from menuitem ";
            if (filter.Trim() != "") { SQL += "where " + filter.Trim(); }

            Tbl = DBEngine.GetTable(SQL);

            return Tbl;
        }
        public static DataTable menuItemTable()
        {
            return DBEngine.GetTable("select * from menuitem");
        }
        public static void createNew(string name, decimal price, string category, string note)
        {//
            string SQL = "INSERT INTO menuitem (Itemname, Price, Category, SpecialNotes) VALUES ('" + name + "', " + price + ", '" + category + "', '" + note + "');";
            DBEngine.Execute(SQL);
        }
         
        public void save()
        {
            string SQL = $"UPDATE Menuitem SET ItemName='{ItemName}', Price='{Price}', Category='{Category}', SpecialNotes='{SpecialNotes}' where ItemID = '{ItemID}'";
            DBEngine.Execute(SQL);
        }

        public void delete()
        {
            string sqldel = $"SELECT * FROM MenuItem Where ItemID=\"{ItemID}\"";
            DataTable dt = DBEngine.GetTable(sqldel);
            /*if (dt.Rows.Count > 0)
            {
                MessageBox.Show($"You cannot delete this Item!, {dt.Rows[0]}");
            }
            else
            {
                string SQL = "DELETE FROM MenuItem WHERE ItemID=" + ItemID.ToString();
                DBEngine.Execute(SQL);
            }*/
            string sqldelchild = $"DELETE FROM menuitemorder Where ItemID=\"{ItemID}\"";
            string SQL = "DELETE FROM MenuItem WHERE ItemID=" + ItemID.ToString();

            DBEngine.Execute(SQL);
        }
        public int ItemID => (int)_Row["ItemID"];
        public string ItemName
        {
            get
            {
                return (string)_Row["ItemName"];
            }
            set
            {
                _Row["ItemName"] = value;
            }
        }
        public Decimal Price
        {
            get
            {
               return (Decimal)_Row["Price"];
            }
            set
            {
                _Row["Price"] = value;
            }
        }
        public String Category
        {
            get
            {
                return (String)_Row["Category"];
            }
            set
            {
                _Row["Category"] = value;
            }
        }
        public String SpecialNotes
        {
            get
            {
                if (_Row["SpecialNotes"].GetType() != typeof(DBNull))
                {
                    return (string)_Row["SpecialNotes"];
                }
                return "";
                
            }
            set
            {
                _Row["SpecialNotes"] = value;
            }
        }




    }




}
   

