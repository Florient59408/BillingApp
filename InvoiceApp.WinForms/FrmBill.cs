using InvoiceApp.BLL;
using InvoiceApp.BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceApp.WinForms
{
    public partial class FrmBill : Form
    {

        List<Product> products;
        public FrmBill()
        {
            InitializeComponent();
            FrmPreview.Printed += btnClear_Click;
            products = new List<Product>();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {            
            
            txtName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            nudQuantity.Value = 0 ;
            txtReference.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtReference.Text == string.Empty || txtName.Text == string.Empty)
                    throw new Exception("Reference and name can't be empty !");

                if (!double.TryParse(txtPrice.Text, out _))
                    throw new Exception("Invalid price !");

                if(nudQuantity.Value == 0)
                    throw new Exception("Invalid quantity !");

                if (!int.TryParse(txtPhoneNumber.Text, out _))
                    throw new Exception("Invalid phone number !");


                var product = new Product(txtReference.Text, txtName.Text, double.Parse(txtPrice.Text), (int)nudQuantity.Value);

                foreach (var p in products)
                    if (p.Reference == product.Reference)
                        throw new Exception("Invalid reference !");

                products.Add(product);

                LoadProduct();
                btnReset_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadProduct()
        {
            lvProductList.Items.Clear();

            foreach(var product in products)
            {
                ListViewItem item = new ListViewItem(new string[] { product.Reference, product.Name, product.Price.ToString(), product.Qantity.ToString() });
                item.Tag = product;
                lvProductList.Items.Add(item);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            try
            {
                ProductManager manager = new ProductManager(txtClientName.Text, int.Parse(txtPhoneNumber.Text));
                if (!int.TryParse(txtPhoneNumber.Text, out _))
                    throw new Exception("Invalid phone number !");

                if (txtClientName.Text == string.Empty)
                    throw new Exception("Name can't be empty !");

                FileInfo file = new FileInfo("parameters.txt");
                string[] oldSettings = File.ReadAllText(file.FullName).Split('|');

                Program.ClientNumber = int.Parse(oldSettings[4]);
                Program.ClientNumber++;

                using (var sW = new StreamWriter("parameters.txt"))
                {
                    sW.WriteLine(oldSettings[0] + '|' + oldSettings[1] + '|' + oldSettings[2] + '|' + oldSettings[3] + '|' + Program.ClientNumber);
                }

                foreach(var product in products)
                    manager.CreateProduct(product);

                var bill = new Invoice(txtClientName.Text, txtPhoneNumber.Text, manager.GetAllProducts());
                var frm = new FrmPreview(bill);
                
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPhoneNumber.Text = string.Empty;
            txtClientName.Text = string.Empty;
            products.Clear();
            lvProductList.Items.Clear();
        }


        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvProductList.SelectedItems.Count != 0)
            {
                var result = MessageBox.Show("Do you realy want to delete selected product ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem item in lvProductList.SelectedItems)
                    {
                        lvProductList.Items.Remove(item);
                        products.Remove(item.Tag as Product);
                    }
                }
            }
            LoadProduct();
        }


        private void FrmBill_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var frm = new FrmParameters(true);
            frm.ShowDialog();
        }
    }
}
