using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsSeguimientoRecomendacionesDAL
    {
        public bool mtdConsultarAreasResponsables(ref DataTable dtCaracOut, ref string strErrMsg, string año)
        {
            #region Vars
            string strConsulta = string.Empty;
            string strCondicion = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {
                /*strConsulta = string.Format("SELECT DISTINCT(IdDependenciaRespuesta) as IdArea, PJO.NombreHijo as Area FROM[Auditoria].[Recomendacion] as AR" +
                    " INNER JOIN [Parametrizacion].[JerarquiaOrganizacional] as PJO on PJO.idHijo = AR.IdDependenciaRespuesta" +
                    " where YEAR(AR.FechaRegistro) = {0}", año);*/
                strConsulta = string.Format("SELECT DISTINCT(PDJO.idHijo) as IdArea, PA.NombreArea as Area FROM[Auditoria].[Recomendacion] as AR" +
                " INNER JOIN [Parametrizacion].[JerarquiaOrganizacional] as PJO on PJO.idHijo = AR.IdDependenciaRespuesta" +
                " INNER JOIN Parametrizacion.DetalleJerarquiaOrg as PDJO on PDJO.idHijo = PJO.idHijo" +
                " INNER JOIN Parametrizacion.Area as PA on PA.IdArea = PDJO.IdArea" +
                " where YEAR(AR.FechaRegistro) = {0}", año);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los departamentos. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return boolResult;
        }
        public int mtdGetValueRecomendacionesGeneral(ref DataTable dtCaracOut, ref string strErrMsg, string año, string meses, string IdArea)
        {
            #region Vars
            string strConsulta = string.Empty;
            string strCondicion = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            int value = 0;
            #endregion Vars
            try
            {
                /*strConsulta = string.Format("SELECT count(AH.[IdHallazgo]) as countRecomendacion FROM [Auditoria].[Hallazgo] as AH"+
                    " INNER JOIN [Parametrizacion].[DetalleTipos] AS PDT ON PDT.IdDetalleTipo = AH.IdNivelRiesgo"+
                    " INNER JOIN Auditoria.Recomendacion as AR on AR.IdHallazgo = AH.IdHallazgo"+
                    " where YEAR(AH.FechaRegistro) = {0} and MONTH(AH.FechaRegistro) in ({1}) and AR.IdDependenciaRespuesta={2} and AR.Estado not in ('Realizada','Finalizada')", año, meses, IdArea);
                    */
                strConsulta = string.Format("SELECT count(AH.[IdHallazgo]) as countRecomendacion FROM Auditoria.Auditoria as AA "+
                    "LEFT OUTER JOIN [Auditoria].[Hallazgo] as AH on AH.IdAuditoria = AA.IdAuditoria " +
                    "LEFT OUTER JOIN [Parametrizacion].[DetalleTipos] AS PDT ON PDT.IdDetalleTipo = AH.IdNivelRiesgo " +
                    "LEFT OUTER JOIN Auditoria.Recomendacion as AR on AR.IdHallazgo = AH.IdHallazgo " +
                    "where YEAR(AH.FechaRegistro) = {0} and MONTH(AH.FechaRegistro) in ({1}) and AR.IdDependenciaRespuesta={2}", año, meses, IdArea);
                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                foreach (DataRow row in dtCaracOut.Rows)
                {
                    value = Convert.ToInt32(row["countRecomendacion"].ToString());
                }
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la cantidad de recomendaciones. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return value;
        }
        public int mtdGetValueRecomendacionesTotal(ref DataTable dtCaracOut, ref string strErrMsg, string año, string meses)
        {
            #region Vars
            string strConsulta = string.Empty;
            string strCondicion = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            int value = 0;
            #endregion Vars
            try
            {
                /*strConsulta = string.Format("SELECT count(AH.[IdHallazgo]) as countRecomendacion FROM [Auditoria].[Hallazgo] as AH" +
                    " INNER JOIN [Parametrizacion].[DetalleTipos] AS PDT ON PDT.IdDetalleTipo = AH.IdNivelRiesgo" +
                    " INNER JOIN Auditoria.Recomendacion as AR on AR.IdHallazgo = AH.IdHallazgo" +
                    " where YEAR(AH.FechaRegistro) = {0} and MONTH(AH.FechaRegistro) in ({1}) and AR.Estado not in ('Realizada','Finalizada')", año, meses);*/
                strConsulta = string.Format("SELECT count(AH.[IdHallazgo]) as countRecomendacion FROM Auditoria.Auditoria as AA "+
                    "LEFT OUTER JOIN [Auditoria].[Hallazgo] as AH on AH.IdAuditoria = AA.IdAuditoria " +
                    "LEFT OUTER JOIN [Parametrizacion].[DetalleTipos] AS PDT ON PDT.IdDetalleTipo = AH.IdNivelRiesgo " +
                    "LEFT OUTER JOIN Auditoria.Recomendacion as AR on AR.IdHallazgo = AH.IdHallazgo " +
                    "where YEAR(AH.FechaRegistro) = {0} and MONTH(AH.FechaRegistro) in ({1}) ", año, meses);

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                foreach (DataRow row in dtCaracOut.Rows)
                {
                    value = Convert.ToInt32(row["countRecomendacion"].ToString());
                }
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar la cantidad de recomendaciones. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return value;
        }
        public bool mtdGenerarReporteSeguimiento(ref DataTable dtCaracOut, ref string strErrMsg, string año, string fechaActual)
        {
            #region Vars
            string strConsulta = string.Empty;
            string strCondicion = string.Empty;
            cDataBase cDatabase = new cDataBase();
            bool boolResult = false;
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT AA.IdAuditoria, AA.Tema, AH.FechaRegistro,DATEDIFF(MONTH,AH.FechaRegistro, '{1}') as CantMesDiff,"+
                    "AH.Hallazgo, ISNULL(AR.IdDependenciaRespuesta,0) AS IdDependenciaRespuesta, PJO.NombreHijo as Responsable,AR.Observacion,AH.IdNivelRiesgo, PDT.NombreDetalle " +
                    ",AAS.Seguimiento, AAS.FechaSeguimiento " +
                    "FROM [Auditoria].[Hallazgo] as AH "+
                    "LEFT OUTER JOIN [Parametrizacion].[DetalleTipos] AS PDT ON PDT.IdDetalleTipo = AH.IdNivelRiesgo " +
                    "LEFT OUTER JOIN Auditoria.Recomendacion as AR on AR.IdHallazgo = AH.IdHallazgo " +
                    "LEFT OUTER JOIN Auditoria.Auditoria as AA on AA.IdAuditoria = AH.IdAuditoria " +
                    "LEFT OUTER JOIN Parametrizacion.JerarquiaOrganizacional as PJO on PJO.idHijo = AR.IdDependenciaRespuesta " +
                    "LEFT OUTER JOIN Auditoria.AuditoriaSeguimiento AS AAS on AAS.IdAuditoria = AA.IdAuditoria " +
                    "where YEAR(AH.FechaRegistro) = {0}  ", año, fechaActual);
                //and AR.Estado not in ('Realizada','Finalizada')

                cDatabase.conectar();
                dtCaracOut = cDatabase.ejecutarConsulta(strConsulta);
                boolResult = true;
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al consultar los departamentos. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return boolResult;
        }
    }
}