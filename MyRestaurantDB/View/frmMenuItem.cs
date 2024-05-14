using MyRestaurantDB.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyRestaurantDB.View
{
    public partial class frmMenuItem : Form
    {
        private DataTable dt = new DataTable();
        private Model.MenuItem selectedMenuItem;

        public frmMenuItem()
        {
            InitializeComponent();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            showMenuItems();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Model.MenuItem.createNew(
                txtItemName.Text,
                Decimal.Parse(txtPrice.Text), 
                txtCategory.Text, 
                txtSpecialNotes.Text);
            showMenuItems();

        }
        private void showMenuItems()
        {
            // DataTable aTable = DBEngin.GetTable("select * from MenuItem");
            DataTable aTable = Model.MenuItem.menuItemTable();
            dataGridView1.DataSource = aTable;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedMenuItem.ItemName = txtItemName.Text;
            selectedMenuItem.Price = Decimal.Parse(txtPrice.Text);
            selectedMenuItem.Category = txtCategory.Text;
            selectedMenuItem.SpecialNotes = txtSpecialNotes.Text;

            selectedMenuItem.save();
            showMenuItems();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete Menu Item with " +
            "Item Name (" + selectedMenuItem.ItemName + ")", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                selectedMenuItem.delete();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
            showMenuItems();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView Grd = dataGridView1;
                DataTable Tbl = (DataTable)Grd.DataSource;
                DataRow SelRow = Tbl.Rows[e.RowIndex];
                Model.MenuItem menuItem = new Model.MenuItem(SelRow);

                txtItemID.Text = menuItem.ItemID.ToString();
                txtItemName.Text = menuItem.ItemName;
                txtPrice.Text = menuItem.Price.ToString();
                txtCategory.Text = menuItem.Category;
                txtSpecialNotes.Text = menuItem.SpecialNotes;

                selectedMenuItem = menuItem;
            }
            catch (Exception ex) {
               // txtSpecialNotes.Text = ex.Message;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Model.MenuItem.search("ItemName like '%" + txtSearch.Text.Trim() + "%'");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
