using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes
{
    public class clsDTOParaPeriodicidad
    {
        #region Variables
        private int _IdPeriodicidad;
        private string _NombrePeriodicidad;
        private decimal _Valor;
        #endregion Variables
        #region Get/Set
        public int intIdPeriodicidad
        {
            get { return _IdPeriodicidad; }
            set { _IdPeriodicidad = value; }
        }
        public string strNombrePeriodicidad
        {
            get { return _NombrePeriodicidad; }
            set { _NombrePeriodicidad = value; }
        }
        public decimal dcValor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
        #endregion Get/Set
        #region Constructor
        public clsDTOParaPeriodicidad() { }
        #endregion Constructor
    }
}