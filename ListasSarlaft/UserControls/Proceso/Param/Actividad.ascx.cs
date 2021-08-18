using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ListasSarlaft.UserControls.Proceso
{
    public partial class Actividad : System.Web.UI.UserControl
    {
        string IdFormulario = "4007";
        cCuenta cCuenta = new cCuenta();

        #region Properties
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdInicializarValores();
                    if (mtdCargarActividades(ref strErrMsg))
                        omb.ShowMessage(strErrMsg, 3, "Atención");

                    PopulateTreeView();
                }
            }
        }

        #region Gridviews
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(GridView1.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Modificar":
                    mtdModificar();
                    break;
                case "Activar":
                    mtdActivar();
                    break;
            }
        }
        #endregion

        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView2.ExpandAll();
        }

        private DataTable GetTreeViewData()
        {
            string selectCommand = "SELECT PJO.IdHijo, PJO.IdPadre, PJO.NombreHijo, PDJ.NombreResponsable, PDJ.CorreoResponsable " +
                "FROM Parametrizacion.JerarquiaOrganizacional PJO LEFT JOIN Parametrizacion.DetalleJerarquiaOrg PDJ ON PJO.idHijo = PDJ.idHijo";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        private void AddTopTreeViewNodes(DataTable treeViewData)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";

            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                TreeView2.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                newNode.ToolTip = "Nombre: " + row["NombreResponsable"].ToString() + "\rCorreo: " + row["CorreoResponsable"].ToString();
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
            tbxCargoResponsable.Text = TreeView2.SelectedNode.Text;
            lblIdDependencia.Text = TreeView2.SelectedNode.Value;
        }
        #endregion Treeview

        #region Buttons
        /// <summary>
        /// Permite habilitar los campos para crear un nueva salida.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                tbxDescripcion.Focus();

                mtdResetCampos();

                tbxId.Enabled = false;
                tbxUsuarioCreacion.Enabled = false;
                tbxFecha.Enabled = false;
                ChBEstado.Enabled = true;

                btnImgInsertar.Visible = true;
                btnImgActualizar.Visible = false;
                filaDetalle.Visible = true;
                filaGrid.Visible = false;
                imgCopia1.Visible = true;
            }
        }

        /// <summary>
        /// Permite la insercion de la actividad.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (mtdInsertarActividad(ref strErrMsg))
                {
                    omb.ShowMessage("Actividad registrada exitosamente.", 3, "Atención");
                    filaDetalle.Visible = false;
                    filaGrid.Visible = true;

                    mtdResetCampos();
                    mtdCargarActividades(ref strErrMsg);
                }
                else
                    omb.ShowMessage(strErrMsg, 1, "Atención");
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error al registrar la actividad. <br/> Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    if (mtdActualizarActividad(ref strErrMsg))
                    {
                        omb.ShowMessage("Actividad modificada exitosamente.", 3, "Atención");
                        filaDetalle.Visible = false;
                        filaGrid.Visible = true;

                        mtdResetCampos();
                        mtdCargarActividades(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error al modificar la actividad." + "<br/>" + "Descripción: " + except.Message.ToString(), 1, "Atención");
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaGrid.Visible = true;
            filaDetalle.Visible = false;
        }

        protected void btnModificarEstado_Click(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;
            mpeMsgBox.Hide();

            try
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    if (mtdActualizarEstado(ref strErrMsg))
                    {
                        omb.ShowMessage("Actividad (in)activada exitosamente.", 3, "Atención");
                        filaDetalle.Visible = false;
                        filaGrid.Visible = true;

                        mtdResetCampos();
                        mtdCargarActividades(ref strErrMsg);
                    }
                    else
                        omb.ShowMessage(strErrMsg, 1, "Atención");
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al (in)activar la  actividad. <br/> Descripción: " + ex.Message.ToString(), 1, "Atención");
            }
        }

        #endregion

        #region Metodos

        private void mtdInicializarValores()
        {
            PagIndex = 0;
        }
        
        private void mtdResetCampos()
        {
            tbxId.Text = string.Empty;
            tbxDescripcion.Text = string.Empty;
            ChBEstado.Checked = true;
            tbxCargoResponsable.Text = string.Empty;
            lblIdDependencia.Text = string.Empty;
            tbxUsuarioCreacion.Text = string.Empty;
            tbxFecha.Text = string.Empty;
        }

        #region Cargas
        #region Gridview
        /// <summary>
        /// Metodo que se encarga de hacer el llamado para instanciar el Grid
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private bool mtdCargarActividades(ref string strErrMsg)
        {
            bool booResult = false;

            mtdLoadGridActividades();
            mtdLoadInfoGridActividades(ref strErrMsg);

            if (!string.IsNullOrEmpty(strErrMsg))
                booResult = true;

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadGridActividades()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strDescripcion", typeof(string));
            grid.Columns.Add("booEstado", typeof(string));
            grid.Columns.Add("intCargoResponsable", typeof(string));
            grid.Columns.Add("strNombreCargoResponsable", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strNombreUsuario", typeof(string));
            grid.Columns.Add("dtFechaRegistro", typeof(string));

            GridView1.DataSource = grid;
            GridView1.DataBind();
            InfoGrid = grid;
        }

        /// <summary>
        /// Hace el llamdo y la instancia de los campos de la actividad al grid.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        private void mtdLoadInfoGridActividades(ref string strErrMsg)
        {
            List<clsActividad> lstActividad = new List<clsActividad>();
            clsActividadBLL cActividad = new clsActividadBLL();

            lstActividad = cActividad.mtdConsultarActividad(ref strErrMsg);

            if (lstActividad != null)
            {
                mtdLoadInfoGridActividad(lstActividad);
                GridView1.DataSource = lstActividad;
                GridView1.PageIndex = PagIndex;
                GridView1.DataBind();
            }
        }

        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstSalida">Lista con las Actividades</param>
        private void mtdLoadInfoGridActividad(List<clsActividad> lstActividad)
        {
            foreach (clsActividad objActividad in lstActividad)
            {
                InfoGrid.Rows.Add(new Object[] {
                    objActividad.intId.ToString().Trim(),
                    objActividad.strDescripcion.ToString().Trim(),
                    objActividad.booEstado.ToString().Trim(),
                    objActividad.intCargoResponsable.ToString().Trim(),
                    objActividad.strNombreCargoResponsable.ToString().Trim(),
                    objActividad.intIdUsuario.ToString().Trim(),
                    objActividad.strNombreUsuario.ToString().Trim(),
                    objActividad.dtFechaRegistro.ToString().Trim()
                    });
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// Habilita los campos a modificar.
        /// </summary>
        private void mtdModificar()
        {
            filaGrid.Visible = false;
            filaDetalle.Visible = true;
            btnImgInsertar.Visible = false;
            btnImgActualizar.Visible = true;

            tbxId.Enabled = false;
            tbxFecha.Enabled = false;
            ChBEstado.Enabled = false;
            tbxUsuarioCreacion.Enabled = false;

            tbxDescripcion.Focus();

            // Carga los datos en la respectiva caja de texto
            tbxId.Text = InfoGrid.Rows[RowGrid][0].ToString().Trim();
            tbxDescripcion.Text = InfoGrid.Rows[RowGrid][1].ToString().Trim();

            #region CheckBox
            ChBEstado.Checked = InfoGrid.Rows[RowGrid][2].ToString().Trim() == "True" ? true : false;
            #endregion CheckBox

            tbxCargoResponsable.Text = InfoGrid.Rows[RowGrid][4].ToString().Trim();
            tbxCargoResponsable.Enabled = false;
            //imgCopia1.Visible = false;
            lblIdDependencia.Text = InfoGrid.Rows[RowGrid][3].ToString().Trim();

            tbxUsuarioCreacion.Text = InfoGrid.Rows[RowGrid][6].ToString().Trim();
            tbxFecha.Text = InfoGrid.Rows[RowGrid][7].ToString().Trim();
        }

        /// <summary>
        /// Permite mostar el mensaje de activar o inactivar.
        /// </summary>
        private void mtdActivar()
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                string strEstado = string.Empty;
                bool booEstado = InfoGrid.Rows[RowGrid][2].ToString().Trim() == "True" ? true : false;

                if (booEstado)
                    strEstado = "inactivar";
                else
                    strEstado = "activar";

                lblMsgBox.Text = string.Format("Desea {0} la actividad?", strEstado);
                mpeMsgBox.Show();
            }
        }

        /// <summary>
        /// Realiza la insercion de las actividades.
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>SI la operacion fue exitosa o no.</returns>
        private bool mtdInsertarActividad(ref string strErrMsg)
        {
            bool booResult = false;
            clsActividad objActividad = new clsActividad(0,
                tbxDescripcion.Text.Trim(),
                ChBEstado.Checked,
                lblIdDependencia.Text.Trim().Equals("") ? 0 :Convert.ToInt32(lblIdDependencia.Text.Trim()),
                Convert.ToInt32(Session["idUsuario"].ToString().Trim()),
                string.Empty);
            clsActividadBLL cActividad = new clsActividadBLL();

            booResult = cActividad.mtdInsertarActividad(objActividad, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarActividad(ref string strErrMsg)
        {
            bool booResult = false;
            clsActividad objActividad = new clsActividad(
                Convert.ToInt32(tbxId.Text.Trim()),
                tbxDescripcion.Text.Trim(),
                ChBEstado.Checked,
                lblIdDependencia.Text.Trim().Equals("") ? 0 : Convert.ToInt32(lblIdDependencia.Text.Trim()),
                0,
                string.Empty);
            clsActividadBLL cActividad = new clsActividadBLL();

            booResult = cActividad.mtdActualizarActividad(objActividad, ref strErrMsg);

            return booResult;
        }

        /// <summary>
        /// Realiza la modificacion de los campos editados
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Si el proceso fue exitoso o no</returns>
        private bool mtdActualizarEstado(ref string strErrMsg)
        {
            bool booResult = false;
            clsActividad objActividad = new clsActividad(
                Convert.ToInt32(InfoGrid.Rows[RowGrid][0].ToString().Trim()),
                string.Empty,
                !(InfoGrid.Rows[RowGrid][2].ToString().Trim() == "True" ? true : false),
                0, 0, string.Empty);
            clsActividadBLL cActividad = new clsActividadBLL();

            booResult = cActividad.mtdActualizarEstado(objActividad, ref strErrMsg);

            return booResult;
        }
        #endregion

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GridView1.PageIndex = PagIndex;
            GridView1.DataBind();
            string strErrMsg = "";
            mtdLoadInfoGridActividades(ref strErrMsg);
        }
    }
}