using ListasSarlaft.Classes.BLL.Riesgos.CalculosRedColsa;
using ListasSarlaft.Classes.DTO.Riesgos.ConsolidadoRiesgos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLRiesgosComparativoRiesgos
    {
        /// <summary>
        /// Realiza la consulta de los perfiles de riesgos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTORiesgosComparativoRiesgos> mtdConsultarPefilesRiesgos(bool booEstado, ref string strErrMsg)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsDTORiesgosComparativoRiesgos> lstComparativo = new List<clsDTORiesgosComparativoRiesgos>();
            clsDALRiesgoComparativoRiesgos cDtcomparativo = new clsDALRiesgoComparativoRiesgos();
            clsDTORiesgosComparativoRiesgos objComparativo;
            bool booResutl = false;
            int iteracion = 0;
            #endregion Vars

            booResutl = cDtcomparativo.mtdConsultarPefilesRiesgos(ref dtInfo, ref strErrMsg);

            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        objComparativo = new clsDTORiesgosComparativoRiesgos();
                        objComparativo.intNumeroRegistro = Convert.ToInt32(dr["NumeroRegistros"].ToString().Trim());
                        objComparativo.intSumatoriaProbabilidad = Convert.ToInt32(dr["SumatoriaProbabilidad"].ToString().Trim());
                        objComparativo.intSumatoriaImpacto = Convert.ToInt32(dr["SumatoriaImpacto"].ToString().Trim());
                        objComparativo.intPromedioProbabilidad = objComparativo.intSumatoriaProbabilidad / objComparativo.intNumeroRegistro;
                        objComparativo.intPromedioImpacto = objComparativo.intSumatoriaImpacto / objComparativo.intNumeroRegistro;
                        if (iteracion == 0)
                            objComparativo.strPerfil = "Perfil Riesgo Inherente";
                        if (iteracion == 1)
                            objComparativo.strPerfil = "Perfil Riesgo Residual";
                        lstComparativo.Add(objComparativo);
                        iteracion++;
                    }
                }
                else
                {
                    lstComparativo = null;
                    strErrMsg = "No hay datos de los perfiles.";
                }
            }
            else
                lstComparativo = null;

            return lstComparativo;
        }
        /// <summary>
        /// Realiza la consulta de los perfiles de riesgos
        /// </summary>
        /// <param name="strErrMsg">Mensaje de error</param>
        /// <returns>Retorna si el proceso fue exitoso o no</returns>
        public List<clsDTOCuadroComparativoRiesgos> mtdConsultarCuadroComperativo(bool booEstado, ref string strErrMsg, int opc)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsDTOCuadroComparativoRiesgos> lstComparativo = new List<clsDTOCuadroComparativoRiesgos>();
            clsDALRiesgoComparativoRiesgos cDtcomparativo = new clsDALRiesgoComparativoRiesgos();
            clsDTOCuadroComparativoRiesgos objComparativo;
            bool booResutl = false;
            int iteracion = 0;
            #endregion Vars
            string nombreRiesgo = string.Empty;
            string riesgo = string.Empty;
            int cantTotalRiesgo = 0;
            booResutl = cDtcomparativo.mtdConsultarCuadroComperativo(ref dtInfo, ref strErrMsg, opc);
            cantTotalRiesgo = getCantTotalRiesgo(ref strErrMsg);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {

                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        /*if(dr["ClasificacionRiesgo"].ToString().Trim() != nombreRiesgo && dr["RiesgoInherente"].ToString().Trim() != riesgo)
                        {*/
                        objComparativo = new clsDTOCuadroComparativoRiesgos();
                        objComparativo.strClasificacionRiesgo = dr["NombreClasificacionRiesgo"].ToString().Trim();
                        decimal cantidadInherente = Convert.ToDecimal(dr["IdProbabilidad"].ToString());
                        decimal cantRiesgoInherente = getCantRiesgoInherente(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        int resultProbabilidad = 0;
                        /*if (cantRiesgoInherente > 0)
                            resultProbabilidad = Convert.ToInt32(Math.Round(cantidadInherente / cantRiesgoInherente));*/
                        decimal cantidadInherenteImpacto = Convert.ToDecimal(dr["IdImpacto"].ToString());
                        int resultImpacto = 0;
                        /*if (cantRiesgoInherente > 0)
                            resultImpacto = Convert.ToInt32(Math.Round(cantidadInherenteImpacto / cantRiesgoInherente));*/
                        //objComparativo.strRiesgoInherente = dr["RiesgoInherente"].ToString().Trim();
                        /*if(cantTotalRiesgo > 0)
                            objComparativo.intCantRiesgoInherente = (getCantRiesgoInherente(dr["RiesgoInherente"].ToString().Trim(), dr["ClasificacionRiesgo"].ToString().Trim(), ref strErrMsg) / cantTotalRiesgo);*/
                        //objComparativo.intCantRiesgoInherente = resultProbabilidad;//getCantRiesgoInherente(dr["RiesgoInherente"].ToString().Trim(), dr["ClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        objComparativo.strRiesgoInherente = getRiesgoInherente(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()) , Convert.ToInt32(dr["IdImpacto"].ToString().Trim()));
                        //objComparativo.intCantRiesgoInherente = getValorRiesgoInherente(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()), Convert.ToInt32(dr["IdImpacto"].ToString().Trim()));
                        objComparativo.intCantRiesgoInherente = Convert.ToInt32(dr["cant"].ToString().Trim());
                        decimal cantidadResidualProbabilidad = getCantRiesgoResidualProbabilidad(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        decimal cantidadResidualImpacto = getCantRiesgoResidualImpacto(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        int resultProbabilidadResidual = 0;
                        int resultImpactoResidual = 0;
                        if(cantRiesgoInherente > 0)
                        {
                            resultProbabilidadResidual = Convert.ToInt32(Math.Round(cantidadResidualProbabilidad / cantRiesgoInherente));
                            resultImpactoResidual = Convert.ToInt32(Math.Round(cantidadResidualImpacto / cantRiesgoInherente));
                        }
                        //objComparativo.intCantRiesgoResidual = resultProbabilidadResidual;
                        objComparativo.strRiesgoResidual = getRiesgoInherente(ref strErrMsg, resultProbabilidadResidual, resultImpactoResidual);
                        //objComparativo.intCantRiesgoResidual = Convert.ToInt32(dr["cant"].ToString().Trim());
                        //objComparativo.intCantRiesgoResidual = getValorRiesgoInherente(ref strErrMsg, resultProbabilidad, resultImpacto);
                        /*if(cantTotalRiesgo > 0)
                            objComparativo.intCantRiesgoResidual = (getCantRiesgoResidual(dr["RiesgoResidual"].ToString().Trim(), dr["ClasificacionRiesgo"].ToString().Trim(), ref strErrMsg) / cantTotalRiesgo);*/
                        //objComparativo.intCantRiesgoResidual = getCantRiesgoResidual(dr["RiesgoInherente"].ToString().Trim(), dr["ClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        
                        
                            lstComparativo.Add(objComparativo);
                        
                        
                        /*}
                        nombreRiesgo = dr["ClasificacionRiesgo"].ToString().Trim();
                        riesgo = dr["RiesgoInherente"].ToString().Trim();*/
                        iteracion++;
                    }
                }
                else
                {
                    lstComparativo = null;
                    strErrMsg = "No hay datos para comparar.";
                }
            }
            else
                lstComparativo = null;

            return lstComparativo;
        }
        public List<clsDTOCuadroComparativoRiesgos> mtdConsultarCuadroComperativoResidual(bool booEstado, ref string strErrMsg, int opc)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsDTOCuadroComparativoRiesgos> lstComparativo = new List<clsDTOCuadroComparativoRiesgos>();
            clsDALRiesgoComparativoRiesgos cDtcomparativo = new clsDALRiesgoComparativoRiesgos();
            clsDTOCuadroComparativoRiesgos objComparativo;
            bool booResutl = false;
            int iteracion = 0;
            #endregion Vars
            string nombreRiesgo = string.Empty;
            string riesgo = string.Empty;
            int cantTotalRiesgo = 0;
            booResutl = cDtcomparativo.mtdConsultarCuadroComperativo(ref dtInfo, ref strErrMsg, opc);
            cantTotalRiesgo = getCantTotalRiesgo(ref strErrMsg);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    clsBLLRiesgosCalculos objProcess = new clsBLLRiesgosCalculos();
                    double sumIndicadorSARResidual = 0;
                    int countRiesgoResigual = 1;
                    List<clsDTOCuadroComparativoRiesgosResidual> lstMapaAnt = new List<clsDTOCuadroComparativoRiesgosResidual>();
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        /*if(dr["ClasificacionRiesgo"].ToString().Trim() != nombreRiesgo && dr["RiesgoInherente"].ToString().Trim() != riesgo)
                        {*/
                        objComparativo = new clsDTOCuadroComparativoRiesgos();
                        objComparativo.strClasificacionRiesgo = dr["NombreClasificacionRiesgo"].ToString().Trim();
                        decimal cantidadInherente = Convert.ToDecimal(dr["IdProbabilidad"].ToString());
                        decimal cantRiesgoInherente = getCantRiesgoInherente(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        int resultProbabilidad = 0;
                        /*if (cantRiesgoInherente > 0)
                            resultProbabilidad = Convert.ToInt32(Math.Round(cantidadInherente / cantRiesgoInherente));*/
                        decimal cantidadInherenteImpacto = Convert.ToDecimal(dr["IdImpacto"].ToString());
                        int resultImpacto = 0;
                        /*if (cantRiesgoInherente > 0)
                            resultImpacto = Convert.ToInt32(Math.Round(cantidadInherenteImpacto / cantRiesgoInherente));*/
                        //objComparativo.strRiesgoInherente = dr["RiesgoInherente"].ToString().Trim();
                        /*if(cantTotalRiesgo > 0)
                            objComparativo.intCantRiesgoInherente = (getCantRiesgoInherente(dr["RiesgoInherente"].ToString().Trim(), dr["ClasificacionRiesgo"].ToString().Trim(), ref strErrMsg) / cantTotalRiesgo);*/
                        //objComparativo.intCantRiesgoInherente = resultProbabilidad;//getCantRiesgoInherente(dr["RiesgoInherente"].ToString().Trim(), dr["ClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        objComparativo.strRiesgoInherente = getRiesgoInherente(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()), Convert.ToInt32(dr["IdImpacto"].ToString().Trim()));
                        //objComparativo.intCantRiesgoInherente = getValorRiesgoInherente(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()), Convert.ToInt32(dr["IdImpacto"].ToString().Trim()));
                        //objComparativo.intCantRiesgoInherente = Convert.ToInt32(dr["cant"].ToString().Trim());
                        decimal cantidadResidualProbabilidad = getCantRiesgoResidualProbabilidad(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        decimal cantidadResidualImpacto = getCantRiesgoResidualImpacto(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        int resultProbabilidadResidual = 0;
                        int resultImpactoResidual = 0;
                        /*if (cantRiesgoInherente > 0)
                        {
                            resultProbabilidadResidual = Convert.ToInt32(Math.Round(cantidadResidualProbabilidad / cantRiesgoInherente));
                            resultImpactoResidual = Convert.ToInt32(Math.Round(cantidadResidualImpacto / cantRiesgoInherente));
                        }*/
                        
                        
                        /*for (int rows = 0; rows < dtInfo.Rows.Count; rows++)
                        {*/
                            string efectividad = dr["efectividad"].ToString();
                            string operatividad = dr["operatividad"].ToString();
                            string solidez = objProcess.mtdCalculoSolidezControl(objProcess.mtdCalculoEfectividad(efectividad), objProcess.mtdCalculoOperatividad(operatividad));
                            string valor = objProcess.mtdCalculoSARinherente(dr["IdProbabilidad"].ToString().Trim(), dr["IdImpacto"].ToString().Trim());
                            sumIndicadorSARResidual += Convert.ToDouble(objProcess.mtdCalculoIndicadorSARresidual(valor, solidez/*row["NombreEscala"].ToString()*/));
                            if (countRiesgoResigual == Convert.ToInt32(dr["cantRiesgo"]))
                            {
                                double promedioSAR = sumIndicadorSARResidual / Convert.ToDouble(dr["cantRiesgo"]);
                                string probabilidad = string.Empty;
                                string impacto = string.Empty;
                                objProcess.mtdGetCoordenadaMapa(promedioSAR.ToString(), ref probabilidad, ref impacto);

                                
                                clsDTOCuadroComparativoRiesgosResidual objMapa = new clsDTOCuadroComparativoRiesgosResidual();
                                objMapa.intNumeroRegistros = 0;
                                objMapa.intIdProbabilidad = Convert.ToInt32(probabilidad);
                                objMapa.intIdImpacto = Convert.ToInt32(impacto);
                                objMapa.strClasificacion = objComparativo.strClasificacionRiesgo;
                                lstMapaAnt.Add(objMapa);

                                sumIndicadorSARResidual = 0;
                                countRiesgoResigual = 1;
                            }
                            else
                            {
                                countRiesgoResigual++;
                            }

                        //}
                        //List<clsDTOMapaRiesgoResidual> lstMapaCla = new List<clsDTOMapaRiesgoResidual>();
                        //objComparativo.intCantRiesgoResidual = resultProbabilidadResidual;
                        objComparativo.strRiesgoResidual = getRiesgoInherente(ref strErrMsg, resultProbabilidadResidual, resultImpactoResidual);
                        //objComparativo.intCantRiesgoResidual = Convert.ToInt32(dr["cant"].ToString().Trim());
                        //objComparativo.intCantRiesgoResidual = getValorRiesgoInherente(ref strErrMsg, resultProbabilidad, resultImpacto);
                        /*if(cantTotalRiesgo > 0)
                            objComparativo.intCantRiesgoResidual = (getCantRiesgoResidual(dr["RiesgoResidual"].ToString().Trim(), dr["ClasificacionRiesgo"].ToString().Trim(), ref strErrMsg) / cantTotalRiesgo);*/
                        //objComparativo.intCantRiesgoResidual = getCantRiesgoResidual(dr["RiesgoInherente"].ToString().Trim(), dr["ClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        //lstComparativo.Add(objComparativo);
                        /*}
                        nombreRiesgo = dr["ClasificacionRiesgo"].ToString().Trim();
                        riesgo = dr["RiesgoInherente"].ToString().Trim();*/
                        iteracion++;
                    }
                    dtInfo = new DataTable();
                    cRiesgo cRiesgo = new cRiesgo();
                    dtInfo = cRiesgo.loadDDLClasificacion();
                    for (int i = 0; i < dtInfo.Rows.Count; i++)
                    {
                        var lstMapaCla = lstMapaAnt.Where(x => x.strClasificacion == dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim()).ToList();
                        List<clsDTOMapaRiesgoResidual> lstMapa = new List<clsDTOMapaRiesgoResidual>();
                        lstMapa = objProcess.mtdCountCoordenadaComparatiroRiesgo(lstMapaCla);
                        foreach(clsDTOMapaRiesgoResidual obj in lstMapa)
                            {
                            if(obj.intNumeroRegistros > 0)
                            {
                                objComparativo = new clsDTOCuadroComparativoRiesgos();
                                objComparativo.strClasificacionRiesgo = dtInfo.Rows[i]["NombreClasificacionRiesgo"].ToString().Trim();
                                objComparativo.intCantRiesgoResidual = obj.intNumeroRegistros;
                                objComparativo.strRiesgoResidual = getRiesgoInherente(ref strErrMsg, obj.intIdProbabilidad, obj.intIdImpacto);

                                lstComparativo.Add(objComparativo);
                            }
                        }
                    }
                    
                }
                else
                {
                    lstComparativo = null;
                    strErrMsg = "No hay datos para comparar.";
                }
            }
            else
                lstComparativo = null;

            return lstComparativo;
        }
        public int getCantRiesgoInherente(string Riesgo, ref string strErrMsg)
        {
            int cantRiesgo = 0;
            bool booResutl = false;
            DataTable dtInfo = new DataTable();
            clsDALRiesgoComparativoRiesgos cDtcomparativo = new clsDALRiesgoComparativoRiesgos();
            string nombreRiesgo = string.Empty;
            string riesgo = string.Empty;
            booResutl = cDtcomparativo.mtdConsultarCantidadRiesgoInherente(ref dtInfo, ref strErrMsg, Riesgo);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        cantRiesgo = Convert.ToInt32(dr["cantRiesgo"].ToString().Trim());
                    }
                }
                else
                {
                    cantRiesgo = 0;
                    strErrMsg = "No hay datos para comparar.";
                }
            }
            return cantRiesgo;
        }
        public decimal getCantRiesgoResidualProbabilidad(string Riesgo, ref string strErrMsg)
        {
            decimal cantRiesgo = 0;
            bool booResutl = false;
            DataTable dtInfo = new DataTable();
            clsDALRiesgoComparativoRiesgos cDtcomparativo = new clsDALRiesgoComparativoRiesgos();
            string nombreRiesgo = string.Empty;
            string riesgo = string.Empty;
            booResutl = cDtcomparativo.mtdConsultarCantidadRiesgoPropabilidad(ref dtInfo, ref strErrMsg, Riesgo);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        cantRiesgo = Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim());
                    }
                }
                else
                {
                    cantRiesgo = 0;
                    strErrMsg = "No hay datos para comparar.";
                }
            }
            return cantRiesgo;
        }
        public decimal getCantRiesgoResidualImpacto(string Riesgo, ref string strErrMsg)
        {
            decimal cantRiesgo = 0;
            bool booResutl = false;
            DataTable dtInfo = new DataTable();
            clsDALRiesgoComparativoRiesgos cDtcomparativo = new clsDALRiesgoComparativoRiesgos();
            string nombreRiesgo = string.Empty;
            string riesgo = string.Empty;
            booResutl = cDtcomparativo.mtdConsultarCantidadRiesgoImpacto(ref dtInfo, ref strErrMsg, Riesgo);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        cantRiesgo = Convert.ToInt32(dr["IdImpacto"].ToString().Trim());
                    }
                }
                else
                {
                    cantRiesgo = 0;
                    strErrMsg = "No hay datos para comparar.";
                }
            }
            return cantRiesgo;
        }
        public int getCantTotalRiesgo(ref string strErrMsg)
        {
            int cantRiesgo = 0;
            bool booResutl = false;
            DataTable dtInfo = new DataTable();
            clsDALRiesgoComparativoRiesgos cDtcomparativo = new clsDALRiesgoComparativoRiesgos();
            string nombreRiesgo = string.Empty;
            string riesgo = string.Empty;
            booResutl = cDtcomparativo.mtdConsultarCantidadTotalRiesgos(ref dtInfo, ref strErrMsg);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        cantRiesgo = Convert.ToInt32(dr["cantTotalRiesgo"].ToString().Trim());
                    }
                }
                else
                {
                    cantRiesgo = 0;
                    strErrMsg = "No hay datos para comparar.";
                }
            }
            return cantRiesgo;
        }
        public string getRiesgoInherente(ref string strErrMsg, int probabilidad, int impacto)
        {
            //int cantRiesgo = 0;
            bool booResutl = false;
            DataTable dtInfo = new DataTable();
            clsDALRiesgoComparativoRiesgos cDtcomparativo = new clsDALRiesgoComparativoRiesgos();
            string nombreRiesgo = string.Empty;
            string riesgo = string.Empty;
            booResutl = cDtcomparativo.mtdConsultarRiesgoInherente(ref dtInfo, ref strErrMsg, probabilidad, impacto);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        riesgo = dr["NombreRiesgoInherente"].ToString().Trim();
                    }
                }
                else
                {
                    riesgo ="" ;
                    strErrMsg = "No hay datos para comparar.";
                }
            }
            return riesgo;
        }
        public int getValorRiesgoInherente(ref string strErrMsg, int probabilidad, int impacto)
        {
            //int cantRiesgo = 0;
            bool booResutl = false;
            DataTable dtInfo = new DataTable();
            clsDALRiesgoComparativoRiesgos cDtcomparativo = new clsDALRiesgoComparativoRiesgos();
            string nombreRiesgo = string.Empty;
            int riesgo = 0;
            booResutl = cDtcomparativo.mtdConsultarValorRiesgoInherente(ref dtInfo, ref strErrMsg, probabilidad, impacto);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        riesgo =Convert.ToInt32( dr["ValorRiesgoInherente"].ToString().Trim());
                    }
                }
                else
                {
                    riesgo = 0;
                    strErrMsg = "No hay datos para comparar.";
                }
            }
            return riesgo;
        }
    }
}