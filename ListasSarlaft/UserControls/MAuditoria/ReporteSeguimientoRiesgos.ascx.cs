using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Data;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class ReporteSeguimientoRiesgos : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                string Ca = (Request.QueryString["Ca"]);
                txtCodAuditoria.Text = Ca;
                clsHallazgosAuditoriaDAL dbHallazgos = new clsHallazgosAuditoriaDAL();
                //string Ca = (Request.QueryString["Ca"]);
                /*string IdHallazgo = string.Empty;
                IdHallazgo = (e.Parameters["IdHallazgo"].Values.First()).ToString();*/
                /*DataTable dtInfo = new DataTable();
                string err = string.Empty;
                dbHallazgos.mtdGetImagenesAuditoria(ref dtInfo, ref err, ref Ca);
                ReportDataSource datasourceHallazgosImagenes = new ReportDataSource("DSImagenes", (DataTable)dtInfo);
                ReportViewer1.LocalReport.DataSources.Add(datasourceHallazgosImagenes);*/
                try
                {
                    ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
                    this.ReportViewer1.LocalReport.DisplayName = "PreinformeAuditoria";
                    this.ReportViewer1.LocalReport.EnableExternalImages = true;
                    this.ReportViewer1.LocalReport.Refresh();
                }
                catch (Exception ex)
                {
                    string men = ex.Message;
                }

            }
        }
        public void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
        {

            /*if (e.Parameters.Count() > 0)
            {*/
            e.DataSources.Add(new ReportDataSource("DSRecomendacionesHallazgo", "odsRecomendaciones"));
            e.DataSources.Add(new ReportDataSource("DSRiesgosHallazgo", "odsRiesgos"));
            //e.DataSources.Add(new ReportDataSource("DSimagenes", "odsImagenes"));
            clsHallazgosAuditoriaDAL dbHallazgos = new clsHallazgosAuditoriaDAL();
            try
            {
                if (e.DataSourceNames[0].ToString() == "dsImagenes")
                {
                    string Ca = (Request.QueryString["Ca"]);
                    string IdHallazgo = string.Empty;
                    IdHallazgo = (e.Parameters["IdHallazgo"].Values.FirstOrDefault()).ToString();
                    DataTable dtInfo = new DataTable();
                    string err = string.Empty;
                    e.DataSources.Clear();
                    dbHallazgos.mtdGetImagenesAuditoria(ref dtInfo, ref err, ref Ca, IdHallazgo);
                    ReportDataSource datasourceHallazgosImagenes = new ReportDataSource("dsImagenes", (DataTable)dtInfo);
                    e.DataSources.Add(datasourceHallazgosImagenes);
                }

            }
            catch (Exception ex)
            {
                string men = ex.Message;
            }

            //}

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ReportViewer1.LocalReport.Refresh();
        }
    }
}