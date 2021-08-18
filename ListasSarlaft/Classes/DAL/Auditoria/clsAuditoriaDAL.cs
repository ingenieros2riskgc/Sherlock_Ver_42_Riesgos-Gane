using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsAuditoriaDAL
    {
        public bool mtdInsertarAuditoria(clsAuditoriaDTO objEnc, ref string strErrMsg, decimal consecutivo, string area)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("INSERT INTO [Auditoria].[Auditoria] ([Tema], [IdEstandar], [Tipo], [FechaRegistro], [IdPlaneacion], [IdDependencia], " +
                    "[IdProceso], [IdUsuario], [IdEmpresa], [Estado], [Recursos], [Objetivo], [Alcance], [NivelImportancia], [IdDetalleTipo_TipoNaturaleza], " +
                    "[FechaInicio], [FechaCierre], [IdTipoProceso],[IdMesEjecucion],[SemanaEjecucion],[Especial],[Periodicidad],[codigoAuditoria],[idArea]) " +
                    "VALUES ('{0}',{1},'{2}',(CONVERT(SMALLDATETIME,'{3}',101)),{4},{5},{6},{7},{8},'{9}','{10}','{11}','{12}','{13}',"+
                    "{14},(CONVERT(SMALLDATETIME,'{15}',101)),(CONVERT(SMALLDATETIME,'{16}',101)),{17},{18},'{19}','{20}',{21},'{22}',{23})",
                    objEnc.strTema, objEnc.intIdEstandar, objEnc.strTipo, objEnc.strFechaRegistro, objEnc.intIdPlaneacion, objEnc.intIdDependencia, objEnc.intIdProceso, objEnc.intIdUsuario,
                    objEnc.intIdEmpresa, objEnc.strEstado, objEnc.strRecursos, objEnc.strObjetivo, objEnc.strAlcance, objEnc.strNivelImportancia, objEnc.intIdDetalle_TipoNaturaleza,
                    objEnc.strFechaInicio, objEnc.strFechaCierre, objEnc.intIdTipoProceso, objEnc.intIdMesExe, objEnc.strSemanaExe, objEnc.strEspecial,objEnc.intPeriodicidad,
                    objEnc.strCodigoAuditoria,objEnc.intIdArea);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);

                strConsulta = string.Format("UPDATE [Auditoria].[AuditoriaCodigos] "+
                    "SET [consecutivo] = {0} where area = '{1}'",consecutivo,area);
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Auditoria. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public bool mtdInsertarAuditoriaProceso(int IdAuditoria, int IdProceso, int IdTipoProceso, ref string strErrMsg)
        {
            #region Vars
            bool booResult = false;
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            #endregion Vars

            try
            {

                strConsulta = string.Format("INSERT INTO [Auditoria].[AuditoriaProceso] ([IdProceso],[IdTipoProceso],[IdAuditoria])"
                    + "VALUES({1},{2},{0})", IdAuditoria, IdProceso, IdTipoProceso);

                cDatabase.conectar();
                cDatabase.ejecutarQuery(strConsulta);
                booResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al crear la Auditoria. [{0}]", ex.Message);
                booResult = false;
            }
            finally
            {
                cDatabase.desconectar();
            }

            return booResult;
        }
        public DataTable mtdLastIdAuditoria(ref string strErrMsg)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT MAX(IdAuditoria) LastId FROM [Auditoria].[Auditoria]");

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar el ID de la Auditoria. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
    }
}