using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOCuadroComparativoRiesgos
    {
        #region Variables
        private string _ClasificacionRiesgo;
        private string _RiesgoInherente;
        private int _CantRiesgoInherente;
        private string _RiesgoResidual;
        private int _CantRiesgoResidual;
        private decimal _DecValoracion;
        #endregion Variables
        #region Get/Set
        public string strClasificacionRiesgo
        {
            get { return _ClasificacionRiesgo; }
            set { _ClasificacionRiesgo = value; }
        }
        public string strRiesgoInherente
        {
            get { return _RiesgoInherente; }
            set { _RiesgoInherente = value; }
        }
        public int intCantRiesgoInherente
        {
            get { return _CantRiesgoInherente; }
            set { _CantRiesgoInherente = value; }
        }
        public string strRiesgoResidual
        {
            get { return _RiesgoResidual; }
            set { _RiesgoResidual = value; }
        }
        public int intCantRiesgoResidual
        {
            get { return _CantRiesgoResidual; }
            set { _CantRiesgoResidual = value; }
        }
        public decimal decDecValoracion
        {
            get { return _DecValoracion; }
            set { _DecValoracion = value; }
        }
        #endregion
        #region Constructor
        public clsDTOCuadroComparativoRiesgos() { }
        #endregion Constructor
    }
}