using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListasSarlaft.Classes.DTO.Riesgos.ConsolidadoRiesgos;
namespace ListasSarlaft.Classes.BLL.Riesgos.CalculosRedColsa
{
    public class clsBLLRiesgosCalculos
    {
        public string mtdCalculoSARinherente(string frecuencia, string impacto)
        {
            string result = string.Empty;
            if (frecuencia == "1" && impacto == "1")
                result = "1";
            if (frecuencia == "1" && impacto == "2")
                result = "1,6";
            if (frecuencia == "1" && impacto == "3")
                result = "2,6";
            if (frecuencia == "1" && impacto == "4")
                result = "3,62";
            if (frecuencia == "1" && impacto == "5")
                result = "3,9";
            if (frecuencia == "2" && impacto == "1")
                result = "1,2";
            if (frecuencia == "2" && impacto == "2")
                result = "1,9";
            if (frecuencia == "2" && impacto == "3")
                result = "2,9";
            if (frecuencia == "2" && impacto == "4")
                result = "2,9";
            if (frecuencia == "2" && impacto == "5")
                result = "4,5";
            if (frecuencia == "3" && impacto == "1")
                result = "1,4";
            if (frecuencia == "3" && impacto == "2")
                result = "2,3";
            if (frecuencia == "3" && impacto == "3")
                result = "3,37";
            if (frecuencia == "3" && impacto == "4")
                result = "4,12";
            if (frecuencia == "3" && impacto == "5")
                result = "4,62";
            if (frecuencia == "4" && impacto == "1")
                result = "2";
            if (frecuencia == "4" && impacto == "2")
                result = "3,12";
            if (frecuencia == "4" && impacto == "3")
                result = "3,5";
            if (frecuencia == "4" && impacto == "4")
                result = "4,25";
            if (frecuencia == "4" && impacto == "5")
                result = "4,75";
            if (frecuencia == "5" && impacto == "1")
                result = "3";
            if (frecuencia == "5" && impacto == "2")
                result = "3,25";
            if (frecuencia == "5" && impacto == "3")
                result = "4";
            if (frecuencia == "5" && impacto == "4")
                result = "4,37";
            if (frecuencia == "5" && impacto == "5")
                result = "5";
            return result;
        }
        public void mtdGetCoordenadaMapa(string SARinherente, ref string coox, ref string cooy)
        {
            if(Convert.ToDouble(SARinherente) >= 1 && Convert.ToDouble(SARinherente) < 1.199)
            {
                coox = "1";
                cooy = "1";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 1.20 && Convert.ToDouble(SARinherente) < 1.399)
            {
                coox = "2";
                cooy = "1";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 1.40 && Convert.ToDouble(SARinherente) < 1.599)
            {
                coox = "3";
                cooy = "1";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 2 && Convert.ToDouble(SARinherente) <= 2.29)
            {
                coox = "4";
                cooy = "1";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3 && Convert.ToDouble(SARinherente) <= 3.119)
            {
                coox = "5";
                cooy = "1";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 1.60 && Convert.ToDouble(SARinherente) < 1.899)
            {
                coox = "1";
                cooy = "2";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 1.90 && Convert.ToDouble(SARinherente) < 1.999)
            {
                coox = "2";
                cooy = "2";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 2.30 && Convert.ToDouble(SARinherente) < 2.599)
            {
                coox = "3";
                cooy = "2";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3.12 && Convert.ToDouble(SARinherente) < 3.249)
            {
                coox = "4";
                cooy = "2";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3.25 && Convert.ToDouble(SARinherente) <= 3.369)
            {
                coox = "5";
                cooy = "2";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 2.60 && Convert.ToDouble(SARinherente) < 2.899)
            {
                coox = "1";
                cooy = "3";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 2.90 && Convert.ToDouble(SARinherente) < 2.999)
            {
                coox = "2";
                cooy = "3";
            }
            if (Convert.ToDouble(SARinherente) >= 3.37 && Convert.ToDouble(SARinherente) <= 3.499)
            {
                coox = "3";
                cooy = "3";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3.5 && Convert.ToDouble(SARinherente) < 3.619)
            {
                coox = "4";
                cooy = "3";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4 && Convert.ToDouble(SARinherente) <= 4.119)
            {
                coox = "5";
                cooy = "3";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3.62 && Convert.ToDouble(SARinherente) < 3.699)
            {
                coox = "1";
                cooy = "4";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3.70 && Convert.ToDouble(SARinherente) <= 3.899)
            {
                coox = "2";
                cooy = "4";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4.12 && Convert.ToDouble(SARinherente) <= 4.249)
            {
                coox = "3";
                cooy = "4";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4.25 && Convert.ToDouble(SARinherente) < 4.369)
            {
                coox = "4";
                cooy = "4";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4.37 && Convert.ToDouble(SARinherente) <= 4.499)
            {
                coox = "5";
                cooy = "4";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3.90 && Convert.ToDouble(SARinherente) < 3.999)
            {
                coox = "1";
                cooy = "5";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4.50 && Convert.ToDouble(SARinherente) <= 4.619)
            {
                coox = "2";
                cooy = "5";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4.62 && Convert.ToDouble(SARinherente) <= 4.749)
            {
                coox = "3";
                cooy = "5";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4.75 && Convert.ToDouble(SARinherente) <= 4.999)
            {
                coox = "4";
                cooy = "5";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 5 && Convert.ToDouble(SARinherente) <= 6)
            {
                coox = "5";
                cooy = "5";
                return;
            }
        }
        public void mtdGetRiesgoResidual(string SARinherente, ref string RiesgoResidual)
        {
            if (Convert.ToDouble(SARinherente) >= 1 && Convert.ToDouble(SARinherente) < 1.199)
            {
                RiesgoResidual = "Bajo";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 1.20 && Convert.ToDouble(SARinherente) < 1.399)
            {
                RiesgoResidual = "Bajo";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 1.40 && Convert.ToDouble(SARinherente) < 1.599)
            {
                RiesgoResidual = "Bajo";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 2 && Convert.ToDouble(SARinherente) <= 2.29)
            {
                RiesgoResidual = "Moderado";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3 && Convert.ToDouble(SARinherente) <= 3.119)
            {
                RiesgoResidual = "Alto";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 1.60 && Convert.ToDouble(SARinherente) < 1.899)
            {
                RiesgoResidual = "Bajo";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 1.90 && Convert.ToDouble(SARinherente) < 1.999)
            {
                RiesgoResidual = "Bajo";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 2.30 && Convert.ToDouble(SARinherente) < 2.599)
            {
                RiesgoResidual = "Moderado";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3.12 && Convert.ToDouble(SARinherente) < 3.249)
            {
                RiesgoResidual = "Alto";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3.25 && Convert.ToDouble(SARinherente) <= 3.369)
            {
                RiesgoResidual = "Alto";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 2.60 && Convert.ToDouble(SARinherente) < 2.899)
            {
                RiesgoResidual = "Moderado";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 2.90 && Convert.ToDouble(SARinherente) < 2.999)
            {
                RiesgoResidual = "Moderado";
            }
            if (Convert.ToDouble(SARinherente) >= 3.37 && Convert.ToDouble(SARinherente) <= 3.499)
            {
                RiesgoResidual = "Alto";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3.5 && Convert.ToDouble(SARinherente) < 3.619)
            {
                RiesgoResidual = "Alto";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4 && Convert.ToDouble(SARinherente) <= 4.119)
            {
                RiesgoResidual = "Extremo";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3.62 && Convert.ToDouble(SARinherente) < 3.699)
            {
                RiesgoResidual = "Alto";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3.70 && Convert.ToDouble(SARinherente) <= 3.899)
            {
                RiesgoResidual = "Alto";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4.12 && Convert.ToDouble(SARinherente) <= 4.249)
            {
                RiesgoResidual = "Extremo";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4.25 && Convert.ToDouble(SARinherente) < 4.369)
            {
                RiesgoResidual = "Extremo";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4.37 && Convert.ToDouble(SARinherente) <= 4.499)
            {
                RiesgoResidual = "Extremo";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 3.90 && Convert.ToDouble(SARinherente) < 3.999)
            {
                RiesgoResidual = "Alto";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4.50 && Convert.ToDouble(SARinherente) <= 4.619)
            {
                RiesgoResidual = "Extremo";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4.62 && Convert.ToDouble(SARinherente) <= 4.749)
            {
                RiesgoResidual = "Extremo";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 4.75 && Convert.ToDouble(SARinherente) <= 4.999)
            {
                RiesgoResidual = "Extremo";
                return;
            }
            if (Convert.ToDouble(SARinherente) >= 5 && Convert.ToDouble(SARinherente) <= 6)
            {
                RiesgoResidual = "Extremo";
                return;
            }
        }
        public string mtdCalculoEfectividad(string efectividad)
        {
            string result = string.Empty;
            if (efectividad == "Muy Fuerte")
                result = "1";
            if (efectividad == "Fuerte")
                result = "2";
            if (efectividad == "Moderada")
                result = "3";
            if (efectividad == "Débil")
                result = "4";
            return result;
        }
        public string mtdCalculoOperatividad(string operatividad)
        {
            string result = string.Empty;
            if (operatividad == "Alta")
                result = "1";
            if (operatividad == "Media")
                result = "2";
            if (operatividad == "Baja")
                result = "3";
            if (operatividad == "Muy Baja")
                result = "4";
            return result;
        }
        public string mtdCalculoSolidezControl(string efectividad, string operatividad)
        {
            string result = string.Empty;
            if (efectividad == "1" && operatividad == "1")
                result = "Excelente";
            if (efectividad == "1" && operatividad == "2")
                result = "Bueno";
            if (efectividad == "1" && operatividad == "3")
                result = "Regular";
            if (efectividad == "1" && operatividad == "4")
                result = "Deficiente";
            if (efectividad == "2" && operatividad == "1")
                result = "Excelente";
            if (efectividad == "2" && operatividad == "2")
                result = "Bueno";
            if (efectividad == "2" && operatividad == "3")
                result = "Regular";
            if (efectividad == "2" && operatividad == "4")
                result = "Deficiente";
            if (efectividad == "3" && operatividad == "1")
                result = "Bueno";
            if (efectividad == "3" && operatividad == "2")
                result = "Bueno";
            if (efectividad == "3" && operatividad == "3")
                result = "Regular";
            if (efectividad == "3" && operatividad == "4")
                result = "Deficiente";
            if (efectividad == "4" && operatividad == "1")
                result = "Regular";
            if (efectividad == "4" && operatividad == "2")
                result = "Regular";
            if (efectividad == "4" && operatividad == "3")
                result = "Deficiente";
            if (efectividad == "4" && operatividad == "4")
                result = "Deficiente";
            return result;
        }
        public string mtdCalculoIndicadorSARresidual(string IndicadorSAR, string solidez)
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
        public List<clsDTOMapaRiesgoResidual> mtdCountCoordenada(List<clsDTOMapaRiesgoResidual> coordenadas)
        {
            int count11 = 0;
            int count12 = 0;
            int count13 = 0;
            int count14 = 0;
            int count15 = 0;
            int count21 = 0;
            int count22 = 0;
            int count23 = 0;
            int count24 = 0;
            int count25 = 0;
            int count31 = 0;
            int count32 = 0;
            int count33 = 0;
            int count34 = 0;
            int count35 = 0;
            int count41 = 0;
            int count42 = 0;
            int count43 = 0;
            int count44 = 0;
            int count45 = 0;
            int count51 = 0;
            int count52 = 0;
            int count53 = 0;
            int count54 = 0;
            int count55 = 0;
            foreach (clsDTOMapaRiesgoResidual obj in coordenadas)
            {
                if(obj.intIdProbabilidad.ToString() == "1" && obj.intIdImpacto.ToString() == "1")
                {
                    count11++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "2" && obj.intIdImpacto.ToString() == "1")
                {
                    count12++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "3" && obj.intIdImpacto.ToString() == "1")
                {
                    count13++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "4" && obj.intIdImpacto.ToString() == "1")
                {
                    count14++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "5" && obj.intIdImpacto.ToString() == "1")
                {
                    count15++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "1" && obj.intIdImpacto.ToString() == "2")
                {
                    count21++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "2" && obj.intIdImpacto.ToString() == "2")
                {
                    count22++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "3" && obj.intIdImpacto.ToString() == "2")
                {
                    count23++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "4" && obj.intIdImpacto.ToString() == "2")
                {
                    count24++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "5" && obj.intIdImpacto.ToString() == "2")
                {
                    count25++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "1" && obj.intIdImpacto.ToString() == "3")
                {
                    count31++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "2" && obj.intIdImpacto.ToString() == "3")
                {
                    count32++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "3" && obj.intIdImpacto.ToString() == "3")
                {
                    count33++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "4" && obj.intIdImpacto.ToString() == "3")
                {
                    count34++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "5" && obj.intIdImpacto.ToString() == "3")
                {
                    count35++;
                   
                }
                if (obj.intIdProbabilidad.ToString() == "1" && obj.intIdImpacto.ToString() == "4")
                {
                    count41++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "2" && obj.intIdImpacto.ToString() == "4")
                {
                    count42++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "3" && obj.intIdImpacto.ToString() == "4")
                {
                    count43++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "4" && obj.intIdImpacto.ToString() == "4")
                {
                    count44++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "5" && obj.intIdImpacto.ToString() == "4")
                {
                    count45++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "1" && obj.intIdImpacto.ToString() == "5")
                {
                    count51++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "2" && obj.intIdImpacto.ToString() == "5")
                {
                    count52++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "3" && obj.intIdImpacto.ToString() == "5")
                {
                    count53++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "4" && obj.intIdImpacto.ToString() == "5")
                {
                    count54++;
                    
                }
                if (obj.intIdProbabilidad.ToString() == "5" && obj.intIdImpacto.ToString() == "5")
                {
                    count55++;
                    
                }
            }
            clsDTOMapaRiesgoResidual objMapa = new clsDTOMapaRiesgoResidual();
            List<clsDTOMapaRiesgoResidual> lstNewMapa = new List<clsDTOMapaRiesgoResidual>();
            objMapa.intNumeroRegistros = count11;
            objMapa.intIdProbabilidad = 1;
            objMapa.intIdImpacto = 1;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count12;
            objMapa.intIdProbabilidad = 2;
            objMapa.intIdImpacto = 1;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count13;
            objMapa.intIdProbabilidad = 3;
            objMapa.intIdImpacto = 1;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count14;
            objMapa.intIdProbabilidad = 4;
            objMapa.intIdImpacto = 1;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count15;
            objMapa.intIdProbabilidad = 5;
            objMapa.intIdImpacto = 1;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count21;
            objMapa.intIdProbabilidad = 1;
            objMapa.intIdImpacto = 2;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count22;
            objMapa.intIdProbabilidad = 2;
            objMapa.intIdImpacto = 2;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count23;
            objMapa.intIdProbabilidad = 3;
            objMapa.intIdImpacto = 2;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count24;
            objMapa.intIdProbabilidad = 4;
            objMapa.intIdImpacto = 2;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count25;
            objMapa.intIdProbabilidad = 5;
            objMapa.intIdImpacto = 2;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count31;
            objMapa.intIdProbabilidad = 1;
            objMapa.intIdImpacto = 3;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count32;
            objMapa.intIdProbabilidad = 2;
            objMapa.intIdImpacto = 3;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count33;
            objMapa.intIdProbabilidad = 3;
            objMapa.intIdImpacto = 3;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count34;
            objMapa.intIdProbabilidad = 4;
            objMapa.intIdImpacto = 3;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count35;
            objMapa.intIdProbabilidad = 5;
            objMapa.intIdImpacto = 3;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count41;
            objMapa.intIdProbabilidad = 1;
            objMapa.intIdImpacto = 4;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count42;
            objMapa.intIdProbabilidad = 2;
            objMapa.intIdImpacto = 4;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count43;
            objMapa.intIdProbabilidad = 3;
            objMapa.intIdImpacto = 4;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count44;
            objMapa.intIdProbabilidad = 4;
            objMapa.intIdImpacto = 4;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count45;
            objMapa.intIdProbabilidad = 5;
            objMapa.intIdImpacto = 4;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count51;
            objMapa.intIdProbabilidad = 1;
            objMapa.intIdImpacto = 5;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count52;
            objMapa.intIdProbabilidad = 2;
            objMapa.intIdImpacto = 5;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count53;
            objMapa.intIdProbabilidad = 3;
            objMapa.intIdImpacto = 5;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count54;
            objMapa.intIdProbabilidad = 4;
            objMapa.intIdImpacto = 5;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count55;
            objMapa.intIdProbabilidad = 5;
            objMapa.intIdImpacto = 5;
            lstNewMapa.Add(objMapa);
            return lstNewMapa;
        }
        public List<clsDTOMapaRiesgoResidual> mtdCountCoordenadaComparatiroRiesgo(List<clsDTOCuadroComparativoRiesgosResidual> coordenadas)
        {
            int count11 = 0;
            int count12 = 0;
            int count13 = 0;
            int count14 = 0;
            int count15 = 0;
            int count21 = 0;
            int count22 = 0;
            int count23 = 0;
            int count24 = 0;
            int count25 = 0;
            int count31 = 0;
            int count32 = 0;
            int count33 = 0;
            int count34 = 0;
            int count35 = 0;
            int count41 = 0;
            int count42 = 0;
            int count43 = 0;
            int count44 = 0;
            int count45 = 0;
            int count51 = 0;
            int count52 = 0;
            int count53 = 0;
            int count54 = 0;
            int count55 = 0;
            foreach (clsDTOCuadroComparativoRiesgosResidual obj in coordenadas)
            {
                if (obj.intIdProbabilidad.ToString() == "1" && obj.intIdImpacto.ToString() == "1")
                {
                    count11++;

                }
                if (obj.intIdProbabilidad.ToString() == "2" && obj.intIdImpacto.ToString() == "1")
                {
                    count12++;

                }
                if (obj.intIdProbabilidad.ToString() == "3" && obj.intIdImpacto.ToString() == "1")
                {
                    count13++;

                }
                if (obj.intIdProbabilidad.ToString() == "4" && obj.intIdImpacto.ToString() == "1")
                {
                    count14++;

                }
                if (obj.intIdProbabilidad.ToString() == "5" && obj.intIdImpacto.ToString() == "1")
                {
                    count15++;

                }
                if (obj.intIdProbabilidad.ToString() == "1" && obj.intIdImpacto.ToString() == "2")
                {
                    count21++;

                }
                if (obj.intIdProbabilidad.ToString() == "2" && obj.intIdImpacto.ToString() == "2")
                {
                    count22++;

                }
                if (obj.intIdProbabilidad.ToString() == "3" && obj.intIdImpacto.ToString() == "2")
                {
                    count23++;

                }
                if (obj.intIdProbabilidad.ToString() == "4" && obj.intIdImpacto.ToString() == "2")
                {
                    count24++;

                }
                if (obj.intIdProbabilidad.ToString() == "5" && obj.intIdImpacto.ToString() == "2")
                {
                    count25++;

                }
                if (obj.intIdProbabilidad.ToString() == "1" && obj.intIdImpacto.ToString() == "3")
                {
                    count31++;

                }
                if (obj.intIdProbabilidad.ToString() == "2" && obj.intIdImpacto.ToString() == "3")
                {
                    count32++;

                }
                if (obj.intIdProbabilidad.ToString() == "3" && obj.intIdImpacto.ToString() == "3")
                {
                    count33++;

                }
                if (obj.intIdProbabilidad.ToString() == "4" && obj.intIdImpacto.ToString() == "3")
                {
                    count34++;

                }
                if (obj.intIdProbabilidad.ToString() == "5" && obj.intIdImpacto.ToString() == "3")
                {
                    count35++;

                }
                if (obj.intIdProbabilidad.ToString() == "1" && obj.intIdImpacto.ToString() == "4")
                {
                    count41++;

                }
                if (obj.intIdProbabilidad.ToString() == "2" && obj.intIdImpacto.ToString() == "4")
                {
                    count42++;

                }
                if (obj.intIdProbabilidad.ToString() == "3" && obj.intIdImpacto.ToString() == "4")
                {
                    count43++;

                }
                if (obj.intIdProbabilidad.ToString() == "4" && obj.intIdImpacto.ToString() == "4")
                {
                    count44++;

                }
                if (obj.intIdProbabilidad.ToString() == "5" && obj.intIdImpacto.ToString() == "4")
                {
                    count45++;

                }
                if (obj.intIdProbabilidad.ToString() == "1" && obj.intIdImpacto.ToString() == "5")
                {
                    count51++;

                }
                if (obj.intIdProbabilidad.ToString() == "2" && obj.intIdImpacto.ToString() == "5")
                {
                    count52++;

                }
                if (obj.intIdProbabilidad.ToString() == "3" && obj.intIdImpacto.ToString() == "5")
                {
                    count53++;

                }
                if (obj.intIdProbabilidad.ToString() == "4" && obj.intIdImpacto.ToString() == "5")
                {
                    count54++;

                }
                if (obj.intIdProbabilidad.ToString() == "5" && obj.intIdImpacto.ToString() == "5")
                {
                    count55++;

                }
            }
            clsDTOMapaRiesgoResidual objMapa = new clsDTOMapaRiesgoResidual();
            List<clsDTOMapaRiesgoResidual> lstNewMapa = new List<clsDTOMapaRiesgoResidual>();
            objMapa.intNumeroRegistros = count11;
            objMapa.intIdProbabilidad = 1;
            objMapa.intIdImpacto = 1;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count12;
            objMapa.intIdProbabilidad = 2;
            objMapa.intIdImpacto = 1;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count13;
            objMapa.intIdProbabilidad = 3;
            objMapa.intIdImpacto = 1;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count14;
            objMapa.intIdProbabilidad = 4;
            objMapa.intIdImpacto = 1;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count15;
            objMapa.intIdProbabilidad = 5;
            objMapa.intIdImpacto = 1;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count21;
            objMapa.intIdProbabilidad = 1;
            objMapa.intIdImpacto = 2;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count22;
            objMapa.intIdProbabilidad = 2;
            objMapa.intIdImpacto = 2;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count23;
            objMapa.intIdProbabilidad = 3;
            objMapa.intIdImpacto = 2;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count24;
            objMapa.intIdProbabilidad = 4;
            objMapa.intIdImpacto = 2;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count25;
            objMapa.intIdProbabilidad = 5;
            objMapa.intIdImpacto = 2;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count31;
            objMapa.intIdProbabilidad = 1;
            objMapa.intIdImpacto = 3;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count32;
            objMapa.intIdProbabilidad = 2;
            objMapa.intIdImpacto = 3;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count33;
            objMapa.intIdProbabilidad = 3;
            objMapa.intIdImpacto = 3;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count34;
            objMapa.intIdProbabilidad = 4;
            objMapa.intIdImpacto = 3;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count35;
            objMapa.intIdProbabilidad = 5;
            objMapa.intIdImpacto = 3;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count41;
            objMapa.intIdProbabilidad = 1;
            objMapa.intIdImpacto = 4;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count42;
            objMapa.intIdProbabilidad = 2;
            objMapa.intIdImpacto = 4;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count43;
            objMapa.intIdProbabilidad = 3;
            objMapa.intIdImpacto = 4;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count44;
            objMapa.intIdProbabilidad = 4;
            objMapa.intIdImpacto = 4;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count45;
            objMapa.intIdProbabilidad = 5;
            objMapa.intIdImpacto = 4;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count51;
            objMapa.intIdProbabilidad = 1;
            objMapa.intIdImpacto = 5;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count52;
            objMapa.intIdProbabilidad = 2;
            objMapa.intIdImpacto = 5;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count53;
            objMapa.intIdProbabilidad = 3;
            objMapa.intIdImpacto = 5;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count54;
            objMapa.intIdProbabilidad = 4;
            objMapa.intIdImpacto = 5;
            lstNewMapa.Add(objMapa);
            objMapa = new clsDTOMapaRiesgoResidual();
            objMapa.intNumeroRegistros = count55;
            objMapa.intIdProbabilidad = 5;
            objMapa.intIdImpacto = 5;
            lstNewMapa.Add(objMapa);
            return lstNewMapa;
        }
    }
}