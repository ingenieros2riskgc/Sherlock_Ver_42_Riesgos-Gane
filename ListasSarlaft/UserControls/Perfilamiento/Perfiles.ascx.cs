using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using clsLogica;
using clsDTO;
//using ListasSarlaft.Classes;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.Perfilamiento
{
    public partial class Perfiles : System.Web.UI.UserControl
    {
        string IdFormulario = "11002";
        clsCuenta cCuenta = new clsCuenta();
        ListasSarlaft.Classes.cCuenta ccCuenta = new ListasSarlaft.Classes.cCuenta();

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
        protected void gvPerfiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            gvPerfiles.PageIndex = PagIndex;
            gvPerfiles.DataBind();

            mtdLoadGridView();
        }

        protected void gvPerfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(gvPerfiles.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdModificar();
                    break;
            }
        }
        #endregion Gridview

        #region Loads
        private void mtdLoadGridView()
        {
            mtdLoadGrid();
            mtdLoadInfoGrid();
        }

        private void mtdLoadGrid()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("StrIdPerfil", typeof(string));
            grid.Columns.Add("StrNombrePerfil", typeof(string));
            grid.Columns.Add("StrValorMinimo", typeof(string));
            grid.Columns.Add("StrValorMaximo", typeof(string));

            gvPerfiles.DataSource = grid;
            gvPerfiles.DataBind();
            InfoGrid = grid;
        }

        private void mtdLoadInfoGrid()
        {
            #region Vars
            string strErrMsg = string.Empty;
            clsPerfil cPerfil = new clsPerfil();
            List<clsDTOPerfil> lstPerfiles = new List<clsDTOPerfil>();
            #endregion Vars

            lstPerfiles = cPerfil.mtdCargarInfoPerfiles(ref strErrMsg);

            if (lstPerfiles != null)
            {
                mtdLoadInfoGrid(lstPerfiles);
                gvPerfiles.DataSource = lstPerfiles;
                gvPerfiles.DataBind();
            }
        }

        private void mtdLoadInfoGrid(List<clsDTOPerfil> lstPerfiles)
        {
            foreach (clsDTOPerfil objPerfil in lstPerfiles)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objPerfil.StrIdPerfil.ToString().Trim(),
                    objPerfil.StrNombrePerfil.ToString().Trim(),
                    objPerfil.StrValorMinimo.ToString().Trim(),
                    objPerfil.StrValorMaximo.ToString().Trim()    
                    });
            }
        }
        #endregion Loads

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
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosAgregar(Convert.ToInt32(Session["IdRol"].ToString()), IdFormulario) == "False")
                    mtdMensaje("No tiene los permisos suficientes para llevar a cabo esta acción.");
                else
                {
                    if (mtdValidarRango(Sanitizer.GetSafeHtmlFragment(tbValorMinimo.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbValorMaximo.Text.Trim())))
                    {
                        if (Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(tbValorMaximo.Text.Trim())) <= 100)
                        {
                            mtdAgregarPerfil(Sanitizer.GetSafeHtmlFragment(tbNombrePerfil.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbValorMinimo.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbValorMaximo.Text.Trim()), ref strErrMsg);

                            mtdResetValues();
                            mtdLoadGridView();
                        }
                        else
                            strErrMsg = "El rango máximo debe ser menor o igual a 100.";
                    }
                    else
                        strErrMsg = "El rango mínimo debe ser inferior o igual al rango máximo.";

                    if (string.IsNullOrEmpty(strErrMsg))
                        mtdMensaje("El perfil fue creada exitósamente.");
                    else
                        mtdMensaje(strErrMsg);

                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al agregar el perfil. [" + ex.Message + "].");
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
                    if (mtdValidarRango(Sanitizer.GetSafeHtmlFragment(tbValorMinimo.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbValorMaximo.Text.Trim())))
                    {
                        if (Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(tbValorMaximo.Text.Trim())) <= 100)
                        {
                            mtdActualizarPerfil(InfoGrid.Rows[RowGrid]["StrIdPerfil"].ToString().Trim(),
                                Sanitizer.GetSafeHtmlFragment(tbNombrePerfil.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbValorMinimo.Text.Trim()), Sanitizer.GetSafeHtmlFragment(tbValorMaximo.Text.Trim()), ref strErrMsg);

                            mtdResetValues();
                            mtdLoadGridView();
                        }
                        else
                            strErrMsg = "El rango máximo debe ser menor o igual a 100.";
                    }
                    else
                        strErrMsg = "El rango mínimo debe ser inferior o igual al rango máximo.";

                    if (string.IsNullOrEmpty(strErrMsg))
                        mtdMensaje("El perfil fue actualizado exitósamente.");
                    else
                        mtdMensaje(strErrMsg);
                }
            }
            catch (Exception ex)
            {
                mtdMensaje("Error al modificar el perfil. " + ex.Message);
            }
        }

        protected void ibtnCancelUpd_Click(object sender, EventArgs e)
        {
            mtdResetValues();
        }
        #endregion

        private void mtdInicializarValores()
        {
            PagIndex = 0;
        }

        private void mtdModificar()
        {
            updateUser.Visible = true;
            ibtnGuardar.Visible = false;
            ibtnGuardarUpd.Visible = true;

            tbNombrePerfil.Text = InfoGrid.Rows[RowGrid]["StrNombrePerfil"].ToString().Trim();
            tbValorMinimo.Text = InfoGrid.Rows[RowGrid]["StrValorMinimo"].ToString().Trim();
            tbValorMaximo.Text = InfoGrid.Rows[RowGrid]["StrValorMaximo"].ToString().Trim();
        }

        private void mtdResetValues()
        {
            tbNombrePerfil.Text = string.Empty;
            tbValorMinimo.Text = string.Empty;
            tbValorMaximo.Text = string.Empty;

            updateUser.Visible = false;
            ibtnGuardar.Visible = false;
            ibtnGuardarUpd.Visible = false;
        }

        private void mtdMensaje(string Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        private bool mtdValidarRango(string strValorMinimo, string strValorMaximo)
        {
            bool booResult = true;

            if (Convert.ToInt32(strValorMinimo) > Convert.ToInt32(strValorMaximo))
                booResult = false;

            return booResult;
        }

        private void mtdAgregarPerfil(string strNombrePerfil, string strValorMinimo, string strValorMaximo, ref string strErrMsg)
        {
            clsPerfil cPerfil = new clsPerfil();
            clsDTOPerfil objPerfil = new clsDTOPerfil(string.Empty, strNombrePerfil, strValorMinimo, strValorMaximo);

            cPerfil.mtdAgregarPerfil(objPerfil, ref strErrMsg);
        }

        private void mtdActualizarPerfil(string strIdPerfil, string strNombrePerfil, string strValorMinimo, string strValorMaximo, ref string strErrMsg)
        {
            clsPerfil cPerfil = new clsPerfil();
            clsDTOPerfil objPerfil = new clsDTOPerfil(strIdPerfil, strNombrePerfil, strValorMinimo, strValorMaximo);

            cPerfil.mtdActualizarPerfil(objPerfil, ref strErrMsg);
        }    
    }
}