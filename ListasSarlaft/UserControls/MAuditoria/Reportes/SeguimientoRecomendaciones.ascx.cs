using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Security.Application;
using ListasSarlaft.Classes;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace ListasSarlaft.UserControls.MAuditoria.Reportes
{
    public partial class SeguimientoRecomendaciones : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.IBsearch);
            scriptManager.RegisterPostBackControl(this.ImbSearchRiesgo);
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
        #endregion Properties
        protected void IBsearch_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            string año = Sanitizer.GetSafeHtmlFragment(TXfechaInicial.Text);
            DataTable dtInfo = new DataTable();
            clsSeguimientoRecomendacionesBLL cRegistros = new clsSeguimientoRecomendacionesBLL();
            try
            {
                dtInfo = cRegistros.mtdGetValueRecomendacionesGeneral(ref strErrMsg, año);
                if (dtInfo != null)
                {
                    GVseguimientoRecomendaciones.DataSource = dtInfo;
                    GVseguimientoRecomendaciones.DataBind();
                    BodyGridRiesgos.Visible = false;
                    BodyGridSR.Visible = true;
                }
                else
                {
                    omb.ShowMessage(strErrMsg, 2, "Atención");
                }
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en la carga de Recomendaciones Generales: "+ex, 1, "Atención");
            }
            
            string FechaActual = Sanitizer.GetSafeHtmlFragment(txbFechaActual.Text);
            List<clsSeguimientoRecomendacionesDTO> lstRecomendaciones = new List<clsSeguimientoRecomendacionesDTO>();
            try
            {
                lstRecomendaciones = cRegistros.mtdGenerarReporteSeguimiento(ref strErrMsg, ref lstRecomendaciones, año, FechaActual);
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en la carga de los Seguimientos: "+ex, 1, "Atención");
            }
            
            int valorMayor12Alto = 0;
            int valorMayor12Medio = 0;
            int valorMayor12Bajo = 0;
            int totalMayor12 = 0;
            int valorMayor6Alto = 0;
            int valorMayor6Medio = 0;
            int valorMayor6Bajo = 0;
            int totalMayor6 = 0;
            int valorMayor4Alto = 0;
            int valorMayor4Medio = 0;
            int valorMayor4Bajo = 0;
            int totalMayor4 = 0;
            int valorMayor2Alto = 0;
            int valorMayor2Medio = 0;
            int valorMayor2Bajo = 0;
            int totalMayor2 = 0;
            int valorRecienteAlto = 0;
            int valorRecienteMedio = 0;
            int valorRecienteBajo = 0;
            int totalReciente = 0;
            try
            {
                if (lstRecomendaciones != null && lstRecomendaciones.Count > 0)
                {
                    foreach (clsSeguimientoRecomendacionesDTO objRecomendaciones in lstRecomendaciones)
                    {
                        if (objRecomendaciones.intMesDiff >= 12)
                        {
                            if (objRecomendaciones.strRiesgo == "Alto")
                                valorMayor12Alto = valorMayor12Alto + 1;
                            if (objRecomendaciones.strRiesgo == "Medio")
                                valorMayor12Medio = valorMayor12Medio + 1;
                            if (objRecomendaciones.strRiesgo == "Bajo")
                                valorMayor12Bajo = valorMayor12Bajo + 1;
                            //totalMayor12 = totalMayor12 + 1;
                        }
                        if (objRecomendaciones.intMesDiff >= 6 && objRecomendaciones.intMesDiff < 12)
                        {
                            if (objRecomendaciones.strRiesgo == "Alto")
                                valorMayor6Alto = valorMayor6Alto + 1;
                            if (objRecomendaciones.strRiesgo == "Medio")
                                valorMayor6Medio = valorMayor6Medio + 1;
                            if (objRecomendaciones.strRiesgo == "Bajo")
                                valorMayor6Bajo = valorMayor6Bajo + 1;
                            //totalMayor6 = totalMayor6 + 1;
                        }
                        if (objRecomendaciones.intMesDiff >= 4 && objRecomendaciones.intMesDiff < 6)
                        {
                            if (objRecomendaciones.strRiesgo == "Alto")
                                valorMayor4Alto = valorMayor4Alto + 1;
                            if (objRecomendaciones.strRiesgo == "Medio")
                                valorMayor4Medio = valorMayor4Medio + 1;
                            if (objRecomendaciones.strRiesgo == "Bajo")
                                valorMayor4Bajo = valorMayor4Bajo + 1;
                            //totalMayor4 = totalMayor4 + 1;
                        }
                        if (objRecomendaciones.intMesDiff >= 2 && objRecomendaciones.intMesDiff < 4)
                        {
                            if (objRecomendaciones.strRiesgo == "Alto")
                                valorMayor2Alto = valorMayor2Alto + 1;
                            if (objRecomendaciones.strRiesgo == "Medio")
                                valorMayor2Medio = valorMayor2Medio + 1;
                            if (objRecomendaciones.strRiesgo == "Bajo")
                                valorMayor2Bajo = valorMayor2Bajo + 1;
                            //totalMayor2 = totalMayor2 + 1;
                        }
                        if (objRecomendaciones.intMesDiff < 2)
                        {
                            if (objRecomendaciones.strRiesgo == "Alto")
                                valorRecienteAlto = valorRecienteAlto + 1;
                            if (objRecomendaciones.strRiesgo == "Medio")
                                valorRecienteMedio = valorRecienteMedio + 1;
                            if (objRecomendaciones.strRiesgo == "Bajo")
                                valorRecienteBajo = valorRecienteBajo + 1;
                            //totalReciente = totalReciente + 1;
                        }
                    }
                    totalMayor12 = valorMayor12Alto + valorMayor12Medio + valorMayor12Bajo;
                    totalMayor6 = valorMayor6Alto + valorMayor6Medio + valorMayor6Bajo;
                    totalMayor4 = valorMayor4Alto + valorMayor4Medio + valorMayor4Bajo;
                    totalMayor2 = valorMayor2Alto + valorMayor2Medio + valorMayor2Bajo;
                    totalReciente = valorRecienteAlto + valorRecienteMedio + valorRecienteBajo;
                    DataTable dtValoresRiesgos = new DataTable();
                    DataColumn dcColumn;

                    dcColumn = new DataColumn();
                    dcColumn.ColumnName = "Edad";
                    dtValoresRiesgos.Columns.Add(dcColumn);
                    dcColumn = new DataColumn();
                    dcColumn.ColumnName = "Alto";
                    dtValoresRiesgos.Columns.Add(dcColumn);
                    dcColumn = new DataColumn();
                    dcColumn.ColumnName = "Medio";
                    dtValoresRiesgos.Columns.Add(dcColumn);
                    dcColumn = new DataColumn();
                    dcColumn.ColumnName = "Bajo";
                    dtValoresRiesgos.Columns.Add(dcColumn);
                    dcColumn = new DataColumn();
                    dcColumn.ColumnName = "Total";
                    dtValoresRiesgos.Columns.Add(dcColumn);

                    DataRow dr;
                    dr = dtValoresRiesgos.NewRow();
                    dr["Edad"] = "Mayor 12 meses";
                    dr["Alto"] = valorMayor12Alto;
                    dr["Medio"] = valorMayor12Medio;
                    dr["Bajo"] = valorMayor12Bajo;
                    dr["Total"] = totalMayor12;
                    dtValoresRiesgos.Rows.Add(dr);
                    dr = dtValoresRiesgos.NewRow();
                    dr["Edad"] = "Mayor 6 meses";
                    dr["Alto"] = valorMayor6Alto;
                    dr["Medio"] = valorMayor6Medio;
                    dr["Bajo"] = valorMayor6Bajo;
                    dr["Total"] = totalMayor6;
                    dtValoresRiesgos.Rows.Add(dr);
                    dr = dtValoresRiesgos.NewRow();
                    dr["Edad"] = "Mayor 4 meses";
                    dr["Alto"] = valorMayor4Alto;
                    dr["Medio"] = valorMayor4Medio;
                    dr["Bajo"] = valorMayor4Bajo;
                    dr["Total"] = totalMayor4;
                    dtValoresRiesgos.Rows.Add(dr);
                    dr = dtValoresRiesgos.NewRow();
                    dr["Edad"] = "Mayor 2 meses";
                    dr["Alto"] = valorMayor2Alto;
                    dr["Medio"] = valorMayor2Medio;
                    dr["Bajo"] = valorMayor2Bajo;
                    dr["Total"] = totalMayor2;
                    dtValoresRiesgos.Rows.Add(dr);
                    dr = dtValoresRiesgos.NewRow();
                    dr["Edad"] = "Reciente";
                    dr["Alto"] = valorRecienteAlto;
                    dr["Medio"] = valorRecienteMedio;
                    dr["Bajo"] = valorRecienteBajo;
                    dr["Total"] = totalReciente;
                    dtValoresRiesgos.Rows.Add(dr);

                    GVvalorRiesgo.DataSource = dtValoresRiesgos;
                    GVvalorRiesgo.DataBind();
                    formRiesgos.Visible = true;
                }
            
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en la carga del detalle: " + ex, 1, "Atención");
            }
        }

        protected void GVvalorRiesgo_PreRender(object sender, EventArgs e)
        {
            int iteracion = 0;
            if(GVvalorRiesgo.Rows.Count > 0)
            {
                GVvalorRiesgo.Rows[0].Cells[1].BackColor = System.Drawing.Color.Red;
                GVvalorRiesgo.Rows[1].Cells[1].BackColor = System.Drawing.Color.Red;
                GVvalorRiesgo.Rows[2].Cells[1].BackColor = System.Drawing.Color.Red;
                GVvalorRiesgo.Rows[3].Cells[1].BackColor = System.Drawing.Color.Red;
                GVvalorRiesgo.Rows[4].Cells[1].BackColor = System.Drawing.Color.Red;

                GVvalorRiesgo.Rows[0].Cells[2].BackColor = System.Drawing.Color.Yellow;
                GVvalorRiesgo.Rows[1].Cells[2].BackColor = System.Drawing.Color.Yellow;
                GVvalorRiesgo.Rows[2].Cells[2].BackColor = System.Drawing.Color.Yellow;
                GVvalorRiesgo.Rows[3].Cells[2].BackColor = System.Drawing.Color.Yellow;
                GVvalorRiesgo.Rows[4].Cells[2].BackColor = System.Drawing.Color.Yellow;

                GVvalorRiesgo.Rows[0].Cells[3].BackColor = System.Drawing.Color.Green;
                GVvalorRiesgo.Rows[1].Cells[3].BackColor = System.Drawing.Color.Green;
                GVvalorRiesgo.Rows[2].Cells[3].BackColor = System.Drawing.Color.Green;
                GVvalorRiesgo.Rows[3].Cells[3].BackColor = System.Drawing.Color.Green;
                GVvalorRiesgo.Rows[4].Cells[3].BackColor = System.Drawing.Color.Green;
            }
            
            
        }

        protected void ImbSearchRiesgo_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            string año = Sanitizer.GetSafeHtmlFragment(TXfechaInicial.Text);
            DataTable dtInfo = new DataTable();
            clsSeguimientoRecomendacionesBLL cRegistros = new clsSeguimientoRecomendacionesBLL();
            string FechaActual = Sanitizer.GetSafeHtmlFragment(txbFechaActual.Text);
            List<clsSeguimientoRecomendacionesDTO> lstRecomendaciones = new List<clsSeguimientoRecomendacionesDTO>();
            lstRecomendaciones = cRegistros.mtdGenerarReporteSeguimiento(ref strErrMsg, ref lstRecomendaciones, año, FechaActual);
            if (dtInfo != null)
            {
                mtdLoadGridRiesgos();
                mtdLoadGridRiesgos(lstRecomendaciones);
                GVriesgosDetalle.DataSource = InfoGrid;
                GVriesgosDetalle.PageIndex = pagIndex;
                GVriesgosDetalle.DataBind();
                BodyGridSR.Visible = false;
                BodyGridRiesgos.Visible = true;
            }
            else
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }
        public void mtdLoadDetalle(ref string strErrMsg)
        {
            string año = Sanitizer.GetSafeHtmlFragment(TXfechaInicial.Text);
            DataTable dtInfo = new DataTable();
            clsSeguimientoRecomendacionesBLL cRegistros = new clsSeguimientoRecomendacionesBLL();
            string FechaActual = Sanitizer.GetSafeHtmlFragment(txbFechaActual.Text);
            List<clsSeguimientoRecomendacionesDTO> lstRecomendaciones = new List<clsSeguimientoRecomendacionesDTO>();
            lstRecomendaciones = cRegistros.mtdGenerarReporteSeguimiento(ref strErrMsg, ref lstRecomendaciones, año, FechaActual);
            Session["lstRecomendaciones"] = lstRecomendaciones;
            if (dtInfo != null)
            {
                mtdLoadGridRiesgos();
                mtdLoadGridRiesgos(lstRecomendaciones);
                GVriesgosDetalle.DataSource = InfoGrid;
                GVriesgosDetalle.PageIndex = pagIndex;
                GVriesgosDetalle.DataBind();
                BodyGridSR.Visible = false;
                BodyGridRiesgos.Visible = true;
            }
            else
            {
                omb.ShowMessage(strErrMsg, 2, "Atención");
            }
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridRiesgos()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdAuditoria", typeof(string));
            grid.Columns.Add("strTema", typeof(string));
            grid.Columns.Add("strFechaRegistro", typeof(string));
            grid.Columns.Add("intMesDiff", typeof(string));
            grid.Columns.Add("strHallazgo", typeof(string));
            grid.Columns.Add("intIdDependencia", typeof(string));
            grid.Columns.Add("strResponsable", typeof(string));
            grid.Columns.Add("strObservaciones", typeof(string));
            grid.Columns.Add("intIdNivelRiesgo", typeof(string));
            grid.Columns.Add("strRiesgo", typeof(string));
            grid.Columns.Add("dtFechaSeguimiento", typeof(string));
            grid.Columns.Add("strSeguimiento", typeof(string));

            GVriesgosDetalle.DataSource = grid;
            GVriesgosDetalle.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstIndicadores">Lista con los Indicadores</param>
        private void mtdLoadGridRiesgos(List<clsSeguimientoRecomendacionesDTO> lstIndicadores)
        {
            string strErrMsg = String.Empty;

            foreach (clsSeguimientoRecomendacionesDTO objEvaComp in lstIndicadores)
            {
                if(objEvaComp.strRiesgo == ddlRiesgos.SelectedItem.Text)
                {
                    InfoGrid.Rows.Add(new Object[] {
                    objEvaComp.intIdAuditoria.ToString().Trim(),
                    objEvaComp.strTema.ToString().Trim(),
                    objEvaComp.strFechaRegistro.ToString().Trim(),
                    objEvaComp.intMesDiff.ToString().Trim(),
                    objEvaComp.strHallazgo.ToString().Trim(),
                    objEvaComp.intIdDependencia.ToString().Trim(),
                    objEvaComp.strResponsable.ToString().Trim(),
                    objEvaComp.strObservaciones.ToString().Trim(),
                    objEvaComp.intIdNivelRiesgo.ToString().Trim(),
                    objEvaComp.strRiesgo.ToString().Trim(),
                    objEvaComp.dtFechaSeguimiento.ToShortDateString(),
                    objEvaComp.strSeguimiento.ToString().Trim()
                    });
                }
                
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            BodyGridRiesgos.Visible = false;
            BodyGridSR.Visible = false;
            formRiesgos.Visible = false;
            ddlRiesgos.SelectedIndex = 0;
            TXfechaInicial.Text = string.Empty;
            txbFechaActual.Text = string.Empty;
        }

        protected void ImbCancelDtpo_Click(object sender, ImageClickEventArgs e)
        {
            BodyGridRiesgos.Visible = false;
            ddlRiesgos.SelectedIndex = 0;
        }

        protected void GVriesgosDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagIndex = e.NewPageIndex;
            string strErrMsg = "";
            mtdLoadDetalle(ref strErrMsg);
        }

        protected void GVriesgosDetalle_PreRender(object sender, EventArgs e)
        {
            for (int rowIndex = GVriesgosDetalle.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = GVriesgosDetalle.Rows[rowIndex];
                GridViewRow previousRow = GVriesgosDetalle.Rows[rowIndex + 1];

                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    if(cellIndex == 7)
                    {
                        string fecha = row.Cells[cellIndex].Text;
                        string fechaPre = previousRow.Cells[cellIndex].Text;
                        if (fecha == default(DateTime).ToShortDateString())
                        {
                            row.Cells[cellIndex].Text = "";
                        }
                        if(fechaPre == default(DateTime).ToShortDateString())
                        {
                            previousRow.Cells[cellIndex].Text = "";
                        }
                    }
                }
            }
        }
        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteSeguimientoRecomendaciones" + ddlRiesgos.SelectedItem.Text + "_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {

            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("No.AUD:");
            gridEncabezado.Columns.Add("Auditoria:");
            gridEncabezado.Columns.Add("Fecha Informe:");
            gridEncabezado.Columns.Add("Edad (Meses):");
            gridEncabezado.Columns.Add("Hallazgo:");
            gridEncabezado.Columns.Add("Recomendación:");
            gridEncabezado.Columns.Add("Responsable:");
            gridEncabezado.Columns.Add("Fecha Seguimiento:");
            gridEncabezado.Columns.Add("Observaciones:");
            gridEncabezado.Columns.Add("Nivel Riesgo:");

            DataRow rowEncabezado;
            string año = Sanitizer.GetSafeHtmlFragment(TXfechaInicial.Text);
            DataTable dtInfo = new DataTable();
            clsSeguimientoRecomendacionesBLL cRegistros = new clsSeguimientoRecomendacionesBLL();
            string FechaActual = Sanitizer.GetSafeHtmlFragment(txbFechaActual.Text);
            List<clsSeguimientoRecomendacionesDTO> lstRecomendaciones = new List<clsSeguimientoRecomendacionesDTO>();
            string strErrMsg = string.Empty;
            lstRecomendaciones = cRegistros.mtdGenerarReporteSeguimiento(ref strErrMsg, ref lstRecomendaciones, año, FechaActual);
            /*foreach (GridViewRow GridViewRow in GVriesgosDetalle.Rows)
            {
                rowEncabezado = gridEncabezado.NewRow();
                string strTema = ((Label)GridViewRow.FindControl("strTema")).Text;

                rowEncabezado["No.AUD:"] = GridViewRow.Cells[0].Text;
                rowEncabezado["Auditoria:"] = strTema;
                rowEncabezado["Fecha Informe:"] = GridViewRow.Cells[1].Text;
                rowEncabezado["Edad (Meses):"] = GridViewRow.Cells[2].Text;
                rowEncabezado["Hallazgo:"] = GridViewRow.Cells[3].Text;
                rowEncabezado["Recomendación:"] = GridViewRow.Cells[4].Text;
                rowEncabezado["Responsable:"] = GridViewRow.Cells[5].Text;
                rowEncabezado["Fecha Seguimiento:"] = GridViewRow.Cells[6].Text;
                rowEncabezado["Observaciones:"] = GridViewRow.Cells[7].Text;
                rowEncabezado["Nivel Riesgo:"] = GridViewRow.Cells[9].Text;
                gridEncabezado.Rows.Add(rowEncabezado);
            }*/
            foreach (clsSeguimientoRecomendacionesDTO objEvaComp in lstRecomendaciones)
            {
                if (objEvaComp.strRiesgo == ddlRiesgos.SelectedItem.Text)
                {
                    rowEncabezado = gridEncabezado.NewRow();

                    rowEncabezado["No.AUD:"] = objEvaComp.intIdAuditoria.ToString().Trim();
                    rowEncabezado["Auditoria:"] = objEvaComp.strTema.ToString().Trim();
                    rowEncabezado["Fecha Informe:"] = objEvaComp.strFechaRegistro.ToString().Trim();
                    rowEncabezado["Edad (Meses):"] = objEvaComp.intMesDiff.ToString().Trim();
                    rowEncabezado["Hallazgo:"] = objEvaComp.strHallazgo.ToString().Trim();
                    rowEncabezado["Recomendación:"] = objEvaComp.strObservaciones.ToString().Trim();
                    rowEncabezado["Responsable:"] = objEvaComp.strResponsable.ToString().Trim();
                    rowEncabezado["Fecha Seguimiento:"] = objEvaComp.dtFechaSeguimiento.ToShortDateString();
                    rowEncabezado["Observaciones:"] = objEvaComp.strSeguimiento.ToString().Trim();
                    rowEncabezado["Nivel Riesgo:"] = objEvaComp.strRiesgo.ToString().Trim();
                    gridEncabezado.Rows.Add(rowEncabezado);
                }

            }
            //gridEncabezado.Rows.Add(rowEncabezado);


            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(gridEncabezado, "AuditoriaConsolidado");
            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + filename + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }
    }
}