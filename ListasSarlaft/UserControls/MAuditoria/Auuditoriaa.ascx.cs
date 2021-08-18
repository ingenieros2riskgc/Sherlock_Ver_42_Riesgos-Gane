using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using ListasSarlaft.Classes;
using ListasSarlaft.Classes.DTO.Auditoria;
using ListasSarlaft.Classes.DAL;
using Microsoft.Security.Application;
using ListasSarlaft.Classes.DAL.Auditoria;

namespace ListasSarlaft.UserControls.MAuditoria
{
    public partial class Auuditoriaa : System.Web.UI.UserControl
    {
        string IdFormulario = "3003";
        cCuenta cCuenta = new cCuenta();
        cAuditoria cAu = new cAuditoria();
        cRiesgo cRiesgo = new cRiesgo();

        private static int LastInsertIdCE;
        private static int LastInsertId;

        #region Properties

        private DataTable infoGrid;
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
        private DataTable infoGridReaud;
        private DataTable InfoGridReaud
        {
            get
            {
                infoGridReaud = (DataTable)ViewState["infoGridReaud"];
                return infoGridReaud;
            }
            set
            {
                infoGridReaud = value;
                ViewState["infoGridReaud"] = infoGridReaud;
            }
        }

        private int rowGridReaud;
        private int RowGridReaud
        {
            get
            {
                rowGridReaud = (int)ViewState["rowGridReaud"];
                return rowGridReaud;
            }
            set
            {
                rowGridReaud = value;
                ViewState["rowGridReaud"] = rowGridReaud;
            }
        }

