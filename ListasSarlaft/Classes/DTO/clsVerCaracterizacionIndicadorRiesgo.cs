using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListasSarlaft.Classes
{
    public class clsVerCaracterizacionIndicadorRiesgo
    {
        private string _NombreIndicador;
        private string _DescripcionIndicador;
        private int _IdIndicador;
        private string _CodigoRiesgo;
        private string _NombreRiesgo;
        private string _DescripcionRiesgo;
        private int _IdRiesgo;
        private string _CodigoControl;
        private string _NombreControl;

        public string strNombreIndicador
        {
            get { return _NombreIndicador; }
            set { _NombreIndicador = value; }
        }
        public string strDescripcionIndicador
        {
            get { return _DescripcionIndicador; }
            set { _DescripcionIndicador = value; }
        }
        public int intIdIndicador
        {
            get { return _IdIndicador; }
            set { _IdIndicador = value; }
        }
        public string strCodigoRiesgo
        {
            get { return _CodigoRiesgo; }
            set { _CodigoRiesgo = value; }
        }
        public string strNombreRiesgo
        {
            get { return _NombreRiesgo; }
            set { _NombreRiesgo = value; }
        }
        public string strDescripcionRiesgo
        {
            get { return _DescripcionRiesgo; }
            set { _DescripcionRiesgo = value; }
        }
        public int intIdRiesgo
        {
            get { return _IdRiesgo; }
            set { _IdRiesgo = value; }
        }
        public string strCodigoControl
        {
            get { return _CodigoControl; }
            set { _CodigoControl = value; }
        }
        public string strNombreControl
        {
            get { return _NombreControl; }
            set { _NombreControl = value; }
        }
        #region Constructors
        public clsVerCaracterizacionIndicadorRiesgo()
        {
        }

        public clsVerCaracterizacionIndicadorRiesgo(string strNombreIndicador, string strDescripcionIndicador, int intIdIndicador, string strCodigoRiesgo, 
            string strNombreRiesgo, string strDescripcionRiesgo, int intIdRiesgo, string strCodigoControl, string strNombreControl)
        {
            this.strNombreIndicador = strNombreIndicador;
            this.strDescripcionIndicador = strDescripcionIndicador;
            this.intIdIndicador = intIdIndicador;
            this.strCodigoRiesgo = strCodigoRiesgo;
            this.strNombreRiesgo = strNombreRiesgo;
            this.strDescripcionRiesgo = strDescripcionRiesgo;
            this.intIdRiesgo = intIdRiesgo;
            this.strCodigoControl = strCodigoControl;
            this.strNombreControl = strNombreControl;
        }
        #endregion
    }
}