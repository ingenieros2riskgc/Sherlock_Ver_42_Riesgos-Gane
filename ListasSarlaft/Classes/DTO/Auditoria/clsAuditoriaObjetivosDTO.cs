using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListasSarlaft.Classes.DTO.Auditoria
{
    public class clsAuditoriaObjetivosDTO
    {
        private int _IdObjetivo;
        private int _IdEstandar;
        private int _Numero;
        private string _Nombre;
        private string _Descripcion;
        private int _IdUsuario;

        public int intIdObjetivo
        {
            set { _IdObjetivo = value; }
            get { return _IdObjetivo; }
        }
        public int intIdEstandar
        {
            set { _IdEstandar = value; }
            get { return _IdEstandar; }
        }
        public int intNumero
        {
            set { _Numero = value; }
            get { return _Numero; }
        }
        public string strNombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
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

        public clsAuditoriaObjetivosDTO() { }
    }
}