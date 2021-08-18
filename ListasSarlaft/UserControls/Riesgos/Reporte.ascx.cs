using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ListasSarlaft.Classes;
using Excel = Microsoft.Office.Interop.Excel;
using ClosedXML;
using ClosedXML.Excel;
using System.IO;
using Microsoft.Security.Application;
using ListasSarlaft.Classes.BLL.Riesgos.CalculosRedColsa;

namespace ListasSarlaft.UserControls.Riesgos
{
    public partial class Reporte : System.Web.UI.UserControl
    {
        cRiesgo cRiesgo = new cRiesgo();
        cCuenta cCuenta = new cCuenta();
        cControl cControl = new cControl();
        String IdFormulario = "5017";

        #region Properties
        private DataTable infoGridReporteRiesgos;
        private DataTable InfoGridReporteRiesgos
        {
            get
            {
                infoGridReporteRiesgos = (DataTable)ViewState["infoGridReporteRiesgos"];
                return infoGridReporteRiesgos;
            }
            set
            {
                infoGridReporteRiesgos = value;
                ViewState["infoGridReporteRiesgos"] = infoGridReporteRiesgos;
            }
        }

        private int pagIndexInfoGridReporteRiesgos;
        private int PagIndexInfoGridReporteRiesgos
        {
            get
            {
                pagIndexInfoGridReporteRiesgos = (int)ViewState["pagIndexInfoGridReporteRiesgos"];
                return pagIndexInfoGridReporteRiesgos;
            }
            set
            {
                pagIndexInfoGridReporteRiesgos = value;
                ViewState["pagIndexInfoGridReporteRiesgos"] = pagIndexInfoGridReporteRiesgos;
            }
        }

        private DataTable infoGridReporteRiesgosControles;
        private DataTable InfoGridReporteRiesgosControles
        {
            get
            {
                infoGridReporteRiesgosControles = (DataTable)ViewState["infoGridReporteRiesgosControles"];
                return infoGridReporteRiesgosControles;
            }
            set
            {
                infoGridReporteRiesgosControles = value;
                ViewState["infoGridReporteRiesgosControles"] = infoGridReporteRiesgosControles;
            }
        }

        private int pagIndexInfoGridReporteRiesgosControles;
        private int PagIndexInfoGridReporteRiesgosControles
        {
            get
            {
                pagIndexInfoGridReporteRiesgosControles = (int)ViewState["pagIndexInfoGridReporteRiesgosControles"];
                return pagIndexInfoGridReporteRiesgosControles;
            }
            set
            {
                pagIndexInfoGridReporteRiesgosControles = value;
                ViewState["pagIndexInfoGridReporteRiesgosControles"] = pagIndexInfoGridReporteRiesgosControles;
            }
        }
        private DataTable infoGridReporteRiesgosControlesConsolidado;
        private DataTable InfoGridReporteRiesgosControlesConsolidado
        {
            get
            {
                infoGridReporteRiesgosControlesConsolidado = (DataTable)ViewState["infoGridReporteRiesgosControlesConsolidado"];
                return infoGridReporteRiesgosControlesConsolidado;
            }
            set
            {
                infoGridReporteRiesgosControlesConsolidado = value;
                ViewState["infoGridReporteRiesgosControlesConsolidado"] = infoGridReporteRiesgosControlesConsolidado;
            }
        }

        private int pagIndexInfoGridReporteRiesgosControlesConsolidado;
        private int PagIndexInfoGridReporteRiesgosControlesConsolidado
        {
            get
            {
                pagIndexInfoGridReporteRiesgosControlesConsolidado = (int)ViewState["pagIndexInfoGridReporteRiesgosControlesConsolidado"];
                return pagIndexInfoGridReporteRiesgosControlesConsolidado;
            }
            set
            {
                pagIndexInfoGridReporteRiesgosControlesConsolidado = value;
                ViewState["pagIndexInfoGridReporteRiesgosControlesConsolidado"] = pagIndexInfoGridReporteRiesgosControlesConsolidado;
            }
        }

        private DataTable infoGridReporteRiesgosEventos;
        private DataTable InfoGridReporteRiesgosEventos
        {
            get
            {
                infoGridReporteRiesgosEventos = (DataTable)ViewState["infoGridReporteRiesgosEventos"];
                return infoGridReporteRiesgosEventos;
            }
            set
            {
                infoGridReporteRiesgosEventos = value;
                ViewState["infoGridReporteRiesgosEventos"] = infoGridReporteRiesgosEventos;
            }
        }

        private int pagIndexInfoGridReporteRiesgosEventos;
        private int PagIndexInfoGridReporteRiesgosEventos
        {
            get
            {
                pagIndexInfoGridReporteRiesgosEventos = (int)ViewState["pagIndexInfoGridReporteRiesgosEventos"];
                return pagIndexInfoGridReporteRiesgosEventos;
            }
            set
            {
                pagIndexInfoGridReporteRiesgosEventos = value;
                ViewState["pagIndexInfoGridReporteRiesgosEventos"] = pagIndexInfoGridReporteRiesgosEventos;
            }
        }

        private DataTable infoGridReporteRiesgosPlanesAccion;
        private DataTable InfoGridReporteRiesgosPlanesAccion
        {
            get
            {
                infoGridReporteRiesgosPlanesAccion = (DataTable)ViewState["infoGridReporteRiesgosPlanesAccion"];
                return infoGridReporteRiesgosPlanesAccion;
            }
            set
            {
                infoGridReporteRiesgosPlanesAccion = value;
                ViewState["infoGridReporteRiesgosPlanesAccion"] = infoGridReporteRiesgosPlanesAccion;
            }
        }

        private int pagIndexInfoGridReporteRiesgosPlanesAccion;
        private int PagIndexInfoGridReporteRiesgosPlanesAccion
        {
            get
            {
                pagIndexInfoGridReporteRiesgosPlanesAccion = (int)ViewState["pagIndexInfoGridReporteRiesgosPlanesAccion"];
                return pagIndexInfoGridReporteRiesgosPlanesAccion;
            }
            set
            {
                pagIndexInfoGridReporteRiesgosPlanesAccion = value;
                ViewState["pagIndexInfoGridReporteRiesgosPlanesAccion"] = pagIndexInfoGridReporteRiesgosPlanesAccion;
            }
        }


        private int pagIndexInfoGridReporteControles;
        private int PagIndexInfoGridReporteControles
        {
            get
            {
                pagIndexInfoGridReporteControles = (int)ViewState["pagIndexInfoGridReporteControles"];
                return pagIndexInfoGridReporteControles;
            }
            set
            {
                pagIndexInfoGridReporteControles = value;
                ViewState["pagIndexInfoGridReporteControles"] = pagIndexInfoGridReporteControles;
            }
        }

        private DataTable infoGridReporteControles;
        private DataTable InfoGridReporteControles
        {
            get
            {
                infoGridReporteControles = (DataTable)ViewState["infoGridReporteControles"];
                return infoGridReporteControles;
            }
            set
            {
                infoGridReporteControles = value;
                ViewState["infoGridReporteControles"] = infoGridReporteControles;
            }
        }

        #region Reportes de Modificacion de Controles y Riesgos
        private DataTable dtInfoGridReporteModControl;
        private DataTable DtInfoGridReporteModControl
        {
            get
            {
                dtInfoGridReporteModControl = (DataTable)ViewState["dtInfoGridReporteModControl"];
                return dtInfoGridReporteModControl;
            }
            set
            {
                dtInfoGridReporteModControl = value;
                ViewState["dtInfoGridReporteModControl"] = dtInfoGridReporteModControl;
            }
        }

        private DataTable dtInfoGridReporteModRiesgo;
        private DataTable DtInfoGridReporteModRiesgo
        {
            get
            {
                dtInfoGridReporteModRiesgo = (DataTable)ViewState["dtInfoGridReporteModRiesgo"];
                return dtInfoGridReporteModRiesgo;
            }
            set
            {
                dtInfoGridReporteModRiesgo = value;
                ViewState["dtInfoGridReporteModRiesgo"] = dtInfoGridReporteModRiesgo;
            }
        }

        private int pagIndexInfoGridReporteModControl;
        private int PagIndexInfoGridReporteModControl
        {
            get
            {
                pagIndexInfoGridReporteModControl = (int)ViewState["pagIndexInfoGridReporteModControl"];
                return pagIndexInfoGridReporteModControl;
            }
            set
            {
                pagIndexInfoGridReporteModControl = value;
                ViewState["pagIndexInfoGridReporteModControl"] = pagIndexInfoGridReporteModControl;
            }
        }

        private int pagIndexInfoGridReporteModRiesgo;
        private int PagIndexInfoGridReporteModRiesgo
        {
            get
            {
                pagIndexInfoGridReporteModRiesgo = (int)ViewState["pagIndexInfoGridReporteModRiesgo"];
                return pagIndexInfoGridReporteModRiesgo;
            }
            set
            {
                pagIndexInfoGridReporteModRiesgo = value;
                ViewState["pagIndexInfoGridReporteModRiesgo"] = pagIndexInfoGridReporteModRiesgo;
            }
        }
        #endregion Reportes de Modificacion de Controles y Riesgos
        #region Reporte Causas sin Control
        private DataTable infoGridReporteCausaSinControl;
        private DataTable InfoGridReporteCausaSinControl
        {
            get
            {
                infoGridReporteCausaSinControl = (DataTable)ViewState["infoGridReporteCausaSinControl"];
                return infoGridReporteCausaSinControl;
            }
            set
            {
                infoGridReporteCausaSinControl = value;
                ViewState["infoGridReporteCausaSinControl"] = infoGridReporteCausaSinControl;
            }
        }
        private int rowGridReporteCausaSinControl;
        private int RowGridReporteCausaSinControl
        {
            get
            {
                rowGridReporteCausaSinControl = (int)ViewState["rowGridReporteCausaSinControl"];
                return rowGridReporteCausaSinControl;
            }
            set
            {
                rowGridReporteCausaSinControl = value;
                ViewState["rowGridReporteCausaSinControl"] = rowGridReporteCausaSinControl;
            }
        }
        private int pagIndexReporteCausaSinControl;
        private int PagIndexReporteCausaSinControl
        {
            get
            {
                pagIndexReporteCausaSinControl = (int)ViewState["pagIndexReporteCausaSinControl"];
                return pagIndexReporteCausaSinControl;
            }
            set
            {
                pagIndexReporteCausaSinControl = value;
                ViewState["pagIndexReporteCausaSinControl"] = pagIndexReporteCausaSinControl;
            }
        }
        #endregion Reporte Causas sin Control
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
            {
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            }
            if (!Page.IsPostBack)
            {
                loadDDLCadenaValor();
                loadDDLClasificacion();
                mtdLoadAreas();
                inicializarValores();
            }
        }

        private void inicializarValores()
        {
            PagIndexInfoGridReporteRiesgos = 0;
            PagIndexInfoGridReporteRiesgosControles = 0;
            PagIndexInfoGridReporteRiesgosEventos = 0;
            PagIndexInfoGridReporteRiesgosPlanesAccion = 0;
            PagIndexInfoGridReporteModControl = 0;
            PagIndexInfoGridReporteModRiesgo = 0;
            PagIndexInfoGridReporteControles = 0;
            PagIndexReporteCausaSinControl = 0;
            PagIndexInfoGridReporteRiesgosControlesConsolidado = 0;
        }

