using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Auditoria
{
    public class clsAuditoriaIntroduccionDTO
    {
        private int _IdAuditoriaIntroduccion;
        private int _IdAuditoria;
        private string _Introduccion;
        private int _IdUsuario;
        public int intIdAuditoriaIntroduccion
        {
            set { _IdAuditoriaIntroduccion = value; }
            get { return _IdAuditoriaIntroduccion; }
        }
        public int intIdAuditoria
        {
            set { _IdAuditoria = value; }
            get { return _IdAuditoria; }
        }
        public string strIntroduccion
        {
            set { _Introduccion = value; }
            get { return _Introduccion; }
        }
        public int intIdUsuario
        {
            set { _IdUsuario = value; }
            get { return _IdUsuario; }
        }

        public clsAuditoriaIntroduccionDTO() { }
    }
}