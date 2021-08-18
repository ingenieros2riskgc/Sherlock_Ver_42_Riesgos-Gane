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
    public partial class ConfEstructuraArchivo : System.Web.UI.UserControl
    {
        string IdFormulario = "10001";
        clsCuenta cCuenta = new clsCuenta();
        cCuenta ccCuenta = new cCuenta();
        // Trae las posiciones donde se guardan estos campos
        string SenalAlertaPosTipoIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosTipoIden"].ToString();
        string SenalAlertaPosNumeroIden = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNumeroIden"].ToString();
        string SenalAlertaPosNombre = System.Configuration.ConfigurationManager.AppSettings["SenalAlertaPosNombre"].ToString();

        #region Properties

        private int pagIndex;
        private DataTable infoGrid;
        private int rowGrid;

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
                infoGrid = (DataTable)ViewState["infGrid2"];
                return infoGrid;
            }
            set
            {
                infoGrid = value;
                ViewState["infGrid2"] = infoGrid;
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ccCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");

            if (!Page.IsPostBack)
            {
                mtdInicializarValores();
                mtdLoadGridView();
            }
        }

        #region Gridview
        protected void gvEstructura_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvEstructura.PageIndex = PagIndex;
            gvEstructura.DataBind();

            mtdLoadGridView();
        }

        protected void gvEstructura_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(gvEstructura.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Modificar":

                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvRow = gvEstructura.Rows[index];

                    if (!new[] { SenalAlertaPosTipoIden, SenalAlertaPosNumeroIden, SenalAlertaPosNombre }.Any(x => x == gvRow.Cells[8].Text))
                    {
                        mtdLoadDDLTipoParametro();
                        mtdModificar();
                    }
                    else
                    {
                        mtdMensaje("No se permiten modificaciones a este campo");
                        updateUser.Visible = false;
                    }

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
                mtdLoadDDLTipoParametro();
                mtdResetValues();

                ddlTipoParametro.SelectedIndex = 1;

                updateUser.Visible = true;
                ibtnGuardar.Visible = true;
                ibtnGuardarUpd.Visible = false;
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
                    if (ddlTipoParametro.SelectedIndex != 0)
                    {
                        if (new[] { SenalAlertaPosTipoIden, SenalAlertaPosNumeroIden, SenalAlertaPosNombre }.Any(x => x == tbPosicion.Text))
                        {
                            mtdMensaje("No se puede asignar la posición especificada.");
                            return;
                        }
                        mtdAgregarEstructuraCampo(Sanitizer.GetSafeHtmlFragment(tbNombreCampo.Text.Trim()), "1", "0", chbEsParametrico.Checked,
                            ddlTipoParametro.SelectedValue.ToString().Trim(), string.Empty, string.Empty, Sanitizer.GetSafeHtmlFragment(tbPosicion.Text.Trim()), ref strErrMsg);

                        mtdResetValues();
                        mtdLoadGridView();

                        if (string.IsNullOrEmpty(strErrMsg))
                            mtdMensaje("La estructura fue creada exitósamente.");
                        else
                            mtdMensaje(strErrMsg);
                    }
                    else
                        mtdMensaje("Por favor modifique el Tipo de Parámetro");
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al agregar el Tipo Parámetro. [" + ex.Message + "].");
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
                    if (new[] { SenalAlertaPosTipoIden, SenalAlertaPosNumeroIden, SenalAlertaPosNombre }.Any(x => x == tbPosicion.Text))
                    {
                        mtdMensaje("No se puede asignar la posición especificada.");
                        return;
                    }
                    mtdActualizarEstructura(InfoGrid.Rows[RowGrid]["StrIdEstructCampo"].ToString().Trim(),
                        Sanitizer.GetSafeHtmlFragment(tbNombreCampo.Text.Trim()), "1", "0", chbEsParametrico.Checked, ddlTipoParametro.SelectedValue,
                        string.Empty, string.Empty, Sanitizer.GetSafeHtmlFragment(tbPosicion.Text.Trim()), ref strErrMsg);

                    mtdResetValues();
                    mtdLoadGridView();

                    if (string.IsNullOrEmpty(strErrMsg))
                        mtdMensaje("La estructura fue actualizada exitósamente.");
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
        #endregion

        protected void chbEsParametrico_CheckedChanged(object sender, EventArgs e)
        {
            if (chbEsParametrico.Checked)
                ddlTipoParametro.Enabled = true;
            else
                ddlTipoParametro.Enabled = false;
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

            grid.Columns.Add("StrIdEstructCampo", typeof(string));
            grid.Columns.Add("StrNombreCampo", typeof(string));
            grid.Columns.Add("StrLongitud", typeof(string));
            grid.Columns.Add("BooParametrico", typeof(string));
            grid.Columns.Add("StrIdTipoParametro", typeof(string));
            grid.Columns.Add("StrNombreTipoParametro", typeof(string));
            grid.Columns.Add("StrIdTipoDato", typeof(string));
            grid.Columns.Add("StrNombreTipoDato", typeof(string));
            grid.Columns.Add("StrPosicion", typeof(string));

            gvEstructura.DataSource = grid;
            gvEstructura.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGrid()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsDTOVariable objVariable = new clsDTOVariable(string.Empty, string.Empty, string.Empty, true);
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOEstructuraCampo> lstEstructura = new List<clsDTOEstructuraCampo>();
            #endregion Vars

            lstEstructura = cParamArchivo.mtdCargarInfoEstructura(objVariable, ref strErrMsg);

            if (lstEstructura != null)
            {
                mtdLoadInfoGrid(lstEstructura);
                gvEstructura.DataSource = lstEstructura;
                gvEstructura.DataBind();

                // Se habilita el checkbox del estado de la columna
                foreach (GridViewRow gvrow in gvEstructura.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkEstado");
                    if (lstEstructura.Where(x => x.StrIdEstructCampo == gvrow.Cells[0].Text).Select(o => o.Estado).FirstOrDefault() == 1)
                        chk.Checked = true;
                    else
                        chk.Checked = false;
                    if (new[] { SenalAlertaPosTipoIden, SenalAlertaPosNumeroIden, SenalAlertaPosNombre }.Any(x => x == gvrow.Cells[8].Text))
                    {
                        chk.Enabled = false;
                    }
                }
            }
        }

        private void mtdLoadInfoGrid(List<clsDTOEstructuraCampo> lstEstructura)
        {
            foreach (clsDTOEstructuraCampo objEstructura in lstEstructura)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objEstructura.StrIdEstructCampo.ToString().Trim(),
                    objEstructura.StrNombreCampo.ToString().Trim(),
                    objEstructura.StrLongitud.ToString().Trim(),
                    objEstructura.BooEsParametrico,
                    objEstructura.StrIdVariable.ToString().Trim(),
                    objEstructura.StrNombreVariable.ToString().Trim(),
                    objEstructura.StrIdTipoDato.ToString().Trim(),
                    objEstructura.StrNombreTipoDato.ToString().Trim(),
                    objEstructura.StrPosicion.ToString().Trim(),
                    });
            }
        }

        private void mtdLoadDDLTipoParametro()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsDTOVariable objVariableIn = new clsDTOVariable(string.Empty, string.Empty, string.Empty, true);
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            List<clsDTOVariable> lstVariables = new List<clsDTOVariable>();
            #endregion Vars

            lstVariables = cParamArchivo.mtdCargarInfoVariables(objVariableIn, ref strErrMsg);

            if (lstVariables != null)
            {
                int intCounter = 1;
                ddlTipoParametro.Items.Clear();
                ddlTipoParametro.Items.Insert(0, new ListItem("", "0"));

                foreach (clsDTOVariable objVariable in lstVariables)
                {
                    ddlTipoParametro.Items.Insert(intCounter, new ListItem(objVariable.StrNombreVariable, objVariable.StrIdVariable));
                    intCounter++;
                }
            }
            else
                mtdMensaje(strErrMsg);
        }

        #endregion Loads

        #region Methods
        private void mtdInicializarValores()
        {
            PagIndex = 0;
        }

        private void mtdResetValues()
        {
            tbNombreCampo.Text = string.Empty;
            tbTipoDato.Text = string.Empty;
            tbLongitud.Text = string.Empty;
            tbPosicion.Text = string.Empty;
            chbEsParametrico.Checked = false;
            ddlTipoParametro.SelectedIndex = 0;

            ddlTipoParametro.Enabled = false;
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

            tbNombreCampo.Text = InfoGrid.Rows[RowGrid]["StrNombreCampo"].ToString().Trim();
            tbPosicion.Text = InfoGrid.Rows[RowGrid]["StrPosicion"].ToString().Trim();

            #region CheckBox
            chbEsParametrico.Checked = InfoGrid.Rows[RowGrid][3].ToString().Trim() == "True" ? true : false;

            if (chbEsParametrico.Checked)
                ddlTipoParametro.Enabled = true;
            else
                ddlTipoParametro.Enabled = false;
            #endregion CheckBox

            #region Ciclo ddlTipoParametro

            for (int i = 0; i < ddlTipoParametro.Items.Count; i++)
            {
                ddlTipoParametro.SelectedIndex = i;
                if (ddlTipoParametro.SelectedItem.Text.Trim() == InfoGrid.Rows[RowGrid]["StrNombreTipoParametro"].ToString().Trim())
                    break;
            }
            #endregion Ciclo ddlTipoParametro
        }

        private void mtdAgregarEstructuraCampo(string strNombreCampo, string strIdTipoDato, string strLongitud,
            bool booEsParametrico, string strIdTipoParametro, string strNombreTipoParametro, string strNombreTipoDato, string strPosicion, ref string strErrMsg)
        {
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            clsDTOEstructuraCampo objTipoParamIn = new clsDTOEstructuraCampo(string.Empty, strNombreCampo, strIdTipoDato, strLongitud,
                booEsParametrico, strIdTipoParametro, strNombreTipoParametro, strNombreTipoDato, strPosicion, 1);

            cParamArchivo.mtdAgregarEstructuraCampo(objTipoParamIn, ref strErrMsg);
        }

        private void mtdActualizarEstructura(string strIdEstrucCampo, string strNombreCampo, string strIdTipoDato, string strLongitud,
            bool booEsParametrico, string strIdTipoParametro, string strNombreTipoParametro, string strNombreTipoDato, string strPosicion, ref string strErrMsg)
        {
            clsParamArchivo cParamArchivo = new clsParamArchivo();
            clsDTOEstructuraCampo objTipoParamIn = new clsDTOEstructuraCampo(strIdEstrucCampo, strNombreCampo, strIdTipoDato, strLongitud,
                booEsParametrico, strIdTipoParametro, strNombreTipoParametro, strNombreTipoDato, strPosicion, 1);

            cParamArchivo.mtdActualizarEstructura(objTipoParamIn, ref strErrMsg);
        }
        #endregion Methods

        protected void chkEstado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                clsParamArchivo cParamArchivo = new clsParamArchivo();
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                int index = row.RowIndex;
                CheckBox cb1 = (CheckBox)gvEstructura.Rows[index].FindControl("chkEstado");
                int estado = cb1.Checked ? 1 : 0;
                string campo = row.Cells[0].Text;
                cParamArchivo.ActualizarEstadoCampo(Convert.ToInt32(campo), estado);
            }
            catch (Exception ex)
            {
                mtdMensaje($"Error al actualizar el estado {ex.Message}");
            }
        }
    }
}