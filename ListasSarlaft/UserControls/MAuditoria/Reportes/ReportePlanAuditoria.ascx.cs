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

namespace ListasSarlaft.UserControls.MAuditoria.Reportes
{
    public partial class ReportePlanAuditoria : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexport);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelExport);
            scriptManager.RegisterPostBackControl(this.ImButtonPDFexportAud);
            scriptManager.RegisterPostBackControl(this.ImButtonExcelexportAud);
            mtdInicializarValores();
            SqlDSplaneaciones.SelectParameters["IdArea"].DefaultValue = Session["IdAreaUser"].ToString();
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
            ddlProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Seleccione---", "0")); // Inserta el Item con texto Vacio
            /*PagIndex2 = 0;*/
            //txtNombreEva.Text = "";
            //TXfecharegistro.Text = "" + DateTime.Now;
            //tbxResponsable.Text = "";
            //TXjefe.Text = "";
        }
        protected void ddlMacroProceso_DataBound(object sender, EventArgs e)
    {
        ddlMacroProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Seleccione---", "0")); // Inserta el Item con texto Vacio  
    }
    protected void ddlMacroProceso_SelectedIndexChanged(object sender, EventArgs e)
    {
        string IdMacro = ddlMacroProceso.SelectedValue;
            string strErrMsg = string.Empty;
            clsReportesAuditoriaDAL cReporte = new clsReportesAuditoriaDAL();
            DataTable dt = cReporte.mtdGetProceso(ref strErrMsg, IdMacro);
            ddlProceso.DataSource = dt;
            ddlProceso.DataBind();
    }
        
        protected void IBsearch_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            if (!mtdLoadReporteAuditoria(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }
        private bool mtdLoadReporteAuditoria(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            GridViewRow row = GVreporteConsolidado.Rows[RowGridConsolidado];
            var colsNoVisible = GVreporteConsolidado.DataKeys[RowGridConsolidado].Values;
            int IdRegistro = Convert.ToInt32(colsNoVisible[0].ToString());
            List<clsReporteAuditoriaDTO> lstReporteAuditoria = new List<clsReporteAuditoriaDTO>();
            clsReporteAuditoriaBLL cReporte = new clsReporteAuditoriaBLL();
            string idMacroProceso = string.Empty;
            string IdProceso = string.Empty;
            string Mes = string.Empty;
            string Periodicidad = string.Empty;
            string IdGrupoAuditoria = string.Empty;
            string auditor = string.Empty;
            #endregion Vars
            try
            {
                 idMacroProceso = ddlMacroProceso.SelectedValue;
                 IdProceso = ddlProceso.SelectedValue;
                 Mes = ddlMesEjecucion.SelectedValue;
                 Periodicidad = ddlPeriodicidad.SelectedValue;
                 IdGrupoAuditoria = ddlGrupoAud2.SelectedValue;
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en toma de filtros: "+ex, 2, "Atención");
            }
            try
            {
                lstReporteAuditoria = cReporte.mtdReporteAuditoria(ref strErrMsg, ref lstReporteAuditoria, IdRegistro, idMacroProceso, IdProceso, Mes, Periodicidad, IdGrupoAuditoria);
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en la consulta: " + ex, 2, "Atención");
            }

            try
            {
                if (lstReporteAuditoria != null)
                {
                    mtdLoadReporteAuditoria();
                    mtdLoadReporteAuditoria(lstReporteAuditoria);
                    
                    dvGrid.Visible = true;
                    booResult = true;
                }else
                {
                    GVreporteAuditoria.DataSource = null;
                    GVreporteAuditoria.DataBind();
                }
            }catch(Exception ex)
            {
                omb.ShowMessage("Error en la generación del reporte: " + ex, 2, "Atención");
            }
            
            DbuttonsAud.Visible = true;
            DbuttonConsolidado.Visible = false;
            form.Visible = true;
            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadReporteAuditoria()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intIdDependencia", typeof(string));
            grid.Columns.Add("strNombreDependencia", typeof(string));
            grid.Columns.Add("intIdProceso", typeof(string));
            grid.Columns.Add("strNombreProces", typeof(string));
            grid.Columns.Add("strTema", typeof(string));
            grid.Columns.Add("strObjetivo", typeof(string));
            grid.Columns.Add("strNombreObjetivo", typeof(string));
            grid.Columns.Add("strDescripcionObjetivo", typeof(string));
            grid.Columns.Add("strEstado", typeof(string));
            grid.Columns.Add("intIdGrupoAuditoria", typeof(string));
            grid.Columns.Add("strGrupoAuditoria", typeof(string));
            grid.Columns.Add("strEspecial", typeof(string));
            grid.Columns.Add("strPeriodicidad", typeof(string));
            grid.Columns.Add("strMes", typeof(string));
            grid.Columns.Add("strMesExe", typeof(string));
            grid.Columns.Add("strSemanaExe", typeof(string));
            grid.Columns.Add("strFechaInicio", typeof(string));
            grid.Columns.Add("strFechaCierre", typeof(string));
            GVreporteAuditoria.DataSource = grid;
            GVreporteAuditoria.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadReporteAuditoria(List<clsReporteAuditoriaDTO> lstControl)
        {
            string strErrMsg = String.Empty;

            foreach (clsReporteAuditoriaDTO objReau in lstControl)
            {
                if(ddlPeriodicidad.SelectedValue != "0")
                {
                    if (ddlPeriodicidad.SelectedItem.Text == objReau.strPeriodicidad)
                    {
                        InfoGrid.Rows.Add(new Object[] {
                    objReau.intIdDependencia.ToString().Trim(),
                    objReau.strNombreDependencia.ToString().Trim(),
                    objReau.intIdProceso.ToString().Trim(),
                    objReau.strNombreProces.ToString().Trim(),
                    objReau.strTema.ToString().Trim(),
                    objReau.strObjetivo.ToString().Trim(),
                    objReau.strNombreObjetivo.ToString().Trim(),
                    objReau.strDescripcionObjetivo.ToString().Trim(),
                    objReau.strEstado.ToString().Trim(),
                    objReau.intIdGrupoAuditoria.ToString().Trim(),
                    objReau.strGrupoAuditoria.ToString().Trim(),
                    objReau.strEspecial.ToString().Trim(),
                    objReau.strPeriodicidad.ToString().Trim(),
                    objReau.strMes.ToString().Trim(),
                    objReau.strMesExe.ToString().Trim(),
                    objReau.strSemanaExe.ToString().Trim(),
                    objReau.strFechaInicio.ToString().Trim(),
                    objReau.strFechaCierre.ToString().Trim()
                    });


                    }

                }else
                {
                    InfoGrid.Rows.Add(new Object[] {
                    objReau.intIdDependencia.ToString().Trim(),
                    objReau.strNombreDependencia.ToString().Trim(),
                    objReau.intIdProceso.ToString().Trim(),
                    objReau.strNombreProces.ToString().Trim(),
                    objReau.strTema.ToString().Trim(),
                    objReau.strObjetivo.ToString().Trim(),
                    objReau.strNombreObjetivo.ToString().Trim(),
                    objReau.strDescripcionObjetivo.ToString().Trim(),
                    objReau.strEstado.ToString().Trim(),
                    objReau.intIdGrupoAuditoria.ToString().Trim(),
                    objReau.strGrupoAuditoria.ToString().Trim(),
                    objReau.strEspecial.ToString().Trim(),
                    objReau.strPeriodicidad.ToString().Trim(),
                    objReau.strMes.ToString().Trim(),
                    objReau.strMesExe.ToString().Trim(),
                    objReau.strSemanaExe.ToString().Trim(),
                    objReau.strFechaInicio.ToString().Trim(),
                    objReau.strFechaCierre.ToString().Trim()
                    });
                }
                
            }
            GVreporteAuditoria.DataSource = InfoGrid;
            GVreporteAuditoria.PageIndex = pagIndex;
            GVreporteAuditoria.DataBind();
        }

        private bool mtdLoadReporteConsolidado(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsReporteConsolidadoDTO> lstReporteAuditoria = new List<clsReporteConsolidadoDTO>();
            clsReporteAuditoriaBLL cReporte = new clsReporteAuditoriaBLL();
            #endregion Vars
            string IdPlaneacion = ddlPlaneacion.SelectedValue;
            string IdRegistroAud = ddlRegistrosAud.SelectedValue;
            string Mes = ddlMesEjecucion.SelectedValue;
            string Periodicidad = ddlPeriodicidad.SelectedValue;
            string IdGrupoAuditoria = ddlGrupoAud2.SelectedValue;
            lstReporteAuditoria = cReporte.mtdReporteConsolidado(ref strErrMsg, ref lstReporteAuditoria, IdPlaneacion, IdRegistroAud);

            if (lstReporteAuditoria != null)
            {
                mtdLoadReporteConsolidado();
                mtdLoadReporteConsolidado(lstReporteAuditoria);
                GVreporteConsolidado.DataSource = lstReporteAuditoria;
                GVreporteConsolidado.PageIndex = pagIndexConsolidado;
                GVreporteConsolidado.DataBind();
                dvGridConsolidado.Visible = true;
                booResult = true;
            }
            DbuttonConsolidado.Visible = true;
            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadReporteConsolidado()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("strPlaneacion", typeof(string));
            grid.Columns.Add("intIdRegistro", typeof(string));
            grid.Columns.Add("strTema", typeof(string));
            grid.Columns.Add("dbRealizadas", typeof(string));
            grid.Columns.Add("dbProgramadas", typeof(string));
            grid.Columns.Add("dbCumplimiento", typeof(string));
            grid.Columns.Add("strFechaInicio", typeof(string));
            grid.Columns.Add("strFechaCierre", typeof(string));
            GVreporteConsolidado.DataSource = grid;
            GVreporteConsolidado.DataBind();
            InfoGridConsolidado = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadReporteConsolidado(List<clsReporteConsolidadoDTO> lstControl)
        {
            string strErrMsg = String.Empty;

            foreach (clsReporteConsolidadoDTO objReau in lstControl)
            {

                InfoGridConsolidado.Rows.Add(new Object[] {
                    objReau.strPlaneacion.ToString().Trim(),
                    objReau.intIdRegistro.ToString().Trim(),
                    objReau.strTema.ToString().Trim(),
                    objReau.dbRealizadas.ToString().Trim(),
                    objReau.dbProgramadas.ToString().Trim(),
                    objReau.dbCumplimiento.ToString().Trim(),
                    objReau.strFechaInicio.ToString().Trim(),
                    objReau.strFechaCierre.ToString().Trim()
                    });
            }
        }

        protected void GVreporteConsolidado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridConsolidado = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    dvGridConsolidado.Visible = false;
                    dvFiltros.Visible = false;
                    string strErrMsg = string.Empty;
                    if (!mtdLoadReporteAuditoria(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                    break;
            }
        }

        protected void ImButtonPDFexport_Click(object sender, ImageClickEventArgs e)
        {
            mtdExportPdf();
        }
        private void mtdExportPdf()
        {
            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, Color.BLACK);

            Document pdfDocument = new Document(PageSize.A4, 1, 1, 5, 20);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Reporte de Auditoria Consolidado");
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
            pdfcellImage.Border = Rectangle.NO_BORDER;
            pdfcellImage.Border = Rectangle.NO_BORDER;
            float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImage);
            PdfPCell pdfcellImageEmpresa = new PdfPCell(imagenEmpresa, true);
            pdfcellImageEmpresa.FixedHeight = 40f;
            pdfcellImageEmpresa.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.VerticalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            percentage = 40 / imagenEmpresa.Width;
            imagenEmpresa.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImageEmpresa);
            //Chunk chnCompany = new Chunk("Risk Consulting", _standardFont);
            Phrase phHeader = new Phrase();

            phHeader.Add(pdftblImage);
            //phHeader.Add(chnCompany);
            #region Tabla de Datos Principales
            
            PdfPTable pdfTableData = new PdfPTable(GVreporteConsolidado.HeaderRow.Cells.Count);
            int iteracionHeader = 0;
            foreach (TableCell headerCell in GVreporteConsolidado.HeaderRow.Cells)
            {
                /*if(iteracionHeader != 8)
                {*/
                    Font font = new Font();
                    font.Color = new Color(GVreporteConsolidado.HeaderStyle.ForeColor);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                    pdfCell.BackgroundColor = new Color(GVreporteConsolidado.HeaderStyle.BackColor);
                    pdfTableData.AddCell(pdfCell);
                //}
                iteracionHeader++;
            }

            foreach (GridViewRow GridViewRow in GVreporteConsolidado.Rows)
            {
                string strPlaneacion = ((Label)GridViewRow.FindControl("strPlaneacion")).Text;
                string intIdRegistro = ((Label)GridViewRow.FindControl("intIdRegistro")).Text;
                string strTema = ((Label)GridViewRow.FindControl("strTema")).Text;
                string strFechaInicio = ((Label)GridViewRow.FindControl("strFechaInicio")).Text;
                string strFechaCierre = ((Label)GridViewRow.FindControl("strFechaCierre")).Text;
                string dbRealizadas = ((Label)GridViewRow.FindControl("dbRealizadas")).Text;
                string dbProgramadas = ((Label)GridViewRow.FindControl("dbProgramadas")).Text;
                string dbCumplimiento = ((Label)GridViewRow.FindControl("dbCumplimiento")).Text;
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {
                    
                    if (iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteConsolidado.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strPlaneacion));
                        pdfCell.BackgroundColor = new Color(GVreporteConsolidado.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteConsolidado.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(intIdRegistro));
                        pdfCell.BackgroundColor = new Color(GVreporteConsolidado.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteConsolidado.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strTema));
                        pdfCell.BackgroundColor = new Color(GVreporteConsolidado.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteConsolidado.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(dbRealizadas));
                        pdfCell.BackgroundColor = new Color(GVreporteConsolidado.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 4)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteConsolidado.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(dbProgramadas));
                        pdfCell.BackgroundColor = new Color(GVreporteConsolidado.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 5)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteConsolidado.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(dbCumplimiento));
                        pdfCell.BackgroundColor = new Color(GVreporteConsolidado.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 6)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteConsolidado.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strFechaInicio));
                        pdfCell.BackgroundColor = new Color(GVreporteConsolidado.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 7)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteConsolidado.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strFechaCierre));
                        pdfCell.BackgroundColor = new Color(GVreporteConsolidado.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 8)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteConsolidado.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(""));
                        pdfCell.BackgroundColor = new Color(GVreporteConsolidado.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    iteracion++;
                }

            }

            #endregion

            HeaderFooter header = new HeaderFooter(phHeader, false);
            header.Border = Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();

            /*float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);*/
            //PdfPCell clImagen = new PdfPCell(imagen);
            //pdfDocument.Add(imagen);

            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Auditoria Consolidado"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);
            
            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteAuditoriaConsolidado.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }

        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            exportExcel(Response, "ReporteAuditoriaConsolidado_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcel(HttpResponse Response, string filename)
        {

            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("Planeacion:");
            gridEncabezado.Columns.Add("Código:");
            gridEncabezado.Columns.Add("Tema:");
            gridEncabezado.Columns.Add("Realizadas:");
            gridEncabezado.Columns.Add("Programadas:");
            gridEncabezado.Columns.Add("Cumplimiento:");
            gridEncabezado.Columns.Add("Fecha inicio:");
            gridEncabezado.Columns.Add("Fecha cierre:");
            
            
            DataRow rowEncabezado;
            foreach (GridViewRow GridViewRow in GVreporteConsolidado.Rows)
            {
                rowEncabezado = gridEncabezado.NewRow();
                string strPlaneacion = ((Label)GridViewRow.FindControl("strPlaneacion")).Text;
                string intIdRegistro = ((Label)GridViewRow.FindControl("intIdRegistro")).Text;
                string strTema = ((Label)GridViewRow.FindControl("strTema")).Text;
                string strFechaInicio = ((Label)GridViewRow.FindControl("strFechaInicio")).Text;
                string strFechaCierre = ((Label)GridViewRow.FindControl("strFechaCierre")).Text;
                string dbRealizadas = ((Label)GridViewRow.FindControl("dbRealizadas")).Text;
                string dbProgramadas = ((Label)GridViewRow.FindControl("dbProgramadas")).Text;
                string dbCumplimiento = ((Label)GridViewRow.FindControl("dbCumplimiento")).Text;
                rowEncabezado["Planeacion:"] = strPlaneacion;
                rowEncabezado["Código:"] = intIdRegistro;
                rowEncabezado["Tema:"] = strTema;
                rowEncabezado["Realizadas:"] = dbRealizadas;
                rowEncabezado["Programadas:"] = dbProgramadas;
                rowEncabezado["Cumplimiento:"] = dbCumplimiento;
                rowEncabezado["Fecha inicio:"] = strFechaInicio;
                rowEncabezado["Fecha cierre:"] = strFechaCierre;
                gridEncabezado.Rows.Add(rowEncabezado);
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

        protected void ImButtonPDFexportAud_Click(object sender, ImageClickEventArgs e)
        {
            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 8, iTextSharp.text.Font.NORMAL, Color.BLACK);

            Document pdfDocument = new Document(PageSize.A4.Rotate(), 1, 1, 5, 20);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            //...definimos el autor del documento.
            pdfDocument.AddAuthor("Sherlock");
            //...el creador, que será el mismo eh!
            pdfDocument.AddCreator("Sherlock");
            //hacemos que se inserte la fecha de creación para el documento
            pdfDocument.AddCreationDate();
            //...título
            pdfDocument.AddTitle("Reporte de Auditoria Detallado");
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
            pdfcellImage.Border = Rectangle.NO_BORDER;
            pdfcellImage.Border = Rectangle.NO_BORDER;
            float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImage);
            PdfPCell pdfcellImageEmpresa = new PdfPCell(imagenEmpresa, true);
            pdfcellImageEmpresa.FixedHeight = 40f;
            pdfcellImageEmpresa.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.VerticalAlignment = Element.ALIGN_RIGHT;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            pdfcellImageEmpresa.Border = Rectangle.NO_BORDER;
            percentage = 40 / imagenEmpresa.Width;
            imagenEmpresa.ScalePercent(percentage * 100);
            pdftblImage.AddCell(pdfcellImageEmpresa);
            //Chunk chnCompany = new Chunk("Risk Consulting", _standardFont);
            Phrase phHeader = new Phrase();

            phHeader.Add(pdftblImage);
            //phHeader.Add(chnCompany);
            #region Tabla de Datos Principales

            PdfPTable pdfTableData = new PdfPTable(GVreporteAuditoria.HeaderRow.Cells.Count);
            int iteracionHeader = 0;
            foreach (TableCell headerCell in GVreporteAuditoria.HeaderRow.Cells)
            {
                /*if(iteracionHeader != 8)
                {*/
                Font font = new Font();
                font.Color = new Color(GVreporteAuditoria.HeaderStyle.ForeColor);
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                pdfCell.BackgroundColor = new Color(GVreporteAuditoria.HeaderStyle.BackColor);
                pdfTableData.AddCell(pdfCell);
                //}
                iteracionHeader++;
            }

            foreach (GridViewRow GridViewRow in GVreporteAuditoria.Rows)
            {
                string strNombreDependencia = ((Label)GridViewRow.FindControl("strNombreDependencia")).Text;
                string strNombreProces = ((Label)GridViewRow.FindControl("strNombreProces")).Text;
                string strTema = ((Label)GridViewRow.FindControl("strTema")).Text;
                string strObjetivo = ((Label)GridViewRow.FindControl("strObjetivo")).Text;
                string strEstado = ((Label)GridViewRow.FindControl("strEstado")).Text;
                string strGrupoAuditoria = ((Label)GridViewRow.FindControl("strGrupoAuditoria")).Text;
                string strEspecial = ((Label)GridViewRow.FindControl("strEspecial")).Text;
                string strPeriodicidad = ((Label)GridViewRow.FindControl("strPeriodicidad")).Text;
                string strMes = ((Label)GridViewRow.FindControl("strMes")).Text;
                string strMesExe = ((Label)GridViewRow.FindControl("strMesExe")).Text;
                string strSemanaExe = ((Label)GridViewRow.FindControl("strSemanaExe")).Text;
                string strFechaInicio = ((Label)GridViewRow.FindControl("strFechaInicio")).Text;
                string strFechaCierre = ((Label)GridViewRow.FindControl("strFechaCierre")).Text;
                
                int iteracion = 0;
                foreach (TableCell tableCell in GridViewRow.Cells)
                {

                    if (iteracion == 0)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteAuditoria.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strNombreDependencia));
                        pdfCell.BackgroundColor = new Color(GVreporteAuditoria.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 1)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteAuditoria.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strNombreProces));
                        pdfCell.BackgroundColor = new Color(GVreporteAuditoria.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 2)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteAuditoria.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strTema));
                        pdfCell.BackgroundColor = new Color(GVreporteAuditoria.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 3)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteAuditoria.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strObjetivo));
                        pdfCell.BackgroundColor = new Color(GVreporteAuditoria.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 4)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteAuditoria.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strEstado));
                        pdfCell.BackgroundColor = new Color(GVreporteAuditoria.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 5)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteAuditoria.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strGrupoAuditoria));
                        pdfCell.BackgroundColor = new Color(GVreporteAuditoria.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 6)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteAuditoria.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strEspecial));
                        pdfCell.BackgroundColor = new Color(GVreporteAuditoria.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 7)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteAuditoria.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strPeriodicidad));
                        pdfCell.BackgroundColor = new Color(GVreporteAuditoria.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 8)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteAuditoria.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strMes));
                        pdfCell.BackgroundColor = new Color(GVreporteAuditoria.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    
                    if (iteracion == 9)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteAuditoria.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strFechaInicio));
                        pdfCell.BackgroundColor = new Color(GVreporteAuditoria.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 10)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteAuditoria.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strFechaCierre));
                        pdfCell.BackgroundColor = new Color(GVreporteAuditoria.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 11)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteAuditoria.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strMesExe));
                        pdfCell.BackgroundColor = new Color(GVreporteAuditoria.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    if (iteracion == 12)
                    {
                        Font font = new Font();
                        font.Color = new Color(GVreporteAuditoria.RowStyle.ForeColor);
                        PdfPCell pdfCell = new PdfPCell(new Phrase(strSemanaExe));
                        pdfCell.BackgroundColor = new Color(GVreporteAuditoria.RowStyle.BackColor);
                        pdfTableData.AddCell(pdfCell);
                    }
                    iteracion++;
                }

            }

            #endregion

            HeaderFooter header = new HeaderFooter(phHeader, false);
            header.Border = Rectangle.NO_BORDER;
            header.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Header = header;
            pdfDocument.Open();

            /*float percentage = 0.0f;
            percentage = 80 / imagen.Width;
            imagen.ScalePercent(percentage * 100);*/
            //PdfPCell clImagen = new PdfPCell(imagen);
            //pdfDocument.Add(imagen);

            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(Chunk.NEWLINE);
            Paragraph Titulo = new Paragraph(new Phrase("Reporte de Auditoria Detallado"));
            Titulo.SetAlignment("Center");
            pdfDocument.Add(Titulo);
            pdfDocument.Add(new Phrase(""));
            pdfDocument.Add(pdfTableData);
            pdfDocument.Add(Chunk.NEWLINE);

            /*pdfDocument.Add(pdfpTableRiesgoControl);*/
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteAuditoriaDetallado.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }

        protected void ImButtonExcelexportAud_Click(object sender, ImageClickEventArgs e)
        {
            exportExcelDetallado(Response, "ReporteAuditoriaDetallado_" + System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        protected void exportExcelDetallado(HttpResponse Response, string filename)
        {

            DataTable gridEncabezado = new DataTable();
            gridEncabezado.Columns.Add("Nombre Dependencia:");
            gridEncabezado.Columns.Add("Nombre Proceso:");
            gridEncabezado.Columns.Add("Tema:");
            gridEncabezado.Columns.Add("Objetivo:");
            gridEncabezado.Columns.Add("Estado:");
            gridEncabezado.Columns.Add("Grupo Auditoria:");
            gridEncabezado.Columns.Add("Periodicidad:");
            gridEncabezado.Columns.Add("Mes:");
            gridEncabezado.Columns.Add("Mes Ejecución:");
            gridEncabezado.Columns.Add("Semana Ejecución:");
            gridEncabezado.Columns.Add("Fecha inicio:");
            gridEncabezado.Columns.Add("Fecha cierre:");


            DataRow rowEncabezado;
            foreach (GridViewRow GridViewRow in GVreporteAuditoria.Rows)
            {
                rowEncabezado = gridEncabezado.NewRow();
                string strNombreDependencia = ((Label)GridViewRow.FindControl("strNombreDependencia")).Text;
                string strNombreProces = ((Label)GridViewRow.FindControl("strNombreProces")).Text;
                string strTema = ((Label)GridViewRow.FindControl("strTema")).Text;
                string strObjetivo = ((Label)GridViewRow.FindControl("strObjetivo")).Text;
                string strEstado = ((Label)GridViewRow.FindControl("strEstado")).Text;
                string strGrupoAuditoria = ((Label)GridViewRow.FindControl("strGrupoAuditoria")).Text;
                string strPeriodicidad = ((Label)GridViewRow.FindControl("strPeriodicidad")).Text;
                string strMes = ((Label)GridViewRow.FindControl("strMes")).Text;
                string strMesExe = ((Label)GridViewRow.FindControl("strMesExe")).Text;
                string strSemanaExe = ((Label)GridViewRow.FindControl("strSemanaExe")).Text;
                string strFechaInicio = ((Label)GridViewRow.FindControl("strFechaInicio")).Text;
                string strFechaCierre = ((Label)GridViewRow.FindControl("strFechaCierre")).Text;
                rowEncabezado["Nombre Dependencia:"] = strNombreDependencia;
                rowEncabezado["Nombre Proceso:"] = strNombreProces;
                rowEncabezado["Tema:"] = strTema;
                rowEncabezado["Objetivo:"] = strObjetivo;
                rowEncabezado["Estado:"] = strEstado;
                rowEncabezado["Grupo Auditoria:"] = strGrupoAuditoria;
                rowEncabezado["Periodicidad:"] = strPeriodicidad;
                rowEncabezado["Mes:"] = strMes;
                rowEncabezado["Mes Ejecución:"] = strMesExe;
                rowEncabezado["Semana Ejecución:"] = strSemanaExe;
                rowEncabezado["Fecha inicio:"] = strFechaInicio;
                rowEncabezado["Fecha cierre:"] = strFechaCierre;
                gridEncabezado.Rows.Add(rowEncabezado);
            }
            //gridEncabezado.Rows.Add(rowEncabezado);


            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            workbook.Worksheets.Add(gridEncabezado, "AuditoriaDetallado");
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

        protected void ddlProceso_DataBound(object sender, EventArgs e)
        {
            ddlProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Seleccione---", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlGrupoAud2_DataBound(object sender, EventArgs e)
        {
            ddlGrupoAud2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Seleccione--", "0"));
        }

        protected void GVreporteConsolidado_PreRender(object sender, EventArgs e)
        {
            MergeRowsMatrizData(GVreporteConsolidado);
        }
        public void MergeRowsMatrizData(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    //string text = ((Label)row.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;

                    //string previousText = ((Label)previousRow.Cells[cellIndex].FindControl("DescripcionEntrada" + cellIndex)).Text;
                    if (cellIndex == 0)
                    {
                        string text = ((Label)row.FindControl("strPlaneacion")).Text;
                        string previousText = ((Label)previousRow.FindControl("strPlaneacion")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 1)
                    {
                        string text = ((Label)row.FindControl("intIdRegistro")).Text;
                        string previousText = ((Label)previousRow.FindControl("intIdRegistro")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 2)
                    {
                        string text = ((Label)row.FindControl("strTema")).Text;
                        string previousText = ((Label)previousRow.FindControl("strTema")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 3)
                    {
                        string text = ((Label)row.FindControl("dbRealizadas")).Text;
                        string previousText = ((Label)previousRow.FindControl("dbRealizadas")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 4)
                    {
                        string text = ((Label)row.FindControl("dbProgramadas")).Text;
                        string previousText = ((Label)previousRow.FindControl("dbProgramadas")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 5)
                    {
                        string text = ((Label)row.FindControl("dbCumplimiento")).Text;
                        string previousText = ((Label)previousRow.FindControl("dbCumplimiento")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 6)
                    {
                        string text = ((Label)row.FindControl("strFechaInicio")).Text;
                        string previousText = ((Label)previousRow.FindControl("strFechaInicio")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                    if (cellIndex == 7)
                    {
                        string text = ((Label)row.FindControl("strFechaCierre")).Text;
                        string previousText = ((Label)previousRow.FindControl("strFechaCierre")).Text;
                        if (text == previousText)
                        {
                            row.Cells[cellIndex].RowSpan = previousRow.Cells[cellIndex].RowSpan < 2 ? 2 : previousRow.Cells[cellIndex].RowSpan + 1;
                            previousRow.Cells[cellIndex].Visible = false;
                        }
                    }
                }
            }
        }

        protected void ddlPlaneacion_DataBound(object sender, EventArgs e)
        {
            ddlPlaneacion.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Seleccione---", "0")); // Inserta el Item con texto Vacio  
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            DbuttonsAud.Visible = false;
            dvGrid.Visible = false;
            form.Visible = false;
            dvFiltros.Visible = true;
            ddlMacroProceso.SelectedIndex = 0;
            ddlProceso.Items.Clear();
            ddlMesEjecucion.SelectedIndex = 0;
            ddlPeriodicidad.SelectedIndex = 0;
            ddlGrupoAud2.SelectedIndex = 0;
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            DbuttonConsolidado.Visible = false;
            dvGridConsolidado.Visible = false;
            ddlPlaneacion.SelectedIndex = 0;
            ddlRegistrosAud.Items.Clear();
        }

        protected void ImbSearchFiltro_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            if (!mtdLoadReporteConsolidado(ref strErrMsg))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        protected void ddlPlaneacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IdPlaneacion = ddlPlaneacion.SelectedValue;
            string strErrMsg = string.Empty;
            clsReportesAuditoriaDAL cReporte = new clsReportesAuditoriaDAL();
            DataTable dt = cReporte.mtdGetRegistrosAud(ref strErrMsg, IdPlaneacion);
            ddlRegistrosAud.DataSource = dt;
            ddlRegistrosAud.DataBind();
        }

        protected void ddlRegistrosAud_DataBound(object sender, EventArgs e)
        {
            ddlRegistrosAud.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Seleccione---", "0")); // Inserta el Item con texto Vacio  
        }
    }

}