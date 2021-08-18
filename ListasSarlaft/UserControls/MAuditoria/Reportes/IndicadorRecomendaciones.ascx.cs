using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Security.Application;
using ListasSarlaft.Classes;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Configuration;
using ListasSarlaft.Classes.Utilerias;

namespace ListasSarlaft.UserControls.MAuditoria.Reportes
{
    public partial class IndicadorRecomendaciones : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;
        private DataTable infoGridDtpo;
        private int rowGridDtpo;
        private int pagIndexDtpo;

        private DataTable InfoGrid
        {
            get
            {
                infoGrid = (DataTable)ViewState["infoGrid"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infoGrid"] = infoGrid;
            }
        }

        private int RowGrid
        {
            get
            {
                rowGrid = (int)ViewState["rowGrid"];
                return rowGrid;
            }
            set
            {
                rowGrid = value;
                ViewState["rowGrid"] = rowGrid;
            }
        }

        private int PagIndex
        {
            get
            {
                pagIndex = (int)ViewState["pagIndex"];
                return pagIndex;
            }
            set
            {
                pagIndex = value;
                ViewState["pagIndex"] = pagIndex;
            }
        }
        private DataTable InfoGridDtpo
        {
            get
            {
                infoGridDtpo = (DataTable)ViewState["infoGridDtpo"];
                return infoGridDtpo;
            }
            set
            {
                infoGridDtpo = value;
                ViewState["infoGridDtpo"] = infoGridDtpo;
            }
        }

        private int RowGridDtpo
        {
            get
            {
                rowGridDtpo = (int)ViewState["rowGridDtpo"];
                return rowGridDtpo;
            }
            set
            {
                rowGridDtpo = value;
                ViewState["rowGridDtpo"] = rowGridDtpo;
            }
        }

        private int PagIndexDtpo
        {
            get
            {
                pagIndexDtpo = (int)ViewState["pagIndexDtpo"];
                return pagIndexDtpo;
            }
            set
            {
                pagIndexDtpo = value;
                ViewState["pagIndexDtpo"] = pagIndexDtpo;
            }
        }
        #endregion
        protected void IBsearch_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlTipoReporte.SelectedValue == "1")
                mtdReportGeneral();
            if (ddlTipoReporte.SelectedValue == "2")
                mtdReporteDepartamento();
        }
        public void mtdReportGeneral()
        {
            string strErrMsg = string.Empty;
            bool booResult = false;
            string año = string.Empty;
            clsIndicadorRecomendacionesBLL cRegistros = new clsIndicadorRecomendacionesBLL();
            List<clsIndicadorRecomendacionesDTO> lstIndicador = new List<clsIndicadorRecomendacionesDTO>();
            string fechaInicial = string.Empty;
            string fechaFinal = string.Empty;
            string planeacion = string.Empty;
            try
            {
                año = Sanitizer.GetSafeHtmlFragment(TXfechaInicial.Text);
                
                 fechaInicial = Sanitizer.GetSafeHtmlFragment(txbFechaInicialGen.Text);
                 fechaFinal = Sanitizer.GetSafeHtmlFragment(txbFechaFinalGen.Text);
                 planeacion = ddlPlaneacion.SelectedValue;
                
            }catch(Exception ex)
            {
                omb.ShowMessage("Error: "+ex, 2, "Atención");
            }
            try
            {
                lstIndicador = cRegistros.mtdConsultarIndicadorRecomendaciones(ref strErrMsg, ref lstIndicador, año, fechaInicial, fechaFinal, planeacion);
                if (lstIndicador != null)
                {
                    mtdLoadGridIndicadorRecomendaciones();
                    mtdLoadGridIndicadorRecomendaciones(lstIndicador);
                    GVindicadorRecomendaciones.DataSource = lstIndicador;
                    GVindicadorRecomendaciones.PageIndex = pagIndex;
                    GVindicadorRecomendaciones.DataBind();
                    booResult = true;
                    BodyGridDtpo.Visible = false;
                    BodyGridIR.Visible = true;
                    Dbutton.Visible = true;
                }
                else
                {
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                }
            }catch(Exception ex)
            {
                omb.ShowMessage("Error: " + ex, 2, "Atención");
            }
        }
        public void mtdReporteDepartamento()
        {
            string strErrMsg = string.Empty;
            bool booResult = false;
            string año = Sanitizer.GetSafeHtmlFragment(TXfechaInicial.Text);
            clsIndicadorRecomendacionesDtpoBLL cRegistros = new clsIndicadorRecomendacionesDtpoBLL();
            List<clsIndicadorRecomendacionesDTO> lstIndicador = new List<clsIndicadorRecomendacionesDTO>();
            string fechaInicial = Sanitizer.GetSafeHtmlFragment(txbFechaInicialGen.Text);
            string fechaFinal = Sanitizer.GetSafeHtmlFragment(txbFechaFinalGen.Text);
            string planeacion = ddlPlaneacion.SelectedValue;
            string IdArea = ddlDepartamentos.SelectedValue;
            lstIndicador = cRegistros.mtdConsultarIndicadorRecomendacionesDtpo(ref strErrMsg, ref lstIndicador, año, fechaInicial, fechaFinal, planeacion, IdArea);
            if (lstIndicador != null)
            {
                mtdLoadGridIndicadorRecomendacionesDtpo();
                mtdLoadGridIndicadorRecomendacionesDtpo(lstIndicador);
                GVrecomendacionesDtpo.DataSource = lstIndicador;
                GVrecomendacionesDtpo.PageIndex = pagIndexDtpo;
                GVrecomendacionesDtpo.DataBind();
                booResult = true;
                BodyGridDtpo.Visible = true;
                BodyGridIR.Visible = false;
            }
            else
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridIndicadorRecomendaciones()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdIndicador", typeof(string));
            grid.Columns.Add("strIndicador", typeof(string));
            grid.Columns.Add("strMetodologia", typeof(string));
            grid.Columns.Add("strFrecuencia", typeof(string));
            grid.Columns.Add("strResponsable", typeof(string));
            grid.Columns.Add("intMeta", typeof(string));
            grid.Columns.Add("intAcumuladoDtpo", typeof(string));
            grid.Columns.Add("intCumplimiento", typeof(string));
            grid.Columns.Add("intIdFrecuencia", typeof(string));

            GVindicadorRecomendaciones.DataSource = grid;
            GVindicadorRecomendaciones.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstIndicadores">Lista con los Indicadores</param>
        private void mtdLoadGridIndicadorRecomendaciones(List<clsIndicadorRecomendacionesDTO> lstIndicadores)
        {
            string strErrMsg = String.Empty;

            foreach (clsIndicadorRecomendacionesDTO objEvaComp in lstIndicadores)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objEvaComp.intIdIndicador.ToString().Trim(),
                    objEvaComp.strIndicador.ToString().Trim(),
                    objEvaComp.strMetodologia.ToString().Trim(),
                    objEvaComp.strFrecuencia.ToString().Trim(),
                    objEvaComp.strResponsable.ToString().Trim(),
                    objEvaComp.intMeta.ToString().Trim(),
                    objEvaComp.intAcumuladoDtpo.ToString().Trim(),
                    objEvaComp.intCumplimiento.ToString().Trim(),
                    objEvaComp.intIdFrecuencia.ToString().Trim()
                    });
            }
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridIndicadorRecomendacionesDtpo()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdIndicador", typeof(string));
            grid.Columns.Add("strIndicador", typeof(string));
            grid.Columns.Add("strMetodologia", typeof(string));
            grid.Columns.Add("strFrecuencia", typeof(string));
            grid.Columns.Add("strResponsable", typeof(string));
            grid.Columns.Add("intMeta", typeof(string));
            grid.Columns.Add("intAcumulado", typeof(string));
            grid.Columns.Add("intCumplimiento", typeof(string));
            grid.Columns.Add("intIdFrecuencia", typeof(string));

            GVrecomendacionesDtpo.DataSource = grid;
            GVrecomendacionesDtpo.DataBind();
            InfoGridDtpo = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstIndicadores">Lista con los Indicadores</param>
        private void mtdLoadGridIndicadorRecomendacionesDtpo(List<clsIndicadorRecomendacionesDTO> lstIndicadores)
        {
            string strErrMsg = String.Empty;

            foreach (clsIndicadorRecomendacionesDTO objEvaComp in lstIndicadores)
            {

                InfoGridDtpo.Rows.Add(new Object[] {
                    objEvaComp.intIdIndicador.ToString().Trim(),
                    objEvaComp.strIndicador.ToString().Trim(),
                    objEvaComp.strMetodologia.ToString().Trim(),
                    objEvaComp.strFrecuencia.ToString().Trim(),
                    objEvaComp.strResponsable.ToString().Trim(),
                    objEvaComp.intMeta.ToString().Trim(),
                    objEvaComp.intAcumulado.ToString().Trim(),
                    objEvaComp.intCumplimiento.ToString().Trim(),
                    objEvaComp.intIdFrecuencia.ToString().Trim()
                    });
            }
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strErrMsg = string.Empty;
                string año = Sanitizer.GetSafeHtmlFragment(TXfechaInicial.Text);
                clsIndicadorRecomendacionesBLL cRegistros = new clsIndicadorRecomendacionesBLL();
                int frecuencia = Convert.ToInt32(GVindicadorRecomendaciones.DataKeys[e.Row.RowIndex].Values[0].ToString());
                int IdIndicador = Convert.ToInt32(GVindicadorRecomendaciones.DataKeys[e.Row.RowIndex].Values[1].ToString());
                DataTable ValuesRecomendaciones = cRegistros.mtdGetValueRecomendacion(ref strErrMsg, año, frecuencia, IdIndicador);
                GridView gvRecomendaciones = e.Row.FindControl("gvRecomendaciones") as GridView;
                gvRecomendaciones.DataSource = ValuesRecomendaciones;
                gvRecomendaciones.DataBind();
                mtdGenerarGrafico(ValuesRecomendaciones, frecuencia);
                dvGrafico.Visible = true;
            }
        }
        protected void OnRowDataBoundDtpo(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strErrMsg = string.Empty;
                string año = Sanitizer.GetSafeHtmlFragment(TXfechaInicial.Text);
                clsIndicadorRecomendacionesDtpoBLL cRegistros = new clsIndicadorRecomendacionesDtpoBLL();
                int frecuencia = Convert.ToInt32(GVrecomendacionesDtpo.DataKeys[e.Row.RowIndex].Values[0].ToString());
                int IdIndicador = Convert.ToInt32(GVrecomendacionesDtpo.DataKeys[e.Row.RowIndex].Values[1].ToString());
                DataTable ValuesRecomendaciones = cRegistros.mtdGetValueRecomendacionDtpo(ref strErrMsg, año, frecuencia, IdIndicador);
                GridView gvRecomendacionesDtpo = e.Row.FindControl("gvRecomendacionesDtpo") as GridView;
                gvRecomendacionesDtpo.DataSource = ValuesRecomendaciones;
                gvRecomendacionesDtpo.DataBind();
                formDtpo.Visible = true;
                mtdLoadDepartamentos();
                /*mtdGenerarGrafico(ValuesRecomendaciones, frecuencia);
                dvGrafico.Visible = true;*/
            }
        }
        public void mtdLoadDepartamentos()
        {
            string strErrMsg = string.Empty;
            string año = Sanitizer.GetSafeHtmlFragment(TXfechaInicial.Text);
            DataTable dtInfo = new DataTable();
            clsIndicadorRecomendacionesDtpoDAL dalRegistros = new clsIndicadorRecomendacionesDtpoDAL();
            bool booResult = false;
            booResult = dalRegistros.mtdConsultarAreasResponsables(ref dtInfo, ref strErrMsg, año);
            if(booResult == true)
            {
                ddlDepartamentos.Items.Clear();
                ddlDepartamentos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Seleccione--", "0"));
                int intCounter = 1;
                foreach (DataRow row in dtInfo.Rows)
                {
                    ddlDepartamentos.Items.Insert(intCounter, new System.Web.UI.WebControls.ListItem(row["Area"].ToString(), row["IdArea"].ToString()));
                    intCounter++;
                }
            }
        }
        public void mtdGenerarGrafico(DataTable dtInfo, int Frecuencia)
        {
            string[] mensual = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            string[] mensualMes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            string[] bimestral = new string[] { "1,2", "3,4", "5,6", "7,8", "9,10", "11,12" };
            string[] bimestralMes = new string[] { "Enero,Febrero", "Marzo,Abril", "Mayo,Junio", "Julio,Agosto", "Septiembre,Octubre", "Noviembre,Diciembre" };
            string[] trimestral = new string[] { "1,2,3", "4,5,6", "7,8,9", "10,11,12" };
            string[] trimestralMes = new string[] { "Enero,Febrero,Marzo", "Abril,Mayo,Junio", "Julio,Agosto,Septiembre", "Octubre,Noviembre,Diciembre" };
            string[] cuatrimestral = new string[] { "1,2,3,4", "5,6,7,8", "9,10,11,12" };
            string[] cuatrimestralMes = new string[] { "Enero,Febrero,Marzo,Abril", "Mayo,Junio,Julio,Agosto", "Septiembre, Octubre,Noviembre,Diciembre" };
            string[] semestral = new string[] { "1,2,3,4,5,6", "7,8,9,10,11,12" };
            string[] semestralMes = new string[] { "Enero,Febrero,Marzo,Abril,Mayo,Junio", "Julio,Agosto,Septiembre, Octubre,Noviembre,Diciembre" };
            string[] anual = new string[] { "1,2,3,4,5,6,7,8,9,10,11,12" };
            string[] anualMes = new string[] { "Enero,Febrero,Marzo,Abril,Mayo,Junio, Julio,Agosto,Septiembre, Octubre,Noviembre,Diciembre" };
            try
            {
                if (Frecuencia == 1)
                {
                    for (int i = 0; i < mensualMes.Length; i++)
                    {
                        Chart1.Series["Series1"].Points.AddXY(mensualMes[i].ToString(), dtInfo.Rows[0][i].ToString());
                        Chart1.Series["Series1"].Points[i].Label = dtInfo.Rows[0][i].ToString();
                        Chart1.Series["Series1"].Points[i].ToolTip = dtInfo.Rows[0][i].ToString();

                        Chart1.Series["Series2"].Points.AddXY(mensualMes[i].ToString(), dtInfo.Rows[1][i].ToString());
                        Chart1.Series["Series2"].Points[i].Label = dtInfo.Rows[1][i].ToString();
                        Chart1.Series["Series2"].Points[i].ToolTip = dtInfo.Rows[1][i].ToString();
                    }
                }
                if (Frecuencia == 2)
                {
                    for (int i = 0; i < bimestralMes.Length; i++)
                    {
                        Chart1.Series["Series1"].Points.AddXY(bimestralMes[i].ToString(), dtInfo.Rows[0][i].ToString());
                        Chart1.Series["Series1"].Points[i].Label = dtInfo.Rows[0][i].ToString();
                        Chart1.Series["Series1"].Points[i].ToolTip = dtInfo.Rows[0][i].ToString();

                        Chart1.Series["Series2"].Points.AddXY(bimestralMes[i].ToString(), dtInfo.Rows[1][i].ToString());
                        Chart1.Series["Series2"].Points[i].Label = dtInfo.Rows[1][i].ToString();
                        Chart1.Series["Series2"].Points[i].ToolTip = dtInfo.Rows[1][i].ToString();
                    }
                }
                if (Frecuencia == 3)
                {
                    for (int i = 0; i < trimestralMes.Length; i++)
                    {
                        Chart1.Series["Series1"].Points.AddXY(trimestralMes[i].ToString(), dtInfo.Rows[0][i].ToString());
                        Chart1.Series["Series1"].Points[i].Label = dtInfo.Rows[0][i].ToString();
                        Chart1.Series["Series1"].Points[i].ToolTip = dtInfo.Rows[0][i].ToString();

                        Chart1.Series["Series2"].Points.AddXY(trimestralMes[i].ToString(), dtInfo.Rows[1][i].ToString());
                        Chart1.Series["Series2"].Points[i].Label = dtInfo.Rows[1][i].ToString();
                        Chart1.Series["Series2"].Points[i].ToolTip = dtInfo.Rows[1][i].ToString();
                    }

                }
                if (Frecuencia == 4)
                {
                    for (int i = 0; i < cuatrimestralMes.Length; i++)
                    {
                        Chart1.Series["Series1"].Points.AddXY(cuatrimestralMes[i].ToString(), dtInfo.Rows[0][i].ToString());
                        Chart1.Series["Series1"].Points[i].Label = dtInfo.Rows[0][i].ToString();
                        Chart1.Series["Series1"].Points[i].ToolTip = dtInfo.Rows[0][i].ToString();

                        Chart1.Series["Series2"].Points.AddXY(cuatrimestralMes[i].ToString(), dtInfo.Rows[1][i].ToString());
                        Chart1.Series["Series2"].Points[i].Label = dtInfo.Rows[1][i].ToString();
                        Chart1.Series["Series2"].Points[i].ToolTip = dtInfo.Rows[1][i].ToString();
                    }
                }
                if (Frecuencia == 5)
                {
                    for (int i = 0; i < semestralMes.Length; i++)
                    {
                        Chart1.Series["Series1"].Points.AddXY(semestralMes[i].ToString(), dtInfo.Rows[0][i].ToString());
                        Chart1.Series["Series1"].Points[i].Label = dtInfo.Rows[0][i].ToString();
                        Chart1.Series["Series1"].Points[i].ToolTip = dtInfo.Rows[0][i].ToString();

                        Chart1.Series["Series2"].Points.AddXY(semestralMes[i].ToString(), dtInfo.Rows[1][i].ToString());
                        Chart1.Series["Series2"].Points[i].Label = dtInfo.Rows[1][i].ToString();
                        Chart1.Series["Series2"].Points[i].ToolTip = dtInfo.Rows[1][i].ToString();
                    }
                }
                if (Frecuencia == 6)
                {
                    for (int i = 0; i < anualMes.Length; i++)
                    {
                        Chart1.Series["Series1"].Points.AddXY(anualMes[i].ToString(), dtInfo.Rows[0][i].ToString());
                        Chart1.Series["Series1"].Points[i].Label = dtInfo.Rows[0][i].ToString();
                        Chart1.Series["Series1"].Points[i].ToolTip = dtInfo.Rows[0][i].ToString();

                        Chart1.Series["Series2"].Points.AddXY(anualMes[i].ToString(), dtInfo.Rows[1][i].ToString());
                        Chart1.Series["Series2"].Points[i].Label = dtInfo.Rows[1][i].ToString();
                        Chart1.Series["Series2"].Points[i].ToolTip = dtInfo.Rows[1][i].ToString();
                    }
                }
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en la generación del gráfico", 2, "Atención");
            }
            
            // Set series chart type
            Chart1.Series["Series1"].ChartType = SeriesChartType.Line;
            Chart1.Series["Series2"].ChartType = SeriesChartType.Spline;

            // Set point labels
            Chart1.Series["Series1"].IsValueShownAsLabel = true;
            Chart1.Series["Series2"].IsValueShownAsLabel = true;

            // Enable X axis margin
            Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
            Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Periodicidad";
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Valor Recomendaciones";

            // Enable 3D, and show data point marker lines
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.Series["Series1"]["ShowMarkerLines"] = "True";
            Chart1.Series["Series2"]["ShowMarkerLines"] = "True";
        }

        protected void ImbSearchDtpo_Click(object sender, ImageClickEventArgs e)
        {
            mtdReporteDepartamentoDetalleado();
        }
        public void mtdReporteDepartamentoDetalleado()
        {
            string strErrMsg = string.Empty;
            bool booResult = false;
            string año = Sanitizer.GetSafeHtmlFragment(TXfechaInicial.Text);
            clsIndicadorRecomendacionesDtpoBLL cRegistros = new clsIndicadorRecomendacionesDtpoBLL();
            List<clsIndicadorRecomendacionesDTO> lstIndicador = new List<clsIndicadorRecomendacionesDTO>();
            string fechaInicial = Sanitizer.GetSafeHtmlFragment(txbFechaInicialGen.Text);
            string fechaFinal = Sanitizer.GetSafeHtmlFragment(txbFechaFinalGen.Text);
            string planeacion = ddlPlaneacion.SelectedValue;
            string idArea = ddlDepartamentos.SelectedValue;
            lstIndicador = cRegistros.mtdConsultarIndicadorRecomendacionesDtpo(ref strErrMsg, ref lstIndicador, año, fechaInicial, fechaFinal, planeacion, idArea);
            if (lstIndicador != null)
            {
                mtdLoadGridIndicadorRecomendacionesDtpoDetallado();
                mtdLoadGridIndicadorRecomendacionesDtpoDetallado(lstIndicador);
                GVdeptoDetalle.DataSource = lstIndicador;
                GVdeptoDetalle.PageIndex = pagIndexDtpo;
                GVdeptoDetalle.DataBind();
                booResult = true;
                BodyGridDtpo.Visible = false;
                BodyGridIR.Visible = false;
                BodyGridDtpoDetalle.Visible = true;
            }
            else
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridIndicadorRecomendacionesDtpoDetallado()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdIndicador", typeof(string));
            grid.Columns.Add("strIndicador", typeof(string));
            grid.Columns.Add("strMetodologia", typeof(string));
            grid.Columns.Add("strFrecuencia", typeof(string));
            grid.Columns.Add("strResponsable", typeof(string));
            grid.Columns.Add("intMeta", typeof(string));
            grid.Columns.Add("intAcumuladoDtpo", typeof(string));
            grid.Columns.Add("intCumplimiento", typeof(string));
            grid.Columns.Add("intIdFrecuencia", typeof(string));

            GVindicadorRecomendaciones.DataSource = grid;
            GVindicadorRecomendaciones.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstIndicadores">Lista con los Indicadores</param>
        private void mtdLoadGridIndicadorRecomendacionesDtpoDetallado(List<clsIndicadorRecomendacionesDTO> lstIndicadores)
        {
            string strErrMsg = String.Empty;

            foreach (clsIndicadorRecomendacionesDTO objEvaComp in lstIndicadores)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objEvaComp.intIdIndicador.ToString().Trim(),
                    objEvaComp.strIndicador.ToString().Trim(),
                    objEvaComp.strMetodologia.ToString().Trim(),
                    objEvaComp.strFrecuencia.ToString().Trim(),
                    objEvaComp.strResponsable.ToString().Trim(),
                    objEvaComp.intMeta.ToString().Trim(),
                    objEvaComp.intAcumuladoDtpo.ToString().Trim(),
                    objEvaComp.intCumplimiento.ToString().Trim(),
                    objEvaComp.intIdFrecuencia.ToString().Trim()
                    });
            }
        }
        protected void OnRowDataBoundDtpoDetallado(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strErrMsg = string.Empty;
                string año = Sanitizer.GetSafeHtmlFragment(TXfechaInicial.Text);
                clsIndicadorRecomendacionesDtpoBLL cRegistros = new clsIndicadorRecomendacionesDtpoBLL();
                int frecuencia = Convert.ToInt32(GVrecomendacionesDtpo.DataKeys[e.Row.RowIndex].Values[0].ToString());
                int IdIndicador = Convert.ToInt32(GVrecomendacionesDtpo.DataKeys[e.Row.RowIndex].Values[1].ToString());
                DataTable ValuesRecomendaciones = cRegistros.mtdGetValueRecomendacionDtpoDetallado(ref strErrMsg, año, frecuencia, IdIndicador);
                GridView gvRecomendacionesDtpo = e.Row.FindControl("gvRecomendacionesDtpo") as GridView;
                gvRecomendacionesDtpo.DataSource = ValuesRecomendaciones;
                gvRecomendacionesDtpo.DataBind();
                mtdGenerarGrafico(ValuesRecomendaciones, frecuencia);
                dvGrafico.Visible = true;
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdClean();
            BodyGridIR.Visible = false;
            dvGrafico.Visible = false;
            BodyGridDtpoDetalle.Visible = false;
            BodyGridDtpo.Visible = false;
            Dbutton.Visible = false;
        }
        public void mtdClean()
        {
            ddlTipoReporte.SelectedIndex = 0;
            TXfechaInicial.Text = string.Empty;
            txbFechaInicialGen.Text = string.Empty;
            txbFechaFinalGen.Text = string.Empty;
            ddlPlaneacion.SelectedIndex = 0;
        }

        protected void ImbCancelDtpo_Click(object sender, ImageClickEventArgs e)
        {
            BodyGridIR.Visible = false;
            dvGrafico.Visible = false;
            BodyGridDtpoDetalle.Visible = false;
            BodyGridDtpo.Visible = false;
            Dbutton.Visible = false;
            ddlDepartamentos.SelectedIndex = 0;
            txbFechaInicialDep.Text = string.Empty;
            txbFechaFinalDep.Text = string.Empty;
            ddPlaneacionDep.SelectedIndex = 0;
        }

        protected void ddlTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlTipoReporte.SelectedValue == "1")
            {
                formDtpo.Visible = false;
            }
        }

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            // Creamos el tipo de Font que vamos utilizar
            Tools tools = new Tools();
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, Color.BLACK);

            Document pdfDocument = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Indicador de Recomendaciones");
            //....header
            // we Add a Header that will show up on PAGE 1
            // Creamos la imagen y le ajustamos el tamaño
            string pathImg = Server.MapPath("~") + "Imagenes/Logos/Risk.png";
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(pathImg);
            pathImg = Server.MapPath("~") + ConfigurationManager.AppSettings.Get("EmpresaLogo").ToString();
            iTextSharp.text.Image imagenEmpresa = iTextSharp.text.Image.GetInstance(pathImg);
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_RIGHT;
            PdfPTable pdftblImage = new PdfPTable(2);
            PdfPCell pdfcellImage = new PdfPCell(imagen, true);
            pdfcellImage.FixedHeight = 40f;
            /*pdfcellImage.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfcellImage.VerticalAlignment = Element.ALIGN_LEFT;*/
            pdfcellImage.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pdfcellImage.Border = iTextSharp.text.Rectangle.NO_BORDER;
            float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImage);
            PdfPCell pdfcellImageEmpresa = new PdfPCell(imagenEmpresa, true);
            pdfcellImageEmpresa.FixedHeight = 40f;
            pdfcellImageEmpresa.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.VerticalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pdfcellImageEmpresa.Border = iTextSharp.text.Rectangle.NO_BORDER;
            percentage = 40 / imagenEmpresa.Width;
            imagenEmpresa.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImageEmpresa);
            //Chunk chnCompany = new Chunk("Risk Consulting", _standardFont);
            Phrase phHeader = new Phrase();
            phHeader.Add(pdftblImage);
            
            iTextSharp.text.Font font1 = new iTextSharp.text.Font();
            font1.Color = Color.WHITE;
            PdfPTable pdfTableData = new PdfPTable(2);
            PdfPCell pdfCellEncabezado = new PdfPCell(new Phrase("Tipo Reporte:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(ddlTipoReporte.SelectedItem.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Año de Indicador:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(TXfechaInicial.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Inicial:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txbFechaInicialGen.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Fecha Final:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txbFechaFinalGen.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Proceso:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(ddlPlaneacion.SelectedValue));
            pdfTableData.AddCell(pdfCellEncabezado);
            PdfPTable pdfpTable = new PdfPTable(2);

            pdfpTable = new PdfPTable(GVindicadorRecomendaciones.HeaderRow.Cells.Count);
            //int iteracion = 0;
            foreach (TableCell headerCell in GVindicadorRecomendaciones.HeaderRow.Cells)
            {
                /*if(iteracion <= 7)
                {*/
                    iTextSharp.text.Font font = new iTextSharp.text.Font();
                    font.Color = new Color(GVindicadorRecomendaciones.HeaderStyle.ForeColor);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(HttpUtility.HtmlDecode( headerCell.Text), font));
                    pdfCell.BackgroundColor = new Color(GVindicadorRecomendaciones.HeaderStyle.BackColor);
                    pdfpTable.AddCell(pdfCell);
                /*}
                iteracion++;*/
            }

            foreach (GridViewRow GridViewRow in GVindicadorRecomendaciones.Rows)
            {
                string intIdIndicador = ((Label)GridViewRow.FindControl("intIdIndicador")).Text;
                string strIndicador = ((Label)GridViewRow.FindControl("strIndicador")).Text;
                string strMetodologia = ((Label)GridViewRow.FindControl("strMetodologia")).Text;
                string strFrecuencia = ((Label)GridViewRow.FindControl("strFrecuencia")).Text;
                string strResponsable = ((Label)GridViewRow.FindControl("strResponsable")).Text;
                string intMeta = ((Label)GridViewRow.FindControl("intMeta")).Text;
                string intAcumulado = ((Label)GridViewRow.FindControl("intAcumulado")).Text;
                string intCumplimiento = ((Label)GridViewRow.FindControl("intCumplimiento")).Text;
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVindicadorRecomendaciones.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intIdIndicador));
                        pdfCell.BackgroundColor = new Color(GVindicadorRecomendaciones.RowStyle.BackColor);
                        pdfpTable.AddCell(pdfCell);
                    }
                    if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVindicadorRecomendaciones.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strIndicador));
                        pdfCell.BackgroundColor = new Color(GVindicadorRecomendaciones.RowStyle.BackColor);
                        pdfpTable.AddCell(pdfCell);
                    }
                    if (iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVindicadorRecomendaciones.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strMetodologia));
                        pdfCell.BackgroundColor = new Color(GVindicadorRecomendaciones.RowStyle.BackColor);
                        pdfpTable.AddCell(pdfCell);
                    }
                    if (iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVindicadorRecomendaciones.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strFrecuencia));
                        pdfCell.BackgroundColor = new Color(GVindicadorRecomendaciones.RowStyle.BackColor);
                        pdfpTable.AddCell(pdfCell);
                    }
                    if (iteracion == 4)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVindicadorRecomendaciones.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strResponsable));
                        pdfCell.BackgroundColor = new Color(GVindicadorRecomendaciones.RowStyle.BackColor);
                        pdfpTable.AddCell(pdfCell);
                    }
                    if (iteracion == 5)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVindicadorRecomendaciones.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intMeta));
                        pdfCell.BackgroundColor = new Color(GVindicadorRecomendaciones.RowStyle.BackColor);
                        pdfpTable.AddCell(pdfCell);
                    }
                    if (iteracion == 6)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVindicadorRecomendaciones.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intAcumulado));
                        pdfCell.BackgroundColor = new Color(GVindicadorRecomendaciones.RowStyle.BackColor);
                        pdfpTable.AddCell(pdfCell);
                    }
                    if (iteracion == 7)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVindicadorRecomendaciones.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intCumplimiento));
                        pdfCell.BackgroundColor = new Color(GVindicadorRecomendaciones.RowStyle.BackColor);
                        pdfpTable.AddCell(pdfCell);
                    }
                    if (iteracion == 8)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVindicadorRecomendaciones.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(""));
                        pdfCell.BackgroundColor = new Color(GVindicadorRecomendaciones.RowStyle.BackColor);
                        pdfpTable.AddCell(pdfCell);
                    }
                    iteracion++;
                    /*iTextSharp.text.Font font = new iTextSharp.text.Font();
                    font.Color = new Color(GVindicadorRecomendaciones.RowStyle.ForeColor);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text));
                    pdfCell.BackgroundColor = new Color(GVindicadorRecomendaciones.RowStyle.BackColor);
                    pdfpTable.AddCell(pdfCell);*/
                }
            }
            if (ddlTipoReporte.SelectedValue == "1")
                mtdReportGeneral();
            if (ddlTipoReporte.SelectedValue == "2")
                mtdReporteDepartamento();
            MemoryStream streamControles = new MemoryStream();
            //MemoryStream streamRiesgosSarlaft = new MemoryStream();
            
                Chart1.SaveImage(streamControles, ChartImageFormat.Png);

            PdfPTable pdftblImageGraficoControles = new PdfPTable(1);


            iTextSharp.text.Image imagenGraficoControles = iTextSharp.text.Image.GetInstance(streamControles.GetBuffer());
            //iTextSharp.text.Image imagenGraficoSarlaft = iTextSharp.text.Image.GetInstance(streamRiesgosSarlaft.GetBuffer());

            PdfPCell pdfcellImageGraficoControles = new PdfPCell(imagenGraficoControles, true);
            //PdfPCell pdfcellImageGraficoRiesgosSarlaft = new PdfPCell(imagenGraficoSarlaft, true);
            pdftblImageGraficoControles.AddCell(pdfcellImageGraficoControles);
            //pdftblImageGraficoRiesgosSaro.AddCell(pdfcellImageGraficoControles);
            iTextSharp.text.HeaderFooter header = new iTextSharp.text.HeaderFooter(phHeader, false);
            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase(ddlTipoReporte.SelectedItem.Text));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfpTable);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(pdftblImageGraficoControles);
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=IndicadorRecomendaciones.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }

        protected void ImbCancel_Click(object sender, ImageClickEventArgs e)
        {
            mtdClean();
            BodyGridIR.Visible = false;
            dvGrafico.Visible = false;
            BodyGridDtpoDetalle.Visible = false;
            BodyGridDtpo.Visible = false;
            ddlDepartamentos.ClearSelection();
            txbFechaInicialDep.Text = string.Empty;
            txbFechaFinalDep.Text = string.Empty;
            ddPlaneacionDep.ClearSelection();

            Dbutton.Visible = false;

        }

        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            /*string strErrMsg = string.Empty;
            bool booResult = false;
            string año = string.Empty;
            clsIndicadorRecomendacionesBLL cRegistros = new clsIndicadorRecomendacionesBLL();
            List<clsIndicadorRecomendacionesDTO> lstIndicador = new List<clsIndicadorRecomendacionesDTO>();
            string fechaInicial = string.Empty;
            string fechaFinal = string.Empty;
            string planeacion = string.Empty;
            try
            {
                año = Sanitizer.GetSafeHtmlFragment(TXfechaInicial.Text);

                fechaInicial = Sanitizer.GetSafeHtmlFragment(txbFechaInicialGen.Text);
                fechaFinal = Sanitizer.GetSafeHtmlFragment(txbFechaFinalGen.Text);
                planeacion = ddlPlaneacion.SelectedValue;

            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error: " + ex, 2, "Atención");
            }
                lstIndicador = cRegistros.mtdConsultarIndicadorRecomendaciones(ref strErrMsg, ref lstIndicador, año, fechaInicial, fechaFinal, planeacion);*/
            if (ddlTipoReporte.SelectedValue == "1")
                mtdReportGeneral();
            string tmpChartName = "GraficoReporteGeneral.jpg";
            string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;
            Chart1.SaveImage(imgPath);
            string imgPath2 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=IndicadorRecomendaciones.xls;");
            string tab = "";
            System.Data.DataTable gridEncabezado = new System.Data.DataTable();
            gridEncabezado.Columns.Add("Tipo reporte:");
            gridEncabezado.Columns.Add("Año de Indicador:");
            gridEncabezado.Columns.Add("Fecha Inicial:");
            gridEncabezado.Columns.Add("Fecha Final:");
            gridEncabezado.Columns.Add("Proceso:");

            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Tipo reporte:"] = ddlTipoReporte.SelectedItem.Text;
            rowEncabezado["Año de Indicador:"] = TXfechaInicial.Text;
            rowEncabezado["Fecha Inicial:"] = txbFechaInicialGen.Text;
            rowEncabezado["Fecha Final:"] = txbFechaFinalGen.Text;
            if(ddlPlaneacion.SelectedValue != "0")
                rowEncabezado["Proceso:"] = ddlPlaneacion.SelectedItem.Text;
            else
                rowEncabezado["Proceso:"] = 0;
            gridEncabezado.Rows.Add(rowEncabezado);

            /*System.Data.DataTable gridReporte = new System.Data.DataTable();
            gridReporte.Columns.Add("Código");
            gridReporte.Columns.Add("Indicador");
            gridReporte.Columns.Add("Metodologia");
            gridReporte.Columns.Add("Frecuencia");
            gridReporte.Columns.Add("Responsable");
            gridReporte.Columns.Add("Meta");
            gridReporte.Columns.Add("Acumulado");
            gridReporte.Columns.Add("Cumplimiento");

            DataRow rowReporte;
            foreach (clsIndicadorRecomendacionesDTO objEvaComp in lstIndicador)
            {
                rowReporte = gridReporte.NewRow();
                rowReporte["Código"] = objEvaComp.intIdIndicador.ToString().Trim();
                rowReporte["Indicador"] = objEvaComp.strIndicador.ToString().Trim();
                rowReporte["Metodologia"] = objEvaComp.strMetodologia.ToString().Trim();
                rowReporte["Frecuencia"] = objEvaComp.strFrecuencia.ToString().Trim();
                rowReporte["Responsable"] = objEvaComp.strResponsable.ToString().Trim();
                rowReporte["Meta"] = objEvaComp.intMeta.ToString().Trim();
                rowReporte["Acumulado"] = objEvaComp.intAcumuladoDtpo.ToString().Trim();
                rowReporte["Cumplimiento"] = objEvaComp.intCumplimiento.ToString().Trim();
                
            }*/
            //InfoGrid
            foreach (DataColumn dc in gridEncabezado.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in gridEncabezado.Rows)
            {
                tab = "";
                for (i = 0; i < gridEncabezado.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString()+ "|");
                    tab = "\t";
                }
                Response.Write("\n");
            }
            /*foreach (DataColumn dc in gridReporte.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            foreach (DataRow dr in gridReporte.Rows)
            {
                tab = "";
                for (i = 0; i < gridReporte.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }*/

            StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                string headerTable = @"<Table><tr><td><img src='" + imgPath2 + @"' \></td></tr></Table>";
                Response.Write(headerTable);
                Response.Write(stringWrite.ToString());
                Response.Write("\n");
                Response.Write("\n");
            
            Response.End();
        }
    }
}