using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using clsLogica;
using clsDTO;
using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.ConfigEstructura
{
    public partial class Parametrizacion : System.Web.UI.UserControl
    {
        string IdFormulario = "10003";
        clsCuenta cCuenta = new clsCuenta();
        cCuenta ccCuenta = new cCuenta();

        #region Properties

        private int pagIndex;
        private DataTable infoGrid;
        private int rowGrid;

        private DataTable infoOpGrid;
        private int indexRowOp;
        private int pagOpIndex;

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

        private DataTable InfoOpGrid
        {
            get
            {
                infoOpGrid = (DataTable)ViewState["infoOpGrid"];
                return infoOpGrid;
            }
            set
            {
                infoOpGrid = value;
                ViewState["infoOpGrid"] = infoOpGrid;
            }
        }

        private int IndexRowOp
        {
            get
            {
                indexRowOp = (int)ViewState["indexRowOp"];
                return indexRowOp;
            }
            set
            {
                indexRowOp = value;
                ViewState["indexRowOp"] = indexRowOp;
            }
        }

        private int PagOpIndex
        {
            get
            {
                pagOpIndex = (int)ViewState["pagOpIndex"];
                return pagOpIndex;
            }
            set
            {
                pagOpIndex = value;
                ViewState["pagOpIndex"] = pagOpIndex;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1", false);

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scrtManager = ScriptManager.GetCurrent(this.Page);

            if (!Page.IsPostBack)
            {
                mtdInicializarValores();
                mtdLoadGridView();
                mtdLoadGridViewOperadores();
            }
        }

        #region Gridview
        protected void gvParametrizacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(gvParametrizacion.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdLoadDDLTipoParametro();
                    mtdModificar();
                    break;
                case "Eliminar":
                    mtdEliminarCategoria();
                    mtdLoadGridView();
                    break;
            }
        }

        #region Operador
        protected void gvOperador_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagOpIndex = e.NewPageIndex;
            gvOperador.PageIndex = PagOpIndex;
            gvOperador.DataSource = InfoOpGrid;
            gvOperador.DataBind();
        }

        protected void gvOperador_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IndexRowOp = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "SelecOperador":
                    if (InfoOpGrid.Rows[IndexRowOp]["StrNombreOperador"].ToString().Trim().ToUpper() == "ENTRE")
                        mtdHabilitarControlRango(true);
                    else
                        mtdHabilitarControlRango(false);
                    mtdSeleccionOperador();
                    break;
            }
        }
        #endregion
        #endregion Gridview

        #region Buttons

        protected void ibtnAgregar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
            else
            {
                mtdLoadDDLTipoParametro();
                mtdResetValues();

                ddlTipoParametro.SelectedIndex = 0;

                updateUser.Visible = true;
                ibtnGuardar.Visible = true;
                ibtnGuardarUpd.Visible = false;
                mtdHabilitarControlRango(false);
            }
        }

        protected void ibtnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (clsLogica.clsUtilidades.mtdEsNumero(Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim())))
                    {
                        if (ddlTipoParametro.SelectedIndex != 0)
                        {
                            if (chbCondicion.Checked)
                            {
                                mtdAgregarParametrizacion(Sanitizer.GetSafeHtmlFragment(tbNombreParametro.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim()), tbCodParametro.Text.Trim(),
                                ddlTipoParametro.SelectedValue.ToString().Trim(), chbCondicion.Checked, ref strErrMsg);
                            }
                            else
                            {
                                mtdAgregarParametrizacion(Sanitizer.GetSafeHtmlFragment(tbNombreParametro.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbCodParametro.Text.Trim()),
                                                                    ddlTipoParametro.SelectedValue.ToString().Trim(), chbCondicion.Checked, ref strErrMsg);
                            }


                            mtdResetValues();
                            mtdLoadGridView();

                            if (string.IsNullOrEmpty(strErrMsg))
                                mtdMensaje("La categoría fue creada exitósamente.");
                            else
                                mtdMensaje(strErrMsg);
                        }
                        else
                            mtdMensaje("Por favor modifique la variable");
                    }
                    else
                        mtdMensaje("Por favor verifique que la calificación sea un número entero.");
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al agregar la categoría. [" + ex.Message + "].");
            }
        }

        protected void ibtnGuardarUpd_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (clsLogica.clsUtilidades.mtdEsNumero(Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim())))
                    {
                        if (ddlTipoParametro.SelectedIndex != 0)
                        {
                            if (chbCondicion.Checked)
                            {
                                mtdActualizarParametrizacion(InfoGrid.Rows[RowGrid]["StrIdParametros"].ToString().Trim(),
                                Sanitizer.GetSafeHtmlFragment(tbNombreParametro.Text.Trim()), tbCodParametro.Text.Trim(), Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim()),
                                ddlTipoParametro.SelectedValue.ToString().Trim(), chbCondicion.Checked, ref strErrMsg);
                            }
                            else
                            {
                                mtdActualizarParametrizacion(InfoGrid.Rows[RowGrid]["StrIdParametros"].ToString().Trim(),
                                Sanitizer.GetSafeHtmlFragment(tbNombreParametro.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbCodParametro.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbCalificacion.Text.Trim()),
                                ddlTipoParametro.SelectedValue.ToString().Trim(), chbCondicion.Checked, ref strErrMsg);
                            }
                            mtdResetValues();
                            mtdLoadGridView();
                        }
                        else
                            mtdMensaje("Por favor modifique la variable");
                    }
                    else
                        strErrMsg = "Por favor verifique que la calificación sea un número entero.";

                    if (string.IsNullOrEmpty(strErrMsg))
                        mtdMensaje("La parámetrización fue actualizada exitósamente.");
                    else
                        mtdMensaje(strErrMsg);
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al modificar el tipo de parámetro. " + ex.Message);
            }
        }

        protected void ibtnCancelUpd_Click(object sender, EventArgs e)
        {
            mtdResetValues();
        }

        protected void ibtnSelecOtroValor_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(tbxOtroValor.Text.Trim())))
            {
                tbCodParametro.Text = tbCodParametro.Text + " " + Sanitizer.GetSafeHtmlFragment(tbxOtroValor.Text);
                mtdHabilitarControlesFormula(1);
                mtdHabilitarControlRango(false);
            }
            else
                mtdMensaje("Este campo debe tener información.");
        }

        protected void ibtnRango_Click(object sender, EventArgs e)
        {
            bool booIsErr = false;

            #region Validacion Campos Rango Vacio
            if (string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(tbxDesde.Text.Trim())))
            {
                mtdMensaje("Error. El campo [Desde] no debe estar vacio.");
                booIsErr = true;
            }

            if (string.IsNullOrEmpty(Sanitizer.GetSafeHtmlFragment(tbxHasta.Text.Trim())))
            {
                mtdMensaje("Error. El campo [Hasta] no debe estar vacio.");
                booIsErr = true;
            }
            #endregion

            #region Validacion Campos Rango numericos
            if (!booIsErr)
            {
                if (!clsLogica.clsUtilidades.mtdEsNumero(Sanitizer.GetSafeHtmlFragment(tbxDesde.Text.Trim())))
                {
                    mtdMensaje("Error. El campo [Desde] debe ser un número.");
                    booIsErr = true;
                }

                if (!clsLogica.clsUtilidades.mtdEsNumero(Sanitizer.GetSafeHtmlFragment(tbxHasta.Text.Trim())))
                {
                    mtdMensaje("Error. El campo [Hasta] debe ser un número.");
                    booIsErr = true;
                }
            }
            #endregion

            if (!booIsErr)
            {
                tbCodParametro.Text = Sanitizer.GetSafeHtmlFragment(tbCodParametro.Text) + " " + Sanitizer.GetSafeHtmlFragment(tbxDesde.Text) + " y " + Sanitizer.GetSafeHtmlFragment(tbxHasta.Text);
                mtdHabilitarControlesFormula(1);
                mtdHabilitarControlRango(false);
            }
        }

        protected void btnGVEliminar_Click(object sender, ImageClickEventArgs e)
        {
            lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
            mpeMsgBox.Show();
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            mpeMsgBox.Hide();
            string strErrMsg = string.Empty;
            clsParamArchivo cParam = new clsParamArchivo();

            clsDTOParametrizacion objParam = new clsDTOParametrizacion(InfoGrid.Rows[RowGrid]["StrCodigoParametro"].ToString().Trim(),
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false);
            cParam.mtdEliminarCategoria(objParam, ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
                mtdMensaje("La señal de alerta y fórmula fueron eliminadas exitósamente.");
            else
                mtdMensaje(strErrMsg);
        }

        private void mtdEliminarCategoria()
        {
            if (cCuenta.permisosBorrar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
            {
                mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                return;
            }
            mpeMsgBox.Hide();
            string strErrMsg = string.Empty;
            clsParamArchivo cParam = new clsParamArchivo();

            clsDTOParametrizacion objParam = new clsDTOParametrizacion(InfoGrid.Rows[RowGrid][0].ToString().Trim(),
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false);
            cParam.mtdEliminarCategoria(objParam, ref strErrMsg);

            if (string.IsNullOrEmpty(strErrMsg))
                mtdMensaje("La categoría fue eliminada exitósamente.");
            else
                mtdMensaje(strErrMsg);
        }
        #endregion

        protected void chbCondicion_CheckedChanged(object sender, EventArgs e)
        {
            if (chbCondicion.Checked)
            {
                mtdHabilitarControlesFormula(2);
                tbCodParametro.Enabled = false;
            }
            else
            {
                mtdHabilitarControlesFormula(1);
                tbCodParametro.Enabled = true;
            }
        }

        #region Loads
        private void mtdLoadGridView()
        {
            mtdLoadGrid();
            mtdLoadInfoGrid();
        }

        private void mtdLoadGrid()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdParametros", typeof(string));
            grid.Columns.Add("StrNombreParametro", typeof(string));
            grid.Columns.Add("StrCodigoParametro", typeof(string));
            grid.Columns.Add("StrIdTipoParametro", typeof(string));
            grid.Columns.Add("StrNombreTipoParametro", typeof(string));
            grid.Columns.Add("StrCalificacionParametro", typeof(string));
            grid.Columns.Add("BooEsFormula", typeof(string));

            gvParametrizacion.DataSource = grid;
            gvParametrizacion.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGrid()
        {
            #region Variables
            string strErrMsg = string.Empty;
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOParametrizacion> lstParametrizacion = new List<clsDTOParametrizacion>();
            clsDTOVariable objVariable = new clsDTOVariable(string.Empty, string.Empty, string.Empty, true);
            #endregion

            lstParametrizacion = cParamArchivo.mtdCargarInfoParametrizacion(objVariable, ref strErrMsg);

            if (lstParametrizacion != null)
            {
                mtdLoadInfoGrid(lstParametrizacion);
                gvParametrizacion.DataSource = lstParametrizacion;
                gvParametrizacion.DataBind();
            }
        }

        private void mtdLoadInfoGrid(List<clsDTOParametrizacion> lstParametrizacion)
        {
            foreach (clsDTOParametrizacion objParametrizacion in lstParametrizacion)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objParametrizacion.StrIdCategoria.ToString().Trim(),
                    objParametrizacion.StrNombreCategoria.ToString().Trim(),
                    objParametrizacion.StrCodigoCategoria.ToString().Trim(),
                    objParametrizacion.StrIdVariable.ToString().Trim(),
                    objParametrizacion.StrNombreVariable.ToString().Trim(),
                    objParametrizacion.StrCalificacionCategoria.ToString().Trim(),
                    objParametrizacion.BooEsFormula.ToString()
                    });
            }
        }

        private void mtdLoadDDLTipoParametro()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsDTOVariable objVariable = new clsDTOVariable(string.Empty, string.Empty, string.Empty, true);
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOVariable> lstTipoParam = new List<clsDTOVariable>();
            #endregion Vars

            lstTipoParam = cParamArchivo.mtdCargarInfoVariables(objVariable, ref strErrMsg);

            if (lstTipoParam != null)
            {
                int intCounter = 1;
                ddlTipoParametro.Items.Clear();
                ddlTipoParametro.Items.Insert(0, new ListItem("", "0"));

                foreach (clsDTOVariable objTipo in lstTipoParam)
                {
                    ddlTipoParametro.Items.Insert(intCounter, new ListItem(objTipo.StrNombreVariable, objTipo.StrIdVariable));
                    intCounter++;
                }
            }
            else
                mtdMensaje(strErrMsg);
        }

        #region Operadores
        private void mtdLoadGridViewOperadores()
        {
            mtdLoadGridOps();
            mtdLoadInfoGridOps();
        }

        private void mtdLoadGridOps()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdOperador", typeof(string));
            grid.Columns.Add("StrNombreOperador", typeof(string));
            grid.Columns.Add("StrIdentificadorOperador", typeof(string));

            gvOperador.DataSource = grid;
            gvOperador.DataBind();
            InfoOpGrid = grid;
        }

        private void mtdLoadInfoGridOps()
        {
            string strErrMsg = string.Empty;
            clsSenal cSenal = new clsSenal();
            List<clsDTOOperador> lstOps = new List<clsDTOOperador>();

            lstOps = cSenal.mtdCargarInfoOps(ref strErrMsg);

            if (lstOps != null)
            {
                mtdLoadInfoGridOps(lstOps);
                gvOperador.DataSource = lstOps;
                gvOperador.DataBind();
            }
        }

        private void mtdLoadInfoGridOps(List<clsDTOOperador> lstOps)
        {
            foreach (clsDTOOperador objOp in lstOps)
            {
                InfoOpGrid.Rows.Add(new Object[] {
                    objOp.StrIdOperador.ToString().Trim(),
                    objOp.StrNombreOperador.ToString().Trim(),
                    objOp.StrIdentificadorOperador.ToString().Trim()
                    });
            }
        }
        #endregion
        #endregion Loads

        #region Methods
        private void mtdInicializarValores()
        {
            PagIndex = 0;
        }

        private void mtdResetValues()
        {
            tbNombreParametro.Text = string.Empty;
            tbCalificacion.Text = string.Empty;
            tbCodParametro.Text = string.Empty;
            tbxOtroValor.Text = string.Empty;
            tbxDesde.Text = string.Empty;
            tbxHasta.Text = string.Empty;
            ddlTipoParametro.SelectedIndex = 0;

            updateUser.Visible = false;
            ibtnGuardar.Visible = false;
            ibtnGuardarUpd.Visible = false;
            mtdHabilitarControlesFormula(1);
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
            mtdHabilitarControlRango(false);
            mtdHabilitarControlesFormula(1);

            #region Informacion a modificar
            tbNombreParametro.Text = InfoGrid.Rows[RowGrid]["StrNombreParametro"].ToString().Trim();
            tbCalificacion.Text = InfoGrid.Rows[RowGrid]["StrCalificacionParametro"].ToString().Trim();
            tbCodParametro.Text = InfoGrid.Rows[RowGrid]["StrCodigoParametro"].ToString().Trim();
            chbCondicion.Checked = InfoGrid.Rows[RowGrid]["BooEsFormula"].ToString().Trim() == "False" ? false : true;
            #endregion

            #region Habilitar Condicion
            if (chbCondicion.Checked)
            {
                mtdHabilitarControlesFormula(2);
                tbCodParametro.Enabled = false;
            }
            #endregion

            #region Ciclo ddlTipoParametro
            for (int i = 0; i < ddlTipoParametro.Items.Count; i++)
            {
                ddlTipoParametro.SelectedIndex = i;
                if (ddlTipoParametro.SelectedItem.Text.Trim() == InfoGrid.Rows[RowGrid]["StrNombreTipoParametro"].ToString().Trim())
                    break;
            }
            #endregion Ciclo ddlTipoParametro
        }

        private void mtdActualizarParametrizacion(string strIdParametros, string strNombreParametro, string strCodParametro,
            string strCalificacion, string strIdTipoParametro, bool booEsCondicion, ref string strErrMsg)
        {
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            clsDTOParametrizacion objParametrizacion = new clsDTOParametrizacion(strIdParametros, strIdTipoParametro,
                strNombreParametro, strCodParametro, strCalificacion, string.Empty, booEsCondicion);

            cParamArchivo.mtdActualizarParametrizacion(objParametrizacion, ref strErrMsg);
        }

        private void mtdAgregarParametrizacion(string strNombreParametro, string strCalificacion, string strCodParametro,
            string strIdTipoParametro, bool booEsCondicion, ref string strErrMsg)
        {
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            clsDTOParametrizacion objTipoParamIn = new clsDTOParametrizacion(string.Empty, strIdTipoParametro,
                strNombreParametro, strCodParametro, strCalificacion, string.Empty, booEsCondicion);

            cParamArchivo.mtdAgregarParametrizacion(objTipoParamIn, ref strErrMsg);
        }

        private void mtdHabilitarControlRango(bool booActivar)
        {
            lblDesde.Visible = booActivar;
            lblHasta.Visible = booActivar;
            tbxHasta.Visible = booActivar;
            tbxDesde.Visible = booActivar;
            ibtnRango.Visible = booActivar;

            tbxOtroValor.Visible = !booActivar;
            ibtnSelecOtroValor.Visible = !booActivar;
        }

        private void mtdSeleccionOperador()
        {
            tbCodParametro.Text = InfoOpGrid.Rows[IndexRowOp]["StrIdentificadorOperador"].ToString().Trim();
            mtdHabilitarControlesFormula(3);
        }

        private void mtdHabilitarControlesFormula(int intFase)
        {
            switch (intFase)
            {
                case 1:
                    gvOperador.Enabled = false;
                    ibtnSelecOtroValor.Enabled = false;
                    ibtnRango.Enabled = false;
                    break;
                case 2:
                    gvOperador.Enabled = true;
                    ibtnSelecOtroValor.Enabled = false;
                    break;
                case 3:
                    gvOperador.Enabled = false;
                    ibtnSelecOtroValor.Enabled = true;
                    ibtnRango.Enabled = true;
                    break;
            }
        }

        #endregion


    }
}