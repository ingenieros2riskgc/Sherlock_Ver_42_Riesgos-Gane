using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsReporteAuditoriaBLL
    {
        /// <summary>
        /// Metodo que permite generar el reporte de las Auditorias
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsReporteAuditoriaDTO> mtdReporteAuditoria(ref string strErrMsg, ref List<clsReporteAuditoriaDTO> lstRegistros, int IdRegistro,string idMacroProceso,
            string IdProceso, string Mes, string Periodicidad, string IdGrupoAuditoria)
        {

            //bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsReportesAuditoriaDAL cDtRegistros = new clsReportesAuditoriaDAL();

            dtInfo = cDtRegistros.mtdReporteAuditoria(ref strErrMsg, IdRegistro, idMacroProceso,
             IdProceso, Mes, Periodicidad, IdGrupoAuditoria);
            try
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsReporteAuditoriaDTO objReau = new clsReporteAuditoriaDTO();
                            //objReau.intIdRegistroAuditoria = Convert.ToInt32(dr["IdRegistroAuditoria"].ToString().Trim());
                            objReau.intIdDependencia = Convert.ToInt32(dr["IdDependencia"].ToString().Trim());
                            objReau.strNombreDependencia = dr["NombreDP"].ToString().Trim();
                            objReau.intIdProceso = Convert.ToInt32(dr["IdProceso"].ToString().Trim());
                            objReau.strNombreProces = dr["NombreProceso"].ToString().Trim();
                            objReau.strTema = dr["Tema"].ToString().Trim();
                            objReau.strObjetivo = dr["Objetivo"].ToString().Trim();
                            objReau.strNombreObjetivo = dr["NombreObjetivo"].ToString().Trim();
                            objReau.strDescripcionObjetivo = dr["Descripcion"].ToString().Trim();
                            objReau.strEstado = dr["Estado"].ToString().Trim();
                            objReau.intIdGrupoAuditoria = Convert.ToInt32(dr["IdGrupoAuditoria"].ToString().Trim());
                            objReau.strGrupoAuditoria = dr["Nombre"].ToString().Trim();
                            objReau.strFechaInicio = Convert.ToDateTime(dr["FechaInicio"].ToString().Trim()).ToShortDateString();
                            objReau.strFechaCierre = Convert.ToDateTime(dr["FechaCierre"].ToString().Trim()).ToShortDateString();
                            DateTime Inicio = Convert.ToDateTime(dr["FechaInicio"].ToString().Trim());
                            int MesInicio = Inicio.Month;
                            DateTime Fin = Convert.ToDateTime(dr["FechaCierre"].ToString().Trim());
                            int MesFin = Fin.Month;
                            int periodicidad = 0;
                            if(dr["FechaCierre"].ToString().Trim() != string.Empty)
                                periodicidad =  Convert.ToInt32(dr["periodicidad"].ToString().Trim());
                            if (periodicidad == 6)
                            {
                                objReau.strPeriodicidad = "Mensual";
                                objReau.strMes = MonthName(MesInicio);
                            }
                            //int result = MesFin - MesInicio;
                            if (periodicidad == 1)
                            {
                                objReau.strPeriodicidad = "Semestral";
                                objReau.strMes = MonthName(MesInicio) + " - " + MonthName(MesFin);
                            }
                            if (periodicidad == 2)
                            {
                                objReau.strPeriodicidad = "Trimestral";
                                objReau.strMes = MonthName(MesInicio) + " - " + MonthName(MesFin);
                            }
                            if (periodicidad == 3)
                            {
                                objReau.strPeriodicidad = "Cuatrimestral";
                                objReau.strMes = MonthName(MesInicio) + " - " + MonthName(MesFin);
                            }
                            if (periodicidad == 4)
                            {
                                objReau.strPeriodicidad = "Semestral";
                                objReau.strMes = MonthName(MesInicio) + " - " + MonthName(MesFin);
                            }
                            if (periodicidad == 5)
                            {
                                objReau.strPeriodicidad = "Anual";
                                objReau.strMes = MonthName(MesInicio) + " - " + MonthName(MesFin);
                            }
                            objReau.intIdMesExe = Convert.ToInt32(dr["IdMesEjecucion"].ToString().Trim());
                            if (Convert.ToInt32(dr["IdMesEjecucion"].ToString().Trim()) == 0)
                                objReau.strMesExe = "";
                            else
                                objReau.strMesExe = MonthName(Convert.ToInt32(dr["IdMesEjecucion"].ToString().Trim()));
                            objReau.strSemanaExe = dr["SemanaEjecucion"].ToString().Trim();
                            objReau.strEspecial = dr["Especial"].ToString().Trim();
                            lstRegistros.Add(objReau);
                        }
                    }
                    else
                    {
                        lstRegistros = null;
                        strErrMsg = "No hay datos para generar el reporte";
                    }
                }
                
                        
            }catch(Exception ex)
            {
                strErrMsg = "Error: " + ex;
            }
            return lstRegistros;
        }
        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }
        /// <summary>
        /// Metodo que permite generar el reporte de las Auditorias consolidado
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsReporteConsolidadoDTO> mtdReporteConsolidado(ref string strErrMsg, ref List<clsReporteConsolidadoDTO> lstRegistros, string IdPlaneacion, string IdRegistroAud)
        {

            //bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsReportesAuditoriaDAL cDtRegistros = new clsReportesAuditoriaDAL();
            double realizadas = 0;
            double programadas = 0;
            double cumplimiento = 0;
            dtInfo = cDtRegistros.mtdReporteConsolidado(ref strErrMsg, IdPlaneacion, IdRegistroAud);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsReporteConsolidadoDTO objReau = new clsReporteConsolidadoDTO();
                        //objReau.intIdRegistroAuditoria = Convert.ToInt32(dr["IdRegistroAuditoria"].ToString().Trim());
                        objReau.intIdRegistro = Convert.ToInt32(dr["IdRegistroAuditoria"].ToString().Trim());
                        objReau.strPlaneacion = dr["Nombre"].ToString().Trim();
                        objReau.strTema = dr["Tema"].ToString().Trim();
                        objReau.strFechaInicio = Convert.ToDateTime(dr["FechaInicio"].ToString().Trim()).ToShortDateString();
                        objReau.strFechaCierre = Convert.ToDateTime(dr["FechaCierre"].ToString().Trim()).ToShortDateString();
                        realizadas = cDtRegistros.mtdGetAudRealizadas(ref strErrMsg, Convert.ToInt32(dr["IdRegistroAuditoria"].ToString().Trim()));
                        programadas = cDtRegistros.mtdGetAudProgramadas(ref strErrMsg, Convert.ToInt32(dr["IdRegistroAuditoria"].ToString().Trim()));
                        if(programadas != 0)
                        {
                            cumplimiento = realizadas / programadas;
                        }
                        objReau.dbRealizadas = realizadas;
                        objReau.dbProgramadas = programadas;
                        objReau.dbCumplimiento = cumplimiento;
                        lstRegistros.Add(objReau);
                    }
                }
                else
                    lstRegistros = null;
            }
            return lstRegistros;
        }
    }
}