using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using Microsoft.Security.Application;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class Verificacion : System.Web.UI.UserControl
    {
        cCuenta cCuenta = new cCuenta();
        String IdFormulario = "3005";
        cAuditoria cAuditoria = new cAuditoria();
        cRiesgo cRiesgo = new cRiesgo();
        cEvento cEvento = new cEvento();
        private int rowGridAuditoria;
        private int RowGridAuditoria
        {
            get
            {
                rowGridAuditoria = (int)ViewState["rowGridAuditoria"];
                return rowGridAuditoria;
            }
            set
            {
                rowGridAuditoria = value;
                ViewState["rowGridAuditoria"] = rowGridAuditoria;
            }
        }
        private DataTable infoGridAuditorias;
        private DataTable InfoGridAuditorias
        {
            get
            {
                infoGridAuditorias = (DataTable)ViewState["infoGridAuditorias"];
                return infoGridAuditorias;
            }
            set
            {
                infoGridAuditorias = value;
                ViewState["infoGridAuditorias"] = infoGridAuditorias;
            }
        }
        private int pagIndexInfoGridAuditoria;
        private int PagIndexInfoGridAuditoria
        {
            get
            {
                pagIndexInfoGridAuditoria = (int)ViewState["pagIndexInfoGridAuditoria"];
                return pagIndexInfoGridAuditoria;
            }
            set
            {
                pagIndexInfoGridAuditoria = value;
                ViewState["pagIndexInfoGridAuditoria"] = pagIndexInfoGridAuditoria;
            }
        }
        private DataTable infoGridEventos;
        private DataTable InfoGridEventos
        {
            get
            {
                infoGridEventos = (DataTable)ViewState["infoGridEventos"];
                return infoGridEventos;
            }
            set
            {
                infoGridEventos = value;
                ViewState["infoGridEventos"] = infoGridEventos;
            }
        }

        private int rowGridEventos;
        private int RowGridEventos
        {
            get
            {
                rowGridEventos = (int)ViewState["rowGridEventos"];
                return rowGridEventos;
            }
            set
            {
                rowGridEventos = value;
                ViewState["rowGridEventos"] = rowGridEventos;
            }
        }
        private int pagIndexInfoGridEventos;
        private int PagIndexInfoGridEventos
        {
            get
            {
                pagIndexInfoGridEventos = (int)ViewState["pagIndexInfoGridEventos"];
                return pagIndexInfoGridEventos;
            }
            set
            {
                pagIndexInfoGridEventos = value;
                ViewState["pagIndexInfoGridEventos"] = pagIndexInfoGridEventos;
            }
        }
        private static int LastInsertIdCE;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                /*Page.Form.Attributes.Add("enctype", "multipart/form-data");
                ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
                scripManager.RegisterPostBackControl(imgBtnAdjuntar);
                scripManager.RegisterPostBackControl(GridView100);
                scripManager.RegisterPostBackControl(imgBtnInsertarArchivo);
                scripManager.RegisterPostBackControl(ImageButton18);*/
                SqlDataSource8.SelectParameters["IdArea"].DefaultValue = Session["IdAreaUser"].ToString();
                if (!Page.IsPostBack)
                {
                    PopulateTreeView();

                    GridView1.DataBind();
                    GridView11.DataBind();
                    GridView13.DataBind();
                    mtdLoadMetod();

                    TabContainer1.ActiveTabIndex = 0;
                }
            }
        }

        public void mtdLoadMetod()
        {
            PagIndexInfoGridEventos = 0;
            loadDDLCadenaValor();
        }

        #region Treeview

        /// <summary>
        /// Get the data from the database and create the top-level
        /// TreeView items
        /// </summary>
        private void PopulateTreeView()
        {
            TreeNodeCollection nodes = this.TreeView1.Nodes;
            if (nodes.Count <= 0)
            {
                DataTable treeViewData = GetTreeViewData();
                AddTopTreeViewNodes(treeViewData, 1);
            }

            nodes = this.TreeView2.Nodes;
            if (nodes.Count <= 0)
            {
                DataTable treeViewData2 = GetTreeViewData();
                AddTopTreeViewNodes(treeViewData2, 2);
            }

            nodes = this.TreeView3.Nodes;
            if (nodes.Count <= 0)
            {
                DataTable treeViewData3 = GetTreeViewData();
                AddTopTreeViewNodes(treeViewData3, 3);
            }
        }

        /// <summary>
        /// Use a DataAdapter and DataTable to grab the database data
        /// </summary>
        /// <returns></returns>
        private DataTable GetTreeViewData()
        {
            // Get JerarquiaOrganizacional table
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional]";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        /// <summary>
        /// Filter the data to get only the rows that have a
        /// null ParentID (these are the top-level TreeView items)
        /// </summary>
        private void AddTopTreeViewNodes(DataTable treeViewData, int Arbol)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                if (Arbol == 1)
                    TreeView1.Nodes.Add(newNode);
                else if (Arbol == 2)
                    TreeView2.Nodes.Add(newNode);
                else if (Arbol == 3)
                    TreeView3.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString(), row["IdHijo"].ToString());
                TreeViewlast.Nodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        /// <summary>
        /// Recursively add child TreeView items by filtering by ParentID
        /// </summary>
        private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                //newNode.SelectAction = TreeNodeSelectAction.Select;
                //newNode.NavigateUrl = "javascript:void(0)";
                //newNode.Target = "";
                newNode.ToolTip = DetalleNodo(1, row["IdHijo"].ToString());
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (filaDetalleRecomendacion.Visible == true)
            {
                txtDependenciaRec1.Text = TreeView1.SelectedNode.Text;
                lblIdDependenciaRec1.Text = TreeView1.SelectedNode.Value;
            }
        }

        protected void TreeView2_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtDependenciaRec2.Text = TreeView2.SelectedNode.Text;
            lblIdDependenciaRec2.Text = TreeView2.SelectedNode.Value;

            string subcadena;
            subcadena = TreeView2.SelectedNode.ToolTip;
            subcadena = subcadena.Substring(subcadena.IndexOf("Correo:") + 7);
            lblCorreoDepedenciaRec2.Text = subcadena;
        }

        protected void TreeView3_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtDependenciaRie.Text = TreeView3.SelectedNode.Text;
            lblIdDependenciaRie.Text = TreeView3.SelectedNode.Value;

            string subcadena;
            subcadena = TreeView3.SelectedNode.ToolTip;
            subcadena = subcadena.Substring(subcadena.IndexOf("Correo:") + 7);
            lblCorreoDependenciaRie.Text = subcadena;
        }

        /// <summary>
        /// Get the data from the database and create the top-level
        /// TreeView items
        /// </summary>
        private void PopulateTreeViewGA()
        {
            DataTable treeViewData = GetTreeViewDataGA();
            AddTopTreeViewNodesGA(treeViewData);
        }

        /// <summary>
        /// Use a DataAdapter and DataTable to grab the database data
        /// </summary>
        /// <returns></returns>
        private DataTable GetTreeViewDataGA()
        {
            // Get JerarquiaOrganizacional table
            // string selectCommand = "SELECT IdHijo,IdPadre,NombreHijoAuditoria FROM [Auditoria].[JerarquiaGrupoAuditoria] WHERE idGrupoAuditoria = " + ddlGrupoAud.SelectedValue;
            //string selectCommand = "SELECT	JGA.IdHijo,JGA.IdPadre,JGA.NombreHijoAuditoria FROM [Auditoria].[JerarquiaGrupoAuditoria] AS JGA, [Auditoria].[AuditoriaObjetivo] AS AO WHERE JGA.idGrupoAuditoria = AO.IdGrupoAuditoria AND AO.IdAuditoria = " + txtCodAuditoriaSel.Text + " AND AO.IdObjetivo = " + txtCodObjetivoSel.Text;
            string selectCommand = "SELECT distinct(JGA.IdHijo),JGA.IdPadre,JGA.NombreHijoAuditoria " + "\n" +
                "FROM [Auditoria].[JerarquiaGrupoAuditoria] AS JGA "+"\n"+
                "INNER JOIN Auditoria.AudObjRecurso AS AAOR on AAOR.IdGrupoAuditoria = JGA.idGrupoAuditoria "+"\n"+
                "WHERE AAOR.IdAuditoria = "+ txtCodAuditoriaSel.Text;
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);
            return dtblDiscuss;
        }

        /// <summary>
        /// Filter the data to get only the rows that have a
        /// null ParentID (these are the top-level TreeView items)
        /// </summary>
        private void AddTopTreeViewNodesGA(DataTable treeViewData)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = 0";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijoAuditoria"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.Expanded = true;
                newNode.ToolTip = DetalleNodo(2, row["IdHijo"].ToString());
                TVGrupoAud.Nodes.Add(newNode);
                AddChildTreeViewNodesGA(treeViewData, newNode);
            }
        }

        /// <summary>
        /// Recursively add child TreeView items by filtering by ParentID
        /// </summary>
        private void AddChildTreeViewNodesGA(DataTable treeViewData, TreeNode parentTreeViewNode)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = " + parentTreeViewNode.Value;
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijoAuditoria"].ToString().Trim(), row["IdHijo"].ToString());
                newNode.Expanded = true;
                newNode.ToolTip = DetalleNodo(2, row["IdHijo"].ToString());
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodesGA(treeViewData, newNode);
            }
        }

        protected void TVGrupoAud_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtRecurso.Text = TVGrupoAud.SelectedNode.Text;
            lblCodNodoGA.Text = TVGrupoAud.SelectedNode.Value;
        }
        #endregion

        #region DDL

        protected void ddlRecPoD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRecPoD.SelectedItem.Value == "Procesos")
            {
                filaDependenciaRec.Visible = false;
                filaProcesoRec.Visible = true;
            }
            else
            {
                filaDependenciaRec.Visible = true;
                filaProcesoRec.Visible = false;
            }
        }

        protected void ddlRiePoD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRiePoD.SelectedItem.Value == "Procesos")
            {
                filaDependenciaRie.Visible = false;
                filaProcesoRie.Visible = true;
            }
            else
            {
                filaDependenciaRie.Visible = true;
                filaProcesoRie.Visible = false;
            }
        }

        protected void ddlMacroProceso_DataBound(object sender, EventArgs e)
        {
            ddlMacroProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlProceso_DataBound(object sender, EventArgs e)
        {
            ddlProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlMacroProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProceso.Items.Clear();
            ddlProceso.DataBind();
            txtProcesoRec.Text = "";
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filaDetalleRecomendacion.Visible == true)
            {
                txtProcesoRec.Text = ddlProceso.SelectedItem.Text;
                lblIdProcesoRec.Text = ddlProceso.SelectedValue;
            }
            else if (filaDetalleRiesgo.Visible == true)
            {
                txtProcesoRie.Text = ddlProceso.SelectedItem.Text;
                lblIdProcesoRie.Text = ddlProceso.SelectedValue;
            }
        }

        protected void ddlMacroProcesoRie_DataBound(object sender, EventArgs e)
        {
            ddlMacroProcesoRie.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlProcesoRie_DataBound(object sender, EventArgs e)
        {
            ddlProcesoRie.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlMacroProcesoRie_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProcesoRie.Items.Clear();
            ddlProcesoRie.DataBind();
            txtProcesoRie.Text = "";
        }

        protected void ddlProcesoRie_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProcesoRie.Text = ddlProcesoRie.SelectedItem.Text;
            lblIdProcesoRie.Text = ddlProcesoRie.SelectedValue;
        }

        protected void ddlTipoHallazgo_DataBound(object sender, EventArgs e)
        {
            ddlTipoHallazgo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlEstadoHallazgo_DataBound(object sender, EventArgs e)
        {
            ddlEstadoHallazgo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlProbabilidad_DataBound(object sender, EventArgs e)
        {
            ddlProbabilidad.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlImpacto_DataBound(object sender, EventArgs e)
        {
            ddlImpacto.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlTipoRiesgo_DataBound(object sender, EventArgs e)
        {
            ddlTipoRiesgo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlNivelRiesgo_DataBound(object sender, EventArgs e)
        {
            ddlNivelRiesgo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        private void loadDDLCadenaValor()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLCadenaValor();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlCadenaValor.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCadenaValor"].ToString().Trim(), dtInfo.Rows[i]["IdCadenaValor"].ToString()));
                    DropDownList67.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCadenaValor"].ToString().Trim(), dtInfo.Rows[i]["IdCadenaValor"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar cadena valor. " + ex.Message,2,"Atención");
            }
        }
        private void loadDDLMacroproceso(String IdCadenaValor)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLMacroproceso(IdCadenaValor);
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlMacroprocesoEventos.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar macroproceso. " + ex.Message, 2, "Atención");
            }
        }
        private void loadDDLProceso(String IdMacroproceso)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLProceso(IdMacroproceso);
                
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                    ddlProcesoEvento.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProceso"].ToString().Trim(), dtInfo.Rows[i]["IdProceso"].ToString()));
                            }
                       
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar proceso. " + ex.Message, 2, "Atención");
            }
        }
        private void loadDDLSubProceso(String IdProceso)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLSubProceso(IdProceso);
                
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                ddlSubproceso.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreSubProceso"].ToString().Trim(), dtInfo.Rows[i]["IdSubProceso"].ToString()));
                            }
                       
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar subproceso. " + ex.Message,2,"Atención");
            }
        }
        #endregion

        #region Buttons

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalle.Visible = false;
        }

        protected void btnTemasAud_Click(object sender, EventArgs e)
        {
            filaDetalle.Visible = false;
            filaAcciones.Visible = true;
            GridView1.DataBind();
        }

        protected void btnImgEliminar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgokEliminar_Click(object sender, EventArgs e)
        {
            bool err = false;
            string TextoAdicional = string.Empty;
            mpeMsgBox.Hide();

            if (lblAccion.Text != "APROBAR" && lblAccion.Text != "ANULAR" && lblAccion.Text != "TareasP" && lblAccion.Text != "DevolverAuditor")
            {
                #region
                try
                {
                    if (TabContainer1.ActiveTabIndex.ToString() == "0")
                    {
                        SqlDataSource1.DeleteParameters["IdHallazgo"].DefaultValue = txtCodHallazgo.Text;
                        SqlDataSource1.Delete();
                    }
                    else if (TabContainer1.ActiveTabIndex.ToString() == "1")
                    {
                        SqlDataSource22.DeleteParameters["IdRecomendacion"].DefaultValue = txtCodRec.Text;
                        SqlDataSource22.Delete();
                    }
                    else if (TabContainer1.ActiveTabIndex.ToString() == "2")
                    {
                        SqlDataSource23.DeleteParameters["IdRiesgo"].DefaultValue = txtCodRiesgo.Text;
                        SqlDataSource23.Delete();
                    }
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la eliminación de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }
                #endregion

                if (!err)
                {
                    omb.ShowMessage("La información se eliminó con éxito en la Base de Datos.", 3, "Atención");
                    if (TabContainer1.ActiveTabIndex.ToString() == "0")
                    {
                        TabContainer1.Tabs[1].Enabled = false;
                        TabContainer1.Tabs[2].Enabled = false;
                    }
                }
            }
            else if (lblAccion.Text == "APROBAR")
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    #region
                    try
                    {
                        SqlDataSource24.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                        //04-04-2014
                        SqlDataSource24.UpdateParameters["Estado"].DefaultValue = "INFORME";
                        SqlDataSource24.Update();
                        cAuditoria.ActualizarLogHistoricoAudutoria(txtCodAuditoriaSel.Text, "NULL", "NULL", "GETDATE()", "NO");
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización del estado de la auditoría." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                        err = true;
                    }
                    #endregion

                    if (!err)
                    {
                        filaAcciones.Visible = false;
                        filaTabGestion.Visible = false;
                        filaMetodologia.Visible = false;
                        filaConclusion.Visible = false;
                        filaObs.Visible = false;

                        txtCodAuditoriaSel.Text = "";
                        txtCodObjetivoSel.Text = "";
                        txtCodEnfoqueSel.Text = "";
                        txtCodLiteralSel.Text = "";
                        txtNomAuditoriaSel.Text = "";
                        txtNomObjetivoSel.Text = "";
                        txtNomEnfoqueSel.Text = "";
                        txtNomLiteralSel.Text = "";
                        txtNomEnfoqueSel.Height = 18;
                        txtNomEnfoqueSel.Width = 402;
                        txtNomLiteralSel.Height = 18;
                        txtNomLiteralSel.Width = 402;

                        GridView6.DataBind();
                        imgBtnAuditoria.Focus();
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                    }
                }
            }
            else if (lblAccion.Text == "ANULAR")
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    #region
                    try
                    {
                        SqlDataSource24.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                        SqlDataSource24.UpdateParameters["Estado"].DefaultValue = "ANULADA";
                        SqlDataSource24.Update();
                        cAuditoria.AnularAuditoria(txtCodAuditoriaSel.Text.Trim(), TextBox1.Text.Trim());
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización del estado de la auditoría." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                        err = true;
                    }
                    #endregion

                    if (!err)
                    {
                        filaAcciones.Visible = false;
                        filaTabGestion.Visible = false;
                        txtCodAuditoriaSel.Text = "";
                        txtCodObjetivoSel.Text = "";
                        txtCodEnfoqueSel.Text = "";
                        txtCodLiteralSel.Text = "";
                        txtNomAuditoriaSel.Text = "";
                        txtNomObjetivoSel.Text = "";
                        txtNomEnfoqueSel.Text = "";
                        txtNomLiteralSel.Text = "";
                        txtNomEnfoqueSel.Height = 18;
                        txtNomEnfoqueSel.Width = 402;
                        txtNomLiteralSel.Height = 18;
                        txtNomLiteralSel.Width = 402;
                        GridView6.DataBind();
                        imgBtnAuditoria.Focus();
                        TextBox1.Text = "";
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                    }
                }
            }
            else if (lblAccion.Text == "TareasP")
            {
                #region
                try
                {
                    SqlDataSource9.DeleteParameters["IdTareasPendientes"].DefaultValue = txtCodigoTP.Text;
                    SqlDataSource9.Delete();
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la eliminación de la tarea." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }
                #endregion

                if (!err)
                    omb.ShowMessage("La información se eliminó con éxito en la Base de Datos.", 3, "Atención");
            }
            else if (lblAccion.Text == "DevolverAuditor")
            {
                if (cCuenta.permisosActualizar(IdFormulario) == "False")
                {
                    //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                    lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                    mpeMsgBox.Show();
                }
                else
                {
                    try
                    {
                        TextoAdicional = "Planeación Código: " + txtCodPlaneacion.Text + ", Nombre: " + txtNomPlaneacion.Text + "<br>";
                        TextoAdicional = TextoAdicional + "Auditoría Código: " + txtCodAuditoriaSel.Text + ", Nombre: " + txtNomAuditoriaSel.Text + "<br>";
                        SqlDataSource24.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                        SqlDataSource24.UpdateParameters["Estado"].DefaultValue = "EJECUCIÓN";
                        SqlDataSource24.Update();
                        try
                        {
                            boolEnviarNotificacion(21, Convert.ToInt32(txtCodAuditoriaSel.Text.Trim()), 2, "", TextoAdicional);
                        }
                        catch (Exception except)
                        {
                            omb.ShowMessage("Error en el envío de la notificació." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                            err = true;
                        }

                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización del estado de la auditoría." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                        err = true;
                    }

                    if (!err)
                    {
                        filaAcciones.Visible = false;
                        filaTabGestion.Visible = false;
                        txtCodAuditoriaSel.Text = "";
                        txtCodObjetivoSel.Text = "";
                        txtCodEnfoqueSel.Text = "";
                        txtCodLiteralSel.Text = "";
                        txtNomAuditoriaSel.Text = "";
                        txtNomObjetivoSel.Text = "";
                        txtNomEnfoqueSel.Text = "";
                        txtNomLiteralSel.Text = "";
                        txtNomEnfoqueSel.Height = 18;
                        txtNomEnfoqueSel.Width = 402;
                        txtNomLiteralSel.Height = 18;
                        txtNomLiteralSel.Width = 402;
                        GridView6.DataBind();
                        imgBtnAuditoria.Focus();
                        TextBox1.Text = "";
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                    }
                }
            }
        }

        protected void btnImgCancelarHallazgo_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleHallazgo.Visible = false;
            filaTabGestion.Visible = true;
            filaAcciones.Visible = true;
        }

        protected void btnImgCancelarRec_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleRecomendacion.Visible = false;
            filaTabGestion.Visible = true;
            filaAcciones.Visible = true;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgCancelarRie_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleRiesgo.Visible = false;
            filaTabGestion.Visible = true;
            filaAcciones.Visible = true;

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgEliminarRec_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblAccion.Text = "Recomendacion";
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgEliminarRie_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblAccion.Text = "Riesgo";
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        #region Hallazgo
        protected void btnImgInsertarHallazgo_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false;
            string strErrMsg = string.Empty;
            clsHallazgosAuditoriaDAL cHallazgo = new clsHallazgosAuditoriaDAL();
            DataTable dtInfo = new DataTable();
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    int IdAuditoria = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaSel.Text));
                    int Secuencia = 0;
                    dtInfo = cHallazgo.mtdGetSecuencia(ref strErrMsg, IdAuditoria);
                    if (dtInfo != null)
                    {
                        if (dtInfo.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtInfo.Rows)
                            {
                                Secuencia = Convert.ToInt32(dr["CountH"].ToString()) + 1;
                            }
                        }
                    }
                    SqlDataSource1.InsertParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                    SqlDataSource1.InsertParameters["IdDetalleEnfoque"].DefaultValue = txtCodEnfoqueSel.Text;
                    SqlDataSource1.InsertParameters["IdDetalleTipoHallazgo"].DefaultValue = ddlTipoHallazgo.SelectedValue;
                    SqlDataSource1.InsertParameters["IdEstado"].DefaultValue = ddlEstadoHallazgo.SelectedValue;
                    SqlDataSource1.InsertParameters["Hallazgo"].DefaultValue = txtHallazgo.Text;
                    SqlDataSource1.InsertParameters["ComentarioAuditado"].DefaultValue = txtComentario.Text;
                    SqlDataSource1.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource1.InsertParameters["IdNivelRiesgo"].DefaultValue = ddlNivelRiesgo.SelectedValue;
                    SqlDataSource1.InsertParameters["secuencia"].DefaultValue = Secuencia.ToString();
                    SqlDataSource1.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaTabGestion.Visible = true;
                    filaAcciones.Visible = true;
                    filaDetalleHallazgo.Visible = false;
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgActualizarHallazgo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (VerificarCampos())
                {
                    try
                    {

                        SqlDataSource1.UpdateParameters["IdDetalleTipoHallazgo"].DefaultValue = ddlTipoHallazgo.SelectedValue;
                        SqlDataSource1.UpdateParameters["IdEstado"].DefaultValue = ddlEstadoHallazgo.SelectedValue;
                        SqlDataSource1.UpdateParameters["Hallazgo"].DefaultValue = txtHallazgo.Text;
                        SqlDataSource1.UpdateParameters["IdHallazgo"].DefaultValue = txtCodHallazgo.Text;
                        SqlDataSource1.UpdateParameters["ComentarioAuditado"].DefaultValue = txtComentario.Text;
                        SqlDataSource1.UpdateParameters["IdNivelRiesgo"].DefaultValue = ddlNivelRiesgo.SelectedValue;
                        SqlDataSource1.Update();
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaTabGestion.Visible = true;
                        filaAcciones.Visible = true;
                        filaDetalleHallazgo.Visible = false;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void btnImgEliminarHallazgo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblAccion.Text = "Hallazgo";
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }
        #endregion

        protected void btnImgInsertarRec_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource22.InsertParameters["IdHallazgo"].DefaultValue = txtCodHallazgoGen.Text;
                    SqlDataSource22.InsertParameters["Tipo"].DefaultValue = ddlRecPoD.SelectedValue;

                    if (ddlRecPoD.SelectedValue == "Dependencia")
                        SqlDataSource22.InsertParameters["IdDependenciaAuditada"].DefaultValue = lblIdDependenciaRec2.Text;
                    else
                        SqlDataSource22.InsertParameters["IdSubproceso"].DefaultValue = lblIdProcesoRec.Text;

                    SqlDataSource22.InsertParameters["IdDependenciaRespuesta"].DefaultValue = lblIdDependenciaRec1.Text;
                    SqlDataSource22.InsertParameters["Estado"].DefaultValue = "Formulado";
                    SqlDataSource22.InsertParameters["Observacion"].DefaultValue = txtRecomendacion.Text;
                    SqlDataSource22.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource22.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD

                    SqlDataSource22.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaTabGestion.Visible = true;
                    filaAcciones.Visible = true;
                    filaDetalleRecomendacion.Visible = false;
                }
                catch (Exception except)
                {
                    //Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgActualizarRec_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (VerificarCampos())
                {
                    try
                    {
                        SqlDataSource22.UpdateParameters["IdRecomendacion"].DefaultValue = txtCodRec.Text;
                        SqlDataSource22.UpdateParameters["Tipo"].DefaultValue = ddlRecPoD.SelectedValue;
                        if (ddlRecPoD.SelectedValue == "Dependencia")
                        {
                            SqlDataSource22.UpdateParameters["IdDependenciaAuditada"].DefaultValue = lblIdDependenciaRec2.Text;
                            SqlDataSource22.UpdateParameters["IdSubproceso"].DefaultValue = null;
                        }
                        else
                        {
                            SqlDataSource22.UpdateParameters["IdSubproceso"].DefaultValue = lblIdProcesoRec.Text;
                            SqlDataSource22.UpdateParameters["IdDependenciaAuditada"].DefaultValue = null;
                        }
                        SqlDataSource22.UpdateParameters["IdDependenciaRespuesta"].DefaultValue = lblIdDependenciaRec1.Text;
                        SqlDataSource22.UpdateParameters["Observacion"].DefaultValue = txtRecomendacion.Text;

                        SqlDataSource22.Update();

                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaTabGestion.Visible = true;
                        filaAcciones.Visible = true;
                        filaDetalleRecomendacion.Visible = false;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgInsertarRie_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                try
                {
                    SqlDataSource23.InsertParameters["IdHallazgo"].DefaultValue = txtCodHallazgoGen.Text;
                    SqlDataSource23.InsertParameters["Nombre"].DefaultValue = txtNomRiesgo.Text;
                    SqlDataSource23.InsertParameters["IdDetalleTipoRiesgo"].DefaultValue = ddlTipoRiesgo.SelectedValue;
                    SqlDataSource23.InsertParameters["Tipo"].DefaultValue = ddlRiePoD.SelectedValue;

                    if (ddlRiePoD.SelectedValue == "Dependencia")
                    {
                        SqlDataSource23.InsertParameters["IdDependencia"].DefaultValue = lblIdDependenciaRie.Text;
                        SqlDataSource23.InsertParameters["IdSubproceso"].DefaultValue = null;
                    }
                    else
                    {
                        SqlDataSource23.InsertParameters["IdDependencia"].DefaultValue = null;
                        SqlDataSource23.InsertParameters["IdSubproceso"].DefaultValue = lblIdProcesoRie.Text;
                    }

                    SqlDataSource23.InsertParameters["Estado"].DefaultValue = "Formulado";
                    SqlDataSource23.InsertParameters["Observacion"].DefaultValue = txtDescRiesgo.Text;
                    SqlDataSource23.InsertParameters["IdDetalleTipoProbabilidad"].DefaultValue = ddlProbabilidad.SelectedValue;
                    SqlDataSource23.InsertParameters["IdDetalleTipoImpacto"].DefaultValue = ddlImpacto.SelectedValue;
                    SqlDataSource23.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource23.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD

                    //Inserta el maestro del nodo hijo
                    SqlDataSource23.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaTabGestion.Visible = true;
                    filaAcciones.Visible = true;
                    filaDetalleRiesgo.Visible = false;
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgActualizarRie_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (VerificarCampos())
                {
                    try
                    {
                        SqlDataSource23.UpdateParameters["IdRiesgo"].DefaultValue = txtCodRiesgo.Text;
                        SqlDataSource23.UpdateParameters["Nombre"].DefaultValue = txtNomRiesgo.Text;
                        SqlDataSource23.UpdateParameters["IdDetalleTipoRiesgo"].DefaultValue = ddlTipoRiesgo.SelectedValue;
                        SqlDataSource23.UpdateParameters["Tipo"].DefaultValue = ddlRiePoD.SelectedValue;

                        if (ddlRiePoD.SelectedValue == "Dependencia")
                        {
                            SqlDataSource23.UpdateParameters["IdDependencia"].DefaultValue = lblIdDependenciaRie.Text;
                            SqlDataSource23.UpdateParameters["IdSubproceso"].DefaultValue = null;
                        }
                        else
                        {
                            SqlDataSource23.UpdateParameters["IdDependencia"].DefaultValue = null;
                            SqlDataSource23.UpdateParameters["IdSubproceso"].DefaultValue = lblIdProcesoRie.Text;
                        }

                        SqlDataSource23.UpdateParameters["Observacion"].DefaultValue = txtDescRiesgo.Text;
                        SqlDataSource23.UpdateParameters["IdDetalleTipoProbabilidad"].DefaultValue = ddlProbabilidad.SelectedValue;
                        SqlDataSource23.UpdateParameters["IdDetalleTipoImpacto"].DefaultValue = ddlImpacto.SelectedValue;

                        SqlDataSource23.Update();

                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaTabGestion.Visible = true;
                        filaAcciones.Visible = true;
                        filaDetalleRiesgo.Visible = false;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }

            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;
        }

        protected void btnImgCancelarEnc_Click(object sender, ImageClickEventArgs e)
        {
            filaEncabezado.Visible = false;
            filaTabGestion.Visible = true;
            TabContainer1.ActiveTabIndex = 0;
            filaAcciones.Visible = true;
        }

        protected void btnImgActualizarEnc_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                try
                {
                    SqlDataSource18.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                    SqlDataSource18.UpdateParameters["Encabezado"].DefaultValue = txtEncabezado.Text;
                    SqlDataSource18.Update();
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            filaGridTareasP.Visible = false;
            filaTabGestion.Visible = true;
            TabContainer1.ActiveTabIndex = 0;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            filaAcciones.Visible = true;
        }

        protected void btnImgCancelarTareaP_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleTareasP.Visible = false;
            filaGridTareasP.Visible = true;
        }

        protected void btnImgActualizarTP_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (VerificarCampos())
                {
                    //Inserta el maestro del nodo hijo
                    try
                    {
                        SqlDataSource9.UpdateParameters["IdTareasPendientes"].DefaultValue = txtCodigoTP.Text;
                        SqlDataSource9.UpdateParameters["Estado"].DefaultValue = ddlEstado.SelectedValue; //Aca va el id del Usuario de la BD
                        SqlDataSource9.UpdateParameters["IdHijo"].DefaultValue = lblCodNodoGA.Text; //Aca va el id del Usuario de la BD
                        SqlDataSource9.UpdateParameters["Tarea"].DefaultValue = txtTarea.Text;

                        SqlDataSource9.Update();

                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaGridTareasP.Visible = true;
                        filaDetalleTareasP.Visible = false;
                    }
                    catch (Exception except)
                    {
                        // Handle the Exception.
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void btnImgInsertarTP_Click(object sender, ImageClickEventArgs e)
        {
            bool err = false;
            string TextoAdicional = "";
            int nodoJerarquia = 0;

            if (VerificarCampos())
            {
                SqlDataSource9.InsertParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                SqlDataSource9.InsertParameters["Tarea"].DefaultValue = txtTarea.Text;
                SqlDataSource9.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource9.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource9.InsertParameters["Estado"].DefaultValue = ddlEstado.SelectedValue; //Aca va el id del Usuario de la BD
                SqlDataSource9.InsertParameters["IdHijo"].DefaultValue = lblCodNodoGA.Text; //Aca va el id del Usuario de la BD
                SqlDataSource9.InsertParameters["IdObjetivo"].DefaultValue = txtCodObjetivoSel.Text; //Aca va el id del Usuario de la BD

                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource9.Insert();
                    cAuditoria.ActualizarLogHistoricoAudutoria(txtCodAuditoriaSel.Text, "GETDATE()", "NULL", "NULL", "SI");
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaGridTareasP.Visible = true;
                    filaDetalleTareasP.Visible = false;

                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");

                    // Enviar la Notificacion de Creacion de Tarea Pendiente
                    nodoJerarquia = Convert.ToInt32(lblCodNodoGA.Text.Trim());

                    TextoAdicional = "Planeación Código: " + txtCodPlaneacion.Text + ", Nombre: " + txtNomPlaneacion.Text + "<br>";
                    TextoAdicional = TextoAdicional + "Auditoría Código: " + txtCodAuditoriaSel.Text + ", Nombre: " + txtNomAuditoriaSel.Text + "<br>";
                    TextoAdicional = TextoAdicional + "Objetivo Código: " + txtCodObjetivoSel.Text + ", Nombre: " + txtCodObjetivoSel.Text + "<div><br></div>";

                    boolEnviarNotificacion(13, Convert.ToInt32(txtCodObjetivoSel.Text.Trim()), nodoJerarquia, "", TextoAdicional);
                }
            }
        }

        protected void btnImgEliminarTP_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblAccion.Text = "TareasP";
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgActualizarMetodologia_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                try
                {
                    SqlDataSource12.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                    SqlDataSource12.UpdateParameters["Metodologia"].DefaultValue = txtMetodologia.Text;
                    SqlDataSource12.Update();
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgCancelarArchivo_Click(object sender, ImageClickEventArgs e)
        {
            filaGridAnexos.Visible = true;
            filaSubirAnexos.Visible = false;
        }

        protected void btnVolverArchivo_Click(object sender, EventArgs e)
        {
            filaTabGestion.Visible = true;
            filaAcciones.Visible = true;
            filaAnexos.Visible = false;
        }

        protected void btnImgActualizarConclusion_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                try
                {
                    SqlDataSource13.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                    SqlDataSource13.UpdateParameters["Conclusion"].DefaultValue = txtConclusion.Text;

                    SqlDataSource13.Update();
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgActualizarObs_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                try
                {
                    SqlDataSource14.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text.Trim();
                    SqlDataSource14.UpdateParameters["Observaciones"].DefaultValue = tbxObs.Text.Trim();
                    SqlDataSource14.Update();
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        #region Botones Acciones abajo

        protected void btnDevolverAuditor_Click(object sender, EventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblAccion.Text = "DevolverAuditor";
                lblMsgBox.Text = "Desea Devolver a Auditor la Auditoria?";
                mpeMsgBox.Show();
            }
        }

        protected void btnApruebaAud_Click(object sender, EventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                lblAccion.Text = "APROBAR";
                lblMsgBox.Text = "Desea aprobar el informe de auditoría?";
                mpeMsgBox.Show();
            }
        }

        protected void btnAnulaAud_Click(object sender, EventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                TbAnulaAuditoria.Visible = true;
                TextBox2.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                TextBox3.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                TextBox1.Focus();
            }
        }

        protected void btnEncabezado_Click(object sender, EventArgs e)
        {
            filaTabGestion.Visible = false;
            filaAcciones.Visible = false;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            filaEncabezado.Visible = true;

            txtEncabezado.Focus();
        }

        protected void btnTareasPendientes_Click(object sender, EventArgs e)
        {
            filaGridTareasP.Visible = true;
            filaTabGestion.Visible = false;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            filaAcciones.Visible = false;
        }
        #endregion

        protected void imgBtnRiesgo_Click(object sender, EventArgs e)
        {
            TabContainer1.Tabs[1].Enabled = true;
            TabContainer1.Tabs[2].Enabled = true;
            TabContainer1.ActiveTabIndex = 2;
        }

        protected void imgBtnRecomendacion_Click(object sender, EventArgs e)
        {
            TabContainer1.Tabs[1].Enabled = true;
            TabContainer1.Tabs[2].Enabled = true;
            TabContainer1.ActiveTabIndex = 1;
        }

        protected void imgBtnInsertarHallazgo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                txtCodHallazgo.Text = "";
                ddlTipoHallazgo.SelectedValue = null;
                ddlTipoHallazgo.Focus();
                txtHallazgo.Text = "";
                txtComentario.Text = "";
                ddlEstadoHallazgo.SelectedValue = null;
                txtUsuarioHallazgo.Text = "";
                txtFecCreacionHallazgo.Text = "";
                filaTabGestion.Visible = false;
                filaAcciones.Visible = false;
                filaDetalleHallazgo.Visible = true;
                txtUsuarioHallazgo.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionHallazgo.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgActualizarHallazgo.Visible = false;
                btnImgInsertarHallazgo.Visible = true;
            }
        }

        protected void imgBtnInsertarRec_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                ddlRecPoD.SelectedValue = "Procesos";
                ddlRecPoD.Focus();
                filaProcesoRec.Visible = true;
                filaDependenciaRec.Visible = false;
                txtCodRec.Text = "";
                txtDependenciaRec1.Text = "";
                txtDependenciaRec2.Text = "";
                lblCorreoDepedenciaRec2.Text = "";
                txtProcesoRec.Text = "";
                txtRecomendacion.Text = "";
                txtUsuarioRec.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                ddlMacroProceso.SelectedValue = null;
                ddlProceso.SelectedValue = null;
                ddlMacroProcesoRie.SelectedValue = null;
                ddlProcesoRie.SelectedValue = null;
                txtFecCreacionRec.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgActualizarRec.Visible = false;
                btnImgInsertarRec.Visible = true;
                filaTabGestion.Visible = false;
                filaDetalleRecomendacion.Visible = true;
                filaAcciones.Visible = false;
            }
        }

        protected void imgBtnInsertarRie_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                txtNomRiesgo.Text = "";
                ddlTipoRiesgo.Focus();
                ddlTipoRiesgo.SelectedValue = null;
                ddlRiePoD.SelectedValue = "Procesos";
                filaProcesoRie.Visible = true;
                filaDependenciaRie.Visible = false;
                txtCodRiesgo.Text = "";
                txtDependenciaRie.Text = "";
                txtProcesoRie.Text = "";
                txtDescRiesgo.Text = "";
                lblCorreoDependenciaRie.Text = "";
                txtUsuarioRie.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                ddlProbabilidad.SelectedValue = null;
                ddlImpacto.SelectedValue = null;
                txtFecCreacionRie.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgActualizarRie.Visible = false;
                btnImgInsertarRie.Visible = true;
                filaTabGestion.Visible = false;
                filaAcciones.Visible = false;
                filaDetalleRiesgo.Visible = true;
            }
        }

        protected void imgBtnInsertarTP_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                filaGridTareasP.Visible = false;
                filaDetalleTareasP.Visible = true;
                ddlEstado.SelectedValue = "0";
                lblCodNodoGA.Text = "";
                txtRecurso.Text = "";
                txtCodigoTP.Text = "";
                txtTarea.Text = "";
                txtTarea.Focus();
                txtUsuarioTP.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionTP.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgInsertarTP.Visible = true;
                btnImgActualizarTP.Visible = false;
            }
        }

        protected void imgBtnAdjuntar_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if (Path.GetExtension(FileUpload1.FileName).ToLower().ToString().Trim() == ".pdf")
                {
                    mtdCargarPdfVerificacion();
                    GridView100.DataBind();
                    omb.ShowMessage("Archivo cargado exitósamente.", 2, "Atención");
                }
                else
                    omb.ShowMessage("Solamente se permiten cargar archivos PDF!", 2, "Atención");
            }
            else
            {//omb.ShowMessage("¡Debe seleccionar un archivo PDF!", 2, "Atención");
                lblMsgBox.Text = "No hay archivos para cargar.";
                mpeMsgBox.Show();
            }
        }

        protected void imgBtnInsertarArchivo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                filaGridAnexos.Visible = false;
                filaSubirAnexos.Visible = true;
                FileUpload1.Focus();
                txtDescArchivo.Text = "";
            }
        }

        protected void ImageButton14_Click(object sender, ImageClickEventArgs e)
        {
            lblAccion.Text = "ANULAR";
            lblMsgBox.Text = "Desea anular el informe de auditoría?";
            mpeMsgBox.Show();
        }

        protected void ImageButton15_Click(object sender, ImageClickEventArgs e)
        {
            TbAnulaAuditoria.Visible = false;
            TextBox1.Text = "";
        }
        #endregion

        #region GridView

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                filaTabGestion.Visible = false;
                filaAcciones.Visible = false;
                filaDetalleHallazgo.Visible = true;
                btnImgActualizarHallazgo.Visible = true;
                btnImgInsertarHallazgo.Visible = false;
            }
            else if (e.CommandArgument.ToString() == "Anexar")
            {
                filaAnexos.Visible = true;
                filaGridAnexos.Visible = true;
                filaSubirAnexos.Visible = false;
                filaAcciones.Visible = false;
                filaTabGestion.Visible = false;
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                filaGridTareasP.Visible = false;
                filaDetalleTareasP.Visible = true;
                btnImgInsertarTP.Visible = false;
                btnImgActualizarTP.Visible = true;
            }
        }

        protected void GridView11_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                filaTabGestion.Visible = false;
                filaAcciones.Visible = false;
                filaDetalleRecomendacion.Visible = true;
                btnImgInsertarRec.Visible = false;
                btnImgActualizarRec.Visible = true;
            }
        }

        protected void GridView13_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                filaTabGestion.Visible = false;
                filaAcciones.Visible = false;
                filaDetalleRiesgo.Visible = true;
                btnImgInsertarRie.Visible = false;
                btnImgActualizarRie.Visible = true;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodHallazgo.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                lblIdHallazgo.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                ddlTipoHallazgo.SelectedValue = GridView1.SelectedDataKey[1].ToString().Trim();
                ddlTipoHallazgo.Focus();
                txtHallazgo.Text = GridView1.SelectedRow.Cells[5].Text.Trim();
                txtComentario.Text = GridView1.SelectedDataKey[3].ToString().Trim();
                ddlEstadoHallazgo.SelectedValue = GridView1.SelectedDataKey[2].ToString().Trim();
                txtUsuarioHallazgo.Text = GridView1.SelectedDataKey[5].ToString().Trim();
                txtFecCreacionHallazgo.Text = GridView1.SelectedRow.Cells[7].Text.Trim();
                txtHallazgoGen.Text = GridView1.SelectedRow.Cells[5].Text.Trim();
                txtCodHallazgoGen.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                txtHallazgoRec.Text = txtHallazgoGen.Text;
                txtHallazgoRie.Text = txtHallazgoGen.Text;
                ddlNivelRiesgo.SelectedValue = GridView1.SelectedDataKey[6].ToString().Trim();

                TabContainer1.Tabs[1].Enabled = true;
                TabContainer1.Tabs[2].Enabled = true;
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodigoTP.Text = GridView2.SelectedRow.Cells[0].Text.Trim();
            txtTarea.Text = GridView2.SelectedRow.Cells[2].Text.Trim();
            ddlEstado.SelectedValue = GridView2.SelectedRow.Cells[3].Text.Trim();
            lblCodNodoGA.Text = GridView2.SelectedDataKey[3].ToString().Trim();
            txtRecurso.Text = GridView2.SelectedDataKey[4].ToString().Trim();
            txtTarea.Focus();
            txtUsuarioTP.Text = GridView2.SelectedDataKey[2].ToString().Trim(); ; //Aca va el codigo de usuario logueado
            txtFecCreacionTP.Text = GridView2.SelectedRow.Cells[4].Text.Trim(); ;
        }

        protected void GridView6_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodAuditoriaSel.Text = GridView6.SelectedRow.Cells[0].Text.Trim();
            txtNomAuditoriaSel.Text = GridView6.SelectedRow.Cells[1].Text.Trim();
            txtEncabezado.Text = GridView6.SelectedDataKey[1].ToString().Trim();
            txtMetodologiaGen.Text = GridView6.SelectedDataKey[2].ToString().Trim();
            txtConclusionGen.Text = GridView6.SelectedDataKey[3].ToString().Trim();
            tbxObsGen.Text = GridView6.SelectedDataKey[4].ToString().Trim();

            txtCodObjetivoSel.Text = "";
            txtCodEnfoqueSel.Text = "";
            txtCodLiteralSel.Text = "";
            txtNomObjetivoSel.Text = "";
            txtNomEnfoqueSel.Text = "";
            txtNomLiteralSel.Text = "";
            txtNomEnfoqueSel.Height = 18;
            txtNomEnfoqueSel.Width = 402;
            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;
            if (filaTabGestion.Visible == true) filaTabGestion.Visible = false;
            filaAcciones.Visible = false;
            filaEncabezado.Visible = false;
            filaGridTareasP.Visible = false;
            filaDetalleTareasP.Visible = false;

            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            TabContainer1.ActiveTabIndex = 0;
            TabContainer1.Tabs[1].Enabled = false;
            TabContainer1.Tabs[2].Enabled = false;
            filaAcciones.Visible = true;
            btnTareasPEndientes.Visible = false;

            popupAuditoria.Cancel();
        }

        protected void GridView7_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodObjetivoSel.Text = GridView7.SelectedRow.Cells[0].Text.Trim();
            txtNomObjetivoSel.Text = GridView7.SelectedRow.Cells[1].Text.Trim();
            txtCodObjetivo.Text = GridView7.SelectedRow.Cells[0].Text.Trim();
            txtCodEnfoqueSel.Text = "";
            txtCodLiteralSel.Text = "";
            txtNomEnfoqueSel.Text = "";
            txtNomLiteralSel.Text = "";
            txtNomEnfoqueSel.Height = 18;
            txtNomEnfoqueSel.Width = 402;
            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;
            if (filaTabGestion.Visible == true) filaTabGestion.Visible = false;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            //filaAcciones.Visible = false;
            filaEncabezado.Visible = false;
            filaGridTareasP.Visible = false;
            filaDetalleTareasP.Visible = false;
            TabContainer1.ActiveTabIndex = 0;
            TabContainer1.Tabs[1].Enabled = false;
            TabContainer1.Tabs[2].Enabled = false;
            btnTareasPEndientes.Visible = true;
            GridView6.DataBind();
            GridView2.DataBind();
            TVGrupoAud.Nodes.Clear();
            PopulateTreeViewGA();
            popupObjetivo.Cancel();
        }

        protected void GridView8_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNomPlaneacion.Text = GridView8.SelectedRow.Cells[1].Text.Trim();
            txtCodPlaneacion.Text = GridView8.SelectedRow.Cells[0].Text.Trim();
            txtEncabezado.Text = "";
            txtCodAuditoriaSel.Text = "";
            txtCodObjetivoSel.Text = "";
            txtCodEnfoqueSel.Text = "";
            txtCodLiteralSel.Text = "";
            txtNomAuditoriaSel.Text = "";
            txtNomObjetivoSel.Text = "";
            txtNomEnfoqueSel.Text = "";
            txtNomLiteralSel.Text = "";
            txtNomEnfoqueSel.Height = 18;
            txtNomEnfoqueSel.Width = 402;
            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;

            if (filaTabGestion.Visible == true) filaTabGestion.Visible = false;
            trAuditorias.Visible = true;
            filaAcciones.Visible = false;
            filaEncabezado.Visible = false;
            filaGridTareasP.Visible = false;
            filaDetalleTareasP.Visible = false;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;

            SqlDSregistrosAud.SelectParameters["IdArea"].DefaultValue = Session["IdAreaUser"].ToString();

            popupPlanea.Cancel();
        }

        protected void GridView9_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodEnfoqueSel.Text = GridView9.SelectedRow.Cells[0].Text.Trim();
            txtNomEnfoqueSel.Text = GridView9.SelectedRow.Cells[1].Text.Trim();
            txtCodLiteralSel.Text = "";
            txtNomLiteralSel.Text = "";
            txtNomEnfoqueSel.Height = 18;
            txtNomEnfoqueSel.Width = 402;
            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;
            if (filaTabGestion.Visible == true) filaTabGestion.Visible = false;
            //filaAcciones.Visible = false;
            filaGridTareasP.Visible = false;
            filaEncabezado.Visible = false;
            filaDetalleTareasP.Visible = false;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            TabContainer1.ActiveTabIndex = 0;
            TabContainer1.Tabs[1].Enabled = false;
            TabContainer1.Tabs[2].Enabled = false;
            GridView6.DataBind();
            popupEnfoque.Cancel();

            int rows = 0, longMax = 0, rowsAdd = 0;
            double temp = 0;

            //Cambia la altura y el ancho del labol de Enfoque
            //Revisa la longitud max del texto y el número de líneas
            foreach (string strItem in Regex.Split(GridView9.SelectedRow.Cells[1].Text, "</div>"))
            {
                rows = rows + 1;
                if (strItem.Length > longMax) longMax = strItem.Length;
                if (strItem.Length > 126)
                {
                    temp = strItem.Length / 126;
                    rowsAdd = rowsAdd + (int)Math.Truncate(temp);
                }
            }

            if (rows + rowsAdd > 1) txtNomEnfoqueSel.Height = (rows + rowsAdd) * 18;

            if (longMax > 72)
                txtNomEnfoqueSel.Width = 700;

            else
                txtNomEnfoqueSel.Width = 402;
        }

        protected void GridView10_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rows = 0, longMax = 0, rowsAdd = 0;
            double temp = 0;

            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;

            //string[] lines = Regex.Split(value, "</div");
            //Revisa la longitud max del texto y el número de líneas
            foreach (string strItem in Regex.Split(GridView10.SelectedRow.Cells[1].Text, "</div>"))
            {
                rows = rows + 1;
                if (strItem.Length > longMax) longMax = strItem.Length;
                if (strItem.Length > 126)
                {
                    temp = strItem.Length / 126;
                    rowsAdd = rowsAdd + (int)Math.Truncate(temp);
                }
            }

            txtNomLiteralSel.Text = GridView10.SelectedRow.Cells[1].Text.Trim();
            txtCodLiteralSel.Text = GridView10.SelectedRow.Cells[0].Text.Trim();

            if (rows + rowsAdd > 1) txtNomLiteralSel.Height = (rows + rowsAdd) * 18;

            if (longMax > 72)
                txtNomLiteralSel.Width = 700;
            else
                txtNomLiteralSel.Width = 402;

            popupLiteral.Cancel();

            filaTabGestion.Visible = true;
            filaAcciones.Visible = true;
            filaEncabezado.Visible = false;
            filaGridTareasP.Visible = false;
            filaDetalleTareasP.Visible = false;
            filaDetalleHallazgo.Visible = false;
            filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;
            TabContainer1.Tabs[1].Enabled = false;
            TabContainer1.Tabs[2].Enabled = false;

            GridView1.DataBind();
            ddlTipoHallazgo.Items.Clear();
            ddlTipoHallazgo.DataBind();

            txtMetodologia.Text = txtMetodologiaGen.Text;
            txtConclusion.Text = txtConclusionGen.Text;
            tbxObs.Text = tbxObsGen.Text;

            GridView6.DataBind();
            TabContainer1.ActiveTabIndex = 0;
        }

        protected void GridView11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView11.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                ddlRecPoD.SelectedValue = GridView11.SelectedRow.Cells[3].Text.Trim();
                ddlRecPoD.Focus();
                if (GridView11.SelectedRow.Cells[3].Text == "Procesos")
                {
                    txtProcesoRec.Text = GridView11.SelectedDataKey[1].ToString().Trim();
                    lblIdProcesoRec.Text = GridView11.SelectedDataKey[4].ToString().Trim();
                    lblIdDependenciaRec2.Text = "";
                    txtDependenciaRec2.Text = "";
                    lblCorreoDepedenciaRec2.Text = "";
                    filaProcesoRec.Visible = true;
                    filaDependenciaRec.Visible = false;
                }
                else
                {
                    txtDependenciaRec2.Text = GridView11.SelectedDataKey[1].ToString().Trim();
                    lblIdDependenciaRec2.Text = GridView11.SelectedDataKey[0].ToString().Trim();
                    lblCorreoDepedenciaRec2.Text = GridView11.SelectedDataKey[8].ToString().Trim();
                    lblIdProcesoRec.Text = "";
                    txtProcesoRec.Text = "";
                    filaProcesoRec.Visible = false;
                    filaDependenciaRec.Visible = true;
                }

                ddlMacroProceso.SelectedValue = null;
                ddlProceso.SelectedValue = null;
                ddlMacroProcesoRie.SelectedValue = null;
                ddlProcesoRie.SelectedValue = null;

                txtCodRec.Text = GridView11.SelectedRow.Cells[0].Text.Trim();
                txtDependenciaRec1.Text = GridView11.SelectedDataKey[3].ToString().Trim();
                lblIdDependenciaRec1.Text = GridView11.SelectedDataKey[2].ToString().Trim();
                txtRecomendacion.Text = GridView11.SelectedRow.Cells[10].Text.Trim();
                txtUsuarioRec.Text = GridView11.SelectedDataKey[7].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionRec.Text = GridView11.SelectedRow.Cells[11].Text.Trim();
            }
        }

        protected void GridView13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView13.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodRiesgo.Text = GridView13.SelectedRow.Cells[0].Text.Trim();
                ddlTipoRiesgo.SelectedValue = GridView13.SelectedDataKey[0].ToString().Trim();
                ddlTipoRiesgo.Focus();
                txtNomRiesgo.Text = GridView13.SelectedRow.Cells[2].Text.Trim();
                ddlRiePoD.SelectedValue = GridView13.SelectedDataKey[1].ToString().Trim();
                if (GridView13.SelectedDataKey[1].ToString() == "Procesos")
                {
                    txtProcesoRie.Text = GridView13.SelectedDataKey[4].ToString().Trim();
                    lblIdProcesoRie.Text = GridView13.SelectedDataKey[3].ToString().Trim();
                    lblIdDependenciaRie.Text = "";
                    txtDependenciaRie.Text = "";
                    lblCorreoDependenciaRie.Text = "";
                    filaProcesoRie.Visible = true;
                    filaDependenciaRie.Visible = false;
                }
                else
                {
                    txtDependenciaRie.Text = GridView13.SelectedDataKey[4].ToString().Trim();
                    lblIdDependenciaRie.Text = GridView13.SelectedDataKey[2].ToString().Trim();
                    lblCorreoDependenciaRie.Text = GridView13.SelectedDataKey[11].ToString().Trim();
                    lblIdProcesoRie.Text = "";
                    txtProcesoRie.Text = "";
                    filaProcesoRie.Visible = false;
                    filaDependenciaRie.Visible = true;
                }

                ddlMacroProcesoRie.SelectedValue = null;
                ddlProcesoRie.SelectedValue = null;

                txtDescRiesgo.Text = GridView13.SelectedDataKey[6].ToString().Trim();
                ddlProbabilidad.SelectedValue = GridView13.SelectedDataKey[7].ToString().Trim();
                ddlImpacto.SelectedValue = GridView13.SelectedDataKey[8].ToString().Trim();
                txtUsuarioRie.Text = GridView13.SelectedDataKey[10].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionRie.Text = GridView13.SelectedRow.Cells[13].Text;
            }
        }

        protected void GridView100_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nameFile;
            nameFile = GridView100.SelectedRow.Cells[1].Text.Trim();
            //descargarArchivo(nameFile);
            mtdDescargarPdfVerificacion(nameFile);
        }

        #endregion

        private string DetalleNodo(int tipoSelect, string idHijo)
        {
            string Detalle = "";
            string selectCommand = "";

            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            if (tipoSelect == 1)
                selectCommand = "SELECT NombreResponsable,CorreoResponsable FROM [Parametrizacion].[DetalleJerarquiaOrg] WHERE idHijo = " + idHijo;
            else
                selectCommand = "SELECT NombreResponsable,CorreoResponsable, NombreHijo FROM [Parametrizacion].[JerarquiaOrganizacional] LEFT OUTER JOIN [Parametrizacion].[DetalleJerarquiaOrg] ON [DetalleJerarquiaOrg].idHijo = [JerarquiaOrganizacional].idHijo WHERE [JerarquiaOrganizacional].idHijo = " + idHijo;

            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);

            DataView view = new DataView(dtblDiscuss);

            foreach (DataRowView row in view)
            {
                Detalle = "Responsable: " + row["NombreResponsable"].ToString() + "\r";
                Detalle = Detalle + "Correo: " + row["CorreoResponsable"].ToString().Trim();

                if (tipoSelect == 2)
                    Detalle = Detalle + "\r Nodo Jerarquía Org.: " + row["NombreHijo"].ToString().Trim();
            }

            if (Detalle == "")
                Detalle = "Responsable: \rCorreo:";

            return (Detalle);
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 3)
                filaMetodologia.Visible = true;
            else
                filaMetodologia.Visible = false;

            if (TabContainer1.ActiveTabIndex == 4)
                filaConclusion.Visible = true;
            else
                filaConclusion.Visible = false;

            if (TabContainer1.ActiveTabIndex == 5)
                filaObs.Visible = true;
            else
                filaObs.Visible = false;
        }

        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        protected Boolean VerificarCampos()
        {
            bool err = true;

            if (filaDetalleHallazgo.Visible == true)
            {
                if (ddlTipoHallazgo.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Tipo de Hallazgo.", 2, "Atención");
                    ddlTipoHallazgo.Focus();
                }
                else if (ValidarCadenaVacia(txtHallazgo.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Hallazgo.", 2, "Atención");
                    txtHallazgo.Focus();
                }
                else if (ddlEstadoHallazgo.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Estado del Hallazgo.", 2, "Atención");
                    ddlEstadoHallazgo.Focus();
                }
            }
            else if (filaDetalleRecomendacion.Visible == true)
            {
                if (ddlRecPoD.SelectedValue == "Procesos" && ValidarCadenaVacia(txtProcesoRec.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar un Proceso.", 2, "Atención");
                    txtProcesoRec.Focus();
                }
                else if (ddlRecPoD.SelectedValue == "Dependencia" && ValidarCadenaVacia(txtDependenciaRec2.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar una Dependencia.", 2, "Atención");
                    txtDependenciaRec2.Focus();
                }
                else if (ddlRecPoD.SelectedValue == "Dependencia" && ValidarCadenaVacia(lblCorreoDepedenciaRec2.Text.Trim()))
                {
                    err = false;
                    omb.ShowMessage("El nodo seleccionado de la Jerarquía Organizacional no tiene un correo asociado. \r Actualice la informacion en el Módulo de Parametrización o seleccione otro nodo de la Jerarquía Organizacional.", 2, "Atención");
                    txtDependenciaRec2.Focus();
                }
                else if (ValidarCadenaVacia(txtDependenciaRec1.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar una Dependencia de Respuesta.", 2, "Atención");
                    txtDependenciaRec1.Focus();
                }
                else if (ValidarCadenaVacia(txtRecomendacion.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Recomendacion.", 2, "Atención");
                    txtRecomendacion.Focus();
                }
            }
            else if (filaDetalleRiesgo.Visible == true)
            {
                if (ddlTipoRiesgo.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Tipo de Riesgo.", 2, "Atención");
                    ddlTipoRiesgo.Focus();
                }
                else if (ValidarCadenaVacia(txtNomRiesgo.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Nombre del Riesgo.", 2, "Atención");
                    txtNomRiesgo.Focus();
                }
                else if (ValidarCadenaVacia(txtDescRiesgo.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Descripcion del Riesgo.", 2, "Atención");
                    txtDescRiesgo.Focus();
                }
                else if (ddlRiePoD.SelectedValue == "Procesos" && ValidarCadenaVacia(txtProcesoRie.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar un Proceso.", 2, "Atención");
                    txtProcesoRie.Focus();
                }
                else if (ddlRiePoD.SelectedValue == "Dependencia" && ValidarCadenaVacia(txtDependenciaRie.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar una Dependencia.", 2, "Atención");
                    txtDependenciaRie.Focus();
                }
                else if (ddlRiePoD.SelectedValue == "Dependencia" && ValidarCadenaVacia(lblCorreoDependenciaRie.Text.Trim()))
                {
                    err = false;
                    omb.ShowMessage("El nodo seleccionado de la Jerarquía Organizacional no tiene un correo asociado. \r Actualice la informacion en el Módulo de Parametrización o seleccione otro nodo de la Jerarquía Organizacional.", 2, "Atención");
                    txtDependenciaRie.Focus();
                }
                else if (ddlProbabilidad.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar la Probabilidad.", 2, "Atención");
                    ddlProbabilidad.Focus();
                }
                else if (ddlImpacto.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Impacto.", 2, "Atención");
                    ddlImpacto.Focus();
                }
            }

            else if (filaDetalleTareasP.Visible == true)
            {
                if (ddlEstado.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Estado.", 2, "Atención");
                    ddlEstado.Focus();
                }
                else if (ValidarCadenaVacia(txtRecurso.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Recurso.", 2, "Atención");
                    txtRecurso.Focus();
                }
                else if (ValidarCadenaVacia(txtTarea.Text))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la tarea.", 2, "Atención");
                    txtTarea.Focus();
                }
            }

            return err;
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string customerId = GridView4.DataKeys[e.Row.RowIndex].Value.ToString();
                //int IdEvento = Convert.ToInt32(Session["IdEvento"].ToString());
                //int IdRiesgo = Convert.ToInt32(Session["IdRiesgo"].ToString());
                GridView gvOrders = e.Row.FindControl("gvOrders") as GridView;
                string strCondicion = "AND ([Auditoria].[Auditoria].[Estado] = 'PRE INFORME')";
                string strConsulta = string.Format("SELECT [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo],ISNULL([IdDependencia],0) AS IdDependencia, ISNULL([IdProceso], 0) as IdProceso, [NombreHijo] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro],120) AS FechaRegistro, [Auditoria].[IdEmpresa],  CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo , CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia],0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza],0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10),[FechaInicio],120) AS FechaInicio,  CONVERT(VARCHAR(10),[FechaCierre],120) AS FechaCierre,ISNULL([IdMesEjecucion],0) as IdMesEjecucion,[SemanaEjecucion] " +
         " [Encabezado], [Metodologia], [Conclusion], [Observaciones], [TituloInforme],[ReferenciaInforme],[Auditoria].[Auditoria].[IdAuditoria] " +
                    "FROM [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Parametrizacion].[JerarquiaOrganizacional] " +
         "WHERE[Auditoria].IdEstandar = [Estandar].IdEstandar AND " +
                "[Auditoria].IdUsuario = [Usuarios].IdUsuario AND " +
                "[Auditoria].IdDependencia = [JerarquiaOrganizacional].IdHijo AND " +
                "[Auditoria].IdPlaneacion = {0} and IdDependencia <> 0 and IdProceso = 0  and [Auditoria].[IdArea] = {2} {1}" +
         " UNION " +
         "SELECT [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo], ISNULL([IdDependencia],0) AS IdDependencia, ISNULL([Auditoria].[IdProceso], 0) as IdProceso, [Proceso].[Nombre] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro], 120) AS FechaRegistro, [Auditoria].[IdEmpresa], CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo, CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia], 0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza], 0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10), [FechaInicio], 120) AS FechaInicio, CONVERT(VARCHAR(10),[FechaCierre], 120) AS FechaCierre,ISNULL([IdMesEjecucion],0) as IdMesEjecucion,[SemanaEjecucion] " +
         " [Encabezado], [Metodologia], [Conclusion], [Observaciones], [TituloInforme],[ReferenciaInforme],[Auditoria].[Auditoria].[IdAuditoria] " +
         "FROM [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Procesos].[Proceso], [Parametrizacion].[JerarquiaOrganizacional], Auditoria.AuditoriaProceso " +
"WHERE [Auditoria].IdEstandar = [Estandar].IdEstandar AND " +
"[Auditoria].IdUsuario = [Usuarios].IdUsuario AND " +
"[Auditoria].IdProceso = [Proceso].IdProceso AND " +
"[Auditoria].[IdAuditoria] = AuditoriaProceso.IdAuditoria and " +
"[Auditoria].IdPlaneacion = {0} " +
"and AuditoriaProceso.IdTipoProceso = 2 and [Auditoria].[IdArea] = {2} {1}" +
         " UNION " +
         "SELECT [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo], ISNULL([IdDependencia],0) AS IdDependencia, ISNULL([Auditoria].[IdProceso], 0) as IdProceso, [MacroProceso].[Nombre] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro],120) AS FechaRegistro, [Auditoria].[IdEmpresa], CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo, CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia], 0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza], 0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10), [FechaInicio], 120) AS FechaInicio, CONVERT(VARCHAR(10),[FechaCierre], 120) AS FechaCierre,ISNULL([IdMesEjecucion],0) as IdMesEjecucion,[SemanaEjecucion] " +
         " [Encabezado], [Metodologia], [Conclusion], [Observaciones], [TituloInforme],[ReferenciaInforme],[Auditoria].[Auditoria].[IdAuditoria] " +
         "FROM [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Procesos].[MacroProceso], [Parametrizacion].[JerarquiaOrganizacional], Auditoria.AuditoriaProceso " +
"WHERE [Auditoria].IdEstandar = [Estandar].IdEstandar AND" +
"[Auditoria].IdUsuario = [Usuarios].IdUsuario AND " +
"[Auditoria].IdProceso = [MacroProceso].IdMacroProceso AND " +
"[Auditoria].[IdAuditoria] = AuditoriaProceso.IdAuditoria and" +
"[Auditoria].IdPlaneacion = {0}" +
" and AuditoriaProceso.IdTipoProceso = 1 and [Auditoria].[IdArea] = {2} {1}", customerId, strCondicion, Session["IdAreaUser"].ToString());
                gvOrders.DataSource = GetData(strConsulta);

                gvOrders.DataBind();
            }
        }
        private static DataTable GetData(string query)
        {
            string strConnString = ConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (System.Data.DataSet ds = new System.Data.DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

            int index = gvr.RowIndex;
            //int index = row.RowIndex;
            int IdPlaneacion = Convert.ToInt32(e.CommandArgument);
            /*InfoGridAuditorias = GetData(string.Format("SELECT [IdAuditoria], [Tema], [Encabezado], [Metodologia], [Conclusion], [Observaciones] " +
                " FROM [Auditoria].[Auditoria] " +
                " WHERE ([IdPlaneacion] = {0} AND ([Estado] = 'PRE INFORME'))", IdPlaneacion));*/
            string strCondicion = "AND ([Auditoria].[Auditoria].[Estado] = 'PRE INFORME')";
            string strConsulta = string.Format("SELECT [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo],ISNULL([IdDependencia],0) AS IdDependencia, ISNULL([IdProceso], 0) as IdProceso, [NombreHijo] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro],120) AS FechaRegistro, [Auditoria].[IdEmpresa],  CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo , CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia],0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza],0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10),[FechaInicio],120) AS FechaInicio,  CONVERT(VARCHAR(10),[FechaCierre],120) AS FechaCierre,ISNULL([IdMesEjecucion],0) as IdMesEjecucion,[SemanaEjecucion] " +
     " [Encabezado], [Metodologia], [Conclusion], [Observaciones], [TituloInforme],[ReferenciaInforme],[Auditoria].[Auditoria].[IdAuditoria] " +
                "FROM [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Parametrizacion].[JerarquiaOrganizacional] " +
     "WHERE[Auditoria].IdEstandar = [Estandar].IdEstandar AND " +
            "[Auditoria].IdUsuario = [Usuarios].IdUsuario AND " +
            "[Auditoria].IdDependencia = [JerarquiaOrganizacional].IdHijo AND " +
            "[Auditoria].IdPlaneacion = {0} and IdDependencia <> 0 and IdProceso = 0 {1}" +
     " UNION " +
     "SELECT [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo], ISNULL([IdDependencia],0) AS IdDependencia, ISNULL([Auditoria].[IdProceso], 0) as IdProceso, [Proceso].[Nombre] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro], 120) AS FechaRegistro, [Auditoria].[IdEmpresa], CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo, CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia], 0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza], 0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10), [FechaInicio], 120) AS FechaInicio, CONVERT(VARCHAR(10),[FechaCierre], 120) AS FechaCierre,ISNULL([IdMesEjecucion],0) as IdMesEjecucion,[SemanaEjecucion] " +
     " [Encabezado], [Metodologia], [Conclusion], [Observaciones], [TituloInforme],[ReferenciaInforme],[Auditoria].[Auditoria].[IdAuditoria] " +
     "FROM [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Procesos].[Proceso], [Parametrizacion].[JerarquiaOrganizacional], Auditoria.AuditoriaProceso " +
"WHERE [Auditoria].IdEstandar = [Estandar].IdEstandar AND " +
"[Auditoria].IdUsuario = [Usuarios].IdUsuario AND " +
"[Auditoria].IdProceso = [Proceso].IdProceso AND " +
"[Auditoria].[IdAuditoria] = AuditoriaProceso.IdAuditoria and " +
"[Auditoria].IdPlaneacion = {0} " +
"and AuditoriaProceso.IdTipoProceso = 2 {1}" +
     " UNION " +
     "SELECT [Tema], [Estandar].[IdEstandar], [Estandar].[Nombre], [IdPlaneacion], [Tipo], ISNULL([IdDependencia],0) AS IdDependencia, ISNULL([Auditoria].[IdProceso], 0) as IdProceso, [MacroProceso].[Nombre] as NombreDP, [Auditoria].[IdUsuario], [Usuarios].[Usuario], CONVERT(VARCHAR(10),[Auditoria].[FechaRegistro],120) AS FechaRegistro, [Auditoria].[IdEmpresa], CONVERT(VARCHAR(MAX), [Recursos]) AS Recursos, CONVERT(VARCHAR(MAX), [Auditoria].[Objetivo]) AS Objetivo, CONVERT(VARCHAR(MAX), [Alcance]) AS Alcance, IsNull([NivelImportancia], 0) AS NivelImportancia, IsNull([IdDetalleTipo_TipoNaturaleza], 0) AS IdDetalleTipo_TipoNaturaleza, CONVERT(VARCHAR(10), [FechaInicio], 120) AS FechaInicio, CONVERT(VARCHAR(10),[FechaCierre], 120) AS FechaCierre,ISNULL([IdMesEjecucion],0) as IdMesEjecucion,[SemanaEjecucion] " +
     " [Encabezado], [Metodologia], [Conclusion], [Observaciones], [TituloInforme],[ReferenciaInforme],[Auditoria].[Auditoria].[IdAuditoria] " +
     "FROM [Auditoria].[Auditoria], [Auditoria].[Estandar], [Listas].[Usuarios], [Procesos].[MacroProceso], [Parametrizacion].[JerarquiaOrganizacional], Auditoria.AuditoriaProceso " +
"WHERE [Auditoria].IdEstandar = [Estandar].IdEstandar AND" +
"[Auditoria].IdUsuario = [Usuarios].IdUsuario AND " +
"[Auditoria].IdProceso = [MacroProceso].IdMacroProceso AND " +
"[Auditoria].[IdAuditoria] = AuditoriaProceso.IdAuditoria and" +
"[Auditoria].IdPlaneacion = {0}" +
" and AuditoriaProceso.IdTipoProceso = 1 {1}", IdPlaneacion, strCondicion);
            InfoGridAuditorias = GetData(strConsulta);
            txtCodAuditoriaSel.Text = InfoGridAuditorias.Rows[index]["IdAuditoria"].ToString().Trim();
            txtNomAuditoriaSel.Text = InfoGridAuditorias.Rows[index]["Tema"].ToString().Trim();
            //txtMetodologiaGen.Text = InfoGridAuditorias.Rows[index]["Metodologia"].ToString().Trim();
            /*txtConclusionGen.Text = gvOrders.SelectedDataKey[1].ToString().Trim();
            tbxObsGen.Text = gvOrders.SelectedDataKey[2].ToString().Trim();*/
            //txtEncabezado.Text = InfoGridAuditorias.Rows[index]["Encabezado"].ToString().Trim();

            /*if (filaGridRec.Visible == true)
                filaGridRec.Visible = false;*/

            //filaCierreRec.Visible = true;
            //filaLogEstados.Visible = false;
            filaDetalleRecomendacion.Visible = false;
            txtCodObjetivoSel.Text = "";
            txtNomObjetivoSel.Text = "";
            txtCodEnfoqueSel.Text = "";
            txtCodLiteralSel.Text = "";
            txtNomEnfoqueSel.Text = "";
            txtNomLiteralSel.Text = "";
            //txtNomHallazgoSel.Text = "";
            //txtCodHallazgoSel.Text = "";
            txtNomEnfoqueSel.Height = 18;
            txtNomEnfoqueSel.Width = 402;
            txtNomLiteralSel.Height = 18;
            txtNomLiteralSel.Width = 402;
            //txtNomHallazgoSel.Height = 18;
            //txtNomHallazgoSel.Width = 402;

            /*filaMetodologia.Visible = false;
            filaConclusion.Visible = false;
            filaObs.Visible = false;*/
            popupAuditoria.Cancel();
            /*TabContainer1.ActiveTabIndex = 0;
            TabContainer1.Tabs[1].Enabled = false;
            TabContainer1.Tabs[2].Enabled = false;
            filaGridTareasP.Visible = false;
            filaDetalleTareasP.Visible = false;
            filaAcciones.Visible = true;
            btnTareasPEndientes.Visible = false;*/
            trAuditorias.Visible = false;

            //if (filaTabGestion.Visible == true) filaTabGestion.Visible = false;
        }

        protected Boolean ValidarCadenaVacia(string cadena)
        {
            //Regex rx = new Regex(@"^-?\d+(\.\d{2})?$");
            string Espacio = "<br>";
            string Div = "<div>";
            string Div2 = "</div>";
            string b = "<b>";
            string b2 = "</b>";
            string cadena2 = "";
            cadena2 = Regex.Replace(cadena, Espacio, " ");
            cadena2 = Regex.Replace(cadena2, Div, " ");
            cadena2 = Regex.Replace(cadena2, Div2, " ");
            cadena2 = Regex.Replace(cadena2, b, " ");
            cadena2 = Regex.Replace(cadena2, b2, " ");
            if (cadena2.Trim() == "")
                return (true);
            else
                return (false);
        }

        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            #region Variables
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion Variables

            #region Registro Correos
            try
            {
                #region informacion basica necesario para enviar el correo
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
                    //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                    selectCommand = "SELECT CD.Copia,CD.Otros,CD.Asunto,CD.Cuerpo,CD.NroDiasRecordatorio,CD.AJefeInmediato,CD.AJefeMediato,E.RequiereFechaCierre " +
                        "FROM [Notificaciones].[CorreosDestinatarios] AS CD INNER JOIN [Notificaciones].[Evento] AS E ON CD.IdEvento = E.IdEvento " +
                        "WHERE E. IdEvento = " + idEvento;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Copia = row["Copia"].ToString().Trim();
                        Otros = row["Otros"].ToString().Trim();
                        Asunto = row["Asunto"].ToString().Trim();
                        Cuerpo = textoAdicional + row["Cuerpo"].ToString().Trim();
                        NroDiasRecordatorio = row["NroDiasRecordatorio"].ToString().Trim();
                        AJefeInmediato = row["AJefeInmediato"].ToString().Trim();
                        AJefeMediato = row["AJefeMediato"].ToString().Trim();
                        RequiereFechaCierre = row["RequiereFechaCierre"].ToString().Trim();
                    }
                }
                #endregion informacion basica necesario para enviar el correo

                #region Destinatario
                if (!string.IsNullOrEmpty(idNodoJerarquia.ToString().Trim()))
                {
                    //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                    selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                        "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                        "WHERE JO.idHijo = " + idNodoJerarquia;

                    dad = new SqlDataAdapter(selectCommand, conString);
                    dtblDiscuss.Clear();
                    dad.Fill(dtblDiscuss);
                    view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        Destinatario = row["CorreoResponsable"].ToString().Trim();
                        idJefeInmediato = row["idPadre"].ToString().Trim();
                    }
                }
                #endregion Destinatario

                #region Jefe Inmediato
                //Consulta el correo del Jefe Inmediato
                if (AJefeInmediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeInmediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeInmediato;
                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                            idJefeMediato = row["idPadre"].ToString().Trim();
                        }
                    }
                }
                #endregion Jefe Inmediato

                #region Jefe Mediato
                //Consulta el correo del Jefe Mediato
                if (AJefeMediato == "SI")
                {
                    if (!string.IsNullOrEmpty(idJefeMediato.Trim()))
                    {
                        selectCommand = "SELECT DJ.CorreoResponsable, JO.idPadre " +
                            "FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO INNER JOIN [Parametrizacion].[DetalleJerarquiaOrg] AS DJ ON DJ.idHijo = JO.idHijo " +
                            "WHERE JO.idHijo = " + idJefeMediato;

                        dad = new SqlDataAdapter(selectCommand, conString);
                        dtblDiscuss.Clear();
                        dad.Fill(dtblDiscuss);
                        view = new DataView(dtblDiscuss);

                        foreach (DataRowView row in view)
                        {
                            Destinatario = Destinatario + ";" + row["CorreoResponsable"].ToString().Trim();
                        }
                    }
                }
                #endregion Jefe Mediato

                #region Registro en la tabla de Correos Enviados
                //Insertar el Registro en la tabla de Correos Enviados
                SqlDataSource200.InsertParameters["Destinatario"].DefaultValue = Destinatario.Trim();
                SqlDataSource200.InsertParameters["Copia"].DefaultValue = Copia;
                SqlDataSource200.InsertParameters["Otros"].DefaultValue = Otros;
                SqlDataSource200.InsertParameters["Asunto"].DefaultValue = Asunto;
                SqlDataSource200.InsertParameters["Cuerpo"].DefaultValue = Cuerpo;
                SqlDataSource200.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                SqlDataSource200.InsertParameters["Tipo"].DefaultValue = "CREACION";
                SqlDataSource200.InsertParameters["FechaEnvio"].DefaultValue = "";
                SqlDataSource200.InsertParameters["IdEvento"].DefaultValue = idEvento.ToString().Trim();
                SqlDataSource200.InsertParameters["IdRegistro"].DefaultValue = idRegistro.ToString().Trim();
                SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = "1"; //Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource200.Insert();
                #endregion Registro en la tabla de Correos Enviados
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }
            #endregion

            if (!err)
            {
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                #region Insercion CorreosRecordatorios
                if (RequiereFechaCierre == "SI" && FechaFinal != "")
                //if (RequiereFechaCierre == "SI")
                {
                    //Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = FechaFinal;
                    SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = "1";//Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource201.Insert();
                }
                #endregion

                #region Envio de correo
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    //MailAddress fromAddress = new MailAddress("risksherlock@hotmail.com", "Software Sherlock");
                    MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");

                    message.From = fromAddress;//here you can set address

                    #region
                    foreach (string substr in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.To.Add(substr);
                    }
                    #endregion

                    #region
                    if (Copia.Trim() != "")
                        foreach (string substr in Copia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    #region
                    if (Otros.Trim() != "")
                        foreach (string substr in Otros.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    message.Subject = Asunto;//subject of email
                    message.IsBodyHtml = true;//To determine email body is html or not
                    message.Body = Cuerpo;

                    smtpClient.Send(message);
                }
                catch (Exception ex)
                {
                    //throw exception here you can write code to handle exception here
                    omb.ShowMessage("Error en el envio de la notificacion." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }
                #endregion

                #region Actualizacion Correo Enviado
                if (!err)
                {
                    //Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource200.Update();

                }
                #endregion
            }

            return (err);
        }

        #region PDFs

        private void loadFile()
        {
            #region Vars
            bool err = false;
            string IdMaximo;
            string nameFile;

            //Calcula el siguiente codigo a asignar como id de la tabla Auditoria.Archivo
            DataView dvArchivo = (DataView)this.SqlDataSource101.Select(new DataSourceSelectArguments());
            IdMaximo = dvArchivo[0]["Maximo"].ToString().Trim();

            //En la formacion del nombre del archivo el segundo item, es decir -10-, corresponde al codigo de la tabla
            // Parametrizacion.ControlesUsuario , en este caso al control de usuario Gestión de Auditoría
            nameFile = IdMaximo + "-10-" + lblIdHallazgo.Text.Trim() + "-" + FileUpload1.FileName.ToString().Trim();
            #endregion Vars

            try
            {

                try
                {
                    FileUpload1.SaveAs(Server.MapPath("~/Archivos/PDFsGestionVerificacion/") + nameFile);
                }
                catch
                {
                    err = true;
                }

                if (!err)
                {
                    #region SQL
                    SqlDataSource100.InsertParameters["IdControlUsuario"].DefaultValue = "10";
                    SqlDataSource100.InsertParameters["IdRegistro"].DefaultValue = lblIdHallazgo.Text.Trim();
                    SqlDataSource100.InsertParameters["UrlArchivo"].DefaultValue = nameFile;
                    SqlDataSource100.InsertParameters["Descripcion"].DefaultValue = txtDescArchivo.Text;
                    SqlDataSource100.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource100.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim();
                    SqlDataSource100.Insert();
                    #endregion SQL
                }
            }
            catch (Exception ex)
            {
                #region
                omb.ShowMessage("El archivo no pudo ser cargado." + "<br/>" + "Se presento el siguiente error:" + ex.Message, 1, "Atencion");
                err = true;
                #endregion
            }

            if (!err)
            {
                #region
                omb.ShowMessage("El archivo se cargo con exito en el servidor de datos." + "<br/>" + "Tamaño del Archivo: " + FileUpload1.FileBytes.Length / 1024 + " Kb" + "<br/> Nombre del Archivo: " + nameFile, 3, "Atención");
                filaGridAnexos.Visible = true;
                filaSubirAnexos.Visible = false;
                #endregion
            }
        }

        private void descargarArchivo(string nameFile)
        {
            string filePath;
            filePath = Server.MapPath("~/Archivos/PDFsGestionVerificacion/" + nameFile);
            try
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nameFile + ";");
                Response.TransmitFile(filePath);
                Response.End();
            }
            catch (Exception ex)
            {
                omb.ShowMessage("El archivo no pudo ser descargado del servidor de datos." + "<br/>" + "Se presento el siguiente error:" + ex.Message, 1, "Atencion");
            }
        }

        private void mtdCargarPdfVerificacion()
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            string strNombreArchivo = string.Empty, strIdControl = "10";
            #endregion Vars

            dtInfo = cAuditoria.loadCodigoArchivo();

            #region Nombre Archivo

            if (dtInfo.Rows.Count > 0)
                strNombreArchivo = string.Format("{0}-{1}-{2}-{3}",
                    dtInfo.Rows[0]["NumRegistros"].ToString().Trim(),
                    strIdControl, lblIdHallazgo.Text.Trim(), FileUpload1.FileName.ToString().Trim());
            else
                strNombreArchivo = string.Format("1-{0}-{1}-{2}",
                    strIdControl, lblIdHallazgo.Text.Trim(), FileUpload1.FileName.ToString().Trim());
            #endregion Nombre Archivo

            #region Archivo
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bPdfData = br.ReadBytes((Int32)fs.Length);
            #endregion Archivo

            cAuditoria.mtdAgregarArchivoPdf(lblIdHallazgo.Text.Trim(), strIdControl, txtDescArchivo.Text.Trim(),
                strNombreArchivo, bPdfData);

            filaGridAnexos.Visible = true;
            filaSubirAnexos.Visible = false;
        }

        private void mtdDescargarPdfVerificacion(string strNombreArchivo)
        {
            #region Vars
            //string strNombreArchivo = InfoGridControles.Rows[RowGridArchivoControl]["UrlArchivo"].ToString().Trim();
            byte[] bPdfData = cAuditoria.mtdDescargarArchivoPdf(strNombreArchivo);
            #endregion Vars

            if (bPdfData != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strNombreArchivo);
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bPdfData);
                Response.End();
            }
        }

        #endregion PDFs

        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMacroprocesoEventos.Items.Clear();
            ddlMacroprocesoEventos.Items.Insert(0, new ListItem("---", "---"));
            ddlProcesoEvento.Items.Clear();
            ddlProcesoEvento.Items.Insert(0, new ListItem("---", "---"));
            ddlSubproceso.Items.Clear();
            ddlSubproceso.Items.Insert(0, new ListItem("---", "---"));
            if (ddlCadenaValor.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLMacroproceso(ddlCadenaValor.SelectedValue.ToString().Trim());
            }
        }

        protected void ddlMacroprocesoEventos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProcesoEvento.Items.Clear();
            ddlProcesoEvento.Items.Insert(0, new ListItem("---", "---"));
            ddlSubproceso.Items.Clear();
            ddlSubproceso.Items.Insert(0, new ListItem("---", "---"));
            if (ddlMacroprocesoEventos.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLProceso(ddlMacroprocesoEventos.SelectedValue.ToString().Trim());
            }
        }

        protected void ddlProcesoEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSubproceso.Items.Clear();
            ddlSubproceso.Items.Insert(0, new ListItem("---", "---"));
            if (ddlProcesoEvento.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubProceso(ddlProcesoEvento.SelectedValue.ToString().Trim());
            }
        }

        protected void ImgSearchEvento_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Sanitizer.GetSafeHtmlFragment(txbCodEvento.Text.Trim()) == "" && Sanitizer.GetSafeHtmlFragment(txbDescripcionEvento.Text.Trim()) == "" && 
                    ddlCadenaValor.SelectedValue.ToString().Trim() == "---" && ddlMacroprocesoEventos.SelectedValue.ToString().Trim() == "---" && 
                    ddlProcesoEvento.SelectedValue.ToString().Trim() == "---" && ddlSubproceso.SelectedValue.ToString().Trim() == "---")
                    omb.ShowMessage("Debe ingresar por lo menos un parámetro de consulta.",2, "Atención");
                else
                {
                    loadGridEventos();
                    loadInfoEventos();
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al realizar la consulta. " + ex.Message,2,"Atención");
            }
        }
        private void loadGridEventos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdEvento", typeof(string));
            grid.Columns.Add("CodigoEvento", typeof(string));
            grid.Columns.Add("IdEmpresa", typeof(string));
            grid.Columns.Add("IdRegion", typeof(string));
            grid.Columns.Add("IdPais", typeof(string));
            grid.Columns.Add("IdDepartamento", typeof(string));
            grid.Columns.Add("IdCiudad", typeof(string));
            grid.Columns.Add("IdOficinaSucursal", typeof(string));
            grid.Columns.Add("DetalleUbicacion", typeof(string));
            grid.Columns.Add("DescripcionEvento", typeof(string));
            grid.Columns.Add("IdServicio", typeof(string));
            grid.Columns.Add("IdSubServicio", typeof(string));
            grid.Columns.Add("FechaInicio", typeof(string));
            //grid.Columns.Add("HoraInicio", typeof(string));
            grid.Columns.Add("HorI", typeof(string));
            grid.Columns.Add("MinI", typeof(string));
            grid.Columns.Add("amI", typeof(string));
            grid.Columns.Add("FechaFinalizacion", typeof(string));
            //grid.Columns.Add("HoraFinalizacion", typeof(string));
            grid.Columns.Add("HorF", typeof(string));
            grid.Columns.Add("MinF", typeof(string));
            grid.Columns.Add("amF", typeof(string));
            grid.Columns.Add("FechaDescubrimiento", typeof(string));
            //grid.Columns.Add("HoraDescubrimiento", typeof(string));
            grid.Columns.Add("HorD", typeof(string));
            grid.Columns.Add("MinD", typeof(string));
            grid.Columns.Add("amD", typeof(string));
            grid.Columns.Add("IdCanal", typeof(string));
            grid.Columns.Add("IdGeneraEvento", typeof(string));
            grid.Columns.Add("GeneraEvento", typeof(string));
            grid.Columns.Add("GeneradorEvento", typeof(string));
            grid.Columns.Add("cuantiaperdida", typeof(string));
            grid.Columns.Add("FechaEvento", typeof(string));
            grid.Columns.Add("Usuario", typeof(string));
            grid.Columns.Add("IdCadenaValor", typeof(string));
            grid.Columns.Add("IdMacroproceso", typeof(string));
            grid.Columns.Add("IdProceso", typeof(string));
            grid.Columns.Add("IdSubProceso", typeof(string));
            grid.Columns.Add("IdActividad", typeof(string));
            grid.Columns.Add("ResponsableEvento", typeof(string));
            grid.Columns.Add("ResponsableSolucion", typeof(string));
            grid.Columns.Add("IdClase", typeof(string));
            grid.Columns.Add("NombreClaseEvento", typeof(string));
            grid.Columns.Add("IdSubClase", typeof(string));
            grid.Columns.Add("NombreTipoPerdidaEvento", typeof(string));
            grid.Columns.Add("AfectaContinudad", typeof(string));
            grid.Columns.Add("IdEstado", typeof(string));
            grid.Columns.Add("Observaciones", typeof(string));
            grid.Columns.Add("ResponsableContabilidad", typeof(string));
            grid.Columns.Add("NombreResContabilidad", typeof(string));
            grid.Columns.Add("CuentaPUC", typeof(string));
            grid.Columns.Add("CuentaOrden", typeof(string));
            grid.Columns.Add("CuentaPerdida", typeof(string));
            grid.Columns.Add("Moneda1", typeof(string));
            grid.Columns.Add("TasaCambio1", typeof(string));
            grid.Columns.Add("ValorPesos1", typeof(string));
            grid.Columns.Add("ValorRecuperadoTotal", typeof(string));
            grid.Columns.Add("Moneda2", typeof(string));
            grid.Columns.Add("TasaCambio2", typeof(string));
            grid.Columns.Add("ValorPesos2", typeof(string));
            grid.Columns.Add("ValorRecuperadoSeguro", typeof(string));
            grid.Columns.Add("ValorPesos3", typeof(string));
            grid.Columns.Add("Recuperacion", typeof(string));
            grid.Columns.Add("ValorRecuperacion", typeof(string));
            grid.Columns.Add("IdLineaProceso", typeof(string));
            grid.Columns.Add("IdSubLineaProceso", typeof(string));
            grid.Columns.Add("MasLineas", typeof(string));
            grid.Columns.Add("NomGeneradorEvento", typeof(string));
            grid.Columns.Add("FechaContab", typeof(string));
            grid.Columns.Add("HoraContab", typeof(string));
            grid.Columns.Add("MinContab", typeof(string));
            grid.Columns.Add("amContab", typeof(string));
            InfoGridEventos = grid;
            GVeventos.DataSource = InfoGridEventos;
            GVeventos.DataBind();
        }

        private void loadInfoEventos()
        {
            DataTable dtInfo = new DataTable();
            int IdUsuarioJerarquia = Convert.ToInt32(Session["IdJerarquia"].ToString());
            dtInfo = cEvento.loadInfoEventos(Sanitizer.GetSafeHtmlFragment(txbCodEvento.Text.Trim()), Sanitizer.GetSafeHtmlFragment(txbDescripcionEvento.Text.Trim()),
                ddlCadenaValor.SelectedValue.ToString().Trim(), ddlMacroprocesoEventos.SelectedValue.ToString().Trim(), ddlProcesoEvento.SelectedValue.ToString().Trim(),
                ddlSubproceso.SelectedValue.ToString().Trim(), IdUsuarioJerarquia);

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    TbEventos.Visible = true;
                    InfoGridEventos.Rows.Add(new Object[] {
                                                            dtInfo.Rows[rows]["IdEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["CodigoEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdEmpresa"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdRegion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdPais"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdDepartamento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdCiudad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdOficinaSucursal"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["DetalleUbicacion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["DescripcionEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdServicio"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdSubServicio"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaInicio"].ToString().Trim(),
                                                            //dtInfo.Rows[rows]["HoraInicio"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["HorI"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MinI"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["amI"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaFinalizacion"].ToString().Trim(),
                                                            //dtInfo.Rows[rows]["HoraFinalizacion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["HorF"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MinF"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["amF"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaDescubrimiento"].ToString().Trim(),
                                                            //dtInfo.Rows[rows]["HoraDescubrimiento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["HorD"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MinD"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["amD"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdCanal"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdGeneraEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["GeneraEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["GeneradorEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["cuantiaperdida"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Usuario"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdCadenaValor"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdMacroproceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdProceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdSubProceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdActividad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ResponsableEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ResponsableSolucion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdClase"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreClaseEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdSubClase"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreTipoPerdidaEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["AfectaContinudad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdEstado"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Observaciones"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ResponsableContabilidad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NombreResContabilidad"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["CuentaPUC"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["CuentaOrden"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["CuentaPerdida"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Moneda1"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["TasaCambio1"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorPesos1"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorRecuperadoTotal"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Moneda2"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["TasaCambio2"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorPesos2"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorRecuperadoSeguro"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorPesos3"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["Recuperacion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["ValorRecuperacion"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdLineaProceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["IdSubLineaProceso"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MasLineas"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["NomGeneradorEvento"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["FechaContab"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["HoraContab"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["MinContab"].ToString().Trim(),
                                                            dtInfo.Rows[rows]["amContab"].ToString().Trim()
                                                          });
                }

                GVeventos.PageIndex = PagIndexInfoGridEventos;
                GVeventos.DataSource = InfoGridEventos;
                GVeventos.DataBind();
            }
            else
            {
                loadGridEventos();
                TbEventos.Visible = false;
                omb.ShowMessage("El usuario no tiene eventos reportados.",2,"Atención");
            }
        }

        protected void GVeventos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            switch (e.CommandName)
            {
                case "Modificar":
                    RowGridEventos = (Convert.ToInt16(GVeventos.PageSize) * PagIndexInfoGridEventos) + Convert.ToInt16(e.CommandArgument);
                    int Rows = Convert.ToInt16(e.CommandArgument);
                    GridViewRow row = GVeventos.Rows[Rows];
                    var colsNoVisible = GVeventos.DataKeys[Rows].Values;
                    Session["IdEvento"] = colsNoVisible[0].ToString();
                    cargardatosevento();
                    break;
            }
        }
        private void mtdLoadDLL()
        {
            loadDDLRegion();
            loadDDLCadenaValor();
            
            
            loadDDLTipoPerdidaEvento();
            //loadDDLTipoRecursoPlanAccion();
            //loadDDLEstadoPlanAccion();
            //loadGridEventos();
            PopulateTreeView();
            loadDDLServicio();
            loadDDLClaseRiesgo();
            loadDDLEstado();
            loadDDLCanal();
            loadDDLGenerador();
            loadDDLLineaNegocio();
            mtdLoadDDLEmpresa();
            //loadDDLMailEvent();
        }
        private void cargardatosevento()
        {
            mtdLoadDLL();
            TbConEventos.Visible = true;
            TabContainerEventos.Tabs[1].Visible = true;
            //TabContainerEventos.Tabs[2].Visible = true;
            ImageButton6.Visible = false;
            ImageButton8.Visible = true;
            //trRiesgosEventos.Visible = true;
            //tab 1
            lblCodigoEvento.Text = InfoGridEventos.Rows[RowGridEventos]["CodigoEvento"].ToString().Trim();
            txbDetalleUbicacion.Text = InfoGridEventos.Rows[RowGridEventos]["DetalleUbicacion"].ToString().Trim();
            txbDescripcionEventoEv.Text = InfoGridEventos.Rows[RowGridEventos]["DescripcionEvento"].ToString().Trim();
            //Fecha Inicio
            txbFechaIniEvento.Text = InfoGridEventos.Rows[RowGridEventos]["FechaInicio"].ToString().Trim();

            for (int i = 0; i < DropDownList12.Items.Count; i++)
            {
                DropDownList12.SelectedIndex = i;
                if (DropDownList12.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["HorI"].ToString().Trim())
                    break;
                else
                    DropDownList12.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList13.Items.Count; i++)
            {
                DropDownList13.SelectedIndex = i;
                if (DropDownList13.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["MinI"].ToString().Trim())
                    break;
                else
                    DropDownList13.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList14.Items.Count; i++)
            {
                DropDownList14.SelectedIndex = i;
                if (DropDownList14.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["amI"].ToString().Trim())
                    break;
                else
                    DropDownList14.SelectedIndex = 0;
            }

            //Fecha Finalizacion
            txbFechaFinEvento.Text = InfoGridEventos.Rows[RowGridEventos]["FechaFinalizacion"].ToString().Trim();
            for (int i = 0; i < DropDownList68.Items.Count; i++)
            {
                DropDownList68.SelectedIndex = i;
                if (DropDownList68.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["HorF"].ToString().Trim())
                    break;
                else
                    DropDownList68.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList69.Items.Count; i++)
            {
                DropDownList69.SelectedIndex = i;
                if (DropDownList69.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["MinF"].ToString().Trim())
                    break;
                else
                    DropDownList69.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList70.Items.Count; i++)
            {
                DropDownList70.SelectedIndex = i;
                if (DropDownList70.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["amF"].ToString().Trim())
                    break;
                else
                    DropDownList70.SelectedIndex = 0;
            }

            //Fecha Descubrimiento
            TextBox49.Text = InfoGridEventos.Rows[RowGridEventos]["FechaDescubrimiento"].ToString().Trim();
            for (int i = 0; i < DropDownList71.Items.Count; i++)
            {
                DropDownList71.SelectedIndex = i;
                if (DropDownList71.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["HorD"].ToString().Trim())
                    break;
                else
                    DropDownList71.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList72.Items.Count; i++)
            {
                DropDownList72.SelectedIndex = i;
                if (DropDownList72.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["MinD"].ToString().Trim())
                    break;
                else
                    DropDownList72.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList73.Items.Count; i++)
            {
                DropDownList73.SelectedIndex = i;
                if (DropDownList73.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["amD"].ToString().Trim())
                    break;
                else
                    DropDownList73.SelectedIndex = 0;
            }

            lblIdDependencia4.Text = InfoGridEventos.Rows[RowGridEventos]["GeneraEvento"].ToString().Trim();
            TextBox52.Text = InfoGridEventos.Rows[RowGridEventos]["cuantiaperdida"].ToString().Trim();
            TextBox39.Text = InfoGridEventos.Rows[RowGridEventos]["FechaEvento"].ToString().Trim();
            TextBox40.Text = InfoGridEventos.Rows[RowGridEventos]["Usuario"].ToString().Trim();

            for (int i = 0; i < ddlEmpresa.Items.Count; i++)
            {
                ddlEmpresa.SelectedIndex = i;
                if (ddlEmpresa.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdEmpresa"].ToString().Trim())
                    break;
                else
                    ddlEmpresa.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdRegion"].ToString().Trim())
                    break;
                else
                    DropDownList1.SelectedIndex = 0;
            }

            loadDDLPais(DropDownList1.SelectedValue.ToString().Trim(), 1);
            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                DropDownList2.SelectedIndex = i;
                if (DropDownList2.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdPais"].ToString().Trim())
                    break;
                else
                    DropDownList2.SelectedIndex = 0;
            }

            loadDDLDepartamento(DropDownList2.SelectedValue.ToString().Trim(), 1);
            for (int i = 0; i < DropDownList3.Items.Count; i++)
            {
                DropDownList3.SelectedIndex = i;
                if (DropDownList3.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdDepartamento"].ToString().Trim())
                    break;
                else
                    DropDownList3.SelectedIndex = 0;
            }

            loadDDLCiudad(DropDownList3.SelectedValue.ToString().Trim(), 1);
            for (int i = 0; i < DropDownList4.Items.Count; i++)
            {
                DropDownList4.SelectedIndex = i;
                if (DropDownList4.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdCiudad"].ToString().Trim())
                    break;
                else
                    DropDownList4.SelectedIndex = 0;
            }

            loadDDLOficinaSucursal(DropDownList4.SelectedValue.ToString().Trim(), 1);
            for (int i = 0; i < DropDownList5.Items.Count; i++)
            {
                DropDownList5.SelectedIndex = i;
                if (DropDownList5.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdOficinaSucursal"].ToString().Trim())
                    break;
                else
                    DropDownList5.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList24.Items.Count; i++)
            {
                DropDownList24.SelectedIndex = i;
                if (DropDownList24.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdServicio"].ToString().Trim())
                    break;
                else
                    DropDownList24.SelectedIndex = 0;
            }

            if (DropDownList24.SelectedValue != "---")
            {
                loadDDLSubServicio(DropDownList24.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList25.Items.Count; i++)
                {
                    DropDownList25.SelectedIndex = i;
                    if (DropDownList25.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdSubServicio"].ToString().Trim())
                        break;
                    else
                        DropDownList25.SelectedIndex = 0;
                }
            }

            for (int i = 0; i < DropDownList26.Items.Count; i++)
            {
                DropDownList26.SelectedIndex = i;
                if (DropDownList26.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdCanal"].ToString().Trim())
                    break;
                else

                    DropDownList26.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList27.Items.Count; i++)
            {
                DropDownList27.SelectedIndex = i;
                if (DropDownList27.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdGeneraEvento"].ToString().Trim())
                    break;
                else
                    DropDownList27.SelectedIndex = 0;
            }

            //Muetsra la informacion del generador de evento
            if (InfoGridEventos.Rows[RowGridEventos]["IdGeneraEvento"].ToString().Trim() == "1")
            {
                lresponable.Visible = true;
                tresponsable.Visible = true;
                TextBox51.Enabled = false;
                imgDependencia4.Visible = true;
                TextBox51.Text = InfoGridEventos.Rows[RowGridEventos]["GeneradorEvento"].ToString().Trim();
            }
            else
            {
                lresponable.Visible = true;
                tresponsable.Visible = true;
                TextBox51.Enabled = true;
                imgDependencia4.Visible = false;
                TextBox51.Text = InfoGridEventos.Rows[RowGridEventos]["NomGeneradorEvento"].ToString().Trim();
            }

            //tab 2
            #region TAB 2 Datos Complementarios
            TextBox34.Text = InfoGridEventos.Rows[RowGridEventos]["ResponsableSolucion"].ToString().Trim();
            lblIdDependencia1.Text = InfoGridEventos.Rows[RowGridEventos]["ResponsableEvento"].ToString().Trim();
            for (int i = 0; i < DropDownList67.Items.Count; i++)
            {
                DropDownList67.SelectedIndex = i;
                if (DropDownList67.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdCadenaValor"].ToString().Trim())
                    break;
                else
                    DropDownList67.SelectedIndex = 0;
            }

            if (DropDownList67.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLMacroproceso(DropDownList67.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList9.Items.Count; i++)
                {
                    DropDownList9.SelectedIndex = i;
                    if (DropDownList9.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdMacroproceso"].ToString().Trim())
                        break;
                    else
                        DropDownList9.SelectedIndex = 0;
                }
            }
            if (DropDownList9.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLProceso(DropDownList9.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList10.Items.Count; i++)
                {
                    DropDownList10.SelectedIndex = i;
                    if (DropDownList10.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdProceso"].ToString().Trim())
                        break;
                    else
                        DropDownList10.SelectedIndex = 0;
                }
            }

            if (DropDownList10.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubProceso(DropDownList10.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList6.Items.Count; i++)
                {
                    DropDownList6.SelectedIndex = i;
                    if (DropDownList6.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdSubProceso"].ToString().Trim())
                        break;
                    else
                        DropDownList6.SelectedIndex = 0;
                }
            }

            if (DropDownList6.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLActividad(DropDownList6.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList11.Items.Count; i++)
                {
                    DropDownList11.SelectedIndex = i;
                    if (DropDownList11.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdActividad"].ToString().Trim())
                        break;
                    else
                        DropDownList11.SelectedIndex = 0;
                }
            }

            lblIdDependencia4.Text = InfoGridEventos.Rows[RowGridEventos]["GeneraEvento"].ToString().Trim();

            for (int i = 0; i < DropDownList33.Items.Count; i++)
            {
                DropDownList33.SelectedIndex = i;
                if (DropDownList33.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdClase"].ToString().Trim())
                    break;
                else
                    DropDownList33.SelectedIndex = 0;
            }

            if (DropDownList33.SelectedValue != "---")
            {
                loadDDLSubClaseRiesgo(DropDownList33.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList34.Items.Count; i++)
                {
                    DropDownList34.SelectedIndex = i;
                    if (DropDownList34.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdSubClase"].ToString().Trim())
                        break;
                    else
                        DropDownList34.SelectedIndex = 0;
                }
            }

            for (int i = 0; i < DropDownList8.Items.Count; i++)
            {
                DropDownList8.SelectedIndex = i;
                if (DropDownList8.SelectedItem.Text.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["NombreTipoPerdidaEvento"].ToString().Trim())
                    break;
                else
                    DropDownList8.SelectedIndex = 0;
            }

            //cargamos los los tipo y subtipos de lineas
            for (int i = 0; i < DropDownList23.Items.Count; i++)
            {
                DropDownList23.SelectedIndex = i;
                if (DropDownList23.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdLineaProceso"].ToString().Trim())
                    break;
                else
                    DropDownList23.SelectedIndex = 0;
            }

            if (DropDownList23.SelectedValue != "NULL")
            {
                loadDDLSubLineaNegocio(DropDownList23.SelectedValue.ToString().Trim(), 1);
                for (int i = 0; i < DropDownList29.Items.Count; i++)
                {
                    DropDownList29.SelectedIndex = i;
                    if (DropDownList29.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdSubLineaProceso"].ToString().Trim())
                        break;
                    else
                        DropDownList29.SelectedIndex = 0;
                }
            }

            string ff = InfoGridEventos.Rows[RowGridEventos]["AfectaContinudad"].ToString().Trim();
            if (ff == "True")
                ff = "1";
            else
                ff = "0";

            for (int i = 0; i < DropDownList15.Items.Count; i++)
            {
                DropDownList15.SelectedIndex = i;
                if (DropDownList15.SelectedValue.ToString().Trim() == ff)
                    break;
                else
                    DropDownList15.SelectedIndex = 0;
            }

            for (int i = 0; i < DropDownList16.Items.Count; i++)
            {
                DropDownList16.SelectedIndex = i;
                if (DropDownList16.SelectedValue.ToString().Trim() == InfoGridEventos.Rows[RowGridEventos]["IdEstado"].ToString().Trim())
                    break;
                else
                    DropDownList16.SelectedIndex = 0;
            }

           

            TextBox53.Text = InfoGridEventos.Rows[RowGridEventos]["Observaciones"].ToString().Trim();
            TextBox17.Text = InfoGridEventos.Rows[RowGridEventos]["MasLineas"].ToString().Trim();

            if (TextBox17.Text != "")
                TrMasLNegocio.Visible = true;
            else
                TrMasLNegocio.Visible = false;

            #endregion TAB 2 Contabilizacion

            ///Validad si es nuevo para el envío de notificacioón
            if (!string.IsNullOrEmpty(lblIdDependencia1.Text.Trim()))
                lblExisteResponsableNotificacion.Text = "S";
        }
        private void loadDDLPais(String IdRegion, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLPais(IdRegion);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList2.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombrePais"].ToString().Trim(), dtInfo.Rows[i]["IdPais"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar pais. " + ex.Message,2,"Atención");
            }
        }
        private void loadDDLSubServicio(String IdServicio, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLSubServicio(IdServicio);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList25.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["SubDescripcion"].ToString().Trim(), dtInfo.Rows[i]["IdSubServicio"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar SubServicio. " + ex.Message,2,"Atención");
            }
        }

        private void loadDDLSubClaseRiesgo(String IdClase, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLSubClaseRiesgo(IdClase);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList34.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["SubDescripcion"].ToString().Trim(), dtInfo.Rows[i]["IdSubClase"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar SubClaseRiesgo. " + ex.Message,2,"Atención");
            }
        }

        private void loadDDLSubLineaNegocio(String IdLineaNegocio, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLSubLineaNegocio(IdLineaNegocio);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList29.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["SubDescripcion"].ToString().Trim(), dtInfo.Rows[i]["IdSubLineaNegocio"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar SubLineaNegocioRiesgo. " + ex.Message,2,"Atención");
            }
        }
        private void loadDDLDepartamento(String IdPais, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLDepartamento(IdPais);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList3.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreDepartamento"].ToString().Trim(), dtInfo.Rows[i]["IdDepartamento"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar departamento. " + ex.Message,2,"Atención");
            }
        }

        private void loadDDLCiudad(String IdDepartamento, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLCiudad(IdDepartamento);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList4.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCiudad"].ToString().Trim(), dtInfo.Rows[i]["IdCiudad"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar ciudad. " + ex.Message,2,"Atención");
            }
        }

        private void loadDDLOficinaSucursal(String IdCiudad, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLOficinaSucursal(IdCiudad);
                switch (Tipo)
                {
                    case 1:
                        for (int i = 0; i < dtInfo.Rows.Count; i++)
                        {
                            DropDownList5.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreOficinaSucursal"].ToString().Trim(), dtInfo.Rows[i]["IdOficinaSucursal"].ToString()));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar oficina/Sucursal. " + ex.Message,2,"Atención");
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
                    case 1:
                        if (DropDownList67.SelectedValue != "---")
                        {
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                DropDownList9.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                            }
                        }
                        break;
                    /*case 3:
                        if (DropDownList19.SelectedValue != "---")
                        {
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                DropDownList20.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                            }
                        }
                        break;*/
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar macroproceso. " + ex.Message,2,"Atención");
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
                    case 1:
                        if (DropDownList9.SelectedValue != "---")
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                DropDownList10.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProceso"].ToString().Trim(), dtInfo.Rows[i]["IdProceso"].ToString()));
                            }
                        break;
                    /*case 3:
                        if (DropDownList20.SelectedValue != "---")
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                DropDownList21.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProceso"].ToString().Trim(), dtInfo.Rows[i]["IdProceso"].ToString()));
                            }
                        break;*/
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar proceso. " + ex.Message,2,"Atención");
            }
        }

        private void loadDDLSubProceso(String IdProceso, int Tipo)
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLSubProceso(IdProceso);
                switch (Tipo)
                {
                    case 1:
                        if (DropDownList10.SelectedValue != "---")
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                DropDownList6.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreSubProceso"].ToString().Trim(), dtInfo.Rows[i]["IdSubProceso"].ToString()));
                            }
                        break;
                    /*case 3:
                        if (DropDownList21.SelectedValue != "---")
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                DropDownList22.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreSubProceso"].ToString().Trim(), dtInfo.Rows[i]["IdSubProceso"].ToString()));
                            }
                        break;*/
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar subproceso. " + ex.Message,2,"Atención");
            }
        }

        private void loadDDLActividad(String IdSubproceso, int Tipo)
        {
            if (DropDownList6.SelectedValue != "---")
            {
                try
                {
                    DataTable dtInfo = new DataTable();
                    dtInfo = cRiesgo.loadDDLActividad(IdSubproceso);
                    switch (Tipo)
                    {
                        case 1:
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                DropDownList11.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreActividad"].ToString().Trim(), dtInfo.Rows[i]["IdActividad"].ToString()));
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    omb.ShowMessage("Error al cargar actividad. " + ex.Message,2,"Atención");
                }
            }
        }
        private void mtdLoadDDLEmpresa()
        {
            DataTable dtInfo = new DataTable();

            try
            {
                dtInfo = cRiesgo.mtdLoadEmpresa(true);
                ddlEmpresa.Items.Insert(0, new ListItem("---", "---"));
                //ddlEmpresa1.Items.Insert(0, new ListItem("---", "---"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlEmpresa.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdEmpresa"].ToString()));
                    //ddlEmpresa1.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdEmpresa"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar Empresas. " + ex.Message,2,"Atención");
            }
        }
        private void loadDDLRegion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLRegion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList1.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreRegion"].ToString().Trim(), dtInfo.Rows[i]["IdRegion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar region. " + ex.Message,2,"Atención");
            }
        }
        private void loadDDLTipoPerdidaEvento()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cEvento.loadDDLTipoPerdidaEvento();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList8.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreTipoPerdidaEvento"].ToString().Trim(), dtInfo.Rows[i]["IdTipoPerdidaEvento"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar tipo perdida. " + ex.Message,2,"Atención");
            }
        }
        /*private void loadDDLTipoRecursoPlanAccion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLTipoRecursoPlanAccion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList17.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreTipoRecursoPlanAccion"].ToString().Trim(), dtInfo.Rows[i]["IdTipoRecursoPlanAccion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar tipo recurso. " + ex.Message,2,"Atención");
            }
        }*/
        /*private void loadDDLEstadoPlanAccion()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLEstadoPlanAccion();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList18.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreEstadoPlanAccion"].ToString().Trim(), dtInfo.Rows[i]["IdEstadoPlanAccion"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar estado. " + ex.Message,2,"Atención");
            }
        }*/
        private void loadDDLServicio()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLServicio();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList24.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdServicio"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar Servicio. " + ex.Message,2,"Atención");
            }
        }
        private void loadDDLClaseRiesgo()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLClaseRiesgo();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList33.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdClase"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar Clase Riesgo. " + ex.Message,2,"Atención");
            }
        }
        private void loadDDLEstado()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLEstado();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList16.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdEstado"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar Estado. " + ex.Message,2,"Atención");
            }
        }
        private void loadDDLCanal()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLCanal();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList26.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdCanal"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar Estado. " + ex.Message,2,"Atención");
            }
        }
        private void loadDDLGenerador()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLGenerador();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList27.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdGenerador"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar Generador del evento. " + ex.Message,2,"Atención");
            }
        }
        private void loadDDLLineaNegocio()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLLineaNegocio();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    DropDownList23.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdLineaNegocio"].ToString()));
                }
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al cargar Linea Negocio. " + ex.Message,2,"Atención");
            }
        }
        #region DDL

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            DropDownList2.Items.Insert(0, new ListItem("---", "---"));
            DropDownList3.Items.Clear();
            DropDownList3.Items.Insert(0, new ListItem("---", "---"));
            DropDownList4.Items.Clear();
            DropDownList4.Items.Insert(0, new ListItem("---", "---"));
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList1.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLPais(DropDownList1.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList3.Items.Clear();
            DropDownList3.Items.Insert(0, new ListItem("---", "---"));
            DropDownList4.Items.Clear();
            DropDownList4.Items.Insert(0, new ListItem("---", "---"));
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList2.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLDepartamento(DropDownList2.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList4.Items.Clear();
            DropDownList4.Items.Insert(0, new ListItem("---", "---"));
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList3.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLCiudad(DropDownList3.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList5.Items.Clear();
            DropDownList5.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList4.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLOficinaSucursal(DropDownList4.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList11.Items.Clear();
            DropDownList11.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList6.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLActividad(DropDownList6.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList9_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList10.Items.Clear();
            DropDownList10.Items.Insert(0, new ListItem("---", "0"));
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "0"));
            DropDownList11.Items.Clear();
            DropDownList11.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList9.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLProceso(DropDownList9.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList10_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "0"));
            DropDownList11.Items.Clear();
            DropDownList11.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList10.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubProceso(DropDownList10.SelectedValue.ToString().Trim(), 1);
            }
        }

        /*protected void DropDownList19_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList20.Items.Clear();
            DropDownList20.Items.Insert(0, new ListItem("---", "---"));
            DropDownList21.Items.Clear();
            DropDownList21.Items.Insert(0, new ListItem("---", "---"));
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList19.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLMacroproceso(DropDownList19.SelectedValue.ToString().Trim(), 3);
            }
        }*/  

        /*protected void DropDownList20_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList21.Items.Clear();
            DropDownList21.Items.Insert(0, new ListItem("---", "---"));
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList20.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLProceso(DropDownList20.SelectedValue.ToString().Trim(), 3);
            }
        }*/

        /*protected void DropDownList21_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList22.Items.Clear();
            DropDownList22.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList21.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubProceso(DropDownList21.SelectedValue.ToString().Trim(), 3);
            }
        }*/

        protected void DropDownList23_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList29.SelectedValue == "---")
            {
                TextBox17.Text = "";
                TrMasLNegocio.Visible = false;
            }
            DropDownList29.Items.Clear();
            DropDownList29.Items.Insert(0, new ListItem("---", "NULL"));
            if (DropDownList23.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubLineaNegocio(DropDownList23.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList24_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList25.Items.Clear();
            DropDownList25.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList24.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubServicio(DropDownList24.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList27_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox51.Text = null;
            lblIdDependencia4.Text = null;
            if (DropDownList27.SelectedValue != "---")
            {
                lresponable.Visible = true;
                tresponsable.Visible = true;
                RequiredFieldValidatorTextBox51.ValidationGroup = "Addne";
                if (DropDownList27.SelectedValue != "1")
                {
                    TextBox51.Enabled = true;
                    imgDependencia4.Visible = false;
                }
                else
                {
                    TextBox51.Enabled = false;
                    imgDependencia4.Visible = true;
                }
            }
            else
            {
                RequiredFieldValidatorTextBox51.ValidationGroup = null;
                lresponable.Visible = false;
                tresponsable.Visible = false;
                TextBox51.Enabled = true;
            }
        }

        /*protected void DropDownList28_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList28.SelectedValue == "1")
            {
                trecuperacion.Visible = true;
                lrecuperacio.Visible = true;
                TextBox46.Focus();
            }
            else
            {
                trecuperacion.Visible = false;
                lrecuperacio.Visible = false;
                TextBox46.Text = "";
            }
        }*/

        protected void DropDownList29_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList29.SelectedValue != "---")
            {
                TrMasLNegocio.Visible = true;
            }
            else
            {
                TextBox17.Text = "";
                TrMasLNegocio.Visible = false;
            }
        }

        protected void DropDownList33_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList34.Items.Clear();
            DropDownList34.Items.Insert(0, new ListItem("---", "---"));
            if (DropDownList33.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLSubClaseRiesgo(DropDownList33.SelectedValue.ToString().Trim(), 1);
            }
        }

        protected void DropDownList67_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList9.Items.Clear();
            DropDownList9.Items.Insert(0, new ListItem("---", "---"));
            DropDownList10.Items.Clear();
            DropDownList10.Items.Insert(0, new ListItem("---", "0"));
            DropDownList6.Items.Clear();
            DropDownList6.Items.Insert(0, new ListItem("---", "0"));
            DropDownList11.Items.Clear();
            DropDownList11.Items.Insert(0, new ListItem("---", "0"));
            if (DropDownList67.SelectedValue.ToString().Trim() != "---")
            {
                loadDDLMacroproceso(DropDownList67.SelectedValue.ToString().Trim(), 1);
            }
        }

        #endregion DDL

        protected void TreeView4_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox34.Text = TreeView4.SelectedNode.Text.Trim();
            lblIdDependencia4.Text = TreeView4.SelectedNode.Value;
        }
        #region Textbox
        protected void TextBox52_TextChanged(object sender, EventArgs e)
        {
            try
            {
                long N = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(TextBox52.Text));
                TextBox52.Text = N.ToString("N0");
            }
            catch (Exception a)
            {
                TextBox52.Text = "";
                TextBox52.Focus();
                omb.ShowMessage("Error al poner separadores de mil. <br/> Solo números. [Error: " + a.Message + "]", 2, "Atención");
            }
        }
        #endregion Textbox
        string FechaFin = string.Empty;
        string NombreResponsable = string.Empty;
        string strCodigoNHEvento = string.Empty, strCodigoEvento = string.Empty;

        protected void ImageButton18_Click(object sender, ImageClickEventArgs e)
        {
            TbConEventos.Visible = false;
            //RadioButtonList1.ClearSelection();
            reseteartodo();
            TabContainerEventos.Tabs[1].Visible = false;
            GVeventos.DataSource = null;
            GVeventos.DataBind();
            //TabContainerEventos.Tabs[2].Visible = false;
            //trRiesgosEventos.Visible = false;
        }
        private void reseteartodo()
        {
            lblExisteResponsableNotificacion.Text = string.Empty;
            Label55.Text = "";
            ddlEmpresa.SelectedIndex = 0;
            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
            DropDownList4.SelectedIndex = 0;
            DropDownList5.SelectedIndex = 0;
            txbDetalleUbicacion.Text = "";
            txbDescripcionEventoEv.Text = "";
            DropDownList24.SelectedIndex = 0;
            DropDownList25.SelectedIndex = 0;
            txbFechaIniEvento.Text = "";
            DropDownList13.SelectedIndex = 0;
            DropDownList13.SelectedIndex = 0;
            DropDownList14.SelectedIndex = 0;
            txbFechaFinEvento.Text = "";
            DropDownList68.SelectedIndex = 0;
            DropDownList69.SelectedIndex = 0;
            DropDownList70.SelectedIndex = 0;
            TextBox49.Text = "";
            DropDownList71.SelectedIndex = 0;
            DropDownList72.SelectedIndex = 0;
            DropDownList73.SelectedIndex = 0;
            DropDownList26.SelectedIndex = 0;
            DropDownList27.SelectedIndex = 0;
            TextBox51.Text = "";
            TextBox52.Text = "";
            TextBox39.Text = "";
            TextBox40.Text = "";
            //tab 2
            DropDownList67.SelectedIndex = 0;
            DropDownList9.SelectedIndex = 0;
            DropDownList10.SelectedIndex = 0;
            DropDownList6.SelectedIndex = 0;
            DropDownList11.SelectedIndex = 0;
            TextBox34.Text = "";
            DropDownList33.SelectedIndex = 0;
            DropDownList23.SelectedIndex = 0;
            DropDownList29.SelectedIndex = 0;
            DropDownList34.SelectedIndex = 0;
            DropDownList8.SelectedIndex = 0;
            DropDownList15.SelectedIndex = 0;
            DropDownList16.SelectedIndex = 0;
            TextBox53.Text = "";
            
            ImageButton6.Visible = false;
            ImageButton8.Visible = false;

            if (TreeViewlast.SelectedNode != null)
                TreeViewlast.SelectedNode.Selected = false;

            if (TreeView2.SelectedNode != null)
                TreeView2.SelectedNode.Selected = false;

            if (TreeView3.SelectedNode != null)
                TreeView3.SelectedNode.Selected = false;

            if (TreeView4.SelectedNode != null)
                TreeView4.SelectedNode.Selected = false;

        }
        protected void ImageButton17_Click(object sender, ImageClickEventArgs e)
        {
            
                string Horaini = string.Empty, Horafin = string.Empty, Horades = string.Empty;

                if (DropDownList12.SelectedValue != "---" && DropDownList13.SelectedValue != "---" && DropDownList14.SelectedValue != "---")
                    Horaini = DropDownList12.SelectedItem.Text.ToString() + ":" + DropDownList13.SelectedItem.Text.ToString() + " " + DropDownList14.SelectedItem.Text.ToString().Trim();

                if (DropDownList68.SelectedValue != "---" && DropDownList69.SelectedValue != "---" && DropDownList70.SelectedValue != "---")
                    Horafin = DropDownList68.SelectedItem.Text.ToString() + ":" + DropDownList69.SelectedItem.Text.ToString() + " " + DropDownList70.SelectedItem.Text.ToString().Trim();

                if (DropDownList71.SelectedValue != "" && DropDownList72.SelectedValue != "---" && DropDownList73.SelectedValue != "---")
                    Horades = DropDownList71.SelectedItem.Text.ToString() + ":" + DropDownList72.SelectedItem.Text.ToString() + " " + DropDownList73.SelectedItem.Text.ToString().Trim();

                //valida si la fecha finalizacion es o no NULL
                if (Sanitizer.GetSafeHtmlFragment(txbFechaFinEvento.Text) == "")
                    FechaFin = "Null";
                else
                    FechaFin = "CONVERT(datetime,'" + Sanitizer.GetSafeHtmlFragment(txbFechaFinEvento.Text.Trim()) + " 12:00:00:000" + "', 120)";
                //Valida si el responsable es o no funcionario

                if (DropDownList27.SelectedValue != "1")
                    NombreResponsable = "'" + Sanitizer.GetSafeHtmlFragment(TextBox51.Text.Trim()) + "'";
                else
                    NombreResponsable = "Null";

                try
                {
                    //Camilo
                    cEvento.ModificaEventoTab1(ddlEmpresa.SelectedValue.ToString().Trim(), DropDownList1.SelectedValue.ToString().Trim(), DropDownList2.SelectedValue.ToString().Trim(),
                        DropDownList3.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(), DropDownList5.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txbDetalleUbicacion.Text),
                        Sanitizer.GetSafeHtmlFragment(txbDescripcionEventoEv.Text), DropDownList24.SelectedValue.ToString().Trim(), DropDownList25.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txbFechaIniEvento.Text.Trim()) + " 12:00:00:000",
                        Horaini, FechaFin, Horafin, TextBox49.Text.Trim() + " 12:00:00:000", Horades, DropDownList26.SelectedValue.ToString().Trim(),
                        DropDownList27.SelectedValue.ToString().Trim(), lblIdDependencia4.Text, Sanitizer.GetSafeHtmlFragment(TextBox52.Text), lblCodigoEvento.Text.Trim(), NombreResponsable);

                    loadGridEventos();
                    loadInfoEventos();

                    cargardatosevento();
                    //trRiesgosEventos.Visible = true;
                    //trAdjComPlaAcci.Visible = true;

                    //agregarComentarioEvento();
                    loadGridEventos();
                    loadInfoEventos();
                    omb.ShowMessage("Evento actualizado correctamente",3,"Atención");

                    if (TreeView4.SelectedNode != null)
                        TreeView4.SelectedNode.Selected = false;
                }
                catch (Exception a)
                {
                    omb.ShowMessage("Error al actualizar Evento." + a.Message,2,"Atención");
                }
        }

        protected void ImageButton20_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                
                    cEvento.modificarEvento(DropDownList67.SelectedValue.ToString(), DropDownList9.SelectedValue.ToString(), DropDownList10.SelectedValue.ToString(), 
                        DropDownList6.SelectedValue.ToString(), DropDownList11.SelectedValue.ToString(), lblIdDependencia1.Text, DropDownList33.SelectedValue.ToString(), 
                        DropDownList34.SelectedValue.ToString(), DropDownList8.SelectedValue.ToString(), DropDownList15.SelectedValue.ToString(), DropDownList16.SelectedValue.ToString(), 
                        TextBox53.Text.Trim(), lblCodigoEvento.Text, DropDownList23.SelectedItem.Value, DropDownList29.SelectedItem.Value, TextBox17.Text.Trim());
                    if (lblIdDependencia1.Text != "" && string.IsNullOrEmpty(lblExisteResponsableNotificacion.Text))
                        boolEnviarNotificacion(9, Convert.ToInt16("0"), Convert.ToInt16(lblIdDependencia1.Text.Trim()), "", "Ha sido asignado como responsable del Evento: " + Label55.Text.Trim() + ", en la aplicación de Sherlock para la Gestión de Riesgos y Control Interno.<br /><br />");

                    loadGridEventos();
                    loadInfoEventos();
                    omb.ShowMessage("Evento actualizado correctamente",3,"Atención");
                    /*cEvento.modificarEvento(DropDownList67.SelectedValue.ToString().Trim(), DropDownList9.SelectedValue.ToString().Trim(), DropDownList10.SelectedValue.ToString().Trim(), DropDownList6.SelectedValue.ToString().Trim(), DropDownList11.SelectedValue.ToString().Trim(), lblIdDependencia1.Text, DropDownList33.SelectedValue.ToString().Trim(), DropDownList34.SelectedValue.ToString().Trim(), DropDownList8.SelectedValue.ToString().Trim(), DropDownList15.SelectedValue.ToString().Trim(), DropDownList16.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox53.Text.Trim()), Label55.Text, DropDownList23.SelectedItem.Value, DropDownList29.SelectedItem.Value, Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim()));
                    if (lblIdDependencia1.Text != "")
                        boolEnviarNotificacion(9, Convert.ToInt16("0"), Convert.ToInt16(lblIdDependencia1.Text.Trim()), "", "Ha sido asignado como responsable del Evento: " + Label55.Text.Trim() + ", en la aplicación de Sherlock para la Gestión de Riesgos y Control Interno.<br /><br />");

                    loadGridEventos();
                    loadInfoEventos();
                    omb.ShowMessage("Evento actualizado correctamente",2,"Atención");*/

                    if (TreeViewlast.SelectedNode != null)
                        TreeViewlast.SelectedNode.Selected = false;
                
            }
            catch (Exception a)
            {
                omb.ShowMessage("Error al actualizar evento." + a.Message,2,"Atención");
            }
        }

        protected void ImageButton21_Click(object sender, ImageClickEventArgs e)
        {
            TbConEventos.Visible = false;
            //RadioButtonList1.ClearSelection();
            reseteartodo();
            TabContainerEventos.Tabs[1].Visible = false;
            //TabContainerEventos.Tabs[2].Visible = false;
        }

        protected void GVeventos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridEventos = e.NewPageIndex;
            GVeventos.PageIndex = PagIndexInfoGridEventos;
            GVeventos.DataSource = InfoGridEventos;
            GVeventos.DataBind();
        }

        protected void ImgCleanEvento_Click(object sender, ImageClickEventArgs e)
        {
            txbCodEvento.Text = "";
            txbDescripcionEvento.Text = "";
            ddlCadenaValor.SelectedIndex = 0;
            ddlMacroprocesoEventos.Items.Clear();
            ddlMacroprocesoEventos.Items.Insert(0, new ListItem("---", "---"));
            ddlProcesoEvento.Items.Clear();
            ddlProcesoEvento.Items.Insert(0, new ListItem("---", "---"));
            ddlSubproceso.Items.Clear();
            ddlSubproceso.Items.Insert(0, new ListItem("---", "---"));
            TbConEventos.Visible = false;
            GVeventos.DataSource = null;
            GVeventos.DataBind();
        }

        /*private void agregarComentarioEvento()
{
   cEvento.agregarComentarioEvento(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.ToString().Trim()), InfoGridEventos.Rows[RowGridEventos]["IdEvento"].ToString().Trim());
}*/
        protected void TreeViewlast_SelectedNodeChanged(object sender, EventArgs e)
        {
            TextBox34.Text = TreeViewlast.SelectedNode.Text;
            lblIdDependencia1.Text = TreeViewlast.SelectedNode.Value;

        }
    }
}