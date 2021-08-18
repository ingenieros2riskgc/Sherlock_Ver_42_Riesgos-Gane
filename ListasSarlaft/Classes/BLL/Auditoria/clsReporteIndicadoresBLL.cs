using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsReporteIndicadoresBLL
    {
        /// <summary>
        /// Metodo que permite generar el reporte de las Auditorias consolidado
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsReporteIndicadoresDTO> mtdReporteIndicadores(ref string strErrMsg, ref List<clsReporteIndicadoresDTO> lstRegistros, clsReporteIndicadoresDTO objReporte)
        {

            //bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsReporteIndicadoresDAL cDtRegistros = new clsReporteIndicadoresDAL();
            double realizadas = 0;
            double programadas = 0;
            double cumplimiento = 0;
            

            if (objReporte.intMes == 0)
            {
                
                    for(int i = 0; i < 12; i++)
                    {
                        clsReporteIndicadoresDTO objReau = new clsReporteIndicadoresDTO();
                        objReporte.intMes = i+1;
                    //objReau.intIdRegistroAuditoria = Convert.ToInt32(dr["IdRegistroAuditoria"].ToString().Trim());
                    objReau.intMes = i + 1;
                    objReau.strNombreMes = MonthName(i + 1);
                    objReau.strAno = objReporte.strAno;
                        realizadas = cDtRegistros.mtdGetAudRealizadas(ref strErrMsg, objReporte);
                        programadas = cDtRegistros.mtdGetAudProgramadas(ref strErrMsg, objReporte);
                        if (programadas != 0)
                        {
                            cumplimiento = realizadas / programadas * 100;
                        }
                        objReau.dbRealizadas = realizadas;
                        objReau.dbProgramadas = programadas;
                        objReau.dbCumplimiento = cumplimiento;
                        lstRegistros.Add(objReau);
                    }
            }else
            {
                clsReporteIndicadoresDTO objReau = new clsReporteIndicadoresDTO();
                //objReau.intIdRegistroAuditoria = Convert.ToInt32(dr["IdRegistroAuditoria"].ToString().Trim());

                objReau.strNombreMes = MonthName(objReporte.intMes);
                objReau.strAno = objReporte.strAno;
                realizadas = cDtRegistros.mtdGetAudRealizadas(ref strErrMsg, objReporte);
                programadas = cDtRegistros.mtdGetAudProgramadas(ref strErrMsg, objReporte);
                if (programadas != 0)
                {
                    cumplimiento = realizadas / programadas * 100;
                }
                objReau.dbRealizadas = realizadas;
                objReau.dbProgramadas = programadas;
                objReau.dbCumplimiento = cumplimiento;
                lstRegistros.Add(objReau);
            }
            return lstRegistros;
        }
        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }

        public DataTable mtdReporteIndicadoresRedcolsa(ref string strErrMsg, int IdPlaneacion)
        {

            //bool booResult = false;
            DataTable dtInfo = new DataTable();
            dtInfo.Columns.Add("Metodologia", typeof(string));
            dtInfo.Columns.Add("Frecuencia", typeof(string));
            dtInfo.Columns.Add("Auditor", typeof(string));
            dtInfo.Columns.Add("Meta", typeof(string));
            dtInfo.Columns.Add("ENE", typeof(string));
            dtInfo.Columns.Add("FEB", typeof(string));
            dtInfo.Columns.Add("MAR", typeof(string));
            dtInfo.Columns.Add("ABR", typeof(string));
            dtInfo.Columns.Add("MAY", typeof(string));
            dtInfo.Columns.Add("JUN", typeof(string));
            dtInfo.Columns.Add("JUL", typeof(string));
            dtInfo.Columns.Add("AGO", typeof(string));
            dtInfo.Columns.Add("SEP", typeof(string));
            dtInfo.Columns.Add("OCT", typeof(string));
            dtInfo.Columns.Add("NOV", typeof(string));
            dtInfo.Columns.Add("DIC", typeof(string));
            clsReporteIndicadoresDAL cDtRegistros = new clsReporteIndicadoresDAL();
            double realizadas = 0;
            double programadas = 0;
            double cumplimiento = 0;

            double progene = cDtRegistros.mtdGetAudProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 1);
            double progfeb = cDtRegistros.mtdGetAudProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 2);
            double progmar = cDtRegistros.mtdGetAudProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 3);
            double progabr = cDtRegistros.mtdGetAudProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 4);
            double progmay = cDtRegistros.mtdGetAudProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 5);
            double progjun = cDtRegistros.mtdGetAudProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 6);
            double progjul = cDtRegistros.mtdGetAudProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 7);
            double progago = cDtRegistros.mtdGetAudProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 8);
            double progsep = cDtRegistros.mtdGetAudProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 9);
            double progoct = cDtRegistros.mtdGetAudProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 10);
            double prognov = cDtRegistros.mtdGetAudProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 11);
            double progdic = cDtRegistros.mtdGetAudProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 12);

            double reaene = cDtRegistros.mtdGetAudRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 1);
            double reafeb = cDtRegistros.mtdGetAudRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 2);
            double reamar = cDtRegistros.mtdGetAudRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 3);
            double reaabr = cDtRegistros.mtdGetAudRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 4);
            double reamay = cDtRegistros.mtdGetAudRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 5);
            double reajun = cDtRegistros.mtdGetAudRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 6);
            double reajul = cDtRegistros.mtdGetAudRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 7);
            double reaago = cDtRegistros.mtdGetAudRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 8);
            double reasep = cDtRegistros.mtdGetAudRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 9);
            double reaoct = cDtRegistros.mtdGetAudRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 10);
            double reanov = cDtRegistros.mtdGetAudRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 11);
            double readic = cDtRegistros.mtdGetAudRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 12);
            
            
            dtInfo.Rows.Add(new Object[] {
                "No. de auditorías realizadas",
                "Bimestral",
                "Auditor",
                "90%",
                reaene,
                reafeb,
                reamar,
                reaabr,
                reamay,
                reajun,
                reajul,
                reaago,
                reasep,
                reaoct,
                reanov,
                readic
            });
            dtInfo.Rows.Add(new Object[] {
                "No. de auditorías programadas",
                "Bimestral",
                "Auditor",
                "90%",
                progene,
                progfeb,
                progmar,
                progabr,
                progmay,
                progjun,
                progjul,
                progago,
                progsep,
                progoct,
                prognov,
                progdic
            });
            double result1 = (reaene / progene)*100;
            double result2 = (reafeb / progfeb)*100;
            double result3 = (reamar / progmar)*100;
            double result4 = (reaabr / progabr)*100;
            double result5 = (reamay / progmay)*100;
            double result6 = (reajun / progjun)*100;
            double result7 = (reajul / progjul)*100;
            double result8 = (reaago / progago)*100;
            double result9 = (reasep / progsep)*100;
            double result10 = (reaoct / progoct)*100;
            double result11 = (reanov / prognov)*100;
            double result12 = (readic / progdic)*100;
            dtInfo.Rows.Add(new Object[] {
                "",
                "",
                "",
                "Medición:",
                result1,
                result2,
                result3,
                result4,
                result5,
                result6,
                result7,
                result8,
                result9,
                result10,
                result11,
                result12
            });
            return dtInfo;
        }
        public DataTable mtdReporteIndRecomdacionRedcolsa(ref string strErrMsg, int IdPlaneacion)
        {

            //bool booResult = false;
            DataTable dtInfo = new DataTable();
            dtInfo.Columns.Add("Metodologia", typeof(string));
            dtInfo.Columns.Add("Frecuencia", typeof(string));
            dtInfo.Columns.Add("Auditor", typeof(string));
            dtInfo.Columns.Add("Meta", typeof(string));
            dtInfo.Columns.Add("ENE", typeof(string));
            dtInfo.Columns.Add("FEB", typeof(string));
            dtInfo.Columns.Add("MAR", typeof(string));
            dtInfo.Columns.Add("ABR", typeof(string));
            dtInfo.Columns.Add("MAY", typeof(string));
            dtInfo.Columns.Add("JUN", typeof(string));
            dtInfo.Columns.Add("JUL", typeof(string));
            dtInfo.Columns.Add("AGO", typeof(string));
            dtInfo.Columns.Add("SEP", typeof(string));
            dtInfo.Columns.Add("OCT", typeof(string));
            dtInfo.Columns.Add("NOV", typeof(string));
            dtInfo.Columns.Add("DIC", typeof(string));
            clsReporteIndicadoresDAL cDtRegistros = new clsReporteIndicadoresDAL();
            double realizadas = 0;
            double programadas = 0;
            double cumplimiento = 0;

            double progene = cDtRegistros.mtdGetRecProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 1);
            double progfeb = cDtRegistros.mtdGetRecProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 2);
            double progmar = cDtRegistros.mtdGetRecProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 3);
            double progabr = cDtRegistros.mtdGetRecProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 4);
            double progmay = cDtRegistros.mtdGetRecProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 5);
            double progjun = cDtRegistros.mtdGetRecProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 6);
            double progjul = cDtRegistros.mtdGetRecProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 7);
            double progago = cDtRegistros.mtdGetRecProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 8);
            double progsep = cDtRegistros.mtdGetRecProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 9);
            double progoct = cDtRegistros.mtdGetRecProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 10);
            double prognov = cDtRegistros.mtdGetRecProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 11);
            double progdic = cDtRegistros.mtdGetRecProgramadasRedcolsa(ref strErrMsg, IdPlaneacion, 12);

            double reaene = cDtRegistros.mtdGetRecRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 1);
            double reafeb = cDtRegistros.mtdGetRecRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 2);
            double reamar = cDtRegistros.mtdGetRecRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 3);
            double reaabr = cDtRegistros.mtdGetRecRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 4);
            double reamay = cDtRegistros.mtdGetRecRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 5);
            double reajun = cDtRegistros.mtdGetRecRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 6);
            double reajul = cDtRegistros.mtdGetRecRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 7);
            double reaago = cDtRegistros.mtdGetRecRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 8);
            double reasep = cDtRegistros.mtdGetRecRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 9);
            double reaoct = cDtRegistros.mtdGetRecRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 10);
            double reanov = cDtRegistros.mtdGetRecRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 11);
            double readic = cDtRegistros.mtdGetRecRealizadasRedcolsa(ref strErrMsg, IdPlaneacion, 12);


            dtInfo.Rows.Add(new Object[] {
                "No. de auditorías realizadas",
                "Bimestral",
                "Auditor",
                "90%",
                reaene,
                reafeb,
                reamar,
                reaabr,
                reamay,
                reajun,
                reajul,
                reaago,
                reasep,
                reaoct,
                reanov,
                readic
            });
            dtInfo.Rows.Add(new Object[] {
                "No. de auditorías programadas",
                "Bimestral",
                "Auditor",
                "90%",
                progene,
                progfeb,
                progmar,
                progabr,
                progmay,
                progjun,
                progjul,
                progago,
                progsep,
                progoct,
                prognov,
                progdic
            });
            double result1 = (reaene / progene) * 100;
            double result2 = (reafeb / progfeb) * 100;
            double result3 = (reamar / progmar) * 100;
            double result4 = (reaabr / progabr) * 100;
            double result5 = (reamay / progmay) * 100;
            double result6 = (reajun / progjun) * 100;
            double result7 = (reajul / progjul) * 100;
            double result8 = (reaago / progago) * 100;
            double result9 = (reasep / progsep) * 100;
            double result10 = (reaoct / progoct) * 100;
            double result11 = (reanov / prognov) * 100;
            double result12 = (readic / progdic) * 100;
            dtInfo.Rows.Add(new Object[] {
                "",
                "",
                "",
                "Medición:",
                result1,
                result2,
                result3,
                result4,
                result5,
                result6,
                result7,
                result8,
                result9,
                result10,
                result11,
                result12
            });
            return dtInfo;
        }
    }
}