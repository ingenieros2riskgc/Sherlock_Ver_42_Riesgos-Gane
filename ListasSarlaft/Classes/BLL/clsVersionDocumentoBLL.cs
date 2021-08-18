using ListasSarlaft.Classes.DTO.Calidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsVersionDocumentoBLL
    {
        /// <summary>
        /// Metodo para insertar el control del documento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarActualizarControlDocumento(clsVersionDocumento cCrlVersion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtControlDocumento cDtCrlVersion = new clsDtControlDocumento();

            booResult = cDtCrlVersion.mtdInsertarActualizarControlDocumento(cCrlVersion, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la no corformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdControlNoConformidad(ref string strErrMsg)
        {
            int LastId = 0;
            clsDtControlDocumento cDtControl = new clsDtControlDocumento();
            DataTable dt = cDtControl.mtdLastIdControlVersion(ref strErrMsg);
            foreach (DataRow dr in dt.Rows)
            {
                string a = dr["LastId"].ToString();
                LastId = dr["LastId"].ToString().Equals("") ? 1 : Convert.ToInt32(dr["LastId"].ToString());
            }
            return LastId;
        }
        /// <summary>
        /// Metodo para insertar el control del documento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarControlVersion(clsControlVersion cCrlVersion, ref string strErrMsg)
        {
            bool booResult = false;
            clsDtControlDocumento cDtCrlVersion = new clsDtControlDocumento();

            booResult = cDtCrlVersion.mtdInsertarControlVersion(cCrlVersion, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para insertar el control del documento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarArchivo(string nombrearchivo, int length, byte[] archivo, int IdRegistro, ref string strErrMsg, string extension)
        {
            bool booResult = false;
            clsDtControlDocumento cDtCrlVersion = new clsDtControlDocumento();
            try
            {
                booResult = cDtCrlVersion.Guardar(nombrearchivo, length, archivo, IdRegistro, ref strErrMsg, extension);
            }
            catch (Exception ex)
            {
                strErrMsg = "nombrearchivo: " + nombrearchivo + " length: " + length + " IdRegistro:" + " extension:" + extension;
            }
            return booResult;
        }
        /// <summary>
        /// Metodo que permite consultar todos los Controles de la Version
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsVersionDocumento> mtdConsultarControlVersion(ref string strErrMsg, ref List<clsVersionDocumento> lstCrlVersion, string where)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtControlDocumento cDtControl = new clsDtControlDocumento();

            booResult = cDtControl.mtdConsultarControlVersion(ref dtInfo, ref strErrMsg, where);
            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsVersionDocumento objControl = new clsVersionDocumento();
                            objControl.intId = Convert.ToInt32(dr["Id"].ToString());
                            objControl.intIdMacroProceso = Convert.ToInt32(dr["IdMacroProceso"].ToString());
                            objControl.strNombreProceso = dr["Nombre"].ToString();
                            objControl.IdProceso = dr["IdProceso"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["IdProceso"].ToString());
                            objControl.NombreProceso = dr["NombreProceso"].ToString().Equals("") ? "Sin Proceso asociado" : dr["NombreProceso"].ToString();
                            objControl.IdSubproceso = dr["IdSubproceso"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["IdSubproceso"].ToString());
                            objControl.NombreSubproceso = dr["NombreSubproceso"].ToString();
                            objControl.strCodigoDocumento = dr["CodigoDocumento"].ToString();
                            objControl.IdCadenaValor = dr["IdCadenaValor"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["IdCadenaValor"].ToString());
                            objControl.NombreCadenaValor = dr["NombreCadenaValor"].ToString();
                            objControl.dtFechaImplementacion = dr["FechaImplementacion"].ToString();
                            objControl.intIdTipoDocumento = Convert.ToInt32(dr["IdTipoDocumento"].ToString());
                            objControl.strNombreCargo = dr["NombreHijo"].ToString();
                            objControl.intidCargoResponsable = Convert.ToInt32(dr["CargoResponsable"].ToString());
                            objControl.strUbicacionAlmacenamiento = dr["UbicacionAlmacemiento"].ToString();
                            objControl.strRecuperacion = dr["Recuperacion"].ToString();
                            objControl.strTiempoRetencionActivo = dr["TiempoRetencionActivo"].ToString();
                            objControl.strTiempoRetencionInactivo = dr["TiempoRetencionInactivo"].ToString();
                            objControl.strDisposicionFinal = dr["DisposionFinal"].ToString();
                            objControl.strMedioSoporte = dr["MedioSoporte"].ToString();
                            objControl.strFormato = dr["Formato"].ToString();
                            objControl.dtFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);
                            objControl.intIdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            objControl.strUsuario = dr["Usuario"].ToString();
                            objControl.strNombreDocumento = dr["NombreDocumento"].ToString();
                            objControl.intIdTipoProceso = Convert.ToInt32(dr["IdTipoProceso"].ToString().Trim());
                            objControl.IdEstadoDocumento = dr["IdEstadoDocumento"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr["IdEstadoDocumento"].ToString().Trim());
                            objControl.CorreoResponsable = dr["CorreoResponsable"].ToString();
                            objControl.CorreoUsuario = dr["CorreoUsuario"].ToString();
                            objControl.NombreTipoDocumento = dr["NombreTipoDocumento"].ToString();
                            lstCrlVersion.Add(objControl);
                        }
                    }
                    else
                        lstCrlVersion = null;
                }
                else
                    lstCrlVersion = null;
            }
            return lstCrlVersion;
        }
        /// <summary>
        /// Metodo que permite consultar todos los Controles de la Version
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsControlVersion> mtdConsultarVersion(ref string strErrMsg, ref List<clsControlVersion> lstCrlVersion, ref int IdVersionDocumento)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtControlDocumento cDtControl = new clsDtControlDocumento();

            booResult = cDtControl.mtdConsultarVersion(ref dtInfo, ref strErrMsg, ref IdVersionDocumento);
            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsControlVersion objControl = new clsControlVersion();
                            objControl.intId = Convert.ToInt32(dr["Id"].ToString());
                            objControl.strVersion = dr["Version"].ToString();
                            objControl.dtFechaModificacion = dr["FechaModificacion"].ToString();
                            objControl.dtFechaEliminacion = dr["FechaEliminacion"].ToString();
                            objControl.strObservaciones = dr["Observaciones"].ToString();
                            objControl.strPathFIle = dr["pathFile"].ToString();
                            objControl.intbitActivo = Convert.ToInt32(dr["bitActivo"].ToString());
                            string a = dr["IdTipoDocumento"].ToString();
                            if (dr["IdTipoDocumento"].ToString() == "1")
                                objControl.strTipoDocumento = "Documento";
                            if (dr["IdTipoDocumento"].ToString() == "2")
                                objControl.strTipoDocumento = "Registro";
                            if (dr["IdTipoDocumento"].ToString() == "3")
                                objControl.strTipoDocumento = "Eliminación";
                            if (dr["IdTipoDocumento"].ToString() == "4")
                                objControl.strTipoDocumento = "Procedimiento";
                            if (dr["IdTipoDocumento"].ToString() == "5")
                                objControl.strTipoDocumento = "Politica";
                            if (dr["IdTipoDocumento"].ToString() == "6")
                                objControl.strTipoDocumento = "Manual";
                            if (dr["IdTipoDocumento"].ToString() == "7")
                                objControl.strTipoDocumento = "Instructivo";
                            if (dr["IdTipoDocumento"].ToString() == "8")
                                objControl.strTipoDocumento = "Reglamento";
                            if (dr["IdTipoDocumento"].ToString() == "9")
                                objControl.strTipoDocumento = "Formato";
                            if (dr["IdTipoDocumento"].ToString() == "10")
                                objControl.strTipoDocumento = "Circular";
                            objControl.JustificacionCambios = dr["JustificacionCambios"].ToString();
                            objControl.Nombres = dr["Nombres"].ToString();
                            lstCrlVersion.Add(objControl);
                        }
                    }
                    else
                        lstCrlVersion = null;
                }
                else
                    lstCrlVersion = null;
            }
            return lstCrlVersion;
        }
        /// <summary>
        /// Metodo para actualizar el Control de Infraestructura
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdUpdateVersion(clsControlVersion cCrlInfra, ref string strErrMsg, int IdDocumento)
        {
            bool booResult = false;
            clsDtControlDocumento cDtCrlInfra = new clsDtControlDocumento();

            booResult = cDtCrlInfra.mtdUpdateVersion(cCrlInfra, ref strErrMsg, IdDocumento);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite consultar todos los Controles de la Version
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public byte[] mtdDownLoadFile(ref string strErrMsg, ref int IdVersionDocumento, string filename)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsDtControlDocumento cDtControl = new clsDtControlDocumento();
            byte[] file = null;
            booResult = cDtControl.mtdDownLoadFile(ref dtInfo, ref strErrMsg, ref IdVersionDocumento, filename);
            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            file = (byte[])dr["archivo"];
                        }
                    }
                    else
                        file = null;
                }
                else
                    file = null;
            }
            return file;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la no corformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public string mtdDownLoadFileData(ref string strErrMsg, ref int IdVersionDocumento, string filename)
        {
            bool booResult = false;
            string ext = string.Empty;
            DataTable dtInfo = new DataTable();
            clsDtControlDocumento cDtControl = new clsDtControlDocumento();
            DataTable dt = cDtControl.mtdDownLoadFileData(ref dtInfo, ref strErrMsg, ref IdVersionDocumento, filename);
            foreach (DataRow dr in dt.Rows)
            {
                ext = dr["extension"].ToString();
            }
            return ext;
        }

        public List<EstadoDocumento> opcionesEstadoDocumento()
        {
            try
            {
                using (clsDtControlDocumento objData = new clsDtControlDocumento())
                {
                    return objData.opcionesEstadoDocumento();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ObtenerDireccionCorreo(int idJerarquia)
        {
            try
            {
                using (clsDtControlDocumento objData = new clsDtControlDocumento())
                {
                    return objData.ObtenerDireccionCorreo(idJerarquia);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}