        private int pagIndexReaud;
        private int PagIndexReaud
        {
            get
            {
                pagIndexReaud = (int)ViewState["pagIndexReaud"];
                return pagIndexReaud;
            }
            set
            {
                pagIndexReaud = value;
                ViewState["pagIndexReaud"] = pagIndexReaud;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ImbSearchFiltro);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    SqlDataSource8.SelectParameters["IdArea"].DefaultValue = Session["IdAreaUser"].ToString();
                    if (Request.QueryString["IdAuditoria"] != null)
                    {
                        LidAuditoria.Text = Request.QueryString["IdAuditoria"];
                        txtCodAuditoriaGen.Text = Request.QueryString["IdAuditoria"];
                        txtCodPlaneacion.Text = Request.QueryString["IdAuditoria"];
                        TXIdPlaneacionCNC.Text = Request.QueryString["IdPlaneacion"];
                        TXIdPlaneacionCNC.Visible = true;
                        txtCodPlaneacion.Visible = false;
                        txtCodEstandarObj.Text = Request.QueryString["IdEstandar"];
                        txtNomPlaneacion.Text = Request.QueryString["planeacion"];
                        GridView7.Visible = true;

                        /*txtNomPlaneacion.Text = GridView8.SelectedRow.Cells[1].Text.Trim();
                        txtCodPlaneacion.Text = GridView8.SelectedRow.Cells[0].Text.Trim();*/
                        txbCodigoFiltro.Text = string.Empty;
                        txbTemaFiltro.Text = string.Empty;
                        GridView1.DataSource = SqlDStemas;
                        GridView1.DataBind();
                        TRfiltros.Visible = true;
                    }
                    else
                    {
                        PagIndex = 0;
                        txtCodAuditoria.Text = "";
                        txtCodObjetivo.Text = "";
                        TreeNodeCollection nodes = this.TreeView1.Nodes;

                        if (nodes.Count <= 0)
                            PopulateTreeView();

                        if (Page.PreviousPage != null)
                        {
                            Control placeHolder = Page.PreviousPage.Controls[0].FindControl("ContentPlaceHolder1");
                            Control usercontrol = placeHolder.FindControl("Planeacion");
                            TextBox txtIdPlaneacion = (TextBox)usercontrol.FindControl("txtId");
                            TextBox txtNombrePlaneacion = (TextBox)usercontrol.FindControl("txtNombre");

                            if (txtIdPlaneacion != null)
                            {
                                txtCodPlaneacion.Text = Sanitizer.GetSafeHtmlFragment(txtIdPlaneacion.Text);
                                txtNomPlaneacion.Text = Sanitizer.GetSafeHtmlFragment(txtNombrePlaneacion.Text);
                                txbCodigoFiltro.Text = string.Empty;
                                txbTemaFiltro.Text = string.Empty;
                                GridView1.DataSource = SqlDStemas;
                                GridView1.DataBind();
                                TRfiltros.Visible = true;
                            }
                        }
                        else
                            txtCodPlaneacion.Text = "0";

                        GridView1.DataBind();
                        TabContainer2.ActiveTabIndex = 0;
                        GridView4.DataBind();
                        ddlNaturaleza.DataBind();
                        mtdLoadDDLEmpresa();

                    }
                }
            }

        }
        #region Loads
        private void mtdLoadDDLEmpresa()
        {
            DataTable dtInfo = new DataTable();
            cRiesgo cRiesgo = new cRiesgo();

            try
            {
                dtInfo = cRiesgo.mtdLoadEmpresa(true);
                ddlEmpresa.Items.Insert(0, new ListItem("---", "0"));
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlEmpresa.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["Descripcion"].ToString().Trim(), dtInfo.Rows[i]["IdEmpresa"].ToString()));
                }
                //ddlEmpresa.DataBind();
            }
            catch (Exception ex)
            {
                //Mensaje("Error al cargar Empresas. " + ex.Message);
                omb.ShowMessage("Error al cargar Empresas.", 2, "Atención");
            }
        }
        #endregion

        #region Treeview
        /// <summary>
        /// Get the data from the database and create the top-level
        /// TreeView items
        /// </summary>
        private void PopulateTreeView()
        {
            DataTable treeViewData = GetTreeViewData();
            AddTopTreeViewNodes(treeViewData);
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
        private void AddTopTreeViewNodes(DataTable treeViewData)
        {
            DataView view = new DataView(treeViewData);
            view.RowFilter = "IdPadre = -1";
            foreach (DataRowView row in view)
            {
                TreeNode newNode = new TreeNode(row["NombreHijo"].ToString().Trim(), row["IdHijo"].ToString());
                TreeView1.Nodes.Add(newNode);
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
                parentTreeViewNode.ChildNodes.Add(newNode);
                AddChildTreeViewNodes(treeViewData, newNode);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            txtDependencia.Text = TreeView1.SelectedNode.Text;
            lblIdDependencia.Text = TreeView1.SelectedNode.Value;
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
            string selectCommand = "SELECT IdHijo,IdPadre,NombreHijoAuditoria FROM [Auditoria].[JerarquiaGrupoAuditoria] WHERE idGrupoAuditoria = " + ddlGrupoAud.SelectedValue;
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

        #endregion Treeview

        #region CleanForm
        public void mtdCleanRegistroAud()
        {
            txtTemaCodigo.Text = string.Empty;
            txtTemaTema.Text = string.Empty;
            txtTemaFechaIni.Text = string.Empty;
            txtTemaFechaCierre.Text = string.Empty;
            txtTemaUser.Text = string.Empty;
            txtTemaFecha.Text = string.Empty;
        }
        #endregion CleanForm
        #region Gridview
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView1.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                /*if(Session["Eliminar"] != null)
                {*/
                try
                {
                    txtCodAuditoria.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                    txtTemaCodigo.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                    txtCodAuditoriaGen.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                    txtTemaTema.Text = GridView1.SelectedRow.Cells[1].Text.Trim();
                    string FI = GridView1.SelectedRow.Cells[5].Text.Trim();
                    txtTemaFechaIni.Text = Convert.ToDateTime(FI).ToShortDateString();
                    string FF = GridView1.SelectedRow.Cells[6].Text.Trim();
                    txtTemaFechaCierre.Text = Convert.ToDateTime(FF).ToShortDateString();

                    txtTemaUser.Text = GridView1.SelectedRow.Cells[3].Text.Trim(); //Aca va el codigo de usuario logueado
                    string F = GridView1.SelectedRow.Cells[4].Text.Trim();
                    txtTemaFecha.Text = Convert.ToDateTime(F).ToShortDateString();


                    TabContainer2.ActiveTabIndex = 0;
                    TabContainer2.Tabs[1].Enabled = false;
                    TabContainer2.Tabs[2].Enabled = false;
                    filaGridOF.Visible = false;
                    filaGridRICC.Visible = false;
                    filaGridObjetivo.Visible = false;
                    filaDetalleOF.Visible = false;
                    filaDetalleRICC.Visible = false;
                    filaDetalleObjetivo.Visible = false;
                    filaDetalleObjEnfoque.Visible = false;
                    FilaGridObjEnfoque.Visible = false;
                    FilaGridObjetivoSE.Visible = false;
                    FilaGridObjRecursos.Visible = false;
                    filaBtnTemas.Visible = false;
                    filaDetalleAuditoria.Visible = false;
                    filaAuditoria.Visible = false;
                    filaDetalle.Visible = false;
                    FormTemaAud.Visible = true;
                } catch (Exception ex)
                {
                    omb.ShowMessage("Error en la captura de los datos a eliminar: " + ex.Message, 2, "Atención");
                }

                /*}else
                {
                    txtCodAuditoria.Text = GridView1.SelectedRow.Cells[0].Text.Trim();
                }*/

            }
        }
        protected void GridView7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridView7.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                txtCodAuditoria.Text = GridView7.SelectedRow.Cells[0].Text;
                txtCodAuditoriaGen.Text = GridView7.SelectedRow.Cells[0].Text;
                txtCodEstandarObj.Text = GridView7.SelectedDataKey.Values[1].ToString();
                txtAuditoriaObj.Text = GridView7.SelectedRow.Cells[1].Text;
                txtAuditoriaRICC.Text = GridView7.SelectedRow.Cells[1].Text;
                txtNomAuditoria.Text = GridView7.SelectedRow.Cells[1].Text;
                txtCodObjetivo.Text = "0";
                txtObjetivoEnf.Text = "0";
                ddlNaturaleza.DataBind();
                txtObjetivo.Text = GridView7.SelectedDataKey[6].ToString();
                txtRecursos.Text = GridView7.SelectedDataKey[5].ToString();
                txtAlcance.Text = GridView7.SelectedDataKey[7].ToString();
                txtFecIniA.Text = GridView7.SelectedDataKey[10].ToString();
                txtFecFinA.Text = GridView7.SelectedDataKey[11].ToString();
                ddlNaturaleza.SelectedValue = GridView7.SelectedDataKey[9].ToString();
                ddlNivelImportancia.SelectedValue = GridView7.SelectedDataKey[8].ToString();
                ddlEmpresa.SelectedValue = GridView7.SelectedDataKey[12].ToString();
                ddlEstandar.SelectedValue = GridView7.SelectedDataKey[1].ToString();
                ddlTipo.SelectedValue = GridView7.SelectedRow.Cells[5].Text;
                ddlMacroProceso.SelectedValue = null;
                ddlProceso.SelectedValue = null;

                if (GridView7.SelectedRow.Cells[5].Text == "Procesos")
                {
                    txtProceso.Text = GridView7.SelectedDataKey[4].ToString();
                    lblIdProceso.Text = GridView7.SelectedDataKey[3].ToString();
                    lblIdDependencia.Text = "";
                    txtDependencia.Text = "";
                    filaProceso.Visible = true;
                    filaDependencia.Visible = false;
                }
                else if (GridView7.SelectedRow.Cells[5].Text == "Dependencia")
                {
                    txtDependencia.Text = GridView7.SelectedDataKey[4].ToString();
                    lblIdDependencia.Text = GridView7.SelectedDataKey[2].ToString();
                    lblIdProceso.Text = "";
                    txtProceso.Text = "";
                    filaProceso.Visible = false;
                    filaDependencia.Visible = true;
                }
                txtUsuario.Text = GridView7.SelectedDataKey[0].ToString(); //Aca va el codigo de usuario logueado
                txtFecha.Text = GridView7.SelectedRow.Cells[11].Text;

                txtPlaneacionObj.Text = txtNomPlaneacion.Text;
                txtPlaneacionRICC.Text = txtNomPlaneacion.Text;
                txtEstandarObj.Text = GridView7.SelectedRow.Cells[3].Text;
                txtEstandarRICC.Text = GridView7.SelectedRow.Cells[3].Text;
                ddlObjetivo.DataBind();
                ddlCC.DataBind();
                ddlCiclo.DataBind();
                ddlCicloOF.DataBind();
                ddlRI.DataBind();
                ddlOF.DataBind();
                ddlGrupoAud.DataBind();
                if (ddlEstandar.SelectedValue != "")
                    ddlGrupoAud2.DataBind();

                TabContainer2.ActiveTabIndex = 0;
                TabContainer2.Tabs[1].Enabled = true;
                TabContainer2.Tabs[2].Enabled = true;
                filaGridOF.Visible = true;
                filaGridRICC.Visible = true;
                filaGridObjetivo.Visible = true;
                filaDetalleOF.Visible = false;
                filaDetalleRICC.Visible = false;
                filaDetalleObjetivo.Visible = false;
                filaDetalleObjEnfoque.Visible = false;
                FilaGridObjEnfoque.Visible = false;
                FilaGridObjetivoSE.Visible = true;
                FilaGridObjRecursos.Visible = false;
                filaBtnTemas.Visible = true;
                filaDetalleAuditoria.Visible = true;
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTipoUA.Text = "1";

            if (GridView2.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                ddlCiclo.SelectedValue = GridView2.SelectedDataKey[1].ToString().Trim();
                ddlRI.SelectedValue = GridView2.SelectedDataKey[2].ToString().Trim();
                ddlCC.SelectedValue = GridView2.SelectedDataKey[3].ToString().Trim();
                txtUsuarioRICC.Text = GridView2.SelectedDataKey[4].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionRICC.Text = GridView2.SelectedRow.Cells[7].Text.Trim();
            }
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string t;
            ddlObjetivo.DataBind();

            txtAlcanceObj.Text = GridView3.SelectedDataKey[1].ToString().Trim();
            txtCodObjetivo.Text = GridView3.SelectedDataKey[0].ToString().Trim();
            ddlObjetivo.SelectedValue = GridView3.SelectedDataKey[0].ToString().Trim();
            txtUsuarioObj.Text = GridView3.SelectedDataKey[2].ToString().Trim();
            txtFechaCreacionObj.Text = GridView3.SelectedRow.Cells[5].Text.Trim();
            t = GridView3.SelectedDataKey[3].ToString().Trim();

            ddlEnfoque.DataBind();

            GridView4.DataSource = SqlDataSource15;
            GridView4.DataBind();
        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTipoUA.Text = "4";

            if (GridView4.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                ddlEnfoque.DataBind();
                txtObjetivoEnf.Text = ddlObjetivo.SelectedItem.Text;
                ddlEnfoque.SelectedValue = GridView4.SelectedDataKey[2].ToString().Trim();
                txtUsuarioObjEnf.Text = GridView4.SelectedDataKey[4].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionObjEnf.Text = GridView4.SelectedRow.Cells[5].Text.Trim();
            }
        }

        protected void GridView5_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTipoUA.Text = "5";

            if (GridView5.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                lblCodNodoGA.Text = GridView5.SelectedDataKey[3].ToString().Trim();
                txtRecurso.Text = GridView5.SelectedRow.Cells[5].Text.Trim();
                txtFecIniRec.Text = GridView5.SelectedRow.Cells[8].Text.Trim();
                txtFecFinRec.Text = GridView5.SelectedRow.Cells[9].Text.Trim();
                txtHorasPlan.Text = GridView5.SelectedRow.Cells[10].Text.Trim();
                ddlEtapa.SelectedValue = GridView5.SelectedRow.Cells[7].Text.Trim();
                txtUsuarioRec.Text = GridView5.SelectedDataKey[5].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionRec.Text = GridView5.SelectedRow.Cells[11].Text.Trim();
                GridView6.DataBind();
                DiasHorasLaborables();
                imgMR.Visible = true;
            }
        }

        protected void GridView8_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNomPlaneacion.Text = GridView8.SelectedRow.Cells[1].Text.Trim();
            txtCodPlaneacion.Text = GridView8.SelectedRow.Cells[0].Text.Trim();
            txbCodigoFiltro.Text = string.Empty;
            txbTemaFiltro.Text = string.Empty;
            SqlDStemas.SelectParameters["IdArea"].DefaultValue = Session["IdAreaUser"].ToString();
            GridView1.DataSource = SqlDStemas;
            GridView1.DataBind();
            TRfiltros.Visible = true;
            //TextBox1.Text = GridView1.SelectedRow.Cells(0).Text
            //txtNombre.Text = GridView1.SelectedRow.Cells(1).Text
            //txtMod.Text = GridView1.SelectedRow.Cells(2).Text
        }

        protected void GridView12_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTipoUA.Text = "2";

            if (GridView12.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                ddlOF.SelectedValue = GridView12.SelectedDataKey[1].ToString().Trim();
                ddlCicloOF.SelectedValue = GridView12.SelectedDataKey[3].ToString().Trim();
                txtUsuarioOF.Text = GridView12.SelectedDataKey[2].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionOF.Text = GridView12.SelectedRow.Cells[5].Text.Trim();
                ddlCicloOF.Enabled = false;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Eliminar")
            {
                try
                {
                    Session["Eliminar"] = 1;
                    ImgBSaveTema.Visible = false;
                    ImgBUpdateTema.Visible = true;
                    filaAuditoria.Visible = false;
                    filaDetalle.Visible = true;
                    txtNomAuditoria.Focus();
                } catch (Exception ex)
                {
                    omb.ShowMessage("Error: " + ex.Message, 2, "Atención");
                }

            }
            if (e.CommandName.ToString() == "Add")
            {
                try
                {
                    ImgBSaveTema.Visible = false;
                    ImgBUpdateTema.Visible = false;
                    filaAuditoria.Visible = false;
                    filaDetalle.Visible = false;
                    txtNomAuditoria.Focus();
                    int RowGrid = Convert.ToInt16(e.CommandArgument);
                    GridViewRow row = GridView1.Rows[RowGrid];
                    //var colsNoVisible = GridView1.DataKeys[RowGrid].Values;
                    Session["IdRegistroAuditoria"] = row.Cells[0].Text;
                    TRfiltroAud.Visible = true;
                    lblIdRegistroAuditoria.Text = row.Cells[0].Text;
                    lblNombreAud.Text = row.Cells[1].Text;
                    GridRegistrosAud.Visible = true;
                    string strErrMsg = string.Empty;
                    int IdArea = Convert.ToInt32(Session["IdAreaUser"].ToString());
                    if (!mtdLoadRegistrosAuditoria(ref strErrMsg, IdArea))
                        omb.ShowMessage(strErrMsg, 2, "Atención");
                } catch (Exception ex)
                {
                    omb.ShowMessage("Error: " + ex.Message, 2, "Atención");
                }

            }
        }
        private bool mtdLoadRegistrosAuditoria(ref string strErrMsg, int idArea)
        {
            #region Vars
            bool booResult = false;
            clsRegistroAuditoriaDTO objRegistroAuditoria = new clsRegistroAuditoriaDTO();
            List<clsRegistroAuditoriaDTO> lstRegistros = new List<clsRegistroAuditoriaDTO>();
            clsRegistroAuditoriaBLL cBllRegistros = new clsRegistroAuditoriaBLL();
            int IdAuditoria = Convert.ToInt32(Session["IdRegistroAuditoria"].ToString());
            #endregion Vars
            int CodAuditoria = 0;
            if (txbCodigoAudFiltro.Text != "")
                CodAuditoria = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txbCodigoAudFiltro.Text));
            string NombreAud = string.Empty;
            if (txbTemaAudFiltro.Text != "")
                NombreAud = Sanitizer.GetSafeHtmlFragment(txbTemaAudFiltro.Text);
            int IdEstandar = 0;
            if (ddlEstandarAud.SelectedValue != "0" && ddlEstandarAud.SelectedValue != "")
                IdEstandar = Convert.ToInt32(ddlEstandarAud.SelectedValue);
            lstRegistros = cBllRegistros.mtdConsultarRegistrosAuditoria(ref strErrMsg, ref lstRegistros, IdAuditoria, CodAuditoria, NombreAud, IdEstandar, idArea);

            if (lstRegistros != null)
            {
                mtdLoadRegistrosAuditoria();
                mtdLoadRegistrosAuditoria(lstRegistros);
                GVregistrosAuditoria.DataSource = lstRegistros;
                GVregistrosAuditoria.PageIndex = pagIndexReaud;
                GVregistrosAuditoria.DataBind();
                GVregistrosAuditoria.Visible = true;
                booResult = true;
            } else
            {
                GVregistrosAuditoria.DataSource = null;
                GVregistrosAuditoria.DataBind();
            }

            return booResult;
        }

        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadRegistrosAuditoria()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("strCodigoAuditoria", typeof(string));
            grid.Columns.Add("intIdAuditoria", typeof(string));
            grid.Columns.Add("strTema", typeof(string));
            grid.Columns.Add("intIdEstandar", typeof(string));
            grid.Columns.Add("strEstandar", typeof(string));
            grid.Columns.Add("intIdPlaneacion", typeof(string));
            grid.Columns.Add("strTipo", typeof(string));
            grid.Columns.Add("intIdDependencia", typeof(string));
            grid.Columns.Add("intIdProceso", typeof(string));
            grid.Columns.Add("strNombreDP", typeof(string));
            grid.Columns.Add("strFechaRegistro", typeof(string));
            grid.Columns.Add("intIdUsuario", typeof(string));
            grid.Columns.Add("strUser", typeof(string));
            grid.Columns.Add("intIdEmpresa", typeof(string));
            grid.Columns.Add("strRecursos", typeof(string));
            grid.Columns.Add("strObjetivo", typeof(string));
            grid.Columns.Add("strAlcance", typeof(string));
            grid.Columns.Add("strNivelImportancia ", typeof(string));
            grid.Columns.Add("intIdDetalle_TipoNaturaleza", typeof(string));
            grid.Columns.Add("strFechaInicio", typeof(string));
            grid.Columns.Add("strFechaCierre", typeof(string));
            grid.Columns.Add("intIdMesExe", typeof(string));
            grid.Columns.Add("strSemanaExe", typeof(string));
            grid.Columns.Add("intPeriodicidad", typeof(string));

            GVregistrosAuditoria.DataSource = grid;
            GVregistrosAuditoria.DataBind();
            InfoGridReaud = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadRegistrosAuditoria(List<clsRegistroAuditoriaDTO> lstControl)
        {
            string strErrMsg = String.Empty;

            foreach (clsRegistroAuditoriaDTO objReau in lstControl)
            {

                InfoGridReaud.Rows.Add(new Object[] {
                    objReau.strCodigoAuditoria.ToString().Trim(),
objReau.intIdAuditoria.ToString().Trim(),
objReau.strTema.ToString().Trim(),
objReau.intIdEstandar.ToString().Trim(),
objReau.strEstandar.ToString().Trim(),
objReau.intIdPlaneacion.ToString().Trim(),
objReau.strTipo.ToString().Trim(),
objReau.intIdDependencia.ToString().Trim(),
objReau.intIdProceso.ToString().Trim(),
objReau.strNombreDP.ToString().Trim(),
objReau.strFechaRegistro.ToString().Trim(),
objReau.intIdUsuario.ToString().Trim(),
objReau.strUser.ToString().Trim(),
objReau.intIdEmpresa.ToString().Trim(),
objReau.strRecursos.ToString().Trim(),
objReau.strObjetivo.ToString().Trim(),
objReau.strAlcance.ToString().Trim(),
objReau.strNivelImportancia.ToString().Trim(),
objReau.intIdDetalle_TipoNaturaleza.ToString().Trim(),
objReau.strFechaInicio.ToString().Trim(),
objReau.strFechaCierre.ToString().Trim(),
objReau.intIdMesExe.ToString().Trim(),
objReau.strSemanaExe.ToString().Trim(),
objReau.intPeriodicidad.ToString().Trim()
                    });
            }
        }
        protected void GridView7_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                btnImgInsertar.Visible = false;
                btnImgActualizar.Visible = true;
                filaAuditoria.Visible = false;
                filaDetalle.Visible = true;
                txtNomAuditoria.Focus();
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int nroPag, tamPag;
            if (e.CommandArgument.ToString() == "Editar")
            {
                ddlCiclo.Enabled = false;
                ddlRI.Enabled = false;
                ddlCC.Enabled = false;
                btnImgInsertarRICC.Visible = false;
                btnImgActualizarRICC.Visible = false;
                filaDetalleRICC.Visible = true;
                filaGridRICC.Visible = false;
                filaDetalleOF.Visible = false;
                filaGridOF.Visible = false;
            }

            if (e.CommandArgument.ToString() == "Eliminar")
            {
                // Convierte el indice de la fila del GridView almacenado en la propiedad CommandArgument a un tipo entero
                int index = Convert.ToInt32((e.CommandArgument).ToString());
                nroPag = GridView1.PageIndex;  // Obtiene el Numero de Pagina en la que se encuentra el GridView
                tamPag = GridView1.PageSize; // Obtiene el Tamano de cada Pagina del GridView

                index = (index - tamPag * nroPag); // Calcula el Numero de Fila del GridView dentro de la pagina actual

                // Recupera la fila que contiene el boton al que se le hizo click por el usuario de la coleccion Rows
                GridViewRow row = GridView1.Rows[index];

                // Obtiene el Id del registro a Eliminar
                Session["IdRiesgoInherente"] = row.Cells[3].Text.Trim();

            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblTipoUA.Text = "3";

            if (e.CommandArgument.ToString() == "Editar")
            {
                ddlObjetivo.Enabled = true;
                txtAlcanceObj.Focus();
                btnImgInsertarObjetivo.Visible = false;
                btnImgActualizarObjetivo.Visible = true;
                filaDetalleObjetivo.Visible = true;
                filaGridObjetivo.Visible = false;
            }

            if (e.CommandArgument.ToString() == "Actividades")
            {
                FilaGridObjEnfoque.Visible = true;
                FilaGridObjRecursos.Visible = false;
            }

            if (e.CommandArgument.ToString() == "Recursos")
            {
                FilaGridObjEnfoque.Visible = false;
                FilaGridObjRecursos.Visible = true;
            }
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                ddlEnfoque.Enabled = false;
                btnImgInsertarObjEnfoque.Visible = false;
                btnImgActualizarObjEnfoque.Visible = false;
                filaGridObjetivo.Visible = false;
                filaDetalleObjEnfoque.Visible = true;
            }

            if (e.CommandArgument.ToString() == "Eliminar")
            {
            }
        }

        protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "Editar")
            {
                btnImgInsertarRecursos.Visible = false;
                btnImgActualizarRecursos.Visible = true;
                filaGridObjetivo.Visible = false;
                filaDetalleRecursos.Visible = true;
                ddlEtapa.Enabled = false;
                imgGrupoRec.Enabled = false;

                filaGridObjetivo.Visible = false;
                filaDetalleRecursos.Visible = true;
            }
        }

        protected void GridView12_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandArgument.ToString() == "Editar")
            {
                ddlOF.Enabled = false;
                btnImgInsertarOF.Visible = false;
                btnImgActualizarOF.Visible = false;
                filaDetalleOF.Visible = true;
                filaDetalleRICC.Visible = false;
                filaGridRICC.Visible = false;
                filaGridOF.Visible = false;
            }

            if (e.CommandArgument.ToString() == "Eliminar")
            {
            }
        }

        protected void GdRecursos_RowsCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = (Convert.ToInt16(GdRecursos.PageSize) * PagIndex) + Convert.ToInt16(e.CommandArgument);

            if (e.CommandName == "Modificar")
            {
                TbAddEtapas.Visible = true;
                TrVerEtapas.Visible = false;
                ImageButton11.Visible = false;
                ImageButton10.Visible = true;
                loadDDLGrupoAuditoria();
                LoadAsignacionRecursos();
            }
            else if (e.CommandName == "Eliminar")
            {
                EliminarAsignacionRecursos();
                omb.ShowMessage("La información se eliminó con éxito en la Base de Datos.", 3, "Atención");
            }
        }

        protected void GdRecursos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GdRecursos.PageIndex = PagIndex;
            GdRecursos.DataSource = InfoGrid;
            GdRecursos.DataBind();
        }
        #endregion Gridview

        #region DDL
        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedItem.Value == "Procesos")
            {
                filaDependencia.Visible = false;
                filaProceso.Visible = true;
            }
            else
            {
                filaDependencia.Visible = true;
                filaProceso.Visible = false;
            }
        }

        protected void ddlMacroProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProceso.Items.Clear();
            ddlProceso.DataBind();
            txtProceso.Text = ddlMacroProceso.SelectedItem.Text;
            lblIdProceso.Text = ddlMacroProceso.SelectedValue;
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProceso.Text = ddlProceso.SelectedItem.Text;
            lblIdProceso.Text = ddlProceso.SelectedValue;

        }

        protected void ddlGrupoAud_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGrupoAud.SelectedValue == "0")
                imgGrupoAud.Enabled = false;
            else
            {
                imgGrupoAud.Enabled = true;
                TVGrupoAud.Nodes.Clear();
                PopulateTreeViewGA();
            }
        }

        protected void ddlObjetivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cAu.mtdConsultarObjetivo(ddlObjetivo.SelectedValue.ToString().Trim(), txtCodAuditoria.Text.Trim());

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    txtCodObjetivo.Text = dtInfo.Rows[0]["IdObjetivo"].ToString().Trim();
                    txtAlcanceObj.Text = dtInfo.Rows[0]["Alcance"].ToString().Trim();
                    txtUsuarioObj.Text = dtInfo.Rows[0]["Usuario"].ToString().Trim();
                    txtFechaCreacionObj.Text = dtInfo.Rows[0]["FechaRegistro"].ToString().Trim();
                }
            }
        }

        protected void ddlObjetivo_DataBound(object sender, EventArgs e)
        {
            ddlObjetivo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlEstandar_DataBound(object sender, EventArgs e)
        {
            ddlEstandar.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio    
        }

        protected void ddlMacroProceso_DataBound(object sender, EventArgs e)
        {
            ddlMacroProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlProceso_DataBound(object sender, EventArgs e)
        {
            ddlProceso.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlCiclo_DataBound(object sender, EventArgs e)
        {
            ddlCiclo.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlRI_DataBound(object sender, EventArgs e)
        {
            ddlRI.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlCC_DataBound(object sender, EventArgs e)
        {
            ddlCC.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlOF_DataBound(object sender, EventArgs e)
        {
            ddlOF.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlEnfoque_DataBound(object sender, EventArgs e)
        {
            ddlEnfoque.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlGrupoAud_DataBound(object sender, EventArgs e)
        {
            ddlGrupoAud.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlCicloOF_DataBound(object sender, EventArgs e)
        {
            ddlCicloOF.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio
        }

        protected void ddlNaturaleza_DataBound(object sender, EventArgs e)
        {
            ddlNaturaleza.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio       
        }

        //03-03-2014 Camilo Aponte
        protected void ddlGrupoAud2_DataBound(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                ddlGrupoAud2.Items.Insert(0, new ListItem("---", "0")); // Inserta el Item con texto Vacio
                //cargar el idgrupoauditoria
                CargarIdGrupoAud();

                if (ddlGrupoAud2.SelectedItem.ToString() != "---")
                {
                    DataTable DtInfo = new DataTable();
                    DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
                    txtCodObjetivo.Text = DtInfo.Rows[0]["IdObjetivo"].ToString();
                    loadGrid();
                    loadInfo();
                    TrVerEtapas.Visible = true;
                }
            }
        }

        #endregion DDL

        #region Buttons
        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalle.Visible = false;
            //filaAuditoria.Visible = true;
            GridRegistrosAud.Visible = true;
            filaBtnTemas.Visible = false;
            filaDetalleAuditoria.Visible = false;
            ddlGrupoAud2.SelectedIndex = 0;
            TrVerEtapas.Visible = false;
            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;
            ddlMesEjecucion.SelectedIndex = 0;
            ddlEspecial.SelectedIndex = 0;
            cblSemanaExe.ClearSelection();
            string strErrMsg = string.Empty;
            int IdArea = Convert.ToInt32(Session["IdAreaUser"].ToString());
            if (!mtdLoadRegistrosAuditoria(ref strErrMsg, IdArea))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        protected void btnImgInsertar_Click(object sender, ImageClickEventArgs e)
        {
            /*if(Session["IdAuditoria"] != null)
            {
                //Inserta el maestro del nodo hijo
                bool err = false;
                try
                {
                    
                    int IdProceso = 0;
                    int IdTipoProceso = 0;
                    if (ddlTipo.SelectedValue == "Procesos" && lblIdProceso.Text == "")
                    {
                        omb.ShowMessage("Por favor seleccione un Proceso.", 2, "Atención");
                        err = true;
                    }
                    else if (ddlTipo.SelectedValue == "Dependencia" && lblIdDependencia.Text == "")
                    {
                        omb.ShowMessage("Por favor seleccione una Dependencia.", 2, "Atención");
                        err = true;
                    }

                    if (err == false)
                    {
                        DataTable DtInfo = new DataTable();
                        if (VerificarCampos())
                        {
                            clsAuditoriaDTO auditoria = new clsAuditoriaDTO();
                            clsAuditoriaBLL auditoriaBLL = new clsAuditoriaBLL();
                            Boolean booResult = false;
                            /*SqlDataSource1.InsertParameters["Tema"].DefaultValue = txtNomAuditoria.Text;
                            SqlDataSource1.InsertParameters["IdEstandar"].DefaultValue = ddlEstandar.SelectedValue.ToString();
                            SqlDataSource1.InsertParameters["IdPlaneacion"].DefaultValue = txtCodPlaneacion.Text;
                            SqlDataSource1.InsertParameters["Tipo"].DefaultValue = ddlTipo.SelectedValue.ToString();
                            auditoria.intIdAuditoria = Convert.ToInt32(Session["IdAuditoria"].ToString());
                            auditoria.strTema = txtNomAuditoria.Text;
                            auditoria.intIdEstandar = Convert.ToInt32(ddlEstandar.SelectedValue.ToString());
                            auditoria.intIdPlaneacion = Convert.ToInt32(txtCodPlaneacion.Text);
                            auditoria.strTipo = ddlTipo.SelectedValue.ToString();
                            //04-04-2014
                            //SqlDataSource1.InsertParameters["Estado"].DefaultValue = "ABIERTA";
                            //SqlDataSource1.InsertParameters["Estado"].DefaultValue = "EJECUCIÓN";
                            auditoria.strEstado = "EJECUCIÓN";
                            if (ddlTipo.SelectedValue == "Procesos")
                            {
                                /*SqlDataSource1.InsertParameters["IdDependencia"].DefaultValue = "0";
                                SqlDataSource1.InsertParameters["IdProceso"].DefaultValue = lblIdProceso.Text;
                                auditoria.intIdDependencia = 0;
                                auditoria.intIdProceso = Convert.ToInt32(lblIdProceso.Text);
                                if (ddlProceso.SelectedValue != "0")
                                {
                                    IdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
                                    IdTipoProceso = 2;
                                }
                                else
                                {
                                    if (ddlMacroProceso.SelectedValue != "0")
                                    {
                                        IdProceso = Convert.ToInt32(ddlMacroProceso.SelectedValue);
                                        IdTipoProceso = 1;
                                    }
                                }
                            }
                            else
                            {
                                /*SqlDataSource1.InsertParameters["IdDependencia"].DefaultValue = "" + lblIdDependencia.Text;
                                SqlDataSource1.InsertParameters["IdProceso"].DefaultValue = "0";
                                auditoria.intIdProceso = 0;
                                auditoria.intIdDependencia = Convert.ToInt32(lblIdDependencia.Text);
                            }
                            /*SqlDataSource1.InsertParameters["IdEmpresa"].DefaultValue = ddlEmpresa.SelectedValue;
                            SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString(); //Aca va el id del Usuario de la BD
                            SqlDataSource1.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            SqlDataSource1.InsertParameters["Recursos"].DefaultValue = txtRecursos.Text;
                            auditoria.intIdEmpresa = Convert.ToInt32(ddlEmpresa.SelectedValue);
                            auditoria.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
                            auditoria.strFechaRegistro = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            auditoria.strRecursos = txtRecursos.Text;

                            /*SqlDataSource1.InsertParameters["Objetivo"].DefaultValue = txtObjetivo.Text;
                            SqlDataSource1.InsertParameters["Alcance"].DefaultValue = txtAlcance.Text;
                            SqlDataSource1.InsertParameters["FechaInicio"].DefaultValue = txtFecIniA.Text;
                            SqlDataSource1.InsertParameters["FechaCierre"].DefaultValue = txtFecFinA.Text;
                            SqlDataSource1.InsertParameters["IdDetalleTipo_TipoNaturaleza"].DefaultValue = ddlNaturaleza.SelectedValue;
                            SqlDataSource1.InsertParameters["NivelImportancia"].DefaultValue = ddlNivelImportancia.SelectedValue;
                            auditoria.strObjetivo = txtObjetivo.Text;
                            auditoria.strAlcance = txtAlcance.Text;
                            auditoria.strFechaInicio = txtFecIniA.Text;
                            auditoria.strFechaCierre = txtFecFinA.Text;
                            auditoria.intIdDetalle_TipoNaturaleza = Convert.ToInt32(ddlNaturaleza.SelectedValue);
                            auditoria.strNivelImportancia = ddlNivelImportancia.SelectedValue;
                            
                            //DataTable DtInfo = new DataTable();
                            //03-03-2014 Camilo Aponte
                            txtCodAuditoriaGen.Text = LastInsertId.ToString().Trim();
                            //DataTable DtInfo = new DataTable();
                            DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
                            if (DtInfo.Rows[0]["IdObjetivo"].ToString() != "")
                            {
                                string strErrMsg = string.Empty;
                                try
                                {
                                    //booResult = auditoriaBLL.mtdInsertarAuditoria(auditoria, ref strErrMsg);
                                    booResult = auditoriaBLL.mtdInsertarAuditoria(auditoria, ref strErrMsg);
                                }
                                catch (Exception ex)
                                {
                                    omb.ShowMessage("Error: " + ex, 2, "Atención");
                                }
                                if (booResult == true)
                                {
                                    int UltimoId = auditoriaBLL.mtdLastIdAuditoria(ref strErrMsg);
                                    booResult = auditoriaBLL.mtdInsertarAuditoriaProceso(UltimoId, IdProceso, IdTipoProceso, ref strErrMsg);
                                    //SqlDataSource1.Insert();

                                    //03-03-2014 Camilo Aponte
                                    //txtCodAuditoriaGen.Text = LastInsertId.ToString();
                                    txtCodAuditoriaGen.Text = UltimoId.ToString();

                                    //DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
                                    txtCodObjetivo.Text = DtInfo.Rows[0]["IdObjetivo"].ToString();
                                    cAu.InseratObjetivoGrupoAuditoria(txtCodAuditoriaGen.Text, txtCodObjetivo.Text, ddlGrupoAud2.SelectedValue.ToString(), System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), Session["idUsuario"].ToString());
                                    cAu.LogHistoricoAudutoria(txtCodAuditoriaGen.Text, "getdate()", "NULL", "NULL", "NO", Session["idUsuario"].ToString());
                                    loadGrid();
                                    loadInfo();
                                    TrVerEtapas.Visible = true;
                                }
                                else
                                {
                                    omb.ShowMessage("Error: No se ha llevado a cabo la inserción, " + strErrMsg, 1, "Atención");
                                    err = true;
                                }
                            }
                            else
                            {
                                omb.ShowMessage("Error: El estandar no tiene asignado un Objetivo", 2, "Atención");
                                err = true;
                            }
                        }
                        if (!err && DtInfo.Rows[0]["IdObjetivo"].ToString() != "")
                        {
                            omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                            TabContainer2.Tabs[1].Enabled = true;
                            TabContainer2.Tabs[2].Enabled = true;
                            btnImgActualizar.Visible = true;
                            btnImgInsertar.Visible = false;

                            txtPlaneacionObj.Text = Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text);
                            txtPlaneacionRICC.Text = Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text);

                            txtCodAuditoriaGen.Text = LastInsertId.ToString().Trim();
                            txtCodAuditoria.Text = LastInsertId.ToString().Trim();
                            txtCodEstandarObj.Text = ddlEstandar.SelectedValue;
                            txtAuditoriaObj.Text = Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text);
                            txtAuditoriaRICC.Text = Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text);
                            txtEstandarObj.Text = ddlEstandar.SelectedItem.Text;
                            txtEstandarRICC.Text = ddlEstandar.SelectedItem.Text;
                            GridView1.DataSource = SqlDStemas;
                            GridView1.DataBind();
                        }
                        //LOG DE HISORICO
                    }
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }
            }
            else
            {*/
            bool err = false;
            int IdProceso = 0;
            int IdTipoProceso = 0;
            if (ddlTipo.SelectedValue == "Procesos" && lblIdProceso.Text == "")
            {
                omb.ShowMessage("Por favor seleccione un Proceso.", 2, "Atención");
                err = true;
            }
            else if (ddlTipo.SelectedValue == "Dependencia" && lblIdDependencia.Text == "")
            {
                omb.ShowMessage("Por favor seleccione una Dependencia.", 2, "Atención");
                err = true;
            }

            if (err == false)
            {
                DataTable DtInfo = new DataTable();
                if (VerificarCampos())
                {
                    //Inserta el maestro del nodo hijo
                    try
                    {
                        clsAuditoriaDTO auditoria = new clsAuditoriaDTO();
                        clsAuditoriaBLL auditoriaBLL = new clsAuditoriaBLL();
                        Boolean booResult = false;
                        /*SqlDataSource1.InsertParameters["Tema"].DefaultValue = txtNomAuditoria.Text;
                        SqlDataSource1.InsertParameters["IdEstandar"].DefaultValue = ddlEstandar.SelectedValue.ToString();
                        SqlDataSource1.InsertParameters["IdPlaneacion"].DefaultValue = txtCodPlaneacion.Text;
                        SqlDataSource1.InsertParameters["Tipo"].DefaultValue = ddlTipo.SelectedValue.ToString();*/
                        auditoria.strCodigoAuditoria = "AUD01";//txtCodigoAuditoria.Text;
                        auditoria.strTema = txtNomAuditoria.Text;
                        auditoria.intIdEstandar = Convert.ToInt32(ddlEstandar.SelectedValue.ToString());
                        auditoria.intIdPlaneacion = Convert.ToInt32(Session["IdRegistroAuditoria"].ToString());
                        auditoria.strTipo = ddlTipo.SelectedValue.ToString();
                        //04-04-2014
                        //SqlDataSource1.InsertParameters["Estado"].DefaultValue = "ABIERTA";
                        //SqlDataSource1.InsertParameters["Estado"].DefaultValue = "EJECUCIÓN";
                        auditoria.strEstado = "EJECUCIÓN";
                        if (ddlTipo.SelectedValue == "Procesos")
                        {
                            /*SqlDataSource1.InsertParameters["IdDependencia"].DefaultValue = "0";
                            SqlDataSource1.InsertParameters["IdProceso"].DefaultValue = lblIdProceso.Text;*/
                            auditoria.intIdDependencia = 0;
                            auditoria.intIdProceso = Convert.ToInt32(lblIdProceso.Text);
                            if (ddlProceso.SelectedValue != "0")
                            {
                                IdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
                                IdTipoProceso = 2;
                            }
                            else
                            {
                                if (ddlMacroProceso.SelectedValue != "0")
                                {
                                    IdProceso = Convert.ToInt32(ddlMacroProceso.SelectedValue);
                                    IdTipoProceso = 1;
                                }
                            }
                            auditoria.intIdTipoProceso = IdTipoProceso;
                        }
                        else
                        {
                            /*SqlDataSource1.InsertParameters["IdDependencia"].DefaultValue = "" + lblIdDependencia.Text;
                            SqlDataSource1.InsertParameters["IdProceso"].DefaultValue = "0";*/
                            auditoria.intIdProceso = 0;
                            auditoria.intIdDependencia = Convert.ToInt32(lblIdDependencia.Text);
                        }
                        /*SqlDataSource1.InsertParameters["IdEmpresa"].DefaultValue = ddlEmpresa.SelectedValue;
                        SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString(); //Aca va el id del Usuario de la BD
                        SqlDataSource1.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        SqlDataSource1.InsertParameters["Recursos"].DefaultValue = txtRecursos.Text;*/
                        auditoria.intIdEmpresa = Convert.ToInt32(ddlEmpresa.SelectedValue);
                        auditoria.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
                        auditoria.strFechaRegistro = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        auditoria.strRecursos = txtRecursos.Text;

                        /*SqlDataSource1.InsertParameters["Objetivo"].DefaultValue = txtObjetivo.Text;
                        SqlDataSource1.InsertParameters["Alcance"].DefaultValue = txtAlcance.Text;
                        SqlDataSource1.InsertParameters["FechaInicio"].DefaultValue = txtFecIniA.Text;
                        SqlDataSource1.InsertParameters["FechaCierre"].DefaultValue = txtFecFinA.Text;
                        SqlDataSource1.InsertParameters["IdDetalleTipo_TipoNaturaleza"].DefaultValue = ddlNaturaleza.SelectedValue;
                        SqlDataSource1.InsertParameters["NivelImportancia"].DefaultValue = ddlNivelImportancia.SelectedValue;*/
                        auditoria.strObjetivo = txtObjetivo.Text;
                        auditoria.strAlcance = txtAlcance.Text;
                        auditoria.strFechaInicio = txtFecIniA.Text;
                        auditoria.strFechaCierre = txtFecFinA.Text;
                        auditoria.intIdDetalle_TipoNaturaleza = Convert.ToInt32(ddlNaturaleza.SelectedValue);
                        auditoria.strNivelImportancia = ddlNivelImportancia.SelectedValue;
                        auditoria.strEspecial = ddlEspecial.SelectedValue;
                        auditoria.intIdMesExe = Convert.ToInt32(ddlMesEjecucion.SelectedValue);
                        auditoria.intPeriodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
                        for (int i = 0; i < cblSemanaExe.Items.Count; i++)
                        {

                            if (cblSemanaExe.Items[i].Selected)
                            {
                                auditoria.strSemanaExe = auditoria.strSemanaExe + cblSemanaExe.Items[i].Text + ",";
                            }
                        }
                        auditoria.intIdArea = Convert.ToInt32(Session["IdAreaUser"].ToString());
                        //DataTable DtInfo = new DataTable();
                        //03-03-2014 Camilo Aponte
                        //txtCodAuditoriaGen.Text = LastInsertId.ToString().Trim();
                        string strErrMsg = string.Empty;
                        try
                        {
                            decimal consecutivo = Convert.ToDecimal(Session["consecutivoAud"].ToString()) + 1;
                            string Area = Session["AreaUser"].ToString();
                            booResult = auditoriaBLL.mtdInsertarAuditoria(auditoria, ref strErrMsg, consecutivo, Area);
                        }
                        catch (Exception ex)
                        {
                            omb.ShowMessage("Error: " + ex, 2, "Atención");
                        }
                        int UltimoId = auditoriaBLL.mtdLastIdAuditoria(ref strErrMsg);
                        if(rblAudCondicion.SelectedValue == "2")
                            mtdRegistrarObjetivoEnfoque(UltimoId);
                        else
                        {
                            DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
                            try
                            {
                                txtCodObjetivo.Text = DtInfo.Rows[0]["IdObjetivo"].ToString();
                                if (cAu.SelectObjetivoGrupoAuditoria(UltimoId.ToString(), txtCodObjetivo.Text))
                                    cAu.InseratObjetivoGrupoAuditoria(UltimoId.ToString(), txtCodObjetivo.Text, ddlGrupoAud2.SelectedValue.ToString(), System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), Session["idUsuario"].ToString());
                                cAu.LogHistoricoAudutoria(UltimoId.ToString(), "getdate()", "NULL", "NULL", "NO", Session["idUsuario"].ToString());
                                loadGrid();
                                loadInfo();
                                TrVerEtapas.Visible = true;
                            }
                            catch (Exception ex)
                            {
                                omb.ShowMessage("Error: " + ex, 2, "Atención");
                                err = true;
                            }
                        }
                        if (booResult == true)
                        {

                            booResult = auditoriaBLL.mtdInsertarAuditoriaProceso(UltimoId, IdProceso, IdTipoProceso, ref strErrMsg);
                            //SqlDataSource1.Insert();

                            //03-03-2014 Camilo Aponte
                            //txtCodAuditoriaGen.Text = LastInsertId.ToString();
                            LastInsertId = UltimoId;
                            txtCodAuditoriaGen.Text = UltimoId.ToString();

                            //DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
                            //txtCodObjetivo.Text = DtInfo.Rows[0]["IdObjetivo"].ToString();

                            cAu.LogHistoricoAudutoria(txtCodAuditoriaGen.Text, "getdate()", "NULL", "NULL", "NO", Session["idUsuario"].ToString());
                            loadGrid();
                            loadInfo();
                            TrVerEtapas.Visible = true;
                            //LOG DE HISORICO
                        }
                        else
                        {
                            omb.ShowMessage("Error: No se ha llevado a cabo la inserción, " + strErrMsg, 1, "Atención");
                            err = true;
                        }
                        
                        /*if (DtInfo.Rows[0]["IdObjetivo"].ToString() != "")
                        {


                        }
                        else
                        {
                            omb.ShowMessage("Error: El estandar no tiene asignado un Objetivo", 2, "Atención");
                            err = true;
                        }*/

                    }
                    catch (Exception except)
                    {
                        // Handle the Exception.
                        omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                        err = true;
                    }
                    //&& DtInfo.Rows[0]["IdObjetivo"].ToString() != ""
                    if (!err )
                    {
                        omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                        TabContainer2.Tabs[1].Enabled = true;
                        TabContainer2.Tabs[2].Enabled = true;
                        btnImgActualizar.Visible = true;
                        btnImgInsertar.Visible = false;

                        txtPlaneacionObj.Text = Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text);
                        txtPlaneacionRICC.Text = Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text);

                        txtCodAuditoriaGen.Text = LastInsertId.ToString().Trim();
                        txtCodAuditoria.Text = LastInsertId.ToString().Trim();
                        txtCodEstandarObj.Text = ddlEstandar.SelectedValue;
                        txtAuditoriaObj.Text = Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text);
                        txtAuditoriaRICC.Text = Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text);
                        txtEstandarObj.Text = ddlEstandar.SelectedItem.Text;
                        txtEstandarRICC.Text = ddlEstandar.SelectedItem.Text;
                        GridView1.DataSource = SqlDStemas;
                        GridView1.DataBind();
                    }
                }
            }
            //}
            GridView1.DataSource = SqlDStemas;
            GridView1.DataBind();
            if (TreeView1.SelectedNode != null)
                TreeView1.SelectedNode.Selected = false;
        }

        public void mtdRegistrarObjetivoEnfoque(int UltimoId)
        {
            #region Crea Objetivo desde Riesgo

            clsAuditoriaObjetivosDTO objObjetivo = new clsAuditoriaObjetivosDTO();
            clsAuditoriaObjetivosDAL bdObjetivo = new clsAuditoriaObjetivosDAL();
            clsAuditoriaEnfoqueDTO objEnfoque = new clsAuditoriaEnfoqueDTO();
            try
            {
                string codigoRiesgo = string.Empty;
                string IdObjetivo = string.Empty;
                foreach (DataRow row in InfoGridRiesgosControlesAdd.Rows)
                {
                    sqlAudObjSelMax.SelectParameters["IdEstandar"].DefaultValue = ddlEstandar.SelectedValue;
                    DataView dvObjetivo = (DataView)this.sqlAudObjSelMax.Select(new DataSourceSelectArguments());
                    objObjetivo.intIdEstandar = Convert.ToInt32(ddlEstandar.SelectedValue);
                    if (dvObjetivo != null)
                        objObjetivo.intNumero = Convert.ToInt32(dvObjetivo[0]["Maximo"].ToString().Trim());
                    else
                        objObjetivo.intNumero = 1;
                    objObjetivo.strNombre = row["CodigoRiesgo"].ToString() + " - " + row["NombreRiesgo"].ToString();
                    objObjetivo.strDescripcion = "Evaluar el Riesgo: " + row["CodigoRiesgo"].ToString() + " " + row["NombreRiesgo"].ToString();
                    objObjetivo.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());

                    DataTable dtInfoObjId = bdObjetivo.mtdGetObjetivoNombre(objObjetivo.strNombre);
                    string IdObjetivoUlt = "";
                    if (dtInfoObjId.Rows.Count == 0)
                    {
                        if (row["CodigoRiesgo"].ToString() != codigoRiesgo)
                            bdObjetivo.mtdInsertAuditoriaObjetivos(objObjetivo);

                        DataView dvObjetivoId = (DataView)this.sqlAudObjSelMaxId.Select(new DataSourceSelectArguments());
                        sqlAudEnfSelMax.SelectParameters["IdObjetivo"].DefaultValue = dvObjetivoId[0]["Maximo"].ToString().Trim();

                        IdObjetivoUlt = dvObjetivoId[0]["Maximo"].ToString().Trim();
                        IdObjetivo = IdObjetivoUlt;
                        cAu.InseratObjetivoGrupoAuditoria(UltimoId.ToString(), dvObjetivoId[0]["Maximo"].ToString().Trim(), ddlGrupoAud2.SelectedValue.ToString(), System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), Session["idUsuario"].ToString());
                    }
                    else
                    {
                        cAu.InseratObjetivoGrupoAuditoria(UltimoId.ToString(), dtInfoObjId.Rows[0]["IdObjetivo"].ToString().Trim(), ddlGrupoAud2.SelectedValue.ToString(), System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), Session["idUsuario"].ToString());
                        IdObjetivoUlt = dtInfoObjId.Rows[0]["IdObjetivo"].ToString().Trim();
                    }


                    objEnfoque.intIdObjetivo = Convert.ToInt32(IdObjetivoUlt);
                    DataView dvEnfoque = (DataView)this.sqlAudEnfSelMax.Select(new DataSourceSelectArguments());
                    if (dvEnfoque != null)
                        objEnfoque.intNumero = Convert.ToInt32(dvEnfoque[0]["Maximo"].ToString().Trim());
                    else
                        objEnfoque.intNumero = 1;
                    objEnfoque.strDescripcion = "Evaluar el Control: " + row["CodigoControl"].ToString() + " - " + row["NombreControl"].ToString();
                    objEnfoque.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());

                    DataTable dtInfoEnfId = bdObjetivo.mtdGetEnfoqueNombre("Evaluar el Control: " + row["CodigoControl"].ToString() + " - " + row["NombreControl"].ToString());
                    if (dtInfoEnfId.Rows.Count == 0)
                    {
                        bdObjetivo.mtdInsertAuditoriaEnfoque(objEnfoque);
                        dtInfoEnfId = bdObjetivo.mtdGetEnfoqueNombre("Evaluar el Control: " + row["CodigoControl"].ToString() + " - " + row["NombreControl"].ToString());
                    }

                    try
                    {
                        
                        SqlDataSource15.InsertParameters["IdAuditoria"].DefaultValue = UltimoId.ToString();
                        SqlDataSource15.InsertParameters["IdObjetivo"].DefaultValue = IdObjetivoUlt;
                        SqlDataSource15.InsertParameters["IdEnfoque"].DefaultValue = dtInfoEnfId.Rows[0]["IdEnfoque"].ToString();
                        SqlDataSource15.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                        SqlDataSource15.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD

                        SqlDataSource15.Insert();

                    }
                    catch (Exception except)
                    {
                        // Handle the Exception.
                        omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                    codigoRiesgo = row["CodigoRiesgo"].ToString();

                    clsAuditoriaRiesgosDAL dbAudRieCon = new clsAuditoriaRiesgosDAL();
                    clsAuditoriaRiesgosControlesDTO objAudRieCon = new clsAuditoriaRiesgosControlesDTO();
                    objAudRieCon.intIdAuditoria = UltimoId;
                    objAudRieCon.intIdRiesgo = Convert.ToInt32(row["IdRiesgo"].ToString());
                    objAudRieCon.intIdControl = Convert.ToInt32(row["IdControl"].ToString());
                    objAudRieCon.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString().Trim());
                    dbAudRieCon.mtdInsertAuditoriaRiesgosControles(objAudRieCon);
                }
                string strErrMsq = string.Empty;
                //bdObjetivo.mtdUpdateAudObjRecurso(UltimoId, Convert.ToInt32(IdObetivo), ref strErrMsq);
                foreach (DataRow row in InfoGridActControlesAdd.Rows)
                {
                    DataTable dtInfoObjId = bdObjetivo.mtdGetEnfoqueNombre(objEnfoque.strDescripcion);
                    string IdEnfoqueUlt = "";
                    IdEnfoqueUlt = dtInfoObjId.Rows[0]["IdEnfoque"].ToString().Trim();
                    
                    // Calcula el número maximo para el consecutivo del literal por enfoque
                    sqlAudDetalleEnfoqueMax.SelectParameters["IdEnfoque"].DefaultValue = Sanitizer.GetSafeHtmlFragment(IdEnfoqueUlt);
                    DataView dvDetJerarquia = (DataView)this.sqlAudDetalleEnfoqueMax.Select(new DataSourceSelectArguments());
                    int Numero = Convert.ToInt32(dvDetJerarquia[0]["Maximo"].ToString().Trim());
                    DataTable dtInfoLiteral = bdObjetivo.mtdGetLiteralDescripcion(row["DescripcionAct"].ToString(), IdEnfoqueUlt);
                    if (dtInfoLiteral.Rows.Count == 0)
                    {
                        try
                        {
                            sqlAudDetalleEnfoqueIns.InsertParameters["Descripcion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(row["DescripcionAct"].ToString());
                            sqlAudDetalleEnfoqueIns.InsertParameters["Numero"].DefaultValue = Numero.ToString();
                            sqlAudDetalleEnfoqueIns.InsertParameters["IdEnfoque"].DefaultValue = IdEnfoqueUlt;
                            sqlAudDetalleEnfoqueIns.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                            sqlAudDetalleEnfoqueIns.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                            sqlAudDetalleEnfoqueIns.Insert();
                        }
                        catch (Exception ex)
                        {
                            omb.ShowMessage("Error en la inserción del Literal." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Mensaje("Error en la creación del objetivo: " + ex.Message);
            }
            #endregion
        }

        protected void btnImgActualizar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                bool err = false;
                if (ddlTipo.SelectedValue == "Procesos" && Sanitizer.GetSafeHtmlFragment(txtProceso.Text) == "")
                {
                    omb.ShowMessage("Por favor seleccione un Proceso.", 2, "Atención");
                    txtProceso.Focus();
                    err = true;
                }
                else if (ddlTipo.SelectedValue == "Dependencia" && Sanitizer.GetSafeHtmlFragment(txtDependencia.Text) == "")
                {
                    omb.ShowMessage("Por favor seleccione una Dependencia.", 2, "Atención");
                    txtProceso.Focus();
                    err = true;
                }

                if (err == false)
                {
                    if (VerificarCampos())
                    {
                        /*if (Session["IdAuditoria"] == null || Session["IdAuditoria"].ToString() == "0")
                        {
                            try
                            {
                                SqlDataSource1.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text);
                                SqlDataSource1.UpdateParameters["Tema"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text);
                                SqlDataSource1.UpdateParameters["IdEstandar"].DefaultValue = ddlEstandar.SelectedValue;
                                SqlDataSource1.UpdateParameters["IdPlaneacion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text);
                                SqlDataSource1.UpdateParameters["Tipo"].DefaultValue = ddlTipo.SelectedValue;
                                if (ddlTipo.SelectedValue == "Procesos")
                                {
                                    SqlDataSource1.UpdateParameters["IdDependencia"].DefaultValue = "";
                                    SqlDataSource1.UpdateParameters["IdProceso"].DefaultValue = lblIdProceso.Text;
                                }
                                else
                                {
                                    SqlDataSource1.UpdateParameters["IdDependencia"].DefaultValue = lblIdDependencia.Text;
                                    SqlDataSource1.UpdateParameters["IdProceso"].DefaultValue = "";
                                }
                                SqlDataSource1.UpdateParameters["Recursos"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtRecursos.Text);
                                SqlDataSource1.UpdateParameters["Objetivo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtObjetivo.Text);
                                SqlDataSource1.UpdateParameters["Alcance"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAlcance.Text);
                                SqlDataSource1.UpdateParameters["FechaInicio"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIniA.Text);
                                SqlDataSource1.UpdateParameters["FechaCierre"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFinA.Text);
                                SqlDataSource1.UpdateParameters["IdDetalleTipo_TipoNaturaleza"].DefaultValue = ddlNaturaleza.SelectedValue;
                                SqlDataSource1.UpdateParameters["NivelImportancia"].DefaultValue = ddlNivelImportancia.SelectedValue;
                                SqlDataSource1.UpdateParameters["IdEmpresa"].DefaultValue = ddlEmpresa.SelectedValue; ;
                                SqlDataSource1.Update();

                                //03-03-2014 Camilo Aponte
                                //txtCodAuditoriaGen.Text = LastInsertId.ToString().Trim();
                                DataTable DtInfo = new DataTable();
                                DtInfo = cAu.mtdVerObjetivoEstandar(ddlEstandar.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text.Trim()));
                                txtCodObjetivo.Text = DtInfo.Rows[0]["IdObjetivo"].ToString().Trim();
                                cAu.ModificarObjetivoGrupoAuditoria(Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text), Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text), ddlGrupoAud2.SelectedValue.ToString());
                                loadGrid();
                                loadInfo();
                                TrVerEtapas.Visible = true;

                            }
                            catch (Exception except)
                            {
                                omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                                err = true;
                            }
                        }
                        else
                        {*/
                            DataTable DtInfo = new DataTable();
                            int IdProceso = 0;
                            int IdTipoProceso = 0;
                            clsRegistroAuditoriaDTO auditoria = new clsRegistroAuditoriaDTO();
                            clsRegistroAuditoriaBLL auditoriaBLL = new clsRegistroAuditoriaBLL();
                            Boolean booResult = false;
                            /*SqlDataSource1.InsertParameters["Tema"].DefaultValue = txtNomAuditoria.Text;
                            SqlDataSource1.InsertParameters["IdEstandar"].DefaultValue = ddlEstandar.SelectedValue.ToString();
                            SqlDataSource1.InsertParameters["IdPlaneacion"].DefaultValue = txtCodPlaneacion.Text;
                            SqlDataSource1.InsertParameters["Tipo"].DefaultValue = ddlTipo.SelectedValue.ToString();*/
                            auditoria.intIdRegistroAuditoria = Convert.ToInt32(txtCodAuditoria.Text);
                            auditoria.intIdAuditoria = Convert.ToInt32(Session["IdAuditoria"].ToString());
                            auditoria.strTema = txtNomAuditoria.Text;
                            auditoria.intIdEstandar = Convert.ToInt32(ddlEstandar.SelectedValue.ToString());
                            auditoria.intIdPlaneacion = Convert.ToInt32(Session["IdRegistroAuditoria"].ToString());
                            auditoria.strTipo = ddlTipo.SelectedValue.ToString();
                            //04-04-2014
                            //SqlDataSource1.InsertParameters["Estado"].DefaultValue = "ABIERTA";
                            //SqlDataSource1.InsertParameters["Estado"].DefaultValue = "EJECUCIÓN";
                            auditoria.strEstado = "EJECUCIÓN";
                            if (ddlTipo.SelectedValue == "Procesos")
                            {
                                /*SqlDataSource1.InsertParameters["IdDependencia"].DefaultValue = "0";
                                SqlDataSource1.InsertParameters["IdProceso"].DefaultValue = lblIdProceso.Text;*/
                                auditoria.intIdDependencia = 0;
                                auditoria.intIdProceso = Convert.ToInt32(lblIdProceso.Text);
                                if (ddlProceso.SelectedValue != "0")
                                {
                                    IdProceso = Convert.ToInt32(ddlProceso.SelectedValue);
                                    IdTipoProceso = 2;
                                }
                                else
                                {
                                    if (ddlMacroProceso.SelectedValue != "0")
                                    {
                                        IdProceso = Convert.ToInt32(ddlMacroProceso.SelectedValue);
                                        IdTipoProceso = 1;
                                    }
                                }
                            }
                            else
                            {
                                /*SqlDataSource1.InsertParameters["IdDependencia"].DefaultValue = "" + lblIdDependencia.Text;
                                SqlDataSource1.InsertParameters["IdProceso"].DefaultValue = "0";*/
                                auditoria.intIdProceso = 0;
                                auditoria.intIdDependencia = Convert.ToInt32(lblIdDependencia.Text);
                            }
                            /*SqlDataSource1.InsertParameters["IdEmpresa"].DefaultValue = ddlEmpresa.SelectedValue;
                            SqlDataSource1.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString(); //Aca va el id del Usuario de la BD
                            SqlDataSource1.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            SqlDataSource1.InsertParameters["Recursos"].DefaultValue = txtRecursos.Text;*/
                            auditoria.intIdEmpresa = Convert.ToInt32(ddlEmpresa.SelectedValue);
                            auditoria.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
                            auditoria.strFechaRegistro = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            auditoria.strRecursos = txtRecursos.Text;

                            /*SqlDataSource1.InsertParameters["Objetivo"].DefaultValue = txtObjetivo.Text;
                            SqlDataSource1.InsertParameters["Alcance"].DefaultValue = txtAlcance.Text;
                            SqlDataSource1.InsertParameters["FechaInicio"].DefaultValue = txtFecIniA.Text;
                            SqlDataSource1.InsertParameters["FechaCierre"].DefaultValue = txtFecFinA.Text;
                            SqlDataSource1.InsertParameters["IdDetalleTipo_TipoNaturaleza"].DefaultValue = ddlNaturaleza.SelectedValue;
                            SqlDataSource1.InsertParameters["NivelImportancia"].DefaultValue = ddlNivelImportancia.SelectedValue;*/
                            auditoria.strObjetivo = txtObjetivo.Text;
                            auditoria.strAlcance = txtAlcance.Text;
                            auditoria.strFechaInicio = txtFecIniA.Text;
                            auditoria.strFechaCierre = txtFecFinA.Text;
                            auditoria.intIdDetalle_TipoNaturaleza = Convert.ToInt32(ddlNaturaleza.SelectedValue);
                            auditoria.strNivelImportancia = ddlNivelImportancia.SelectedValue;
                            auditoria.strEspecial = ddlEspecial.SelectedValue;
                            auditoria.intIdMesExe = Convert.ToInt32(ddlMesEjecucion.SelectedValue);

                            for (int i = 0; i < cblSemanaExe.Items.Count; i++)
                            {

                                if (cblSemanaExe.Items[i].Selected)
                                {
                                    auditoria.strSemanaExe = auditoria.strSemanaExe + cblSemanaExe.Items[i].Text + ",";
                                }
                            }
                        auditoria.intPeriodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
                            //DataTable DtInfo = new DataTable();
                            //03-03-2014 Camilo Aponte
                            txtCodAuditoriaGen.Text = LastInsertId.ToString().Trim();
                            //DataTable DtInfo = new DataTable();
                            DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
                            
                            
                                string strErrMsg = string.Empty;
                                try
                                {
                                    booResult = auditoriaBLL.mtdActualizarRegistroAuditoria(auditoria, ref strErrMsg);
                                }
                                catch (Exception ex)
                                {
                                    omb.ShowMessage("Error: " + ex, 2, "Atención");
                                }
                                if (booResult == true)
                                {
                                    //int UltimoId = auditoriaBLL.mtdLastIdAuditoria(ref strErrMsg);
                                    if(IdProceso>0)
                                        booResult = auditoriaBLL.mtdActualizarAuditoriaProceso(Convert.ToInt32(txtCodAuditoria.Text), IdProceso, IdTipoProceso, ref strErrMsg);
                                    //SqlDataSource1.Insert();

                                    //03-03-2014 Camilo Aponte
                                    //txtCodAuditoriaGen.Text = LastInsertId.ToString();
                                    txtCodAuditoriaGen.Text = txtCodAuditoria.Text;

                                    //DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
                                    
                            //if (DtInfo.Rows[0]["IdObjetivo"].ToString() != "")
                            if (rblAudCondicion.SelectedValue != "2")
                            {
                                
                                try
                                    {
                                    txtCodObjetivo.Text = DtInfo.Rows[0]["IdObjetivo"].ToString();
                                    if (cAu.SelectObjetivoGrupoAuditoria(txtCodAuditoriaGen.Text, txtCodObjetivo.Text))
                                            cAu.InseratObjetivoGrupoAuditoria(txtCodAuditoriaGen.Text, txtCodObjetivo.Text, ddlGrupoAud2.SelectedValue.ToString(), System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), Session["idUsuario"].ToString());
                                        cAu.LogHistoricoAudutoria(txtCodAuditoriaGen.Text, "getdate()", "NULL", "NULL", "NO", Session["idUsuario"].ToString());
                                        loadGrid();
                                        loadInfo();
                                        TrVerEtapas.Visible = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        omb.ShowMessage("Error: " + ex, 2, "Atención");
                                        err = true;
                                    }

                                
                                }
                                else
                                {
                                clsAuditoriaObjetivosDAL dbObjetivos = new clsAuditoriaObjetivosDAL();
                                dbObjetivos.mtdDeleteObjetivoEnfoque(Convert.ToInt32(Session["IdAuditoria"].ToString()), ref strErrMsg);
                                mtdRegistrarObjetivoEnfoque(Convert.ToInt32(Session["IdAuditoria"].ToString()));
                                /* omb.ShowMessage("Error: No se ha llevado a cabo la inserción, " + strErrMsg, 1, "Atención");
                                 err = true;*/
                            }
                            }


                            if (!err)
                            {
                                omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                            GridView1.DataSource = SqlDStemas;
                            GridView1.DataBind();
                        }
                        }
                    //}

                }
                if (TreeView1.SelectedNode != null)
                    TreeView1.SelectedNode.Selected = false;
            }
            GridView1.DataSource = SqlDStemas;
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
                btnImgokEliminar.Visible = true;
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgokEliminar_Click(object sender, EventArgs e)
        {
            bool err = false;

            mpeMsgBox.Hide();

            try
            {
                if (TabContainer2.ActiveTabIndex.ToString() == "0")
                {
                    /*SqlDStemas.DeleteParameters["IdRegistroAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text);
                    SqlDStemas.Delete();*/
                    SqlHistoricoAud.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text);
                    SqlHistoricoAud.Delete();
                    SqlAuditoriaObjDel.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text);
                    SqlAuditoriaObjDel.Delete();
                    SqlAuditoriaRICCDel.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text);
                    SqlAuditoriaRICCDel.Delete();
                    SqlDataSource1.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text);
                    SqlDataSource1.Delete();
                    mtdClean();
                }
                else if (TabContainer2.ActiveTabIndex.ToString() == "1")
                {
                    if (lblTipoUA.Text == "1")
                    {
                        SqlDataSource4.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource4.DeleteParameters["IdCiclo"].DefaultValue = ddlCiclo.SelectedValue;
                        SqlDataSource4.DeleteParameters["IdRiesgoInherente"].DefaultValue = ddlRI.SelectedValue;
                        SqlDataSource4.DeleteParameters["IdCalificacionControl"].DefaultValue = ddlCC.SelectedValue;
                        SqlDataSource4.Delete();
                    }
                    else if (lblTipoUA.Text == "2")
                    {
                        SqlDataSource12.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource12.DeleteParameters["IdOtrosFactores"].DefaultValue = ddlOF.SelectedValue;
                        SqlDataSource12.Delete();
                    }
                }
                else if (TabContainer2.ActiveTabIndex.ToString() == "2")
                {
                    if (lblTipoUA.Text == "3")
                    {
                        SqlDelAudObjEnfoque.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDelAudObjEnfoque.DeleteParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                        SqlDelAudObjEnfoque.Delete();
                        SqlDataSource2.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource2.DeleteParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                        SqlDataSource2.Delete();
                    }
                    else if (lblTipoUA.Text == "4")
                    {
                        SqlDataSource15.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource15.DeleteParameters["IdObjetivo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text);
                        SqlDataSource15.DeleteParameters["IdEnfoque"].DefaultValue = ddlEnfoque.SelectedValue;
                        SqlDataSource15.Delete();
                    }
                    else if (lblTipoUA.Text == "5")
                    {
                        SqlDataSource17.DeleteParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource17.DeleteParameters["IdObjetivo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text);
                        SqlDataSource17.DeleteParameters["IdGrupoAuditoria"].DefaultValue = ddlGrupoAud.SelectedValue;
                        SqlDataSource17.DeleteParameters["IdHijo"].DefaultValue = lblCodNodoGA.Text;
                        SqlDataSource17.DeleteParameters["Etapa"].DefaultValue = ddlEtapa.SelectedValue;
                        SqlDataSource17.Delete();
                    }
                }
            }
            catch (SqlException odbcEx)
            {
                if (odbcEx.Number == 547)
                    omb.ShowMessage("Error en la eliminación de la información. <br/> La información a borrar tiene relacionada con el histórico de auditoría. <br/> Por favor revise la información.", 1, "Atención");
                else
                    omb.ShowMessage("Error en la eliminación de la información." + "<br/>" + "Descripción: " + odbcEx.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }
            catch (Exception except)
            {
                omb.ShowMessage("Error en la eliminación de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
                omb.ShowMessage("La información se eliminó con éxito en la Base de Datos.", 3, "Atención");
            mtdCleanAud();
            GridView3.DataBind();
            GridView2.DataBind();
            GridView12.DataBind();
            filaAuditoria.Visible = true;
            filaDetalle.Visible = false;
            filaBtnTemas.Visible = false;
            filaDetalleAuditoria.Visible = false;
            GridView1.DataSource = SqlDStemas;
            GridView1.DataBind();
            //ddlGrupoAud2.SelectedIndex = 0;
            TrVerEtapas.Visible = false;
            ddlMesEjecucion.SelectedIndex = 0;
            ddlEspecial.SelectedIndex = 0;
            cblSemanaExe.ClearSelection();
        }

        protected void btnImgEliminarObjetivo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                ImageButton btn = (ImageButton)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                string idRegistro = GridView1.DataKeys[row.RowIndex].Value.ToString();
                /*Response.Write("Row Index of Link button: " + row.RowIndex +
                               "DataKey value:" + GridView1.DataKeys[row.RowIndex].Value.ToString());*/
                txtCodAuditoria.Text = idRegistro;
                btnDelTemaAud.Visible = true;
                btnImgokEliminar.Visible = false;
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgCancelarObjetivo_Click(object sender, ImageClickEventArgs e)
        {
            ddlGrupoAud2.SelectedIndex = 0;
            TrVerEtapas.Visible = false;
            filaDetalleObjetivo.Visible = false;
            filaGridObjetivo.Visible = true;
            FilaGridObjEnfoque.Visible = false;
        }

        protected void btnImgActualizarObjetivo_Click(object sender, ImageClickEventArgs e)
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
                        #region Objetivo
                        SqlDataSource2.DeleteParameters["IdAuditoria"].DefaultValue = Session["IdAuditoria"].ToString();
                        SqlDataSource2.DeleteParameters["IdObjetivo"].DefaultValue = txtCodObjetivo.Text;

                        SqlDataSource2.InsertParameters["IdAuditoria"].DefaultValue = Session["IdAuditoria"].ToString(); 
                        SqlDataSource2.InsertParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                        SqlDataSource2.InsertParameters["Alcance"].DefaultValue = txtAlcanceObj.Text;
                        if (ddlGrupoAud.SelectedValue == "0")
                            SqlDataSource2.InsertParameters["IdGrupoAuditoria"].DefaultValue = null;
                        else
                            SqlDataSource2.InsertParameters["IdGrupoAuditoria"].DefaultValue = ddlGrupoAud.SelectedValue;
                        SqlDataSource2.InsertParameters["FechaInicial"].DefaultValue = txtFecIni.Text;
                        SqlDataSource2.InsertParameters["FechaFinal"].DefaultValue = txtFecFin.Text;
                        SqlDataSource2.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim();
                        SqlDataSource2.InsertParameters["FechaRegistro"].DefaultValue = txtFechaCreacionObj.Text;

                        #endregion

                        #region Objetivo - Enfoque
                        SqlDataSource2A.UpdateParameters["IdAuditoria"].DefaultValue = Session["IdAuditoria"].ToString();
                        SqlDataSource2A.UpdateParameters["IdObjetivo"].DefaultValue = txtCodObjetivo.Text;
                        SqlDataSource2A.UpdateParameters["IdObjetivoNuevo"].DefaultValue = ddlObjetivo.SelectedValue;
                        #endregion
                        SqlDataSource2.Delete();
                        SqlDataSource2A.Update();
                        SqlDataSource2.Insert();
                        
                        

                        GridView3.DataBind();
                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaDetalleObjetivo.Visible = false;
                        filaGridObjetivo.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void btnImgInsertarObjetivo_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource2.InsertParameters["IdAuditoria"].DefaultValue = Session["IdAuditoria"].ToString();
                    SqlDataSource2.InsertParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                    SqlDataSource2.InsertParameters["Alcance"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAlcanceObj.Text);
                    if (ddlGrupoAud.SelectedValue == "0")
                        SqlDataSource2.InsertParameters["IdGrupoAuditoria"].DefaultValue = null;
                    else
                        SqlDataSource2.InsertParameters["IdGrupoAuditoria"].DefaultValue = ddlGrupoAud.SelectedValue;
                    SqlDataSource2.InsertParameters["FechaInicial"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIni.Text);
                    SqlDataSource2.InsertParameters["FechaFinal"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFin.Text);
                    SqlDataSource2.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource2.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");

                    txtFecIni.Text = "";
                    txtFecFin.Text = "";
                    SqlDataSource2.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaDetalleObjetivo.Visible = false;
                    filaGridObjetivo.Visible = true;
                    GridView3.DataBind();
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgInsertarRICC_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource4.InsertParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                    SqlDataSource4.InsertParameters["IdCiclo"].DefaultValue = ddlCiclo.SelectedValue;
                    SqlDataSource4.InsertParameters["IdRiesgoInherente"].DefaultValue = ddlRI.SelectedValue;
                    SqlDataSource4.InsertParameters["IdCalificacionControl"].DefaultValue = ddlCC.SelectedValue;
                    SqlDataSource4.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource4.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");

                }catch(Exception ex)
                {
                    omb.ShowMessage("Error en capturar la información." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
                }
                    try
                {
                    SqlDataSource4.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaDetalleRICC.Visible = false;
                    filaGridRICC.Visible = true;
                    filaDetalleOF.Visible = false;
                    filaGridOF.Visible = true;
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgActualizarRICC_Click(object sender, ImageClickEventArgs e)
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
                        SqlDataSource2.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource2.UpdateParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                        SqlDataSource2.UpdateParameters["Alcance"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAlcanceObj.Text);
                        SqlDataSource2.UpdateParameters["FechaInicial"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIni.Text);
                        SqlDataSource2.UpdateParameters["FechaFinal"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFin.Text);

                        SqlDataSource2.Update();

                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaDetalleObjetivo.Visible = false;
                        filaGridObjetivo.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void btnImgCancelarRICC_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleRICC.Visible = false;
            filaGridRICC.Visible = true;
            filaDetalleOF.Visible = false;
            filaGridOF.Visible = true;
        }

        protected void btnImgEliminarRICC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                btnImgokEliminarIRCC.Visible = false;
                btnDelTemaAud.Visible = false;
                btnImgokEliminar.Visible = true;
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnImgEliminarOF_Click(object sender, ImageClickEventArgs e)
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

        protected void btnImgInsertarOF_Click(object sender, ImageClickEventArgs e)
        {
            //Inserta el maestro del nodo hijo
            try
            {
                SqlDataSource12.InsertParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                SqlDataSource12.InsertParameters["IdCiclo"].DefaultValue = ddlCicloOF.SelectedValue;
                SqlDataSource12.InsertParameters["IdOtrosFactores"].DefaultValue = ddlOF.SelectedValue;
                SqlDataSource12.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource12.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");

                SqlDataSource12.Insert();

                omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                filaDetalleRICC.Visible = false;
                filaGridRICC.Visible = true;
                filaDetalleOF.Visible = false;
                filaGridOF.Visible = true;
                mtdCleanUniversioAuditoable();
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
            }
        }

        protected void btnImgActualizarOF_Click(object sender, ImageClickEventArgs e)
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
                    SqlDataSource12.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                    SqlDataSource12.UpdateParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                    SqlDataSource12.UpdateParameters["Alcance"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAlcanceObj.Text);
                    SqlDataSource12.UpdateParameters["FechaInicial"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIni.Text);
                    SqlDataSource12.UpdateParameters["FechaFinal"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFin.Text);

                    SqlDataSource12.Update();

                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                    filaDetalleObjetivo.Visible = false;
                    filaGridObjetivo.Visible = true;
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgCancelarOF_Click(object sender, ImageClickEventArgs e)
        {
            ddlGrupoAud2.SelectedIndex = 0;
            filaDetalleOF.Visible = false;
            filaDetalleRICC.Visible = false;
            filaGridRICC.Visible = true;
            filaGridOF.Visible = true;
        }

        protected void btnTemasAud_Click(object sender, EventArgs e)
        {
            filaAuditoria.Visible = true;
            filaDetalle.Visible = false;
            filaBtnTemas.Visible = false;
            filaDetalleAuditoria.Visible = false;
            GridView1.DataSource = SqlDStemas;
            GridView1.DataBind();
            ddlGrupoAud2.SelectedIndex = 0;
            TrVerEtapas.Visible = false;
            ddlMesEjecucion.SelectedIndex = 0;
            ddlEspecial.SelectedIndex = 0;
            cblSemanaExe.ClearSelection();
        }

        protected void btnImgCancelarObjEnfoque_Click(object sender, ImageClickEventArgs e)
        {
            filaDetalleObjEnfoque.Visible = false;
            filaGridObjetivo.Visible = true;
        }

        protected void btnImgInsertarObjEnfoque_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource15.InsertParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                    SqlDataSource15.InsertParameters["IdObjetivo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text);
                    SqlDataSource15.InsertParameters["IdEnfoque"].DefaultValue = ddlEnfoque.SelectedValue;
                    SqlDataSource15.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource15.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD

                    SqlDataSource15.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaDetalleObjEnfoque.Visible = false;
                    filaGridObjetivo.Visible = true;
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgActualizarObjEnfoque_Click(object sender, ImageClickEventArgs e)
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
                        SqlDataSource12.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource12.UpdateParameters["IdObjetivo"].DefaultValue = ddlObjetivo.SelectedValue;
                        SqlDataSource12.UpdateParameters["Alcance"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtAlcanceObj.Text);
                        SqlDataSource12.UpdateParameters["FechaInicial"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIni.Text);
                        SqlDataSource12.UpdateParameters["FechaFinal"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFin.Text);

                        SqlDataSource12.Update();

                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaDetalleObjetivo.Visible = false;
                        filaGridObjetivo.Visible = true;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void btnImgCancelarRecursos_Click(object sender, ImageClickEventArgs e)
        {
            ddlGrupoAud2.SelectedIndex = 0;
            filaGridObjetivo.Visible = true;
            filaDetalleRecursos.Visible = false;
        }

        protected void btnImgInsertarRecursos_Click(object sender, ImageClickEventArgs e)
        {
            if (VerificarCampos())
            {
                //Inserta el maestro del nodo hijo
                try
                {
                    SqlDataSource17.InsertParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                    SqlDataSource17.InsertParameters["IdObjetivo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text);
                    SqlDataSource17.InsertParameters["IdGrupoAuditoria"].DefaultValue = ddlGrupoAud.SelectedValue;
                    SqlDataSource17.InsertParameters["IdHijo"].DefaultValue = lblCodNodoGA.Text;
                    SqlDataSource17.InsertParameters["Etapa"].DefaultValue = ddlEtapa.SelectedValue;
                    SqlDataSource17.InsertParameters["FechaInicial"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIniRec.Text);
                    SqlDataSource17.InsertParameters["FechaFinal"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFinRec.Text);
                    SqlDataSource17.InsertParameters["HorasPlaneadas"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtHorasPlan.Text);
                    SqlDataSource17.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    SqlDataSource17.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource17.Insert();

                    omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                    filaGridObjetivo.Visible = true;
                    //capo
                    filaDetalleRecursos.Visible = true;
                    GridView5.DataBind();
                }
                catch (Exception except)
                {
                    // Handle the Exception.
                    omb.ShowMessage("Error en la inserción de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                }

                try
                {
                    //enviar la notificacion 
                    string InfAdicional = "Ha sido asignado como responsable de la auditoría:<br /><br />";
                    InfAdicional += "Planeación: " + Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text) + ", " + Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text) + "<br />";
                    InfAdicional += "Auditoría: " + Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text) + ", " + Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text) + "<br />";
                    InfAdicional += "Objetivo: " + Sanitizer.GetSafeHtmlFragment(txtObjetivoRec.Text) + "<br />";
                    InfAdicional += "Fecha Inicio: " + Sanitizer.GetSafeHtmlFragment(txtFecIniRec.Text) + "<br />" + "Fecha Fin: " + Sanitizer.GetSafeHtmlFragment(txtFecFinRec.Text) + "<br />" + "Horas: " + Sanitizer.GetSafeHtmlFragment(txtHorasPlan.Text) + "<br /><br />";
                    boolEnviarNotificacion(15, Convert.ToInt16("0"), Convert.ToInt16(lblCodNodoGA.Text.Trim()), "", InfAdicional);
                }
                catch (Exception ex)
                {
                    omb.ShowMessage("Error al enviar la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
                }
            }
        }

        protected void btnImgActualizarRecursos_Click(object sender, ImageClickEventArgs e)
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
                        SqlDataSource17.UpdateParameters["IdAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text);
                        SqlDataSource17.UpdateParameters["IdObjetivo"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text);
                        SqlDataSource17.UpdateParameters["IdGrupoAuditoria"].DefaultValue = ddlGrupoAud.SelectedValue;
                        SqlDataSource17.UpdateParameters["IdHijo"].DefaultValue = lblCodNodoGA.Text;
                        SqlDataSource17.UpdateParameters["Etapa"].DefaultValue = ddlEtapa.SelectedValue;
                        SqlDataSource17.UpdateParameters["FechaInicial"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecIniRec.Text);
                        SqlDataSource17.UpdateParameters["FechaFinal"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtFecFinRec.Text);
                        SqlDataSource17.UpdateParameters["HorasPlaneadas"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtHorasPlan.Text);
                        SqlDataSource17.Update();

                        omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                        filaGridObjetivo.Visible = true;
                        filaDetalleRecursos.Visible = false;
                    }
                    catch (Exception except)
                    {
                        omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    }
                }
            }
        }

        protected void imgBtnInsertarRICC_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                ddlCiclo.Focus();
                ddlCiclo.SelectedValue = null;
                ddlRI.SelectedValue = null;
                ddlCC.SelectedValue = null;
                txtUsuarioRICC.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionRICC.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                ddlCiclo.Enabled = true;
                ddlRI.Enabled = true;
                ddlCC.Enabled = true;
                btnImgInsertarRICC.Visible = true;
                btnImgActualizarRICC.Visible = false;

                filaDetalleRICC.Visible = true;
                filaGridRICC.Visible = false;
                filaDetalleOF.Visible = false;
                filaGridOF.Visible = false;
                
                trRiesgosForm.Visible = false;
            }
        }

        protected void imgBtnInsertar_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if(txtCodPlaneacion.Text != "0")
                {
                    
                    txtCodAuditoria.Text = "";
                    txtNomAuditoria.Text = "";
                    txtNomAuditoria.Focus();
                    txtDependencia.Text = "";
                    txtProceso.Text = "";
                    ddlEstandar.SelectedValue = null;
                    txtUsuario.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                    txtFecha.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    txtAlcance.Text = "";
                    txtObjetivo.Text = "";
                    txtRecursos.Text = "";
                    txtFecIniA.Text = "";
                    txtFecFinA.Text = "";
                    ddlEmpresa.SelectedValue = "0";
                    ddlNaturaleza.SelectedValue = "0";
                    ddlNivelImportancia.SelectedValue = "0";
                    ImgBSaveTema.Visible = true;
                    ImgBUpdateTema.Visible = false;
                    FormTemaAud.Visible = true;
                    filaAuditoria.Visible = false;
                    filaDetalle.Visible = false;
                    TabContainer2.Tabs[1].Enabled = false;
                    TabContainer2.Tabs[2].Enabled = false;
                    filaGridOF.Visible = false;
                    filaGridRICC.Visible = false;
                    filaGridObjetivo.Visible = false;
                    filaDetalleOF.Visible = false;
                    filaDetalleRICC.Visible = false;
                    filaDetalleObjetivo.Visible = false;
                    filaDetalleObjEnfoque.Visible = false;
                    FilaGridObjEnfoque.Visible = false;
                    FilaGridObjetivoSE.Visible = false;
                    FilaGridObjRecursos.Visible = false;
                    filaBtnTemas.Visible = false;
                    filaBtnTemas.Visible = false;
                    filaDetalleAuditoria.Visible = false;
                }else
                {
                    omb.ShowMessage("Por favor seleccionar un tema a auditar", 1, "Atención");
                }
                
            }
        }

        protected void imgBtnInsertarObjetivo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                filaDetalleObjetivo.Visible = true;
                filaGridObjetivo.Visible = false;
                txtAlcanceObj.Text = "";
                txtFecFin.Text = "";
                txtFecIni.Text = "";
                ddlObjetivo.SelectedValue = null;
                ddlObjetivo.Enabled = true;
                ddlGrupoAud.SelectedValue = null;
                imgGrupoAud.Enabled = false;
                txtUsuarioObj.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionObj.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgInsertarObjetivo.Visible = true;
                btnImgActualizarObjetivo.Visible = false;
                ddlObjetivo.Focus();
            }
        }

        protected void imgBtnInsertarOF_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                txtUsuarioOF.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionOF.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgInsertarOF.Visible = true;
                btnImgActualizarOF.Visible = false;
                ddlOF.Enabled = true;
                ddlCicloOF.Enabled = true;
                ddlOF.SelectedValue = null;
                ddlCicloOF.SelectedValue = null;

                filaDetalleOF.Visible = true;
                filaDetalleRICC.Visible = false;
                filaGridRICC.Visible = false;
                filaGridOF.Visible = false;
                
                trRiesgosForm.Visible = false;
            }
        }

        protected void imgBtnInsertarObjEnfoque_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                txtUsuarioObjEnf.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFechaCreacionObjEnf.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                btnImgInsertarObjEnfoque.Visible = true;
                btnImgActualizarObjEnfoque.Visible = false;
                ddlEnfoque.Enabled = true;
                ddlEnfoque.SelectedValue = null;
                txtObjetivoEnf.Text = ddlObjetivo.SelectedItem.Text;

                filaDetalleObjEnfoque.Visible = true;
                filaGridObjetivo.Visible = false;
            }
        }

        protected void imgBtnInsertarObjRecursos_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                ddlEtapa.SelectedValue = "";
                ddlEtapa.Focus();

                txtRecurso.Text = "";
                txtFecIniRec.Text = "";
                txtFecFinRec.Text = "";
                txtHorasPlan.Text = "";
                ddlEtapa.SelectedValue = "";

                if (TVGrupoAud.SelectedNode != null)
                    TVGrupoAud.SelectedNode.Selected = false;

                txtUsuarioRec.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionRec.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                lblCodNodoGA.Text = "0";
                GridView6.DataBind();
                btnImgInsertarRecursos.Visible = true;
                btnImgActualizarRecursos.Visible = false;
                ddlEtapa.Enabled = true;
                imgGrupoRec.Enabled = true;
                imgMR.Visible = false;

                filaGridObjetivo.Visible = false;
                filaDetalleRecursos.Visible = true;
            }
        }

        protected void imgBtnInsertarMasEtapa_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                ddlEtapa.SelectedValue = "";
                ddlEtapa.Focus();

                //txtRecurso.Text = "";
                txtFecIniRec.Text = "";
                txtFecFinRec.Text = "";
                txtHorasPlan.Text = "";
                ddlEtapa.SelectedValue = "";

                if (TVGrupoAud.SelectedNode != null)
                    TVGrupoAud.SelectedNode.Selected = false;

                txtUsuarioRec.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                txtFecCreacionRec.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                lblCodNodoGA.Text = "0";
                GridView6.DataBind();
                btnImgInsertarRecursos.Visible = true;
                btnImgActualizarRecursos.Visible = false;
                ddlEtapa.Enabled = true;
                imgGrupoRec.Enabled = true;
                imgMR.Visible = false;

                filaGridObjetivo.Visible = false;
                filaDetalleRecursos.Visible = true;
            }
        }

        protected void ImageButton12_Click(object sender, ImageClickEventArgs e)
        {
            TrVerEtapas.Visible = false;
            TbAddEtapas.Visible = true;
            ImageButton11.Visible = true;
            ImageButton10.Visible = false;
            loadDDLGrupoAuditoria();
        }

        protected void ImageButton13_Click(object sender, ImageClickEventArgs e)
        {
            TbAddEtapas.Visible = false;
            TrVerEtapas.Visible = true;
            ddlGrupoAud2.SelectedIndex = 0;
            TrVerEtapas.Visible = false;
        }

        protected void ImageButton11_Click(object sender, ImageClickEventArgs e)
        {
            
                if (rblAudCondicion.SelectedValue != "2")
                {
                    DataTable DtInfo = new DataTable();
                    DtInfo = cAu.mtdVerObjetivoEstandar(ddlEstandar.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text.Trim()));
                    mtdRegistrarAudObjRecurso(DtInfo.Rows[0]["IdObjetivo"].ToString().Trim());
            }
            else
            {
                mtdRegistrarAudObjRecurso("0");
            }
                    

        }
        public void mtdRegistrarAudObjRecurso(string IdObjetivo)
        {
            
            try
            {
            
            cAu.InsertarRecursosGruposAuditoria(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text.Trim()), IdObjetivo,//DtInfo.Rows[0]["IdObjetivo"].ToString().Trim()
                ddlGrupoAud2.SelectedValue.ToString().Trim(), DropDownList2.SelectedValue.ToString().Trim(),
                DropDownList1.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox1.Text), Sanitizer.GetSafeHtmlFragment(TextBox2.Text), Sanitizer.GetSafeHtmlFragment(TextBox3.Text),
                System.DateTime.Now.ToString("yyyy-MM-dd"), Session["idUsuario"].ToString());

            //enviar la notificacion 
            //Revisar 04-04-2014 camilo aponte
            string InfAdicional = "Ha sido asignado como responsable de la auditoría:<br /><br />";
            InfAdicional += "Planeación: " + Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text) + ", " + Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text) + "<br />";
            InfAdicional += "Auditoría: " + Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text) + ", " + Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text) + "<br />";
            InfAdicional += "Objetivo: " + Sanitizer.GetSafeHtmlFragment(txtObjetivoRec.Text) + "<br />";
            InfAdicional += "Fecha Inicio: " + Sanitizer.GetSafeHtmlFragment(TextBox1.Text) + "<br />" + "Fecha Fin: " + Sanitizer.GetSafeHtmlFragment(TextBox2.Text) + "<br />" + "Horas: " + Sanitizer.GetSafeHtmlFragment(TextBox3.Text) + "<br /><br />";
            try
            {
                boolEnviarNotificacion(15, Convert.ToInt16("0"), Convert.ToInt16(DropDownList2.SelectedValue.ToString()), "", InfAdicional);
            }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al enviar la notificación." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
            }

            loadGrid();
            loadInfo();
            TrVerEtapas.Visible = true;
            TbAddEtapas.Visible = false;
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            DropDownList1.SelectedIndex = 0;
            omb.ShowMessage("La información se ingresó con éxito en la Base de Datos.", 3, "Atención");
                }
            catch (Exception ex)
            {
                omb.ShowMessage("Error al guardar la información." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
            }
}
        protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
        {
            DataTable DtInfo = new DataTable();
            DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
            cAu.ModificarRecursosGruposAuditoria(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text), DtInfo.Rows[0]["IdObjetivo"].ToString().Trim(), ddlGrupoAud2.SelectedValue.ToString().Trim(), DropDownList2.SelectedValue.ToString().Trim(), DropDownList1.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(TextBox1.Text), Sanitizer.GetSafeHtmlFragment(TextBox2.Text), Sanitizer.GetSafeHtmlFragment(TextBox3.Text));
            loadGrid();
            loadInfo();
            TrVerEtapas.Visible = true;
            TbAddEtapas.Visible = false;
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            DropDownList1.SelectedIndex = 0;
            GridView61.DataBind();
            omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
        }

        #endregion Buttons

        protected void txtCodEstandarObj_TextChanged(object sender, EventArgs e)
        {
            ddlObjetivo.Items.Clear();
            ddlObjetivo.DataBind();
        }

        protected void TVGrupoAud_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (filaDetalleRecursos.Visible == true)
            {
                txtRecurso.Text = TVGrupoAud.SelectedNode.Text;
                lblCodNodoGA.Text = TVGrupoAud.SelectedNode.Value;
                GridView6.DataBind();
                DiasHorasLaborables();
                imgMR.Visible = true;
            }
        }

        protected void TabContainer2_ActiveTabChanged(object sender, EventArgs e)
        {
            if ((TabContainer2.ActiveTabIndex.ToString() == "0" || TabContainer2.ActiveTabIndex.ToString() == "1") && filaDetalleObjetivo.Visible == true)
            {
                filaDetalleObjetivo.Visible = false;
                filaGridObjetivo.Visible = true;
            }

            if ((TabContainer2.ActiveTabIndex.ToString() == "0"))
            {
                filaDetalleAuditoria.Visible = true;
            }
            else if ((TabContainer2.ActiveTabIndex.ToString() != "0"))
            {
                filaDetalleAuditoria.Visible = false;

                mtdLoadGridAuditoriaRiesgos();
            }
        }

        public void mtdLoadGridAuditoriaRiesgos()
        {
            DataTable dtInfoAuditoriaRiesgo = new DataTable();
            DataTable dtInfo = new DataTable();
            clsAuditoriaRiesgosDAL dbAudRie = new clsAuditoriaRiesgosDAL();
            dtInfoAuditoriaRiesgo = dbAudRie.mtdLoadAuditoriaRiesgos(Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text)));
            dtInfo = dbAudRie.mtdLoadAuditoriaRiesgosControles(Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text)));
            /*if (dtInfo.Rows.Count == 0)
                lblMesanjeControles.Visible = true;
            else
                lblMesanjeControles.Visible = false;

            grdRiesgos.DataSource = dtInfoAuditoriaRiesgo;
            grdRiesgos.DataBind();*/
        }
        protected void SqlDataSource200_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            LastInsertIdCE = (int)e.Command.Parameters["@NewParameter2"].Value;
        }

        protected void SqlDataSource1_On_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            //LastInsertId = (int)e.Command.Parameters["@NewParameter"].Value;
        }

        private void EliminarAsignacionRecursos()
        {
            DataTable DtInfo = new DataTable();
            DtInfo = cAu.VerObjetivoEstandar(ddlEstandar.SelectedValue.ToString());
            cAu.EliminarRecursosGruposAuditoria(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text), DtInfo.Rows[0]["IdObjetivo"].ToString().Trim(), ddlGrupoAud2.SelectedValue.ToString().Trim(), InfoGrid.Rows[RowGrid]["IdHijo"].ToString().Trim(), InfoGrid.Rows[RowGrid]["Etapa"].ToString().Trim());
            loadGrid();
            loadInfo();
        }

        private void CargarIdGrupoAud()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cAu.VerIdGrupoAuditoriaGuardado(Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text));

            for (int i = 0; i < ddlGrupoAud2.Items.Count; i++)
            {
                ddlGrupoAud2.SelectedIndex = i;
                if (ddlGrupoAud2.SelectedValue.ToString().Trim() == dtInfo.Rows[0]["IdGrupoAuditoria"].ToString().Trim())
                {
                    break;
                }
                else
                {
                    ddlGrupoAud2.SelectedIndex = 0;
                }
            }


        }

        private void LoadAsignacionRecursos()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            //DropDownList1.SelectedIndex = 0;
            //DropDownList2.SelectedIndex = 0;

            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                DropDownList1.SelectedIndex = i;
                if (DropDownList1.SelectedItem.Text.ToString().Trim() == InfoGrid.Rows[RowGrid]["Etapa"].ToString().Trim())
                {
                    break;
                }
                else
                {
                    DropDownList1.SelectedIndex = 0;
                }
            }
            for (int i = 0; i < DropDownList2.Items.Count; i++)
            {
                DropDownList2.SelectedIndex = i;
                if (DropDownList2.SelectedItem.Text.ToString().Trim() == InfoGrid.Rows[RowGrid]["NombreResponsable"].ToString().Trim())
                {
                    break;
                }
                else
                {
                    DropDownList2.SelectedIndex = 0;
                }
            }

            TextBox1.Text = InfoGrid.Rows[RowGrid]["FechaInicial"].ToString().Trim();
            TextBox2.Text = InfoGrid.Rows[RowGrid]["FechaFinal"].ToString().Trim();
            TextBox3.Text = InfoGrid.Rows[RowGrid]["HorasPlaneadas"].ToString().Trim();
            lblCodNodoGA1.Text = InfoGrid.Rows[RowGrid]["IdHijo"].ToString().Trim();
            DiasHorasLaborables1();
        }

        private void loadDDLGrupoAuditoria()
        {
            DataTable dtInfo = new DataTable();
            DropDownList2.Items.Clear();
            DropDownList2.Items.Insert(0, new ListItem("---", "---"));
            dtInfo = cAu.LoadGruposAuditoria(ddlGrupoAud2.SelectedValue.ToString());
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                DropDownList2.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreResponsable"].ToString().Trim(), dtInfo.Rows[i]["idHijo"].ToString()));
            }
        }

        private void loadGrid()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("Etapa", typeof(string));
            grid.Columns.Add("IdHijo", typeof(string));
            grid.Columns.Add("NombreResponsable", typeof(string));
            grid.Columns.Add("FechaInicial", typeof(string));
            grid.Columns.Add("FechaFinal", typeof(string));
            grid.Columns.Add("HorasPlaneadas", typeof(string));
            InfoGrid = grid;
            GdRecursos.DataSource = InfoGrid;
            GdRecursos.DataBind();
        }

        private void loadInfo()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cAu.LoasRecursosPlaneacion(Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text), Sanitizer.GetSafeHtmlFragment(txtCodObjetivo.Text), ddlGrupoAud2.SelectedValue.ToString());

            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGrid.Rows.Add(new Object[] {
                            dtInfo.Rows[rows]["Etapa"].ToString().Trim(),
                            dtInfo.Rows[rows]["IdHijo"].ToString().Trim(),                                          
                            dtInfo.Rows[rows]["NombreResponsable"].ToString().Trim(),
                            dtInfo.Rows[rows]["FechaInicial"].ToString().Trim(),                                                                  
                            dtInfo.Rows[rows]["FechaFinal"].ToString().Trim(),
                            dtInfo.Rows[rows]["HorasPlaneadas"].ToString().Trim()
                        });
                }

                GdRecursos.DataSource = InfoGrid;
                GdRecursos.DataBind();
                ddlGrupoAud2.Enabled = false;
            }
            else
            {
                TrVerEtapas.Visible = false;
                ddlGrupoAud2.Enabled = true;
            }
        }

        protected Boolean VerificarCampos()
        {
            bool err = true;

            if (TabContainer2.ActiveTabIndex == 0)
            {
                if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtNomAuditoria.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Tema.", 2, "Atención");
                    txtNomAuditoria.Focus();
                }

                if (ddlEmpresa.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar la Empresa.", 2, "Atención");
                    txtNomAuditoria.Focus();
                }
                else if (ddlEstandar.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Programa/Estandár.", 2, "Atención");
                    ddlEstandar.Focus();
                }
                //04-04-2014
                else if (ddlGrupoAud2.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar el Grupo de Auditoría.", 2, "Atención");
                    ddlGrupoAud2.Focus();
                }
                else if (ddlNaturaleza.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar la Naturaleza.", 2, "Atención");
                    ddlNaturaleza.Focus();
                }
                else if (ddlNivelImportancia.SelectedValue == "0")
                {
                    err = false;
                    omb.ShowMessage("Debe seleccionar el Nivel de Importancia.", 2, "Atención");
                    ddlNivelImportancia.Focus();
                }
                else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtFecIniA.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Fecha de Inicio de la Auditoría.", 2, "Atención");
                    txtFecIni.Focus();
                }
                else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtFecFinA.Text)))
                {
                    err = false;
                    omb.ShowMessage("Debe ingresar la Fecha Proyectada de Cierre de la Auditoría.", 2, "Atención");
                    txtFecFin.Focus();
                }
            }
            else if (TabContainer2.ActiveTabIndex == 1)
            {
                if (filaDetalleRICC.Visible == true)
                {
                    if (ddlCiclo.SelectedValue == "0")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar el Ciclo.", 2, "Atención");
                        ddlCiclo.Focus();
                    }
                    else if (ddlRI.SelectedValue == "0")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar el Riesgo Inherente.", 2, "Atención");
                        ddlRI.Focus();
                    }
                    else if (ddlCC.SelectedValue == "0")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar la Calificación Control.", 2, "Atención");
                        ddlCC.Focus();
                    }
                }
                else if (filaDetalleOF.Visible == true)
                {
                    if (ddlOF.SelectedValue == "0")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar Otros Factores.", 2, "Atención");
                        ddlOF.Focus();
                    }
                }
            }
            else if (TabContainer2.ActiveTabIndex == 2)
            {
                if (filaDetalleObjetivo.Visible == true)
                {
                    if (ddlObjetivo.SelectedValue == "0")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar el Objetivo.", 2, "Atención");
                        ddlObjetivo.Focus();
                    }
                    else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtAlcanceObj.Text)))
                    {
                        err = false;
                        omb.ShowMessage("Debe ingresar el Alcance.", 2, "Atención");
                        txtAlcanceObj.Focus();
                    }
                    //04-04-2014
                    //else if (ddlGrupoAud.SelectedValue == "0")
                    //{
                    //    err = false;
                    //    omb.ShowMessage("Debe seleccionar el Grupo de Auditoria.", 2, "Atención");
                    //    ddlGrupoAud.Focus();
                    //}
                    //else if (ValidarCadenaVacia(txtFecIni.Text))
                    //{
                    //    err = false;
                    //    omb.ShowMessage("Debe ingresar la Fecha Inicial.", 2, "Atención");
                    //    txtFecIni.Focus();
                    //}
                    //else if (ValidarCadenaVacia(txtFecFin.Text))
                    //{
                    //    err = false;
                    //    omb.ShowMessage("Debe ingresar la Fecha Final.", 2, "Atención");
                    //    txtFecFin.Focus();
                    //}
                }
                else if (filaDetalleRecursos.Visible == true)
                {
                    if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtRecurso.Text)))
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar el Recurso.", 2, "Atención");
                        //txtAlcanceObj.Focus();
                    }
                    else if (ddlEtapa.SelectedValue == "")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar la Etapa.", 2, "Atención");
                        ddlEtapa.Focus();
                    }
                    else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtFecIniRec.Text)))
                    {
                        err = false;
                        omb.ShowMessage("Debe ingresar la Fecha Inicial.", 2, "Atención");
                        txtFecIniRec.Focus();
                    }
                    else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtFecFinRec.Text)))
                    {
                        err = false;
                        omb.ShowMessage("Debe ingresar la Fecha Final.", 2, "Atención");
                        txtFecFinRec.Focus();
                    }
                    else if (ValidarCadenaVacia(Sanitizer.GetSafeHtmlFragment(txtHorasPlan.Text)))
                    {
                        err = false;
                        omb.ShowMessage("Debe ingresar el Número de Horas Planeadas.", 2, "Atención");
                        txtHorasPlan.Focus();
                    }
                }
                else if (filaDetalleObjEnfoque.Visible == true)
                {
                    if (ddlEnfoque.SelectedValue == "0")
                    {
                        err = false;
                        omb.ShowMessage("Debe seleccionar el Enfoque.", 2, "Atención");
                        ddlEnfoque.Focus();
                    }
                }
            }
            return err;
        }

        protected Boolean ValidarCadenaVacia(string cadena)
        {
            Regex rx = new Regex(@"^-?\d+(\.\d{2})?$");
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

        protected void DiasHorasLaborables()
        {
            string selectCommand = "";
            int HorasDisp;
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            selectCommand = "SELECT CONVERT(VARCHAR(10),Min(FechaInicial),103) AS FechaInicial, CONVERT(VARCHAR(10),Max(FechaFinal),103) AS FechaFinal, Parametrizacion.FN_Horas_Laborables(" + lblCodNodoGA.Text.Trim() + ") AS HorasLab FROM Auditoria.AudObjRecurso WHERE IdHijo = " + lblCodNodoGA.Text.Trim();
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);

            DataView view = new DataView(dtblDiscuss);

            foreach (DataRowView row in view)
            {
                txtFecIniMR.Text = row["FechaInicial"].ToString().Trim();
                txtFecFinMR.Text = row["FechaFinal"].ToString().Trim();
                if (row["HorasLab"].ToString().Trim() == "0")
                    txtHorasLab.Text = "";
                else
                    txtHorasLab.Text = row["HorasLab"].ToString().Trim();
            }

            selectCommand = "SELECT SUM(HorasPlaneadas) as Suma FROM Auditoria.AudObjRecurso WHERE IdHijo = " + lblCodNodoGA.Text.Trim();
            dad = new SqlDataAdapter(selectCommand, conString);
            dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);

            view = new DataView(dtblDiscuss);

            foreach (DataRowView row in view)
            {
                if (row["Suma"].ToString().Trim() == "0")
                    txtHorasUtil.Text = "";
                else
                    txtHorasUtil.Text = row["Suma"].ToString().Trim();
            }

            if (txtHorasLab.Text != "")
            {
                HorasDisp = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtHorasLab.Text.Trim())) - Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtHorasUtil.Text.Trim()));
                txtHorasDisp.Text = HorasDisp.ToString().Trim();
                if (HorasDisp <= 0)
                    txtHorasDisp.Attributes["style"] = "color:red;";
                else
                    txtHorasDisp.Attributes["style"] = "color:green;";
            }
        }

        protected void DiasHorasLaborables1()
        {
            string selectCommand = "";
            int HorasDisp;
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;

            selectCommand = "SELECT CONVERT(VARCHAR(10),Min(FechaInicial),103) AS FechaInicial, CONVERT(VARCHAR(10),Max(FechaFinal),103) AS FechaFinal, Parametrizacion.FN_Horas_Laborables(" + lblCodNodoGA1.Text.Trim() + ") AS HorasLab FROM Auditoria.AudObjRecurso WHERE IdHijo = " + lblCodNodoGA1.Text.Trim();
            SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            DataTable dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);

            DataView view = new DataView(dtblDiscuss);

            foreach (DataRowView row in view)
            {
                txtFecIniMR1.Text = row["FechaInicial"].ToString().Trim();
                txtFecFinMR1.Text = row["FechaFinal"].ToString().Trim();
                if (row["HorasLab"].ToString().Trim() == "0")
                    txtHorasLab1.Text = "";
                else
                    txtHorasLab1.Text = row["HorasLab"].ToString().Trim();
            }

            selectCommand = "SELECT SUM(HorasPlaneadas) as Suma FROM Auditoria.AudObjRecurso WHERE IdHijo = " + lblCodNodoGA1.Text.Trim();
            dad = new SqlDataAdapter(selectCommand, conString);
            dtblDiscuss = new DataTable();
            dad.Fill(dtblDiscuss);

            view = new DataView(dtblDiscuss);

            foreach (DataRowView row in view)
            {
                if (row["Suma"].ToString().Trim() == "0")
                    txtHorasUtil1.Text = "";
                else
                    txtHorasUtil1.Text = row["Suma"].ToString().Trim();
            }

            if (txtHorasLab1.Text != "")
            {
                HorasDisp = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtHorasLab1.Text.Trim())) - Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtHorasUtil1.Text.Trim()));
                txtHorasDisp1.Text = HorasDisp.ToString().Trim();
                if (HorasDisp <= 0)
                    txtHorasDisp1.Attributes["style"] = "color:red;";
                else
                    txtHorasDisp1.Attributes["style"] = "color:green;";
            }
        }

        private Boolean boolEnviarNotificacion(int idEvento, int idRegistro, int idNodoJerarquia, string FechaFinal, string textoAdicional)
        {
            #region Variables
            bool err = false;
            string Destinatario = "", Copia = "", Asunto = "", Otros = "", Cuerpo = "", NroDiasRecordatorio = "";
            string selectCommand = "", AJefeInmediato = "", AJefeMediato = "", RequiereFechaCierre = "";
            string idJefeInmediato = "", idJefeMediato = "";
            string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
            #endregion

            try
            {
                #region informacion basica
                SqlDataAdapter dad = null;
                DataTable dtblDiscuss = new DataTable();
                DataView view = null;

                if (!string.IsNullOrEmpty(idEvento.ToString().Trim()))
                {
                    //Consulta la informacion basica necesario para enviar el correo de la tabla correos destinatarios
                    selectCommand = "SELECT CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.AJefeInmediato, CD.AJefeMediato, E.RequiereFechaCierre " +
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
                #endregion

                #region correo del Destinatario
                //Consulta el correo del Destinatario segun el nodo de la Jerarquia Organizacional
                if (!string.IsNullOrEmpty(idNodoJerarquia.ToString().Trim()))
                {
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
                #endregion

                #region correo del Jefe Inmediato
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
                #endregion

                #region correo del Jefe Mediato
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
                #endregion

                #region Correos Enviados
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
                SqlDataSource200.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                SqlDataSource200.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                SqlDataSource200.Insert();
                #endregion
            }
            catch (Exception except)
            {
                // Handle the Exception.
                omb.ShowMessage("Error en el envío de la notificación." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                err = true;
            }

            if (!err)
            {
                // Si no existe error en la creacion del registro en el log de correos enviados se procede a escribir en la tabla CorreosRecordatorios y a enviar el correo 
                if (RequiereFechaCierre == "SI" && FechaFinal != "")
                {
                    #region
                    //Si los NroDiasRecordatorio es diferente de vacio se inserta el registro correspondiente en la tabla CorreosRecordatorio
                    SqlDataSource201.InsertParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource201.InsertParameters["NroDiasRecordatorio"].DefaultValue = NroDiasRecordatorio;
                    SqlDataSource201.InsertParameters["Estado"].DefaultValue = "POR ENVIAR";
                    SqlDataSource201.InsertParameters["FechaFinal"].DefaultValue = FechaFinal;
                    SqlDataSource201.InsertParameters["IdUsuario"].DefaultValue = Session["idUsuario"].ToString().Trim(); //Aca va el id del Usuario de la BD
                    SqlDataSource201.InsertParameters["FechaRegistro"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource201.Insert();
                    #endregion
                }

                try
                {
                    #region Envio Correo
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    //MailAddress fromAddress = new MailAddress("risksherlock@hotmail.com", "Software Sherlock");
                    MailAddress fromAddress = new MailAddress(((System.Net.NetworkCredential)(smtpClient.Credentials)).UserName, "Software Sherlock");

                    message.From = fromAddress;//here you can set address

                    #region Destinatario
                    foreach (string substr in Destinatario.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(substr.Trim()))
                            message.To.Add(substr);
                    }
                    #endregion

                    #region Copia
                    if (Copia.Trim() != "")
                        foreach (string substr in Copia.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(substr.Trim()))
                                message.CC.Add(substr);
                        }
                    #endregion

                    #region Otros
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
                    #endregion
                }
                catch (Exception ex)
                {
                    //throw exception here you can write code to handle exception here
                    omb.ShowMessage("Error en el envio de la notificacion." + "<br/>" + "Descripción: " + ex.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    #region
                    //Actualiza el Estado del Correo Enviado
                    SqlDataSource200.UpdateParameters["IdCorreosEnviados"].DefaultValue = LastInsertIdCE.ToString().Trim();
                    SqlDataSource200.UpdateParameters["Estado"].DefaultValue = "ENVIADO";
                    SqlDataSource200.UpdateParameters["FechaEnvio"].DefaultValue = System.DateTime.Now.ToString().Trim();
                    SqlDataSource200.Update();
                    #endregion
                }
            }

            return (err);
        }

        protected void ImgBNewRegistro_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                if (txtCodPlaneacion.Text != "0")
                {
                    string Area = Session["AreaUser"].ToString();
                    DataTable dtInfo = new DataTable();
                    clsAuditoriaCodigosDAL dbCodigos = new clsAuditoriaCodigosDAL();
                    dtInfo = dbCodigos.mtdGetCodigoAuditoria(Area);
                    string sigla = string.Empty;
                    decimal consecutivo = 0;
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            sigla = dr["sigla"].ToString();
                            consecutivo = Convert.ToDecimal(dr["consecutivo"].ToString());
                        }
                    }
                    Session["consecutivoAud"] = consecutivo;
                    txtCodAuditoria.Text = "" ;
                    //txtCodigoAuditoria.Text = "" + sigla + "-" + (consecutivo + 1);
                    txtNomAuditoria.Text = "";
                    txtNomAuditoria.Focus();
                    txtDependencia.Text = "";
                    txtProceso.Text = "";
                    ddlEstandar.SelectedValue = null;
                    txtUsuario.Text = Session["loginUsuario"].ToString().Trim(); //Aca va el codigo de usuario logueado
                    txtFecha.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    txtAlcance.Text = "";
                    txtObjetivo.Text = "";
                    txtRecursos.Text = "";
                    txtFecIniA.Text = "";
                    txtFecFinA.Text = "";
                    ddlEmpresa.SelectedValue = "0";
                    ddlNaturaleza.SelectedValue = "0";
                    ddlNivelImportancia.SelectedValue = "0";
                    ddlGrupoAud.Enabled = true;
                    btnImgInsertar.Visible = true;
                    btnImgActualizar.Visible = false;
                    filaAuditoria.Visible = false;
                    filaDetalle.Visible = true;
                    TabContainer2.Tabs[1].Enabled = false;
                    TabContainer2.Tabs[2].Enabled = false;
                    filaGridOF.Visible = true;
                    filaGridRICC.Visible = true;
                    filaGridObjetivo.Visible = true;
                    filaDetalleOF.Visible = false;
                    filaDetalleRICC.Visible = false;
                    filaDetalleObjetivo.Visible = false;
                    filaDetalleObjEnfoque.Visible = false;
                    FilaGridObjEnfoque.Visible = false;
                    FilaGridObjetivoSE.Visible = true;
                    FilaGridObjRecursos.Visible = false;
                    filaBtnTemas.Visible = true;
                    filaBtnTemas.Visible = true;
                    filaDetalleAuditoria.Visible = true;

                    GridRegistrosAud.Visible = false;
                    GridView3.DataBind();
                    GridView2.DataBind();
                    GridView12.DataBind();
                    ddlGrupoAud2.Enabled = true;
                }
                else
                {
                    omb.ShowMessage("Por favor seleccionar un tema a auditar", 1, "Atención");
                }

            }
        }

        protected void GVregistrosAuditoria_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandArgument.ToString() == "0")
                Session["mod"] = 0;
            if (e.CommandArgument.ToString() == "Editar")
            {
                Session["mod"] = 1;
                //Session["del"] = 0;
                btnImgInsertar.Visible = false;
                btnImgActualizar.Visible = true;
                filaAuditoria.Visible = false;
                filaDetalle.Visible = true;
                txtNomAuditoria.Focus();
                GridRegistrosAud.Visible = false;
                filaDetalleAuditoria.Visible = true;
            }

            if (e.CommandArgument.ToString() == "Eliminar")
            {
                Session["mod"] = 0;
                
            }
            if(e.CommandName.ToString() == "Replicar")
            {
                trReplicar.Visible = true;
                GridRegistrosAud.Visible = false;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GVregistrosAuditoria.Rows[index];
                Session["IdAuditoria"] = row.Cells[0].Text;
            }
        }

        protected void GVregistrosAuditoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GVregistrosAuditoria.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                if(Session["mod"] != null && Session["mod"].ToString() == "1")
                {
                    txtCodAuditoria.Text = GVregistrosAuditoria.SelectedRow.Cells[0].Text.Trim();
                    Session["IdAuditoria"] = GVregistrosAuditoria.SelectedRow.Cells[0].Text.Trim();
                    txtCodAuditoriaGen.Text = GVregistrosAuditoria.SelectedRow.Cells[0].Text.Trim();
                    txtCodEstandarObj.Text = GVregistrosAuditoria.SelectedDataKey.Values[1].ToString().Trim();
                    txtAuditoriaObj.Text = GVregistrosAuditoria.SelectedRow.Cells[2].Text.Trim();
                    txtAuditoriaRICC.Text = GVregistrosAuditoria.SelectedRow.Cells[2].Text.Trim();
                    txtNomAuditoria.Text = GVregistrosAuditoria.SelectedRow.Cells[2].Text.Trim();
                    //txtCodigoAuditoria.Text = GVregistrosAuditoria.SelectedRow.Cells[1].Text.Trim();
                    txtCodObjetivo.Text = "0";
                    txtObjetivoEnf.Text = "0";
                    ddlNaturaleza.DataBind();
                    txtObjetivo.Text = GVregistrosAuditoria.SelectedDataKey[6].ToString().Trim();
                    txtRecursos.Text = GVregistrosAuditoria.SelectedDataKey[5].ToString().Trim();
                    txtAlcance.Text = GVregistrosAuditoria.SelectedDataKey[7].ToString().Trim();
                    txtFecIniA.Text = GVregistrosAuditoria.SelectedDataKey[10].ToString().Trim();
                    txtFecFinA.Text = GVregistrosAuditoria.SelectedDataKey[11].ToString().Trim();
                    ddlNaturaleza.SelectedValue = GVregistrosAuditoria.SelectedDataKey[9].ToString().Trim();
                    ddlNivelImportancia.SelectedValue = GVregistrosAuditoria.SelectedDataKey[8].ToString().Trim();
                    ddlEmpresa.SelectedValue = GVregistrosAuditoria.SelectedDataKey[12].ToString().Trim();
                    ddlEstandar.SelectedValue = GVregistrosAuditoria.SelectedDataKey[1].ToString().Trim();
                    ddlTipo.SelectedValue = GVregistrosAuditoria.SelectedRow.Cells[6].Text.Trim();
                    ddlMacroProceso.SelectedValue = null;
                    ddlProceso.SelectedValue = null;

                    if (GVregistrosAuditoria.SelectedRow.Cells[6].Text == "Procesos")
                    {
                        txtProceso.Text = GVregistrosAuditoria.SelectedDataKey[4].ToString().Trim();
                        lblIdProceso.Text = GVregistrosAuditoria.SelectedDataKey[3].ToString().Trim();
                        lblIdDependencia.Text = "";
                        txtDependencia.Text = "";
                        filaProceso.Visible = true;
                        filaDependencia.Visible = false;
                    }
                    else if (GVregistrosAuditoria.SelectedRow.Cells[6].Text == "Dependencia")
                    {
                        txtDependencia.Text = GVregistrosAuditoria.SelectedDataKey[4].ToString().Trim();
                        lblIdDependencia.Text = GVregistrosAuditoria.SelectedDataKey[2].ToString().Trim();
                        lblIdProceso.Text = "";
                        txtProceso.Text = "";
                        filaProceso.Visible = false;
                        filaDependencia.Visible = true;
                    }
                    txtUsuario.Text = GVregistrosAuditoria.SelectedDataKey[0].ToString().Trim(); //Aca va el codigo de usuario logueado
                    txtFecha.Text = GVregistrosAuditoria.SelectedRow.Cells[12].Text.Trim();

                    ddlMesEjecucion.SelectedValue = GVregistrosAuditoria.SelectedDataKey[13].ToString().Trim();
                    string especial = GVregistrosAuditoria.SelectedDataKey[15].ToString().Trim();
                    if (especial != "")
                        ddlEspecial.SelectedValue = GVregistrosAuditoria.SelectedDataKey[15].ToString().Trim();
                    //string semanas = GVregistrosAuditoria.SelectedDataKey[14].ToString().Trim();
                    string periodicidad = GVregistrosAuditoria.SelectedDataKey[16].ToString().Trim();
                    if (periodicidad != string.Empty)
                        ddlPeriodicidad.SelectedValue = periodicidad;
                    char[] delimiter = { ',' };
                    string[] semanas = GVregistrosAuditoria.SelectedDataKey[14].ToString().Trim().Split(delimiter);
                    for (int i = 0; i < semanas.Length; i++)
                    {
                        for (int j = 0; j < cblSemanaExe.Items.Count; j++)
                        {
                            if (semanas[i].ToString().Trim() == cblSemanaExe.Items[j].Value.ToString().Trim())
                            {
                                cblSemanaExe.Items[j].Selected = true;
                            }
                        }
                    }

                    txtPlaneacionObj.Text = Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text);
                    txtPlaneacionRICC.Text = Sanitizer.GetSafeHtmlFragment(txtNomPlaneacion.Text);
                    txtEstandarObj.Text = GVregistrosAuditoria.SelectedRow.Cells[4].Text.Trim();
                    txtEstandarRICC.Text = GVregistrosAuditoria.SelectedRow.Cells[4].Text.Trim();
                    ddlObjetivo.DataBind();
                    ddlCC.DataBind();
                    ddlCiclo.DataBind();
                    ddlCicloOF.DataBind();
                    ddlRI.DataBind();
                    ddlOF.DataBind();
                    ddlGrupoAud.DataBind();
                    if (ddlEstandar.SelectedValue != "")
                        ddlGrupoAud2.DataBind();

                    TabContainer2.ActiveTabIndex = 0;
                    TabContainer2.Tabs[1].Enabled = true;
                    TabContainer2.Tabs[2].Enabled = true;
                    filaGridOF.Visible = true;
                    filaGridRICC.Visible = true;
                    filaGridObjetivo.Visible = true;
                    filaDetalleOF.Visible = false;
                    filaDetalleRICC.Visible = false;
                    filaDetalleObjetivo.Visible = false;
                    filaDetalleObjEnfoque.Visible = false;
                    FilaGridObjEnfoque.Visible = false;
                    FilaGridObjetivoSE.Visible = true;
                    FilaGridObjRecursos.Visible = false;
                    filaBtnTemas.Visible = true;
                    filaDetalleAuditoria.Visible = true;
                    //trControl.Visible = false;
                    //trControlesForm.Visible = false;
                }
                
            }
        }

        protected void ImgBSaveTema_Click(object sender, ImageClickEventArgs e)
        {
            clsRegistroAuditoriaDTO objTemaAuditar = new clsRegistroAuditoriaDTO();
            clsRegistroAuditoriaBLL cBTemaAud = new clsRegistroAuditoriaBLL();
            objTemaAuditar.strTema = Sanitizer.GetSafeHtmlFragment(txtTemaTema.Text);
            objTemaAuditar.intIdPlaneacion = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text));
            objTemaAuditar.strFechaInicio = Sanitizer.GetSafeHtmlFragment(txtTemaFechaIni.Text);
            objTemaAuditar.strFechaCierre = Sanitizer.GetSafeHtmlFragment(txtTemaFechaCierre.Text);
            objTemaAuditar.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            objTemaAuditar.strFechaRegistro = DateTime.Now.ToString("yyyy-MM-dd");
            objTemaAuditar.intIdArea = Convert.ToInt32(Session["IdAreaUser"].ToString());
            bool booResult = false;
            string strErrMsg = string.Empty;
            booResult = cBTemaAud.mtdInsertarRegistroAuditoria(objTemaAuditar, ref strErrMsg);
            if(booResult == true)
            {
                omb.ShowMessage("La información se insertó con éxito en la Base de Datos.", 3, "Atención");
                FormTemaAud.Visible = false;
                filaAuditoria.Visible = true;
                GridView1.DataSource = SqlDStemas;
                GridView1.DataBind();
            }else
                omb.ShowMessage(strErrMsg, 1, "Atención");
            mtdCleanRegistroAud();
        }

        protected void ImgBUpdateTema_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosActualizar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                bool err = false;

                if (err == false)
                {
                    
                          try
                            {
                        //SqlDStemas.UpdateParameters["IdPlaneacion"].DefaultValue = Sanitizer.GetSafeHtmlFragment();
                        //SqlDStemas.UpdateParameters["IdRegistroAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment();
                        //SqlDStemas.UpdateParameters["Tema"].DefaultValue = Sanitizer.GetSafeHtmlFragment();
                        //SqlDStemas.UpdateParameters["IdPlaneacion"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text);
                        //SqlDStemas.UpdateParameters["FechaInicio"].DefaultValue = Sanitizer.GetSafeHtmlFragment();
                        //SqlDStemas.UpdateParameters["FechaCierre"].DefaultValue = Sanitizer.GetSafeHtmlFragment();
                        //"UPDATE [Auditoria].[RegistrosAuditoria] SET  [Tema] = '" + txtTemaTema.Text + "',[FechaInicio] = '" + txtTemaFechaIni.Text + "',[FechaCierre] = '" + txtTemaFechaCierre.Text + "' WHERE [IdPlaneacion] = " + txtCodPlaneacion.Text + " and IdRegistroAuditoria = " + txtCodAuditoria.Text
                        clsRegistroAuditoriaDAL dalRegistro = new clsRegistroAuditoriaDAL();
                        clsRegistroAuditoriaDTO objRegistro = new clsRegistroAuditoriaDTO();
                        string strErrMsg = string.Empty;
                        objRegistro.intIdRegistroAuditoria = Convert.ToInt32(txtCodAuditoria.Text);
                        objRegistro.intIdPlaneacion = Convert.ToInt32(txtCodPlaneacion.Text);
                        objRegistro.strTema = txtTemaTema.Text;
                        objRegistro.strFechaInicio = txtTemaFechaIni.Text;
                        objRegistro.strFechaCierre = txtTemaFechaCierre.Text;
                        bool flag = dalRegistro.mtdActualizarTemaAuditoria(objRegistro, ref strErrMsg);
                        /*loadGrid();
                        loadInfo();*/
                        GridView1.DataSource = SqlDStemas;
                        GridView1.DataBind();
                                TrVerEtapas.Visible = true;
                        filaAuditoria.Visible = true;
                        FormTemaAud.Visible = false;
                            }
                            catch (Exception except)
                            {
                                omb.ShowMessage("Error en la actualización de la información." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                                err = true;
                            }
                       
                            if (!err)
                            {
                                omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                                GridView1.DataBind();
                            }
                        }
                    

                }
                if (TreeView1.SelectedNode != null)
                    TreeView1.SelectedNode.Selected = false;
            mtdCleanRegistroAud();
        }

        protected void ddlEstandarAud_DataBound(object sender, EventArgs e)
        {
            ddlEstandarAud.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio    
        }

        protected void ImbSearachAud_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            int IdArea = Convert.ToInt32(Session["IdAreaUser"].ToString());
            if (!mtdLoadRegistrosAuditoria(ref strErrMsg, IdArea))
                omb.ShowMessage(strErrMsg, 2, "Atención");
        }

        protected void ImbSearchFiltro_Click(object sender, ImageClickEventArgs e)
        {
            int IdPlaneacion = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodPlaneacion.Text));
            string strCondicion = string.Empty;
            try
            {
                
                int CodRegistro = 0;
                if (txbCodigoFiltro.Text != "")
                    CodRegistro = Convert.ToInt32(txbCodigoFiltro.Text);
                string NombreRegistro = string.Empty;
                if (txbTemaFiltro.Text != "")
                    NombreRegistro = Sanitizer.GetSafeHtmlFragment(txbTemaFiltro.Text);
                if (CodRegistro != 0)
                {
                    strCondicion = string.Format(" and ARA.IdRegistroAuditoria = {0}", CodRegistro);
                }
                if (NombreRegistro != "")
                {
                    if (strCondicion != "")
                        strCondicion = strCondicion + string.Format(" and ARA.Tema LIKE '%{0}%'", NombreRegistro);
                    else
                        strCondicion = string.Format(" and ARA.Tema LIKE '%{0}%'", NombreRegistro);
                }
            }catch(Exception ex)
            {
                omb.ShowMessage("Error condicion: " + ex, 2, "Atención");
            }
            try
            {
                SqlDataSource SqlDataSource1 = new SqlDataSource();
                SqlDataSource1.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
                SqlDataSource1.ID = "SqlDataSource1";
                SqlDataSource1.SelectCommand = string.Format("SELECT ARA.IdRegistroAuditoria, ARA.Tema, ARA.idPlaneacion,ARA.FechaRegistro, ARA.FechaInicio,ARA.FechaCierre,ARA.IdUsuario, LU.Usuario" +
                        " FROM[Auditoria].[RegistrosAuditoria] as ARA" +
                        " INNER JOIN Listas.Usuarios as LU on LU.IdUsuario = ARA.IdUsuario" +
                        " where ARA.IdPlaneacion = {0} {1}", IdPlaneacion, strCondicion);
                //GridView1.DataSourceID = "SqlDataSource1";
                GridView1.DataSource = SqlDataSource1;
                GridView1.DataBind();
            }catch(Exception ex)
            {
                omb.ShowMessage("Error consulta: " + ex, 2, "Atención");
            }
            
        }

        protected void ImBCancelTema_Click(object sender, ImageClickEventArgs e)
        {
            FormTemaAud.Visible = false;
            GridView1.DataSource = SqlDStemas;
            GridView1.DataBind();
            filaAuditoria.Visible = true;
            mtdCleanRegistroAud();
        }
        public void mtdClean()
        {
            FormTemaAud.Visible = false;
            filaDetalle.Visible = false;
            filaAuditoria.Visible = false;
            GridRegistrosAud.Visible = true;
            txtTemaCodigo.Text = string.Empty;
            txtTemaTema.Text = string.Empty;
            txtTemaFechaIni.Text = string.Empty;
            txtTemaFechaCierre.Text = string.Empty;
            txtTemaUser.Text = string.Empty;
            txtTemaFecha.Text = string.Empty;
            GridView1.DataSource = SqlDStemas;
            GridView1.DataBind();
        }
        public void mtdCleanAud()
        {
            FormTemaAud.Visible = false;
            filaDetalle.Visible = false;
            filaAuditoria.Visible = true;
            GridRegistrosAud.Visible = false;
            txtTemaCodigo.Text = string.Empty;
            txtTemaTema.Text = string.Empty;
            txtTemaFechaIni.Text = string.Empty;
            txtTemaFechaCierre.Text = string.Empty;
            txtTemaUser.Text = string.Empty;
            txtTemaFecha.Text = string.Empty;
            GridView1.DataSource = SqlDStemas;
            GridView1.DataBind();
        }
        protected void ImgClearAud_Click(object sender, ImageClickEventArgs e)
        {
            mtdCleanAud();
            //GridRegistrosAud.Visible = true;
        }

        protected void ddlPLaneacionRep_DataBound(object sender, EventArgs e)
        {
            ddlPLaneacionRep.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ddlPLaneacionRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdPlaneacion = Convert.ToInt32(ddlPLaneacionRep.SelectedValue);
            SqlDSregistroAud.SelectCommand = string.Format("SELECT [IdRegistroAuditoria],[Tema] FROM [Auditoria].[RegistrosAuditoria] where IdPlaneacion = {0}",IdPlaneacion);
            ddlRegistrosAud.DataSource = SqlDSregistroAud;
            ddlRegistrosAud.DataBind();
        }

        protected void ddlRegistrosAud_DataBound(object sender, EventArgs e)
        {
            ddlRegistrosAud.Items.Insert(0, new ListItem("", "0")); // Inserta el Item con texto Vacio  
        }

        protected void ImbInsert_Click(object sender, ImageClickEventArgs e)
        {
            int IdPlan = Convert.ToInt32(ddlPLaneacionRep.SelectedValue);
            int IdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            int IdRegistro = Convert.ToInt32(ddlRegistrosAud.SelectedValue);
            int IdAuditoria = Convert.ToInt32(Session["IdAuditoria"].ToString());
            bool booResult = false;
            string strErrMsg = string.Empty;
            clsPlanificacionBLL cPlan = new clsPlanificacionBLL();
            booResult = cPlan.mtdReplicarPlanificacion(IdPlan, ref strErrMsg, IdUsuario, IdAuditoria, IdRegistro);
            if (booResult == true)
            {
                omb.ShowMessage("Auditoria replicada exitosamente", 3, "Atención");
                //string strErrMsg = string.Empty;
                int IdArea = Convert.ToInt32(Session["IdAreaUser"].ToString());
                if (!mtdLoadRegistrosAuditoria(ref strErrMsg, IdArea))
                    omb.ShowMessage(strErrMsg, 1, "Atención");
                mtdClean();
                trReplicar.Visible = false;
                //Response.Redirect("~/Formularios/Auditoria/Admin/AudAdmAuditoria.aspx", false);
            }
            else
            {
                omb.ShowMessage(strErrMsg, 1, "Atención");
            }
        }

        protected void ImbCleanReplica_Click(object sender, ImageClickEventArgs e)
        {
            mtdClean();
            trReplicar.Visible = false;
        }

        protected void btnDelTemaAud_Click(object sender, EventArgs e)
        {
            bool err = false;

            mpeMsgBox.Hide();
            try
            {
                SqlDStemas.DeleteParameters["IdRegistroAuditoria"].DefaultValue = Sanitizer.GetSafeHtmlFragment(txtCodAuditoria.Text);
                SqlDStemas.Delete();
                
                mtdClean();
            }
            catch(Exception ex)
            {
                omb.ShowMessage("Error eliminando el tema a auditar: " + ex.Message, 2, "Atención!");
            }
            if (!err)
                omb.ShowMessage("La información se eliminó con éxito en la Base de Datos.", 3, "Atención");
            mtdCleanAud();
            filaAuditoria.Visible = true;
            filaDetalle.Visible = false;
            filaBtnTemas.Visible = false;
            filaDetalleAuditoria.Visible = false;
            GridView1.DataSource = SqlDStemas;
            GridView1.DataBind();
            //ddlGrupoAud2.SelectedIndex = 0;
            TrVerEtapas.Visible = false;
            ddlMesEjecucion.SelectedIndex = 0;
            ddlEspecial.SelectedIndex = 0;
            cblSemanaExe.ClearSelection();
        }

        protected void btnDelAud_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                ImageButton btn = (ImageButton)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                string idRegistro = GVregistrosAuditoria.DataKeys[row.RowIndex][17].ToString();
                /*Response.Write("Row Index of Link button: " + row.RowIndex +
                               "DataKey value:" + GridView1.DataKeys[row.RowIndex].Value.ToString());*/
                txtCodAuditoria.Text = idRegistro;
                btnDelTemaAud.Visible = false;
                btnImgokEliminar.Visible = true;
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void btnEliminarRiesgo_Click(object sender, EventArgs e)
        {

        }

        protected void btnImgokEliminarIRCC_Click(object sender, EventArgs e)
        {

        }

        protected void imgRiesgosControlesAdd_Click(object sender, ImageClickEventArgs e)
        {
            Session["EditRiesgos"] = 0;
            //lblMensajeEditRiesgo.Visible = false;
            mtdLoadRiesgoForms("0");
        }

        protected void imgCancelarRiesgo_Click(object sender, ImageClickEventArgs e)
        {
            mtdCleanUniversioAuditoable();
        }

        /*protected void imgSaveRiesgo_Click(object sender, ImageClickEventArgs e)
        {
            clsAuditoriaRiesgosDTO objAudRies = new clsAuditoriaRiesgosDTO();
            clsAuditoriaRiesgosDAL dbAudRie = new clsAuditoriaRiesgosDAL();
            if (Session["EditRiesgos"].ToString() == "0")
            {
                objAudRies.intIdRiesgo = Convert.ToInt32(ddlRiesgos.SelectedValue);
                objAudRies.intIdAuditoria = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text));
                objAudRies.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString().Trim());

                try
                {
                    dbAudRie.mtdInsertAuditoriaRiesgos(objAudRies);
                }
                catch (Exception ex)
                {
                    Mensaje("Error en la insercion del riesgo: " + ex.Message);
                }
                finally
                {
                    Mensaje("Riesgo adicionado satisfactoriamente");
                }
            }
            else
            {
                objAudRies.intIdAuditoriaRiesgo = Convert.ToInt32(Session["IdAuditoriaRiesgo"].ToString());
                objAudRies.intIdRiesgo = Convert.ToInt32(ddlRiesgos.SelectedValue);
                objAudRies.intIdAuditoria = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text));
                objAudRies.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString().Trim());

                try
                {
                    dbAudRie.mtdUpdateAuditoriaRiesgos(objAudRies);
                }
                catch (Exception ex)
                {
                    Mensaje("Error en la insercion del riesgo: " + ex.Message);
                }
                finally
                {
                    Mensaje("Riesgo modificado satisfactoriamente");
                }
            }
            mtdLoadGridAuditoriaRiesgos();
            mtdCleanUniversioAuditoable();
        }*/
        #region Mensajes
        private void Mensaje(String Mensaje)
        {
            lblMsgBox.Text = Mensaje;
            mpeMsgBox.Show();
        }

        #endregion
        private void loadDDLCadenaValor()
        {
            try
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadDDLCadenaValor();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    ddlCadenavalor.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreCadenaValor"].ToString().Trim(), dtInfo.Rows[i]["IdCadenaValor"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar cadena valor. " + ex.Message);
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
                    ddlMacroprocesoRiesgo.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreMacroproceso"].ToString().Trim(), dtInfo.Rows[i]["IdMacroproceso"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar macroproceso. " + ex.Message);
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
                    ddlProcesoRiesgo.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreProceso"].ToString().Trim(), dtInfo.Rows[i]["IdProceso"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar proceso. " + ex.Message);
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
                    ddlSubpricesoRiesgo.Items.Insert(i + 1, new ListItem(dtInfo.Rows[i]["NombreSubProceso"].ToString().Trim(), dtInfo.Rows[i]["IdSubProceso"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Mensaje("Error al cargar subproceso. " + ex.Message);
            }
        }
        protected void ddlProcesoRiesgo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProcesoRiesgo.SelectedValue.ToString().Trim() != "---")
                loadDDLSubProceso(ddlProcesoRiesgo.SelectedValue.ToString().Trim());
        }

        protected void ddlMacroprocesoRiesgo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMacroprocesoRiesgo.SelectedValue.ToString().Trim() != "---")
                loadDDLProceso(ddlMacroprocesoRiesgo.SelectedValue.ToString().Trim());
        }

        protected void ddlCadenavalor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCadenavalor.SelectedValue.ToString().Trim() != "---")
                loadDDLMacroproceso(ddlCadenavalor.SelectedValue.ToString().Trim());
        }

        protected void imgSearchRiesgo_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadInfoRiesgos(txtCodigoRiesgo.Text, txtNombreRiesgo.Text, ddlCadenavalor.SelectedValue, ddlMacroprocesoRiesgo.SelectedValue, ddlProcesoRiesgo.SelectedValue, ddlSubpricesoRiesgo.SelectedValue, "---", "");
                loadGridRiesgos();
                if (dtInfo.Rows.Count > 0)
                {
                    for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                    {
                        InfoGridRiesgos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdRegion"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdPais"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdDepartamento"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdCiudad"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdOficinaSucursal"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdCadenaValor"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdMacroproceso"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdProceso"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdSubProceso"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdActividad"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdClasificacionRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdClasificacionGeneralRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdClasificacionParticularRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["NombreClasificacionRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdFactorRiesgoOperativo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdTipoRiesgoOperativo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdTipoEventoOperativo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdRiesgoAsociadoOperativo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaRiesgoAsociadoLA"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaFactorRiesgoLAFT"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaCausas"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaConsecuencias"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdResponsableRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdProbabilidad"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["OcurrenciaEventoDesde"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["OcurrenciaEventoHasta"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdImpacto"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["PerdidaEconomicaDesde"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["PerdidaEconomicaHasta"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Nombres"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["NombreHijo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaTratamiento"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["CodRiesgo"].ToString().Trim()
                                                          });
                    }
                    gvRiegosAud.PageIndex = PagIndexInfoGridRiesgos;
                    gvRiegosAud.DataSource = InfoGridRiesgos;
                    gvRiegosAud.DataBind();
                }
                else
                {
                    loadGridRiesgos();
                    Mensaje("El registro no existe o su información no es suficiente para ser visualizada.");
                }
                txtCodigoRiesgo.Text = string.Empty;
                txtNombreRiesgo.Text = string.Empty;
                ddlCadenavalor.ClearSelection();
                ddlMacroprocesoRiesgo.ClearSelection();
                ddlProcesoRiesgo.ClearSelection();
                ddlSubpricesoRiesgo.ClearSelection();
                
            }
        }
        private DataTable infoGridRiesgos;
        private DataTable InfoGridRiesgos
        {
            get
            {
                infoGridRiesgos = (DataTable)ViewState["infoGridRiesgos"];
                return infoGridRiesgos;
            }
            set
            {
                infoGridRiesgos = value;
                ViewState["infoGridRiesgos"] = infoGridRiesgos;
            }
        }

        private int pagIndexInfoGridRiesgos;
        private int PagIndexInfoGridRiesgos
        {
            get
            {
                pagIndexInfoGridRiesgos = (int)ViewState["pagIndexInfoGridRiesgos"];
                return pagIndexInfoGridRiesgos;
            }
            set
            {
                pagIndexInfoGridRiesgos = value;
                ViewState["pagIndexInfoGridRiesgos"] = pagIndexInfoGridRiesgos;
            }
        }
        private void loadGridRiesgos()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdRiesgo", typeof(string));
            grid.Columns.Add("IdRegion", typeof(string));
            grid.Columns.Add("IdPais", typeof(string));
            grid.Columns.Add("IdDepartamento", typeof(string));
            grid.Columns.Add("IdCiudad", typeof(string));
            grid.Columns.Add("IdOficinaSucursal", typeof(string));
            grid.Columns.Add("IdCadenaValor", typeof(string));
            grid.Columns.Add("IdMacroproceso", typeof(string));
            grid.Columns.Add("IdProceso", typeof(string));
            grid.Columns.Add("IdSubProceso", typeof(string));
            grid.Columns.Add("IdActividad", typeof(string));
            grid.Columns.Add("IdClasificacionRiesgo", typeof(string));
            grid.Columns.Add("IdClasificacionGeneralRiesgo", typeof(string));
            grid.Columns.Add("IdClasificacionParticularRiesgo", typeof(string));
            grid.Columns.Add("NombreClasificacionRiesgo", typeof(string));
            grid.Columns.Add("IdFactorRiesgoOperativo", typeof(string));
            grid.Columns.Add("IdTipoRiesgoOperativo", typeof(string));
            grid.Columns.Add("IdTipoEventoOperativo", typeof(string));
            grid.Columns.Add("IdRiesgoAsociadoOperativo", typeof(string));
            grid.Columns.Add("ListaRiesgoAsociadoLA", typeof(string));
            grid.Columns.Add("ListaFactorRiesgoLAFT", typeof(string));
            grid.Columns.Add("Codigo", typeof(string));
            grid.Columns.Add("Nombre", typeof(string));
            grid.Columns.Add("Descripcion", typeof(string));
            grid.Columns.Add("ListaCausas", typeof(string));
            grid.Columns.Add("ListaConsecuencias", typeof(string));
            grid.Columns.Add("IdResponsableRiesgo", typeof(string));
            grid.Columns.Add("IdProbabilidad", typeof(string));
            grid.Columns.Add("OcurrenciaEventoDesde", typeof(string));
            grid.Columns.Add("OcurrenciaEventoHasta", typeof(string));
            grid.Columns.Add("IdImpacto", typeof(string));
            grid.Columns.Add("PerdidaEconomicaDesde", typeof(string));
            grid.Columns.Add("PerdidaEconomicaHasta", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("Nombres", typeof(string));
            grid.Columns.Add("NombreHijo", typeof(string));
            grid.Columns.Add("ListaTratamiento", typeof(string));
            grid.Columns.Add("CodRiesgo", typeof(string));
            InfoGridRiesgos = grid;
            gvRiegosAud.DataSource = InfoGridRiesgos;
            gvRiegosAud.DataBind();
        }
        private void mtdLoadRiesgos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoRiesgos("", "", "---", "---", "---", "---", "---", "");
            /*if (LCodRiesgo.Text != "")
            {
                dtInfo = cRiesgo.loadInfoRiesgos(Sanitizer.GetSafeHtmlFragment(LCodRiesgo.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim()), DropDownList19.SelectedValue.ToString().Trim(), DropDownList20.SelectedValue.ToString().Trim(), DropDownList21.SelectedValue.ToString().Trim(), DropDownList22.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txbCodRiesgoFiltro.Text));
            }
            else
            {
                dtInfo = cRiesgo.loadInfoRiesgos(Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim()), DropDownList19.SelectedValue.ToString().Trim(), DropDownList20.SelectedValue.ToString().Trim(), DropDownList21.SelectedValue.ToString().Trim(), DropDownList22.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txbCodRiesgoFiltro.Text));
            }*/
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridRiesgos.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdRegion"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdPais"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdDepartamento"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdCiudad"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdOficinaSucursal"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdCadenaValor"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdMacroproceso"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdProceso"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdSubProceso"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdActividad"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdClasificacionRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdClasificacionGeneralRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdClasificacionParticularRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["NombreClasificacionRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdFactorRiesgoOperativo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdTipoRiesgoOperativo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdTipoEventoOperativo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdRiesgoAsociadoOperativo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaRiesgoAsociadoLA"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaFactorRiesgoLAFT"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Codigo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Nombre"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Descripcion"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaCausas"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaConsecuencias"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdResponsableRiesgo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdProbabilidad"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["OcurrenciaEventoDesde"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["OcurrenciaEventoHasta"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["IdImpacto"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["PerdidaEconomicaDesde"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["PerdidaEconomicaHasta"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["Nombres"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["NombreHijo"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["ListaTratamiento"].ToString().Trim(),
                                                           dtInfo.Rows[rows]["CodRiesgo"].ToString().Trim()
                                                          });
                }
                gvRiegosAud.PageIndex = PagIndexInfoGridRiesgos;
                gvRiegosAud.DataSource = InfoGridRiesgos;
                gvRiegosAud.DataBind();
            }
            else
            {
                loadGridRiesgos();
                Mensaje("El registro no existe o su información no es suficiente para ser visualizada.");
            }
        }
        /*public void mtdLoadRiesgos()
        {
            DataTable dtInfo = new DataTable();
            dtInfo = cRiesgo.loadInfoRiesgosAuditoria("", "", "---", "---", "---", "---", "---", "");
            gvRiegosAud.DataSource = dtInfo;
            gvRiegosAud.DataBind();
        }*/
        protected void imgCleanRiesgos_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                loadGridRiesgos();
                mtdLoadRiesgos();
                //, , , DropDownList20.SelectedValue.ToString().Trim(), DropDownList21.SelectedValue.ToString().Trim(), DropDownList22.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txbCodRiesgoFiltro.Text)
                ddlCadenavalor.Items.Clear();
                ddlCadenavalor.Items.Insert(0, new ListItem("---", "---"));
                ddlMacroprocesoRiesgo.Items.Clear();
                ddlMacroprocesoRiesgo.Items.Insert(0, new ListItem("---", "---"));
                ddlProcesoRiesgo.Items.Clear();
                ddlProcesoRiesgo.Items.Insert(0, new ListItem("---", "---"));
                ddlSubpricesoRiesgo.Items.Clear();
                ddlSubpricesoRiesgo.Items.Insert(0, new ListItem("---", "---"));
                loadDDLCadenaValor();


            }
        }
        public void mtdCleanUniversioAuditoable()
        {
            filaDetalleRICC.Visible = false;
            filaGridRICC.Visible = true;
            filaDetalleOF.Visible = false;
            filaGridOF.Visible = true;
            /*trRiesgos.Visible = true;
            trRiesgosForm.Visible = false;
            trControlesForm.Visible = false;*/
        }

        protected void imgDeleteAuditoriaRiesgo_Click(object sender, ImageClickEventArgs e)
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
                btnImgokEliminarAudRie.Visible = true;
                mpeMsgBox.Show();
            }
        }

        public void mtdLoadRiesgoForms(string IdRiesgo)
        {
            if (cCuenta.permisosAgregar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                DataTable dtInfo = new DataTable();
                dtInfo = cRiesgo.loadInfoRiesgosAuditoria("", "", "---", "---", "---", "---", "---", "");
                //Sanitizer.GetSafeHtmlFragment(TextBox11.Text.Trim()), Sanitizer.GetSafeHtmlFragment(TextBox17.Text.Trim()), DropDownList19.SelectedValue.ToString().Trim(), DropDownList20.SelectedValue.ToString().Trim(), DropDownList21.SelectedValue.ToString().Trim(), DropDownList22.SelectedValue.ToString().Trim(), DropDownList4.SelectedValue.ToString().Trim(), Sanitizer.GetSafeHtmlFragment(txbCodRiesgoFiltro.Text)
                
                loadDDLCadenaValor();

                filaDetalleOF.Visible = false;
                filaDetalleRICC.Visible = false;
                filaGridRICC.Visible = false;
                filaGridOF.Visible = false;
                /*trRiesgos.Visible = false;
                trRiesgosForm.Visible = true;
                trControl.Visible = false;*/
                //trControlesForm.Visible = false;
            }
            
        }
        protected void grdRiesgos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Add")
            {
                int RowGrid = Convert.ToInt16(e.CommandArgument);
                /*GridViewRow row = grdRiesgos.Rows[RowGrid];
                var colsNoVisible = grdRiesgos.DataKeys[RowGrid]["IdRiesgo"];

                Session["IdRiesgo"] = colsNoVisible;
                DataTable dtInfo = new DataTable();
                clsAuditoriaRiesgosDAL dbRiesgosControles = new clsAuditoriaRiesgosDAL();
                dtInfo = dbRiesgosControles.mtdLoadAuditoriaRiesgosControles(Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text)));
                grdRiegosControles.DataSource = dtInfo;
                grdRiegosControles.DataBind();
                Session["EditControl"] = 0;
                mdtLoadControlforms("0");*/
            }else if(e.CommandName == "Select")
            {
                int RowGrid = Convert.ToInt16(e.CommandArgument);
               /* GridViewRow row = grdRiesgos.Rows[RowGrid];
                var IdAuditoriaRiesgo = grdRiesgos.DataKeys[RowGrid]["IdAuditoriaRiesgo"];
                var IdAuditoria = grdRiesgos.DataKeys[RowGrid]["IdAuditoria"];
                var IdRiesgo = grdRiesgos.DataKeys[RowGrid]["IdRiesgo"];
                Session["IdAuditoria"] = IdAuditoria;
                Session["IdRiesgo"] = IdRiesgo;
                Session["IdAuditoriaRiesgo"] = IdAuditoriaRiesgo;
                Session["EditRiesgos"] = 1;
                mtdLoadRiesgoForms(IdRiesgo.ToString());*/
                //lblMensajeEditRiesgo.Visible = true;
            }else if(e.CommandName == "Eliminar")
            {
                int RowGrid = Convert.ToInt16(e.CommandArgument);
                /*GridViewRow row = grdRiesgos.Rows[RowGrid];
                var IdAuditoriaRiesgo = grdRiesgos.DataKeys[RowGrid]["IdAuditoriaRiesgo"];
                var IdAuditoria = grdRiesgos.DataKeys[RowGrid]["IdAuditoria"];
                var IdRiesgo = grdRiesgos.DataKeys[RowGrid]["IdRiesgo"];
                Session["IdAuditoria"] = IdAuditoria;
                Session["IdRiesgo"] = IdRiesgo;
                Session["IdAuditoriaRiesgo"] = IdAuditoriaRiesgo;*/
            }
            
        }

        /*protected void imgSaveControl_Click(object sender, ImageClickEventArgs e)
        {
            clsAuditoriaRiesgosControlesDTO objAudRiesCon = new clsAuditoriaRiesgosControlesDTO();
            clsAuditoriaRiesgosDAL dbAudRie = new clsAuditoriaRiesgosDAL();
            if (Session["EditControl"].ToString() == "0")
            {
                objAudRiesCon.intIdRiesgo = Convert.ToInt32(ddlRiesgoControl.SelectedValue);
                objAudRiesCon.intIdAuditoria = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text));
                objAudRiesCon.intIdControl = Convert.ToInt32(ddlControles.SelectedValue);
                objAudRiesCon.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString().Trim());
                try
                {
                    dbAudRie.mtdInsertAuditoriaRiesgosControles(objAudRiesCon);
                }
                catch (Exception ex)
                {
                    Mensaje("Error en la insercion del riesgo: " + ex.Message);
                }
                finally
                {
                    Mensaje("Control adicionado satisfactoriamente");
                }
            }
            else
            {
                int IdAuditoriaRiesgoControl = Convert.ToInt32(Session["IdAuditoriaRiesgoControl"].ToString());
                objAudRiesCon.intIdAuditoriaRiesgoControl = IdAuditoriaRiesgoControl;
                objAudRiesCon.intIdAuditoria = Convert.ToInt32(Sanitizer.GetSafeHtmlFragment(txtCodAuditoriaGen.Text));
                objAudRiesCon.intIdControl = Convert.ToInt32(ddlControles.SelectedValue);
                objAudRiesCon.intIdUsuario = Convert.ToInt32(Session["idUsuario"].ToString().Trim());

                    try
                {
                    dbAudRie.mtdUpdateAuditoriaRiesgosControles(objAudRiesCon);
                }
                catch (Exception ex)
                {
                    Mensaje("Error en la insercion del riesgo: " + ex.Message);
                }
                finally
                {
                    Mensaje("Control modificado satisfactoriamente");
                }
            }
            mtdLoadGridAuditoriaRiesgos();
            mtdCleanUniversioAuditoable();
        }*/

        protected void imgCancelarControl_Click(object sender, ImageClickEventArgs e)
        {
            mtdCleanUniversioAuditoable();
        }

        public void mdtLoadControlforms(string IdControl)
        {
            

            DataTable dtInfo = new DataTable();
            cControl cControl = new cControl();
            dtInfo = cControl.LoadInfoControlesDefault(Session["idJerarquia"].ToString());
            

            filaDetalleOF.Visible = false;
            filaDetalleRICC.Visible = false;
            filaGridRICC.Visible = false;
            filaGridOF.Visible = false;
            /*trRiesgos.Visible = false;
            trRiesgosForm.Visible = false;
            trControl.Visible = false;
            trControlesForm.Visible = true;*/
        }

        protected void imgControlAdd_Click(object sender, ImageClickEventArgs e)
        {
            Session["EditControl"] = 0;
            mdtLoadControlforms("0");
        }

        protected void grdRiegosControles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Select")
            {
                int RowGrid = Convert.ToInt16(e.CommandArgument);
                //GridViewRow row = grdRiegosControles.Rows[RowGrid];
                //var IdAuditoriaRiesgoControl = grdRiegosControles.DataKeys[RowGrid]["IdAuditoriaRiesgoControl"];
                //var IdControl = grdRiegosControles.DataKeys[RowGrid]["IdControl"];
               /* Session["IdAuditoriaRiesgoControl"] = IdAuditoriaRiesgoControl;
                Session["EditControl"] = 1;
                mdtLoadControlforms(IdControl.ToString());*/
            }
            else if(e.CommandName == "Eliminar")
            {
                /*int RowGrid = Convert.ToInt16(e.CommandArgument);
                GridViewRow row = grdRiegosControles.Rows[RowGrid];
                var IdAuditoriaRiesgoControl = grdRiegosControles.DataKeys[RowGrid]["IdAuditoriaRiesgoControl"];
                
                Session["IdAuditoriaRiesgoControl"] = IdAuditoriaRiesgoControl;*/
            }
        }

        protected void imgDeleteAuditoriaRiesgoControl_Click(object sender, ImageClickEventArgs e)
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
                btnImgokEliminarAudRieCon.Visible = true;
                mpeMsgBox.Show();
            }
        }

        protected void btnImgokEliminarAudRieCon_Click(object sender, EventArgs e)
        {
            clsAuditoriaRiesgosControlesDTO objAudRiesCon = new clsAuditoriaRiesgosControlesDTO();
            clsAuditoriaRiesgosDAL dbAudRie = new clsAuditoriaRiesgosDAL();
            try
            {
                int IdAuditoriaRiesgoControl = Convert.ToInt32(Session["IdAuditoriaRiesgoControl"].ToString());
                objAudRiesCon.intIdAuditoriaRiesgoControl = IdAuditoriaRiesgoControl;
                dbAudRie.mtdDeleteAuditoriaRiesgosControles(objAudRiesCon);
            }
            catch (Exception ex)
            {
                Mensaje("Error en la insercion del riesgo: " + ex.Message);
            }
            finally
            {
                Mensaje("Control eliminado satisfactoriamente");
                btnImgokEliminarAudRieCon.Visible = false;
            }
            mtdCleanUniversioAuditoable();
        }

        protected void btnImgokEliminarAudRie_Click(object sender, EventArgs e)
        {
            clsAuditoriaRiesgosDTO objAudRie = new clsAuditoriaRiesgosDTO();
            clsAuditoriaRiesgosDAL dbAudRie = new clsAuditoriaRiesgosDAL();
            try
            {
                int IdAuditoriaRiesgo = Convert.ToInt32(Session["IdAuditoriaRiesgo"].ToString());
                int IdAuditoria = Convert.ToInt32(Session["IdAuditoria"].ToString()) ;
                int IdRiesgo = Convert.ToInt32(Session["IdRiesgo"].ToString()) ;
                objAudRie.intIdAuditoriaRiesgo = IdAuditoriaRiesgo;
                objAudRie.intIdAuditoria = IdAuditoria;
                objAudRie.intIdRiesgo = IdRiesgo;
                dbAudRie.mtdDeleteAuditoriaRiesgos(objAudRie);
            }
            catch (Exception ex)
            {
                Mensaje("Error en la insercion del riesgo: " + ex.Message);
            }
            finally
            {
                Mensaje("Riesgo eliminado satisfactoriamente");
                btnImgokEliminarAudRie.Visible = false;
            }
            mtdLoadGridAuditoriaRiesgos();
            mtdCleanUniversioAuditoable();
        }

        protected void rblAudCondicion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rblAudCondicion.SelectedValue == "1")
            {
                trRiesgosAud.Visible = false;
            }else
            {
                DataTable DtInfoEstandar = new DataTable();
                DtInfoEstandar = cAu.VerEstandarRiesgos("Basada en Riesgos");
                ddlEstandar.SelectedValue = DtInfoEstandar.Rows[0]["IdEstandar"].ToString();
                trRiesgosAud.Visible = true;
                PagIndexInfoGridRiesgos = 0;
                loadGridRiesgos();
                mtdLoadRiesgos();
                loadDDLCadenaValor();
            }
        }

        protected void gvRiegosAud_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "select":
                    int RowGridRiesgos = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridRiesgos) + Convert.ToInt16(e.CommandArgument);
                    string strErrMsg = string.Empty;
                    try
                    {
                        int Index = Convert.ToInt16(e.CommandArgument);
                        var colsNoVisible = gvRiegosAud.DataKeys[Index].Values;
                        txtRiesgoControl.Text = colsNoVisible[3].ToString() + " - " + colsNoVisible[2].ToString();
                        Session["IdRiesgo"] = colsNoVisible[0].ToString();
                        Session["CodigoRiesgo"] = colsNoVisible[3].ToString();
                        Session["NombreRiesgo"] = colsNoVisible[2].ToString();
                        trControlesAud.Visible = true;
                        trRiesgosAud.Visible = false;
                        PagIndexInfoGridControles = 0;
                        loadGridControles();
                        loadInfoControles();
                        if(InfoGridRiesgosControlesAdd == null)
                            createRiesgosControlesAdd();
                    }
                    catch (Exception ex)
                    {
                        Mensaje("Error: " + strErrMsg);
                    }
                    break;
            }
        }

        protected void gvRiegosAud_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridRiesgos = e.NewPageIndex;
            gvRiegosAud.PageIndex = PagIndexInfoGridRiesgos;
            gvRiegosAud.DataSource = InfoGridRiesgos;
            gvRiegosAud.DataBind();
        }
        private DataTable infoGridControles;
        private DataTable InfoGridControles
        {
            get
            {
                infoGridControles = (DataTable)ViewState["infoGridControles"];
                return infoGridControles;
            }
            set
            {
                infoGridControles = value;
                ViewState["infoGridControles"] = infoGridControles;
            }
        }

        private int rowGridControles;
        private int RowGridControles
        {
            get
            {
                rowGridControles = (int)ViewState["rowGridControles"];
                return rowGridControles;
            }
            set
            {
                rowGridControles = value;
                ViewState["rowGridControles"] = rowGridControles;
            }
        }

        private DataTable infoGridRiesgosControlesAdd;
        private DataTable InfoGridRiesgosControlesAdd
        {
            get
            {
                infoGridRiesgosControlesAdd = (DataTable)ViewState["infoGridRiesgosControlesAdd"];
                return infoGridRiesgosControlesAdd;
            }
            set
            {
                infoGridRiesgosControlesAdd = value;
                ViewState["infoGridRiesgosControlesAdd"] = infoGridRiesgosControlesAdd ;
            }
        }
        private int pagIndexInfoGridControles;
        private int PagIndexInfoGridControles
        {
            get
            {
                pagIndexInfoGridControles = (int)ViewState["pagIndexInfoGridControles"];
                return pagIndexInfoGridControles;
            }
            set
            {
                pagIndexInfoGridControles = value;
                ViewState["pagIndexInfoGridControles"] = pagIndexInfoGridControles;
            }
        }
        private void loadInfoControles()
        {
            DataTable dtInfo = new DataTable();
            cControl cControl = new cControl();
            dtInfo = cControl.loadInfoControles("","","");
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridControles.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["CodigoControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["DescripcionControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["ObjetivoControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["Responsable"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdPeriodicidad"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdTest"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreTest"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdClaseControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdTipoControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdResponsableExperiencia"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdDocumentacion"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdResponsabilidad"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdCalificacionControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdUsuario"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdMitiga"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreHijo"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["ResponsableEjecucion"].ToString().Trim()
                                                            });
                }
                gvControlesRiesgosAud.DataSource = InfoGridControles;
                gvControlesRiesgosAud.DataBind();
            }
            else
            {
                loadGridControles();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }

        private void loadGridControles()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdControl", typeof(string));
            grid.Columns.Add("CodigoControl", typeof(string));
            grid.Columns.Add("NombreControl", typeof(string));
            grid.Columns.Add("DescripcionControl", typeof(string));
            grid.Columns.Add("ObjetivoControl", typeof(string));
            grid.Columns.Add("Responsable", typeof(string));
            grid.Columns.Add("IdPeriodicidad", typeof(string));
            grid.Columns.Add("IdTest", typeof(string));
            grid.Columns.Add("NombreTest", typeof(string));
            grid.Columns.Add("IdClaseControl", typeof(string));
            grid.Columns.Add("IdTipoControl", typeof(string));
            grid.Columns.Add("IdResponsableExperiencia", typeof(string));
            grid.Columns.Add("IdDocumentacion", typeof(string));
            grid.Columns.Add("IdResponsabilidad", typeof(string));
            grid.Columns.Add("IdCalificacionControl", typeof(string));
            grid.Columns.Add("FechaRegistro", typeof(string));
            grid.Columns.Add("IdUsuario", typeof(string));
            grid.Columns.Add("IdMitiga", typeof(string));
            grid.Columns.Add("NombreHijo", typeof(string));
            grid.Columns.Add("ResponsableEjecucion", typeof(string));
            InfoGridControles = grid;
            gvControlesRiesgosAud.DataSource = InfoGridControles;
            gvControlesRiesgosAud.DataBind();
        }
        private void createRiesgosControlesAdd()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdRiesgo", typeof(string));
            grid.Columns.Add("CodigoRiesgo", typeof(string));
            grid.Columns.Add("NombreRiesgo", typeof(string));
            grid.Columns.Add("IdControl", typeof(string));
            grid.Columns.Add("CodigoControl", typeof(string));
            grid.Columns.Add("NombreControl", typeof(string));

            InfoGridRiesgosControlesAdd = grid;
        }
        private int pagIndexInfoGridAsociaciones;
        private int PagIndexInfoGridAsociaciones
        {
            get
            {
                pagIndexInfoGridAsociaciones = (int)ViewState["pagIndexInfoGridAsociaciones"];
                return pagIndexInfoGridAsociaciones;
            }
            set
            {
                pagIndexInfoGridAsociaciones = value;
                ViewState["pagIndexInfoGridAsociaciones"] = pagIndexInfoGridAsociaciones;
            }
        }
        protected void gvControlesRiesgosAud_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGridControles = (Convert.ToInt16(GridView1.PageSize) * PagIndexInfoGridControles) + Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName){
                case "Add":
                    int IdRiesgo = Convert.ToInt32(Session["IdRiesgo"]);
                    string CodigoRiesgo = Session["CodigoRiesgo"].ToString();
                    string NombreRiesgo = Session["NombreRiesgo"].ToString();
                    int IdControl = Convert.ToInt32(InfoGridControles.Rows[RowGridControles]["IdControl"].ToString().Trim());
                    string CodigoControl = InfoGridControles.Rows[RowGridControles]["CodigoControl"].ToString().Trim();
                    string NombreControl = InfoGridControles.Rows[RowGridControles]["NombreControl"].ToString().Trim();
                    Session["IdControlAud"] = IdControl;
                    Session["CodigoControlAud"] = CodigoControl;
                    Session["NombreControlAud"] = NombreControl;
                    txtControlAct.Text = CodigoControl + " - " + NombreControl;
                    trControlesAud.Visible = false;
                    trActivadesControl.Visible = true;
                    InfoGridRiesgosControlesAdd.Rows.Add(new Object[] {
                        IdRiesgo,CodigoRiesgo,NombreRiesgo,IdControl,CodigoControl,NombreControl
                    });
                    gvAsociaciones.DataSource = InfoGridRiesgosControlesAdd;
                    gvAsociaciones.DataBind();
                    trAsociaciones.Visible = true;
                    if(InfoGridActControlesAdd == null)
                        createActControlAdd();
                    break;
            }
        }

        protected void btnImgEliminarAsociacion_Click(object sender, ImageClickEventArgs e)
        {
            if (cCuenta.permisosBorrar(IdFormulario) == "False")
            {
                //omb.ShowMessage("No tiene los permisos suficientes para llevar a cabo esta acción.", 2, "Atención");
                lblMsgBox.Text = "No tiene los permisos suficientes para llevar a cabo esta acción.";
                mpeMsgBox.Show();
            }
            else
            {
                btnImgokDelAsoc.Visible = true;
                lblMsgBox.Text = "Desea eliminar la información de la Base de Datos?";
                mpeMsgBox.Show();
            }
        }

        protected void gvAsociaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvAsociaciones.SelectedRow.RowType == DataControlRowType.DataRow)
            {
                Session["rowAsociaciones"] = gvAsociaciones.SelectedRow.RowIndex;
            }
        }

        protected void btnImgokDelAsoc_Click(object sender, EventArgs e)
        {
            int Row = Convert.ToInt32(Session["rowAsociaciones"].ToString());
            InfoGridRiesgosControlesAdd.Rows[Row].Delete();
            gvAsociaciones.DataSource = InfoGridRiesgosControlesAdd;
            gvAsociaciones.DataBind();
            btnImgokDelAsoc.Visible = false;
        }

        protected void imgSearchControl_Click(object sender, ImageClickEventArgs e)
        {
            loadGridControles();
            DataTable dtInfo = new DataTable();
            cControl cControl = new cControl();
            dtInfo = cControl.loadInfoControles(txtCodigoControlSearch.Text, txtNombreControlSearch.Text, "");
            if (dtInfo.Rows.Count > 0)
            {
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridControles.Rows.Add(new Object[] {dtInfo.Rows[rows]["IdControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["CodigoControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["DescripcionControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["ObjetivoControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["Responsable"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdPeriodicidad"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdTest"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreTest"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdClaseControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdTipoControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdResponsableExperiencia"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdDocumentacion"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdResponsabilidad"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdCalificacionControl"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["FechaRegistro"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdUsuario"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["IdMitiga"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["NombreHijo"].ToString().Trim(),
                                                             dtInfo.Rows[rows]["ResponsableEjecucion"].ToString().Trim()
                                                            });
                }
                gvControlesRiesgosAud.DataSource = InfoGridControles;
                gvControlesRiesgosAud.DataBind();
            }
            else
            {
                loadGridControles();
                Mensaje("No existen registros asociados a los parámetros de consulta.");
            }
        }

        protected void ImgCancelSearchControl_Click(object sender, ImageClickEventArgs e)
        {
            loadGridControles();
            loadInfoControles();
        }

        protected void gvControlesRiesgosAud_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridControles = e.NewPageIndex;
            gvControlesRiesgosAud.PageIndex = PagIndexInfoGridControles;
            gvControlesRiesgosAud.DataSource = InfoGridControles;
            gvControlesRiesgosAud.DataBind();
        }

        protected void gvAsociaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridAsociaciones = e.NewPageIndex;
            gvAsociaciones.PageIndex = PagIndexInfoGridAsociaciones;
            gvAsociaciones.DataSource = InfoGridRiesgosControlesAdd;
            gvAsociaciones.DataBind();
        }

        protected void imgRetornoRiesgos_Click(object sender, ImageClickEventArgs e)
        {
            trControlesAud.Visible = false;
            trRiesgosAud.Visible = true;
        }

        protected void imgSaveActControl_Click(object sender, ImageClickEventArgs e)
        {
            int IdControl = Convert.ToInt32(Session["IdControlAud"].ToString());
            string CodigoControl = Session["CodigoControlAud"].ToString() ;
            string NombreControl = Session["NombreControlAud"].ToString() ;
            
            string DescripAct = txtDescripcionAct.Text;
            InfoGridActControlesAdd.Rows.Add(new Object[] {
                        IdControl,CodigoControl,NombreControl,DescripAct
                    });
            gvAsocAct.DataSource = InfoGridActControlesAdd;
            gvAsocAct.DataBind();
            txtDescripcionAct.Text = string.Empty;
        }

        protected void imgCleanActControl_Click(object sender, ImageClickEventArgs e)
        {

        }

        private DataTable infoGridActControlesAdd;
        private DataTable InfoGridActControlesAdd
        {
            get
            {
                infoGridActControlesAdd = (DataTable)ViewState["infoGridActControlesAdd"];
                return infoGridActControlesAdd;
            }
            set
            {
                infoGridActControlesAdd = value;
                ViewState["infoGridActControlesAdd"] = infoGridActControlesAdd;
            }
        }
        private int pagIndexInfoGridActCont;
        private int PagIndexInfoGridActCont
        {
            get
            {
                pagIndexInfoGridActCont = (int)ViewState["pagIndexInfoGridActCont"];
                return pagIndexInfoGridActCont;
            }
            set
            {
                pagIndexInfoGridActCont = value;
                ViewState["pagIndexInfoGridActCont"] = pagIndexInfoGridActCont;
            }
        }
        private void createActControlAdd()
        {
            DataTable grid = new DataTable();
            grid.Columns.Add("IdControl", typeof(string));
            grid.Columns.Add("CodigoControl", typeof(string));
            grid.Columns.Add("NombreControl", typeof(string));
            grid.Columns.Add("DescripcionAct", typeof(string));

            InfoGridActControlesAdd = grid;
        }

        protected void imgRegresoRiesgosAct_Click(object sender, ImageClickEventArgs e)
        {
            loadGridControles();
            loadInfoControles();
            trControlesAud.Visible = true;
            trActivadesControl.Visible = false;
        }
    }
}

