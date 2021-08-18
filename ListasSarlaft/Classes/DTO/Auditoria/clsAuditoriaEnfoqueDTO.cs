using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Auditoria
{
    public class clsAuditoriaEnfoqueDTO
    {
        private int _IdEnfoque;
        private int _IdObjetivo;
        private int _Numero;
        private string _Descripcion;
        private int _IdUsuario;
        public int intIdEnfoque
        {
            set { _IdEnfoque = value; }
            get { return _IdEnfoque; }
        }
        public int intIdObjetivo
        {
            set { _IdObjetivo = value; }
            get { return _IdObjetivo; }
        }
        
        public int intNumero
        {
            set { _Numero = value; }
            get { return _Numero; }
        }
        public string strDescripcion
        {
            set { _Descripcion = value; }
            get { return _Descripcion; }
        }
        public int intIdUsuario
        {
            set { _IdUsuario = value; }
            get { return _IdUsuario; }
        }

        public clsAuditoriaEnfoqueDTO() { }
    }
}