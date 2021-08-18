using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ListasSarlaft.Classes;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.IO;
using ListasSarlaft.Classes.DTO.Calidad;
using ListasSarlaft.Classes.Utilidades;

namespace ListasSarlaft.UserControls.Proceso.Procesos
{
    public partial class ControlDocumentoVersiones : System.Web.UI.UserControl
    {
        string IdFormulario = "4029";
        cCuenta cCuenta = new cCuenta();
        clsVersionDocumentoBLL clsVersion = new clsVersionDocumentoBLL();
        cError error = new cError();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.IBinsertGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
            scriptManager.RegisterPostBackControl(this.IBupdateGVC);
            scriptManager.RegisterPostBackControl(GVversiones);
            scriptManager.RegisterPostBackControl(this.GVcontrolDocumento);
            //scriptManager.RegisterPostBackControl(this.fuArchivoPerfil);
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            else
            {
                if (!Page.IsPostBack)
                {
                    mtdStard();
                    mtdCargarDDLs();
                    mtdInicializarValores();
                    Session["FiltroActual"] = "";
                    Dictionary<string, string> datosAnteriores = new Dictionary<string, string>();
                    Session["DatosAnterioresDocumento"] = datosAnteriores;
                }
            }
        }
        #region Properties
        private DataTable infoGrid;
        private int rowGrid;
        private int pagIndex;
        private DataTable infoGridVersiones;
        private int rowGridVersiones;
        private int pagIndexVersiones;
        private DataTable infoGridFile;
        private int rowGridFile;
        private int pagIndexFile;

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
        private DataTable InfoGridVersiones
        {
            get
            {
                infoGridVersiones = (DataTable)ViewState["infoGridVersiones"];
                return infoGridVersiones;
            }
            set
            {
                infoGridVersiones = value;
                ViewState["infoGridVersiones"] = infoGridVersiones;
            }
        }

        private int RowGridVersiones
        {
            get
            {
                rowGridVersiones = (int)ViewState["rowGridVersiones"];
                return rowGridVersiones;
            }
            set
            {
                rowGridVersiones = value;
                ViewState["rowGridVersiones"] = rowGridVersiones;
            }
        }

        private int PagIndexVersiones
        {
            get
            {
                pagIndexVersiones = (int)ViewState["pagIndexVersiones"];
                return pagIndexVersiones;
            }
            set
            {
                pagIndexVersiones = value;
                ViewState["pagIndexVersiones"] = pagIndexVersiones;
            }
        }
        private DataTable InfoGridFile
        {
            get
            {
                infoGridFile = (DataTable)ViewState["infoGridFile"];
                return infoGridFile;
            }
            set
            {
                infoGridFile = value;
                ViewState["infoGridFile"] = infoGridFile;
            }
        }

        private int RowGridFile
        {
            get
            {
                rowGridFile = (int)ViewState["rowGridFile"];
                return rowGridFile;
            }
            set
            {
                rowGridFile = value;
                ViewState["rowGridFile"] = rowGridFile;
            }
        }

