using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using clsLogica;
using clsDTO;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.ConfigEstructura
{
    public partial class TipoParametro : System.Web.UI.UserControl
    {
        string IdFormulario = "10002";
        clsCuenta cCuenta = new clsCuenta();

        #region Properties

        private int pagIndex;
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

        private DataTable infoGrid;
        private DataTable InfoGrid
        {
            get
            {
                infoGrid = (DataTable)ViewState["infGrid2"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infGrid2"] = infoGrid;
            }
        }

        private int rowGrid;
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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosConsulta(Convert.ToInt32(Session["IdUsuario"].ToString()), Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1",false);

                if (!Page.IsPostBack)
                {
                    mtdInicializarValores();
                    mtdLoadGridView();
                }
            }
            catch
            {
                Response.Redirect("~/Formularios/Sitio/Login.aspx", false);
            }
        }

        #region Gridview
        protected void gvTipoParametro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvTipoParametro.PageIndex = PagIndex;
            gvTipoParametro.DataSource = InfoGrid;
            gvTipoParametro.DataBind();

            mtdLoadGridView();
        }

        protected void gvTipoParametro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(gvTipoParametro.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdModificar();
                    break;
            }
        }
        #endregion Gridview

        #region Buttons

        protected void ibtnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                mtdResetValues();
                updateUser.Visible = true;
                ibtnGuardar.Visible = true;
                ibtnGuardarUpd.Visible = false;
            }
        }

        protected void ibtnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (clsUtilidades.mtdEsNumero(Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim())))
                    {
                        mtdAgregarTipoParametro(Sanitizer.GetSafeHtmlFragment(tbNombreTipoParametro.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim()), chbActivo.Checked);

                        mtdResetValues();
                        mtdLoadGridView();
                    }
                    else
                        mtdMensaje("Por favor verificar que la calificación sea un número entero.");
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al agregar la variable. [" + ex.Message + "].");
            }
        }

        protected void ibtnGuardarUpd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cCuenta.permisosActualizar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    mtdActualizarTipoParametro(InfoGrid.Rows[RowGrid]["StrIdVariable"].ToString().Trim(),
                        Sanitizer.GetSafeHtmlFragment(tbNombreTipoParametro.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim()), chbActivo.Checked);

                    mtdResetValues();
                    mtdLoadGridView();
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al modificar la variable. " + ex.Message);
            }
        }

        protected void ibtnCancelUpd_Click(object sender, EventArgs e)
        {
            mtdResetValues();
        }
        #endregion

        #region Loads
        private void mtdLoadGridView()
        {
            mtdLoadGrid();
            mtdLoadInfoGrid();
        }

        private void mtdLoadGrid()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdVariable", typeof(string));
            grid.Columns.Add("StrNombreVariable", typeof(string));
            grid.Columns.Add("StrCalificacion", typeof(string));
            grid.Columns.Add("BooActivo", typeof(string));

            gvTipoParametro.DataSource = grid;
            gvTipoParametro.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGrid()
        {
            string strErrMsg = string.Empty;
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOVariable> lstTipoParam = new List<clsDTOVariable>();

            lstTipoParam = cParamArchivo.mtdCargarInfoVariables(ref strErrMsg);

            if (lstTipoParam != null)
            {
                mtdLoadInfoGrid(lstTipoParam);
                gvTipoParametro.DataSource = lstTipoParam;
                gvTipoParametro.DataBind();
            }
        }

        private void mtdLoadInfoGrid(List<clsDTOVariable> lstVariable)
        {
            foreach (clsDTOVariable objVariable in lstVariable)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objVariable.StrIdVariable.ToString().Trim(),
                    objVariable.StrNombreVariable.ToString().Trim(),
                    objVariable.StrCalificacion.ToString().Trim(),
                    objVariable.BooActivo
                    });
            }
        }
        #endregion Loads

        private void mtdInicializarValores()
        {
            PagIndex = 0;
        }

        private void mtdResetValues()
        {
            tbNombreTipoParametro.Text = string.Empty;
            tbCalificacion.Text = string.Empty;
            chbActivo.Checked = false;

            updateUser.Visible = false;
            ibtnGuardar.Visible = false;
            ibtnGuardarUpd.Visible = false;
        }

        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private void mtdModificar()
        {
            updateUser.Visible = true;
            ibtnGuardar.Visible = false;
            ibtnGuardarUpd.Visible = true;

            tbNombreTipoParametro.Text = InfoGrid.Rows[RowGrid]["StrNombreVariable"].ToString().Trim();
            tbCalificacion.Text = InfoGrid.Rows[RowGrid]["StrCalificacion"].ToString().Trim();
            chbActivo.Checked = InfoGrid.Rows[RowGrid][3].ToString().Trim() == "True" ? true : false;
        }

        private void mtdActualizarTipoParametro(string strIdTipoParametro, string strNombreParametro, string strCalificacion, bool booActivo)
        {
            #region Vars
            int intSumVariables = 0, intSumTemp = 0;
            string strErrMsg = string.Empty;
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            clsDTOVariable objTipoParamIn = new clsDTOVariable(strIdTipoParametro, strNombreParametro, strCalificacion, booActivo);
            #endregion

            if (clsUtilidades.mtdEsNumero(Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim())))
            {
                intSumVariables = cParamArchivo.mtdConsultaSumatoria(ref strErrMsg);

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    intSumTemp = intSumVariables + Convert.ToInt32(strCalificacion);

                    if (intSumTemp <= 100)
                        cParamArchivo.mtdActualizarVariable(objTipoParamIn, ref strErrMsg);
                    else
                        strErrMsg = "Por favor verificar que la sumatoria de la calificación de todas las variables sea menor o igual a 100.";
                }
            }
            else
                strErrMsg = "Por favor verificar que la calificación sea un número entero.";

            if (string.IsNullOrEmpty(strErrMsg))
                mtdMensaje("El tipo de parámetro fue actualizado exitósamente.");
            else
                mtdMensaje(strErrMsg);

        }

        private void mtdAgregarTipoParametro(string strNombreParametro, string strCalificacion, bool booActivo)
        {
            #region Vars
            string strErrMsg = string.Empty;
            int intSumVariables = 0, intSumTemp = 0;
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            clsDTOVariable objTipoParamIn = new clsDTOVariable(string.Empty, strNombreParametro, strCalificacion, booActivo);
            #endregion

            intSumVariables = cParamArchivo.mtdConsultaSumatoria(ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
            {
                intSumTemp = intSumVariables + Convert.ToInt32(strCalificacion);

                if (intSumTemp <= 100)
                    cParamArchivo.mtdAgregarVariable(objTipoParamIn, ref strErrMsg);
                else
                    strErrMsg = "Por favor verificar que la sumatoria de la calificación de todas las variables sea menor o igual a 100.";

                if (string.IsNullOrEmpty(strErrMsg))
                    mtdMensaje("El tipo de parámetro fue creado exitósamente.");
                else
                    mtdMensaje(strErrMsg);
            }
            else
                mtdMensaje(strErrMsg);
        }

        
    }
}