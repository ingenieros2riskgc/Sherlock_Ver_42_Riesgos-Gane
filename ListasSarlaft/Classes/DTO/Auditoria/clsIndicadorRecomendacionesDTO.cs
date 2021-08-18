using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsIndicadorRecomendacionesDTO
    {
        private int _IdIndicador;
        private string _Indicador;
        private string _Metodologia;
        private string _Frecuencia;
        private int _IdFrecuencia;
        private string _Responsable;
        private int _Meta;
        private int _Acumulado;
        private int _AcumuladoDtpo;
        private int _Cumplimiento;
        public int intIdIndicador
        {
            get { return _IdIndicador; }
            set { _IdIndicador = value; }
        }
        public string strIndicador
        {
            get { return _Indicador; }
            set { _Indicador = value; }
        }
        public string strMetodologia
        {
            get { return _Metodologia; }
            set { _Metodologia = value; }
        }
        public string strFrecuencia
        {
            get { return _Frecuencia; }
            set { _Frecuencia = value; }
        }
        public int intIdFrecuencia
        {
            get { return _IdFrecuencia; }
            set { _IdFrecuencia = value; }
        }
        public string strResponsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }
        public int intMeta
        {
            get { return _Meta; }
            set { _Meta = value; }
        }
        public int intAcumulado
        {
            get { return _Acumulado; }
            set { _Acumulado = value; }
        }
        public int intAcumuladoDtpo
        {
            get { return _AcumuladoDtpo; }
            set { _AcumuladoDtpo = value; }
        }
        public int intCumplimiento
        {
            get { return _Cumplimiento; }
            set { _Cumplimiento = value; }
        }

        public clsIndicadorRecomendacionesDTO() { }
    }
}