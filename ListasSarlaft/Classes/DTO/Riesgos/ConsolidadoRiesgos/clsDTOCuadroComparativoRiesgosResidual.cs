using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Riesgos.ConsolidadoRiesgos
{
    public class clsDTOCuadroComparativoRiesgosResidual
    {
        #region Variables
        private int _NumeroRegistros;
        private int _IdProbabilidad;
        private int _IdImpacto;
        private string _Clasificacion;
        #endregion Variables
        #region Get/Set
        public int intNumeroRegistros
        {
            get { return _NumeroRegistros; }
            set { _NumeroRegistros = value; }
        }
        public int intIdProbabilidad
        {
            get { return _IdProbabilidad; }
            set { _IdProbabilidad = value; }
        }
        public int intIdImpacto
        {
            get { return _IdImpacto; }
            set { _IdImpacto = value; }
        }
        public string strClasificacion
        {
            get { return _Clasificacion; }
            set { _Clasificacion = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOCuadroComparativoRiesgosResidual() { }
        #endregion Constructor
    }
}