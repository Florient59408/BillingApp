using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceApp.WinForms
{
    static class Program
    {
        public static string CompanyName { get; set; }
        public static string CompanyPhone { get; set; }
        public static string CompanyEmail { get; set; }
        public static string CompanyLocation { get; set; }
        public static int ClientNumber { get; set; }

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CheckParameters();
            Application.Run();
        }

        public static void CheckParameters()
        {
            var file = new FileInfo("parameters.txt");

            if (!file.Exists || file.Length == 0) 
            {
                ClientNumber = 0;
                var frm = new FrmParameters();
                frm.Show();
                return;
            }
            else
            {
                string[] settings;
                using (var streamReader = new StreamReader(file.OpenRead()))
                {
                    settings = streamReader.ReadLine().Split('|');
                }
                CompanyName = settings[0];
                CompanyLocation = settings[1];
                CompanyPhone = settings[2];
                CompanyEmail = settings[3];
                ClientNumber = 0;

                var frm = new FrmBill();
                frm.Show();
            }
        }
    }
}
