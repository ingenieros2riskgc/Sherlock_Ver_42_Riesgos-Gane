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
    public partial class CuadroComparativo : System.Web.UI.UserControl
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
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }
        public bool mtdLoadCuadroComparativo(ref string strErrMsg)
        {
            bool booResult = false;
            List<clsDTOCuadroComparativoRiesgos> lstComparativo = new List<clsDTOCuadroComparativoRiesgos>();
            clsBLLRiesgosComparativoRiesgos cComparativo = new clsBLLRiesgosComparativoRiesgos();
            try
            {
                lstComparativo = cComparativo.mtdConsultarCuadroComperativo(booResult, ref strErrMsg, 1);
                if (lstComparativo != null)
                {
                    mtdLoadCuadroComparativo();
                    mtdLoadCuadroComparativo(lstComparativo);
                    GVcomparativo.DataSource = lstComparativo;
                    GVcomparativo.PageIndex = pagIndexRiesgoIndicador;
                    GVcomparativo.DataBind();
                    booResult = true;
                }
                lstComparativo = cComparativo.mtdConsultarCuadroComperativoResidual(booResult, ref strErrMsg, 2);
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
            catch(Exception ex)
            {
                omb.ShowMessage("Error en la generación del reporte: "+ex, 2, "Atención");
            }
            
            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadCuadroComparativo()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("strClasificacionRiesgo", typeof(string));
            grid.Columns.Add("strRiesgoInherente", typeof(string));
            grid.Columns.Add("intCantRiesgoInherente", typeof(string));
            grid.Columns.Add("strRiesgoResidual", typeof(string));
            grid.Columns.Add("intCantRiesgoResidual", typeof(string));

            GVcomparativo.DataSource = grid;
            GVcomparativo.DataBind();
            InfoGridRiesgoIndicador = grid;
        }
        private void mtdLoadCuadroComparativoResidual()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("strClasificacionRiesgo", typeof(string));
            grid.Columns.Add("strRiesgoInherente", typeof(string));
            grid.Columns.Add("intCantRiesgoInherente", typeof(string));
            grid.Columns.Add("strRiesgoResidual", typeof(string));
            grid.Columns.Add("intCantRiesgoResidual", typeof(string));

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
                if(objComparativo.strRiesgoInherente != null)
                {
                    InfoGridRiesgoIndicador.Rows.Add(new Object[] {
                    objComparativo.strClasificacionRiesgo.ToString().Trim(),
                    objComparativo.strRiesgoInherente.ToString().Trim(),
                    objComparativo.intCantRiesgoInherente.ToString().Trim(),
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
                    objComparativo.intCantRiesgoResidual.ToString().Trim()
                    });
                }

            }
        }
        protected void GVcomparativo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexRiesgoIndicador = e.NewPageIndex;
            GVcomparativo.PageIndex = PagIndexRiesgoIndicador;
            GVcomparativo.DataSource = InfoGridRiesgoIndicador;
            GVcomparativo.DataBind();
        }

        protected void GVcomparativoResidual_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexRiesgoIndicadorResidual = e.NewPageIndex;
            GVcomparativoResidual.PageIndex = PagIndexRiesgoIndicadorResidual;
            GVcomparativoResidual.DataSource = InfoGridRiesgoIndicadorResidual;
            GVcomparativoResidual.DataBind();
        }
    }
}