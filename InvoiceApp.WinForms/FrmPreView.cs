using InvoiceApp.BO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceApp.WinForms
{
    public partial class FrmPreview : Form
    {

        string path;
        Invoice facture;
        public static EventHandler Printed;
        private FrmPreview()
        {
            InitializeComponent();
        }

        public FrmPreview(Invoice facture):this()
        {
            path = "RptInvoice.rdlc";
            this.facture = facture;
        }

        private void FrmView_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.ReportPath = path;

            this.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("DataSet1", new List<Invoice> { facture })
                ) ;

            this.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("DataSet2", facture.Products)
                );

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.Padding = new Padding(0, 0, 0, 0);
            this.reportViewer1.RefreshReport();
        }

        private void FrmPreview_FormClosed(object sender, FormClosedEventArgs e)
        {
            Printed?.Invoke(sender, e);
        }
    }
}