        #region Loads
        private void loadDDLCadenaValor()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLCadenaValor();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList52.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCadenaValor"].ToString().Trim(), dtInfo.Rows[i]["IdCadenaValor"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar cadena valor. " + ex.Message);
            }
        }
        private void mtdLoadAreas()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLAreas();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DDLareas.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreArea"].ToString().Trim(), dtInfo.Rows[i]["IdArea"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar areas. " + ex.Message);
            }
        }
        private void loadDDLClasificacion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClasificacion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList56.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionRiesgo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar clasificación riesgo. " + ex.Message);
            }
        }

        private void loadDDLMacroproceso(String IdCadenaValor, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLMacroproceso(IdCadenaValor);
                switch (Tipo)
                {
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList53.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar macroproceso. " + ex.Message);
            }
        }

        private void loadDDLProceso(String IdMacroproceso, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLProceso(IdMacroproceso);
                switch (Tipo)
                {
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList54.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProceso"].ToString().Trim(), dtInfo.Rows[i]["IdProceso"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar proceso. " + ex.Message);
            }
        }

        private void loadDDLClasificacionGeneral(String IdClasificacionRiesgo, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClasificacionGeneral(IdClasificacionRiesgo);
                switch (Tipo)
                {
                    case 2:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList57.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreClasificacionGeneralRiesgo"].ToString().Trim(), dtInfo.Rows[i]["IdClasificacionGeneralRiesgo"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar clasificación general. " + ex.Message);
            }
        }
        #endregion Loads

        #region DDL
        protected void DropDownList52_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList53.Items.Clear();
            DropDownList53.Items.Insert(0, new ListItem("---", "---"));
            DropDownList54.Items.Clear();
            DropDownList54.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList52.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLMacroproceso(DropDownList52.SelectedValue.ToString().Trim(), 2);
            }
        }

        protected void DropDownList53_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList54.Items.Clear();
            DropDownList54.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList53.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLProceso(DropDownList53.SelectedValue.ToString().Trim(), 2);
            }
        }

        protected void DropDownList56_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList57.Items.Clear();
            DropDownList57.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList56.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLClasificacionGeneral(DropDownList56.SelectedValue.ToString().Trim(), 2);
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue == "5" || DropDownList1.SelectedValue == "6")
            {
                DropDownList2.Enabled = false;
                DropDownList3.Enabled = false;
                DropDownList4.Enabled = false;
                DropDownList52.Enabled = false;
                DropDownList53.Enabled = false;
                DropDownList54.Enabled = false;
                DropDownList56.Enabled = false;
                DropDownList57.Enabled = false;
                DDLareas.Enabled = false;
                TxtFechaIni.Enabled = true;
                TxtFechaFin.Enabled = true;
            }
            else
            {
                if (DropDownList1.SelectedValue == "7")
                {
                    DropDownList2.Enabled = false;
                    DropDownList3.Enabled = false;
                    DropDownList4.Enabled = false;
                    DropDownList52.Enabled = false;
                    DropDownList53.Enabled = false;
                    DropDownList54.Enabled = false;
                    DropDownList56.Enabled = false;
                    DropDownList57.Enabled = false;
                    DDLareas.Enabled = false;
                    TxtFechaIni.Enabled = false;
                    TxtFechaFin.Enabled = false;
                }
                else
                {
                    DropDownList2.Enabled = true;
                    DropDownList3.Enabled = true;
                    DropDownList4.Enabled = true;
                    DropDownList52.Enabled = true;
                    DropDownList53.Enabled = true;
                    DropDownList54.Enabled = true;
                    DropDownList56.Enabled = true;
                    DropDownList57.Enabled = true;
                    DDLareas.Enabled = true;
                    TxtFechaIni.Enabled = false;
                    TxtFechaFin.Enabled = false;
                }
            }
        }
        #endregion DDL

        #region Buttons
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                inicializarValores();
                switch (DropDownList1.SelectedValue.ToString().Trim())
                {
                    case "1":
                        loadGridReporteRiesgos();
                        loadInfoReporteRiesgos();
                        resetValuesConsulta();
                        ReporteRiesgos.Visible = true;
                        break;
                    case "2":
                        loadGridReporteRiesgosControles();
                        loadInfoReporteRiesgosControles();
                        resetValuesConsulta();
                        ReporteRiesgosControles.Visible = true;
                        break;
                    case "3":
                        loadGridReporteRiesgosEventos();
                        loadInfoReporteRiesgosEventos();
                        resetValuesConsulta();
                        ReporteRiesgosEventos.Visible = true;
                        break;
                    case "4":
                        loadGridReporteRiesgosPlanesAccion();
                        loadInfoReporteRiesgosPlanesAccion();
                        resetValuesConsulta();
                        ReporteRiesgosPlanesAccion.Visible = true;
                        break;
                    case "5":
                        mtdCargarGridReporteModControl();
                        mtdCargarInfoReporteModControl();
                        resetValuesConsulta();
                        ReporteModControles.Visible = true;
                        break;
                    case "6":
                        mtdCargarGridReporteModRiesgo();
                        mtdCargarInfoReporteModRiesgo();
                        resetValuesConsulta();
                        ReporteModRiesgos.Visible = true;
                        break;
                    case "7":
                        mtdCargarGridReporteControles();
                        mtdCargarInfoReporteControles();
                        resetValuesConsulta();
                        ReporteControles.Visible = true;
                        break;
                    case "8":
                        mtdLoadGridReporteCausassinControles();
                        mtdLoadInfoGridReporteCausassinControles();
                        resetValuesConsulta();
                        ReporteCausasSinControl.Visible = true;
                        break;
                    case "9":
                        resetValuesConsulta();
                        string strErrMsg = String.Empty;
                        mtdLoadCuadroComparativo(ref strErrMsg);
                        //if (!)
                        //omb.ShowMessage(strErrMsg, 2, "Atención");
                        ReportePerfilRiesgos.Visible = true;
                        break;
                    case "10":
                        resetValuesConsulta();
                        strErrMsg = String.Empty;
                        mtdLoadRepoRiesgosControl(ref strErrMsg);
                        //if (!)
                        //omb.ShowMessage(strErrMsg, 2, "Atención");
                        ReporteRiesgoControl.Visible = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al realizar la busqueda. " + ex.Message);
            }
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            resetValuesConsulta();
            loadGridReporteRiesgos();
            loadGridReporteRiesgosControles();
            loadGridReporteRiesgosEventos();
            loadGridReporteRiesgosPlanesAccion();
            inicializarValores();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReporteRiesgos, Response, "Reporte Riesgos", 1);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReporteRiesgosControles, Response, "Reporte Riesgos vs controles", 2);
            //InfoGridReporteControles.ExportToExcel(ExcelFilePath);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                exportExcel(InfoGridReporteRiesgosEventos, Response, "Reporte Riesgos vs eventos", 3);
            }
            catch (Exception ex)
            {
                Mensaje("Error al exportar Reporte Riesgos vs eventos." + ex.Message);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReporteRiesgosPlanesAccion, Response, "Reporte Riesgos vs planes de acción", 4);
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReporteControles, Response, "Reporte Controles", 5);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            exportExcel(DtInfoGridReporteModControl, Response, "Reporte Modificacion de Controles", 6);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            exportExcel(DtInfoGridReporteModRiesgo, Response, "Reporte Modificacion de Riesgo", 7);
        }
        #endregion Buttons

        #region GridView
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteRiesgos = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridReporteRiesgos;
            GridView1.DataSource = InfoGridReporteRiesgos;
            GridView1.DataBind();
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteRiesgosControles = e.NewPageIndex;
            GridView2.PageIndex = PagIndexInfoGridReporteRiesgosControles;
            GridView2.DataSource = InfoGridReporteRiesgosControles;
            GridView2.DataBind();
        }

        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteRiesgosEventos = e.NewPageIndex;
            GridView3.PageIndex = PagIndexInfoGridReporteRiesgosEventos;
            GridView3.DataSource = InfoGridReporteRiesgosEventos;
            GridView3.DataBind();
        }

        protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteRiesgosPlanesAccion = e.NewPageIndex;
            GridView4.PageIndex = PagIndexInfoGridReporteRiesgosPlanesAccion;
            GridView4.DataSource = InfoGridReporteRiesgosPlanesAccion;
            GridView4.DataBind();
        }

        protected void GridView7_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteControles = e.NewPageIndex;
            GridView7.PageIndex = PagIndexInfoGridReporteControles;
            GridView7.DataSource = InfoGridReporteControles;
            GridView7.DataBind();
        }

        protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteModControl = e.NewPageIndex;
            GridView5.PageIndex = PagIndexInfoGridReporteModControl;
            GridView5.DataSource = DtInfoGridReporteModControl;
            GridView5.DataBind();
            /*mtdCargarGridReporteModControl();
            mtdCargarInfoReporteModControl();*/

        }

        protected void GridView6_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridReporteModRiesgo = e.NewPageIndex;
            GridView6.PageIndex = PagIndexInfoGridReporteModRiesgo;
            GridView6.DataSource = DtInfoGridReporteModRiesgo;
            GridView6.DataBind();
            /*mtdCargarGridReporteModRiesgo();
            mtdCargarInfoReporteModRiesgo();*/

        }
        #endregion GridView

        private String causas(String Causas)
        {
            DataTable dtInfoCausas = new DataTable();
            dtInfoCausas = cRiesgo.causas(Causas);
            Causas = "";
            for (int ca = 0; ca < dtInfoCausas.Rows.Count; ca++)
            {
                Causas += dtInfoCausas.Rows[ca]["NombreCausas"].ToString().Trim() + ". ";
            }
            return Causas;
        }

        private String consecuencias(String Consecuencias)
        {
            DataTable dtInfoConsecuencias = new DataTable();
            dtInfoConsecuencias = cRiesgo.consecuencias(Consecuencias);
            Consecuencias = "";
            for (int con = 0; con < dtInfoConsecuencias.Rows.Count; con++)
            {
                Consecuencias += dtInfoConsecuencias.Rows[con]["NombreConsecuencia"].ToString().Trim() + ". ";
            }
            return Consecuencias;
        }
        public static XLWorkbook mtdCreateXbookRiesgos(DataTable dt)
        {
            XLWorkbook workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Riesgos");
            //ws.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.Red;
            //***********FORMATO DE TABLA************************/
            ws.Range("A1:AP1").Style.Border.SetBottomBorder(XLBorderStyleValues.Dotted);
            ws.Range("A1:AP1").Style.Border.SetInsideBorder(XLBorderStyleValues.Dotted);
            ws.Range("A1:AP1").Style.Border.SetLeftBorder(XLBorderStyleValues.Dotted);
            ws.Range("A1:AP1").Style.Border.SetRightBorder(XLBorderStyleValues.Dotted);
            ws.Range("A1:AP1").Style.Border.SetOutsideBorder(XLBorderStyleValues.Dotted);
            ws.Range("A1:AP1").Style
             .Font.SetFontSize(12)
             .Font.SetBold(true)
             .Font.SetFontColor(XLColor.White)
             .Fill.SetBackgroundColor(XLColor.DarkBlue);
            int indexRow = 2;
            ws.Cell("A1").Value = "CodigoRiesgo";
            ws.Cell("B" + 1).Value = "CadenaValor";
            ws.Cell("C" + 1).Value = "Macroproceso";
            ws.Cell("D" + 1).Value = "Proceso";
            ws.Cell("E" + 1).Value = "Subproceso";
            ws.Cell("F" + 1).Value = "NombreRiesgo";
            ws.Cell("G" + 1).Value = "Riesgo de proceso";
            ws.Cell("H" + 1).Value = "Causas";
            ws.Cell("I" + 1).Value = "Objetivo estratégico asociado";
            ws.Cell("J" + 1).Value = "Sistema SAR";
            ws.Cell("K" + 1).Value = "Tipo de Riesgo";
            ws.Cell("L" + 1).Value = "Factor de Riesgo";
            ws.Cell("M" + 1).Value = "Parte Interesada que Genera el Riesgo";
            ws.Cell("N" + 1).Value = "Parte Interesada Afectada por el Riesgo";
            ws.Cell("O" + 1).Value = "FrecuenciaInherente";
            ws.Cell("P" + 1).Value = "CodigoFrecuenciaInherente";
            ws.Cell("Q" + 1).Value = "ImpactoInherente";
            ws.Cell("R" + 1).Value = "CodigoImpactoInherente";
            ws.Cell("S" + 1).Value = "RiesgoInherente";
            ws.Cell("T" + 1).Value = "CodigoRiesgoInherente";
            ws.Cell("U" + 1).Value = "Indicador SAR Inherente";
            ws.Cell("V" + 1).Value = "FrecuenciaResidual";
            ws.Cell("W" + 1).Value = "CodigoFrecuenciaResidual";
            ws.Cell("X" + 1).Value = "ImpactoResidual";
            ws.Cell("Y" + 1).Value = "CodigoImpactoResidual";
            ws.Cell("Z" + 1).Value = "RiesgoResidual";
            ws.Cell("AA" + 1).Value = "CodigoRiesgoResidual";
            ws.Cell("AB" + 1).Value = "Indicador SAR Residual";
            ws.Cell("AC" + 1).Value = "ResponsableRiesgo";
            ws.Cell("AD" + 1).Value = "FechaRegistroRiesgo";
            ws.Cell("AE" + 1).Value = "Ubicacion";
            ws.Cell("AF" + 1).Value = "FactorRiesgoOperativo";
            ws.Cell("AG" + 1).Value = "SubFactorRiesgoOperativo";
            ws.Cell("AH" + 1).Value = "ListaRiesgoAsociadoLA";
            ws.Cell("AI" + 1).Value = "ListaFactorRiesgoLAFT";
            ws.Cell("AJ" + 1).Value = "TipoEvento";
            ws.Cell("AK" + 1).Value = "RiesgoAsociadoOperativo";
            ws.Cell("AL" + 1).Value = "Consecuencias";
            ws.Cell("AM" + 1).Value = "Actividad";
            ws.Cell("AN" + 1).Value = "ListaTratamiento";
            ws.Cell("AO" + 1).Value = "NombreArea";
            ws.Cell("AP" + 1).Value = "Usuario";
            foreach (DataRow row in dt.Rows)
            {
                ws.Cell("A" + indexRow).Value = row["CodigoRiesgo"].ToString();
                ws.Cell("B" + indexRow).Value = row["CadenaValor"].ToString();
                ws.Cell("C" + indexRow).Value = row["Macroproceso"].ToString();
                ws.Cell("D" + indexRow).Value = row["Proceso"].ToString();
                ws.Cell("E" + indexRow).Value = row["Subproceso"].ToString();
                ws.Cell("F" + indexRow).Value = row["NombreRiesgo"].ToString();
                ws.Cell("G" + indexRow).Value = row["DescripcionRiesgo"].ToString();
                ws.Cell("H" + indexRow).Value = row["Causas"].ToString();
                ws.Cell("I" + indexRow).Value = row["Objetivo estratégico asociado"].ToString();
                ws.Cell("J" + indexRow).Value = row["ClasificacionRiesgo"].ToString();
                ws.Cell("K" + indexRow).Value = row["ClasificacionGeneralRiesgo"].ToString();
                ws.Cell("L" + indexRow).Value = row["ClasificacionParticularRiesgo"].ToString();
                ws.Cell("M" + indexRow).Value = row["Parte Interesada que Genera el Riesgo"].ToString();
                ws.Cell("N" + indexRow).Value = row["Parte Interesada Afectada por el Riesgo"].ToString();
                ws.Cell("O" + indexRow).Value = row["FrecuenciaInherente"].ToString();
                ws.Cell("P" + indexRow).Value = row["CodigoFrecuenciaInherente"].ToString();
                ws.Cell("Q" + indexRow).Value = row["ImpactoInherente"].ToString();
                ws.Cell("R" + indexRow).Value = row["CodigoImpactoInherente"].ToString();
                ws.Cell("S" + indexRow).Value = row["RiesgoInherente"].ToString();
                if (row["RiesgoInherente"].ToString() == "Bajo")
                {
                    ws.Cell(indexRow, 19).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Green;
                }
                if (row["RiesgoInherente"].ToString() == "Moderado")
                {
                    ws.Cell(indexRow, 19).Style.Fill.BackgroundColor = XLColor.Yellow;
                }
                if (row["RiesgoInherente"].ToString() == "Alto")
                {
                    ws.Cell(indexRow, 19).Style.Fill.BackgroundColor = XLColor.Orange;
                }
                if (row["RiesgoInherente"].ToString() == "Extremo")
                {
                    ws.Cell(indexRow, 19).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Red;
                }
                ws.Cell("T" + indexRow).Value = row["CodigoRiesgoInherente"].ToString();
                ws.Cell("U" + indexRow).Value = row["CodigoFrecuenciaInherente"].ToString() + "," + row["CodigoImpactoInherente"].ToString();
                if (row["RiesgoInherente"].ToString() == "Bajo")
                {
                    ws.Cell(indexRow, 21).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Green;
                }
                if (row["RiesgoInherente"].ToString() == "Moderado")
                {
                    ws.Cell(indexRow, 21).Style.Fill.BackgroundColor = XLColor.Yellow;
                }
                if (row["RiesgoInherente"].ToString() == "Alto")
                {
                    ws.Cell(indexRow, 21).Style.Fill.BackgroundColor = XLColor.Orange;
                }
                if (row["RiesgoInherente"].ToString() == "Extremo")
                {
                    ws.Cell(indexRow, 21).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Red;
                }
                ws.Cell("V" + indexRow).Value = row["FrecuenciaResidual"].ToString();
                ws.Cell("W" + indexRow).Value = row["CodigoFrecuenciaResidual"].ToString();
                ws.Cell("X" + indexRow).Value = row["ImpactoResidual"].ToString();
                ws.Cell("Y" + indexRow).Value = row["CodigoImpactoResidual"].ToString();
                ws.Cell("Z" + indexRow).Value = row["RiesgoResidual"].ToString();
                if (row["RiesgoResidual"].ToString() == "Bajo")
                {
                    ws.Cell(indexRow, 26).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Green;
                }
                if (row["RiesgoResidual"].ToString() == "Moderado")
                {
                    ws.Cell(indexRow, 26).Style.Fill.BackgroundColor = XLColor.Yellow;
                }
                if (row["RiesgoResidual"].ToString() == "Alto")
                {
                    ws.Cell(indexRow, 26).Style.Fill.BackgroundColor = XLColor.Orange;
                }
                if (row["RiesgoResidual"].ToString() == "Extremo")
                {
                    ws.Cell(indexRow, 26).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Red;
                }
                ws.Cell("AA" + indexRow).Value = row["CodigoRiesgoResidual"].ToString();
                ws.Cell("AB" + indexRow).Value = row["CodigoFrecuenciaResidual"].ToString() + "," + row["CodigoImpactoResidual"].ToString();
                if (row["RiesgoResidual"].ToString() == "Bajo")
                {
                    ws.Cell(indexRow, 28).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Green;
                }
                if (row["RiesgoResidual"].ToString() == "Moderado")
                {
                    ws.Cell(indexRow, 28).Style.Fill.BackgroundColor = XLColor.Yellow;
                }
                if (row["RiesgoResidual"].ToString() == "Alto")
                {
                    ws.Cell(indexRow, 28).Style.Fill.BackgroundColor = XLColor.Orange;
                }
                if (row["RiesgoResidual"].ToString() == "Extremo")
                {
                    ws.Cell(indexRow, 28).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Red;
                }
                ws.Cell("AC" + indexRow).Value = row["ResponsableRiesgo"].ToString();
                ws.Cell("AD" + indexRow).Value = row["FechaRegistroRiesgo"].ToString();
                ws.Cell("AE" + indexRow).Value = row["Ubicacion"].ToString();
                ws.Cell("AF" + indexRow).Value = row["FactorRiesgoOperativo"].ToString();
                ws.Cell("AG" + indexRow).Value = row["SubFactorRiesgoOperativo"].ToString();
                ws.Cell("AH" + indexRow).Value = row["ListaRiesgoAsociadoLA"].ToString();
                ws.Cell("AI" + indexRow).Value = row["ListaFactorRiesgoLAFT"].ToString();
                ws.Cell("AJ" + indexRow).Value = row["TipoEvento"].ToString();
                ws.Cell("AK" + indexRow).Value = row["RiesgoAsociadoOperativo"].ToString();
                ws.Cell("AL" + indexRow).Value = row["Consecuencias"].ToString();
                ws.Cell("AM" + indexRow).Value = row["Actividad"].ToString();
                ws.Cell("AN" + indexRow).Value = row["ListaTratamiento"].ToString();
                ws.Cell("AO" + indexRow).Value = row["NombreArea"].ToString();
                ws.Cell("AP" + indexRow).Value = row["Usuario"].ToString();
                indexRow++;
            }
            return workbook;
        }
        public static XLWorkbook mtdCreateXbookRiesgosControles(DataTable dt, ref string strErrMsg)
        {
            XLWorkbook workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("RiesgosControles");
            //ws.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.Red;
            //***********FORMATO DE TABLA************************/
            ws.Range("A1:AK1").Style.Border.SetBottomBorder(XLBorderStyleValues.Dotted);
            ws.Range("A1:AK1").Style.Border.SetInsideBorder(XLBorderStyleValues.Dotted);
            ws.Range("A1:AK1").Style.Border.SetLeftBorder(XLBorderStyleValues.Dotted);
            ws.Range("A1:AK1").Style.Border.SetRightBorder(XLBorderStyleValues.Dotted);
            ws.Range("A1:AK1").Style.Border.SetOutsideBorder(XLBorderStyleValues.Dotted);
            ws.Range("A1:AK1").Style
             .Font.SetFontSize(12)
             .Font.SetBold(true)
             .Font.SetFontColor(XLColor.White)
             .Fill.SetBackgroundColor(XLColor.DarkBlue);
            int indexRow = 2;
            ws.Cell("A" + 1).Value = "CodigoRiesgo";
            ws.Cell("B" + 1).Value = "Tipo de Proceso";
            ws.Cell("C" + 1).Value = "Macroproceso";
            ws.Cell("D" + 1).Value = "Proceso";
            ws.Cell("E" + 1).Value = "Subproceso";
            ws.Cell("F" + 1).Value = "Riesgo de proceso";
            //ws.Cell("G" + 1).Value =  "NombreRiesgo";
            ws.Cell("G" + 1).Value = "Causas";
            ws.Cell("H" + 1).Value = "Objetivo estratégico asociado";
            ws.Cell("I" + 1).Value = "Sistema SAR";
            ws.Cell("J" + 1).Value = "Tipo de Riesgo";
            ws.Cell("K" + 1).Value = "Factor de Riesgo";
            ws.Cell("L" + 1).Value = "Parte Interesada que Genera el Riesgo";
            ws.Cell("M" + 1).Value = "Parte Interesada Afectada por el Riesgo";
            /*ws.Cell("O" + 1).Value = "ResponsableRiesgo";
            ws.Cell("P" + 1).Value = "FechaRegistroRiesgo";
            ws.Cell("Q" + 1).Value = "Ubicacion";
            ws.Cell("R" + 1).Value = "FactorRiesgoOperativo";
            ws.Cell("S" + 1).Value = "SubFactorRiesgoOperativo";
            ws.Cell("T" + 1).Value = "ListaRiesgoAsociadoLA";
            ws.Cell("U" + 1).Value = "ListaFactorRiesgoLAFT";
            ws.Cell("V" + 1).Value = "TipoEvento";*/
            //ws.Cell("N" + 1).Value = "RiesgoAsociadoOperativo";
            ws.Cell("N" + 1).Value = "FrecuenciaInherente";
            //ws.Cell("X" + 1).Value = "CodigoFrecuenciaInherente";
            ws.Cell("O" + 1).Value = "ImpactoInherente";
            //ws.Cell("Z" + 1).Value = "CodigoImpactoInherente";
            ws.Cell("P" + 1).Value = "RiesgoInherente";
            ws.Cell("Q" + 1).Value = "Indicador SAR Inherente";
            ws.Cell("R" + 1).Value = "Indicador SAR Inherente X";
            /*ws.Cell("R" + 1).Value = "Consecuencias";
            ws.Cell("S" + 1).Value = "Actividad";
            ws.Cell("T" + 1).Value = "ListaTratamiento";*/
            ws.Cell("S" + 1).Value = "CodigoControl";
            ws.Cell("T" + 1).Value = "NombreControl";
            ws.Cell("U" + 1).Value = "DescripcionControl";
            ws.Cell("V" + 1).Value = "ObjetivoControl";
            ws.Cell("W" + 1).Value = "Procedimiento Asociado";
            //ws.Cell("AQ" + 1).Value = "CausasAsociacion";
            ws.Cell("X" + 1).Value = "ResponsableControlEjecucion";
            ws.Cell("Y" + 1).Value = "ResponsableControlCalificacion";
            //ws.Cell("AT" + 1).Value = "FechaRegistroControl";
            ws.Cell("Z" + 1).Value = "Periodicidad Control";
            //ws.Cell("AV" + 1).Value = "NombreTest";
            ws.Cell("AA" + 1).Value = "Clase Control";
            ws.Cell("AB" + 1).Value = "Tipo Control";
            //ws.Cell("AY" + 1).Value = "Variable3";
            ws.Cell("AC" + 1).Value = "Documentacion";
            ws.Cell("AD" + 1).Value = "Efectividad";
            ws.Cell("AE" + 1).Value = "Operatividad";
            /*ws.Cell("BA" + 1).Value = "Variable5";
            
            ws.Cell("BD" + 1).Value = "Variable8";
            ws.Cell("BE" + 1).Value = "Variable9";
            ws.Cell("BF" + 1).Value = "Variable10";
            ws.Cell("BG" + 1).Value = "Variable11";
            ws.Cell("BH" + 1).Value = "Variable12";
            ws.Cell("BI" + 1).Value = "Variable13";
            ws.Cell("BJ" + 1).Value = "Variable14";
            ws.Cell("BK" + 1).Value = "Variable15";*/
            ws.Cell("AF" + 1).Value = "Solidez";
            ws.Cell("AG" + 1).Value = "FrecuenciaResidual";
            //ws.Cell("AD" + 1).Value = "CodigoFrecuenciaResidual";
            ws.Cell("AH" + 1).Value = "ImpactoResidual";
            //ws.Cell("AF" + 1).Value = "CodigoImpactoResidual";
            ws.Cell("AI" + 1).Value = "RiesgoResidual";
            ws.Cell("AJ" + 1).Value = "Indicador SAR Residual";
            ws.Cell("AK" + 1).Value = "Indicador SAR Residual X";
            /*
            
            ws.Cell("BM" + 1).Value = "NombreMitiga";
            ws.Cell("BN" + 1).Value = "DesviacionImpacto";
            ws.Cell("BO" + 1).Value = "DesviacionFrecuencia";
            ws.Cell("BP" + 1).Value = "NombreArea";
            ws.Cell("BQ" + 1).Value = "Usuario";*/
            int countRiesgo = 1;
            double sumIndicadorSAR = 0;

            int countRiesgoResigual = 1;
            double sumIndicadorSARResidual = 0;
            /*try
            {*/
            foreach (DataRow row in dt.Rows)
            {
                ws.Cell("A" + indexRow).Value = row["CodigoRiesgo"].ToString();
                ws.Cell("B" + indexRow).Value = row["CadenaValor"].ToString();
                ws.Cell("C" + indexRow).Value = row["Macroproceso"].ToString();
                ws.Cell("D" + indexRow).Value = row["Proceso"].ToString();
                ws.Cell("E" + indexRow).Value = row["Subproceso"].ToString();
                ws.Cell("F" + indexRow).Value = row["DescripcionRiesgo"].ToString();
                //ws.Cell("G" + indexRow).Value = row["NombreRiesgo"].ToString();
                ws.Cell("G" + indexRow).Value = row["Causas"].ToString();
                ws.Cell("H" + indexRow).Value = row["Objetivo estratégico asociado"].ToString();
                ws.Cell("I" + indexRow).Value = row["ClasificacionRiesgo"].ToString();
                ws.Cell("J" + indexRow).Value = row["ClasificacionGeneralRiesgo"].ToString();
                ws.Cell("K" + indexRow).Value = row["ClasificacionParticularRiesgo"].ToString();
                ws.Cell("L" + indexRow).Value = row["Parte Interesada que Genera el Riesgo"].ToString();
                ws.Cell("M" + indexRow).Value = row["Parte Interesada Afectada por el Riesgo"].ToString();
                /*ws.Cell("O" + indexRow).Value = row["ResponsableRiesgo"].ToString();
                ws.Cell("P" + indexRow).Value = row["FechaRegistroRiesgo"].ToString();
                ws.Cell("Q" + indexRow).Value = row["Ubicacion"].ToString();
                ws.Cell("R" + indexRow).Value = row["FactorRiesgoOperativo"].ToString();
                ws.Cell("S" + indexRow).Value = row["SubFactorRiesgoOperativo"].ToString();
                ws.Cell("T" + indexRow).Value = row["ListaRiesgoAsociadoLA"].ToString();
                ws.Cell("U" + indexRow).Value = row["ListaFactorRiesgoLAFT"].ToString();
                ws.Cell("V" + indexRow).Value = row["TipoEvento"].ToString();*/
                //ws.Cell("N" + indexRow).Value = row["RiesgoAsociadoOperativo"].ToString();
                ws.Cell("N" + indexRow).Value = row["FrecuenciaInherente"].ToString();
                //ws.Cell("X" + indexRow).Value = row["CodigoFrecuenciaInherente"].ToString();
                ws.Cell("O" + indexRow).Value = row["ImpactoInherente"].ToString();
                //ws.Cell("Z" + indexRow).Value = row["CodigoImpactoInherente"].ToString();
                if (row["RiesgoInherente"].ToString() == "Bajo")
                {
                    ws.Cell(indexRow, 16).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Green;
                }
                if (row["RiesgoInherente"].ToString() == "Moderado")
                {
                    ws.Cell(indexRow, 16).Style.Fill.BackgroundColor = XLColor.Yellow;
                }
                if (row["RiesgoInherente"].ToString() == "Alto")
                {
                    ws.Cell(indexRow, 16).Style.Fill.BackgroundColor = XLColor.Orange;
                }
                if (row["RiesgoInherente"].ToString() == "Extremo")
                {
                    ws.Cell(indexRow, 16).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Red;
                }
                ws.Cell("P" + indexRow).Value = row["RiesgoInherente"].ToString();


                ws.Cell("Q" + indexRow).Value = mtdCalculoSARinherente(row["CodigoFrecuenciaInherente"].ToString(), row["CodigoImpactoInherente"].ToString());
                sumIndicadorSAR += Convert.ToDouble(mtdCalculoSARinherente(row["CodigoFrecuenciaInherente"].ToString(), row["CodigoImpactoInherente"].ToString()));
                if (countRiesgo == Convert.ToInt32(row["cantRiesgo"]))
                {
                    double promedioSAR = 0;
                    if (row["cantRiesgo"].ToString() != "0")
                        promedioSAR = sumIndicadorSAR / Convert.ToDouble(row["cantRiesgo"]);
                    ws.Cell("R" + indexRow).Value = promedioSAR;
                    sumIndicadorSAR = 0;
                    countRiesgo = 1;

                    if (row["RiesgoInherente"].ToString() == "Bajo")
                    {
                        ws.Cell(indexRow, 18).Style
                            .Font.SetFontColor(XLColor.White)
                            .Fill.BackgroundColor = XLColor.Green;
                    }
                    if (row["RiesgoInherente"].ToString() == "Moderado")
                    {
                        ws.Cell(indexRow, 18).Style.Fill.BackgroundColor = XLColor.Yellow;
                    }
                    if (row["RiesgoInherente"].ToString() == "Alto")
                    {
                        ws.Cell(indexRow, 18).Style.Fill.BackgroundColor = XLColor.Orange;
                    }
                    if (row["RiesgoInherente"].ToString() == "Extremo")
                    {
                        ws.Cell(indexRow, 18).Style
                            .Font.SetFontColor(XLColor.White)
                            .Fill.BackgroundColor = XLColor.Red;
                    }
                }
                else
                {
                    if (row["cantRiesgo"].ToString() != "0")
                        countRiesgo++;
                    else
                        sumIndicadorSAR = 0;
                }
                /*ws.Cell("R" + indexRow).Value = row["Consecuencias"].ToString();
                ws.Cell("S" + indexRow).Value = row["Actividad"].ToString();
                ws.Cell("T" + indexRow).Value = row["ListaTratamiento"].ToString();*/
                ws.Cell("S" + indexRow).Value = row["CodigoControl"].ToString();
                ws.Cell("T" + indexRow).Value = row["NombreControl"].ToString();
                ws.Cell("U" + indexRow).Value = row["DescripcionControl"].ToString();
                ws.Cell("V" + indexRow).Value = row["ObjetivoControl"].ToString();
                ws.Cell("W" + indexRow).Value = row["Procedimiento Asociado"].ToString();
                //ws.Cell("AQ" + indexRow).Value = row["CausasAsociacion"].ToString();
                ws.Cell("X" + indexRow).Value = row["ResponsableControlEjecucion"].ToString();
                ws.Cell("Y" + indexRow).Value = row["ResponsableControlCalificacion"].ToString();
                //ws.Cell("AT" + indexRow).Value = row["FechaRegistroControl"].ToString();
                ws.Cell("Z" + indexRow).Value = row["NombrePeriodicidad"].ToString();
                //ws.Cell("AV" + indexRow).Value = row["NombreTest"].ToString();
                ws.Cell("AA" + indexRow).Value = row["Variable1"].ToString();
                ws.Cell("AB" + indexRow).Value = row["Variable2"].ToString();
                ws.Cell("AC" + indexRow).Value = row["Variable4"].ToString();

                //ws.Cell("AY" + indexRow).Value = row["Variable3"].ToString();
                /*
                ws.Cell("BA" + indexRow).Value = row["Variable5"].ToString();
                ws.Cell("BB" + indexRow).Value = row["Variable6"].ToString();

                ws.Cell("BE" + indexRow).Value = row["Variable9"].ToString();
                ws.Cell("BF" + indexRow).Value = row["Variable10"].ToString();
                ws.Cell("BG" + indexRow).Value = row["Variable11"].ToString();
                ws.Cell("BH" + indexRow).Value = row["Variable12"].ToString();
                ws.Cell("BI" + indexRow).Value = row["Variable13"].ToString();
                ws.Cell("BJ" + indexRow).Value = row["Variable14"].ToString();
                ws.Cell("BK" + indexRow).Value = row["Variable15"].ToString();*/
                string efectividad = row["efectividad"].ToString();
                string operatividad = row["operatividad"].ToString();
                ws.Cell("AD" + indexRow).Value = efectividad;//row["Variable6"].ToString();
                ws.Cell("AE" + indexRow).Value = operatividad;//row["Variable7"].ToString();
                string cod = row["CodigoRiesgo"].ToString();
                string solidez = mtdCalculoSolidezControl(mtdCalculoEfectividad(efectividad), mtdCalculoOperatividad(operatividad));
                ws.Cell("AF" + indexRow).Value = solidez;// row["NombreEscala"].ToString();
                if (/*row["NombreEscala"].ToString()*/solidez == "Excelente")
                {
                    ws.Cell(indexRow, 32).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Green;
                }
                if (/*row["NombreEscala"].ToString()*/solidez == "Bueno")
                {
                    ws.Cell(indexRow, 32).Style.Fill.BackgroundColor = XLColor.Yellow;
                }
                if (/*row["NombreEscala"].ToString()*/solidez == "Regular")
                {
                    ws.Cell(indexRow, 32).Style.Fill.BackgroundColor = XLColor.Orange;
                }
                if (/*row["NombreEscala"].ToString()*/solidez == "Deficiente")
                {
                    ws.Cell(indexRow, 32).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Red;
                }
                ws.Cell("AG" + indexRow).Value = row["FrecuenciaResidual"].ToString();
                //ws.Cell("AD" + indexRow).Value = row["CodigoFrecuenciaResidual"].ToString();
                ws.Cell("AH" + indexRow).Value = row["ImpactoResidual"].ToString();
                //ws.Cell("AF" + indexRow).Value = row["CodigoImpactoResidual"].ToString();
                
                clsBLLRiesgosCalculos objProcess = new clsBLLRiesgosCalculos();
                string indicadorSAR = mtdCalculoSARinherente(row["CodigoFrecuenciaInherente"].ToString(), row["CodigoImpactoInherente"].ToString());
                string valorIndicador = mtdCalculoIndicadorSARresidual(indicadorSAR, solidez);
                string RiesgoResidual = string.Empty;
                objProcess.mtdGetRiesgoResidual(valorIndicador,ref RiesgoResidual);
                ws.Cell("AI" + indexRow).Value = RiesgoResidual;
                if (RiesgoResidual == "Bajo")
                {
                    ws.Cell(indexRow, 35).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Green;
                }
                if (RiesgoResidual == "Moderado")
                {
                    ws.Cell(indexRow, 35).Style.Fill.BackgroundColor = XLColor.Yellow;
                }
                if (RiesgoResidual == "Alto")
                {
                    ws.Cell(indexRow, 35).Style.Fill.BackgroundColor = XLColor.Orange;
                }
                if (RiesgoResidual == "Extremo")
                {
                    ws.Cell(indexRow, 35).Style
                        .Font.SetFontColor(XLColor.White)
                        .Fill.BackgroundColor = XLColor.Red;
                }
                //ws.Cell("AI" + indexRow).Value = row["RiesgoResidual"].ToString();

                //string indicadorSAR = mtdCalculoSARinherente(row["CodigoFrecuenciaResidual"].ToString(), row["CodigoImpactoResidual"].ToString());


                ws.Cell("AJ" + indexRow).Value = mtdCalculoIndicadorSARresidual(indicadorSAR, solidez/*row["NombreEscala"].ToString()*/);
                if (/*row["NombreEscala"].ToString()*/solidez != "")
                {
                    //string valor = mtdCalculoRiesgoResidual(indicadorSAR);
                    sumIndicadorSARResidual += Convert.ToDouble(mtdCalculoIndicadorSARresidual(indicadorSAR, solidez/*row["NombreEscala"].ToString()*/));
                    if (countRiesgoResigual == Convert.ToInt32(row["cantRiesgo"]))
                    {
                        double promedioSAR = sumIndicadorSARResidual / Convert.ToDouble(row["cantRiesgo"]);
                        ws.Cell("AK" + indexRow).Value = promedioSAR;
                        sumIndicadorSARResidual = 0;
                        countRiesgoResigual = 1;
                        if (row["RiesgoResidual"].ToString() == "Bajo")
                        {
                            ws.Cell(indexRow, 37).Style
                                .Font.SetFontColor(XLColor.White)
                                .Fill.BackgroundColor = XLColor.Green;
                        }
                        if (row["RiesgoResidual"].ToString() == "Moderado")
                        {
                            ws.Cell(indexRow, 37).Style.Fill.BackgroundColor = XLColor.Yellow;
                        }
                        if (row["RiesgoResidual"].ToString() == "Alto")
                        {
                            ws.Cell(indexRow, 37).Style.Fill.BackgroundColor = XLColor.Orange;
                        }
                        if (row["RiesgoResidual"].ToString() == "Extremo")
                        {
                            ws.Cell(indexRow, 37).Style
                                .Font.SetFontColor(XLColor.White)
                                .Fill.BackgroundColor = XLColor.Red;
                        }
                    }
                    else
                    {
                        countRiesgoResigual++;
                    }
                }

                /*ws.Cell("BM" + indexRow).Value = row["NombreMitiga"].ToString();
                ws.Cell("BN" + indexRow).Value = row["DesviacionImpacto"].ToString();
                ws.Cell("BO" + indexRow).Value = row["DesviacionFrecuencia"].ToString();
                ws.Cell("BP" + indexRow).Value = row["NombreArea"].ToString();
                ws.Cell("BQ" + indexRow).Value = row["Usuario"].ToString();*/
                indexRow++;
            }
            /*}
            catch(Exception ex)
            {
                strErrMsg = "Error: " + ex.Message;
            }*/

            return workbook;
        }
        public static string mtdCalculoEfectividad(string efectividad)
        {
            string result = string.Empty;
            if (efectividad == "Muy Fuerte")
                result = "1";
            if (efectividad == "Fuerte")
                result = "2";
            if (efectividad == "Moderada")
                result = "3";
            if (efectividad == "Débil")
                result = "4";
            return result;
        }
        public static string mtdCalculoOperatividad(string operatividad)
        {
            string result = string.Empty;
            if (operatividad == "Alta")
                result = "1";
            if (operatividad == "Media")
                result = "2";
            if (operatividad == "Baja")
                result = "3";
            if (operatividad == "Muy Baja")
                result = "4";
            return result;
        }
        public static string mtdCalculoSolidezControl(string efectividad, string operatividad)
        {
            string result = string.Empty;
            if (efectividad == "1" && operatividad == "1")
                result = "Excelente";
            if (efectividad == "1" && operatividad == "2")
                result = "Bueno";
            if (efectividad == "1" && operatividad == "3")
                result = "Regular";
            if (efectividad == "1" && operatividad == "4")
                result = "Deficiente";
            if (efectividad == "2" && operatividad == "1")
                result = "Excelente";
            if (efectividad == "2" && operatividad == "2")
                result = "Bueno";
            if (efectividad == "2" && operatividad == "3")
                result = "Regular";
            if (efectividad == "2" && operatividad == "4")
                result = "Deficiente";
            if (efectividad == "3" && operatividad == "1")
                result = "Bueno";
            if (efectividad == "3" && operatividad == "2")
                result = "Bueno";
            if (efectividad == "3" && operatividad == "3")
                result = "Regular";
            if (efectividad == "3" && operatividad == "4")
                result = "Deficiente";
            if (efectividad == "4" && operatividad == "1")
                result = "Regular";
            if (efectividad == "4" && operatividad == "2")
                result = "Regular";
            if (efectividad == "4" && operatividad == "3")
                result = "Deficiente";
            if (efectividad == "4" && operatividad == "4")
                result = "Deficiente";
            return result;
        }
        public static string mtdCalculoSARinherente(string frecuencia, string impacto)
        {
            string result = string.Empty;
            if (frecuencia == "1" && impacto == "1")
                result = "1";
            if (frecuencia == "1" && impacto == "2")
                result = "1,6";
            if (frecuencia == "1" && impacto == "3")
                result = "2,6";
            if (frecuencia == "1" && impacto == "4")
                result = "3,62";
            if (frecuencia == "1" && impacto == "5")
                result = "3,9";
            if (frecuencia == "2" && impacto == "1")
                result = "1,2";
            if (frecuencia == "2" && impacto == "2")
                result = "1,9";
            if (frecuencia == "2" && impacto == "3")
                result = "2,9";
            if (frecuencia == "2" && impacto == "4")
                result = "2,9";
            if (frecuencia == "2" && impacto == "5")
                result = "4,5";
            if (frecuencia == "3" && impacto == "1")
                result = "1,4";
            if (frecuencia == "3" && impacto == "2")
                result = "2,3";
            if (frecuencia == "3" && impacto == "3")
                result = "3,37";
            if (frecuencia == "3" && impacto == "4")
                result = "4,12";
            if (frecuencia == "3" && impacto == "5")
                result = "4,62";
            if (frecuencia == "4" && impacto == "1")
                result = "2";
            if (frecuencia == "4" && impacto == "2")
                result = "3,12";
            if (frecuencia == "4" && impacto == "3")
                result = "3,5";
            if (frecuencia == "4" && impacto == "4")
                result = "4,25";
            if (frecuencia == "4" && impacto == "5")
                result = "4,75";
            if (frecuencia == "5" && impacto == "1")
                result = "3";
            if (frecuencia == "5" && impacto == "2")
                result = "3,25";
            if (frecuencia == "5" && impacto == "3")
                result = "4";
            if (frecuencia == "5" && impacto == "4")
                result = "4,37";
            if (frecuencia == "5" && impacto == "5")
                result = "5";
            return result;
        }
        public static string mtdCalculoControl(string solidez)
        {
            string result = string.Empty;
            if (solidez == "Excelente")
                result = "1";
            if (solidez == "Bueno")
                result = "2";
            if (solidez == "Regular")
                result = "3";
            if (solidez == "Deficiente")
                result = "4";
            return result;
        }
        public static string mtdCalculoRiesgoResidual(string IndicadorSAR)
        {
            string result = string.Empty;
            if (IndicadorSAR == "1")
                result = "1";
            if (IndicadorSAR == "1,2")
                result = "6";
            if (IndicadorSAR == "1,4")
                result = "11";
            if (IndicadorSAR == "1,6")
                result = "2";
            if (IndicadorSAR == "1,9")
                result = "7";
            if (IndicadorSAR == "2")
                result = "16";
            if (IndicadorSAR == "2,3")
                result = "12";
            if (IndicadorSAR == "2,6")
                result = "3";
            if (IndicadorSAR == "2,9")
                result = "8";
            if (IndicadorSAR == "3")
                result = "21";
            if (IndicadorSAR == "3,12")
                result = "17";
            if (IndicadorSAR == "3,25")
                result = "22";
            if (IndicadorSAR == "3,37")
                result = "13";
            if (IndicadorSAR == "3,5")
                result = "18";
            if (IndicadorSAR == "3,62")
                result = "4";
            if (IndicadorSAR == "3,7")
                result = "9";
            if (IndicadorSAR == "3,9")
                result = "5";
            if (IndicadorSAR == "4")
                result = "23";
            if (IndicadorSAR == "4,12")
                result = "14";
            if (IndicadorSAR == "4,25")
                result = "19";
            if (IndicadorSAR == "4,37")
                result = "24";
            if (IndicadorSAR == "4,5")
                result = "10";
            if (IndicadorSAR == "4,62")
                result = "15";
            if (IndicadorSAR == "4,75")
                result = "20";
            if (IndicadorSAR == "5")
                result = "25";
            return result;
        }
        public static string mtdCalculoIndicadorSARresidual(string IndicadorSAR, string solidez)
        {
            string result = string.Empty;
            /****************************** Solidez = 1 *******************************************/
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "1")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "10")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "11")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "12")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "13")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "14")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "15")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "16")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "17")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "18")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "19")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "2")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "20")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "21")
                result = "1,2";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "22")
                result = "1,2";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "23")
                result = "1,9";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "24")
                result = "2,9";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "25")
                result = "3,7";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "3")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "4")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "5")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "6")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "7")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "8")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "9")
                result = "2,6";
            /****************************** Solidez = 2 *******************************************/
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "1")
                result = "1";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "10")
                result = "3,9";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "11")
                result = "1";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "12")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "13")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "14")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "15")
                result = "3,9";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "16")
                result = "1,2";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "17")
                result = "1,9";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "18")
                result = "2,9";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "19")
                result = "3,7";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "2")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "20")
                result = "4,5";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "21")
                result = "1,4";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "22")
                result = "2,3";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "23")
                result = "3,37";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "24")
                result = "4,12";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "25")
                result = "4,62";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "3")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "4")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "5")
                result = "3,9";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "6")
                result = "1";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "7")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "8")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "9")
                result = "3,62";
            /****************************** Solidez = 3 *******************************************/
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "1")
                result = "1";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "10")
                result = "3,9";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "11")
                result = "1,2";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "12")
                result = "1,9";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "13")
                result = "2,9";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "14")
                result = "3,7";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "15")
                result = "4,5";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "16")
                result = "1,4";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "17")
                result = "2,3";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "18")
                result = "3,37";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "19")
                result = "4,12";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "2")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "20")
                result = "4,62";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "21")
                result = "2";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "22")
                result = "3,12";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "23")
                result = "3,5";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "24")
                result = "4,25";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "25")
                result = "4,75";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "3")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "4")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "5")
                result = "3,9";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "6")
                result = "1";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "7")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "8")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "9")
                result = "3,62";
            /****************************** Solidez = 4 *******************************************/
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "1")
                result = "1";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "10")
                result = "4,5";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "11")
                result = "1,4";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "12")
                result = "2,3";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "13")
                result = "3,37";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "14")
                result = "4,12";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "15")
                result = "4,62";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "16")
                result = "2";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "17")
                result = "3,12";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "18")
                result = "3,5";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "19")
                result = "4,25";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "2")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "20")
                result = "4,75";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "21")
                result = "3";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "22")
                result = "3,25";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "23")
                result = "4";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "24")
                result = "4,37";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "25")
                result = "5";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "3")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "4")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "5")
                result = "3,9";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "6")
                result = "1,2";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "7")
                result = "1,9";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "8")
                result = "2,9";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "9")
                result = "3,7";
            return result;
        }

        public static void exportExcel(DataTable dt, HttpResponse Response, string filename, int tipoReporte)
        {
            /*Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            Response.ContentEncoding = System.Text.Encoding.Default;
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
            dg.DataSource = dt;
            dg.DataBind();
            dg.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();*/
            // Create the workbook
            XLWorkbook workbook = new XLWorkbook();
            //workbook.Worksheets.Add("Sample").Cell(1, 1).SetValue("Hello World");
            // From Known color
            //var ws = workbook.Worksheets.Add("Using Colors");
            //ws.Tables.Add(dt);
            //workbook.Worksheets.Add(dt);
            // Prepare the response
            string strErrMsg = string.Empty;
            if (tipoReporte == 1)
                workbook = mtdCreateXbookRiesgos(dt);
            if (tipoReporte == 2)
                workbook = mtdCreateXbookRiesgosControles(dt, ref strErrMsg);
            if (tipoReporte != 1 && tipoReporte != 2)
                workbook.Worksheets.Add(dt);
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

        private void resetValuesConsulta()
        {
            DropDownList1.SelectedIndex = 0;
            DropDownList52.SelectedIndex = 0;
            DropDownList53.Items.Clear();
            DropDownList53.Items.Insert(0, new ListItem("---", "---"));
            DropDownList54.Items.Clear();
            DropDownList54.Items.Insert(0, new ListItem("---", "---"));
            DropDownList56.SelectedIndex = 0;
            DropDownList57.Items.Clear();
            DropDownList57.Items.Insert(0, new ListItem("---", "---"));
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
            DropDownList4.SelectedIndex = 0;
            DDLareas.ClearSelection();
            ReporteRiesgos.Visible = false;
            ReporteRiesgosControles.Visible = false;
            ReporteRiesgosEventos.Visible = false;
            ReporteRiesgosPlanesAccion.Visible = false;
            ReporteModControles.Visible = false;
            ReporteModRiesgos.Visible = false;
            ReporteCausasSinControl.Visible = false;
            ReportePerfilRiesgos.Visible = false;
            ReporteRiesgoControl.Visible = false;
            TxtFechaIni.Text = string.Empty;
            TxtFechaFin.Text = string.Empty;
            ReporteControles.Visible = false;
        }

        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #region Reporte Riesgos
        private void loadGridReporteRiesgos()
        {
            DataTable grid = new DataTable();

            #region Columnas GRID
            grid.Columns.Add("CodigoRiesgo", typeof(string));
            grid.Columns.Add("CadenaValor", typeof(string));
            grid.Columns.Add("Macroproceso", typeof(string));
            grid.Columns.Add("Proceso", typeof(string));
            grid.Columns.Add("Subproceso", typeof(string));
            grid.Columns.Add("NombreRiesgo", typeof(string));
            grid.Columns.Add("DescripcionRiesgo", typeof(string));
            grid.Columns.Add("Causas", typeof(string));
            grid.Columns.Add("Objetivo estratégico asociado", typeof(string));
            grid.Columns.Add("ClasificacionRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionGeneralRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionParticularRiesgo", typeof(string));
            grid.Columns.Add("Parte Interesada que Genera el Riesgo", typeof(string));
            grid.Columns.Add("Parte Interesada Afectada por el Riesgo", typeof(string));
            grid.Columns.Add("FrecuenciaInherente", typeof(string));
            grid.Columns.Add("CodigoFrecuenciaInherente", typeof(string));
            grid.Columns.Add("ImpactoInherente", typeof(string));
            grid.Columns.Add("CodigoImpactoInherente", typeof(string));
            grid.Columns.Add("RiesgoInherente", typeof(string));
            grid.Columns.Add("CodigoRiesgoInherente", typeof(string));
            grid.Columns.Add("FrecuenciaResidual", typeof(string));
            grid.Columns.Add("CodigoFrecuenciaResidual", typeof(string));
            grid.Columns.Add("ImpactoResidual", typeof(string));
            grid.Columns.Add("CodigoImpactoResidual", typeof(string));
            grid.Columns.Add("RiesgoResidual", typeof(string));
            grid.Columns.Add("CodigoRiesgoResidual", typeof(string));
            grid.Columns.Add("ResponsableRiesgo", typeof(string));
            grid.Columns.Add("FechaRegistroRiesgo", typeof(string));
            grid.Columns.Add("Ubicacion", typeof(string));
            grid.Columns.Add("FactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("SubFactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("ListaRiesgoAsociadoLA", typeof(string));
            grid.Columns.Add("ListaFactorRiesgoLAFT", typeof(string));
            grid.Columns.Add("TipoEvento", typeof(string));
            grid.Columns.Add("RiesgoAsociadoOperativo", typeof(string));
            grid.Columns.Add("Consecuencias", typeof(string));
            grid.Columns.Add("Actividad", typeof(string));
            grid.Columns.Add("ListaTratamiento", typeof(string));
            grid.Columns.Add("NombreArea", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            #endregion Columnas GRID

            //Fin Ajuetes 01
            InfoGridReporteRiesgos = grid;
            GridView1.DataSource = InfoGridReporteRiesgos;
            GridView1.DataBind();
        }

        private void loadInfoReporteRiesgos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.ReporteRiesgos(DropDownList52.SelectedValue.ToString().Trim(),
                DropDownList53.SelectedValue.ToString().Trim(), DropDownList54.SelectedValue.ToString().Trim(),
                DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(),
                DropDownList4.SelectedValue.ToString().Trim(), "1", "---", DDLareas.SelectedValue.ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido para llenar informacion
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridReporteRiesgos.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["CodigoRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["CadenaValor"].ToString().Trim(),
                        dtInfo.Rows[rows]["Macroproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Proceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Subproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["DescripcionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Causas"].ToString().Trim(),//Causas,
                        dtInfo.Rows[rows]["NombreObjetivos"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionGeneralRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionParticularRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["pigenerariesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["piafectariesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FrecuenciaInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoFrecuenciaInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["ImpactoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoImpactoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["FrecuenciaResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoFrecuenciaResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["ImpactoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoImpactoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["ResponsableRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistroRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Ubicacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["FactorRiesgoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubFactorRiesgoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaRiesgoAsociadoLA"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaFactorRiesgoLAFT"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoAsociadoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Consecuencias"].ToString().Trim(),//Consecuencias,
                        dtInfo.Rows[rows]["Actividad"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaTratamiento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreArea"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                        });
                }
                #endregion Recorrido para llenar informacion

                GridView1.PageIndex = PagIndexInfoGridReporteRiesgos;
                GridView1.DataSource = InfoGridReporteRiesgos;
                GridView1.DataBind();
            }
            else
            {
                loadGridReporteRiesgos();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }
        #endregion Reporte Riesgos

        #region Reporte Riesgos-Controles
        private void loadGridReporteRiesgosControles()
        {
            #region GRID
            DataTable grid = new DataTable();
            grid.Columns.Add("CodigoRiesgo", typeof(string));
            grid.Columns.Add("NombreRiesgo", typeof(string));
            grid.Columns.Add("DescripcionRiesgo", typeof(string));
            grid.Columns.Add("CadenaValor", typeof(string));
            grid.Columns.Add("Macroproceso", typeof(string));
            grid.Columns.Add("Proceso", typeof(string));
            grid.Columns.Add("Subproceso", typeof(string));
            grid.Columns.Add("Causas", typeof(string));
            grid.Columns.Add("Objetivo estratégico asociado", typeof(string));
            grid.Columns.Add("ClasificacionRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionGeneralRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionParticularRiesgo", typeof(string));
            grid.Columns.Add("Parte Interesada que Genera el Riesgo", typeof(string));
            grid.Columns.Add("Parte Interesada Afectada por el Riesgo", typeof(string));
            grid.Columns.Add("ResponsableRiesgo", typeof(string));
            grid.Columns.Add("FechaRegistroRiesgo", typeof(string));
            grid.Columns.Add("Ubicacion", typeof(string));
            grid.Columns.Add("FactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("SubFactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("ListaRiesgoAsociadoLA", typeof(string));
            grid.Columns.Add("ListaFactorRiesgoLAFT", typeof(string));
            grid.Columns.Add("TipoEvento", typeof(string));
            grid.Columns.Add("RiesgoAsociadoOperativo", typeof(string));
            grid.Columns.Add("FrecuenciaInherente", typeof(string));
            grid.Columns.Add("CodigoFrecuenciaInherente", typeof(string));
            grid.Columns.Add("ImpactoInherente", typeof(string));
            grid.Columns.Add("CodigoImpactoInherente", typeof(string));
            grid.Columns.Add("RiesgoInherente", typeof(string));
            grid.Columns.Add("CodigoRiesgoInherente", typeof(string));
            grid.Columns.Add("FrecuenciaResidual", typeof(string));
            grid.Columns.Add("CodigoFrecuenciaResidual", typeof(string));
            grid.Columns.Add("ImpactoResidual", typeof(string));
            grid.Columns.Add("CodigoImpactoResidual", typeof(string));
            grid.Columns.Add("RiesgoResidual", typeof(string));
            grid.Columns.Add("CodigoRiesgoResidual", typeof(string));
            grid.Columns.Add("Consecuencias", typeof(string));
            grid.Columns.Add("Actividad", typeof(string));
            grid.Columns.Add("ListaTratamiento", typeof(string));
            grid.Columns.Add("CodigoControl", typeof(string));
            grid.Columns.Add("NombreControl", typeof(string));
            grid.Columns.Add("DescripcionControl", typeof(string));
            grid.Columns.Add("ObjetivoControl", typeof(string));
            grid.Columns.Add("Procedimiento Asociado", typeof(string));
            grid.Columns.Add("CausasAsociacion", typeof(string));
            grid.Columns.Add("ResponsableControlEjecucion", typeof(string));
            grid.Columns.Add("ResponsableControlCalificacion", typeof(string));
            grid.Columns.Add("FechaRegistroControl", typeof(string));
            grid.Columns.Add("NombrePeriodicidad", typeof(string));
            grid.Columns.Add("NombreTest", typeof(string));
            grid.Columns.Add("Variable1", typeof(string));
            grid.Columns.Add("Variable2", typeof(string));
            grid.Columns.Add("Variable3", typeof(string));
            grid.Columns.Add("Variable4", typeof(string));
            grid.Columns.Add("Variable5", typeof(string));
            grid.Columns.Add("Variable6", typeof(string));
            grid.Columns.Add("Variable7", typeof(string));
            grid.Columns.Add("Variable8", typeof(string));
            grid.Columns.Add("Variable9", typeof(string));
            grid.Columns.Add("Variable10", typeof(string));
            grid.Columns.Add("Variable11", typeof(string));
            grid.Columns.Add("Variable12", typeof(string));
            grid.Columns.Add("Variable13", typeof(string));
            grid.Columns.Add("Variable14", typeof(string));
            grid.Columns.Add("Variable15", typeof(string));
            grid.Columns.Add("NombreEscala", typeof(string));
            grid.Columns.Add("efectividad", typeof(string));
            grid.Columns.Add("operatividad", typeof(string));
            grid.Columns.Add("NombreMitiga", typeof(string));
            grid.Columns.Add("DesviacionImpacto", typeof(string));
            grid.Columns.Add("DesviacionFrecuencia", typeof(string));
            grid.Columns.Add("NombreArea", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("cantRiesgo", typeof(string));
            #endregion GRID

            InfoGridReporteRiesgosControles = grid;
            GridView2.DataSource = InfoGridReporteRiesgosControles;
            GridView2.DataBind();

            DataTable gridConsolidado = new DataTable();
            gridConsolidado.Columns.Add("Riesgo", typeof(string));
            gridConsolidado.Columns.Add("Nombre", typeof(string));
            gridConsolidado.Columns.Add("valoracion", typeof(string));
            gridConsolidado.Columns.Add("calificacion", typeof(string));
            InfoGridReporteRiesgosControlesConsolidado = gridConsolidado;
            GridView8.DataSource = InfoGridReporteRiesgosControlesConsolidado;
            GridView8.DataBind();
        }

        private void loadInfoReporteRiesgosControles()
        {

            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.ReporteRiesgos(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                    DropDownList54.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                    DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(), "2", "---",
                    DDLareas.SelectedValue.ToString().Trim());

                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido para llenar informacion
                    string NombreResponsableControlEjecucion = string.Empty, CodigoControl = string.Empty;
                    for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                    {
                        if (dtInfo != null)
                        {
                            if (dtInfo.Rows[rows]["CodigoControl"].ToString().Trim() == "")
                                CodigoControl = "Sin Control Asociado";
                            else
                                CodigoControl = dtInfo.Rows[rows]["CodigoControl"].ToString().Trim();
                            InfoGridReporteRiesgosControles.Rows.Add(new Object[] {
                            dtInfo.Rows[rows]["CodigoRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["NombreRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["DescripcionRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["CadenaValor"].ToString().Trim(),
                            dtInfo.Rows[rows]["Macroproceso"].ToString().Trim(),
                            dtInfo.Rows[rows]["Proceso"].ToString().Trim(),
                            dtInfo.Rows[rows]["Subproceso"].ToString().Trim(),
                            dtInfo.Rows[rows]["Causas"].ToString().Trim(),//Causas,
                            dtInfo.Rows[rows]["NombreObjetivos"].ToString().Trim(),
                            dtInfo.Rows[rows]["ClasificacionRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["ClasificacionGeneralRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["ClasificacionParticularRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["pigenerariesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["piafectariesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["ResponsableRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["FechaRegistroRiesgo"].ToString().Trim(),
                            dtInfo.Rows[rows]["Ubicacion"].ToString().Trim(),
                            dtInfo.Rows[rows]["FactorRiesgoOperativo"].ToString().Trim(),
                            dtInfo.Rows[rows]["SubFactorRiesgoOperativo"].ToString().Trim(),
                            dtInfo.Rows[rows]["ListaRiesgoAsociadoLA"].ToString().Trim(),
                            dtInfo.Rows[rows]["ListaFactorRiesgoLAFT"].ToString().Trim(),
                            dtInfo.Rows[rows]["TipoEvento"].ToString().Trim(),
                            dtInfo.Rows[rows]["RiesgoAsociadoOperativo"].ToString().Trim(),
                            dtInfo.Rows[rows]["FrecuenciaInherente"].ToString().Trim(),
                            dtInfo.Rows[rows]["CodigoFrecuenciaInherente"].ToString().Trim(),
                            dtInfo.Rows[rows]["ImpactoInherente"].ToString().Trim(),
                            dtInfo.Rows[rows]["CodigoImpactoInherente"].ToString().Trim(),
                            dtInfo.Rows[rows]["RiesgoInherente"].ToString().Trim(),
                            dtInfo.Rows[rows]["CodigoRiesgoInherente"].ToString().Trim(),
                            dtInfo.Rows[rows]["FrecuenciaResidual"].ToString().Trim(),
                            dtInfo.Rows[rows]["CodigoFrecuenciaResidual"].ToString().Trim(),
                            dtInfo.Rows[rows]["ImpactoResidual"].ToString().Trim(),
                            dtInfo.Rows[rows]["CodigoImpactoResidual"].ToString().Trim(),
                            dtInfo.Rows[rows]["RiesgoResidual"].ToString().Trim(),
                            dtInfo.Rows[rows]["CodigoRiesgoResidual"].ToString().Trim(),
                            dtInfo.Rows[rows]["Consecuencias"].ToString().Trim(),//Consecuencias,
                            dtInfo.Rows[rows]["Actividad"].ToString().Trim(),
                            dtInfo.Rows[rows]["ListaTratamiento"].ToString().Trim(),
                            CodigoControl,
                            dtInfo.Rows[rows]["NombreControl"].ToString().Trim(),
                            dtInfo.Rows[rows]["DescripcionControl"].ToString().Trim(),
                            dtInfo.Rows[rows]["ObjetivoControl"].ToString().Trim(),
                            dtInfo.Rows[rows]["procedimiento"].ToString().Trim(),
                            dtInfo.Rows[rows]["CausasAsociacion"].ToString().Trim(),
                            NombreResponsableControlEjecucion = mtdBuscarNombresRespEjecucion(dtInfo.Rows[rows]["ResponsableControlEjecucion"].ToString().Trim()),
                            dtInfo.Rows[rows]["ResponsableControlCalificacion"].ToString().Trim(),
                            dtInfo.Rows[rows]["FechaRegistroControl"].ToString().Trim(),
                            dtInfo.Rows[rows]["NombrePeriodicidad"].ToString().Trim(),
                            dtInfo.Rows[rows]["NombreTest"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable1"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable2"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable3"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable4"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable5"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable6"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable7"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable8"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable9"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable10"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable11"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable12"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable13"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable14"].ToString().Trim(),
                            dtInfo.Rows[rows]["Variable15"].ToString().Trim(),
                            dtInfo.Rows[rows]["NombreEscala"].ToString().Trim(),
                            dtInfo.Rows[rows]["efectividad"].ToString().Trim(),
                            dtInfo.Rows[rows]["operatividad"].ToString().Trim(),
                            dtInfo.Rows[rows]["NombreMitiga"].ToString().Trim(),
                            dtInfo.Rows[rows]["DesviacionImpacto"].ToString().Trim(),
                            dtInfo.Rows[rows]["DesviacionFrecuencia"].ToString().Trim(),
                            dtInfo.Rows[rows]["NombreArea"].ToString().Trim(),
                            dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                            dtInfo.Rows[rows]["cantRiesgo"].ToString().Trim()
                            });
                        }

                    }
                    #endregion Recorrido para llenar informacion

                    GridView2.PageIndex = PagIndexInfoGridReporteRiesgosControles;
                    GridView2.DataSource = InfoGridReporteRiesgosControles;
                    GridView2.DataBind();
                }
                else
                {
                    loadGridReporteRiesgosControles();
                    Mensaje("No existen registros asociados a los parámetros de consulta.");
                }
                dtInfo = cRiesgo.ReporteRiesgosConsolidado(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                    DropDownList54.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                    DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(), "2", "---",
                    DDLareas.SelectedValue.ToString().Trim());
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido para llenar informacion
                    string NombreResponsableControlEjecucion = string.Empty, CodigoControl = string.Empty;
                    int countRiesgo = 1;
                    int countRiesgoCR = 1;
                    double sumIndicadorSAR = 0;
                    double sumIndicadorSARCons = 0;
                    int countRiesgoResigual = 1;
                    double sumIndicadorSARResidual = 0;
                    double sumIndicadorSARResidualCons = 0;
                    string riesgo = string.Empty;
                    for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                    {
                        if (dtInfo != null)
                        {
                            /*if (dtInfo.Rows[rows]["CodigoControl"].ToString().Trim() == "")
                                CodigoControl = "Sin Control Asociado";
                            else
                                CodigoControl = dtInfo.Rows[rows]["CodigoControl"].ToString().Trim();
                           
                            dtInfo.Rows[rows]["CodigoFrecuenciaInherente"].ToString().Trim()
                                dtInfo.Rows[rows]["CodigoImpactoInherente"].ToString().Trim()
                                dtInfo.Rows[rows]["cantRiesgo"].ToString().Trim()

                                dtInfo.Rows[rows]["CodigoFrecuenciaResidual"].ToString().Trim()
                                dtInfo.Rows[rows]["NombreEscala"].ToString().Trim()*/

                            //mtdCalculoSARinherente(row["CodigoFrecuenciaInherente"].ToString(), row["CodigoImpactoInherente"].ToString());
                            if (riesgo == dtInfo.Rows[rows]["ClasificacionRiesgo"].ToString().Trim())
                            {
                                sumIndicadorSAR += Convert.ToDouble(mtdCalculoSARinherente(dtInfo.Rows[rows]["CodigoFrecuenciaInherente"].ToString(), dtInfo.Rows[rows]["CodigoImpactoInherente"].ToString()));
                                if (countRiesgo == Convert.ToInt32(dtInfo.Rows[rows]["cantRiesgo"].ToString()))
                                {
                                    double promedioSAR = 0;
                                    if (dtInfo.Rows[rows]["cantRiesgo"].ToString() != "0")
                                    {
                                        promedioSAR = sumIndicadorSAR;/*/ Convert.ToDouble(dtInfo.Rows[rows]["cantRiesgo"]);*/
                                        sumIndicadorSARCons += promedioSAR;
                                    }
                                    sumIndicadorSAR = 0;
                                    countRiesgo = 1;
                                }
                                else
                                {
                                    if (dtInfo.Rows[rows]["cantRiesgo"].ToString() != "0")
                                        countRiesgo++;
                                    else
                                        sumIndicadorSAR = 0;
                                }
                                if (countRiesgoCR == Convert.ToInt32(dtInfo.Rows[rows]["cantCR"]))
                                {
                                    double total = (sumIndicadorSARCons / countRiesgoCR);
                                    InfoGridReporteRiesgosControlesConsolidado.Rows.Add(
                                        new Object[] {
                                            "Riesgo Inherente",
                                            riesgo,
                                            total,
                                            "JI"
                                        }
                                        );
                                }


                                string indicadorSAR = mtdCalculoSARinherente(dtInfo.Rows[rows]["CodigoFrecuenciaResidual"].ToString(), dtInfo.Rows[rows]["CodigoImpactoResidual"].ToString());
                                //ws.Cell("AJ" + indexRow).Value = mtdCalculoIndicadorSARresidual(indicadorSAR, dtInfo.Rows[rows]["NombreEscala"].ToString());
                                if (dtInfo.Rows[rows]["NombreEscala"].ToString() != "")
                                {
                                    sumIndicadorSARResidual += Convert.ToDouble(mtdCalculoIndicadorSARresidual(indicadorSAR, dtInfo.Rows[rows]["NombreEscala"].ToString()));
                                    if (countRiesgoResigual == Convert.ToInt32(dtInfo.Rows[rows]["cantRiesgo"]))
                                    {
                                        double promedioSAR = sumIndicadorSARResidual;/*/ Convert.ToDouble(dtInfo.Rows[rows]["cantRiesgo"]);*/
                                        sumIndicadorSARResidualCons += promedioSAR;
                                        sumIndicadorSARResidual = 0;
                                        countRiesgoResigual = 1;
                                    }
                                    else
                                    {
                                        countRiesgoResigual++;
                                    }
                                }
                                if (countRiesgoCR == Convert.ToInt32(dtInfo.Rows[rows]["cantCR"]))
                                {
                                    double total = (sumIndicadorSARResidualCons / countRiesgoCR);
                                    InfoGridReporteRiesgosControlesConsolidado.Rows.Add(
                                        new Object[] {
                                            "Riesgo Residual",
                                            riesgo,
                                            total,
                                            "JII"
                                        }
                                        );
                                    countRiesgoCR = 1;
                                }
                                else
                                {
                                    countRiesgoCR++;
                                }
                            }
                            else
                            {
                                if (riesgo == string.Empty)
                                    riesgo = dtInfo.Rows[rows]["ClasificacionRiesgo"].ToString().Trim();
                                sumIndicadorSAR += Convert.ToDouble(mtdCalculoSARinherente(dtInfo.Rows[rows]["CodigoFrecuenciaInherente"].ToString(), dtInfo.Rows[rows]["CodigoImpactoInherente"].ToString()));
                                double promedioSAR = 0;
                                if (countRiesgo == Convert.ToInt32(dtInfo.Rows[rows]["cantRiesgo"].ToString()))
                                {

                                    if (dtInfo.Rows[rows]["cantRiesgo"].ToString() != "0")
                                    {
                                        promedioSAR = sumIndicadorSAR; /*/ Convert.ToDouble(dtInfo.Rows[rows]["cantRiesgo"]);*/
                                    }
                                    sumIndicadorSAR = 0;
                                    countRiesgo = 1;
                                }
                                else
                                {
                                    if (dtInfo.Rows[rows]["cantRiesgo"].ToString() != "0")
                                        countRiesgo++;
                                    else
                                        sumIndicadorSAR = 0;
                                }

                                string indicadorSAR = mtdCalculoSARinherente(dtInfo.Rows[rows]["CodigoFrecuenciaResidual"].ToString(), dtInfo.Rows[rows]["CodigoImpactoResidual"].ToString());
                                //ws.Cell("AJ" + indexRow).Value = mtdCalculoIndicadorSARresidual(indicadorSAR, dtInfo.Rows[rows]["NombreEscala"].ToString());
                                double promedioSARresidual = 0;
                                if (dtInfo.Rows[rows]["NombreEscala"].ToString() != "")
                                {
                                    sumIndicadorSARResidual += Convert.ToDouble(mtdCalculoIndicadorSARresidual(indicadorSAR, dtInfo.Rows[rows]["NombreEscala"].ToString()));
                                    if (countRiesgoResigual == Convert.ToInt32(dtInfo.Rows[rows]["cantRiesgo"]))
                                    {
                                        promedioSARresidual = sumIndicadorSARResidual;/*/ Convert.ToDouble(dtInfo.Rows[rows]["cantRiesgo"]);*/
                                        sumIndicadorSARResidual = 0;
                                        countRiesgoResigual = 1;
                                    }
                                    else
                                    {
                                        countRiesgoResigual++;
                                    }
                                }
                                if (countRiesgoCR == Convert.ToInt32(dtInfo.Rows[rows]["cantCR"]))
                                {
                                    double total = (promedioSAR / countRiesgoCR);
                                    InfoGridReporteRiesgosControlesConsolidado.Rows.Add(
                                        new Object[] {
                                            "Riesgo Inherente",
                                            riesgo,
                                            total,
                                            "JI"
                                        }
                                        );

                                    double total2 = (promedioSARresidual / countRiesgoCR);
                                    InfoGridReporteRiesgosControlesConsolidado.Rows.Add(
                                        new Object[] {
                                            "Riesgo Residual",
                                            riesgo,
                                            total2,
                                            "JII"
                                        }
                                        );
                                    countRiesgoCR = 1;
                                }
                                else
                                {
                                    countRiesgoCR++;
                                }
                            }

                            riesgo = dtInfo.Rows[rows]["ClasificacionRiesgo"].ToString().Trim();
                        }

                    }
                    #endregion Recorrido para llenar informacion

                    GridView8.PageIndex = PagIndexInfoGridReporteRiesgosControlesConsolidado;
                    GridView8.DataSource = InfoGridReporteRiesgosControlesConsolidado;
                    GridView8.DataBind();
                    //ReporteControlConsolidado.Visible = true;
                }
                else
                {
                    Mensaje("No hay información consolidada.");
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message);
            }

        }
        #endregion Reporte Riesgos-Controles

        #region Reporte Riesgos-Eventos
        private void loadGridReporteRiesgosEventos()
        {
            #region Grid
            DataTable grid = new DataTable();
            grid.Columns.Add("CodigoRiesgo", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("NombreRiesgo", typeof(string));
            grid.Columns.Add("DescripcionRiesgo", typeof(string));
            grid.Columns.Add("ResponsableRiesgo", typeof(string));
            grid.Columns.Add("FechaRegistroRiesgo", typeof(string));
            grid.Columns.Add("Ubicacion", typeof(string));
            grid.Columns.Add("ClasificacionRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionGeneralRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionParticularRiesgo", typeof(string));
            grid.Columns.Add("FactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("SubFactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("ListaRiesgoAsociadoLA", typeof(string));
            grid.Columns.Add("ListaFactorRiesgoLAFT", typeof(string));
            grid.Columns.Add("TipoEvento", typeof(string));
            grid.Columns.Add("RiesgoAsociadoOperativo", typeof(string));
            grid.Columns.Add("Causas", typeof(string));
            grid.Columns.Add("Consecuencias", typeof(string));
            grid.Columns.Add("CadenaValor", typeof(string));
            grid.Columns.Add("Macroproceso", typeof(string));
            grid.Columns.Add("Proceso", typeof(string));
            grid.Columns.Add("Subproceso", typeof(string));
            grid.Columns.Add("Actividad", typeof(string));
            grid.Columns.Add("ListaTratamiento", typeof(string));
            grid.Columns.Add("FrecuenciaInherente", typeof(string));//
            grid.Columns.Add("CodigoFrecuenciaInherente", typeof(string));//
            grid.Columns.Add("ImpactoInherente", typeof(string));//
            grid.Columns.Add("CodigoImpactoInherente", typeof(string));//
            grid.Columns.Add("RiesgoInherente", typeof(string));
            grid.Columns.Add("CodigoRiesgoInherente", typeof(string));//
            grid.Columns.Add("FrecuenciaResidual", typeof(string));//
            grid.Columns.Add("CodigoFrecuenciaResidual", typeof(string));//
            grid.Columns.Add("ImpactoResidual", typeof(string));//
            grid.Columns.Add("CodigoImpactoResidual", typeof(string));//
            grid.Columns.Add("RiesgoResidual", typeof(string));
            grid.Columns.Add("CodigoRiesgoResidual", typeof(string));//

            grid.Columns.Add("CodigoEvento", typeof(string));
            grid.Columns.Add("DescripcionEvento", typeof(string));
            grid.Columns.Add("ResponsableEvento", typeof(string));
            grid.Columns.Add("FechaRegistroEvento", typeof(string));
            grid.Columns.Add("ProcesoInvolucrado", typeof(string));
            grid.Columns.Add("AplicativoInvolucrado", typeof(string));
            grid.Columns.Add("ServicioProductoAfectado", typeof(string));
            grid.Columns.Add("FechaInicio", typeof(string));
            grid.Columns.Add("FechaFinalizacion", typeof(string));
            grid.Columns.Add("FechaDescubrimiento", typeof(string));
            grid.Columns.Add("CuentaPUC", typeof(string));
            grid.Columns.Add("ValorRecuperadoTotal", typeof(string));
            grid.Columns.Add("ValorRecuperadoSeguro", typeof(string));
            grid.Columns.Add("Observaciones", typeof(string));
            grid.Columns.Add("NombreDepartamento", typeof(string));
            grid.Columns.Add("NombreCiudad", typeof(string));
            grid.Columns.Add("NombreOficinaSucursal", typeof(string));
            grid.Columns.Add("NombreClaseEvento", typeof(string));
            grid.Columns.Add("NombreTipoPerdidaEvento", typeof(string));
            grid.Columns.Add("NombreArea", typeof(string));
            #endregion Grid

            InfoGridReporteRiesgosEventos = grid;
            GridView3.DataSource = InfoGridReporteRiesgosEventos;
            GridView3.DataBind();
        }

        private void loadInfoReporteRiesgosEventos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.ReporteRiesgos(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                DropDownList54.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(), "3", "---",
                DDLareas.SelectedValue.ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido para llenar informacion
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridReporteRiesgosEventos.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["CodigoRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["DescripcionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ResponsableRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistroRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Ubicacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionGeneralRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionParticularRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FactorRiesgoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubFactorRiesgoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaRiesgoAsociadoLA"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaFactorRiesgoLAFT"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoAsociadoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Causas"].ToString().Trim(),
                        dtInfo.Rows[rows]["Consecuencias"].ToString().Trim(),
                        dtInfo.Rows[rows]["CadenaValor"].ToString().Trim(),
                        dtInfo.Rows[rows]["Macroproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Proceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Subproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Actividad"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaTratamiento"].ToString().Trim(),
                        dtInfo.Rows[rows]["FrecuenciaInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoFrecuenciaInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["ImpactoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoImpactoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["FrecuenciaResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoFrecuenciaResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["ImpactoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoImpactoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["DescripcionEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["ResponsableEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistroEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["ProcesoInvolucrado"].ToString().Trim(),
                        dtInfo.Rows[rows]["AplicativoInvolucrado"].ToString().Trim(),
                        dtInfo.Rows[rows]["ServicioProductoAfectado"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaInicio"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaFinalizacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaDescubrimiento"].ToString().Trim(),
                        dtInfo.Rows[rows]["CuentaPUC"].ToString().Trim(),
                        dtInfo.Rows[rows]["ValorRecuperadoTotal"].ToString().Trim(),
                        dtInfo.Rows[rows]["ValorRecuperadoSeguro"].ToString().Trim(),
                        dtInfo.Rows[rows]["Observaciones"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreDepartamento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreCiudad"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreOficinaSucursal"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreClaseEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreTipoPerdidaEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreArea"].ToString().Trim()
                    });
                }
                #endregion Recorrido para llenar informacion

                GridView3.PageIndex = PagIndexInfoGridReporteRiesgosEventos;
                GridView3.DataSource = InfoGridReporteRiesgosEventos;
                GridView3.DataBind();
            }
            else
            {
                loadGridReporteRiesgosEventos();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }
        #endregion Reporte Riesgos-Eventos

        #region Reporte Riesgos-Planes Accion
        private void loadGridReporteRiesgosPlanesAccion()
        {
            #region Grid
            DataTable grid = new DataTable();
            grid.Columns.Add("CodigoRiesgo", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("NombreRiesgo", typeof(string));
            grid.Columns.Add("DescripcionRiesgo", typeof(string));
            grid.Columns.Add("ResponsableRiesgo", typeof(string));
            grid.Columns.Add("FechaRegistroRiesgo", typeof(string));
            grid.Columns.Add("Ubicacion", typeof(string));
            grid.Columns.Add("ClasificacionRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionGeneralRiesgo", typeof(string));
            grid.Columns.Add("ClasificacionParticularRiesgo", typeof(string));
            grid.Columns.Add("FactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("SubFactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("ListaRiesgoAsociadoLA", typeof(string));
            grid.Columns.Add("ListaFactorRiesgoLAFT", typeof(string));
            grid.Columns.Add("TipoEvento", typeof(string));
            grid.Columns.Add("RiesgoAsociadoOperativo", typeof(string));
            grid.Columns.Add("Causas", typeof(string));
            grid.Columns.Add("Consecuencias", typeof(string));
            grid.Columns.Add("CadenaValor", typeof(string));
            grid.Columns.Add("Macroproceso", typeof(string));
            grid.Columns.Add("Proceso", typeof(string));
            grid.Columns.Add("Subproceso", typeof(string));
            grid.Columns.Add("Actividad", typeof(string));
            grid.Columns.Add("ListaTratamiento", typeof(string));
            grid.Columns.Add("FrecuenciaInherente", typeof(string));
            grid.Columns.Add("CodigoFrecuenciaInherente", typeof(string));
            grid.Columns.Add("ImpactoInherente", typeof(string));
            grid.Columns.Add("CodigoImpactoInherente", typeof(string));
            grid.Columns.Add("RiesgoInherente", typeof(string));
            grid.Columns.Add("CodigoRiesgoInherente", typeof(string));
            grid.Columns.Add("FrecuenciaResidual", typeof(string));
            grid.Columns.Add("CodigoFrecuenciaResidual", typeof(string));
            grid.Columns.Add("ImpactoResidual", typeof(string));
            grid.Columns.Add("CodigoImpactoResidual", typeof(string));
            grid.Columns.Add("RiesgoResidual", typeof(string));
            grid.Columns.Add("CodigoRiesgoResidual", typeof(string));
            grid.Columns.Add("DescripcionAccion", typeof(string));
            grid.Columns.Add("NombreTipoRecursoPlanAccion", typeof(string));
            grid.Columns.Add("ValorRecurso", typeof(string));
            grid.Columns.Add("NombreEstadoPlanAccion", typeof(string));
            grid.Columns.Add("FechaCompromiso", typeof(string));
            grid.Columns.Add("ResponsablePlanAccion", typeof(string));
            grid.Columns.Add("NombreArea", typeof(string));
            #endregion Grid

            InfoGridReporteRiesgosPlanesAccion = grid;
            GridView4.DataSource = InfoGridReporteRiesgosPlanesAccion;
            GridView4.DataBind();
        }

        private void loadInfoReporteRiesgosPlanesAccion()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.ReporteRiesgos(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                DropDownList54.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(), "4", "---",
                DDLareas.SelectedValue.ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido de informacion
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridReporteRiesgosPlanesAccion.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["CodigoRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["DescripcionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ResponsableRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaRegistroRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Ubicacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionGeneralRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ClasificacionParticularRiesgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["FactorRiesgoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["SubFactorRiesgoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaRiesgoAsociadoLA"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaFactorRiesgoLAFT"].ToString().Trim(),
                        dtInfo.Rows[rows]["TipoEvento"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoAsociadoOperativo"].ToString().Trim(),
                        dtInfo.Rows[rows]["Causas"].ToString().Trim(),
                        dtInfo.Rows[rows]["Consecuencias"].ToString().Trim(),
                        dtInfo.Rows[rows]["CadenaValor"].ToString().Trim(),
                        dtInfo.Rows[rows]["Macroproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Proceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Subproceso"].ToString().Trim(),
                        dtInfo.Rows[rows]["Actividad"].ToString().Trim(),
                        dtInfo.Rows[rows]["ListaTratamiento"].ToString().Trim(),
                        dtInfo.Rows[rows]["FrecuenciaInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoFrecuenciaInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["ImpactoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoImpactoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgoInherente"].ToString().Trim(),
                        dtInfo.Rows[rows]["FrecuenciaResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoFrecuenciaResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["ImpactoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoImpactoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["RiesgoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["CodigoRiesgoResidual"].ToString().Trim(),
                        dtInfo.Rows[rows]["DescripcionAccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreTipoRecursoPlanAccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["ValorRecurso"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreEstadoPlanAccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["FechaCompromiso"].ToString().Trim(),
                        dtInfo.Rows[rows]["ResponsablePlanAccion"].ToString().Trim(),
                        dtInfo.Rows[rows]["NombreArea"].ToString().Trim()
                        });
                }
                #endregion

                GridView4.PageIndex = PagIndexInfoGridReporteRiesgosPlanesAccion;
                GridView4.DataSource = InfoGridReporteRiesgosPlanesAccion;
                GridView4.DataBind();
            }
            else
            {
                loadGridReporteRiesgosPlanesAccion();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }
        #endregion Reporte Riesgos-Planes Accion

        #region Modificacion Control
        /// <summary>
        /// Metodo para cargar el grid del reporte de Cambios a los controles
        /// </summary>
        private void mtdCargarGridReporteModControl()
        {
            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("FechaModificacion", typeof(string));
            dtGrid.Columns.Add("CodigoControl", typeof(string));
            dtGrid.Columns.Add("NombreControl", typeof(string));
            dtGrid.Columns.Add("DescripcionControl", typeof(string));
            dtGrid.Columns.Add("ResponsableControl", typeof(string));
            dtGrid.Columns.Add("FechaRegistroControl", typeof(string));
            dtGrid.Columns.Add("NombrePeriodicidad", typeof(string));
            dtGrid.Columns.Add("NombreTest", typeof(string));
            /*dtGrid.Columns.Add("NombreClaseControl", typeof(string));
            dtGrid.Columns.Add("NombreTipoControl", typeof(string));
            dtGrid.Columns.Add("NombreResponsableExperiencia", typeof(string));
            dtGrid.Columns.Add("NombreDocumentacion", typeof(string));
            dtGrid.Columns.Add("NombreResponsabilidad", typeof(string));*/
            dtGrid.Columns.Add("NombreVariable", typeof(string));
            dtGrid.Columns.Add("NombreCategoria", typeof(string));
            dtGrid.Columns.Add("NombreEscala", typeof(string));
            dtGrid.Columns.Add("NombreMitiga", typeof(string));
            dtGrid.Columns.Add("NombreUsuarioCambio", typeof(string));
            dtGrid.Columns.Add("JustificacionCambio", typeof(string));
            //dtGrid.Columns.Add("UsuarioCambio", typeof(string));

            DtInfoGridReporteModControl = dtGrid;
            GridView5.DataSource = DtInfoGridReporteModControl;
            GridView5.DataBind();
        }

        private void mtdCargarInfoReporteModControl()
        {
            DataTable dtInfo = new DataTable();

            dtInfo = cRiesgo.mtdReporteCambiosControlRiesgo("1", Sanitizer.GetSafeHtmlFragment(TxtFechaIni.Text), Sanitizer.GetSafeHtmlFragment(TxtFechaFin.Text), DDLareas.SelectedValue.ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido para llenar el Grid
                foreach (DataRow dr in dtInfo.Rows)
                {
                    DtInfoGridReporteModControl.Rows.Add(new Object[] {
                        dr["FechaModificacion"].ToString().Trim(),
                        dr["CodigoControl"].ToString().Trim(),
                        dr["NombreControl"].ToString().Trim(),
                        dr["DescripcionControl"].ToString().Trim(),
                        dr["ResponsableControl"].ToString().Trim(),
                        dr["FechaRegistroControl"].ToString().Trim(),
                        dr["NombrePeriodicidad"].ToString().Trim(),
                        dr["NombreTest"].ToString().Trim(),
                        /*dr["NombreClaseControl"].ToString().Trim(),
                        dr["NombreTipoControl"].ToString().Trim(),
                        dr["NombreResponsableExperiencia"].ToString().Trim(),
                        dr["NombreDocumentacion"].ToString().Trim(),
                        dr["NombreResponsabilidad"].ToString().Trim(),*/
                        dr["NombreVariable"].ToString().Trim(),
                        dr["NombreCategoria"].ToString().Trim(),
                        dr["NombreEscala"].ToString().Trim(),
                        dr["NombreMitiga"].ToString().Trim(),
                        dr["NombreUsuarioCambio"].ToString().Trim(),
                        dr["JustificacionCambio"].ToString().Trim()});
                }
                #endregion Recorrido para llenar el Grid

                GridView5.PageIndex = PagIndexInfoGridReporteModControl;
                GridView5.DataSource = DtInfoGridReporteModControl;
                GridView5.DataBind();
            }
            else
            {
                mtdCargarGridReporteModControl();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }
        #endregion Modificacion Control

        #region Modificacion Riesgo
        private void mtdCargarGridReporteModRiesgo()
        {
            DataTable dtGrid = new DataTable();

            #region Columnas GRID
            dtGrid.Columns.Add("FechaModificacion", typeof(string));
            dtGrid.Columns.Add("CodigoRiesgo", typeof(string));
            dtGrid.Columns.Add("NombreRiesgo", typeof(string));
            dtGrid.Columns.Add("DescripcionRiesgo", typeof(string));
            dtGrid.Columns.Add("ResponsableRiesgo", typeof(string));
            dtGrid.Columns.Add("FechaRegistroRiesgo", typeof(string));
            dtGrid.Columns.Add("Ubicacion", typeof(string));
            dtGrid.Columns.Add("ClasificacionRiesgo", typeof(string));
            dtGrid.Columns.Add("ClasificacionGeneralRiesgo", typeof(string));
            dtGrid.Columns.Add("ClasificacionParticularRiesgo", typeof(string));
            dtGrid.Columns.Add("FactorRiesgoOperativo", typeof(string));
            dtGrid.Columns.Add("SubFactorRiesgoOperativo", typeof(string));
            dtGrid.Columns.Add("ListaRiesgoAsociadoLA", typeof(string));
            dtGrid.Columns.Add("ListaFactorRiesgoLAFT", typeof(string));
            dtGrid.Columns.Add("TipoEvento", typeof(string));
            dtGrid.Columns.Add("RiesgoAsociadoOperativo", typeof(string));
            dtGrid.Columns.Add("Causas", typeof(string));
            dtGrid.Columns.Add("Consecuencias", typeof(string));
            dtGrid.Columns.Add("CadenaValor", typeof(string));
            dtGrid.Columns.Add("Macroproceso", typeof(string));
            dtGrid.Columns.Add("Proceso", typeof(string));
            dtGrid.Columns.Add("Subproceso", typeof(string));
            dtGrid.Columns.Add("Actividad", typeof(string));
            dtGrid.Columns.Add("ListaTratamiento", typeof(string));
            dtGrid.Columns.Add("FrecuenciaInherente", typeof(string));
            dtGrid.Columns.Add("CodigoFrecuenciaInherente", typeof(string));
            dtGrid.Columns.Add("ImpactoInherente", typeof(string));
            dtGrid.Columns.Add("CodigoImpactoInherente", typeof(string));
            dtGrid.Columns.Add("RiesgoInherente", typeof(string));
            dtGrid.Columns.Add("CodigoRiesgoInherente", typeof(string));
            dtGrid.Columns.Add("FrecuenciaResidual", typeof(string));
            dtGrid.Columns.Add("CodigoFrecuenciaResidual", typeof(string));
            dtGrid.Columns.Add("ImpactoResidual", typeof(string));
            dtGrid.Columns.Add("CodigoImpactoResidual", typeof(string));
            dtGrid.Columns.Add("RiesgoResidual", typeof(string));
            dtGrid.Columns.Add("CodigoRiesgoResidual", typeof(string));
            dtGrid.Columns.Add("NombreUsuarioCambio", typeof(string));
            dtGrid.Columns.Add("JustificacionCambio", typeof(string));
            dtGrid.Columns.Add("NombreArea", typeof(string));
            #endregion Columnas GRID

            DtInfoGridReporteModRiesgo = dtGrid;
            GridView6.DataSource = DtInfoGridReporteModRiesgo;
            GridView6.DataBind();
        }

        private void mtdCargarInfoReporteModRiesgo()
        {
            DataTable dtInfo = new DataTable();

            dtInfo = cRiesgo.mtdReporteCambiosControlRiesgo("2", Sanitizer.GetSafeHtmlFragment(TxtFechaIni.Text), Sanitizer.GetSafeHtmlFragment(TxtFechaFin.Text), DDLareas.SelectedValue.ToString().Trim());

            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido para llenar el Grid
                foreach (DataRow dr in dtInfo.Rows)
                {
                    #region Info GRID
                    DtInfoGridReporteModRiesgo.Rows.Add(new Object[] {
                        dr["FechaModificacion"].ToString().Trim(),
                        dr["CodigoRiesgo"].ToString().Trim(),
                        dr["NombreRiesgo"].ToString().Trim(),
                        dr["DescripcionRiesgo"].ToString().Trim(),
                        dr["ResponsableRiesgo"].ToString().Trim(),
                        dr["FechaRegistroRiesgo"].ToString().Trim(),
                        dr["Ubicacion"].ToString().Trim(),
                        dr["ClasificacionRiesgo"].ToString().Trim(),
                        dr["ClasificacionGeneralRiesgo"].ToString().Trim(),
                        dr["ClasificacionParticularRiesgo"].ToString().Trim(),
                        dr["FactorRiesgoOperativo"].ToString().Trim(),
                        dr["SubFactorRiesgoOperativo"].ToString().Trim(),
                        dr["ListaRiesgoAsociadoLA"].ToString().Trim(),
                        dr["ListaFactorRiesgoLAFT"].ToString().Trim(),
                        dr["TipoEvento"].ToString().Trim(),
                        dr["RiesgoAsociadoOperativo"].ToString().Trim(),
                        dr["Causas"].ToString().Trim(),
                        dr["Consecuencias"].ToString().Trim(),
                        dr["CadenaValor"].ToString().Trim(),
                        dr["Macroproceso"].ToString().Trim(),
                        dr["Proceso"].ToString().Trim(),
                        dr["Subproceso"].ToString().Trim(),
                        dr["Actividad"].ToString().Trim(),
                        dr["ListaTratamiento"].ToString().Trim(),
                        dr["FrecuenciaInherente"].ToString().Trim(),
                        dr["CodigoFrecuenciaInherente"].ToString().Trim(),
                        dr["ImpactoInherente"].ToString().Trim(),
                        dr["CodigoImpactoInherente"].ToString().Trim(),
                        dr["RiesgoInherente"].ToString().Trim(),
                        dr["CodigoRiesgoInherente"].ToString().Trim(),
                        dr["FrecuenciaResidual"].ToString().Trim(),
                        dr["CodigoFrecuenciaResidual"].ToString().Trim(),
                        dr["ImpactoResidual"].ToString().Trim(),
                        dr["CodigoImpactoResidual"].ToString().Trim(),
                        dr["RiesgoResidual"].ToString().Trim(),
                        dr["CodigoRiesgoResidual"].ToString().Trim(),
                        dr["NombreUsuarioCambio"].ToString().Trim(),
                        dr["JustificacionCambio"].ToString().Trim(),
                        dr["NombreArea"].ToString().Trim()
                       });
                    #endregion Info GRID
                }
                #endregion Recorrido para llenar el Grid

                GridView6.PageIndex = PagIndexInfoGridReporteModRiesgo;
                GridView6.DataSource = DtInfoGridReporteModRiesgo;
                GridView6.DataBind();
            }
            else
            {
                mtdCargarGridReporteModRiesgo();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }
        #endregion Modificacion Riesgo

        #region Reporte Controles
        private void mtdCargarGridReporteControles()
        {
            DataTable dtGrid = new DataTable();

            dtGrid.Columns.Add("CodigoControl", typeof(string));
            dtGrid.Columns.Add("NombreControl", typeof(string));
            dtGrid.Columns.Add("DescripcionControl", typeof(string));
            dtGrid.Columns.Add("ObjetivoControl", typeof(string));
            dtGrid.Columns.Add("ResponsableEjecucion", typeof(string));
            dtGrid.Columns.Add("ResponsableCalificacion", typeof(string));
            dtGrid.Columns.Add("FechaRegistroControl", typeof(string));
            dtGrid.Columns.Add("NombrePeriodicidad", typeof(string));
            dtGrid.Columns.Add("NombreTest", typeof(string));
            dtGrid.Columns.Add("Variable1", typeof(string));
            dtGrid.Columns.Add("Variable2", typeof(string));
            dtGrid.Columns.Add("Variable3", typeof(string));
            dtGrid.Columns.Add("Variable4", typeof(string));
            dtGrid.Columns.Add("Variable5", typeof(string));
            dtGrid.Columns.Add("Variable6", typeof(string));
            dtGrid.Columns.Add("Variable7", typeof(string));
            dtGrid.Columns.Add("Variable8", typeof(string));
            dtGrid.Columns.Add("Variable9", typeof(string));
            dtGrid.Columns.Add("Variable10", typeof(string));
            dtGrid.Columns.Add("Variable11", typeof(string));
            dtGrid.Columns.Add("Variable12", typeof(string));
            dtGrid.Columns.Add("Variable13", typeof(string));
            dtGrid.Columns.Add("Variable14", typeof(string));
            dtGrid.Columns.Add("Variable15", typeof(string));


            //dtGrid.Columns.Add("NombreVariable", typeof(string));
            //dtGrid.Columns.Add("NombreCategoria", typeof(string));

            dtGrid.Columns.Add("NombreEscala", typeof(string));
            dtGrid.Columns.Add("NombreMitiga", typeof(string));
            dtGrid.Columns.Add("DesviacionImpacto", typeof(string));
            dtGrid.Columns.Add("DesviacionFrecuencia", typeof(string));

            InfoGridReporteControles = dtGrid;
            GridView7.DataSource = InfoGridReporteControles;
            GridView7.DataBind();
        }
        private void mtdLoadGridReporteCausassinControles()
        {
            DataTable dtGrid = new DataTable();

            dtGrid.Columns.Add("NombreCausas", typeof(string));
            dtGrid.Columns.Add("CodigoRiesgo", typeof(string));
            dtGrid.Columns.Add("NombreRiesgo", typeof(string));
            dtGrid.Columns.Add("Descripcion", typeof(string));
            dtGrid.Columns.Add("RiesgoInherente", typeof(string));
            dtGrid.Columns.Add("RiesgoResidual", typeof(string));
            dtGrid.Columns.Add("CodigoEvento", typeof(string));
            dtGrid.Columns.Add("DescripcionEvento", typeof(string));
            dtGrid.Columns.Add("NombreArea", typeof(string));

            InfoGridReporteCausaSinControl = dtGrid;
            GVcausasControl.DataSource = InfoGridReporteCausaSinControl;
            GVcausasControl.DataBind();
        }
        private void mtdCargarInfoReporteControles()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                string ResponsableEjecucion = string.Empty;
                dtInfo = cRiesgo.mtdReporteControles(DropDownList52.SelectedValue.ToString().Trim(), DropDownList53.SelectedValue.ToString().Trim(),
                    DropDownList54.SelectedValue.ToString().Trim(), DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                    DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim());
                if (dtInfo.Rows.Count > 0)
                {
                    #region Recorrido para llenar el Grid
                    foreach (DataRow dr in dtInfo.Rows)
                    {

                        InfoGridReporteControles.Rows.Add(new Object[] {
                        dr["CodigoControl"].ToString().Trim(),
                        dr["NombreControl"].ToString().Trim(),
                        dr["DescripcionControl"].ToString().Trim(),
                        dr["ObjetivoControl"].ToString().Trim(),
                        ResponsableEjecucion = mtdBuscarNombresRespEjecucion(dr["ResponsableEjecucion"].ToString().Trim()),
                        dr["ResponsableCalificacion"].ToString().Trim(),
                        dr["FechaRegistroControl"].ToString().Trim(),
                        dr["NombrePeriodicidad"].ToString().Trim(),
                        dr["NombreTest"].ToString().Trim(),
                        dr["Variable1"].ToString().Trim(),
                        dr["Variable2"].ToString().Trim(),
                        dr["Variable3"].ToString().Trim(),
                        dr["Variable4"].ToString().Trim(),
                        dr["Variable5"].ToString().Trim(),
                        dr["Variable6"].ToString().Trim(),
                        dr["Variable7"].ToString().Trim(),
                        dr["Variable8"].ToString().Trim(),
                        dr["Variable9"].ToString().Trim(),
                        dr["Variable10"].ToString().Trim(),
                        dr["Variable11"].ToString().Trim(),
                        dr["Variable12"].ToString().Trim(),
                        dr["Variable13"].ToString().Trim(),
                        dr["Variable14"].ToString().Trim(),
                        dr["Variable15"].ToString().Trim(),
                        //dr["NombreVariable"].ToString().Trim(),
                        //dr["NombreCategoria"].ToString().Trim(),
                        dr["NombreEscala"].ToString().Trim(),
                        dr["NombreMitiga"].ToString().Trim(),
                        dr["DesviacionImpacto"].ToString().Trim(),
                        dr["DesviacionFrecuencia"].ToString().Trim()
                    });
                    }
                    #endregion Recorrido para llenar el Grid

                    GridView7.PageIndex = PagIndexInfoGridReporteControles;
                    GridView7.DataSource = InfoGridReporteControles;
                    GridView7.DataBind();
                }
                else
                {
                    mtdCargarGridReporteControles();
                    Mensaje("No existen registros asociados a los parámetros de consulta.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void mtdLoadInfoGridReporteCausassinControles()
        {
            DataTable dtInfo = new DataTable();
            string ResponsableEjecucion = string.Empty;
            string strErrMsg = string.Empty;
            /*dtInfo = cRiesgo.ReporteRiesgosCausasSinControl(DropDownList52.SelectedValue.ToString().Trim(),
                DropDownList53.SelectedValue.ToString().Trim(), DropDownList54.SelectedValue.ToString().Trim(),
                DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(),
                DropDownList4.SelectedValue.ToString().Trim(), "---", DDLareas.SelectedValue.ToString().Trim());*/
            clsBLLReporteCausasSinControl cCausassincontrol = new clsBLLReporteCausasSinControl();
            dtInfo = cCausassincontrol.mtdConsultarCausasSinControl(ref strErrMsg, DropDownList52.SelectedValue.ToString().Trim(),
                DropDownList53.SelectedValue.ToString().Trim(), DropDownList54.SelectedValue.ToString().Trim(),
                DropDownList56.SelectedValue.ToString().Trim(), DropDownList57.SelectedValue.ToString().Trim(),
                DropDownList2.SelectedValue.ToString().Trim(), DropDownList3.SelectedValue.ToString().Trim(),
                DropDownList4.SelectedValue.ToString().Trim(), "---", DDLareas.SelectedValue.ToString().Trim());
            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido para llenar el Grid
                foreach (DataRow dr in dtInfo.Rows)
                {
                    InfoGridReporteCausaSinControl.Rows.Add(new Object[] {
                        dr["NombreCausas"].ToString().Trim(),
                        dr["CodigoRiesgo"].ToString().Trim(),
                        dr["NombreRiesgo"].ToString().Trim(),
                        dr["Descripcion"].ToString().Trim(),
                        dr["RiesgoInherente"].ToString().Trim(),
                        dr["RiesgoResidual"].ToString().Trim(),
                        dr["CodigoEvento"].ToString().Trim(),
                        dr["DescripcionEvento"].ToString().Trim(),
                        dr["NombreArea"].ToString().Trim()
                    });

                }
                #endregion Recorrido para llenar el Grid

                GVcausasControl.PageIndex = PagIndexReporteCausaSinControl;
                GVcausasControl.DataSource = InfoGridReporteCausaSinControl;
                GVcausasControl.DataBind();
            }
            else
            {
                mtdCargarGridReporteControles();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }

        }
        #endregion Reporte Controles

        private string mtdBuscarNombresRespEjecucion(string IdResponsableEjecucion)
        {

            try
            {
                string NombresResponsablesEjecucion = string.Empty;
                string[] srtSeparator = new string[] { "|" };
                string[] arrNombres = IdResponsableEjecucion.Split(srtSeparator, StringSplitOptions.None);
                string IdNombre = string.Empty;
                int i = arrNombres.Length;

                if (IdResponsableEjecucion != string.Empty)
                {
                    int a = -1;
                    for (int j = 0; j < i; j++)
                    {
                        //Heber Jessid Correal 05/04/2017 Se valida que tenga 3 o mas caracteres para poder remover 3 caracteres
                        if (arrNombres[j].Length >= 3)
                        {
                            //Heber Jessid Correal 05/04/2017 Se controla que el valor enviado al metodo sea número.
                            if (int.TryParse(arrNombres[j].Remove(0, 3), out a))
                            {
                                if (arrNombres[j].Contains("JO"))
                                    NombresResponsablesEjecucion += cControl.NombreJerarquia(arrNombres[j].Remove(0, 3));
                                else if (arrNombres[j].Contains("GT"))
                                    NombresResponsablesEjecucion += cControl.NombreGrupoTrabajo(arrNombres[j].Remove(0, 3));
                            }
                        }
                    }
                }
                return NombresResponsablesEjecucion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            exportExcel(InfoGridReporteCausaSinControl, Response, "Reporte Causas sin control", 8);
        }

        protected void GVcausasControl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexReporteCausaSinControl = e.NewPageIndex;
            GVcausasControl.PageIndex = PagIndexReporteCausaSinControl;
            GVcausasControl.DataSource = InfoGridReporteCausaSinControl;
            GVcausasControl.DataBind();
        }
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
        private DataTable infoGridRiesgoIndicadorResidual;
        private int rowGridRiesgoIndicadorResidual;
        private int pagIndexRiesgoIndicadorResidual;

        private DataTable InfoGridRiesgoIndicadorResidual
        {
            get
            {
                infoGridRiesgoIndicadorResidual = (DataTable)ViewState["infoGridRiesgoIndicadorResidual"];
                return infoGridRiesgoIndicadorResidual;
            }
            set
            {
                infoGridRiesgoIndicadorResidual = value;
                ViewState["infoGridRiesgoIndicadorResidual"] = infoGridRiesgoIndicadorResidual;
            }
        }

        private int RowGridRiesgoIndicadorResidual
        {
            get
            {
                rowGridRiesgoIndicadorResidual = (int)ViewState["rowGridRiesgoIndicadorResidual"];
                return rowGridRiesgoIndicadorResidual;
            }
            set
            {
                rowGridRiesgoIndicadorResidual = value;
                ViewState["rowGridRiesgoIndicadorResidual"] = rowGridRiesgoIndicadorResidual;
            }
        }

        private int PagIndexRiesgoIndicadorResidual
        {
            get
            {
                pagIndexRiesgoIndicadorResidual = (int)ViewState["pagIndexRiesgoIndicadorResidual"];
                return pagIndexRiesgoIndicadorResidual;
            }
            set
            {
                pagIndexRiesgoIndicadorResidual = value;
                ViewState["pagIndexRiesgoIndicadorResidual"] = pagIndexRiesgoIndicadorResidual;
            }
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadPerfilRiesgo()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("strClasificacionRiesgo", typeof(string));
            grid.Columns.Add("strRiesgoInherente", typeof(string));
            grid.Columns.Add("intCantRiesgoInherente", typeof(string));
            grid.Columns.Add("strRiesgoResidual", typeof(string));
            grid.Columns.Add("intCantRiesgoResidual", typeof(string));
            grid.Columns.Add("decDecValoracion", typeof(string));

            GVcomparativo.DataSource = grid;
            GVcomparativo.DataBind();
            InfoGridRiesgoIndicador = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstRiesgoInd">Lista con los perfiles de riesgos</param>
        private void mtdLoadPerfilRiesgo(List<clsDTOCuadroComparativoRiesgos> lstComparativo)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOCuadroComparativoRiesgos objComparativo in lstComparativo)
            {
                if (objComparativo.strRiesgoInherente != null)
                {
                    InfoGridRiesgoIndicador.Rows.Add(new Object[] {
                    objComparativo.strClasificacionRiesgo.ToString().Trim(),
                    objComparativo.strRiesgoInherente.ToString().Trim(),
                    objComparativo.intCantRiesgoInherente.ToString().Trim(),
                    objComparativo.strRiesgoResidual.ToString().Trim(),
                    objComparativo.intCantRiesgoResidual.ToString().Trim(),
                    objComparativo.decDecValoracion.ToString().Trim()
                    });
                }

            }
        }

        private void mtdLoadCuadroComparativoResidual()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("strClasificacionRiesgo", typeof(string));
            grid.Columns.Add("strRiesgoInherente", typeof(string));
            grid.Columns.Add("intCantRiesgoInherente", typeof(string));
            grid.Columns.Add("strRiesgoResidual", typeof(string));
            grid.Columns.Add("intCantRiesgoResidual", typeof(string));
            grid.Columns.Add("decDecValoracion", typeof(string));

            GVcomparativoResidual.DataSource = grid;
            GVcomparativoResidual.DataBind();
            InfoGridRiesgoIndicadorResidual = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstRiesgoInd">Lista con los perfiles de riesgos</param>
        private void mtdLoadCuadroComparativo(List<clsDTOCuadroComparativoRiesgos> lstComparativo)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOCuadroComparativoRiesgos objComparativo in lstComparativo)
            {
                if (objComparativo.strRiesgoInherente != null)
                {
                    InfoGridRiesgoIndicador.Rows.Add(new Object[] {
                    objComparativo.strClasificacionRiesgo.ToString().Trim(),
                    objComparativo.strRiesgoInherente.ToString().Trim(),
                    objComparativo.intCantRiesgoInherente.ToString().Trim(),
                    objComparativo.strRiesgoResidual.ToString().Trim(),
                    objComparativo.intCantRiesgoResidual.ToString().Trim(),
                    objComparativo.decDecValoracion.ToString().Trim()
                    });
                }

            }
        }
        private void mtdLoadRiesgoControl()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("strClasificacionRiesgo", typeof(string));
            grid.Columns.Add("strRiesgoInherente", typeof(string));
            grid.Columns.Add("intCantRiesgoInherente", typeof(string));

            gvRiesgoControlInherente.DataSource = grid;
            gvRiesgoControlInherente.DataBind();
            InfoGridRiesgoIndicador = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstRiesgoInd">Lista con los perfiles de riesgos</param>
        private void mtdLoadRiesgoControl(List<clsDTOReportRiesgoControl> lstComparativo)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOReportRiesgoControl objComparativo in lstComparativo)
            {
                if (objComparativo.strClasificacionRiesgo != null)
                {
                    InfoGridRiesgoIndicador.Rows.Add(new Object[] {
                    objComparativo.strClasificacionRiesgo.ToString().Trim(),
                    objComparativo.strRiesgoInherente.ToString().Trim(),
                    objComparativo.intCantRiesgoInherente.ToString().Trim()
                    });
                }

            }
        }

        private void mtdLoadRiesgoControlResidual()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("strClasificacionRiesgo", typeof(string));
            grid.Columns.Add("strRiesgoResidual", typeof(string));
            grid.Columns.Add("intCantRiesgoResidual", typeof(string));

            gvRiesgoControlResidual.DataSource = grid;
            gvRiesgoControlResidual.DataBind();
            InfoGridRiesgoIndicadorResidual = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstRiesgoInd">Lista con los perfiles de riesgos</param>
        private void mtdLoadRiesgoControlResidual(List<clsDTOReportRiesgoControl> lstComparativo)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOReportRiesgoControl objComparativo in lstComparativo)
            {
                if (objComparativo.strClasificacionRiesgo != null)
                {
                    InfoGridRiesgoIndicadorResidual.Rows.Add(new Object[] {
                    objComparativo.strClasificacionRiesgo.ToString().Trim(),
                    objComparativo.strRiesgoResidual.ToString().Trim(),
                    objComparativo.intCantRiesgoResidual.ToString().Trim()
                    });
                }

            }
        }
        private void mtdLoadCuadroComparativoResidual(List<clsDTOCuadroComparativoRiesgos> lstComparativo)
        {
            string strErrMsg = String.Empty;

            foreach (clsDTOCuadroComparativoRiesgos objComparativo in lstComparativo)
            {
                if (objComparativo.strRiesgoInherente != null)
                {
                    InfoGridRiesgoIndicadorResidual.Rows.Add(new Object[] {
                    objComparativo.strClasificacionRiesgo.ToString().Trim(),
                    objComparativo.strRiesgoInherente.ToString().Trim(),
                    objComparativo.intCantRiesgoInherente.ToString().Trim(),
                    objComparativo.strRiesgoResidual.ToString().Trim(),
                    objComparativo.intCantRiesgoResidual.ToString().Trim(),
                    objComparativo.decDecValoracion.ToString().Trim()
                    });
                }

            }
        }
        public bool mtdLoadCuadroComparativo(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsDTOCuadroComparativoRiesgos> lstComparativo = new List<clsDTOCuadroComparativoRiesgos>();
            clsBLLReportePerfilRiesgo cComparativo = new clsBLLReportePerfilRiesgo();
            try
            {
                lstComparativo = cComparativo.mtdConsultarCuadroComperativo(booResult, ref strErrMsg, 2);
                if (lstComparativo != null)
                {
                    mtdLoadPerfilRiesgo();
                    mtdLoadPerfilRiesgo(lstComparativo);
                    GVcomparativo.DataSource = lstComparativo;
                    GVcomparativo.PageIndex = pagIndexRiesgoIndicador;
                    GVcomparativo.DataBind();
                    booResult = true;
                }
                lstComparativo = cComparativo.mtdConsultarCuadroComperativoResidual(booResult, ref strErrMsg, 3);
                if (lstComparativo != null)
                {
                    mtdLoadCuadroComparativoResidual();
                    mtdLoadCuadroComparativoResidual(lstComparativo);
                    GVcomparativoResidual.DataSource = lstComparativo;
                    GVcomparativoResidual.PageIndex = pagIndexRiesgoIndicador;
                    GVcomparativoResidual.DataBind();
                    booResult = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error:" + ex.Message);
                //omb.ShowMessage("Error en la generación del reporte: " + ex, 2, "Atención");
            }

            return booResult;
        }
        public bool mtdLoadRepoRiesgosControl(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsDTOReportRiesgoControl> lstComparativo = new List<clsDTOReportRiesgoControl>();
            clsBLLReportePerfilRiesgo cComparativo = new clsBLLReportePerfilRiesgo();
            try
            {
                lstComparativo = cComparativo.mtdConsultarRiesgoControl(booResult, ref strErrMsg, 4);
                if (lstComparativo != null)
                {
                    mtdLoadRiesgoControl();
                    mtdLoadRiesgoControl(lstComparativo);
                    gvRiesgoControlInherente.DataSource = lstComparativo;
                    gvRiesgoControlInherente.PageIndex = pagIndexRiesgoIndicador;
                    gvRiesgoControlInherente.DataBind();
                    booResult = true;
                }
                lstComparativo = cComparativo.mtdConsultarRiesgoControlResidual(booResult, ref strErrMsg, 5);
                if (lstComparativo != null)
                {
                    mtdLoadRiesgoControlResidual();
                    mtdLoadRiesgoControlResidual(lstComparativo);
                    gvRiesgoControlResidual.DataSource = lstComparativo;
                    gvRiesgoControlResidual.PageIndex = pagIndexRiesgoIndicador;
                    gvRiesgoControlResidual.DataBind();
                    booResult = true;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error:" + ex.Message);
                //omb.ShowMessage("Error en la generación del reporte: " + ex, 2, "Atención");
            }

            return booResult;
        }
        protected void GVcomparativo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexRiesgoIndicador = e.NewPageIndex;
            GVcomparativo.PageIndex = PagIndexRiesgoIndicador;
            GVcomparativo.DataSource = InfoGridRiesgoIndicador;
            GVcomparativo.DataBind();
        }
        int cantRiegosTotal = 0;
        string Clasificacion = "";
        int rowIndex = 1;
        decimal totalValoracion = 0;
        int cantRiegosTotalR = 0;
        string ClasificacionR = "";
        int rowIndexR = 1;
        decimal totalValoracionR = 0;
        protected void GVcomparativo_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool newRow = false;
            if ((Clasificacion != "") && (DataBinder.Eval(e.Row.DataItem, "strClasificacionRiesgo") != null))
            {
                if (Clasificacion != DataBinder.Eval(e.Row.DataItem, "strClasificacionRiesgo").ToString())
                    newRow = true;
            }
            if ((Clasificacion != "") && (DataBinder.Eval(e.Row.DataItem, "strClasificacionRiesgo") == null))
            {
                newRow = true;
                rowIndex = 0;
            }
            if (newRow)
            {
                if (Clasificacion != "")
                {
                    GridView GridView1 = (GridView)sender;
                    GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    NewTotalRow.Font.Bold = true;
                    NewTotalRow.BackColor = System.Drawing.Color.Gray;
                    NewTotalRow.ForeColor = System.Drawing.Color.White;
                    TableCell HeaderCell = new TableCell();
                    HeaderCell.Text = "Total Riesgo";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Left;
                    HeaderCell.ColumnSpan = 3;
                    NewTotalRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                    HeaderCell.Text = cantRiegosTotal.ToString();
                    NewTotalRow.Cells.Add(HeaderCell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
                    if (e.Row.RowIndex != -1)
                        rowIndex++;
                    NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    NewTotalRow.Font.Bold = true;
                    NewTotalRow.BackColor = System.Drawing.Color.White;
                    NewTotalRow.ForeColor = System.Drawing.Color.Gray;
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Perfil de Riesgo";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Left;
                    HeaderCell.ColumnSpan = 3;
                    NewTotalRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                    HeaderCell.Text = (Math.Round(Convert.ToDecimal(cantRiegosTotal) / totalValoracion, 2)).ToString();
                    NewTotalRow.Cells.Add(HeaderCell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
                    if (e.Row.RowIndex != -1)
                        rowIndex++;
                    cantRiegosTotal = 0;
                }

            }
        }

        protected void GVcomparativo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Clasificacion = DataBinder.Eval(e.Row.DataItem, "strClasificacionRiesgo").ToString();
                int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "intCantRiesgoInherente").ToString());
                decimal tmpValoracion = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "decDecValoracion").ToString());
                cantRiegosTotal += tmpTotal;
                totalValoracion += tmpValoracion;
            }
            else
                Clasificacion = "";
        }
        protected void GVcomparativoResidual_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexRiesgoIndicadorResidual = e.NewPageIndex;
            GVcomparativoResidual.PageIndex = PagIndexRiesgoIndicadorResidual;
            GVcomparativoResidual.DataSource = InfoGridRiesgoIndicadorResidual;
            GVcomparativoResidual.DataBind();
        }

        protected void GVcomparativoResidual_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ClasificacionR = DataBinder.Eval(e.Row.DataItem, "strClasificacionRiesgo").ToString();
                int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "intCantRiesgoResidual").ToString());
                decimal tmpValoracion = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "decDecValoracion").ToString());
                cantRiegosTotalR += tmpTotal;
                totalValoracionR += tmpValoracion;
            }
            else
                ClasificacionR = "";
        }

        protected void GVcomparativoResidual_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool newRow = false;
            if ((ClasificacionR != "") && (DataBinder.Eval(e.Row.DataItem, "strClasificacionRiesgo") != null))
            {
                if (ClasificacionR != DataBinder.Eval(e.Row.DataItem, "strClasificacionRiesgo").ToString())
                    newRow = true;
            }
            if ((ClasificacionR != "") && (DataBinder.Eval(e.Row.DataItem, "strClasificacionRiesgo") == null))
            {
                newRow = true;
                rowIndexR = 0;
            }
            if (newRow)
            {
                if (ClasificacionR != "")
                {
                    GridView GridView1 = (GridView)sender;
                    GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    NewTotalRow.Font.Bold = true;
                    NewTotalRow.BackColor = System.Drawing.Color.Gray;
                    NewTotalRow.ForeColor = System.Drawing.Color.White;
                    TableCell HeaderCell = new TableCell();
                    HeaderCell.Text = "Total Riesgo";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Left;
                    HeaderCell.ColumnSpan = 3;
                    NewTotalRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                    HeaderCell.Text = cantRiegosTotalR.ToString();
                    NewTotalRow.Cells.Add(HeaderCell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndexR, NewTotalRow);
                    if (e.Row.RowIndex != -1)
                        rowIndexR++;
                    NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    NewTotalRow.Font.Bold = true;
                    NewTotalRow.BackColor = System.Drawing.Color.White;
                    NewTotalRow.ForeColor = System.Drawing.Color.Gray;
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Perfil de Riesgo";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Left;
                    HeaderCell.ColumnSpan = 3;
                    NewTotalRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
                    HeaderCell.Text = (Math.Round(Convert.ToDecimal(cantRiegosTotalR) / totalValoracionR, 2)).ToString();
                    NewTotalRow.Cells.Add(HeaderCell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndexR, NewTotalRow);
                    if (e.Row.RowIndex != -1)
                        rowIndexR++;
                    cantRiegosTotalR = 0;
                }

            }
        }

        protected void btnExportPerfilRiesgo_Click(object sender, EventArgs e)
        {
            if(InfoGridRiesgoIndicador.Rows.Count > 0 && InfoGridRiesgoIndicadorResidual.Rows.Count > 0)
            {
                XLWorkbook workbook = new XLWorkbook();
                var ws = workbook.Worksheets.Add("Perfil de riesgo");
                //***********FORMATO DE TABLA************************/
                ws.Range("A1:B1").Style.Border.SetBottomBorder(XLBorderStyleValues.Dotted);
                ws.Range("A1:B1").Style.Border.SetInsideBorder(XLBorderStyleValues.Dotted);
                ws.Range("A1:B1").Style.Border.SetLeftBorder(XLBorderStyleValues.Dotted);
                ws.Range("A1:B1").Style.Border.SetRightBorder(XLBorderStyleValues.Dotted);
                ws.Range("A1:B1").Style.Border.SetOutsideBorder(XLBorderStyleValues.Dotted);
                ws.Range("A1:B1").Style
                 .Font.SetFontSize(12)
                 .Font.SetBold(true)
                 .Font.SetFontColor(XLColor.White)
                 .Fill.SetBackgroundColor(XLColor.DarkBlue);
                int indexRow = 2;
                ws.Cell("A" + 1).Value = "Perfil";
                ws.Cell("B" + 1).Value = "Valor";

                ws.Range("A2:B2").Style.Fill.SetBackgroundColor(XLColor.Red);
                ws.Cell("A" + 2).Value = "Extrema";
                ws.Cell("B" + 2).Value = "(4,00 – 5,00)";
                ws.Range("A3:B3").Style.Fill.SetBackgroundColor(XLColor.Orange);
                ws.Cell("A" + 3).Value = "Alta";
                ws.Cell("B" + 3).Value = "(3,00 – 3,99)";
                ws.Range("A4:B4").Style.Fill.SetBackgroundColor(XLColor.Yellow);
                ws.Cell("A" + 4).Value = "Moderada";
                ws.Cell("B" + 4).Value = "(2,00 – 2,99)";
                ws.Range("A5:B5").Style.Fill.SetBackgroundColor(XLColor.Green);
                ws.Cell("A" + 5).Value = "Baja";
                ws.Cell("B" + 5).Value = "(1,00 – 1,99)";

            ws.Range("C1:F1").Style.Border.SetBottomBorder(XLBorderStyleValues.Dotted);
            ws.Range("C1:F1").Style.Border.SetInsideBorder(XLBorderStyleValues.Dotted);
            ws.Range("C1:F1").Style.Border.SetLeftBorder(XLBorderStyleValues.Dotted);
            ws.Range("C1:F1").Style.Border.SetRightBorder(XLBorderStyleValues.Dotted);
            ws.Range("C1:F1").Style.Border.SetOutsideBorder(XLBorderStyleValues.Dotted);
            ws.Range("C1:F1").Style
             .Font.SetFontSize(12)
             .Font.SetBold(true)
             .Font.SetFontColor(XLColor.White)
             .Fill.SetBackgroundColor(XLColor.DarkBlue);
            ws.Cell("C" + 1).Value = "Clasificación Riesgo";
            ws.Cell("D" + 1).Value = "Riesgo Inherente";
            ws.Cell("E" + 1).Value = "Cantidad Riesgo Inherente";
            ws.Cell("F" + 1).Value = "Valoración";
            string riesgos = string.Empty;
            int iteracion = 0;
            int rango = indexRow;
            int totalRows = InfoGridRiesgoIndicador.Rows.Count;
            foreach (DataRow row in InfoGridRiesgoIndicador.Rows)
            {
                /*string valorcol2 = row.Cells[0].Text;
                ws.Cell("C" + indexRow).Value = valorcol2;*/
                if (iteracion > 0)
                {
                    if (riesgos != row["strClasificacionRiesgo"].ToString())
                    {
                        ws.Cell("C" + indexRow).Value = "Total Riesgos:";
                        ws.Cell("E" + indexRow).SetFormulaA1("=SUM(E" + rango + ":E" + (indexRow - 1) + ")")
                            .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        ws.Cell("F" + indexRow).SetFormulaA1("=SUM(F" + rango + ":F" + (indexRow - 1) + ")")
                            .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        indexRow++;
                        ws.Cell("C" + indexRow).Value = "Perfil de Riesgo:";
                        ws.Cell("F" + indexRow).SetFormulaA1("=E" + (indexRow - 1) + "/F" + (indexRow - 1))
                            .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        indexRow++;
                        rango = indexRow;
                    }
                }
                ws.Cell("C" + indexRow).Value = row["strClasificacionRiesgo"].ToString();
                ws.Cell("D" + indexRow).Value = row["strRiesgoInherente"].ToString();
                ws.Cell("E" + indexRow).Value = row["intCantRiesgoInherente"].ToString();
                ws.Cell("F" + indexRow).Value = row["decDecValoracion"].ToString();
                riesgos = row["strClasificacionRiesgo"].ToString();
                iteracion++;
                indexRow++;
                if (totalRows == iteracion)
                {
                    ws.Cell("C" + indexRow).Value = "Total Riesgos:";
                    ws.Cell("E" + indexRow).SetFormulaA1("=SUM(E" + rango + ":E" + (indexRow - 1) + ")")
                        .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                    ws.Cell("F" + indexRow).SetFormulaA1("=SUM(F" + rango + ":F" + (indexRow - 1) + ")")
                        .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);

                        indexRow++;
                        ws.Cell("C" + indexRow).Value = "Perfil de Riesgo:";
                        ws.Cell("F" + indexRow).SetFormulaA1("=E" + (indexRow - 1) + "/ F" + (indexRow - 1))
                               .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        indexRow++;
                    }
                }

            ws.Range("C" + indexRow + ":F" + indexRow).Style.Border.SetBottomBorder(XLBorderStyleValues.Dotted);
            ws.Range("C" + indexRow + ":F" + indexRow).Style.Border.SetInsideBorder(XLBorderStyleValues.Dotted);
            ws.Range("C" + indexRow + ":F" + indexRow).Style.Border.SetLeftBorder(XLBorderStyleValues.Dotted);
            ws.Range("C" + indexRow + ":F" + indexRow).Style.Border.SetRightBorder(XLBorderStyleValues.Dotted);
            ws.Range("C" + indexRow + ":F" + indexRow).Style.Border.SetOutsideBorder(XLBorderStyleValues.Dotted);
            ws.Range("C" + indexRow + ":F" + indexRow).Style
             .Font.SetFontSize(12)
             .Font.SetBold(true)
             .Font.SetFontColor(XLColor.White)
             .Fill.SetBackgroundColor(XLColor.DarkBlue);
            ws.Cell("C" + indexRow).Value = "Clasificación Riesgo";
            ws.Cell("D" + indexRow).Value = "Riesgo Residual";
            ws.Cell("E" + indexRow).Value = "Cantidad Riesgo Residual";
            ws.Cell("F" + indexRow).Value = "Valoración";
            indexRow++;
            riesgos = string.Empty;
            iteracion = 0;
            rango = indexRow;
            totalRows = InfoGridRiesgoIndicadorResidual.Rows.Count;
            foreach (DataRow row in InfoGridRiesgoIndicadorResidual.Rows)
            {
                if (iteracion > 0)
                {
                    if (riesgos != row["strClasificacionRiesgo"].ToString())
                    {
                        ws.Cell("C" + indexRow).Value = "Total Riesgos:";
                        ws.Cell("E" + indexRow).SetFormulaA1("=SUM(E" + rango + ":E" + (indexRow - 1) + ")")
                            .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        ws.Cell("F" + indexRow).SetFormulaA1("=SUM(F" + rango + ":F" + (indexRow - 1) + ")")
                            .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        indexRow++;
                        ws.Cell("C" + indexRow).Value = "Perfil de Riesgo:";
                        ws.Cell("F" + indexRow).SetFormulaA1("=E" + (indexRow - 1) + "/F" + (indexRow - 1))
                            .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        indexRow++;
                        rango = indexRow;
                    }
                }
                ws.Cell("C" + indexRow).Value = row["strClasificacionRiesgo"].ToString();
                ws.Cell("D" + indexRow).Value = row["strRiesgoResidual"].ToString();
                ws.Cell("E" + indexRow).Value = row["intCantRiesgoResidual"].ToString();
                ws.Cell("F" + indexRow).Value = row["decDecValoracion"].ToString();
                /*string valorcol2 = row.Cells[0].Text;
                ws.Cell("C" + indexRow).Value = valorcol2;*/
                riesgos = row["strClasificacionRiesgo"].ToString();
                iteracion++;
                indexRow++;
                if (totalRows == iteracion)
                {
                    ws.Cell("C" + indexRow).Value = "Total Riesgos:";
                    ws.Cell("E" + indexRow).SetFormulaA1("=SUM(E" + rango + ":E" + (indexRow - 1) + ")")
                        .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                    ws.Cell("F" + indexRow).SetFormulaA1("=SUM(F" + rango + ":F" + (indexRow - 1) + ")")
                        .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);

                        indexRow++;
                        ws.Cell("C" + indexRow).Value = "Perfil de Riesgo:";
                        ws.Cell("F" + indexRow).SetFormulaA1("=E" + (indexRow - 1) + "/ F" + (indexRow - 1))
                               .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        indexRow++;
                    }
                }
                string filename = "ReportePerfilRiesgos";
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
            }else
            {
                Mensaje("Error: No hay datos para exportar");
            }
            
        }

        protected void gvRiesgoControlInherente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexRiesgoIndicador = e.NewPageIndex;
            gvRiesgoControlInherente.PageIndex = PagIndexRiesgoIndicador;
            gvRiesgoControlInherente.DataSource = InfoGridRiesgoIndicador;
            gvRiesgoControlInherente.DataBind();
        }

        protected void btnReporteRiesgoControl_Click(object sender, EventArgs e)
        {
            if(InfoGridRiesgoIndicador.Rows.Count > 0 && InfoGridRiesgoIndicadorResidual.Rows.Count > 0)
            {
                XLWorkbook workbook = new XLWorkbook();
                var ws = workbook.Worksheets.Add("Perfil de riesgo");
                //***********FORMATO DE TABLA************************/
                ws.Range("A1:B1").Style.Border.SetBottomBorder(XLBorderStyleValues.Dotted);
                ws.Range("A1:B1").Style.Border.SetInsideBorder(XLBorderStyleValues.Dotted);
                ws.Range("A1:B1").Style.Border.SetLeftBorder(XLBorderStyleValues.Dotted);
                ws.Range("A1:B1").Style.Border.SetRightBorder(XLBorderStyleValues.Dotted);
                ws.Range("A1:B1").Style.Border.SetOutsideBorder(XLBorderStyleValues.Dotted);
                ws.Range("A1:B1").Style
                 .Font.SetFontSize(12)
                 .Font.SetBold(true)
                 .Font.SetFontColor(XLColor.White)
                 .Fill.SetBackgroundColor(XLColor.DarkBlue);
                int indexRow = 2;
                ws.Cell("A" + 1).Value = "Perfil";
                ws.Cell("B" + 1).Value = "Valor";

                ws.Range("A2:B2").Style.Fill.SetBackgroundColor(XLColor.Red);
                ws.Cell("A" + 2).Value = "Extrema";
                ws.Cell("B" + 2).Value = "(4,00 – 5,00)";
                ws.Range("A3:B3").Style.Fill.SetBackgroundColor(XLColor.Orange);
                ws.Cell("A" + 3).Value = "Alta";
                ws.Cell("B" + 3).Value = "(3,00 – 3,99)";
                ws.Range("A4:B4").Style.Fill.SetBackgroundColor(XLColor.Yellow);
                ws.Cell("A" + 4).Value = "Moderada";
                ws.Cell("B" + 4).Value = "(2,00 – 2,99)";
                ws.Range("A5:B5").Style.Fill.SetBackgroundColor(XLColor.Green);
                ws.Cell("A" + 5).Value = "Baja";
                ws.Cell("B" + 5).Value = "(1,00 – 1,99)";

                ws.Range("C1:E1").Style.Border.SetBottomBorder(XLBorderStyleValues.Dotted);
                ws.Range("C1:E1").Style.Border.SetInsideBorder(XLBorderStyleValues.Dotted);
                ws.Range("C1:E1").Style.Border.SetLeftBorder(XLBorderStyleValues.Dotted);
                ws.Range("C1:E1").Style.Border.SetRightBorder(XLBorderStyleValues.Dotted);
                ws.Range("C1:E1").Style.Border.SetOutsideBorder(XLBorderStyleValues.Dotted);
                ws.Range("C1:E1").Style
                 .Font.SetFontSize(12)
                 .Font.SetBold(true)
                 .Font.SetFontColor(XLColor.White)
                 .Fill.SetBackgroundColor(XLColor.DarkBlue);
                ws.Cell("C" + 1).Value = "Clasificación Riesgo";
                ws.Cell("D" + 1).Value = "Riesgo Inherente";
                ws.Cell("E" + 1).Value = "Cantidad Riesgo Inherente";
                string riesgos = string.Empty;
                int iteracion = 0;
                int rango = indexRow;
                int totalRows = InfoGridRiesgoIndicador.Rows.Count;
                foreach (DataRow row in InfoGridRiesgoIndicador.Rows)
                {
                    if (iteracion > 0)
                    {
                        /*if (riesgos != row["strClasificacionRiesgo"].ToString())
                        {
                            ws.Cell("C" + indexRow).Value = "Total Riesgos:";
                            ws.Cell("E" + indexRow).SetFormulaA1("=SUM(E" + rango + ":E" + (indexRow - 1) + ")")
                                .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                            ws.Cell("F" + indexRow).SetFormulaA1("=SUM(F" + rango + ":F" + (indexRow - 1) + ")")
                                .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                            indexRow++;
                            ws.Cell("C" + indexRow).Value = "Perfil de Riesgo:";
                            ws.Cell("F" + indexRow).SetFormulaA1("=E" + (indexRow - 1) + "/F" + (indexRow - 1))
                                .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                            indexRow++;
                            rango = indexRow;
                        }*/
                    }
                    ws.Cell("C" + indexRow).Value = row["strClasificacionRiesgo"].ToString();
                    ws.Cell("D" + indexRow).Value = row["strRiesgoInherente"].ToString();
                    ws.Cell("E" + indexRow).Value = row["intCantRiesgoInherente"].ToString();
                    riesgos = row["strClasificacionRiesgo"].ToString();
                    iteracion++;
                    indexRow++;
                    /*if (totalRows == iteracion)
                    {
                        ws.Cell("C" + indexRow).Value = "Total Riesgos:";
                        ws.Cell("E" + indexRow).SetFormulaA1("=SUM(E" + rango + ":E" + (indexRow - 1) + ")")
                            .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        ws.Cell("F" + indexRow).SetFormulaA1("=SUM(F" + rango + ":F" + (indexRow - 1) + ")")
                            .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);

                        indexRow++;
                        ws.Cell("C" + indexRow).Value = "Perfil de Riesgo:";
                        ws.Cell("F" + indexRow).SetFormulaA1("=E" + (indexRow - 1) + "/ F" + (indexRow - 1))
                               .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        indexRow++;
                    }*/
                }

            ws.Range("C" + indexRow + ":E" + indexRow).Style.Border.SetBottomBorder(XLBorderStyleValues.Dotted);
            ws.Range("C" + indexRow + ":E" + indexRow).Style.Border.SetInsideBorder(XLBorderStyleValues.Dotted);
            ws.Range("C" + indexRow + ":E" + indexRow).Style.Border.SetLeftBorder(XLBorderStyleValues.Dotted);
            ws.Range("C" + indexRow + ":E" + indexRow).Style.Border.SetRightBorder(XLBorderStyleValues.Dotted);
            ws.Range("C" + indexRow + ":E" + indexRow).Style.Border.SetOutsideBorder(XLBorderStyleValues.Dotted);
            ws.Range("C" + indexRow + ":E" + indexRow).Style
             .Font.SetFontSize(12)
             .Font.SetBold(true)
             .Font.SetFontColor(XLColor.White)
             .Fill.SetBackgroundColor(XLColor.DarkBlue);
            ws.Cell("C" + indexRow).Value = "Clasificación Riesgo";
            ws.Cell("D" + indexRow).Value = "Riesgo Residual";
            ws.Cell("E" + indexRow).Value = "Cantidad Riesgo Residual";

            indexRow++;
            riesgos = string.Empty;
            iteracion = 0;
            rango = indexRow;
            totalRows = InfoGridRiesgoIndicadorResidual.Rows.Count;
            foreach (DataRow row in InfoGridRiesgoIndicadorResidual.Rows)
            {
                /*if (iteracion > 0)
                {
                    if (riesgos != row["strClasificacionRiesgo"].ToString())
                    {
                        ws.Cell("C" + indexRow).Value = "Total Riesgos:";
                        ws.Cell("E" + indexRow).SetFormulaA1("=SUM(E" + rango + ":E" + (indexRow - 1) + ")")
                            .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        ws.Cell("F" + indexRow).SetFormulaA1("=SUM(F" + rango + ":F" + (indexRow - 1) + ")")
                            .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        indexRow++;
                        ws.Cell("C" + indexRow).Value = "Perfil de Riesgo:";
                        ws.Cell("F" + indexRow).SetFormulaA1("=E" + (indexRow - 1) + "/F" + (indexRow - 1))
                            .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        indexRow++;
                        rango = indexRow;
                    }
                }*/
                ws.Cell("C" + indexRow).Value = row["strClasificacionRiesgo"].ToString();
                ws.Cell("D" + indexRow).Value = row["strRiesgoResidual"].ToString();
                ws.Cell("E" + indexRow).Value = row["intCantRiesgoResidual"].ToString();


                riesgos = row["strClasificacionRiesgo"].ToString();
                iteracion++;
                indexRow++;
                /*if (totalRows == iteracion)
                {
                    ws.Cell("C" + indexRow).Value = "Total Riesgos:";
                    ws.Cell("E" + indexRow).SetFormulaA1("=SUM(E" + rango + ":E" + (indexRow - 1) + ")")
                        .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                    ws.Cell("F" + indexRow).SetFormulaA1("=SUM(F" + rango + ":F" + (indexRow - 1) + ")")
                        .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);

                        indexRow++;
                        ws.Cell("C" + indexRow).Value = "Perfil de Riesgo:";
                        ws.Cell("F" + indexRow).SetFormulaA1("=E" + (indexRow - 1) + "/ F" + (indexRow - 1))
                               .Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
                        indexRow++;
                    }*/
                }
                string filename = "ReporteRiesgosvsControl";
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
            }else
            {
                Mensaje("Error: No hay datos para exportar");
            }
            
        }
    }
}