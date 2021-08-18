using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsReportesAuditoriaDAL
    {
        public DataTable mtdReporteAuditoria(ref string strErrMsg, int IdRegistro, string idMacroProceso,
            string IdProceso, string Mes, string Periodicidad, string IdGrupoAuditoria)
        {
            #region Vars
            string strConsulta = string.Empty;
            string strCondicion = string.Empty;
            string strForm = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strCondicion = string.Format("WHERE  [IdPlaneacion] = {0} ", IdRegistro);
                if (idMacroProceso != "0" && idMacroProceso != "")
                {
                    if (strCondicion != "")
                        strCondicion = string.Format("{1} and IdProceso = {0} and IdTipoProceso = 1", idMacroProceso, strCondicion);
                    else
                        strCondicion = string.Format("where IdProceso = {0} and IdTipoProceso = 1", idMacroProceso);
                    //strForm = string.Format(" LEFT OUTER JOIN Auditoria.Auditoria as AA on AA.IdPlaneacion = ARA.IdRegistroAuditoria");
                }
                if (IdProceso != "0" && IdProceso != "")
                {
                    if (strCondicion != "")
                        strCondicion = string.Format("{1} and IdProceso = {0} and IdTipoProceso = 2", IdProceso, strCondicion);
                    else
                        strCondicion = string.Format("where IdProceso = {0} and IdTipoProceso = 2", IdProceso);
                    /*if (strForm == "")
                        strForm = string.Format(" LEFT OUTER JOIN Auditoria.Auditoria as AA on AA.IdPlaneacion = ARA.IdRegistroAuditoria");*/
                }
                if (IdGrupoAuditoria != "0" && IdGrupoAuditoria != "")
                {
                    if (strCondicion != "")
                        strCondicion = string.Format("{1} and [IdGrupoAuditoria] = {0} ", IdGrupoAuditoria, strCondicion);
                    else
                        strCondicion = string.Format("where [IdGrupoAuditoria] = {0} ", IdGrupoAuditoria);
                    /*if (strForm == "")
                        strForm = string.Format(" LEFT OUTER JOIN Auditoria.Auditoria as AA on AA.IdPlaneacion = ARA.IdRegistroAuditoria" +
                            " LEFT OUTER JOIN Auditoria.AuditoriaObjetivo as AO on AO.IdAuditoria = AA.IdAuditoria");*/
                }
                if(Mes != "0" && Mes != "")
                {
                    if (strCondicion != "")
                        strCondicion = string.Format("{1} and ([IdMesEjecucion]) = {0}", Mes, strCondicion);
                    else
                        strCondicion = string.Format("where ([IdMesEjecucion]) = {0}", Mes);
                }
                if(Periodicidad != "0" && Periodicidad != "")
                {
                    if (strCondicion != "")
                        strCondicion = string.Format("{1} and periodicidad = {0}", Periodicidad, strCondicion);
                    else
                        strCondicion = string.Format("where periodicidad = {0}", Periodicidad);
                }
                strConsulta = string.Format("SELECT [IdDependencia],[NombreDP],ISNULL([IdProceso],0) AS IdProceso,[NombreProceso],[Tema],[Objetivo]" +
                  ",[IdObjetivo],[NombreObjetivo],[Descripcion],[Estado],[IdGrupoAuditoria],[Nombre],[FechaInicio],[FechaCierre]" +
                  ",ISNULL([IdMesEjecucion],0) as IdMesEjecucion,ISNULL([SemanaEjecucion],0) AS SemanaEjecucion,[Especial], [periodicidad] " +
                  " FROM [Auditoria].[ReporteAuditoria] "+
                  " {0} {1}", strForm,strCondicion);//[Estado] = 'CERRADA' and

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al generar el reporte de Auditoria. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public DataTable mtdReporteConsolidado(ref string strErrMsg, string IdPlaneacion, string IdRegistroAud)
        {
            #region Vars
            string strConsulta = string.Empty;
            string strCondicion = string.Empty;
            string strForm = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                if (IdPlaneacion != "0" && IdPlaneacion != "")
                {
                    if (strCondicion != "")
                        strCondicion = strCondicion + string.Format(" and AP.IdPlaneacion = {0}", IdPlaneacion);
                    else
                        strCondicion = string.Format("where AP.IdPlaneacion = {0}", IdPlaneacion);
                }
                if (IdRegistroAud != "0" && IdRegistroAud != "")
                {
                    if (strCondicion != "")
                        strCondicion = strCondicion + string.Format(" and ARA.IdRegistroAuditoria = {0}", IdRegistroAud);
                    else
                        strCondicion = string.Format("where ARA.IdRegistroAuditoria = {0}", IdRegistroAud);
                }
                strConsulta = string.Format("select AP.Nombre,ARA.IdRegistroAuditoria, ARA.Tema, ARA.FechaInicio, ARA.FechaCierre "+
                " from [Auditoria].[RegistrosAuditoria] AS ARA"+
                " INNER JOIN Auditoria.Planeacion as AP on AP.IdPlaneacion = ARA.IdPlaneacion"+
                " {0}"+
                " {1}", strForm,strCondicion);

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al generar el reporte de Auditoria. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public double mtdGetAudRealizadas(ref string strErrMsg, int IdPlaneacion)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            double realizadas = 0;
            #endregion Vars
            try
            {
                strConsulta = string.Format("select COUNT(IdAuditoria) as Realizadas from Auditoria.Auditoria where IdPlaneacion = {0} and Estado = 'CERRADA'", IdPlaneacion);

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
        public double mtdGetAudProgramadas(ref string strErrMsg, int IdPlaneacion)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            double programadas = 0;
            #endregion Vars
            try
            {
                strConsulta = string.Format("select count(IdAuditoria) as Programadas from Auditoria.Auditoria where IdPlaneacion = {0}", IdPlaneacion);

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
        public DataTable mtdGetProceso(ref string strErrMsg, string IdMacro)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT [IdProceso],[Nombre] FROM [Procesos].[Proceso] where [IdMacroProceso] = {0}", IdMacro);

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al generar el reporte de Auditoria. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
        public DataTable mtdGetRegistrosAud(ref string strErrMsg, string IdPlaneacion)
        {
            #region Vars
            string strConsulta = string.Empty;
            cDataBase cDatabase = new cDataBase();
            DataTable dt = new DataTable();
            #endregion Vars
            try
            {
                strConsulta = string.Format("SELECT [IdRegistroAuditoria],[Tema] FROM [Auditoria].[RegistrosAuditoria] where IdPlaneacion = {0}", IdPlaneacion);

                cDatabase.conectar();
                dt = cDatabase.ejecutarConsulta(strConsulta);

            }
            catch (Exception ex)
            {
                strErrMsg = string.Format("Error al obtener los registros de Auditoria por planeación. [{0}]", ex.Message);
            }
            finally
            {
                cDatabase.desconectar();
            }

            return dt;
        }
    }
}