        private int PagIndexFile
        {
            get
            {
                pagIndexFile = (int)ViewState["pagIndexFile"];
                return pagIndexFile;
            }
            set
            {
                pagIndexFile = value;
                ViewState["pagIndexFile"] = pagIndexFile;
            }
        }
        #endregion
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
            tbxResponsable.Text = TreeView4.SelectedNode.Text;
            lblIdDependencia4.Text = TreeView4.SelectedNode.Value;
        }
        #endregion Treeview
        private void mtdInicializarValores()
        {
            PagIndex = 0;
            //PagIndex = 0;
            //txtFecha.Text = "" + DateTime.Now;
            //PagIndex3 = 0;
        }
        protected void mtdStard()
        {
            string strErrMsg = String.Empty;
            PopulateTreeView();
            //if (!mtdLoadControlVersion(ref strErrMsg))
            //    omb.ShowMessage(strErrMsg, 2, "Atención");

        }
        protected void mtdResetFields()
        {
            string strErrMsg = string.Empty;
            BodyFormCDV.Visible = false;
            BodyGridRNC.Visible = true;
            divFiltrosBusqueda.Visible = true;
            DVVersiones.Visible = false;
            ddlCadenaValor.Enabled = true;
            ddlCadenaValor.ClearSelection();
            ddlMacroproceso.Enabled = true;
            ddlMacroproceso.ClearSelection();
            ddlProceso.Enabled = true;
            ddlProceso.ClearSelection();
            ddlSubproceso.Enabled = true;
            ddlSubproceso.ClearSelection();
            txtId.Text = "";
            TXname.Text = "";
            TXname.Enabled = true;
            TXcodigoDoc.Text = "";
            TXcodigoDoc.Enabled = true;
            TXversion.Text = "";
            TXfechaimplementacion.Text = "";
            TXfechaimplementacion.Enabled = true;
            TXfechaActual.Text = "";
            TXfechaActual.Enabled = true;
            TXfechaUp.Text = "";
            TXfechaDel.Text = "";
            tbxResponsable.Text = "";
            tbxResponsable.Enabled = true;
            imgDependencia4.Visible = true;
            TXalmace.Text = "";
            TXalmace.Enabled = true;
            TXrollback.Text = "";
            TXrollback.Enabled = true;
            TXtiempoactivo.Text = "";
            TXtiempoactivo.Enabled = true;
            TXtiempoinactivo.Text = "";
            TXtiempoinactivo.Enabled = true;
            Txdisposicion.Text = "";
            Txdisposicion.Enabled = true;
            TXmedio.Text = "";
            TXmedio.Enabled = true;
            TXformato.Text = "";
            TXformato.Enabled = true;
            txtFecha.Text = "";
            txtFechaMod.Text = "";
            tbxUsuarioCreacion.Text = "";
            TXobservaciones.Text = "";
            txtJustificacion.Text = string.Empty;
            ddlEstadoDocumento.ClearSelection();

            // Filtros
            ddlCadenaValorFiltro.ClearSelection();
            ddlMacroprocesoFiltro.ClearSelection();
            ddlProcesoFiltro.ClearSelection();
            txtNombreFiltro.Text = string.Empty;
            txtCodigoFiltro.Text = string.Empty;
            txtFechaImplementacionFiltro.Text = string.Empty;
            ddlTipoDocumentoFiltro.ClearSelection();
            if (((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Count > 0)
            {
                mtdLoadControlVersion(ref strErrMsg, Session["FiltroActual"].ToString());
                ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Clear();
            }
        }
        protected void btnInsertarNuevo_Click(object sender, ImageClickEventArgs e)
        {
            trEstadoDocumento.Visible = false;
            trJustificacion.Visible = false;
            BodyGridRNC.Visible = false;
            divFiltrosBusqueda.Visible = false;
            BodyFormCDV.Visible = true;
            IBinsertGVC.Visible = true;
            IBupdateGVC.Visible = false;
            Session["IdControl"] = null;
        }
        #region DDLs
        protected void ddlCadenaValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlMacroproceso.Items.Clear();

            if (mtdLoadDDLMacroProceso(Convert.ToInt32(ddlCadenaValor.SelectedValue)))
            {
                ddlMacroproceso.ClearSelection();
                ddlProceso.Items.Clear();
                ddlSubproceso.Items.Clear();
            };

        }

        protected void ddlMacroproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            ddlProceso.Items.Clear();

            if (mtdLoadDDLProceso(Convert.ToInt32(ddlMacroproceso.SelectedValue)))
            {
                ddlProceso.ClearSelection();
                ddlSubproceso.Items.Clear();
            }
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strErrMsg = string.Empty;

            if (ddlProceso.SelectedValue == "0")
                rfvProceso.Enabled = false;
            else
                rfvProceso.Enabled = true;

            ddlSubproceso.Items.Clear();

            if (mtdLoadDDLSubproceso(Convert.ToInt32(ddlProceso.SelectedValue)))
            {
                ddlSubproceso.ClearSelection();
                if (ddlSubproceso.Items.Count == 1)
                {
                    omb.ShowMessage("No hay información de Subprocesos", 2, "Atención");
                }
            }
        }

        protected void ddlSubproceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProceso.SelectedValue == "0")
                rfvSubproceso.Enabled = false;
            else
                rfvSubproceso.Enabled = true;
        }
        #endregion
        #region DDLs
        private void mtdCargarDDLs()
        {
            string strErrMsg = string.Empty;
            mtdLoadDDLCadenaValor(ref strErrMsg);
            mtdLoadDDLTipo();
            mtdLoadDDLTipoDocumentoFiltro();
            mtdLoadDDLEstadoDocumento();


        }

        public List<EstadoDocumento> loadDdlEstadoDocumento() => clsVersion.opcionesEstadoDocumento();

        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLCadenaValor(ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            List<clsCadenaValor> lstCadenaValor = new List<clsCadenaValor>();
            clsCadenaValorBLL cCadenaValor = new clsCadenaValorBLL();
            #endregion Vars

            try
            {
                lstCadenaValor = cCadenaValor.mtdConsultarCadenaValor(true, ref strErrMsg);

                ddlCadenaValor.Items.Clear();
                ddlCadenaValor.Items.Insert(0, new ListItem("", "0"));

                // Filtro Búsqueda
                ddlCadenaValorFiltro.Items.Clear();
                ddlCadenaValorFiltro.Items.Insert(0, new ListItem("", "0"));

                if (string.IsNullOrEmpty(strErrMsg))
                {
                    if (lstCadenaValor != null)
                    {
                        int intCounter = 1;

                        foreach (clsCadenaValor objCadenaValor in lstCadenaValor)
                        {
                            ddlCadenaValor.Items.Insert(intCounter, new ListItem(objCadenaValor.strNombreCadenaValor, objCadenaValor.intId.ToString()));
                            ddlCadenaValorFiltro.Items.Insert(intCounter, new ListItem(objCadenaValor.strNombreCadenaValor, objCadenaValor.intId.ToString()));
                            intCounter++;
                        }
                        booResult = false;
                    }
                    else
                        booResult = true;
                }
                else
                    booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error durante la consulta de las cadenas de valor. [{0}]", ex.Message);
                booResult = true;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los macroprocesos y carga el DDL de las cadenas de valor.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLMacroProceso(int IdCadenaValor)
        {
            #region Vars
            bool booResult = false;
            List<clsMacroproceso> lstMacroproceso = new List<clsMacroproceso>();
            clsMacroProcesoBLL cMacroproceso = new clsMacroProcesoBLL();
            #endregion Vars

            try
            {
                DataTable dt = cMacroproceso.ConsultarMacroProcesos(IdCadenaValor);
                ddlMacroproceso.Items.Clear();
                ddlMacroproceso.Items.Insert(0, new ListItem("", "0"));

                // Filtro Búsqueda
                ddlMacroprocesoFiltro.Items.Clear();
                ddlMacroprocesoFiltro.Items.Insert(0, new ListItem("", "0"));


                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow Row in dt.Rows)
                    {
                        int intCounter = 1;
                        ddlMacroproceso.Items.Insert(intCounter, new ListItem(Row["Nombre"].ToString(), Row["IdMacroProceso"].ToString()));
                        ddlMacroprocesoFiltro.Items.Insert(intCounter, new ListItem(Row["Nombre"].ToString(), Row["IdMacroProceso"].ToString()));
                        intCounter++;
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                throw ex;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los Procesos y carga el DDL de los macroprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLProceso(int IdMacroproceso)
        {
            #region Vars
            bool booResult = false;
            clsMacroproceso objMProceso = new clsMacroproceso();
            List<clsProceso> lstProceso = new List<clsProceso>();
            clsProcesoBLL cProceso = new clsProcesoBLL();
            #endregion Vars

            try
            {

                DataTable dt = cProceso.ConsultarProcesos(IdMacroproceso);
                ddlProceso.Items.Clear();
                ddlProceso.Items.Insert(0, new ListItem("", "0"));

                // Filtro Búsqueda
                ddlProcesoFiltro.Items.Clear();
                ddlProcesoFiltro.Items.Insert(0, new ListItem("", "0"));


                if (dt != null && dt.Rows.Count > 0)
                {
                    int intCounter = 1;
                    foreach (DataRow Row in dt.Rows)
                    {
                        ddlProceso.Items.Insert(intCounter, new ListItem(Row["Nombre"].ToString(), Row["IdProceso"].ToString()));
                        ddlProcesoFiltro.Items.Insert(intCounter, new ListItem(Row["Nombre"].ToString(), Row["IdProceso"].ToString()));
                        intCounter++;
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = true;
                throw ex;
            }

            return booResult;
        }

        /// <summary>
        /// Consulta los Procesos y carga el DDL de los subprocesos.
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        private bool mtdLoadDDLSubproceso(int? idProceso)
        {
            #region Vars
            bool booResult = false;
            clsProceso objProceso = new clsProceso();
            List<clsSubproceso> lstSubproceso = new List<clsSubproceso>();
            clsSubprocesoBLL cSubproceso = new clsSubprocesoBLL();
            #endregion Vars

            try
            {
                DataTable dt = cSubproceso.ConsultarSubprocesos(idProceso);

                ddlSubproceso.Items.Clear();
                ddlSubproceso.Items.Insert(0, new ListItem("", "0"));

                // Filtro Búsqueda
                ddlSubprocesoFiltro.Items.Clear();
                ddlSubprocesoFiltro.Items.Insert(0, new ListItem("", "0"));

                if (dt != null && dt.Rows.Count > 0)
                {
                    int intCounter = 1;
                    foreach (DataRow Row in dt.Rows)
                    {
                        ddlSubproceso.Items.Insert(intCounter, new ListItem(Row["Nombre"].ToString(), Row["IdSubproceso"].ToString()));
                        ddlSubprocesoFiltro.Items.Insert(intCounter, new ListItem(Row["Nombre"].ToString(), Row["IdSubproceso"].ToString()));
                        intCounter++;
                    }
                }
                booResult = true;
            }
            catch (Exception ex)
            {
                booResult = false;
                throw ex;
            }

            return booResult;
        }
        public void mtdLoadDDLTipo()
        {
            DDLtipo.Items.Clear();
            DDLtipo.Items.Insert(0, new ListItem("Documento", "1"));
            DDLtipo.Items.Insert(0, new ListItem("Registro", "2"));
            DDLtipo.Items.Insert(0, new ListItem("Eliminación", "3"));
            DDLtipo.Items.Insert(0, new ListItem("Procedimiento", "4"));
            DDLtipo.Items.Insert(0, new ListItem("Politica", "5"));
            DDLtipo.Items.Insert(0, new ListItem("Manual", "6"));
            DDLtipo.Items.Insert(0, new ListItem("Instructivo", "7"));
            DDLtipo.Items.Insert(0, new ListItem("Reglamento", "8"));
            DDLtipo.Items.Insert(0, new ListItem("Formato", "9"));
            DDLtipo.Items.Insert(0, new ListItem("Circular", "10"));
            DDLtipo.Items.Insert(0, new ListItem("", "0"));
        }

        public void mtdLoadDDLEstadoDocumento()
        {
            ddlEstadoDocumento.Items.Clear();
            ddlEstadoDocumento.DataSource = loadDdlEstadoDocumento();
            ddlEstadoDocumento.DataValueField = "IdEstadoDocumento";
            ddlEstadoDocumento.DataTextField = "NombreEstadoDocumento";
            ddlEstadoDocumento.DataBind();
            ddlEstadoDocumento.Items.Insert(0, new ListItem("", "0"));
            ListItem item = ddlEstadoDocumento.Items.FindByValue("0");
            item.Attributes.Add("style", "display:none");
        }

        public void mtdLoadDDLTipoDocumentoFiltro()
        {
            ddlTipoDocumentoFiltro.Items.Clear();
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Documento", "1"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Registro", "2"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Eliminación", "3"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Procedimiento", "4"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Politica", "5"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Manual", "6"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Instructivo", "7"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Reglamento", "8"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Formato", "9"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("Circular", "10"));
            ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("", "0"));
        }

        #endregion

        protected void IBinsertGVC_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;
            bool booResult = new bool();
            if (fuArchivoPerfil.HasFile && (Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".pdf" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".docx" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".doc" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".xlsx" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".xls"))
            {
                omb.ShowMessage("Solo archivos en formato pdf,word,excel", 2, "Atención");
                return;
            }
            if (DDLtipo.SelectedValue == "1")
            {
                if (fuArchivoPerfil.HasFile)
                {

                    if (System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".pdf" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".docx" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".doc" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".xlsx")
                    {
                        booResult = mtdInsertarActualizarControlDoc(ref strErrMsg);
                        if (booResult == false)
                        {
                            omb.ShowMessage(strErrMsg, 1, "Atención");
                            mtdResetFields();
                            mtdStard();
                        }
                        else
                        {
                            omb.ShowMessage(strErrMsg, 3, "Atención");
                            mtdResetFields();
                            mtdStard();
                        }
                    }
                    else
                    {
                        omb.ShowMessage("Soló se permiten archivos PDF, Word, Excel", 2, "Atención");
                    }
                }
                else
                {
                    omb.ShowMessage("Cargar el archivo es Obligatorio", 2, "Atención");
                }
            }
            else
            {
                booResult = mtdInsertarActualizarControlDoc(ref strErrMsg);
                if (booResult == false)
                {
                    omb.ShowMessage(strErrMsg, 1, "Atención");
                    mtdResetFields();
                    mtdStard();
                }
                else
                {
                    omb.ShowMessage(strErrMsg, 3, "Atención");
                    mtdResetFields();
                    mtdStard();
                }
            }
            mtdLoadControlVersion(ref strErrMsg, Session["FiltroActual"].ToString());
        }

        protected void IBupdateGVC_Click(object sender, ImageClickEventArgs e)
        {
            trEstadoDocumento.Visible = true;
            trJustificacion.Visible = true;
            string strErrMsg = String.Empty;
            string version = TXversion.Text;

            // Se valida para insertar una justificacion
            if (int.TryParse((string)Session["IdControl"], out int outResult))
            {
                if (string.IsNullOrEmpty(txtJustificacion.Text))
                {
                    omb.ShowMessage("Debe indicar una justificación para el cambio", 2, "Atención");
                    return;
                }
            }

            if (version != "")
            {
                if (fuArchivoPerfil.HasFile && (Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".pdf" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".docx" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".doc" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".xlsx" && Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() != ".xls"))
                {
                    omb.ShowMessage("Solo archivos en formato pdf,word,excel", 2, "Atención");
                    return;
                }
                if (DDLtipo.SelectedValue == "1")
                {
                    if (fuArchivoPerfil.HasFile)
                    {
                        if ((mtdInsertarActualizarControlDoc(ref strErrMsg)))
                        {
                            omb.ShowMessage(strErrMsg, 3, "Atención");
                            mtdStard();
                            mtdResetFields();
                        }
                    }

                    else
                    {
                        omb.ShowMessage("Cargar el archivo es Obligatorio", 2, "Atención");
                    }
                }
                else
                {
                    if (mtdInsertarActualizarControlDoc(ref strErrMsg))
                    {
                        omb.ShowMessage(strErrMsg, 3, "Atención");
                        mtdStard();
                        mtdResetFields();
                    }
                }
            }
            else
            {
                omb.ShowMessage("La version no puede ir vacia", 2, "Atención");
            }
        }
        private bool mtdUpdateControlVersion(ref string strErrMsg)
        {
            txtFechaMod.Text = "" + DateTime.Now;
            bool booResult = false;
            clsControlVersion objCrlInfra = new clsControlVersion();
            objCrlInfra.intIdVersionDocumento = Convert.ToInt32(Session["IdControl"].ToString());
            objCrlInfra.strVersion = TXversion.Text;
            objCrlInfra.strObservaciones = TXobservaciones.Text;
            objCrlInfra.dtFechaModificacion = TXfechaUp.Text;
            objCrlInfra.dtFechaEliminacion = TXfechaDel.Text;
            objCrlInfra.dtFechaRegistro = Convert.ToDateTime(txtFechaMod.Text);
            objCrlInfra.intIdUsuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            string pathFile = string.Empty;
            int IdTipo = Convert.ToInt32(DDLtipo.SelectedValue);
            objCrlInfra.intIdTipoDocumento = IdTipo;
            clsVersionDocumentoBLL cCrlVersion = new clsVersionDocumentoBLL();
            if (fuArchivoPerfil.HasFile)
            {
                if (System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".pdf" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".docx" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".doc" || System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim() == ".xlsx")
                {
                    pathFile = "Ver." + TXversion.Text + fuArchivoPerfil.FileName;
                    Byte[] archivo = fuArchivoPerfil.FileBytes;
                    int length = Convert.ToInt32(fuArchivoPerfil.FileContent.Length);
                    string extension = System.IO.Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim();
                    saveFile(pathFile, length, archivo, Convert.ToInt32(txtId.Text), extension);
                    //mtdCargarArchivo();
                    objCrlInfra.strPathFIle = pathFile;

                    booResult = cCrlVersion.mtdUpdateVersion(objCrlInfra, ref strErrMsg, IdTipo);
                }
                else
                    omb.ShowMessage("Archivo sin guardar. Solo archivos en formato pdf,word,excel", 2, "Atención");
            }
            else
            {
                booResult = cCrlVersion.mtdUpdateVersion(objCrlInfra, ref strErrMsg, IdTipo);
            }


            if (booResult == true)
                strErrMsg = "Control del documento  actualizado exitosamente";
            else
                strErrMsg = "Error al actualizar control del Documento";
            return booResult;
        }

        private void saveFile(string NombreArchivo, int Length, byte[] archivo, int IdRegistro, string extension)
        {
            string strErrMsg = string.Empty;
            clsVersionDocumentoBLL cVersion = new clsVersionDocumentoBLL();

            if (!cVersion.mtdInsertarArchivo(NombreArchivo, Length, archivo, IdRegistro, ref strErrMsg, extension))
                omb.ShowMessage(strErrMsg, 1, "Atención");
        }

        private bool mtdInsertarActualizarControlDoc(ref string strErrMsg)
        {
            bool booResult = false;
            try
            {
                int outResult = 0;
                clsVersionDocumento objVersion = new clsVersionDocumento();
                if (ddlSubproceso.SelectedValue != "" && ddlSubproceso.SelectedValue != "0")
                {
                    objVersion.intIdMacroProceso = Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
                    objVersion.intIdTipoProceso = 3;
                }
                else
                {
                    if (ddlProceso.SelectedValue != "" && ddlProceso.SelectedValue != "0")
                    {
                        objVersion.intIdMacroProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                        objVersion.intIdTipoProceso = 2;
                    }
                    else
                    {
                        if (ddlMacroproceso.SelectedValue != "" && ddlMacroproceso.SelectedValue != "0")
                        {
                            objVersion.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                            objVersion.intIdTipoProceso = 1;
                        }
                    }
                }

                // Se valida si el documento se va insertar o a actualizar
                if (int.TryParse((string)Session["IdControl"], out outResult))
                    objVersion.intId = Convert.ToInt32(Session["IdControl"].ToString());

                objVersion.intIdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                objVersion.IdProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                objVersion.IdSubproceso = ddlSubproceso.SelectedValue.Equals("") ? 0 : Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
                objVersion.strCodigoDocumento = TXcodigoDoc.Text;
                objVersion.dtFechaImplementacion = TXfechaimplementacion.Text;
                objVersion.intIdTipoDocumento = Convert.ToInt32(DDLtipo.SelectedValue.ToString());
                objVersion.intidCargoResponsable = Convert.ToInt32(lblIdDependencia4.Text);
                objVersion.strUbicacionAlmacenamiento = TXalmace.Text;
                objVersion.strRecuperacion = TXrollback.Text;
                objVersion.strTiempoRetencionActivo = TXtiempoactivo.Text;
                objVersion.strTiempoRetencionInactivo = TXtiempoinactivo.Text;
                objVersion.strDisposicionFinal = Txdisposicion.Text;
                objVersion.strMedioSoporte = TXmedio.Text;
                objVersion.strFormato = TXformato.Text;
                objVersion.dtFechaRegistro = DateTime.Now;
                objVersion.intIdUsuario = Convert.ToInt32(Session["IdUsuario"]);
                objVersion.strNombreDocumento = TXname.Text;
                objVersion.dtFechaModificacion = TXfechaUp.Text;
                objVersion.dtFechaEliminacion = TXfechaDel.Text;
                objVersion.IdEstadoDocumento = Convert.ToInt32(ddlEstadoDocumento.SelectedValue);
                string pathFile = string.Empty;
                clsVersionDocumentoBLL cCrlVersion = new clsVersionDocumentoBLL();
                booResult = cCrlVersion.mtdInsertarActualizarControlDocumento(objVersion, ref strErrMsg);

                // Recupera el último Id de la tabla
                int LastId = cCrlVersion.mtdLastIdControlNoConformidad(ref strErrMsg);

                clsControlVersion version = new clsControlVersion();

                // Determina el documento al que va a adjuntar la versión
                if (int.TryParse((string)Session["IdControl"], out outResult))
                    version.intIdVersionDocumento = Convert.ToInt32(Session["IdControl"].ToString());
                else
                {
                    version.intIdVersionDocumento = LastId;
                }
                //--> Inicio Nuevos campos
                version.IdMacroProceso = Convert.ToInt32(ddlMacroproceso.SelectedValue.ToString());
                version.IdProceso = Convert.ToInt32(ddlProceso.SelectedValue.ToString());
                version.IdSubproceso = ddlSubproceso.SelectedValue.Equals("") ? 0 : Convert.ToInt32(ddlSubproceso.SelectedValue.ToString());
                version.CodigoDocumento = TXcodigoDoc.Text;
                version.CargoResponsable = Convert.ToInt32(lblIdDependencia4.Text);
                version.UbicacionAlmacemiento = TXalmace.Text;
                version.Recuperacion = TXrollback.Text;
                version.TiempoRetencionActivo = TXtiempoactivo.Text;
                version.TiempoRetencionInactivo = TXtiempoinactivo.Text;
                version.DisposionFinal = Txdisposicion.Text;
                version.MedioSoporte = TXmedio.Text;
                version.Formato = TXformato.Text;
                version.NombreDocumento = TXname.Text;
                version.IdTipoProceso = objVersion.intIdTipoProceso;
                version.FechaImplementacion = TXfechaimplementacion.Text;
                version.JustificacionCambios = txtJustificacion.Text;
                //Fin Nuevos campos <--
                version.strVersion = TXversion.Text;
                version.dtFechaModificacion = TXfechaUp.Text;
                version.dtFechaEliminacion = TXfechaDel.Text;
                version.strObservaciones = TXobservaciones.Text;
                version.strPathFIle = pathFile;
                version.dtFechaRegistro = DateTime.Now;
                version.intIdUsuario = Convert.ToInt32(Session["IdUsuario"]);
                version.intIdTipoDocumento = Convert.ToInt32(DDLtipo.SelectedValue.ToString());
                version.IdEstadoDocumento = Convert.ToInt32(ddlEstadoDocumento.SelectedValue);

                //Subir archivo
                if (fuArchivoPerfil.HasFile)
                {
                    pathFile = "Ver." + TXversion.Text + fuArchivoPerfil.FileName;
                    Byte[] archivo = fuArchivoPerfil.FileBytes;
                    int length = Convert.ToInt32(fuArchivoPerfil.FileContent.Length);
                    string extension = Path.GetExtension(fuArchivoPerfil.FileName).ToLower().ToString().Trim();
                    saveFile(pathFile, length, archivo, txtId.Text.Equals("") ? LastId : Convert.ToInt32(txtId.Text), extension);
                    version.strPathFIle = pathFile;
                }

                booResult = cCrlVersion.mtdInsertarControlVersion(version, ref strErrMsg);

                if (booResult == true)
                {
                    if (int.TryParse((string)Session["IdControl"], out outResult))
                        strErrMsg = "Versión registrada exitosamente";
                    else
                        strErrMsg = "Documento registrado exitosamente";
                }
                else
                {
                    strErrMsg = "Error al registrar el control";
                    booResult = false;
                }

                mtdEnviarNotificaciones(version.NombreDocumento, version.CodigoDocumento, version.JustificacionCambios);

                return booResult;
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error {ex.Message}", 1, "Error");
                booResult = false;
                return booResult;
            }
        }
        private bool mtdLoadControlVersion(ref string strErrMsg, string where)
        {
            #region Vars
            bool booResult = false;
            clsVersionDocumento objCrlVersion = new clsVersionDocumento();
            List<clsVersionDocumento> lstCrlVersion = new List<clsVersionDocumento>();
            clsVersionDocumentoBLL cCrtVersion = new clsVersionDocumentoBLL();
            #endregion Vars
            lstCrlVersion = cCrtVersion.mtdConsultarControlVersion(ref strErrMsg, ref lstCrlVersion, where);

            // Guarda la variable en sesión
            Session["lstCrlVersion"] = lstCrlVersion;

            if (lstCrlVersion != null)
            {
                mtdLoadControlVersion();
                mtdLoadControlVersion(lstCrlVersion);
                GVcontrolDocumento.DataSource = lstCrlVersion;
                GVcontrolDocumento.PageIndex = pagIndex;
                GVcontrolDocumento.DataBind();
                GVcontrolDocumento.Visible = true;
                booResult = true;
            }
            else
            {
                GVcontrolDocumento.DataSource = null;
                GVcontrolDocumento.DataBind();
                GVcontrolDocumento.Visible = false;
                omb.ShowMessage("No hay información para cargar", 2, "Atención");
                strErrMsg = "No hay información para cargar";
            }

            return booResult;
        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadControlVersion()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strNombreProceso", typeof(string));
            grid.Columns.Add("strNombreDocumento", typeof(string));
            grid.Columns.Add("dtFechaImplementacion", typeof(string));
            grid.Columns.Add("strCodigoDocumento", typeof(string));


            GVcontrolDocumento.DataSource = grid;
            GVcontrolDocumento.DataBind();
            InfoGrid = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadControlVersion(List<clsVersionDocumento> lstControl)
        {
            string strErrMsg = String.Empty;

            foreach (clsVersionDocumento objEvaComp in lstControl)
            {

                InfoGrid.Rows.Add(new Object[] {
                    objEvaComp.intId.ToString().Trim(),
                    objEvaComp.strNombreProceso.ToString().Trim(),
                    objEvaComp.strNombreDocumento.ToString().Trim(),
                    objEvaComp.dtFechaImplementacion.ToString().Trim(),
                    objEvaComp.strCodigoDocumento.ToString().Trim()
                    });
            }
        }

        private void mtdEnviarNotificaciones(string nombreDoc, string codigoDoc, string justificacion)
        {
            try
            {
                // Envia la notificación de acuerdo a la condicion especifica para cada caso
                switch (ddlEstadoDocumento.SelectedValue)
                {
                    case "1":
                        if (ddlEstadoDocumento.SelectedValue != string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value))
                            && string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value)) == "3")
                        {
                            string direccionCorreo = string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "CorreoResponsable" || x.Key == "CorreoUsuario").Select(o => o.Value));
                            if (direccionCorreo != string.Empty)
                                HelperCorreo.EnviarCorreo(direccionCorreo, $"<div>Buen día, <br /><br />El documento {nombreDoc.ToUpper()} con código [{codigoDoc}] que había sido devuelto ha sido cargado nuevamente. <br /><br />Observaciones: {justificacion}</div>", "Documento Cargado");
                        };
                        break;
                    case "2":
                        if (ddlEstadoDocumento.SelectedValue != string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value)))
                        {
                            string direccionCorreo = string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "CorreoUsuario").Select(o => o.Value));
                            if (direccionCorreo != string.Empty)
                                HelperCorreo.EnviarCorreo(direccionCorreo, $"<div>Buen día, <br /><br />El documento {nombreDoc.ToUpper()} con código [{codigoDoc}] cargado por usted ha sido aprobado. <br /><br />Observaciones: {justificacion}</div>", "Documento Aprobado");
                        };
                        break;
                    case "3":
                        if (ddlEstadoDocumento.SelectedValue != string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "EstadoAnterior").Select(o => o.Value)))
                        {
                            string direccionCorreo = string.Join(",", ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Where(x => x.Key == "CorreoUsuario").Select(o => o.Value));
                            if (direccionCorreo != string.Empty)
                                HelperCorreo.EnviarCorreo(direccionCorreo, $"<div>Buen día, <br /><br />El documento {nombreDoc.ToUpper()} con código [{codigoDoc}] cargado por usted ha sido devuelto. <br /><br />Observaciones: {justificacion} <br /><br />Por favor modifiquélo y carguélo nuevamente.</div>", "Documento Devuelto");
                        };
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GVcontrolDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Seleccionar":
                    mtdShowUpdate(RowGrid);
                    IBinsertGVC.Visible = false;
                    IBupdateGVC.Visible = true;
                    break;
            }
        }

        private void mtdShowUpdate(int RowGrid)
        {
            try
            {
                string strErrMsg = string.Empty;
                mtdCargarDDLs();
                GridViewRow row = GVcontrolDocumento.Rows[RowGrid];
                var colsNoVisible = GVcontrolDocumento.DataKeys[RowGrid].Values;
                BodyGridRNC.Visible = false;
                divFiltrosBusqueda.Visible = false;
                BodyFormCDV.Visible = true;
                #region DatosControl
                txtId.Text = row.Cells[0].Text;
                Session["IdControl"] = row.Cells[0].Text;

                // Se obtiene el documento actual
                List<clsVersionDocumento> lst = (List<clsVersionDocumento>)Session["lstCrlVersion"];
                clsVersionDocumento doc = new clsVersionDocumento();
                doc = lst.Find(item => item.intId == Convert.ToInt32(row.Cells[0].Text));

                // Se guardan lo datos anteriores necesarios para una posterior validación
                if (((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Count == 0)
                {
                    ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Add("CorreoResponsable", doc.CorreoResponsable);
                    ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Add("CorreoUsuario", doc.CorreoUsuario);
                    ((Dictionary<string, string>)Session["DatosAnterioresDocumento"]).Add("EstadoAnterior", doc.IdEstadoDocumento.ToString());
                }
                mtdLoadDDLMacroProceso(doc.IdCadenaValor);
                mtdLoadDDLProceso(doc.intIdMacroProceso);
                mtdLoadDDLSubproceso(doc.IdProceso);

                // llena el formulario
                if(ddlCadenaValor.Items.OfType<ListItem>().Any(x => x.Value == doc.IdCadenaValor.ToString()))
                ddlCadenaValor.SelectedValue = doc.IdCadenaValor.ToString();
                if(ddlMacroproceso.Items.OfType<ListItem>().Any(x => x.Value == doc.intIdMacroProceso.ToString()))
                ddlMacroproceso.SelectedValue = doc.intIdMacroProceso.ToString();
                if(ddlProceso.Items.OfType<ListItem>().Any(x => x.Value == doc.IdProceso.ToString()))
                ddlProceso.SelectedValue = doc.IdProceso.ToString();
                if(ddlSubproceso.Items.OfType<ListItem>().Any(x => x.Value == doc.IdSubproceso.ToString()))
                ddlSubproceso.SelectedValue = doc.IdSubproceso.ToString();
                TXname.Text = doc.strNombreDocumento;
                TXfechaimplementacion.Text = doc.dtFechaImplementacion;
                TXcodigoDoc.Text = doc.strCodigoDocumento;
                tbxUsuarioCreacion.Text = doc.strUsuario;
                txtFecha.Text = doc.dtFechaRegistro.ToString();
                tbxProcIndica.Text = doc.intIdMacroProceso.ToString();
                DDLtipo.SelectedValue = doc.intIdTipoDocumento.ToString();
                lblIdDependencia4.Text = doc.intidCargoResponsable.ToString();
                tbxResponsable.Text = doc.strNombreCargo;
                tbxResponsable.Enabled = true;
                imgDependencia4.Visible = true;
                TXalmace.Text = doc.strUbicacionAlmacenamiento;
                TXrollback.Text = doc.strRecuperacion;
                TXtiempoactivo.Text = doc.strTiempoRetencionActivo;
                TXtiempoinactivo.Text = doc.strTiempoRetencionInactivo;
                Txdisposicion.Text = doc.strDisposicionFinal;
                TXmedio.Text = doc.strMedioSoporte;
                TXformato.Text = doc.strFormato;
                trEstadoDocumento.Visible = true;
                trJustificacion.Visible = true;
                ddlEstadoDocumento.SelectedValue = doc.IdEstadoDocumento.ToString();
                #endregion
                #region Version Documento
                #region Vars

                //string strErrMsg = string.Empty;
                clsControlVersion objCrlVersion = new clsControlVersion();
                List<clsControlVersion> lstCrlVersion = new List<clsControlVersion>();
                clsVersionDocumentoBLL cCrtVersion = new clsVersionDocumentoBLL();
                #endregion Vars
                int IdVersionDocumento = Convert.ToInt32(row.Cells[0].Text);
                lstCrlVersion = cCrtVersion.mtdConsultarVersion(ref strErrMsg, ref lstCrlVersion, ref IdVersionDocumento);
                if (lstCrlVersion != null)
                {
                    mtdLoadVersion();
                    mtdLoadVersion(lstCrlVersion);
                    GVversiones.DataSource = lstCrlVersion;
                    GVversiones.PageIndex = pagIndexVersiones;
                    GVversiones.DataBind();
                    DVVersiones.Visible = true;
                }

                // Bloquea el aprobado si no es el responsable
                if (Session["IdJerarquia"].ToString() != lblIdDependencia4.Text)
                {
                    ListItem item = ddlEstadoDocumento.Items.FindByValue("2");
                    item.Attributes.Add("style", "display:none");
                }
                #endregion
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al modificar el perfil. {ex.Message}", 1);
            }

        }
        /// <summary>
        /// Carga la informacion de las columnas del grid
        /// </summary>
        private void mtdLoadVersion()
        {
            DataTable grid = new DataTable();

            grid.Columns.Add("intId", typeof(string));
            grid.Columns.Add("strVersion", typeof(string));
            grid.Columns.Add("strTipoDocumento", typeof(string));
            grid.Columns.Add("dtFechaModificacion", typeof(string));
            grid.Columns.Add("dtFechaEliminacion", typeof(string));
            grid.Columns.Add("strObservaciones", typeof(string));
            grid.Columns.Add("strPathFile", typeof(string));
            grid.Columns.Add("intBitActivo", typeof(string));
            GVversiones.DataSource = grid;
            GVversiones.DataBind();
            InfoGridVersiones = grid;
        }
        /// <summary>
        /// Realiza la carga de informacion en un datagrid.
        /// </summary>
        /// <param name="lstMacroproceso">Lista con los Indicadores</param>
        private void mtdLoadVersion(List<clsControlVersion> lstControl)
        {
            string strErrMsg = String.Empty;

            foreach (clsControlVersion objEvaComp in lstControl)
            {

                InfoGridVersiones.Rows.Add(new Object[] {
                    objEvaComp.intId.ToString().Trim(),
                    objEvaComp.strVersion.ToString().Trim(),
                    objEvaComp.strTipoDocumento.ToString().Trim(),
                    objEvaComp.dtFechaModificacion.ToString().Trim(),
                    objEvaComp.dtFechaEliminacion.ToString().Trim(),
                    objEvaComp.strObservaciones.ToString().Trim(),
                    objEvaComp.strPathFIle.ToString().Trim(),
                    objEvaComp.intbitActivo.ToString().Trim()
                    });
            }
        }

        protected void GVversiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RowGrid = Convert.ToInt16(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Descargar":
                    mtdDownloadFile(RowGrid);
                    break;
            }
        }
        public void mtdDownloadFile(int RowGrid)
        {
            GridViewRow row = GVversiones.Rows[RowGrid];
            var colsNoVisible = GVversiones.DataKeys[RowGrid].Values;
            int IdRegistro = Convert.ToInt32(txtId.Text);
            int bitActivo = Convert.ToInt32(colsNoVisible[0].ToString());
            string archivo = ((Label)row.FindControl("archivo")).Text;
            clsVersionDocumentoBLL cCrtVersion = new clsVersionDocumentoBLL();
            string strErrMsg = string.Empty;
            if (bitActivo == 1)
            {
                /*if (DDLtipo.SelectedValue == "1")
                {*/
                string extension = cCrtVersion.mtdDownLoadFileData(ref strErrMsg, ref IdRegistro, archivo);
                if (extension != "")
                {
                    Byte[] file = cCrtVersion.mtdDownLoadFile(ref strErrMsg, ref IdRegistro, archivo);
                    extension = extension.Remove(0, 1);
                    //System.IO.File.WriteAllBytes(archivo, file);
                    /*Response.Clear();
                    Response.AddHeader("content-disposition", "at‌​tachment;filename=" + archivo);
                    if(extension == "pdf")
                        Response.ContentType = "application/pdf";
                    if(extension == "doc" || extension == "docx")
                        Response.ContentType = "application/ms-word";
                    if (extension == "xlsx" || extension == "xls")
                        Response.ContentType = "application/vnd.xls";
                    Response.BinaryWrite(file);
                    Response.End();*/
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("content-disposition", "attachment; filename=" + archivo);
                    //Set the content type as file extension type
                    Response.ContentType = extension;
                    //Write the file content
                    this.Response.BinaryWrite(file);
                    this.Response.End();
                }
                else
                {
                    omb.ShowMessage("No hay documentos para descargar", 2, "Atención");
                }
                /*}else
                {
                    omb.ShowMessage("Solo el tipo 'Documento' puede realizar descarga", 2, "Atención");
                }*/
            }
            else
            {
                omb.ShowMessage("Archivo no habilitado usa la ultima versión", 2, "Atención");
            }
        }

        protected void btnImgCancelar_Click(object sender, ImageClickEventArgs e)
        {
            mtdResetFields();
            mtdStard();
        }

        protected void GVcontrolDocumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndex = e.NewPageIndex;
            GVversiones.PageIndex = PagIndex;
            GVversiones.DataBind();
            string strErrMsg = "";
            //mtdLoadControlVersion(ref strErrMsg);
        }

        protected void DDLtipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLtipo.SelectedValue.ToString() == "3")
            {
                RFVfechaDel.Enabled = true;
                //RFVfechaDel2.Enabled = true;
            }
            else
            {
                RFVfechaDel.Enabled = false;
                //RFVfechaDel2.Enabled = false;
            }
        }

        protected void ddlCadenaValorFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMacroprocesoFiltro.Items.Clear();

            if (mtdLoadDDLMacroProceso(Convert.ToInt32(ddlCadenaValorFiltro.SelectedValue)))
            {
                ddlMacroprocesoFiltro.ClearSelection();
                ddlProcesoFiltro.Items.Clear();

            };
        }

        protected void ddlMacroprocesoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProcesoFiltro.Items.Clear();

            if (mtdLoadDDLProceso(Convert.ToInt32(ddlMacroprocesoFiltro.SelectedValue)))
            {
                ddlProcesoFiltro.ClearSelection();
            }
        }

        protected void ddlProcesoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSubprocesoFiltro.Items.Clear();

            if (mtdLoadDDLSubproceso(Convert.ToInt32(ddlProcesoFiltro.SelectedValue)))
            {
                ddlSubprocesoFiltro.ClearSelection();
                if (ddlSubproceso.Items.Count == 1)
                {
                    omb.ShowMessage("No hay información de Subprocesos", 2, "Atención");
                }
            }
        }

        protected void btnBusquedaFiltro_Click(object sender, ImageClickEventArgs e)
        {
            string strErrMsg = string.Empty;

            try
            {
                Dictionary<string, string> filtrosBusqueda = new Dictionary<string, string>
            {
                { "[IdCadenaValor]", ddlCadenaValorFiltro.SelectedValue },
                { "[IdMacroProceso]", ddlMacroprocesoFiltro.SelectedValue },
                { "[IdProceso]", ddlProcesoFiltro.SelectedValue },
                { "[IdSubproceso]", ddlSubprocesoFiltro.SelectedValue},
                { "UPPER([NombreDocumento])", txtNombreFiltro.Text },
                { "UPPER([CodigoDocumento])", txtCodigoFiltro.Text },
                { "[FechaImplementacion]", txtFechaImplementacionFiltro.Text },
                { "[IdTipoDocumento]", ddlTipoDocumentoFiltro.SelectedValue }
            };

                string where = "WHERE " + string.Join(" AND ", filtrosBusqueda.Where(o => o.Value != string.Empty && o.Value != "0").Select(o => $"cast(UPPER({ o.Key }) as varchar(32)) = '{ o.Value }'"));

                if (where == "WHERE ")
                    where = string.Empty;

                Session["FiltroActual"] = where;

                mtdLoadControlVersion(ref strErrMsg, where);
            }
            catch (Exception ex)
            {
                omb.ShowMessage($"Error al cargar los documentos: {ex.Message}", 1, "Error");
            }
        }

        protected void btnLimpiarFiltro_Click(object sender, ImageClickEventArgs e)
        {
            ddlCadenaValorFiltro.ClearSelection();
            ddlMacroprocesoFiltro.ClearSelection();
            ddlProcesoFiltro.ClearSelection();
            txtNombreFiltro.Text = string.Empty;
            txtCodigoFiltro.Text = string.Empty;
            txtFechaImplementacionFiltro.Text = string.Empty;
            ddlTipoDocumentoFiltro.ClearSelection();
            GVcontrolDocumento.DataSource = null;
            GVcontrolDocumento.DataBind();
        }
    }
}