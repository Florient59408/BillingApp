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
    public partial class FrmParameters : Form
    {
        bool setting;
        public FrmParameters()
        {
            InitializeComponent();
            setting = false;
        }
        public FrmParameters(bool setting) : this()
        {
            if(setting)
            {
                this.setting = setting;
                txtEmail.Text = Program.CompanyEmail;
                txtLocation.Text = Program.CompanyLocation;
                txtName.Text = Program.CompanyName;
                txtPhone.Text = Program.CompanyPhone;
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtEmail.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPhone.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            try
            {
                Program.CompanyEmail = txtEmail.Text;
                Program.CompanyLocation = txtLocation.Text;
                Program.CompanyName = txtName.Text;

                if (txtName.Text == string.Empty)
                    throw new Exception("Invalid name !");

                if (!double.TryParse(txtPhone.Text, out _))
                    throw new Exception("Invalid phone number !");

                Program.CompanyPhone = txtPhone.Text;

                var file = new FileInfo("parameters.txt");

                if (file.Exists && File.ReadAllText(file.FullName).Split('|')[4]!="0")
                {
                    string oldNumber = File.ReadAllText(file.FullName).Split('|')[4];
                    using (var sW = new StreamWriter("parameters.txt"))
                    {
                        sW.WriteLine(txtName.Text + '|' + txtLocation.Text + '|' + txtPhone.Text + '|' + txtEmail.Text+'|'+ oldNumber);
                    }
                }
                else
                {
                    using (var sW = new StreamWriter("parameters.txt"))
                    {
                        sW.WriteLine(txtName.Text + '|' + txtLocation.Text + '|' + txtPhone.Text + '|' + txtEmail.Text+ '|' + '0');
                    }
                }

                MessageBox.Show("Informations saved", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FrmParameters_FormClosing(object sender, FormClosingEventArgs e)
        {
            var file = new FileInfo("parameters.txt");

            if (file.Exists)
            {
                if(!setting)
                {
                    var frm = new FrmBill();
                    frm.Show();
                }
                else
                    setting = false;
            }
            else
                Application.Exit();
        }
    }
}
