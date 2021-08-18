using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using clsLogica;
using clsDTO;
using System.IO;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Perfilamiento
{
    public partial class SenalAlertaManual : System.Web.UI.UserControl
    {
        string IdFormulario = "11007";
        clsCuenta cCuenta = new clsCuenta();
        cCuenta ccCuenta = new cCuenta();

        #region Properties

        private int indexRow;
        private DataTable infoGrid;

        private int IndexRow
        {
            get
            {
                indexRow = (int)ViewState["indexRow"];
                return indexRow;
            }
            set
            {
                indexRow = value;
                ViewState["indexRow"] = indexRow;
            }
        }

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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.AsyncPostBackTimeout = 360000;
            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                mtdLimpiarCampos();
                mtdLoadGridViewSenales();
            }
        }

        #region Loads
        private void mtdLoadGridViewSenales()
        {
            mtdLoadGridSenales();
            mtdLoadInfoGridSenales();
        }

        private void mtdLoadGridSenales()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdSenal", typeof(string));
            grid.Columns.Add("StrCodigoSenal", typeof(string));
            grid.Columns.Add("StrDescripcionSenal", typeof(string));
            grid.Columns.Add("BooEsAutomatico", typeof(string));

            gvSenal.DataSource = grid;
            gvSenal.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGridSenales()
        {
            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            List<clsDTOSenal> lstSenal = new List<clsDTOSenal>();

            lstSenal = cSenal.mtdCargarInfoSenal(ref strErrMsg);

            if (lstSenal != null)
            {
                mtdLoadInfoGridSenales(lstSenal);
                gvSenal.DataSource = lstSenal;
                gvSenal.DataBind();
            }
        }

        private void mtdLoadInfoGridSenales(List<clsDTOSenal> lstSenal)
        {
            foreach (clsDTOSenal objSenal in lstSenal)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objSenal.StrIdSenal.ToString().Trim(),
                    objSenal.StrCodigoSenal.ToString().Trim(),
                    objSenal.StrDescripcionSenal.ToString().Trim(),
                    objSenal.BooEsAutomatico
                    });
            }
        }
        #endregion

        #region Buttons
        protected void ibtnEjecutar_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void ibtnCancel_Click(object sender, ImageClickEventArgs e)
        {
            mtdLimpiarCampos();
        }
        #endregion

        #region GridView
        protected void gvSenal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IndexRow = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Analizar":
                    if (mtdValidarCampos(Sanitizer.GetSafeHtmlFragment(tbxFechaInicial.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxFechaFinal.Text.Trim())))
                        mtdEjecutarSenal(InfoGrid.Rows[IndexRow]["StrIdSenal"].ToString().Trim(),
                            Sanitizer.GetSafeHtmlFragment(tbxFechaInicial.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbxFechaFinal.Text.Trim()),
                            Convert.ToInt32(Session["idUsuario"].ToString().Trim()), Session["nombreUsuario"].ToString().Trim());
                    else
                        mtdMensaje("Por favor verificar que las fechas tengan información.");
                    break;
            }
        }
        #endregion

        #region Meths
        private void mtdLimpiarCampos()
        {
            tbxFechaInicial.Text = string.Empty;
            tbxFechaFinal.Text = string.Empty;
        }

        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdEjecutarSenal(string strIdSenal, string strFechaInicial, string strFechaFinal, int strIdUsuario, string strNombreUsuario)
        {
            #region Vars
            int intOcurrencias = 0;
            string strErrMsg = string.Empty;
            DataTable dtInfoCargue = new DataTable();
            DataTable dtCargueTotal = new DataTable();
            DataTable dtSerializedData = new DataTable();

            clsArchivo cArchivo = new clsArchivo();
            Classes.clsPerfil cPerfil = new Classes.clsPerfil();
            clsSenal cSenal = new clsSenal();
            clsParamArchivo cParam = new clsParamArchivo();
            clsDTOSenal objSenal = new clsDTOSenal(strIdSenal, string.Empty, string.Empty, false);
            List<clsDTOArchivo> lstArchivos = new List<clsDTOArchivo>();
            List<clsDTOSenalVariable> lstFormulas = new List<clsDTOSenalVariable>();
            List<clsDTOEstructuraCampo> lstEstruc = new List<clsDTOEstructuraCampo>();
            DataRow dr;
            #endregion

            try
            {
                #region Consulta de Informacion
                lstArchivos = cArchivo.mtdConsultarArchivos(strFechaInicial, strFechaFinal, ref strErrMsg);
                lstFormulas = cSenal.mtdConsultarFormSenalAuto(objSenal, false, ref strErrMsg);
                lstEstruc = cParam.mtdCargarInfoEstructura(ref strErrMsg);
                #endregion
                if (lstArchivos != null && lstFormulas != null)
                {
                    int iteracion = 0;
                    foreach (clsDTOArchivo objArchivo in lstArchivos)
                    {
                        dtInfoCargue = cArchivo.mtdConsultarInfoCargada(objArchivo, ref strErrMsg);
                        if (iteracion == 0)
                            dtCargueTotal = dtInfoCargue.Clone();
                        foreach (DataRow drId in dtInfoCargue.Rows)
                        {
                            //int Posicion = Convert.ToInt32(drId["Posicion"].ToString());
                            dtCargueTotal.ImportRow(drId);
                        }
                        iteracion++;
                    }
                }
                if (lstArchivos != null && lstFormulas != null)
                {


                    #region Recorrido de informacion
                    string strUltimoArchivo = string.Empty;

                    //foreach (clsDTOArchivo objArchivo in lstArchivos)
                    //{

                    //dtInfoCargue = cArchivo.mtdConsultarInfoCargada(objArchivo, ref strErrMsg);

                    cSenal.mtdEjecutarSenalGlobal(lstFormulas, lstEstruc, dtCargueTotal, strIdUsuario, strNombreUsuario, ref intOcurrencias, ref strErrMsg);
                    cSenal.mtdEjecutarSenalManual(lstFormulas, lstEstruc, dtCargueTotal, strIdUsuario, strNombreUsuario, ref intOcurrencias, ref strErrMsg);

                    /*if (!string.IsNullOrEmpty(strErrMsg))
                    {
                        strUltimoArchivo = objArchivo.StrUrlArchivo;
                        break;
                    }
                    iteracion++;*/
                    //}

                    if (string.IsNullOrEmpty(strErrMsg))
                    {
                        string strMensaje = string.Format("Se han realizado el análisis de las señales manuales exitósamente. Se encontraron {0} coincidencias.", intOcurrencias);
                        mtdMensaje(strMensaje);
                    }
                    else
                    {
                        string strMsgAdicional = string.Format("{0} - Archivo:[{1}]", strErrMsg, strUltimoArchivo);
                        mtdMensaje(strMsgAdicional);
                    }
                    #endregion
                }
                else
                {
                    if (string.IsNullOrEmpty(strErrMsg))
                        mtdMensaje("No se realizó información porque no hay datos a analizar");
                    else
                        mtdMensaje(strErrMsg);
                }
            }
            catch (Exception ex)
            {
                mtdMensaje($"Valor con formato no válido. {ex.Message}");
            }
        }

        private bool mtdValidarCampos(string strFechaInicial, string strFechaFinal)
        {
            bool booResult = false;

            if ((!string.IsNullOrEmpty(strFechaInicial)) && (!string.IsNullOrEmpty(strFechaFinal)))
                booResult = true;

            return booResult;
        }
        #endregion
    }
}