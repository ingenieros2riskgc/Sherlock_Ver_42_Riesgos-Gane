using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using ListasSarlaft.Classes.Utilerias;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using Microsoft.Security.Application;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using System.Configuration;
using ClosedXML.Excel;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Web.UI.DataVisualization.Charting;

namespace ListasSarlaft.UserControls.Riesgos.ComparativoRiesgos
{
    public partial class ComparativoRiesgos : System.Web.UI.UserControl
    {
        string IdFormulario = "5030";
        cCuenta cCuenta = new cCuenta();
        cRiesgo cRiesgo = new cRiesgo();
        private cGestion cGestion = new cGestion();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            /*scriptManager.RegisterPostBackControl(this.IBconsultar);
            scriptManager.RegisterPostBackControl(this.IBcancel);*/
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();
                    mtdInicializarValores();
                }
            }
        }
        #region Properties
        private DataTable infoGridRiesgoIndicador;
        private int rowGridRiesgoIndicador;
        private int pagIndexRiesgoIndicador;

        private DataTable InfoGridRiesgoIndicador
        {
            get
            {
                infoGridRiesgoIndicador = (DataTable)ViewState["infoGridRiesgoIndicador"];
                return infoGridRiesgoIndicador;
            }
            set
            {
                infoGridRiesgoIndicador = value;
                ViewState["infoGridRiesgoIndicador"] = infoGridRiesgoIndicador;
            }
        }

        private int RowGridRiesgoIndicador
        {
            get
            {
                rowGridRiesgoIndicador = (int)ViewState["rowGridRiesgoIndicador"];
                return rowGridRiesgoIndicador;
            }
            set
            {
                rowGridRiesgoIndicador = value;
                ViewState["rowGridRiesgoIndicador"] = rowGridRiesgoIndicador;
            }
        }

        private int PagIndexRiesgoIndicador
        {
            get
            {
                pagIndexRiesgoIndicador = (int)ViewState["pagIndexRiesgoIndicador"];
                return pagIndexRiesgoIndicador;
            }
            set
            {
                pagIndexRiesgoIndicador = value;
                ViewState["pagIndexRiesgoIndicador"] = pagIndexRiesgoIndicador;
            }
        }
        #endregion Properties
        private void mtdInicializarValores()
        {
            PagIndexRiesgoIndicador = 0;
            //PagIndex = 0;
            //txtFecha.Text = "" + DateTime.Now;
            //PagIndex3 = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            if (!mtdLoadCuadroComparativo(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 2 ,"Atención");
        }
        public bool mtdLoadCuadroComparativo(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsDTORiesgosComparativoRiesgos> lstComparativo = new List<clsDTORiesgosComparativoRiesgos>();
            clsBLLRiesgosComparativoRiesgos cComparativo = new clsBLLRiesgosComparativoRiesgos();
            lstComparativo = cComparativo.mtdConsultarPefilesRiesgos(booResult, ref strErrMsg);
            if(lstComparativo != null)
            {
                mtdLoadCuadroComparativo();
                mtdLoadCuadroComparativo(lstComparativo);
                GVcomparativo.DataSource = lstComparativo;
                GVcomparativo.PageIndex = pagIndexRiesgoIndicador;
                GVcomparativo.DataBind();
                mtdViewChartComparativo();
                booResult = true;
            }
            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadCuadroComparativo()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intNumeroRegistro", typeof(string));
            grid.Columns.Add("intSumatoriaProbabilidad", typeof(string));
            grid.Columns.Add("intSumatoriaImpacto", typeof(string));
            grid.Columns.Add("intPromedioProbabilidad", typeof(string));
            grid.Columns.Add("intPromedioImpacto", typeof(string));
            grid.Columns.Add("strPerfil", typeof(string));

            GVcomparativo.DataSource = grid;
            GVcomparativo.DataBind();
            InfoGridRiesgoIndicador = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstRiesgoInd">Lista con los perfiles de riesgos</param>
        private void mtdLoadCuadroComparativo(List<clsDTORiesgosComparativoRiesgos> lstComparativo)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTORiesgosComparativoRiesgos objComparativo in lstComparativo)
            {

                InfoGridRiesgoIndicador.Rows.Add(new Object[] {
                    objComparativo.intNumeroRegistro.ToString().Trim(),
                    objComparativo.intSumatoriaProbabilidad.ToString().Trim(),
                    objComparativo.intSumatoriaImpacto.ToString().Trim(),
                    objComparativo.intPromedioProbabilidad.ToString().Trim(),
                    objComparativo.intPromedioImpacto.ToString().Trim(),
                    objComparativo.strPerfil.ToString().Trim()
                    });
            }
        }
        public void mtdViewChartComparativo()
        {
            System.Data.DataTable dtInfo = new System.Data.DataTable();
            clsDALRiesgoComparativoRiesgos dalComparativo = new clsDALRiesgoComparativoRiesgos();
            string strErrMsg = string.Empty;
            dalComparativo.mtdConsultarPefilesRiesgos(ref dtInfo, ref strErrMsg);
            string[] x = new string[dtInfo.Rows.Count];
            int[] y = new int[dtInfo.Rows.Count];
            int Total = 0;
            /*for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                if (i == 0)*/
                    x[0] = "Frecuencia Riesgo Inherente";
                //if(i == 1)
                    x[1] = "Impacto Riesgo Inherente";
                y[0] = Convert.ToInt32(dtInfo.Rows[0][1]);
                y[1] = Convert.ToInt32(dtInfo.Rows[0][2]);
                Total = Convert.ToInt32(dtInfo.Rows[0][2]) + Convert.ToInt32(dtInfo.Rows[0][1]);
            //}
            ChartSaro.Series[0].Points.DataBindXY(x, y);
            ChartSaro.Series[0].ChartType = SeriesChartType.Pie;
            ChartSaro.ChartAreas["ChartSaroArea"].Area3DStyle.Enable3D = true;
            ChartSaro.Legends[0].Enabled = true;
            //ChartSaro.Titles.Add("Grafico Comparativo de Perfiles de Riesgo");
            //ChartSaro.Titles[1].Text = "Saro Valor Total: " + Total;
            foreach (System.Web.UI.DataVisualization.Charting.Series charts in ChartSaro.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    /*switch (point.AxisLabel)
                    {
                        case "SumatoriaProbabilidad": point.Color = System.Drawing.Color.Green; break;
                        case "Moderado": point.Color = System.Drawing.Color.Yellow; break;
                        case "Alto": point.Color = System.Drawing.Color.Orange; break;
                        case "SumatoriaImpacto": point.Color = System.Drawing.Color.Red; break;
                    }*/
                    point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

                }
            }
            x = new string[dtInfo.Rows.Count];
            y = new int[dtInfo.Rows.Count];
            Total = 0;
            x[0] = "Frecuencia Riesgo Residual";
            //if(i == 1)
            x[1] = "Impacto Riesgo Residual";
            y[0] = Convert.ToInt32(dtInfo.Rows[1][1]);
            y[1] = Convert.ToInt32(dtInfo.Rows[1][2]);
            Total = Convert.ToInt32(dtInfo.Rows[1][2]) + Convert.ToInt32(dtInfo.Rows[1][1]);
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].ChartType = SeriesChartType.Pie;
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.Legends[0].Enabled = true;
            //ChartSaro.Titles.Add("Grafico Comparativo de Perfiles de Riesgo");
            foreach (System.Web.UI.DataVisualization.Charting.Series charts in Chart1.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);
                    switch (point.AxisLabel)
                    {
                        case "Frecuencia Riesgo Residual": point.Color = System.Drawing.Color.Green; break;
                        case "Impacto Riesgo Residual": point.Color = System.Drawing.Color.Red; break;
                    }
                }
            }
        }
    }
}