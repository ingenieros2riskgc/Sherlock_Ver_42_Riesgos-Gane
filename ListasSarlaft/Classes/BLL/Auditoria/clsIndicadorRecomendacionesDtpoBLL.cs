using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsIndicadorRecomendacionesDtpoBLL
    {
        /// <summary>
        /// Metodo que permite consultar los datos del reporte por departamento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsIndicadorRecomendacionesDTO> mtdConsultarIndicadorRecomendacionesDtpo(ref string strErrMsg, ref List<clsIndicadorRecomendacionesDTO> lstRegistros, string año,
            string fechaInicial, string FechaFinal, string planeacion, string IdArea)
        {
            bool booResult = false;
            DataTable dtInfo = new DataTable();
            clsIndicadorRecomendacionesDtpoDAL cDtRegistros = new clsIndicadorRecomendacionesDtpoDAL();

            booResult = cDtRegistros.mtdConsultarIndicadorRecomendacionesDtpo(ref dtInfo, ref strErrMsg, año, fechaInicial, FechaFinal, planeacion, IdArea);
            if (booResult)
            {
                if (dtInfo != null)
                {
                    if (dtInfo.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtInfo.Rows)
                        {
                            clsIndicadorRecomendacionesDTO objReau = new clsIndicadorRecomendacionesDTO();
                            //objReau.intIdRegistroAuditoria = Convert.ToInt32(dr["IdRegistroAuditoria"].ToString().Trim());
                            objReau.intIdIndicador = Convert.ToInt32(dr["IdIndicador"].ToString().Trim());
                            objReau.strIndicador = dr["Indicador"].ToString().Trim();
                            objReau.strMetodologia = dr["MetodologiaNominador"].ToString().Trim() + " / " + dr["MetodologiaDenominador"].ToString().Trim();
                            objReau.intIdFrecuencia = Convert.ToInt32(dr["Frecuencia"].ToString().Trim());
                            int ValorFrecuencia = Convert.ToInt32(dr["Frecuencia"].ToString().Trim());
                            if (ValorFrecuencia == 1)
                                objReau.strFrecuencia = "Mensual";
                            if (ValorFrecuencia == 2)
                                objReau.strFrecuencia = "Bimestral";
                            if (ValorFrecuencia == 3)
                                objReau.strFrecuencia = "Trimestral";
                            if (ValorFrecuencia == 4)
                                objReau.strFrecuencia = "Cuatrimestral";
                            if (ValorFrecuencia == 5)
                                objReau.strFrecuencia = "Semestral";
                            if (ValorFrecuencia == 6)
                                objReau.strFrecuencia = "Anual";
                            objReau.strResponsable = dr["Responsable"].ToString().Trim();
                            objReau.intMeta = Convert.ToInt32(dr["Meta"].ToString().Trim());
                            objReau.intAcumulado = Convert.ToInt32(dr["Acumulado"].ToString().Trim());
                            objReau.intAcumuladoDtpo = Convert.ToInt32(dr["AcumuladoDtpo"].ToString().Trim());
                            objReau.intCumplimiento = Convert.ToInt32(dr["NivelCumplimiento"].ToString().Trim());


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
        /// <summary>
        /// Metodo que permite generar los valores de las recomendaciones por departamento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public DataTable mtdGetValueRecomendacionDtpo(ref string strErrMsg, string año, int Frecuencia, int IdIndicador)
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
            clsIndicadorRecomendacionesDtpoDAL cDtRegistros = new clsIndicadorRecomendacionesDtpoDAL();
            booResult = cDtRegistros.mtdConsultarAreasResponsables(ref dtInfo, ref strErrMsg, año);
            string mes = string.Empty;
            int valor = 0;
            double implementada = 0;
            double realizada = 0;
            double result = 0;
            DataTable dtResult = new DataTable();
            if (booResult == true)
            {
                foreach(DataRow row in dtInfo.Rows)
                {
                    if (Frecuencia == 1)
                    {
                        dtResult = mtdGetData(mensual, mensualMes, año, ref strErrMsg, IdIndicador, row["IdArea"].ToString(), row["Area"].ToString());
                    }
                    if (Frecuencia == 2)
                    {
                        dtResult = mtdGetData(bimestral, bimestralMes, año, ref strErrMsg, IdIndicador, row["IdArea"].ToString(), row["Area"].ToString());
                    }
                    if (Frecuencia == 3)
                    {
                        dtResult = mtdGetData(trimestral, trimestralMes, año, ref strErrMsg, IdIndicador, row["IdArea"].ToString(), row["Area"].ToString());
                    }
                    if (Frecuencia == 4)
                    {
                        dtResult = mtdGetData(cuatrimestral, cuatrimestralMes, año, ref strErrMsg, IdIndicador, row["IdArea"].ToString(), row["Area"].ToString());
                    }
                    if (Frecuencia == 5)
                    {
                        dtResult = mtdGetData(semestral, semestralMes, año, ref strErrMsg, IdIndicador, row["IdArea"].ToString(), row["Area"].ToString());
                    }
                    if (Frecuencia == 6)
                    {
                        dtResult = mtdGetData(anual, anualMes, año, ref strErrMsg, IdIndicador, row["IdArea"].ToString(), row["Area"].ToString());
                    }
                }
                
            }
            
            
            return dtResult;
        }
        public DataTable mtdGetData(string[] periodo, string[] meses, string año, ref string strErrMsg, int IdIndicador, string IdArea, string Area)
        {
            DataTable dtResult = new DataTable();
            DataTable dtInfo = new DataTable();
            clsIndicadorRecomendacionesDtpoDAL cDtRegistros = new clsIndicadorRecomendacionesDtpoDAL();
            double valor = 0;
            double implementada = 0;
            double realizada = 0;
            double result = 0;
            double total = 0;
            DataRow dr1 = dtResult.NewRow();
            /*DataRow dr2 = dtResult.NewRow();
            DataRow dr3 = dtResult.NewRow();*/
            dtResult.Columns.Add("Area:", Type.GetType("System.String"));
            dr1["Area:"] = Area;
            for (int i = 0; i < periodo.Length; i++)
            {
                dtResult.Columns.Add(meses[i].ToString(), Type.GetType("System.String"));
                /*valor = cDtRegistros.mtdGetValueRecomendacionesImplementadasDtpo(ref dtInfo, ref strErrMsg, año, periodo[i].ToString(), IdArea);
                dr1[meses[i].ToString()] = valor;

                valor = cDtRegistros.mtdGetValueRecomendacionesRealizadaDtpo(ref dtInfo, ref strErrMsg, año, periodo[i].ToString(), IdArea);
                dr2[meses[i].ToString()] = valor;*/

                implementada = cDtRegistros.mtdGetValueRecomendacionesImplementadasDtpo(ref dtInfo, ref strErrMsg, año, periodo[i].ToString(), IdArea);
                realizada = cDtRegistros.mtdGetValueRecomendacionesRealizadaDtpo(ref dtInfo, ref strErrMsg, año, periodo[i].ToString(), IdArea);
                total = total + realizada;
                if (realizada != 0)
                {
                    result = implementada / total;
                }
                else
                {
                    result = 0;
                }
                dr1[meses[i].ToString()] = result + "%";
            }
            dtResult.Rows.Add(dr1);
            /*dtResult.Rows.Add(dr2);
            dtResult.Rows.Add(dr3);*/
            /*try
            {
                cDtRegistros.mtdUpdateAcumuladoDtpo(IdIndicador, total, ref strErrMsg);
            }
            catch (Exception ex)
            {
                strErrMsg = "Error en la actualización del acumulado: " + ex;
            }*/
            return dtResult;
        }
        /// <summary>
        /// Metodo que permite generar los valores de las recomendaciones por departamento
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public DataTable mtdGetValueRecomendacionDtpoDetallado(ref string strErrMsg, string año, int Frecuencia, int IdIndicador)
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
            clsIndicadorRecomendacionesDtpoDAL cDtRegistros = new clsIndicadorRecomendacionesDtpoDAL();
            booResult = cDtRegistros.mtdConsultarAreasResponsables(ref dtInfo, ref strErrMsg, año);
            string mes = string.Empty;
            int valor = 0;
            double implementada = 0;
            double realizada = 0;
            double result = 0;
            DataTable dtResult = new DataTable();
            if (booResult == true)
            {
                foreach (DataRow row in dtInfo.Rows)
                {
                    if (Frecuencia == 1)
                    {
                        dtResult = mtdGetDataDetallado(mensual, mensualMes, año, ref strErrMsg, IdIndicador, row["IdArea"].ToString(), row["Area"].ToString());
                    }
                    if (Frecuencia == 2)
                    {
                        dtResult = mtdGetDataDetallado(bimestral, bimestralMes, año, ref strErrMsg, IdIndicador, row["IdArea"].ToString(), row["Area"].ToString());
                    }
                    if (Frecuencia == 3)
                    {
                        dtResult = mtdGetDataDetallado(trimestral, trimestralMes, año, ref strErrMsg, IdIndicador, row["IdArea"].ToString(), row["Area"].ToString());
                    }
                    if (Frecuencia == 4)
                    {
                        dtResult = mtdGetDataDetallado(cuatrimestral, cuatrimestralMes, año, ref strErrMsg, IdIndicador, row["IdArea"].ToString(), row["Area"].ToString());
                    }
                    if (Frecuencia == 5)
                    {
                        dtResult = mtdGetDataDetallado(semestral, semestralMes, año, ref strErrMsg, IdIndicador, row["IdArea"].ToString(), row["Area"].ToString());
                    }
                    if (Frecuencia == 6)
                    {
                        dtResult = mtdGetDataDetallado(anual, anualMes, año, ref strErrMsg, IdIndicador, row["IdArea"].ToString(), row["Area"].ToString());
                    }
                }

            }


            return dtResult;
        }
        public DataTable mtdGetDataDetallado(string[] periodo, string[] meses, string año, ref string strErrMsg, int IdIndicador, string IdArea, string Area)
        {
            DataTable dtResult = new DataTable();
            DataTable dtInfo = new DataTable();
            clsIndicadorRecomendacionesDtpoDAL cDtRegistros = new clsIndicadorRecomendacionesDtpoDAL();
            double valor = 0;
            double implementada = 0;
            double realizada = 0;
            double result = 0;
            double total = 0;
            DataRow dr1 = dtResult.NewRow();
            DataRow dr2 = dtResult.NewRow();
            DataRow dr3 = dtResult.NewRow();
            //dtResult.Columns.Add("Area:", Type.GetType("System.String"));
            //dr1["Area:"] = Area;
            for (int i = 0; i < periodo.Length; i++)
            {
                dtResult.Columns.Add(meses[i].ToString(), Type.GetType("System.String"));
                valor = cDtRegistros.mtdGetValueRecomendacionesImplementadasDtpo(ref dtInfo, ref strErrMsg, año, periodo[i].ToString(), IdArea);
                dr1[meses[i].ToString()] = valor;

                valor = cDtRegistros.mtdGetValueRecomendacionesRealizadaDtpo(ref dtInfo, ref strErrMsg, año, periodo[i].ToString(), IdArea);
                dr2[meses[i].ToString()] = valor;

                implementada = cDtRegistros.mtdGetValueRecomendacionesImplementadasDtpo(ref dtInfo, ref strErrMsg, año, periodo[i].ToString(), IdArea);
                realizada = cDtRegistros.mtdGetValueRecomendacionesRealizadaDtpo(ref dtInfo, ref strErrMsg, año, periodo[i].ToString(), IdArea);
                total = total + realizada;
                if (realizada != 0)
                {
                    result = implementada / total;
                }
                else
                {
                    result = 0;
                }
                dr3[meses[i].ToString()] = result + "%";
            }
            dtResult.Rows.Add(dr1);
            dtResult.Rows.Add(dr2);
            dtResult.Rows.Add(dr3);
            try
            {
                cDtRegistros.mtdUpdateAcumuladoDtpo(IdIndicador, total, ref strErrMsg);
            }
            catch (Exception ex)
            {
                strErrMsg = "Error en la actualización del acumulado: " + ex;
            }
            return dtResult;
        }
    }
}