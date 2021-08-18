using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsRegistroAuditoriaBLL
    {
        /// <summary>
        /// Metodo para insertar el Registro de No conformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarRegistroAuditoria(clsRegistroAuditoriaDTO cAuditoria, ref string strErrMsg)
        {
            bool booResult = false;
            clsRegistroAuditoriaDAL cDtAuditoria = new clsRegistroAuditoriaDAL();

            booResult = cDtAuditoria.mtdInsertarRegistroAuditoria(cAuditoria, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para insertar el Registro de No conformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdInsertarAuditoriaProceso(int IdAuditoria, int IdProceso, int IdTipoProceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsRegistroAuditoriaDAL cDtAuditoria = new clsRegistroAuditoriaDAL();

            booResult = cDtAuditoria.mtdInsertarAuditoriaProceso(IdAuditoria, IdProceso, IdTipoProceso, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo que permite tomar le ultimo id de la no corformidad
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public int mtdLastIdAuditoria(ref string strErrMsg)
        {
            int LastId = 0;
            clsRegistroAuditoriaDAL cDtAuditoria = new clsRegistroAuditoriaDAL();
            DataTable dt = cDtAuditoria.mtdLastIdAuditoria(ref strErrMsg);
            foreach (DataRow dr in dt.Rows)
            {
                LastId = Convert.ToInt32(dr["LastId"].ToString());
            }
            return LastId;
        }
        /// <summary>
        /// Metodo que permite consultar todos los registros de la auditoria
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsRegistroAuditoriaDTO> mtdConsultarRegistrosAuditoria(ref string strErrMsg, ref List<clsRegistroAuditoriaDTO> lstRegistros, int IdAuditoria
            , int CodAuditoria, string NombreAud, int IdEstandar, int IdArea)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsRegistroAuditoriaDAL cDtRegistros = new clsRegistroAuditoriaDAL();

            booResult = cDtRegistros.mtdConsultarRegistrosAuditoria(ref dtInfo, ref strErrMsg, IdAuditoria,  CodAuditoria,  NombreAud,  IdEstandar, IdArea);
            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsRegistroAuditoriaDTO objReau = new clsRegistroAuditoriaDTO();
                            //objReau.intIdRegistroAuditoria = Convert.ToInt32(dr["IdRegistroAuditoria"].ToString().Trim());
                            objReau.strCodigoAuditoria = dr["codigoAuditoria"].ToString().Trim();
                            objReau.intIdAuditoria = Convert.ToInt32(dr["IdAuditoria"].ToString().Trim());
                            objReau.strTema = dr["Tema"].ToString().Trim();
                            objReau.intIdEstandar = Convert.ToInt32(dr["IdEstandar"].ToString().Trim());
                            objReau.strEstandar = dr["Nombre"].ToString().Trim();
                            objReau.intIdPlaneacion = Convert.ToInt32( dr["IdPlaneacion"].ToString().Trim());
                            objReau.strTipo = dr["Tipo"].ToString().Trim();
                            objReau.intIdDependencia = Convert.ToInt32(dr["IdDependencia"].ToString().Trim());
                            objReau.intIdProceso = Convert.ToInt32(dr["IdProceso"].ToString().Trim());
                            objReau.strNombreDP = dr["NombreDP"].ToString().Trim();
                            objReau.strFechaRegistro = dr["FechaRegistro"].ToString().Trim();
                            objReau.intIdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            objReau.strUser = dr["Usuario"].ToString().Trim();
                            objReau.intIdEmpresa = Convert.ToInt32(dr["IdEmpresa"].ToString().Trim());
                            objReau.strRecursos = dr["Recursos"].ToString().Trim();
                            objReau.strObjetivo = dr["Objetivo"].ToString().Trim();
                            objReau.strAlcance = dr["Alcance"].ToString().Trim();
                            objReau.strNivelImportancia = dr["NivelImportancia"].ToString().Trim();
                            objReau.intIdDetalle_TipoNaturaleza = Convert.ToInt32(dr["IdDetalleTipo_TipoNaturaleza"].ToString().Trim());
                            objReau.strFechaInicio = dr["FechaInicio"].ToString().Trim();
                            objReau.strFechaCierre = dr["FechaCierre"].ToString().Trim();
                            objReau.strEspecial = dr["Especial"].ToString().Trim();
                            objReau.intIdMesExe = Convert.ToInt32(dr["IdMesEjecucion"].ToString().Trim());
                            string semana = dr["SemanaEjecucion"].ToString().Trim().TrimEnd(',');
                            objReau.strSemanaExe = semana;
                            if (dr["periodicidad"].ToString().Trim() != "")
                                objReau.intPeriodicidad = Convert.ToInt32(dr["periodicidad"].ToString().Trim());
                            else
                                objReau.intPeriodicidad = 0;

                            lstRegistros.Add(objReau);
                        }
                    }
                    else
                    {
                        lstRegistros = null;
                        strErrMsg = "No hay datos registrados";
                    }
                        
                }
                else
                {
                    lstRegistros = null;
                    strErrMsg = "No hay datos registrados";
                }
                    
            }
            return lstRegistros;
        }
        /// <summary>
        /// Metodo para actualizar el registro de Auditoria
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarRegistroAuditoria(clsRegistroAuditoriaDTO cAuditoria, ref string strErrMsg)
        {
            bool booResult = false;
            clsRegistroAuditoriaDAL cDtAuditoria = new clsRegistroAuditoriaDAL();

            booResult = cDtAuditoria.mtdActualizarRegistroAuditoria(cAuditoria, ref strErrMsg);

            return booResult;
        }
        /// <summary>
        /// Metodo para actualizar el Registro
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public bool mtdActualizarAuditoriaProceso(int IdAuditoria, int IdProceso, int IdTipoProceso, ref string strErrMsg)
        {
            bool booResult = false;
            clsRegistroAuditoriaDAL cDtAuditoria = new clsRegistroAuditoriaDAL();

            booResult = cDtAuditoria.mtdActualizarAuditoriaProceso(IdAuditoria, IdProceso, IdTipoProceso, ref strErrMsg);

            return booResult;
        }
    }
}