using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Auditoria
{
    public class clsAuditoriaRiesgosControlesDTO
    {
        private int _IdAuditoriaRiesgoControl;
        private int _IdAuditoria;
        private int _IdRiesgo;
        private int _IdControl;
        private int _IdUsuario;

        public int intIdAuditoriaRiesgoControl
        {
            get { return _IdAuditoriaRiesgoControl; }
            set { _IdAuditoriaRiesgoControl = value; }
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
        public int intIdControl
        {
            get { return _IdControl; }
            set { _IdControl = value; }
        }
        public int intIdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public clsAuditoriaRiesgosControlesDTO() { }
    }
}