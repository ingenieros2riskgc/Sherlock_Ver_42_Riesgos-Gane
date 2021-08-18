using ListasSarlaft.Classes.BLL.Riesgos.CalculosRedColsa;
using ListasSarlaft.Classes.DTO.Riesgos.ConsolidadoRiesgos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsBLLReportePerfilRiesgo
    {
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
            string clasificacion = string.Empty;
            int cantTotalRiesgo = 0;
            decimal sumValor = 0;
            string codRiesgo = string.Empty;
            booResutl = cDtcomparativo.mtdConsultarCuadroComperativo(ref dtInfo, ref strErrMsg, opc);
            cantTotalRiesgo = getCantTotalRiesgo(ref strErrMsg);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    int cantidadRiesgoIn = 0;
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsBLLRiesgosCalculos objProcess = new clsBLLRiesgosCalculos();
                        
                        objComparativo = new clsDTOCuadroComparativoRiesgos();
                        objComparativo.strClasificacionRiesgo = dr["NombreClasificacionRiesgo"].ToString().Trim();
                        decimal cantidadInherente = Convert.ToDecimal(dr["IdProbabilidad"].ToString());
                        decimal cantRiesgoInherente = getCantRiesgoInherente(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        int resultProbabilidad = 0;
                        
                        decimal cantidadInherenteImpacto = Convert.ToDecimal(dr["IdImpacto"].ToString());
                        int resultImpacto = 0;
                        
                        objComparativo.strRiesgoInherente = getRiesgoInherente(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()), Convert.ToInt32(dr["IdImpacto"].ToString().Trim()));
                        
                        objComparativo.intCantRiesgoInherente = Convert.ToInt32(dr["cant"].ToString().Trim());
                        
                        decimal cantidadResidualProbabilidad = getCantRiesgoResidualProbabilidad(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        decimal cantidadResidualImpacto = getCantRiesgoResidualImpacto(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        int resultProbabilidadResidual = 0;
                        int resultImpactoResidual = 0;
                        if (cantRiesgoInherente > 0)
                        {
                            resultProbabilidadResidual = Convert.ToInt32(Math.Round(cantidadResidualProbabilidad / cantRiesgoInherente));
                            resultImpactoResidual = Convert.ToInt32(Math.Round(cantidadResidualImpacto / cantRiesgoInherente));
                        }
                        
                        objComparativo.strRiesgoResidual = getRiesgoInherente(ref strErrMsg, resultProbabilidadResidual, resultImpactoResidual);
                        string efectividad = dr["efectividad"].ToString();
                        string operatividad = dr["operatividad"].ToString();
                        string solidez = objProcess.mtdCalculoSolidezControl(objProcess.mtdCalculoEfectividad(efectividad), objProcess.mtdCalculoOperatividad(operatividad));
                        string valor = objProcess.mtdCalculoSARinherente(dr["IdProbabilidad"].ToString().Trim(), dr["IdImpacto"].ToString().Trim());
                        if (iteracion > 0)
                        {
                            if(clasificacion == objComparativo.strClasificacionRiesgo)
                            {
                                if(codRiesgo != dr["CodRiesgo"].ToString().Trim())
                                {
                                    if (riesgo != objComparativo.strRiesgoInherente)
                                    {
                                        clsDTOCuadroComparativoRiesgos objComparativoOld = new clsDTOCuadroComparativoRiesgos();
                                        objComparativoOld.intCantRiesgoInherente = cantidadRiesgoIn;
                                        objComparativoOld.strClasificacionRiesgo = clasificacion;
                                        objComparativoOld.strRiesgoInherente = riesgo;
                                        objComparativoOld.strRiesgoResidual = "";
                                        objComparativoOld.intCantRiesgoResidual = 0;
                                        objComparativoOld.decDecValoracion = sumValor;
                                        lstComparativo.Add(objComparativoOld);
                                        cantidadRiesgoIn = Convert.ToInt32(dr["cant"].ToString().Trim());
                                        riesgo = objComparativo.strRiesgoInherente;
                                        sumValor = Convert.ToDecimal(valor);
                                    }
                                    else
                                    {
                                        cantidadRiesgoIn += Convert.ToInt32(dr["cant"].ToString().Trim());
                                        sumValor += Convert.ToDecimal(valor);
                                    }
                                }
                                
                            }
                            else
                            {
                                if (riesgo != objComparativo.strRiesgoInherente)
                                {
                                    clsDTOCuadroComparativoRiesgos objComparativoOld = new clsDTOCuadroComparativoRiesgos();
                                objComparativoOld.intCantRiesgoInherente = cantidadRiesgoIn;
                                objComparativoOld.strClasificacionRiesgo = clasificacion;
                                objComparativoOld.strRiesgoInherente = riesgo;
                                objComparativoOld.strRiesgoResidual = "";
                                objComparativoOld.intCantRiesgoResidual = 0;
                                objComparativoOld.decDecValoracion = sumValor;
                                lstComparativo.Add(objComparativoOld);
                                cantidadRiesgoIn = Convert.ToInt32(dr["cant"].ToString().Trim());
                                riesgo = objComparativo.strRiesgoInherente;
                                sumValor = Convert.ToDecimal(valor);
                                    }
                                    else
                                    {
                                        cantidadRiesgoIn += Convert.ToInt32(dr["cant"].ToString().Trim());
                                        sumValor += Convert.ToDecimal(valor);
                                    }
                            }
                        }
                        riesgo = objComparativo.strRiesgoInherente;
                        clasificacion = objComparativo.strClasificacionRiesgo;
                        if(iteracion == 0)
                        {
                            cantidadRiesgoIn = Convert.ToInt32(dr["cant"].ToString().Trim());
                            sumValor = Convert.ToDecimal(valor);
                        }
                        codRiesgo = dr["CodRiesgo"].ToString().Trim();
                        
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
        public List<clsDTOReportRiesgoControl> mtdConsultarRiesgoControl(bool booEstado, ref string strErrMsg, int opc)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsDTOReportRiesgoControl> lstComparativo = new List<clsDTOReportRiesgoControl>();
            clsDALRiesgoComparativoRiesgos cDtcomparativo = new clsDALRiesgoComparativoRiesgos();
            clsDTOCuadroComparativoRiesgos objComparativo;
            bool booResutl = false;
            int iteracion = 0;
            #endregion Vars
            string nombreRiesgo = string.Empty;
            string riesgo = string.Empty;
            string clasificacion = string.Empty;
            int cantTotalRiesgo = 0;
            decimal sumValor = 0;
            string codRiesgo = string.Empty;
            booResutl = cDtcomparativo.mtdConsultarCuadroComperativo(ref dtInfo, ref strErrMsg, opc);
            cantTotalRiesgo = getCantTotalRiesgo(ref strErrMsg);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    int cantidadRiesgoIn = 0;
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsBLLRiesgosCalculos objProcess = new clsBLLRiesgosCalculos();

                        objComparativo = new clsDTOCuadroComparativoRiesgos();
                        objComparativo.strClasificacionRiesgo = dr["RiesgoControl"].ToString().Trim();
                        decimal cantidadInherente = Convert.ToDecimal(dr["IdProbabilidad"].ToString());
                        decimal cantRiesgoInherente = getCantRiesgoInherente(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        int resultProbabilidad = 0;

                        decimal cantidadInherenteImpacto = Convert.ToDecimal(dr["IdImpacto"].ToString());
                        int resultImpacto = 0;

                        objComparativo.strRiesgoInherente = getRiesgoInherente(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()), Convert.ToInt32(dr["IdImpacto"].ToString().Trim()));

                        objComparativo.intCantRiesgoInherente = Convert.ToInt32(dr["cant"].ToString().Trim());

                        decimal cantidadResidualProbabilidad = getCantRiesgoResidualProbabilidad(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        decimal cantidadResidualImpacto = getCantRiesgoResidualImpacto(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        int resultProbabilidadResidual = 0;
                        int resultImpactoResidual = 0;
                        if (cantRiesgoInherente > 0)
                        {
                            resultProbabilidadResidual = Convert.ToInt32(Math.Round(cantidadResidualProbabilidad / cantRiesgoInherente));
                            resultImpactoResidual = Convert.ToInt32(Math.Round(cantidadResidualImpacto / cantRiesgoInherente));
                        }

                        objComparativo.strRiesgoResidual = getRiesgoInherente(ref strErrMsg, resultProbabilidadResidual, resultImpactoResidual);
                        string efectividad = dr["efectividad"].ToString();
                        string operatividad = dr["operatividad"].ToString();
                        string solidez = objProcess.mtdCalculoSolidezControl(objProcess.mtdCalculoEfectividad(efectividad), objProcess.mtdCalculoOperatividad(operatividad));
                        string valor = objProcess.mtdCalculoSARinherente(dr["IdProbabilidad"].ToString().Trim(), dr["IdImpacto"].ToString().Trim());
                        if (iteracion > 0)
                        {
                            if (clasificacion != objComparativo.strClasificacionRiesgo)
                            {
                                //if (codRiesgo != dr["CodRiesgo"].ToString().Trim())
                                //{
                                    if (riesgo != objComparativo.strRiesgoInherente)
                                    {
                                        clsDTOReportRiesgoControl objComparativoOld = new clsDTOReportRiesgoControl();
                                        objComparativoOld.intCantRiesgoInherente = cantidadRiesgoIn;
                                    objComparativoOld.strRiesgoInherente = riesgo;
                                    objComparativoOld.strClasificacionRiesgo = clasificacion;
                                        objComparativoOld.intCantRiesgoResidual = 0;
                                        
                                        lstComparativo.Add(objComparativoOld);
                                        cantidadRiesgoIn = Convert.ToInt32(dr["cant"].ToString().Trim());
                                        //riesgo = objComparativo.strRiesgoInherente;
                                        sumValor = Convert.ToDecimal(valor);
                                    }
                                    else
                                    {
                                        cantidadRiesgoIn += Convert.ToInt32(dr["cant"].ToString().Trim());
                                        sumValor += Convert.ToDecimal(valor);
                                    }
                                //}

                            }
                            else
                            {
                                if (riesgo != objComparativo.strRiesgoInherente)
                                {
                                    clsDTOReportRiesgoControl objComparativoOld = new clsDTOReportRiesgoControl();
                                    objComparativoOld.intCantRiesgoInherente = cantidadRiesgoIn;
                                    objComparativoOld.strClasificacionRiesgo = clasificacion;
                                    objComparativoOld.strRiesgoInherente = riesgo;
                                    objComparativoOld.intCantRiesgoResidual = 0;
                                    
                                    lstComparativo.Add(objComparativoOld);
                                    cantidadRiesgoIn = Convert.ToInt32(dr["cant"].ToString().Trim());
                                    riesgo = objComparativo.strRiesgoInherente;
                                    sumValor = Convert.ToDecimal(valor);
                                }
                                else
                                {
                                    cantidadRiesgoIn += Convert.ToInt32(dr["cant"].ToString().Trim());
                                    sumValor += Convert.ToDecimal(valor);
                                }
                            }
                        }
                        riesgo = objComparativo.strRiesgoInherente;
                        clasificacion = objComparativo.strClasificacionRiesgo;
                        if (iteracion == 0)
                        {
                            cantidadRiesgoIn = Convert.ToInt32(dr["cant"].ToString().Trim());
                            sumValor = Convert.ToDecimal(valor);
                        }
                        codRiesgo = dr["CodRiesgo"].ToString().Trim();

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
        public List<clsDTOReportRiesgoControl> mtdConsultarRiesgoControlResidual(bool booEstado, ref string strErrMsg, int opc)
        {
            #region Vars
            DataTable dtInfo = new DataTable();
            List<clsDTOReportRiesgoControl> lstComparativo = new List<clsDTOReportRiesgoControl>();
            clsDALRiesgoComparativoRiesgos cDtcomparativo = new clsDALRiesgoComparativoRiesgos();
            clsDTOCuadroComparativoRiesgos objComparativo;
            bool booResutl = false;
            int iteracion = 0;
            #endregion Vars
            string nombreRiesgo = string.Empty;
            string riesgo = string.Empty;
            string clasificacion = string.Empty;
            int cantTotalRiesgo = 0;
            decimal sumValor = 0;
            string codRiesgo = string.Empty;
            booResutl = cDtcomparativo.mtdConsultarCuadroComperativo(ref dtInfo, ref strErrMsg, opc);
            cantTotalRiesgo = getCantTotalRiesgo(ref strErrMsg);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    int cantidadRiesgoIn = 0;
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsBLLRiesgosCalculos objProcess = new clsBLLRiesgosCalculos();

                        objComparativo = new clsDTOCuadroComparativoRiesgos();
                        objComparativo.strClasificacionRiesgo = dr["RiesgoControl"].ToString().Trim();
                        decimal cantidadInherente = Convert.ToDecimal(dr["IdProbabilidad"].ToString());
                        decimal cantRiesgoInherente = getCantRiesgoInherente(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        int resultProbabilidad = 0;

                        decimal cantidadInherenteImpacto = Convert.ToDecimal(dr["IdImpacto"].ToString());
                        int resultImpacto = 0;

                        objComparativo.strRiesgoInherente = getRiesgoInherente(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()), Convert.ToInt32(dr["IdImpacto"].ToString().Trim()));

                        objComparativo.intCantRiesgoInherente = Convert.ToInt32(dr["cant"].ToString().Trim());

                        decimal cantidadResidualProbabilidad = getCantRiesgoResidualProbabilidad(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        decimal cantidadResidualImpacto = getCantRiesgoResidualImpacto(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        int resultProbabilidadResidual = 0;
                        int resultImpactoResidual = 0;
                        if (cantRiesgoInherente > 0)
                        {
                            resultProbabilidadResidual = Convert.ToInt32(Math.Round(cantidadResidualProbabilidad / cantRiesgoInherente));
                            resultImpactoResidual = Convert.ToInt32(Math.Round(cantidadResidualImpacto / cantRiesgoInherente));
                        }
                        objComparativo.strRiesgoResidual = dr["RiesgoResidual"].ToString();
                        //objComparativo.strRiesgoResidual = getRiesgoInherente(ref strErrMsg, resultProbabilidadResidual, resultImpactoResidual);
                        string efectividad = dr["efectividad"].ToString();
                        string operatividad = dr["operatividad"].ToString();
                        string solidez = objProcess.mtdCalculoSolidezControl(objProcess.mtdCalculoEfectividad(efectividad), objProcess.mtdCalculoOperatividad(operatividad));
                        string valor = objProcess.mtdCalculoSARinherente(dr["IdProbabilidad"].ToString().Trim(), dr["IdImpacto"].ToString().Trim());

                        string valorResidual = mtdCalculoIndicadorSARresidual(valor, solidez/*row["NombreEscala"].ToString()*/);
                        if (iteracion > 0)
                        {
                            if (clasificacion != objComparativo.strClasificacionRiesgo)
                            {
                                //if (codRiesgo != dr["CodRiesgo"].ToString().Trim())
                                //{
                                //if (riesgo != objComparativo.strRiesgoResidual)
                                //{
                                    clsDTOReportRiesgoControl objComparativoOld = new clsDTOReportRiesgoControl();
                                    objComparativoOld.intCantRiesgoResidual = cantidadRiesgoIn;
                                    objComparativoOld.strRiesgoResidual = riesgo;
                                    objComparativoOld.strClasificacionRiesgo = clasificacion;
                                    //objComparativoOld.intCantRiesgoResidual = 0;

                                    lstComparativo.Add(objComparativoOld);
                                    cantidadRiesgoIn = Convert.ToInt32(dr["cant"].ToString().Trim());
                                    //riesgo = objComparativo.strRiesgoInherente;
                                    sumValor = Convert.ToDecimal(valorResidual);
                                /*}
                                else
                                {
                                    /*cantidadRiesgoIn += Convert.ToInt32(dr["cant"].ToString().Trim());
                                    sumValor += Convert.ToDecimal(valorResidual);
                                    clsDTOReportRiesgoControl objComparativoOld = new clsDTOReportRiesgoControl();
                                    objComparativoOld.intCantRiesgoResidual = cantidadRiesgoIn;
                                    objComparativoOld.strRiesgoResidual = riesgo;
                                    objComparativoOld.strClasificacionRiesgo = clasificacion;
                                    //objComparativoOld.intCantRiesgoResidual = 0;

                                    lstComparativo.Add(objComparativoOld);
                                    cantidadRiesgoIn = Convert.ToInt32(dr["cant"].ToString().Trim());
                                    //riesgo = objComparativo.strRiesgoInherente;
                                    sumValor = Convert.ToDecimal(valorResidual);
                                }*/
                                //}

                            }
                            else
                            {
                                if (riesgo != objComparativo.strRiesgoResidual)
                                {
                                    clsDTOReportRiesgoControl objComparativoOld = new clsDTOReportRiesgoControl();
                                    objComparativoOld.intCantRiesgoResidual = cantidadRiesgoIn;
                                    objComparativoOld.strRiesgoResidual = riesgo;
                                    objComparativoOld.strClasificacionRiesgo = clasificacion;
                                    //objComparativoOld.intCantRiesgoResidual = 0;

                                    lstComparativo.Add(objComparativoOld);
                                    cantidadRiesgoIn = Convert.ToInt32(dr["cant"].ToString().Trim());
                                    riesgo = objComparativo.strRiesgoResidual;
                                    sumValor = Convert.ToDecimal(valorResidual);
                                }
                                else
                                {
                                    cantidadRiesgoIn += Convert.ToInt32(dr["cant"].ToString().Trim());
                                    sumValor += Convert.ToDecimal(valorResidual);
                                }
                            }
                        }
                        riesgo = objComparativo.strRiesgoResidual;
                        clasificacion = objComparativo.strClasificacionRiesgo;
                        if (iteracion == 0)
                        {
                            cantidadRiesgoIn = Convert.ToInt32(dr["cant"].ToString().Trim());
                            sumValor = Convert.ToDecimal(valorResidual);
                        }
                        codRiesgo = dr["CodRiesgo"].ToString().Trim();

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
                    riesgo = "";
                    strErrMsg = "No hay datos para comparar.";
                }
            }
            return riesgo;
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
            string clasificacion = string.Empty;
            int cantTotalRiesgo = 0;
            decimal sumValor = 0;
            string codRiesgo = string.Empty;
            booResutl = cDtcomparativo.mtdConsultarCuadroComperativo(ref dtInfo, ref strErrMsg, opc);
            cantTotalRiesgo = getCantTotalRiesgo(ref strErrMsg);
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    int cantidadRiesgoIn = 0;
                    foreach (DataRow dr in dtInfo.Rows)
                    {
                        clsBLLRiesgosCalculos objProcess = new clsBLLRiesgosCalculos();

                        objComparativo = new clsDTOCuadroComparativoRiesgos();
                        objComparativo.strClasificacionRiesgo = dr["NombreClasificacionRiesgo"].ToString().Trim();
                        decimal cantidadInherente = Convert.ToDecimal(dr["IdProbabilidad"].ToString());
                        decimal cantRiesgoInherente = getCantRiesgoInherente(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        int resultProbabilidad = 0;

                        decimal cantidadInherenteImpacto = Convert.ToDecimal(dr["IdImpacto"].ToString());
                        int resultImpacto = 0;

                        //objComparativo.strRiesgoInherente = getRiesgoInherente(ref strErrMsg, Convert.ToInt32(dr["IdProbabilidad"].ToString().Trim()), Convert.ToInt32(dr["IdImpacto"].ToString().Trim()));
                        objComparativo.strRiesgoResidual = dr["RiesgoResidual"].ToString();
                        objComparativo.intCantRiesgoInherente = Convert.ToInt32(dr["cant"].ToString().Trim());

                        decimal cantidadResidualProbabilidad = getCantRiesgoResidualProbabilidad(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        decimal cantidadResidualImpacto = getCantRiesgoResidualImpacto(dr["NombreClasificacionRiesgo"].ToString().Trim(), ref strErrMsg);
                        int resultProbabilidadResidual = 0;
                        int resultImpactoResidual = 0;
                        if (cantRiesgoInherente > 0)
                        {
                            resultProbabilidadResidual = Convert.ToInt32(Math.Round(cantidadResidualProbabilidad / cantRiesgoInherente));
                            resultImpactoResidual = Convert.ToInt32(Math.Round(cantidadResidualImpacto / cantRiesgoInherente));
                        }

                        //objComparativo.strRiesgoResidual = getRiesgoInherente(ref strErrMsg, resultProbabilidadResidual, resultImpactoResidual);
                        string efectividad = dr["efectividad"].ToString();
                        string operatividad = dr["operatividad"].ToString();
                        string solidez = objProcess.mtdCalculoSolidezControl(objProcess.mtdCalculoEfectividad(efectividad), objProcess.mtdCalculoOperatividad(operatividad));
                        string valor = objProcess.mtdCalculoSARinherente(dr["IdProbabilidad"].ToString().Trim(), dr["IdImpacto"].ToString().Trim());

                        string valorResidual = mtdCalculoIndicadorSARresidual(valor, solidez/*row["NombreEscala"].ToString()*/);
                        if (iteracion > 0)
                        {
                            if (clasificacion == objComparativo.strClasificacionRiesgo)
                            {
                                if (codRiesgo != dr["CodRiesgo"].ToString().Trim())
                                {
                                    if (riesgo != objComparativo.strRiesgoResidual)
                                    {
                                        clsDTOCuadroComparativoRiesgos objComparativoOld = new clsDTOCuadroComparativoRiesgos();
                                        objComparativoOld.intCantRiesgoInherente = 0;
                                        objComparativoOld.strClasificacionRiesgo = clasificacion;
                                        objComparativoOld.strRiesgoInherente = "";
                                        objComparativoOld.strRiesgoResidual = riesgo;
                                        objComparativoOld.intCantRiesgoResidual = cantidadRiesgoIn;
                                        objComparativoOld.decDecValoracion = sumValor;
                                        lstComparativo.Add(objComparativoOld);
                                        cantidadRiesgoIn = Convert.ToInt32(dr["cant"].ToString().Trim());
                                        riesgo = objComparativo.strRiesgoInherente;
                                        sumValor = Convert.ToDecimal(valor);
                                    }
                                    else
                                    {
                                        cantidadRiesgoIn += Convert.ToInt32(dr["cant"].ToString().Trim());
                                        sumValor += Convert.ToDecimal(valorResidual);
                                    }
                                }

                            }
                            else
                            {
                                clsDTOCuadroComparativoRiesgos objComparativoOld = new clsDTOCuadroComparativoRiesgos();
                                objComparativoOld.intCantRiesgoInherente = 0;
                                objComparativoOld.strClasificacionRiesgo = clasificacion;
                                objComparativoOld.strRiesgoInherente = "";
                                objComparativoOld.strRiesgoResidual = riesgo;
                                objComparativoOld.intCantRiesgoResidual = cantidadRiesgoIn;
                                objComparativoOld.decDecValoracion = sumValor;
                                lstComparativo.Add(objComparativoOld);
                                cantidadRiesgoIn = Convert.ToInt32(dr["cant"].ToString().Trim());
                                riesgo = objComparativo.strRiesgoInherente;
                                sumValor = Convert.ToDecimal(valorResidual);
                            }
                        }
                        riesgo = objComparativo.strRiesgoResidual;
                        clasificacion = objComparativo.strClasificacionRiesgo;
                        if (iteracion == 0)
                        {
                            cantidadRiesgoIn = Convert.ToInt32(dr["cant"].ToString().Trim());
                            sumValor = Convert.ToDecimal(valorResidual);
                        }
                        codRiesgo = dr["CodRiesgo"].ToString().Trim();

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
        public static string mtdCalculoIndicadorSARresidual(string IndicadorSAR, string solidez)
        {
            string result = string.Empty;
            /****************************** Solidez = 1 *******************************************/
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "1")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "10")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "11")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "12")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "13")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "14")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "15")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "16")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "17")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "18")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "19")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "2")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "20")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "21")
                result = "1,2";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "22")
                result = "1,2";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "23")
                result = "1,9";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "24")
                result = "2,9";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "25")
                result = "3,7";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "3")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "4")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "5")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "6")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "7")
                result = "1";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "8")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "9")
                result = "2,6";
            /****************************** Solidez = 2 *******************************************/
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "1")
                result = "1";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "10")
                result = "3,9";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "11")
                result = "1";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "12")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "13")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "14")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "15")
                result = "3,9";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "16")
                result = "1,2";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "17")
                result = "1,9";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "18")
                result = "2,9";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "19")
                result = "3,7";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "2")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "20")
                result = "4,5";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "21")
                result = "1,4";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "22")
                result = "2,3";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "23")
                result = "3,37";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "24")
                result = "4,12";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "25")
                result = "4,62";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "3")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "1" && mtdCalculoRiesgoResidual(IndicadorSAR) == "4")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "5")
                result = "3,9";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "6")
                result = "1";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "7")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "8")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "2" && mtdCalculoRiesgoResidual(IndicadorSAR) == "9")
                result = "3,62";
            /****************************** Solidez = 3 *******************************************/
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "1")
                result = "1";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "10")
                result = "3,9";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "11")
                result = "1,2";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "12")
                result = "1,9";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "13")
                result = "2,9";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "14")
                result = "3,7";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "15")
                result = "4,5";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "16")
                result = "1,4";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "17")
                result = "2,3";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "18")
                result = "3,37";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "19")
                result = "4,12";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "2")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "20")
                result = "4,62";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "21")
                result = "2";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "22")
                result = "3,12";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "23")
                result = "3,5";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "24")
                result = "4,25";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "25")
                result = "4,75";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "3")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "4")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "5")
                result = "3,9";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "6")
                result = "1";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "7")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "8")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "3" && mtdCalculoRiesgoResidual(IndicadorSAR) == "9")
                result = "3,62";
            /****************************** Solidez = 4 *******************************************/
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "1")
                result = "1";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "10")
                result = "4,5";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "11")
                result = "1,4";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "12")
                result = "2,3";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "13")
                result = "3,37";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "14")
                result = "4,12";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "15")
                result = "4,62";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "16")
                result = "2";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "17")
                result = "3,12";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "18")
                result = "3,5";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "19")
                result = "4,25";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "2")
                result = "1,6";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "20")
                result = "4,75";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "21")
                result = "3";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "22")
                result = "3,25";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "23")
                result = "4";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "24")
                result = "4,37";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "25")
                result = "5";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "3")
                result = "2,6";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "4")
                result = "3,62";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "5")
                result = "3,9";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "6")
                result = "1,2";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "7")
                result = "1,9";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "8")
                result = "2,9";
            if (mtdCalculoControl(solidez) == "4" && mtdCalculoRiesgoResidual(IndicadorSAR) == "9")
                result = "3,7";
            return result;
        }
        public static string mtdCalculoControl(string solidez)
        {
            string result = string.Empty;
            if (solidez == "Excelente")
                result = "1";
            if (solidez == "Bueno")
                result = "2";
            if (solidez == "Regular")
                result = "3";
            if (solidez == "Deficiente")
                result = "4";
            return result;
        }
        public static string mtdCalculoRiesgoResidual(string IndicadorSAR)
        {
            string result = string.Empty;
            if (IndicadorSAR == "1")
                result = "1";
            if (IndicadorSAR == "1,2")
                result = "6";
            if (IndicadorSAR == "1,4")
                result = "11";
            if (IndicadorSAR == "1,6")
                result = "2";
            if (IndicadorSAR == "1,9")
                result = "7";
            if (IndicadorSAR == "2")
                result = "16";
            if (IndicadorSAR == "2,3")
                result = "12";
            if (IndicadorSAR == "2,6")
                result = "3";
            if (IndicadorSAR == "2,9")
                result = "8";
            if (IndicadorSAR == "3")
                result = "21";
            if (IndicadorSAR == "3,12")
                result = "17";
            if (IndicadorSAR == "3,25")
                result = "22";
            if (IndicadorSAR == "3,37")
                result = "13";
            if (IndicadorSAR == "3,5")
                result = "18";
            if (IndicadorSAR == "3,62")
                result = "4";
            if (IndicadorSAR == "3,7")
                result = "9";
            if (IndicadorSAR == "3,9")
                result = "5";
            if (IndicadorSAR == "4")
                result = "23";
            if (IndicadorSAR == "4,12")
                result = "14";
            if (IndicadorSAR == "4,25")
                result = "19";
            if (IndicadorSAR == "4,37")
                result = "24";
            if (IndicadorSAR == "4,5")
                result = "10";
            if (IndicadorSAR == "4,62")
                result = "15";
            if (IndicadorSAR == "4,75")
                result = "20";
            if (IndicadorSAR == "5")
                result = "25";
            return result;
        }
    }
}