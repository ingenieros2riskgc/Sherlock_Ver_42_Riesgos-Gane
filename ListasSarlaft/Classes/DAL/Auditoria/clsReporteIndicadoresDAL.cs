using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsReporteIndicadoresDAL
    {
        public double mtdGetAudRealizadas(ref string strErrMsg, clsReporteIndicadoresDTO objReporte)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            double realizadas = 0;
            #endregion Vars
            try
            {
                /*strConsulta = string.Format("select COUNT(AA.IdAuditoria) as Realizadas from Auditoria.Auditoria as AA "+
                                "where Estado = 'CUMPLIDA' and MONTH(AA.FechaCierre) = {0} and YEAR(AA.FechaCierre) = {1}", objReporte.intMes,objReporte.strAno);*/
                                /*strConsulta = string.Format("SELECT COUNT(AA.IdAuditoria) as Realizadas FROM [Auditoria].[Auditoria] AS AA"
                    + " INNER JOIN [Auditoria].[AudObjRecurso] AS AAOR ON AAOR.IdAuditoria = AA.IdAuditoria"
                    + " WHERE YEAR(AA.FechaCierre) = '{0}' and MONTH(AA.FechaCierre) = {1}"
                    + " and ( AA.Estado = 'CERRADA') "//AA.Estado = 'CUMPLIDA' OR AA.Estado = 'SEGUIMIENTO' OR
                    , objReporte.strAno, objReporte.intMes);//AND AAOR.Etapa= 'INFORME' + "  AND AA.FechaCierre <= AAOR.FechaFinal"*/
                    strConsulta = string.Format("SELECT COUNT(0) as Realizadas  FROM [Auditoria].[ReporteAuditoria] "
                    + " WHERE YEAR([FechaCierre]) = '{0}' and MONTH([FechaCierre]) = {1} AND [Estado] = 'CERRADA'"
                    , objReporte.strAno, objReporte.intMes);

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            realizadas = Convert.ToDouble(dr["Realizadas"].ToString().Trim());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al obtener la cantidad de auditorias realizadas. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return realizadas;
        }
        public double mtdGetAudProgramadas(ref string strErrMsg, clsReporteIndicadoresDTO objReporte)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            double programadas = 0;
            #endregion Vars
            try
            {
                /*strConsulta = string.Format("select count(AA.IdAuditoria) as Programadas from Auditoria.Auditoria as AA "+
                    "where MONTH(AA.FechaCierre) = {0} and YEAR(AA.FechaCierre) = {1}", objReporte.intMes, objReporte.strAno);*/
                strConsulta = string.Format("select count(AA.IdAuditoria) as Programadas from Auditoria.Auditoria as AA "+
                    "INNER JOIN [Parametrizacion].[DetalleTipos] as PDT on PDT.IdDetalleTipo = AA.IdDetalleTipo_TipoNaturaleza "+
                    "where MONTH(AA.FechaCierre) = {0} and YEAR(AA.FechaCierre) = {1} ", objReporte.intMes, objReporte.strAno);//and PDT.NombreDetalle = 'Planeado'

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            programadas = Convert.ToDouble(dr["Programadas"].ToString().Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al obtener la cantidad de auditorias programadas. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return programadas;
        }

        public double mtdGetAudRealizadasRedcolsa(ref string strErrMsg, int IdPlaneacion, int mes)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            double realizadas = 0;
            #endregion Vars
            try
            {
                /*strConsulta = string.Format("select COUNT(AA.IdAuditoria) as Realizadas from Auditoria.Auditoria as AA "+
                                "where Estado = 'CUMPLIDA' and MONTH(AA.FechaCierre) = {0} and YEAR(AA.FechaCierre) = {1}", objReporte.intMes,objReporte.strAno);*/
                /*strConsulta = string.Format("SELECT COUNT(AA.IdAuditoria) as Realizadas FROM [Auditoria].[Auditoria] AS AA"
    + " INNER JOIN [Auditoria].[AudObjRecurso] AS AAOR ON AAOR.IdAuditoria = AA.IdAuditoria"
    + " WHERE YEAR(AA.FechaCierre) = '{0}' and MONTH(AA.FechaCierre) = {1}"
    + " and ( AA.Estado = 'CERRADA') "//AA.Estado = 'CUMPLIDA' OR AA.Estado = 'SEGUIMIENTO' OR
    , objReporte.strAno, objReporte.intMes);//AND AAOR.Etapa= 'INFORME' + "  AND AA.FechaCierre <= AAOR.FechaFinal"*/
                strConsulta = string.Format("SELECT COUNT(0) as Realizadas  FROM [Auditoria].[ReporteAuditoria] "
                + " WHERE IdPlanAud = {0} and MONTH([FechaAprobacion]) = {1}"
                , IdPlaneacion, mes);

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            realizadas = Convert.ToDouble(dr["Realizadas"].ToString().Trim());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al obtener la cantidad de auditorias realizadas. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return realizadas;
        }
        public double mtdGetAudProgramadasRedcolsa(ref string strErrMsg, int IdPlaneacion, int mes)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            double programadas = 0;
            #endregion Vars
            try
            {
                /*strConsulta = string.Format("select count(AA.IdAuditoria) as Programadas from Auditoria.Auditoria as AA "+
                    "where MONTH(AA.FechaCierre) = {0} and YEAR(AA.FechaCierre) = {1}", objReporte.intMes, objReporte.strAno);*/
                strConsulta = string.Format("SELECT COUNT(0) as Programadas  FROM [Auditoria].[ReporteAuditoria]" +
                    "where IdPlanAud = {0} and MONTH([FechaInicio]) = {1}", IdPlaneacion, mes);//and PDT.NombreDetalle = 'Planeado'

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            programadas = Convert.ToDouble(dr["Programadas"].ToString().Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al obtener la cantidad de auditorias programadas. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return programadas;
        }

        public double mtdGetRecProgramadasRedcolsa(ref string strErrMsg, int IdPlaneacion, int mes)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            double programadas = 0;
            #endregion Vars
            try
            {
                /*strConsulta = string.Format("select count(AA.IdAuditoria) as Programadas from Auditoria.Auditoria as AA "+
                    "where MONTH(AA.FechaCierre) = {0} and YEAR(AA.FechaCierre) = {1}", objReporte.intMes, objReporte.strAno);*/
                strConsulta = string.Format("select COUNT(1) as Programadas FROM [Auditoria].[ReporteAuditoria] as ARA " +
                    "INNER JOIN Auditoria.Hallazgo as AH on AH.IdAuditoria = ARA.IdAuditoria "+
                    "INNER JOIN [Auditoria].[Recomendacion] as AR ON AR.IdHallazgo = AH.IdHallazgo "+
                    "LEFT OUTER JOIN [Auditoria].[RecomendacionEstados] AS ARE ON ARE.IdRecomendacion = AR.IdRecomendacion "+
                    "where IdPlanAud = {0} and MONTH([FechaInicio]) = {1}", IdPlaneacion, mes);//and PDT.NombreDetalle = 'Planeado'

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            programadas = Convert.ToDouble(dr["Programadas"].ToString().Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al obtener la cantidad de recomendaciones programadas. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return programadas;
        }
        public double mtdGetRecRealizadasRedcolsa(ref string strErrMsg, int IdPlaneacion, int mes)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            double realizadas = 0;
            #endregion Vars
            try
            {
                
                strConsulta = string.Format("select COUNT(1) as Realizadas FROM [Auditoria].[ReporteAuditoria] as ARA "
                    + "INNER JOIN Auditoria.Hallazgo as AH on AH.IdAuditoria = ARA.IdAuditoria "
                    + "INNER JOIN [Auditoria].[Recomendacion] as AR ON AR.IdHallazgo = AH.IdHallazgo "
                    + "LEFT OUTER JOIN [Auditoria].[RecomendacionEstados] AS ARE ON ARE.IdRecomendacion = AR.IdRecomendacion "
                + " WHERE IdPlanAud = {0} and MONTH(ARE.FechaRegistro) = {1}"
                , IdPlaneacion, mes);

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            realizadas = Convert.ToDouble(dr["Realizadas"].ToString().Trim());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al obtener la cantidad de recomendaciones realizadas. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return realizadas;
        }
    }
}