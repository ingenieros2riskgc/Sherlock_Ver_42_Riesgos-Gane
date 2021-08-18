using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Security.Application;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;
using ListasSarlaft.Classes;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class Indicadores : System.Web.UI.UserControl
    {
        string IdFormulario = "3011";
        cCuenta cCuenta = new cCuenta();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!IsPostBack)
                    PopulateTreeView();
            }
        }
        #region Treeview
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
            TreeView4.ExpandAll();
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
                TreeView4.Nodes.Add(newNode);
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

        protected void TreeView4_SelectedNodeChanged(object sender, EventArgs e)
        {
            txbResponsable.Text = TreeView4.SelectedNode.Text;
            lblIdDependencia4.Text = TreeView4.SelectedNode.Value;
        }
        #endregion Treeview
        public void mtdReset()
        {
            txtId.Text = string.Empty;
            txbIndicador.Text = string.Empty;
            txbMetodologiaNominador.Text = string.Empty;
            txbMetodologiaDenominador.Text = string.Empty;
            ddlFrecuencia.SelectedIndex = 0;
            txbMeta.Text = string.Empty;
            txbNivelCumplimiento.Text = string.Empty;
            txbResponsable.Text = string.Empty;
            tbxUsuarioCreacion.Text = string.Empty;
            txtFecha.Text = string.Empty;
        }

        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            BodyFormI.Visible = true;
            BodyGridI.Visible = false;

            IBinsertI.Visible = true;
            IBupdateI.Visible = false;
        }

        protected void IBinsertI_Click(object sender, ImageClickEventArgs e)
        {
            SqlDSindicadores.InsertParameters["Indicador"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbIndicador.Text);
            SqlDSindicadores.InsertParameters["MetodologiaNominador"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbMetodologiaNominador.Text);
            SqlDSindicadores.InsertParameters["MetodologiaDenominador"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbMetodologiaDenominador.Text);
            SqlDSindicadores.InsertParameters["Frecuencia"].DefaultValue = ddlFrecuencia.SelectedValue;
            SqlDSindicadores.InsertParameters["Responsable"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbResponsable.Text);
            SqlDSindicadores.InsertParameters["Meta"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbMeta.Text);
            SqlDSindicadores.InsertParameters["NivelCumplimiento"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbNivelCumplimiento.Text);
            SqlDSindicadores.InsertParameters["UsuarioRegistro"].DefaultValue = Session["IdUsuario"].ToString();
            SqlDSindicadores.InsertParameters["FechaRegistro"].DefaultValue = DateTime.Now.ToString();
            SqlDSindicadores.InsertParameters["año"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbAño.Text);
            SqlDSindicadores.InsertParameters["planeacion"].DefaultValue = ddlPlaneacion.SelectedValue;
            SqlDSindicadores.Insert();
            omb.ShowMessage("Indicador registrado exitosamente", 3, "Atención");
            BodyFormI.Visible = false;
            BodyGridI.Visible = true;

            GVIndicadores.DataBind();
            mtdClean();
        }

        protected void GVIndicadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGrid);
                    IBinsertI.Visible = false;
                    IBupdateI.Visible = true;
                    break;
            }
        }
        private void mtdShowUpdate(int RowGrid)
        {
            string strErrMsg = string.Empty;
            GridViewRow row = GVIndicadores.Rows[RowGrid];
            var colsNoVisible = GVIndicadores.DataKeys[RowGrid].Values;

            BodyFormI.Visible = true;
            BodyGridI.Visible = false;

            txtId.Text = row.Cells[0].Text;
            txbIndicador.Text = row.Cells[1].Text;
            txbMetodologiaNominador.Text = row.Cells[2].Text;
            txbMetodologiaDenominador.Text = row.Cells[3].Text;
            ddlFrecuencia.SelectedValue = row.Cells[4].Text;
            txbMeta.Text = colsNoVisible[0].ToString();
            txbNivelCumplimiento.Text = colsNoVisible[2].ToString();
            txbResponsable.Text = colsNoVisible[6].ToString();
            tbxUsuarioCreacion.Text = colsNoVisible[5].ToString();
            txtFecha.Text = colsNoVisible[4].ToString();
            txbAño.Text = row.Cells[5].Text;
            string planeacion = row.Cells[6].Text;
            if (planeacion != "" && planeacion != "&nbsp;")
                ddlPlaneacion.SelectedValue = planeacion;
        }

        protected void IBupdateI_Click(object sender, ImageClickEventArgs e)
        {
            SqlDSindicadores.UpdateParameters["IdIndicador"].DefaultValue = txtId.Text;
            SqlDSindicadores.UpdateParameters["Indicador"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbIndicador.Text);
            SqlDSindicadores.UpdateParameters["MetodologiaNominador"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbMetodologiaNominador.Text);
            SqlDSindicadores.UpdateParameters["MetodologiaDenominador"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbMetodologiaDenominador.Text);
            SqlDSindicadores.UpdateParameters["Frecuencia"].DefaultValue = ddlFrecuencia.SelectedValue;
            SqlDSindicadores.UpdateParameters["Responsable"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbResponsable.Text);
            SqlDSindicadores.UpdateParameters["Meta"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbMeta.Text);
            SqlDSindicadores.UpdateParameters["NivelCumplimiento"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbNivelCumplimiento.Text);
            SqlDSindicadores.UpdateParameters["año"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txbAño.Text);
            SqlDSindicadores.UpdateParameters["planeacion"].DefaultValue = ddlPlaneacion.SelectedValue;
            SqlDSindicadores.Update();
            omb.ShowMessage("Indicador actualizado exitosamente", 3, "Atención");
            BodyFormI.Visible = false;
            BodyGridI.Visible = true;

            GVIndicadores.DataBind();
            mtdClean();
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            BodyFormI.Visible = false;
            BodyGridI.Visible = true;
            GVIndicadores.DataBind();
            mtdClean();
        }
        public void mtdClean()
        {
            txtId.Text = string.Empty;
            txbIndicador.Text = string.Empty;
            txbMetodologiaNominador.Text = string.Empty;
            txbMetodologiaDenominador.Text = string.Empty;
            ddlFrecuencia.SelectedIndex = 0;
            txbResponsable.Text = string.Empty;
            txbMeta.Text = string.Empty;
            txbNivelCumplimiento.Text = string.Empty;
            tbxUsuarioCreacion.Text = string.Empty;
            txtFecha.Text = string.Empty;
            txbAño.Text = string.Empty;
            ddlPlaneacion.SelectedIndex = 0;
        }
    }
}