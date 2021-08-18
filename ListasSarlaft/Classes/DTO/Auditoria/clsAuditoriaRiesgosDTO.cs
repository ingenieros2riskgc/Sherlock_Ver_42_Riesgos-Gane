using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Auditoria
{
    public class clsAuditoriaRiesgosDTO
    {
        private int _IdAuditoriaRiesgo;
        private int _IdAuditoria;
        private int _IdRiesgo;
        private int _IdUsuario;
        public int intIdAuditoriaRiesgo
        {
            get { return _IdAuditoriaRiesgo; }
            set { _IdAuditoriaRiesgo = value; }
        }
        public int intIdAuditoria
        {
            get { return _IdAuditoria; }
            set { _IdAuditoria = value; }
        }
        public int intIdRiesgo
        {
            get { return _IdRiesgo; }
            set { _IdRiesgo = value; }
        }
        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public clsAuditoriaRiesgosDTO() { }
    }
}