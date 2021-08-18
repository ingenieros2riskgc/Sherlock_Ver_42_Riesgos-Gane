using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using ListasSarlaft.Classes;
using System.Configuration;
using ListasSarlaft.Classes.BLL.Auditoria;
using ListasSarlaft.Classes.DAL.Auditoria.Indicadores;
using ClosedXML.Excel;
using System.IO;

namespace ListasSarlaft.UserControls.MAuditoria.Indicadores
{
    public partial class IndicadorSeguimentoRecomendacion : System.Web.UI.UserControl
    {
        string IdFormulario = "3012";
        cCuenta cCuenta = new cCuenta();
        cAuditoria cAu = new cAuditoria();
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
        private static int LastInsertIdCE;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cCuenta.permisosConsulta(IdFormulario) == "False")
                Response.Redirect("~/Formularios/Sarlaft/Admin/HomeAdmin.aspx?Denegar=1");
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            SqlDataSource8.SelectParameters["IdArea"].DefaultValue = Session["IdAreaUser"].ToString();
        }
        #region DataTable
        private void loadGridReporteRiesgos()
        {
            DataTable grid = new DataTable();

            #region Columnas GRID
            grid.Columns.Add("I-P", typeof(string));
            grid.Columns.Add("No.AUD", typeof(string));
            grid.Columns.Add("(MM/DD/AAAA)", typeof(string));
            grid.Columns.Add("MES", typeof(string));
            grid.Columns.Add("EDAD", typeof(string));
            grid.Columns.Add("HALLAZGO", typeof(string));
            grid.Columns.Add("RECOMENDACIÓN", typeof(string));
            grid.Columns.Add("ÁREA", typeof(string));
            grid.Columns.Add("DEPARTAMENTO", typeof(string));
            grid.Columns.Add("RIESGO", typeof(string));
            #endregion Columnas GRID

            //Fin Ajuetes 01
            InfoGridAuditorias = grid;
            GridView1.DataSource = InfoGridAuditorias;
            GridView1.DataBind();
        }
        private void loadInfoReporteRiesgos(DataTable dtInfo)
        {
            if (dtInfo.Rows.Count > 0)
            {
                #region Recorrido para llenar informacion
                for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                {
                    InfoGridAuditorias.Rows.Add(new Object[] {
                        dtInfo.Rows[rows]["Especial"].ToString().Trim(),
                        dtInfo.Rows[rows]["ReferenciaInforme"].ToString().Trim()+" - "+dtInfo.Rows[rows]["Tema"].ToString().Trim(),
                        dtInfo.Rows[rows]["fechaAprobacion"].ToString().Trim(),
                        mtdMesEjecuccion(dtInfo.Rows[rows]["IdMesEjecucion"].ToString().Trim()),
                        dtInfo.Rows[rows]["edadRecomendacion"].ToString().Trim(),
                        dtInfo.Rows[rows]["Hallazgo"].ToString().Trim(),
                        dtInfo.Rows[rows]["recomendacion"].ToString().Trim(),
                        "",
                        "",
                        ""
                        });
                }
                #endregion Recorrido para llenar informacion

                GridView1.PageIndex = PagIndexInfoGridAuditoria;
                GridView1.DataSource = InfoGridAuditorias;
                GridView1.DataBind();
            }
            else
            {
                loadGridReporteRiesgos();
                omb.ShowMessage("Error en el momento de la consulta", 3, "Atención");
            }
        }

        public string mtdMesEjecuccion(string idMes)
        {
            string mes = string.Empty;
            if (idMes == "1")
                mes = "ENERO";
            if (idMes == "2")
                mes = "FEBRERO";
            if (idMes == "3")
                mes = "MARZO";
            if (idMes == "4")
                mes = "ABRIL";
            if (idMes == "5")
                mes = "MAYO";
            if (idMes == "6")
                mes = "JUNIO";
            if (idMes == "7")
                mes = "JULIO";
            if (idMes == "8")
                mes = "AGOSTO";
            if (idMes == "9")
                mes = "SEPTIEMBRE";
            if (idMes == "10")
                mes = "OCTUBRE";
            if (idMes == "11")
                mes = "NOMVIEMBRE";
            if (idMes == "12")
                mes = "DICIEMBRE";
            return mes;
        }
        #endregion DataTable
        #region Grids
        protected void GridView8_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNomPlaneacion.Text = GridView8.SelectedRow.Cells[1].Text.Trim();
            txtCodPlaneacion.Text = GridView8.SelectedRow.Cells[0].Text.Trim();
            PagIndexInfoGridAuditoria = 0;
            clsSeguimientoRecomendacionDAL dbSegRec = new clsSeguimientoRecomendacionDAL();
            string strErrmsg = string.Empty;
            DataTable dtInfo = dbSegRec.mdtIndicadorSeguimientoRecomendacion(Int32.Parse(GridView8.SelectedRow.Cells[0].Text.Trim()),ref strErrmsg);
            loadGridReporteRiesgos();
            loadInfoReporteRiesgos(dtInfo);
            
        }
        
        
        
        
        #endregion
        #region eventosPopup
        protected void btnImgok_Click(object sender, EventArgs e)
        {
            /*bool err = false;
            string TextoAdicional = "", selectCommand;
            int nodoAuditoria = 0;

            mpeMsgBox.Hide();

            if (lblAccion.Text == "REVERTIR")
            {
                try
                {
                    SqlDataSource2.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                    SqlDataSource2.Update();
                    cAu.ActualizarLogHistoricoAudutoria(txtCodAuditoriaSel.Text, "NULL", "GETDATE()", "NULL", "SI");
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización del estado." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

                if (!err)
                {
                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");

                    //Trae el nodo del grupo de Auditoria
                    string conString = WebConfigurationManager.ConnectionStrings["SarlaftConnectionString"].ConnectionString;
                    selectCommand = "SELECT JO.idHijo FROM [Parametrizacion].[JerarquiaOrganizacional] AS JO WHERE JO.TipoArea = 'A'";
                    SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
                    DataTable dtblDiscuss = new DataTable();
                    dad.Fill(dtblDiscuss);

                    DataView view = new DataView(dtblDiscuss);

                    foreach (DataRowView row in view)
                    {
                        nodoAuditoria = Convert.ToInt32(row["idHijo"].ToString().Trim());
                    }

                    TextoAdicional = "Planeación Código: " + txtCodPlaneacion.Text + ", Nombre: " + txtNomPlaneacion.Text + "<br>";
                    TextoAdicional = TextoAdicional + "Auditoría Código: " + txtCodAuditoriaSel.Text + ", Nombre: " + txtNomAuditoriaSel.Text + "<div><br></div>";

                    boolEnviarNotificacion(1, Convert.ToInt32(txtCodAuditoriaSel.Text.Trim()), nodoAuditoria, "", TextoAdicional);
                }
            }
            else if (lblAccion.Text == "CERRAR")
            {
                try
                {
                    SqlDataSource26.UpdateParameters["IdAuditoria"].DefaultValue = txtCodAuditoriaSel.Text;
                    SqlDataSource26.UpdateParameters["Estado"].DefaultValue = "CUMPLIDA";
                    SqlDataSource26.Update();
                    cAu.CerrarLogHistoricoAudutoria(txtCodAuditoriaSel.Text, "NULL", "NULL", "NULL", "NO", "GETDATE()");

                    omb.ShowMessage("La información se actualizó con éxito en la Base de Datos.", 3, "Atención");
                }
                catch (Exception except)
                {
                    omb.ShowMessage("Error en la actualización del estado de la auditoría." + "<br/>" + "Descripción: " + except.Message.ToString().Trim(), 1, "Atención");
                    err = true;
                }

            }

            if (!err)
            {
                txtNomPlaneacion.Text = "";
                txtCodPlaneacion.Text = "";
                txtCodAuditoriaSel.Text = "";
                txtCodObjetivoSel.Text = "";
                txtCodEnfoqueSel.Text = "";
                txtCodLiteralSel.Text = "";
                txtCodHallazgoSel.Text = "";
                txtNomAuditoriaSel.Text = "";
                txtNomObjetivoSel.Text = "";
                txtNomEnfoqueSel.Text = "";
                txtNomLiteralSel.Text = "";
                txtNomHallazgoSel.Text = "";
                txtNomEnfoqueSel.Height = 18;
                txtNomEnfoqueSel.Width = 402;
                txtNomLiteralSel.Height = 18;
                txtNomLiteralSel.Width = 402;
                txtNomHallazgoSel.Height = 18;
                txtNomHallazgoSel.Width = 402;
                filaGridRec.Visible = false;
                filaCierreRec.Visible = false;
                GridView8.DataBind();
                imgBtnAuditoria.Focus();
            }*/
        }
        #endregion eventosPopup
        protected void imgBtnAuditoria_Click(object sender, ImageClickEventArgs e) { }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PagIndexInfoGridAuditoria = e.NewPageIndex;
            GridView1.PageIndex = PagIndexInfoGridAuditoria;
            GridView1.DataSource = InfoGridAuditorias;
            GridView1.DataBind();
        }

        protected void ImButtonExcelExport_Click(object sender, ImageClickEventArgs e)
        {
            XLWorkbook workbook = new XLWorkbook();
            workbook.Worksheets.Add(InfoGridAuditorias);
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"IndicadoresSeguimientoRecomendacion.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }
    }
}