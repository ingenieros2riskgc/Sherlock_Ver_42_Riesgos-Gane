using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsSeguimientoRecomendacionesBLL
    {
        /// <summary>
        /// Metodo que permite generar los valores de las recomendaciones por departamento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public DataTable mtdGetValueRecomendacionesGeneral(ref string strErrMsg, string año)
        {
            string[] mensual = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            string[] mensualMes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            string[] bimestral = new string[] { "1,2", "3,4", "5,6", "7,8", "9,10", "11,12" };
            string[] bimestralMes = new string[] { "Enero,Febrero", "Marzo,Abril", "Mayo,Junio", "Julio,Agosto", "Septiembre,Octubre", "Noviembre,Diciembre" };
            string[] trimestral = new string[] { "1,2,3", "4,5,6", "7,8,9", "10,11,12" };
            string[] trimestralMes = new string[] { "Enero,Febrero,Marzo", "Abril,Mayo,Junio", "Julio,Agosto,Septiembre", "Octubre,Noviembre,Diciembre" };
            string[] cuatrimestral = new string[] { "1,2,3,4", "5,6,7,8", "9,10,11,12" };
            string[] cuatrimestralMes = new string[] { "Enero,Febrero,Marzo,Abril", "Mayo,Junio,Julio,Agosto", "Septiembre, Octubre,Noviembre,Diciembre" };
            string[] semestral = new string[] { "1,2,3,4,5,6", "7,8,9,10,11,12" };
            string[] semestralMes = new string[] { "Enero,Febrero,Marzo,Abril,Mayo,Junio", "Julio,Agosto,Septiembre, Octubre,Noviembre,Diciembre" };
            string[] anual = new string[] { "1,2,3,4,5,6,7,8,9,10,11,12" };
            string[] anualMes = new string[] { "Enero,Febrero,Marzo,Abril,Mayo,Junio, Julio,Agosto,Septiembre, Octubre,Noviembre,Diciembre" };
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsSeguimientoRecomendacionesDAL cDtRegistros = new clsSeguimientoRecomendacionesDAL();
            booResult = cDtRegistros.mtdConsultarAreasResponsables(ref dtInfo, ref strErrMsg, año);
            string mes = string.Empty;
            int valor = 0;
            double implementada = 0;
            double realizada = 0;
            double result = 0;
            DataTable dtResult = new DataTable();
            if (booResult == true)
            {
                    dtResult = mtdGetDataGeneral(mensual, mensualMes, año, ref strErrMsg, dtInfo);
            }
            return dtResult;
        }
        public DataTable mtdGetDataGeneral(string[] periodo, string[] meses, string año, ref string strErrMsg,  DataTable Areas)
        {
            DataTable dtResult = new DataTable();
            DataTable dtInfo = new DataTable();
            clsSeguimientoRecomendacionesDAL cDtRegistros = new clsSeguimientoRecomendacionesDAL();
            double valor = 0;
            double implementada = 0;
            double realizada = 0;
            double result = 0;
            double total = 0;
            
            dtResult.Columns.Add("Area:", Type.GetType("System.String"));
            for (int i = 0; i < periodo.Length; i++)
            {
                dtResult.Columns.Add(meses[i].ToString(), Type.GetType("System.String"));
            }
                /*DataRow dr2 = dtResult.NewRow();
                DataRow dr3 = dtResult.NewRow();*/
                foreach (DataRow row in Areas.Rows)
                {
                DataRow dr1 = dtResult.NewRow();
                dr1["Area:"] = row["Area"].ToString();
                for (int i = 0; i < periodo.Length; i++)
                {
                    //dtResult.Columns.Add(meses[i].ToString(), Type.GetType("System.String"));
                    valor = cDtRegistros.mtdGetValueRecomendacionesGeneral(ref dtInfo, ref strErrMsg, año, periodo[i].ToString(), row["IdArea"].ToString());

                    dr1[meses[i].ToString()] = valor;
                }
                dtResult.Rows.Add(dr1);
            }
            
                DataRow dr2 = dtResult.NewRow();
                dr2["Area:"] = "Total:";
                for (int i = 0; i < periodo.Length; i++)
                {
                    //dtResult.Columns.Add(meses[i].ToString(), Type.GetType("System.String"));
                    valor = cDtRegistros.mtdGetValueRecomendacionesTotal(ref dtInfo, ref strErrMsg, año, periodo[i].ToString());

                    dr2[meses[i].ToString()] = valor;
                }
                dtResult.Rows.Add(dr2);
            
            return dtResult;
        }
        /// <summary>
        /// Metodo que permite consultar los datos del reporte
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsSeguimientoRecomendacionesDTO> mtdGenerarReporteSeguimiento(ref string strErrMsg, ref List<clsSeguimientoRecomendacionesDTO> lstRegistros, string año, string FechaActual)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsSeguimientoRecomendacionesDAL cDtRegistros = new clsSeguimientoRecomendacionesDAL();
            string[] mensual = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            string[] mensualMes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            
                booResult = cDtRegistros.mtdGenerarReporteSeguimiento(ref dtInfo, ref strErrMsg, año, FechaActual);
            
            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsSeguimientoRecomendacionesDTO objReau = new clsSeguimientoRecomendacionesDTO();
                            //objReau.intIdRegistroAuditoria = Convert.ToInt32(dr["IdRegistroAuditoria"].ToString().Trim());
                            objReau.intIdAuditoria = Convert.ToInt32(dr["IdAuditoria"].ToString().Trim());
                            objReau.strTema = dr["Tema"].ToString().Trim();
                            objReau.strFechaRegistro = dr["FechaRegistro"].ToString().Trim();
                            objReau.intMesDiff = Convert.ToInt32(dr["CantMesDiff"].ToString().Trim());
                            objReau.strHallazgo = dr["Hallazgo"].ToString().Trim();
                            if(dr["IdDependenciaRespuesta"].ToString() != "")
                                objReau.intIdDependencia = Convert.ToInt32(dr["IdDependenciaRespuesta"].ToString());
                            objReau.strResponsable = dr["Responsable"].ToString().Trim();
                            objReau.strObservaciones = dr["Observacion"].ToString().Trim();
                            if(dr["IdNivelRiesgo"].ToString().Trim() != "")
                                objReau.intIdNivelRiesgo = Convert.ToInt32(dr["IdNivelRiesgo"].ToString().Trim());
                            objReau.strRiesgo = dr["NombreDetalle"].ToString().Trim();
                            if(dr["FechaSeguimiento"].ToString().Trim() != "")
                                objReau.dtFechaSeguimiento = Convert.ToDateTime(dr["FechaSeguimiento"].ToString().Trim());

                            objReau.strSeguimiento = dr["Seguimiento"].ToString().Trim();

                            lstRegistros.Add(objReau);
                        }
                    }
                    else
                    {
                        lstRegistros = null;
                        strErrMsg = "No hay datos para generar el reporte";
                    }

                }
                else
                {
                    lstRegistros = null;
                    strErrMsg = "No hay datos para generar el reporte";
                }

            }
            return lstRegistros;
        }
    }
}