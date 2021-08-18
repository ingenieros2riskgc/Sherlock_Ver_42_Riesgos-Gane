using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTORiesgosComparativoRiesgos
    {
        #region Variables
        private int _NumeroRegistro;
        private int _SumatoriaProbabilidad;
        private int _SumatoriaImpacto;
        private int _PromedioProbabilidad;
        private int _PromedioImpacto;
        private string _Perfil;
        #endregion Variables
        #region Get/Set
        public int intNumeroRegistro
        {
            get { return _NumeroRegistro; }
            set { _NumeroRegistro = value; }
        }
        public int intSumatoriaProbabilidad
        {
            get { return _SumatoriaProbabilidad; }
            set { _SumatoriaProbabilidad = value; }
        }
        public int intSumatoriaImpacto
        {
            get { return _SumatoriaImpacto; }
            set { _SumatoriaImpacto = value; }
        }
        public int intPromedioProbabilidad
        {
            get { return _PromedioProbabilidad; }
            set { _PromedioProbabilidad = value; }
        }
        public int intPromedioImpacto
        {
            get { return _PromedioImpacto; }
            set { _PromedioImpacto = value; }
        }
        public string strPerfil
        {
            get { return _Perfil; }
            set { _Perfil = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTORiesgosComparativoRiesgos() { }
        #endregion Constructor
    }
}