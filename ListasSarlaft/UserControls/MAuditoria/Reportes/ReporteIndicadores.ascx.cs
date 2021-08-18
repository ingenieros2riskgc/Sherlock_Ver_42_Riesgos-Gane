using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using DataSets = System.Data;
using clsLogica;
using clsDTO;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using ClosedXML.Excel;
using Microsoft.Security.Application;
using System.Web.UI.DataVisualization.Charting;
using ListasSarlaft.Classes.Utilerias;

namespace ListasSarlaft.UserControls.MAuditoria.Reportes
{
    public partial class ReporteIndicadores : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            mtdInicializarValores();
        }
        #region Propiedades
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;
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
        private DataTable infoGridConsolidado;
        private int rowGridConsolidado;
        private int pagIndexConsolidado;
        private DataTable InfoGridConsolidado
        {
            get
            {
                infoGridConsolidado = (DataTable)ViewState["infoGridConsolidado"];
                return infoGridConsolidado;
            }
            set
            {
                infoGridConsolidado = value;
                ViewState["infoGridConsolidado"] = infoGridConsolidado;
            }
        }

        private int RowGridConsolidado
        {
            get
            {
                rowGridConsolidado = (int)ViewState["rowGridConsolidado"];
                return rowGridConsolidado;
            }
            set
            {
                rowGridConsolidado = value;
                ViewState["rowGridConsolidado"] = rowGridConsolidado;
            }
        }

        private int PagIndexConsolidado
        {
            get
            {
                pagIndexConsolidado = (int)ViewState["pagIndexConsolidado"];
                return pagIndexConsolidado;
            }
            set
            {
                pagIndexConsolidado = value;
                ViewState["pagIndexConsolidado"] = pagIndexConsolidado;
            }
        }
        #endregion Propiedades
        private void mtdInicializarValores()
        {
            PagIndex = 0;
        }
        protected void ImbSearchFiltro_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            if (!mtdLoadReporteIndicadores(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }
        private bool mtdLoadReporteIndicadores(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;

            List<clsReporteIndicadoresDTO> lstReporteIndicador = new List<clsReporteIndicadoresDTO>();
            clsReporteIndicadoresBLL cReporte = new clsReporteIndicadoresBLL();
            clsReporteIndicadoresDTO objReporte = new clsReporteIndicadoresDTO();
            #endregion Vars
            objReporte.strAno = Sanitizer.GetSafeHtmlFragment(txbAno.Text);
            int Mes = 0;
            if(ddlMesEjecucion.SelectedValue != "0" && ddlMesEjecucion.SelectedValue != "")
            {
                objReporte.intMes = Convert.ToInt32(ddlMesEjecucion.SelectedValue);
            }
            lstReporteIndicador = cReporte.mtdReporteIndicadores(ref strErrMsg, ref lstReporteIndicador, objReporte);

            if (lstReporteIndicador != null)
            {
                mtdLoadReporteIndicadores();
                mtdLoadReporteIndicadores(lstReporteIndicador);
                mtdGenerateCharts(lstReporteIndicador);
                GVreporte.DataSource = lstReporteIndicador;
                GVreporte.PageIndex = pagIndex;
                GVreporte.DataBind();
                dvGridReporte.Visible = true;
                dvGraficos.Visible = true;
                Dbutton.Visible = true;
                booResult = true;
            }
            
            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadReporteIndicadores()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("strNombreMes", typeof(string));
            grid.Columns.Add("dbRealizadas", typeof(string));
            grid.Columns.Add("dbProgramadas", typeof(string));
            grid.Columns.Add("dbCumplimiento", typeof(string));
            
            GVreporte.DataSource = grid;
            GVreporte.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadReporteIndicadores(List<clsReporteIndicadoresDTO> lstIndicador)
        {
            string strErrMsg = String.Empty;

            foreach (clsReporteIndicadoresDTO objReau in lstIndicador)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objReau.strNombreMes.ToString().Trim(),
                    objReau.dbRealizadas.ToString().Trim(),
                    objReau.dbProgramadas.ToString().Trim(),
                    Decimal.Round(Convert.ToDecimal(objReau.dbCumplimiento.ToString().Trim()),2).ToString()
                    });
            }
        }
        private void mtdGenerateCharts(List<clsReporteIndicadoresDTO> lstIndicador)
        {
            DataTable dtLoad = mtdLoadTableCharts(lstIndicador);
            LoadChartGeneral(dtLoad);
        }
        private DataTable mtdLoadTableCharts(List<clsReporteIndicadoresDTO> lstIndicador)
        {
            DataTable initialDataSource = new DataTable();
            initialDataSource.Columns.Add("Mes", Type.GetType("System.String"));
            initialDataSource.Columns.Add("Cumplimiento", Type.GetType("System.String"));
            foreach (clsReporteIndicadoresDTO objReau in lstIndicador)
            {
                DataRow dr1 = initialDataSource.NewRow();
                dr1["Mes"] = objReau.strNombreMes;
                dr1["Cumplimiento"] = objReau.dbCumplimiento;
                initialDataSource.Rows.Add(dr1);
            }

                return initialDataSource;
        }
        private void LoadChartGeneral(System.Data.DataTable dtInfo)
        {
            string[] x = new string[dtInfo.Rows.Count];
            int[] y = new int[dtInfo.Rows.Count];
            //int Total = 0;
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                x[i] = dtInfo.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(Math.Round(Convert.ToDouble(dtInfo.Rows[i][1].ToString())));
                //Total = Total + Convert.ToInt32(dtInfo.Rows[i][1]);
                //ChartRiesgoInherente.Series[0].Points.AddY(Convert.ToInt32(dtInfo.Rows[i][1]));
                //ChartRiesgoInherente.Series[0].Points.AddXY(Convert.ToInt32(dtInfo.Rows[i][0]), Convert.ToInt32(dtInfo.Rows[i][1]));
            }
            ChartRiesgoInherente.Series[0].Points.DataBindXY(x, y);
            ChartRiesgoInherente.Series[0].ChartType = SeriesChartType.Bar;
            ChartRiesgoInherente.ChartAreas["ChartRiesgoInherenteArea"].Area3DStyle.Enable3D = true;
            ChartRiesgoInherente.Legends[0].Enabled = true;
            ChartRiesgoInherente.Titles.Add("NewTitle");
            ChartRiesgoInherente.Titles[1].Text = "Porcentaje Cumplimiento de Indicadores: ";
            ChartRiesgoInherente.ChartAreas["ChartRiesgoInherenteArea"].AxisX.Title = "Mes";
            ChartRiesgoInherente.ChartAreas["ChartRiesgoInherenteArea"].AxisY.Title = "Cantidad";

            foreach (System.Web.UI.DataVisualization.Charting.Series charts in ChartRiesgoInherente.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    switch (point.AxisLabel)
                    {
                        case "enero": point.Color = System.Drawing.Color.Blue; break;
                        case "febrero": point.Color = System.Drawing.Color.Green; break;
                        case "marzo": point.Color = System.Drawing.Color.Yellow; break;
                        case "abril": point.Color = System.Drawing.Color.Orange; break;
                        case "mayo": point.Color = System.Drawing.Color.Red; break;
                        case "junio": point.Color = System.Drawing.Color.Bisque; break;
                        case "julio": point.Color = System.Drawing.Color.BurlyWood; break;
                        case "agosto": point.Color = System.Drawing.Color.Crimson; break;
                        case "septiembre": point.Color = System.Drawing.Color.BlueViolet; break;
                        case "octubre": point.Color = System.Drawing.Color.Brown; break;
                        case "noviembre": point.Color = System.Drawing.Color.Gray; break;
                        case "diciembre": point.Color = System.Drawing.Color.HotPink; break;
                    }
                    point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

                }
            }
        }

        protected void ImbCancel_Click(object sender, ImageClickEventArgs e)
        {
            txbAno.Text = string.Empty;
            ddlMesEjecucion.SelectedIndex = 0;
            dvGridReporte.Visible = false;
            GVreporte.DataSource = null;
            GVreporte.DataBind();
            dvGraficos.Visible = false;
            Dbutton.Visible = false;
        }

        protected void ImbCancelB_Click(object sender, ImageClickEventArgs e)
        {
            txbAno.Text = string.Empty;
            ddlMesEjecucion.SelectedIndex = 0;
            dvGridReporte.Visible = false;
            GVreporte.DataSource = null;
            GVreporte.DataBind();
            dvGraficos.Visible = false;
            Dbutton.Visible = false;
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
            pdfDocument.AddTitle("Reporte de Indicadores");
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
            pdfCellEncabezado = new PdfPCell(new Phrase("Reporte Indicadores"));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Año de Indicador:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(txbAno.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase("Mes de Ejecución:", font1));
            pdfCellEncabezado.BackgroundColor = Color.LIGHT_GRAY;
            pdfTableData.AddCell(pdfCellEncabezado);
            pdfCellEncabezado = new PdfPCell(new Phrase(ddlMesEjecucion.SelectedItem.Text));
            pdfTableData.AddCell(pdfCellEncabezado);
            
            PdfPTable pdfpTable = new PdfPTable(2);

            pdfpTable = new PdfPTable(GVreporte.HeaderRow.Cells.Count);
            //int iteracion = 0;
            foreach (TableCell headerCell in GVreporte.HeaderRow.Cells)
            {
                /*if(iteracion <= 7)
                {*/
                iTextSharp.text.Font font = new iTextSharp.text.Font();
                font.Color = new Color(GVreporte.HeaderStyle.ForeColor);
                PdfPCell pdfCell = new PdfPCell(new Phrase(HttpUtility.HtmlDecode(headerCell.Text), font));
                pdfCell.BackgroundColor = new Color(GVreporte.HeaderStyle.BackColor);
                pdfpTable.AddCell(pdfCell);
                /*}
                iteracion++;*/
            }

            foreach (GridViewRow GridViewRow in GVreporte.Rows)
            {
                string strNombreMes = ((Label)GridViewRow.FindControl("strNombreMes")).Text;
                string dbRealizadas = ((Label)GridViewRow.FindControl("dbRealizadas")).Text;
                string dbProgramadas = ((Label)GridViewRow.FindControl("dbProgramadas")).Text;
                string dbCumplimiento = ((Label)GridViewRow.FindControl("dbCumplimiento")).Text;
                
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    if (iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporte.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strNombreMes));
                        pdfCell.BackgroundColor = new Color(GVreporte.RowStyle.BackColor);
                        pdfpTable.AddCell(pdfCell);
                    }
                    if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporte.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(dbRealizadas));
                        pdfCell.BackgroundColor = new Color(GVreporte.RowStyle.BackColor);
                        pdfpTable.AddCell(pdfCell);
                    }
                    if (iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporte.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(dbProgramadas));
                        pdfCell.BackgroundColor = new Color(GVreporte.RowStyle.BackColor);
                        pdfpTable.AddCell(pdfCell);
                    }
                    if (iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporte.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(dbCumplimiento));
                        pdfCell.BackgroundColor = new Color(GVreporte.RowStyle.BackColor);
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
            string strErrMsg = string.Empty;
            if (!mtdLoadReporteIndicadores(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            /*if (ddlTipoReporte.SelectedValue == "1")
                mtdReportGeneral();
            if (ddlTipoReporte.SelectedValue == "2")
                mtdReporteDepartamento();*/
            MemoryStream streamControles = new MemoryStream();
            //MemoryStream streamRiesgosSarlaft = new MemoryStream();

            ChartRiesgoInherente.SaveImage(streamControles, ChartImageFormat.Png);

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
            Paragraph Titulo = new Paragraph(new Phrase("Reporte Indicadores"));
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
            Response.AddHeader("content-disposition", "attachment;filename=ReporteIndicadores.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
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
            /*if (ddlTipoReporte.SelectedValue == "1")
                mtdReportGeneral();*/
            string strErrMsg = string.Empty;
            if (!mtdLoadReporteIndicadores(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
            string tmpChartName = "GraficoReporteIndicadores.jpg";
            string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;
            ChartRiesgoInherente.SaveImage(imgPath);
            string imgPath2 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=ReporteIndicadores.xls;");
            string tab = "";
            System.Data.DataTable gridEncabezado = new System.Data.DataTable();
            gridEncabezado.Columns.Add("Tipo reporte:");
            gridEncabezado.Columns.Add("Año de Indicador:");
            gridEncabezado.Columns.Add("Mes de Ejecución:");
            

            DataRow rowEncabezado;
            rowEncabezado = gridEncabezado.NewRow();
            rowEncabezado["Tipo reporte:"] = "Reporte Indicadores";
            rowEncabezado["Año de Indicador:"] = txbAno.Text;
            rowEncabezado["Mes de Ejecución:"] = ddlMesEjecucion.SelectedItem.Text;
            
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
                    Response.Write(tab + dr[i].ToString() + "|");
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