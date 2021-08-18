using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Auditoria
{
    public class clsRecomendacionDTO
    {
        private int _IdRecomendacion;
        private int _Numero;
        private int _IdHallazgo;
        private DateTime _FechaRegistro;
        private int _edadRecomendacion;

        public int intIdRecomendacion
        {
            get { return _IdRecomendacion; }
            set { _IdRecomendacion = value; }
        }
        public int intIdHallazgo
        {
            get { return _IdHallazgo; }
            set { _IdHallazgo = value; }
        }
        public DateTime dtFechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        public int intedadRecomendacion
        {
            get { return _edadRecomendacion; }
            set { _edadRecomendacion = value; }
        }
        public clsRecomendacionDTO() { }
    }
}